using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AdminTemplates
{

    [ClassIdentifier("70201", "70")]
    public class BusinessPartner :ModelBase
    {
        public string name { get; set; }
        public string bcId { get; set; }
        public string bcName { get; set; }
        public bool inactive { get; set; }
        public short activeStatus { get; set; }
        
        public string email { get; set; }
        public AddressBook address { get; set; }

    }
}
