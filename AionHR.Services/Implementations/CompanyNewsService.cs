using Infrastructure.Session;
using Model.Company.News;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
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
