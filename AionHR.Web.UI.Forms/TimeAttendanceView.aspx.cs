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

namespace AionHR.Web.UI.Forms
{
    public partial class TimeAttendanceView : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
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
                FillBranch();
                FillDepartment();
                FillDivision();
                dayId.SelectedDate = DateTime.Today;
                

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


            int dayId = Convert.ToInt32(e.ExtraParams["dayId"]);
            int employeeId = Convert.ToInt32(e.ExtraParams["employeeId"]);
            CurrentDay.Text = dayId.ToString();
            CurrentEmployee.Text = employeeId.ToString();
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    AttendanceShiftListRequest request = new AttendanceShiftListRequest();
                    request.EmployeeId = employeeId;
                    request.DayId = dayId;
                    ListResponse<AttendanceShift> shifts = _timeAttendanceService.ChildGetAll<AttendanceShift>(request);
                    if(shifts.Success)
                    {
                        attendanceShiftStore.DataSource = shifts.Items;
                        attendanceShiftStore.DataBind();
                        AttendanceShiftWindow.Show();
                    }

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
                    X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                    return;
                }
                
                //Step 2 :  remove the object from the store
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

        private AttendnanceDayListRequest GetAttendanceDayRequest()
        {
            AttendnanceDayListRequest req = new AttendnanceDayListRequest();

            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
            {
                req.BranchId = branchId.Value.ToString();
                GridPanel1.ColumnModel.Columns.Where(a => a.ID == "ColBranchName").First().SetHidden(true);


            }
            else
            {
                req.BranchId = "0";
                GridPanel1.ColumnModel.Columns.Where(a => a.ID == "ColBranchName").First().SetHidden(false);
            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
            {
                req.DepartmentId = departmentId.Value.ToString();
                GridPanel1.ColumnModel.Columns.Where(a => a.ID == "ColDepartmentName").First().SetHidden(true);

            }
            else
            {
                req.DepartmentId = "0";
                GridPanel1.ColumnModel.Columns.Where(a => a.ID == "ColDepartmentName").First().SetHidden(false);
            }

            if (dayId.SelectedDate != DateTime.MinValue)

            {
                req.DayId = dayId.SelectedDate.ToString("yyyyMMdd");
                GridPanel1.ColumnModel.Columns.Where(a => a.ID == "ColDay").First().SetHidden(true);


            }
            else
            {
                req.DayId = "";
                GridPanel1.ColumnModel.Columns.Where(a => a.ID == "ColDay").First().SetHidden(false);
            }

            if (!string.IsNullOrEmpty(employeeId.Text) && employeeId.Value.ToString() != "0")
            {
                req.EmployeeId = employeeId.Value.ToString();
                GridPanel1.ColumnModel.Columns.Where(a => a.ID == "ColName").First().SetHidden(true);


            }
            else
            {
                req.EmployeeId = "0";
                GridPanel1.ColumnModel.Columns.Where(a => a.ID == "ColName").First().SetHidden(false);

            }

            req.Month = "0";
            req.Year = "0";
            req.Size = "30";
            req.StartAt = "1";
            req.Filter = "";
            req.SortBy = "firstName";
            return req;
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page

            AttendnanceDayListRequest req = GetAttendanceDayRequest();

            ListResponse<AttendanceDay> daysResponse = _timeAttendanceService.ChildGetAll<AttendanceDay>(req);
            if(!daysResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, daysResponse.Summary).Show();
                return;
            }
            int total = daysResponse.Items.Sum(x => x.netOL);
            string totalWorked, totalBreaks;
            int hoursWorked=0, minsWorked = 0,hoursBreak=0,minsBrea=0;
            daysResponse.Items.ForEach(x => { hoursWorked += Convert.ToInt32(x.workingTime.Substring(0, 2)); minsWorked += Convert.ToInt32(x.workingTime.Substring(3, 2)); hoursBreak += Convert.ToInt32(x.breaks.Substring(0, 2)); minsBrea += Convert.ToInt32(x.breaks.Substring(3, 2)); });
            hoursWorked += minsWorked / 60;
            minsWorked = minsWorked % 60;

            hoursBreak += minsBrea / 60;
            minsBrea = minsBrea % 60;
            totalWorked = hoursWorked.ToString() + ":" + minsWorked.ToString();
            totalBreaks = hoursBreak.ToString() + ":" + minsBrea.ToString();
            X.Call("setTotal", totalWorked,totalBreaks);
            this.total.Text= total.ToString();
            var data = daysResponse.Items;
            if (daysResponse.Items != null)
            {
                this.Store1.DataSource = daysResponse.Items;
                this.Store1.DataBind();
            }
            e.Total = daysResponse.count;
        }


        private void FillDepartment()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if(!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if(!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
        }

        private void FillDivision()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();
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



            List<Employee> data = GetEmployeesFiltered(prms.Query);

            data.ForEach(s => s.fullName = s.name.fullName);
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
            req.SortBy = "firstName";

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;




            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            if(!response.Success)
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
            return response.Items;
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
                    PostResponse<AttendanceShift> r = _timeAttendanceService.ChildAddOrUpdate<AttendanceShift>(request);
                    b.recordId = r.recordId;

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
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
                    PostResponse<AttendanceShift> r = _timeAttendanceService.ChildAddOrUpdate<AttendanceShift>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
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
            shiftDayId.Text = CurrentDay.Text;
            shiftEmpId.Text = CurrentEmployee.Text;
            this.EditShiftWindow.Title = Resources.Common.AddNewRecord;
          
            this.EditShiftWindow.Show();
        }
    }
}