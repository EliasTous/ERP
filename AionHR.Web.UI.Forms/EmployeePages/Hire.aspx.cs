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

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class Hire : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
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
             
                if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                CurrentEmployee.Text = Request.QueryString["employeeId"];
                FillNoticePeriod();
                FillBranchField();

                HireInfoRecordRequest req = new HireInfoRecordRequest();
                req.EmployeeId = Request.QueryString["employeeId"];
                RecordResponse<HireInfo> resp = _employeeService.ChildGetRecord<HireInfo>(req);
                if(!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                    return;
                }
                if (resp.result != null)
                {
                    hireInfoForm.SetValues(resp.result);
                    npId.Select(resp.result.npId.ToString());
                    regBranchId.Select(resp.result.regBranchId);
                }
                probationEndDate.Format = nextReviewDate.Format = termEndDate.Format = pyActiveDate.Format= _systemService.SessionHelper.GetDateformat();

                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";

                saveButton.Disabled = disabled;

                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(HireInfo), actualPanel, null, null, saveButton);
                    
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport11.Hidden = true;
                    return;
                }
                if (recruitmentInfo.InputType == InputType.Password)
                {
                    recruitmentInfo.Visible = false;
                    infoField.Visible = true;
                }
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
                this.Viewport11.RTL = true;

            }
        }


        protected void SaveHire(object sender, DirectEventArgs e)
        {
            string info = e.ExtraParams["values"];
            HireInfo h = JsonConvert.DeserializeObject<HireInfo>(info);

            PostRequest<HireInfo> req = new PostRequest<HireInfo>();
            req.entity = h;
            h.employeeId = CurrentEmployee.Text;

            PostResponse<HireInfo> resp = _employeeService.ChildAddOrUpdate<HireInfo>(req);
            if(!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
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

        private void FillNoticePeriod()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<NoticePeriod> resp = _employeeService.ChildGetAll<NoticePeriod>(departmentsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
            npStore.DataSource = resp.Items;
            npStore.DataBind();
        }
        protected void addNP(object sender, DirectEventArgs e)
        {
            NoticePeriod dept = new NoticePeriod();
            dept.name = npId.Text;
           
            PostRequest<NoticePeriod> depReq = new PostRequest<NoticePeriod>();
            depReq.entity = dept;
            PostResponse<NoticePeriod> response = _employeeService.ChildAddOrUpdate<NoticePeriod>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillNoticePeriod();
                npId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
                return;
            }

        }
        protected void addBranch(object sender, DirectEventArgs e)
        {
            Branch obj = new Branch();
            obj.name = regBranchId.Text;

            PostRequest<Branch> req = new PostRequest<Branch>();
            req.entity = obj;
            PostResponse<Branch> response = _companyStructureService.ChildAddOrUpdate<Branch>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillBranchField();
                regBranchId.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
                return;
            }


        }
        private void FillBranchField()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return;
            }
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
        }

    }
}