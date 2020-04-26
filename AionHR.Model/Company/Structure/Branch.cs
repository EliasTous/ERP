using Infrastructure.Domain;
using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Company.Structure
{
    [ClassIdentifier("21020", "21")]
    public  class Branch : ModelBase,IEntity
    {
        [PropertyID("21020_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("21020_02")]
        [ApplySecurity]
        public string branchRef { get; set; }
        [PropertyID("21020_03")]
        [ApplySecurity]
        public int timeZone { get; set; }
        [PropertyID("21020_04")]
        [ApplySecurity]
        public bool? isInactive { get; set; }
        [PropertyID("21020_05")]
        [ApplySecurity]
        public AddressBook address { get; set; }
        public short activeStatus { get; set; }
        public string naId { get; set; }
        
        public string stId { get; set; }
        public int? scId { get; set; }
        public string scName { get; set; }
        public int? caId { get; set; }
        public string caName { get; set; }
        public string managerId { get; set; }
        public string managerName { get; set; }


    }
    [ClassIdentifier("82101", "82")]
    public class BranchSchedule
    {
     
        public string openAt { get; set; }
        public string closeAt { get; set; }

    }

    public class OpenClose {
        public string openAt { get; set; }
        public string closeAt { get; set; }
    }
}
