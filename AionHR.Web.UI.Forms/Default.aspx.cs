using AionHR.Infrastructure;
using AionHR.Model.Access_Control;
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
        /// <summary>
        /// Could be added in a base page, but we keep it here in order to control the page UI. This method should be copied to each page
        /// </summary>
        protected override void InitializeCulture()
        {

            bool rtl = true;
            if (!_systemService.SessionHelper.CheckIfArabicSession())
            {
                rtl = false;
                base.InitializeCulture();
                LocalisationManager.Instance.SetEnglishLocalisation();
            }

            if (rtl)
            {
                base.InitializeCulture();
                LocalisationManager.Instance.SetArabicLocalisation();
            }

        }
        private bool HandleExternalUrl()
        {
            string decrypted = EncryptionHelper.decrypt(Request.QueryString["param"], null);
            var parsed = HttpUtility.ParseQueryString(decrypted);
            if (string.IsNullOrEmpty(parsed["_a"]) || string.IsNullOrEmpty(parsed["_u"]) || string.IsNullOrEmpty(parsed["_p"]) || string.IsNullOrEmpty(parsed["_c"]))
                return false;
          
            
            UrlKeyRequest reqkey = new UrlKeyRequest();
            reqkey.keyId = Request.QueryString["param"];
            RecordResponse<KeyId> keyresp = _systemService.ChildGetRecord<KeyId>(reqkey);
            if(!keyresp.Success)
            {
                return false;
            }
            
            AuthenticateRequest req = new AuthenticateRequest();
            req.UserName = parsed["_u"];
            req.Password = parsed["_p"];
            _systemService.SessionHelper.Set("AccountId", parsed["_a"]);
            AuthenticateResponse resp = _systemService.Authenticate(req);
            if (!resp.Success)
            {
                return false;
            }
            X.Call("openNewTab","", PageLookup.GetPageUrlByClassId(Convert.ToInt32(parsed["_c"])),"" , "");
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty( Request.QueryString["param"]))
            {
                bool success = HandleExternalUrl();
                if(!success)
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
                if (_systemService.SessionHelper.CheckIfArabicSession())
                    Response.Redirect("ARLogin.aspx?timeout=yes", true);
                else
                    Response.Redirect("Login.aspx?timeout=yes", true);
            }

            //if (!X.IsAjaxRequest)
            //{
            //    this.ResourceManager1.DirectEventUrl = this.Request.Url.AbsoluteUri;
            //}
            //this.ResourceManager1.DirectEventUrl = this.ResourceManager1.DirectEventUrl.Replace("http", "https");
            if (!IsPostBack && !X.IsAjaxRequest)
            {
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
                    btnCompany.Hidden = btnEmployeeFiles.Hidden = btnPayroll.Hidden = btnReport.Hidden = btnScheduler.Hidden = true;
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

        private void TryRegister()
        {
            Registration r = new Registration();
            r.company = "ssss";
            r.email = "george.kalssash@softmachine.co";
            r.languageId = 1;
            r.name = "ssss";
            PostRequest<Registration> req = new PostRequest<Registration>();
            req.entity = r;

            PostResponse<Registration> response = null;
            try
            {
                response = _masterService.AddRegistration(r);
            }
            catch { }




            Account acc = new Account();
            acc.registrationId = Convert.ToInt32(response.recordId);
            acc.accountName = "ssss";
            acc.companyName = "ssss";
            acc.languageId = 1;
            PostRequest<Account> accountRequest = new PostRequest<Account>();
            PostResponse<Account> accountResponse = null;
            try
            {
                accountResponse = _masterService.AddAccount(acc);


            }
            catch { }
            DbSetup set = new DbSetup();
            set.accountId = Convert.ToInt32(accountResponse.recordId);


            PostRequest<DbSetup> dbRequest = new PostRequest<DbSetup>();
            dbRequest.entity = set;
            try
            {
                PostResponse<DbSetup> dbResponse = _masterService.CreateDB(set);
            }
            catch { }

            BatchSql bat = new BatchSql();
            try
            {
                PostResponse<BatchSql> dbResponse = _systemService.RunSqlBatch(bat);
            }
            catch { }
            UserInfo s = new UserInfo();
            s.email = "george.kalssash@softmachine.co";
            s.fullName = "georgess kalash";
            s.accountId = accountResponse.recordId;
            s.languageId = r.languageId;
            s.password = "123";
            s.recordId = null;
            s.isAdmin = true;
            s.isInactive = false;
            PostRequest<UserInfo> userReq = new PostRequest<UserInfo>();
            userReq.entity = s;
            try
            {
                PostResponse<UserInfo> userResp = _systemService.ChildAddOrUpdate<UserInfo>(userReq);
            }
            catch
            {

            }

        }

        private void SetHeaderStyle()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            HtmlLink css = new HtmlLink();
            if (rtl)
                css.Href = "CSS/HeaderInsideAR.css";
            else
                css.Href = "CSS/HeaderInside.css";

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
                    tabHome.Loader.Url = "MyInfos.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                case 8:
                    nodes = TreeBuilder.Instance.BuildAdminTemplates(commonTree.Root);
                    tabHome.Loader.Url = "MyInfos.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                case 9:
                    nodes = TreeBuilder.Instance.BuildAdminTemplates(commonTree.Root);
                    tabHome.Loader.Url = "AdminTemplates.aspx";
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


    }
}