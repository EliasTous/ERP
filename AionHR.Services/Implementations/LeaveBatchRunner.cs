using Infrastructure.Domain;
using Infrastructure.Session;
using Infrastructure.Tokens;
using Model.Employees.Leaves;
using Model.Employees.Profile;
using Model.LeaveManagement;
using Model.System;
using Services.Interfaces;
using Services.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
   public class LeaveBatchRunner : ImportBatchRunner<LeaveRequest>
    {
        IEmployeeService _employeeService;
        ILeaveManagementService _leaveManagementService;

        Dictionary<string, string> ids;
        Dictionary<string, string> idL;
        public LeaveBatchRunner(ISessionStorage store, ISystemService system, ILeaveManagementService main, IEmployeeService employeeService) :base(system,main)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


            this._employeeService = employeeService;
            this._leaveManagementService = main;
            BatchStatus = new BatchOperationStatus() { classId = ClassId.LMLR, processed = 0, tableSize = 0, status = 0 };
            ids = new Dictionary<string, string>();
            idL = new Dictionary<string, string>();
        }
        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeRef + ","
                    + error.startDate + "," +
                    error.endDate + "," +
                   error.returnDate + "," +
                   error.ltRef + "," +
                    error.destination + "," +
                    error.justification + "," +
                    error.apStatus +","+
                    
                    errorMessages[i++].Replace('\r', ' ').Replace(',', ';')

                    );

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

       
        private string GetEmployeeId(string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = _employeeService.ChildGetRecord<Employee>(req);
            if (resp == null || resp.result == null)
                return employeeRef;
            else
                return resp.result.recordId;
        }
        private string GetltId(string ltRef)
        {
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<LeaveType> response = _leaveManagementService.ChildGetAll<LeaveType>(request);
            if (response == null || response.Items == null)
                return ltRef;

            if (response.Items.Where(x => x.reference == ltRef).ToArray() == null)
                return ltRef;
            else
            {
                if (response.Items.Where(x => x.reference == ltRef).ToList().Count() != 0)
                    return response.Items.Where(x => x.reference == ltRef).ToList().First().recordId;
                else
                    return ltRef;
            }



        }


        protected override void PreProcessElement(LeaveRequest item)
        {
            if (string.IsNullOrEmpty(item.employeeRef)&& string.IsNullOrEmpty(item.ltRef))
                return;
            if (!ids.ContainsKey(item.employeeRef))
                ids.Add(item.employeeRef, GetEmployeeId(item.employeeRef));
            item.employeeId = ids[item.employeeRef];
          
            if (!idL.ContainsKey(item.ltRef))
                idL.Add(item.ltRef, GetltId(item.ltRef));
            item.ltId = Convert.ToInt32(idL[item.ltRef]);
        }
    }
}
