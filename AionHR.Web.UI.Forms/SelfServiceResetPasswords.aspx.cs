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
using System;
using AionHR.Infrastructure;

namespace AionHR.Web.UI.Forms
{
    public partial class SelfServiceResetPasswords : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISelfServiceService _selfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(UserInfoSelfService), BasicInfoTab, null, null, SaveButton);

                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();

                    return;
                }

                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetCurrentUserId().ToString()))
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();

              
             
                this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                this.EditRecordWindow.Show();







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
            SelfServiceResetPassword h = JsonConvert.DeserializeObject<SelfServiceResetPassword>(obj, settings);
            RecordRequest r = new RecordRequest();
            r.RecordID = _systemService.SessionHelper.GetCurrentUserId();
            RecordResponse<UserInfoSelfService> response = _selfServiceService.ChildGetRecord<UserInfoSelfService>(r);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary, "closeCurrentTab()").Show();
                return;
            }
           if( EncryptionHelper.encrypt(h.oldPassword)!=response.result.password)
            {
                X.Msg.Alert(Resources.Common.Error,GetLocalResourceObject("incorrectPassword").ToString()).Show();
                return;
            }


            PostRequest<UserInfoSelfService> req = new PostRequest<UserInfoSelfService>();

            req.entity = response.result;
            req.entity.password = EncryptionHelper.encrypt(h.Password);


            PostResponse<UserInfoSelfService> resp = _selfServiceService.ChildAddOrUpdate<UserInfoSelfService>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }

            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });

            EditRecordWindow.Close();
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

       

    }
}