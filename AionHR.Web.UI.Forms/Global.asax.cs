using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Logging;
using DevExpress.XtraReports.Security;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AionHR.Web.UI.Forms
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            BootStrapper.ConfigureStructureMap();

            //Building up the factories 
            ApplicationSettingsFactory.InitializeApplicationSettingsFactory
                               (ServiceLocator.Current.GetInstance<IApplicationSettings>());

            LoggingFactory.InitializeLogFactory(ServiceLocator.Current.GetInstance<ILogger>());
            ScriptPermissionManager.GlobalInstance = new ScriptPermissionManager(ExecutionMode.Unrestricted);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
           {
          
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls;
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11;
            // Add TLS 1.2
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

            }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}