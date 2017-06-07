using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attributes
{
    public class PropertyID:Attribute
    {
        private string id;
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }

        }

        public PropertyID(string id)
        {
            
            this.ID = id.Replace("_","");
        }
    }

    public class ClassIdentifier:Attribute
    {
        public string ClassID { get; set; }

        public string ModuleId { get; set; }

        public ClassIdentifier( string id,string mod)
        {
            
            this.ClassID = id;
            ModuleId = mod;
        }
    }
}
