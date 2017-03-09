using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    public class Currency:ModelBase
    {
        public string reference { get; set; }
        public string name { get; set; }
    }
}
