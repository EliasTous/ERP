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
using AionHR.Infrastructure.Session;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Model.TimeAttendance;
using AionHR.Model.HelpFunction;
using AionHR.Services.Messaging.TimeAttendance;
using AionHR.Services.Messaging.Reports;

namespace AionHR.Web.UI.Forms
{
    public partial class TimeAttendanceViewSelfServices : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
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
                HideShowButtons();
                HideShowColumns();
                
                format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
               startDayId.SelectedDate = DateTime.Today;
                endDayId.SelectedDate = DateTime.Today;
                if (!string.IsNullOrEmpty(Request.QueryString["_fromDayId"])&&!string.IsNullOrEmpty(Request.QueryString["_toDayId"]))
                {
                    startDayId.SelectedDate = DateTime.ParseExact(Server.UrlDecode(Request.QueryString["_fromDayId"]).Trim('\''), "yyyyMMdd", new CultureInfo("en") ) ;
                    endDayId.SelectedDate =DateTime.ParseExact(Server.UrlDecode(Request.QueryString["_toDayId"]).Trim('\''), "yyyyMMdd", new CultureInfo("en"));

                }
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(AttendanceDay), null, GridPanel1, null, null);
                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(AttendanceShift), EditShiftForm, attendanceShiftGrid, btnAdd, SaveButton);
                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}

                
            }

          
        }



        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }


        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string dayId = e.ExtraParams["dayId"];
            int employeeId = Convert.ToInt32(e.ExtraParams["employeeId"]);
            CurrentDay.Text = dayId.ToString();
            CurrentEmployee.Text = employeeId.ToString();
            CurrentCA.Text = e.ExtraParams["ca"];
            CurrentSC.Text = e.ExtraParams["sc"];
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    AttendanceShiftListRequest request = new AttendanceShiftListRequest();
                    request.EmployeeId = employeeId;
                    request.DayId = dayId ;
                    ListResponse<AttendanceShift> shifts = _timeAttendanceService.ChildGetAll<AttendanceShift>(request);
                    if(shifts.Success)
                    {
                        attendanceShiftStore.DataSource = shifts.Items;
                        attendanceShiftStore.DataBind();
                        AttendanceShiftWindow.Show();
                    }

                    break;
                case "LinkRender":
                    FillTimeApproval(dayId, employeeId);
                    TimeApprovalWindow.Show(); 

                    break;

                default:
                    break;
            }


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

        protected void PoPuPShift(object sender, DirectEventArgs e)
        {


         
            string type = e.ExtraParams["type"];
            string cIn = e.ExtraParams["checkedIn"];
            string cOut = e.ExtraParams["checkedOut"];
            string day = CurrentDay.Text;
            string emp = CurrentEmployee.Text;
            string id = e.ExtraParams["shiftId"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    recordId.Text = id;
                    shiftDayId.Text = day;
                    shiftEmpId.Text = emp;
                    checkIn.Text = cIn;
                    checkOut.Text = cOut;
                    ca.Text = CurrentCA.Text;
                    sc.Text = CurrentSC.Text;
                    EditShiftWindow.Show();


                    break;
                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteShift({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
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
        public void DeleteShift(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                AttendanceShift s = new AttendanceShift();
                s.recordId = index;
                s.dayId = CurrentDay.Text;
                s.employeeId = CurrentEmployee.Text;
                s.checkIn = "00:00";
                s.checkOut = "00:00";
                PostRequest<AttendanceShift> req = new PostRequest<AttendanceShift>();
                req.entity = s;
                PostResponse<AttendanceShift> resp = _timeAttendanceService.ChildDelete<AttendanceShift>(req);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(resp);
                    return;
                }
                SynchronizeAttendanceDay GD = new SynchronizeAttendanceDay();
                PostRequest<SynchronizeAttendanceDay> request = new PostRequest<SynchronizeAttendanceDay>();
                GD.employeeId =Convert.ToInt32( CurrentEmployee.Text);
                GD.fromDayId = CurrentDay.Text;
                GD.toDayId = CurrentDay.Text;
                request.entity = GD;


                PostResponse<SynchronizeAttendanceDay> resp1 = _helpFunctionService.ChildAddOrUpdate<SynchronizeAttendanceDay>(request);
                //Step 2 :  remove the object from the store
             
                if (!resp1.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp1.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp1.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") +resp1.LogId : resp1.Summary).Show();
                    return;
                }
                attendanceShiftStore.Remove(index);
                //Step 3 : Showing a notification for the user 
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordDeletedSucc
                });

                Store1.Reload();
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


            //RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
            //if (sm.SelectedRows.Count() <= 0)
            //    return;
            X.Msg.Confirm(Resources.Common.Confirmation, GetLocalResourceObject("DeleteBranchAttendances").ToString(), new MessageBoxButtonsConfig
            {
                //Calling DoYes the direct method for removing selecte record
                Yes = new MessageBoxButtonConfig
                {
                    Handler = "App.direct.deleteBranchAttendances()",
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
        ///    [DirectMethod(ShowMask = true)]
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
        /// </summary>
        [DirectMethod(ShowMask = true)]
        public void deleteBranchAttendances()
        {
            BranchAttendance BA = new BranchAttendance();
            var d = jobInfo1.GetJobInfo();
            if (d.BranchId.HasValue)
            {
                BA.branchId =Convert.ToInt32( d.BranchId.Value.ToString());
              


            }
            else
            {
                BA.branchId = 0;
              
            }
            

            if (startDayId.SelectedDate != DateTime.MinValue)

            {
                BA.fromDayId= startDayId.SelectedDate.ToString("yyyyMMdd");


            }
            else
            {
                BA.fromDayId = "";
            }
            if (endDayId.SelectedDate != DateTime.MinValue)

            {
                BA.toDayId = endDayId.SelectedDate.ToString("yyyyMMdd");


            }
            else
            {
                BA.toDayId = "";
            }
           
            try
            {

              
                PostRequest<BranchAttendance> request = new PostRequest<BranchAttendance>();
               
                request.entity = BA;


                PostResponse<BranchAttendance> r = _helpFunctionService.ChildAddOrUpdate<BranchAttendance>(request);
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
                    //Showing successful notification
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.ManyRecordDeletedSucc
                    });
                }

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

        private AttendnanceDayListRequest GetAttendanceDayRequest()
        {
            AttendnanceDayListRequest req = new AttendnanceDayListRequest();

          
            
                req.BranchId = "0";
             
           
           
                req.DivisionId = "0";
          
                req.DepartmentId = "0";
               
            
            if (startDayId.SelectedDate != DateTime.MinValue)

            {
                req.StartDayId = startDayId.SelectedDate.ToString("yyyyMMdd");


            }
            else
            {
                req.StartDayId = "";
            }
            if (endDayId.SelectedDate != DateTime.MinValue)

            {
                req.EndDayId = endDayId.SelectedDate.ToString("yyyyMMdd");


            }
            else
            {
                req.EndDayId = "";
            }

            req.EmployeeId = _systemService.SessionHelper.GetEmployeeId(); 
           

            req.Month = "0";
            req.Year = "0";
            req.Size = "30";
            req.StartAt = "0";
            req.Filter = "";
            req.SortBy = "dayId";
          
         //  req.apStatus = 0;
         

            return req;
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                //GEtting the filter from the page

                //AttendnanceDayListRequest req = GetAttendanceDayRequest();
                string rep_params = "";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                if (startDayId.SelectedDate != DateTime.MinValue)

                {
                    parameters.Add("2", startDayId.SelectedDate.ToString("yyyyMMdd")); ;


                }
               
                if (endDayId.SelectedDate != DateTime.MinValue)

                {
                    parameters.Add("3", endDayId.SelectedDate.ToString("yyyyMMdd"));


                }

                parameters.Add("1",_systemService.SessionHelper.GetEmployeeId());
             
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params = rep_params.Remove(rep_params.Length - 1);
                }



                TimeAttendanceViewListRequest req = new TimeAttendanceViewListRequest();


                req.paramString = rep_params;


                req.Size = "30";
                req.sortBy = "recordId";


             
                req.StartAt = e.Start.ToString();
                ListResponse<AttendanceDay> daysResponse = _selfServiceService.ChildGetAll<AttendanceDay>(req);
                if (!daysResponse.Success)
                {
                   Common.errorMessage( daysResponse);
                    return;
                }
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();

                daysResponse.Items.ForEach(x =>

                            {
                                x.netOLString = time(x.netOL, true);
                                if (rtl)
                                    x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("ar-AE"));
                                else
                                    x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("en-US"));
                                switch (x.apStatus)
                                {
                                    case 1:
                                        x.apStatusString = pendingHF.Text;
                                        break;
                                    case 2:
                                        x.apStatusString = approvedHF.Text;
                                        break;

                                }
                            });




                int total = daysResponse.Items.Sum(x => x.netOL);
                string totalWorked, totalBreaks;
                int hoursWorked = 0, minsWorked = 0, hoursBreak, minsBrea = 0;
                daysResponse.Items.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.workingTime))
                    {
                        hoursWorked += Convert.ToInt32(x.workingTime.Substring(0, 2));
                        minsWorked += Convert.ToInt32(x.workingTime.Substring(3, 2));
                    }
                    if (!string.IsNullOrEmpty(x.breaks))
                    {
                        if (x.breaks[0] == '-')
                        {
                            minsBrea -= Convert.ToInt32(x.breaks.Substring(1, 2)) * 60;
                            minsBrea -= Convert.ToInt32(x.breaks.Substring(4, 2));
                        }
                        else

                        {
                            minsBrea += Convert.ToInt32(x.breaks.Substring(0, 2)) * 60;
                            minsBrea += Convert.ToInt32(x.breaks.Substring(3, 2));

                        }
                    }
                });
                hoursWorked += minsWorked / 60;
                minsWorked = minsWorked % 60;

                hoursBreak = minsBrea / 60;
                minsBrea = minsBrea % 60;
                totalWorked = hoursWorked.ToString() + ":" + minsWorked.ToString();
                totalBreaks = hoursBreak.ToString() + ":" + minsBrea.ToString();
                X.Call("setTotal", totalWorked, totalBreaks);
                this.total.Text = total.ToString();
                var data = daysResponse.Items;
                if (daysResponse.Items != null)
                {
                    this.Store1.DataSource = daysResponse.Items;
                    this.Store1.DataBind();
                }
                e.Total = daysResponse.count;
            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error,exp.Message).Show();
            }
        }


        public static string time(int _minutes, bool _signed)
        {
            if (_minutes == 0)
                return "00:00";

            bool isNegative = _minutes < 0 ? true : false;

            _minutes = Math.Abs(_minutes);

            string hours = (_minutes / 60).ToString(), minutes = (_minutes % 60).ToString(), formattedTime;

            if (hours.Length == 1)
                hours = "0" + hours;

            if (minutes.Length == 1)
                minutes = "0" + minutes;

            formattedTime = hours + ':' + minutes;

            if (isNegative && _signed)
                formattedTime = "-" + formattedTime;

            return formattedTime;
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



        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }

        protected void attendanceShiftStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

        }

        protected void SaveShift(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["recordId"];
            string day = e.ExtraParams["dayId"];
            string emp = e.ExtraParams["EmployeeId"];
            

            string obj = e.ExtraParams["values"];
            AttendanceShift b = JsonConvert.DeserializeObject<AttendanceShift>(obj);

            b.recordId = id;
            b.dayId = day;
            b.employeeId = emp;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AttendanceShift> request = new PostRequest<AttendanceShift>();
                    request.entity = b;
                    PostResponse<AttendanceShift> r = _selfServiceService.ChildAddOrUpdate<AttendanceShift>(request);
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

                        //Add this record to the store 
                        this.attendanceShiftStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditShiftWindow.Close();
                        RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        sm.DeselectAll();
                        sm.Select(b.recordId.ToString());


                        Store1.Reload();
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
                    PostRequest<AttendanceShift> request = new PostRequest<AttendanceShift>();
                    request.entity = b;
                    PostResponse<AttendanceShift> r = _selfServiceService.ChildAddOrUpdate<AttendanceShift>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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


                        ModelProxy record = this.attendanceShiftStore.GetById(index);
                        
                        EditShiftForm.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditShiftWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }

                Store1.Reload();
            }
        }


        protected void AddShift(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object

            EditShiftForm.Reset();
            ca.Text = CurrentCA.Text;
            sc.Text = CurrentSC.Text;
            shiftDayId.Text = CurrentDay.Text;
            shiftEmpId.Text = CurrentEmployee.Text;
            this.EditShiftWindow.Title = Resources.Common.AddNewRecord;
          
            this.EditShiftWindow.Show();
        }

        protected void FillTimeApproval(string dayId, int employeeId)
        {
            try
            {
                
                string rep_params = "";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("1", employeeId.ToString());
                parameters.Add("2", dayId.ToString());
                parameters.Add("3", dayId.ToString());
                parameters.Add("4", "0");
                parameters.Add("5", "0");
                parameters.Add("6", "0");
                parameters.Add("7", "0");
                parameters.Add("8", "0");
                parameters.Add("9", "0");
                parameters.Add("10", "0");
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params = rep_params.Remove(rep_params.Length - 1);
                }



                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;


                ListResponse<Time> Times = _timeAttendanceService.ChildGetAll<Time>(req);
                if (!Times.Success)
                {
                    Common.errorMessage(Times);
                    return;
                }
                Times.Items.ForEach(x =>
                {
                    x.timeCodeString = GetLocalResourceObject(x.timeCode + "text").ToString();
                    switch (x.status)
                    {
                        case 1:
                            x.statusString = pendingHF.Text;
                            break;
                        case 2:
                            x.statusString = approvedHF.Text;
                            break;

                    }
                });

                TimeStore.DataSource = Times.Items;
                ////List<ActiveLeave> leaves = new List<ActiveLeave>();
                //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


                TimeStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
    }
}