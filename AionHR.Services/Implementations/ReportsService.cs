using Infrastructure.Session;
using Model.Reports;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
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
