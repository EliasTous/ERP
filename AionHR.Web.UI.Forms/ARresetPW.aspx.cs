using AionHR.Infrastructure;
using AionHR.Model.MasterModule;
using AionHR.Model.System;
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
    public partial class ARresetPW : System.Web.UI.Page
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
            ResetPassword RP = new ResetPassword(); 
            RP.Email = Request.QueryString["email"];
            RP.Guid = Request.QueryString["guid"];
            RP.NewPassword = EncryptionHelper.encrypt(tbPassword.Text);
            PostRequest<ResetPassword> depReq = new PostRequest<ResetPassword>();
            depReq.entity = RP;

            PostResponse<ResetPassword> response = _systemService.ChildAddOrUpdate<ResetPassword>(depReq);

            if (response.Success)
            {
                X.Msg.Alert("Success", "Your Password Has been changed succesfully!");
                Response.Redirect("~/Login.aspx");
            }
            else
                lblError.Text = response.Error;


        }


    }
}