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
using AionHR.Model.SelfService;
using AionHR.Services.Messaging.Reports;

namespace AionHR.Web.UI.Forms
{
    public partial class DailyScheduleSelfServices : System.Web.UI.Page
    {
        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IHelpFunctionService _helpFunctionService = ServiceLocator.Current.GetInstance<IHelpFunctionService>();
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
               
                this.workingHours.Value = string.Empty;
                dateFrom.SelectedDate = DateTime.Now;
                dateTo.SelectedDate = DateTime.Now;
                Load_Click(sender, new DirectEventArgs(new Ext.Net.ParameterCollection()));
            }


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

          //  data.ForEach(s => s.fullName = s.name.fullName);
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
                ListResponse<FlatScheduleSelfService> response = _selfServiceService.ChildGetAll<FlatScheduleSelfService>(reqFS);
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
                ListResponse<FlatScheduleSelfService> response = _selfServiceService.ChildGetAll<FlatScheduleSelfService>(reqFS);
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
            //if (employeeId.Value == null || employeeId.Value.ToString() == string.Empty)
            //{
            //    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectEmployee")).Show();
            //    return;
            //}

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
            reqFS.EmployeeId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());
            reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.ToDayId = dateTo.SelectedDate.ToString("yyyyMMdd");
            reqFS.BranchId = 0;
            ListResponse<FlatScheduleSelfService> response =_selfServiceService.ChildGetAll<FlatScheduleSelfService>(reqFS);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return;
            }
            this.dayId.Value = string.Empty;
            BuildSchedule(response.Items);

            //ListResponse<FlatScheduleWorkingHours> workingHoursResponse = _helpFunctionService.ChildGetAll<FlatScheduleWorkingHours>(reqFS);
            //if (!workingHoursResponse.Success)
            //{
            //    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
            //    return;
            //}
            //else
            //    workingHours.Text = workingHoursResponse.Items[0].workingHours.ToString(); 

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
                SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
                req.Key = "dailySchedule";
                RecordResponse<KeyValuePair<string, string>> SystemDefaultResponse = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
                if (!SystemDefaultResponse.Success)
                {
                    Common.errorMessage(SystemDefaultResponse);
                    return;
                }
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
                  Common.errorMessage(r);
                    return;
                }
                else
                {
                    //reloading the day only
                    string rep_params="";
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
               



                    if(!string.IsNullOrEmpty(dayId.Value.ToString()))
                    parameters.Add("1", dayId.Value.ToString());
                    if (!string.IsNullOrEmpty(dayId.Value.ToString()))
                        parameters.Add("2", dayId.Value.ToString());
                    if (!string.IsNullOrEmpty(employeeId.Value.ToString()))
                        parameters.Add("3", employeeId.Value.ToString());
                    parameters.Add("4", "0");

                    foreach (KeyValuePair<string, string> entry in parameters)
                    {
                        rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                    }
                    if (rep_params.Length > 0)
                    {
                        if (rep_params[rep_params.Length - 1] == '^')
                            rep_params = rep_params.Remove(rep_params.Length - 1);
                    }

                                      

                    //BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
                    //reqFS.EmployeeId = Convert.ToInt32(employeeId.Value.ToString());
                    //reqFS.FromDayId = dayId.Value.ToString();
                    //reqFS.ToDayId = dayId.Value.ToString();
                    //reqFS.BranchId = 0;
                    ReportGenericRequest reqFS = new ReportGenericRequest();
                    reqFS.paramString = rep_params;

                    ListResponse<FlatSchedule> response = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqFS);
                    if (!response.Success)
                    {
                        X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                        return;
                    }

                    //Filling the ids list to reload
                    List<string> listIds = new List<string>();
                    foreach (FlatSchedule fss in response.Items)
                    {
                        DateTime activeDate = DateTime.ParseExact(fss.dayId, "yyyyMMdd", new CultureInfo("en"));
                        DateTime fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fss.from.Split(':')[0]), Convert.ToInt32(fss.from.Split(':')[1]), 0);
                        DateTime fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fss.to.Split(':')[0]), Convert.ToInt32(fss.to.Split(':')[1]), 0);

                        do
                        {
                            listIds.Add(fsfromDate.ToString("yyyyMMdd") + "_" + fsfromDate.ToString("HH:mm"));
                            fsfromDate = fsfromDate.AddMinutes(Convert.ToInt32( SystemDefaultResponse.result.Value));
                        } while (fsToDate >= fsfromDate);

                    }
                    var d = response.Items.GroupBy(x => x.dayId);
                    List<string> totaldayId = new List<string>();
                    List<string> totaldaySum = new List<string>();
                    d.ToList().ForEach(x =>
                    {
                        totaldayId.Add(x.ToList()[0].dayId + "_Total");
                        totaldaySum.Add(x.ToList().Sum(y => Convert.ToDouble(y.duration) / 60).ToString());
                    });

                    //eliasList<string> listIds = new List<string>();
                    //DateTime effectiveDate = fsfromTime;
                    //do
                    //{
                    //    listIds.Add(effectiveDate.ToString("yyyyMMdd") + "_" + fsfromTime.ToString("HH:mm"));
                    //    fsfromTime = fsfromTime.AddMinutes(30);
                    //} while (fsToTime >= fsfromTime);
                 
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
                    ListResponse<FlatScheduleSelfService> response = _timeAttendanceService.ChildGetAll<FlatScheduleSelfService>(reqFS);
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

        private void BuildSchedule(List<FlatScheduleSelfService> items)
        {
            try
            {
                SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
                req.Key = "dailySchedule";
                RecordResponse<KeyValuePair<string, string>> SystemDefaultResponse = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
                if (!SystemDefaultResponse.Success)
                {
                    Common.errorMessage(SystemDefaultResponse);
                    return; 
                }
               
                   
                string html = @"<div style = 'margin: 0px auto; max-width: 99%;width:auto; height: 98%; overflow:auto;' > 
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0' style='width:auto;' >";

                //CAlling the branch cvailability before proceeding

                string startAt, closeAt = string.Empty;

                if (items.Count==0)
                {
                
                    
                        html += @"</table></div>";
                        this.pnlSchedule.Html = html;
                        X.Call("DisableTools");
                        X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorBranchWorkingHours")).Show();
                        return;
                    

                }
                //   GetBranchSchedule(out startAt, out closeAt);
                //TimeSpan tsStart = new TimeSpan(); ;
                //TimeSpan tsClose = new TimeSpan();

                //for (int i =0; i<items.Count; i++)
                //{
                //    if (i==0)
                //    {
                //        tsStart= TimeSpan.Parse(items[i].from);
                //        tsClose= TimeSpan.Parse(items[i].to);
                //        continue; 
                //    }

                //    if (TimeSpan.Parse(items[i].from) < tsStart)
                //        tsStart = TimeSpan.Parse(items[i].from);
                //    if (TimeSpan.Parse(items[i].to) > tsClose)
                //        tsClose = TimeSpan.Parse(items[i].to);

                //}
              
                //timeFrom.Value = tsStart;
                // timeTo.MinTime = tsStart.Add(TimeSpan.FromMinutes(30));
              
                //timeTo.Value = tsClose;

               
                //TimeSpan EmployeeTsStart, EmployeeTsEnd;


                //items.ForEach(x =>
                // {
                //     EmployeeTsStart = TimeSpan.Parse(x.from);
                //     EmployeeTsEnd = TimeSpan.Parse(x.to);
                //     if (EmployeeTsStart < tsStart || EmployeeTsEnd > tsClose)
                //     {
                //         html += @"</table></div>";
                //         this.pnlSchedule.Html = html;
                //         X.Call("DisableTools");
                //         X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", "ErrorEmployeeTimeOutside").ToString() + x.employeeName.reference).Show();
                //         return; 
                //     }
                // });


                //Filling The Times Slot
                List<TimeSlot> timesList = new List<TimeSlot>();
                DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime dtEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                if (dtEnd == dtStart)
                {
                    dtEnd = dtEnd.AddDays(1).AddMinutes(-Convert.ToInt32(SystemDefaultResponse.result.Value));
                }
                do
                {
                    TimeSlot ts = new TimeSlot();
                    ts.ID = dtStart.ToString("HH:mm");
                    ts.Time = dtStart.ToString("HH:mm");
                    timesList.Add(ts);
                    dtStart = dtStart.AddMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value));
                } while (dtStart <= dtEnd);

                //filling the Day slots
                int totalDays = Convert.ToInt32(Math.Ceiling((dateTo.SelectedDate - dateFrom.SelectedDate).TotalDays));

                html = FillFirstRow(html, timesList);

                html = FillOtherRow(html, timesList, totalDays);

                //Preparing the ids to get colorified
                List<string> listIds = new List<string>();
                foreach (FlatScheduleSelfService fs in items)
                {
                    DateTime activeDate = DateTime.ParseExact(fs.dayId, "yyyyMMdd", new CultureInfo("en"));
                    DateTime fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.from.Split(':')[0]), Convert.ToInt32(fs.from.Split(':')[1]), 0);
                    DateTime fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.to.Split(':')[0]), Convert.ToInt32(fs.to.Split(':')[1]), 0);
                    if (fsToDate.Minute == 0 && fsToDate.Hour == 00)
                    {
                        fsToDate = fsToDate.AddDays(1);
                        do
                        {


                            fsToDate = fsToDate.AddMinutes(-Convert.ToInt32(SystemDefaultResponse.result.Value));
                            listIds.Add(fsToDate.ToString("yyyyMMdd") + "_" + fsToDate.ToString("HH:mm"));
                        } while (fsToDate != fsfromDate);
                    }
                    else
                    {
                        do
                        {

                            listIds.Add(fsfromDate.ToString("yyyyMMdd") + "_" + fsfromDate.ToString("HH:mm"));
                            fsfromDate = fsfromDate.AddMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value));
                        } while (fsToDate >= fsfromDate);
                    }

                }

                var d = items.GroupBy(x => x.dayId);
                List<string> totaldayId = new List<string>();
                List<string> totaldaySum = new List<string>();
                d.ToList().ForEach(x =>
                {
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
            }catch
            { }
        }


        private void BuildAvailability(List<FlatScheduleBranchAvailability> items)
        {
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "dailySchedule";
            RecordResponse<KeyValuePair<string, string>> SystemDefaultResponse = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!SystemDefaultResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + SystemDefaultResponse.LogId : SystemDefaultResponse.Summary).Show();
                return;
            }
            string html = @"<div style = 'margin: 5px auto; width: 99%; height: 98%; overflow:auto;' > 
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0' >";

            //CAlling the branch cvailability before proceeding

            string startAt = "00:00";
               string closeAt = "00:00";
         //   GetBranchSchedule(out startAt, out closeAt);
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
            if (dtStart >= dtEnd)
            {
                dtEnd = dtEnd.AddDays(1);
            }
            do
            {
                TimeSlot ts = new TimeSlot();
                ts.ID = dtStart.ToString("HH:mm");
                ts.Time = dtStart.ToString("HH:mm");
                timesList.Add(ts);
                dtStart = dtStart.AddMinutes(Convert.ToInt32( SystemDefaultResponse.result.Value));
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
        //    req.IncludeIsInactive = 2;
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
            ListResponse<FlatScheduleSelfService> response = _timeAttendanceService.ChildGetAll<FlatScheduleSelfService>(reqFS);
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
           
          
            //req.IncludeIsInactive = 0;
            //req.SortBy = "firstName";

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
                ListResponse<FlatScheduleSelfService> response = _timeAttendanceService.ChildGetAll<FlatScheduleSelfService>(reqFS);
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

     
        

    }

   


}