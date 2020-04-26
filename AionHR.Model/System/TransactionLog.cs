using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{
    [ClassIdentifier("20060", "20")]
    public class TransactionLog:ModelBase
    {
        public int classId { get; set; }

        public int userId { get; set; }

        public string userName { get; set;}

        public int type { get; set; }

        public string data { get; set; }

        public DateTime eventDt { get; set; }
    }
}
