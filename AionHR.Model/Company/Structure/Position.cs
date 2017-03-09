using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
   public class Position : ModelBase
    {
        public string reference { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? referToPositionId { get; set; }
        public string referToPositionName { get; set; }
    }
}
