using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class Dependant
    {
        public string employeeId { get; set; }
        public string seqNo { get; set; }
        public string dependencyType { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string phoneNumber { get; set; }
        public string isStudent { get; set; }
        public string isCitizen { get; set; }

        public AddressBook addressId { get; set; }
    }
}
