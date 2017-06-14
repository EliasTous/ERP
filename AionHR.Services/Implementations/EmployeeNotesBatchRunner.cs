using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
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
        Dictionary<string, string> ids;
        Dictionary<string, string> userids;
        public EmployeeNotesBatchRunner(ISessionStorage store, IEmployeeService attendance, ISystemService system, IEmployeeService employee) : base(system, attendance)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());
            ids = new Dictionary<string, string>();
            userids = new Dictionary<string, string>();
            this.main = employee;
            BatchStatus = new BatchOperationStatus() { classId = ClassId.EPNO, processed = 0, tableSize = 0, status = 0 };

        }
       

        protected override void PreProcessElement(EmployeeNote item)
        {
            if (!string.IsNullOrEmpty(item.employeeRef))
            {
                if (!ids.ContainsKey(item.employeeRef))
                    ids.Add(item.employeeRef, GetEmployeeId(item.employeeRef));
                item.employeeId = ids[item.employeeRef];
            }
            if (!string.IsNullOrEmpty(item.userName))
            {
                if (!userids.ContainsKey(item.userName))
                    userids.Add(item.userName, GetUserByEmail(item.userName));
                item.userId = userids[item.userName];
            }

        }
        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeRef+","+ error.note+"," + error.userName+","+ errorMessages[i++].Replace('\r', ' ').Replace(',', ';'));

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
                return employeeRef;
            else
                return resp.result.recordId;
        }

        private string GetUserByEmail(string email)
        {
            UserByEmailRequest req = new UserByEmailRequest();
            req.Email = email;
            RecordResponse<UserInfo> resp = base._systemService.Get<UserInfo>(req);
            if (resp == null || resp.result == null)
                return "0";
            else
                return resp.result.recordId;
        }
    }
}
