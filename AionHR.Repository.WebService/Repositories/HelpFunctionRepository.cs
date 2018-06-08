using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
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
 public   class HelpFunctionRepository: Repository<IEntity, string>, IHelpFunctionRepository
    {
        private string serviceName = "HF.asmx/";

        public HelpFunctionRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(LocalsRate), "NQ01");
            ChildGetAllLookup.Add(typeof(LeaveCalendarDay), "TA02");
            ChildGetAllLookup.Add(typeof(BranchSchedule), "CS01");
            ChildGetAllLookup.Add(typeof(FlatScheduleBranchAvailability), "TA01");
            ChildGetAllLookup.Add(typeof(FlatScheduleEmployeeCell), "TA05");
            ChildGetAllLookup.Add(typeof(FlatScheduleWorkingHours), "TA06");



            ChildGetLookup.Add(typeof(MyInfo), "getEM1");
            ChildGetLookup.Add(typeof(BranchSchedule), "CS01");
           

            
            ChildAddOrUpdateLookup.Add(typeof(MyInfo), "setEM");
            ChildAddOrUpdateLookup.Add(typeof(FlatScheduleImport), "TA03");
            ChildAddOrUpdateLookup.Add(typeof(FlatBulkSchedule), "TA04");
            ChildAddOrUpdateLookup.Add(typeof(SynchronizeAttendanceDay), "TA07");
            ChildAddOrUpdateLookup.Add(typeof(GenerateAttendanceShift), "TA08");








            ChildDeleteLookup.Add(typeof(MyInfo), "delEM");
        }
    }
}
