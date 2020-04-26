using Infrastructure;
using Model.Employees.Profile;
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
    public partial class Login : Page
    {



        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IMasterService _masterService = ServiceLocator.Current.GetInstance<IMasterService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        protected override void InitializeCulture()
        {
        

            base.InitializeCulture();
            //User came to english login so set the language to english           
            _systemService.SessionHelper.SetLanguage("en");
            LocalisationManager.Instance.SetEnglishLocalisation();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                ResourceManager1.RegisterIcon(Icon.Tick);
                ResourceManager1.RegisterIcon(Icon.Error);

                Store store = this.languageId.GetStore();
                store.DataSource = new object[]
                {
                new object[] { "1", "English" },
              
                };
            }
            languageId.HideBaseTrigger = true;
            this.languageId.Call("getTrigger(0).hide");
            languageId.Select(0);
            //ResourceManager1.RegisterIcon(Icon.Tick);
            //ResourceManager1.RegisterIcon(Icon.Error);
            if (Request.QueryString["timeout"] != null && Request.QueryString["timeout"].ToString() == "yes")
            {
              //  lblError.Text = Resources.Common.SessionDisconnected;
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
            if (!IsPostBack)
            {
                
                tbAccountName.IndicatorIcon = Icon.Accept;
                ResourceManager1.RegisterIcon(Icon.Accept);
            }
            if (!IsPostBack && Request.Cookies["email"] != null)
            {
                tbUsername.Text = Request.Cookies["email"].Value;
                tbPassword.Text = Request.Cookies["password"].Value;
                rememberMeCheck.Checked = true;
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

                lblError.Text = getACResponse.Error;
                return "error";//Error in authentication
            }

            _systemService.SessionHelper.Set("AccountId", getACResponse.result.accountId);
            AuthenticateRequest request = new AuthenticateRequest();

            request.UserName = tbUsername.Text;
            request.Password = EncryptionHelper.encrypt(tbPassword.Text);
            AuthenticateResponse response = _systemService.Authenticate(request);
            if (response.User==null)
            {
                if (string.IsNullOrEmpty(response.Error))
                    lblError.Text = GetGlobalResourceObject("Errors", "authenticationError").ToString();
                else
                    lblError.Text = response.Error;
             
                return "error";
            }
            if ((ActiveStatus)response.User.activeStatus == ActiveStatus.INACTIVE)
            {
                lblError.Text = GetGlobalResourceObject("Errors", "inactiveUser").ToString();
                return "error";
            }
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
                //switch (response.User.languageId)
                //{
                //    case 1: _systemService.SessionHelper.SetLanguage("en");
                //        break;
                //    case 2:
                //        _systemService.SessionHelper.SetLanguage("ar");
                //        break;
                //    case 3: _systemService.SessionHelper.SetLanguage("fr");
                //        break;
                //    default: _systemService.SessionHelper.SetLanguage("en");
                //        break; 

                //}

                _systemService.SessionHelper.SetLanguage("en");

                _systemService.SessionHelper.Set("CompanyName", getACResponse.result.companyName);

                _systemService.SessionHelper.SetUserType(response.User.userType);
                _systemService.SessionHelper.SetEmployeeId(response.User.employeeId);
                _systemService.SessionHelper.Set("CurrentUserName", userName);

                _systemService.SessionHelper.Set("IsAdmin", response.User.isAdmin);
                StoreSystemDefaults();
                return "1";//Succeded

            }
            else
            {
                lblError.Text = response.Error;
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
                _systemService.SessionHelper.SetNameFormat("{firstName}{lastName} ");
            }
            try
            {
                _systemService.SessionHelper.SetCurrencyId(defaults.Items.Where(s => s.Key == "currencyId").First().Value);
            }
            catch
            {
                _systemService.SessionHelper.SetCurrencyId("0");
            }
            try
            {
                _systemService.SessionHelper.SetHijriSupport(Convert.ToBoolean(defaults.Items.Where(s => s.Key == "enableHijri").First().Value));
            }
            catch
            {
                _systemService.SessionHelper.SetHijriSupport(false);
            }
            //try
            //{
            //    _systemService.SessionHelper.SetDefaultTimeZone(Convert.ToInt32(defaults.Items.Where(s => s.Key == "timeZone").First().Value));
            //}
            //catch
            //{
            //    _systemService.SessionHelper.SetDefaultTimeZone(0);
            //}
            try
            {
                _systemService.SessionHelper.SetCalendarId(defaults.Items.Where(s => s.Key == "caId").First().Value);
            }
            catch
            {
                _systemService.SessionHelper.SetCalendarId("0");
            }
            try
            {
                _systemService.SessionHelper.SetVacationScheduleId(defaults.Items.Where(s => s.Key == "vsId").First().Value);
            }
            catch
            {
                _systemService.SessionHelper.SetVacationScheduleId("0");
            }
            try
            {
                EmployeeListRequest request = new EmployeeListRequest();
              //  request.BranchId = request.DepartmentId = request.PositionId = "0";
                request.StartAt = "0";
                request.SortBy = "hireDate";
                request.Size = "1";
           //     request.IncludeIsInactive = 2;
                var resp = _employeeService.GetAll<Employee>(request);
                
                _systemService.SessionHelper.SetStartDate(resp.Items[0].hireDate.Value);
            }
            catch(Exception exp)
            {
                _systemService.SessionHelper.SetStartDate(DateTime.Now);
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
                
                lblError.Text =  response.Error;
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

        protected void Change_language(object sender, DirectEventArgs e)
        {
        string language=e.ExtraParams["value"];
           
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
    }
}