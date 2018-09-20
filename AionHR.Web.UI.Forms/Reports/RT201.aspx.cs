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
using Reports;
using System.Threading;
using AionHR.Model.Employees.Profile;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT201 : System.Web.UI.Page
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


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();

                try
                {

                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT201), null, null, null, null);
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
            req.StartAt = "1";
            req.SortBy = "firstName";
            req.Add(jobInfo1.GetJobInfo());
            req.Add(activeStatus.GetActiveStatus());
            req.Add(scrFilter.GetSCR());
            return req;
        }
        protected void firstStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            ReportCompositeRequest req = GetRequest();
            ListResponse<AionHR.Model.Reports.RT201> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT201>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+resp.LogId : resp.Summary).Show();
                return;
            }

         
            
            //firstStore.DataSource = resp.Items;
            //firstStore.DataBind();

        }

      
        public void FillReport(bool throwException=true)
        {
            ReportCompositeRequest req = GetRequest();
            ListResponse<AionHR.Model.Reports.RT201> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT201>(req);
            if (!resp.Success)
            {
            
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+resp.LogId : resp.Summary).Show();
                    return;
                
            }
           
            SalaryHistory h = new SalaryHistory();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            resp.Items.ForEach(x => { x.PaymentFrequencyString = x.paymentFrequency.HasValue? GetGlobalResourceObject("Common", ((PaymentFrequency)x.paymentFrequency).ToString()).ToString():""; });
            resp.Items.ForEach(x => { x.SalaryTypeString = x.salaryType.HasValue? GetGlobalResourceObject("Common",((SalaryType)x.salaryType).ToString()).ToString():"";  x.EffectiveDateString = x.effectiveDate.ToString(_systemService.SessionHelper.GetDateformat(),new CultureInfo("en")); });
            h.DataSource = resp.Items;
            string user = _systemService.SessionHelper.GetCurrentUser();
            h.Parameters["User"].Value = user;
            if (resp.Items.Count > 0)
            {
                if (req.Parameters["_departmentId"] != "0")
                    h.Parameters["Department"].Value = jobInfo1.GetDepartment();
                else
                    h.Parameters["Department"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_branchId"] != "0")
                    h.Parameters["Branch"].Value = jobInfo1.GetBranch();
                else
                    h.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_positionId"] != "0")
                    h.Parameters["Position"].Value = jobInfo1.GetPosition();
                else
                    h.Parameters["Position"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_divisionId"] != "0")
                    h.Parameters["Division"].Value = jobInfo1.GetDivision();
                else
                    h.Parameters["Division"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_scrId"] != "0")
                    h.Parameters["ChangeReason"].Value = scrFilter.GetChangeReason();
                else
                    h.Parameters["ChangeReason"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_activeStatus"] != "0")
                    h.Parameters["Status"].Value = activeStatus.GetStatusString();
                else
                    h.Parameters["Status"].Value = GetGlobalResourceObject("Common", "All");

            }

            h.CreateDocument();
            //string format = "Pdf";
            //string fileName = String.Format("Report.{0}", format);
            //MemoryStream ms = new MemoryStream();
            //h.ExportToPdf(ms,new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
            //Response.BinaryWrite(ms.ToArray());
            //Response.End();
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