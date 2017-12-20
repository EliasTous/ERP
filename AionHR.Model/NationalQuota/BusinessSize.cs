using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.NationalQuota
{
 public   class BusinessSize:ModelBase
    {
        public string name { get; set; }
        public int minEmployees { get; set; }
        public int maxEmployees { get; set; }
    }
}
