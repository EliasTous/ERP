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
using AionHR.Model.Employees.Profile;

namespace AionHR.Web.UI.Forms
{
    public partial class PayrollDefaults : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SystemDefault), BasicInfoTab, null, null, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }

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
            ulDeductionStore.DataSource = GetDeductions();
            ulDeductionStore.DataBind();

            ssDeductionStore.DataSource = GetDeductions();
            ssDeductionStore.DataBind();

            peDeductionStore.DataSource = GetDeductions();
            peDeductionStore.DataBind();

            loanDeductionStore.DataSource = GetDeductions();
            loanDeductionStore.DataBind();

            otEntitlementStore.DataSource = GetEntitlements();
            otEntitlementStore.DataBind();

            latenessDeductionStore.DataSource = GetDeductions();
            latenessDeductionStore.DataBind();
        }

        private void FillDefaults(List<KeyValuePair<string, string>> items)
        {
            try
            {
                ulDeductionId.Select(items.Where(s => s.Key == "ulDeductionId").First().Value);
            }
            catch { }
            try
            {
                ssDeductionId.Select(items.Where(s => s.Key == "ssDeductionId").First().Value);
            }
            catch { }
            try
            {
                loanDeductionId.Select(items.Where(s => s.Key == "loanDeductionId").First().Value);
            }
            catch { }
            try { peDeductionId.Select(items.Where(s => s.Key == "penaltyDeductionId").First().Value); }
            catch { }
            try
            {
                otEntitlementId.Select(items.Where(s => s.Key == "otEntitlementId").First().Value);
            }

            catch { }
            try
            {
                ldMethod.Select(items.Where(s => s.Key == "ldMethod").First().Value);
            }

            catch { }
            try
            {
                latenessDeductionId.Select(items.Where(s => s.Key == "latenessDeductionId").First().Value);
            }

            catch { }
            try
            {
                latenessPCTOf.Select(items.Where(s => s.Key == "latenessPCTOf").First().Value);
            }

            catch { }
            try
            {
                overtimePCTOf.Select(items.Where(s => s.Key == "overtimePCTOf").First().Value);
            }

            catch { }

            try
            {
                ldValue.Text = (items.Where(s => s.Key == "ldValue").First().Value);
            }

            catch { }
            try
            {
                latenessDedAmount.Text = (items.Where(s => s.Key == "latenessDedAmount").First().Value);
            }

            catch { }
            try
            {
                overtimeEntAmount.Text = (items.Where(s => s.Key == "overtimeEntAmount").First().Value);
            }

            catch { }
            try
            {
                allowedLateness.Text = (items.Where(s => s.Key == "allowedLateness").First().Value);
            }

            catch { }
            try
            {
                minimumOvertime.Text = (items.Where(s => s.Key == "minimumOvertime").First().Value);
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


        private List<EntitlementDeduction> GetEntitlements()
        {
            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
            return eds.Items.Where(s => s.type == 1).ToList();
        }
        private List<EntitlementDeduction> GetDeductions()
        {
            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
            return eds.Items.Where(s => s.type == 2).ToList();
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
            if (values.ulDeductionId!=null&&!string.IsNullOrEmpty(values.ulDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ulDeductionId", values.ulDeductionId.ToString()));
            if (values.ssDeductionId != null && !string.IsNullOrEmpty(values.ssDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ssDeductionId", values.ssDeductionId.ToString()));
            if (values.loanDeductionId != null && !string.IsNullOrEmpty(values.loanDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("loanDeductionId", values.loanDeductionId.ToString()));
            if (values.peDeductionId != null && !string.IsNullOrEmpty(values.peDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("penaltyDeductionId", values.peDeductionId.ToString()));
            if (values.otEntitlementId != null && !string.IsNullOrEmpty(values.otEntitlementId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("otEntitlementId", values.otEntitlementId.ToString()));
            if (values.latenessDeductionId != null && !string.IsNullOrEmpty(values.latenessDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("latenessDeductionId", values.latenessDeductionId.ToString()));
            if (values.allowedLateness != null && !string.IsNullOrEmpty(values.allowedLateness.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("allowedLateness", values.allowedLateness.ToString()));
            if (values.minimumOvertime != null && !string.IsNullOrEmpty(values.minimumOvertime.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("minimumOvertime", values.minimumOvertime.ToString()));
            if (values.latenessDedAmount != null && !string.IsNullOrEmpty(values.latenessDedAmount.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("latenessDedAmount", values.latenessDedAmount.ToString()));
            if (values.overtimeEntAmount != null && !string.IsNullOrEmpty(values.overtimeEntAmount.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("overtimeEntAmount", values.overtimeEntAmount.ToString()));
            if (values.ldMethod != null && !string.IsNullOrEmpty(values.ldMethod.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ldMethod", values.ldMethod.ToString()));
            if (values.latenessPCTOf != null && !string.IsNullOrEmpty(values.latenessPCTOf.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("latenessPCTOf", values.latenessPCTOf.ToString()));
            if (values.overtimePCTOf != null && !string.IsNullOrEmpty(values.overtimePCTOf.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("overtimePCTOf", values.overtimePCTOf.ToString()));
            if (values.ldValue != null && !string.IsNullOrEmpty(values.ldValue.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ldValue", values.ldValue.ToString()));

            KeyValuePair<string, string>[] valArr = submittedValues.ToArray();
            PostRequest<KeyValuePair<string, string>[]> req = new PostRequest<KeyValuePair<string, string>[]>();
            req.entity = valArr;
            PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            else
            {


                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
            }
        }




        protected void addEnt(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(otEntitlementId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = otEntitlementId.Text;
            dept.type = 1;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCombos();
                otEntitlementId.Value = response.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

        }

        protected void addDedUL(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(ulDeductionId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = ulDeductionId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCombos();
                ulDeductionId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

        }
        protected void addDedSS(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(ssDeductionId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = ssDeductionId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCombos();
                ssDeductionId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

        }
        protected void addDedpe(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(peDeductionId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = peDeductionId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCombos();
                peDeductionId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

        }
        protected void addDedloan(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(loanDeductionId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = loanDeductionId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCombos();
                loanDeductionId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

        }
        protected void addDedLateness(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(latenessDeductionId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = latenessDeductionId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCombos();
                latenessDeductionId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
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


    }
}