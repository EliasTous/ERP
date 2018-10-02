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

namespace AionHR.Web.UI.Forms
{
    public partial class Dashboard : System.Web.UI.Page
    {
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
                try
                {

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



                        FillStatus();

                        //outStore.Reload();
                        //activeStore.Reload();
                        //latenessStore.Reload();
                        //leavesStore.Reload();
                        //missingPunchesStore.Reload();
                        //checkMontierStore.Reload();
                        format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                        DateColumn5.Format = DateColumn1.Format = DateColumn2.Format = DateColumn3.Format = DateColumn4.Format = _systemService.SessionHelper.GetDateformat();
                        periodToDate.SelectedDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                        //CountDateTo.SelectedDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                        CountDateTo.SelectedDate = DateTime.Now;
                        dimension.Select(0);
                        BindAlerts();

                    }
                    catch { }
                }
                catch (DashBoardAccessDenied dx) { }
            }

        }



        private void FillStatus()
        {
            ListRequest statusReq = new ListRequest();
            ListResponse<EmploymentStatus> resp = _employeeService.ChildGetAll<EmploymentStatus>(statusReq);
            statusStore.DataSource = resp.Items;
            statusStore.DataBind();
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
             
                DashboardRequest req2 = GetDashboardRequest();
                ListResponse<DashboardItem> dashoard = _dashBoardService.ChildGetAll<DashboardItem>(req2);
                if (!dashoard.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, dashoard.Summary).Show();
                    return new ListResponse<DashboardItem>();
                }

                int birth = dashoard.Items.Where(x => x.itemId == 905).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 905).First().count : 0;
                int annev = dashoard.Items.Where(x => x.itemId == 907).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 907).First().count : 0;
                int comp = dashoard.Items.Where(x => x.itemId == 908).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 908).First().count : 0;
                int empRW = dashoard.Items.Where(x => x.itemId == 909).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 909).First().count : 0;
                int scr = dashoard.Items.Where(x => x.itemId == 901).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 901).First().count : 0;
                int prob = dashoard.Items.Where(x => x.itemId == 902).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 902).First().count : 0;
                int retirementAge = dashoard.Items.Where(x => x.itemId == 906).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 906).First().count : 0;
                int TermEndDate = dashoard.Items.Where(x => x.itemId == 904).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 904).First().count : 0;
                int EmploymentReviewDate = dashoard.Items.Where(x => x.itemId == 903).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 903).First().count : 0;
                int totalLoans = dashoard.Items.Where(x => x.itemId == 910).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 903).First().count : 0;



                annversaries.Text = annev.ToString();
                birthdays.Text = birth.ToString();
                companyRW.Text = comp.ToString();
                salaryChange.Text = scr.ToString();
                probation.Text = prob.ToString();
                employeeRW.Text = empRW.ToString();
                totalLoansLbl.Text = totalLoans.ToString();
                EmploymentReviewDateLbl.Text = EmploymentReviewDate.ToString();
                termEndDateLBL.Text = TermEndDate.ToString();
                retirementAgeLBL.Text = retirementAge.ToString();

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
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
          ListResponse<DashboardItem> dashoard = FillDashBoardItems();




            List<ChartData> activeChartData = new List<ChartData>();
            activeChartData.Add(new ChartData() { name = GetLocalResourceObject("PENDING").ToString(), y = dashoard.Items.Where(x => x.itemId == 102).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 102).First().count : 0, index = 0 });// 10 - Attended
            activeChartData.Add(new ChartData() { name = GetLocalResourceObject("NO_SHOW_UP").ToString(), y = dashoard.Items.Where(x => x.itemId == 103).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 103).First().count : 0, index = 1 });// 110 - Vacations
            activeChartData.Add(new ChartData() { name = GetLocalResourceObject("CHECKED").ToString(), y = dashoard.Items.Where(x => x.itemId == 101).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 101).ToList()[0].count : 0, index = 2 });// 111 - Unpaid leave
            activeChartData.Add(new ChartData() { name = GetLocalResourceObject("LEAVE_WITHOUT_EXCUSE").ToString(), y = dashoard.Items.Where(x => x.itemId == 104).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 104).ToList()[0].count : 0, index = 3 });// 112 - Leave without excuse
                                                                                                                                                                                                                                                                        //activeChartData.Add(new ChartData() { name = GetLocalResourceObject("BusinessLeave").ToString(), y = dashoard.Items.Where(x => x.itemId == 113).ToList()[0].count, index =4 });// 113 - business leave
            activeChartData.Add(new ChartData() { name = GetLocalResourceObject("LEAVE").ToString(), y = dashoard.Items.Where(x => x.itemId == 105).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 105).ToList()[0].count : 0, index = 4 });
            activeChartData.Add(new ChartData() { name = GetLocalResourceObject("DAY_OFF").ToString(), y = dashoard.Items.Where(x => x.itemId == 106).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 106).ToList()[0].count : 0, index = 5 });


            X.Call("drawActiveHightChartPie", JSON.JavaScriptSerialize(activeChartData), rtl ? true : false, normalSized);


            List<ChartData> lateChartData = new List<ChartData>();
            lateChartData.Add(new ChartData() { name = GetLocalResourceObject("EARLY_CHECKIN").ToString(), y = dashoard.Items.Where(x => x.itemId == 221).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 221).ToList()[0].count : 0, index = ConstTimeVariationType.EARLY_CHECKIN });//  ALs.Items.Count
            lateChartData.Add(new ChartData() { name = GetLocalResourceObject("LATE_CHECKIN").ToString(), y = dashoard.Items.Where(x => x.itemId == 211).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 211).ToList()[0].count : 0, index = ConstTimeVariationType.LATE_CHECKIN });
            lateChartData.Add(new ChartData() { name = GetLocalResourceObject("DURING_SHIFT_LEAVE").ToString(), y = dashoard.Items.Where(x => x.itemId == 212).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 212).ToList()[0].count : 0, index = ConstTimeVariationType.DURING_SHIFT_LEAVE });
            lateChartData.Add(new ChartData() { name = GetLocalResourceObject("EARLY_LEAVE").ToString(), y = dashoard.Items.Where(x => x.itemId == 213).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 213).ToList()[0].count : 0, index = ConstTimeVariationType.EARLY_LEAVE });
            lateChartData.Add(new ChartData() { name = GetLocalResourceObject("OVERTIME").ToString(), y = dashoard.Items.Where(x => x.itemId == 222).ToList().Count != 0 ? dashoard.Items.Where(x => x.itemId == 222).ToList()[0].count : 0, index = ConstTimeVariationType.OVERTIME });
            X.Call("drawLateHightChartPie", JSON.JavaScriptSerialize(lateChartData), rtl ? true : false, normalSized);



            List<ChartData> breaksChartData = new List<ChartData>();
            breaksChartData.Add(new ChartData() { name = GetLocalResourceObject("Breaks").ToString(), y = 0 /*dashoard.Items.Where(x => x.itemId == 13).ToList()[0].count*/, index = 0 });// count.result.count - ACs.Items.Count
            breaksChartData.Add(new ChartData() { name = GetLocalResourceObject("Attendance").ToString(), y = 0 /*dashoard.Items.Where(x => x.itemId == 10).ToList()[0].count - dashoard.Items.Where(x => x.itemId == 13).ToList()[0].count*/, index = 1 });//

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

        protected void missingPunchesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                ListResponse<MissedPunch> ACs = _timeAttendanceService.ChildGetAll<MissedPunch>(r);
                if (!ACs.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", ACs.ErrorCode) != null ? GetGlobalResourceObject("Errors", ACs.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + ACs.LogId : ACs.Summary).Show();
                    return;
                }

                //List<MissedPunch> s = new List<MissedPunch>();
                //s.Add(new MissedPunch() { date = DateTime.Now, employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "issa" }, missedIn = true, missedOut = false, recordId = "1", time = "08:30" });
                missingPunchesStore.DataSource = ACs.Items;
                missingPunchesStore.DataBind();
                mpCount.Text = "6";
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

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
            ActiveAttendanceRequest req = new ActiveAttendanceRequest();
            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
            req.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value : 0;
            req.PositionId = d.PositionId.HasValue ? d.PositionId.Value : 0;

            int intResult;


            if (!string.IsNullOrEmpty(esId.Text) && esId.Value.ToString() != "0" && int.TryParse(esId.Value.ToString(), out intResult))
            {
                req.StatusId = intResult;


            }
            else
            {
                req.StatusId = 0;

            }




            return req;
        }

        private EmployeeCountRequest GetEmployeeCountRequest()

        {
            EmployeeCountRequest req = new EmployeeCountRequest();
            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
            req.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value : 0;
            req.PositionId = d.PositionId.HasValue ? d.PositionId.Value : 0;
            int intResult;



            if (!string.IsNullOrEmpty(esId.Text) && esId.Value.ToString() != "0" && int.TryParse(esId.Value.ToString(), out intResult))
            {
                req.StatusId = intResult;


            }
            else
            {
                req.StatusId = 0;

            }




            return req;
        }

        protected void OverDueStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                TaskManagementListRequest req = GetTaskManagementRequest();
                ListResponse<Model.TaskManagement.Task> resp = _taskManagementService.GetAll<Model.TaskManagement.Task>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                    return;
                }
                List<Model.TaskManagement.Task> today = resp.Items.Where(x => x.dueDate == DateTime.Today && !x.completed).ToList();
                List<Model.TaskManagement.Task> late = resp.Items.Where(x => x.dueDate < DateTime.Today && !x.completed).ToList();
                int count = resp.Items.Count(x => !x.completed);

                OverDueStore.DataSource = late;
                OverDueStore.DataBind();
                DueTodayStore.DataSource = today;
                DueTodayStore.DataBind();
                int overDue = late.Count;
                int todays = today.Count;
                X.Call("chart3", todays, count);
                X.Call("chart2", overDue, count);
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }



        private TaskManagementListRequest GetTaskManagementRequest()
        {
            TaskManagementListRequest req = new TaskManagementListRequest();


            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;



            req.DivisionId = 0;


            req.Size = "30";
            req.StartAt = "1";
            req.Filter = "";
            req.SortBy = "dueDate";
            req.InRelationToId = 0;
            req.AssignToId = 0;
            req.Completed = 0;

            return req;
        }

        private LoanManagementListRequest GetLoanManagementRequest()
        {
            LoanManagementListRequest req = new LoanManagementListRequest();
            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;




            req.DivisionId = 0;


            req.Size = "30";
            req.StartAt = "1";
            req.Filter = "";
            req.SortBy = "recordId";
            req.Status = 2;


            return req;
        }

        private LeaveRequestListRequest GetLeaveManagementRequest()
        {
            LeaveRequestListRequest req = new LeaveRequestListRequest();

            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
            RecordRequest r = new RecordRequest();
            r.RecordID = _systemService.SessionHelper.GetCurrentUserId();
            RecordResponse<UserInfo> response = _systemService.ChildGetRecord<UserInfo>(r);
            if (response.result == null)
                return null;

            req.raEmployeeId = Convert.ToInt32(response.result.employeeId);
            if (string.IsNullOrEmpty(response.result.employeeId))
                return null;
            userSessionEmployeeId.Text = response.result.employeeId;
            req.status = 1;



            req.Size = "30";
            req.StartAt = "1";
            req.Filter = "";
            req.SortBy = "recordId";


            return req;
        }

        private DashboardRequest GetDashboardRequest()
        {

            DashboardRequest req = new DashboardRequest();
         
            int intResult;
            
            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
            req.PositionId = d.PositionId.HasValue ? d.PositionId.Value : 0;
            req.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value : 0;
            if (!string.IsNullOrEmpty(esId.Text) && esId.Value.ToString() != "0")
            {
                req.EsId = Convert.ToInt32(esId.Value);



            }
            else
            {
                req.EsId = 0;

            }





            return req;
        }

        protected void RefreshAllGrid(object sender, EventArgs e)
        {
            BindAlerts();
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




        protected void LoansStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                LoanManagementListRequest req = GetLoanManagementRequest();
                req.Status = 1;
                ListResponse<Loan> resp = _loanService.GetAll<Loan>(req);
                //List<Loan> OpenLoans = loans.Items.Where(t => t.status == 1).ToList();

                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                    return;
                }

                LoansStore.DataSource = resp.Items;
                LoansStore.DataBind();
                int x = resp.count;
                X.Call("loansChart", x, x + (10 - (x % 10)));
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void LeaveRequestsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {

                LeaveRequestListRequest req = GetLeaveManagementRequest();

                if (req != null)
                {
                    ListResponse<LeaveRequest> resp = _leaveManagementService.ChildGetAll<LeaveRequest>(req);

                    if (!resp.Success)
                    {
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                        return;
                    }


                    LeaveRequestsStore.DataSource = resp.Items;
                    LeaveRequestsStore.DataBind();
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
                DashboardRequest req = GetDashboardRequest();
                ListResponse<EmployeeBirthday> resp = _systemService.ChildGetAll<EmployeeBirthday>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                    return;
                }
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
                DashboardRequest req = GetDashboardRequest();
                ListResponse<WorkAnniversary> resp = _systemService.ChildGetAll<WorkAnniversary>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                    return;
                }
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
                DashboardRequest req = GetDashboardRequest();
                ListResponse<CompanyRTW> resp = _systemService.ChildGetAll<CompanyRTW>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                    return;
                }
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
                DashboardRequest req = GetDashboardRequest();
                ListResponse<EmpRTW> resp = _systemService.ChildGetAll<EmpRTW>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
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
                DashboardRequest req = GetDashboardRequest();
                ListResponse<SalaryChange> resp = _systemService.ChildGetAll<SalaryChange>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                    return;
                }
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
                DashboardRequest req = GetDashboardRequest();
                ListResponse<ProbationEnd> resp = _systemService.ChildGetAll<ProbationEnd>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                    return;
                }
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
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
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
                TimeBoundedActiveAttendanceRequest req = GetTimeBoundedActiveAttendanceRequest();

                ListResponse<AttendancePeriod> resp = _systemService.ChildGetAll<AttendancePeriod>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
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

        private TimeBoundedActiveAttendanceRequest GetTimeBoundedActiveAttendanceRequest()
        {
            ActiveAttendanceRequest re = GetActiveAttendanceRequest();
            TimeBoundedActiveAttendanceRequest b = new TimeBoundedActiveAttendanceRequest();
            b.DepartmentId = re.DepartmentId;
            b.BranchId = re.BranchId;
            b.DayStatus = re.DayStatus;
            b.DivisionId = re.DivisionId;
            b.PositionId = re.PositionId;

            try
            {
                b.startingDayId = periodToDate.SelectedDate.ToString("yyyyMMdd");
            }
            catch
            {
                b.startingDayId = DateTime.Now.ToString("yyyyMMdd");


            }
            return b;
        }

        protected void CompletedLoansStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                LoanManagementListRequest req = new LoanManagementListRequest();
                req = GetLoanManagementRequest();

                ListResponse<Loan> resp = _loanService.GetAll<Loan>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
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
                req.Status = 2;
                ListResponse<Loan> resp = _loanService.GetAll<Loan>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
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
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
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
                string filter = string.Empty;
                int totalCount = 1;



                //Fetching the corresponding list

                //in this test will take a list of News
                //ListRequest request = new ListRequest();
                LoanManagementListRequest request = new LoanManagementListRequest();
                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                {
                    ApprovalLoanStore.DataSource = new List<Loan>();
                    ApprovalLoanStore.DataBind();
                    return;
                }
                request.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());
                request.BranchId = 0;
                request.DepartmentId = 0;
                request.DivisionId = 0;
                request.EmployeeId = 0;
                request.Status = 1;
                request.Filter = "";

                request.SortBy = "recordId";





                request.Size = "1000";
                request.StartAt = "0";
                ListResponse<Loan> routers = _loanService.GetAll<Loan>(request);
                if (!routers.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId : routers.Summary).Show();
                    return;
                }
                routers.Items.ForEach(x =>
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
                this.ApprovalLoanStore.DataSource = routers.Items;
                e.Total = routers.count;

                this.ApprovalLoanStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        protected void TimeStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                DashboardTimeListRequest r = new DashboardTimeListRequest();
                r.dayId = "";
                r.employeeId = 0;
                if (!string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    r.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());
                else
                {
                    TimeStore.DataSource = new List<Time>();
                    TimeStore.DataBind();
                    return;
                }
                ListResponse<Time> Times = _timeAttendanceService.ChildGetAll<Time>(r);
                if (!Times.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", Times.ErrorCode) != null ? GetGlobalResourceObject("Errors", Times.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + Times.LogId : Times.Summary).Show();
                    return;
                }
                Times.Items.ForEach(x =>
                {
                    x.timeCodeString = GetLocalResourceObject(x.timeCode + "text").ToString();
                });

                TimeStore.DataSource = Times.Items;
                ////List<ActiveLeave> leaves = new List<ActiveLeave>();
                //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


                TimeStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }


        protected void LocalRateStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                ActiveAttendanceRequest req = GetActiveAttendanceRequest();
                ListResponse<LocalsRate> resp = _helpFunctionService.ChildGetAll<LocalsRate>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                    return;
                }
                inName.Text = resp.Items[0].inName;
                bsName.Text = resp.Items[0].bsName;
                leName.Text = resp.Items[0].leName;
                List<object> RateObjs = new List<object>();
                RateObjs.Add(new { category = GetLocalResourceObject("netRate").ToString(), number = resp.Items[0].netRate });
                RateObjs.Add(new { category = GetLocalResourceObject("rate").ToString(), number = resp.Items[0].rate });
                RateObjs.Add(new { category = GetLocalResourceObject("minLERate").ToString(), number = resp.Items[0].minLERate });
                RateObjs.Add(new { category = GetLocalResourceObject("minNextLERate").ToString(), number = resp.Items[0].minNextLERate });


                List<string> listCategories = new List<string>() { GetLocalResourceObject("rate").ToString(), GetLocalResourceObject("minLERate").ToString(), GetLocalResourceObject("netRate").ToString(), GetLocalResourceObject("minNextLERate").ToString() };
                List<double> listValues = new List<double>() { resp.Items[0].rate, resp.Items[0].minLERate, resp.Items[0].netRate, resp.Items[0].minNextLERate };

                X.Call("drawMinLocalRateCountHightChartColumn", JSON.JavaScriptSerialize(listValues), JSON.JavaScriptSerialize(listCategories), rtl ? true : false);




                List<object> CountObjs = new List<object>();
                CountObjs.Add(new { category = GetLocalResourceObject("employeeCount").ToString(), number = resp.Items[0].employeeCount });//here 
                CountObjs.Add(new { category = GetLocalResourceObject("nationalCount").ToString(), number = resp.Items[0].nationalCount });//here
                CountObjs.Add(new { category = GetLocalResourceObject("netNationalCount").ToString(), number = resp.Items[0].netNationalCount });


                List<string> listCount = new List<string>() { GetLocalResourceObject("employeeCount").ToString(), GetLocalResourceObject("nationalCount").ToString(), GetLocalResourceObject("netNationalCount").ToString() };
                List<double> listempValues = new List<double>() { resp.Items[0].employeeCount, resp.Items[0].nationalCount, resp.Items[0].netNationalCount };

                X.Call("drawLocalCountHightChartColumn", JSON.JavaScriptSerialize(listempValues), JSON.JavaScriptSerialize(listCount), rtl ? true : false);

            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }

        [DirectMethod]
        protected void TimePoPUP(object sender, DirectEventArgs e)
        {
            string employeeId = e.ExtraParams["employeeId"];
            string employeeName = e.ExtraParams["employeeName"];
            string dayId = e.ExtraParams["dayId"];
            string dayIdDate = e.ExtraParams["dayIdDate"];
            string timeCode = e.ExtraParams["timeCode"];
            string timeCodeString = e.ExtraParams["timeCodeString"];
            string status = e.ExtraParams["status"];


            string notes = e.ExtraParams["notes"];
            TimeEmployeeName.Text = employeeName;
            TimedayIdDate.Text = dayIdDate;
            TimeTimeCodeString.Text = timeCodeString;
            TimeStatus.Select(status);

            TimeemployeeIdTF.Text = employeeId;
            TimedayIdTF.Text = dayId;
            TimeTimeCodeTF.Text = timeCode;

            this.TimeWindow.Title = Resources.Common.EditWindowsTitle;
            this.TimeWindow.Show();

        }

        [DirectMethod]
        protected void ApprovalLoanPoPUP(object sender, DirectEventArgs e)
        {
            try
            {
                string id = e.ExtraParams["id"];

                //SetTabPanelEnable(true);





                //Step 1 : get the object from the Web Service 
                RecordRequest r = new RecordRequest();
                r.RecordID = id;

                RecordResponse<Loan> response = _loanService.Get<Loan>(r);
                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                    return;
                }







                //    effectiveDate.Disabled = response.result.status != 3;
                //FillFilesStore(Convert.ToInt32(id));

                //Step 2 : call setvalues with the retrieved object
                this.ApprovalLoanForm.SetValues(response.result);

                ApprovalLoanEmployeeName.Text = response.result.employeeName.fullName.ToString();

                status.Select(response.result.status.ToString());

                //if (!string.IsNullOrEmpty(response.result.branchId))
                //    branchId.Select(response.result.branchId);

                //if (!response.result.effectiveDate.HasValue)
                //    effectiveDate.SelectedDate = DateTime.Now;

                this.approvalLoanWindow.Title = Resources.Common.EditWindowsTitle;
                this.approvalLoanWindow.Show();







            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        [DirectMethod]
        protected void leavePoPUP(object sender, DirectEventArgs e)
        {
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

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
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object

                    this.LeaveRecordForm.SetValues(response.result);
                    employeeName.Text = response.result.employeeName.fullName;

                    this.LeaveRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.LeaveRecordWindow.Show();
                    break;

                case "imgEdit":
                    leaveRequest1.Update(id);
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
            string status = e.ExtraParams["status"];
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

                LA.status = Convert.ToInt16(status);
                LA.notes = notes;
                LA.loanId = Convert.ToInt32(id);
                LA.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());

                PostRequest<LoanApproval> request = new PostRequest<LoanApproval>();
                request.entity = LA;


                PostResponse<LoanApproval> r = _loanService.ChildAddOrUpdate<LoanApproval>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + r.LogId : r.Summary).Show();
                    return;
                }
                else
                {

                    ApprovalLoanStore.Reload();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });

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
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            string id = e.ExtraParams["id"];
            LeaveRequest LV = JsonConvert.DeserializeObject<LeaveRequest>(obj);
            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 

                PostRequest<DashboardLeave> request = new PostRequest<DashboardLeave>();
                request.entity = new DashboardLeave();
                request.entity.leaveId = Convert.ToInt32(LV.recordId);
                request.entity.employeeId = Convert.ToInt32(userSessionEmployeeId.Text);
                request.entity.status = LV.status;
                if (!string.IsNullOrEmpty(LV.returnNotes))
                    request.entity.notes = LV.returnNotes;
                else
                    request.entity.notes = " ";


                PostResponse<DashboardLeave> r = _leaveManagementService.ChildAddOrUpdate<DashboardLeave>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + r.LogId : r.Summary).Show();
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
            string id = e.ExtraParams["id"];
            Time TI = JsonConvert.DeserializeObject<Time>(obj);
            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 

                PostRequest<Time> request = new PostRequest<Time>();
                request.entity = new Time();
                request.entity.employeeId = TI.employeeId;
                request.entity.dayId = TI.dayId;
                request.entity.timeCode = TI.timeCode;
                request.entity.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());
                request.entity.status = TI.status;
                request.entity.notes = TI.notes;

                PostResponse<Time> r = _timeAttendanceService.ChildAddOrUpdate<Time>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + r.LogId : r.Summary).Show();
                    return;
                }
                else
                {

                    TimeStore.Reload();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });

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
            ReportCompositeRequest req = new ReportCompositeRequest();
            DateRangeParameterSet range = new DateRangeParameterSet();
            range.DateFrom = new DateTime(1991, 1, 1);
            try
            {
                range.DateTo = CountDateTo.SelectedDate;
            }
            catch (Exception)
            {

                range.DateTo = DateTime.Now;
            }
            req.Add(range);
            ListResponse<RT103> resp = _reportsService.ChildGetAll<RT103>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
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

            DashboardRequest req = GetDashboardRequest();
            ListResponse<RetirementAge> resp = _systemService.ChildGetAll<RetirementAge>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            retirementAgeStore.DataSource = resp.Items;
            retirementAgeStore.DataBind();
        }
        protected void TermEndDate_ReadData(object sender, StoreReadDataEventArgs e)
        {

            DashboardRequest req = GetDashboardRequest();
            ListResponse<TermEndDate> resp = _systemService.ChildGetAll<TermEndDate>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            retirementAgeStore.DataSource = resp.Items;
            retirementAgeStore.DataBind();
        }

        protected void Checked_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<DashBoardCH> resp = _dashBoardService.ChildGetAll<DashBoardCH>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            CheckedStore.DataSource = resp.Items;
            CheckedStore.DataBind();
        }

        protected void Pending_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<DashBoardPE> resp = _dashBoardService.ChildGetAll<DashBoardPE>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            PendingStore.DataSource = resp.Items;
            PendingStore.DataBind();
        }
        protected void NoShowUp_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<DashboardNS> resp = _dashBoardService.ChildGetAll<DashboardNS>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            NoShowUpStore.DataSource = resp.Items;
            NoShowUpStore.DataBind();
        }
        protected void LeaveWithoutExcuse_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<DashboardLW> resp = _dashBoardService.ChildGetAll<DashboardLW>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            LeaveWithoutExcuseStore.DataSource = resp.Items;
            LeaveWithoutExcuseStore.DataBind();
        }
        protected void DayOff_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<DashBoardDO> resp = _dashBoardService.ChildGetAll<DashBoardDO>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            DayOffStore.DataSource = resp.Items;
            DayOffStore.DataBind();
        }

        protected void Leave_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<DashBoardLE> resp = _dashBoardService.ChildGetAll<DashBoardLE>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
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
            try
            {
                DashboardRequest req = GetDashboardRequest();
                TimeVariationListRequest TVReq = new TimeVariationListRequest();
                TVReq.BranchId = req.BranchId;
                TVReq.DepartmentId = req.DepartmentId;
                TVReq.DivisionId = req.DivisionId;
                TVReq.EsId = req.EsId;
                TVReq.timeVariationType = CurrentTimeVariationType.Text;
                TVReq.fromDayId = DateTime.Now;
                TVReq.toDayId = DateTime.Now;
                TVReq.PositionId = req.PositionId;



                ListResponse<DashBoardTimeVariation> resp = _dashBoardService.ChildGetAll<DashBoardTimeVariation>(TVReq);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
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
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error,exp.Message).Show();
            }

        }
    }
}
