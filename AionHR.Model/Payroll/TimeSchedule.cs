using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51012","51")]
   public class TimeSchedule:ModelBase
    {
        [PropertyID("51012_01")]
        public string name { get; set; }
    }
}
