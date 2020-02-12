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
using AionHR.Services.Messaging.TaskManagement;
using AionHR.Services.Messaging.LoanManagment;
using AionHR.Model.LoadTracking;
using AionHR.Model.LeaveManagement;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Messaging.System;
using AionHR.Model.Dashboard;
using AionHR.Model.TimeAttendance;
using AionHR.Services.Messaging.Reports;
using AionHR.Model.Reports;
using AionHR.Model.Access_Control;
using AionHR.Services.Messaging.TimeAttendance;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Services.Messaging.Employees;
using AionHR.Model.Employees;
using AionHR.Services.Messaging.DashBoard;
using AionHR.Services.Messaging.Asset_Management;
using AionHR.Model.AssetManagement;
using AionHR.Services.Messaging.HelpFunction;
using AionHR.Services.Messaging.LeaveManagement;

namespace AionHR.Web.UI.Forms
{
    public partial class Dashboard : System.Web.UI.Page
    {
        static int count = 0;
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ITaskManagementService _taskManagementService = ServiceLocator.Current.GetInstance<ITaskManagementService>();
        ILoanTrackingService _loanService = ServiceLocator.Current.GetInstance<ILoanTrackingService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IHelpFunctionService _helpFunctionService = ServiceLocator.Current.GetInstance<IHelpFunctionService>();
        IDashBoardService _dashBoardService = ServiceLocator.Current.GetInstance<IDashBoardService>();
        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();

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
                
