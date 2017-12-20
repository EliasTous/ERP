using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.NationalQuota
{
    public class Citizenship:ModelBase
    {
        public string name { get; set; }
        public int ceiling { get; set;  }
        public double points { get; set; }
         
    }
}
