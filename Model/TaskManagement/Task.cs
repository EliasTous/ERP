using Infrastructure.Domain;
using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TaskManagement
{
    [ClassIdentifier("32010", "32")]
    public class Task:ModelBase,IEntity
    {
        [PropertyID("32010_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("32010_02")]
        [ApplySecurity]
        public int assignToId { get; set; }
        [PropertyID("32010_03")]
        [ApplySecurity]
        public int? inRelationToId { get; set; }
        [PropertyID("32010_04")]
        [ApplySecurity]
        public int ttId { get; set; }
        [PropertyID("32010_05")]
        [ApplySecurity]
        public DateTime dueDate { get; set; }
        [PropertyID("32010_06")]
        [ApplySecurity]
        public string description { get; set; }
        [PropertyID("32010_07")]
        [ApplySecurity]
        public bool completed { get; set; }

        // get 
        [PropertyID("32010_02")]
        [ApplySecurity]
        public EmployeeName assignToName { get; set; }
        [PropertyID("32010_03")]
        [ApplySecurity]
        public EmployeeName inRelationToName { get; set; }
        [PropertyID("32010_04")]
        [ApplySecurity]
        public string ttName { get; set; }
    }
}