                try
                {
                    switch (_systemService.SessionHelper.getLangauge())
                    {
                        case "ar":
                            {
                                currentLanguage.Text = "ar";
                                ResourceManager1.Locale = "ar";
                            }
                            break;
                        case "en":
                            {
                                currentLanguage.Text = "en";
                                ResourceManager1.Locale = "en";
                            }
                            break;

                        case "fr":
                            {
                                currentLanguage.Text = "fr-FR";
                                ResourceManager1.Locale = "fr-FR";
                            }
                            break;
                        case "de":
                            {
                                currentLanguage.Text = "de-DE";
                                ResourceManager1.Locale = "de-DE";
                            }
                            break;
                        default:
                            {

                                currentLanguage.Text = "en";
                                ResourceManager1.Locale = "en";
                            }
                            break;
                    }

                    SetExtLanguage();
                    HideShowButtons();
                    HideShowColumns();
                    
                    try
                    {
                        //ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
                        //classReq.ClassId = "2001";
                        //classReq.UserId = _systemService.SessionHelper.GetCurrentUserId();
                        //RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
                        //if (modClass.result.accessLevel == 0)
                        //{
                        //    Viewport1.Hidden = true;
                        //    throw new DashBoardAccessDenied();
                        //}
                        //else
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Dashboard.Dashboard), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }
                    try
                    {

                        AccessControlApplier.ApplyAccessControlOnPage(typeof(DashboardLeave), LeaveRecordForm, null, null, SaveButton);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        LeaveRecordWindow.Close();

                        return;
                    }
                    try
                    {
                       // DashboardRequest req2 = GetDashboardRequest();
                        branchAvailabilityStore.Reload();
                        BindAlerts();
                        alertStore.Reload();
                        ppTypeStore.DataSource = Common.XMLDictionaryList(_systemService, "26");
                        ppTypeStore.DataBind();
                        FillUdStore();
                        returnTypeStore.DataSource = Common.XMLDictionaryList(_systemService, "41");
                        returnTypeStore.DataBind();

                        //outStore.Reload();
                        //activeStore.Reload();
                        //latenessStore.Reload();
                        //leavesStore.Reload();
                        //missingPunchesStore.Reload();
                        //checkMontierStore.Reload();
                        format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                        DateColumn4.Format = _systemService.SessionHelper.GetDateformat() + " HH:mm:ss";
                     fromDate.Format=toDate.Format=    dtTo.Format=dtFrom.Format= PADate.Format= ColDate.Format= DateColumn12.Format= DateColumn10.Format=DateColumn11.Format = DateColumn9.Format  =  ColtermEndDate.Format = ColNextReviewDate.Format = ColProbationEndDate.Format = DateColumn5.Format = DateColumn1.Format = DateColumn2.Format =  _systemService.SessionHelper.GetDateformat();
                        periodToDate.SelectedDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                        //CountDateTo.SelectedDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                        CountDateTo.SelectedDate = DateTime.Now;
                        dimension.Select(0);
                      
                        LWFromField.Format = LWToField.Format = _systemService.SessionHelper.GetDateformat();

                    }
                    catch { }
                }
                catch (DashBoardAccessDenied dx) { }
            }

        }


        private void FillUdStore()
        {
            ListRequest req = new ListRequest();
            req.Filter = "";
            ListResponse<BiometricDevice> resp = _timeAttendanceService.ChildGetAll<BiometricDevice>(req);
            if(!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            udStore.DataSource = resp.Items;
            udStore.DataBind();
        }
     

        protected void Page_Init(object sender, EventArgs e)
        {

            leaveRequest1.Store1 = this.LeaveRequestsStore;
            leaveRequest1.GrigPanel1 = this.leaveGrid;

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
            }
        }




        protected void activeStore_refresh(object sender, StoreReadDataEventArgs e)
        {






            ActiveAttendanceRequest r = GetActiveAttendanceRequest();

            ListResponse<ActiveCheck> ACs = _timeAttendanceService.ChildGetAll<ActiveCheck>(r);
            //ListResponse<ActiveAbsence> ABs = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
            ListResponse<ActiveLate> ALs = _timeAttendanceService.ChildGetAll<ActiveLate>(r);
            //ListResponse<ActiveLeave> Leaves = _timeAttendanceService.ChildGetAll<ActiveLeave>(r);
            //ListResponse<MissedPunch> MPs = _timeAttendanceService.ChildGetAll<MissedPunch>(r);
            //ListResponse<ActiveOut> AOs = _timeAttendanceService.ChildGetAll<ActiveOut>(r);
            EmployeeCountRequest req = GetEmployeeCountRequest();
            RecordResponse<EmployeeCount> count = _employeeService.ChildGetRecord<EmployeeCount>(req);
            if (!ACs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, ACs.Summary).Show();
                return;
            }
            List<object> b = new List<object>();
            b.Add(new { Name = GetLocalResourceObject("ActiveGridTitle").ToString(), Count = ACs.Items.Count });
            //b.Add(new { Name = GetLocalResourceObject("OutGridTitle").ToString(), Count = AOs.Items.Count });
            //b.Add(new { Name = GetLocalResourceObject("MissingPunchesGridTitle").ToString(), Count = MPs.Items.Count });
            //b.Add(new { Name = GetLocalResourceObject("LeavesGridTitle").ToString(), Count = Leaves.Items.Count });
            //b.Add(new { Name = GetLocalResourceObject("LatenessGridTitle").ToString(), Count = ALs.Items.Count });


            activeStore.DataSource = ACs.Items;
            activeStore.DataBind();
            activeCount.Text = ACs.Items.Count.ToString();


            //missingPunchesStore.DataSource = MPs.Items;
            //missingPunchesStore.DataBind();

            //leavesStore.DataSource = Leaves.Items;
            //leavesStore.DataBind();

            //latenessStore.DataSource = ALs.Items;
            //latenessStore.DataBind();

            //absenseStore.DataSource = ABs.Items;
            //absenseStore.DataBind();

            //List<ChartData> activeChartData = new List<ChartData>();
            //activeChartData.Add(new ChartData() { name = GetLocalResourceObject("Attendance").ToString(), y = 70, index = 0 });// ACs.Items.Count
            //activeChartData.Add(new ChartData() { name = GetLocalResourceObject("NAttendance").ToString(), y = 100 - 70, index = 1 });//count.result.count - ACs.Items.Count

            //X.Call("drawActiveHightChartPie", JSON.JavaScriptSerialize(activeChartData), rtl ? true : false);


            //List<ChartData> lateChartData = new List<ChartData>();
            //lateChartData.Add(new ChartData() { name = GetLocalResourceObject("Late").ToString(), y =20, index = 0 });//  ALs.Items.Count
            //lateChartData.Add(new ChartData() { name = GetLocalResourceObject("NLate").ToString(), y =50 , index = 1 });//ACs.Items.Count - ALs.Items.Count                                                                                                                                                                 
            //X.Call("drawLateHightChartPie", JSON.JavaScriptSerialize(lateChartData), rtl ? true : false);



            //List<ChartData> breaksChartData = new List<ChartData>();
            //breaksChartData.Add(new ChartData() { name = GetLocalResourceObject("Break").ToString(), y = 20, index = 0 });// 
            //breaksChartData.Add(new ChartData() { name = GetLocalResourceObject("NBreak").ToString(), y = 50, index = 1 });//

            //X.Call("drawBreakHightChartPie", JSON.JavaScriptSerialize(breaksChartData), rtl ? true : false);


            //List<ChartData> leaveChartData = new List<ChartData>();
            //leaveChartData.Add(new ChartData() { name = GetLocalResourceObject("Leaves").ToString(), y = 20, index = 0 });// count.result.count - ACs.Items.Count
            //leaveChartData.Add(new ChartData() { name = GetLocalResourceObject("NLeaves").ToString(), y = 80, index = 1 });//

            //X.Call("drawLeaveHightChartPie", JSON.JavaScriptSerialize(leaveChartData), rtl ? true : false);


            //List<ChartData> paidUnPaidChartData = new List<ChartData>();
            //paidUnPaidChartData.Add(new ChartData() { name = GetLocalResourceObject("PaidLeaves").ToString(), y = 15, index = 0 });// count.result.count - ACs.Items.Count
            //paidUnPaidChartData.Add(new ChartData() { name = GetLocalResourceObject("NPaidLeaves").ToString(), y = 5, index = 1 });//
            //X.Call("drawPaidUnPaidHightChartPie", JSON.JavaScriptSerialize(paidUnPaidChartData), rtl ? true : false);

            //int x = ALs.Items.Count;
            //X.Call("lateChart", x, count.result.count);
            ////int y = ABs.Items.Count;
            ////X.Call("absentChart", y, count.result.count);
            //int z = ACs.Items.Count;
            //X.Call("activeChart", z, count.result.count);
            BindAlerts();
            //BindDepartmentsCount();
        }
        private ListResponse<DashboardItem> FillDashBoardItems()
        {
            try
            {
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();

                string rep_params = vals.Text;
                ReportGenericRequest req2 = new ReportGenericRequest();
                req2.paramString = rep_params;
               ListResponse<DashboardItem> dashoard = _dashBoardService.ChildGetAll<DashboardItem>(req2);
                if (!dashoard.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", dashoard.ErrorCode) != null ? GetGlobalResourceObject("Errors", dashoard.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + dashoard.LogId : dashoard.Summary).Show();
                    return new ListResponse<DashboardItem>();
                }

                //int birth = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.BIRTHDAY).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.BIRTHDAY).First().count : 0;
                //int annev = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.WORK_ANNIVERSARY).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.WORK_ANNIVERSARY).First().count : 0;
                //int comp = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.COMPANY_RIGHT_TO_WORK).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.COMPANY_RIGHT_TO_WORK).First().count : 0;
                //int empRW = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.EMPLOYEE_RIGHT_TO_WORK).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.EMPLOYEE_RIGHT_TO_WORK).First().count : 0;
                //int scr = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.SALARY_CHANGE_DUE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.SALARY_CHANGE_DUE).First().count : 0;
                //int prob = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.END_OF_PROBATION).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.END_OF_PROBATION).First().count : 0;
                //int retirementAge = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.RETIREMENT).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.RETIREMENT).First().count : 0;
                //int TermEndDate = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TERM_END_DATE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TERM_END_DATE).First().count : 0;
                //int EmploymentReviewDate = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.EMPLOYMENT_REVIEW_DATE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.EMPLOYMENT_REVIEW_DATE).First().count : 0;
                //int totalLoans = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.LOANS).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.LOANS).First().count : 0;
                //int vacations = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.VACATIONS).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.VACATIONS).First().count : 0;


                int APPROVAL_TIME = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_TIME).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_TIME).First().count : 0;
                int APPROVAL_LEAVE = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_LEAVE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_LEAVE).First().count : 0;
                int APPROVAL_LOAN = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_LOAN).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_LOAN).First().count : 0;
                int APPROVAL_PENALTY = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_PENALTY).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_PENALTY).First().count : 0;
                int APPROVAL_PURCHASE_ORDER = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_PURCHASE_ORDER).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_PURCHASE_ORDER).First().count : 0;
                int PENDING_PUNCHES = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.PENDING_PUNCHES).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.PENDING_PUNCHES).First().count : 0;
                int APPROVAL_LeaveReturn = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_LeaveReturn).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.APPROVAL_LeaveReturn).First().count : 0;
                //List<DashboardItem> alert = new List<DashboardItem>();
                //alert.Add(new DashboardItem() { itemString=GetLocalResourceObject("Anneversaries").ToString(),count=annev,itemId= ConstDashboardItem.WORK_ANNIVERSARY });
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("Birthdays").ToString(), count = birth, itemId = ConstDashboardItem.BIRTHDAY });
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("ComapnyRightToWork").ToString(), count = comp, itemId = ConstDashboardItem.COMPANY_RIGHT_TO_WORK });
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("SalaryChange").ToString(), count = scr, itemId = ConstDashboardItem.SALARY_CHANGE_DUE });
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("Probation").ToString(), count = prob, itemId = ConstDashboardItem.END_OF_PROBATION});
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("EmployeeRightToWork").ToString(), count = empRW, itemId = ConstDashboardItem.EMPLOYMENT_REVIEW_DATE });
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("TotalLoans").ToString(), count = totalLoans, itemId = ConstDashboardItem.LOANS });
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("EmploymentReviewDate").ToString(), count = EmploymentReviewDate, itemId = ConstDashboardItem.EMPLOYMENT_REVIEW_DATE });
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("TermEndDate").ToString(), count = TermEndDate, itemId = ConstDashboardItem.TERM_END_DATE });
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("retirementAge").ToString(), count = retirementAge, itemId = ConstDashboardItem.RETIREMENT});
                //alert.Add(new DashboardItem() { itemString = GetLocalResourceObject("vacationsLBL").ToString(), count = vacations, itemId = ConstDashboardItem.VACATIONS });
                //alertStore.DataSource = alert;
                //alertStore.DataBind();
                //annversaries.Text = annev.ToString();
                //birthdays.Text = birth.ToString();
                //companyRW.Text = comp.ToString();
                //salaryChange.Text = scr.ToString();
                //probation.Text = prob.ToString();
                //employeeRW.Text = empRW.ToString();
                //totalLoansLbl.Text = totalLoans.ToString();
                //EmploymentReviewDateLbl.Text = EmploymentReviewDate.ToString();
                //termEndDateLBL.Text = TermEndDate.ToString();
                //retirementAgeLBL.Text = retirementAge.ToString();
                //vacationsLBL.Text = vacations.ToString();

                LeavesGrid.Title = GetLocalResourceObject("Leaves").ToString() + " " + (APPROVAL_LEAVE != 0 ? APPROVAL_LEAVE.ToString() : "");
                ApprovalLoanGrid.Title = GetLocalResourceObject("ApprovalLoan").ToString() + " " + (APPROVAL_LOAN != 0 ? APPROVAL_LOAN.ToString() : "");
                TimeGridPanel.Title = GetLocalResourceObject("Time").ToString() + " " + (APPROVAL_TIME != 0 ? APPROVAL_TIME.ToString() : "");
                EmployeePenaltyApprovalGrid.Title = GetLocalResourceObject("EmployeePenaltyApproval").ToString() + " " + (APPROVAL_PENALTY != 0 ? APPROVAL_PENALTY.ToString() : "");
                PurchasesGrid.Title= GetLocalResourceObject("PurchasesApproval").ToString() + " " + (APPROVAL_PURCHASE_ORDER != 0 ? APPROVAL_PURCHASE_ORDER.ToString() : "");
                leveReturnGrid.Title = GetLocalResourceObject("LeaveReturn").ToString() + " " + (APPROVAL_LeaveReturn != 0 ? APPROVAL_LeaveReturn.ToString() : "");
                if (PENDING_PUNCHES != 0 )
                {
                    PunchesGrid.Hidden = false;
                    belowt.ShowTab(PunchesGrid);
                    PunchesGrid.Title = GetLocalResourceObject("PunchesGrid").ToString() + " " + (PENDING_PUNCHES != 0 ? PENDING_PUNCHES.ToString() : "");
                    belowt.ActiveIndex = belowt.ActiveTabIndex==6?5: belowt.ActiveTabIndex;
                }
                else
                {
                    belowt.HideTab(PunchesGrid);
                  if (belowt.ActiveTabIndex==5)
                    belowt.ActiveIndex = 0;
                    //PunchesGrid.Hidden = true;
                }
                    
                return dashoard;
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                return new ListResponse<DashboardItem>();
            }


        }
        private void BindAlerts(bool normalSized = true)
        {
            try
            {
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                ListResponse<DashboardItem> dashoard = FillDashBoardItems();
               

                if (dashoard.count == 0)
                {
                    return;
                }

                List<ChartData> activeChartData = new List<ChartData>();
                activeChartData.Add(new ChartData() { name = GetLocalResourceObject("PENDING").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_PENDING).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_PENDING).First().count : 0, index = 0 });// 10 - Attended
                activeChartData.Add(new ChartData() { name = GetLocalResourceObject("NO_SHOW_UP").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_NO_SHOW_UP).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_NO_SHOW_UP).First().count : 0, index = 1 });// 110 - Vacations
                activeChartData.Add(new ChartData() { name = GetLocalResourceObject("CHECKED").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_CHECKED).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_CHECKED).ToList()[0].count : 0, index = 2 });// 111 - Unpaid leave
            //    activeChartData.Add(new ChartData() { name = GetLocalResourceObject("LEAVE_WITHOUT_EXCUSE").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_LEAVE_WITHOUT_EXCUSE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_LEAVE_WITHOUT_EXCUSE).ToList()[0].count : 0, index = 3 });// 112 - Leave without excuse
                                                                                                                                                                                                                                                                                                                                                          //activeChartData.Add(new ChartData() { name = GetLocalResourceObject("BusinessLeave").ToString(), y = dashoard.Items.Where(x => x.itemId == 113).ToList()[0].count, index =4 });// 113 - business leave
                activeChartData.Add(new ChartData() { name = GetLocalResourceObject("LEAVE").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_LEAVE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_LEAVE).ToList()[0].count : 0, index = 4 });
                activeChartData.Add(new ChartData() { name = GetLocalResourceObject("DAY_OFF").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_DAY_OFF).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_DAY_OFF).ToList()[0].count : 0, index = 5 });


                X.Call("drawActiveHightChartPie", JSON.JavaScriptSerialize(activeChartData), rtl ? true : false, normalSized);


                List<ChartData> lateChartData = new List<ChartData>();
                lateChartData.Add(new ChartData() { name = GetLocalResourceObject("EARLY_CHECKIN").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_EARLY_CHECKIN).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_EARLY_CHECKIN).ToList()[0].count : 0, index = ConstTimeVariationType.EARLY_CHECKIN, });//  ALs.Items.Count
                lateChartData.Add(new ChartData() { name = GetLocalResourceObject("LATE_CHECKIN").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_LATE_CHECKIN).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_LATE_CHECKIN).ToList()[0].count : 0, index = ConstTimeVariationType.LATE_CHECKIN });
                lateChartData.Add(new ChartData() { name = GetLocalResourceObject("DURING_SHIFT_LEAVE").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.DURING_SHIFT_LEAVE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.DURING_SHIFT_LEAVE).ToList()[0].count : 0, index = ConstTimeVariationType.DURING_SHIFT_LEAVE });
                lateChartData.Add(new ChartData() { name = GetLocalResourceObject("EARLY_LEAVE").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_EARLY_LEAVE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_EARLY_LEAVE).ToList()[0].count : 0, index = ConstTimeVariationType.EARLY_LEAVE });
                lateChartData.Add(new ChartData() { name = GetLocalResourceObject("OVERTIME").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_OVERTIME).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_OVERTIME).ToList()[0].count : 0, index = ConstTimeVariationType.OVERTIME });
                X.Call("drawLateHightChartPie", JSON.JavaScriptSerialize(lateChartData), rtl ? true : false, normalSized);



                List<ChartData> breaksChartData = new List<ChartData>();
                breaksChartData.Add(new ChartData() { name = GetLocalResourceObject("PAID_LEAVE").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_PAID_LEAVE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_PAID_LEAVE).ToList()[0].count : 0, index = ConstDashboardItem.TA_PAID_LEAVE });
                breaksChartData.Add(new ChartData() { name = GetLocalResourceObject("UNPAID_LEAVE").ToString(), y = dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_UNPAID_LEAVE).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == ConstDashboardItem.TA_UNPAID_LEAVE).ToList()[0].count : 0, index = ConstDashboardItem.TA_UNPAID_LEAVE });

                X.Call("drawBreakHightChartPie", JSON.JavaScriptSerialize(breaksChartData), rtl ? true : false, normalSized);


                //List<ChartData> leaveChartData = new List<ChartData>();
                //leaveChartData.Add(new ChartData() { name = GetLocalResourceObject("Leaves").ToString(), y = dashoard.Items.Where(x => x.itemId == 13).ToList()[0].count, index = 0 });// count.result.count - ACs.Items.Count
                //leaveChartData.Add(new ChartData() { name = GetLocalResourceObject("NLeaves").ToString(), y = dashoard.Items.Where(x => x.itemId == 10).ToList()[0].count - dashoard.Items.Where(x => x.itemId == 13).ToList()[0].count, index = 1 });//

                //X.Call("drawLeaveHightChartPie", JSON.JavaScriptSerialize(leaveChartData), rtl ? true : false);


                //List<ChartData> paidUnPaidChartData = new List<ChartData>();
                //paidUnPaidChartData.Add(new ChartData() { name = GetLocalResourceObject("PaidLeaves").ToString(), y = 15, index = 0 });// count.result.count - ACs.Items.Count
                //paidUnPaidChartData.Add(new ChartData() { name = GetLocalResourceObject("NPaidLeaves").ToString(), y = 5, index = 1 });//
                //X.Call("drawPaidUnPaidHightChartPie", JSON.JavaScriptSerialize(paidUnPaidChartData), rtl ? true : false);







                //  AbsentLeaveStore.DataSource = objs;
                //  AbsentLeaveStore.DataBind();
            } catch (Exception exp)
            {

                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();




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



        protected void absenseStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                ListResponse<ActiveAbsence> ABs = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
                //List<ActiveAbsence> a = new List<ActiveAbsence>();
                //a.Add(new ActiveAbsence() { branchName = "here", positionName = "someone", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "rabie" } });
                if (!ABs.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", ABs.ErrorCode) != null ? GetGlobalResourceObject("Errors", ABs.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + ABs.LogId : ABs.Summary).Show();
                    return;
                }
                absenseStore.DataSource = ABs.Items;
                absenseStore.DataBind();
                abbsenseCount.Text = "10";
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void latenessStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                ListResponse<ActiveLate> ALs = _timeAttendanceService.ChildGetAll<ActiveLate>(r);
                if (!ALs.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", ALs.ErrorCode) != null ? GetGlobalResourceObject("Errors", ALs.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + ALs.LogId : ALs.Summary).Show();
                    return;
                }
                latenessStore.DataSource = ALs.Items;
                latenessStore.DataBind();
                latensessCount.Text = "7";
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void leavesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                ActiveAttendanceRequest r = GetActiveAttendanceRequest();
                r.DayStatus = 2;
                ListResponse<ActiveLeave> Leaves = _timeAttendanceService.ChildGetAll<ActiveLeave>(r);
                if (!Leaves.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", Leaves.ErrorCode) != null ? GetGlobalResourceObject("Errors", Leaves.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + Leaves.LogId : Leaves.Summary).Show();
                    return;
                }
                leavesStore.DataSource = Leaves.Items;
                //List<ActiveLeave> leaves = new List<ActiveLeave>();
                //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


                leavesStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        //protected void missingPunchesStore_ReadData(object sender, StoreReadDataEventArgs e)
        //{
        //    try
        //    {
        //        ActiveAttendanceRequest r = GetActiveAttendanceRequest();

        //        ListResponse<MissedPunch> ACs = _timeAttendanceService.ChildGetAll<MissedPunch>(r);
        //        if (!ACs.Success)
        //        {
        //            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", ACs.ErrorCode) != null ? GetGlobalResourceObject("Errors", ACs.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + ACs.LogId : ACs.Summary).Show();
        //            return;
        //        }

        //        //List<MissedPunch> s = new List<MissedPunch>();
        //        //s.Add(new MissedPunch() { date = DateTime.Now, employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "issa" }, missedIn = true, missedOut = false, recordId = "1", time = "08:30" });
        //        missingPunchesStore.DataSource = ACs.Items;
        //        missingPunchesStore.DataBind();
        //        mpCount.Text = "6";
        //    }
        //    catch (Exception exp)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //    }
        //}

        //protected void checkMontierStore_ReadData(object sender, StoreReadDataEventArgs e)
        //{
        //    try
        //    {
        //        ActiveAttendanceRequest r = GetActiveAttendanceRequest();

        //        ListResponse<CheckMonitor> CMs = _timeAttendanceService.ChildGetAll<CheckMonitor>(r);
        //        if (!CMs.Success)
        //        {
        //            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", CMs.ErrorCode) != null ? GetGlobalResourceObject("Errors", CMs.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + CMs.LogId : CMs.Summary).Show();
        //            return;
        //        }
        //        foreach (var item in CMs.Items)
        //        {
        //            item.figureTitle = GetLocalResourceObject(item.figureId.ToString()).ToString();
        //            item.rate = item.rate / 100;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //    }
        //    //checkMontierStore.DataSource = CMs.Items;
        //    //checkMontierStore.DataBind();

        //}

        protected void OutStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                ListResponse<ActiveOut> AOs = _timeAttendanceService.ChildGetAll<ActiveOut>(r);
                if (!AOs.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", AOs.ErrorCode) != null ? GetGlobalResourceObject("Errors", AOs.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + AOs.LogId : AOs.Summary).Show();
                    return;
                }
                OutStore.DataSource = AOs.Items;


                OutStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
        [DirectMethod]
        public void RefreshAll()
        {
            try
            {
                ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                ListResponse<ActiveAbsence> ABs = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
                if (!ABs.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", ABs.ErrorCode) != null ? GetGlobalResourceObject("Errors", ABs.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + ABs.LogId : ABs.Summary).Show();
                    return;
                }
                absenseStore.DataSource = ABs.Items;
                absenseStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        private ActiveAttendanceRequest GetActiveAttendanceRequest()
        {
           
            Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);


            ActiveAttendanceRequest req = new ActiveAttendanceRequest();

            req.BranchId = parameters.ContainsKey("branchId") ? parameters["branchId"].ToString() : "0";
            req.DepartmentId = parameters.ContainsKey("departmentId") ? parameters["branchId"].ToString() : "0";
            req.DivisionId = parameters.ContainsKey("divisionId") ? parameters["branchId"].ToString() : "0";
            req.PositionId = parameters.ContainsKey("positionId") ? parameters["branchId"].ToString() : "0";
            req.StatusId = parameters.ContainsKey("esId") ? parameters["esId"].ToString() : "0";
            req.StartAt = "0";
            req.Size = "1000";
       

            return req;
        }

        private EmployeeCountRequest GetEmployeeCountRequest()

        {
            try
            {
                Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);
                EmployeeCountRequest req = new EmployeeCountRequest();
                req.BranchId = parameters.ContainsKey("branchId") ? parameters["branchId"].ToString() : "0";
                req.DepartmentId = parameters.ContainsKey("departmentId") ? parameters["branchId"].ToString() : "0";
                req.DivisionId = parameters.ContainsKey("divisionId") ? parameters["branchId"].ToString() : "0";
                req.PositionId = parameters.ContainsKey("positionId") ? parameters["branchId"].ToString() : "0";
                req.StatusId = parameters.ContainsKey("esId") ? parameters["esId"].ToString() : "0";







                return req;
            }
            catch(Exception exp)
            {
                X.MessageBox.Alert(Resources.Common.Error, exp.Message).Show();
                return new EmployeeCountRequest();
            }
        }

        //protected void OverDueStore_ReadData(object sender, StoreReadDataEventArgs e)
        //{
        //    try
        //    {
        //        TaskManagementListRequest req = GetTaskManagementRequest();
        //        ListResponse<Model.TaskManagement.Task> resp = _taskManagementService.GetAll<Model.TaskManagement.Task>(req);
        //        if (!resp.Success)
        //        {
        //            Common.errorMessage(resp);
        //            return;
        //        }
        //        List<Model.TaskManagement.Task> today = resp.Items.Where(x => x.dueDate == DateTime.Today && !x.completed).ToList();
        //        List<Model.TaskManagement.Task> late = resp.Items.Where(x => x.dueDate < DateTime.Today && !x.completed).ToList();
        //        int count = resp.Items.Count(x => !x.completed);

        //        OverDueStore.DataSource = late;
        //        OverDueStore.DataBind();
        //        DueTodayStore.DataSource = today;
        //        DueTodayStore.DataBind();
        //        int overDue = late.Count;
        //        int todays = today.Count;
        //        X.Call("chart3", todays, count);
        //        X.Call("chart2", overDue, count);
        //    }
        //    catch (Exception exp)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //    }

        //}



        //private TaskManagementListRequest GetTaskManagementRequest()
        //{
        //    TaskManagementListRequest req = new TaskManagementListRequest();


        //    var d = jobInfo1.GetJobInfo();
        //    req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
        //    req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;



        //    req.DivisionId = 0;


        //    req.Size = "30";
        //    req.StartAt = "0";
        //    req.Filter = "";
        //    req.SortBy = "dueDate";
        //    req.InRelationToId = 0;
        //    req.AssignToId = 0;
        //    req.Completed = 0;

        //    return req;
        //}

        private LoanManagementListRequest GetLoanManagementRequest()
        {
            Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);
            LoanManagementListRequest req = new LoanManagementListRequest();

            req.BranchId = parameters.ContainsKey("branchId") ? parameters["branchId"].ToString() : "0";
            req.DepartmentId = parameters.ContainsKey("departmentId") ? parameters["branchId"].ToString() : "0";
            req.DivisionId = parameters.ContainsKey("divisionId") ? parameters["branchId"].ToString() : "0";
            req.PositionId = parameters.ContainsKey("positionId") ? parameters["branchId"].ToString() : "0";


            req.Size = "30";
            req.StartAt = "0";
            req.Filter = "";
            req.SortBy = "recordId";
            req.Status = "2";


            return req;
        }

        //private LeaveRequestListRequest GetLeaveManagementRequest()
        //{
        //    LeaveRequestListRequest req = new LeaveRequestListRequest();

        //    var d = jobInfo1.GetJobInfo();
        //    req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
        //    req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
        //    RecordRequest r = new RecordRequest();
        //    r.RecordID = _systemService.SessionHelper.GetCurrentUserId();
        //    RecordResponse<UserInfo> response = _systemService.ChildGetRecord<UserInfo>(r);
        //    if (response.result == null)
        //        return null;

        //    req.raEmployeeId = Convert.ToInt32(response.result.employeeId);
        //    if (string.IsNullOrEmpty(response.result.employeeId))
        //        return null;
        //    userSessionEmployeeId.Text = response.result.employeeId;
        //    req.status = 1;
        //    if (!string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
        //        req.ApproverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());



        //    req.Size = "30";
        //    req.StartAt = "0";
        //    req.Filter = "";
        //    req.SortBy = "recordId";


        //    return req;
        //}

        private DashboardRequest GetDashboardRequest()
        {
            Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);
            DashboardRequest req = new DashboardRequest();

            
          
            req.BranchId = parameters.ContainsKey("2") ? parameters["2"] : "0";
            req.DepartmentId = parameters.ContainsKey("3") ? parameters["3"] : "0";
            req.DivisionId = parameters.ContainsKey("4") ? parameters["4"] : "0";
            req.PositionId = parameters.ContainsKey("1") ? parameters["1"] : "0";
            req.EsId = parameters.ContainsKey("5") ? parameters["5"] : "0";




            return req;
        }

        protected void RefreshAllGrid(object sender, EventArgs e)
        {
            alertStore.Reload();
            branchAvailabilityStore.Reload();
            BindAlerts();
            LeaveRequestsStore.Reload();
        }

        protected void attendanceChartStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<object> b = new List<object>();
            b.Add(new { Name = "Active", Count = activeCount.Text });
            b.Add(new { Name = "Out", Count = outCount.Text });
            b.Add(new { Name = "Missing Punches", Count = mpCount.Text });
            b.Add(new { Name = "Leave", Count = leaveCount.Text });
            b.Add(new { Name = "Late", Count = latensessCount.Text });

        }




        //protected void LoansStore_ReadData(object sender, StoreReadDataEventArgs e)
        //{
        //    try
        //    {
        //        LoanManagementListRequest req = GetLoanManagementRequest();
        //        req.Status = 1;
        //        ListResponse<Loan> resp = _loanService.GetAll<Loan>(req);
        //        //List<Loan> OpenLoans = loans.Items.Where(t => t.status == 1).ToList();

        //        if (!resp.Success)
        //        {
        //            Common.errorMessage(resp);
        //            return;
        //        }

        //        LoansStore.DataSource = resp.Items;
        //        LoansStore.DataBind();
        //        int x = resp.count;
        //        X.Call("loansChart", x, x + (10 - (x % 10)));
        //    }
        //    catch (Exception exp)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //    }
        //}

        protected void LeaveRequestsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                
                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    return;

                string rep_params = "";
                Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
                if (parameters.ContainsKey("5"))
                    parameters.ChangeKey("5", "7");
                if (parameters.ContainsKey("4"))
                    parameters.ChangeKey("4", "6");
                if (parameters.ContainsKey("2"))
                    parameters.ChangeKey("2", "5");
                if (parameters.ContainsKey("1"))
                parameters.ChangeKey("1", "4");





              //  parameters.Add("1", "1");
              //  parameters.Add("8", "0");
                parameters.Add("2", _systemService.SessionHelper.GetEmployeeId().ToString());

                parameters.Add("1", "1");
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value+"^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params= rep_params.Remove(rep_params.Length - 1);
                }





              

                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse<AionHR.Model.LeaveManagement.PendingLA> resp = _leaveManagementService.ChildGetAll<AionHR.Model.LeaveManagement.PendingLA>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                if (resp.Items != null)
                {
                    LeaveRequestsStore.DataSource = resp.Items;
                    LeaveRequestsStore.DataBind();
                }
              
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        protected void leaveReturnStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    return;

                string rep_params = "";
                Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
                if (parameters.ContainsKey("5"))
                    parameters.ChangeKey("5", "7");
                if (parameters.ContainsKey("4"))
                    parameters.ChangeKey("4", "6");
                if (parameters.ContainsKey("2"))
                    parameters.ChangeKey("2", "5");
                if (parameters.ContainsKey("1"))
                    parameters.ChangeKey("1", "4");





                //  parameters.Add("1", "1");
                //  parameters.Add("8", "0");
                parameters.Add("2", _systemService.SessionHelper.GetEmployeeId().ToString());

                parameters.Add("1", "1");
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params = rep_params.Remove(rep_params.Length - 1);
                }







                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse<AionHR.Model.LeaveManagement.pendingRA> resp = _leaveManagementService.ChildGetAll<AionHR.Model.LeaveManagement.pendingRA>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                if (resp.Items != null)
                {
                    leaveReturnStore.DataSource = resp.Items;
                    leaveReturnStore.DataBind();
                }

            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }


        protected void BirthdaysStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse<EmployeeBirthday> resp = _dashBoardService.ChildGetAll<EmployeeBirthday>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                resp.Items.ForEach(x => x.birthDateString = x.birthDate.ToString(_systemService.SessionHelper.GetDateformat()));
                BirthdaysStore.DataSource = resp.Items;
                BirthdaysStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void AnniversaryStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse<WorkAnniversary> resp = _dashBoardService.ChildGetAll<WorkAnniversary>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                resp.Items.ForEach(x => x.hireDateString = x.hireDate.ToString(_systemService.SessionHelper.GetDateformat()));
                AnniversaryStore.DataSource = resp.Items;
                AnniversaryStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void CompanyRightToWorkStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse <CompanyRTW> resp = _dashBoardService.ChildGetAll<CompanyRTW>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                resp.Items.ForEach(x => x.expiryDateString = x.expiryDate.ToString(_systemService.SessionHelper.GetDateformat()));
                CompanyRightToWorkStore.DataSource = resp.Items;
                CompanyRightToWorkStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }

        protected void EmployeeRightToWorkStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse<EmpRTW> resp = _dashBoardService.ChildGetAll<EmpRTW>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                EmployeeRightToWorkStore.DataSource = resp.Items;
                EmployeeRightToWorkStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void SCRStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse<SalaryChange> resp = _dashBoardService.ChildGetAll<SalaryChange>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                resp.Items.ForEach(x => x.effectiveDateString = x.effectiveDate.ToString(_systemService.SessionHelper.GetDateformat()));
                SCRStore.DataSource = resp.Items;
                SCRStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void ProbationStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse<ProbationEnd> resp = _dashBoardService.ChildGetAll<ProbationEnd>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                resp.Items.ForEach(x => x.probationEndDateString = x.probationEndDate.ToString(_systemService.SessionHelper.GetDateformat()));
                ProbationStore.DataSource = resp.Items;
                ProbationStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }


        protected void departments2Count_ReadData(object sender, StoreReadDataEventArgs e)
        {
            BindDepartmentsCount();
        }

        private void BindDepartmentsCount(bool normalSized = true)
        {
            try
            {
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                ListRequest req = new ListRequest();
                ListResponse<DepartmentActivity> resp = _systemService.ChildGetAll<DepartmentActivity>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }

                List<string> listCategories = resp.Items.Select(a => a.departmentName).ToList();
                List<int> listIn = resp.Items.Select(a => a.checkedIn).ToList();
                List<int> listOut = resp.Items.Select(a => a.checkedOut).ToList();


                // Store1.DataSource = resp.Items;
                // Store1.DataBind();
                X.Call("drawDepartmentsCountHightChartColumn", GetLocalResourceObject("In").ToString(), GetLocalResourceObject("Out").ToString(), JSON.JavaScriptSerialize(listIn), JSON.JavaScriptSerialize(listOut), JSON.JavaScriptSerialize(listCategories), rtl ? true : false, normalSized);
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
        private void BindAttendancePeriod(bool normalSized = true)
        {
            try
            {
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                string rep_params = vals.Text;
                TimeBoundedActiveAttendanceRequest req = new TimeBoundedActiveAttendanceRequest();
                try
                {
                    req.startingDayId = periodToDate.SelectedDate.ToString("yyyyMMdd");
                }
                catch
                {
                    req.startingDayId = DateTime.Now.ToString("yyyyMMdd");


                }
                
                req.paramString = rep_params;

                ListResponse<AttendancePeriod> resp = _dashBoardService.ChildGetAll<AttendancePeriod>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }


                List<int> listIn1 = resp.Items.Select(a => a.d_cnt).ToList();
                List<int> listIn2 = resp.Items.Select(a => a.l_cnt).ToList();
                List<int> listIn3 = resp.Items.Select(a => a.m_cnt).ToList();
                List<int> listIn4 = resp.Items.Select(a => a.p_cnt).ToList();
                List<string> cats = new List<string>();
                foreach (string s in resp.Items.Select(a => a.dayId))
                {
                    DateTime d = DateTime.ParseExact(s, "yyyyMMdd", new CultureInfo("en"));
                    cats.Add(GetGlobalResourceObject("Common", Enum.GetName(typeof(DayOfWeek), d.DayOfWeek) + "Text").ToString() + " - " + d.ToString("MM-dd") + "  ");

                }


                // Store1.DataSource = resp.Items;
                // Store1.DataBind();
                X.Call("drawAttendancePeriodChart", JSON.JavaScriptSerialize(cats), GetLocalResourceObject("Absent").ToString(), GetLocalResourceObject("Late").ToString(),


                    GetLocalResourceObject("MissingPunchesGridTitle").ToString(), GetLocalResourceObject("Attendance").ToString()
                    , JSON.JavaScriptSerialize(listIn1), JSON.JavaScriptSerialize(listIn2), JSON.JavaScriptSerialize(listIn3), JSON.JavaScriptSerialize(listIn4)
                    , rtl ? true : false, normalSized ? true : false);
            }
            catch (Exception Exp)
            {
                X.Msg.Alert(Resources.Common.Error, Exp.Message).Show();
            }
        }

        //private TimeBoundedActiveAttendanceRequest GetTimeBoundedActiveAttendanceRequest()
        //{
        //    ActiveAttendanceRequest re = GetActiveAttendanceRequest();
        //    TimeBoundedActiveAttendanceRequest b = new TimeBoundedActiveAttendanceRequest();
        //    b.DepartmentId = re.DepartmentId;
        //    b.BranchId = re.BranchId;
        //    b.DayStatus = re.DayStatus;
        //    b.DivisionId = re.DivisionId;
        //    b.PositionId = re.PositionId;

        //    try
        //    {
        //        b.startingDayId = periodToDate.SelectedDate.ToString("yyyyMMdd");
        //    }
        //    catch
        //    {
        //        b.startingDayId = DateTime.Now.ToString("yyyyMMdd");


        //    }
        //    return b;
        //}

        protected void CompletedLoansStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                LoanManagementListRequest req = new LoanManagementListRequest();
                req = GetLoanManagementRequest();

                ListResponse<Loan> resp = _loanService.GetAll<Loan>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                CompletedLoansStore.DataSource = resp.Items;
                CompletedLoansStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void totalLoansStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                LoanManagementListRequest req = new LoanManagementListRequest();
                req = GetLoanManagementRequest();
                req.Status = "2";
                req.LoanId = "0";
                ListResponse<Loan> resp = _loanService.GetAll<Loan>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                totalLoansStore.DataSource = resp.Items;
                totalLoansStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void OnLeaveStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

        }

        protected void UnpaidLeavesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                ActiveAttendanceRequest r = GetActiveAttendanceRequest();
                r.DayStatus = 4;
                ListResponse<ActiveLeave> resp = _timeAttendanceService.ChildGetAll<ActiveLeave>(r);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                UnpaidLeavesStore.DataSource = resp.Items;
                //List<ActiveLeave> leaves = new List<ActiveLeave>();
                //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


                UnpaidLeavesStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }



        protected void ApprovaLoan_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    return;

                string rep_params = "";
                Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
                if (parameters.ContainsKey("5"))
                    parameters.ChangeKey("5", "7");
                if (parameters.ContainsKey("4"))
                    parameters.ChangeKey("4", "6");
                if (parameters.ContainsKey("2"))
                    parameters.ChangeKey("2", "5");
                if (parameters.ContainsKey("1"))
                    parameters.ChangeKey("1", "4");





              //  parameters.Add("1", "1");
              
                parameters.Add("2", _systemService.SessionHelper.GetEmployeeId().ToString());
                parameters.Add("1", "1");


                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params = rep_params.Remove(rep_params.Length - 1);
                }
                                                                      
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                ListResponse<AionHR.Model.LoadTracking.PendingLA> resp = _loanService.ChildGetAll<AionHR.Model.LoadTracking.PendingLA>(req);

                                                                         
                if (!resp.Success)
                {
                     Common.errorMessage(resp);
                    return;
                }
                resp.Items.ForEach(x =>
                {

                    x.statusString = FillApprovalStatus(x.status);
                }
                  );
                this.ApprovalLoanStore.DataSource = resp.Items;
                e.Total = resp.count;

                this.ApprovalLoanStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        
              protected void punchesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {


              
                string rep_params = "";
        
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("1", ppType.SelectedItem.Value == null ? "0" : ppType.SelectedItem.Value.ToString());
                parameters.Add("2", udid.SelectedItem.Value == null ? "0" : udid.SelectedItem.Value.ToString());
                if(fromDate.SelectedDate.ToString("yyyyMMdd")!="00010101")
                parameters.Add("3", fromDate.SelectedDate.ToString("yyyyMMdd"));
                if (toDate.SelectedDate.ToString("yyyyMMdd") != "00010101")
                    parameters.Add("4", toDate.SelectedDate.ToString("yyyyMMdd"));





                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params = rep_params.Remove(rep_params.Length - 1);
                }



                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;

                ListResponse<PendingPunch> punches = _timeAttendanceService.ChildGetAll<PendingPunch>(req);
                if (!punches.Success)
                {
                    Common.errorMessage(punches);
                    return;
                }
               
                //punches.Items.ForEach(x=>
                //{
                //    if (x.ppType != null )
                //        x.ppTypeString = GetLocalResourceObject("ppType" + x.ppType).ToString();
                //}
                //    );
            
                punchesStore.DataSource = punches.Items;
                if (punches.Items != null)
                    PunchesGrid.Title = GetLocalResourceObject("PunchesGrid").ToString() + " " + (punches.Items.Count != 0 ? punches.Items.Count.ToString() : "");
                else
                    PunchesGrid.Title = GetLocalResourceObject("PunchesGrid").ToString();
                punchesStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        private string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        protected void TimeStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {


                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    return;

                string rep_params = "";
                Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
                if (parameters.ContainsKey("2"))
                    parameters.ChangeKey("2", "8");
                if (parameters.ContainsKey("3"))
                    parameters.ChangeKey("3", "9");
                if (parameters.ContainsKey("5"))
                    parameters.ChangeKey("5", "10");
                if (parameters.ContainsKey("1"))
                    parameters.Remove("1");
                if (parameters.ContainsKey("4"))
                    parameters.Remove("4");


             //   parameters.Add("1", "0");

                parameters.Add("6", _systemService.SessionHelper.GetEmployeeId());
                parameters.Add("5", timeVariationType.GetTimeCode());
                //    parameters.Add("4", "0");
                //  parameters.Add("7", "1");
                parameters.Add("7", "1");




                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params = rep_params.Remove(rep_params.Length - 1);
                }



                //DashboardRequest req = GetDashboardRequest();
                //DashboardTimeListRequest r = new DashboardTimeListRequest();
                //r.dayId = "";
                //r.employeeId = 0;
                //if (!string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                //    r.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());

                //else
                //{
                //    TimeStore.DataSource = new List<Time>();
                //    TimeStore.DataBind();
                //    return;
                //}
                //r.timeCode = timeVariationType.GetTimeCode(); 
                //r.shiftId = "0";
                //r.apStatus = "1";
                //r.BranchId = req.BranchId;
                //r.DivisionId = req.DivisionId;
                //r.PositionId = req.PositionId;
                //r.DepartmentId = req.DepartmentId;
                //r.EsId = req.EsId;
                //r.Size = "50";
                //r.StartAt = "0";

                PendingTimeApprovalListRequest r = new PendingTimeApprovalListRequest();
                r.StartAt = e.Start.ToString();
                r.Size = e.Limit.ToString();
                r.sortBy = "employeeName";
                r.paramString = rep_params;
                ListResponse<PendingTA> Times = _timeAttendanceService.ChildGetAll<PendingTA>(r);
                if (!Times.Success)
                {
                    Common.errorMessage(Times);
                    return;
                }

                List<XMLDictionary> timeCode = ConstTimeVariationType.TimeCodeList(_systemService);
                int currentTimeCode;
                Times.Items.ForEach(x =>
                {
                    x.employeeName = x.employeeName.Replace(". ", ".");
                    if (string.IsNullOrEmpty(x.shiftId))
                        x.shiftId = " ";
                  
                    x.statusString = FillApprovalStatus(x.status);

                    if (Int32.TryParse(x.timeCode, out currentTimeCode))
                    {
                        x.timeCodeString = timeCode.Where(y => y.key == Convert.ToInt32(x.timeCode)).Count() != 0 ? timeCode.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;
                    }
                    if (string.IsNullOrEmpty(x.notes))
                        x.notes = " ";
                });

                // TimeStore.DataSource = Times.Items.Where(x=>x.status==1).ToList<Time>();
                ////List<ActiveLeave> leaves = new List<ActiveLeave>();
                //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });
                
                TimeStore.DataSource = Times.Items;
                e.Total = Times.count;
                TimeStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }


        protected void LocalRateStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            int employeeCount = 0, nationalCount = 0, netNationalCount = 0;
            string inNameValue="", bsNameValue="", leNameValue="";
        double rate = 0, netRate = 0, minLERate = 0, minNextLERate = 0;

            try
            {
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                DashboardRequest req1 = GetDashboardRequest();
                ActiveAttendanceRecordRequest req = new ActiveAttendanceRecordRequest();
                req.BranchId = req1.BranchId;
              
                req.DepartmentId = req1.DepartmentId;
                req.DivisionId = req1.DivisionId;
                req.PositionId = req1.PositionId;
                req.StatusId = req1.EsId; 
                RecordResponse <LocalsRate> resp = _helpFunctionService.ChildGetRecord<LocalsRate>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                if (resp.result != null)
                {
                    inNameValue = resp.result.inName;
                    bsNameValue = resp.result.bsName;
                    leNameValue = resp.result.leName;
                    netRate = resp.result.netRate;
                    rate = resp.result.rate;
                    minLERate = resp.result.minLERate;
                    minNextLERate = resp.result.minNextLERate;
                    employeeCount = resp.result.employeeCount;
                    nationalCount = resp.result.nationalCount;
                    netNationalCount = resp.result.netNationalCount;
                }


                inName.Text = inNameValue;
                bsName.Text = bsNameValue;
                leName.Text = leNameValue;

                List<object> RateObjs = new List<object>();
                RateObjs.Add(new { category = GetLocalResourceObject("netRate").ToString(), number =netRate  });
                RateObjs.Add(new { category = GetLocalResourceObject("rate").ToString(), number = rate});
                RateObjs.Add(new { category = GetLocalResourceObject("minLERate").ToString(), number =minLERate });
                RateObjs.Add(new { category = GetLocalResourceObject("minNextLERate").ToString(), number =minNextLERate  });


                List<string> listCategories = new List<string>() { GetLocalResourceObject("rate").ToString(), GetLocalResourceObject("minLERate").ToString(), GetLocalResourceObject("netRate").ToString(), GetLocalResourceObject("minNextLERate").ToString() };
                List<double> listValues = new List<double>() { rate, minLERate, netRate, minNextLERate };

                X.Call("drawMinLocalRateCountHightChartColumn", JSON.JavaScriptSerialize(listValues), JSON.JavaScriptSerialize(listCategories), rtl ? true : false);




                List<object> CountObjs = new List<object>();
                CountObjs.Add(new { category = GetLocalResourceObject("employeeCount").ToString(), number = employeeCount });//here 
                CountObjs.Add(new { category = GetLocalResourceObject("nationalCount").ToString(), number = nationalCount });//here
                CountObjs.Add(new { category = GetLocalResourceObject("netNationalCount").ToString(), number = netNationalCount });


                List<string> listCount = new List<string>() { GetLocalResourceObject("employeeCount").ToString(), GetLocalResourceObject("nationalCount").ToString(), GetLocalResourceObject("netNationalCount").ToString() };
                List<double> listempValues = new List<double>() { employeeCount, nationalCount, netNationalCount };

                X.Call("drawLocalCountHightChartColumn", JSON.JavaScriptSerialize(listempValues), JSON.JavaScriptSerialize(listCount), rtl ? true : false);

            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
        [DirectMethod]
        protected void POPUPPendingPunches(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    //We are call a direct request metho for deleting a record
                    Handler = String.Format("App.direct.DeletePendingPunchRecord({0})", id),
                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Text = Resources.Common.No
                }

            }).Show();
           
        }
        [DirectMethod]
        public void DeletePendingPunchRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                PendingPunch n = new PendingPunch();
                n.recordId = index;
                n.clockStamp = new DateTime();
                n.employeeRef = "";
                n.serialNo = "0";
                n.udId = "";

                PostRequest<PendingPunch> req = new PostRequest<PendingPunch>();
                req.entity = n;
                PostResponse<PendingPunch> res = _timeAttendanceService.ChildDelete<PendingPunch>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(res);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    punchesStore.Reload();
                    BindAlerts();
                   
                    //Step 3 : Showing a notification for the user 
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordDeletedSucc
                    });
                }

            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }

        [DirectMethod]
        protected void TimePoPUP(object sender, DirectEventArgs e)
        {
            try
            {
                TabPanel1.ActiveIndex = 0;



                currentSeqNo.Text = e.ExtraParams["seqNo"];
                string tvIdParameter = e.ExtraParams["tvId"];




                TimeApprovalRecordRequest r = new TimeApprovalRecordRequest();
                r.seqNo = currentSeqNo.Text;

                r.tvId = tvIdParameter;


                RecordResponse<Time> response = _timeAttendanceService.ChildGetRecord<Time>(r);
                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(response);
                    return;
                }
                if (response.result != null)
                {
                    TimeApprovalStatusControl.setApprovalStatus(response.result.status.ToString());
                    if (response.result.damageLevel == "1")
                    {
                        response.result.damageLevel = GetLocalResourceObject("DamageWITHOUT_DAMAGE").ToString();
                        damageLevel.Text = GetLocalResourceObject("DamageWITHOUT_DAMAGE").ToString();
                    }
                    else
                    {
                        response.result.damageLevel = GetLocalResourceObject("DamageWITH_DAMAGE").ToString();
                        damageLevel.Text = GetLocalResourceObject("DamageWITH_DAMAGE").ToString();
                    }

                  


                    if (!string.IsNullOrEmpty(response.result.shiftStart))
                        response.result.shiftStart = response.result.shiftStart;
                    if (!string.IsNullOrEmpty(response.result.shiftEnd))
                        response.result.shiftStart += " " + response.result.shiftEnd;



                

                    // clockDuration.Text = response.result.clockDuration + " " + response.result.duration;
                    List<XMLDictionary> timeCode = ConstTimeVariationType.TimeCodeList(_systemService);


                    //clockDuration.Text = response.result.clockDuration;
                    //duration.Text = response.result.duration;
                    //TimeEmployeeName.Text = response.result.employeeName;
                    //TimedayIdDate.SelectedDate = (DateTime)response.result.date;
                    response.result.timeCodeString = timeCode.Where(y => y.key == Convert.ToInt32(response.result.timeCode)).Count() != 0 ? timeCode.Where(y => y.key == Convert.ToInt32(response.result.timeCode)).First().value : string.Empty; ;
                    TimeApprovalStatusControl.setApprovalStatus(response.result.status.ToString());
                    //shiftIdTF.Text = string.IsNullOrEmpty(response.result.shiftId) ? "" : response.result.shiftId;
                    //TimeNotes.Text = response.result.notes;
                    //justification.Text = response.result.justification;
                    tvId.Text = tvIdParameter;
                    // helpText.Text = response.result.helpText;
                    TimeFormPanel.SetValues(response.result);
                    FillTimeApproval(tvIdParameter);
                }
                this.TimeWindow.Title = Resources.Common.EditWindowsTitle;
                this.TimeWindow.Show();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();

            }
        }
        [DirectMethod]
        protected void PurchasesApprovalPoPUP(object sender, DirectEventArgs e)
        {
            //TabPanel1.ActiveIndex = 0;
            string poIdParameter = e.ExtraParams["poIdParameter"];
            string qtyParameter = e.ExtraParams["qtyParameter"];
            string statusParameter = e.ExtraParams["statusParameter"];
            string department = e.ExtraParams["department"];
            string branch = e.ExtraParams["branchName"];
            string category = e.ExtraParams["categoryName"];
            string commentsParameter = e.ExtraParams["comments"];
            string arIdParameter = e.ExtraParams["arId"];
            currentSeqNo.Text = e.ExtraParams["seqNo"];
            PurchaseApprovalStatusControl.setApprovalStatus(statusParameter);
            PurchasApprovalReasonControl.setApprovalReason(arIdParameter);


            //TimeApprovalRecordRequest r = new TimeApprovalRecordRequest();
            //r.approverId = _systemService.SessionHelper.GetEmployeeId().ToString();
            //r.employeeId = employeeId;
            //r.dayId = dayId;
            //r.timeCode = timeCode;
            //r.shiftId = shiftId;
            //RecordResponse<Time> response = _timeAttendanceService.ChildGetRecord<Time>(r);
            //if (!response.Success)
            //{
            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //    Common.errorMessage(response);
            //    return;
            //}
            //TimeStatus.Select(response.result.status.ToString());
            //if (response.result.damageLevel == "1")
            //    response.result.damageLevel = GetLocalResourceObject("DamageWITHOUT_DAMAGE").ToString();
            //else
            //    response.result.damageLevel = GetLocalResourceObject("DamageWITH_DAMAGE").ToString();

            //TimeFormPanel.SetValues(response.result);


            departmentName.Text = department;
            branchName.Text = branch;
            categoryName.Text = category;

            qty.Text = qtyParameter;
            comments.Text = commentsParameter;
            poId.Text = poIdParameter;


            //FillTimeApproval(Convert.ToInt32(dayId), Convert.ToInt32(employeeId), timeCode, shiftId, status);

            this.purchaseApprovalWindow.Title = Resources.Common.EditWindowsTitle;
            this.purchaseApprovalWindow.Show();

        }
        [DirectMethod]
        protected void ApprovalLoanPoPUP(object sender, DirectEventArgs e)
        {
            try
            {
                string id = e.ExtraParams["id"];
                string employeeId = e.ExtraParams["employeeId"];
                string arId = e.ExtraParams["arId"];
                currentSeqNo.Text = e.ExtraParams["seqNo"];
                string status = e.ExtraParams["status"];
                
                //SetTabPanelEnable(true);





                //Step 1 : get the object from the Web Service 
                RecordRequest r = new RecordRequest();
                r.RecordID = id;

                RecordResponse<Loan> response = _loanService.Get<Loan>(r);
                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(response);
                    return;
                }







                //    effectiveDate.Disabled = response.result.status != 3;
                //FillFilesStore(Convert.ToInt32(id));

                //Step 2 : call setvalues with the retrieved object
                this.ApprovalLoanForm.SetValues(response.result);
                LoanApprovalStatusControl.setApprovalStatus(status);
                LoanApprovalReasonControl.setApprovalReason(arId);

                ApprovalLoanEmployeeName.Text = response.result.employeeName.ToString();



                //if (!string.IsNullOrEmpty(response.result.branchId))
                //    branchId.Select(response.result.branchId);

                //if (!response.result.effectiveDate.HasValue)
                //    effectiveDate.SelectedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(employeeId))
                FillApprovalLoan(Convert.ToInt32(employeeId),id);
                this.approvalLoanWindow.Title = Resources.Common.EditWindowsTitle;
                this.approvalLoanWindow.Show();







            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        
             [DirectMethod]
        protected void alertPoPUP(object sender, DirectEventArgs e)
        {
      
                string id = e.ExtraParams["id"];
                switch (id)
                {
                    case "8":
                        AnniversaryStore.Reload();
                        anniversaryWindow.Show();
                        break;
                    case "3":
                        ProbationStore.Reload();
                        ProbationWindow.Show();
                        break;
                    case "7":
                        TermEndDateStore.Reload();
                        TermEndDateWindow.Show();
                        break;
                    case "5":
                        EmploymentReviewDateStore.Reload();
                        EmploymentReviewDateWindow.Show();

                        break;
                    case "2":
                        EmployeeRightToWorkStore.Reload();
                        EmployeeRightToWorkWindow.Show();
                        break;
                    case "9":
                        BirthdaysStore.Reload();
                        BirthdaysWindow.Show();
                        break;
                    case "4":
                        SCRStore.Reload();
                        SCRWindow.Show();
                        break;
                    case "6":
                        retirementAgeStore.Reload();
                        retirementAgeWindow.Show();
                        break;
                    //case "10":
                    //    totalLoansStore.Reload();
                    //    totalLoansWindow.Show();
                    //    break;
                    case "10":
                    CurrentItemId.Text = "10";
                        LeaveingSoonStore.Reload();
                    LeaveingSoonWindow.Title = GetLocalResourceObject("Vacation").ToString();
                    LeaveingSoonWindow.Show();
                        break;
                    case "11":
                    CurrentItemId.Text = "11";
                    LeaveingSoonStore.Reload();
                    LeaveingSoonWindow.Title = GetLocalResourceObject("LeaveReturn").ToString();
                    LeaveingSoonWindow.Show();
                    break;
                case "1":
                        CompanyRightToWorkStore.Reload();
                        CompanyRightToWorkWindow.Show();
                        break;


                }
            

        }
        
        [DirectMethod]
        protected void leavePoPUP(object sender, DirectEventArgs e)
        {
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string arId= e.ExtraParams["arId"];
            string seqNoParamter = e.ExtraParams["seqNo"];
            string status = e.ExtraParams["status"];

           
            switch (type)
            {



                case "imgAttach":
                    //Step 1 : get the object from the Web Service 

                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;


                    RecordResponse<LeaveRequest> response = _leaveManagementService.ChildGetRecord<LeaveRequest>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object

                    this.LeaveRecordForm.SetValues(response.result);
                    leaveReqApprove.Text = response.result.recordId.ToString();
                    employeeName.Text = response.result.employeeName;
                    LeaveApprovalReasonControl.setApprovalReason(arId);
                    seqNo.Text = seqNoParamter;
                    LeveApprovalStatusControl.setApprovalStatus(status);
                    this.LeaveRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.LeaveRecordWindow.Show();
                    //DateColumn2.Format = Convert.ToDateTime(response.result.endDate.Date).ToString("MM dd yyyy", new CultureInfo("en-EN"));
                    break;

                case "imgEdit":
                    leaveRequest1.Update(id);
                    break;
            }
        }


        protected void fillLeavesStore()
        {


            LeaveRequestListRequest req = new LeaveRequestListRequest();



            req.BranchId = 0;
            req.DepartmentId = 0;
            req.raEmployeeId = 0;


            req.EmployeeId = Convert.ToInt32(employeeId.SelectedItem.Value);





            req.status = 2;



            req.Size = "50";
            req.StartAt = "0";
            req.SortBy = "recordId";

            req.Filter = "";
            ListResponse<LeaveRequest> resp = _leaveManagementService.ChildGetAll<LeaveRequest>(req);





            //check if the insert failed
            if (!resp.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            else
            {

                resp.Items.ForEach(x =>
                {
                    x.leaveRef = x.leaveRef + " " + x.startDate.ToString(_systemService.SessionHelper.GetDateformat()) + " " + x.endDate.ToString(_systemService.SessionHelper.GetDateformat());
                });
            }
            leaveIdStore.DataSource = resp.Items;
            leaveIdStore.DataBind();
        }


        [DirectMethod]
       
        protected void leaveReturnPoPUP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];

            string seqNoParamter = e.ExtraParams["seqNo"];
            string type = e.ExtraParams["type"];
            //ApprovalsGridPanel.Disabled = false;
            switch (type)
            {
                case "imgEdit":
                    currentSeqNo.Text = seqNoParamter;

                    leaveReturnForm.Reset();

                   

                    LeaveReturnRecordRequest r = new LeaveReturnRecordRequest();
                    r.leaveId = id;
                    RecordResponse<LeaveReturn> response = _leaveManagementService.ChildGetRecord<LeaveReturn>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    employeeId.GetStore().Add(new object[]
                      {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName
                                }
                      });
                    employeeId.SetValue(response.result.employeeId);
                    employeeId.Select(response.result.employeeId);
                    seqNo.Text = seqNoParamter;
                    leaveActivityId.Value = id;
                    leaveActivityId.Text = id;

                    fillLeavesStore();
                    //Step 2 : call setvalues with the retrieved object
                    this.leaveReturnForm.SetValues(response.result);
                   // FillApprovalsStore(id, employeeIdParam, returnTypeParam);

                    this.LeaveReturnWindow.Title = Resources.Common.EditWindowsTitle;
                    this.LeaveReturnWindow.Show();
                    break;

               

                case "imgAttach":

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }

        [DirectMethod]
        protected void ApprovalLoanTab_load(object sender, EventArgs e)
        {

        }
        [DirectMethod]
        protected void TimeTab_Load(object sender, EventArgs e)
        {

        }
        [DirectMethod]
        protected void LeaveRecordTab_Load(object sender, EventArgs e)
        {

        }
        protected void SaveApprovalLoanRecord(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            string id = e.ExtraParams["id"];
         
            string notes = e.ExtraParams["notes"];
            //LoanApproval LV = JsonConvert.DeserializeObject<LoanApproval>(obj);

            //RecordRequest loanRecordRequest = new RecordRequest();
            //loanRecordRequest.RecordID = id;

            //RecordResponse<Loan> loanRecord = _loanService.Get<Loan>(loanRecordRequest);
            //if (!loanRecord.Success)
            //{
            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", loanRecord.ErrorCode) != null ? GetGlobalResourceObject("Errors", loanRecord.ErrorCode).ToString() + "<br>Technical Error: " + loanRecord.ErrorCode + "<br> Summary: " + loanRecord.Summary : loanRecord.Summary).Show();
            //    return;
            //}


            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 
                LoanApproval LA = new LoanApproval();

                LA.status = Convert.ToInt16(LoanApprovalStatusControl.GetApprovalStatus());
                LA.notes = notes;
                LA.activityId = id;
                LA.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());
                LA.arId = LoanApprovalReasonControl.getApprovalReason() == "0" ? null : LoanApprovalReasonControl.getApprovalReason();
                LA.seqNo = currentSeqNo.Text;
                PostRequest<LoanApproval> request = new PostRequest<LoanApproval>();
                request.entity = LA;


                PostResponse<LoanApproval> r = _loanService.ChildAddOrUpdate<LoanApproval>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r); ;
                    return;
                }
                else
                {
                    currentSeqNo.Text = "";
                    ApprovalLoanStore.Reload();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });
                    BindAlerts();
                    this.approvalLoanWindow.Close();
                }

            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }

        }
        protected void SaveLeaveReturnRecord(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            string id = e.ExtraParams["id"];

            string notes = e.ExtraParams["notes"];
           

            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 
                LeaveReturnApproval LA = new LeaveReturnApproval();

                LA.status = Convert.ToInt16(LeaveReturnApprovalControl.GetApprovalStatus());
                LA.notes = notes;
                LA.activityId = id;
                LA.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());
                LA.arId = LeaveReturnApprovalReasonControl.getApprovalReason() == "0" ? null : LeaveReturnApprovalReasonControl.getApprovalReason();
                LA.seqNo = currentSeqNo.Text;
                PostRequest<LeaveReturnApproval> request = new PostRequest<LeaveReturnApproval>();
                request.entity = LA;


                PostResponse<LeaveReturnApproval> r = _leaveManagementService.ChildAddOrUpdate<LeaveReturnApproval>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r); ;
                    return;
                }
                else
                {
                    currentSeqNo.Text = "";
                    leaveReturnStore.Reload();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });
                    BindAlerts();
                    this.LeaveReturnWindow.Close();
                }

            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }

        }



        
        protected void SavePurchaseApprovalRecord(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            string poId = e.ExtraParams["poId"];

           
            
            AssetManagementPurchaseOrderApproval PA = JsonConvert.DeserializeObject<AssetManagementPurchaseOrderApproval>(obj);

            PA.status = string.IsNullOrEmpty(PurchaseApprovalStatusControl.GetApprovalStatus()) ? (Int16?)null : Convert.ToInt16(PurchaseApprovalStatusControl.GetApprovalStatus());
            PA.activityId = string.IsNullOrEmpty(poId) ? null : poId;
            PA.arId = PurchasApprovalReasonControl.getApprovalReason() == "0" ? null : PurchasApprovalReasonControl.getApprovalReason();
            PA.approverId = _systemService.SessionHelper.GetEmployeeId();
            PA.seqNo = currentSeqNo.Text;
            
            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 
              
               

                PostRequest<AssetManagementPurchaseOrderApproval> request = new PostRequest<AssetManagementPurchaseOrderApproval>();
                request.entity = PA;
                PostResponse<AssetManagementPurchaseOrderApproval> r = _assetManagementService.ChildAddOrUpdate<AssetManagementPurchaseOrderApproval>(request);
                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }
                else
                {
                    currentSeqNo.Text = "";
                    PurchasesApprovalStore.Reload();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });
                    BindAlerts();
                    this.purchaseApprovalWindow.Close();
                }

            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }

        }
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            string id = e.ExtraParams["id"];
            string seqNo = e.ExtraParams["seqNo"];
            string returnNotes = e.ExtraParams["returnNotes"];
            //short LeveApprovalStatusControl = Convert.ToInt16(e.ExtraParams["LeveApprovalStatusControl"]);

            //LeaveRequest LV = JsonConvert.DeserializeObject<LeaveRequest>(obj);
            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 
                LeaveRequest LV = new LeaveRequest();
                PostRequest<DashboardLeave> request = new PostRequest<DashboardLeave>();
                request.entity = new DashboardLeave();
                request.entity.activityId = leaveReqApprove.Text;//LV.recordId;
                if (_systemService.SessionHelper.GetEmployeeId() != null)
                    request.entity.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());
                else
                    return;
                request.entity.status = Convert.ToInt16(LeveApprovalStatusControl.GetApprovalStatus()); //LV.apStatus;
                if (!string.IsNullOrEmpty(LV.returnNotes))
                    request.entity.notes = LV.returnNotes;
                else
                    request.entity.notes = " ";

                request.entity.arId = LeaveApprovalReasonControl.getApprovalReason() == "0" ? null : LeaveApprovalReasonControl.getApprovalReason();
                request.entity.seqNo = seqNo;
                request.entity.status = Convert.ToInt16(LeveApprovalStatusControl.GetApprovalStatus());
                PostResponse<DashboardLeave> r = _leaveManagementService.ChildAddOrUpdate<DashboardLeave>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }
                else
                {

                    LeaveRequestsStore.Reload();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });
                    BindAlerts();
                    this.LeaveRecordWindow.Close();
                }

            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }

        }
        protected void SaveTimeRecord(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            string tvId = e.ExtraParams["tvId"];
            string notes = e.ExtraParams["notes"];
            //Time TI = JsonConvert.DeserializeObject<Time>(obj);
            try
            {
                TimeApprovalRecordRequest recReq = new TimeApprovalRecordRequest();
                recReq.seqNo = currentSeqNo.Text;

                recReq.tvId = tvId;


                RecordResponse<Time> reqResp = _timeAttendanceService.ChildGetRecord<Time>(recReq);
                if (!reqResp.Success)
                {
                    Common.errorMessage(reqResp);
                }

                PostRequest<Time> request = new PostRequest<Time>();
                request.entity = reqResp.result;
                
                request.entity.status = Convert.ToInt16(TimeApprovalStatusControl.GetApprovalStatus());
                request.entity.notes = notes;
                request.entity.activityId = tvId;



                PostResponse<Time> r = _timeAttendanceService.ChildAddOrUpdate<Time>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }
                else
                {

                    TimeStore.Reload();
                    seqNo.Text = "";
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });
                    BindAlerts();
                    this.TimeWindow.Close();
                }

            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }

        }



        protected void AttendancePeriodStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            BindAttendancePeriod();
        }

        protected void CompanyHeadCountStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            BindCompanyHeadCount();
        }

        private void BindCompanyHeadCount(bool normalSized = true)
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();

            string rep_params = "";
            ReportGenericRequest req = new ReportGenericRequest();
          

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("1", new DateTime(1991, 1, 1).ToString("yyyyMMdd"));
            parameters.Add("2", CountDateTo.SelectedDate.ToString("yyyyMMdd"));





            foreach (KeyValuePair<string, string> entry in parameters)
            {
                rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
            }
            if (rep_params.Length > 0)
            {
                if (rep_params[rep_params.Length - 1] == '^')
                    rep_params = rep_params.Remove(rep_params.Length - 1);
            }

            //ReportCompositeRequest req = new ReportCompositeRequest();
            //DateRangeParameterSet range = new DateRangeParameterSet();
            //range.DateFrom = new DateTime(1991, 1, 1);
            //try
            //{
            //    range.DateTo = CountDateTo.SelectedDate;
            //}
            //catch (Exception)
            //{

            //    range.DateTo = DateTime.Now;
            //}
            //req.Add(range);
            req.paramString = rep_params;
            ListResponse<RT103> resp = _reportsService.ChildGetAll<RT103>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            List<String> cats = new List<string>();
            foreach (DateTime d in resp.Items.Select(a => a.date))
            {

                cats.Add(GetGlobalResourceObject("Common", Enum.GetName(typeof(DayOfWeek), d.DayOfWeek) + "Text").ToString() + " - " + d.ToString("MM-dd") + "  ");

            }
            List<int> listIn = resp.Items.Select(a => a.headCount).ToList();



            // Store1.DataSource = resp.Items;
            // Store1.DataBind();
            X.Call("drawCompanyHeadCountChart", JSON.JavaScriptSerialize(listIn), JSON.JavaScriptSerialize(cats), rtl ? true : false, normalSized);

        }

        protected void DimensionalHeadCountStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            BindDimensionalHeadCount(true);
        }

        protected void BindDimensionalHeadCount(bool normalSized = true)
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            ReportCompositeRequest req = new ReportCompositeRequest();
            DayIdParameterSet s = new DayIdParameterSet();
            try
            {
                s.Date = CountDateTo.SelectedDate;
            }
            catch (Exception)
            {

                s.Date = DateTime.Now;
            }
            req.Add(s);
            ListResponse<RT110> resp = _reportsService.ChildGetAll<RT110>(req);
            if (!resp.Success)
            {
                return;
            }

            int dim = 1;
            try
            {
                dim = Convert.ToInt32(dimension.SelectedItem.Value);
            }
            catch (Exception)
            {


            }
            List<string> listCategories = resp.Items.Where(a => a.dimension == dim).Select(a => a.name).ToList();
            List<int> listIn = resp.Items.Where(a => a.dimension == dim).Select(a => a.headCount).ToList();



            // Store1.DataSource = resp.Items;
            // Store1.DataBind();
            X.Call("drawDimensionalHeadCountChart", JSON.JavaScriptSerialize(listIn), JSON.JavaScriptSerialize(listCategories), rtl ? true : false, normalSized);

        }

        protected void zoomAttendancePeriod(object sender, DirectEventArgs e)
        {
            BindAttendancePeriod(false);

        }
        protected void zoomAlert(object sender, DirectEventArgs e)
        {
            BindAlerts(false);

        }

        protected void zoomDepartmentCount(object sender, DirectEventArgs e)
        {
            BindDepartmentsCount(false);

        }
        protected void zoomDimensionalHeadCount(object sender, DirectEventArgs e)
        {
            BindDimensionalHeadCount(false);

        }
        protected void zoomCompanyHeadCount(object sender, DirectEventArgs e)
        {
            BindCompanyHeadCount(false);

        }

        protected void UnlateStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();

            ListResponse<ActiveCheck> ACs = _timeAttendanceService.ChildGetAll<ActiveCheck>(r);

            if (!ACs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, ACs.Summary).Show();
                return;
            }
            ListResponse<ActiveLate> ALs = _timeAttendanceService.ChildGetAll<ActiveLate>(r);
            if (!ALs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, ALs.Summary).Show();
                return;
            }
            List<ActiveCheck> nonLate = new List<ActiveCheck>();
            foreach (var item in ACs.Items)
            {
                if (!ALs.Items.Exists(x => x.employeeId == item.employeeId))
                    nonLate.Add(item);

            }
            UnlateStore.DataSource = nonLate;
            UnlateStore.DataBind();
        }

        protected void InStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();

            ListResponse<ActiveCheck> ACs = _timeAttendanceService.ChildGetAll<ActiveCheck>(r);

            if (!ACs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, ACs.Summary).Show();
                return;
            }
            ListResponse<ActiveOut> AOs = _timeAttendanceService.ChildGetAll<ActiveOut>(r);
            if (!AOs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, AOs.Summary).Show();
                return;
            }

            List<ActiveCheck> activeIn = new List<ActiveCheck>();
            foreach (var item in ACs.Items)
            {
                if (!AOs.Items.Exists(x => x.employeeId == item.employeeId))
                    activeIn.Add(item);

            }
            InStore.DataSource = activeIn;
            InStore.DataBind();
        }
        protected void retirementAge_ReadData(object sender, StoreReadDataEventArgs e)
        {

            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<RetirementAge> resp = _dashBoardService.ChildGetAll<RetirementAge>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            resp.Items.ForEach(x => x.birthDateString = x.birthDate.ToString(_systemService.SessionHelper.GetDateformat()));
            retirementAgeStore.DataSource = resp.Items;
            retirementAgeStore.DataBind();
        }
        protected void TermEndDate_ReadData(object sender, StoreReadDataEventArgs e)
        {

            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<TermEndDate> resp = _dashBoardService.ChildGetAll<TermEndDate>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            TermEndDateStore.DataSource = resp.Items;
            TermEndDateStore.DataBind();
        }

        protected void Checked_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<DashBoardCH> resp = _dashBoardService.ChildGetAll<DashBoardCH>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            CheckedStore.DataSource = resp.Items;
            CheckedStore.DataBind();
        }

        protected void Pending_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<DashBoardPE> resp = _dashBoardService.ChildGetAll<DashBoardPE>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            PendingStore.DataSource = resp.Items;
            PendingStore.DataBind();
        }
        protected void NoShowUp_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<DashboardNS> resp = _dashBoardService.ChildGetAll<DashboardNS>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            NoShowUpStore.DataSource = resp.Items;
            NoShowUpStore.DataBind();
        }
        protected void LeaveWithoutExcuse_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<DashboardLW> resp = _dashBoardService.ChildGetAll<DashboardLW>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            LeaveWithoutExcuseStore.DataSource = resp.Items;
            LeaveWithoutExcuseStore.DataBind();
        }
        protected void DayOff_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<DashBoardDO> resp = _dashBoardService.ChildGetAll<DashBoardDO>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            DayOffStore.DataSource = resp.Items;
            DayOffStore.DataBind();
        }
        protected void EmploymentReviewDate_ReadData(object sender, StoreReadDataEventArgs e)
        {

            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<EmploymentReview> resp = _dashBoardService.ChildGetAll<EmploymentReview>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            EmploymentReviewDateStore.DataSource = resp.Items;
            EmploymentReviewDateStore.DataBind();
        }
        protected void Leave_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<DashBoardLE> resp = _dashBoardService.ChildGetAll<DashBoardLE>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            LeaveStore.DataSource = resp.Items;
            LeaveStore.DataBind();
        }


        #region Classes to be moved to a client folder model 
        public class ChartData
        {
            public string name { get; set; }
            public double y { get; set; }
            public int index { get; set; }
        }
        #endregion

        protected void TimeVariationStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
           
            //string rep_params = "";
            try
            {
                //    Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
                //    if (parameters.ContainsKey("2"))
                //        parameters.ChangeKey("2", "7");
                //    if (parameters.ContainsKey("3"))
                //        parameters.ChangeKey("3", "8");
                //    if (parameters.ContainsKey("5"))
                //        parameters.ChangeKey("5", "9");
                //    if (parameters.ContainsKey("1"))
                //        parameters.Remove("1");
                //    if (parameters.ContainsKey("4"))
                //        parameters.Remove("4");





                ////    parameters.Add("1", "0");
                //    parameters.Add("2", DateTime.Now.ToString("yyyyMMdd"));
                //    parameters.Add("3", DateTime.Now.ToString("yyyyMMdd"));
                //  parameters.Add("5", CurrentTimeVariationType.Text);
                //  //  parameters.Add("6", "0");



                //foreach (KeyValuePair<string, string> entry in parameters)
                //{
                //    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                //}
                //if (rep_params.Length > 0)
                //{
                //    if (rep_params[rep_params.Length - 1] == '^')
                //        rep_params = rep_params.Remove(rep_params.Length - 1);
                //}

                string rep_params = vals.Text;





                Services.Messaging.DashBoard.TimeVariationListRequest TVReq = new Services.Messaging.DashBoard.TimeVariationListRequest();
                TVReq.paramString = rep_params;
                TVReq.timeCode = CurrentTimeVariationType.Text;
                ListResponse<DashBoardTimeVariation> resp = _dashBoardService.ChildGetAll<DashBoardTimeVariation>(TVReq);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                resp.Items.ForEach(x =>
                {
                    switch (x.apStatus)
                    {
                        case 1: x.apStatusString = GetLocalResourceObject("FieldNew").ToString();
                            break;
                        case 2: x.apStatusString = approved.Text;
                            break;
                        case -1: x.apStatusString = rejected.Text;
                            break;
                        default: x.apStatusString = string.Empty;
                            break;


                    }
                });
                TimeVariationStore.DataSource = resp.Items;
                TimeVariationStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }




       
        [DirectMethod]

        public void PaidAndUnpaidLeaveWindow(string index)
        {
            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<DashBoardPL> respPE;
            ListResponse<DashBoardUL> respUL;
            if (index == ConstDashboardItem.TA_PAID_LEAVE.ToString())
            {

                respPE = _dashBoardService.ChildGetAll<DashBoardPL>(req);
                if (!respPE.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", respPE.ErrorCode) != null ? GetGlobalResourceObject("Errors", respPE.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + respPE.LogId : respPE.Summary).Show();
                    return;
                }
                LeaveStore.DataSource = respPE.Items;
                LeaveStore.DataBind();
                //   LeaveWindow.Title = GetGlobalResourceObject("Common", "PaidLeaves").ToString(); 
                LeaveWindow.Show();
            }
            else
            {
                respUL = _dashBoardService.ChildGetAll<DashBoardUL>(req);

                if (!respUL.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", respUL.ErrorCode) != null ? GetGlobalResourceObject("Errors", respUL.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + respUL.LogId : respUL.Summary).Show();
                    return;
                }
                LeaveStore.DataSource = respUL.Items;
                LeaveStore.DataBind();
                //    LeaveWindow.Title = GetGlobalResourceObject("Common", "UnpaidLeaves").ToString();
                LeaveWindow.Show();

            }

        }
        protected void EmployeePenaltyApprovalStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    return;

                string rep_params = "";
                Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
                if (parameters.ContainsKey("5"))
                    parameters.ChangeKey("5", "7");
                if (parameters.ContainsKey("4"))
                    parameters.ChangeKey("4", "6");
                if (parameters.ContainsKey("2"))
                    parameters.ChangeKey("2", "5");
                if (parameters.ContainsKey("1"))
                    parameters.ChangeKey("1", "4");



                parameters.Add("1", "1");
                parameters.Add("2", _systemService.SessionHelper.GetEmployeeId());

             


                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params = rep_params.Remove(rep_params.Length - 1);
                }







                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;




                //DashboardRequest request = GetDashboardRequest();
                //EmployeePenaltyApprovalListRequest req = new EmployeePenaltyApprovalListRequest();

                //req.apStatus = "1";
                //req.penaltyId = "0";
                //req.approverId = _systemService.SessionHelper.GetEmployeeId() != null ? _systemService.SessionHelper.GetEmployeeId().ToString() : null;
                //req.BranchId = request.BranchId;
                //req.PositionId = request.PositionId;
                //req.DivisionId = request.DivisionId;
                //req.DepartmentId = request.DepartmentId;
                //req.EsId = request.EsId;
                //if (string.IsNullOrEmpty(req.penaltyId) || string.IsNullOrEmpty(req.approverId))
                //{
                //    EmployeePenaltyApprovalStore.DataSource = new List<EmployeePenaltyApproval>();
                //    EmployeePenaltyApprovalStore.DataBind();
                //    return;
                //}
                ListResponse<AionHR.Model.Employees.PendingPA> response = _employeeService.ChildGetAll<AionHR.Model.Employees.PendingPA>(req);

                if (!response.Success)
                {
                    Common.errorMessage(response);
                    return;
                }
                response.Items.ForEach(x =>
                {

                    switch (x.status)
                    {
                        case 1:
                            x.statusString = StatusNew.Text;
                            break;
                        case 2:
                            x.statusString = StatusInProcess.Text;
                            ;
                            break;
                        case 3:
                            x.statusString = StatusApproved.Text;
                            ;
                            break;
                        case -1:
                            x.statusString = StatusRejected.Text;

                            break;
                    }
                }
              );
                EmployeePenaltyApprovalStore.DataSource = response.Items;
                EmployeePenaltyApprovalStore.DataBind();
            } catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void PurchasesApprovalStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {


               
                    if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                        return;

                    string rep_params = "";
                    Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
                    if (parameters.ContainsKey("5"))
                        parameters.ChangeKey("5", "7");
                    if (parameters.ContainsKey("4"))
                        parameters.ChangeKey("4", "6");
                    if (parameters.ContainsKey("2"))
                        parameters.ChangeKey("2", "5");
                    if (parameters.ContainsKey("1"))
                        parameters.ChangeKey("1", "4");



                    parameters.Add("1", "1");
                    parameters.Add("2", _systemService.SessionHelper.GetEmployeeId());




                    foreach (KeyValuePair<string, string> entry in parameters)
                    {
                        rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                    }
                    if (rep_params.Length > 0)
                    {
                        if (rep_params[rep_params.Length - 1] == '^')
                            rep_params = rep_params.Remove(rep_params.Length - 1);
                    }







                    ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
              
                ListResponse<AionHR.Model.AssetManagement.PendingPA> response = _assetManagementService.ChildGetAll<AionHR.Model.AssetManagement.PendingPA>(req);

                if (!response.Success)
                {
                    Common.errorMessage(response);
                    return;
                }
             
             
                PurchasesApprovalStore.DataSource = response.Items;
                PurchasesApprovalStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        private void FillTimeApproval(string tvId)
        {

         
           
                string rep_params = "";
                try
                {
                    Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
                    if (parameters.ContainsKey("2"))
                        parameters.ChangeKey("2", "8");
                    if (parameters.ContainsKey("3"))
                        parameters.ChangeKey("3", "9");
                    if (parameters.ContainsKey("5"))
                        parameters.ChangeKey("5", "10");
                    if (parameters.ContainsKey("1"))
                        parameters.Remove("1");
                    if (parameters.ContainsKey("4"))
                        parameters.Remove("4");







                 
              
                parameters.Add("12", tvId);
                //if (dtFrom != null)
                //    parameters.Add("2",((DateTime) dtFrom).ToString("yyyyMMdd"));
                ////else
                ////    parameters.Add("2", ((DateTime)date).ToString("yyyyMMdd"));
                //if (dtTo != null)
                //    parameters.Add("3", ((DateTime)dtTo).ToString("yyyyMMdd"));
                //else
                //    parameters.Add("3", ((DateTime)date).ToString("yyyyMMdd"));
                //if (!string.IsNullOrEmpty(shiftId))
                //    parameters.Add("4", shiftId);

                //parameters.Add("5", timeCode);
                //parameters.Add("6", "0");
                //parameters.Add("7", "0");




                foreach (KeyValuePair<string, string> entry in parameters)
                    {
                        rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                    }
                    if (rep_params.Length > 0)
                    {
                        if (rep_params[rep_params.Length - 1] == '^')
                            rep_params = rep_params.Remove(rep_params.Length - 1);
                    }







                    ReportGenericRequest r = new ReportGenericRequest();
                    r.paramString = rep_params;


              

                ListResponse<Time> Times = _timeAttendanceService.ChildGetAll<Time>(r);
                if (!Times.Success)
                {
                    Common.errorMessage(Times);
                    return;
                }
                List<XMLDictionary> timeCodeList = ConstTimeVariationType.TimeCodeList(_systemService);
                int currentTimeCode;
                Times.Items.ForEach(x =>
                {
                    if (Int32.TryParse(x.timeCode, out currentTimeCode))
                    {
                        x.timeCodeString = timeCodeList.Where(y => y.key == Convert.ToInt32(x.timeCode)).Count() != 0 ? timeCodeList.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;
                    }


                    x.statusString = FillApprovalStatus(x.status);
                });

                timeApprovalStore.DataSource = Times.Items;
                //////List<ActiveLeave> leaves = new List<ActiveLeave>();
                ////leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


                timeApprovalStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

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

        protected void LeaveingSoonStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                if (CurrentItemId.Text == "11")
                {
                    ListResponse<ReturnFromLeave> resp = _dashBoardService.ChildGetAll<ReturnFromLeave>(req);
                    if (!resp.Success)
                    {
                        Common.errorMessage(resp);
                        return;
                    }
                    LeaveingSoonStore.DataSource = resp.Items;
                    LeaveingSoonStore.DataBind();
                }
                else
                {

                    ListResponse<LeavingSoon> respLeave = _dashBoardService.ChildGetAll<LeavingSoon>(req);

                    if (!respLeave.Success)
                    {
                        Common.errorMessage(respLeave);
                        return;
                    }
                    LeaveingSoonStore.DataSource = respLeave.Items;
                    LeaveingSoonStore.DataBind();
                }
               
              

            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void FillApprovalLoan(int EmployeeId,string loanId)
        {
            try
            {

                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    return;

             
                


              

                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = "8|" + loanId;
                ListResponse<LoanApproval> routers = _loanService.ChildGetAll<LoanApproval>(req);                                                                                                        
               
                //request.EmployeeId = EmployeeId;
                //request.Status = 0;
                //request.Filter = "";
                //request.LoanId = loanId; 
                //request.SortBy = "recordId";





                //request.Size = "1000";
                //request.StartAt = "0";
              
                if (!routers.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId : routers.Summary).Show();
                    return;
                }
                routers.Items.ForEach(x =>
                {

                    x.statusString = FillApprovalStatus(x.status);
                });
                this.storeApprovalLoan.DataSource = routers.Items;


                this.storeApprovalLoan.DataBind();
            }

            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();


            }
        }

        protected void alertStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<DashboardAlertItem> resp = _dashBoardService.ChildGetAll<DashboardAlertItem>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return; 
                             
            }
            alertStore.DataSource = resp.Items;
            alertStore.DataBind(); 
        }
        protected void branchAvailabilityStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
            ListResponse<DashboardBranchAvailability> resp = _dashBoardService.ChildGetAll<DashboardBranchAvailability>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;

            }
          //  resp.Items.Add(new DashboardBranchAvailability() { branchName = "elias", scheduled = 10, present = 10 });
            branchAvailabilityStore.DataSource = resp.Items;
            branchAvailabilityStore.DataBind();
        }
        
             protected void processPunches(object sender, DirectEventArgs e)
        {
            PostRequest<PendingPunch> req = new PostRequest<PendingPunch>();
            req.entity = new PendingPunch();
            PostResponse<PendingPunch> resp = _timeAttendanceService.ChildAddOrUpdate<PendingPunch>(req); 
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return; 

            }
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordSavingSucc
            });
            punchesStore.Reload();
            BindAlerts();
           
        }


        protected void DeletePunches(object sender, DirectEventArgs e)
        {
            try
            {
                if (ppType.SelectedItem.Value != "4")
                    return;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = "1^"+ppType.SelectedItem.Value == null ? "0" : ppType.SelectedItem.Value.ToString();

             


                ListResponse<PendingPunch> punches = _timeAttendanceService.ChildGetAll<PendingPunch>(req);
                if (!punches.Success)
                {
                    Common.errorMessage(punches);
                    return;
                }

                PostRequest<PendingPunch> delReq = new PostRequest<PendingPunch>();


                punches.Items.ForEach(x =>
                {
                    delReq.entity = x;
                    PostResponse<PendingPunch> delResp = _timeAttendanceService.ChildDelete<PendingPunch>(delReq);
                    if (!delResp.Success)
                    {
                        Common.errorMessage(delResp);
                        throw new Exception();
                    }
                });


                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });
               
                punchesStore.Reload();
                BindAlerts();
            }
            catch(Exception exp)
            {
                if (!string.IsNullOrEmpty(exp.Message))
                X.MessageBox.Alert(Resources.Common.Error, exp.Message);
            }
        }

        protected void Timebatch(object sender, DirectEventArgs e)
        {
            string approve = e.ExtraParams["approve"];

            X.Msg.Confirm(Resources.Common.Confirmation,approve=="true"?GetLocalResourceObject("approveConfirmation").ToString(): GetLocalResourceObject("rejectConfirmation").ToString(), new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    //We are call a direct request metho for deleting a record
                    Handler = String.Format("App.direct.Timebatch({0})", approve),
                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Text = Resources.Common.No
                }

            }).Show();
        }
        [DirectMethod]
        
       public void Timebatch(string approve)
        {
           
            DashboardRequest req = GetDashboardRequest();
          
            if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))


            { 
                TimeStore.DataSource = new List<Time>();
                TimeStore.DataBind();
                return;
            }
            

            string rep_params = "";
           



            Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
            if (parameters.ContainsKey("2"))
                parameters.ChangeKey("2", "8");
            if (parameters.ContainsKey("3"))
                parameters.ChangeKey("3", "9");
            if (parameters.ContainsKey("5"))
                parameters.ChangeKey("5", "10");
            if (parameters.ContainsKey("1"))
                parameters.Remove("1");
            if (parameters.ContainsKey("4"))
                parameters.Remove("4");


            parameters.Add("6", _systemService.SessionHelper.GetEmployeeId());
            parameters.Add("5", timeVariationType.GetTimeCode());
          
            parameters.Add("7", "1");





            foreach (KeyValuePair<string, string> entry in parameters)
            {
                rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
            }
            if (rep_params.Length > 0)
            {
                if (rep_params[rep_params.Length - 1] == '^')
                    rep_params = rep_params.Remove(rep_params.Length - 1);
            }



            ReportGenericRequest r = new ReportGenericRequest();
            r.paramString = rep_params;

            ListResponse<Time> Times = _timeAttendanceService.ChildGetAll<Time>(r);
            if (!Times.Success)
            {
                Common.errorMessage(Times);
                return;
            }
            PostRequest<Time> request= new PostRequest<Time>();
            PostResponse<Time> resp;
            Times.Items.ForEach(x =>
            {

                request.entity = x;
                if (approve == "true")
                    request.entity.status = 2;
                else
                    request.entity.status = -1;
                resp = _timeAttendanceService.ChildAddOrUpdate<Time>(request);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
            });
            TimeStore.Reload();
            BindAlerts();
        }
        [DirectMethod]
        protected void EmployeePenaltyApprovalPoPUP(object sender, DirectEventArgs e)
        {
            TabPanel5.ActiveIndex = 0;

            string penaltyId = e.ExtraParams["penaltyId"];
            string status = e.ExtraParams["status"];

            currentSeqNo.Text= e.ExtraParams["seqNo"];
            try { 

            EmployeePenaltyApprovalRecordRequest r = new EmployeePenaltyApprovalRecordRequest();
            r.penaltyId = penaltyId;
            r.approverId = _systemService.SessionHelper.GetEmployeeId();
                r.seqNo = currentSeqNo.Text; 
            RecordResponse<EmployeePenaltyApproval> resp = _employeeService.ChildGetRecord<EmployeePenaltyApproval>(r);
                if (!resp.Success)
                {

                    Common.errorMessage(resp);
                    return;
                }
                PADate.Value = resp.result.date;
                penaltyName.Text = resp.result.penaltyName;
                PAEmployeeName.Text = resp.result.employeeName;
                PenaltyApprovalStatusControl.setApprovalStatus(resp.result.status.ToString());
                PAAmount.Text = resp.result.amount;
                notes.Text = resp.result.notes;

                PERecordId.Text = penaltyId;



                this.employeePenaltyApprovalWindow.Title = Resources.Common.EditWindowsTitle;
                this.employeePenaltyApprovalWindow.Show();


            }catch(Exception exp)
            {
                X.MessageBox.Alert(Resources.Common.Error, exp.Message);
            }

        }


        
        protected void saveEmployeePenalty(object sender, DirectEventArgs e)
        {
           
          


            try
            {
                string penaltyId = e.ExtraParams["penaltyId"];
              
                string notes = e.ExtraParams["notes"];

                EmployeePenaltyApprovalRecordRequest r = new EmployeePenaltyApprovalRecordRequest();
                r.penaltyId = penaltyId;
                r.approverId = _systemService.SessionHelper.GetEmployeeId();
                r.seqNo = currentSeqNo.Text;
                RecordResponse<EmployeePenaltyApproval> resp = _employeeService.ChildGetRecord<EmployeePenaltyApproval>(r);
                if (!resp.Success)
                {

                    Common.errorMessage(resp);
                    return;
                }
                //New Mode
                //Step 1 : Fill The object and insert in the store 
                EmployeePenaltyApproval PA = resp.result;
                short number = 0;
                if (!Int16.TryParse(PenaltyApprovalStatusControl.GetApprovalStatus(), out number))
                    return;


                PA.status = number;
                PA.notes = notes;





                PostRequest<EmployeePenaltyApproval> request = new PostRequest<EmployeePenaltyApproval>();
                request.entity = PA;
                request.entity.seqNo = currentSeqNo.Text;


                PostResponse<EmployeePenaltyApproval> resp1 = _employeeService.ChildAddOrUpdate<EmployeePenaltyApproval>(request);


                //check if the insert failed
                if (!resp1.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(resp1); ;
                    return;
                }
                else
                {

                    EmployeePenaltyApprovalStore.Reload();
                    BindAlerts();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });
                   
                    this.employeePenaltyApprovalWindow.Close();
                }

            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
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



       
    }
}
