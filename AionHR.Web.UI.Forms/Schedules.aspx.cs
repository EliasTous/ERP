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
using AionHR.Model.Employees.Profile;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Model.TimeAttendance;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;

namespace AionHR.Web.UI.Forms
{
    public partial class Schedules : System.Web.UI.Page
    {
        ITimeAttendanceService _branchService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();

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
                if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AttendanceSchedule), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AttendanceScheduleDay), dayBreaksForm, scheduleDays, null, Button3);
                }
                catch (AccessDeniedException exp)
                {

                    dayBreaksForm.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AttendanceBreak), null, periodsGrid, AddBreakButton, null);
                }
                catch (AccessDeniedException exp)
                {

                    periodsGrid.Hidden = true;
                    return;
                }

                ApplyAccessControlOnBreaks();

            }


        }

        private void ApplyAccessControlOnBreaks()
        {
            ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
            classReq.ClassId = (typeof(AttendanceBreak).GetCustomAttributes(typeof(ClassIdentifier), false).ToList()[0] as ClassIdentifier).ClassID;
            classReq.UserId = _systemService.SessionHelper.GetCurrentUserId();
            RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
            switch (modClass.result.accessLevel)
            {
                case 1: AddBreakButton.Disabled = true; editDisabled.Text = "1"; deleteDisabled.Value = "1"; break;
                case 2: AddBreakButton.Disabled = true; deleteDisabled.Value = "1"; break;
                default: break;
            }
            var properties = AccessControlApplier.GetPropertiesLevels(typeof(AttendanceBreak));

            int i = 1;
            foreach (var item in properties)
            {
                if (item.accessLevel < 2 && periodsGrid.ColumnModel.Columns[i].Editor.Count > 0)
                    periodsGrid.ColumnModel.Columns[i].Editor[0].ReadOnly = true;
                if (item.accessLevel < 1 && periodsGrid.ColumnModel.Columns[i].Editor.Count > 0)
                    periodsGrid.ColumnModel.Columns[i].Editor[0].InputType = InputType.Password;

                i++;
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


        protected void Prev_Click(object sender, DirectEventArgs e)
        {
            int index = int.Parse(e.ExtraParams["index"]);

            if ((index - 1) >= 0)
            {
                this.Viewport1.ActiveIndex = index - 1;
            }


        }
        protected void PoPuP(object sender, DirectEventArgs e)
        {

            ScheduleNamelb.Text = e.ExtraParams["ScheduleNamePar"];
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    ////Step 1 : get the object from the Web Service 
                    //panelRecordDetails.ActiveIndex = 0;
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<AttendanceSchedule> response = _branchService.ChildGetRecord<AttendanceSchedule>(r);
                    
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                        return;
                    }
                    ////Step 2 : call setvalues with the retrieved object
                    BasicInfoTab.Reset();
                    this.BasicInfoTab.SetValues(response.result);
                    //_systemService.SessionHelper.Set("currentSchedule",r.RecordID);
                    //// InitCombos(response.result);
                    setDefaultBtn.Hidden = false;
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();
                    //FillDow("1");



                    break;

                case "imgAttach":
                    //panelRecordDetails.ActiveIndex = 0;

                    AttendanceScheduleDayListRequest daysRequest = new AttendanceScheduleDayListRequest();
                    daysRequest.ScheduleId = id.ToString();

                    ListResponse<AttendanceScheduleDay> daysResponse = _branchService.ChildGetAll<AttendanceScheduleDay>(daysRequest);
                    if (!daysResponse.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, daysResponse.Summary).Show();
                        return;
                    }

                    //Step 2 : call setvalues with the retrieved object
                    scheduleDays.Store[0].DataSource = daysResponse.Items;
                    scheduleDays.Store[0].DataBind();
                    CurrentSchedule.Text = id.ToString();

                    Viewport1.ActiveIndex = 1;
                    // InitCombos(response.result);
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

        protected void PoPuPDay(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    ////Step 1 : get the object from the Web Service 
                    //panelRecordDetails.ActiveIndex = 0;
                    try
                    {
                        isBatch.Text = "0";
                        FillDow(id.ToString());
                    }
                    catch (Exception exp)
                    {

                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                        return;

                    }
                    //_systemService.SessionHelper.Set("currentSchedule",r.RecordID);
                    //// InitCombos(response.result);
                    this.EditDayBreaks.Title = Resources.Common.EditWindowsTitle;
                    this.EditDayBreaks.Show();
                    //FillDow("1");



                    break;
                default: break;
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
                AttendanceSchedule s = new AttendanceSchedule();
                s.recordId = index;
                s.fci_max_lt = s.fci_min_ot = s.lco_max_el = s.lco_max_ot = s.lco_min_ot = 0;
                s.name = "";
                PostRequest<AttendanceSchedule> req = new PostRequest<AttendanceSchedule>();
                req.entity = s;
                PostResponse<AttendanceSchedule> r = _branchService.ChildDelete<AttendanceSchedule>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
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


        [DirectMethod]
        public object FillParent(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<VacationSchedule> data;
            ListRequest req = new ListRequest();

            ListResponse<VacationSchedule> response = _branchService.ChildGetAll<VacationSchedule>(req);
            data = response.Items;
            return new
            {
                data
            };

        }
        [DirectMethod]
        public object FillSupervisor(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<Employee> data = GetEmployeesFiltered(prms.Query);

            //  return new
            // {
            return data;
            //};

        }

        private List<Employee> GetEmployeeByID(string id)
        {

            RecordRequest req = new RecordRequest();
            req.RecordID = id;



            List<Employee> emps = new List<Employee>();
            RecordResponse<Employee> emp = _employeeService.Get<Employee>(req);
            emps.Add(emp.result);
            return emps;
        }
        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = "firstName";

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;



            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
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

            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;

            this.EditRecordWindow.Show();
            setDefaultBtn.Hidden = true;
            //fci_max_lt.Text = fci_min_ot.Text = lco_max_el.Text = lco_max_ot.Text = lco_min_ot.Text = "0";
        }
        protected void AddNewDay(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            dayBreaksForm.Reset();

            this.EditDayBreaks.Title = Resources.Common.AddNewRecord;

            this.EditDayBreaks.Show();
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();
            request.Filter = "";
            ListResponse<AttendanceSchedule> branches = _branchService.ChildGetAll<AttendanceSchedule>(request);
            if (!branches.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + branches.LogId: branches.Summary).Show();
            this.Store1.DataSource = branches.Items;
            e.Total = branches.count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["schedule"];
            AttendanceSchedule b = JsonConvert.DeserializeObject<AttendanceSchedule>(obj);
            if (!b.fci_max_lt.HasValue)
                b.fci_max_lt = 0;
            if (!b.fci_min_ot.HasValue)
                b.fci_min_ot = 0;
            if (!b.lco_max_el.HasValue)
                b.lco_max_el = 0;
            if (!b.lco_max_ot.HasValue)
                b.lco_max_ot = 0;
            if (!b.lco_min_ot.HasValue)
                b.lco_min_ot = 0;
            b.recordId = id;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AttendanceSchedule> request = new PostRequest<AttendanceSchedule>();
                    request.entity = b;
                    PostResponse<AttendanceSchedule> r = _branchService.ChildAddOrUpdate<AttendanceSchedule>(request);
                    b.recordId = r.recordId;

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
                        return;
                    }



                    else
                    {

                        //Add this record to the store 
                        this.Store1.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                        setDefaultBtn.Hidden = false;
                        recordId.Text = b.recordId;

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
                    int index = Convert.ToInt32(id);//getting the id of the record
                    PostRequest<AttendanceSchedule> modifyHeaderRequest = new PostRequest<AttendanceSchedule>();
                    modifyHeaderRequest.entity = b;
                    PostResponse<AttendanceSchedule> r = _branchService.ChildAddOrUpdate<AttendanceSchedule>(modifyHeaderRequest);                   //Step 1 Selecting the object or building up the object for update purpose
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                        return;
                    }

                    //Step 2 : saving to store


                    else
                    {


                        ModelProxy record = this.Store1.GetById(index);
                        BasicInfoTab.UpdateRecord(record);

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
        private bool AddPeriodsList(string scheduleIdString, List<VacationSchedulePeriod> periods)
        {
            short i = 1;
            int scheduleId = Convert.ToInt32(scheduleIdString);
            foreach (var period in periods)
            {
                period.seqNo = i++;
                period.vsId = scheduleId;

            }
            PostRequest<VacationSchedulePeriod[]> periodRequest = new PostRequest<VacationSchedulePeriod[]>();
            periodRequest.entity = periods.ToArray();
            PostResponse<VacationSchedulePeriod[]> response = _branchService.ChildAddOrUpdate<VacationSchedulePeriod[]>(periodRequest);
            if (!response.Success)
            {
                return false;
            }
            return true;
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


        [DirectMethod]
        public void panelRecordDetails_TabChanged()
        {
            //if (panelRecordDetails.ActiveIndex != 1)
            //    return;
            //string scheduleId = _systemService.SessionHelper.Get("currentSchedule").ToString();
            //RecordRequest request = new RecordRequest();
            //request.QueryStringParams.Add("_scId", scheduleId);
            //RecordResponse<AttendanceScheduleDay> day = _branchService.ChildGetRecord<AttendanceScheduleDay>(request);
            //if (!day.Success)
            //    return;
            //sundayForm.SetValues(day);
            //ListRequest req = new ListRequest();
            //req.QueryStringParams.Add("_vsId", scheduleId);
            //req.QueryStringParams.Add("dow", "1");
            //ListResponse<AttendanceBreak> periods = _branchService.ChildGetAll<AttendanceBreak>(req);
            //sundayGrid.Store[0].DataSource = periods.Items;
            //sundayGrid.Store[0].DataBind();
            //sundayGrid.DataBind();
        }

        private void FillDow(string dow)
        {

            AttendanceScheduleDayRecordRequest dayRequest = new AttendanceScheduleDayRecordRequest();

            dayRequest.ScheduleId = CurrentSchedule.Text;// _systemService.SessionHelper.Get("currentSchedule").ToString();
            dayRequest.DayOfWeek = dow;
            CurrentDow.Text = dow;
            List<DayType> dayTypes = LoadDayTypes();
            dayTypesStore.DataSource = dayTypes;
            dayTypesStore.DataBind();

            RecordResponse<AttendanceScheduleDay> dayResponse = _branchService.ChildGetRecord<AttendanceScheduleDay>(dayRequest);
            if (!dayResponse.Success)
            {
                throw new Exception(dayResponse.Summary);
            }
            ////Step 2 : call setvalues with the retrieved object
            this.dayBreaksForm.SetValues(dayResponse.result);

            dayTypeId.Select(dayResponse.result.dayTypeId);


            if (dayTypes.Where(x => x.recordId == dayResponse.result.dayTypeId).First().isWorkingDay)
            {
                SetDayFormEnabled(true);
                AttendanceDayBreaksListRequest breaksRequest = new AttendanceDayBreaksListRequest();
                breaksRequest.ScheduleId = dayRequest.ScheduleId;
                breaksRequest.DayOfWeek = dow;
                ListResponse<AttendanceBreak> breaks = _branchService.ChildGetAll<AttendanceBreak>(breaksRequest);
                if (!breaks.Success)
                {
                    throw new Exception(breaks.Summary);
                }
                periodsGrid.Store[0].DataSource = breaks.Items;
                periodsGrid.Store[0].DataBind();
            }
            else
            {

                SetDayFormEnabled(false);
            }

            switch (dow)
            {
                case "1":
                    dayBreaksForm.Title = GetLocalResourceObject("DayBreaksForm").ToString() +" - "+ GetGlobalResourceObject("Common", "MondayText");
                    break;
                   
                case "2":
                    dayBreaksForm.Title = GetLocalResourceObject("DayBreaksForm").ToString() + " - " + GetGlobalResourceObject("Common", "TuesdayText");
                    break;
                case "3":
                    dayBreaksForm.Title = GetLocalResourceObject("DayBreaksForm").ToString() + " - " + GetGlobalResourceObject("Common", "WednesdayText");
                    break;
                   
                case "4":
                    dayBreaksForm.Title = GetLocalResourceObject("DayBreaksForm").ToString() + " - " + GetGlobalResourceObject("Common", "ThursdayText");
                    break;
                   
                case "5":
                    dayBreaksForm.Title = GetLocalResourceObject("DayBreaksForm").ToString() + " - " + GetGlobalResourceObject("Common", "FridayText");
                    break; 
                case "6":
                    dayBreaksForm.Title = GetLocalResourceObject("DayBreaksForm").ToString() + " - " + GetGlobalResourceObject("Common", "SaturdayText");
                    break; 
                case "7":
                    dayBreaksForm.Title = GetLocalResourceObject("DayBreaksForm").ToString() + " - " + GetGlobalResourceObject("Common", "SundayText");
                    break;
            }
           
        }

        private void ReloadDays()
        {
            AttendanceScheduleDayListRequest daysRequest = new AttendanceScheduleDayListRequest();
            daysRequest.ScheduleId = CurrentSchedule.Text;

            ListResponse<AttendanceScheduleDay> daysResponse = _branchService.ChildGetAll<AttendanceScheduleDay>(daysRequest);
            if (!daysResponse.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, daysResponse.Summary).Show();
                return;
            }

            //Step 2 : call setvalues with the retrieved object
            scheduleDays.Store[0].DataSource = daysResponse.Items;
            scheduleDays.Store[0].DataBind();
        }

        protected void SaveDayBreaks(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

            string dayJ = e.ExtraParams["day"];
            string breaksJ = e.ExtraParams["breaks"];

            AttendanceScheduleDay day = JsonConvert.DeserializeObject<AttendanceScheduleDay>(dayJ);
            bool workingDay = IsWorkingDay.Text == "True";
            List<AttendanceBreak> breaks = null;
            if (workingDay)
                breaks = JsonConvert.DeserializeObject<List<AttendanceBreak>>(breaksJ);
            else
            {
                day.firstIn = "00:00";
                day.lastOut = "00:00";
            }
            day.scId = Convert.ToInt32(CurrentSchedule.Text);
            if (!string.IsNullOrEmpty(CurrentDow.Text))
                day.dow = Convert.ToInt16(CurrentDow.Text);

            // Define the object to add or edit as null

            if (isBatch.Text == "1")
            {
                for (short i = 1; i <= 7; i++)
                {
                    day.dow = i;
                    SaveDay(day, workingDay, breaks);
                }
                ReloadDays();
            }
            else
            {
                SaveDay(day, workingDay, breaks);
                ReloadDays();
            }
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });

            this.EditDayBreaks.Close();
        }

        private void SaveDay(AttendanceScheduleDay day, bool workingDay, List<AttendanceBreak> breaks)
        {
            try
            {
                //getting the id of the record
                PostRequest<AttendanceScheduleDay> modifyHeaderRequest = new PostRequest<AttendanceScheduleDay>();
                modifyHeaderRequest.entity = day;
                if (string.IsNullOrEmpty(modifyHeaderRequest.entity.duration))
                    modifyHeaderRequest.entity.duration = "0";
                PostResponse<AttendanceScheduleDay> r = _branchService.ChildAddOrUpdate<AttendanceScheduleDay>(modifyHeaderRequest);                   //Step 1 Selecting the object or building up the object for update purpose
                if (!r.Success)//it maybe another check
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
                    return;
                }
                if (workingDay)
                {

                    PostResponse<AttendanceBreak> deleteResponse = _branchService.DeleteDayBreaks(day.scId, day.dow);
                    if (!deleteResponse.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, deleteResponse.Summary).Show();
                        return;
                    }
                    try

                    {
                        AddBreaksList(day.scId.ToString(), day.dow, breaks);
                    }
                    catch (Exception ex)
                    {

                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
                        return;
                    }

                }



                //ModelProxy record = this.scheduleStore.GetById(day.dow);
                //dayBreaksForm.UpdateRecord(record);
                //if (!workingDay)
                //{
                //    record.Set("firstIn", "00:00");
                //    record.Set("lastOut", "00:00");
                //}
                //record.Commit();
               





            }
            catch (Exception ex)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
            }
        }

        private void AddBreaksList(string scheduleIdString, short dow, List<AttendanceBreak> breaks)
        {
            short i = 1;
            int scheduleId = Convert.ToInt32(scheduleIdString);
            foreach (var period in breaks)
            {
                period.seqNo = i++;
                period.scId = scheduleId;
                period.dow = dow;
                //Added to sent the time only as string


            }
            PostRequest<AttendanceBreak[]> periodRequest = new PostRequest<AttendanceBreak[]>();
            periodRequest.entity = breaks.ToArray();
            PostResponse<AttendanceBreak[]> response = _branchService.ChildAddOrUpdate<AttendanceBreak[]>(periodRequest);
            if (!response.Success)
            {
                throw new Exception(response.Summary);
            }

        }
        [DirectMethod]
        public object CheckTime(string value)
        {

            int hours = Convert.ToInt32((value[0] + value[1]).ToString());
            int mins = Convert.ToInt32((value[3] + value[4]).ToString());
            if (hours > 23 || mins > 59)
                return false;
            return true;
        }

        private List<DayType> LoadDayTypes()
        {
            ListRequest req = new ListRequest();
            ListResponse<DayType> days = _branchService.ChildGetAll<DayType>(req);
            return days.Items;


        }

        private DayType GetDayType(string id)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = id;
            RecordResponse<DayType> day = _branchService.ChildGetRecord<DayType>(req);
            return day.result;
        }

        protected void Unnamed_Event(object sender, DirectEventArgs e)
        {
            string selectedDayTypeId = dayTypeId.Value.ToString();
            if (string.IsNullOrEmpty(selectedDayTypeId) || GetDayType(selectedDayTypeId) == null)
                return;
            if (GetDayType(selectedDayTypeId).isWorkingDay)
                SetDayFormEnabled(true);
            else
                SetDayFormEnabled(false);

        }

        private void SetDayFormEnabled(bool v)
        {
            // ((TextFieldBase)dayBreaksForm.FindControl("fromField")).SetEditable(false);

            IsWorkingDay.Text = v.ToString();
            X.Call("setReadOnly", "firstIn", v);
            X.Call("setReadOnly", "lastOut", v);
            X.Call("setReadOnly", "periodsGrid", v);

        }

        protected void selectPattern_click(object sender, DirectEventArgs e)
        {
            List<DayType> days = LoadDayTypes();
            workDTStore.DataSource = days.Where(s => s.isWorkingDay == true).ToList();
            workDTStore.DataBind();
            WeekendDTStore.DataSource = days.Where(s => s.isWorkingDay == false).ToList();
            WeekendDTStore.DataBind();

            patternWindow.Show();
        }

        protected void SavePattern(object sender, DirectEventArgs e)
        {
            string day = e.ExtraParams["pattern"];
            SchedulePattern b = JSON.Deserialize<SchedulePattern>(day);

            b.scId = Convert.ToInt32(CurrentSchedule.Text);
            PostRequest<SchedulePattern> request = new PostRequest<SchedulePattern>();
            request.entity = b;
            PostResponse<SchedulePattern> response = _branchService.ChildAddOrUpdate<SchedulePattern>(request);

            if (!response.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }
            else
            {
                Store1.Reload();
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.PatternAppliedSucc
                });
                this.patternWindow.Close();
            }

        }

        protected void SetDefaultClick(object sender, DirectEventArgs e)
        {
            KeyValuePair<string, string> pair = new KeyValuePair<string, string>("scId", e.ExtraParams["id"].ToString());
            PostRequest<KeyValuePair<string, string>> req = new PostRequest<KeyValuePair<string, string>>();
            req.entity = pair;
            PostResponse<KeyValuePair<string, string>> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+resp.LogId : resp.Summary).Show();
                return;
            }
            else
            {
                X.Msg.Alert(Resources.Common.RecordSavingSucc, GetLocalResourceObject("DefaultSetSucc").ToString()).Show();
            }
        }

        protected void batchClicked(object sender, DirectEventArgs e)
        {
            dayBreaksForm.Reset();
            isBatch.Text = "1";
            fieldScId.Text = CurrentSchedule.Text;

            List<DayType> dayTypes = LoadDayTypes();
            dayTypesStore.DataSource = dayTypes;
            dayTypesStore.DataBind();
            this.EditDayBreaks.Title = Resources.Common.AddNewRecord;

            this.EditDayBreaks.Show();
        }
    }
}