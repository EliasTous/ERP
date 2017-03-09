using AionHR.Infrastructure.Domain;
using AionHR.Model.Employees.Leaves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LeaveManagement
{
    public interface ILeaveManagementRepository:IRepository<VacationSchedule,string>
    {
    }
}
