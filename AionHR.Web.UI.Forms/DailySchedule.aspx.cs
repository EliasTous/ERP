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
using AionHR.Services.Messaging.System;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Model.HelpFunction;
using AionHR.Model.System;
using System.Text.RegularExpressions;
using AionHR.Infrastructure.Domain;
using DevExpress.XtraReports.UI;
using AionHR.Model.Employees;
using AionHR.Services.Messaging.Employees;
using AionHR.Services.Messaging.Reports;
using Reports.AttendanceSchedule;
using System.Threading;

namespace AionHR.Web.UI.Forms
{
    public partial class DailySchedule : System.Web.UI.Page
    {
        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IHelpFunctionService _helpFunctionService = ServiceLocator.Current.GetInstance<IHelpFunctionService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();

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


        [DirectMethod]
        public void SetLabels(string labels)
        {
            this.labels.Text = labels;

           

        }

        [DirectMethod]
        public void SetVals(string labels)
        {
            this.vals.Text = labels;
            Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(labels);
            DateTime from = new DateTime();
            DateTime to = new DateTime();
            String datefrom = "", dateTo = "";

            if (parameters.ContainsKey("1"))
            {

                if (DateTime.TryParseExact(parameters["1"], "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.AdjustToUniversal, out from))
                    datefrom = from.ToString();
                else
                    datefrom = null;
            }
            if (parameters.ContainsKey("2"))
            {
                if (DateTime.TryParseExact(parameters["2"], "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.AdjustToUniversal, out to))
                    dateTo = to.ToString();
                else
                    dateTo = null;
            }
            //if (parameters.ContainsKey("3"))
            //{
            //    employeeId.Select(parameters["3"]);
            //    employeeId.SetValue(parameters["3"]);
            //}

            //if (parameters.ContainsKey("4"))
            //{
            //    branchId.Select(parameters["4"]);
            //    branchId.SetValue(parameters["4"]);
            //}

            X.Call("setComboValues", parameters.ContainsKey("3") ? parameters["3"] : null, parameters.ContainsKey("4") ? parameters["4"] : null, datefrom, dateTo);
        }

        [DirectMethod]
        public void SetTexts(string labels)
        {
            this.texts.Text = labels;
        }
        protected void Page_Load(object sender, EventArgs e)
        {




            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                FillBranches();
                FillDepartment();
               
                this.workingHours.Value = string.Empty;
            }
            try
            {
                AccessControlApplier.ApplyAccessControlOnPage(typeof(DailySchedule), null, null, null, null);
            }
            catch (AccessDeniedException exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();
                Viewport1.Hidden = true;





                return;
            }


        }

        private void FillBranches()
        {
            ListRequest request = new ListRequest();
            request.Filter = string.Empty;
            ListResponse<Branch> branches = _branchService.ChildGetAll<Branch>(request);
            if (!branches.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString()+"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+branches.LogId : branches.Summary).Show();
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
            List<EmployeeSnapShot> data = Common.GetEmployeesFiltered(prms.Query);

           //data.ForEach(s => s.fullName = s.name.fullName);
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
              Common.errorMessage(r);
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
                reqFS.BranchId = 0;
                ListResponse<FlatSchedule> response = _helpFunctionService.ChildGetAll<FlatSchedule>(reqFS);
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
            List<EmployeeSnapShot> data = Common.GetEmployeesFiltered(prms.Query);
            string recordId = employeeId.Value.ToString();
            data = data.Where(a => a.recordId != recordId).ToList();
          //  data.ForEach(s => s.fullName = s.name.fullName);
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
            if (dateFrom.SelectedDate < DateTime.Now || dateTo.SelectedDate == DateTime.Now)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ValidFromToDate")).Show();
                return;
            }
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
            fs.dtTo = dateTo.SelectedDate;
            fs.dtFrom = dateFrom.SelectedDate;
            PostRequest<FlatScheduleRange> request = new PostRequest<FlatScheduleRange>();

            request.entity = fs;
            PostResponse<FlatScheduleRange> r = _timeAttendanceService.ChildDelete<FlatScheduleRange>(request);


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
                //Reload Again
                BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
                reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
                reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
                reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
                reqFS.BranchId = 0;
                ListResponse<FlatSchedule> response = _helpFunctionService.ChildGetAll<FlatSchedule>(reqFS);
                if (!response.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                    return;
                }
                BuildSchedule(response.Items);
            
             
                ListResponse<FlatScheduleWorkingHours> workingHoursResponse = _helpFunctionService.ChildGetAll<FlatScheduleWorkingHours>(reqFS);
                if (!workingHoursResponse.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                    return;
                }
                else
                    workingHours.Text = workingHoursResponse.Items[0].workingHours.ToString();
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
            DateTime parsed = DateTime.Now;
            if (DateTime.TryParseExact(dayId.Value.ToString(), "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.AdjustToUniversal, out parsed))
            {
                fs.dtFrom = parsed;
                fs.dtTo= parsed;

            }
            else
                return; 

                fs.shiftId = 0;
            PostRequest<FlatSchedule> request = new PostRequest<FlatSchedule>();

            request.entity = fs;
            PostResponse<FlatSchedule> r = _timeAttendanceService.ChildDelete<FlatSchedule>(request);


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
                BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
                reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
                reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
                reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
                reqFS.BranchId = 0;
                ListResponse<FlatScheduleWorkingHours> workingHoursResponse = _helpFunctionService.ChildGetAll<FlatScheduleWorkingHours>(reqFS);
                if (!workingHoursResponse.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                    return;
                }
                else
                    workingHours.Text = workingHoursResponse.Items[0].workingHours.ToString();
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
            this.workingHours.Value = string.Empty;
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
            if (!string.IsNullOrEmpty(departmentId.SelectedItem.Text))
                reqFS.departmentId = Convert.ToInt32(departmentId.Value.ToString());
            else
                reqFS.departmentId = 0;

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
            X.Call("EnableTools");
            string branchID = string.Empty;
            if (branchId.Value == null || branchId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectBranch")).Show();
                return;
            }
            else
                branchID = branchId.Value.ToString();

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
            reqFS.BranchId = 0;
            ListResponse<FlatSchedule> response = _helpFunctionService.ChildGetAll<FlatSchedule>(reqFS);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                return;
            }
            this.dayId.Value = string.Empty;
            BuildSchedule(response.Items);

            ListResponse<FlatScheduleWorkingHours> workingHoursResponse = _helpFunctionService.ChildGetAll<FlatScheduleWorkingHours>(reqFS);
            if (!workingHoursResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                return;
            }
            else
                workingHours.Text = workingHoursResponse.Items[0].workingHours.ToString(); 

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


            DateTime activeTime = DateTime.ParseExact(dayId.Value.ToString() == string.Empty ? dateFrom.SelectedDate.ToString("yyyyMMdd") : dayId.Value.ToString(), "yyyyMMdd", new CultureInfo("en"));
            DateTime fsfromTime = new DateTime(activeTime.Year, activeTime.Month, activeTime.Day, timeFrom.SelectedTime.Hours, timeFrom.SelectedTime.Minutes, 0);
            DateTime fsToTime = new DateTime(activeTime.Year, activeTime.Month, activeTime.Day, timeTo.SelectedTime.Hours, timeTo.SelectedTime.Minutes, 0);
            if (timeTo.SelectedTime.Hours >= 0 && timeTo.SelectedTime.Hours <= 12 && timeFrom.SelectedTime.Hours >= 12 && timeFrom.SelectedTime.Hours <= 24)
                fsToTime = fsToTime.AddDays(1);

            if (dayId.Value != null && dayId.Value.ToString() != string.Empty)
            {
                SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
                req.Key = "dailySchedule";
                RecordResponse<KeyValuePair<string, string>> SystemDefaultResponse = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
                if (!SystemDefaultResponse.Success)
                {
                    Common.errorMessage(SystemDefaultResponse);
                    return;
                   
                }
                
                FlatSchedule fs = new FlatSchedule();
                fs.dtFrom = fsfromTime;
                fs.dtTo = fsToTime;
                fs.employeeId = Convert.ToInt32(employeeId.Value.ToString());
                //fs.dayId = dayId.Value.ToString();
              
                    PostRequest<FlatSchedule> request = new PostRequest<FlatSchedule>();

                    request.entity = fs;
                    PostResponse<FlatSchedule> r = _timeAttendanceService.ChildAddOrUpdate<FlatSchedule>(request);


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
                        //reloading the day only
                        BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
                        reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
                        reqFS.FromDayId = dayId.Value.ToString();
                        reqFS.ToDayId = dayId.Value.ToString();
                        reqFS.BranchId = 0;
                        ListResponse<FlatSchedule> response = _helpFunctionService.ChildGetAll<FlatSchedule>(reqFS);
                        if (!response.Success)
                        {
                            X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                            return;
                        }

                        //Filling the ids list to reload
                        List<string> listIds = new List<string>();
                        foreach (FlatSchedule fss in response.Items)
                        {

                      
                        //DateTime activeDate = DateTime.ParseExact(fss.dayId, "yyyyMMdd", new CultureInfo("en"));
                        //DateTime fsfromDate=new DateTime();
                        //DateTime fsToDate=new DateTime();
                        //     int i = 0;
                        //if (!Int32.TryParse(fss.from.Split(':')[0], out i))
                        //{
                        //    if (i >= 24)
                        //        fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, i%24, Convert.ToInt32(fss.from.Split(':')[1]), 0);
                        //    else
                        //        fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fss.from.Split(':')[0]), Convert.ToInt32(fss.from.Split(':')[1]), 0);
                        //}
                        //if (!Int32.TryParse(fss.to.Split(':')[0], out i))
                        //{
                        //    if (i >= 24)
                        //        fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, i % 24, Convert.ToInt32(fss.from.Split(':')[1]), 0);
                        //    else
                        //        fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fss.to.Split(':')[0]), Convert.ToInt32(fss.to.Split(':')[1]), 0);

                        //}
                       
                     
                        
                        //    DateTime temp = activeDate;
                        //    if (fsfromDate > fsToDate)
                        //    {
                               
                        //        fsToDate = fsToDate.AddDays(1);
                               
                               
                        //    }
                           
                            while (fss.dtTo >= fss.dtFrom) 
                            {
                              
                                listIds.Add(fss.dtFrom.ToString("yyyyMMdd") + "_" + fss.dtFrom.ToString("HH:mm"));
                            fss.dtFrom = fss.dtFrom.AddMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value));

                            } 
                          
                        }
                        var d = response.Items.GroupBy(x => x.dtFrom);
                        List<string> totaldayId = new List<string>();
                        List<string> totaldaySum = new List<string>();
                        //d.ToList().ForEach(x =>
                        //{
                        //    totaldayId.Add(x.ToList()[0].dayId + "_Total");
                        //    totaldaySum.Add(x.ToList().Sum(y => Convert.ToDouble(y.duration) / 60).ToString());
                        //});

                      

                        X.Call("DeleteDaySchedule", dayId.Value.ToString());
                        X.Call("filldaytotal", totaldayId, totaldaySum);
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
                if (dateFrom.SelectedDate < DateTime.Today || dateTo.SelectedDate <DateTime.Today)
                {
                    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("updateError"));
                    return;
                }
                PostRequest<FlatBulkSchedule> request = new PostRequest<FlatBulkSchedule>();

                request.entity = fs;
                PostResponse<FlatBulkSchedule> r = _helpFunctionService.ChildAddOrUpdate<FlatBulkSchedule>(request);


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
                    //Reload

                    //Reload Again
                    BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
                    reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
                    reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
                    reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
                    reqFS.BranchId = 0;
                    ListResponse<FlatSchedule> response = _helpFunctionService.ChildGetAll<FlatSchedule>(reqFS);
                    if (!response.Success)
                    {
                        X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                        return;
                    }

                    BuildSchedule(response.Items);
                 
                   
                }

            }
            FlatScheduleWorkingHoursRequest reqFS1 = new FlatScheduleWorkingHoursRequest();
            reqFS1.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
            reqFS1.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS1.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
      
            ListResponse<FlatScheduleWorkingHours> workingHoursResponse = _helpFunctionService.ChildGetAll<FlatScheduleWorkingHours>(reqFS1);
            if (!workingHoursResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                return;
            }
            else
                workingHours.Text = workingHoursResponse.Items[0].workingHours.ToString();
        }

