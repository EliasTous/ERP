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
using Reports;
using AionHR.Model.Reports;
using AionHR.Model.Employees.Profile;

using Reports.DetailedAttendanceCross;
using AionHR.Model.TimeAttendance;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT303 : System.Web.UI.Page
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

        public string timeformat(int time)
        {
            string hr, min;
            min = Convert.ToString(time % 60);
            hr = Convert.ToString(time / 60);
            if (hr.Length == 1) hr = "0" + hr;
            if (min.Length == 1) min = "0" + min;
            return hr + ":" + min;
        }

        private void FillReport(bool isInitial = false, bool throwException = true)
        {

            string rep_params = vals.Text;
            TimeAttendanceViewListRequest req = new TimeAttendanceViewListRequest();
            req.paramString = rep_params;
            req.StartAt = "0";
          
            req.Size = "1000";
            req.sortBy = "dayId";



            //ListResponse<AttendanceDay> resp = _timeAttendanceService.ChildGetAll<AttendanceDay>(req);
            ListResponse<AionHR.Model.Reports.RT303> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT303>(req);
            //if (!resp.Success)
            //{


            //        throw new Exception(resp.Error + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId + "</br>");

            //}
            if (!resp.Success)
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            resp.Items.ForEach(x =>
            {


               
                    x.dayId = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat());
              


            }
            );

            double OTL = 0;
            double OTO = 0;
            string getLan = _systemService.SessionHelper.getLangauge();
            Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);
            DetailedAttendance h = new DetailedAttendance(parameters, getLan);
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            
            List<AionHR.Model.Reports.RT303> data = new List<AionHR.Model.Reports.RT303>();
            if (resp.Items.Count != 0)
            {
                

                Model.Reports.RT303 Item;
                resp.Items.ForEach(x =>
                {                    
                    Item = new Model.Reports.RT303();
                    Item.dayId = x.dayId;
                    Item.employeeName = x.employeeName;
                    Item.branchName = x.branchName;
                    Item.effectiveTime = x.effectiveTime;
                    if (x.firstPunch == null)
                    { Item.firstPunch = "00:00"; }
                    else
                    { Item.firstPunch = x.firstPunch; }

                    if (x.lastPunch == null)
                    { Item.lastPunch = "00:00"; }
                    else
                    { Item.lastPunch = x.lastPunch; }

                    Item.dayStatus = x.dayStatus;
                    Item.totEffectiveTime = Convert.ToDouble(x.effectiveTime);//calculateTimeInMinutes(x.effectiveTime);


                    if (x.LATE_CHECKIN == 0)
                    { Item.lateCheckin = "00:00"; }
                    else
                    { Item.lateCheckin = time(Convert.ToInt32(x.LATE_CHECKIN), false); }
                    //Item.lateCheckin = x.variationsList.Where(y => y.timeCode == 31).Count() != 0 ? time(x.variationsList.Where(y => y.timeCode == 31).First().duration, false) : "00:00";
                    //Item.duringShiftLeave = x.variationsList.Where(y => y.timeCode == 32).Count() != 0 ? time(x.variationsList.Where(y => y.timeCode == 32).First().duration, false) : "";
                    double dsl = 0;
                    double totalLatness = 0;
                    double totalOverTime = 0;
                    double totEarlyCI = 0;
                    double totlateCI = 0;
                    double totEarlyLe = 0;
                    double missedShift = 0;
                    
                    foreach (DetailedAttendanceVariation obj in x.variationsList)
                    {
                        if (obj.timeCode == 32)
                            dsl += obj.duration;
                        if (obj.timeCode == 31 || obj.timeCode == 33)
                            totalLatness += obj.duration;
                        if (obj.timeCode == 51 || obj.timeCode == 52)
                            totalOverTime += obj.duration;
                        if (obj.timeCode == 51)
                            totEarlyCI += obj.duration;
                        if (obj.timeCode == 31)
                            totlateCI += obj.duration;
                        if (obj.timeCode == 33)
                            totEarlyLe += obj.duration;
                        if (obj.timeCode == 21)
                            missedShift += obj.duration;
                    }

                    Item.duringShiftLeave = x.DURING_SHIFT_LEAVE.ToString();//dsl.ToString();
                    Item.totTotalLateness = x.NET_LATENESS;//totalLatness;
                    Item.totTotalOvertime = x.NET_OVERTIME;// totalOverTime;
                    Item.lineTotalOvertime = x.OVERTIME;//totalOverTime;
                    Item.missedShift = x.SHIFT_LEAVE_WITHOUT_EXCUSE;//missedShift;

                    Item.lineDuringShiftLeave = x.DURING_SHIFT_LEAVE;
                    Item.lineEarlyCheckIn = x.EARLY_CHECKIN;//totEarlyCI;
                    Item.lineLateCheckIn = x.LATE_CHECKIN;//totlateCI;
                    Item.lineEarlyLeave = x.EARLY_LEAVE;

                    Item.totTotalLateness = x.NET_LATENESS;
                    Item.bTotTotalLateness = x.NET_LATENESS.ToString();//totalLatness.ToString();
                    Item.bTotTotalOvertime = x.NET_OVERTIME.ToString();//totalOverTime.ToString();
                    Item.strMissedShift = timeformat(Convert.ToInt32(x.SHIFT_LEAVE_WITHOUT_EXCUSE.ToString()));//missedShift//totalLatness.ToString();
                    Item.totalLateness = timeformat(Convert.ToInt32(x.NET_LATENESS));//timeformat(Convert.ToInt32(totalLatness + dsl+missedShift));//totalLatness.ToString();
                    Item.totalOvertime = timeformat(Convert.ToInt32(x.NET_OVERTIME));//timeformat(Convert.ToInt32(totalOverTime));//totalOverTime.ToString();
                    //Item.earlyLeave = x.variationsList.Where(y => y.timeCode == 33).Count() != 0 ? time(x.variationsList.Where(y => y.timeCode == 33).First().duration, false) : "00:00";
                    //Item.earlyCheckin = x.variationsList.Where(y => y.timeCode == 51).Count() != 0 ? time(x.variationsList.Where(y => y.timeCode == 51).First().duration, false) : "00:00";

                    if (x.EARLY_LEAVE == 0)
                    { Item.earlyLeave = "00:00"; }
                    else
                    { Item.earlyLeave = time(Convert.ToInt32(x.EARLY_LEAVE), false); }

                    if (x.EARLY_CHECKIN == 0)
                    { Item.earlyCheckin = "00:00"; }
                    else
                    { Item.earlyCheckin = time(Convert.ToInt32(x.EARLY_CHECKIN), false); }

                    if (x.OVERTIME == 0)
                    { Item.overtime = "00:00"; }
                    else
                    { Item.overtime = time(Convert.ToInt32(x.OVERTIME), false); }

                    //Item.overtime = time(Convert.ToInt32(x.netOvertime),false);//x.variationsList.Where(y => y.timeCode == 52).Count() != 0 ? time(x.variationsList.Where(y => y.timeCode == 52).First().duration, false) : "";

                    
                    data.Add(Item);

                });
                
                

                h.DataSource = data;              

                
            }
            else

            h.DataSource = resp.Items;

            string user = _systemService.SessionHelper.GetCurrentUser();

        
            h.Parameters["User"].Value = user;       


           


            h.CreateDocument();
            ASPxWebDocumentViewer1.OpenReport(h);
            ASPxWebDocumentViewer1.DataBind();

        }
        public double calculateTimeInMinutes(string currentTime)
        {
            double hours = Convert.ToInt32(currentTime.Substring(0, 2));
            double HoursInminutes = ((hours % 60)) * 60;
            string minutesInString = currentTime.Split(':')[1];
            double minutes = Convert.ToInt32(minutesInString);
            double totalMinutes = HoursInminutes + minutes;
            return totalMinutes;      
        }


        public string time(int _minutes, bool _signed)
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