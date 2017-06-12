using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Access_Control
{
    [ClassIdentifier("90102", "90")]
    public  class SecurityGroupUser
    {
        [PropertyID("90102_01")]
        [ApplySecurity]
        public string sgId { get; set; }

        [PropertyID("90102_01")]
        [ApplySecurity]
        public string sgName { get; set; }

        [PropertyID("90102_02")]
        [ApplySecurity]
        public string userId { get; set; }

        [PropertyID("90102_02")]
        [ApplySecurity]
        public string fullName { get; set; }
    }
}
