using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TaskManagement
{
    [ClassIdentifier("32020", "32")]
    public class TaskComment:ModelBase
    {
        [PropertyID("32020_01")]
        [ApplySecurity]
        public int userId { get; set; }
        [PropertyID("32020_02")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("32020_03")]
        [ApplySecurity]
        public string comment { get; set; }
       

        public short? seqNo { get; set; }
        [PropertyID("32020_01")]
        [ApplySecurity]
        public string userName { get; set; }
        public int taskId { get; set; }
    }
}
