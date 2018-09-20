﻿using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Employees.Profile;
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
    public partial class GenerateAttendanceDays : System.Web.UI.Page
    {
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        SessionHelper h;
        EmployeeService _emp;
        SystemService _system;
        TimeAttendanceService _time;
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
                employeeFilter.AddItem(GetGlobalResourceObject("Common", "All").ToString(), 0);
                employeeFilter.Select(0);

                //startingDate.MinDate = DateTime.Now.AddDays(-180);

                // endingDate.MinDate = startingDate.SelectedDate;
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
        protected void GenerateAttendance(object sender, DirectEventArgs e)
        {
            GenerateAttendanceDay GD = new GenerateAttendanceDay(); 
            PostRequest<GenerateAttendanceDay> request = new PostRequest<GenerateAttendanceDay>();
            GD.employeeId = Convert.ToInt32(employeeFilter.Value); 
            GD.startingDate = startingDate.SelectedDate.ToString("yyyyMMdd");
            GD.endingDate = endingDate.SelectedDate.ToString("yyyyMMdd");
            request.entity = GD;


            PostResponse<GenerateAttendanceDay> resp = _timeAttendanceService.ChildAddOrUpdate<GenerateAttendanceDay>(request);
            if (!resp.Success)
            { //Show an error saving...

                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;

            }
            else
            {
                X.Msg.Alert("", GetGlobalResourceObject("Common", "GenerateAttendanceDaySucc").ToString()).Show();
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

        protected void StartLongAction(object sender, DirectEventArgs e)
        {
            this.Session["LongActionProgressGenAD"] = 0;
            DictionarySessionStorage storage = new DictionarySessionStorage();
            storage.Save("AccountId", _systemService.SessionHelper.Get("AccountId"));
            storage.Save("UserId", _systemService.SessionHelper.Get("UserId"));
            storage.Save("key", _systemService.SessionHelper.Get("Key"));
            h = new SessionHelper(storage, new APIKeyBasedTokenGenerator());
            //HttpRuntime.Cache.Insert("TotalRecords", 0);
            //HttpRuntime.Cache.Insert("LongActionProgress", 0);
            //HttpRuntime.Cache.Insert("finished", "0");
            if (HttpRuntime.Cache["finishedGenAD"] == null)
            {
                ThreadPool.QueueUserWorkItem(LongAction, new object[] { h });
                HttpRuntime.Cache.Insert("finishedGenAD", "0");
             
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("processingGen")).Show();
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
            _time = new TimeAttendanceService(h,new TimeAttendanceRepository());



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
                HttpRuntime.Cache.Insert("TotalRecordsGenAD", emps.count -1 );


                GenerateAttendanceDay GD = new GenerateAttendanceDay();
                PostRequest<GenerateAttendanceDay> request = new PostRequest<GenerateAttendanceDay>();
                int i = 0;
                emps.Items.ForEach(x =>
                {
                    GD.employeeId = Convert.ToInt32(x.recordId);
                    GD.startingDate = startingDate.SelectedDate.ToString("yyyyMMdd");
                    GD.endingDate = endingDate.SelectedDate.ToString("yyyyMMdd");
                    request.entity = GD;
                    PostResponse<GenerateAttendanceDay> resp = _time.ChildAddOrUpdate<GenerateAttendanceDay>(request);

                    if (!resp.Success)
                    { //Show an error saving...

                        //X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        //X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                        //throw new Exception();

                    }
                    //if (HttpRuntime.Cache["LongActionProgress"] != null)
                    //    HttpRuntime.Cache.Remove("LongActionProgress");
                    //HttpRuntime.Cache["LongActionProgress"] = i + 1;
                 
                    HttpRuntime.Cache.Insert("LongActionProgressGenAD",i++ );
                }

                );


                HttpRuntime.Cache.Insert("finishedGenAD", "1");
            }

            catch (Exception exp)
            {
                HttpRuntime.Cache.Insert("ErrorMsgGenAD", exp.Message);
                HttpRuntime.Cache.Insert("ErrorLogIdGenAD", exp.Message);

            }








        }

        protected void RefreshProgress(object sender, DirectEventArgs e)
        {

            float progress;



            if (HttpRuntime.Cache["LongActionProgressGenAD"] != null && HttpRuntime.Cache["TotalRecordsGenAD"] != null)
            {
               
             

                if (HttpRuntime.Cache["ErrorMsgGenAD"] != null)
                {



                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", TaskManager1.ClientID);

                    X.Msg.Alert(Resources.Common.Error, HttpRuntime.Cache["ErrorMsgGenAD"].ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + HttpRuntime.Cache["ErrorLogIdGenAD"].ToString()).Show();
                    HttpRuntime.Cache.Remove("LongActionProgressGenAD");
                    HttpRuntime.Cache.Remove("TotalRecordsGenAD");
                    HttpRuntime.Cache.Remove("ErrorMsgGenAD");
                    HttpRuntime.Cache.Remove("ErrorLogIdGenAD");
                    HttpRuntime.Cache.Remove("finishedGenAD");
                    processing.Text = "0";
                    return;
                }
                progress = float.Parse((HttpRuntime.Cache["TotalRecordsGenAD"].ToString())) / 100;
                if (HttpRuntime.Cache["LongActionProgressGenAD"].ToString() != HttpRuntime.Cache["TotalRecordsGenAD"].ToString())
                {
                    this.Progress1.UpdateProgress(100, string.Format(GetGlobalResourceObject("Common", "Step").ToString(), HttpRuntime.Cache["LongActionProgressGenAD"].ToString(), HttpRuntime.Cache["TotalRecordsGenAD"]));
                }
                if (HttpRuntime.Cache["finishedGenAD"] != null)
                {
                    if (HttpRuntime.Cache.Get("finishedGenAD").ToString() == "1")
                    {



                        this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", TaskManager1.ClientID);

                        X.Msg.Alert("", GetGlobalResourceObject("Common", "GenerateAttendanceDaySucc").ToString()).Show();
                        HttpRuntime.Cache.Remove("LongActionProgressGenAD");
                        HttpRuntime.Cache.Remove("TotalRecordsGenAD");
                        HttpRuntime.Cache.Remove("ErrorMsgGenAD");
                        HttpRuntime.Cache.Remove("ErrorLogIdGenAD");
                        HttpRuntime.Cache.Remove("finishedGenAD");
                        processing.Text = "0";
                        return;

                    }
                }
            }
        }
    }
}