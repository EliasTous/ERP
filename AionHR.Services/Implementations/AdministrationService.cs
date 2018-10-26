using AionHR.Infrastructure.Session;
using AionHR.Model.AdminTemplates;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
   public class AdministrationService : BaseService, IAdministrationService
    {
        IAdministrationRepository _casesRepository;
      
        public AdministrationService(IAdministrationRepository caseRepo, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _casesRepository = caseRepo;

        }


        protected override dynamic GetRepository()
        {
            return _casesRepository;
        }
    }
}
