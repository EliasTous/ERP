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
using AionHR.Services.Messaging.System;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Services.Messaging.CompanyStructure;
using AionHR.Services.Messaging.Reports;
using System.Threading;
using AionHR.Model.AdminTemplates;
using AionHR.Infrastructure.Tokens;
using AionHR.Services.Implementations;
using AionHR.Repository.WebService.Repositories;
using AionHR.Services.Messaging.AdministrativeAffairs;
using AionHR.Services.Messaging.Employees;

namespace AionHR.Web.UI.Forms
{
    public partial class Employees : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IAdministrationService _administrationService = ServiceLocator.Current.GetInstance<IAdministrationService>();



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

        protected void Page_Init(object sender, EventArgs e)
        {
            employeeControl1.Store1 = Store1;

        }

        private void FillTemplateStore()
        {

            //GEtting the filter from the page
         


            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<AdTemplate> resp = _administrationService.ChildGetAll<AdTemplate>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            this.templateStore.DataSource = resp.Items;
           

            this.templateStore.DataBind();
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!X.IsAjaxRequest && !IsPostBack)
            {

                switch (_systemService.SessionHelper.getLangauge())
                {
                    case "ar":
                        {
                           
                            ResourceManager1.Locale = "ar";
                        }
                        break;
                    case "en":
                        {
                           
                            ResourceManager1.Locale = "en";
                        }
                        break;

                    case "fr":
                        {
                          
                            ResourceManager1.Locale = "fr-FR";
                        }
                        break;
                    case "de":
                        {
                           
                            ResourceManager1.Locale = "de-DE";
                        }
                        break;
                    default:
                        {


                            ResourceManager1.Locale = "en";
                        }
                        break;
                }




                FillTemplateStore();
                langaugeStore.DataSource = Common.XMLDictionaryList(_systemService, "23");
                langaugeStore.DataBind();
                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                ColHireDate.Format = _systemService.SessionHelper.GetDateformat();

                //inactivePref.Select("0");
                CurrentClassId.Text = ClassId.EPEM.ToString();

                BuildQuickViewTemplate();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Employee), null, GridPanel1, btnAdd, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }

                if (GridPanel1.ColumnModel.Columns[1].Renderer != null && !string.IsNullOrEmpty(GridPanel1.ColumnModel.Columns[1].Renderer.Handler))
                    imageVisible.Text = "False";
                else
                    imageVisible.Text = "True";


                


            }


        }

        [DirectMethod]
        public void SetLabels(string labels)
        {
            this.labels.Text = labels;
        }

        [DirectMethod]
        public void SetVals(string labels)
        {
            this.vals.Text = labels;
            searchTrigger.Text = string.Empty;
            labelbar.Hidden = false;
        }

        [DirectMethod]
        public void SetTexts(string labels)
        {
            this.texts.Text = labels;
            filter1.Text = labels;
        }

        private void BuildQuickViewTemplate()                         
             
                         
                           


        {
            try
            {
                string html = "<table width='50%' style='font-weight:bold;'><tr><td> ";


                html += GetLocalResourceObject("usedLeaves").ToString() + " {usedLeaves} </td><td>";
                html += GetLocalResourceObject("serviceDuration").ToString() + " {serviceDuration}</td><td>";
                html += GetLocalResourceObject("leavesPayment").ToString() + "{leavePayments}  </td> </tr><tr><td>";
                
                    //html += GetLocalResourceObject("LoansBalance").ToString() + " {loansBalance}   
                html += GetLocalResourceObject("LoansBalance").ToString() + " {loansBalance}   </td><td>";
                html += GetLocalResourceObject("EmployeeStatus").ToString() + " {status}  </td><td> </tr> <tr><td>";
                html += GetLocalResourceObject("lastLeaveStartDateTitle").ToString() + " {lastLeave}";


                html += GetLocalResourceObject("earnedLeaves").ToString() + " {earnedLeaves}</td><td>";
                html += GetLocalResourceObject("leavesBalanceTitle").ToString() + " {leavesBalance} </td><td></td></tr></table>";
                //     html += GetLocalResourceObject("salary").ToString() + " {salary}  </td><td>";


                //html += GetLocalResourceObject("lastLeaveStartDateTitle").ToString() + "{lastLeave} ";

           













                //html += GetLocalResourceObject("eosBalanceTitle").ToString() + " {eosBalance}</td><td>";

                //html += GetLocalResourceObject("lastLeaveStartDateTitle").ToString() + " {lastLeave}</td><td>";
                //html += GetLocalResourceObject("paidLeavesYTDTitle").ToString() + " {paidLeavesYTD}</td></tr><tr><td>";

                //html += GetLocalResourceObject("leavesBalanceTitle").ToString() + " {leavesBalance}</td><td>";


                //html += GetLocalResourceObject("earnedLeaves").ToString() + " {earnedLeaves}</td><td>";
                //html += GetLocalResourceObject("usedLeaves").ToString() + " {usedLeaves}</td></tr><tr><td>";
                //html += GetLocalResourceObject("paidLeaves").ToString() + " {paidLeaves}</td><td>";

                //html += GetLocalResourceObject("allowedLeaveYtdTitle").ToString() + " {allowedLeaveYtd}</td></tr></table>";
                RowExpander1.Template.Html = html;
            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message);
            }
        }
        private List<Department> GetDepartments()
        {
            DepartmentListRequest departmentsRequest = new DepartmentListRequest();
            departmentsRequest.type = 0;

            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+ resp.LogId : resp.Summary).Show();
                return new List<Department>();
            }
            return resp.Items;
        }
        private List<Division> GetDivisions()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return new List<Division>();
            }
            return resp.Items;
        }
        private List<Branch> GetBranches()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId")+ resp.LogId : resp.Summary).Show();
                return new List<Branch>();
            }

            return resp.Items;
        }
        private List<Model.Company.Structure.Position> GetPositions()
        {
            PositionListRequest positionsRequest = new PositionListRequest();
          
            positionsRequest.StartAt = "0";
            positionsRequest.Size = "1000";
            positionsRequest.SortBy = "positionRef";
            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(positionsRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return new List<Model.Company.Structure.Position>();
            }
            return resp.Items;
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
                this.Viewport1.RTL = true;
                BuildQuickViewTemplate();
            }
            pRTL.Text = rtl.ToString();
        }



        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string fullName = e.ExtraParams["fullName"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":

                    employeeControl1.Update(id.ToString(), fullName);


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

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>






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
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            employeeControl1.Add();
        }

        //private EmployeeListRequest GetListRequest(StoreReadDataEventArgs e)
        //{
        //    previousStartAt.Text= e.Start.ToString();
        //    EmployeeListRequest empRequest = new EmployeeListRequest();
        //    if (!string.IsNullOrEmpty(inactivePref.Text) && inactivePref.Value.ToString() != "")
        //    {
        //        empRequest.IncludeIsInactive = Convert.ToInt32(inactivePref.Value);
        //    }
        //    else
        //    {
        //        empRequest.IncludeIsInactive = 2;
        //    }

        //    var d = jobInfo1.GetJobInfo();
        //    empRequest.BranchId = d.BranchId.HasValue ? d.BranchId.Value.ToString() : "0";
        //    empRequest.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value.ToString() : "0";
        //    empRequest.filterField =  !string.IsNullOrEmpty(filterField.Text) ? filterField.Value.ToString() : "0";
        //    //empRequest.PositionId = d.PositionId.HasValue ? d.PositionId.Value.ToString() : "0";
        //    //empRequest.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value.ToString() : "0";


        //    if (e.Sort[0].Property == "name")
        //        empRequest.SortBy = GetNameFormat();
        //    else if (e.Sort[0].Property == "reference")
        //        empRequest.SortBy = "reference";
        //    else
        //        empRequest.SortBy = e.Sort[0].Property;
        //    if (storeSize.Text == "unlimited")
        //    {
        //        empRequest.Size = "1000";
        //        empRequest.StartAt = "0";
        //        storeSize.Text = "limited";
        //    }
        //    else
        //    {
        //        empRequest.Size = e.Limit.ToString();
        //        empRequest.StartAt = e.Start.ToString();
        //    }
        //    if (string.IsNullOrEmpty(searchTrigger.Text))
        //        empRequest.StartAt = previousStartAt.Text;
        //    empRequest.Filter = searchTrigger.Text;

        //    return empRequest;
        //}
        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();

        }
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            try {
                //GEtting the filter from the page

                //EmployeeListRequest empRequest = GetListRequest(e);


                string rep_params = vals.Text;
                EmployeeListRequest empRequest = new EmployeeListRequest();
                // ReportGenericRequest req = new ReportGenericRequest();
                empRequest.paramString= vals.Text;
                empRequest.StartAt = e.Start.ToString();
                empRequest.SortBy = "recordId";
                empRequest.Size = "30";




                if (string.IsNullOrEmpty(searchTrigger.Text))
                {
                    ListResponse<Employee> emps = _employeeService.GetAll<Employee>(empRequest);
                    if (!emps.Success)
                    {
                        Common.errorMessage(emps);
                        return;
                    }
                    e.Total = emps.count;
                    if (emps.Items != null)
                    {
                        this.Store1.DataSource = emps.Items;
                        this.Store1.DataBind();
                    }
                }
                else
                {
                    List<EmployeeSnapShot> emps = Common.GetEmployeesFiltered(searchTrigger.Text);
                    e.Total = emps.Count();
                    this.Store1.DataSource = emps;
                    this.Store1.DataBind();
                    labelbar.Hidden = true;


                }


            }
            catch( Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
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


        protected void Unnamed_Load(object sender, EventArgs e)
        {
        }

        [DirectMethod]
        public object GetQuickView(Dictionary<string, string> parameters)
        {
            try
            {
                EmployeeTerminationRecordRequest caRequest = new EmployeeTerminationRecordRequest();
                caRequest.employeeId = parameters["id"];
                RecordResponse<EmployeeTermination> response = _employeeService.ChildGetRecord<EmployeeTermination>(caRequest);

                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(response);
                    throw new Exception();
                }




                EmployeeQuickViewRecordRequest req = new EmployeeQuickViewRecordRequest();
                req.RecordID = parameters["id"];
                if (response.result!=null)
                {
                        req.asOfDate = response.result.date ?? DateTime.Now;
                
                }
                else
                req.asOfDate = DateTime.Now;
                RecordResponse<EmployeeQuickView> qv = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
                if (!qv.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", qv.ErrorCode) != null ? GetGlobalResourceObject("Errors", qv.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + qv.LogId : qv.Summary).Show();
                    return null;
                }
               if (qv.result.terminationDate!=null)
                qv.result.statusString = GetLocalResourceObject("EmployeeStatus3").ToString();
               else
                    qv.result.statusString = GetLocalResourceObject("EmployeeStatus1").ToString();




                return new
                {


                   
                    reportsTo = qv.result.reportToName!=null? qv.result.reportToName:"",
                    indemnity = qv.result.indemnity,
                    salary = qv.result.salary,
                    leavesBalance = qv.result.leaveBalance,
                    serviceDuration = qv.result.serviceDuration,
                    earnedLeaves = qv.result.earnedLeaves,
                    usedLeaves = qv.result.usedLeaves,
                    paidLeaves = qv.result.paidLeaves,
                    earnedLeavesLeg = qv.result.earnedLeavesLeg,
                    usedLeavesLeg = qv.result.usedLeavesLeg,

                    lastLeave = qv.result.LastLeave(_systemService.SessionHelper.GetDateformat()),
                    status = qv.result.statusString,
                    loansBalance = qv.result.loanBalance,
                    leavePayments = qv.result.leavePayments



                };
            }
            catch (Exception exp)
            {
                if (exp!=null)
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                return null; 
            }
         


        }

        //protected void openBatchEM(object sender, DirectEventArgs e )
        //{
        //    batchForm.Reset();
        //    FillCalendars();
        //    FillSchedules();
        //    FillVS();
        //    FillDepartments();
        //    FillBranches();
        //    FillDivisions();
        //    FillPositions();
        //    batchWindow.Show();
        //}
        private void FillDepartments()
        {
            departmentStore.DataSource = GetDepartments();
            departmentStore.DataBind();
            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;
            UserDataRecordRequest ud = new UserDataRecordRequest();
            ud.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ud.RecordID = "0";
            ud.classId = ((ClassIdentifier)typeof(Department).GetCustomAttributes(true).Where(t => t is ClassIdentifier).ToList()[0]).ClassID;
            RecordResponse<UserDataAccess> udR = _accessControlService.ChildGetRecord<UserDataAccess>(ud);

            if (udR.result == null || !udR.result.hasAccess)
            {
                departmentId.Select(0);
                X.Call("setDepartmentAllowBlank", true);
            }
        }
        private void FillBranches()
        {
            branchStore.DataSource = GetBranches();
            branchStore.DataBind();
            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;
            UserDataRecordRequest ud = new UserDataRecordRequest();
            ud.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ud.RecordID = "0";
            ud.classId = ((ClassIdentifier)typeof(Branch).GetCustomAttributes(true).Where(t => t is ClassIdentifier).ToList()[0]).ClassID;
            RecordResponse<UserDataAccess> udR = _accessControlService.ChildGetRecord<UserDataAccess>(ud);
            if (udR.result == null || !udR.result.hasAccess)
            {
                branchId.Select(0);
                X.Call("setBranchAllowBlank", true);
            }
        }

        private void FillDivisions()
        {
            divisionStore.DataSource = GetDivisions();
            divisionStore.DataBind();

            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;
            UserDataRecordRequest ud = new UserDataRecordRequest();
            ud.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ud.RecordID = "0";
            ud.classId = ((ClassIdentifier)typeof(Division).GetCustomAttributes(true).Where(t => t is ClassIdentifier).ToList()[0]).ClassID;
            RecordResponse<UserDataAccess> udR = _accessControlService.ChildGetRecord<UserDataAccess>(ud);
            if (udR.result == null || !udR.result.hasAccess)
            {
                divisionId.Select(0);
                X.Call("setDivisionAllowBlank", true);
            }
        }

        private void FillPositions()
        {

            positionStore.DataSource = GetPositions();
            positionStore.DataBind();
        }
        private void FillCalendars()
        {
            ListRequest req = new ListRequest();
            ListResponse<WorkingCalendar> resp = _timeAttendanceService.ChildGetAll<WorkingCalendar>(req);
            if(!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId")  + resp.LogId : resp.Summary).Show();
                return;
            }
            calendarStore.DataSource = resp.Items;
            calendarStore.DataBind();
        }

        private void FillSchedules()
        {
            ListRequest req = new ListRequest();
            ListResponse<AttendanceSchedule> resp = _timeAttendanceService.ChildGetAll<AttendanceSchedule>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            ScheduleStore.DataSource = resp.Items;
            ScheduleStore.DataBind();
        }

        private void FillVS()
        {
            ListRequest req = new ListRequest();
            ListResponse<VacationSchedule> resp = _leaveManagementService.ChildGetAll<VacationSchedule>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            VSStore.DataSource = resp.Items;
            VSStore.DataBind();
        }

        protected void SaveBatch(object sender, DirectEventArgs e )
        {
            BatchEM bat = JsonConvert.DeserializeObject< BatchEM>(e.ExtraParams["values"]);
            if(!bat.scId.HasValue && !bat.vsId.HasValue && !bat.caId.HasValue)
            {
                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("FewParameters").ToString()).Show();
                return;
            }

            if (!bat.branchId.HasValue)
                bat.branchId = 0;

            if (!bat.departmentId.HasValue)
                bat.departmentId = 0;

            if (!bat.positionId.HasValue)
                bat.positionId = 0;

            if (!bat.divisionId.HasValue)
                bat.divisionId = 0;

            PostRequest<BatchEM> batReq = new PostRequest<BatchEM>();
            
            batReq.entity = bat;
            PostResponse<BatchEM> resp = _employeeService.ChildAddOrUpdate<BatchEM>(batReq);
            if(!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            else
            {
                X.Msg.Alert(Resources.Common.RecordUpdatedSucc, GetLocalResourceObject("BatchSucc").ToString()).Show();
                batchWindow.Close();
                return;
            }
        }

        protected void StartLongAction(object sender, DirectEventArgs e)
        {
          
          
           
            //this.Session["LongActionProgressGenAD"] = 0;
            DictionarySessionStorage storage = new DictionarySessionStorage();
            storage.Save("AccountId", _systemService.SessionHelper.Get("AccountId"));
            storage.Save("UserId", _systemService.SessionHelper.Get("UserId"));
            storage.Save("key", _systemService.SessionHelper.Get("Key"));
            storage.Save("LanguageId", _systemService.SessionHelper.Get("Language").ToString() == "en" ? "1" : "2");
            SessionHelper h = new SessionHelper(storage, new APIKeyBasedTokenGenerator());

            //HttpRuntime.Cache.Insert("TotalRecords", 0);
            //HttpRuntime.Cache.Insert("LongActionProgress", 0);
            //HttpRuntime.Cache.Insert("finished", "0");
            string tamplate = templateId.SelectedItem.Value; 
            ThreadPool.QueueUserWorkItem(MailEm, new object[] { h , tamplate, vals.Text });



            this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", TaskManager1.ClientID);



        }

        protected void MailEm(object state)
        {
            try
            {
                object[] array = state as object[];
                SessionHelper h = (SessionHelper)array[0];
                string tamplate= (string)array[1];
                string vals = (string)array[2];

                AdministrationService administrationService = new AdministrationService(new AdministrationRepository(), h);


                MailEmployeeRecordRequest req = new MailEmployeeRecordRequest();
                req.param = vals;
                req.templateId = tamplate;

                RecordResponse<MailEmployee> resp = administrationService.ChildGetRecord<MailEmployee>(req);
                if (!resp.Success)
                {

                    HttpRuntime.Cache.Insert("ErrorMsgGenEM", resp.Summary);
                    HttpRuntime.Cache.Insert("ErrorLogIdGenEM", resp.LogId);
                    HttpRuntime.Cache.Insert("ErrorErrorCodeGenEM", resp.ErrorCode);

                }
                else
                {
                    if (!string.IsNullOrEmpty(resp.recordId))
                        HttpRuntime.Cache.Insert("genEM_RecordId", resp.recordId);
                }




            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();

            }
        }
        protected void RefreshProgress(object sender, DirectEventArgs e)
        {

            try
            {


                double progress = 0;
                
                if (
                HttpRuntime.Cache.Get("ErrorMsgGenEM") != null ||
                HttpRuntime.Cache.Get("ErrorLogIdGenEM") != null ||
                HttpRuntime.Cache.Get("ErrorErrorCodeGenEM") != null)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", "Error_" + HttpRuntime.Cache.Get("ErrorErrorCodeGenEM")) != null ? GetGlobalResourceObject("Errors", "Error_" + HttpRuntime.Cache.Get("ErrorErrorCodeGenEM").ToString()).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + HttpRuntime.Cache.Get("ErrorLogIdGenEM").ToString() : HttpRuntime.Cache.Get("ErrorMsgGenEM").ToString()).Show();
                    HttpRuntime.Cache.Remove("genEM_RecordId");
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    mailWindow.Close();
                   
                
                    if (HttpRuntime.Cache.Get("ErrorErrorCodeGenEM") != null)
                        HttpRuntime.Cache.Remove("ErrorErrorCodeGenEM");
                    if (HttpRuntime.Cache.Get("ErrorLogIdGenEM") != null)
                        HttpRuntime.Cache.Remove("ErrorLogIdGenEM");
                    if (HttpRuntime.Cache.Get("ErrorErrorCodeGenEM") != null)
                        HttpRuntime.Cache.Remove("ErrorErrorCodeGenEM");

                    return;

                }


              if (!Common.isThreadSafe())
                {
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    HttpRuntime.Cache.Remove("isThreadSafe");
                    Common.errorMessage(null);
                }
              else
                {
                    Common.increasThreadSafeCounter();
                }
                RecordRequest req = new RecordRequest();
                if (HttpRuntime.Cache["genEM_RecordId"] != null)
                    req.RecordID = HttpRuntime.Cache["genEM_RecordId"].ToString();
                else
                {
                   
                    return;
                }
                RecordResponse<BackgroundJob> resp = _systemService.ChildGetRecord<BackgroundJob>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    HttpRuntime.Cache.Remove("genEM_RecordId");
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    mailWindow.Close();
                    Viewport1.ActiveIndex = 0;
                
                    return;
                }
                if (resp.result.errorId != null)
                {


                    X.Msg.Alert(Resources.Common.Error, resp.result.errorName).Show();
                    HttpRuntime.Cache.Remove("genEM_RecordId");
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                   
                  
                    mailWindow.Close();
                    return;
                }
                else
                {


                    if (resp.result.taskSize == 0)
                    {
                        progress = 0;
                        this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                        mailWindow.Close();
                        
                      
                      
                    }
                    else
                    {
                        progress = (double)resp.result.completed / resp.result.taskSize;
                        string prog = (float.Parse(progress.ToString()) * 100).ToString();
                        string message = GetGlobalResourceObject("Common", "working").ToString();
                   
                        this.mailEmployeesProgressBar.UpdateProgress(float.Parse(progress.ToString()), string.Format(message + " {0}%", (int)(float.Parse(progress.ToString()) * 100)));

                    }


                    if (resp.result.taskSize == resp.result.completed)
                    {
                        this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                        HttpRuntime.Cache.Remove("genEM_RecordId");
                        mailWindow.Close();
                       
                      
                        X.Msg.Alert("", Resources.Common.operationCompleted).Show();

                    }
                }
            }
            catch (Exception exp)
            {
                this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();

            }









        }

        private  string ReplaceAll( string seed, char[] chars, char replacementCharacter)
        {
            return chars.Aggregate(seed, (str, cItem) => str.Replace(cItem, replacementCharacter));
        }
        protected void FillPreview(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(templateId.SelectedItem.Value.ToString()) || string.IsNullOrEmpty(templateId.SelectedItem.Value.ToString()))
                return; 
            TemplateBodyRecordRequest req = new TemplateBodyRecordRequest();
            req.TemplateId = Convert.ToInt32(templateId.SelectedItem.Value); 
            req.LanguageId= Convert.ToInt32(langaugeId.SelectedItem.Value);
            RecordResponse<TemplateBody> resp = _administrationService.ChildGetRecord<TemplateBody>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return; 
            }

            if (resp.result != null)
            {
                Panel1.Update(Server.UrlDecode(resp.result.textBody), true);
                pnlMaximumTamplate.Update(Server.UrlDecode(resp.result.textBody), true);
            }
        }
    }
}