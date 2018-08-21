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
    public partial class RT503 : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
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


                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT200), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }
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
            req.StartAt = "1";

            req.Add(paymentMethodCombo.GetPaymentMethod());
            req.Add(jobInfo1.GetJobInfo());
            req.Add(GetPayRef());
         
            

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

            req.StartAt = "1";
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

            ListResponse<AionHR.Model.Reports.RT503> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT503>(req);
            if (!resp.Success)
            {
                if (throwException)
                    throw new Exception(resp.Summary);
                else
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                    return;
                }
            }

            var d = resp.Items.GroupBy(x => x.departmentName);
            DepartmentPayrollLineCollection lines = new DepartmentPayrollLineCollection();
            HashSet<DepartmentEntitlementDeduction> ens = new HashSet<DepartmentEntitlementDeduction>(new DepartmentEntitlementDeductionComparer());
            HashSet<DepartmentEntitlementDeduction> des = new HashSet<DepartmentEntitlementDeduction>(new DepartmentEntitlementDeductionComparer());
            resp.Items.ForEach(x =>
            {
                DepartmentEntitlementDeduction DE = new DepartmentEntitlementDeduction();

                if (x.edType == 1)
                {

                    try
                    {
                        DE.name = GetLocalResourceObject(x.edName.Trim()).ToString().TrimEnd();
                        DE.amount = 0; DE.isTaxable = x.isTaxable;
                    }
                    catch { DE.name = x.edName; DE.amount = 0; DE.isTaxable = x.isTaxable; }
                    ens.Add(DE);
                }
                else
                {

                    try
                    {
                        DE.name = GetLocalResourceObject(x.edName.Trim()).ToString().TrimEnd();
                        DE.amount = 0;
                    }
                    catch { DE.name = x.edName; DE.amount = 0; }
                    des.Add(DE);
                }
            });

            foreach (var item in d)
            {

                var list = item.ToList();
                list.ForEach(y =>
                {
                    try
                    {
                        y.edName = GetLocalResourceObject(y.edName.Trim()).ToString().TrimEnd();

                    }
                    catch { y.edName = y.edName; }
                });
                DepartmentPayrollLine line = new DepartmentPayrollLine(ens, des, list, GetLocalResourceObject("taxableeAmount").ToString(), GetLocalResourceObject("eAmount").ToString(), GetLocalResourceObject("dAmount").ToString(), GetLocalResourceObject("net").ToString(), GetLocalResourceObject("essString").ToString(), GetLocalResourceObject("cssString").ToString(), _systemService.SessionHelper.GetDateformat(), GetLocalResourceObject("netSalaryString").ToString());
                lines.Add(line);
            }

            DepartmentPayrollCollection s = new DepartmentPayrollCollection();
            if (lines.Count > 0)
            {

                DepartmentPayrollSet p = new DepartmentPayrollSet(GetLocalResourceObject("Entitlements").ToString(), GetLocalResourceObject("Taxable").ToString(), GetLocalResourceObject("Deductions").ToString());
                //p.PayPeriodString = resp.Items[0].startDate.ToString(_systemService.SessionHelper.GetDateformat()) + " - " + resp.Items[0].endDate.ToString(_systemService.SessionHelper.GetDateformat());
                //p.PayDate = GetLocalResourceObject("PaidAt") + " " + resp.Items[0].payDate.ToString(_systemService.SessionHelper.GetDateformat());
                p.Names = (lines[0] as DepartmentPayrollLine).Entitlements;
                p.DIndex = ens.Count;
                p.taxableIndex = ens.Count(x => x.isTaxable);
                p.Payrolls = lines;
                s.Add(p);
            }

            DepartmentPayroll h = new DepartmentPayroll();

            h.DataSource = s;
            h.Parameters["columnCount"].Value = ens.Count + des.Count;
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            string user = _systemService.SessionHelper.GetCurrentUser();
            h.Parameters["User"].Value = user;
            if (resp.Items.Count > 0)
            {


                if (req.Parameters["_branchId"] != "0")
                    h.Parameters["Branch"].Value = jobInfo1.GetBranch();
                else
                    h.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_paymentMethod"] != "0")
                    h.Parameters["Payment"].Value = paymentMethodCombo.GetPaymentMethodString();
                else
                    h.Parameters["Payment"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_payRef"] != "0")
                    h.Parameters["Ref"].Value = req.Parameters["_payRef"];
                else
                    h.Parameters["Ref"].Value = GetGlobalResourceObject("Common", "All");

            }

            h.CreateDocument();
            //rptDepartmentsSalaries h = new rptDepartmentsSalaries(resp.Items, true);
            //h.CreateDocument();
            h.PrintingSystem.Document.AutoFitToPagesWidth = 1;
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
        private PayRefParameterSet GetPayRef()
        {
            PayRefParameterSet p = new PayRefParameterSet();


            if (!string.IsNullOrEmpty(payRef.Text) && payRef.Value.ToString() != "0")
            {
                p.payRef = payRef.Value.ToString(); ;



            }
            else
            {
                p.payRef = "0";

            }
            return p;
        }


    }
}