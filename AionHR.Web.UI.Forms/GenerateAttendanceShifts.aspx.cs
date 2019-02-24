using AionHR.Model.Company.Structure;
using AionHR.Model.Employees.Profile;
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
    public partial class GenerateAttendanceShifts : System.Web.UI.Page
    {

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
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
               
                startingDate.Value = DateTime.Now;

              //  startingDate.MinDate = DateTime.Now.AddDays(-60);

                endingDate.MinDate = startingDate.SelectedDate;
                endingDate.Value = DateTime.Now;

                FillBranch();
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
        protected void GenerateAttendance(object sender, DirectEventArgs e)
        {
            try
            {
                ListResponse<Employee> emps;

                if (Convert.ToInt32(employeeFilter.Value) == 0)
                {
                    EmployeeListRequest empRequest = new EmployeeListRequest();
                    empRequest.BranchId = "0";
                    empRequest.DepartmentId = "0";
                    empRequest.DivisionId = "0";
                    empRequest.Filter = "";
                    empRequest.filterField = "0";
                    empRequest.IncludeIsInactive = 0;
                    empRequest.PositionId = "0";
                    empRequest.SortBy = "reference";
                    empRequest.StartAt = "0";
                    empRequest.Size = "2000";
                    emps = _employeeService.GetAll<Employee>(empRequest);
                    if (!emps.Success)
                    {
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", emps.ErrorCode) != null ? GetGlobalResourceObject("Errors", emps.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + emps.LogId : emps.Summary).Show();
                        return;
                    }
                }


                else
                {
                    emps = new ListResponse<Employee>();
                    emps.Items.Add(new Employee { recordId = employeeFilter.Value.ToString() });
                }


                GenerateAttendanceShift GD = new GenerateAttendanceShift();
                PostRequest<GenerateAttendanceShift> request = new PostRequest<GenerateAttendanceShift>();

                emps.Items.ForEach(x =>
                {
                    GD.employeeId = Convert.ToInt32(x.recordId);
                    GD.branchId = Convert.ToInt32(branchId.SelectedItem.Value);
                    GD.fromDayId = startingDate.SelectedDate.ToString("yyyyMMdd");
                    GD.toDayId = endingDate.SelectedDate.ToString("yyyyMMdd");
                    request.entity = GD;


                    PostResponse<GenerateAttendanceShift> resp = _helpFunctionService.ChildAddOrUpdate<GenerateAttendanceShift>(request);
                    if (!resp.Success)
                    { //Show an error saving...

                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(resp);
                        throw new Exception();

                    }
                });




                X.Msg.Alert("", GetGlobalResourceObject("Common", "GenerateAttendanceShiftSucc").ToString()).Show();

            }
            catch (Exception exp)
            {
                if (exp != null)
                    X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }

       

        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _branchService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString()+ "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
            BranchStore.DataSource = resp.Items;
            BranchStore.DataBind();
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

            req.StartAt = "0";
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