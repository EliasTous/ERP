using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
    [ClassIdentifier("21021", "21")]
    public  class LegalReference

    {
        [PropertyID("21021_01")]
        [ApplySecurity]
        public string goName { get; set; }
        [PropertyID("21021_02")]
        [ApplySecurity]
        public int branchId { get; set; }
        [PropertyID("21021_03")]
        [ApplySecurity]
        public int goId { set; get; }
        [PropertyID("21021_04")]
        [ApplySecurity]
        public string reference { get; set; }
        [PropertyID("21021_05")]
        [ApplySecurity]
        public DateTime? releaseDate { set; get; }

    }
}
