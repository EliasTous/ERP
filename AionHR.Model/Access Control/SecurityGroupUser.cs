using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Access_Control
{
   public  class SecurityGroupUser
    {
        public string sgId { get; set; }

        public string sgName { get; set; }

        public string userId { get; set; }

        public string fullName { get; set; }
    }
}
