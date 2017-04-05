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
    public partial class Login : Page
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
            ResourceManager1.RegisterIcon(Icon.Tick);
            ResourceManager1.RegisterIcon(Icon.Error);
            if (Request.QueryString["timeout"] != null && Request.QueryString["timeout"].ToString() == "yes")
            {
                //lblError.Text = Resources.Common.SessionDisconnected;
            }
            if (!IsPostBack && Request.QueryString["account"] != null)
            {
                tbAccountName.Text = Request.QueryString["account"];
                DirectCheckField(tbAccountName.Text);
            }
            if (!IsPostBack && Request.Cookies["accountName"] != null)
            {
                tbAccountName.Text = Request.Cookies["accountName"].Value;
                DirectCheckField(tbAccountName.Text);
            }
            if (!IsPostBack && Request.Cookies["email"] != null)
            {
                tbUsername.Text = Request.Cookies["email"].Value;
                tbPassword.Text = Request.Cookies["password"].Value;
                rememberMeCheck.Checked = true;
            }

            if (!X.IsAjaxRequest)
            {
                ResourceManager1.RegisterIcon(Icon.Tick);
                ResourceManager1.RegisterIcon(Icon.Error);
            }
        }

        [DirectMethod]
        public string Authenticate(string accountName, string userName, string password)
        {
            GetAccountRequest GetACrequest = new GetAccountRequest();
            GetACrequest.Account = tbAccountName.Text;

            Response<Account> getACResponse = _masterService.GetAccount(GetACrequest);
            if(!getACResponse.Success)
            {
                lblError.Text = GetGlobalResourceObject("Errors", getACResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", getACResponse.ErrorCode).ToString() : getACResponse.Summary;
                return "error";//Error in authentication
            }

            _systemService.SessionHelper.Set("AccountId", getACResponse.result.accountId);
            AuthenticateRequest request = new AuthenticateRequest();

            request.UserName = tbUsername.Text;
            request.Password = tbPassword.Text;
            AuthenticateResponse response = _systemService.Authenticate(request);
            if (response.Success)
            {
                //Redirecting..
                Response.Cookies.Add(new HttpCookie("accountName", accountName) { Expires = DateTime.Now.AddDays(30) });
                if (rememberMeCheck.Checked)
                {
                    Response.Cookies.Add(new HttpCookie("email") { Value = userName, Expires = DateTime.Now.AddDays(30), });
                    Response.Cookies.Add(new HttpCookie("password") { Value = password, Expires = DateTime.Now.AddDays(30), });

                }
                else
                {
                    RemoveCookies();
                    
                }
                if (response.User.languageId == 3)
                    _systemService.SessionHelper.SetLanguage("ar");
                else
                    _systemService.SessionHelper.SetLanguage("en");

                _systemService.SessionHelper.Set("CompanyName", getACResponse.result.companyName);

                StoreSystemDefaults();
                return "1";//Succeded

            }
            else
            {
                lblError.Text = GetGlobalResourceObject("Errors", response.ErrorCode) !=null? GetGlobalResourceObject("Errors", response.ErrorCode).ToString():response.Summary;
                return "error";//Error in authentication

            }
        }

        private void StoreSystemDefaults()
        {
            ListRequest req = new ListRequest();
            ListResponse<KeyValuePair<string, string>> defaults = _systemService.ChildGetAll<KeyValuePair<string, string>>(req);
            if (!defaults.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, defaults.Summary).Show();
                return;
            }
            try
            {
                _systemService.SessionHelper.SetDateformat(defaults.Items.Where(s => s.Key == "dateFormat").First().Value);
            }
            catch
            {
                _systemService.SessionHelper.SetDateformat("MMM, dd, yyyy");
            }
            try
            {
                _systemService.SessionHelper.SetNameFormat(defaults.Items.Where(s => s.Key == "nameFormat").First().Value);
            }
            catch
            {
                _systemService.SessionHelper.SetDateformat("{firstName}{lastName} ");
            }
            try
            {
                _systemService.SessionHelper.SetCurrencyId(defaults.Items.Where(s => s.Key == "currencyId").First().Value);
            }
            catch
            {
                _systemService.SessionHelper.SetCurrencyId("0");
            }
        }
        private object GetDateFormat()
        {
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "dateFormat";
            RecordResponse<KeyValuePair<string, string>> response = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!response.Success)
            {
               
            }
            return response.result.Value;
        }
        private string GetNameFormat()
        {
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "nameFormat";
            RecordResponse<KeyValuePair<string, string>> response = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!response.Success)
            {

            }
            string paranthized = response.result.Value;
            paranthized = paranthized.Replace('{', ' ');
            paranthized = paranthized.Replace('}', ',');
            paranthized = paranthized.Substring(0, paranthized.Length - 1);
            paranthized = paranthized.Replace(" ", string.Empty);
            return paranthized;

        }

        private void RemoveCookies()
        {
            HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["email"];
            if (currentUserCookie == null)
                return;
            HttpContext.Current.Response.Cookies.Remove("email");
            currentUserCookie.Expires = DateTime.Now.AddDays(-10);
            currentUserCookie.Value = null;
            HttpContext.Current.Response.SetCookie(currentUserCookie);
            HttpCookie passwordCookie = HttpContext.Current.Request.Cookies["password"];
            HttpContext.Current.Response.Cookies.Remove("password");
            passwordCookie.Expires = DateTime.Now.AddDays(-10);
            passwordCookie.Value = null;
            HttpContext.Current.Response.SetCookie(passwordCookie);
        }

        [DirectMethod]
        public object DirectCheckField(string value)
        {
            //return true;
            GetAccountRequest request = new GetAccountRequest();
            request.Account = value;

            Response<Account> response = _masterService.GetAccount(request);

            if (response.Success)
            {

                tbAccountName.IndicatorIcon = Icon.Accept;
                ResourceManager1.RegisterIcon(Icon.Accept);

            }
            else
            {
                
                lblError.Text = GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() : response.Summary;
            }
            
            
            return response.Success;
        }
        protected void CheckField(object sender, RemoteValidationEventArgs e)
        {
            TextField field = (TextField)sender;
            GetAccountRequest request = new GetAccountRequest();
            request.Account = field.Text;

            Response<Account> response = _masterService.GetAccount(request);

            if (response.Success)
            {


                e.Success = true;

            }
            else
            {

              
            }
            //tbAccountName.ShowIndicator();

        }

        [DirectMethod]
        public string CheckFieldDirect(string accName)
        {
            
            GetAccountRequest request = new GetAccountRequest();
            request.Account = accName;

            Response<Account> response = _masterService.GetAccount(request);

            if (response.result!=null)
            {


                return "1";

            }
            else
            {

                return "0";
            }
           

        }


        protected void forgotpw_Event(object sender, EventArgs e)
        {
            Response.Redirect("~/ForgotPassword.aspx");

        }
    }
}