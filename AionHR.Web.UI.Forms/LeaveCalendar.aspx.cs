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
using AionHR.Infrastructure.Session;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Model.LeaveManagement;
using AionHR.Model.TimeAttendance;

namespace AionHR.Web.UI.Forms
{
    public partial class LeaveCalendar : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
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
        protected DataTable getData(string month, string year)
        {
            LeaveRequestListRequest req = GetFilteredRequest();

            ListResponse<LeaveRequest> resp = _leaveManagementService.ChildGetAll<LeaveRequest>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new DataTable();
            }
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("start", typeof(DateTime));
            dt.Columns.Add("end", typeof(DateTime));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("resource", typeof(string));
            dt.Columns.Add("backColor", typeof(string));

            DataRow dr;

            DateTime startDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
            DateTime endDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month)));
            foreach (var item in resp.Items)
            {
                
                
                if (item.startDate > endDate || item.endDate < startDate)
                    continue;
                DayPilotScheduler1.Resources.Add(new DayPilot.Web.Ui.Resource(item.employeeName.fullName, item.employeeId.ToString()));
                dr = dt.NewRow();
                dr["id"] = item.recordId;
                dr["start"] = item.startDate;
                dr["end"] = item.endDate.AddDays(1) ;
                dr["name"] = item.employeeName.fullName;
                dr["resource"] = item.employeeId;
                switch (item.status)
                {
                    case 0:
                        dr["backColor"] = "#00ff00";
                        break;
                    case 1:
                        dr["backColor"] = "#ff0000";
                        break;
                    case 2:
                        dr["backColor"] = "#0000ff";
                        break;
                }

                dt.Rows.Add(dr);
            }


            return dt;

        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                FillBranch();
                FillDepartment();

                //dayId.SelectedDate = DateTime.Today;
                CurrentMonth.Text = DateTime.Today.Month.ToString();
                CurrentYear.Text = DateTime.Today.Year.ToString();
                monthLbl.Text = DateTime.Today.ToString("MMMM");
                yearLbl.Text = DateTime.Today.ToString("yyyy");
                DayPilotScheduler1.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DayPilotScheduler1.Days = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
                DayPilotScheduler1.DataStartField = "start";
                DayPilotScheduler1.DataEndField = "end";
                DayPilotScheduler1.DataIdField = "id";
                DayPilotScheduler1.DataTextField = "name";
                DayPilotScheduler1.DataResourceField = "resource";
                DateFormat.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                UpdateCal(CurrentMonth.Text, CurrentYear.Text, Viewport1.Width.Value.ToString());

            }


        }



        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }

        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            BasicInfoTab.Reset();

            FillLeaveType();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }

        private void HideShowButtons()
        {

        }


        /// <summary>
        /// hiding uncessary column in the grid. 
        /// </summary>
        private void HideShowColumns()
        {
            // this.colAttach.Visible = false;
        }


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                // this.Viewport1.RTL = true;

            }
        }

        private LeaveRequestListRequest GetFilteredRequest()
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



            if (!string.IsNullOrEmpty(employeeFilter.Text) && employeeFilter.Value.ToString() != "0")
            {
                req.EmployeeId = Convert.ToInt32(employeeFilter.Value);


            }
            else
            {
                req.EmployeeId = 0;

            }

            if (!string.IsNullOrEmpty(includeOpen.Text))
            {
                req.OpenRequests = Convert.ToInt32(includeOpen.Value);


            }
            else
            {
                req.OpenRequests = 3;

            }

            req.Size = "50";
            req.StartAt = "1";
            req.SortBy = "firstName";

            return req;
        }


        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>

        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            ////GEtting the filter from the page

            //AttendnanceDayListRequest req = GetAttendanceDayRequest();

            //ListResponse<AttendanceDay> daysResponse = _timeAttendanceService.ChildGetAll<AttendanceDay>(req);
            //if (!daysResponse.Success)
            //{
            //    X.Msg.Alert(Resources.Common.Error, daysResponse.Summary).Show();
            //    return;
            //}
            //int total = daysResponse.Items.Sum(s => s.netOL);
            //X.Call("setTotal", total);
            //this.total.Text = total.ToString();
            //var data = daysResponse.Items;
            //if (daysResponse.Items != null)
            //{
            //    this.Store1.DataSource = daysResponse.Items;
            //    this.Store1.DataBind();
            //}
            //e.Total = daysResponse.count;
        }


        private void FillDepartment()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
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
        public void UpdateCal(string month, string year,string parentWidth)
        {
            try
            {
                DayPilotScheduler1.StartDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
                DayPilotScheduler1.Days = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month));
                DayPilotScheduler1.DataStartField = "start";
                DayPilotScheduler1.DataEndField = "end";
                DayPilotScheduler1.DataIdField = "id";
                DayPilotScheduler1.DataTextField = "name";
                DayPilotScheduler1.DataResourceField = "resource";
                DayPilotScheduler1.DataSource = getData(month, year);
                
                DayPilotScheduler1.DataBind();
                int widthInt = Convert.ToInt32(parentWidth);

                DayPilotScheduler1.CellWidth = ((int)widthInt / DayPilotScheduler1.Days -3);
                
                DayPilotScheduler1.Update();
                
                schedulerHolder.UpdateLayout();
          

            }
            catch (Exception exp) { }
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
        [DirectMethod]
        public object FillEmployeeFilter(string action, Dictionary<string, object> extraParams)
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

        [DirectMethod]
        public void HandleClick(string id)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = id;
            CurrentLeave.Text = r.RecordID;

            RecordResponse<LeaveRequest> response = _leaveManagementService.ChildGetRecord<LeaveRequest>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }
            //Step 2 : call setvalues with the retrieved object
            this.BasicInfoTab.SetValues(response.result);


            FillLeaveType();
            ltId.Select(response.result.ltId.ToString());
            StoredLeaveChanged.Text = "0";
            if (response.result.employeeId != 0)
            {

                employeeId.GetStore().Add(new object[]
                   {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName.fullName
                                }
                   });
                employeeId.SetValue(response.result.employeeId);

            }
            LeaveDayListRequest req = new LeaveDayListRequest();
            req.LeaveId = CurrentLeave.Text;
            ListResponse<LeaveDay> resp = _leaveManagementService.ChildGetAll<LeaveDay>(req);
            if (!resp.Success)
            {

            }

            leaveDaysStore.DataSource = resp.Items;
            resp.Items.ForEach(x => x.dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek);
            leaveDaysStore.DataBind();
            LeaveChanged.Text = "0";

            panelRecordDetails.ActiveTabIndex = 0;
            this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
            this.EditRecordWindow.Show();
        }
        protected void DayPilotScheduler1_EventClick(object sender, DayPilot.Web.Ui.Events.EventClickEventArgs e)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = e.Id;


            RecordResponse<LeaveRequest> response = _leaveManagementService.ChildGetRecord<LeaveRequest>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }
            //Step 2 : call setvalues with the retrieved object
            this.BasicInfoTab.SetValues(response.result);

            FillLeaveType();
            ltId.Select(response.result.ltId.ToString());
            if (response.result.employeeId != 0)
            {

                employeeId.GetStore().Add(new object[]
                   {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName.fullName
                                }
                   });
                employeeId.SetValue(response.result.employeeId);

            }

            this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
            this.EditRecordWindow.Show();


        }

        public void FillLeaveType()
        {

            ListRequest req = new ListRequest();

            ListResponse<LeaveType> response = _leaveManagementService.ChildGetAll<LeaveType>(req);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }
            ltStore.DataSource = response.Items;
            ltStore.DataBind();

        }

        protected void addLeaveType(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(ltId.Text))
                return;
            LeaveType dept = new LeaveType();
            dept.name = ltId.Text;

            PostRequest<LeaveType> depReq = new PostRequest<LeaveType>();
            depReq.entity = dept;
            PostResponse<LeaveType> response = _leaveManagementService.ChildAddOrUpdate<LeaveType>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillLeaveType();
                ltId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }
        private List<LeaveDay> GenerateLeaveDays(string encoded)
        {
            List<LeaveDay> days = JsonConvert.DeserializeObject<List<LeaveDay>>(encoded);

            return days;
        }
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

            string obj = e.ExtraParams["values"];
            LeaveRequest b = JsonConvert.DeserializeObject<LeaveRequest>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null
            if (!b.isPaid.HasValue)
                b.isPaid = false;
            b.employeeName = new EmployeeName();
            if (employeeId.SelectedItem != null)

                b.employeeName.fullName = employeeId.SelectedItem.Text;
            if (ltId.SelectedItem != null)

                b.ltName = ltId.SelectedItem.Text;

            List<LeaveDay> days = GenerateLeaveDays(e.ExtraParams["days"]);
            if(days.Count==0)
            {
                days = GenerateDefaultLeaveDays();
            }
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<LeaveRequest> request = new PostRequest<LeaveRequest>();

                    request.entity = b;
                    PostResponse<LeaveRequest> r = _leaveManagementService.ChildAddOrUpdate<LeaveRequest>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                        return;
                    }
                    else
                    {
                        days.ForEach(d => d.leaveId = Convert.ToInt32(b.recordId));
                        AddDays(days);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        UpdateCal(CurrentMonth.Text, CurrentYear.Text, Viewport1.Width.Value.ToString());
                        this.EditRecordWindow.Close();




                    }
                }
                catch (Exception ex)
                {
                    //Error exception displaying a messsage box
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                }


            }
            else
            {
                //Update Mode

                try
                {
                    //getting the id of the record
                    PostRequest<LeaveRequest> request = new PostRequest<LeaveRequest>();
                    request.entity = b;
                    PostResponse<LeaveRequest> r = _leaveManagementService.ChildAddOrUpdate<LeaveRequest>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                        return;
                    }
                    else
                    {


                        var deleteDesponse = _leaveManagementService.DeleteLeaveDays(Convert.ToInt32(b.recordId));
                        if (!deleteDesponse.Success)//it maybe another check
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, deleteDesponse.Summary).Show();
                            return;
                        }
                        days.ForEach(x => x.leaveId = Convert.ToInt32(b.recordId));
                        AddDays(days);
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        UpdateCal(CurrentMonth.Text, CurrentYear.Text, Viewport1.Width.ToString());
                        this.EditRecordWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }
        private List<LeaveDay> GenerateDefaultLeaveDays()
        {
            string startDay = startDate.SelectedDate.ToString("yyyyMMdd");
            string endDay = endDate.SelectedDate.ToString("yyyyMMdd");

            LeaveCalendarDayListRequest req = new LeaveCalendarDayListRequest();
            req.StartDayId = startDay;
            req.EndDayId = endDay;
            req.IsWorkingDay = true;
            int bulk;

            req.CaId = GetEmployeeCalendar(employeeId.Value.ToString()).ToString();
            ListResponse<LeaveCalendarDay> days = _timeAttendanceService.ChildGetAll<LeaveCalendarDay>(req);

            List<LeaveDay> leaveDays = new List<LeaveDay>();
            days.Items.ForEach(x => leaveDays.Add(new LeaveDay() { dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek, dayId = x.dayId, workingHours = x.workingHours, leaveHours = x.workingHours }));
            return leaveDays;
        }
        protected void DayPilotScheduler1_BeforeEventRender(object sender, DayPilot.Web.Ui.Events.Scheduler.BeforeEventRenderEventArgs e)
        {
            e.BackgroundColor = (string)e.DataItem["backColor"];

        }

        [DirectMethod]
        public void FixLayout(string width)
        {
            int widthInt = Convert.ToInt32(width);

            DayPilotScheduler1.CellWidth = ((int)widthInt / 40)+20;
            DayPilotScheduler1.Update();
        }


        private int GetEmployeeCalendar(string empId)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = empId;
            RecordResponse<Employee> resp = _employeeService.Get<Employee>(req);
            if (!resp.Success)
            {

            }
            return resp.result.caId.Value;
        }
        [DirectMethod]
        public void MarkLeaveChanged()
        {
            LeaveChanged.Text = "1";
            if (startDate.SelectedDate == DateTime.MinValue || endDate.SelectedDate == DateTime.MinValue)
            {

                return;
            }
            string startDay = startDate.SelectedDate.ToString("yyyyMMdd");
            string endDay = endDate.SelectedDate.ToString("yyyyMMdd");

            LeaveCalendarDayListRequest req = new LeaveCalendarDayListRequest();
            req.StartDayId = startDay;
            req.EndDayId = endDay;
            req.IsWorkingDay = true;
            int bulk;
            if (string.IsNullOrEmpty(employeeId.Value.ToString()) || !int.TryParse(employeeId.Value.ToString(), out bulk))
            {

                return;
            }
            req.CaId = GetEmployeeCalendar(employeeId.Value.ToString()).ToString();
            ListResponse<LeaveCalendarDay> days = _timeAttendanceService.ChildGetAll<LeaveCalendarDay>(req);
            if (!days.Success)
            {

            }
            List<LeaveDay> leaveDays = new List<LeaveDay>();
            days.Items.ForEach(x => leaveDays.Add(new LeaveDay() { dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek, dayId = x.dayId, workingHours = x.workingHours, leaveHours = x.workingHours }));
            leaveDaysStore.DataSource = leaveDays;
            leaveDaysStore.DataBind();
        }
        [DirectMethod]
        public void Unnamed_Event()
        {
            if (panelRecordDetails.ActiveTabIndex == 1)
            {


                if (startDate.SelectedDate == DateTime.MinValue || endDate.SelectedDate == DateTime.MinValue)
                {
                    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorSelectDate")).Show();
                    panelRecordDetails.ActiveTabIndex = 0;
                    return;
                }
                int bulk;
                if (string.IsNullOrEmpty(employeeId.Value.ToString()) || !int.TryParse(employeeId.Value.ToString(), out bulk))
                {
                    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorSelectEmployee")).Show();
                    panelRecordDetails.ActiveTabIndex = 0;
                    return;
                }

            }
        }

        private bool AddDays(List<LeaveDay> days)
        {
            PostRequest<LeaveDay[]> req = new PostRequest<LeaveDay[]>();
            req.entity = days.ToArray();
            PostResponse<LeaveDay[]> resp = _leaveManagementService.ChildAddOrUpdate<LeaveDay[]>(req);
            if (!resp.Success)
            {

                return false;
            }
            return true;
        }
    }
}