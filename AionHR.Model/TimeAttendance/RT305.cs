using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    [ClassIdentifier("41063","41")]
   public class RT305
    {
        [PropertyID("4106301")]
        [ApplySecurity]
        public string name { get; set; }

        [PropertyID("4106302")]
        [ApplySecurity]
        public string dayId { get; set; }
        [PropertyID("4106303")]
        [ApplySecurity]
        public string branchName { get; set; }
        [PropertyID("4106304")]
        [ApplySecurity]
        public string departmentName { get; set; }
        [PropertyID("4106305")]
        [ApplySecurity]
        public string ltName { get; set; }

        public int dayStatus { get; set; }

        public string dayStatusString { get; set; }

        public string DateString { get; set; }


    }
}
