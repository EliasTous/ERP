using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeEmergencyContact:ModelBase
    {
        public int employeeId { get; set; }

        public string name { get; set; }
        public int rtId { get; set; }

        public string rtName { get; set; }

        public string workPhone { get; set; }

        public string homePhone { get; set; }

        public string cellPhone { get; set; }

        public string email { get; set; }

        public AddressBook addressId { get; set; }

        public int naId { get; set; }
    }
}
