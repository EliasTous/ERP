using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class JobInfoParameterSet : ReportParameterSet
    {
        public int? DepartmentId { get; set; }

        public int? BranchId { get; set; }

        public int? PositionId { get; set; }

        public int? DivisionId { get; set; }
        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = new Dictionary<string, string>();
                if (DepartmentId.HasValue)
                    parameters.Add("_departmentId", DepartmentId.ToString());
                if (BranchId.HasValue)
                    parameters.Add("_branchId", BranchId.ToString());
                if (PositionId.HasValue)
                    parameters.Add("_positionId", PositionId.ToString());
                if (DivisionId.HasValue)
                    parameters.Add("_divisionId", DivisionId.ToString());
                return parameters;
            }
        }

    }
}
