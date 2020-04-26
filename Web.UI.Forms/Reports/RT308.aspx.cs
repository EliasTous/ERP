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
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Web.UI.Forms.Utilities;
using Model.Company.News;
using Services.Messaging;
using Model.Company.Structure;
using Model.System;
using Model.Attendance;
using Services.Messaging.Reports;
using System.Threading;
using Reports;
using Model.Reports;
using Model.Employees.Profile;
using Services.Messaging.TimeAttendance;
using Model.TimeAttendance;
using Web.UI.Forms.ConstClasses;
using Reports.ShiftLogs;
using Reports.PunchLog;

namespace Web.UI.Forms.Reports
{
    public partial class RT308 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();


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
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(Model.Reports.RT309), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }

                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                    CurrentReportId.Text = Request.QueryString["id"];
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


        [DirectMethod]
        public void SetLabels(string labels)
        {
            this.labels.Text = labels;
        }

        [DirectMethod]
        public void SetVals(string labels)
        {
            this.vals.Text = labels;
        }

        [DirectMethod]
        public void SetTexts(string labels)
        {
            this.texts.Text = labels;
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





        private void FillRT308Report(bool isInitial = false, bool throwException = true)
        {


            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;


            ListResponse<Model.Reports.RT308> resp = _reportsService.ChildGetAll<Model.Reports.RT308>(req);
                if (!resp.Success)
                    Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
                int counter = 1;
            int maxPunchCount = 0;
            List<Model.Reports.RT308> newPunchLogsList = new List<Model.Reports.RT308>();
                Model.Reports.RT308 record = new Model.Reports.RT308();
                DateTime parsed = DateTime.Now;

            string getLan = _systemService.SessionHelper.getLangauge();

            foreach (var e in resp.Items.GroupBy(x => x.employeeName))
                {


                    e.ToList().ForEach(y =>
                    {
                        counter = 1;
                        if (DateTime.TryParseExact(y.dayId, "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.AdjustToUniversal, out parsed))
                        {

                            //y.dayIdDateTime = parsed;
                            //record = new Model.Reports.RT309();
                            //record.employeeName = y.employeeName;
                            //record.dayIdDateTime = y.dayIdDateTime;
                            //record.shiftLog = y.shiftLog;

                            //record.shiftId = String.Format("{0} {1}", "Shift", counter);
                            //counter++;
                            //newShiftLogsList.Add(record);
                            for (int i = 0; i < y.punchLog.Count() ; i++)
                            {

                                y.dayIdDateTime = parsed;
                                record = new Model.Reports.RT308();
                                record.employeeName = y.employeeName;
                                record.dayIdDateTime = y.dayIdDateTime;
                                record.punchString = y.punchLog[i];
                                //if (_systemService.SessionHelper.CheckIfArabicSession())
                                if (getLan == "ar")
                                    record.punchId = String.Format("{0} {1}", "البصمة ", (i + 1));
                                else if (getLan == "fr")
                                    record.punchId = String.Format("{0} {1}", "Poinçon ", (i + 1));
                                else
                                    record.punchId = String.Format("{0} {1}", "Punch ", (i + 1));
                                if (maxPunchCount < counter)
                                    maxPunchCount = counter;
                                counter++;
                                newPunchLogsList.Add(record);
                            }

                        }




                    });

                }
            Dictionary<string, string> parameters = Web.UI.Forms.Common.FetchReportParameters(texts.Text);
            PunchLogReport h = new PunchLogReport(newPunchLogsList, /*_systemService.SessionHelper.CheckIfArabicSession()*/ getLan, _systemService.SessionHelper.GetDateformat(), parameters,maxPunchCount, "RT308");
            h.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
                h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;


                //string from = DateTime.ParseExact(req.Parameters["_fromDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
                //string to = DateTime.ParseExact(req.Parameters["_toDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
                h.Parameters["User"].Value = string.IsNullOrEmpty(_systemService.SessionHelper.GetCurrentUser()) ? " " : _systemService.SessionHelper.GetCurrentUser();
            //h.Parameters["Fitlers"].Value = texts.Text;







            h.CreateDocument();


                ASPxWebDocumentViewer1.OpenReport(h);


            
         

        }
        private void FillRT311Report(bool isInitial = false, bool throwException = true)
        {


            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;


            ListResponse<Model.Reports.RT311> resp = _reportsService.ChildGetAll<Model.Reports.RT311>(req);
            if (!resp.Success)
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
            int counter = 1;
            int maxPunchCount = 0;
            List<Model.Reports.RT308> newPunchLogsList = new List<Model.Reports.RT308>();
            Model.Reports.RT308 record = new Model.Reports.RT311();
            DateTime parsed = DateTime.Now;

            string getLan = _systemService.SessionHelper.getLangauge();

            foreach (var e in resp.Items.GroupBy(x => x.employeeName))
            {


                e.ToList().ForEach(y =>
                {
                    counter = 1;
                    if (DateTime.TryParseExact(y.dayId, "yyyyMMdd", new CultureInfo("fr"), DateTimeStyles.AdjustToUniversal, out parsed))
                    {

                        //y.dayIdDateTime = parsed;
                        //record = new Model.Reports.RT309();
                        //record.employeeName = y.employeeName;
                        //record.dayIdDateTime = y.dayIdDateTime;
                        //record.shiftLog = y.shiftLog;

                        //record.shiftId = String.Format("{0} {1}", "Shift", counter);
                        //counter++;
                        //newShiftLogsList.Add(record);
                        for (int i = 0; i < y.punchLog.Count(); i++)
                        {
                            y.dayIdDateTime = parsed;                            
                            record = new Model.Reports.RT311();
                            record.employeeName = y.employeeName;
                            record.dayIdDateTime = y.dayIdDateTime;
                            record.punchString = y.punchLog[i];
                            //if (_systemService.SessionHelper.CheckIfArabicSession())
                            if (getLan == "ar")
                                record.punchId = String.Format("{0} {1}", "البصمة ", (i + 1));
                            else if (getLan == "fr")
                                record.punchId = String.Format("{0} {1}", "Poinçon ", (i + 1));
                            else
                                record.punchId = String.Format("{0} {1}", "Punch ", (i + 1));
                            if (maxPunchCount < counter)
                                maxPunchCount = counter;
                            counter++;
                            newPunchLogsList.Add(record);
                        }

                    }




                });

            }
            Dictionary<string, string> parameters = Web.UI.Forms.Common.FetchReportParameters(texts.Text);
            PunchLogReport h = new PunchLogReport(newPunchLogsList, /*_systemService.SessionHelper.CheckIfArabicSession()*/ getLan, _systemService.SessionHelper.GetDateformat(), parameters, maxPunchCount,"RT311");
            h.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;


            //string from = DateTime.ParseExact(req.Parameters["_fromDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            //string to = DateTime.ParseExact(req.Parameters["_toDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            h.Parameters["User"].Value = string.IsNullOrEmpty(_systemService.SessionHelper.GetCurrentUser()) ? " " : _systemService.SessionHelper.GetCurrentUser();
            //h.Parameters["Fitlers"].Value = texts.Text;







            h.CreateDocument();


            ASPxWebDocumentViewer1.OpenReport(h);





        }



        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] parameters = e.Parameter.Split('|');
            int pageIndex = Convert.ToInt32(parameters[0]);

            if (pageIndex == 1)
            {
                if (CurrentReportId.Text == "308")
                    FillRT308Report();
                else
                    FillRT311Report();


            }

        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {



        }

        protected void ASPxCallbackPanel1_Load(object sender, EventArgs e)
        {
            ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            //FillReport(true);
        }

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }


    }
}