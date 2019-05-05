using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80307", "80")]
    public class RT307
    {
        public string approverName { get; set; }
        public int status_new { get; set; }
        public int status_approved { get; set; }
        public int status_rejected { get; set; }

    }
}
