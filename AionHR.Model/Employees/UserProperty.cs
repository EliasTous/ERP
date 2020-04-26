using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees
{
    public class UserProperty :ModelBase 
    {
        public string maskString { get; set; }
        public string name { get; set; }
        public short mask { get; set; }
    }
}
