using AionHR.Model.MasterModule;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using AionHR.Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                if (_systemService.SessionHelper.CheckIfArabicSession())
                    Response.Redirect("ARLogin.aspx?timeout=yes", true);
                else
                    Response.Redirect("Login.aspx?timeout=yes", true);
            }

            if (!X.IsAjaxRequest)
            {
                this.ResourceManager1.DirectEventUrl = this.Request.Url.AbsoluteUri;
            }

            if (!IsPostBack && !X.IsAjaxRequest)
            {
                SetExtLanguage();
                SetHeaderStyle();
                CompanyNameLiteral.Text = "- "+_systemService.SessionHelper.Get("CompanyName").ToString();
                //Building the tree
                _systemService.SessionHelper.Set("ActiveModule", "-1");
                BuildTree(1);
                
               // TryRegister();
            }
        }

        private void TryRegister()
        {
            Registration r = new Registration();
            r.company = "sss";
            r.email = "george.kalsssssash@softmachine.co";
            r.languageId = 1;
            r.name = "sss";
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
            acc.accountName = "sss";
            acc.companyName = "sss";
            acc.languageId = 1;
            PostRequest<Account> accountRequest = new PostRequest<Account>();
            PostResponse<Account> accountResponse = null;
            try
            {
                accountResponse = _masterService.AddAccount(acc);


            }
            catch { }
            DbSetup s = new DbSetup();
            s.accountId = Convert.ToInt32(accountResponse.recordId);
            s.email = "george.kalsssssash@softmachine.co";
            s.fullName = "georgessss kalash";

            s.languageId = r.languageId;
            s.password = "123";

            PostRequest<DbSetup> dbRequest = new PostRequest<DbSetup>();

            try
            {
                PostResponse<DbSetup> dbResponse = _masterService.CreateDB(s);
            }
            catch { }

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
                    tabHome.Loader.Url = "Employees.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();

                case 3:
                    nodes = TreeBuilder.Instance.BuildCompanyStructureTree(commonTree.Root);
                    tabHome.Loader.Url = "OrganizationChart.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();
                case 4:
                    nodes = TreeBuilder.Instance.BuildTimeManagementTree(commonTree.Root);
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
                    logBodyForm.SetValues(log.result);
                    logBodyScreen.Show();
                    break;
                default: int x = 0;
                    break;

              
            }


        }

        [DirectMethod]
        public void FillTransactionLogWindow(int classRef, int recordId)
        {
           
            TransactionLogListRequest request = new TransactionLogListRequest();
            request.ClassId = classRef;
            request.PrimaryKey = recordId;
            request.UserId = 0;
            request.Type = 0;
            request.StartAt = "1";
            request.Size = "50";
            request.SoryBy = "eventDt";
            ListResponse<TransactionLog> logs = _systemService.ChildGetAll<TransactionLog>(request);
            if(!logs.Success)
            {

            }
            transactionLogStore.DataSource = logs.Items;
            transactionLogStore.DataBind();
            TransationLogScreen.Show();
        }
    }
}