using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attributes
{
    public class PropertyID:Attribute
    {
        public string ID { get; set; }

        public PropertyID(string id)
        {
            this.ID = id;
        }
    }
}
