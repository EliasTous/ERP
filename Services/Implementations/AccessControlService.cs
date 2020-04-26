using Infrastructure.Domain;
using Infrastructure.Session;
using Model.Access_Control;
using Model.Company.Structure;
using Model.Employees.Profile;
using Model.System;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
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
