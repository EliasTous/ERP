using AionHR.Infrastructure.Session;
using AionHR.Model.Access_Control;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class AccessControlService : BaseService, IAccessControlService
    {
        private IAccessControlRepository repository;
        public AccessControlService(SessionHelper helper,IAccessControlRepository repository):base(helper)
        {
            this.repository = repository;
        }
        protected override dynamic GetRepository()
        {
            return repository;
        }
    }
}
