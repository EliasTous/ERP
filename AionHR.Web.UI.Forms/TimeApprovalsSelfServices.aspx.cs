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
using AionHR.Model.System;
using AionHR.Model.Attendance;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Model.LeaveManagement;
using AionHR.Services.Messaging.System;
using AionHR.Model.Company.Cases;
using System.Net;
using AionHR.Infrastructure.Domain;
using AionHR.Model.LoadTracking;
using AionHR.Services.Messaging.LoanManagment;
using AionHR.Model.Attributes;
using AionHR.Model.SelfService;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Services.Messaging.TimeAttendance;
using AionHR.Model.TimeAttendance;
using AionHR.Model.SelfService;

namespace AionHR.Web.UI.Forms
{
    public partial class TimeApprovalsSelfServices : System.Web.UI.Page
    {
        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
        }

        private List<Employee> GetEmployeesFiltered(string query)
        {
            //fill employee request 

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 0;
            req.SortBy = GetNameFormat();

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }

        private string GetNameFormat()
        {
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "nameFormat";
            RecordResponse<KeyValuePair<string, string>> response = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!response.Success)
            {

            }
            string paranthized = response.result.Value;
            paranthized = paranthized.Replace('{', ' ');
            paranthized = paranthized.Replace('}', ',');
            paranthized = paranthized.Substring(0, paranthized.Length - 1);
            paranthized = paranthized.Replace(" ", string.Empty);
            return paranthized;

        }

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();

        ILoanTrackingService _loanService = ServiceLocator.Current.GetInstance<ILoanTrackingService>();

        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();

        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        ISelfServiceService _selfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();
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
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();

                FillStatus();

                TimeStore.Reload();


                //if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                //CurrentEmployee.Text = Request.QueryString["employeeId"];

                DateColumn5.Format= TimedayIdDate.Format = _systemService.SessionHelper.GetDateformat();
                if (!string.IsNullOrEmpty(Request.QueryString["_employeeId"]) && !string.IsNullOrEmpty(Request.QueryString["_loanId"]))
                {
                    var p1 = new Ext.Net.Parameter("id", Request.QueryString["_loanId"]);
                    var p2 = new Ext.Net.Parameter("type", "imgEdit");
                    var col = new Ext.Net.ParameterCollection();
                    col.Add(p1);
                    col.Add(p2);
                 //   PoPuP(null, new DirectEventArgs(col));

                }
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(loanSelfService), BasicInfoTab, GridPanel1, btnAdd, SaveButton);

                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}
                //try

                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(LoanComment), null, loanCommentGrid, null, Button1);
                //    ApplyAccessControlOnLoanComments();
                //}
                //catch (AccessDeniedException exp)
                //{

                //    caseCommentsTab.Hidden = true;

                //}
                //if (purpose.InputType == InputType.Password)
                //{
                //    purpose.Visible = false;
                //    purposeField.Visible = true;
                //}
            }

        }
        [DirectMethod]
        protected void TimeTab_Load(object sender, EventArgs e)
        {

        }
        protected void TimeStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                DashboardRequest req = GetDashboardRequest();
                DashboardTimeListRequest r = new DashboardTimeListRequest();
                r.dayId = "";
                r.employeeId = 0;
                if (!string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    r.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());

                else
                {
                    TimeStore.DataSource = new List<Time>();
                    TimeStore.DataBind();
                    return;
                }
                r.timeCode = timeVariationType.GetTimeCode();
                r.shiftId = "0";
                r.apStatus = "1";
                r.BranchId = req.BranchId;
                r.DivisionId = req.DivisionId;
                r.PositionId = req.PositionId;
                r.DepartmentId = req.DepartmentId;
                r.EsId = req.EsId;
                r.Size = "50";
                r.StartAt = "0";


                ListResponse<TimeSelfService> Times = _selfServiceService.ChildGetAll<TimeSelfService>(r);
                if (!Times.Success)
                {
                    Common.errorMessage(Times);
                    return;
                }
                List<XMLDictionary> timeCodeList = ConstTimeVariationType.TimeCodeList(_systemService);
                int currentTimeCode;
                Times.Items.ForEach(x =>
                {
                    x.fullName = x.employeeName;
                    x.statusString = FillApprovalStatus(x.status);
                    if (Int32.TryParse(x.timeCode, out currentTimeCode))
                    {
                        x.timeCodeString = timeCodeList.Where(y => y.key == Convert.ToInt32(x.timeCode)).Count() != 0 ? timeCodeList.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;
                    }
                    if (string.IsNullOrEmpty(x.notes))
                        x.notes = " ";
                });

                // TimeStore.DataSource = Times.Items.Where(x=>x.status==1).ToList<Time>();
                ////List<ActiveLeave> leaves = new List<ActiveLeave>();
                //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });

                TimeStore.DataSource = Times.Items;
                TimeStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        [DirectMethod]
        protected void TimePoPUP(object sender, DirectEventArgs e)
        {
            TabPanel1.ActiveIndex = 0;
            string employeeId = e.ExtraParams["employeeId"];
            string employeeName = e.ExtraParams["employeeName"];
            string dayId = e.ExtraParams["dayId"];
            string dayIdDate = e.ExtraParams["dayIdDate"];
            string timeCode = e.ExtraParams["timeCode"];
            string timeCodeString = e.ExtraParams["timeCodeString"];
            string status = e.ExtraParams["status"];
            string shiftId = e.ExtraParams["shiftId"];


            string notes = e.ExtraParams["notes"];

            TimeApprovalRecordRequest r = new TimeApprovalRecordRequest();
            r.approverId = _systemService.SessionHelper.GetEmployeeId().ToString();
            r.employeeId = employeeId;
            r.dayId = dayId;
            r.timeCode = timeCode;
            r.shiftId = shiftId;
            RecordResponse<TimeSelfService> response = _selfServiceService.ChildGetRecord<TimeSelfService>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(response);
                return;
            }
            TimeStatus.Select(response.result.status.ToString());
            if (response.result.damageLevel == "1")
                response.result.damageLevel = GetLocalResourceObject("DamageWITHOUT_DAMAGE").ToString();
            else
                response.result.damageLevel = GetLocalResourceObject("DamageWITH_DAMAGE").ToString();

            TimeFormPanel.SetValues(response.result);


            TimeEmployeeName.Text = employeeName;
            TimedayIdDate.Text = dayIdDate;
            TimeTimeCodeString.Text = timeCodeString;

            shiftIdTF.Text = shiftId;
            TimeemployeeIdTF.Text = employeeId;
            TimedayIdTF.Text = dayId;
            TimeTimeCodeTF.Text = timeCode;


            FillTimeApproval(Convert.ToInt32(dayId), Convert.ToInt32(employeeId), timeCode, shiftId, status);

            this.TimeWindow.Title = Resources.Common.EditWindowsTitle;
            this.TimeWindow.Show();

        }
        private void FillTimeApproval(int dayId, int employeeId, string timeCode, string shiftId, string apstatus)
        {
            try
            {
                DashboardRequest req = GetDashboardRequest();
                DashboardTimeListRequest r = new DashboardTimeListRequest();
                r.fromDayId = dayId.ToString();
                r.toDayId = dayId.ToString();
                r.employeeId = employeeId;
                r.approverId = 0;
                r.timeCode = timeCode;
                r.shiftId = shiftId;
                // r.apStatus = apstatus.ToString();
                r.apStatus = "0";
                r.PositionId = req.PositionId;
                r.DepartmentId = req.DepartmentId;
                r.DivisionId = req.DivisionId;
                r.BranchId = req.BranchId;
                r.EsId = req.EsId;
                r.StartAt = "0";
                r.Size = "1000";


                ListResponse<TimeSelfService> Times = _selfServiceService.ChildGetAll<TimeSelfService>(r);
                if (!Times.Success)
                {
                    Common.errorMessage(Times);
                    return;
                }
                List<XMLDictionary> timeCodeList = ConstTimeVariationType.TimeCodeList(_systemService);
                int currentTimeCode;
                Times.Items.ForEach(x =>
                {
                    if (Int32.TryParse(x.timeCode, out currentTimeCode))
                    {
                        x.timeCodeString = timeCodeList.Where(y => y.key == Convert.ToInt32(x.timeCode)).Count() != 0 ? timeCodeList.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;
                    }

                    x.statusString = FillApprovalStatus(x.status);
                });

                timeApprovalStore.DataSource = Times.Items;
                ////List<ActiveLeave> leaves = new List<ActiveLeave>();
                //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


                timeApprovalStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
        protected void SaveTimeRecord(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            string id = e.ExtraParams["id"];
            Time TI = JsonConvert.DeserializeObject<Time>(obj);
            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 

                PostRequest<TimeSelfService> request = new PostRequest<TimeSelfService>();
                request.entity = new TimeSelfService();
                request.entity.employeeId = TI.employeeId;
                request.entity.dayId = TI.dayId;
                request.entity.timeCode = TI.timeCode;
                request.entity.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());
                request.entity.status = TI.status;
                request.entity.notes = TI.notes;
                request.entity.shiftId = TI.shiftId;

                PostResponse<TimeSelfService> r = _selfServiceService.ChildAddOrUpdate<TimeSelfService>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r); ;
                    return;
                }
                else
                {

                    TimeStore.Reload();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });
                  
                    this.TimeWindow.Close();
                }

            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }

        }
        private string FillApprovalStatus(short? apStatus)
        {
            string R;
            switch (apStatus)
            {
                case 1:
                    R = GetLocalResourceObject("FieldNew").ToString();
                    break;
                case 2:
                    R = GetLocalResourceObject("FieldApproved").ToString();
                    break;
                case -1:
                    R = GetLocalResourceObject("FieldRejected").ToString();
                    break;
                default:
                    R = string.Empty;
                    break;


            }
            return R;
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
       


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;
                CurrentLanguage.Text = "ar";
            }
            else
            {
                CurrentLanguage.Text = "en";
            }
        }


       


        [DirectMethod]
     


    



        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAll(object sender, DirectEventArgs e)
        {


            RowSelectionModel sm = this.TimeGridPanel.GetSelectionModel() as RowSelectionModel;
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




       

        [DirectMethod(ShowMask = true)]
        public void DoYes()
        {
            try
            {
                RowSelectionModel sm = this.TimeGridPanel.GetSelectionModel() as RowSelectionModel;

                foreach (SelectedRow row in sm.SelectedRows)
                {
                    //Step 1 :Getting the id of the selected record: it maybe string 
                    int id = int.Parse(row.RecordID);


                    //Step 2 : removing the record from the store
                    //To do add code here 

                    //Step 3 :  remove the record from the store
                    TimeStore.Remove(id);

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

     
        private void FillStatus()
        {
            ListRequest statusReq = new ListRequest();
            ListResponse<EmploymentStatus> resp = _employeeService.ChildGetAll<EmploymentStatus>(statusReq);
            statusStore.DataSource = resp.Items;
            statusStore.DataBind();
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

        protected void BasicInfoTab_Load(object sender, EventArgs e)
        {

        }
      
      
        protected void Timebatch(object sender, DirectEventArgs e)
        {
            string approve = e.ExtraParams["approve"];
            DashboardRequest req = GetDashboardRequest();
            DashboardTimeListRequest r = new DashboardTimeListRequest();
            r.dayId = "";
            r.employeeId = 0;
            if (!string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                r.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());

            else
            {
                TimeStore.DataSource = new List<Time>();
                TimeStore.DataBind();
                return;
            }
            r.timeCode = timeVariationType.GetTimeCode();
            r.shiftId = "0";
            r.apStatus = "1";
            r.BranchId = req.BranchId;
            r.DivisionId = req.DivisionId;
            r.PositionId = req.PositionId;
            r.DepartmentId = req.DepartmentId;
            r.EsId = req.EsId;
            r.StartAt = "0";
            r.Size = "50";

            ListResponse<TimeSelfService> Times = _selfServiceService.ChildGetAll<TimeSelfService>(r);
            if (!Times.Success)
            {
                Common.errorMessage(Times);
                return;
            }
            PostRequest<TimeSelfService> request = new PostRequest<TimeSelfService>();
            PostResponse<TimeSelfService> resp;
            Times.Items.ForEach(x =>
            {

                request.entity = x;
                if (approve == "true")
                    request.entity.status = 2;
                else
                    request.entity.status = -1;
                resp = _selfServiceService.ChildAddOrUpdate<TimeSelfService>(request);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
            });
            TimeStore.Reload();
           
        }
        private DashboardRequest GetDashboardRequest()
        {

            DashboardRequest req = new DashboardRequest();

            int intResult;

            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value.ToString() : "0";
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value.ToString() : "0";
            req.PositionId = d.PositionId.HasValue ? d.PositionId.Value.ToString() : "0"; 
            req.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value.ToString() : "0";
            if (!string.IsNullOrEmpty(esId.Text) && esId.Value.ToString() != "0")
            {
                req.EsId = esId.Value.ToString();



            }
            else
            {
                req.EsId = "0";

            }





            return req;
        }
    }
}