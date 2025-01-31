﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.HelpFunction
{
   public class ActiveAttendanceRecordRequest : RecordRequest
    {
        public string DepartmentId { get; set; }

        public string BranchId { get; set; }

        public string PositionId { get; set; }
        public string StatusId { get; set; }
        public string DivisionId { get; set; }

        public int? DayStatus { get; set; }
        private Dictionary<string, string> parameters;

        /// <summary>
        /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = base.Parameters;
                parameters.Add("_departmentId", DepartmentId.ToString());
                parameters.Add("_branchId", BranchId.ToString());
                parameters.Add("_positionId", PositionId.ToString());
                parameters.Add("_divisionId", DivisionId.ToString());
                parameters.Add("_esId", StatusId.ToString());


                if (DayStatus.HasValue)
                    parameters.Add("_dayStatus", DayStatus.Value.ToString());
                return parameters;
            }
        }
       
    }
}
