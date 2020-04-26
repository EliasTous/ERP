using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
  public  class DashBoardDO :DashboardLW 
    {
        public DateTime dtFrom { get; set; }
        public DateTime dtTo { get; set; }
    }
}
