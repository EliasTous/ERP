using AionHR.Infrastructure.Session;
using AionHR.Model.SelfService;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class SelfServiceService : BaseService, ISelfServiceService
    {
        private ISelfServiceRepository _selfServiceRepository;

        public SelfServiceService(ISelfServiceRepository complaintsRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _selfServiceRepository = complaintsRepository;
        }

        protected override dynamic GetRepository()
        {
            return _selfServiceRepository;
        }
    }
}
