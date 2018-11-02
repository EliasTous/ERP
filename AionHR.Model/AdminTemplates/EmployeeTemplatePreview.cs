using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
   public  class EmployeeTemplatePreview:IEntity
    {
        public string employeeId { get; set; }

        public string teId { get; set; }

        public string languageId { get; set; }

        public string textBody { get; set; }

    }
}
