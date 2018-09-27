using AionHR.Infrastructure.Session;
using AionHR.Model.Dashboard;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
   public class DashBoardService : BaseService, IDashBoardService
    {
        private IDashBoardRepository _dashBoardRepository;

        public DashBoardService(IDashBoardRepository complaintsRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _dashBoardRepository = complaintsRepository;
        }

        protected override dynamic GetRepository()
        {
            return _dashBoardRepository;
        }
    }
}
