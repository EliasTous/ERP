using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.MasterModule
{
    public class Account : IEntity
    {
        public int? accountId { get; set; }
        public int registrationId { get; set; }
        public string accountName { get; set; }
        public short languageId { get; set; }
        public short timeZone { get; set; }
        public string companyName { get; set; }

        public bool trial { get; set; }
    }
}
