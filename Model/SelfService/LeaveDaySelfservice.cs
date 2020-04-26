using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SelfService
{
public  class LeaveDaySelfservice:ModelBase
    {

        public int leaveId { get; set; }
       
        public string dayId { get; set; }
       
        public double workingHours { get; set; }
        
        public double leaveHours { get; set; }

        public short dow { get; set; }
    }
}
