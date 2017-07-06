using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Company.Structure;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class DepartmentBatchRunner : ImportBatchRunner<Department>
    {
        public DepartmentBatchRunner(ISessionStorage store, ISystemService system, ICompanyStructureService main) :base(system,main)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


          
            BatchStatus = new BatchOperationStatus() { classId = ClassId.CSDE, processed = 0, tableSize = 0, status = 0 };
            
        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.departmentRef + ","
                    + error.name + "," +
                   
                    errorMessages[i++].Replace('\r', ' ').Replace(',', ';')

                    );

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElement(Department item)
        {
           
        }
    }

}
