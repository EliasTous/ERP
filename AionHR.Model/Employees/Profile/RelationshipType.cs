using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31120", "31")]
    public class RelationshipType:ModelBase
    {
        [PropertyID("31120_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}
