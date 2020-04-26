using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging
{
    /// <summary>
    /// The base response result returned by a service
    /// </summary>
    public abstract class ResponseBase
    {
        /// <summary>
        /// indicate if request succeeded
        /// </summary>
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string errorName { get; set; }

        /// <summary>
        /// message returned with the request. In case of failure it returns the error message.
        /// </summary>
        public string Message { get; set; }
        public string Error { get; set; }
        public string LogId { get; set; }
        public string recordId { get; set; }


        public string Summary
        {
            get; set;

        }
    }
}