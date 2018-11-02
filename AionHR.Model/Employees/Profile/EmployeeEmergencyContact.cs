using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31121", "31")]
    public class EmployeeEmergencyContact:ModelBase
    {

        [PropertyID("31121_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("31121_02")]
        [ApplySecurity]
        public int rtId { get; set; }
        [PropertyID("31121_02")]
        [ApplySecurity]
        public string rtName { get; set; }
        [PropertyID("31121_03")]
        [ApplySecurity]
        public string workPhone { get; set; }
        [PropertyID("31121_04")]
        [ApplySecurity]
        public string homePhone { get; set; }
        [PropertyID("31121_05")]
        [ApplySecurity]
        public string cellPhone { get; set; }
        [PropertyID("31121_06")]
        [ApplySecurity]
        public string email { get; set; }
        [PropertyID("31121_07")]
        [ApplySecurity]
        public AddressBook address { get; set; }

        public string naId { get; set; }

        public string stateId { get; set; }
        public int employeeId { get; set; }
    }
}
