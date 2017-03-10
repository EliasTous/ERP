using AionHR.Model.MasterModule;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
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
    public partial class EmployeeRoot : System.Web.UI.Page
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

                //Building the tree
                _systemService.SessionHelper.Set("ActiveModule", "-1");
                BuildTree(1);
                commonTree.Title = Resources.Common.EmployeeFiles;

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
            nodes = TreeBuilder.Instance.BuildEmployeeDetailsTree(commonTree.Root);
            return nodes.ToJson();
            switch (id)
            {
                case 1:
                    nodes = TreeBuilder.Instance.BuildEmployeeFilesTree(commonTree.Root);
                    tabHome.Loader.Url = "Employees.aspx";
                    tabHome.Loader.LoadContent();
                    return nodes.ToJson();

                case 3:
                    nodes = TreeBuilder.Instance.BuildCompanyStructureTree(commonTree.Root);
                    tabHome.Loader.Url = "Departments.aspx";
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
    }
}