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
using Model.Employees.Leaves;
using Model.Employees.Profile;
using Model.LeaveManagement;
using Services.Messaging.System;
using Model.Company.Cases;
using System.Net;
using Infrastructure.Domain;
using Model.LoadTracking;
using Services.Messaging.LoanManagment;
using Model.Attributes;
using Model.Payroll;
using Reports;
using Services.Messaging.TimeAttendance;
using Model.TimeAttendance;
using Services.Messaging.Reports;
using Model.Dashboard;
using Web.UI.Forms.ConstClasses;

namespace Web.UI.Forms
{
    public partial class MonthlyLatenesses : System.Web.UI.Page
    {

        IPayrollService payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        ISystemService systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();

        protected override void InitializeCulture()
        {


            switch (systemService.SessionHelper.getLangauge())
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



        //[DirectMethod]
        //public object FillPayId(string action, Dictionary<string, object> extraParams)
        //{


        //        PayrollListRequest req = new PayrollListRequest();
        //        req.Year = "0";
        //        req.PeriodType = "5";
        //        req.Status = "0";
        //        req.Size = "30";
        //        req.StartAt = "0";
        //        req.Filter = "";

        //        ListResponse<GenerationHeader> resp = payrollService.ChildGetAll<GenerationHeader>(req);
        //        if (!resp.Success)
        //        {
        //            Common.errorMessage(resp);
        //            return new GenerationHeader();
        //        }

        //        string dateFormat = systemService.SessionHelper.GetDateformat();
        //        if (systemService.SessionHelper.CheckIfArabicSession())
        //            resp.Items.ForEach(x => x.payRefWithDateRange = x.payRef + " ( " + x.startDate.ToString("dd/MM/yyyy", new CultureInfo("ar-AE")) + " - " + x.endDate.ToString("dd/MM/yyyy", new CultureInfo("ar-AE")) + " )");
        //        else
        //            resp.Items.ForEach(x => x.payRefWithDateRange = x.payRef + " ( " + x.startDate.ToString("dd/MM/yyyy", new CultureInfo("en")) + " - " + x.endDate.ToString("dd/MM/yyyy", new CultureInfo("en")) + " )");
        //        //payIdStore.DataSource = resp.Items;
        //        //payIdStore.DataBind();
        //        return resp.Items;
        //    }

        public string timeformat(int time)
        {
            string hr, min;
            min = Convert.ToString(time % 60);
            hr = Convert.ToString(time / 60);
            if (hr.Length == 1) hr = "0" + hr;
            if (min.Length == 1) min = "0" + min;
            return hr + ":" + min;
        }

        private void FillPayId()
        {


            PayrollListRequest req = new PayrollListRequest();
            req.Year = "0";
            req.PeriodType = "5";
            req.Status = "0";
            req.Size = "30";
            req.StartAt = "0";
            req.Filter = "";

            ListResponse<GenerationHeader> resp = payrollService.ChildGetAll<GenerationHeader>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }

            string dateFormat = systemService.SessionHelper.GetDateformat();
            if (systemService.SessionHelper.CheckIfArabicSession())
                resp.Items.ForEach(x => x.payRefWithDateRange = x.payRef + " ( " + x.startDate.ToString("dd/MM/yyyy", new CultureInfo("ar-AE")) + " - " + x.endDate.ToString("dd/MM/yyyy", new CultureInfo("ar-AE")) + " )");
            else
                resp.Items.ForEach(x => x.payRefWithDateRange = x.payRef + " ( " + x.startDate.ToString("dd/MM/yyyy", new CultureInfo("en")) + " - " + x.endDate.ToString("dd/MM/yyyy", new CultureInfo("en")) + " )");



            payIdStore.DataSource = resp.Items;
            payIdStore.DataBind();
            //return resp.Items;


        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
        }


        protected void Preview_Click(object sender, DirectEventArgs e)
        {
            if (payId.Value == null || payId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectPay")).Show();
                return;
            }


            MonthlyLatenessListRequest mlLR = new MonthlyLatenessListRequest();
            mlLR.PayId = payId.SelectedItem.Value;


            ListResponse<MonthlyLateness> response = _timeAttendanceService.ChildGetAll<MonthlyLateness>(mlLR);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingLatenesses")).Show();
                return;
            }

            foreach (var item in response.Items)
            {
                item.strClockDuration = timeformat(item.clockDuration);
                item.strDuration = timeformat(item.duration);
                item.strNetLateness = timeformat(item.netLateness);
            }

            this.Store1.DataSource = response.Items;
            //e.Total = response.Items.Count; 

            this.Store1.DataBind();


        }

        protected void Generate_Click(object sender, DirectEventArgs e)
        {
            if (payId.Value == null || payId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectPay")).Show();
                return;
            }



            RecordRequest r = new RecordRequest();
            r.RecordID = payId.SelectedItem.Value;


            RecordResponse<GenerationHeader> response = payrollService.ChildGetRecord<GenerationHeader>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(response);
                return;
            }

