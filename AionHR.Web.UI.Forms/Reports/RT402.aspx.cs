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

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT402 : System.Web.UI.Page
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
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT109), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;

                        return;
                    }
                    //dateRange1.DefaultStartDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                   ErrorEmptyValue.Text= GetLocalResourceObject("ErrorEmptyValue").ToString();

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
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }


        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "0";

            req.SortBy = "date";
            // req.Add(dateRange1.GetRange());
            //  req.Add(employeeCombo1.GetEmployee());

            req.Add(GetEmployee());



            return req;
        }

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = GetNameFormat();

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }

        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }
        private void FillReport(bool isInitial = false, bool throwException = true)
        {

            ReportCompositeRequest req = GetRequest();

            ListResponse<AionHR.Model.Reports.RT402> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT402>(req);
            if (!resp.Success)
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());

            for (int i=0; i<resp.Items.Count;i++)
            {
                if (i == 0)
                    resp.Items[i].balance = resp.Items[i].amount;
                else
                    resp.Items[i].balance = resp.Items[i-1].balance+ resp.Items[i].amount;
                resp.Items[i].dateStringFormat = resp.Items[i].date.ToString(_systemService.SessionHelper.GetDateformat());

            }

          
                LoanStatement h = new LoanStatement();
                h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
                h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
                h.DataSource = resp.Items;

                //  string from = DateTime.Parse(req.Parameters["_fromDate"]).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
                //  string to = DateTime.Parse(req.Parameters["_toDate"]).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
                string user = _systemService.SessionHelper.GetCurrentUser();




                h.Parameters["User"].Value = user;
                h.Parameters["EmployeeName"].Value = employeeFilter.SelectedItem.Text;

                h.Parameters["descriptionTrxType1"].Value = GetLocalResourceObject("descriptionTrxType1");
                h.Parameters["descriptionTrxType2"].Value = GetLocalResourceObject("descriptionTrxType2");








                h.CreateDocument();


                ASPxWebDocumentViewer1.DataBind();
                ASPxWebDocumentViewer1.OpenReport(h);
            
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
      
        protected void ASPxCallbackPanel1_Load(object sender, EventArgs e)
        {
            // ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            // FillReport(true);
        }

        public EmployeeParameterSet GetEmployee()
        {
            EmployeeParameterSet s = new EmployeeParameterSet();
            int bulk;
            if (employeeFilter.Value == null || !int.TryParse(employeeFilter.Value.ToString(), out bulk))

                s.employeeId = 0;
            else
                s.employeeId = bulk;

            return s;
        }

    }
}
