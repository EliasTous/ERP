using AionHR.Model.Attributes;
using AionHR.Model.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80305", "80")]
    public  class RT305 : DashBoardTimeVariation
    {
        public short seqNo { get; set; }
        public double edAmount { get; set; }
     
        public double groupingKey
        {
            get { return employeeId * 100 + timeCode; }
            set { value = groupingKey; }
        }


    }
}
