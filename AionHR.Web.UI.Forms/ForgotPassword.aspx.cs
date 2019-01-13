using AionHR.Model.MasterModule;
using AionHR.Services.Implementations;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using AionHR.Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using StructureMap.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IMasterService _masterService = ServiceLocator.Current.GetInstance<IMasterService>();

        protected override void InitializeCulture()
        {

            base.InitializeCulture();
            //User came to english login so set the language to english           
            _systemService.SessionHelper.SetLanguage("en");
            LocalisationManager.Instance.SetEnglishLocalisation();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["timeout"] != null && Request.QueryString["timeout"].ToString() == "yes")
            {
                lblError.Text = Resources.Common.SessionDisconnected;
            }
            ResourceManager1.RegisterIcon(Icon.Tick);
            ResourceManager1.RegisterIcon(Icon.Error);
        }

        protected void login_Click(object sender, EventArgs e)
        {
            GetAccountRequest request = new GetAccountRequest();
            request.Account = tbAccountName.Text;
            Response<Account> account = _masterService.GetAccount(request);
            if(!account.Success)
            {
                lblError.Text = (String)GetLocalResourceObject(account.Message);
            }
            AccountRecoveryRequest recoverRequest = new AccountRecoveryRequest();
            recoverRequest.Email = tbUsername.Text;
            PasswordRecoveryResponse response = _systemService.RequestPasswordRecovery(recoverRequest);
            if (response.Success)
            {
                
                X.Msg.Alert("Recovery","Your Password has been sent to your email!");
                //Redirecting..
                Response.Redirect("Login.aspx", true);
            }
            else
            {
                lblError.Text = response.Error;//(String)GetLocalResourceObject(response.Message);

            }
        }

        
        protected void CheckField(object sender, RemoteValidationEventArgs e)
        {
            TextField field = (TextField)sender;
            GetAccountRequest request = new GetAccountRequest();
            request.Account = field.Text;

            Response<Account> response = _masterService.GetAccount(request);

            if (response.Success)
            {

                tbAccountName.IndicatorIcon = Icon.Accept;
                tbAccountName.IndicatorTip = "Identified";

                e.Success = true;
            }
            else
            {
                tbAccountName.IndicatorIcon = Icon.Error;

                e.Success = false;
                tbAccountName.IndicatorTip = "Unidentified";
                e.ErrorMessage = "Invalid Account";//should access local resources, just didn't figure how yet , only Resources.Common is accessible

            }

        }

        protected void forgotpw_Event()
        {

        }
    }
}