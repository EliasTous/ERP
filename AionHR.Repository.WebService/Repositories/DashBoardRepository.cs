using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Benefits;
using AionHR.Model.Company.Structure;
using AionHR.Model.Dashboard;
using AionHR.Model.HelpFunction;
using AionHR.Model.SelfService;
using AionHR.Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
  public  class DashBoardRepository : Repository<IEntity, string>, IDashBoardRepository
    {
        private string serviceName = "DB.asmx/";

        public DashBoardRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(DashBoardDO), "qryDO");

            ChildGetAllLookup.Add(typeof(DashboardLW), "qryLW");
            ChildGetAllLookup.Add(typeof(DashboardNS), "qryNS");
            ChildGetAllLookup.Add(typeof(DashBoardPE), "qryPE");
            ChildGetAllLookup.Add(typeof(DashBoardLE), "qryLE");
            ChildGetAllLookup.Add(typeof(DashBoardCH), "qryCH");
            ChildGetAllLookup.Add(typeof(DashBoardTimeVariation), "qryTV");






            ChildGetLookup.Add(typeof(MyInfo), "getEM1");
           



            ChildAddOrUpdateLookup.Add(typeof(MyInfo), "setEM");
           








            ChildDeleteLookup.Add(typeof(MyInfo), "delEM");
        }
    }
}
