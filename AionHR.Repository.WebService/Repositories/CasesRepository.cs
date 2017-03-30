using AionHR.Infrastructure.Configuration;
using AionHR.Model.Company.Cases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
    public class CasesRepository:Repository<Case,string>, ICasesRepository
    {
        private string serviceName = "CM.asmx/";

        public CasesRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
            GetAllMethodName = "qryCA";
            AddOrUpdateMethodName = "setCA";
            GetRecordMethodName = "getCA";
            DeleteMethodName = "delCA";

            ChildGetAllLookup.Add(typeof(CaseComment), "qryCC");

            ChildAddOrUpdateLookup.Add(typeof(CaseComment), "setCC");

            ChildDeleteLookup.Add(typeof(CaseComment), "delCC");

        }
    }
}
