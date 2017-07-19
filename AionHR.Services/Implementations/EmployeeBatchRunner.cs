using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
   public class EmployeeBatchRunner:ImportBatchRunner<Employee>
    {
        public EmployeeBatchRunner(ISessionStorage store, IEmployeeService employee, ISystemService system):base(system, employee)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());

            
            BatchStatus = new BatchOperationStatus() { classId = ClassId.EPEM, processed = 0, tableSize = 0, status = 0 };
            errors = new List<Employee>();
        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.reference + "," + error.firstName + "," + error.lastName+ "," + error.hireDate + "," + error.caId + "," + errorMessages[i++].Replace('\r', ' ').Replace(',', ';'));

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElement(Employee item)
        {
            
        }

        protected override void ProcessElement(Employee item)
        {
            PostRequest<Employee> req = new PostRequest<Employee>();
            req.entity = item;

            PostResponse<Employee> resp = service.AddOrUpdate<Employee>(req);
            if (!resp.Success)
            {
                errors.Add(item);
                errorMessages.Add(resp.Summary);
            }
        }
    }
}
