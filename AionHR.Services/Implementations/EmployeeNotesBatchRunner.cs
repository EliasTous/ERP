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
    public class EmployeeNotesBatchRunner : ImportBatchRunner<EmployeeNote>
    {
        private IEmployeeService main;
        public EmployeeNotesBatchRunner(ISessionStorage store, IEmployeeService attendance, ISystemService system, IEmployeeService employee) : base(system, attendance)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


            this.main = employee;
            BatchStatus = new BatchOperationStatus() { classId = ClassId.EPNO, processed = 0, tableSize = 0, status = 0 };

        }
        protected override void PreProcessElements()
        {
            Dictionary<string, string> ids = new Dictionary<string, string>();
            Dictionary<string, string> userids = new Dictionary<string, string>();
            foreach (var item in Items)
            {
                if (!string.IsNullOrEmpty(item.employeeRef))
                {
                    if (!ids.ContainsKey(item.employeeRef))
                        ids.Add(item.employeeRef, GetEmployeeId(item.employeeRef));
                    item.employeeId = ids[item.employeeRef];
                }
                if(!string.IsNullOrEmpty(item.userName))
                {
                    if (!userids.ContainsKey(item.userName))
                        userids.Add(item.userName, GetUserByEmail(item.userName));
                    item.userId = userids[item.userName];
                }
                

            }
        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeRef+","+ error.note+"," + error.userName);

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }
        private string GetEmployeeId(string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = main.ChildGetRecord<Employee>(req);
            if (resp == null || resp.result == null)
                return "";
            else
                return resp.result.recordId;
        }

        private string GetUserByEmail(string email)
        {
            return "";
        }
    }
}
