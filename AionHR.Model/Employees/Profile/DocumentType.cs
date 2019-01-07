using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31050", "31")]
   public class DocumentType : ModelBase
    {
        [PropertyID("31050_01")]
        [ApplySecurity]
        public string name { get; set; }
        //public string intName { get; set; }
    }
}
