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

namespace AionHR.Web.UI.Forms
{
    public partial class DailySchedule : System.Web.UI.Page
    {
        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IHelpFunctionService _helpFunctionService = ServiceLocator.Current.GetInstance<IHelpFunctionService>();
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
                FillBranches();

            }


        }

        private void FillBranches()
        {
            ListRequest request = new ListRequest();
            request.Filter = string.Empty;
            ListResponse<Branch> branches = _branchService.ChildGetAll<Branch>(request);
            if (!branches.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString() : branches.Summary).Show();
                return;
            }
            this.branchStore.DataSource = branches.Items;
            this.branchStore.DataBind();
        }

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {
            string branchID = string.Empty;
            if (branchId.Value == null || branchId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectBranch")).Show();
                return null;
            }
            else
                branchID = branchId.Value.ToString();



            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);

            data.ForEach(s => s.fullName = s.name.fullName);
            //  return new
            // {

            this.cmbEmployeeImport.Value = string.Empty;
            return data;
            //};

        }

        public void Import_Click(object sender, DirectEventArgs e)
        {
            string branchID = string.Empty;
            if (employeeId.Value == null || employeeId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectEmployee")).Show();
                return;
            }
            if (cmbEmployeeImport.Value == null || cmbEmployeeImport.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectImport")).Show();
                return;
            }
            else
                branchID = branchId.Value.ToString();

            if (dateFrom.SelectedDate == DateTime.MinValue || dateTo.SelectedDate == DateTime.MinValue)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ValidFromToDate")).Show();
                return;
            }
            if (dateFrom.SelectedDate > dateTo.SelectedDate)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ToDateHigherFromDate")).Show();
                return;
            }


            FlatScheduleImport fs = new FlatScheduleImport();
            fs.toEmployeeId = Convert.ToInt32(employeeId.Value.ToString());
            fs.fromEmployeeId = Convert.ToInt32(cmbEmployeeImport.Value.ToString());
            fs.fromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            fs.toDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
            PostRequest<FlatScheduleImport> request = new PostRequest<FlatScheduleImport>();

            request.entity = fs;

            PostResponse<FlatScheduleImport> r = _helpFunctionService.ChildAddOrUpdate<FlatScheduleImport>(request);


            //check if the insert failed
            if (!r.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: " + r.ErrorCode + "<br> Summary: " + r.Summary : r.Summary).Show();
                return;
            }
            else
            {

                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = (string)GetLocalResourceObject("ImportSuccess")
                });


                //Reload Again
                BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
                reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
                reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
                reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
                ListResponse<FlatSchedule> response = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqFS);
                if (!response.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                    return;
                }
                this.dayId.Value = string.Empty;
                BuildSchedule(response.Items);
                this.cmbEmployeeImport.Value = string.Empty;
            }


        }

        [DirectMethod]
        public object FillImportEmployee(string action, Dictionary<string, object> extraParams)
        {
            string branchID = string.Empty;
            if (employeeId.Value == null || employeeId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectEmployee")).Show();
                return null;
            }
            if (branchId.Value == null || branchId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectBranch")).Show();
                return null;
            }
            else
                branchID = branchId.Value.ToString();



            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            string recordId = employeeId.Value.ToString();
            data = data.Where(a => a.recordId != recordId).ToList();
            data.ForEach(s => s.fullName = s.name.fullName);
            //  return new
            // {
            return data;
            //};

        }

        protected void Delete_Click(object sender, DirectEventArgs e)
        {

            X.Msg.Confirm(Resources.Common.Confirmation, (string)GetLocalResourceObject("ConfirmDeleteAllDays"), new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    //We are call a direct request metho for deleting a record
                    Handler = String.Format("App.direct.DayRangeDelete()"),
                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Text = Resources.Common.No
                }

            }).Show();


        }

        [DirectMethod(ShowMask = true)]
        public void DayRangeDelete()
        {
            if (employeeId.Value == null || employeeId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectEmployee")).Show();
                return;
            }

            if (dateFrom.SelectedDate == DateTime.MinValue || dateTo.SelectedDate == DateTime.MinValue)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ValidFromToDate")).Show();
                return;
            }
            if (dateFrom.SelectedDate > dateTo.SelectedDate)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ToDateHigherFromDate")).Show();
                return;
            }

            FlatScheduleRange fs = new FlatScheduleRange();
            fs.employeeId = Convert.ToInt32(employeeId.Value.ToString());
            fs.fromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            fs.toDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
            PostRequest<FlatScheduleRange> request = new PostRequest<FlatScheduleRange>();

            request.entity = fs;
            PostResponse<FlatScheduleRange> r = _timeAttendanceService.ChildDelete<FlatScheduleRange>(request);


            //check if the insert failed
            if (!r.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: " + r.ErrorCode + "<br> Summary: " + r.Summary : r.Summary).Show();
                return;
            }
            else
            {
                //Reload Again
                BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
                reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
                reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
                reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
                ListResponse<FlatSchedule> response = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqFS);
                if (!response.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                    return;
                }
                BuildSchedule(response.Items);

            }
        }


        protected void DeleteDay_Click(object sender, DirectEventArgs e)
        {
            X.Msg.Confirm(Resources.Common.Confirmation, (string)GetLocalResourceObject("ConfirmDeleteDay"), new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    //We are call a direct request metho for deleting a record
                    Handler = String.Format("App.direct.DayDelete()"),
                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Text = Resources.Common.No
                }

            }).Show();
        }

        [DirectMethod(ShowMask = true)]
        public void DayDelete()
        {
            if (employeeId.Value == null || employeeId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectEmployee")).Show();
                return;
            }

            if (dayId.Value == null || dayId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectDay")).Show();
                return;
            }





            FlatSchedule fs = new FlatSchedule();
            fs.employeeId = Convert.ToInt32(employeeId.Value.ToString());
            fs.dayId = dayId.Value.ToString();
            fs.shiftId = 0;
            PostRequest<FlatSchedule> request = new PostRequest<FlatSchedule>();

            request.entity = fs;
            PostResponse<FlatSchedule> r = _timeAttendanceService.ChildDelete<FlatSchedule>(request);


            //check if the insert failed
            if (!r.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: " + r.ErrorCode + "<br> Summary: " + r.Summary : r.Summary).Show();
                return;
            }

            else
            {
                //List<string> listIds = new List<string>();
                //DateTime activeDate = DateTime.ParseExact(dayId.Value.ToString(), "yyyyMMdd", new CultureInfo("en"));
                //DateTime fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, 0,0, 0);
                //DateTime fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, 0, 0, 0);

                //do
                //{
                //    listIds.Add(fsfromDate.ToString("yyyyMMdd") + "_" + fsfromDate.ToString("HH:mm"));
                //    fsfromDate = fsfromDate.AddMinutes(30);
                //} while (fsToDate >= fsfromDate);

                X.Call("DeleteDaySchedule", dayId.Value.ToString());
            }
        }
        protected void Clear_Click(object sender, DirectEventArgs e)
        {

            this.branchId.Value = string.Empty;
            this.EmployeeStore = null;
            this.employeeId.Value = string.Empty;
            this.cmbEmployeeImport.Value = string.Empty;
            this.btnSave.Disabled = true; this.btnDeleteDay.Disabled = true;
            this.timeFrom.MinTime = TimeSpan.FromMinutes(0);
            this.timeTo.MinTime = TimeSpan.FromMinutes(0);
            this.timeTo.MaxTime = new TimeSpan(23, 30, 0);
            this.pnlSchedule.Html = string.Empty;
            this.dayId.Value = string.Empty;
        }
        protected void BranchAvailability_Click(object sender, DirectEventArgs e)
        {
            if (branchId.Value == null || branchId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectBranch")).Show();
                return;
            }

            if (dateFrom.SelectedDate == DateTime.MinValue || dateTo.SelectedDate == DateTime.MinValue)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ValidFromToDate")).Show();
                return;
            }
            if (dateFrom.SelectedDate > dateTo.SelectedDate)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ToDateHigherFromDate")).Show();
                return;
            }

            //Proceed to load

            BranchAvailabilityScheduleRecordRequest reqFS = new BranchAvailabilityScheduleRecordRequest();
            reqFS.BranchId = Convert.ToInt32(branchId.Value.ToString());
            reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
            ListResponse<FlatScheduleBranchAvailability> response = _helpFunctionService.ChildGetAll<FlatScheduleBranchAvailability>(reqFS);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                return;
            }
            BuildAvailability(response.Items);
        }
        protected void Load_Click(object sender, DirectEventArgs e)
        {
            if (employeeId.Value == null || employeeId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectEmployee")).Show();
                return;
            }

            if (dateFrom.SelectedDate == DateTime.MinValue || dateTo.SelectedDate == DateTime.MinValue)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ValidFromToDate")).Show();
                return;
            }
            if (dateFrom.SelectedDate > dateTo.SelectedDate)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ToDateHigherFromDate")).Show();
                return;
            }

            //Proceed to load

            BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
            reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
            reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
            ListResponse<FlatSchedule> response = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqFS);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                return;
            }
            this.dayId.Value = string.Empty;
            BuildSchedule(response.Items);
        }
        protected void Save_Click(object sender, DirectEventArgs e)
        {
            if (employeeId.Value == null || employeeId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectEmployee")).Show();
                return;
            }

            if (dayId.Value == null || dayId.Value.ToString() == string.Empty)
            {
                if (dateFrom.SelectedDate == DateTime.MinValue || dateTo.SelectedDate == DateTime.MinValue)
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectdayOrFromToDate")).Show();
                    return;
                }
                if (dateFrom.SelectedDate > dateTo.SelectedDate)
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ToDateHigherFromDate")).Show();
                    return;
                }


            }


            DateTime activeTime = DateTime.ParseExact(dayId.Value.ToString()==string.Empty? dateFrom.SelectedDate.ToString("yyyyMMdd") : dayId.Value.ToString(), "yyyyMMdd", new CultureInfo("en"));
            DateTime fsfromTime = new DateTime(activeTime.Year, activeTime.Month, activeTime.Day, timeFrom.SelectedTime.Hours, timeFrom.SelectedTime.Minutes, 0);
            DateTime fsToTime = new DateTime(activeTime.Year, activeTime.Month, activeTime.Day, timeTo.SelectedTime.Hours, timeTo.SelectedTime.Minutes, 0);
            if (timeTo.SelectedTime.ToString() == "00:00:00")
            {
                fsToTime = fsToTime.AddDays(1);
            }

            if ((fsfromTime.ToString("tt") == "AM" && "AM" == fsToTime.ToString("tt")) || ((fsfromTime.ToString("tt") == "PM" && "PM" == fsToTime.ToString("tt"))))
            {
                if (fsfromTime >= fsToTime)
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ValideFromToTime")).Show();
                    return;
                }
            }

            if ((fsfromTime > fsToTime) && (fsfromTime.ToString("tt") == "PM" && "AM" == fsToTime.ToString("tt")))
            {
                fsToTime = fsToTime.AddDays(1);
            }


            //if (TimeSpan.Compare(timeFrom.SelectedTime, timeTo.SelectedTime) >= 0 && timeTo.SelectedTime.ToString() != "00:00:00")
            //{
            //    X.Msg.Alert(Resources.Common.Error, "Please Select a valid from and To times").Show();
            //    return;
            //}


            //Proceed to Save SelectdayOrFromToDate

            if (dayId.Value != null && dayId.Value.ToString() != string.Empty)
            {
                FlatSchedule fs = new FlatSchedule();
                fs.from = fsfromTime.ToString("HH:mm");
                fs.to = fsToTime.ToString("HH:mm");
                fs.employeeId = Convert.ToInt32(employeeId.Value.ToString());
                fs.dayId = dayId.Value.ToString();
                PostRequest<FlatSchedule> request = new PostRequest<FlatSchedule>();

                request.entity = fs;
                PostResponse<FlatSchedule> r = _timeAttendanceService.ChildAddOrUpdate<FlatSchedule>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: " + r.ErrorCode + "<br> Summary: " + r.Summary : r.Summary).Show();
                    return;
                }
                else
                {
                    List<string> listIds = new List<string>();
                    DateTime effectiveDate = fsfromTime;
                    do
                    {
                        listIds.Add(effectiveDate.ToString("yyyyMMdd") + "_" + fsfromTime.ToString("HH:mm"));
                        fsfromTime = fsfromTime.AddMinutes(30);
                    } while (fsToTime >= fsfromTime);

                    X.Call("ColorifySchedule", JSON.JavaScriptSerialize(listIds));
                }
            }
            else
            {
                FlatBulkSchedule fs = new FlatBulkSchedule();
                fs.shiftStart = fsfromTime.ToString("HH:mm");
                fs.shiftEnd = fsToTime.ToString("HH:mm");
                fs.employeeId = Convert.ToInt32(employeeId.Value.ToString());
                fs.fromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
                fs.toDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
                PostRequest<FlatBulkSchedule> request = new PostRequest<FlatBulkSchedule>();

                request.entity = fs;
                PostResponse<FlatBulkSchedule> r = _helpFunctionService.ChildAddOrUpdate<FlatBulkSchedule>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: " + r.ErrorCode + "<br> Summary: " + r.Summary : r.Summary).Show();
                    return;
                }
                else
                {
                    //Reload

                    //Reload Again
                    BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
                    reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
                    reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
                    reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
                    ListResponse<FlatSchedule> response = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqFS);
                    if (!response.Success)
                    {
                        X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                        return;
                    }

                    BuildSchedule(response.Items);
                }

            }
        }

        private void BuildSchedule(List<FlatSchedule> items)
        {
            string html = @"<div style = 'margin: 5px auto; width: 99%; height: 98%; overflow:auto;' > 
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0' >";

            //CAlling the branch cvailability before proceeding

            string startAt, closeAt = string.Empty;
            GetBranchSchedule(out startAt, out closeAt);
            if (string.IsNullOrEmpty(startAt) || string.IsNullOrEmpty(closeAt))
            {
                html += @"</table></div>";
                this.pnlSchedule.Html = html;
                X.Call("DisableTools");
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorBranchWorkingHours")).Show();
                return;
            }

            TimeSpan tsStart = TimeSpan.Parse(startAt);
            timeFrom.MinTime = tsStart;
            timeTo.MinTime = tsStart.Add(TimeSpan.FromMinutes(30));
            TimeSpan tsClose = TimeSpan.Parse(closeAt);
            timeTo.MaxTime = tsClose;



            //Filling The Times Slot
            List<TimeSlot> timesList = new List<TimeSlot>();
            DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsStart.Hours, tsStart.Minutes, 0);
            DateTime dtEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsClose.Hours, tsClose.Minutes, 0);
            do
            {
                TimeSlot ts = new TimeSlot();
                ts.ID = dtStart.ToString("HH:mm");
                ts.Time = dtStart.ToString("HH:mm");
                timesList.Add(ts);
                dtStart = dtStart.AddMinutes(30);
            } while (dtStart <= dtEnd);

            //filling the Day slots
            int totalDays = Convert.ToInt32(Math.Ceiling((dateTo.SelectedDate - dateFrom.SelectedDate).TotalDays));

            html = FillFirstRow(html, timesList);

            html = FillOtherRow(html, timesList, totalDays);

            //Preparing the ids to get colorified
            List<string> listIds = new List<string>();
            foreach (FlatSchedule fs in items)
            {
                DateTime activeDate = DateTime.ParseExact(fs.dayId, "yyyyMMdd", new CultureInfo("en"));
                DateTime fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.from.Split(':')[0]), Convert.ToInt32(fs.from.Split(':')[1]), 0);
                DateTime fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.to.Split(':')[0]), Convert.ToInt32(fs.to.Split(':')[1]), 0);

                do
                {
                    listIds.Add(fsfromDate.ToString("yyyyMMdd") + "_" + fsfromDate.ToString("HH:mm"));
                    fsfromDate = fsfromDate.AddMinutes(30);
                } while (fsToDate >= fsfromDate);

            }



            html += @"</table></div>";
            this.pnlSchedule.Html = html;
            X.Call("ColorifySchedule", JSON.JavaScriptSerialize(listIds));
            X.Call("Init");
            X.Call("DisableTools");
        }


        private void BuildAvailability(List<FlatScheduleBranchAvailability> items)
        {
            string html = @"<div style = 'margin: 5px auto; width: 99%; height: 98%; overflow:auto;' > 
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0' >";

            //CAlling the branch cvailability before proceeding

            string startAt, closeAt = string.Empty;
            GetBranchSchedule(out startAt, out closeAt);
            if (string.IsNullOrEmpty(startAt) || string.IsNullOrEmpty(closeAt))
            {




                html += @"</table></div>";
                this.pnlSchedule.Html = html;

                X.Call("DisableTools");
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorBranchWorkingHours")).Show();

                return;
            }

            TimeSpan tsStart = TimeSpan.Parse(startAt);
            timeFrom.MinTime = tsStart;
            timeTo.MinTime = tsStart.Add(TimeSpan.FromMinutes(30));
            TimeSpan tsClose = TimeSpan.Parse(closeAt);
            timeTo.MaxTime = tsClose;



            //Filling The Times Slot
            List<TimeSlot> timesList = new List<TimeSlot>();
            DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsStart.Hours, tsStart.Minutes, 0);
            DateTime dtEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsClose.Hours, tsClose.Minutes, 0);
            do
            {
                TimeSlot ts = new TimeSlot();
                ts.ID = dtStart.ToString("HH:mm");
                ts.Time = dtStart.ToString("HH:mm");
                timesList.Add(ts);
                dtStart = dtStart.AddMinutes(30);
            } while (dtStart <= dtEnd);

            //filling the Day slots
            int totalDays = Convert.ToInt32(Math.Ceiling((dateTo.SelectedDate - dateFrom.SelectedDate).TotalDays));

            html = FillFirstRow(html, timesList);

            html = FillOtherRow(html, timesList, totalDays);

            //Preparing the ids to get colorified
            List<Availability> listIds = new List<Availability>();
            foreach (FlatScheduleBranchAvailability fs in items)
            {
                Availability avb = new Availability();
                DateTime activeDate = DateTime.ParseExact(fs.dayId, "yyyyMMdd", new CultureInfo("en"));
                DateTime fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.time.Split(':')[0]), Convert.ToInt32(fs.time.Split(':')[1]), 0);

                avb.Id = fsfromDate.ToString("yyyyMMdd") + "_" + fsfromDate.ToString("HH:mm");
                avb.Count = fs.cnt.ToString();

                listIds.Add(avb);



            }



            html += @"</table></div>";
            X.Call("BranchAvailability");
            this.pnlSchedule.Html = html;
            X.Call("ColorifyAndCountSchedule", JSON.JavaScriptSerialize(listIds));


        }

        private void GetBranchSchedule(out string startAt, out string closeAt)
        {
            BranchWorkRecordRequest reqBS = new BranchWorkRecordRequest();
            reqBS.BranchId = branchId.Value.ToString();
            reqBS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqBS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");

            ListResponse<BranchSchedule> response = _helpFunctionService.ChildGetAll<BranchSchedule>(reqBS);
            if (response.Success)
            {
                startAt = response.Items[0].openAt;
                closeAt = response.Items[0].closeAt;
            }
            else
            {
                startAt = string.Empty;
                closeAt = string.Empty;
            }
        }

        private string FillOtherRow(string html, List<TimeSlot> timesList, int totalDays)
        {
            DateTime firstDate = dateFrom.SelectedDate;

            for (int count = 0; count <= totalDays; count++)
            {
                html += "<tr>";
                if (!_systemService.SessionHelper.CheckIfArabicSession())
                {
                    html += "<td id=" + firstDate.ToString("yyyyMMdd") + " class='day'>" + firstDate.ToString("ddd, MMM d") + "</td>";
                }
                else
                {
                    string day = firstDate.ToString("ddd");
                    string dayNumber = firstDate.ToString("dd");
                    string month = firstDate.ToString("MM");
                    html += "<td id=" + firstDate.ToString("yyyyMMdd") + " class='day'>" + string.Format("{0} {1} - {2}", (string)GetLocalResourceObject(day), dayNumber, month) + "</td>";
                }

                for (int index = 0; index < timesList.Count; index++)
                {
                    html += "<td id=" + firstDate.ToString("yyyyMMdd") + "_" + timesList[index].ID + "></td>";
                }
                html += "</tr>";
                firstDate = firstDate.AddDays(1);
            }
            return html;
        }

        private string FillFirstRow(string html, List<TimeSlot> timesList)
        {

            html += "<tr><th style='width:95px;'></th>";
            for (int index = 0; index < timesList.Count; index++)
            {
                html += "<th>" + timesList[index].Time + "</th>";
            }
            html += "</tr>";
            return html;
        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = branchId.Value.ToString();
            req.IncludeIsInactive = 2;
            req.SortBy = "firstName";

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;
            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            if (!response.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
            return response.Items;
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

        [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }






    }

    internal class Availability
    {
        public string Count { get; set; }
        public string Id { get; set; }
    }
}