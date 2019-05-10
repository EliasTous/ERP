using AionHR.Infrastructure;
using AionHR.Model.Access_Control;
using AionHR.Model.Employees.Profile;
using AionHR.Model.MasterModule;
using AionHR.Model.Payroll;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using AionHR.Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms
{
    public partial class Default : System.Web.UI.Page
    {


        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IMasterService _masterService = ServiceLocator.Current.GetInstance<IMasterService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        /// <summary>
        /// Could be added in a base page, but we keep it here in order to control the page UI. This method should be copied to each page
        /// </summary>
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
        private bool HandleExternalUrl()
        {
            string decrypted = EncryptionHelper.decrypt(Request.QueryString["param"], null);
            var parsed = HttpUtility.ParseQueryString(decrypted);
            if (string.IsNullOrEmpty(parsed["_a"]) || string.IsNullOrEmpty(parsed["_e"]) || string.IsNullOrEmpty(parsed["_p"]) || string.IsNullOrEmpty(parsed["_c"]))
                return false;


            UrlKeyRequest reqkey = new UrlKeyRequest();
            reqkey.keyId = Server.UrlEncode(Request.QueryString["param"]);
            _systemService.SessionHelper.Set("AccountId", parsed["_a"]);



            AuthenticateRequest req = new AuthenticateRequest();
            req.UserName = parsed["_e"];
            req.Password = parsed["_p"];
            _systemService.SessionHelper.Set("AccountId", parsed["_a"]);
            AuthenticateResponse resp = _systemService.Authenticate(req);

            if (!resp.Success)
            {
                return false;
            }
            if (resp.User.languageId == 2)
                _systemService.SessionHelper.SetLanguage("ar");
            else
                _systemService.SessionHelper.SetLanguage("en");
            RecordResponse<KeyId> keyresp = _systemService.ChildGetRecord<KeyId>(reqkey);
            if (!keyresp.Success)
            {
                return false;
            }
            _systemService.SessionHelper.Set("CompanyName", " ");

            _systemService.SessionHelper.SetUserType(resp.User.userType);
            _systemService.SessionHelper.SetEmployeeId(resp.User.employeeId);
            _systemService.SessionHelper.Set("CurrentUserName", parsed["_e"]);

            _systemService.SessionHelper.Set("IsAdmin", resp.User.isAdmin);
            StoreSystemDefaults();
            string url = PageLookup.GetPageUrlByClassId(Convert.ToInt32(parsed["_c"])) + "?" +parsed["_k"].Replace('#', '&');
            X.Call("openNewTab", parsed["_c"], url, GetGlobalResourceObject("Classes", "Class" + parsed["_c"]), "icon-Employees");
            //Response.Redirect("Default.aspx");
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack && !X.IsAjaxRequest&&!string.IsNullOrEmpty(Request.QueryString["param"]))
            {
                bool success = HandleExternalUrl();
                if (!success)
                    Response.Redirect("Login.aspx?timeout=yes", true);


            }
            try
            {
                if (CheckSession() == "0")
                    Logout();
            }
            catch
            {
                Logout();
            }
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                switch (_systemService.SessionHelper.getLangauge())
                {
                    case "1":
                        Response.Redirect("Login.aspx?timeout=yes", true);
                        break;
                    case "2":
                        Response.Redirect("ARLogin.aspx?timeout=yes", true);
                        break;
                    case "3":
                        Response.Redirect("FRLogin.aspx?timeout=yes", true);
                        break;
                    case "4":
                        Response.Redirect("DELogin.aspx?timeout=yes", true);
                        break;
                    default: Response.Redirect("Login.aspx?timeout=yes", true);
                        break;
                }


             
                  
            }

            //if (!X.IsAjaxRequest)
            //{
            //    this.ResourceManager1.DirectEventUrl = this.Request.Url.AbsoluteUri;
            //}
            //this.ResourceManager1.DirectEventUrl = this.ResourceManager1.DirectEventUrl.Replace("http", "https");
            if (!IsPostBack && !X.IsAjaxRequest)
            {
                if (!X.IsAjaxRequest)
                {
                    ResourceManager1.RegisterIcon(Icon.Tick);
                    ResourceManager1.RegisterIcon(Icon.Error);

                    Store store = this.languageId.GetStore();
                    store.DataSource = new object[]
                    {
                new object[] { "1", "English" },
                new object[] { "2", "عربي" },
                new object[] { "3", "Français" },
                 new object[] { "4", "Deutsch" }
                    };
                }
                languageId.HideBaseTrigger = true;
                this.languageId.Call("getTrigger(0).hide");
                switch (_systemService.SessionHelper.getLangauge())
                {
                    case "en":languageId.Select(0);
                        break; 
                    case "ar":languageId.Select(1);
                        break;
                    case "fr":
                        languageId.Select(2);
                        break;
                    case "de":
                        languageId.Select(3);
                        break;
                    
                }
                SetExtLanguage();
                SetHeaderStyle();
                CompanyNameLiteral.Text = "" + _systemService.SessionHelper.Get("CompanyName").ToString();
                username.Text = _systemService.SessionHelper.Get("CurrentUserName").ToString();
                //Building the tree
                _systemService.SessionHelper.Set("ActiveModule", "-1");

                transactionDate.Format = _systemService.SessionHelper.GetDateformat() + ", hh:mm:ss";
                if (string.IsNullOrEmpty(activeModule.Text))
                    activeModule.Text = "7";
                //TryRegister();
        
                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    btnSelfService.Disabled = true;
                if (_systemService.SessionHelper.GetUserType() == 4)
                {
                    b1.Hidden = true;
                  btnAdminAffairs.Hidden=  btnCompany.Hidden = btnEmployeeFiles.Hidden = btnPayroll.Hidden = btnReport.Hidden = btnScheduler.Hidden = true;
                    sep1.Hidden = sep2.Hidden = sep3.Hidden = sep4.Hidden = true;
                    BuildTree(7);
                }
                else
                {
                  
                    BuildTree(1);
                }

                DefaultTabCloseMenu.CloseAllTabsText = GetGlobalResourceObject("Common", "CloseAllTabsText").ToString();
                DefaultTabCloseMenu.CloseOthersTabsText= GetGlobalResourceObject("Common", "CloseOthersTabsText").ToString();
                DefaultTabCloseMenu.CloseTabText= GetGlobalResourceObject("Common", "CloseTabText").ToString();
             

            }
        }

        //private void TryRegister()
        //{
        //    Registration r = new Registration();
        //    r.company = "ssss";
        //    r.email = "george.kalssash@softmachine.co";
        //    r.languageId = 1;
        //    r.name = "ssss";
        //    PostRequest<Registration> req = new PostRequest<Registration>();
        //    req.entity = r;

        //    PostResponse<Registration> response = null;
        //    try
        //    {
        //        response = _masterService.AddRegistration(r);
        //    }
        //    catch { }




        //    Account acc = new Account();
        // //   acc.registrationId = Convert.ToInt32(response.recordId);
        //    acc.accountName = "ssss";
        //    acc.companyName = "ssss";
        //    acc.languageId = 1;
        //    PostRequest<Account> accountRequest = new PostRequest<Account>();
        //    PostResponse<Account> accountResponse = null;
        //    try
        //    {
        //        accountResponse = _masterService.AddAccount(acc);


        //    }
        //    catch { }
        //    DbSetup set = new DbSetup();
        //    set.accountId = Convert.ToInt32(accountResponse.recordId);


        //    PostRequest<DbSetup> dbRequest = new PostRequest<DbSetup>();
        //    dbRequest.entity = set;
        //    try
        //    {
        //        PostResponse<DbSetup> dbResponse = _masterService.CreateDB(set);
        //    }
        //    catch { }

        //    BatchSql bat = new BatchSql();
        //    try
        //    {
        //        PostResponse<BatchSql> dbResponse = _systemService.RunSqlBatch(bat);
        //    }
        //    catch { }
        //    UserInfo s = new UserInfo();
        //    s.email = "george.kalssash@softmachine.co";
        //    s.fullName = "georgess kalash";
        //    s.accountId = accountResponse.recordId;
        //    s.languageId = r.languageId;
        //    s.password = "123";
        //    s.recordId = null;
        //    s.isAdmin = true;
        //    s.isInactive = false;
        //    PostRequest<UserInfo> userReq = new PostRequest<UserInfo>();
        //    userReq.entity = s;
        //    try
        //    {
        //        PostResponse<UserInfo> userResp = _systemService.ChildAddOrUpdate<UserInfo>(userReq);
        //    }
        //    catch
        //    {

        //    }

        //}

        private void SetHeaderStyle()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            HtmlLink css = new HtmlLink();
            if (rtl)
                css.Href = "CSS/HeaderInsideAR.css?id=10";
            else
                css.Href = "CSS/HeaderInside.css?id=20";

            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            css.Attributes["media"] = "all";
            Page.Header.Controls.Add(css);
        }

        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
        }


        [DirectMethod]
        public string BuildTree(int id)
        {
            object activeModule = _systemService.SessionHelper.Get("ActiveModule");
            if (activeModule != null && activeModule.ToString() == id.ToString())
            {
                return "Stop";

            }
            //setting session and continue
            _systemService.SessionHelper.Set("ActiveModule", id);
            Ext.Net.NodeCollection nodes = null;
            switch (id)
            {
                case 1:
                    nodes = TreeBuilder.Instance.BuildEmployeeFilesTree(commonTree.Root);
                    tabHome.Loader.Url = "Dashboard.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();

                case 3:
                    nodes = TreeBuilder.Instance.BuildCompanyStructureTree(commonTree.Root);
                    tabHome.Loader.Url = "Dashboard.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                case 4:
                    nodes = TreeBuilder.Instance.BuildTimeManagementTree(commonTree.Root);
                    tabHome.Loader.Url = "Dashboard.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                case 5:
                    nodes = TreeBuilder.Instance.BuildReportsTree(commonTree.Root);
                    tabHome.Loader.Url = "Dashboard.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                case 6:
                    nodes = TreeBuilder.Instance.BuildPayrollTree(commonTree.Root);
                    tabHome.Loader.Url = "Dashboard.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                case 7:
                    nodes = TreeBuilder.Instance.BuildSelftService(commonTree.Root);
                    if (_systemService.SessionHelper.GetUserType() != 4)
                        tabHome.Loader.Url = "Dashboard.aspx";
                    else
                        tabHome.Loader.Url = "BlankPage.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                case 8:
                    nodes = TreeBuilder.Instance.BuildAdminTemplates(commonTree.Root);
                    tabHome.Loader.Url = "Dashboard.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                case 9:
                    nodes = TreeBuilder.Instance.BuildAdminTemplates(commonTree.Root);
                    tabHome.Loader.Url = "Dashboard.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                default:
                    nodes = TreeBuilder.Instance.BuildCaseManagementTree(commonTree.Root);
                    return nodes.ToJson();

            }


        }


        [DirectMethod()]
        public string Localise()
        {
            if (_systemService.SessionHelper.CheckIfArabicSession())
                _systemService.SessionHelper.SetLanguage("en");
            else
                _systemService.SessionHelper.SetLanguage("ar");

            return "ok";

        }

        [DirectMethod()]
        public string Logout()
        {

            string toReturn = string.Empty;
            if (_systemService.SessionHelper.CheckIfArabicSession())
            {
                toReturn = "ARLogin.aspx";
            }
            else
                toReturn = "Login.aspx";
            _systemService.SessionHelper.ClearSession();
            return toReturn;

        }
        [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }

        protected void TransactionLog_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            List<Ext.Net.Parameter> l = e.Parameters.ToList<Ext.Net.Parameter>();

            //GEtting the filter from the page
            TransactionLogListRequest request = new TransactionLogListRequest();
            request.ClassId = Convert.ToInt32(l[0].Value);
            request.PrimaryKey = Convert.ToInt32(l[1].Value);
            request.UserId = 0;
            request.Type = 0;
            request.StartAt = e.Start.ToString();
            request.Size = e.Limit.ToString();
            request.SoryBy = "eventDt";
            ListResponse<TransactionLog> logs = _systemService.ChildGetAll<TransactionLog>(request);
            if (!logs.Success)
            {
                return;
            }

            e.Total = logs.count;
            transactionLogStore.DataSource = logs.Items;
            transactionLogStore.DataBind();
            TransationLogScreen.Show();
        }

        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<TransactionLog> log = _systemService.ChildGetRecord<TransactionLog>(r);
                    string x = "";
                    try
                    {
                        JObject json = JObject.Parse(log.result.data);
                        string formatted = json.ToString();
                        x = formatted;
                    }
                    catch
                    {
                        x = log.result.data;
                    }
                    log.result.data = x;
                    logBodyForm.SetValues(log.result);
                    logBodyScreen.Show();
                    break;
                default:

                    break;


            }


        }

        [DirectMethod]
        public void FillTransactionLogWindow(int classRef, int recordId)
        {
            CurrentClassRef.Text = classRef.ToString();
            CurrentRecordId.Text = recordId.ToString();
            Ext.Net.ParameterCollection col = new Ext.Net.ParameterCollection();
            col.Add(new Ext.Net.Parameter() { Name = "ClassId", Value = classRef.ToString() });
            col.Add(new Ext.Net.Parameter() { Name = "PrimaryKey", Value = recordId.ToString() });
            transactionLogStore.Reload(col);

        }

        protected void SyncLoanDeductions(object sender, DirectEventArgs e)
        {
            PostRequest<SyncED> req = new PostRequest<SyncED>();
            PostResponse<SyncED> resp = _payrollService.ChildAddOrUpdate<SyncED>(req);
            if (!resp.Success)
            { //Show an error saving...

                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
                return;

            }
            else
            {
                X.Msg.Alert("", GetGlobalResourceObject("Common", "LoanSyncSucc").ToString()).Show();
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
            try
            {
                _systemService.SessionHelper.SetDefaultTimeZone(Convert.ToInt32(defaults.Items.Where(s => s.Key == "timeZone").First().Value));
            }
            catch
            {
                _systemService.SessionHelper.SetDefaultTimeZone(0);
            }
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
            catch (Exception exp)
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
        [DirectMethod]
        public void Change_language(string language )
        {
            

            if (string.IsNullOrEmpty(language))
            {
                language = "1";
                _systemService.SessionHelper.SetLanguage("en");
                return;
            }


            switch (language)
            {
                case "1":
                    {
                        _systemService.SessionHelper.SetLanguage("en");
                    }
                    break;
                case "2":
                  
                    _systemService.SessionHelper.SetLanguage("ar");
                    SetHeaderStyle();
                    break;
                case "3":
                    _systemService.SessionHelper.SetLanguage("fr");
                    break;
                case "4":
                    _systemService.SessionHelper.SetLanguage("de");
                    break;
                default:
                    _systemService.SessionHelper.SetLanguage("en");
                  
                    break;
                  

            }

            Response.Redirect("~/Default.aspx");

        }

    }
}