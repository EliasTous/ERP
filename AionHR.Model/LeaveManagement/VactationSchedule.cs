using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Leaves
{
    [ClassIdentifier("42010", "42")]
    public class VacationSchedule:ModelBase,IEntity
    {
        [PropertyID("42010_01")]
        [ApplySecurity]
        public string name { get; set; }

        public short? calcMethod { get; set; }
    }
    [ClassIdentifier("42012", "42")]
    public  class VacationSchedulePeriod 
    {
        [PropertyID("42012_01")]
        [ApplySecurity]
        public short from { get; set; }
        [PropertyID("42012_02")]
        [ApplySecurity]
        public short to { get; set; }
        [PropertyID("42012_03")]
        [ApplySecurity]
        public short days { get; set; }
       

        public int vsId { get; set; }

        public short seqNo { get; set; }

        
    }
}
