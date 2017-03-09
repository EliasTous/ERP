using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.Configuration
{
    /// <summary>
    /// Application setting
    /// </summary>
    public class WebConfigApplicationSettings: IApplicationSettings
    {
        /// <summary>
        /// Base address of the webservice url
        /// </summary>
        public string BaseURL
        {
            get { return ConfigurationManager.AppSettings["BaseURL"]; }
        }
        /// <summary>
        /// The logger name
        /// </summary>
        public string LoggerName
        {
            get { return ConfigurationManager.AppSettings["LoggerName"]; }
        }
    }
}
