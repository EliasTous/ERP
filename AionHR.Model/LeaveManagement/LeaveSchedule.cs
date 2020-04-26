using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Leaves
{
    [ClassIdentifier("42013", "20")]
    public class LeaveSchedule : ModelBase,IEntity
    {
        
        public string name { get; set; }
    }
    [ClassIdentifier("42014", "20")]
    public  class LeaveSchedulePeriod
    {
       
        public short from { get; set; }
      
        public short to { get; set; }
       
        public short pct { get; set; }
       

        public int lsId { get; set; }

        public short seqNo { get; set; }
    }
}
