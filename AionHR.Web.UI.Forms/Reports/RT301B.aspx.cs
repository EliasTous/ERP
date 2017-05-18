﻿using System;
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
using Reports;
using AionHR.Model.Reports;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT301B : System.Web.UI.Page
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
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                    FillReport(false, false);
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


            req.Add(dateRange1.GetRange());
            return req;
        }

        private void FillReport(bool isInitial = false , bool throwException=true)
        {

            ReportCompositeRequest req = GetRequest();

            ListResponse<AionHR.Model.Reports.RT301> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT301>(req);
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
            
            
            List<AionHR.Model.Reports.DailyAttendance> atts = new List<AionHR.Model.Reports.DailyAttendance>();
            resp.Items.ForEach(x => atts.Add(new AionHR.Model.Reports.DailyAttendance() {
                                        name = x.name.fullName, branchName=x.branchName, departmentName = x.departmentName,
                                        Date = DateTime.ParseExact(x.dayId,"yyyyMMdd",new CultureInfo("en")),
                                        lateness = x.OL_A_SIGN[0]=='-'?(new TimeSpan(-Convert.ToInt32(x.OL_A.Substring(0, 2)),- Convert.ToInt32(x.OL_A.Substring(3, 2)),0)):(new TimeSpan(Convert.ToInt32(x.OL_A.Substring(0, 2)), Convert.ToInt32(x.OL_A.Substring(3, 2)), 0)),
                                        early = x.OL_D_SIGN[0] == '-' ? (new TimeSpan(-Convert.ToInt32(x.OL_D.Substring(0, 2)), -Convert.ToInt32(x.OL_D.Substring(3, 2)), 0)) : (new TimeSpan(Convert.ToInt32(x.OL_D.Substring(0, 2)), Convert.ToInt32(x.OL_D.Substring(3, 2)), 0)),
                workingHours = new TimeSpan(Convert.ToInt32(x.workingTime.Substring(0,2)), Convert.ToInt32(x.workingTime.Substring(3, 2)),0)
                                    }));
            atts.ForEach(x => { x.DOW = GetGlobalResourceObject("Common", x.Date.DayOfWeek.ToString() + "Text").ToString(); x.DateString = x.Date.ToString(_systemService.SessionHelper.GetDateformat()); });
            
            
            DailyAttendance h = new DailyAttendance();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            h.DataSource = atts;


            h.Parameters["From"].Value = DateTime.Parse(req.Parameters["_fromDate"]).ToString(_systemService.SessionHelper.GetDateformat());
            h.Parameters["To"].Value = DateTime.Parse(req.Parameters["_toDate"]).ToString(_systemService.SessionHelper.GetDateformat());
            h.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();

            h.CreateDocument();


            ASPxWebDocumentViewer1.DataBind();
            ASPxWebDocumentViewer1.OpenReport(h);
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
        protected void Unnamed_Click(object sender, EventArgs e)
        {



        }

        protected void ASPxCallbackPanel1_Load(object sender, EventArgs e)
        {
            //ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            //FillReport(true);
        }
    }
}