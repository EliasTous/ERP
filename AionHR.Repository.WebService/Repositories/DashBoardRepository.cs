using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Benefits;
using AionHR.Model.Company.Structure;
using AionHR.Model.Dashboard;
using AionHR.Model.HelpFunction;
using AionHR.Model.SelfService;
using AionHR.Model.System;
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
            ChildGetAllLookup.Add(typeof(DashBoardPL), "qryPL");
            ChildGetAllLookup.Add(typeof(DashBoardUL), "qryUL");
            ChildGetAllLookup.Add(typeof(DashBoardTimeVariation), "qryTV");
            ChildGetAllLookup.Add(typeof(AttendancePeriod), "qryAP");
            ChildGetAllLookup.Add(typeof(DashboardItem), "dashBoard");
            ChildGetAllLookup.Add(typeof(DashboardAlertItem), "qryAA");
            ChildGetAllLookup.Add(typeof(DashboardBranchAvailability), "qryBA");
           

            ChildGetAllLookup.Add(typeof(CompanyRTW), "qryCR");
            ChildGetAllLookup.Add(typeof(EmpRTW), "qryER");
            ChildGetAllLookup.Add(typeof(SalaryChange), "qrySC");
            ChildGetAllLookup.Add(typeof(ProbationEnd), "qryPR");
            ChildGetAllLookup.Add(typeof(WorkAnniversary), "qryWA");
            ChildGetAllLookup.Add(typeof(EmployeeBirthday), "qryBD");
            ChildGetAllLookup.Add(typeof(RetirementAge), "qryRS");
            ChildGetAllLookup.Add(typeof(TermEndDate), "qryTE");
            ChildGetAllLookup.Add(typeof(LeavingSoon), "qryLS");
            ChildGetAllLookup.Add(typeof(EmploymentReview), "qryRE");


            ChildGetLookup.Add(typeof(MyInfo), "getEM1");
           



            ChildAddOrUpdateLookup.Add(typeof(MyInfo), "setEM");
            ChildAddOrUpdateLookup.Add(typeof(DashBoardTimeVariation), "setTV");

            







            ChildDeleteLookup.Add(typeof(MyInfo), "delEM");
        }
    }
}
