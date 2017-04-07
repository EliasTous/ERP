using AionHR.Infrastructure.Configuration;
using AionHR.Model.Company.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
   public  class CompanyNewsRepository:Repository<News,string>,ICompanyNewsRepository
    {
        private string serviceName = "CN.asmx/";

        public CompanyNewsRepository()
        {

            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

          
            base.GetAllMethodName = "qryNW";
            base.GetRecordMethodName = "getNW";
            base.DeleteMethodName = "delNW";
            base.AddOrUpdateMethodName = "setNW";
        }
    }
}
