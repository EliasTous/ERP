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
using AionHR.Services.Messaging.TimeAttendance;
using AionHR.Model.TimeAttendance;
using AionHR.Web.UI.Forms.ConstClasses;
using Reports.ShiftLogs;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT309 : System.Web.UI.Page
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
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT309), null, null, null, null);
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


                    if (Request.QueryString["_fromUP"] == "true" && !string.IsNullOrEmpty(Request.QueryString["_employeeId"]))
                    {
                        reportToolbar.Hidden = true;


                        Dictionary<string, string> parameters = new Dictionary<string, string>();
                        if (!string.IsNullOrEmpty(Request.QueryString["_startDate"]))
                            parameters.Add("1", Request.QueryString["_startDate"]);
                        if (!string.IsNullOrEmpty(Request.QueryString["_endDate"]))
                            parameters.Add("2", Request.QueryString["_endDate"]);
                        if (!string.IsNullOrEmpty(Request.QueryString["_branchId"]))
                            parameters.Add("4", Request.QueryString["_branchId"]);
                        parameters.Add("3", Request.QueryString["_employeeId"]);

                        string rep_params = "";
                        foreach (KeyValuePair<string, string> entry in parameters)
                        {
                            rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                        }
                        if (rep_params.Length > 0)
                        {
                            if (rep_params[rep_params.Length - 1] == '^')
                                rep_params = rep_params.Remove(rep_params.Length - 1);
                        }
                       
                        vals.Text = rep_params;


                        X.Call("reportCallBack");
                      

                       // FillReport(true);


                    }
                    else
                    {
                        reportToolbar.Hidden = false;
                        vals.Text = string.Empty;
                    }

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
        /// 

        [DirectMethod]
        public bool ExistMandatoryFields(string reportCode)
        {
            ReportParametersListRequest req = new ReportParametersListRequest();
            req.ReportName = reportCode;
            ListResponse<ReportParameter> parameters = _systemService.ChildGetAll<ReportParameter>(req);
            if (!parameters.Success)
            {
                X.Msg.Alert("Error", "Error");
            }
            foreach (var item in parameters.Items)
                if (item.mandatory)
                    return true;
            return false;
        }
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



        private void FillReport(bool isInitial = false, bool throwException = true)
        {

          
                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;


                ListResponse<AionHR.Model.Reports.RT309> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT309>(req);
                if (!resp.Success)
                    Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
                int counter = 1;
            int maxShiftCount = 0;
            List<AionHR.Model.Reports.RT309> newShiftLogsList = new List<AionHR.Model.Reports.RT309>();
                AionHR.Model.Reports.RT309 record = new AionHR.Model.Reports.RT309();
                DateTime parsed = DateTime.Now;


            string getLan = _systemService.SessionHelper.getLangauge();

            foreach (var e in resp.Items.GroupBy(x=>x.employeeName))
                {

               
                    e.ToList().ForEach(y =>
                    {

                        counter = 1;
                        if (DateTime.TryParseExact(y.dayId, "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.AdjustToUniversal, out parsed))
                            {


                         

                            y.shiftLog.ForEach(z =>
                            {
                                y.dayIdDateTime = parsed;
                                record = new Model.Reports.RT309();
                                record.employeeName = y.employeeName;
                                record.dayIdDateTime = y.dayIdDateTime;
                                record.branchName = y.branchName;
                                record.departmentName = y.departmentName;
                                record.positionName = y.positionName;
                                record.duration = y.duration;
                                record.shiftLog = new List<ShiftLog>();
                                record.shiftLog.Add(new ShiftLog { start = z.start, end = z.end, shiftStatus = z.shiftStatus });

                                //if (_systemService.SessionHelper.CheckIfArabicSession())
                                if (getLan == "ar")
                                    record.shiftId = String.Format("{0} {1}", "فترة  ",counter);
                                else if (getLan == "fr")
                                    record.shiftId = String.Format("{0} {1}", "Horraire ", counter);
                                else 
                                    record.shiftId = String.Format("{0} {1}", "shift ", counter);
                                if (maxShiftCount < counter)
                                    maxShiftCount = counter;
                                
                                counter++;
                                newShiftLogsList.Add(record);
                               

                            }
                            );
                        }

                    });
                  
                  
                }
                Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);
                ShiftLogsReport h = new ShiftLogsReport(newShiftLogsList, /*_systemService.SessionHelper.CheckIfArabicSession()*/ getLan, _systemService.SessionHelper.GetDateformat(), parameters, maxShiftCount);
               // BasicShiftLogReport h = new BasicShiftLogReport(newShiftLogsList, _systemService.SessionHelper.CheckIfArabicSession(), _systemService.SessionHelper.GetDateformat(), parameters, maxShiftCount);
                h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
                h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
             //  h.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            //string from = DateTime.ParseExact(req.Parameters["_fromDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            //string to = DateTime.ParseExact(req.Parameters["_toDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            h.Parameters["User"].Value = string.IsNullOrEmpty(_systemService.SessionHelper.GetCurrentUser()) ? " " : _systemService.SessionHelper.GetCurrentUser();


              
                h.CreateDocument();


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
            ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            //FillReport(true);
        }

        [DirectMethod]
   
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }


    }
}