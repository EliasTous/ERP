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
        
        public string name { get; set; }

        public string positionRef { get; set; }
        public string description { get; set; }
        public int? referToPositionId { get; set; }
        public string referToPositionName { get; set; }
    }
}
