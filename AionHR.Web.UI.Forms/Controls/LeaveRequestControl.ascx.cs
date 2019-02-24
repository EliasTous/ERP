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
using Reports;
using AionHR.Services.Messaging.Reports;
using AionHR.Model.Dashboard;
using AionHR.Services.Messaging.DashBoard;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms.Controls
{
    public partial class LeaveRequestControl : System.Web.UI.UserControl
    {
        public int fromSelfService { get; set; }

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

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IHelpFunctionService _helpFunctionService = ServiceLocator.Current.GetInstance<IHelpFunctionService>();
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
                    if (fromSelfService != 1)
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(LeaveRequest), BasicInfoTab, null, null, SaveButton);
                        ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
                        classReq.ClassId = (typeof(LeaveRequest).GetCustomAttributes(typeof(ClassIdentifier), false).ToList()[0] as ClassIdentifier).ClassID;
                        classReq.UserId = _systemService.SessionHelper.GetCurrentUserId();
                        RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
                        if (modClass != null && modClass.result != null && modClass.result.accessLevel < 2)
                        {
                            ViewOnly.Text = "1";
                        }
                        if (returnNotes.InputType == InputType.Password)
                        {
                            returnNotes.Visible = false;
                            notesField.Visible = true;
                            textField1.Visible = false;
                            textField2.Visible = true;
                        }
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

        private void ImgButton_Click(object sender, ImageClickEventArgs e)
        {
            ReportCompositeRequest request = new ReportCompositeRequest();
            request.Size = "1000";
            request.StartAt = "0";



            ListResponse<AionHR.Model.Reports.RT106> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT106>(request);
            if (!resp.Success)
            {
            }

            TurnoverRate y = new TurnoverRate();
            y.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            y.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            y.DataSource = resp.Items;
            string user = _systemService.SessionHelper.GetCurrentUser();
            y.Parameters["User"].Value = user;
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);
            MemoryStream ms = new MemoryStream();
            y.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
        }

        #region public interface
        public void Update(string id)
        {
        
            try {
                leaveDaysField.SetHidden(true);
                leaveHours.SetHidden(true);
                workingHours.SetHidden(true);
                RecordRequest r = new RecordRequest();
                r.RecordID = id;
                CurrentLeave.Text = r.RecordID;
                shouldDisableLastDay.Text = "0";

                RecordResponse<LeaveRequest> response = _leaveManagementService.ChildGetRecord<LeaveRequest>(r);
                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(response);
                    return;
                }
                //Step 2 : call setvalues with the retrieved object
                this.BasicInfoTab.SetValues(response.result);



                FillLeaveType();

                ltId.Select(response.result.ltId.ToString());
                //status.Select(response.result.status);
                StoredLeaveChanged.Text = "0";
                if (response.result.employeeId != "0")
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
                    Common.errorMessage(resp);
                }

                leaveDaysStore.DataSource = resp.Items;
                resp.Items.ForEach(x => x.dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek);
                leaveDaysStore.DataBind();
                FillApprovals(id);
                LeaveChanged.Text = "0";
                X.Call("CalcSum");
                panelRecordDetails.ActiveTabIndex = 0;

                setNormal();
                if (response.result.status == 2)
                {
                    endDateHidden.Text = response.result.endDate.ToString();
                    startDateHidden.Text = response.result.startDate.ToString();
                    setApproved(true);
                }

                else if (response.result.status == 3 || response.result.status == -1)
                    setUsed(true);
                else
                { setNormal(); }
                if (ViewOnly.Text == "1")
                    SaveButton.Disabled = true;
                RefreshSecurityForControls();
                this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                this.EditRecordWindow.Show();
                X.Call("calcDays");
            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        public void Add()
        {

            //employeeId.SuspendEvents();
            //startDate.SuspendEvents();
            //endDate.SuspendEvents();
            leaveDaysField.SetHidden(false);
            leaveHours.SetHidden(false);
            workingHours.SetHidden(false);
            panelRecordDetails.SuspendEvents();
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
            status.Disabled = true;
            status.Select(0);
            shouldDisableLastDay.Text = "0";
            startDate.Value = DateTime.Now;
            this.EditRecordWindow.Show();
            panelRecordDetails.ResumeEvents();
            //employeeId.ResumeEvents();
            //endDate.ResumeEvents();
            //startDate.ResumeEvents();
            //endDate.ResumeEvent("Change");

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
           endDate.Disabled = employeeId.Disabled = justification.Disabled = destination.Disabled = /*isPaid.Disabled = */ltId.Disabled = TotalText.Disabled = disabled;
            returnDate.Disabled = !disabled;
            replacementIdCB.Disabled = disabled;
            approved.Text = disabled.ToString();
            //leavePeriod.Disabled = disabled;
            calDays.Disabled = disabled;
            leaveRef.Disabled = disabled;
            leaveDaysField.Disabled = disabled;
            leaveHours.Disabled = disabled;
            workingHours.Disabled = disabled;
            endDate.MaxDate = DateTime.Parse(endDateHidden.Text);
            endDate.MinDate = DateTime.Parse(startDateHidden.Text);
            endDateHidden.Text = "";
            startDateHidden.Text = "";
        }


        private void setNormal()
        {
            GridDisabled.Text = "False";
            startDate.Disabled = false;
            endDate.Disabled = employeeId.Disabled = justification.Disabled = destination.Disabled =/* isPaid.Disabled = */ltId.Disabled = TotalText.Disabled = false;
            returnDate.Disabled = true;
            replacementIdCB.Disabled = false;
            //leavePeriod.Disabled = false;
            approved.Text = "False";
            SaveButton.Disabled = false;
            calDays.Disabled = false;
            leaveRef.Disabled = false;
            leaveDaysField.Disabled = false;
            workingHours.Disabled = false;
            leaveHours.Disabled = false;
            endDate.MaxDate = new DateTime();
            endDate.MinDate = new DateTime();
        }

        private void setUsed(bool disabled)
        {
            GridDisabled.Text = disabled.ToString();
            startDate.Disabled = disabled;
            endDate.Disabled = employeeId.Disabled = justification.Disabled = destination.Disabled = /*isPaid.Disabled*/ ltId.Disabled = TotalText.Disabled = disabled;
            returnDate.Disabled = disabled;
            SaveButton.Disabled = disabled;
            //leavePeriod.Disabled = disabled;
            calDays.Disabled = disabled;
            leaveRef.Disabled = disabled;
            leaveDaysField.Disabled = disabled;
            workingHours.Disabled = disabled;
            leaveHours.Disabled = disabled; 

        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>



        private void LoadQuickViewInfo(string employeeId)
        {
            EmployeeQuickViewRecordRequest r = new EmployeeQuickViewRecordRequest();
            r.RecordID = employeeId;
            r.asOfDate = DateTime.Now;
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
            try
            {
                //Getting the id to check if it is an Add or an edit as they are managed within the same form.

                string obj = e.ExtraParams["values"];
                string status = e.ExtraParams["status"];

                JsonSerializerSettings settings = new JsonSerializerSettings();
                CustomResolver res = new CustomResolver();
                //  res.AddRule("leaveRequest1_employeeId", "employeeId");
                //res.AddRule("leaveRequest1_ltId", "ltId");
                // res.AddRule("leaveRequest1_status", "status");
                settings.ContractResolver = res;
                LeaveRequest b = JsonConvert.DeserializeObject<LeaveRequest>(obj, settings);
                b.status = Convert.ToInt16(status);
                b.leaveDays = Convert.ToDouble(leaveDaysField.Text);
                //b.status = Convert.ToInt16(status1); 
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
                           Common.errorMessage(r);;
                            return;
                        }
                        else
                        {
                            b.recordId = r.recordId;
                            CurrentLeave.Text = b.recordId; 
                       //     LeaveRequestNotification(b);
                            //Add this record to the store 
                            days.ForEach(d =>
                            {
                                d.leaveId = Convert.ToInt32(b.recordId);
                                d.employeeId = Convert.ToInt32(b.employeeId);
                            });
                            //AddDays(days);
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
                else
                {
                    //Update Mode

                    try
                    {
                        if (approved.Text == "True")
                        {
                            DateTime endDate = b.endDate;

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
                            //recordResponse.result.leavePeriod = leavePeriod.Text;
                            b = recordResponse.result;
                            b.status = 3;
                            b.endDate = endDate;

                            //postReq.entity = recordResponse.result;
                            //postReq.entity.returnDate = b.returnDate;
                            //postReq.entity.status = 3;
                            //PostResponse<LeaveRequest> resp = _leaveManagementService.ChildAddOrUpdate<LeaveRequest>(postReq);
                            //if (!resp.Success)
                            //{
                            //   Common.errorMessage(resp);
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
                           Common.errorMessage(r);;
                            return;
                        }

                        else
                        {
                            //LeaveRequestNotification(b);
                            CurrentLeave.Text = b.recordId;
                            //var deleteDesponse = _leaveManagementService.DeleteLeaveDays(Convert.ToInt32(b.recordId));
                            //if (!deleteDesponse.Success)//it maybe another check
                            //{
                            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            //    X.Msg.Alert(Resources.Common.Error, deleteDesponse.Summary).Show();
                            //    return;
                            //}
                            days.ForEach(d =>
                            {
                                d.leaveId = Convert.ToInt32(b.recordId);
                                d.employeeId = Convert.ToInt32(b.employeeId);
                            });

                            //AddDays(days);
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
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        protected void SaveReplacement(object sender, DirectEventArgs e)
        {
            try
            {
                string id = e.ExtraParams["id"];
                string leaveId = CurrentLeave.Text;
                string dayId = e.ExtraParams["dayId"];
                string dow = e.ExtraParams["dow"];
                string workingHours = e.ExtraParams["workingHours"];
                string leaveHours = e.ExtraParams["leaveHours"];
                string replacementIdCB = e.ExtraParams["replacementId"];
                LeaveDay lD = new LeaveDay();
                lD.recordId = id;
                lD.leaveId = Convert.ToInt32(leaveId);
                lD.dayId = dayId;
                lD.dow = Convert.ToInt16(dow);
                lD.workingHours = Convert.ToDouble(workingHours);
                lD.leaveHours = Convert.ToDouble(leaveHours);
                lD.replacementId = replacementIdCB;


                PostRequest<LeaveDay> request = new PostRequest<LeaveDay>();
                request.entity = lD;
                PostResponse<LeaveDay> r = _leaveManagementService.ChildAddOrUpdate<LeaveDay>(request);

                if (!r.Success)//it maybe another check
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }

                else
                {



                    FillLeaveDay(CurrentLeave.Text);



                    //Display successful notification
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });


                    //RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                    //sm.DeselectAll();
                    //sm.Select(b.recordId.ToString());
                   
                    replacementWindow.Close();
                }

            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
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

            req.employeeId = employeeId.Value.ToString();
            //if (req.CaId == "0")
            //{

            //    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorNoCalendar").ToString()).Show();
            //    return new List<LeaveDay>();

            //}

            ListResponse<LeaveCalendarDay> days = _helpFunctionService.ChildGetAll<LeaveCalendarDay>(req);
            if (!days.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", days.ErrorCode) != null ? GetGlobalResourceObject("Errors", days.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + days.Summary : days.Summary).Show();
                return new List<LeaveDay>();
            }
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
        protected void replacementForm_Load(object sender, EventArgs e)
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
               Common.errorMessage(response);
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
               Common.errorMessage(resp);
                return 0;
            }
            if (!resp.result.caId.HasValue)
            {
                if (_systemService.SessionHelper.GetCalendarId() == 0)
                {
                   Common.errorMessage(resp);
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
            req.employeeId = employeeId.Value.ToString();
            //if (req.CaId == "0")
            //{

            //    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorNoCalendar").ToString()).Show();
            //    return;

            //}
            ListResponse<LeaveCalendarDay> days = _helpFunctionService.ChildGetAll<LeaveCalendarDay>(req);
            if (!days.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", days.ErrorCode) != null ? GetGlobalResourceObject("Errors", days.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + days.Summary : days.Summary).Show();
                return;
            }
            List<LeaveDay> leaveDays = new List<LeaveDay>();
            days.Items.ForEach(x => leaveDays.Add(new LeaveDay() { dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek, dayId = x.dayId, workingHours = x.workingHours, leaveHours = x.workingHours }));
            if (!string.IsNullOrEmpty(CurrentLeave.Text))
            {
                LeaveDayListRequest req1 = new LeaveDayListRequest();
                req1.LeaveId = CurrentLeave.Text;
                ListResponse<LeaveDay> resp = _leaveManagementService.ChildGetAll<LeaveDay>(req1);
                if (!resp.Success)

                {

                }
                foreach (LeaveDay l in resp.Items)
                {
                    if (leaveDays.Where(x => x.dayId == l.dayId).Count() > 0)
                        leaveDays.Where(x => x.dayId == l.dayId).First().leaveHours = l.leaveHours;
                }
            }
            leaveDaysStore.DataSource = leaveDays;
            leaveDaysStore.DataBind();
            leaveDaysField.Text = leaveDays.Count.ToString();
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
            req.employeeId = employeeId.Value.ToString();
            //if (req.CaId == "0")
            //{

            //    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorNoCalendar").ToString()).Show();
            //    return;

            //}

            ListResponse<LeaveCalendarDay> days = _helpFunctionService.ChildGetAll<LeaveCalendarDay>(req);
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

        private bool AddDays(List<LeaveDay> days)
        {
            PostRequest<LeaveDay[]> req = new PostRequest<LeaveDay[]>();
            req.entity = days.ToArray();
            PostResponse<LeaveDay[]> resp = _leaveManagementService.ChildAddOrUpdate<LeaveDay[]>(req);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
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
            req.status = 0;
            req.StartAt = "0";
            req.Size = "1";
            req.SortBy = "endDate";

            ListResponse<LeaveRequest> resp = _leaveManagementService.ChildGetAll<LeaveRequest>(req);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
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
               Common.errorMessage(resp);
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

        protected void Unnamed_Event1(object sender, EventArgs e)
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

            EmployeeQuickViewRecordRequest req = new EmployeeQuickViewRecordRequest();
            req.RecordID = response.result.employeeId;
            req.asOfDate = DateTime.Now;
            RecordResponse<EmployeeQuickView> resp = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
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
            //y.Parameters["Hours"].Value = leavePeriod.Text;
            //y.Parameters["Justification"].Value = leavePeriod.Text;
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
        public void FillApprovals(string Id)
        {
            LeaveApprovalListRequest req = new LeaveApprovalListRequest();
            req.leaveId = Id;
            req.approverId = "0";
            req.status = 0;
            req.DivisionId = 0;
            req.PositionId = 0;
            req.DepartmentId = 0;
            req.BranchId = 0;
            req.EsId = 0;
            ListResponse<AionHR.Model.LeaveManagement.Approvals> response = _leaveManagementService.ChildGetAll<AionHR.Model.LeaveManagement.Approvals>(req);
            if (!response.Success)
            {
               Common.errorMessage(response);
                return;
            }
            response.Items.ForEach(x =>
            {
                switch (x.status)
                {
                    case 1:
                        x.stringStatus = GetLocalResourceObject("New").ToString();
                        break;
                    case 2:
                        x.stringStatus = GetLocalResourceObject("FieldApproved").ToString();
                        break;
                    case -1:
                        x.stringStatus = GetLocalResourceObject("FieldRefused").ToString();
                        break;
                    default:
                        x.stringStatus = "";
                        break;


                }
            }
            );
            ApprovalsStore.DataSource = response.Items;
            ApprovalsStore.DataBind();


        }

        protected void EnableStatus(object sender, DirectEventArgs e)
        {
            if (ltId.SelectedItem.Value != null)
            {
                RecordRequest r = new RecordRequest();
                r.RecordID = ltId.SelectedItem.Value;

                RecordResponse<LeaveType> response = _leaveManagementService.ChildGetRecord<LeaveType>(r);
                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(response);
                    return;
                }
                //if (!response.result.requireApproval)
                //    status.Disabled = false;
                //else
                //    status.Disabled = true;
            }
        }
        //private void LeaveRequestNotification(LeaveRequest b)
        //{
        //    RecordRequest r = new RecordRequest();
        //    r.RecordID = b.ltId.ToString();
        //    RecordResponse<LeaveType> response = _leaveManagementService.ChildGetRecord<LeaveType>(r);


        //    if (_systemService.SessionHelper.GetEmployeeId() != null /*&& response.result.requireApproval == false*/)
        //    {
        //        PostRequest<DashboardLeave> DBRequset = new PostRequest<DashboardLeave>();
        //        DBRequset.entity = new DashboardLeave() { leaveId = Convert.ToInt32(b.recordId), approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId()), status = b.status, notes = " " };
        //        PostResponse<DashboardLeave> DBResponse = _leaveManagementService.ChildAddOrUpdate<DashboardLeave>(DBRequset);
        //        if (!DBResponse.Success)
        //        {
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", DBResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", DBResponse.ErrorCode).ToString() +"<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId").ToString() + DBResponse.LogId : DBResponse.Summary).Show();
        //            return;
        //        }
        //    }

        //}
        protected void PoPuPLD(object sender, DirectEventArgs e)
        {


           

            string id = e.ExtraParams["id"];
            string leaveId = CurrentLeave.Text; 
            string dayId = e.ExtraParams["dayId"];
            string dow = e.ExtraParams["dow"];
            string workingHours = e.ExtraParams["workingHours"];
            string leaveHours = e.ExtraParams["leaveHours"];
            string replacementIdCB = e.ExtraParams["replacementId"];

            recordIdTF.Text = id;
            leaveIdTF.Text = leaveId;
            dayIdTF.Text = dayId;
            dowTF.Text = dow;
            workingHoursTF.Text = workingHours;
            leaveHoursTF.Text = leaveHours;
            replacementId.Select(replacementIdCB);
            replacementWindow.Show(); 



        }
        private void FillLeaveDay(string leaveId)
        {
            LeaveDayListRequest req = new LeaveDayListRequest();
            req.LeaveId = CurrentLeave.Text;
            ListResponse<LeaveDay> resp = _leaveManagementService.ChildGetAll<LeaveDay>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return; 
            }
            resp.Items.ForEach(x => x.dow = (short)DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).DayOfWeek);
            leaveDaysStore.DataSource = resp.Items;
            leaveDaysStore.DataBind(); 

          
        }
        protected void ApprovalsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            FillApprovals(CurrentLeave.Text);
        }

    }
}