using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    public class AllowanceType : ModelBase
    {
        public string name { get; set; }
        public bool isCash { get; set; }
    }
}
