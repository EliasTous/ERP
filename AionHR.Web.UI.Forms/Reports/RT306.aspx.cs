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
using AionHR.Services.Messaging.TimeAttendance;
using AionHR.Model.TimeAttendance;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT306 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
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


        //private ReportCompositeRequest GetRequest()
        //{
        //    ReportCompositeRequest req = new ReportCompositeRequest();

        //    req.Size = "1000";
        //    req.StartAt = "1";


        //    req.Add(dateRange1.GetRange());
        //    req.Add(employeeCombo1.GetEmployee());
        //    req.Add(jobInfo1.GetJobInfo());
        //    req.Add(getFillter());


        //    return req;
        //}

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

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }

        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }
        //private void FillReport(bool isInitial = false, bool throwException = true)
        //{

        //    ReportCompositeRequest req = GetRequest();

        //    ListResponse<AionHR.Model.Reports.RT306> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT306>(req);
        //    if (!resp.Success)
        //    {
        //        if (throwException)
        //            throw new Exception(resp.Summary);
        //        else
        //        {
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
        //            return;
        //        }
        //    }

        //    resp.Items.ForEach(x =>
        //    {
        //        DateTime date = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en"));
        //        x.dateString = date.ToString(_systemService.SessionHelper.GetDateformat());
        //        x.dowString = GetGlobalResourceObject("Common", date.DayOfWeek.ToString() + "Text").ToString();
        //        x.specialTasks = x.jobTasks = "00:00";
        //        x.specialTasks = x.unpaidLeaves;
        //        x.jobTasks = x.paidLeaves;
        //        x.dayStatusString = GetLocalResourceObject("status" + x.dayStatus.ToString()).ToString();
        //        //if (x.workingHours != "00:00")
        //        //{

        //        //    x.unpaidLeaves = "00:00";
        //        //    x.jobTasks = x.paidLeaves;
        //        //    x.paidLeaves = "00:00";
        //        //    x.specialTasks = x.unpaidLeaves;

        //        //}
        //        //else





        //    }
        //    );


        //    DayStatus h = new DayStatus();
        //    h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
        //    h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
        //    h.DataSource = resp.Items;

        //    string from = DateTime.ParseExact(req.Parameters["_fromDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
        //    string to = DateTime.ParseExact(req.Parameters["_toDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
        //    string user = _systemService.SessionHelper.GetCurrentUser();

        //    h.Parameters["FromParameter"].Value = from;
        //    h.Parameters["ToParameter"].Value = to;
        //    h.Parameters["UserParameter"].Value = user;
        //    if (req.Parameters["_dayStatus"] != "0")
        //        h.Parameters["dayStatusParameter"].Value = dayStatus.SelectedItem.Text;
        //    else
        //        h.Parameters["dayStatusParameter"].Value = GetGlobalResourceObject("Common", "All");
        //    if (req.Parameters["_punchStatus"] != "0")
        //        h.Parameters["punchStatus"].Value = punchStatus.SelectedItem.Text;
        //    else
        //        h.Parameters["punchStatus"].Value = GetGlobalResourceObject("Common", "All");
        //    if (req.Parameters["_departmentId"] != "0")
        //        h.Parameters["DepartmentName"].Value = jobInfo1.GetDepartment();
        //    else
        //        h.Parameters["DepartmentName"].Value = GetGlobalResourceObject("Common", "All");




        //    //ListRequest def = new ListRequest();
        //    //int lateness = 0;
        //    //ListResponse<KeyValuePair<string, string>> items = _systemService.ChildGetAll<KeyValuePair<string, string>>(def);
        //    //try
        //    //{
        //    //    lateness = Convert.ToInt32(items.Items.Where(s => s.Key == "allowedLateness").First().Value);
        //    //}
        //    //catch
        //    //{

        //    //}
        //    //h.Parameters["AllowedLatenessParameter"].Value = lateness;

        //    h.PrintingSystem.Document.ScaleFactor = 4;
        //    h.CreateDocument();


        //    ASPxWebDocumentViewer1.OpenReport(h);

        //}
        private void FillReport(bool isInitial = false, bool throwException = true)
        {

            try
            {
                DashboardTimeListRequest r = new DashboardTimeListRequest();
               
               
                r.fromDayId=date2.GetRange().DateFrom.ToString("yyyyMMdd");
                r.toDayId= date2.GetRange().DateTo.ToString("yyyyMMdd");
                r.employeeId = employeeCombo1.GetEmployee().employeeId;

                r.timeCode = timeVariationType.GetTimeCode(); 
                if (string.IsNullOrEmpty(approverId.Value.ToString()))
                    r.approverId = 0;
                else
                    r.approverId = Convert.ToInt32(approverId.Value.ToString());
                r.shiftId = "0";
                r.Parameters.Add("_sortBy", "dayId");
                r.apStatus = string.IsNullOrEmpty(apStatus.Value.ToString()) ? "0" : apStatus.Value.ToString();
                ListResponse <Time> resp = _timeAttendanceService.ChildGetAll<Time>(r);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                    return;
                }
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                resp.Items.ForEach(
                     x =>
                     {
                         if (!string.IsNullOrEmpty(x.clockDuration))
                         x.clockDuration = time(Convert.ToInt32( x.clockDuration), true);
                         if (!string.IsNullOrEmpty(x.duration))
                             x.duration = time(Convert.ToInt32(x.duration), true);
                         if (!string.IsNullOrEmpty(x.timeCode))
                             x.timeCodeString = FillTimeCode(Convert.ToInt32(x.timeCode));
                         x.statusString = FillApprovalStatus(x.status);
                         if (!string.IsNullOrEmpty(x.damageLevel))
                             x.damageLevel = FillDamageLevelString(Convert.ToInt16(x.damageLevel));
                         if (rtl)
                             x.dayId = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("ar-AE"));
                         else
                             x.dayId = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("en-US"));


                     }
                     );




                TimeApproval h = new TimeApproval();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            h.DataSource = resp.Items;

            //string from = DateTime.ParseExact(req.Parameters["_fromDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            //string to = DateTime.ParseExact(req.Parameters["_toDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            string user = _systemService.SessionHelper.GetCurrentUser();

            //h.Parameters["FromParameter"].Value = from;
            //h.Parameters["ToParameter"].Value = to;
            h.Parameters["UserParameter"].Value = user;
            //if (req.Parameters["_dayStatus"] != "0")
            //    h.Parameters["dayStatusParameter"].Value = dayStatus.SelectedItem.Text;
            //else
            //    h.Parameters["dayStatusParameter"].Value = GetGlobalResourceObject("Common", "All");
            //if (req.Parameters["_punchStatus"] != "0")
            //    h.Parameters["punchStatus"].Value = punchStatus.SelectedItem.Text;
            //else
            //    h.Parameters["punchStatus"].Value = GetGlobalResourceObject("Common", "All");
            //if (req.Parameters["_departmentId"] != "0")
            //    h.Parameters["DepartmentName"].Value = jobInfo1.GetDepartment();
            //else
            //    h.Parameters["DepartmentName"].Value = GetGlobalResourceObject("Common", "All");




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

           
            h.CreateDocument();


            ASPxWebDocumentViewer1.OpenReport(h);

            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
        private string FillDamageLevelString(short? DamageLevel)
        {
            string R;
            switch (DamageLevel)
            {
                case 1:
                    R = GetLocalResourceObject("DamageWITHOUT_DAMAGE").ToString();
                    break;
                case 2:
                    R = GetLocalResourceObject("DamageWITH_DAMAGE").ToString();
                    break;
                default:
                    R = string.Empty;
                    break;

            }
            return R;
        }
      
        private string FillApprovalStatus(short? apStatus)
        {
            string R;
            switch (apStatus)
            {
                case 1:
                    R = GetLocalResourceObject("FieldNew").ToString();
                    break;
                case 2:
                    R = GetLocalResourceObject("FieldApproved").ToString();
                    break;
                case -1:
                    R = GetLocalResourceObject("FieldRejected").ToString();
                    break;
                default:
                    R = string.Empty;
                    break;


            }
            return R;
        }
        public static string time(int _minutes, bool _signed)
        {
            if (_minutes == 0)
                return "00:00";

            bool isNegative = _minutes < 0 ? true : false;

            _minutes = Math.Abs(_minutes);

            string hours = (_minutes / 60).ToString(), minutes = (_minutes % 60).ToString(), formattedTime;

            if (hours.Length == 1)
                hours = "0" + hours;

            if (minutes.Length == 1)
                minutes = "0" + minutes;

            formattedTime = hours + ':' + minutes;

            if (isNegative && _signed)
                formattedTime = "-" + formattedTime;

            return formattedTime;
        }

        private string FillTimeCode(int timeCode)
        {
            string R = "";


            // Retrieve the value of the string resource named "welcome".
            // The resource manager will retrieve the value of the  
            // localized resource using the caller's current culture setting.


            try
            {

                switch (timeCode)
                {
                    case ConstTimeVariationType.UNPAID_LEAVE:
                        R = GetGlobalResourceObject("Common", "UnpaidLeaves").ToString();
                        break;
                    case ConstTimeVariationType.PAID_LEAVE:
                        R = GetGlobalResourceObject("Common", "PaidLeaves").ToString();
                        break;
                    case ConstTimeVariationType.DAY_LEAVE_WITHOUT_EXCUSE:
                        R = GetGlobalResourceObject("Common", "DAY_LEAVE_WITHOUT_EXCUSE").ToString();
                        break;
                    case ConstTimeVariationType.SHIFT_LEAVE_WITHOUT_EXCUSE:
                        R = GetGlobalResourceObject("Common", "SHIFT_LEAVE_WITHOUT_EXCUSE").ToString();
                        break;
                    case ConstTimeVariationType.LATE_CHECKIN:
                        R = GetGlobalResourceObject("Common", "LATE_CHECKIN").ToString();
                        break;
                    case ConstTimeVariationType.DURING_SHIFT_LEAVE:
                        R = GetGlobalResourceObject("Common", "DURING_SHIFT_LEAVE").ToString();
                        break;
                    case ConstTimeVariationType.EARLY_LEAVE:
                        R = GetGlobalResourceObject("Common", "EARLY_LEAVE").ToString();
                        break;
                    case ConstTimeVariationType.MISSED_PUNCH:
                        R = GetGlobalResourceObject("Common", "MISSED_PUNCH").ToString();
                        break;
                    case ConstTimeVariationType.EARLY_CHECKIN:
                        R = GetGlobalResourceObject("Common", "EARLY_CHECKIN").ToString();
                        break;

                    case ConstTimeVariationType.OVERTIME:
                        R = GetGlobalResourceObject("Common", "OVERTIME").ToString();
                        break;

                    case ConstTimeVariationType.COUNT:
                        R = GetGlobalResourceObject("Common", "COUNT").ToString();
                        break;
                    case ConstTimeVariationType.Day_Bonus:
                        R = GetGlobalResourceObject("Common", "Day_Bonus").ToString();
                        break;

                }

                return R;
            }
            catch { return string.Empty; }
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
        //private dayStatusParameterSet getFillter()
        //{
        //    dayStatusParameterSet p = new dayStatusParameterSet();
        //    if (!string.IsNullOrEmpty(dayStatus.Text) && dayStatus.Value.ToString() != "0")
        //    {
        //        p.dayStatus = dayStatus.Value.ToString();
        //    }
        //    else
        //    {
        //        p.dayStatus = "0";

        //    }
        //    if (!string.IsNullOrEmpty(punchStatus.Text) && punchStatus.Value.ToString() != "0")
        //    {
        //        p.punchStatus = punchStatus.Value.ToString();
        //    }
        //    else
        //    {
        //        p.punchStatus = "0";

        //    }
        //    return p;
        //}
        [DirectMethod]
        public object FillApprover(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
        }

       
    }
}