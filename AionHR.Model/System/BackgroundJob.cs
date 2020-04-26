using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{
    [ClassIdentifier("20001", "20")]
    public  class BackgroundJob :ModelBase
    {

        public string userId { get; set; }
        public string classId { get; set; }
        public string MyProperty { get; set; }

        public int taskSize { get; set; }
        public int completed { get; set; }

        public int status { get; set; }

        public string errorId { get; set; }
        public string errorName { get; set; }

        public string argStr { get; set; }
        public string argInt { get; set; }
        public string infoList { get; set; }


    }
}
