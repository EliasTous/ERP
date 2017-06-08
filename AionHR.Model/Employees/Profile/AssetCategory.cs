using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31010", "31")]
    public class AssetCategory : ModelBase
    {
        [PropertyID("31010_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}
