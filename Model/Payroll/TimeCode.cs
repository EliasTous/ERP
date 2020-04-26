using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Payroll
{
    [ClassIdentifier("51013","51")]
 public class TimeCode 
    {
        public short? edType { get; set; }
        public string edTypeString { get; set; }
        public short? timeCode { get; set; }
        public string name { get; set; }
        public string edId { get; set; }
        public string edName { get; set; }
        public string apId { get; set; }
        public string apName { get; set; }
        public int gracePeriod { get; set; }



    }
}
