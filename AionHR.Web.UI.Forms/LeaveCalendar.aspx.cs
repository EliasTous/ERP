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
using AionHR.Model.LeaveManagement;
using AionHR.Model.TimeAttendance;
using AionHR.Infrastructure.JSON;

namespace AionHR.Web.UI.Forms
{
    public partial class LeaveCalendar : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();


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
        protected DataTable getData(string month, string year)
        {
            LeaveRequestListRequest req = GetFilteredRequest();

            ListResponse<LeaveRequest> resp = _leaveManagementService.ChildGetAll<LeaveRequest>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>" + GetGlobalResourceObject("Errors", "ErrorLogId")+ resp.LogId : resp.Summary).Show();
                return new DataTable();
            }
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("start", typeof(DateTime));
            dt.Columns.Add("end", typeof(DateTime));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("resource", typeof(string));
            dt.Columns.Add("backColor", typeof(string));

            DataRow dr;

            DateTime startDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
            DateTime endDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month)));
          
            List<string> employeeid = new List<string>(); 
            foreach (var item in resp.Items)
            {


                if (item.startDate > endDate || item.endDate < startDate)
                    continue;

                if (!employeeid.Contains(item.employeeId))
                {
                    DayPilotScheduler1.Resources.Add(new DayPilot.Web.Ui.Resource(item.employeeName, item.employeeId.ToString()));
                    employeeid.Add(item.employeeId);
                }
           
                dr = dt.NewRow();
                dr["id"] = item.recordId;
                dr["start"] = item.startDate;
                dr["end"] = item.endDate.AddDays(1);
                dr["name"] = item.employeeName;
                dr["resource"] = item.employeeId;
                switch (item.apStatus)
                {
                    case -1:
                        dr["backColor"] = "#ccff00";
                        break;
                    case 0:
                        dr["backColor"] = "#00ff00";
                        break;
                    case 1:
                        dr["backColor"] = "#ff0000";
                        break;
                    case 2:
                        dr["backColor"] = "#0000ff";
                        break;
                    case 3:
                        dr["backColor"] = "#00cccf";
                        break;
                }

                dt.Rows.Add(dr);
            }


            return dt;

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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(LeaveRequest),null,null ,btnAdd ,null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }

                //dayId.SelectedDate = DateTime.Today;
                CurrentMonth.Text = DateTime.Today.Month.ToString();
                CurrentYear.Text = DateTime.Today.Year.ToString();
                monthLbl.Text = DateTime.Today.ToString("MMMM");
                yearLbl.Text = DateTime.Today.ToString("yyyy");
                DayPilotScheduler1.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DayPilotScheduler1.Days = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
                DayPilotScheduler1.DataStartField = "start";
                DayPilotScheduler1.DataEndField = "end";
                DayPilotScheduler1.DataIdField = "id";
                DayPilotScheduler1.DataTextField = "name";
                DayPilotScheduler1.DataResourceField = "resource";
                UpdateCal(CurrentMonth.Text, CurrentYear.Text, Viewport1.Width.Value.ToString());

            }


        }

        protected void Page_Init(object sender, EventArgs e)
        {
            leaveRequest1.RefreshLeaveCalendarCallBack = new Forms.Controls.LeaveRequestControl.RefreshParent(RefreshCalendar);
            leaveRequest1.Store1 = null;
            leaveRequest1.GrigPanel1 = null;
        }

        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }

        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            leaveRequest1.Add();
        }

        private void HideShowButtons()
        {

        }


        /// <summary>
        /// hiding uncessary column in the grid. 
        /// </summary>
        private void HideShowColumns()
        {
            // this.colAttach.Visible = false;
        }


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                // this.Viewport1.RTL = true;

            }
        }

        private LeaveRequestListRequest GetFilteredRequest()
        {
            LeaveRequestListRequest req = new LeaveRequestListRequest();

            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;



            if (!string.IsNullOrEmpty(employeeFilter.Text) && employeeFilter.Value.ToString() != "0")
            {
                req.EmployeeId = Convert.ToInt32(employeeFilter.Value);


            }
            else
            {
                req.EmployeeId = 0;

            }

            if (!string.IsNullOrEmpty(includeOpen.Text))
            {
                req.status = Convert.ToInt32(includeOpen.Value);


            }
            else
            {
                req.status = 0;

            }

            req.Size = "50";
            req.StartAt = "0";
            req.SortBy = "recordId";

            return req;
        }


        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>

        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            ////GEtting the filter from the page

            //AttendnanceDayListRequest req = GetAttendanceDayRequest();

            //ListResponse<AttendanceDay> daysResponse = _timeAttendanceService.ChildGetAll<AttendanceDay>(req);
            //if (!daysResponse.Success)
            //{
            //    X.Msg.Alert(Resources.Common.Error, daysResponse.Summary).Show();
            //    return;
            //}
            //int total = daysResponse.Items.Sum(s => s.netOL);
            //X.Call("setTotal", total);
            //this.total.Text = total.ToString();
            //var data = daysResponse.Items;
            //if (daysResponse.Items != null)
            //{
            //    this.Store1.DataSource = daysResponse.Items;
            //    this.Store1.DataBind();
            //}
            //e.Total = daysResponse.count;
        }


       

        private void RefreshCalendar()
        {

            X.Call("refresh");
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
        public void UpdateCal(string month, string year, string parentWidth)
        {
            try
            {
                DayPilotScheduler1.StartDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
                DayPilotScheduler1.Days = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month));
                DayPilotScheduler1.DataStartField = "start";
                DayPilotScheduler1.DataEndField = "end";
                DayPilotScheduler1.DataIdField = "id";
                DayPilotScheduler1.DataTextField = "name";
                DayPilotScheduler1.DataResourceField = "resource";
                DayPilotScheduler1.DataSource = getData(month, year);

                DayPilotScheduler1.DataBind();
                int widthInt = Convert.ToInt32(parentWidth);

                DayPilotScheduler1.CellWidth = ((int)widthInt / DayPilotScheduler1.Days - 3);

                DayPilotScheduler1.Update();

                schedulerHolder.UpdateLayout();


            }
            catch (Exception exp) { }
        }

      
        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }
        [DirectMethod]
        public object FillEmployeeFilter(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }
      
        
       


        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }
       
        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    leaveRequest1.Update(id);
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

       
        [DirectMethod]
        public void HandleClick(string id)
        {
            leaveRequest1.Update(id);
        }
        protected void DayPilotScheduler1_EventClick(object sender, DayPilot.Web.Ui.Events.EventClickEventArgs e)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = e.Id;


            leaveRequest1.Update(e.Id);


        }

 
        protected void DayPilotScheduler1_BeforeEventRender(object sender, DayPilot.Web.Ui.Events.Scheduler.BeforeEventRenderEventArgs e)
        {
            e.BackgroundColor = (string)e.DataItem["backColor"];

        }

        [DirectMethod]
        public void FixLayout(string width)
        {
            int widthInt = Convert.ToInt32(width);

            DayPilotScheduler1.CellWidth = ((int)widthInt / 40) + 20;
            DayPilotScheduler1.Update();
        }



        protected void ReturnLeave(object sender, DirectEventArgs e)
        {
            leaveRequest1.Return();
        }

        [DirectMethod]
        public object FillReplacementEmployee(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
          
            //  return new
            // {
            return Common.GetEmployeesFiltered(prms.Query);
        }


    }
}