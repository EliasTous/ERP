using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51050", "51")]
    public  class Bank :ModelBase
    {
        public string swiftCode { get; set; }
        public string name { get; set; }
       
    }
}
