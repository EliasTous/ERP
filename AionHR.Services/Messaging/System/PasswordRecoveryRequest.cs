using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.System
{
    public class ResetPasswordRequest:RequestBase
    {
        public string Email { get; set; }

       
        
        public string Guid { get; set; }

        public string NewPassword { get; set; }


        private Dictionary<String, String> parameters;

        public override Dictionary<String, String> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_email", Email);
                parameters.Add("_guid", Guid);
                parameters.Add("_newPassword", NewPassword);
                return parameters;
            }
        }
    }
}
