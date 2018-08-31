using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.WebService
{
    /// <summary>
    /// The base web service response returned when calling a web service methos
    /// </summary>
    public abstract class BaseWebServiceResponse
    {
        /// <summary>
        /// the stutus of the response
        /// </summary>
        public string statusId { get; set; }
        
        /// <summary>
        /// the message returned
        /// </summary>
        public string message { get; set; }

        public string error { get; set; }

        public string logId { get; set; }

        public string reference { get; set; }

        public string description { get; set; }

        public string Details
        {
            get
            {
                return string.Format("{0}<br> Reference: {1}</br>", error, logId);
            }
        }

    }
}
