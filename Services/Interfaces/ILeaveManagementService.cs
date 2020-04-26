using Model.Employees.Leaves;
using Model.LeaveManagement;
using Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILeaveManagementService:IBaseService
    {
        PostResponse<VacationSchedulePeriod> DeleteVacationSchedulePeriods(int vacationScheduleId);

        //PostResponse<LeaveDay> DeleteLeaveDays(int leaveId);
    }
}
