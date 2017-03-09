using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.MasterModule
{
    public class CrashLog:ModelBase
    {
        public int errorId { get; set; }
        public string errorMessage { get; set; }
        public int? userId { get; set; }
        public int? accountId { get; set; }
        public string extendedInfo { get; set; }
    }
}
