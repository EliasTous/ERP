using AionHR.Infrastructure.Session;
using AionHR.Model.TaskSchedule;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class TaskScheduleService : BaseService, ITaskScheduleService
    {

        private ITaskScheduleRepository _TaskScheduleRepository;

        public TaskScheduleService(ITaskScheduleRepository complaintsRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _TaskScheduleRepository = complaintsRepository;
        }

        protected override dynamic GetRepository()
        {
            return _TaskScheduleRepository;
        }
    }
}
