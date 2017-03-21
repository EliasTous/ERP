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
        protected DataTable getData()
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("start", typeof(DateTime));
            dt.Columns.Add("end", typeof(DateTime));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("resource", typeof(string));
            dt.Columns.Add("color", typeof(string));

            DataRow dr;

            DateTime start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            dr = dt.NewRow();
            dr["id"] = 0;
            dr["start"] = start.AddDays(1);
            dr["end"] = start.AddDays(5);
            dr["name"] = "Event 1";
            dr["resource"] = "A";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = 1;
            dr["start"] = start.AddDays(2);
            dr["end"] = start.AddDays(10);
            dr["name"] = "Event 2";
            dr["resource"] = "A";
            dr["color"] = "#00aa00";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = 2;
            dr["start"] = start.AddDays(7);
            dr["end"] = start.AddDays(15);
            dr["name"] = "Event 3";
            dr["color"] = "#cc0000";
            dr["resource"] = "B";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = 3;
            dr["start"] = start.AddDays(3);
            dr["end"] = start.AddDays(25);
            dr["name"] = "Sales Dept. Meeting Once Again";
            dr["color"] = "#0000cc";
            dr["resource"] = "D";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = 4;
            dr["start"] = start.AddDays(1);
            dr["end"] = start.AddDays(10);
            dr["name"] = "Event 4";
            dr["resource"] = "I";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = 5;
            dr["start"] = start.AddDays(4);
            dr["end"] = start.AddDays(14);
            dr["name"] = "Event 5";
            dr["resource"] = "E";
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            dr["id"] = 6;
            dr["start"] = start.AddDays(3);
            dr["end"] = start.AddDays(7);
            dr["name"] = "Event 6";
            dr["resource"] = "F";
            dt.Rows.Add(dr);

            return dt;

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

                //dayId.SelectedDate = DateTime.Today;
                DayPilotScheduler1.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DayPilotScheduler1.Days = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
                
                

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


        private void FillDepartment()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
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

        protected void Unnamed_Event(object sender, DirectEventArgs e)
        {
            try
            {
                DayPilotScheduler1.StartDate = new DateTime(Convert.ToInt32(yearCombo.Value), Convert.ToInt32(monthCombo.Value),1);
                DayPilotScheduler1.Days = DateTime.DaysInMonth(Convert.ToInt32(yearCombo.Value), Convert.ToInt32(monthCombo.Value));
                
            }
            catch (Exception exp){ }
        }
    }
}