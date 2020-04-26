using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TaskManagement
{
    [ClassIdentifier("32000", "32")]
    public class TaskType : ModelBase
    {
        [PropertyID("32000_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}
