using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeContact:ModelBase
    {
        public int employeeId { get; set; }

        public string street1 { get; set; }

        public string street2 { get; set; }

        public string city { get; set; }
        public string phone { get; set; }

        public string postalCode { get; set; }

        public AddressBook addressId { get; set; }

        public int naId { get; set; }

        public string naName { get; set; }

        public int rtId { get; set; }

        public string rtName { get; set; }

        public string stateId { get; set; }
    }
}
