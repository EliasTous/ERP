using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Domain;

namespace AionHR.Model.Employees.Profile
{
   public class DocumentType : ModelBase
    {
        public string name { get; set; }
        public string intName { get; set; }
    }
}
