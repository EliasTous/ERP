using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.System
{
    public class DashboardRequest:ListRequest
    {
        public string DepartmentId { get; set; }
        public string DivisionId { get; set; }
        public string PositionId { get; set; }
        public string BranchId { get; set; }
        public string EsId { get; set; }

        

        public override Dictionary<string,string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_departmentId", DepartmentId.ToString());
                parameters.Add("_divisionId", DivisionId.ToString());
                parameters.Add("_branchId", BranchId.ToString());
                parameters.Add("_esId", EsId.ToString());
                parameters.Add("_positionId", PositionId.ToString());
                return parameters;
            }
        }
    }
    
}
