using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31150","31")]
    public class Dependant
    {
        public string employeeId { get; set; }
        public string seqNo { get; set; }
        [PropertyID("31150_01")]
        [ApplySecurity]
        public string dependencyType { get; set; }
        [PropertyID("31150_02")]
        [ApplySecurity]
        public string firstName { get; set; }
        [PropertyID("31150_03")]
        [ApplySecurity]
        public string middleName { get; set; }
        [PropertyID("31150_04")]
        [ApplySecurity]
        public string lastName { get; set; }
        [PropertyID("31150_05")]
        [ApplySecurity]
        public DateTime? birthDate { get; set; }
        [PropertyID("31150_06")]
        [ApplySecurity]
        public string gender { get; set; }
        [PropertyID("31150_07")]
        [ApplySecurity]
        public string phoneNumber { get; set; }
        [PropertyID("31150_08")]
        [ApplySecurity]
        public bool? isStudent { get; set; }
        [PropertyID("31150_09")]
        [ApplySecurity]
        public bool? isCitizen { get; set; }
        [PropertyID("31150_10")]
        [ApplySecurity]

        public AddressBook address { get; set; }

        public string naId { get; set; }
        public bool? hasSpecialNeeds { get; set; }
        [PropertyID("31150_11")]
        [ApplySecurity]

        public bool? isEmployed { get; set; }
        [PropertyID("31150_12")]
        [ApplySecurity]

        public string stateId { get; set; }
        public string dtName { get; set; }






    }
}
