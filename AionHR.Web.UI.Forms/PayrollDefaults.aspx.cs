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
            if (!string.IsNullOrEmpty(values.ulDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ulDeductionId", values.ulDeductionId.ToString()));
            if (!string.IsNullOrEmpty(values.ssDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ssDeductionId", values.ssDeductionId.ToString()));
            if (!string.IsNullOrEmpty(values.loanDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("loanDeductionId", values.loanDeductionId.ToString()));
            if (!string.IsNullOrEmpty(values.peDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("penaltyDeductionId", values.peDeductionId.ToString()));
            if (!string.IsNullOrEmpty(values.otEntitlementId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("otEntitlementId", values.otEntitlementId.ToString()));
          
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
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
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
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
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
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
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
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
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

    
    }
}