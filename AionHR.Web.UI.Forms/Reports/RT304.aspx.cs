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
using Model.Reports;

namespace Web.UI.Forms.Reports
{
    public partial class RT304 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();


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



                    //format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();



                }
                catch { }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Model.Reports.RT103), null, null, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
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
                //this.rtl.Text = rtl.ToString();
            }
        }

        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "0";

            req.Add(jobInfo1.GetJobInfo());


            return req;
        }

        protected void firstStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ReportCompositeRequest requst = GetRequest();
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

        protected void Unnamed_Event(object sender, DirectEventArgs e)
        {
            ReportCompositeRequest request = GetRequest();
            if (string.IsNullOrEmpty(e.ExtraParams["week"]))
                return;
            string week = e.ExtraParams["week"];
            int year = Convert.ToInt32(week.Split('-')[0]);
            int weekNo = Convert.ToInt32(week.Split('-')[1]);
            DateTime d = FirstDateOfWeekISO8601(year, weekNo);
            DateTime dF = d.AddDays(6);
            DateRangeParameterSet r = new DateRangeParameterSet();
            r.DateFrom = d;
            r.DateTo = dF;
            r.IsDayId = true;
            request.Add(r);
            ListResponse<Model.Reports.RT304> resp = _reportsService.ChildGetAll<Model.Reports.RT304>(request);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
                return;
            }
            if (resp.Items.Count > 0)
                DisplayResult(resp.Items);
            else
            {
                firstStore.DataSource = new List<departmentAvailability>();
                firstStore.DataBind();
            }

        }

        private void DisplayResult(List<Model.Reports.RT304> items)
        {
            List<TimeSpan> timesFrom = items
                      .Select(x => x.from)
                      .ToList();

            var min = timesFrom.Min();
            List<TimeSpan> timesTo = items
                      .Select(x => x.to)
                      .ToList();

            var max = timesTo.Max();
            List<departmentAvailability> avs = new List<departmentAvailability>();
            InitAvailability(avs, min, max);

            //items.Where(x => x.dow == 1).ToList().ForEach(x => { foreach (var item in avs) { if (item.from >= x.from && item.to <= x.to) { if (x.active) item.day1 += x.headCount; else item.day1 -= x.headCount; } } });

            var first = items.Where(x => x.dow == 1).ToList();
            foreach (var item in avs)
            {
                foreach (var x in first)
                {
                    if (item.from >= x.from && item.to <= x.to)
                    {
                        if (x.active)
                            item.day1 += x.headCount;
                        else item.day1 -= x.headCount;
                    }
                }
            }
            items.Where(x => x.dow == 2).ToList().ForEach(x => { foreach (var item in avs) { if (item.from >= x.from && item.to <= x.to) { if (x.active) item.day2 += x.headCount; else item.day2 -= x.headCount; } } });
            items.Where(x => x.dow == 3).ToList().ForEach(x => { foreach (var item in avs) { if (item.from >= x.from && item.to <= x.to) { if (x.active) item.day3 += x.headCount; else item.day3 -= x.headCount; } } });
            items.Where(x => x.dow == 4).ToList().ForEach(x => { foreach (var item in avs) { if (item.from >= x.from && item.to <= x.to) { if (x.active) item.day4 += x.headCount; else item.day4 -= x.headCount; } } });
            items.Where(x => x.dow == 5).ToList().ForEach(x => { foreach (var item in avs) { if (item.from >= x.from && item.to <= x.to) { if (x.active) item.day5 += x.headCount; else item.day5 -= x.headCount; } } });
            items.Where(x => x.dow == 6).ToList().ForEach(x => { foreach (var item in avs) { if (item.from >= x.from && item.to <= x.to) { if (x.active) item.day6 += x.headCount; else item.day6 -= x.headCount; } } });
            items.Where(x => x.dow == 7).ToList().ForEach(x => { foreach (var item in avs) { if (item.from >= x.from && item.to <= x.to) { if (x.active) item.day7 += x.headCount; else item.day7 -= x.headCount; } } });
            firstStore.DataSource = avs;
            firstStore.DataBind();

        }

        private void InitAvailability(List<departmentAvailability> avs, TimeSpan min, TimeSpan max)
        {
            TimeSpan p = min;
            while (p <= max)
            {
                avs.Add(new departmentAvailability() { from = p, to = p.Add(new TimeSpan(0, 30, 0)) });
                p = p.Add(new TimeSpan(0, 30, 0));
            }
        }




        private DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
    }
}