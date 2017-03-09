using AionHR.Model.Employees.Leaves;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Interfaces
{
    public interface ILeaveManagementService:IBaseService
    {
        PostResponse<VacationSchedulePeriod> DeleteVacationSchedulePeriods(int vacationScheduleId);

    }
}
