using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
    public class AdminDepartment:IEntity

    {
        public string departmentId { get; set; }
        public string departmentName { get; set; }
    }
}
