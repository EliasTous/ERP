using AionHR.Infrastructure.Session;
using AionHR.Model.Company.News;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class CompanyNewsService : BaseService, ICompanyNewsService
    {
        private ICompanyNewsRepository repo;
        public CompanyNewsService(ICompanyNewsRepository _companyNewsRepo, SessionHelper sessionHelper):base(sessionHelper)
        {
            repo = _companyNewsRepo;
        }
        protected override dynamic GetRepository()
        {
            return repo;
        }
    }
}
