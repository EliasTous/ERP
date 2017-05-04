using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
    public class PayrollRepository:Repository<IEntity,string>,IPayrollRepository
    {
        private string serviceName = "PY.asmx/";

        public PayrollRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(FiscalYear), "qryYE");
            ChildGetAllLookup.Add(typeof(GenerationHeader), "qryHE");
            ChildGetAllLookup.Add(typeof(FiscalPeriod), "qryPE");
            ChildGetAllLookup.Add(typeof(EmployeePayroll), "qryEM");

            ChildAddOrUpdateLookup.Add(typeof(FiscalYear), "setYE");
            ChildAddOrUpdateLookup.Add(typeof(GenerationHeader), "setHE");
            ChildAddOrUpdateLookup.Add(typeof(EmployeePayroll), "setEM");

            ChildDeleteLookup.Add(typeof(FiscalYear), "delYE");

        }
    }
}
