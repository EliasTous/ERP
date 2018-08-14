using AionHR.Infrastructure.Session;
using AionHR.Model.Benefits;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
  public  class BenefitsService : BaseService, IBenefitsService
    {
        private IBenefitsRepository _benefitsRepository;

        public BenefitsService(IBenefitsRepository complaintsRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _benefitsRepository = complaintsRepository;
        }

        protected override dynamic GetRepository()
        {
            return _benefitsRepository;
        }
    }
}
