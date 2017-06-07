using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TaskManagement
{
    [ClassIdentifier("32000", "32")]
    public class TaskType : ModelBase
    {
        public string name { get; set; }
    }
}
