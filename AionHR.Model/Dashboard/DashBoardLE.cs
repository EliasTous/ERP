using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
    [ClassIdentifier("81112", "81")]
    public  class DashBoardLE :DashboardLW

    {

        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}
