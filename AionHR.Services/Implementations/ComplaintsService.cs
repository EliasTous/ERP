using Infrastructure.Session;
using Model.EmployeeComplaints;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ComplaintsService : BaseService, IComplaintsService
    {
        private IComplaintsRepository _complaintsRepository;

        public ComplaintsService(IComplaintsRepository complaintsRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _complaintsRepository = complaintsRepository;
        }

        protected override dynamic GetRepository()
        {
            return _complaintsRepository;
        }
    }
}
