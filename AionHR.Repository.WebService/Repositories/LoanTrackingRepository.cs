﻿using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.LoadTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
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
            ChildGetLookup.Add(typeof(LoanOverride), "getEM");
            ChildGetAllLookup.Add(typeof(LoanType), "qryLT");
            ChildGetAllLookup.Add(typeof(LoanOverride), "qryEM");
            ChildAddOrUpdateLookup.Add(typeof(LoanType), "setLT");
            ChildDeleteLookup.Add(typeof(LoanType), "delLT");
            ChildDeleteLookup.Add(typeof(LoanOverride), "delEM");

            ChildGetAllLookup.Add(typeof(LoanComment), "qryLC");
            ChildAddOrUpdateLookup.Add(typeof(LoanComment), "setLC");
            ChildAddOrUpdateLookup.Add(typeof(LoanOverride), "setEM");
            ChildDeleteLookup.Add(typeof(LoanComment), "delLC");


        }
    }
}
