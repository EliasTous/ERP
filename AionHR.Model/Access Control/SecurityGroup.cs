using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Access_Control
{
    [ClassIdentifier("90101", "90")]
    public class SecurityGroup : ModelBase
    {
        [PropertyID("90101_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("90101_02")]
        [ApplySecurity]
        public string description { get; set; }
    }
    
    public class UC
    {
        public string propertyId { get; set; }

        public int accessLevel { get; set; }

        public string index { get; set; }
    }

    public class ClassPropertyDefinition
    {
        public string propertyId { get; set; }
        public string name { get; set; }
        public string index { set; get; }

    }
    [ClassIdentifier("90104", "90")]
    public class ClassProperty
    {
        [PropertyID("90104_01")]
        [ApplySecurity]
        public string propertyId { get; set; }
        
        public string sgId { get; set; }

        public string classId { get; set; }

        public string name { get; set; }
        [PropertyID("90104_02")]
        [ApplySecurity]
        public int accessLevel { get; set; }

        public string index { set; get; }

        public ClassProperty() { }
        public ClassProperty(ClassPropertyDefinition def)
        {
            propertyId = def.propertyId;
            name = def.name;
            index = def.index;
        }
    }
    public class PropertyAccessLevel
    {
        public string text { get; set; }

        public string value { get; set; }

        public PropertyAccessLevel(string text, string value)
        {
            this.text = text;
            this.value = value;
        }
    }

    public partial class ModuleClassDefinition
    {
        public string id { get; set; }

        public string classId { get; set; }

       

        public string name { get; set; }

        

        public List<ClassPropertyDefinition> properties { get; set; }
    }
    [ClassIdentifier("90103", "90")]
    public partial class ModuleClass
    {

        public string id { get; set; }
        [PropertyID("90103_01")]
        [ApplySecurity]
        public string classId { get; set; }
        [PropertyID("90103_02")]
        [ApplySecurity]
        public int accessLevel { get; set; }

        public string name { get; set; }

        public string sgId { get; set; }


        public ModuleClass() { }
        public ModuleClass(ModuleClassDefinition def)
        {
            name = def.name;
            classId = def.classId;
            id = def.id;

        }

        public List<ClassProperty> properties { get; set; }
    }

    public class Module
    {
        public string id { get; set; }

        public string name { get; set; }

        public List<ModuleClassDefinition> classes { get; set; }
    }
}
