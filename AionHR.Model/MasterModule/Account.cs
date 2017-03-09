using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.MasterModule
{
    public class Account:IEntity
    {
        public int? accountId;
        public int registrationId;
        public string accountName;
        public short languageId;
        public short timeZone;
        public string companyName;
    }
}
