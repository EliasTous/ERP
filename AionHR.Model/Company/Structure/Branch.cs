using AionHR.Infrastructure.Domain;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
   public  class Branch : ModelBase,IEntity
    {
        public string name { get; set; }
        public string reference { get; set; }
        
        public int timeZone { get; set; }
        
        public bool? isInactive { get; set; }

        public AddressBook addressId { get; set; }

        public string naId { get; set; }

        public string stId { get; set; }
    }
}
