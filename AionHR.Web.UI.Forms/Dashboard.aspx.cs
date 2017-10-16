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

                

                    FillStatus();
                    Store2.Reload();

                    //outStore.Reload();
                    //activeStore.Reload();
                    //latenessStore.Reload();
                    //leavesStore.Reload();
                    //missingPunchesStore.Reload();
                    //checkMontierStore.Reload();
                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                    DateColumn1.Format = DateColumn2.Format = DateColumn3.Format = DateColumn4.Format = _systemService.SessionHelper.GetDateformat();
                }
                catch { }
            }

        }

      

        private void FillStatus()
        {
            ListRequest statusReq = new ListRequest();
            ListResponse<EmploymentStatus> resp = _employeeService.ChildGetAll<EmploymentStatus>(statusReq);
            statusStore.DataSource = resp.Items;
            statusStore.DataBind();
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

            int x = ALs.Items.Count;
            X.Call("lateChart", x, count.result.count);
            //int y = ABs.Items.Count;
            //X.Call("absentChart", y, count.result.count);
            int z = ACs.Items.Count;
            X.Call("activeChart", z, count.result.count);
            BindAlerts();
        }

        private void BindAlerts()
        {
            ListRequest req = new ListRequest();
            ListResponse<DashboardItem> dashoard = _systemService.ChildGetAll<DashboardItem>(req);
            if (!dashoard.Success)
            {
                X.Msg.Alert(Resources.Common.Error, dashoard.Summary).Show();
                return;
            }

            int birth = dashoard.Items.Where(x => x.itemId == 43).ToList()[0].count;
            int annev = dashoard.Items.Where(x => x.itemId == 53).ToList()[0].count;
            int comp = dashoard.Items.Where(x => x.itemId == 61).ToList()[0].count;
            int empRW = dashoard.Items.Where(x => x.itemId == 62).ToList()[0].count;
            int scr = dashoard.Items.Where(x => x.itemId == 31).ToList()[0].count;
            int prob = dashoard.Items.Where(x => x.itemId == 32).ToList()[0].count;
            var l1 = dashoard.Items.Where(x => x.itemId == 71).ToList();
            int total = 0, paid = 0;
            if (l1.Count > 0)
                total = l1[0].count;
            var l2 = dashoard.Items.Where(x => x.itemId == 72).ToList();
            if (l2.Count > 0)
                paid = l2[0].count;
            annversaries.Text = annev.ToString();
            birthdays.Text = birth.ToString();
            companyRW.Text = comp.ToString();
            salaryChange.Text = scr.ToString();
            probation.Text = prob.ToString();
            employeeRW.Text = empRW.ToString();
            totalLoansLbl.Text = total.ToString("N0");
            deductedLoansLbl.Text = paid.ToString("N0");
            //X.Call("alerts", annev, annevTotal, birthdays, birthdaysTotal, empRW, empRWTotal, compRW, compRWTotal, scr, scrTotal, prob, probTotal);
            List <object> objs = new List<object>();
            objs.Add(new { Count = dashoard.Items.Where(x => x.itemId == 110).ToList()[0].count, emps = GetLocalResourceObject("Paid").ToString() });
            objs.Add(new { Count = dashoard.Items.Where(x => x.itemId == 111).ToList()[0].count, emps = GetLocalResourceObject("Unpaid").ToString() });
            objs.Add(new { Count = dashoard.Items.Where(x => x.itemId == 112).ToList()[0].count, emps = GetLocalResourceObject("Absent").ToString() });

            AbsentLeaveStore.DataSource = objs;
            AbsentLeaveStore.DataBind();
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
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();

            ListResponse<ActiveAbsence> ABs = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
            //List<ActiveAbsence> a = new List<ActiveAbsence>();
            //a.Add(new ActiveAbsence() { branchName = "here", positionName = "someone", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "rabie" } });
            if (!ABs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, ABs.Summary).Show();
                return;
            }
            absenseStore.DataSource = ABs.Items;
            absenseStore.DataBind();
            abbsenseCount.Text = "10";
        }

        protected void latenessStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();

            ListResponse<ActiveLate> ALs = _timeAttendanceService.ChildGetAll<ActiveLate>(r);
            if (!ALs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, ALs.Summary).Show();
                return;
            }
            latenessStore.DataSource = ALs.Items;
            latenessStore.DataBind();
            latensessCount.Text = "7";
        }

        protected void leavesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();
            r.DayStatus = 2;
            ListResponse<ActiveLeave> Leaves = _timeAttendanceService.ChildGetAll<ActiveLeave>(r);
            if (!Leaves.Success)
            {
                X.Msg.Alert(Resources.Common.Error, Leaves.Summary).Show();
                return;
            }
            leavesStore.DataSource = Leaves.Items;
            //List<ActiveLeave> leaves = new List<ActiveLeave>();
            //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });

            
            leavesStore.DataBind();
        }

        protected void missingPunchesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();

            ListResponse<MissedPunch> ACs = _timeAttendanceService.ChildGetAll<MissedPunch>(r);
            if (!ACs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, ACs.Summary).Show();
                return;
            }

            //List<MissedPunch> s = new List<MissedPunch>();
            //s.Add(new MissedPunch() { date = DateTime.Now, employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "issa" }, missedIn = true, missedOut = false, recordId = "1", time = "08:30" });
            missingPunchesStore.DataSource = ACs.Items;
            missingPunchesStore.DataBind();
            mpCount.Text = "6";
        }

        protected void checkMontierStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();

            ListResponse<CheckMonitor> CMs = _timeAttendanceService.ChildGetAll<CheckMonitor>(r);
            if (!CMs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, CMs.Summary).Show();
                return;
            }
            foreach (var item in CMs.Items)
            {
                item.figureTitle = GetLocalResourceObject(item.figureId.ToString()).ToString();
                item.rate = item.rate / 100;
            }

            //checkMontierStore.DataSource = CMs.Items;
            //checkMontierStore.DataBind();

        }

        protected void outStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();

            ListResponse<ActiveOut> AOs = _timeAttendanceService.ChildGetAll<ActiveOut>(r);
            if (!AOs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, AOs.Summary).Show();
                return;
            }
            //outStore.DataSource = AOs.Items;

            //outCount.Text = "17";
            //outStore.DataBind();

        }
        [DirectMethod]
        public void RefreshAll()
        {
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();

            ListResponse<ActiveAbsence> ABs = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
            absenseStore.DataSource = ABs.Items;
            absenseStore.DataBind();
        }
        private ActiveAttendanceRequest GetActiveAttendanceRequest()
        {
            ActiveAttendanceRequest req = new ActiveAttendanceRequest();
            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue?d.BranchId.Value:0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
            req.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value : 0;
            req.PositionId = d.PositionId.HasValue ? d.PositionId.Value : 0;
            int intResult;

            //if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0" && int.TryParse(branchId.Value.ToString(), out intResult))
            //{
            //    req.BranchId = intResult;



            //}
            //else
            //{
            //    req.BranchId = 0;

            //}

            //if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0" && int.TryParse(departmentId.Value.ToString(), out intResult))
            //{
            //    req.DepartmentId = intResult;


            //}
            //else
            //{
            //    req.DepartmentId = 0;

            //}
            //if (!string.IsNullOrEmpty(ComboBox1.Text) && ComboBox1.Value.ToString() != "0" && int.TryParse(ComboBox1.Value.ToString(), out intResult))
            //{
            //    req.PositionId = intResult;


            //}
            //else
            //{
            //    req.PositionId = 0;

            //}

            //if (!string.IsNullOrEmpty(divisionId.Text) && divisionId.Value.ToString() != "0" && int.TryParse(divisionId.Value.ToString(), out intResult))
            //{
            //    req.DivisionId = intResult;


            //}
            //else
            //{
            //    req.DivisionId = 0;

            //}

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
            TaskManagementListRequest req = GetTaskManagementRequest();
            ListResponse<Model.TaskManagement.Task> resp = _taskManagementService.GetAll<Model.TaskManagement.Task>(req);
            if (!resp.Success)
            {
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


            return req;
        }

        private LeaveRequestListRequest GetLeaveManagementRequest()
        {
            LeaveRequestListRequest req = new LeaveRequestListRequest();

            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;





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

            

            if (!string.IsNullOrEmpty(esId.Text) && esId.Value.ToString() != "0" && int.TryParse(esId.Value.ToString(), out intResult))
            {
                req.EsId = intResult;


            }
            else
            {
                req.EsId = 0;

            }




            return req;
        }

        protected void Unnamed_DataBinding(object sender, EventArgs e)
        {

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
            LoanManagementListRequest req = GetLoanManagementRequest();
            ListResponse<Loan> loans = _loanService.GetAll<Loan>(req);
            List<Loan> OpenLoans = loans.Items.Where(t => t.status == 2).ToList();
            LoansStore.DataSource = OpenLoans;
            LoansStore.DataBind();
            int x = OpenLoans.Count;
            X.Call("loansChart", x, x + (10 - (x % 10)));
        }

        protected void LeaveRequestsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            LeaveRequestListRequest req = GetLeaveManagementRequest();
            ListResponse<LeaveRequest> loans = _leaveManagementService.ChildGetAll<LeaveRequest>(req);
            List<LeaveRequest> OpenLoans = loans.Items.Where(t => t.status == 1).ToList();
            LeaveRequestsStore.DataSource = OpenLoans;
            LeaveRequestsStore.DataBind();

        }

        protected void BirthdaysStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<EmployeeBirthday> resp = _systemService.ChildGetAll<EmployeeBirthday>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            BirthdaysStore.DataSource = resp.Items;
            BirthdaysStore.DataBind();
        }

        protected void AnniversaryStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            DashboardRequest req = GetDashboardRequest();
            ListResponse<WorkAnniversary> resp = _systemService.ChildGetAll<WorkAnniversary>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            AnniversaryStore.DataSource = resp.Items;
            AnniversaryStore.DataBind();
        }

        protected void CompanyRightToWorkStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<CompanyRTW> resp = _systemService.ChildGetAll<CompanyRTW>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            CompanyRightToWorkStore.DataSource = resp.Items;
            CompanyRightToWorkStore.DataBind();
        }

        protected void EmployeeRightToWorkStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<EmpRTW> resp = _systemService.ChildGetAll<EmpRTW>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            EmployeeRightToWorkStore.DataSource = resp.Items;
            EmployeeRightToWorkStore.DataBind();
        }

        protected void SCRStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<SalaryChange> resp = _systemService.ChildGetAll<SalaryChange>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            SCRStore.DataSource = resp.Items;
            SCRStore.DataBind();
        }

        protected void ProbationStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = GetDashboardRequest();
            ListResponse<ProbationEnd> resp = _systemService.ChildGetAll<ProbationEnd>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            ProbationStore.DataSource = resp.Items;
            ProbationStore.DataBind();


        }


        protected void departments2Count_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ListRequest req = new ListRequest();
            ListResponse<DepartmentActivity> resp = _systemService.ChildGetAll<DepartmentActivity>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
        
            Store1.DataSource = resp.Items;
            Store1.DataBind();
            X.Call("fixWidth", resp.Items.Count);
        }

        protected void CompletedLoansStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            LoanManagementListRequest req = new LoanManagementListRequest();
            req = GetLoanManagementRequest();
            req.Status = 2;
            ListResponse<Loan> resp = _loanService.GetAll<Loan>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            CompletedLoansStore.DataSource = resp.Items;
            CompletedLoansStore.DataBind();
        }

        protected void totalLoansStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            LoanManagementListRequest req = new LoanManagementListRequest();
            req = GetLoanManagementRequest();
            req.Status = 1;
            ListResponse<Loan> resp = _loanService.GetAll<Loan>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            totalLoansStore.DataSource = resp.Items;
            totalLoansStore.DataBind();
        }

        protected void OnLeaveStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

        }

        protected void UnpaidLeavesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ActiveAttendanceRequest r = GetActiveAttendanceRequest();
            r.DayStatus = 4;
            ListResponse<ActiveLeave> Leaves = _timeAttendanceService.ChildGetAll<ActiveLeave>(r);
            if (!Leaves.Success)
            {
                X.Msg.Alert(Resources.Common.Error, Leaves.Summary).Show();
                return;
            }
            UnpaidLeavesStore.DataSource = Leaves.Items;
            //List<ActiveLeave> leaves = new List<ActiveLeave>();
            //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


            UnpaidLeavesStore.DataBind();
        }

        protected void LocalRate_ReadData(object sender, StoreReadDataEventArgs e)
        {
            DashboardRequest req = LocalRateRequest();
            ListResponse<LocalRate> resp = _systemService.ChildGetAll<LocalRate>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }

            Store2.DataSource = resp.Items;
            Store2.DataBind();
            X.Call("fixWidth", resp.Items.Count);

        }
        private DashboardRequest LocalRateRequest()
        {
            DashboardRequest req = new DashboardRequest();
            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
            req.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value : 0;
            req.PositionId = d.PositionId.HasValue ? d.PositionId.Value : 0;
            int intResult;

          
            if (!string.IsNullOrEmpty(esId.SelectedItem.Value))
            {
                req.EsId = Convert.ToInt32(esId.SelectedItem.Value);
                
            }
            else
            {
                req.EsId = 0;

            }



            return req;
        }
    }
}