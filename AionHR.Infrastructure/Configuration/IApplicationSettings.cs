using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.Configuration
{
    /// <summary>
    /// Interface all setttings
    /// </summary>
    public interface IApplicationSettings
    {
        string BaseURL { get; }
        string LoggerName { get; }
    }
}
