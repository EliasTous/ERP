using Infrastructure.Configuration;
using Infrastructure.Domain;
using Model;
using Model.Attendance;
using Model.Dashboard;
using Model.Employees.Leaves;
using Model.Employees.Profile;
using Model.LeaveManagement;
using Model.LoadTracking;
using Model.Payroll;
using Model.SelfService;
using Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.WebService.Repositories
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
            ChildGetAllLookup.Add(typeof(TimeSelfService), "qryTA");
            ChildGetAllLookup.Add(typeof(LeaveType), "qryLT");
            ChildGetAllLookup.Add(typeof(loanSelfService), "qryLO");
            ChildGetAllLookup.Add(typeof(Dependant), "qryDE");
            ChildGetAllLookup.Add(typeof(EmployeeContact), "qryCO");
            ChildGetAllLookup.Add(typeof(EmployeeEmergencyContact), "qryEC");
            ChildGetAllLookup.Add(typeof(DashBoardTimeVariation), "qryTV");
            ChildGetAllLookup.Add(typeof(PayrollEntitlementDeduction), "qryED");
            ChildGetAllLookup.Add(typeof(PayrollSocialSecurity), "qryES");
            ChildGetAllLookup.Add(typeof(AdminDocTransfer), "qryTR");
            ChildGetAllLookup.Add(typeof(AssetLoan), "qryAL");
            ChildGetAllLookup.Add(typeof(GenerationHeader), "qryPE");
            ChildGetAllLookup.Add(typeof(LeaveReplacementApproval), "qryRA");

            ChildGetLookup.Add(typeof(MyInfo), "getEM1");
            ChildGetLookup.Add(typeof(EmployeeComplaintSelfService), "getCO");
            ChildGetLookup.Add(typeof(loanSelfService), "getLO");
            ChildGetLookup.Add(typeof(TimeSelfService), "getTA");
            ChildGetLookup.Add(typeof(UserInfoSelfService), "getUS");
            ChildGetLookup.Add(typeof(LeaveRequest), "getLR");
            ChildGetLookup.Add(typeof(EmployeeQuickView), "getQV");
            //ChildGetLookup.Add(typeof(KeyValuePair<string, string>), "getDE");
            ChildGetLookup.Add(typeof(Dependant), "getDE");
            ChildGetLookup.Add(typeof(EmployeeContact), "getCO");
            ChildGetLookup.Add(typeof(EmployeeEmergencyContact), "getEC");
            ChildGetLookup.Add(typeof(GenerationHeader), "getPE");
            ChildGetLookup.Add(typeof(LeaveReplacementApproval), "getRA");

            ChildAddOrUpdateLookup.Add(typeof(MyInfo), "setEM");
            ChildAddOrUpdateLookup.Add(typeof(UserInfoSelfService), "setUS");
            ChildAddOrUpdateLookup.Add(typeof(LeaveRequest), "setLR");
            ChildAddOrUpdateLookup.Add(typeof(loanSelfService), "setLO");
            ChildAddOrUpdateLookup.Add(typeof(LetterSelfservice), "setLE");
            ChildAddOrUpdateLookup.Add(typeof(LeaveDay[]), "arrLD");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeComplaintSelfService), "setCO");
            ChildAddOrUpdateLookup.Add(typeof(TimeSelfService), "setTA");
            ChildAddOrUpdateLookup.Add(typeof(Dependant), "setDE");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeContact), "setCO");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeEmergencyContact), "setEC");
            ChildAddOrUpdateLookup.Add(typeof(DashBoardTimeVariation), "setTV");
            ChildAddOrUpdateLookup.Add(typeof(AttendanceShift), "setAS");
            ChildAddOrUpdateLookup.Add(typeof(AdminDocTransfer), "setTR");
            ChildAddOrUpdateLookup.Add(typeof(GenerationHeader), "setPE");
            ChildAddOrUpdateLookup.Add(typeof(LeaveReplacementApproval), "setRA");

            ChildDeleteLookup.Add(typeof(MyInfo), "delEM");
            ChildDeleteLookup.Add(typeof(EmployeeComplaintSelfService), "delCO");
            ChildDeleteLookup.Add(typeof(Dependant), "delDE");
            ChildDeleteLookup.Add(typeof(EmployeeContact), "delCO");
            ChildDeleteLookup.Add(typeof(EmployeeEmergencyContact), "delEC");
            ChildDeleteLookup.Add(typeof(leaveRequetsSelfservice), "delLR");
            ChildDeleteLookup.Add(typeof(loanSelfService), "delLO");
            ChildDeleteLookup.Add(typeof(AttendanceShift), "delAS");
            ChildDeleteLookup.Add(typeof(GenerationHeader), "delPE");
            ChildDeleteLookup.Add(typeof(LeaveReplacementApproval), "delRA");
        }
        }
    
}
