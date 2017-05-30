using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
    public class ReportsRepository:Repository<IEntity,string>,IReportsRepository
    {
        private string serviceName = "RT.asmx/";
        public ReportsRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(RT01), "RT101");
            ChildGetAllLookup.Add(typeof(RT102A), "RT102A");
            ChildGetAllLookup.Add(typeof(RT102B), "RT102B");
            ChildGetAllLookup.Add(typeof(RT103), "RT103");
            ChildGetAllLookup.Add(typeof(RT104), "RT104");
            ChildGetAllLookup.Add(typeof(RT105), "RT105");
            ChildGetAllLookup.Add(typeof(RT106), "RT106");
            ChildGetAllLookup.Add(typeof(RT107), "RT107");
            ChildGetAllLookup.Add(typeof(RT107B), "RT107b");

            ChildGetAllLookup.Add(typeof(RT201), "RT201");
            ChildGetAllLookup.Add(typeof(RT202), "RT202");
            ChildGetAllLookup.Add(typeof(RT203), "RT203");

            ChildGetAllLookup.Add(typeof(RT301), "RT301");
            ChildGetAllLookup.Add(typeof(RT302), "RT302");

            ChildGetAllLookup.Add(typeof(RT401), "RT401");

            ChildGetAllLookup.Add(typeof(RT501), "RT501");

            ChildGetAllLookup.Add(typeof(RT601), "RT601");
            ChildGetAllLookup.Add(typeof(RT602), "RT602");

            ChildGetAllLookup.Add(typeof(RT801), "RT801");
            ChildGetAllLookup.Add(typeof(RT802), "RT802");

        }
    }
}
