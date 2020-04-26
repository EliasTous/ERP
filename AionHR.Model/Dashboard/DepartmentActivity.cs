using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
   public class DepartmentActivity
    {
        public string departmentName { get; set; }

        public int checkedIn { get; set; }

        public int checkedOut { get; set; }
    }
}
