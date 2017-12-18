using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
    [ClassIdentifier("21020", "21")]
    public  class Branch : ModelBase,IEntity
    {
        [PropertyID("21020_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("21020_02")]
        [ApplySecurity]
        public string reference { get; set; }
        [PropertyID("21020_03")]
        [ApplySecurity]
        public int timeZone { get; set; }
        [PropertyID("21020_04")]
        [ApplySecurity]
        public bool? isInactive { get; set; }
        [PropertyID("21020_05")]
        [ApplySecurity]
        public AddressBook addressId { get; set; }
        
        public string naId { get; set; }
        
        public string stId { get; set; }
        public int scId { get; set; }
        public string scName { get; set; }


    }
}
