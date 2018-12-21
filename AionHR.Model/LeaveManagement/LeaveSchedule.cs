using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Leaves
{
  
    public class LeaveSchedule : ModelBase,IEntity
    {
        
        public string name { get; set; }
    }
  
    public  class LeaveSchedulePeriod
    {
       
        public short from { get; set; }
      
        public short to { get; set; }
       
        public short pct { get; set; }
       

        public int lsId { get; set; }

        public short seqNo { get; set; }
    }
}
