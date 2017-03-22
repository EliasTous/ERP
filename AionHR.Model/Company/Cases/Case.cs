using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Cases
{
    public  class Case : ModelBase,IEntity
    {
        public int employeeId { get; set; }
        public DateTime date { get; set; }
        public string details { get; set; }
        public short status { get; set; }
        public DateTime closedDate { get; set; }
    }
}
