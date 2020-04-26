using Infrastructure;
using Infrastructure.Session;
using Infrastructure.Tokens;
using Model;
using Model.Employees.Profile;
using Model.HelpFunction;
using Model.System;
using Model.TimeAttendance;
using Repository.WebService.Repositories;
using Services.Implementations;
using Services.Interfaces;
using Services.Messaging;
using Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Web.UI.Forms
{
    public partial class SyncActivities : System.Web.UI.Page
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
                activityStore.DataSource = Common.XMLDictionaryList(_systemService, "42");
                activityStore.DataBind();
              
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
       

        
      
        //{

        //    EmployeeListRequest req = new EmployeeListRequest();
        //    req.DepartmentId = "0";
        //    req.BranchId = "0";
        //    req.IncludeIsInactive = 2;
        //    req.SortBy = GetNameFormat();

        //    req.StartAt = "0";
        //    req.Size = "20";
        //    req.Filter = query;

        //    ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
        //    return response.Items;
        //}
        //private string GetNameFormat()
        //{
        //    return _systemService.SessionHelper.Get("nameFormat").ToString();
        //}

        protected void StartLongAction(object sender, DirectEventArgs e)
        {
            this.Session["LongActionProgressSync"] = 0;
            DictionarySessionStorage storage = new DictionarySessionStorage();
            storage.Save("AccountId", _systemService.SessionHelper.Get("AccountId"));
            storage.Save("UserId", _systemService.SessionHelper.Get("UserId"));
            storage.Save("key", _systemService.SessionHelper.Get("Key"));
            h = new SessionHelper(storage, new APIKeyBasedTokenGenerator());
            //HttpRuntime.Cache.Insert("TotalRecords", 0);
            //HttpRuntime.Cache.Insert("LongActionProgress", 0);
            //HttpRuntime.Cache.Insert("finished", "0");
            HttpRuntime.Cache.Remove("isThreadSafe");
            ThreadPool.QueueUserWorkItem(LongAction, new object[] { h,activityId.SelectedItem.Value });
               
                
            this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", TaskManager1.ClientID);



        }
        private void LongAction(object state)
        {
            object[] array = state as object[];
            SessionHelper h = (SessionHelper)array[0];
            int activity = Convert.ToInt32(array[1]);





            _emp = new EmployeeService(new EmployeeRepository(), h);
            LoanTrackingService _lt = new LoanTrackingService(new LoanTrackingRepository(), h);
            LeaveManagementService _lm = new LeaveManagementService( h,new LeaveManagementRepository());
            TimeAttendanceService _ta = new TimeAttendanceService(h, new TimeAttendanceRepository());
            AssetManagementService _am = new AssetManagementService(new AssetManagementRepository(), h);
            _system = new SystemService(new SystemRepository(), h);
            _help = new HelpFunctionService(new HelpFunctionRepository(), h);


            try
            {

                SyncActivity SA = new SyncActivity();
                SA.startDate = startingDate.SelectedDate;
                SA.endDate = endingDate.SelectedDate;

                PostRequest<SyncActivity> req = new PostRequest<SyncActivity>();
                req.entity = SA;
                PostResponse<SyncActivity> resp = new PostResponse<SyncActivity>();

                switch (activity)
                { 
                    case 1:
                    resp = _ta.ChildAddOrUpdate<SyncActivity>(req);
                    break;
                case 2:
                    resp = _lm.ChildAddOrUpdate<SyncActivity>(req);
                    break;
                case 3:
                    resp = _lm.ChildAddOrUpdate<SyncActivity>(req);
                    break;
                case 4:
                    resp = _lt.ChildAddOrUpdate<SyncActivity>(req);
                    break;
                case 5:
                    resp = _emp.ChildAddOrUpdate<SyncActivity>(req);
                    break;
                case 6:
                    resp = _am.ChildAddOrUpdate<SyncActivity>(req);
                    break;

                }


                if (!resp.Success)
                {
                    HttpRuntime.Cache.Insert("ErrorMsgSync", resp.Error);
                    HttpRuntime.Cache.Insert("LogIdMsgSync", resp.LogId);

                }



                HttpRuntime.Cache.Insert("syncAC_RecordId", resp.recordId);






            }

            catch (Exception exp)
            {
                HttpRuntime.Cache.Insert("ExceptionMsgSync", exp.Message );
              

            }








        }

        private void deleteFromCash()
        {
            if (HttpRuntime.Cache.Get("ExceptionMsgSync")!=null)
                HttpRuntime.Cache.Remove("ExceptionMsgSync");
            if (HttpRuntime.Cache.Get("ErrorMsgSync") != null)
                HttpRuntime.Cache.Remove("ErrorMsgSync");
            if (HttpRuntime.Cache.Get("LogIdMsgSync") != null)
                HttpRuntime.Cache.Remove("LogIdMsgSync");
            if (HttpRuntime.Cache.Get("syncAC_RecordId") != null)
                HttpRuntime.Cache.Remove("syncAC_RecordId");


        }
        protected void RefreshProgress(object sender, DirectEventArgs e)
        {

            try
            {


                double progress = 0;
                if ( HttpRuntime.Cache.Get("ExceptionMsgSync") != null )
              
                {
                    X.Msg.Alert(Resources.Common.Error,  HttpRuntime.Cache.Get("ExceptionMsgSync").ToString()).Show();
                    deleteFromCash();
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);             
                              
                                 


                }
                if (HttpRuntime.Cache.Get("ErrorMsgSync") != null || HttpRuntime.Cache.Get("LogIdMsgSync") != null)

                {
                    X.Msg.Alert(Resources.Common.Error, HttpRuntime.Cache.Get("ErrorMsgSync").ToString() + "<br>" + Resources.Errors.ErrorLogId + HttpRuntime.Cache.Get("LogIdMsgSync").ToString() + "<br>").Show();

                    deleteFromCash();


                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);




                }


                if (!Common.isThreadSafe())
                {
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    HttpRuntime.Cache.Remove("isThreadSafe");
                    deleteFromCash();
                    Common.errorMessage(null);
                }
                else
                {
                    Common.increasThreadSafeCounter();
                }
                RecordRequest req = new RecordRequest();
                if (HttpRuntime.Cache["syncAC_RecordId"] != null)
                    req.RecordID = HttpRuntime.Cache["syncAC_RecordId"].ToString();
                else
                {
                    // this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    return;
                }
                RecordResponse<BackgroundJob> resp = _systemService.ChildGetRecord<BackgroundJob>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                   
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);

                    deleteFromCash();

                    return;
                }
                if (resp.result.errorId != null)
                {


                    X.Msg.Alert(Resources.Common.Error, resp.result.errorName).Show();
                    deleteFromCash();
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                   
                }
                else
                {


                    if (resp.result.taskSize == 0)
                    {
                        progress = 0;
                        this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);

                        deleteFromCash();

                        X.Msg.Alert("",  Resources.Common.SyncSucc).Show();
                        this.Progress1.UpdateProgress(float.Parse(progress.ToString()), string.Format(" {0}%", (int)(float.Parse(progress.ToString()) * 100)));
                    }
                    else
                    {
                        progress = (double)resp.result.completed / resp.result.taskSize;
                        string prog = (float.Parse(progress.ToString()) * 100).ToString();
                        string message = GetGlobalResourceObject("Common", "working").ToString();
                        this.Progress1.UpdateProgress(float.Parse(progress.ToString()), string.Format(message + " {0}%", (int)(float.Parse(progress.ToString()) * 100)));
                      //  this.payListProgressBar.UpdateProgress(float.Parse(progress.ToString()), string.Format(message + " {0}%", (int)(float.Parse(progress.ToString()) * 100)));

                    }


                    if (resp.result.taskSize == resp.result.completed)
                    {
                        this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                        deleteFromCash();
                        //EditGenerateWindow.Close();
                        //Store1.Reload();

                        //payListWidow.Close();
                        X.Msg.Alert("", Resources.Common.operationCompleted).Show();
                        this.Progress1.UpdateProgress(float.Parse(progress.ToString()), string.Format( " {0}%", (int)(float.Parse(progress.ToString()) * 100)));
                    }
                }
            }
            catch (Exception exp)
            {
                this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                deleteFromCash();

            }









        }




    }
}