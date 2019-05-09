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


namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT507 : System.Web.UI.Page
    {
       
            ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
            ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
            ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
            IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
            IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
            IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
            static List<GenerationHeader> payIdList;

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

        
            private void FillReport(bool isInitial = false, bool throwException = true)
            {


            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;


            ListResponse<AionHR.Model.Reports.RT507> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT507>(req);

            if (!resp.Success)
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
            List<AionHR.Model.Reports.RT501> Items = new List<AionHR.Model.Reports.RT501>();
            Items.AddRange(resp.Items);
            Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);
            EmployeesPaySlip h = new EmployeesPaySlip(Items, _systemService.SessionHelper.CheckIfArabicSession(),parameters);




            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            string user = _systemService.SessionHelper.GetCurrentUser();
            //h.Parameters["User"].Value = user;
          //  h.Parameters["Fitlers"].Value = texts.Text;


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