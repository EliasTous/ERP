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
    public class MathematicalService : BaseService, IMathematicalService
    {
        private IMathematicalRepository _mathematicalService;

        public MathematicalService(IMathematicalRepository complaintsRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _mathematicalService = complaintsRepository;
        }

        protected override dynamic GetRepository()
        {
            return _mathematicalService;
        }
    }
}
