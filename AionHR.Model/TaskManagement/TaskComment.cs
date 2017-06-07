using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TaskManagement
{
    [ClassIdentifier("32020", "32")]
    public class TaskComment:ModelBase
    {

        public int userId { get; set; }
        public DateTime date { get; set; }
        public string comment { get; set; }
       

        public short? seqNo { get; set; }
        public string userName { get; set; }
        public int taskId { get; set; }
    }
}
