using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
    public class DashboardRequest:ListRequest
    {
        public int DepartmentId { get; set; }
        public int DivisionId { get; set; }
        public int PositionId { get; set; }
        public int BranchId { get; set; }
        public int EsId { get; set; }

        

        public override Dictionary<string,string> Parameters
        {
            get
            {
                parameters = base.parameters;
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
