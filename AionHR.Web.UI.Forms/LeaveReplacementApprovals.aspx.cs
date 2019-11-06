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
using AionHR.Model.SelfService;
using AionHR.Services.Messaging.SelfService;

namespace AionHR.Web.UI.Forms
{
    public partial class LeaveReplacementApprovals : System.Web.UI.Page
    {

        [DirectMethod]
        public object FillReplacementEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<EmployeeSnapShot> employees = Common.GetEmployeesFiltered(prms.Query);
            employees = employees.Where(x => x.activeStatus == Convert.ToInt16(ActiveStatus.ACTIVE)).ToList();


            return employees;

        }



        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<EmployeeSnapShot> employees = Common.GetEmployeesFiltered(prms.Query);
            employees = employees.Where(x => x.activeStatus == Convert.ToInt16(ActiveStatus.ACTIVE)).ToList();
            return employees;

        }


        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }




        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ISelfServiceService _selfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();



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



                Column2.Format = Column1.Format = _systemService.SessionHelper.GetDateformat();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(LeaveRequest), null, GridPanel1, null, null);

                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
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


        private void LoadQuickViewInfo(string employeeId, DateTime? startDate = null)
        {
            EmployeeQuickViewRecordRequest r = new EmployeeQuickViewRecordRequest();
            r.RecordID = employeeId;

            r.asOfDate = startDate == null ? DateTime.Now : (DateTime)startDate;

            RecordResponse<EmployeeQuickView> resp = _employeeService.ChildGetRecord<EmployeeQuickView>(r);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }

            leaveBalance.Text = resp.result.leaveBalance.ToString();
            yearsInService.Text = resp.result.serviceDuration;

        }


        private void setApproved(bool disabled)
        {
         
            startDate.Disabled = disabled;
            endDate.Disabled = employeeId.Disabled = justification.Disabled = destination.Disabled = /*isPaid.Disabled = */ltId.Disabled =  disabled;
          
            replacementIdCB.Disabled = disabled;
            
            //leavePeriod.Disabled = disabled;
            calDays.Disabled = disabled;
            leaveRef.Disabled = disabled;
            leaveDaysField.Disabled = disabled;
            leaveHours.Disabled = disabled;
            workingHours.Disabled = disabled;
           
           
        }


        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string replApStatus = e.ExtraParams["replApStatus"];
            CurrentLeaveId.Text = id;
            apStatus.disableApprovalStatus(true);
            SaveButton.Hidden = true;
            switch (type)
            {
                case "imgEdit":

                    //Step 1 : get the object from the Web Service 
                
                    //startDate.SuspendEvent("change");
                    //endDate.SuspendEvent("change");
                    leaveDaysField.SetHidden(true);
                    leaveHours.SetHidden(true);
                    workingHours.SetHidden(true);
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;
                   // CurrentLeave.Text = r.RecordID;
                  //  shouldDisableLastDay.Text = "0";

                    RecordResponse<LeaveRequest> response = _selfServiceService.ChildGetRecord<LeaveRequest>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    LeaveApprovalStatusControl.setApprovalStatus(response.result.apStatus.ToString());
                 //   calDays.Value = (response.result.endDate - response.result.startDate).Days + 1;
                    
                    FillLeaveType();

                    ltId.Select(response.result.ltId.ToString());
                    //status.Select(response.result.status);
                  //  StoredLeaveChanged.Text = "0";
                    if (response.result.employeeId != "0")
                    {

                        employeeId.GetStore().Add(new object[]
                           {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName
                                }
                           });
                        employeeId.SetValue(response.result.employeeId);

                    }
                    LoadQuickViewInfo(response.result.employeeId, response.result.startDate);

                    LeaveDayListRequest req = new LeaveDayListRequest();
                    req.LeaveId = r.RecordID;
                    ListResponse<LeaveDay> resp = _leaveManagementService.ChildGetAll<LeaveDay>(req);
                    if (!resp.Success)
                    {
                        Common.errorMessage(resp);
                        return;
                    }

                    ;
                    resp.Items.ForEach(x => x.dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek);



                    panelRecordDetails.ActiveTabIndex = 0;





                    //else if (response.result.status == 3 || response.result.status == -1)
                    //    setUsed(true);
                    //else
                    //{ setNormal(); }
                    setApproved(true);
                    calDays.Value = (response.result.endDate - response.result.startDate).Days + 1;
                    apStatus.setApprovalStatus(replApStatus);
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();
                    //employeeId.ResumeEvent("select");
                    //startDate.ResumeEvent("change");
                    //endDate.ResumeEvent("change");
                    if (replApStatus == "1")
                    {
                        apStatus.disableApprovalStatus(false);
                        SaveButton.Hidden = false;
                    }
            
           


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
      
     
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            List<XMLDictionary> statusList = Common.XMLDictionaryList(_systemService, "13");

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;




            LeaveReplacementApprovalListRequest req = new LeaveReplacementApprovalListRequest();

            req.employeeId = _systemService.SessionHelper.GetEmployeeId();
            req.apStatus = "0";
            req.StartAt = e.Start.ToString();
            req.Size = "50";
            req.SortBy = "recordId";


            ListResponse<LeaveReplacementApproval> routers = _selfServiceService.ChildGetAll<LeaveReplacementApproval>(req);

            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            routers.Items.ForEach(x =>
            {
                x.statusString = statusList.Where(y => y.key ==Convert.ToInt16( x.replApStatus)).Count() != 0 ? statusList.Where(y => y.key == Convert.ToInt16(x.replApStatus)).First().value : "";

            }

               );

            this.Store1.DataSource = routers.Items;
            e.Total = routers.count;

            this.Store1.DataBind();
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

        //[DirectMethod]
        //public void CalcReturnDate(object sender, DirectEventArgs e)
        //{
        //    DateTime startDate, endDate, returnDate;
        //    try
        //    {
        //        startDate = DateTime.Parse(e.ExtraParams["startDate"]);
        //        endDate = DateTime.Parse(e.ExtraParams["endDate"]);
        //        returnDate = DateTime.Parse(e.ExtraParams["returnDate"]);
        //        //LeaveChanged.Text = "1";
        //    }
        //    catch
        //    {

        //        panelRecordDetails.ActiveTabIndex = 0;
        //        return;
        //    }
        //    if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
        //    {

        //        return;
        //    }
        //    string startDay = startDate.ToString("yyyyMMdd");
        //    string returnDay = returnDate.ToString("yyyyMMdd");

        //    LeaveCalendarDayListRequest req = new LeaveCalendarDayListRequest();
        //    req.StartDayId = startDay;
        //    req.EndDayId = returnDay;
        //    req.IsWorkingDay = true;
        //    int bulk;
        //    if (string.IsNullOrEmpty(employeeId.Value.ToString()) || !int.TryParse(employeeId.Value.ToString(), out bulk))
        //    {

        //        return;
        //    }
        //    req.employeeId = employeeId.Value.ToString();
        //    //if (req.CaId == "0")
        //    //{

        //    //    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorNoCalendar").ToString()).Show();
        //    //    return;

        //    //}

        //    ListResponse<LeaveCalendarDay> days = _helpFunctionService.ChildGetAll<LeaveCalendarDay>(req);
        //    if (!days.Success)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, days.Summary).Show();
        //        return;
        //    }
        //    List<LeaveDay> leaveDays = new List<LeaveDay>();
        //    days.Items.ForEach(x => leaveDays.Add(new LeaveDay() { dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek, dayId = x.dayId, workingHours = x.workingHours, leaveHours = x.workingHours }));
        //    leaveDaysStore.DataSource = leaveDays;
        //    leaveDaysStore.DataBind();
        //    X.Call("CalcSum");
        //    if (returnDate > endDate)
        //    {
        //        X.Call("EnableLast");
        //    }
        //}
        public void FillLeaveType()
        {

            ListRequest req = new ListRequest();

            ListResponse<LeaveType> response = _leaveManagementService.ChildGetAll<LeaveType>(req);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return;
            }
            ltStore.DataSource = response.Items;
            ltStore.DataBind();

        }


        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {
            try
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
                b.apStatus = Convert.ToInt16(LeaveApprovalStatusControl.GetApprovalStatus());
                //  b.leaveDays = Convert.ToDouble(leaveDaysField.Text);
                //b.status = Convert.ToInt16(status1); 
                string id = e.ExtraParams["id"];
                // Define the object to add or edit as null
                if (!b.isPaid.HasValue)
                    b.isPaid = false;

                if (employeeId.SelectedItem != null)

                    b.employeeName = employeeId.SelectedItem.Text;
                if (ltId.SelectedItem != null)

                    b.ltName = ltId.SelectedItem.Text;

                //List<LeaveDay> days = GenerateLeaveDays(e.ExtraParams["days"]);

              

                    try
                    {
                    RecordRequest rec = new RecordRequest();
                    rec.RecordID = CurrentLeaveId.Text;
                    RecordResponse<LeaveRequest> recordResponse = _leaveManagementService.ChildGetRecord<LeaveRequest>(rec);


                    if (!recordResponse.Success)
                    {
                        Common.errorMessage(recordResponse);
                        return;
                    }
                    PostRequest<LeaveRequest> request = new PostRequest<LeaveRequest>();

                    request.entity = recordResponse.result;
                       request.entity.replApStatus = apStatus.GetApprovalStatus();
                        PostResponse<LeaveRequest> r = _leaveManagementService.ChildAddOrUpdate<LeaveRequest>(request);


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
                            b.recordId = r.recordId;
                           
                            Store1.Reload();


                            //Display successful notification
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordSavingSucc
                            });


                            //RecordRequest rec = new RecordRequest();
                            //rec.RecordID = b.recordId;
                            //RecordResponse<LeaveRequest> recordResponse = _leaveManagementService.ChildGetRecord<LeaveRequest>(rec);
                            //if (!recordResponse.Success)
                            //{
                            //    X.Msg.Alert(Resources.Common.Error, recordResponse.Summary).Show();
                            //    return;
                            //}
                            //leaveRef.Text = recordResponse.result.leaveRef;
                            //this.EditRecordWindow.Close();
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
               
                
            
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }


        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                LeaveRequest s = new LeaveRequest();
                s.recordId = index;
                s.destination = "";
                s.employeeId = "0";
                s.endDate = DateTime.Now;
                s.startDate = DateTime.Now;
                s.apStatus = 0;
                s.isPaid = false;
                s.justification = "";
                s.ltId = 0;

                PostRequest<LeaveRequest> req = new PostRequest<LeaveRequest>();
                req.entity = s;
                PostResponse<LeaveRequest> r = _leaveManagementService.ChildDelete<LeaveRequest>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
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
        public void MarkLeaveChanged(object sender, DirectEventArgs e)
        {
            DateTime startDate, endDate;
            try
            {
                startDate = DateTime.Parse(e.ExtraParams["startDate"]);
                endDate = DateTime.Parse(e.ExtraParams["endDate"]);
                //LeaveChanged.Text = "1";
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
            req.employeeId = employeeId.Value.ToString();
            //if (req.CaId == "0")
            //{

            //    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorNoCalendar").ToString()).Show();
            //    return;

            ////}
            //ListResponse<LeaveCalendarDay> days = _helpFunctionService.ChildGetAll<LeaveCalendarDay>(req);
            //if (!days.Success)
            //{
            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", days.ErrorCode) != null ? GetGlobalResourceObject("Errors", days.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + days.Summary : days.Summary).Show();
            //    return;
            //}
            //List<LeaveDay> leaveDays = new List<LeaveDay>();
            //days.Items.ForEach(x => leaveDays.Add(new LeaveDay() { dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek, dayId = x.dayId, workingHours = x.workingHours, leaveHours = x.workingHours }));
            //if (!string.IsNullOrEmpty(CurrentLeave.Text))
            //{
            //    LeaveDayListRequest req1 = new LeaveDayListRequest();
            //    req1.LeaveId = CurrentLeave.Text;
            //    ListResponse<LeaveDay> resp = _leaveManagementService.ChildGetAll<LeaveDay>(req1);
            //    if (!resp.Success)

            //    {

            //    }
            //    foreach (LeaveDay l in resp.Items)
            //    {
            //        if (leaveDays.Where(x => x.dayId == l.dayId).Count() > 0)
            //            leaveDays.Where(x => x.dayId == l.dayId).First().leaveHours = l.leaveHours;
            //    }
            //}
            //leaveDaysStore.DataSource = leaveDays;
            //leaveDaysStore.DataBind();
           // leaveDaysField.Text = leaveDays.Count.ToString();
            X.Call("CalcSum");
            LoadQuickViewInfo(employeeId.Value.ToString());
        }


    }
}