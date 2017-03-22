using AionHR.Infrastructure.Session;
using AionHR.Model.Company.Cases;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class CaseService: BaseService,ICaseService
    {

        ICasesRepository _casesRepository;
        public enum CompanyStructureErrors
        {
            Company_Department_50005,
        }
        public CaseService(ICasesRepository caseRepo, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _casesRepository = caseRepo;

        }


        protected override dynamic GetRepository()
        {
            return _casesRepository;
        }
    }
}
