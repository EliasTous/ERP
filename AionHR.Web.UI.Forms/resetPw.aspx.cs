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
    public partial class resetPw : System.Web.UI.Page
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
            ValidateQueryStringParams();
            if (Session["AccountId"] == null || Session["AccountId"].ToString() == "0")
                Session["AccountId"] = Request.QueryString["accountId"];
        }

        private void ValidateQueryStringParams()
        {
            if (Request.QueryString["guid"] == null || Request.QueryString["accountId"] == null || Request.QueryString["email"] == null)
                Response.Redirect("~/ForgotPassword.aspx");

        }

        protected void login_Click(object sender, EventArgs e)
        {
            ResetPasswordRequest request = new ResetPasswordRequest();
            request.Email = Request.QueryString["email"];
            request.Guid = Request.QueryString["guid"];
            request.NewPassword = tbPassword.Text;

            PasswordRecoveryResponse response = _systemService.ResetPassword(request);

            if (response.Success)
            {
                X.Msg.Alert("Success", "Your Password Has been changed succesfully!");
                Response.Redirect("~/Login.aspx");
            }
            else
                lblError.Text = response.Summary;


        }



    }
}