using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
   public class Badge : ModelBase
    {
        public string reference { get; set; }
        public string name { get; set; }
    }
}
