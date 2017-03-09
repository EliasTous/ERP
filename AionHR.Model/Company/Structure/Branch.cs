using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
   public  class Branch : ModelBase,IEntity
    {
        public string reference { get; set; }
        public string name { get; set; }
        public int timeZone { get; set; }
        public string segmentCode { get; set; }
        public bool? isInactive { get; set; }
    }
}
