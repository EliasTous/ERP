using Model.MasterModule;
using Services.Implementations;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.System;
using Web.UI.Forms.Utilities;
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

namespace Web.UI.Forms
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IMasterService _masterService = ServiceLocator.Current.GetInstance<IMasterService>();



        protected override void InitializeCulture()
        {

            switch (_systemService.SessionHelper.getLangauge())
            {
                case "ar":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetArabicLocalisation();
                    }
                    break;
                case "en":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetEnglishLocalisation();
                    }
                    break;

                case "fr":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetFrenchLocalisation();
                    }
                    break;
                case "de":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetGermanyLocalisation();
                    }
                    break;
                default:
                    {


                        base.InitializeCulture();
                        LocalisationManager.Instance.SetEnglishLocalisation();
                    }
                    break;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {



            Store store = this.languageId.GetStore();
            store.DataSource = new object[]
            {
                new object[] { "1", "English" },

            };
        
            languageId.HideBaseTrigger = true;
            this.languageId.Call("getTrigger(0).hide");
            languageId.Select(0);

            if (Request.QueryString["timeout"] != null && Request.QueryString["timeout"].ToString() == "yes")
            {
                lblError.Text = Resources.Common.SessionDisconnected;
            }
            ResourceManager1.RegisterIcon(Icon.Tick);
            ResourceManager1.RegisterIcon(Icon.Error);
        }
        protected void Change_language(object sender, DirectEventArgs e)
        {
            string language = e.ExtraParams["value"];

            if (string.IsNullOrEmpty(language))
            {
                language = "1";
                Response.Redirect("~/Login.aspx");
                return;
            }


            switch (language)
            {
                case "1":
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    break;
                case "2":
                    Response.Redirect("~/ARLogin.aspx");
                    break;
                case "3":
                    Response.Redirect("~/FRLogin.aspx");
                    break;
                case "4":
                    Response.Redirect("~/DELogin.aspx");
                    break;
                default:
                    Response.Redirect("~/Login.aspx");
                    break;


            }

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