using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Access_Control
{
    public class SecurityGroup:ModelBase
    {
        public string name { get; set; }

        public string description { get; set; }
    }
}
