using AionHR.Infrastructure.Session;
using AionHR.Model.Reports;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class ReportsService : BaseService, IReportsService
    {
        IReportsRepository repository;

        public ReportsService(SessionHelper helper, IReportsRepository _repo):base(helper)
        {
            repository = _repo;
        }


        protected override dynamic GetRepository()
        {
            return repository;
        }
    }
}
