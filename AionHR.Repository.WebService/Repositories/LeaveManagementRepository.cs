using Infrastructure.Configuration;
using Model;
using Model.Attendance;
using Model.Dashboard;
using Model.Employees.Leaves;
using Model.LeaveManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.WebService.Repositories
{
    /// <summary>
    /// 
    /// </summary>
   public  class LeaveManagementRepository:Repository<VacationSchedule,string>, ILeaveManagementRepository
    {
        private string serviceName = "LM.asmx/";

        public LeaveManagementRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetLookup.Add(typeof(VacationSchedule), "getVS");
            ChildGetLookup.Add(typeof(VacationSchedulePeriod), "getVP");
            ChildGetLookup.Add(typeof(LeaveType), "getLT");
            ChildGetLookup.Add(typeof(LeaveRequest), "getLR");
            ChildGetLookup.Add(typeof(LeaveSchedule), "getLS");
            ChildGetLookup.Add(typeof(LeaveSchedulePeriod), "getLP");
            ChildGetLookup.Add(typeof(LeaveReturn), "getRE");
            

            ChildGetAllLookup.Add(typeof(VacationSchedule), "qryVS");
            ChildGetAllLookup.Add(typeof(VacationSchedulePeriod), "qryVP");
            ChildGetAllLookup.Add(typeof(AttendanceSchedule), "qrySC");
            ChildGetAllLookup.Add(typeof(AttendanceScheduleDay), "qrySD");
            ChildGetAllLookup.Add(typeof(AttendanceBreak), "qrySB");
            ChildGetAllLookup.Add(typeof(LeaveType), "qryLT");
            ChildGetAllLookup.Add(typeof(LeaveRequest), "qryLR");
            ChildGetAllLookup.Add(typeof(LeaveDay), "qryLD");
            ChildGetAllLookup.Add(typeof(PendingLA), "pendingLA");
            ChildGetAllLookup.Add(typeof(Approvals), "qryLA");
            ChildGetAllLookup.Add(typeof(LeaveSchedule), "qryLS");
            ChildGetAllLookup.Add(typeof(LeaveSchedulePeriod), "qryLP");
            ChildGetAllLookup.Add(typeof(LeaveReturn), "qryRE");
            ChildGetAllLookup.Add(typeof(LeaveReturnApproval), "qryRA");
            ChildGetAllLookup.Add(typeof(pendingRA), "pendingRA");

            ChildAddOrUpdateLookup.Add(typeof(VacationSchedule), "setVS");
            ChildAddOrUpdateLookup.Add(typeof(VacationSchedulePeriod[]), "arrVP");
            ChildAddOrUpdateLookup.Add(typeof(AttendanceSchedule), "setSC");
            ChildAddOrUpdateLookup.Add(typeof(AttendanceScheduleDay), "setSD");
            ChildAddOrUpdateLookup.Add(typeof(LeaveType), "setLT");
            ChildAddOrUpdateLookup.Add(typeof(LeaveRequest), "setLR");
            ChildAddOrUpdateLookup.Add(typeof(AttendanceBreak[]), "arrSB");
            ChildAddOrUpdateLookup.Add(typeof(LeaveDay), "setLD");
            ChildAddOrUpdateLookup.Add(typeof(DashboardLeave), "setLA");
            ChildAddOrUpdateLookup.Add(typeof(LeaveSchedule), "setLS");
            ChildAddOrUpdateLookup.Add(typeof(LeaveSchedulePeriod), "setLP");
            ChildAddOrUpdateLookup.Add(typeof(LeaveReturn), "setRE");
            ChildAddOrUpdateLookup.Add(typeof(LeaveReturnApproval), "setRA");
            ChildAddOrUpdateLookup.Add(typeof(SyncActivity), "syncLR");
            ChildAddOrUpdateLookup.Add(typeof(SyncLeaveReplacment), "syncRE");

            

            ChildDeleteLookup.Add(typeof(VacationSchedulePeriod), "delVP");
            ChildDeleteLookup.Add(typeof(LeaveDay), "delLD");
            ChildDeleteLookup.Add(typeof(AttendanceBreak), "delSB");
            ChildDeleteLookup.Add(typeof(VacationSchedule), "delVS");
            ChildDeleteLookup.Add(typeof(LeaveRequest), "delLR");
            ChildDeleteLookup.Add(typeof(LeaveType), "delLT");
            ChildDeleteLookup.Add(typeof(LeaveSchedule), "delLS");
            ChildDeleteLookup.Add(typeof(LeaveSchedulePeriod), "delLP");
            ChildDeleteLookup.Add(typeof(LeaveReturn), "delRE");
        }
    }
}
