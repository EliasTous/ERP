using Infrastructure.Session;
using Model.Benefits;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
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
