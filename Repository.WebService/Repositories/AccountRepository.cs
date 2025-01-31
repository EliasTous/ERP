﻿using Infrastructure.Configuration;
using Model.MasterModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.WebService;

namespace Repository.WebService.Repositories
{
    /// <summary>
    /// Class that handle the communcation between the model and the webservice. it encapsultes all the account related methods
    /// </summary>
    /// 
 
   public class AccountRepository :Repository<Account,string>, IAccountRepository
    {
        // the webservice name       
        private string serviceName = "MA.asmx/";
        public AccountRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
            base.GetRecordMethodName = "getAC";
          //  ChildAddOrUpdateLookup.Add(typeof(Registration), "setRE");
            ChildAddOrUpdateLookup.Add(typeof(Account), "setAC");
            ChildAddOrUpdateLookup.Add(typeof(DbSetup), "setDB");


           // ChildDeleteLookup.Add(typeof(Registration), "delRE");
            ChildDeleteLookup.Add(typeof(Account), "delAC");

        }

        public RecordWebServiceResponse<Account> RequestAccountRecovery(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + "reqAN";
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.GetAsync<RecordWebServiceResponse<Account>>();
        }
    }
}
