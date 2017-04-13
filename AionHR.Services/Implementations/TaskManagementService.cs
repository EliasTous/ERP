using AionHR.Infrastructure.Session;
using AionHR.Model.TaskManagement;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class TaskManagementService : BaseService, ITaskManagementService
    {

        ITaskManagementRepository _repository;

        public TaskManagementService(ITaskManagementRepository repo, SessionHelper helper):base(helper)
        {
            _repository = repo;
        }

        protected override dynamic GetRepository()
        {
            return _repository;
        }
    }
}
