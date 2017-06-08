using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31122", "31")]
    public class EmployeeContact:ModelBase
    {
        [PropertyID("31122_01")]
        [ApplySecurity]
        public string phone { get; set; }
        public string street1 { get; set; }

        public string street2 { get; set; }

        public string city { get; set; }
        public string stateId { get; set; }

        public string postalCode { get; set; }
        [PropertyID("31122_02")]
        [ApplySecurity]
        public AddressBook addressId { get; set; }

        public string naId { get; set; }

        public string naName { get; set; }

        public int rtId { get; set; }

        public string rtName { get; set; }

        
        public int employeeId { get; set; }
    }
}
