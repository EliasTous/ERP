using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{
    [ClassIdentifier("20020", "20")]
    public class Currency:ModelBase
    {
        [PropertyID("20020_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("20020_02")]
        [ApplySecurity]
        public string reference { get; set; }

        public short? profileId { get; set; }
        

    }
}
