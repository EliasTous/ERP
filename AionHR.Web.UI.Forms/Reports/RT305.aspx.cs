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
using AionHR.Model.Dashboard;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT305 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IDashBoardService _dashBoardService = ServiceLocator.Current.GetInstance<IDashBoardService>();
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
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT302), null, null, null, null);
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
                    dateRange1.DefaultStartDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                    
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
            ReportParameterSet TVrequest = new ReportParameterSet();
            if (string.IsNullOrEmpty(apStatus.Value.ToString()))
                TVrequest.Parameters.Add("_apStatus", "0");
            else
                TVrequest.Parameters.Add("_apStatus", apStatus.Value.ToString());

            TVrequest.Parameters.Add("_fromDuration", fromDuration.Text);
            TVrequest.Parameters.Add("_toDuration", toDuration.Text);

            TVrequest.Parameters.Add("_esId", "0");
            TVrequest.Parameters.Add("_fromDayId", dateRange1.GetRange().DateFrom.ToString(("yyyyMMdd")));
            TVrequest.Parameters.Add("_toDayId", dateRange1.GetRange().DateTo.ToString(("yyyyMMdd")));
           

            req.Size = "1000";
            req.StartAt = "1";

            
            
            req.Add(employeeCombo1.GetEmployee());
            req.Add(jobInfo1.GetJobInfo());
            req.Add(TVrequest);

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

            TimeVariationListRequest req = GetAbsentRequest();
           
            ListResponse<Model.Reports.RT305> resp = _reportsService.ChildGetAll<Model.Reports.RT305>(req);
            if (!resp.Success)
            {
                
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(resp);
                    return;
                
            }


           // var edAmountList = resp.Items.GroupBy(x => new { x.employeeId, x.timeCode });
           //// List<Model.Reports.RT305> RT305 = new List<Model.Reports.RT305>();
           // foreach (var item in edAmountList)
           // {


           //     var sums = item.ToList().GroupBy(x => new {x.employeeId , x.timeCode })
           //                      .Select(group => group.Sum(x => x.edAmount)).First();
              
           //         resp.Items.Where(x => x.employeeId == item.ToList().First().employeeId && x.timeCode == item.ToList().First().timeCode).ToList().ForEach(y => y.edAmount = Math.Round(sums, 3));
           //     //item.ToList().First().edAmount = Convert.ToDouble(sums);
           //     //RT305.Add(item.First());


           // }

            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            resp.Items.ForEach(
                x =>
                {
                    x.edAmount = Math.Round(x.edAmount, 2);
                    x.clockDurationString = ConstTimeVariationType.time(x.clockDuration, true);
                    x.durationString = ConstTimeVariationType.time(x.duration, true);
                    x.timeCodeString = ConstTimeVariationType.FillTimeCode(x.timeCode,_systemService);
                    x.apStatusString = FillApprovalStatus(x.apStatus);
                    x.damageLevelString = FillDamageLevelString(x.damageLevel);
                    if (rtl)
                        x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("ar-AE"));
                    else
                        x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("en-US"));


                }
                );

            Absense h = new Absense();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            h.DataSource = resp.Items; 

            string from = req.fromDayId.ToString(_systemService.SessionHelper.GetDateformat());
            string to = req.toDayId.ToString(_systemService.SessionHelper.GetDateformat());
            string user = _systemService.SessionHelper.GetCurrentUser();

            h.Parameters["From"].Value = from;
            h.Parameters["To"].Value = to;
            h.Parameters["User"].Value = user;


            if (resp.Items.Count > 0)
            {
                //if (req.Parameters["_departmentId"] != "0")
                //    h.Parameters["Department"].Value = resp.Items[0].departmentName;
                //else
                //    h.Parameters["Department"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_branchId"] != "0")
                    h.Parameters["Branch"].Value = resp.Items[0].branchName;
                else
                    h.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_positionId"] != "0")
                    h.Parameters["Position"].Value = jobInfo1.GetPosition();
                else
                    h.Parameters["Position"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_divisionId"] != "0")
                    h.Parameters["Division"].Value = jobInfo1.GetDivision();
                else
                    h.Parameters["Division"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_employeeId"] != "0")
                    h.Parameters["Employee"].Value = resp.Items[0].employeeName.fullName;
                else
                    h.Parameters["Employee"].Value = GetGlobalResourceObject("Common", "All");
            }







            h.PrintingSystem.Document.ScaleFactor = 4;
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

        private TimeVariationListRequest GetAbsentRequest()
        {

            TimeVariationListRequest reqTV = new TimeVariationListRequest();
            reqTV.BranchId = jobInfo1.GetJobInfo().BranchId == null ? reqTV.BranchId = 0 : jobInfo1.GetJobInfo().BranchId;
            reqTV.DepartmentId = jobInfo1.GetJobInfo().DepartmentId == null ? reqTV.DepartmentId = 0 : jobInfo1.GetJobInfo().DepartmentId;
            reqTV.DivisionId = jobInfo1.GetJobInfo().DivisionId == null ? reqTV.DivisionId = 0 : jobInfo1.GetJobInfo().DivisionId;
            reqTV.PositionId = jobInfo1.GetJobInfo().PositionId == null ? reqTV.PositionId = 0 : jobInfo1.GetJobInfo().PositionId;
            reqTV.EsId = 0;
            reqTV.fromDayId = dateRange1.GetRange().DateFrom;
            reqTV.toDayId = dateRange1.GetRange().DateTo;
            reqTV.employeeId = employeeCombo1.GetEmployee().employeeId.ToString();
            if (string.IsNullOrEmpty(apStatus.Value.ToString()))
            {

                reqTV.apStatus = "0";
            }
            else
                reqTV.apStatus = apStatus.Value.ToString();
            if (string.IsNullOrEmpty(fromDuration.Text))
            {

                reqTV.fromDuration = "0";
            }
            else
                reqTV.fromDuration = fromDuration.Text;
            if (string.IsNullOrEmpty(toDuration.Text))
            {

                reqTV.toDuration = "0";
            }
            else
                reqTV.toDuration = toDuration.Text;

            if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
            {

                reqTV.timeCode = "0";
            }
            else
                reqTV.timeCode = timeVariationType.Value.ToString();



            return reqTV;
        }

     
        private string FillApprovalStatus(short? apStatus)
        {
            string R;
            switch (apStatus)
            {
                case 1:
                    R = GetLocalResourceObject("FieldNew").ToString();
                    break;
                case 2:
                    R = GetLocalResourceObject("FieldApproved").ToString();
                    break;
                case -1:
                    R = GetLocalResourceObject("FieldRejected").ToString();
                    break;
                default:
                    R = string.Empty;
                    break;


            }
            return R;
        }

        private string FillDamageLevelString(short? DamageLevel)
        {
            string R;
            switch (DamageLevel)
            {
                case 1:
                    R = GetLocalResourceObject("DamageWITHOUT_DAMAGE").ToString();
                    break;
                case 2:
                    R = GetLocalResourceObject("DamageWITH_DAMAGE").ToString();
                    break;
                default:
                    R = string.Empty;
                    break;

            }
            return R;
        }
    }
}