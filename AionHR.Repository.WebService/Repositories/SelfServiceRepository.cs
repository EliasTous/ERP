using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Attendance;
using AionHR.Model.Dashboard;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Model.LeaveManagement;
using AionHR.Model.LoadTracking;
using AionHR.Model.Payroll;
using AionHR.Model.SelfService;
using AionHR.Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
   
        public class SelfServiceRepository : Repository<IEntity, string>, ISelfServiceRepository
        {
            private string serviceName = "SS.asmx/";

            public SelfServiceRepository()
            {
                base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

                ChildGetAllLookup.Add(typeof(MyInfo), "qryEM");
            ChildGetAllLookup.Add(typeof(LetterSelfservice), "qryLE");
            ChildGetAllLookup.Add(typeof(AssetAllowanceSelfService), "qryAA");
            ChildGetAllLookup.Add(typeof(EmployeeComplaintSelfService), "qryCO");
            ChildGetAllLookup.Add(typeof(LeaveDaySelfservice), "qryLD");
            ChildGetAllLookup.Add(typeof(leaveRequetsSelfservice), "qryLR");
            ChildGetAllLookup.Add(typeof(FlatScheduleSelfService), "qryFS");
            ChildGetAllLookup.Add(typeof(AttendanceDay), "qryAD");
            ChildGetAllLookup.Add(typeof(TimeVariationSelfService), "qryTV");
            ChildGetAllLookup.Add(typeof(EmployeePayrollSelfService), "qryPE");
      
            ChildGetAllLookup.Add(typeof(LeaveType), "qryLT");



            ChildGetLookup.Add(typeof(MyInfo), "getEM1");
            ChildGetLookup.Add(typeof(EmployeeComplaintSelfService), "getCO");
            ChildGetLookup.Add(typeof(Loan), "getLO");

            ChildGetLookup.Add(typeof(UserInfoSelfService), "getUS");
            ChildGetLookup.Add(typeof(LeaveRequest), "getLR");
            ChildGetLookup.Add(typeof(EmployeeQuickView), "getQV");
            ChildGetLookup.Add(typeof(KeyValuePair<string, string>), "getDE");

            ChildAddOrUpdateLookup.Add(typeof(MyInfo), "setEM");
            ChildAddOrUpdateLookup.Add(typeof(UserInfoSelfService), "setUS");
            ChildAddOrUpdateLookup.Add(typeof(leaveRequetsSelfservice), "setLR");
            ChildAddOrUpdateLookup.Add(typeof(loanSelfService), "setLO");
            ChildAddOrUpdateLookup.Add(typeof(LetterSelfservice), "setLE");
            ChildAddOrUpdateLookup.Add(typeof(LeaveDay[]), "arrLD");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeComplaintSelfService), "setCO");

            ChildDeleteLookup.Add(typeof(MyInfo), "delEM");
            ChildDeleteLookup.Add(typeof(EmployeeComplaintSelfService), "delCO");
        }
        }
    
}
