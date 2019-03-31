using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Reports;
using AionHR.Model.TimeAttendance;
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
            ChildGetAllLookup.Add(typeof(RT108), "RT108");
            ChildGetAllLookup.Add(typeof(RT109), "RT109");
            ChildGetAllLookup.Add(typeof(RT110), "RT110");
            ChildGetAllLookup.Add(typeof(RT111), "RT111");
            ChildGetAllLookup.Add(typeof(RT112), "RT112");
            ChildGetAllLookup.Add(typeof(RT113), "RT113");
            ChildGetAllLookup.Add(typeof(RT114), "RT114");
            ChildGetAllLookup.Add(typeof(RT115), "RT115");


            ChildGetAllLookup.Add(typeof(RT200), "RT200");
            ChildGetAllLookup.Add(typeof(RT201), "RT201");
            ChildGetAllLookup.Add(typeof(RT202), "RT202");
            ChildGetAllLookup.Add(typeof(RT203), "RT203");
            


            ChildGetAllLookup.Add(typeof(RT301), "RT301");
            ChildGetAllLookup.Add(typeof(RT302), "RT302");
            ChildGetAllLookup.Add(typeof(RT303), "RT303");
            ChildGetAllLookup.Add(typeof(RT304), "RT304");
            ChildGetAllLookup.Add(typeof(Model.Reports.RT305), "RT305");
            ChildGetAllLookup.Add(typeof(RT306), "RT306");
            ChildGetAllLookup.Add(typeof(AionHR.Model.Reports.RT307), "RT307");
            ChildGetAllLookup.Add(typeof(AionHR.Model.Reports.RT309), "RT309");

            ChildGetAllLookup.Add(typeof(RT401), "RT401");
            ChildGetAllLookup.Add(typeof(RT402), "RT402");

            ChildGetAllLookup.Add(typeof(RT501), "RT501");
            ChildGetAllLookup.Add(typeof(PT501), "PT501");



            ChildGetAllLookup.Add(typeof(RT502), "RT502");
            ChildGetAllLookup.Add(typeof(RT503), "RT503");
            ChildGetAllLookup.Add(typeof(RT504), "RT504");
            ChildGetAllLookup.Add(typeof(RT506), "RT506");
            ChildGetAllLookup.Add(typeof(RT507), "RT507");
            ChildGetAllLookup.Add(typeof(RT508), "RT508");
        
            ChildGetAllLookup.Add(typeof(RT601), "RT601");
            ChildGetAllLookup.Add(typeof(RT602), "RT602");

            ChildGetAllLookup.Add(typeof(RT801), "RT801");
            ChildGetAllLookup.Add(typeof(RT802), "RT802");
            ChildGetAllLookup.Add(typeof(RT803), "RT803");

        }
    }
}
