using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Benefits
{
    [ClassIdentifier("25011", "25")]
    public  class Benefit : ModelBase
    {
        public string name { get; set; }
    }
}
