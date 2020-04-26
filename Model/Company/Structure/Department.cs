using Infrastructure.Domain;
using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Company.Structure
{
    [ClassIdentifier("21040", "21")]
    public class Department : ModelBase, IEntity
    {
        [PropertyID("21040_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("21040_02")]
        [ApplySecurity]
        public string departmentRef { get; set; }
        [PropertyID("21040_03")]
        [ApplySecurity]
        public bool? isInactive { get; set; }
        [PropertyID("21040_04")]
        [ApplySecurity]
        public short activeStatus { get; set; }
        public int? supervisorId { get; set; }
        [PropertyID("21040_04")]
        [ApplySecurity]

        public string supervisorRef { get; set; }
        [PropertyID("21040_05")]
        [ApplySecurity]
        public int? parentId { get; set; }

        [PropertyID("21040_05")]
        [ApplySecurity]
        public string parentName { get; set; }
        [PropertyID("21040_05")]
        [ApplySecurity]
        public string parentRef { get; set; }
        [PropertyID("21040_04")]
        [ApplySecurity]
        public string managerName { get; set; }
        [PropertyID("21040_06")]
        [ApplySecurity]
        public string scId { get; set; }
        [PropertyID("21040_06")]
        [ApplySecurity]
        public string scName { set; get; }
        //[PropertyID("21040_07")]
        //[ApplySecurity]
        //public bool? isLeaveAuth { get; set; }

        public short? type { get; set; }
        [PropertyID("21040_08")]
        [ApplySecurity]
        public int?  caId { get; set; }
        [PropertyID("21040_08")]
        [ApplySecurity]
        public string caName { set; get; }
       
    }
}
