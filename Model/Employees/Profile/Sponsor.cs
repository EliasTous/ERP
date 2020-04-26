using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31110", "31")]
    public class Sponsor : ModelBase
    {
        [PropertyID("31110_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("31110_02")]
        [ApplySecurity]
        public string idRef { get; set; }
        [PropertyID("31110_03")]
        [ApplySecurity]
        public string rtwRef { get; set; }
        [PropertyID("31110_04")]
        [ApplySecurity]
        public string address { get; set; }
        [PropertyID("31110_05")]
        [ApplySecurity]
        public string city { get; set; }
        [PropertyID("31110_06")]
        [ApplySecurity]
        public string mobile { get; set; }
        [PropertyID("31110_07")]
        [ApplySecurity]
        public string phone { get; set; }
        [PropertyID("31110_08")]
        [ApplySecurity]
        public string email { get; set; }
        [PropertyID("31110_09")]
        [ApplySecurity]
        public string fax { get; set; }
        public bool? isSupplier { get; set; }





    }
}
