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
using AionHR.Model.Employees.Leaves;

namespace AionHR.Web.UI.Forms
{
    public partial class SystemDefaults : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
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
        }

        private void FillDefaults(List<KeyValuePair<string, string>> items)
        {
            try
            {
                currencyIdCombo.Select(items.Where(s => s.Key == "currencyId").First().Value);
                countryIdCombo.Select(items.Where(s => s.Key == "countryId").First().Value);
                dateFormatCombo.Select(items.Where(s => s.Key == "dateFormat").First().Value);
                nameFormatCombo.Select(items.Where(s => s.Key == "nameFormat").First().Value);
                timeZoneCombo.Select(items.Where(s => s.Key == "timeZone").First().Value);
                fdowCombo.Select(items.Where(s => s.Key == "fdow").First().Value);
                logCheck.Checked = items.Where(s => s.Key == "transactionLog").First().Value == "on";
                enableCameraCheck.Checked = items.Where(s => s.Key == "enableCamera").First().Value == "true";
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
            submittedValues.Add(new KeyValuePair<string, string>("countryId", values.countryId.ToString()));
            submittedValues.Add(new KeyValuePair<string, string>("currencyId", values.currencyId.ToString()));
            submittedValues.Add(new KeyValuePair<string, string>("nameFormat", values.nameFormat.ToString()));
            submittedValues.Add(new KeyValuePair<string, string>("dateFormat", values.dateFormat.ToString()));
            submittedValues.Add(new KeyValuePair<string, string>("timeZone", values.timeZone.ToString()));
            submittedValues.Add(new KeyValuePair<string, string>("fdow", values.fdow.ToString()));
            submittedValues.Add(new KeyValuePair<string, string>("transactionLog", values.transactionLog == null?"off":"on"));
            submittedValues.Add(new KeyValuePair<string, string>("enableCamera", values.enableCamera == null ? "false" : "true"));
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
                _systemService.SessionHelper.SetDateformat( values.dateFormat.ToString());
                _systemService.SessionHelper.SetNameFormat(values.nameFormat.ToString());
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
                NationalityStore.Insert(0, obj);
                countryIdCombo.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
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

        protected void addPosition(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(countryIdCombo.Text))
                return;
            Nationality dept = new Nationality();
            dept.name = countryIdCombo.Text;

            PostRequest<Nationality> depReq = new PostRequest<Nationality>();
            depReq.entity = dept;
            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                NationalityStore.Insert(0, dept);
                countryIdCombo.Select(0);
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