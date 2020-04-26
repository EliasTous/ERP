using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
  public  class DashboardBranchAvailability
    {
        public string branchName { get; set; }
        public int scheduled { get; set; }
        public int present { get; set; }
        public int absent
        {
            get
            {
                return scheduled - present; 
            }
        }


    }
}
