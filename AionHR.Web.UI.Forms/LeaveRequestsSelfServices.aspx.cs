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

namespace AionHR.Web.UI.Forms
{
    public partial class LeaveRequestsSelfServices : System.Web.UI.Page
    {
        public RefreshParent RefreshLeaveCalendarCallBack { get; set; }

        public delegate void RefreshParent();

      
     




        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        ISelfServiceService _selfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();
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

             

              Column2.Format = Column1.Format = _systemService.SessionHelper.GetDateformat();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(leaveRequetsSelfservice), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                 

                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();
                    Viewport1.Hidden = true;
                    return;
                }

            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {

            //leaveRequest1.Store1 = this.Store1;
            //leaveRequest1.GrigPanel1 = this.GridPanel1;

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
                 //   leaveRequest1.Update(id);


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
       
        public void ADDNewRecord(object sender, DirectEventArgs e)
        {
            startDate.SelectedDate=endDate.SelectedDate = DateTime.Now;
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
            employeeId.GetStore().Add(new object[]
                 {
                                new
                                {
                                    recordId = _systemService.SessionHelper.GetEmployeeId(),
                                    fullName =" "

                                }
                 });

            employeeId.SetValue(_systemService.SessionHelper.GetEmployeeId());
        }

        //public void Update(string id)
        //{
        //    RecordRequest r = new RecordRequest();
        //    r.RecordID = id;
        //    CurrentLeave.Text = r.RecordID;
        //    shouldDisableLastDay.Text = "0";

        //    RecordResponse<LeaveRequest> response = _leaveManagementService.ChildGetRecord<LeaveRequest>(r);
        //    if (!response.Success)
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
        //        return;
        //    }
        //    //Step 2 : call setvalues with the retrieved object
        //    this.BasicInfoTab.SetValues(response.result);


        //    FillLeaveType();

        //    ltId.Select(response.result.ltId.ToString());
        //    //status.Select(response.result.status);
        //    StoredLeaveChanged.Text = "0";
        //    if (response.result.employeeId != "0")
        //    {

        //        employeeId.GetStore().Add(new object[]
        //           {
        //                        new
        //                        {
        //                            recordId = response.result.employeeId,
        //                            fullName =response.result.employeeName.fullName
        //                        }
        //           });
        //        employeeId.SetValue(response.result.employeeId);

        //    }
        //    LoadQuickViewInfo(response.result.employeeId);
        //    LeaveDayListRequest req = new LeaveDayListRequest();
        //    req.LeaveId = CurrentLeave.Text;
        //    ListResponse<LeaveDay> resp = _leaveManagementService.ChildGetAll<LeaveDay>(req);
        //    if (!resp.Success)
        //    {

        //    }

        //    leaveDaysStore.DataSource = resp.Items;
        //    resp.Items.ForEach(x => x.dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek);
        //    leaveDaysStore.DataBind();
           
        //    LeaveChanged.Text = "0";
        //    X.Call("CalcSum");
        //    panelRecordDetails.ActiveTabIndex = 0;

        //    setNormal();
        //    if (response.result.status == 2)

        //        setApproved(true);

        //    else if (response.result.status == 3 || response.result.status == -1)
        //        setUsed(true);
        //    else
        //    { setNormal(); }
        //    if (ViewOnly.Text == "1")
        //        SaveButton.Disabled = true;
        //    RefreshSecurityForControls();
        //    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
        //    this.EditRecordWindow.Show();
        //    X.Call("calcDays");
        //}

        private void setNormal()
        {
            GridDisabled.Text = "False";
            startDate.Disabled = false;
            endDate.Disabled = employeeId.Disabled = justification.Disabled = destination.Disabled =/* isPaid.Disabled = */ltId.Disabled = TotalText.Disabled = false;
            returnDate.Disabled = true;
            leavePeriod.Disabled = false;
            approved.Text = "False";
            SaveButton.Disabled = false;
            calDays.Disabled = false;
            leaveRef.Disabled = false;
        }
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            LeaveRequestListRequest request = new LeaveRequestListRequest();
            request.EmployeeId =Convert.ToInt32( _systemService.SessionHelper.GetEmployeeId());
            request.BranchId = 0;
            request.DepartmentId = 0;
            request.raEmployeeId = 0;
            request.status = 0;
            request.Size = "50";
            request.StartAt = e.Start.ToString();
            request.SortBy = "firstName";

            request.Filter = "";
            ListResponse<LeaveRequest> routers = _leaveManagementService.ChildGetAll<LeaveRequest>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() : routers.Summary).Show();
                return;
            }

            this.Store1.DataSource = routers.Items;
            e.Total = routers.count;

            this.Store1.DataBind();
        }


        protected void ReturnLeave(object sender, DirectEventArgs e)
        {
           // leaveRequest1.Return();
        }

        private List<LeaveDay> GenerateLeaveDays(string encoded)
        {
            List<LeaveDay> days = JsonConvert.DeserializeObject<List<LeaveDay>>(encoded);

            return days;
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
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: "+r.ErrorCode + "<br> Summary: "+r.Summary : r.Summary).Show();
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
        public void FillLeaveType()
        {

            ListRequest req = new ListRequest();

            ListResponse<LeaveType> response = _leaveManagementService.ChildGetAll<LeaveType>(req);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
                return;
            }
            ltStore.DataSource = response.Items;
            ltStore.DataBind();

        }
        private void RefreshSecurityForControls()
        {
            AccessControlApplier.ApplyAccessControlOnPage(typeof(LeaveRequest), BasicInfoTab, null, null, SaveButton);
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
                MarkLeaveChanged(sender, e);
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
            //LoadQuickViewInfo(employeeId.Value.ToString());
        }
        private int GetEmployeeCalendar(string empId)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = empId;
            RecordResponse<MyInfo> resp = _selfServiceService.ChildGetRecord<MyInfo>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return 0;
            }
            if (!resp.result.caId.HasValue)
            {
                if (_systemService.SessionHelper.GetCalendarId() == 0)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                    return 0;
                }
                return _systemService.SessionHelper.GetCalendarId();
            }
            return resp.result.caId.Value;
        }
        private void LoadQuickViewInfo(string employeeId)
        {
            EmployeeQuickViewRecordRequest r = new EmployeeQuickViewRecordRequest();
            r.RecordID = employeeId;
            r.asOfDate = DateTime.Now;
            RecordResponse<EmployeeQuickView> resp = _employeeService.ChildGetRecord<EmployeeQuickView>(r);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return;
            }

            leaveBalance.Text = resp.result.leaveBalance.ToString();
            yearsInService.Text = resp.result.serviceDuration;

        }
        [DirectMethod]
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
                return;
            }

        }
        [DirectMethod]
        public void CalcReturnDate(object sender, DirectEventArgs e)
        {
            DateTime startDate, endDate, returnDate;
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
                return;
            }
            List<LeaveDay> leaveDays = new List<LeaveDay>();
            days.Items.ForEach(x => leaveDays.Add(new LeaveDay() { dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek, dayId = x.dayId, workingHours = x.workingHours, leaveHours = x.workingHours }));
            leaveDaysStore.DataSource = leaveDays;
            leaveDaysStore.DataBind();
            X.Call("CalcSum");
            if (returnDate > endDate)
            {
                X.Call("EnableLast");
            }
        }
        protected void LeaveDays_Load(object sender, EventArgs e)
        {

        }
        protected void Button3_Click(object sender, EventArgs e)
        {

            string dateFormat = _systemService.SessionHelper.GetDateformat();
            LeaveRequestReport y = new LeaveRequestReport();
            RecordRequest r = new RecordRequest();
            r.RecordID = CurrentLeave.Text;

            RecordResponse<LeaveRequest> response = _leaveManagementService.ChildGetRecord<LeaveRequest>(r);
            if (response.result == null)
                return;
            RecordRequest qvr = new RecordRequest();
            r.RecordID = response.result.employeeId;
            RecordResponse<EmployeeQuickView> resp = _employeeService.ChildGetRecord<EmployeeQuickView>(r);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return;
            }
            try
            {
                leaveBalance.Text = resp.result.leaveBalance.ToString();
                yearsInService.Text = resp.result.serviceDuration;
                LeaveRequest request = response.result;
                y.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
                y.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

                y.Parameters["Employee"].Value = request.employeeName.fullName;
                y.Parameters["Ref"].Value = request.leaveRef;
                y.Parameters["From"].Value = request.startDate.ToString(dateFormat);
                y.Parameters["To"].Value = request.endDate.ToString(dateFormat);
                y.Parameters["Days"].Value = calDays.Text;
                y.Parameters["Hours"].Value = request.leavePeriod;
                y.Parameters["Justification"].Value = request.justification;
                y.Parameters["Destination"].Value = request.destination;
                y.Parameters["LeaveType"].Value = request.ltName;
                y.Parameters["LeaveBalance"].Value = resp.result.leaveBalance;
                y.Parameters["YearsInService"].Value = resp.result.serviceDuration;
                y.Parameters["IsPaid"].Value = request.isPaid.HasValue && request.isPaid.Value ? "Yes" : "No";
                string format = "Pdf";
                string fileName = String.Format("Report.{0}", format);
                string user = _systemService.SessionHelper.GetCurrentUser();

                MemoryStream ms = new MemoryStream();
                y.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                Response.Close();
            }
            catch
            {

            }
        }
        [DirectMethod]
        protected void Button3_DirectClick(object sender, DirectEventArgs e)
        {
            string dateFormat = _systemService.SessionHelper.GetDateformat();
            LeaveRequestReport y = new LeaveRequestReport();

            y.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            y.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            y.Parameters["Employee"].Value = employeeId.SelectedItem.Text.ToString();
            y.Parameters["Ref"].Value = leaveRef.Text;
            y.Parameters["From"].Value = startDate.SelectedDate.ToString(dateFormat);
            y.Parameters["To"].Value = endDate.SelectedDate.ToString(dateFormat);
            y.Parameters["Days"].Value = calDays.Text;
            y.Parameters["Hours"].Value = leavePeriod.Text;
            y.Parameters["Justification"].Value = leavePeriod.Text;
            y.Parameters["Destination"].Value = destination.Text;
            y.Parameters["LeaveType"].Value = ltId.SelectedItem.Text;
            y.Parameters["LeaveBalance"].Value = leaveBalance.Text;
            y.Parameters["YearsInService"].Value = yearsInService.Text;
            //y.Parameters["IsPaid"].Value = isPaid.Checked ? "Yes" : "No";
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);
            string user = _systemService.SessionHelper.GetCurrentUser();

            MemoryStream ms = new MemoryStream();
            y.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
            //Response.AddHeader("Access-Control-Allow-Origin", "*");
            //Response.BinaryWrite(ms.ToArray());
            //Response.Flush();
            //Response.Close();
            DownloadFile(ms);
        }

        public void DownloadFile(Stream stream)
        {




            //This controls how many bytes to read at a time and send to the client
            int bytesToRead = 10000;

            // Buffer to read bytes in chunk size specified above
            byte[] buffer = new Byte[bytesToRead];

            // The number of bytes read
            try
            {



                //Get the Stream returned from the response

                // prepare the response to the client. resp is the client Response

                var resp = HttpContext.Current.Response;

                //Indicate the type of data being sent
                resp.ContentType = "application/octet-stream";


                //Name the file 
                resp.AddHeader("Content-Disposition", "attachment; filename=\"Report.pdf\"");
                resp.AddHeader("Content-Length", stream.Length.ToString());

                int length;
                do
                {
                    // Verify that the client is connected.
                    if (resp.IsClientConnected)
                    {
                        // Read data into the buffer.
                        length = stream.Read(buffer, 0, bytesToRead);

                        // and write it out to the response's output stream
                        resp.OutputStream.Write(buffer, 0, length);

                        // Flush the data


                        //Clear the buffer
                        buffer = new Byte[bytesToRead];
                    }
                    else
                    {
                        // cancel the download if client has disconnected
                        length = -1;
                    }
                } while (length > 0); //Repeat until no data is read
                resp.Flush();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message + "<br />" + exp.StackTrace).Show();
                return;
            }
            finally
            {
                if (stream != null)
                {
                    //Close the input stream
                    stream.Close();
                }
            }
        }
       [DirectMethod]
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            string status1 = e.ExtraParams["status"];
            JsonSerializerSettings settings = new JsonSerializerSettings();
            CustomResolver res = new CustomResolver();
            //  res.AddRule("leaveRequest1_employeeId", "employeeId");
            //res.AddRule("leaveRequest1_ltId", "ltId");
            // res.AddRule("leaveRequest1_status", "status");

            settings.ContractResolver = res;
            leaveRequetsSelfservice b = JsonConvert.DeserializeObject<leaveRequetsSelfservice>(obj, settings);
            b.status = Convert.ToInt16(status1);
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
                    PostRequest<leaveRequetsSelfservice> request = new PostRequest<leaveRequetsSelfservice>();

                    request.entity = b;
                    PostResponse<leaveRequetsSelfservice> r = _selfServiceService.ChildAddOrUpdate<leaveRequetsSelfservice>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: "+r.ErrorCode + "<br> Summary: "+r.Summary : r.Summary).Show();
                        return;
                    }
                    else
                    {
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        days.ForEach(d => d.leaveId = Convert.ToInt32(b.recordId));
                        AddDays(days);
                        if (Store1 != null)
                            this.Store1.Reload();
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
                        //b = recordResponse.result;
                        b.status = 3;

                        //postReq.entity = recordResponse.result;
                        //postReq.entity.returnDate = b.returnDate;
                        //postReq.entity.status = 3;
                        //PostResponse<LeaveRequest> resp = _leaveManagementService.ChildAddOrUpdate<LeaveRequest>(postReq);
                        //if (!resp.Success)
                        //{
                        //    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
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
                    //request.entity = b;
                    PostResponse<LeaveRequest> r = _leaveManagementService.ChildAddOrUpdate<LeaveRequest>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: "+r.ErrorCode + "<br> Summary: "+r.Summary : r.Summary).Show();
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
                        if (Store1 != null)
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
                            if (RefreshLeaveCalendarCallBack != null)
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
        private bool AddDays(List<LeaveDay> days)
        {
            PostRequest<LeaveDay[]> req = new PostRequest<LeaveDay[]>();
            req.entity = days.ToArray();
            PostResponse<LeaveDay[]> resp = _leaveManagementService.ChildAddOrUpdate<LeaveDay[]>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return false;
            }
            return true;
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
        protected void closing(object sender, DirectEventArgs e)
        {
            leaveDaysStore.DataSource = new List<LeaveDay>();
            leaveDaysStore.DataBind();
        }






    }
}