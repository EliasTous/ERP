using Infrastructure.Session;
using Model.SelfService;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
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
