﻿using Infrastructure.Domain;
using Infrastructure.Session;
using Infrastructure.Tokens;
using Model.Employees.Profile;
using Model.LoadTracking;
using Model.System;
using Model.TimeAttendance;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
  public  class LoansBatchRunner : ImportBatchRunner<Loan>
    {
        IEmployeeService _employeeService;

        
        Dictionary<string, string> ids;
        Dictionary<string, string>idc;

        public LoansBatchRunner(ISessionStorage store, ISystemService system,ILoanTrackingService main, IEmployeeService employeeService) :base(system,main)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());

            ids = new Dictionary<string, string>();
            idc = new Dictionary<string, string>();
            this._employeeService = employeeService;
            BatchStatus = new BatchOperationStatus() { classId = ClassId.LTLR, processed = 0, tableSize = 0, status = 0 };
            errors = new List<Loan>();
        }
        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeRef + ","
                    + error.amount + "," +
                    error.ltId + "," +
                    error.branchId+"," +
                    error.currencyRef + "," +
                    error.purpose + "," +
                    error.apStatus+","+
                    error.date + "," +
                    error.effectiveDate + "," +
                    error.loanRef + "," +
                     error.ldMethod + "," +
                      error.ldValue + "," +
                   errorMessages[i++].Replace('\r', ' ').Replace(',', ';')
                    );

            }
           
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

      

        protected override void PreProcessElement(Loan item)
        {
            if (string.IsNullOrEmpty(item.employeeRef))
                return;
            if (!ids.ContainsKey(item.employeeRef))
                ids.Add(item.employeeRef, GetEmployeeId(item.employeeRef));
            if (!idc.ContainsKey(item.currencyRef))
                idc.Add(item.currencyRef, GetCurrencyId(item.currencyRef));
            //item.currencyId = idc[item.currencyRef];
            item.employeeId = ids[item.employeeRef];
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
        private string GetCurrencyId(string CurrencyRef)
        {
            CurrencyByReferanceRecordRequest req = new CurrencyByReferanceRecordRequest();
            req.Reference = CurrencyRef;
            RecordResponse<CurrencyByRef> resp = _systemService.ChildGetRecord<CurrencyByRef>(req);
            if (resp == null || resp.result == null)
                return CurrencyRef;
            else
                return resp.result.recordId;
        }


        protected override void ProcessElement(Loan item)
        {
            PostRequest<Loan> req = new PostRequest<Loan>();
            req.entity = item;
            req.entity.currencyId = 1;

            PostResponse<Loan> resp = base.service.AddOrUpdate<Loan>(req);
            if (!resp.Success)
            {
                errors.Add(item);
                errorMessages.Add(resp.Summary);
            }
        }
    }
}
