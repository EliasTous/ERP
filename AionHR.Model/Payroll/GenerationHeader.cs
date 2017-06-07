using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51020", "51")]
    public class GenerationHeader:ModelBase
    {
        
        public string payRef { get; set; }
        public string fiscalYear { get; set; }
        public string salaryType { get; set; }
        public string periodId { get; set; }
        public string payDate { get; set; }
        public string status { get; set; }
        public string notes { get; set; }
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
       
    }
}
