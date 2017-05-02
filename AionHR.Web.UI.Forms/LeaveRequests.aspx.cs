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
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Model.LeaveManagement;
using AionHR.Services.Messaging.System;
using AionHR.Model.TimeAttendance;

namespace AionHR.Web.UI.Forms
{
    public partial class LeaveRequests : System.Web.UI.Page
    {
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

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
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

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                FillDepartment();
                FillDivision();
                FillBranch();
                includeOpen.Select(3);
                DateFormat.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
            }

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
            this.colAttach.Visible = false;
        }


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
        }



        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
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
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRecord({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;

                case "colAttach":

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                LeaveRequest s = new LeaveRequest();
                s.recordId = index;
                s.destination = "";
                s.employeeId = 0;
                s.endDate = DateTime.Now;
                s.startDate = DateTime.Now;
                s.status = 0;
                s.isPaid = false;
                s.justification = "";
                s.ltId = 0;

                PostRequest<LeaveRequest> req = new PostRequest<LeaveRequest>();
                req.entity = s;
                PostResponse<LeaveRequest> r = _leaveManagementService.ChildDelete<LeaveRequest>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    Store1.Remove(index);

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





        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAll(object sender, DirectEventArgs e)
        {


            RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
            if (sm.SelectedRows.Count() <= 0)
                return;
            X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteManyRecord, new MessageBoxButtonsConfig
            {
                //Calling DoYes the direct method for removing selecte record
                Yes = new MessageBoxButtonConfig
                {
                    Handler = "App.direct.DoYes()",
                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Text = Resources.Common.No
                }

            }).Show();
        }

        /// <summary>
        /// Direct method for removing multiple records
        /// </summary>
        [DirectMethod(ShowMask = true)]
        public void DoYes()
        {
            try
            {
                RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;

                foreach (SelectedRow row in sm.SelectedRows)
                {
                    //Step 1 :Getting the id of the selected record: it maybe string 
                    int id = int.Parse(row.RecordID);


                    //Step 2 : removing the record from the store
                    //To do add code here 

                    //Step 3 :  remove the record from the store
                    Store1.Remove(id);

                }
                //Showing successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.ManyRecordDeletedSucc
                });

            }
            catch (Exception ex)
            {
                //Alert in case of any failure
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }
        }

        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            BasicInfoTab.Reset();
            CurrentLeave.Text = "";
            FillLeaveType();
            StoredLeaveChanged.Text = "1";
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            //SetTabPanelEnabled(false);
            panelRecordDetails.ActiveTabIndex = 0;
            leaveDaysStore.DataSource = new List<LeaveDay>();
            leaveDaysStore.DataBind();
            this.EditRecordWindow.Show();
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



            return req;
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            LeaveRequestListRequest request = GetFilteredRequest();

            request.Size = "50";
            request.StartAt = "1";
            request.SortBy = "firstName";

            request.Filter = "";
            ListResponse<LeaveRequest> routers = _leaveManagementService.ChildGetAll<LeaveRequest>(request);
            if (!routers.Success)
                return;
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.Store1.DataBind();
        }


        private void SetTabPanelEnabled(bool enabled)
        {
            foreach (var x in panelRecordDetails.Items)
            {
                x.Disabled = !enabled;
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
            
            if (string.IsNullOrEmpty(id))
            {
                if(days.Count==0)
                {
                    days = GenerateDefaultLeaveDays();
                }

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
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        days.ForEach(d => d.leaveId = Convert.ToInt32(b.recordId));
                        AddDays(days);
                        this.Store1.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditRecordWindow.Close();
                        //SetTabPanelEnabled(true);
                        RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        sm.DeselectAll();
                        sm.Select(b.recordId.ToString());



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
                        ModelProxy record = this.Store1.GetById(id);
                        BasicInfoTab.UpdateRecord(record);
                        // record.Set("employeeName", b.employeeName.fullName);
                        record.Set("ltName", b.ltName);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
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

        private List<LeaveDay> GetStoredLeaveDays()
        {
            LeaveDayListRequest req = new LeaveDayListRequest();
            req.LeaveId = CurrentLeave.Text;
            ListResponse<LeaveDay> resp = _leaveManagementService.ChildGetAll<LeaveDay>(req);
            if (!resp.Success)
            {

            }

            
            resp.Items.ForEach(x => x.dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek);
            return resp.Items;
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

        [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }

        protected void BasicInfoTab_Load(object sender, EventArgs e)
        {

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

        private void FillDivision()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();
        }

        protected void LeaveDays_Load(object sender, EventArgs e)
        {

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