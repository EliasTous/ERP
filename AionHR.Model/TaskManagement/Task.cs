using AionHR.Infrastructure.Domain;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TaskManagement
{
    public class Task:ModelBase,IEntity
    {
        public int assignToId { get; set; }
        public int inRelationToId { get; set; }
        public int ttId { get; set; }
        public DateTime dueDate { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public bool completed { get; set; }

        // get
        public EmployeeName assignToName { get; set; }
        public EmployeeName inRelationToName { get; set; }
        public string ttName { get; set; }
    }
}
