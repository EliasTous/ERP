using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
  public  class LegalReference

    {
        public string goName { get; set; }
        public int branchId { get; set; }
        public int goId { set; get; }
        public string reference { get; set; }
        public DateTime? releaseDate { set; get; }

    }
}
