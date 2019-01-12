using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Employees
{
   public class EmployeePenaltyApprovalListRequest :ListRequest
    {
        public int? DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int? PositionId { get; set; }
        public int? BranchId { get; set; }
        public int EsId { get; set; }
        public string penaltyId { get; set; }
        public string approverId { get; set; }

        public string apStatus { get; set; }

        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>(); 
                parameters.Add("_approverId", approverId);
                parameters.Add("_penaltyId", penaltyId);
                parameters.Add("_apStatus", apStatus);
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
