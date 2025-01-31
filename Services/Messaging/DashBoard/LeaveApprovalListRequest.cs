﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.DashBoard
{
   public class LeaveApprovalListRequest :ListRequest
    {
        public short status { get; set; }
        public string approverId { get; set; }
        public string leaveId { get; set; }
        public string DepartmentId { get; set; }
        public string DivisionId { get; set; }
        public string PositionId { get; set; }
        public string BranchId { get; set; }
        public string EsId { get; set; }


        private Dictionary<string, string> parameters;
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
                parameters = new Dictionary<string, string>();
                parameters.Add("_status", status.ToString());
                parameters.Add("_approverId", approverId);
                parameters.Add("_leaveId", leaveId);
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
