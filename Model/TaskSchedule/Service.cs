using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TaskSchedule
{
    public class Service : ModelBase
    {
        public string name { get; set; }
        public int frequency { get; set; }
        public string time { get; set; }
        public int? flags { get; set; }
        public string frequencyName { get; set; }
    }
}
