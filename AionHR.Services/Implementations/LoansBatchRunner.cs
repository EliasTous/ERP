using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Employees.Profile;
using AionHR.Model.LoadTracking;
using AionHR.Model.System;
using AionHR.Model.TimeAttendance;
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
  public  class LoansBatchRunner : ImportBatchRunner<Loan>
    {
        IEmployeeService _employeeService;

        private List<Loan> errors;
        public LoansBatchRunner(ISessionStorage store, ISystemService system,ILoanTrackingService main, IEmployeeService employeeService) :base(system,main)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


            this._employeeService = employeeService;
            BatchStatus = new BatchOperationStatus() { classId = ClassId.LTLR, processed = 0, tableSize = 0, status = 0 };
            errors = new List<Loan>();
        }
        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeRef + ","
                    + error.amount + "," +
                    error.ltId + "," +
                    error.branchId+"," +
                    error.currencyRef + "," +
                    error.purpose + "," +
                    error.status+","+
                    error.date + "," +
                    error.effectiveDate + "," +
                    error.loanRef + "," 
                    );

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElements()
        {
            Dictionary<string, string> ids = new Dictionary<string, string>();
            foreach (var item in Items)
            {
                if (string.IsNullOrEmpty(item.employeeRef))
                    continue;
                if (!ids.ContainsKey(item.employeeRef))
                    ids.Add(item.employeeRef, GetEmployeeId(item.employeeRef));
                item.employeeId = ids[item.employeeRef];


            }
        }
        private string GetEmployeeId(string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = _employeeService.ChildGetRecord<Employee>(req);
            if (resp == null || resp.result == null)
                return "";
            else
                return resp.result.recordId;
        }

        protected override void ProcessElement(Loan item)
        {
            PostRequest<Loan> req = new PostRequest<Loan>();
            req.entity = item;

            PostResponse<Loan> resp = base.service.AddOrUpdate<Loan>(req);
            if (!resp.Success)
            {
                errors.Add(item);
            }
        }
    }
}
