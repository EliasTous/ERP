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
using AionHR.Model.Employees.Profile;
using AionHR.Services.Messaging.System;
using AionHR.Model.Attendance;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class EmployeeCalendars : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
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
                if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                {
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                    return;
                }
                CurrentEmployee.Text = Request.QueryString["employeeId"];
                if (string.IsNullOrEmpty(Request.QueryString["hireDate"]))
                {
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.MissingHireDate).Show();
                    Viewport11.Hidden = true;
                    return;
                }
                CurrentHireDate.Text = Request.QueryString["hireDate"];
                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";
                btnAdd.Disabled = SaveEHButton.Disabled = disabled;

                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(EmploymentHistory), EditEHForm, employeeCalenderGrid, btnAdd, SaveEHButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    //    employeeCalendarGrid.Hidden = true;

                }
                dayIdDtCO.Format = ecDate.Format = _systemService.SessionHelper.GetDateformat();
                FillCalendars();
                FillSchedules();
                ecDate.ReadOnly = false;
                ADDNewRecord.Text = "0";

            }
        }

        private void FillSchedules()
        {
            ListRequest vsRequest = new ListRequest();
            ListResponse<AttendanceSchedule> resp = _timeAttendanceService.ChildGetAll<AttendanceSchedule>(vsRequest);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
                return;
            }
            scheduleStore.DataSource = resp.Items;
            scheduleStore.DataBind();
        }
        private void FillCalendars()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<WorkingCalendar> resp = _timeAttendanceService.ChildGetAll<WorkingCalendar>(nationalityRequest);
            caStore.DataSource = resp.Items;
            caStore.DataBind();
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
                this.Viewport11.RTL = true;

            }
        }



        protected void PoPuPEH(object sender, DirectEventArgs e)
        {


            string dayID = e.ExtraParams["dayIdDtP"];
            string day, month, year;
            year = dayID.Substring(0, 4);
            month = dayID.Substring(5, 2);
            day = dayID.Substring(8, 2);
            dayID = year + month + day;
     


            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    EmployeeCalendarRecordRequest r = new EmployeeCalendarRecordRequest();
                    r.dayId = dayID;
                    r.employeeId = Convert.ToInt32(Request.QueryString["employeeId"]);



                    RecordResponse<EmployeeCalendar> response = _employeeService.ChildGetRecord<EmployeeCalendar>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    response.result.dayIdDt = DateTime.ParseExact(response.result.dayId, "yyyyMMdd", new CultureInfo("en"));
                    this.EditEHForm.SetValues(response.result);

                    //   statusId.Select(response.result.statusId.ToString());

                    this.EditEHwindow.Title = Resources.Common.EditWindowsTitle;
                    ecDate.ReadOnly = true;
                    this.EditEHwindow.Show();
                    break;

                //Step 2 : call setvalues with the retrieved object





                case "ColJIDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            //   Handler = String.Format("App.direct.DeleteJI({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                               Handler = String.Format("App.direct.DeleteEH('{0}','{1}')", dayID, Request.QueryString["employeeId"]),
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
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteEH(string dayID , string employeeId )
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeCalendar n = new EmployeeCalendar();
                n.dayId = dayID;
                n.employeeId =Convert.ToInt32( employeeId);
              


                PostRequest<EmployeeCalendar> req = new PostRequest<EmployeeCalendar>();
                req.entity = n;
                PostResponse<EmployeeCalendar>  res = _employeeService.ChildDelete<EmployeeCalendar>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, res.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store

                    employeeCalenderyStore.Reload();
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







        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Direct method for removing multiple records
        /// </summary>


        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ADDNewEH(object sender, DirectEventArgs e)
        {
            ADDNewRecord.Text = "1";
            //Reset all values of the relative object
            ecDate.ReadOnly = false;
            EditEHForm.Reset();
            this.EditEHwindow.Title = Resources.Common.AddNewRecord;
            this.EditEHwindow.Show();
            //  FillEHStatus();
            /* if (EHCount.Text == "0")
                 ehDate.SelectedDate = DateTime.ParseExact(CurrentHireDate.Text, "yyyy/MM/dd", new CultureInfo("en"));
             else
                 ehDate.SelectedDate = DateTime.Today;
                 */




        }




        protected void employeeCalender_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page





            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeCalendarRequest request = new EmployeeCalendarRequest();

            request.employeeId = Convert.ToInt32(Request.QueryString["employeeId"]);


            ListResponse<EmployeeCalendar> currencies = _employeeService.ChildGetAll<EmployeeCalendar>(request);


            if (!currencies.Success)
                X.Msg.Alert(Resources.Common.Error, currencies.Summary).Show();
            currencies.Items.ForEach(x => x.dayIdDt = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")));

            this.employeeCalenderyStore.DataSource = currencies.Items;
            e.Total = currencies.count;

            this.employeeCalenderyStore.DataBind();
        }




        protected void SaveEH(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

           
            string obj = e.ExtraParams["values"];
            EmployeeCalendar b = JsonConvert.DeserializeObject<EmployeeCalendar>(obj);
            b.dayId = b.dayIdDt.ToString("yyyyMMdd");
            b.employeeId = Convert.ToInt32(Request.QueryString["employeeId"]);
            b.caName= e.ExtraParams["caName"];
            b.scName = e.ExtraParams["scName"];


            if (ADDNewRecord.Text.Equals("1"))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<EmployeeCalendar> request = new PostRequest<EmployeeCalendar>();

                    request.entity = b;
                    PostResponse<EmployeeCalendar> r = _employeeService.ChildAddOrUpdate<EmployeeCalendar>(request);
                    //   b.recordId = r.recordId;

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
                        this.employeeCalenderyStore.Reload();

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                     
                        this.EditEHwindow.Close();
                        /*  RowSelectionModel sm = this.employeeCalenderGrid.GetSelectionModel() as RowSelectionModel;
                          sm.DeselectAll();
                          sm.Select(b.recordId.ToString());
                          EHCount.Text = (Convert.ToInt32(EHCount.Text) + 1).ToString();
                          */


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
                    //  int index = Convert.ToInt32(id);//getting the id of the record
                    PostRequest<EmployeeCalendar> request = new PostRequest<EmployeeCalendar>();
                    request.entity = b;
                    PostResponse<EmployeeCalendar> r = _employeeService.ChildAddOrUpdate<EmployeeCalendar>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
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
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        employeeCalenderyStore.Reload();
                        this.EditEHwindow.Close();


                    }
                }


                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }

        

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

       
       
       

        [DirectMethod]
        public object FillSupervisor(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<Employee> data = GetEmployeesFiltered(prms.Query);
            if (data == null)
                data = new List<Employee>();
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
            //};

        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = GetNameFormat();

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;




            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }

        #region combobox dynamic insert

       

        protected void addStatus(object sender, DirectEventArgs e)
        {
            EmploymentStatus dept = new EmploymentStatus();
            //dept.name = statusId.Text;

            PostRequest<EmploymentStatus> depReq = new PostRequest<EmploymentStatus>();
            depReq.entity = dept;

            PostResponse<EmploymentStatus> response = _employeeService.ChildAddOrUpdate<EmploymentStatus>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
              //  FillEHStatus();
               // statusId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        #endregion

        private string GetNameFormat()
        {
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "nameFormat";
            RecordResponse<KeyValuePair<string, string>> response = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!response.Success)
            {

            }
            string paranthized = response.result.Value;
            paranthized = paranthized.Replace('{', ' ');
            paranthized = paranthized.Replace('}', ',');
            paranthized = paranthized.Substring(0, paranthized.Length - 1);
            paranthized = paranthized.Replace(" ", string.Empty);
            return paranthized;

        }
        protected void addCalendar(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(caId.Text))
                return;
            WorkingCalendar dept = new WorkingCalendar();
            dept.name = caId.Text;

            PostRequest<WorkingCalendar> depReq = new PostRequest<WorkingCalendar>();
            depReq.entity = dept;
            PostResponse<WorkingCalendar> response = _timeAttendanceService.ChildAddOrUpdate<WorkingCalendar>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCalendars();
                caId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }



    }
}