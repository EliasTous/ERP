using AionHR.Infrastructure;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Employees.Profile;
using AionHR.Model.HelpFunction;
using AionHR.Model.TimeAttendance;
using AionHR.Repository.WebService.Repositories;
using AionHR.Services.Implementations;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        SessionHelper h;
        EmployeeService _emp;
        SystemService _system;
        HelpFunctionService _help;
     
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
                employeeFilter.AddItem(GetGlobalResourceObject("Common", "All").ToString(), 0);
                employeeFilter.Select(0);
                startingDate.Value = DateTime.Now;

                //    startingDate.MinDate = DateTime.Now.AddDays(-180);

                //   endingDate.MinDate = startingDate.SelectedDate;
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
                    emps = _emp.GetAll<Employee>(empRequest);
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


                SynchronizeAttendanceDay GD = new SynchronizeAttendanceDay();
                PostRequest<SynchronizeAttendanceDay> request = new PostRequest<SynchronizeAttendanceDay>();

                emps.Items.ForEach(x =>
                {
                    GD.employeeId = Convert.ToInt32(x.recordId);
                    GD.fromDayId = startingDate.SelectedDate.ToString("yyyyMMdd");
                    GD.toDayId = endingDate.SelectedDate.ToString("yyyyMMdd");
                    request.entity = GD;
                    PostResponse<SynchronizeAttendanceDay> resp = _help.ChildAddOrUpdate<SynchronizeAttendanceDay>(request);

                    if (!resp.Success)
                    { //Show an error saving...

                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(resp);
                        throw new Exception();

                    }

                }

                );
                X.Msg.Alert("", GetGlobalResourceObject("Common", "SynchronizeDaySucc").ToString()).Show();


            }
            catch (Exception exp)
            {
                if (exp != null)
                    X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
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

        protected void StartLongAction(object sender, DirectEventArgs e)
        {
            this.Session["LongActionProgressSyncAD"] = 0;
            DictionarySessionStorage storage = new DictionarySessionStorage();
            storage.Save("AccountId", _systemService.SessionHelper.Get("AccountId"));
            storage.Save("UserId", _systemService.SessionHelper.Get("UserId"));
            storage.Save("key", _systemService.SessionHelper.Get("Key"));
            h = new SessionHelper(storage, new APIKeyBasedTokenGenerator());
            //HttpRuntime.Cache.Insert("TotalRecords", 0);
            //HttpRuntime.Cache.Insert("LongActionProgress", 0);
            //HttpRuntime.Cache.Insert("finished", "0");
            if (HttpRuntime.Cache["finishedSyncAD"] == null)
            {
                ThreadPool.QueueUserWorkItem(LongAction, new object[] { h });
                HttpRuntime.Cache.Insert("finishedSyncAD", "0");
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("processingSyn")).Show();
            }
            this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", TaskManager1.ClientID);



        }
        private void LongAction(object state)
        {
            object[] array = state as object[];
            SessionHelper h = (SessionHelper)array[0];

         

            //if (HttpRuntime.Cache["TotalRecords"] == null)
            //    HttpRuntime.Cache.Insert("TotalRecords",0);
            //if (HttpRuntime.Cache["LongActionProgress"] == null)
            //    HttpRuntime.Cache.Insert("LongActionProgress", 0);


            _emp = new EmployeeService(new EmployeeRepository(), h);

            _system = new SystemService(new SystemRepository(), h);
            _help = new HelpFunctionService(new HelpFunctionRepository(), h);


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
                    emps = _emp.GetAll<Employee>(empRequest);

                    if (!emps.Success)
                    {
                        //X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", emps.ErrorCode) != null ? GetGlobalResourceObject("Errors", emps.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + emps.LogId : emps.Summary).Show();
                        return;
                    }
                }


                else
                {
                    emps = new ListResponse<Employee>();
                    emps.Items = new List<Employee>();
                    emps.Items.Add(new Employee { recordId = employeeFilter.Value.ToString() });
                }
                //if (HttpRuntime.Cache["TotalRecords"] != null)
                //    HttpRuntime.Cache.Remove("TotalRecords");
                //HttpRuntime.Cache["TotalRecords"] = emps.count;
                HttpRuntime.Cache.Insert("TotalRecordsSyncAD", emps.count-1);


                SynchronizeAttendanceDay GD = new SynchronizeAttendanceDay();
                PostRequest<SynchronizeAttendanceDay> request = new PostRequest<SynchronizeAttendanceDay>();
                int i = 0;
                emps.Items.ForEach(x =>
                {
                    GD.employeeId = Convert.ToInt32(x.recordId);
                    GD.fromDayId = startingDate.SelectedDate.ToString("yyyyMMdd");
                    GD.toDayId = endingDate.SelectedDate.ToString("yyyyMMdd");
                    request.entity = GD;
                    PostResponse<SynchronizeAttendanceDay> resp = _help.ChildAddOrUpdate<SynchronizeAttendanceDay>(request);

                    if (!resp.Success)
                    { //Show an error saving...

                        //X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        //X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                        //throw new Exception();

                    }
                    //if (HttpRuntime.Cache["LongActionProgress"] != null)
                    //    HttpRuntime.Cache.Remove("LongActionProgress");
                    //HttpRuntime.Cache["LongActionProgress"] = i + 1;
                    
                    HttpRuntime.Cache.Insert("LongActionProgressSyncAD", i++ );
                }

                );


                HttpRuntime.Cache.Insert("finishedSyncAD", "1");
            }

            catch (Exception exp)
            {
                HttpRuntime.Cache.Insert("ErrorMsgSyncAD", exp.Message );
                HttpRuntime.Cache.Insert("ErrorLogIdSyncAD", exp.Message);

            }








        }

        protected void RefreshProgress(object sender, DirectEventArgs e)
        {

            float progress;



            if (HttpRuntime.Cache["LongActionProgressSyncAD"] != null && HttpRuntime.Cache["TotalRecordsSyncAD"] != null)
            {
              progress  = float.Parse((HttpRuntime.Cache["TotalRecordsSyncAD"].ToString())) / 100;
                if (HttpRuntime.Cache["LongActionProgressSyncAD"].ToString() != HttpRuntime.Cache["TotalRecordsSyncAD"].ToString())
                {
                    this.Progress1.UpdateProgress(progress, string.Format(GetGlobalResourceObject("Common", "Step").ToString(), HttpRuntime.Cache["LongActionProgressSyncAD"].ToString(), HttpRuntime.Cache["TotalRecordsSyncAD"]));
                }
                if (HttpRuntime.Cache["finishedSyncAD"] != null)
                {
                    if (HttpRuntime.Cache.Get("finishedSyncAD").ToString() == "1")
                    {



                        this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", TaskManager1.ClientID);

                        X.Msg.Alert("", GetGlobalResourceObject("Common", "SynchronizeDaySucc").ToString()).Show();
                        HttpRuntime.Cache.Remove("LongActionProgressSyncAD");
                        HttpRuntime.Cache.Remove("TotalRecordsSyncAD");
                        HttpRuntime.Cache.Remove("ErrorMsgSyncAD");
                        HttpRuntime.Cache.Remove("ErrorLogIdSyncAD");
                        HttpRuntime.Cache.Remove("finishedSyncAD");
                        processing.Text = "0";

                    }
                }

                if (HttpRuntime.Cache["ErrorMsgSyncAD"] !=null)
                {

                

                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", TaskManager1.ClientID);
                  
                    X.Msg.Alert(Resources.Common.Error, HttpRuntime.Cache["ErrorMsgSyncAD"].ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + HttpRuntime.Cache["ErrorLogIdSyncAD"].ToString()).Show();
                    HttpRuntime.Cache.Remove("LongActionProgressSyncAD");
                    HttpRuntime.Cache.Remove("TotalRecordsSyncAD");
                    HttpRuntime.Cache.Remove("ErrorMsgSyncAD");
                    HttpRuntime.Cache.Remove("ErrorLogIdSyncAD");
                    HttpRuntime.Cache.Remove("finishedSyncAD");
                    processing.Text = "0";

                }
            }
        }

       

    }
}