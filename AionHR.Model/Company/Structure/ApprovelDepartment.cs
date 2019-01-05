using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
    [ClassIdentifier("21051", "21")]
    public class ApprovelDepartment
    {
        public string departmentName { get; set; }
        public int apId { get; set; }
        public int departmentId { get; set; }
    }
}
