using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SelfService
{
   public class AssetLoan :ModelBase

    {
        public string employeeName { get; set; }
        public short? status { get; set; }
        public short? apStatus { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }
        public string branchName { get; set; }
        public string assetName { get; set; }
        public string comments { get; set; }
        public DateTime? date { get; set; }
        public string employeeId { get; set; }
        public string assetId { get; set; }


    }
}
