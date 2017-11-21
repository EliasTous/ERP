using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
   public class Letter:ModelBase
    {
        public string addressedTo { get; set; }
        public DateTime date { get; set; }
        public string letterRef { get; set; }
        public int ltId { get; set; }
        public int employeeId { get; set; }
        public string notes { get; set; }
        public string bodyText { get; set; }
        public string ltName { get; set; }



    }
}
