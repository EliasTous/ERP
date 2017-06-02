using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Access_Control
{
    public class SecurityGroup : ModelBase
    {
        public string name { get; set; }

        public string description { get; set; }
    }

    public class UC
    {
        public string propertyId { get; set; }

        public int accessLevel { get; set; }
    }
    public class ClassProperty
    {
        public string propertyId { get; set; }

        public string sgId { get; set; }

        public string classId { get; set; }

        public string name { get; set; }
        public int accessLevel { get; set; }
    }

    public partial class ModuleClass
    {
        public string id { get; set; }

        public string classId { get; set; }

        public int accessLevel { get; set; }

        public string name { get; set; }

        public string sgId { get; set; }

        public List<ClassProperty> properties { get; set; }
    }

    public class Module
    {
        public string id { get; set; }

        public string name { get; set; }

        public List<ModuleClass> classes { get; set; }
    }
}
