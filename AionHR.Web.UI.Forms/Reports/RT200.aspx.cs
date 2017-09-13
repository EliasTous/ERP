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


namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT200 : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
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
                    FillReport(false, false);
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

            return req;
        }


        private void FillReport(bool isInitial = false, bool throwException = true)
        {

            ReportCompositeRequest req = GetRequest();

            ListResponse<AionHR.Model.Reports.RT200> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT200>(req);
            if (!resp.Success)
            {
                if (throwException)
                    throw new Exception(resp.Summary);
                else
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                    return;
                }
            }

            var d = resp.Items.GroupBy(x => x.employeeName.reference);
            CurrentPayrollLineCollection lines = new CurrentPayrollLineCollection();
            HashSet<CurrentEntitlementDeduction> ens = new HashSet<CurrentEntitlementDeduction>(new CurrentEntitlementDeductionComparer());
            HashSet<CurrentEntitlementDeduction> des = new HashSet<CurrentEntitlementDeduction>(new CurrentEntitlementDeductionComparer());
            resp.Items.ForEach(x =>
            {
                if (x.edType == 1)
                    ens.Add(new CurrentEntitlementDeduction() { name = x.edName, amount = 0 });
                else
                    des.Add(new CurrentEntitlementDeduction() { name = x.edName, amount = 0 });
            });
            foreach (var item in d)
            {
                var list = item.ToList();
                CurrentPayrollLine line = new CurrentPayrollLine(ens, des, list, GetLocalResourceObject("eAmount").ToString(), GetLocalResourceObject("dAmount").ToString(), GetLocalResourceObject("netSalary").ToString());
                lines.Add(line);
            }

            CurrentPayrollCollection s = new CurrentPayrollCollection();
            if (lines.Count > 0)
            {
                CurrentPayrollSet p = new CurrentPayrollSet(GetLocalResourceObject("Entitlements").ToString(), GetLocalResourceObject("Deductions").ToString());
                //p.PayPeriodString = resp.Items[0].startDate.ToString(_systemService.SessionHelper.GetDateformat()) + " - " + resp.Items[0].endDate.ToString(_systemService.SessionHelper.GetDateformat());
                //p.PayDate = GetLocalResourceObject("PaidAt") + " " + resp.Items[0].payDate.ToString(_systemService.SessionHelper.GetDateformat());
                p.Names = (lines[0] as CurrentPayrollLine).Entitlements;
                p.DIndex = ens.Count;
                p.Payrolls = lines;
                s.Add(p);
            }

            CurrentPayroll h = new CurrentPayroll();
            h.DataSource = s;

            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            string user = _systemService.SessionHelper.GetCurrentUser();
            h.Parameters["User"].Value = user;
            if (resp.Items.Count > 0)
            {
                if (req.Parameters["_departmentId"] != "0")
                    h.Parameters["Department"].Value = jobInfo1.GetDepartment();
                else
                    h.Parameters["Department"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_branchId"] != "0")
                    h.Parameters["Branch"].Value = jobInfo1.GetBranch();
                else
                    h.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_paymentMethod"] != "0")
                    h.Parameters["Payment"].Value = paymentMethodCombo.GetPaymentMethodString();
                else
                    h.Parameters["Payment"].Value = GetGlobalResourceObject("Common", "All");


            }


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



















































    }
}