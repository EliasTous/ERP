using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.SelfService
{
     [ClassIdentifier("60106", "60")]
    
    public class LetterSelfservice :ModelBase
    {
        [PropertyID("60106_01")]
        [ApplySecurity]
        public string addressedTo { get; set; }
       
        public DateTime date { get; set; }
        [PropertyID("60106_03")]
        [ApplySecurity]
        public string letterRef { get; set; }
        public int ltId { get; set; }
        public int employeeId { get; set; }
        [PropertyID("60106_04")]
        [ApplySecurity]
        public string notes { get; set; }
        [PropertyID("60106_05")]
        [ApplySecurity]
        public string bodyText { get; set; }
        [PropertyID("60106_06")]
        [ApplySecurity]
        public string ltName { get; set; }
    }
}
