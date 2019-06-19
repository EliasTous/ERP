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

        public string DepartmentId { get; set; }
        public string DivisionId { get; set; }
        public string PositionId { get; set; }
        public string BranchId { get; set; }
        public string EsId { get; set; }
        public string sortBy { get; set; }

        public override Dictionary<string, string> Parameters
        {
            get
            {
                if (DepartmentId == null)
                    DepartmentId = "0";
                if (DivisionId == null)
                    DivisionId = "0";
                if (BranchId == null)
                    BranchId = "0";
                if (PositionId == null)
                    PositionId = "0";
                if (EsId == null)
                    EsId = "0";
                if (StartAt == null)
                    StartAt = "0";
                if (Size == null)
                    Size = "1000";


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
                if (string.IsNullOrEmpty(sortBy))
                    parameters.Add("_sortBy", "dayId");
                //if(string.IsNullOrEmpty(StartAt))
                //    if(parameters.get
                //parameters.Add("_startAt", "0");
                //if (string.IsNullOrEmpty(Size))
                //    parameters.Add("_size", "1000");
                return parameters;
            }
        }
    }
}
