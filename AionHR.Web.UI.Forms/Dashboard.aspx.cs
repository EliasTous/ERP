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
                    FillDepartment();

                    FillBranch();

                    FillPosition();

                    FillDivision();

                    FillStatus();

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

        private void FillPosition()
        {
            ListRequest positionRequest = new ListRequest();
            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(positionRequest);
            positionStore.DataSource = resp.Items;
            positionStore.DataBind();
        }

        private void FillDepartment()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
        }

        private void FillDivision()
        {
            ListRequest divisionReq = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(divisionReq);
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();
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
            ListResponse<ActiveAbsence> ABs = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
            ListResponse<ActiveLate> ALs = _timeAttendanceService.ChildGetAll<ActiveLate>(r);
            ListResponse<ActiveLeave> Leaves = _timeAttendanceService.ChildGetAll<ActiveLeave>(r);
            ListResponse<MissedPunch> MPs = _timeAttendanceService.ChildGetAll<MissedPunch>(r);
            ListResponse<ActiveOut> AOs = _timeAttendanceService.ChildGetAll<ActiveOut>(r);
            EmployeeCountRequest req = GetEmployeeCountRequest();
            RecordResponse < EmployeeCount > count = _employeeService.ChildGetRecord<EmployeeCount>(req);
            if (!ACs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, ACs.Summary).Show();
                return;
            }
            List<object> b = new List<object>();
            b.Add(new { Name = GetLocalResourceObject("ActiveGridTitle").ToString(), Count = ACs.Items.Count });
            b.Add(new { Name = GetLocalResourceObject("OutGridTitle").ToString(), Count = AOs.Items.Count });
            b.Add(new { Name = GetLocalResourceObject("MissingPunchesGridTitle").ToString(), Count = MPs.Items.Count });
            b.Add(new { Name = GetLocalResourceObject("LeavesGridTitle").ToString(), Count = Leaves.Items.Count });
            b.Add(new { Name = GetLocalResourceObject("LatenessGridTitle").ToString(), Count = ALs.Items.Count });
            

            activeStore.DataSource = ACs.Items;
            activeStore.DataBind();
            activeCount.Text = ACs.Items.Count.ToString();
           

            missingPunchesStore.DataSource = MPs.Items;
            missingPunchesStore.DataBind();

            leavesStore.DataSource = Leaves.Items;
            leavesStore.DataBind();

            latenessStore.DataSource = ALs.Items;
            latenessStore.DataBind();

            absenseStore.DataSource = ABs.Items;
            absenseStore.DataBind();

            int x = ALs.Items.Count;
            X.Call("lateChart", x,count.result.count);
            int y = ABs.Items.Count;
            X.Call("absentChart", y, count.result.count);
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
            int annev = dashoard.Items.Where(x => x.itemId == 32).ToList()[0].count;
            int comp = dashoard.Items.Where(x => x.itemId == 61).ToList()[0].count;
            int empRW = dashoard.Items.Where(x => x.itemId == 62).ToList()[0].count;
            int scr = dashoard.Items.Where(x => x.itemId == 31).ToList()[0].count;
            int prob = dashoard.Items.Where(x => x.itemId == 32).ToList()[0].count;
            annversaries.Text = annev.ToString();
            birthdays.Text = birth.ToString();
            companyRW.Text = comp.ToString();
            salaryChange.Text = scr.ToString();
            probation.Text = prob.ToString();
            employeeRW.Text = empRW.ToString();
            //X.Call("alerts", annev, annevTotal, birthdays, birthdaysTotal, empRW, empRWTotal, compRW, compRWTotal, scr, scrTotal, prob, probTotal);

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

            ListResponse<ActiveLeave> Leaves = _timeAttendanceService.ChildGetAll<ActiveLeave>(r);
            if (!Leaves.Success)
            {
                X.Msg.Alert(Resources.Common.Error, Leaves.Summary).Show();
                return;
            }
            leavesStore.DataSource = Leaves.Items;
            //List<ActiveLeave> leaves = new List<ActiveLeave>();
            //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });

            leaveCount.Text = "12";
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

            int intResult;

            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0" && int.TryParse(branchId.Value.ToString(), out intResult))
            {
                req.BranchId = intResult;



            }
            else
            {
                req.BranchId = 0;

            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0" && int.TryParse(departmentId.Value.ToString(), out intResult))
            {
                req.DepartmentId = intResult;


            }
            else
            {
                req.DepartmentId = 0;

            }
            if (!string.IsNullOrEmpty(ComboBox1.Text) && ComboBox1.Value.ToString() != "0" && int.TryParse(ComboBox1.Value.ToString(), out intResult))
            {
                req.PositionId = intResult;


            }
            else
            {
                req.PositionId = 0;

            }

            if (!string.IsNullOrEmpty(divisionId.Text) && divisionId.Value.ToString() != "0" && int.TryParse(divisionId.Value.ToString(), out intResult))
            {
                req.DivisionId = intResult;


            }
            else
            {
                req.DivisionId = 0;

            }

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

            int intResult;

            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0" && int.TryParse(branchId.Value.ToString(), out intResult))
            {
                req.BranchId = intResult;



            }
            else
            {
                req.BranchId = 0;

            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0" && int.TryParse(departmentId.Value.ToString(), out intResult))
            {
                req.DepartmentId = intResult;


            }
            else
            {
                req.DepartmentId = 0;

            }
            if (!string.IsNullOrEmpty(ComboBox1.Text) && ComboBox1.Value.ToString() != "0" && int.TryParse(ComboBox1.Value.ToString(), out intResult))
            {
                req.PositionId = intResult;


            }
            else
            {
                req.PositionId = 0;

            }

            if (!string.IsNullOrEmpty(divisionId.Text) && divisionId.Value.ToString() != "0" && int.TryParse(divisionId.Value.ToString(), out intResult))
            {
                req.DivisionId = intResult;


            }
            else
            {
                req.DivisionId = 0;

            }

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
            ListResponse<Model.TaskManagement.Task> resp = _taskManagementService.GetAll< Model.TaskManagement.Task>(req);
            if(!resp.Success)
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

            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
            {
                req.BranchId = Convert.ToInt32(branchId.Value);
            }
            else
            {
                req.BranchId = 0;
            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
            {
                req.DepartmentId = Convert.ToInt32(departmentId.Value);
            }
            else
            {
                req.DepartmentId = 0;
            }

            
            
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

            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
            {
                req.BranchId = Convert.ToInt32(branchId.Value);
            }
            else
            {
                req.BranchId = 0;
            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
            {
                req.DepartmentId = Convert.ToInt32(departmentId.Value);
            }
            else
            {
                req.DepartmentId = 0;
            }



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

            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
            {
                req.BranchId = Convert.ToInt32(branchId.Value);
            }
            else
            {
                req.BranchId = 0;
            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
            {
                req.DepartmentId = Convert.ToInt32(departmentId.Value);
            }
            else
            {
                req.DepartmentId = 0;
            }



            


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

            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0" && int.TryParse(branchId.Value.ToString(), out intResult))
            {
                req.BranchId = intResult;



            }
            else
            {
                req.BranchId = 0;

            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0" && int.TryParse(departmentId.Value.ToString(), out intResult))
            {
                req.DepartmentId = intResult;


            }
            else
            {
                req.DepartmentId = 0;

            }
            if (!string.IsNullOrEmpty(ComboBox1.Text) && ComboBox1.Value.ToString() != "0" && int.TryParse(ComboBox1.Value.ToString(), out intResult))
            {
                req.PositionId = intResult;


            }
            else
            {
                req.PositionId = 0;

            }

            if (!string.IsNullOrEmpty(divisionId.Text) && divisionId.Value.ToString() != "0" && int.TryParse(divisionId.Value.ToString(), out intResult))
            {
                req.DivisionId = intResult;


            }
            else
            {
                req.DivisionId = 0;

            }

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

        protected void InChartStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<object> b = new List<object>();
            b.Add(new { Name = "On Time", Count = activeCount.Text });
            b.Add(new { Name = "Late", Count = latensessCount.Text });
            InChartStore.DataSource = b;
            InChartStore.DataBind();
        }

        protected void OutChartStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<object> b = new List<object>();
            b.Add(new { Name =  GetLocalResourceObject("LeavesGridTitle").ToString(), Count = leaveCount.Text });
            b.Add(new { Name = GetLocalResourceObject("AbsenseGridTitle").ToString(), Count = abbsenseCount.Text });
            OutChartStore.DataSource = b;
            OutChartStore.DataBind();
        }

        protected void AlertsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            
        }

        protected void LoansStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            LoanManagementListRequest req = GetLoanManagementRequest();
            ListResponse<Loan> loans = _loanService.GetAll<Loan>(req);
            List<Loan> OpenLoans = loans.Items.Where(t => t.status == 2).ToList();
            LoansStore.DataSource = OpenLoans;
            LoansStore.DataBind();
            int x = OpenLoans.Count;
            X.Call("loansChart", x, x + (10-(x%10)));
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
            List<Employee> emps = new List<Employee>();
            emps.Add(new Employee() { recordId = "20", name = new EmployeeName() { fullName = "Issa Mouawad" }, birthDate = new DateTime(2015, 6, 19) });
            BirthdaysStore.DataSource = emps;
            BirthdaysStore.DataBind();
        }

        protected void AnniversaryStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<Employee> emps = new List<Employee>();
            emps.Add(new Employee() { recordId = "20", name = new EmployeeName() { fullName = "Issa Mouawad" }, hireDate = new DateTime(2015, 6, 19) });
            AnniversaryStore.DataSource = emps;
            AnniversaryStore.DataBind();
        }

        protected void CompanyRightToWorkStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<CompanyRightToWork> emps = new List<CompanyRightToWork>();
            emps.Add(new CompanyRightToWork() { recordId = "20", documentRef = "some Document", expiryDate = new DateTime(2015, 6, 19) });
            CompanyRightToWorkStore.DataSource = emps;
            CompanyRightToWorkStore.DataBind();
        }

        protected void EmployeeRightToWorkStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<EmployeeRightToWork> emps = new List<EmployeeRightToWork>();
            emps.Add(new EmployeeRightToWork() { recordId = "20", employeeName=new EmployeeName() { fullName = "Issa Mouawad" }, documentRef = "some Document", expiryDate = new DateTime(2015, 6, 19) });
            EmployeeRightToWorkStore.DataSource = emps;
            EmployeeRightToWorkStore.DataBind();
        }

        protected void SCRStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<EmployeeSalary> emps = new List<EmployeeSalary>();
            emps.Add(new EmployeeSalary() { recordId = "20", employeeName = new EmployeeName() { fullName = "Issa Mouawad" }, currencyRef="$", finalAmount=1400, effectiveDate=new DateTime(2017,6,10) });
            SCRStore.DataSource = emps;
            SCRStore.DataBind();
        }

        protected void ProbationStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<HireInfo> emps = new List<HireInfo>();
            emps.Add(new HireInfo() { employeeId="2",  employeeName = new EmployeeName() { fullName = "Issa Mouawad" }, probationEndDate=new DateTime(2017,6,10) });
            ProbationStore.DataSource = emps;
            ProbationStore.DataBind();


        }
    }
}