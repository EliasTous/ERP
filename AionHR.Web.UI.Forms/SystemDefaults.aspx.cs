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
using AionHR.Model.Employees.Leaves;

namespace AionHR.Web.UI.Forms
{
    public partial class SystemDefaults : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
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


                ListRequest req = new ListRequest();
                ListResponse<KeyValuePair<string, string>> defaults = _systemService.ChildGetAll<KeyValuePair<string, string>>(req);
                if (!defaults.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, defaults.Summary).Show();
                    return;
                }
                FillCombos();
                FillDefaults(defaults.Items);
            }

        }

        private void FillCombos()
        {
            FillNationality();
            FillCurrency();
            FillCalendars();
            FillVS();
        }

        private void FillDefaults(List<KeyValuePair<string, string>> items)
        {
            try
            {
                currencyIdCombo.Select(items.Where(s => s.Key == "currencyId").First().Value);
            }
            catch { }
            try
            {
                countryIdCombo.Select(items.Where(s => s.Key == "countryId").First().Value);
            }
            catch { }
            try
            {
                dateFormatCombo.Select(items.Where(s => s.Key == "dateFormat").First().Value);
            }
            catch { }
            try { nameFormatCombo.Select(items.Where(s => s.Key == "nameFormat").First().Value); }
            catch { }
            try
            {
                timeZoneCombo.Select(items.Where(s => s.Key == "timeZone").First().Value);
            }

            catch { }
            try { fdowCombo.Select(items.Where(s => s.Key == "fdow").First().Value); }

            catch { }
            try { caId.Select(items.Where(s => s.Key == "caId").First().Value); }

            catch { }
            try { vsId.Select(items.Where(s => s.Key == "vsId").First().Value); }

            catch { }
            try

            {
                enableCameraCheck.Checked = items.Where(s => s.Key == "enableCamera").First().Value == "true";
            }
            catch { }
            try

            {
                enableHijri.Checked = items.Where(s => s.Key == "enableHijri").First().Value == "true";
            }
            catch { }

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

            }
        }


        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {
            List<KeyValuePair<string, string>> submittedValues = new List<KeyValuePair<string, string>>();
            dynamic values = JsonConvert.DeserializeObject(e.ExtraParams["values"]);
            if (!string.IsNullOrEmpty(values.countryId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("countryId", values.countryId.ToString()));
            if (!string.IsNullOrEmpty(values.currencyId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("currencyId", values.currencyId.ToString()));
            if (!string.IsNullOrEmpty(values.nameFormat.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("nameFormat", values.nameFormat.ToString()));
            if (!string.IsNullOrEmpty(values.dateFormat.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("dateFormat", values.dateFormat.ToString()));
            if (!string.IsNullOrEmpty(values.timeZone.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("timeZone", values.timeZone.ToString()));
            if (!string.IsNullOrEmpty(values.fdow.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("fdow", values.fdow.ToString()));
            if (!string.IsNullOrEmpty(values.caId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("caId", values.caId.ToString()));
            if (!string.IsNullOrEmpty(values.vsId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("vsId", values.vsId.ToString()));

            submittedValues.Add(new KeyValuePair<string, string>("enableCamera", values.enableCamera == null ? "false" : "true"));
            submittedValues.Add(new KeyValuePair<string, string>("enableHijri", values.enableHijri == null ? "false" : "true"));
            KeyValuePair<string, string>[] valArr = submittedValues.ToArray();
            PostRequest<KeyValuePair<string, string>[]> req = new PostRequest<KeyValuePair<string, string>[]>();
            req.entity = valArr;
            PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            else
            {
                FillDefaults(submittedValues);
                if (!string.IsNullOrEmpty(values.dateFormat.ToString()))
                    _systemService.SessionHelper.SetDateformat(values.dateFormat.ToString());
                if (!string.IsNullOrEmpty(values.nameFormat.ToString()))
                    _systemService.SessionHelper.SetNameFormat(values.nameFormat.ToString());
                if (!string.IsNullOrEmpty(values.countryId.ToString()))
                    _systemService.SessionHelper.SetDefaultCountry(values.countryId.ToString());
                if (!string.IsNullOrEmpty(values.timeZone.ToString()))
                    _systemService.SessionHelper.SetDefaultTimeZone(Convert.ToInt32(values.timeZone.ToString()));
                _systemService.SessionHelper.SetHijriSupport(values.enableHijri == null ? false : true);

                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
            }
        }


        private void FillNationality()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(nationalityRequest);
            NationalityStore.DataSource = resp.Items;
            NationalityStore.DataBind();

        }
        private void FillCurrency()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Currency> resp = _systemService.ChildGetAll<Currency>(nationalityRequest);
            CurrencyStore.DataSource = resp.Items;
            CurrencyStore.DataBind();

        }

        protected void addNationality(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(countryIdCombo.Text))
                return;
            Nationality obj = new Nationality();
            obj.name = countryIdCombo.Text;

            PostRequest<Nationality> req = new PostRequest<Nationality>();
            req.entity = obj;

            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillNationality();
                countryIdCombo.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        private void FillCalendars()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<WorkingCalendar> resp = _timeAttendanceService.ChildGetAll<WorkingCalendar>(nationalityRequest);
            caStore.DataSource = resp.Items;
            caStore.DataBind();
        }
        private void FillVS()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<VacationSchedule> resp = _leaveManagementService.ChildGetAll<VacationSchedule>(nationalityRequest);
            vsStore.DataSource = resp.Items;
            vsStore.DataBind();
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

        protected void addCurrency(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(countryIdCombo.Text))
                return;
            Currency dept = new Currency();
            dept.name = currencyIdCombo.Text;

            PostRequest<Currency> depReq = new PostRequest<Currency>();
            depReq.entity = dept;
            PostResponse<Currency> response = _systemService.ChildAddOrUpdate<Currency>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCurrency();
                currencyIdCombo.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        protected void addCalendar(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(caId.Text))
                return;
            WorkingCalendar dept = new WorkingCalendar();
            dept.name = caId.Text;

            PostRequest<WorkingCalendar> depReq = new PostRequest<WorkingCalendar>();
            depReq.entity = dept;
            PostResponse<WorkingCalendar> response = _timeAttendanceService.ChildAddOrUpdate<WorkingCalendar>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCalendars();
                caId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }


        protected void addVS(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(vsId.Text))
                return;
            VacationSchedule dept = new VacationSchedule();
            dept.name = vsId.Text;

            PostRequest<VacationSchedule> depReq = new PostRequest<VacationSchedule>();
            depReq.entity = dept;
            PostResponse<VacationSchedule> response = _leaveManagementService.ChildAddOrUpdate<VacationSchedule>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillVS();
                vsId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

    }
}