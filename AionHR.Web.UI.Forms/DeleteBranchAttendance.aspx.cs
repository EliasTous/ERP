using AionHR.Model.Company.Structure;
using AionHR.Model.Employees.Profile;
using AionHR.Model.TimeAttendance;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Web.UI.Forms.ConstClasses;
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
    public partial class DeleteBranchAttendance : System.Web.UI.Page
    {
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IHelpFunctionService _helpFunctionService = ServiceLocator.Current.GetInstance<IHelpFunctionService>();


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
        protected void deleteBranchAttendances(object sender, DirectEventArgs e)
        {
            
            BranchAttendance BA = new BranchAttendance();

          
            if (!string.IsNullOrEmpty( branchId.SelectedItem.Value.ToString()))
            BA.branchId = Convert.ToInt32(branchId.SelectedItem.Value.ToString());


            if (!string.IsNullOrEmpty(employeeFilter.SelectedItem.Value.ToString()))
                BA.employeeId = Convert.ToInt32(employeeFilter.SelectedItem.Value.ToString());
            else
                BA.employeeId = 0;


            if (startingDate.SelectedDate != DateTime.MinValue)

            {
                BA.fromDayId = startingDate.SelectedDate.ToString("yyyyMMdd");


            }
            else
            {
                BA.fromDayId = "";
            }
            if (endingDate.SelectedDate != DateTime.MinValue)

            {
                BA.toDayId = endingDate.SelectedDate.ToString("yyyyMMdd");


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
                   Common.errorMessage(r);;
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


        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _branchService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
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
            if (!string.IsNullOrEmpty(branchId.SelectedItem.Value.ToString()))
                req.BranchId = branchId.SelectedItem.Value.ToString();
            else
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