            GenerationHeader gh = new GenerationHeader();
            gh.recordId = response.result.recordId;
            gh.periodId = response.result.periodId;
            gh.payDate = response.result.payDate;
            gh.startDate = response.result.startDate;
            gh.payRef = response.result.payRef;
            gh.endDate = response.result.endDate;
            gh.status = response.result.status;
            gh.payDate = response.result.payDate;
            gh.notes = response.result.notes;
            gh.calendarDays = response.result.calendarDays;
            gh.fiscalYear = response.result.fiscalYear;


            PostRequest<GenerationHeader> request = new PostRequest<GenerationHeader>();
            request.entity = gh;
            PostResponse<GenerationHeader> rrr = _timeAttendanceService.ChildAddOrUpdate<GenerationHeader>(request);
            if (!rrr.Success)//it maybe be another condition
            {
                //Show an error saving...

                Common.errorMessage(rrr);
                return;
            }
            else
            {


                //Add this record to the store 

                //Display successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });



            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                
                FillPayId();

            }

        }

        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }



        private void HideShowButtons()
        {

        }

        private void HideShowColumns()
        {

        }

        private void SetExtLanguage()
        {
            bool rtl = systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
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



        protected void PoPuP(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["empId"]);
            string type = e.ExtraParams["type"];


            //ReportGenericRequest req = new ReportGenericRequest();
            //req.paramString = "1|"+ id + "^2|" + ColStartDate.Value.ToString().Substring(6,4) + ColStartDate.Value.ToString().Substring(3, 2) +
            //    ColStartDate.Value.ToString().Substring(0, 2) + "^3|" + ColEndDate.Value.ToString().Substring(6, 4) + ColEndDate.Value.ToString().Substring(3, 2) +
            //    ColEndDate.Value.ToString().Substring(0, 2) + "^4|35^5|2";

            MLDListRequest mlLR = new MLDListRequest();
            mlLR.PayId = payId.SelectedItem.Value;
            mlLR.EmployeeId = id.ToString();

            ListResponse<DashBoardTimeVariation2> daysResponse = _timeAttendanceService.ChildGetAll<DashBoardTimeVariation2>(mlLR);            
            if (!daysResponse.Success)
            {
                Common.errorMessage(daysResponse);
                return;
            }
            bool rtl = systemService.SessionHelper.CheckIfArabicSession();
            List<XMLDictionary> timeCode = ConstTimeVariationType.TimeCodeList(systemService);
            List<XMLDictionary> statusList = Common.XMLDictionaryList(systemService, "13");
            List<XMLDictionary> damageList = Common.XMLDictionaryList(systemService, "22");
            daysResponse.Items.ForEach(
                x =>
                {
                    x.clockDurationString = time(x.clockDuration, true);
                    x.durationString = time(x.duration, true);
                    x.timeCodeString = timeCode.Where(y => y.key == Convert.ToInt16(x.timeCode)).Count() != 0 ? timeCode.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;

                    x.apStatusString = statusList.Where(y => y.key == x.apStatus).Count() != 0 ? statusList.Where(y => y.key == x.apStatus).First().value : "";
                    x.damageLevelString = damageList.Where(y => y.key == x.damageLevel).Count() != 0 ? damageList.Where(y => y.key == x.damageLevel).First().value : "";
                    if (rtl)
                        x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("ar-AE"));
                    else
                        x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("en-US"));


                }
                );
            TimeVariationStore.DataSource = daysResponse.Items;
            TimeVariationStore.DataBind();
            //e.Total = daysResponse.count;
            //format.Text = systemService.SessionHelper.GetDateformat().ToUpper();
            this.EditRecordWindow.Show();
        }


        [DirectMethod]
        public string CheckSession()
        {
            if (!systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }


        protected void FillDates(object sender, DirectEventArgs e)
        {
            try
            {
                string payId = e.ExtraParams["payId"];
                if (string.IsNullOrEmpty(payId))
                    return;

                MonthlyLatenessPeriodListRequest rr = new MonthlyLatenessPeriodListRequest();
                rr.PayID = payId;
                RecordResponse<MonthlyLatenessPeriod> response = _timeAttendanceService.ChildGetRecord<MonthlyLatenessPeriod>(rr);
                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(response);
                    return;
                }

                //colStartDate.Text = response.Items[0].startDate;
                //colEndDate.Text = response.Items[0].endDate;

                ColStartDate.Text = response.result.startDate.ToString("dd/MM/yyyy");
                ColEndDate.Text = response.result.endDate.ToString("dd/MM/yyyy");

            }
            catch (Exception exp)
            {
                X.MessageBox.Alert(GetGlobalResourceObject("Common", "Error").ToString(), exp.Message);
            }


        }

    }
            
}