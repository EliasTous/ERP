using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Company.Structure
{
  public  class Rule :ModelBase
    {
        public string actionTypeName { get; set; }
        public string actionType { get; set; }
        public string name { get; set; }
        public string expressionId { get; set; }
        

    }
}
