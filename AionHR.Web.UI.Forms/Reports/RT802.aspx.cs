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
using System.Threading;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT802 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
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

            ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;

            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();

                try
                {


                    dateRange1.DefaultStartDate = DateTime.Today;
                    format.Text = _systemService.SessionHelper.GetDateformat();
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;

                    FillReport(false);
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
                UICulture = "ar-AE";
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-AE");
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
            req.StartAt = "1";
            req.SortBy = "eventDt";


            req.Add(dateRange1.GetRange());
            req.Add(userCombo1.GetUser());
            req.Add(transactionCombo1.GetTransactionType());
            req.Add(moduleCombo1.GetModule());

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
            ListRequest req = new ListRequest();

            req.Filter = query;

            ListResponse<UserInfo> users = _systemService.ChildGetAll<UserInfo>(req);
            return users.Items;
        }

        private void FillReport(bool throwException = true)
        {
            ReportCompositeRequest req = GetRequest();
            ListResponse<AionHR.Model.Reports.RT802> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT802>(req);
            if (!resp.Success)
            {
                if (throwException)
                    throw new Exception(resp.Summary);
                else
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                    return;
                }
            }
            resp.Items.ForEach(x => { x.TypeString = GetGlobalResourceObject("Common", "TrType" + x.type.ToString()).ToString(); x.ClassIdString = GetGlobalResourceObject("Classes", "Class" + x.classId.ToString()).ToString(); x.DateString = x.eventDt.ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en")); });
            AuditTrail h = new AuditTrail();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            h.Parameters["username"].Value = _systemService.SessionHelper.Get("CurrentUserName");
            h.Parameters["From"].Value = DateTime.Parse(req.Parameters["_fromDate"]).ToString(_systemService.SessionHelper.GetDateformat());
            h.Parameters["To"].Value = DateTime.Parse(req.Parameters["_toDate"]).ToString(_systemService.SessionHelper.GetDateformat());
            if (resp.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(req.Parameters["_trxType"]))
                    h.Parameters["TrType"].Value = GetGlobalResourceObject("Common", "TrType" + req.Parameters["_trxType"]);
                else
                    h.Parameters["TrType"].Value = GetGlobalResourceObject("Common", "All");

                if (!string.IsNullOrEmpty(req.Parameters["_moduleId"]))
                    h.Parameters["Module"].Value = GetGlobalResourceObject("Common", "Mod" + req.Parameters["_moduleId"]);
                else
                    h.Parameters["Module"].Value = GetGlobalResourceObject("Common", "All");

                if (resp.Items.Count > 0 && req.Parameters["_userId"] != "0")
                    h.Parameters["User"].Value = resp.Items[0].userName;
                else
                    h.Parameters["User"].Value = GetGlobalResourceObject("Common", "All");

            }
            h.DataSource = resp.Items;


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
    }
}