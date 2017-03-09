using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Leaves
{
    public class Leave:ModelBase
    {
        public int? employeeId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int ltId { get; set; }
        public string ltName { get; set; }
        public short status { get; set; }
        public bool isPaid { get; set; }
        public string destination { get; set; }
        public string justification { get; set; }
    }
}
