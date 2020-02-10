using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TaskSchedule
{
    public interface ITaskScheduleRepository : IRepository<IEntity, string>
    {
    }
}
