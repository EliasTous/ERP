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

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT105 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
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
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT105), null, null, null, null);
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
            req.StartAt = "0";
            req.SortBy = "firstName";


            req.Add(jobInfo1.GetJobInfo());//jobInfo1 is a user control that displays combos for employee job information
            req.Add(activeStatus1.GetActiveStatus());

            //req.Add();
            return req;
        }
        protected void firstStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            ReportCompositeRequest req = GetRequest();
            ListResponse<AionHR.Model.Reports.RT202> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT202>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
                return;
            }



            //firstStore.DataSource = resp.Items;
            //firstStore.DataBind();

        }

        [DirectMethod]
        public void FillReport(bool throwException =true)
        {
            ReportCompositeRequest req = GetRequest();
            ListResponse<AionHR.Model.Reports.RT105> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT105>(req);
            //if (resp == null || string.IsNullOrEmpty(resp.Error))
            //{
            //    throw new Exception(GetGlobalResourceObject("Errors", "Error_1").ToString());
            //}
            //if (!resp.Success)
            //{
            //    throw new Exception(resp.Error + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId + "</br>");
            //}
            if (!resp.Success)
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
            resp.Items.ForEach(x => x.DateString = x.date.ToString(_systemService.SessionHelper.GetDateformat()));
            JobHistory h = new JobHistory();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            if (resp.Items.Count > 0)
            {
                if (req.Parameters["_departmentId"] != "0")
                    h.Parameters["Department"].Value = resp.Items[0].departmentName;
                else
                    h.Parameters["Department"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_branchId"] != "0")
                    h.Parameters["Branch"].Value = resp.Items[0].branchName;
                else
                    h.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_positionId"] != "0")
                    h.Parameters["Position"].Value = resp.Items[0].positionName;
                else
                    h.Parameters["Position"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_divisionId"] != "0")
                    h.Parameters["Division"].Value = resp.Items[0].divisionName;
                else
                    h.Parameters["Division"].Value = GetGlobalResourceObject("Common", "All");

            }
            h.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
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