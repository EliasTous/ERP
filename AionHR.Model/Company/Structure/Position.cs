using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
    [ClassIdentifier("21030", "21")]
    public class Position : ModelBase
    {
        [PropertyID("21030_01")]
        [ApplySecurity]
        public string name { get; set; }

        [PropertyID("21030_02")]
        [ApplySecurity]
        public string positionRef { get; set; }
        [PropertyID("21030_03")]
        [ApplySecurity]
        public string description { get; set; }
        [PropertyID("21030_04")]
        [ApplySecurity]
        public int? referToPositionId { get; set; }
        public string referToPositionName { get; set; }
    }
}
