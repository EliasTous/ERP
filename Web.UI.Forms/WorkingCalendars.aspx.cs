﻿using System;
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
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Web.UI.Forms.Utilities;
using Model.Company.News;
using Services.Messaging;
using Model.Company.Structure;
using Model.Employees.Profile;
using Model.Employees.Leaves;
using Model.Attendance;
using Model.TimeAttendance;

namespace Web.UI.Forms
{
    public partial class WorkingCalendars : System.Web.UI.Page
    {
        ITimeAttendanceService _branchService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(WorkingCalendar), BasicInfoTab, GridPanel1, btnAdd, SaveButton);

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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(CalendarYear), calendarYearForm, GridPanel2, Button1, Button3);

                }
                catch (AccessDeniedException exp)
                {

                    GridPanel2.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Model.Attendance.CalendarDay), dayConfigForm, null, null, Button2);

                }
                catch (AccessDeniedException exp)
                {

                    dayConfigForm.Hidden = true;

                }
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
            CurrentCalenderLabel.Text = e.ExtraParams["calenderNamePar"];
            calendarName.Text = e.ExtraParams["calenderNamePar"];
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    ////Step 1 : get the object from the Web Service 
                    //panelRecordDetails.ActiveIndex = 0;
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<WorkingCalendar> response = _branchService.ChildGetRecord<WorkingCalendar>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                    ////Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    //_systemService.SessionHelper.Set("currentSchedule",r.RecordID);
                    //// InitCombos(response.result);
                    setDefaultBtn.Hidden = false;
                    this.EditCalendarWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditCalendarWindow.Show();
                    //FillDow("1");



                    break;
                case "colDayName":
                    ////Step 1 : get the object from the Web Service 
                    //panelRecordDetails.ActiveIndex = 0;
                    //FillDow(id.ToString());

                    //_systemService.SessionHelper.Set("currentSchedule",r.RecordID);
                    //// InitCombos(response.result);
                    //this.EditDayBreaks.Title = Resources.Common.EditWindowsTitle;
                    //this.EditDayBreaks.Show();
                    //FillDow("1");



                    break;
                case "imgAttach":
                    //panelRecordDetails.ActiveIndex = 0;

                    CalendarYearsListRequest yearsRequest = new CalendarYearsListRequest();
                    yearsRequest.CalendarId = id.ToString();

                    ListResponse<CalendarYear> daysResponse = _branchService.ChildGetAll<CalendarYear>(yearsRequest);
                    if (!daysResponse.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", daysResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", daysResponse.ErrorCode).ToString() : daysResponse.Summary).Show();
                        return;
                    }

                    //Step 2 : call setvalues with the retrieved object
                    calendarYears.Store[0].DataSource = daysResponse.Items;
                    calendarYears.Store[0].DataBind();
                    CurrentCalendar.Text = id.ToString();

                    Viewport1.ActiveIndex = 1;
                    // InitCombos(response.result);
                    break;
                case "colYearDetails":
                    //panelRecordDetails.ActiveIndex = 0;

                    //CalendarYearsListRequest yearsRequest = new CalendarYearsListRequest();
                    //yearsRequest.CalendarId = id.ToString();

                    //ListResponse<CalendarYear> daysResponse = _branchService.ChildGetAll<CalendarYear>(yearsRequest);


                    ////Step 2 : call setvalues with the retrieved object
                    //calendarYears.Store[0].DataSource = daysResponse.Items;
                    //calendarYears.Store[0].DataBind();
                    CurrentYear.Text = id.ToString();
                    try
                    {
                        LoadDays();
                    }
                    catch (Exception exp)
                    {

                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                        return;

                    }
                    Viewport1.ActiveIndex = 2;
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

        protected void PoPuPYear(object sender, DirectEventArgs e)
        {
            lbCalenderName.Text = CurrentCalenderLabel.Text;
            lbCalender.Text = e.ExtraParams["CalenderYear"];
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {

                case "imgAttach":
                    //panelRecordDetails.ActiveIndex = 0;

                    //CalendarYearsListRequest yearsRequest = new CalendarYearsListRequest();
                    //yearsRequest.CalendarId = id.ToString();

                    //ListResponse<CalendarYear> daysResponse = _branchService.ChildGetAll<CalendarYear>(yearsRequest);


                    ////Step 2 : call setvalues with the retrieved object
                    //calendarYears.Store[0].DataSource = daysResponse.Items;
                    //calendarYears.Store[0].DataBind();
                    CurrentYear.Text = id.ToString();
                    try
                    {
                        LoadDays();
                    }
                    catch (Exception exp)
                    {

                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                        return;

                    }
                    Viewport1.ActiveIndex = 2;
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


        private void LoadDays()
        {
            CalendarDayListRequest req = new CalendarDayListRequest();
            List<DayType> dtypes = LoadDayTypes();

            req.CalendarId = CurrentCalendar.Text;
            req.Year = CurrentYear.Text;
            if ((Convert.ToInt32(CurrentYear.Text) - 2016) % 4 != 0)
                X.Call("setLeapDay");
            ListResponse<Model.Attendance.CalendarDay> daysResponse = _branchService.ChildGetAll<Model.Attendance.CalendarDay>(req);
            if (!daysResponse.Success)
                throw new Exception(daysResponse.Summary);
            X.Call("init");
            foreach (var item in daysResponse.Items)
            {
                string dayId = item.dayId;
                string Month = dayId[4].ToString() + dayId[5].ToString();
                string Day = dayId[6].ToString() + dayId[7].ToString();




                string color = dtypes.Where(x => x.recordId == item.dayTypeId.ToString()).First().color;
                X.Call("colorify", "td" + Month + Day, "#" + color.Trim(), DateTime.ParseExact(item.dayId, "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat()), GetGlobalResourceObject("Common", ((DayOfWeek)(item.dow % 7)).ToString() + "Text").ToString());




                //c.Style.Add("backgroud-color", "#" + color);
                //tbCalendar.Rows[int.Parse(Month)].Cells[int.Parse(Day)].InnerHtml = "<span >" + Month + Day + "</span>";// <span class='scheduleid'>" + item.scId.ToString() + "</span><span class='daytypeid'>" + item.dayTypeId.ToString() + "</span>";
                //tbCalendar.Rows[int.Parse(Month)].Cells[int.Parse(Day)].Attributes.Add("style", "background-color:#" + dtypes.Where(x => x.recordId == item.dayTypeId.ToString()).First().color) ;
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
                WorkingCalendar c = new WorkingCalendar();
                c.name = "";
                c.recordId = index;
                PostRequest<WorkingCalendar> req = new PostRequest<WorkingCalendar>();
                req.entity = c;

                PostResponse<WorkingCalendar> response = _branchService.ChildDelete<WorkingCalendar>(req);
                if (!response.Success)
                {

                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                     Common.errorMessage(response);
                    return;
                }
                else
                {


                    //Step 1 Code to delete the object from the database 
                    if (_systemService.SessionHelper.GetCalendarId().ToString() == index)
                    {
                        List<KeyValuePair<string, string>> submittedValues = new List<KeyValuePair<string, string>>();
                        submittedValues.Add(new KeyValuePair<string, string>("caId", index));
                        KeyValuePair<string, string>[] valArr = submittedValues.ToArray();
                        PostRequest<KeyValuePair<string, string>[]> reqDef = new PostRequest<KeyValuePair<string, string>[]>();
                        reqDef.entity = valArr;
                        PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(reqDef);
                        if (!resp.Success)
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(resp);
                            
                            return;
                        }
                    }
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
            return Common.GetEmployeesFiltered(prms.Query);

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

            this.EditCalendarWindow.Title = Resources.Common.AddNewRecord;
            setDefaultBtn.Hidden = true;
            this.EditCalendarWindow.Show();
        }
        protected void AddNewDay(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            calendarYearForm.Reset();

            this.EditYearDetails.Title = Resources.Common.AddNewRecord;

            this.EditYearDetails.Show();
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
            ListResponse<WorkingCalendar> branches = _branchService.ChildGetAll<WorkingCalendar>(request);
            if (!branches.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") +branches.LogId: branches.Summary).Show();
            this.Store1.DataSource = branches.Items;
            e.Total = branches.count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["calendar"];
            WorkingCalendar b = JsonConvert.DeserializeObject<WorkingCalendar>(obj);

            b.recordId = id;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<WorkingCalendar> request = new PostRequest<WorkingCalendar>();
                    request.entity = b;
                    PostResponse<WorkingCalendar> r = _branchService.ChildAddOrUpdate<WorkingCalendar>(request);
                    b.recordId = r.recordId;

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
                        return;
                    }



                    else
                    {
                        CurrentCalendar.Text = b.recordId;
                        recordId.Text = b.recordId;
                        //Add this record to the store 
                        this.Store1.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        X.Call("SetVisible");
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
                    PostRequest<WorkingCalendar> modifyHeaderRequest = new PostRequest<WorkingCalendar>();
                    modifyHeaderRequest.entity = b;
                    PostResponse<WorkingCalendar> r = _branchService.ChildAddOrUpdate<WorkingCalendar>(modifyHeaderRequest);                   //Step 1 Selecting the object or building up the object for update purpose
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
                        this.EditCalendarWindow.Close();


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

        protected void SaveCalendarYear(object sender, DirectEventArgs e)
        {
            string caId = e.ExtraParams["caId"];

            string year = e.ExtraParams["year"];
            CalendarYear b = JsonConvert.DeserializeObject<CalendarYear>(year);

            b.caId = Convert.ToInt32(CurrentCalendar.Text);

            PostRequest<CalendarYear> request = new PostRequest<CalendarYear>();
            request.entity = b;
            PostResponse<CalendarYear> response = _branchService.ChildAddOrUpdate<CalendarYear>(request);

            if (!response.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

            //Step 2 : saving to store


            else
            {


                this.scheduleStore.Insert(0, b);

                //Display successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });

                this.EditYearDetails.Close();
                RowSelectionModel sm = this.calendarYears.GetSelectionModel() as RowSelectionModel;
                sm.DeselectAll();
                sm.Select(b.year.ToString());


            }

        }


        protected void SaveDayConfig(object sender, DirectEventArgs e)
        {


            string day = e.ExtraParams["day"];
            Model.Attendance.CalendarDay b = JsonConvert.DeserializeObject<Model.Attendance.CalendarDay>(day);

            b.caId = Convert.ToInt32(CurrentCalendar.Text);
            b.dayId = dayId.Text;
            b.year = Convert.ToInt32(CurrentYear.Text);

            PostRequest<Model.Attendance.CalendarDay> request = new PostRequest<Model.Attendance.CalendarDay>();
            request.entity = b;
            PostResponse<Model.Attendance.CalendarDay> response = _branchService.ChildAddOrUpdate<Model.Attendance.CalendarDay>(request);

            if (!response.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

            //Step 2 : saving to store


            else
            {

                RecordRequest r = new RecordRequest();
                r.RecordID = b.dayTypeId.ToString();
                RecordResponse<DayType> dayType = _branchService.ChildGetRecord<DayType>(r);
                string color = dayType.result.color;
                X.Call("colorify", "td" + b.dayId.Substring(4, 4), "#" + color.Trim());

                //Display successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });

                this.dayConfigWindow.Close();



            }

        }

        [DirectMethod]
        public void OpenDayConfig(string day)
        {
            scId.Clear();
            dayTypeId.Clear();

            dayId.Text = CurrentYear.Text + day;

            CalendarDayRecordRequest request = new CalendarDayRecordRequest();
            request.CaId = CurrentCalendar.Text;
            request.DayId = dayId.Text;
            request.year = CurrentYear.Text;
            RecordResponse<Model.Attendance.CalendarDay> dayObj = _branchService.ChildGetRecord<Model.Attendance.CalendarDay>(request);
            if (!dayObj.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", dayObj.ErrorCode) != null ? GetGlobalResourceObject("Errors", dayObj.ErrorCode).ToString() : dayObj.Summary).Show();
                return;
            }
            dayConfigWindow.Show();
            if (dayObj.result == null)
            {
                DateTime t = DateTime.ParseExact(request.DayId, "yyyyMMdd", new CultureInfo("en"));
                dayConfigForm.Title = GetGlobalResourceObject("Common", (t.DayOfWeek).ToString() + "Text").ToString() + "- " + t.ToString(_systemService.SessionHelper.GetDateformat());

            }
            else
            {
                dayConfigForm.Title = GetGlobalResourceObject("Common", ((DayOfWeek)(dayObj.result.dow % 7)).ToString() + "Text").ToString() + "- " + DateTime.ParseExact(dayObj.result.dayId, "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat());
            }
            schedulesStore.DataSource = LoadSchedules();
            schedulesStore.DataBind();
            dayTypesStore.DataSource = LoadDayTypes();
            dayTypesStore.DataBind();

            if (dayObj.result != null)
            {
                if (dayObj.result.scId.HasValue)
                    scId.Select(dayObj.result.scId.Value.ToString());
                dayTypeId.Select(dayObj.result.dayTypeId.ToString());
            }
        }

        private List<AttendanceSchedule> LoadSchedules()
        {
            ListRequest req = new ListRequest();
            ListResponse<AttendanceSchedule> schedules = _branchService.ChildGetAll<AttendanceSchedule>(req);
            if (!schedules.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", schedules.ErrorCode) != null ? GetGlobalResourceObject("Errors", schedules.ErrorCode).ToString() : schedules.Summary).Show();
            return schedules.Items;
        }
        private List<DayType> LoadDayTypes()
        {
            ListRequest req = new ListRequest();
            ListResponse<DayType> schedules = _branchService.ChildGetAll<DayType>(req);
            if (!schedules.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", schedules.ErrorCode) != null ? GetGlobalResourceObject("Errors", schedules.ErrorCode).ToString() : schedules.Summary).Show();
            List<DayType> mylist = new List<DayType>();
            schedules.Items.ForEach(x => x.color = x.color.Replace(" ", String.Empty));
            colorsStore.DataSource = schedules.Items; 
            colorsStore.DataBind();
            return schedules.Items;
        }

        protected void viewLegent_click(object sender, DirectEventArgs e)
        {
            LoadDayTypes();
            legendsWindow.Show();
        }

        protected void selectPattern_click(object sender, DirectEventArgs e)
        {
            patternScheduleStore.DataSource = LoadSchedules();
            patternFormPanel.Reset();
            patternScheduleStore.DataBind();
            dateTo.MinDate = dateFrom.MinDate = new DateTime(Convert.ToInt32(CurrentYear.Text), 1, 1);
            dateTo.MaxDate = dateFrom.MaxDate = new DateTime(Convert.ToInt32(CurrentYear.Text), 12, 31);
            dateFrom.Value =new  DateTime(Convert.ToInt32(CurrentYear.Text), 1, 1);
            dateTo.Value= new DateTime(Convert.ToInt32(CurrentYear.Text),12, 31);
            patternWindow.Show();
        }

        protected void alternateDayWidnow_click(object sender, DirectEventArgs e)
        {
            alternateDTStore.DataSource = LoadDayTypes();
            alternateDTStore.DataBind();
            alternateDaysForm.Reset();

            startDate.MinDate = dateFrom.MinDate = new DateTime(Convert.ToInt32(CurrentYear.Text), 1, 1);
            endDate.MaxDate = dateFrom.MaxDate = new DateTime(Convert.ToInt32(CurrentYear.Text), 12, 31);

            alternateDaysWindow.Show();
        }

        protected void alternateWeekWidnow_click(object sender, DirectEventArgs e)
        {
            sc1Store.DataSource = LoadSchedules();
            sc1Store.DataBind();
            sc2Store.DataSource = LoadSchedules();
            sc2Store.DataBind();
            alternateWeekWindow.Show();
        }
        protected void SavePattern(object sender, DirectEventArgs e)
        {


            string day = e.ExtraParams["pattern"];
            CalendarPattern b = JSON.Deserialize<CalendarPattern>(day);

            b.caId = CurrentCalendar.Text;

            b.year = CurrentYear.Text;



            PostRequest<CalendarPattern> request = new PostRequest<CalendarPattern>();
            request.entity = b;
            PostResponse<CalendarPattern> response = _branchService.ChildAddOrUpdate<CalendarPattern>(request);

            if (!response.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

            //Step 2 : saving to store


            else
            {

                LoadDays();
                //Display successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });

                this.patternWindow.Close();



            }

        }

        protected void SaveDayAlternation(object sender, DirectEventArgs e)
        {


            string day = e.ExtraParams["alt"];
           
            CalendarAlternation b = JSON.Deserialize<CalendarAlternation>(day);
            b.type = 1;
            b.caId = CurrentCalendar.Text;

            b.year = CurrentYear.Text;

            b.startDayId = b.dateFrom.ToString("yyyyMMdd");
            b.endDayId = b.dateTo.ToString("yyyyMMdd");

            PostRequest<CalendarAlternation> request = new PostRequest<CalendarAlternation>();
            request.entity = b;
            PostResponse<CalendarAlternation> response = _branchService.ChildAddOrUpdate<CalendarAlternation>(request);

            if (!response.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

            //Step 2 : saving to store


            else
            {

                LoadDays();
                //Display successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });

                this.alternateDaysWindow.Close();



            }

        }

        protected void SaveWeekAlternation(object sender, DirectEventArgs e)
        {


            string day = e.ExtraParams["alt"];

            CalendarAlternation b = JSON.Deserialize<CalendarAlternation>(day);
            b.type = 2;
            b.caId = CurrentCalendar.Text;

            b.year = CurrentYear.Text;

            b.startDayId = b.dateFrom.ToString("yyyyMMdd");
            b.endDayId = b.dateTo.ToString("yyyyMMdd");

            PostRequest<CalendarAlternation> request = new PostRequest<CalendarAlternation>();
            request.entity = b;
            PostResponse<CalendarAlternation> response = _branchService.ChildAddOrUpdate<CalendarAlternation>(request);

            if (!response.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

            //Step 2 : saving to store


            else
            {

                LoadDays();
                //Display successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });

                this.alternateWeekWindow.Close();



            }

        }

        protected void SetDefaultClick(object sender, DirectEventArgs e)
        {
            KeyValuePair<string, string> pair = new KeyValuePair<string, string>("caId", e.ExtraParams["id"].ToString());
            PostRequest<KeyValuePair<string, string>> req = new PostRequest<KeyValuePair<string, string>>();
            req.entity = pair;
            PostResponse<KeyValuePair<string, string>> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            else
            {
                X.Msg.Alert(Resources.Common.RecordSavingSucc, GetLocalResourceObject("DefaultSetSucc").ToString()).Show();
            }
        }

        protected void openCopyCalendar(object sender, DirectEventArgs e)
        {

            copyCalendarForm.Reset();
            ListRequest calendars = new ListRequest();
            ListResponse<WorkingCalendar> cals = _branchService.ChildGetAll<WorkingCalendar>(calendars);
            calStore.DataSource = cals.Items;
            calStore.DataBind();
            copyCalendarWindow.Show();
        }

        protected void CopyCalendar(object sender, DirectEventArgs e)
        {
            string id = e.ExtraParams["caId"];
            CalendarDayListRequest req = new CalendarDayListRequest();
            req.CalendarId = id;
            req.Year = CurrentYear.Text;
            ListResponse<Model.Attendance.CalendarDay> days = _branchService.ChildGetAll<Model.Attendance.CalendarDay>(req);
            if (!days.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", days.ErrorCode) != null ? GetGlobalResourceObject("Errors", days.ErrorCode).ToString()+ "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + days.LogId : days.Summary).Show();
                return;
            }
            else if (days.count == 0)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetLocalResourceObject("NoDays").ToString()).Show();
                return;
            }
            else
            {
                PostRequest<Model.Attendance.CalendarDay[]> copy = new PostRequest<Model.Attendance.CalendarDay[]>();
                days.Items.ForEach(x => x.caId = Convert.ToInt32(CurrentCalendar.Text));
                copy.entity = days.Items.ToArray();
                PostResponse<Model.Attendance.CalendarDay[]> response = _branchService.ChildAddOrUpdate<Model.Attendance.CalendarDay[]>(copy);
                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", days.ErrorCode) != null ? GetGlobalResourceObject("Errors", days.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") +days.LogId: days.Summary).Show();
                    return;
                }
                else
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.importCalendar, GetLocalResourceObject("CopiedSucc").ToString()).Show();
                    LoadDays();
                    return;
                }
            }
        }

    }
}