using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TimeAttendance
{
    [ClassIdentifier("41000", "41")]
    public class BiometricDevice : ModelBase
    {
        [PropertyID("41000_01")]
        [ApplySecurity]
        public string name
        {
            get; set;
        }
        [PropertyID("41000_02")]
        [ApplySecurity]
        public string reference { get; set; }
        [PropertyID("41000_03")]
        [ApplySecurity]
        public string divisionName { get; set; }
        [PropertyID("41000_03")]
        [ApplySecurity]
        public string divisionId { get; set; }

        public string branchId { get; set; }



        
    }
}
