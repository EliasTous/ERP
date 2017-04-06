using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class TeamMember:ModelBase
    {
        public string pictureUrl { get; set; }

        public EmployeeName name { get; set; }

        public int positionId { get; set; }

        public string positionName { get; set; }
    }
}
