using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AdminTemplates
{
  public  class GenerateAdminDocumentDue
    {
        public string doId   { get; set; }
        public DateTime startingDate
        { get; set; }
        public short frequency
        { get; set; }
        public short count
        { get; set; }
        public double amount { set; get; }
    }
}
