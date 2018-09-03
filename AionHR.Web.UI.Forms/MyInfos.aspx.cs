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
using AionHR.Model.Employees.Profile;
using AionHR.Model.SelfService;

namespace AionHR.Web.UI.Forms
{
    public partial class MyInfos : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISelfServiceService _iselfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();
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
                try
                {
                     AccessControlApplier.ApplyAccessControlOnPage(typeof(MyInfo), BasicInfoTab, null,null,SaveButton);

                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();

                    return;
                }

                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetCurrentUserId().ToString()))
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();

                //RecordRequest r = new RecordRequest();
                //r.RecordID = _systemService.SessionHelper.GetCurrentUserId();
                //RecordResponse<UserInfo> response = _systemService.ChildGetRecord<UserInfo>(r);
                //if (!response.Success)
                //{
                //    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary, "closeCurrentTab()").Show();
                //    return;
                //}
                CurrentEmployee.Text = _systemService.SessionHelper.GetEmployeeId();
                // EditRecordWindow.Loader.Params("employeeId") = CurrentEmployee.Text; 

                RecordRequest req = new RecordRequest();
                req.RecordID = CurrentEmployee.Text;
                RecordResponse<MyInfo> resp = _iselfServiceService.ChildGetRecord<MyInfo>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+resp.LogId : resp.Summary).Show();
                    return;
                }
                if (resp.result != null)
                {
                    resp.result.familyName = resp.result.name.familyName.ToString();
                    resp.result.middleName = resp.result.name.middleName.ToString();
                    resp.result.firstName = resp.result.name.firstName.ToString();
                    resp.result.lastName = resp.result.name.lastName.ToString();
                    resp.result.reference = resp.result.name.reference.ToString();
                    resp.result.fullName = resp.result.name.fullName.ToString();

                    BasicInfoTab.Reset();
                    BasicInfoTab.SetValues(resp.result);



                }
                this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                this.EditRecordWindow.Show();







                //try
                //{
                //   // AccessControlApplier.ApplyAccessControlOnPage(typeof(HireInfo), actualPanel, null, null, saveButton);

                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();

                //    return;
                //}
                ////if (recruitmentInfo.InputType == InputType.Password)
                ////{
                ////    recruitmentInfo.Visible = false;
                ////    infoField.Visible = true;
                ////}
            }

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


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.EditRecordWindow.RTL = true;

            }
        }


        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            MyInfo h = JsonConvert.DeserializeObject<MyInfo>(obj, settings);
            h.name = new EmployeeName { firstName = firstName.Text, lastName = lastName.Text, familyName = familyName.Text, middleName = middleName.Text, reference = reference.Text };

            h.recordId = CurrentEmployee.Text;

            h.familyName = null;
            h.middleName = null;
            PostRequest<MyInfo> req = new PostRequest<MyInfo>();

            req.entity = h;


            PostResponse<MyInfo> resp = _iselfServiceService.ChildAddOrUpdate<MyInfo>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }

            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });


        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>


        [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }

        private void FixLoaderUrls(string employeeId, string hireDate, bool terminated)
        {
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.Loader != null)
                    item.Loader.Url = item.Loader.Url + "?employeeId=" + employeeId + "&hireDate=" + hireDate + "&terminated=" + (terminated ? "1" : "0");
            }
        }

    }
}