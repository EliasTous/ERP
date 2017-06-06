using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Leaves
{
    public class VacationSchedule:ModelBase,IEntity
    {
        public string name { get; set; }
    }
    public  class VacationSchedulePeriod 
    {
        
        public short from { get; set; }
        public short to { get; set; }
        public short days { get; set; }

        public int vsId { get; set; }
        public short seqNo { get; set; }
    }
}