        private void BuildSchedule(List<FlatSchedule> items)
        {
            try
            {
                string dailyScheduleVariation;
                SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
                req.Key = "dailySchedule";
                RecordResponse<KeyValuePair<string, string>> SystemDefaultResponse = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
                if (!SystemDefaultResponse.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + SystemDefaultResponse.LogId : SystemDefaultResponse.Summary).Show();
                    return;
                }

                if (string.IsNullOrEmpty(SystemDefaultResponse.result.Value))
                    dailyScheduleVariation = "60";
                else
                    dailyScheduleVariation = SystemDefaultResponse.result.Value;
                string html = @"<div style = 'margin: 0px auto; max-width: 99%;width:auto; height: 98%; overflow:auto;' > 
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0' style='width:auto;' >";

                //CAlling the branch cvailability before proceeding

                string startAt = "00:00";
                string closeAt = "00:00";
                //GetBranchSchedule(out startAt, out closeAt);
                if (string.IsNullOrEmpty(startAt) || string.IsNullOrEmpty(closeAt))
                {
                    html += @"</table></div>";
                    this.pnlSchedule.Html = html;
                    X.Call("DisableTools");
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorBranchWorkingHours")).Show();
                    return;
                }

                TimeSpan tsStart = TimeSpan.Parse(startAt);
                timeFrom.Value = tsStart;
                // timeTo.MinTime = tsStart.Add(TimeSpan.FromMinutes(30));
                TimeSpan tsClose = TimeSpan.Parse(closeAt);
                timeTo.Value = tsClose;


                TimeSpan EmployeeTsStart, EmployeeTsEnd;


                //items.ForEach(x =>
                // {
                //     EmployeeTsStart = TimeSpan.Parse(x.from);
                //     EmployeeTsEnd = TimeSpan.Parse(x.to);
                //     //if (EmployeeTsStart < tsStart || EmployeeTsEnd > tsClose)
                //     //{
                //     //    html += @"</table></div>";
                //     //    this.pnlSchedule.Html = html;
                //     //    X.Call("DisableTools");
                //     //    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", "ErrorEmployeeTimeOutside").ToString() + x.employeeName.reference).Show();
                //     //    throw new Exception();
                //     //}
                // });


                //Filling The Times Slot
                List<TimeSlot> timesList = new List<TimeSlot>();
                DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsStart.Hours, tsStart.Minutes, 0);
                DateTime dtEnd;
                if (tsStart > tsClose)
                {
                    DateTime endDate = DateTime.Now;
                    endDate = endDate.AddDays(1);
                    dtEnd = new DateTime(endDate.Year, endDate.Month, endDate.Day, tsClose.Hours, tsClose.Minutes, 0);
                }
                else
                    dtEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsClose.Hours, tsClose.Minutes, 0);

