using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using Ext.Net;
using Newtonsoft.Json;
using AionHR.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using AionHR.Web.UI.Forms.Utilities;
using AionHR.Model.Company.News;
using AionHR.Services.Messaging;
using AionHR.Model.Company.Structure;
using AionHR.Model.System;
using AionHR.Model.Attendance;
using AionHR.Services.Messaging.Reports;
using DevExpress.XtraReports.Web;
using DevExpress.XtraPrinting.Localization;
using Reports;
using System.Threading;
using AionHR.Services.Messaging.System;
using AionHR.Model.Access_Control;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT804 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
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
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();

                try
                {

                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT803), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }
                 
                    format.Text = _systemService.SessionHelper.GetDateformat();
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                    //FillReport(false);

                }
                catch { }
            }

        }


        private void ActivateFirstFilterSet()
        {



        }


        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }



        private void HideShowButtons()
        {

        }


        /// <summary>
        /// hiding uncessary column in the grid. 
        /// </summary>
        private void HideShowColumns()
        {

        }


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;
                this.rtl.Text = rtl.ToString();
                Culture = "ar";
                UICulture = "ar-SA";
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-SA");
            }
            else
            {
                Culture = "en";
                UICulture = "en-US";
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
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


        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "0";
            req.SortBy = "eventDt";


          
            req.Add(userCombo1.GetUser());

            //req.Add();
            return req;
        }
        [DirectMethod]
        public object FillUsers(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<UserInfo> data = GetUsersFiltered(prms.Query);

            //  return new
            // {
            return data;
        }

        private List<UserInfo> GetUsersFiltered(string query)
        {
            UsersListRequest req = new UsersListRequest();
            req.Size = "100";
            req.StartAt = "0";
            req.Filter = query;
            req.SortBy = "fullName";

            req.DepartmentId = "0";
            req.PositionId = "0";
            req.BranchId = "0";

            ListResponse<UserInfo> users = _systemService.ChildGetAll<UserInfo>(req);
            return users.Items;
        }

        private void FillReport(bool throwException = true)
        {

         
            GroupUsersListRequest GroupUserReq = new GroupUsersListRequest();
            GroupUserReq.Size = "";
            GroupUserReq.StartAt = "";
            GroupUserReq.Filter = "";
            GroupUserReq.GroupId =string.IsNullOrEmpty(sgId.Value.ToString())?"0": sgId.Value.ToString();
            GroupUserReq.UserId = userCombo1.GetUser().UserId.ToString();




            ListResponse<SecurityGroupUser> resp = _accessControlService.ChildGetAll<SecurityGroupUser>(GroupUserReq);

            if (!resp.Success)
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());

            SecurityGroupsReport h = new SecurityGroupsReport();

          //  resp.Items.ForEach(x => x.DateString = x.eventDT.ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"))); SignInTrail h = new SignInTrail();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            h.DataSource = resp.Items;
          //  string from = DateTime.Parse(req.Parameters["_fromDate"]).ToString(_systemService.SessionHelper.GetDateformat());
          //  string to = DateTime.Parse(req.Parameters["_toDate"]).ToString(_systemService.SessionHelper.GetDateformat());
            string user = _systemService.SessionHelper.GetCurrentUser();

           // h.Parameters["From"].Value = from;
          //  h.Parameters["To"].Value = to;
            h.Parameters["User"].Value = user;
            //if (resp.Items.Count > 0)
            //{
            //    //if (req.Parameters["_userId"] != "0")
            //    //    h.Parameters["UserId"].Value = resp.Items[0].userName;
            //    else
            //        h.Parameters["UserId"].Value = GetGlobalResourceObject("Common", "All");
            //}

            h.CreateDocument();
            ASPxWebDocumentViewer1.OpenReport(h);
            ASPxWebDocumentViewer1.DataBind();
        }

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] parameters = e.Parameter.Split('|');
            int pageIndex = Convert.ToInt32(parameters[0]);

            if (pageIndex == 1)
            {
                FillReport();
            }
        }

        protected void ASPxCallbackPanel1_Load(object sender, EventArgs e)
        {
            //ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            //FillReport();
        }

        [DirectMethod]
        public object FillSecurityGroup(string action, Dictionary<string, object> extraParams)
        {
            //GEtting the filter from the page
            string filter = string.Empty;


            ListRequest req = new ListRequest();
            req.Size = "1000";
            req.StartAt = "0";
            req.Filter = "";
            

            ListResponse<SecurityGroup> groups = _accessControlService.ChildGetAll<SecurityGroup>(req);
            if (!groups.Success)
            {
                Common.errorMessage(groups);
                return new List<SecurityGroup>();
            }
            return groups.Items;
        }
    }
}