using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31031", "31")]
    public class Bonus:ModelBase
    {
        [PropertyID("31031_01")]
        [ApplySecurity]
        public int currencyId { get; set; }





        [PropertyID("31031_02")]
        [ApplySecurity]
        public int btId { get; set; }




        [PropertyID("31031_03")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("31031_04")]
        [ApplySecurity]
        public int amount { get; set; }

        [PropertyID("31031_05")]
        [ApplySecurity]
        public string comment { get; set; }
        [PropertyID("31031_01")]
        [ApplySecurity]
        public string currencyRef { get; set; }
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        [PropertyID("31031_02")]
        [ApplySecurity]
        public string btName { get; set; }
    }
}
