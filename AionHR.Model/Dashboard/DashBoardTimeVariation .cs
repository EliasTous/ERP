using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
  public  class DashBoardTimeVariation
    {
       
        public int employeeId { get; set; }
        public string dayId { get; set; }
        public short shiftId { get; set; }
        public short timeCode { get; set; }
        public short clockDuration { get; set; }
        public short? apStatus { get; set; }
        public string apStatusString { get; set; }
        public short duration { get; set; }

        public EmployeeName employeeName { get; set; }
        public string branchName { get; set; }
        public string positionName { get; set; }

    }
}
