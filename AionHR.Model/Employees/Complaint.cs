using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees
{
    public class Complaint : ModelBase,IEntity
    {
        public int employeeID { get; set; }
        public string actionTaken { get; set; }
        public string actionRequired { get; set; }
        public string complaintDetails { get; set; }
        public short status { get; set; }
    }
}
