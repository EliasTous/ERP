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
using AionHR.Infrastructure.JSON;
using AionHR.Model.Access_Control;
using AionHR.Model.Attributes;

namespace AionHR.Web.UI.Forms.Controls
{
    public partial class LeaveRequestControl : System.Web.UI.UserControl
    {


        public void FillLeaveType()
        {

            ListRequest req = new ListRequest();

            ListResponse<LeaveType> response = _leaveManagementService.ChildGetAll<LeaveType>(req);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() : response.Summary).Show();
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
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {


                HideShowButtons();

                //////FillDepartment();
                //////FillDivision();
                //////FillBranch();

                DateFormat.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                startDate.Format = endDate.Format = _systemService.SessionHelper.GetDateformat();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(LeaveRequest), BasicInfoTab, null, null, SaveButton);
                    ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
                    classReq.ClassId = (typeof(LeaveRequest).GetCustomAttributes(typeof(ClassIdentifier), false).ToList()[0]as ClassIdentifier).ClassID;
                    classReq.UserId = _systemService.SessionHelper.GetCurrentUserId();
                    RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
                    if (modClass != null && modClass.result != null && modClass.result.accessLevel<2 )
                    {
                        ViewOnly.Text = "1";
                    }
                    if(returnNotes.InputType== InputType.Password)
                    {
                        returnNotes.Visible = false;
                        notesField.Visible = true;
                        textField1.Visible = false;
                        textField2.Visible = true;
                    }
                  
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    EditRecordWindow.Close();
                    return;
                }

              

            }

        }

        private void RefreshSecurityForControls()
        {
            AccessControlApplier.ApplyAccessControlOnPage(typeof(LeaveRequest), BasicInfoTab, null, null, SaveButton);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            LeaveDaysGrid = new GridPanel();
            
        }

        #region public interface
        public void Update(string id)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = id;
            CurrentLeave.Text = r.RecordID;
            shouldDisableLastDay.Text = "0";

            RecordResponse<LeaveRequest> response = _leaveManagementService.ChildGetRecord<LeaveRequest>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() : response.Summary).Show();
                return;
            }
            //Step 2 : call setvalues with the retrieved object
            this.BasicInfoTab.SetValues(response.result);


            FillLeaveType();

            ltId.Select(response.result.ltId.ToString());
            status.Select(response.result.status.ToString());
            StoredLeaveChanged.Text = "0";
            if (response.result.employeeId != "0"  )
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
            LoadQuickViewInfo(response.result.employeeId);
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
            X.Call("CalcSum");
            panelRecordDetails.ActiveTabIndex = 0;

            setNormal();
            if (response.result.status == 2)

                setApproved(true);

            else if (response.result.status == 3 || response.result.status == -1)
                setUsed(true);
            else
            { setNormal(); }
            if (ViewOnly.Text=="1")
                SaveButton.Disabled = true;
            RefreshSecurityForControls();
            this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
            this.EditRecordWindow.Show();
            X.Call("calcDays");
        }

        public void Add()
        {
            BasicInfoTab.Reset();
            CurrentLeave.Text = "";
            FillLeaveType();
            StoredLeaveChanged.Text = "1";
            setNormal();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            //SetTabPanelEnabled(false);
            panelRecordDetails.ActiveTabIndex = 0;
            leaveDaysStore.DataSource = new List<LeaveDay>();
            leaveDaysStore.DataBind();
            status.Select(0);
            shouldDisableLastDay.Text = "0";
            this.EditRecordWindow.Show();
        }

        public void Return()
        {
            leaveReturnForm.Reset();
            leaveReturnWindow.Show();
        }

        public Store Store1 { get; set; }

        public GridPanel GrigPanel1 { get; set; }
        public RefreshParent RefreshLeaveCalendarCallBack { get; set; }

        public delegate void RefreshParent();


        #endregion



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

        private void setApproved(bool disabled)
        {
            GridDisabled.Text = disabled.ToString();
            startDate.Disabled = disabled;
            endDate.Disabled = employeeId.Disabled = justification.Disabled = destination.Disabled = isPaid.Disabled = ltId.Disabled = status.Disabled = TotalText.Disabled = disabled;
            returnDate.Disabled = !disabled;
            approved.Text = disabled.ToString();
            leavePeriod.Disabled = disabled;
            calDays.Disabled = disabled;
            
        }


        private void setNormal()
        {
            GridDisabled.Text = "False";
            startDate.Disabled = false;
            endDate.Disabled = employeeId.Disabled = justification.Disabled = destination.Disabled = isPaid.Disabled = ltId.Disabled = status.Disabled = TotalText.Disabled = false;
            returnDate.Disabled = true;
            leavePeriod.Disabled = false;
            approved.Text = "False";
            SaveButton.Disabled = false;
            calDays.Disabled = false;
        }

        private void setUsed(bool disabled)
        {
            GridDisabled.Text = disabled.ToString();
            startDate.Disabled = disabled;
            endDate.Disabled = employeeId.Disabled = justification.Disabled = destination.Disabled = isPaid.Disabled = ltId.Disabled = status.Disabled = TotalText.Disabled = disabled;
            returnDate.Disabled = disabled;
            SaveButton.Disabled = disabled;
            leavePeriod.Disabled = disabled;
            calDays.Disabled = disabled;

        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>



        private void LoadQuickViewInfo(string employeeId)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = employeeId;
            RecordResponse<EmployeeQuickView> resp = _employeeService.ChildGetRecord<EmployeeQuickView>(r);
            if(!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }

            leaveBalance.Text = resp.result.leavesBalance.ToString();
            yearsInService.Text = resp.result.serviceDuration;

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
            JsonSerializerSettings settings = new JsonSerializerSettings();
            CustomResolver res = new CustomResolver();
          //  res.AddRule("leaveRequest1_employeeId", "employeeId");
            //res.AddRule("leaveRequest1_ltId", "ltId");
           // res.AddRule("leaveRequest1_status", "status");

            settings.ContractResolver = res;
            LeaveRequest b = JsonConvert.DeserializeObject<LeaveRequest>(obj, settings);

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
                if (days.Count == 0)
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
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() : r.Summary).Show();
                        return;
                    }
                    else
                    {
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        days.ForEach(d => d.leaveId = Convert.ToInt32(b.recordId));
                        AddDays(days);
                        if (Store1 != null)
                            this.Store1.Insert(0, b);
                        else
                        {
                            RefreshLeaveCalendarCallBack();
                        }
                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditRecordWindow.Close();
                        //SetTabPanelEnabled(true);
                        //////RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        //////sm.DeselectAll();
                        //////sm.Select(b.recordId.ToString());



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
                    if (approved.Text == "True")
                    {


                        RecordRequest rec = new RecordRequest();
                        rec.RecordID = id;
                        RecordResponse<LeaveRequest> recordResponse = _leaveManagementService.ChildGetRecord<LeaveRequest>(rec);
                        if (!recordResponse.Success)
                        {
                            X.Msg.Alert(Resources.Common.Error, recordResponse.Summary).Show();
                            return;
                        }
                        PostRequest<LeaveRequest> postReq = new PostRequest<LeaveRequest>();
                        recordResponse.result.returnDate = b.returnDate;
                        recordResponse.result.returnNotes = b.returnNotes;
                        recordResponse.result.leavePeriod = leavePeriod.Text;
                        b = recordResponse.result;
                        b.status = 3;
                       
                        //postReq.entity = recordResponse.result;
                        //postReq.entity.returnDate = b.returnDate;
                        //postReq.entity.status = 3;
                        //PostResponse<LeaveRequest> resp = _leaveManagementService.ChildAddOrUpdate<LeaveRequest>(postReq);
                        //if (!resp.Success)
                        //{
                        //    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                        //    return;
                        //}
                        //Notification.Show(new NotificationConfig
                        //{
                        //    Title = Resources.Common.Notification,
                        //    Icon = Icon.Information,
                        //    Html = Resources.Common.RecordUpdatedSucc
                        //});
                        //this.leaveReturnWindow.Close();
                        //if (Store1 != null)
                        //{
                        //    var d = Store1.GetById(id);
                        //    d.Set("returnDate", postReq.entity.returnDate);
                        //    d.Set("status", postReq.entity.status);
                        //    d.Commit();
                        //}
                        //else
                        //{
                        //    RefreshLeaveCalendarCallBack();
                        //}
                        //EditRecordWindow.Close();
                        //return;
                    }
                    //getting the id of the record
                    PostRequest<LeaveRequest> request = new PostRequest<LeaveRequest>();
                    request.entity = b;
                    PostResponse<LeaveRequest> r = _leaveManagementService.ChildAddOrUpdate<LeaveRequest>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() : r.Summary).Show();
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
                        if(Store1!= null)
                        {
                            ModelProxy record = this.Store1.GetById(id);
                            BasicInfoTab.UpdateRecord(record);
                            // record.Set("employeeName", b.employeeName.fullName);
                            record.Set("ltName", b.ltName);
                            record.Set("status", b.status);
                            record.Commit();
                        }
                        else
                        {
                            RefreshLeaveCalendarCallBack();
                        }
                      
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
            if (req.CaId == "0")
            {

                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorNoCalendar").ToString()).Show();
                return new List<LeaveDay>();

            }
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() : response.Summary).Show();
                return;
            }

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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return 0;
            }
            if (!resp.result.caId.HasValue)
            {
                if (_systemService.SessionHelper.GetCalendarId() == 0)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                    return 0;
                }
                return _systemService.SessionHelper.GetCalendarId();
            }
            return resp.result.caId.Value;
        }
        [DirectMethod]
        public void MarkLeaveChanged(object sender, DirectEventArgs e)
        {
            DateTime startDate, endDate;
            try
            {
                startDate = DateTime.Parse(e.ExtraParams["startDate"]);
                endDate = DateTime.Parse(e.ExtraParams["endDate"]);
                LeaveChanged.Text = "1";
            }
            catch
            {

                panelRecordDetails.ActiveTabIndex = 0;
                return;
            }
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {

                return;
            }
            string startDay = startDate.ToString("yyyyMMdd");
            string endDay = endDate.ToString("yyyyMMdd");

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
            if (req.CaId == "0")
            {

                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorNoCalendar").ToString()).Show();
                return;

            }
            ListResponse<LeaveCalendarDay> days = _timeAttendanceService.ChildGetAll<LeaveCalendarDay>(req);
            if (!days.Success)
            {

            }
            List<LeaveDay> leaveDays = new List<LeaveDay>();
            days.Items.ForEach(x => leaveDays.Add(new LeaveDay() { dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek, dayId = x.dayId, workingHours = x.workingHours, leaveHours = x.workingHours }));
            leaveDaysStore.DataSource = leaveDays;
            leaveDaysStore.DataBind();
            X.Call("CalcSum");
            LoadQuickViewInfo(employeeId.Value.ToString());
        }
        [DirectMethod]
        public void Unnamed_Event(object sender, DirectEventArgs e)
        {
            DateTime startDate, endDate;
            try
            {
                startDate = DateTime.Parse(e.ExtraParams["startDate"]);
                endDate = DateTime.Parse(e.ExtraParams["endDate"]);
                LeaveChanged.Text = "1";
            }
            catch
            {
                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorSelectDate")).Show();
                panelRecordDetails.ActiveTabIndex = 0;
                return;
            }
            if (panelRecordDetails.ActiveTabIndex == 1)
            {


                if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
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

        [DirectMethod]
        public void CalcReturnDate(object sender, DirectEventArgs e)
        {
            DateTime startDate, endDate,returnDate;
            try
            {
                startDate = DateTime.Parse(e.ExtraParams["startDate"]);
                endDate = DateTime.Parse(e.ExtraParams["endDate"]);
                returnDate = DateTime.Parse(e.ExtraParams["returnDate"]);
                LeaveChanged.Text = "1";
            }
            catch
            {

                panelRecordDetails.ActiveTabIndex = 0;
                return;
            }
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {

                return;
            }
            string startDay = startDate.ToString("yyyyMMdd");
            string returnDay = returnDate.ToString("yyyyMMdd");

            LeaveCalendarDayListRequest req = new LeaveCalendarDayListRequest();
            req.StartDayId = startDay;
            req.EndDayId = returnDay;
            req.IsWorkingDay = true;
            int bulk;
            if (string.IsNullOrEmpty(employeeId.Value.ToString()) || !int.TryParse(employeeId.Value.ToString(), out bulk))
            {

                return;
            }
            req.CaId = GetEmployeeCalendar(employeeId.Value.ToString()).ToString();
            if (req.CaId == "0")
            {

                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorNoCalendar").ToString()).Show();
                return;

            }
            
            ListResponse<LeaveCalendarDay> days = _timeAttendanceService.ChildGetAll<LeaveCalendarDay>(req);
            if (!days.Success)
            {
                X.Msg.Alert(Resources.Common.Error, days.Summary).Show();
                return ;
            }
            List<LeaveDay> leaveDays = new List<LeaveDay>();
            days.Items.ForEach(x => leaveDays.Add(new LeaveDay() { dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek, dayId = x.dayId, workingHours = x.workingHours, leaveHours = x.workingHours }));
            leaveDaysStore.DataSource = leaveDays;
            leaveDaysStore.DataBind();
            X.Call("CalcSum");
            if(returnDate>endDate)
            {
                X.Call("EnableLast");
            }
        }

        private bool AddDays(List<LeaveDay> days)
        {
            PostRequest<LeaveDay[]> req = new PostRequest<LeaveDay[]>();
            req.entity = days.ToArray();
            PostResponse<LeaveDay[]> resp = _leaveManagementService.ChildAddOrUpdate<LeaveDay[]>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return false;
            }
            return true;
        }

        protected void ReturnLeave(object sender, DirectEventArgs e)
        {
            leaveReturnForm.Reset();
            leaveReturnWindow.Show();
        }

        [DirectMethod]

        public void FillLeave()
        {
            LeaveRequestListRequest req = new LeaveRequestListRequest();

            if (!string.IsNullOrEmpty(returnedEmployee.Text) && returnedEmployee.Value.ToString() != "0")
            {
                req.EmployeeId = Convert.ToInt32(returnedEmployee.Value);


            }
            else
            {
                return;

            }
            req.BranchId = req.DepartmentId = 0;
            req.OpenRequests = 0;
            req.StartAt = "1";
            req.Size = "1";
            req.SortBy = "endDate";

            ListResponse<LeaveRequest> resp = _leaveManagementService.ChildGetAll<LeaveRequest>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            if (resp.Items.Count == 0)
                return;
            if (resp.Items[0].returnDate.HasValue || resp.Items[0].status != 2)
                return;

            leaveId.Text = resp.Items[0].recordId;
            X.Call("FillReturnInfo", resp.Items[0].recordId, resp.Items[0].startDate, resp.Items[0].endDate);
        }
        protected void SaveLeaveReturn(object sender, DirectEventArgs e)
        {
            string id = e.ExtraParams["id"];
            string values = e.ExtraParams["values"];

            RecordRequest r = new RecordRequest();
            r.RecordID = id;
            RecordResponse<LeaveRequest> recordResponse = _leaveManagementService.ChildGetRecord<LeaveRequest>(r);
            if (!recordResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, recordResponse.Summary).Show();
                return;
            }
            CustomResolver res = new CustomResolver();
            res.AddRule("DateField3", "returnDate");
           // res.AddRule("Notes", "returnNotes");
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = res;
            LeaveRequest temp = JsonConvert.DeserializeObject<LeaveRequest>(values, settings);


            PostRequest<LeaveRequest> req = new PostRequest<LeaveRequest>();
            req.entity = recordResponse.result;
            req.entity.returnDate = temp.returnDate;
            req.entity.returnNotes = temp.returnNotes;
            req.entity.status = 3;
            PostResponse<LeaveRequest> resp = _leaveManagementService.ChildAddOrUpdate<LeaveRequest>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });
            this.leaveReturnWindow.Close();
            if (Store1 != null)
            {
                var d = Store1.GetById(id);
                d.Set("returnDate", temp.returnDate);
                d.Set("status", temp.status);
                d.Commit();
            }
            else
            {
                RefreshLeaveCalendarCallBack();
            }


        }

        protected void closing(object sender, DirectEventArgs e)
        {
            leaveDaysStore.DataSource = new List<LeaveDay>();
            leaveDaysStore.DataBind();
        }
    }
}