                if (dtEnd == dtStart)
                {
                    dtEnd = dtEnd.AddDays(1).AddMinutes(-Convert.ToInt32(dailyScheduleVariation));
                }
                do
                {
                    TimeSlot ts = new TimeSlot();
                    ts.ID = dtStart.ToString("HH:mm");
                    ts.Time = dtStart.ToString("HH:mm");
                    timesList.Add(ts);
                    dtStart = dtStart.AddMinutes(Convert.ToInt32(dailyScheduleVariation));
                } while (dtStart <= dtEnd && !string.IsNullOrEmpty(dailyScheduleVariation));

                //filling the Day slots
                int totalDays = Convert.ToInt32(Math.Ceiling((dateTo.SelectedDate - dateFrom.SelectedDate).TotalDays));
                timesList = timesList.Distinct(new TimeSlotComparer()).ToList<TimeSlot>();
                if (_systemService.SessionHelper.getLangauge().ToString() == "de")
                {
                    CultureInfo culture = new CultureInfo("de-DE");
                    Thread.CurrentThread.CurrentCulture = culture;
                }
                html = FillFirstRow(html, timesList);

                html = FillOtherRow(html, timesList, totalDays);

                //Preparing the ids to get colorified
                List<string> listIds = new List<string>();
                foreach (FlatSchedule fs in items)
                {
                    DateTime from = fs.dtFrom;
                    DateTime to = fs.dtTo;

                    while (to >= from)
                    {

                        listIds.Add(from.ToString("yyyyMMdd") + "_" + from.ToString("HH:mm"));
                        from = from.AddMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value));

                    }
                }

                var d = items.GroupBy(x => x.dtFrom);
                List<string> totaldayId = new List<string>();
                List<string> totaldaySum = new List<string>();
                d.ToList().ForEach(x =>
                {
                    double totalduration = x.ToList().Sum(y => Math.Round(Convert.ToDouble(y.duration) / 60, 2));
                    totaldayId.Add(x.ToList()[0].dayId + "_Total");
                    totaldaySum.Add(x.ToList().Sum(y => Math.Round(Convert.ToDouble(y.duration) / 60, 2)).ToString());
                });




                html += @"</table></div>";
                this.pnlSchedule.Html = html;
                X.Call("ColorifySchedule", JSON.JavaScriptSerialize(listIds));
                X.Call("filldaytotal", totaldayId, totaldaySum);
                X.Call("Init");
                X.Call("DisableTools");
                X.Call("FixHeader");

            }catch(Exception exp)
            {

            }
        }


        private void BuildAvailability(List<FlatScheduleBranchAvailability> items)
        {
            string dailyScheduleVariation; 
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "dailySchedule";
            RecordResponse<KeyValuePair<string, string>> SystemDefaultResponse = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!SystemDefaultResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + SystemDefaultResponse.LogId : SystemDefaultResponse.Summary).Show();
                return;
            }
            if (string.IsNullOrEmpty(SystemDefaultResponse.result.Value))
                dailyScheduleVariation = "60";
            else
                dailyScheduleVariation = SystemDefaultResponse.result.Value;
            string html = @"<div style = 'margin: 5px auto; width: 99%; height: 98%; overflow:auto;' > 
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0' >";

            //CAlling the branch cvailability before proceeding

            string startAt = "00:00";
               string closeAt = "00:00";
            //GetBranchSchedule(out startAt, out closeAt);

            if (string.IsNullOrEmpty(startAt) || string.IsNullOrEmpty(closeAt))
            {




                html += @"</table></div>";
                this.pnlSchedule.Html = html;

                X.Call("DisableTools");
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorBranchWorkingHours")).Show();

                return;
            }

            TimeSpan tsStart = TimeSpan.Parse(startAt);
            timeFrom.Value = tsStart;
            // timeTo.MinTime = tsStart.Add(TimeSpan.FromMinutes(30));
            TimeSpan tsClose = TimeSpan.Parse(closeAt);
            timeTo.Value = tsClose;



            //Filling The Times Slot
            List<TimeSlot> timesList = new List<TimeSlot>();
            DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsStart.Hours, tsStart.Minutes, 0);
            DateTime dtEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsClose.Hours, tsClose.Minutes, 0);
            if (dtEnd == dtStart)
            {
                dtEnd = dtEnd.AddDays(1).AddMinutes(-Convert.ToInt32(dailyScheduleVariation));
            }
            do
            {
                TimeSlot ts = new TimeSlot();
                ts.ID = dtStart.ToString("HH:mm");
                ts.Time = dtStart.ToString("HH:mm");
                timesList.Add(ts);
                dtStart = dtStart.AddMinutes(Convert.ToInt32(dailyScheduleVariation));
            } while (dtStart <= dtEnd);

            //filling the Day slots
            int totalDays = Convert.ToInt32(Math.Ceiling((dateTo.SelectedDate - dateFrom.SelectedDate).TotalDays));
            timesList = timesList.Distinct(new TimeSlotComparer()).ToList<TimeSlot>();

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
            X.Call("FixHeader");


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
                    string day = firstDate.ToString("ddd");
                    string dayNumber = firstDate.ToString("MMM d");
                    html += "<td id=" + firstDate.ToString("yyyyMMdd") + " class='day'>" + string.Format("<div style='width:30px;display:inline-block'>{0},</div> {1}", day, dayNumber) + "</td><td id=" + firstDate.ToString("yyyyMMdd") +"_Total></td>";
                    // html += "<td id=" + firstDate.ToString("yyyyMMdd") + " class='day'>" + firstDate.ToString("ddd, MMM d") + "</td>";
                }
                else
                {
                    string day = firstDate.ToString("ddd");
                    string dayNumber = firstDate.ToString("dd");
                    string month = firstDate.ToString("MM");
                    html += "<td id=" + firstDate.ToString("yyyyMMdd") + " class='day'>" + string.Format("<div style='width:43px;display:inline-block'>{0}</div> {1} - {2}", (string)GetLocalResourceObject(day), dayNumber, month) + "</td><td id=" + firstDate.ToString("yyyyMMdd") + "_Total></td>";
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
          

            html += "<thead><tr ><th style='width:120px;'></th><th>"+GetLocalResourceObject("total")+"</th>";
            for (int index = 0; index < timesList.Count; index++)
            {
                html += "<th>" + timesList[index].Time + "</th>";
            }
            html += "</tr></thead>";
            return html;
        }

        //private List<Employee> GetEmployeesFiltered(string query)
        //{

        //    EmployeeListRequest req = new EmployeeListRequest();
        //    req.DepartmentId = "0";
        //    req.BranchId = branchId.Value.ToString();
        //    req.IncludeIsInactive = 0;
        //    req.SortBy = "firstName";

        //    req.StartAt = "0";
        //    req.Size = "20";
        //    req.Filter = query;
        //    ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
        //    if (!response.Success)
        //       Common.errorMessage(response);
        //    return response.Items;
        //}

        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;
                isRTL.Text = "1";

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
        private void FillDepartment()
        {
            DepartmentListRequest departmentsRequest = new DepartmentListRequest();
            departmentsRequest.type = 0;
            ListResponse<Department> resp = _branchService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
               Common.errorMessage(resp);
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
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


        [DirectMethod(ShowMask = true, CustomTarget = "employeeScheduleWindow")]
        public void OpenCell(string day)
        {
          //  this.storeEmployee = null;
            this.employeeScheduleWindow.Show();
            EmployeeCellScheduleRequest reqFS = new EmployeeCellScheduleRequest();
            reqFS.BranchId = Convert.ToInt32(branchId.Value.ToString());
            if (!string.IsNullOrEmpty(departmentId.SelectedItem.Text))
                reqFS.departmentId = Convert.ToInt32(departmentId.Value.ToString());
            else
                reqFS.departmentId = 0;

            reqFS.DayId = day.Split('_')[0];
            reqFS.Time = day.Split('_')[1];


            ListResponse<FlatScheduleEmployeeCell> response = _helpFunctionService.ChildGetAll<FlatScheduleEmployeeCell>(reqFS);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingEmployees")).Show();
                return;
            }

            ////Just For Testing 

            //List<Employee> data = GetEmployeesFiltered("");
            //data.ForEach(s => s.fullName = s.name.fullName);

            ////Just for testing 


            this.storeEmployee.DataSource = response.Items;
            this.storeEmployee.DataBind();




        }

        protected void LoadEmployeeSchedule(object sender, DirectEventArgs e)
        {
           string employeeID = e.ExtraParams["employeeID"];
            string Name = e.ExtraParams["employeeName"];
            //getting the employee to push it to the Combo

            Employee emp = new Employee();
            emp.recordId = employeeID;
            emp.fullName = Name;

          

            BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
            reqFS.EmployeeId = Convert.ToInt32(employeeID);
            reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
            reqFS.BranchId = 0;
            ListResponse<FlatSchedule> response = _helpFunctionService.ChildGetAll<FlatSchedule>(reqFS);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                
                return;
            }
            this.dayId.Value = string.Empty;
            BuildSchedule(response.Items);




          
            this.EmployeeStore.Add(new { recordId = emp.recordId, fullName = emp.fullName });

            this.employeeId.SetValue(emp.recordId);//.SetValueAndFireSelect(emp.recordId);
           // this.employeeId.SelectedItem.Value = emp.recordId;
         //   this.employeeId.Update();
            this.employeeScheduleWindow.Hide();
        }
        protected void userSelectorStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            EmployeeSnapshotListRequest req = new EmployeeSnapshotListRequest();
            if (branchId.Value == null || branchId.Value.ToString() == string.Empty)
            {
                req.BranchId = "0";
            }
            else
                req.BranchId = branchId.Value.ToString();
            //if (departmentId.Value == null || departmentId.Value.ToString() == string.Empty)
            //{
            //    req.DepartmentId = "0";
            //}
            //else
            //    req.DepartmentId = departmentId.Value.ToString();
           
          
           // req.IncludeIsInactive = 0;
           // req.SortBy = "firstName";

            req.StartAt = "0";
            req.Size = "1000";
            req.Filter = "";
            ListResponse<EmployeeSnapShot> response = _employeeService.GetAll<EmployeeSnapShot>(req);
            if (!response.Success)
               Common.errorMessage(response);

            else
                //UsersListRequest req = new UsersListRequest();
                //req.Size = "100";
                //req.StartAt = "0";
                //req.Filter = "";

                //var s = jobInfo1.GetJobInfo();
                //req.DepartmentId = s.DepartmentId.HasValue ? s.DepartmentId.ToString() : "0";
                //req.PositionId = s.PositionId.HasValue ? s.PositionId.ToString() : "0";
                //ListResponse<UserInfo> groups = _systemService.ChildGetAll<UserInfo>(req);
                //if (!groups.Success)
                //{
                //    X.Msg.Alert(Resources.Common.Error, groups.Summary).Show();
                //    return;
                //}
                //List<SecurityGroupUser> all = new List<SecurityGroupUser>();
                //groups.Items.ForEach(x => all.Add(new SecurityGroupUser() { fullName = x.fullName, userId = x.recordId }));
                response.Items.ForEach(x => x.fullName = x.name.fullName);
            if(response.Items.Where(x => x.recordId == employeeId.SelectedItem.Value.ToString()).Count()!=0)
            response.Items.Remove(response.Items.Where(x => x.recordId == employeeId.SelectedItem.Value.ToString()).ToArray()[0]);
            X.Call("AddSource", response.Items);

        }

        //[DirectMethod]
        //public void GetFilteredUsers()
        //{
        //    EmployeeListRequest req = new EmployeeListRequest();
        //    if (branchId.Value == null || branchId.Value.ToString() == string.Empty)
        //    {
        //        req.BranchId = "0";
        //    }
        //    else
        //        req.BranchId = branchId.Value.ToString();
        //    if (departmentId.Value == null || departmentId.Value.ToString() == string.Empty)
        //    {
        //        req.DepartmentId = "0";
        //    }
        //    else
        //        req.DepartmentId = departmentId.Value.ToString();


        //    req.IncludeIsInactive = 2;
        //    req.SortBy = "firstName";

        //    req.StartAt = "0";
        //    req.Size = "20";
        //    req.Filter = "";
        //    ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
        //    if (!response.Success)
        //       Common.errorMessage(response);
            
        //    else
        //        response.Items.ForEach(x => x.fullName = x.name.fullName);
        //    X.Call("AddSource", response.Items);

        //}
        public void Export_Click(object sender, DirectEventArgs e)
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

            userSelectorStore.Reload();
            this.groupUsersWindow.Show();


        }
        [DirectMethod]
        public void sendAttachment()
        {
            try {
                XtraReport report = SetAttendanceScheduleReport();

                if (string.IsNullOrEmpty(device.Value.ToString()))
                    return;

                ShareAttachment sh = new ShareAttachment();
                sh.employeeId = string.IsNullOrEmpty(employeeId.Value.ToString()) ? "0" : employeeId.Value.ToString();
                sh.branchId = string.IsNullOrEmpty(branchId.Value.ToString()) ? "0" : branchId.Value.ToString();
                sh.device = device.Value.ToString();
                byte[] fileData = GetReportAsBuffer(report);
                ShareAttachmentPostRequest req = new ShareAttachmentPostRequest();
                req.entity = sh;
                req.FilesData.Add(fileData);
                req.FileNames.Add("Attachment.Jpeg");
                PostResponse<ShareAttachment> resp = _employeeService.ShareEmployeeAttachments(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }




                X.Msg.Alert("", (string)GetGlobalResourceObject("Common", "operationCompleted")).Show();
                device.Select("");
                //if (fileData != null)
                //{

                //    if (!resp.Success)//it maybe be another condition
                //    {
                //        //Show an error saving...
                //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //        Common.errorMessage(resp);
                //        return;
                //    }
                //}

            }
            catch(Exception exp)
            {
                X.MessageBox.Alert(Resources.Common.Error, exp.Message);
                device.Select("");
            }


        }
        public void Share_Click(object sender, DirectEventArgs e)
        {
            //if (employeeId.Value == null || employeeId.Value.ToString() == string.Empty)
            //{
            //    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectEmployee")).Show();
            //    return;
            //}
            if (dateFrom.SelectedDate.Year <2015 || dateTo.SelectedDate.Year < 2015)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ValidFromToDate")).Show();
                return;
            }
            if (dateFrom.SelectedDate > dateTo.SelectedDate)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ToDateHigherFromDate")).Show();
                return;
            }
            X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.sendDailySchedule, new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    //We are call a direct request metho for deleting a record
                    Handler = String.Format("App.direct.sendAttachment()"),
                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Text = Resources.Common.No
                }

            }).Show();
         






        }
        protected void ExportEmployees(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];
            var select = e.ExtraParams["userSelector"];

            List<Employee> selectedUsers = new List<Employee>();
            foreach (var item in userSelector.SelectedItems)
            {
                
                selectedUsers.Add(new Employee() { recordId = item.Value, fullName = item.Text,  });
            }
            string branchID = string.Empty;
          
            if (selectedUsers.Count==0)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectExport")).Show();
                return;
            }
            else
                branchID = branchId.Value.ToString();

          


            FlatScheduleImport fs = new FlatScheduleImport();

            fs.fromEmployeeId = Convert.ToInt32(employeeId.Value.ToString());
           
            fs.fromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            fs.toDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
            foreach (Employee E in selectedUsers)
            {
                fs.toEmployeeId = Convert.ToInt32(E.recordId);
                PostRequest<FlatScheduleImport> request = new PostRequest<FlatScheduleImport>();

                request.entity = fs;

                PostResponse<FlatScheduleImport> r = _helpFunctionService.ChildAddOrUpdate<FlatScheduleImport>(request);
               if (!r.Success)
                {
                    Common.errorMessage(r);
                }
            }

            //check if the insert failed
            //if (!r.Success)//it maybe be another condition
            //{
            //    //Show an error saving...
            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //  Common.errorMessage(r);
            //    return;
            //}
            //else
            //{

                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = (string)GetLocalResourceObject("ExportSuccess")
                });


                //Reload Again
                BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
                reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
                reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
                reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
                reqFS.BranchId = 0;
                ListResponse<FlatSchedule> response = _helpFunctionService.ChildGetAll<FlatSchedule>(reqFS);
                if (!response.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                    return;
                }
                this.dayId.Value = string.Empty;
                BuildSchedule(response.Items);
                this.cmbEmployeeImport.Value = string.Empty;
                groupUsersWindow.Close();
        }
        private byte[] GetReportAsBuffer(XtraReport report)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                report.ExportToImage(stream, new DevExpress.XtraPrinting.ImageExportOptions(System.Drawing.Imaging.ImageFormat.Jpeg));
                return stream.ToArray();
            }
        }

        private XtraReport SetAttendanceScheduleReport()
        {
            ReportCompositeRequest req = GetRequest();




            ListResponse<AionHR.Model.Reports.RT310> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT310>(req);
            if (!resp.Success)
            {
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
            }
            for (int i = resp.Items.Count - 1; i >= 0; i--)
            {
                DateTime parsed = DateTime.Now;
                if (DateTime.TryParseExact(resp.Items[i].dayId, "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.AdjustToUniversal, out parsed))
                {
                    resp.Items[i].dayIdDateTime = parsed;
                    //x.dayIdString = parsed.ToString(_systemService.SessionHelper.GetDateformat());
                    // Use reformatted
                }
                else

                    resp.Items.RemoveAt(i);
            }
            Dictionary<string, string> parameters = AionHR.Web.UI.Forms.Common.FetchReportParameters(texts.Text);

            AttendanceScheduleReport h = new AttendanceScheduleReport(resp.Items, _systemService.SessionHelper.CheckIfArabicSession(), _systemService.SessionHelper.GetDateformat(), parameters);
            //h.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;


            string from = DateTime.ParseExact(req.Parameters["_fromDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            string to = DateTime.ParseExact(req.Parameters["_toDayId"], "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            h.Parameters["User"].Value = string.IsNullOrEmpty(_systemService.SessionHelper.GetCurrentUser()) ? " " : _systemService.SessionHelper.GetCurrentUser();

            h.Parameters["From"].Value = from;
            h.Parameters["To"].Value = to;

            if (req.Parameters["_branchId"] != "0")
                h.Parameters["Branch"].Value = branchId.SelectedItem != null ? branchId.SelectedItem.Text : "";


            else
                h.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");







            h.CreateDocument();
            return h;
        }
        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest req = new ReportCompositeRequest();
            DateRangeParameterSet DateSet = new DateRangeParameterSet();
            JobInfoParameterSet jobInfoSet = new JobInfoParameterSet();
            EmployeeParameterSet EmployeeSet = new EmployeeParameterSet();
            DateSet.IsDayId = true;
            DateSet.DateFrom = dateFrom.SelectedDate;
            DateSet.DateTo = dateTo.SelectedDate;
            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
            {
                jobInfoSet.BranchId = Convert.ToInt32(branchId.Value);



            }
            else
            {
                jobInfoSet.BranchId = 0;

            }
            int bulk;
            if (employeeId.Value == null || !int.TryParse(employeeId.Value.ToString(), out bulk))

                EmployeeSet.employeeId = 0;
            else
                EmployeeSet.employeeId = bulk;

            req.Size = "1000";
            req.StartAt = "0";

            req.Add(DateSet);
            req.Add(jobInfoSet);
            req.Add(EmployeeSet);


            return req;
        }
    }

    internal class Availability
    {
        public string Count { get; set; }
        public string Id { get; set; }
    }
   


}