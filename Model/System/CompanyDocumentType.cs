using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{
    [ClassIdentifier("20080", "20")]
    public class CompanyDocumentType : ModelBase
    {
        [PropertyID("20080_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}
