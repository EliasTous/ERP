using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
   public class RuleCondition :ModelBase
    {
        public string rcoName { get; set; }
        public string ruleId { get; set; }
        public string keyName { get; set; }
        public int? oper { get; set; }
        public string value { get; set; }
        public string expressionId { get; set; }
    }
}
