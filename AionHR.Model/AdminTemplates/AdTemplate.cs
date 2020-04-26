using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AdminTemplates
{
    [ClassIdentifier("70100", "70")]
    public class AdTemplate: ModelBase, IEntity
    {
        
        [PropertyID("77000_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("77000_02")]
        [ApplySecurity]
        public short usage { get; set; }
    }
}
