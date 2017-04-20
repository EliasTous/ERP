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
using System.Threading;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT102 : System.Web.UI.Page
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



                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
               
                  
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
                //Culture = "ar";
                //UICulture = "ar-AR";
                //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
                //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-AR");
            }
            else
            {
                //Culture = "en";
                //UICulture = "en-EN";
                //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-EN");
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
            req.SortBy = "departmentName";
            
            req.Add(dateRange1.GetRange());
            return req;
        }
       
        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] parameters = e.Parameter.Split('|');
            int pageIndex = Convert.ToInt32(parameters[0]);

            if (pageIndex == 1)
            {

                ReportCompositeRequest req = GetRequest();
                ListResponse<AionHR.Model.Reports.RT102A> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT102A>(req);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                    return;
                }

                Hirings h = new Hirings();
                h.DataSource = resp.Items;
               
               // h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
                //ReportCompositeRequest req2 = GetRequest();
                //ListResponse<AionHR.Model.Reports.RT102B> resp2 = _reportsService.ChildGetAll<AionHR.Model.Reports.RT102B>(req);
                //if (!resp.Success)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                //    return;
                //}
                
                //Reports.Terminations t = new Terminations();
                //t.DataSource = resp2.Items;
                //t.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
                //h.Pages.AddRange(t.Pages);
                ASPxWebDocumentViewer1.DataBind();
                ASPxWebDocumentViewer1.OpenReport(h);
            }

        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
          
            

        }
    }
}