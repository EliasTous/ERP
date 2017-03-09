using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.MasterModule
{
   public class Registration:ModelBase
    {
        public short status;
        public string name;
        public string email;
        public string company;
        public short languageId;
        public string confirmationId;
    }
}
