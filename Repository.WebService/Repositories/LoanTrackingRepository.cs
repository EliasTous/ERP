using Infrastructure.Configuration;
using Infrastructure.Domain;
using Model;
using Model.LeaveManagement;
using Model.LoadTracking;
using Model.SelfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.WebService.Repositories
{
    public class LoanTrackingRepository:Repository<Loan,string>,ILoanTrackingRepository
    {
        private string serviceName = "LT.asmx/";

        public LoanTrackingRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
            GetAllMethodName = "qryLR";
            AddOrUpdateMethodName = "setLR";
            GetRecordMethodName = "getLR";
            DeleteMethodName = "delLR";

            ChildGetLookup.Add(typeof(LoanType), "getLT");
            ChildGetLookup.Add(typeof(LoanDeduction), "getLD");
            ChildGetLookup.Add(typeof(LoanOverride), "getEM");
            ChildGetAllLookup.Add(typeof(LoanType), "qryLT");
            ChildGetAllLookup.Add(typeof(Model.LoadTracking.PendingLA), "pendingLA");
            ChildGetAllLookup.Add(typeof(LoanApproval), "qryLA");
            ChildGetAllLookup.Add(typeof(LoanOverride), "qryEM");


            ChildAddOrUpdateLookup.Add(typeof(LoanType), "setLT");
            ChildAddOrUpdateLookup.Add(typeof(LoanApproval), "setLA");
            ChildAddOrUpdateLookup.Add(typeof(SyncActivity), "syncLR");


            ChildDeleteLookup.Add(typeof(LoanType), "delLT");
            ChildDeleteLookup.Add(typeof(LoanOverride), "delEM");
            ChildDeleteLookup.Add(typeof(LoanDeduction), "delLD");

            ChildGetAllLookup.Add(typeof(LoanComment), "qryLC");
            ChildGetAllLookup.Add(typeof(LoanDeduction), "qryLD");
            ChildAddOrUpdateLookup.Add(typeof(LoanComment), "setLC");
            ChildAddOrUpdateLookup.Add(typeof(LoanDeduction), "setLD");
            ChildAddOrUpdateLookup.Add(typeof(LoanOverride), "setEM");
            ChildDeleteLookup.Add(typeof(LoanComment), "delLC");
            ChildDeleteLookup.Add(typeof(loanSelfService), "delLC");


        }
    }
}
