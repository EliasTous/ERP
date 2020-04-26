using Infrastructure.Session;
using Model.Dashboard;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
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
