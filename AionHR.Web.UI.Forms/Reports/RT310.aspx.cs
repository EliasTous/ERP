using System;
using System.Collections.Generic;
using Ext.Net;
using AionHR.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using AionHR.Web.UI.Forms.Utilities;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using System.Threading;
using AionHR.Model.Employees.Profile;
using Reports.AttendanceSchedule;
using System.Globalization;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT310 : System.Web.UI.Page
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
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT310), null, null, null, null);
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

        [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }





        private void FillReport(bool isInitial = false, bool throwException = true)
        {

            try
            {

                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse<AionHR.Model.Reports.RT310> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT310>(req);
                if (!resp.Success)
                {
                    Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
                }
                for (int i = resp.Items.Count - 1; i >= 0; i--)
                {
                    DateTime parsed = DateTime.Now;
                    if (DateTime.TryParseExact(resp.Items[i].dayId, "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.AdjustToUniversal, out parsed))
                    {
                        resp.Items[i].dayIdDateTime = parsed;
                        //x.dayIdString = parsed.ToString(_systemService.SessionHelper.GetDateformat());
                        // Use reformatted
                    }
                    else

                        resp.Items.RemoveAt(i);
                }
                Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);
                AttendanceScheduleReport h = new AttendanceScheduleReport(resp.Items, _systemService.SessionHelper.CheckIfArabicSession(),_systemService.SessionHelper.GetDateformat(), parameters);
                h.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
                h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;


                string from = DateTime.ParseExact(req.Parameters["_fromDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
                string to = DateTime.ParseExact(req.Parameters["_toDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
                h.Parameters["User"].Value = string.IsNullOrEmpty( _systemService.SessionHelper.GetCurrentUser())?" ": _systemService.SessionHelper.GetCurrentUser();

                h.Parameters["Fitlers"].Value = texts.Text;

                h.CreateDocument();


                ASPxWebDocumentViewer1.OpenReport(h);

            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

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
            ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
           
            //FillReport(true);
        }



    }
}