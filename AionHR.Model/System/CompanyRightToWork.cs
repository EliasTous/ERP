using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    public class CompanyRightToWork : ModelBase
    {
        public int dtId { get; set; }
        public int branchId { get; set; }
        public string dtName { get; set; }
        public string branchName { get; set; }
        public string documentRef { get; set; }
        public string remarks { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime expiryDate { get; set; }

        public string fileUrl { get; set; }
    }
}
