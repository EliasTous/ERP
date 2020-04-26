using Infrastructure.Session;
using Model.NationalQuota;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
   public class NationalQuotaService : BaseService, INationalQuotaService
    {
        private INationalQuotaRepository _nationalQuotaService;

        public NationalQuotaService(INationalQuotaRepository complaintsRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _nationalQuotaService = complaintsRepository;
        }

        protected override dynamic GetRepository()
        {
            return _nationalQuotaService;
        }

    }
}
