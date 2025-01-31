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
using AionHR.Model.Employees.Profile;


namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT306Old : System.Web.UI.Page
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
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT302), null, null, null, null);
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


        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "0";


            req.Add(dateRange1.GetRange());
            req.Add(employeeCombo1.GetEmployee());
            req.Add(jobInfo1.GetJobInfo());
            req.Add(getFillter());
            

            return req;
        }

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = GetNameFormat();

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }

        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }
        private void FillReport(bool isInitial = false, bool throwException = true)
        {

            ReportCompositeRequest req = GetRequest();
          
           ListResponse<AionHR.Model.Reports.RT306> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT306>(req);
            if (!resp.Success)
            {
                if (throwException)
                    throw new Exception(resp.Error + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId + "</br>");
                else
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(resp);
                    return;
                }
            }

            resp.Items.ForEach(x =>
            {
                DateTime date = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en"));
                x.dateString = date.ToString(_systemService.SessionHelper.GetDateformat());
                x.dowString = GetGlobalResourceObject("Common", date.DayOfWeek.ToString() + "Text").ToString();
                x.specialTasks = x.jobTasks = "00:00";
                x.specialTasks = x.unpaidLeaves;
                x.jobTasks = x.paidLeaves;
                x.dayStatusString = GetLocalResourceObject("status" + x.dayStatus.ToString()).ToString();
                //if (x.workingHours != "00:00")
                //{

                //    x.unpaidLeaves = "00:00";
                //    x.jobTasks = x.paidLeaves;
                //    x.paidLeaves = "00:00";
                //    x.specialTasks = x.unpaidLeaves;

                //}
                //else





            }
            );


            DayStatus h = new DayStatus();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            h.DataSource = resp.Items;

            string from = DateTime.ParseExact(req.Parameters["_fromDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            string to = DateTime.ParseExact(req.Parameters["_toDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            string user = _systemService.SessionHelper.GetCurrentUser();

            h.Parameters["FromParameter"].Value = from;
            h.Parameters["ToParameter"].Value = to;
            h.Parameters["UserParameter"].Value = user;
            if (req.Parameters["_dayStatus"] != "0")
                h.Parameters["dayStatusParameter"].Value = dayStatus.SelectedItem.Text;
            else
                h.Parameters["dayStatusParameter"].Value = GetGlobalResourceObject("Common", "All");
            if (req.Parameters["_punchStatus"] != "0")
                h.Parameters["punchStatus"].Value = punchStatus.SelectedItem.Text;
            else
                h.Parameters["punchStatus"].Value = GetGlobalResourceObject("Common", "All");
            if (req.Parameters["_departmentId"] != "0")
                h.Parameters["DepartmentName"].Value = jobInfo1.GetDepartment();
            else
                h.Parameters["DepartmentName"].Value = GetGlobalResourceObject("Common", "All");




            //ListRequest def = new ListRequest();
            //int lateness = 0;
            //ListResponse<KeyValuePair<string, string>> items = _systemService.ChildGetAll<KeyValuePair<string, string>>(def);
            //try
            //{
            //    lateness = Convert.ToInt32(items.Items.Where(s => s.Key == "allowedLateness").First().Value);
            //}
            //catch
            //{

            //}
            //h.Parameters["AllowedLatenessParameter"].Value = lateness;

            h.PrintingSystem.Document.ScaleFactor = 4;
            h.CreateDocument();


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
        private dayStatusParameterSet getFillter()
        {
            dayStatusParameterSet p = new dayStatusParameterSet();
            if (!string.IsNullOrEmpty(dayStatus.Text) && dayStatus.Value.ToString() != "0")
            {
                p.dayStatus =dayStatus.Value.ToString();
            }
            else
            {
                p.dayStatus = "0";

            }
            if (!string.IsNullOrEmpty(punchStatus.Text) && punchStatus.Value.ToString() != "0")
            {
                p.punchStatus = punchStatus.Value.ToString();
            }
            else
            {
                p.punchStatus = "0";

            }
            return p;
        }
    }
}