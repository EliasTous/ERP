using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.TimeAttendance
{
 public  class DashboardTimeListRequest:ListRequest
    {
        public int approverId { get; set; }
        public int employeeId { get; set; }
        public string dayId { get; set; }
        public string timeCode { get; set; }
        public string shiftId { get; set; }
        public string fromDayId { get; set; }
        public string toDayId { get; set; }
        public string apStatus { get; set; }

        public int? DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int? PositionId { get; set; }
        public int? BranchId { get; set; }
        public int EsId { get; set; }
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_approverId", approverId.ToString());
                parameters.Add("_employeeId", employeeId.ToString());
                parameters.Add("_dayId", dayId);
                parameters.Add("_timeCode", timeCode);
                parameters.Add("_shiftId", shiftId);
                parameters.Add("_fromDayId", fromDayId);
                parameters.Add("_toDayId", toDayId);
                parameters.Add("_status", apStatus);


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
