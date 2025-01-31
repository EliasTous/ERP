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

namespace Web.UI.Forms.Reports
{
    public partial class RT301B : System.Web.UI.Page
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
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(Model.Reports.RT301), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }
                    dateRange1.DefaultStartDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                    //FillReport(false, false);
                    dateRange1.DefaultStartDate = DateTime.Now.AddDays(-DateTime.Now.Day);
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
        //[DirectMethod]
        //public object FillEmployee(string action, Dictionary<string, object> extraParams)
        //{

        //    StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        //    return Common.GetEmployeesFiltered(prms.Query);

        //}

        //private List<Employee> GetEmployeesFiltered(string query)
        //{

        //    EmployeeListRequest req = new EmployeeListRequest();
        //    req.DepartmentId = "0";
        //    req.BranchId = "0";
        //    req.IncludeIsInactive = 2;
        //    req.SortBy = GetNameFormat();

        //    req.StartAt = "0";
        //    req.Size = "20";
        //    req.Filter = query;

        //    ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
        //    return response.Items;
        //}

        //private string GetNameFormat()
        //{
        //    return _systemService.SessionHelper.Get("nameFormat").ToString();
        //}
        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }

        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "0";


            req.Add(dateRange1.GetRange());
            req.Add(employeeCombo1.GetEmployee());
            return req;
        }

        private void FillReport(bool isInitial = false, bool throwException = true)
        {

            ReportCompositeRequest req = GetRequest();

            ListResponse<Model.Reports.RT301> resp = _reportsService.ChildGetAll<Model.Reports.RT301>(req);
            //if (!resp.Success)
            //{
            //    throw new Exception(resp.Error + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId + "</br>");
            //}
            if (!resp.Success)
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());


            List<Model.Reports.DailyAttendance> atts = new List<Model.Reports.DailyAttendance>();
            resp.Items.ForEach(x => atts.Add(new Model.Reports.DailyAttendance()
            {
                name = x.name,
                branchName = x.branchName,
                departmentName = x.departmentName,
                Date = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")),
                lateness = x.OL_A_SIGN[0] == '-' ? (new TimeSpan(Convert.ToInt32(x.OL_A.Substring(1, 2)), -Convert.ToInt32(x.OL_A.Substring(4, 2)), 0)) : (new TimeSpan(Convert.ToInt32(x.OL_A.Substring(0, 2)), Convert.ToInt32(x.OL_A.Substring(3, 2)), 0)),
                early = x.OL_D_SIGN[0] == '-' ? (new TimeSpan(Convert.ToInt32(x.OL_D.Substring(1, 2)), Convert.ToInt32(x.OL_D.Substring(4, 2)), 0)) : (new TimeSpan(Convert.ToInt32(x.OL_D.Substring(0, 2)), Convert.ToInt32(x.OL_D.Substring(3, 2)), 0)),
                workingHours = new TimeSpan(Convert.ToInt32(x.workingTime.Substring(0, 2)), Convert.ToInt32(x.workingTime.Substring(3, 2)), 0),
                workingHoursString = x.workingTime,
                earlyString =  x.OL_D,
                latenessString = x.OL_A
            }));
            atts.ForEach(x => { x.DOW = GetGlobalResourceObject("Common", x.Date.DayOfWeek.ToString() + "Text").ToString(); x.DateString = x.Date.ToString(_systemService.SessionHelper.GetDateformat(), CultureInfo.CurrentUICulture); });


            DailyAttendance h = new DailyAttendance();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            h.DataSource = atts;


            h.Parameters["From"].Value = DateTime.Parse(req.Parameters["_fromDate"]).ToString(_systemService.SessionHelper.GetDateformat());
            h.Parameters["To"].Value = DateTime.Parse(req.Parameters["_toDate"]).ToString(_systemService.SessionHelper.GetDateformat());
            h.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            if (resp.Items.Count > 0)
            {

                if (req.Parameters["_employeeId"] != "0")
                    h.Parameters["Employee"].Value = resp.Items[0].name;
                else
                    h.Parameters["Employee"].Value = GetGlobalResourceObject("Common", "All");
            }
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