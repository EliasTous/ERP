using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
    /// <summary>
    /// Class holding the parameter to authenticate the user
    /// </summary>
    public class AuthenticateRequest:RequestBase
    {
        /// <summary>
        /// Company account
        /// </summary>
       

        /// <summary>
        /// User name 
        /// </summary>
        public string UserName{ get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        private Dictionary<String, String> parameters;
        public override Dictionary<String, String> Parameters
        {
            get
            {
                parameters = base.Parameters;
                
                parameters.Add("_email", UserName);
                parameters.Add("_password", Password);
                return parameters;
            }
        }
    }
}
