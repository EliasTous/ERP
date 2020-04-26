using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Company.Structure
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
        [PropertyID("21030_04")]
        [ApplySecurity]
        public string referToPositionName { get; set; }

        [PropertyID("21030_05")]
        [ApplySecurity]
        public int? tsId { get; set; }

        public string tsName { get; set; }
    }
}
