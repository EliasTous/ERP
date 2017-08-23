using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class SalaryTypeParameterSet: ReportParameterSet
    {
        public int SalaryTypeId { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_salaryType", SalaryTypeId.ToString());
                return parameters;
            }
        }
    }
}
