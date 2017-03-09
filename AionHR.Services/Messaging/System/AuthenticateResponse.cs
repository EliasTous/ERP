using AionHR.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
    /// <summary>
    /// Returned class object after making an authentication request
    /// </summary>
    public class AuthenticateResponse:ResponseBase
    {
        /// <summary>
        /// the record returned after a sign in operation
        /// </summary>
        public UserInfo User { get; set; }

    }
}
