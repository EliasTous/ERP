using AionHR.Infrastructure.Session;
using AionHR.Model.Payroll;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class PayrollService:BaseService,IPayrollService
    {
        IPayrollRepository _payrollRepository;
    
        public PayrollService(IPayrollRepository caseRepo, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _payrollRepository = caseRepo;

        }


        protected override dynamic GetRepository()
        {
            return _payrollRepository;
        }
    }
}
