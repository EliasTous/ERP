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
            GetAllMethodName = "qryCA";
            AddOrUpdateMethodName = "setCA";
            GetRecordMethodName = "getCA";
        }
    }
}
