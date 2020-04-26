using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TaskSchedule
{
    public class Receiver
    {
        public int taskId { get; set; }
        public int seqNo { get; set; }
        public int receiverType { get; set; }
        public int? sgId { get; set; }
        public string email { get; set; }
        public string rtName { get; set; }
        public string sgName { get; set; }

        public int? languageId { get; set; }
        public bool pdf { get; set; }
        public bool xls { get; set; }
        public bool csv { get; set; }

    }
}
