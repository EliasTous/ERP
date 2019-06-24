using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
    [ClassIdentifier("21050", "21")]
    public  class Approval:ModelBase 
    {
        public string name { get; set; }

        public string approvalTypeName { get; set; }
        public string approvalFlowName { get; set; }


        public string wfId { get; set; }
        public string approvalType { get; set; }
        public string approvalFlow { get; set; }


       
    }
}
