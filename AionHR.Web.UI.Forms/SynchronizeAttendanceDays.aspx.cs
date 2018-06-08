using AionHR.Model.Employees.Profile;
using AionHR.Model.HelpFunction;
using AionHR.Model.TimeAttendance;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AionHR.Web.UI.Forms
{
    public partial class SynchronizeAttendanceDays : System.Web.UI.Page
    {
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
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
                HideShowButtons();
                HideShowColumns();
                employeeFilter.AddItem("All", 0);
                employeeFilter.Select(0);
                startingDate.Value = DateTime.Now;

                startingDate.MinDate = DateTime.Now.AddDays(-180);

                endingDate.MinDate = startingDate.SelectedDate;
                endingDate.Value = DateTime.Now;


            }

        }

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
        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
        }
        [DirectMethod]
        protected void SynchronizeAttendance(object sender, DirectEventArgs e)
        {
            SynchronizeAttendanceDay GD = new SynchronizeAttendanceDay();
            PostRequest<SynchronizeAttendanceDay> request = new PostRequest<SynchronizeAttendanceDay>();
            GD.employeeId = Convert.ToInt32(employeeFilter.Value);
            GD.fromDayId = startingDate.SelectedDate.ToString("yyyyMMdd");
            GD.toDayId = endingDate.SelectedDate.ToString("yyyyMMdd");
            request.entity = GD;


            PostResponse<SynchronizeAttendanceDay> resp = _helpFunctionService.ChildAddOrUpdate<SynchronizeAttendanceDay>(request);
            if (!resp.Success)
            { //Show an error saving...

                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return;

            }
            else
            {
                X.Msg.Alert("", GetGlobalResourceObject("Common", "SynchronizeDaySucc").ToString()).Show();
            }


        }

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            if (prms.Query == "All" || prms.Query == "الكل")
            {
                List<Employee> data1 = new List<Employee>();
                data1.Add(new Employee() { fullName = prms.Query, recordId = "0" });
                return data1;
            }
            else
            {
                List<Employee> data = GetEmployeesFiltered(prms.Query);
                data.ForEach(s => { s.fullName = s.name.fullName; });

                return data;
            }
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
        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }
    }
}