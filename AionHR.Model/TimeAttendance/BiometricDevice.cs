using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class BiometricDevice : ModelBase
    {
        public string name
        {
            get; set;
        }
        public string reference { get; set; }
     
        public string divisionId { get; set; }
    }
}
