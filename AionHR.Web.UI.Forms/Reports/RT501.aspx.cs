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
using AionHR.Model.Payroll;
using Reports.EmployeePayRoll;
using Reports.EmployeePayRollCross;
using System.Text.RegularExpressions;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT501 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
     static   List<GenerationHeader> payIdList;

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


                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT501), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }
                    payIdList = new List<GenerationHeader>();
                   
                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                    //FillReport(false, false);
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
        public void SetLabels(string labels)
        {
            this.labels.Text = labels;
        }

        [DirectMethod]
        public void SetVals(string labels)
        {
            this.vals.Text = labels;
        }

        [DirectMethod]
        public void SetTexts(string labels)
        {
            this.texts.Text = labels;
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


        

        private void FillReport(bool isInitial = false, bool throwException = true)
        {

            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;

            ListResponse<AionHR.Model.Reports.RT501> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT501>(req);
            if (!resp.Success)
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
            //resp.Items is the list of RT501 objects  that you can used it as data source for reprot  


            //Filters parameters as string 

            resp.Items.ForEach(x =>
            {
                x.startDateString = x.startDate.ToString(_systemService.SessionHelper.GetDateformat());
                x.endDateString = x.endDate.ToString(_systemService.SessionHelper.GetDateformat());
            });

            string user = _systemService.SessionHelper.GetCurrentUser();
            //string paymentMethod = paymentMethodCombo.GetPaymentMethodString();
            //string payRef = payId.SelectedItem.Text;
            //string department = jobInfo1.GetDepartment();
            //string position = jobInfo1.GetPosition();
            //string employee = employeeFilter.GetEmployeeName();
        
            // this variable for check if the user request arabic report or english   true mean arabic reprot
            bool isArabic = _systemService.SessionHelper.CheckIfArabicSession();
            //those two lines code for fill the viewer with your report 
            //ASPxWebDocumentViewer1.DataBind();
            //ASPxWebDocumentViewer1.OpenReport();



            //Old work 
            //var d = resp.Items.GroupBy(x => x.employeeName.fullName);
            //PayrollLineCollection lines = new PayrollLineCollection();
            //HashSet<Model.Reports.EntitlementDeduction> ens = new HashSet<Model.Reports.EntitlementDeduction>(new EntitlementDeductionComparer());
            //HashSet<Model.Reports.EntitlementDeduction> des = new HashSet<Model.Reports.EntitlementDeduction>(new EntitlementDeductionComparer());
            //resp.Items.ForEach(x =>
            //{
            //    Model.Reports.EntitlementDeduction DE = new Model.Reports.EntitlementDeduction();

            //    if (x.edType == 1)
            //    {

            //        try
            //        {
            //            DE.name = GetLocalResourceObject(x.edName.Trim()).ToString().TrimEnd();
            //            DE.amount = 0; DE.isTaxable = x.isTaxable;
            //        }
            //        catch { DE.name = x.edName; DE.amount = 0; DE.isTaxable = x.isTaxable; }
            //        ens.Add(DE);
            //    }
            //    else
            //    {

            //        try
            //        {
            //            DE.name = GetLocalResourceObject(x.edName.Trim()).ToString().TrimEnd();
            //            DE.amount = 0;
            //        }
            //        catch { DE.name = x.edName; DE.amount = 0; }
            //        des.Add(DE);
            //    }
            //});

            //foreach (var item in d)
            //{
            //    var list = item.ToList();
            //    list.ForEach(y =>
            //    {

            //        try
            //        {
            //            y.edName = GetLocalResourceObject(y.edName.Trim()).ToString().TrimEnd();

            //        }
            //        catch { y.edName = y.edName; }

            //    });
            //    PayrollLine line = new PayrollLine(ens, des, list, GetLocalResourceObject("taxableeAmount").ToString(), GetLocalResourceObject("eAmount").ToString(), GetLocalResourceObject("dAmount").ToString(), GetLocalResourceObject("net").ToString(), GetLocalResourceObject("essString").ToString(), GetLocalResourceObject("cssString").ToString(), _systemService.SessionHelper.GetDateformat(),GetLocalResourceObject("netSalaryString").ToString());
            //    lines.Add(line);
            //}

            //MonthlyPayrollCollection s = new MonthlyPayrollCollection();


            //if (lines.Count > 0)
            //{
            //    MonthlyPayrollSet p = new MonthlyPayrollSet(GetLocalResourceObject("Entitlements").ToString(), GetLocalResourceObject("Taxable").ToString(), GetLocalResourceObject("Deductions").ToString());
            //    p.PayPeriodString = resp.Items[0].startDate.ToString(_systemService.SessionHelper.GetDateformat()) + " - " + resp.Items[0].endDate.ToString(_systemService.SessionHelper.GetDateformat());
            //    p.PayDate = GetLocalResourceObject("PaidAt")+" "+ resp.Items[0].payDate.ToString(_systemService.SessionHelper.GetDateformat());
            //    p.Names = (lines[0] as PayrollLine).Entitlements;
            //    p.DIndex = ens.Count;
            //    p.taxableIndex = ens.Count(x => x.isTaxable);
            //    p.Payrolls = lines;
            //    s.Add(p);
            //}
            Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);
            EmployeePayrollCrossReport h = new EmployeePayrollCrossReport(resp.Items, isArabic, parameters);
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            h.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            //h.DataSource = s;
            //h.objectDataSource2.DataSource = resp.Items.Where(x => x.edType == 2).ToList();



          
            h.Parameters["User"].Value = user;
          


               
          


         

           


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
        protected void Unnamed_Click(object sender, EventArgs e)
        {



        }

        protected void ASPxCallbackPanel1_Load(object sender, EventArgs e)
        {
            //ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            //FillReport(true);
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
        

       
    }
}