using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AssetManagement
{
   public class AssetPropertyValue
    {
        public string assetId { get; set; }
        public string categoryId { get; set; }
        public string propertyId { get; set; }
        public string value { get; set; }

        public double GetValueDouble()
        { return Convert.ToDouble(value); }
        public DateTime GetValueDateTime()
        { return DateTime.Parse(value); }

        public bool GetValueBool()
        {
            return value == "true";
        }
    }
}
