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
using AionHR.Services.Messaging.Reports;

namespace AionHR.Web.UI.Forms
{
    public partial class TimeApprovalsSelfServices : System.Web.UI.Page
    {


        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }
        //private List<Employee> GetEmployeesFiltered(string query)
        //{
        //    //fill employee request 

        //    EmployeeListRequest req = new EmployeeListRequest();
        //    req.DepartmentId = "0";
        //    req.BranchId = "0";
        //    req.IncludeIsInactive = 0;
        //    req.SortBy = GetNameFormat();

        //    req.StartAt = "0";
        //    req.Size = "20";
        //    req.Filter = query;

        //    ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
        //    return response.Items;
        //}

        //private string GetNameFormat()
        //{
        //    SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
        //    req.Key = "nameFormat";
        //    RecordResponse<KeyValuePair<string, string>> response = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
        //    if (!response.Success)
        //    {

        //    }
        //    string paranthized = response.result.Value;
        //    paranthized = paranthized.Replace('{', ' ');
        //    paranthized = paranthized.Replace('}', ',');
        //    paranthized = paranthized.Substring(0, paranthized.Length - 1);
        //    paranthized = paranthized.Replace(" ", string.Empty);
        //    return paranthized;

        //}

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();

        ILoanTrackingService _loanService = ServiceLocator.Current.GetInstance<ILoanTrackingService>();

        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();

        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        ISelfServiceService _selfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();


        [DirectMethod]
        public void SetLabels(string labels)
        {
            this.labels.Text = labels;
        }

        [DirectMethod]
        public void SetVals(string labels)
        {
            this.vals.Text = labels;

            labelbar.Hidden = false;
        }

        [DirectMethod]
        public void SetTexts(string labels)
        {
            this.texts.Text = labels;
        }

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



                TimeStore.Reload();


                //if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                //CurrentEmployee.Text = Request.QueryString["employeeId"];

                DateColumn5.Format = TimedayIdDate.Format = _systemService.SessionHelper.GetDateformat();
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
                //DashboardRequest req = GetDashboardRequest();
                //DashboardTimeListRequest r = new DashboardTimeListRequest();
                //r.fromDayId = dateRange1.GetRange().DateFrom.ToString("yyyyMMdd");
                //r.toDayId = dateRange1.GetRange().DateTo.ToString("yyyyMMdd");
                //r.dayId = "";
                //r.employeeId = 0;
                //if (!string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                //    r.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());

                //else
                //{
                //    TimeStore.DataSource = new List<Time>();
                //    TimeStore.DataBind();
                //    return;
                //}
                //r.timeCode = timeVariationType.GetTimeCode();
                //r.shiftId = "0";
                //r.apStatus = "1";
                //r.BranchId = req.BranchId;
                //r.DivisionId = req.DivisionId;
                //r.PositionId = req.PositionId;
                //r.DepartmentId = req.DepartmentId;
                //r.EsId = req.EsId;
                //r.Size = "50";
                //r.StartAt = "0";


                //ListResponse<TimeSelfService> Times = _selfServiceService.ChildGetAll<TimeSelfService>(r);
                string rep_params = vals.Text;
                TimeAttendanceViewListRequest req = new TimeAttendanceViewListRequest();
                req.paramString = rep_params;
                req.StartAt = e.Start.ToString();
                req.Size = "30";
                req.sortBy = "dayId";
                ListResponse<TimeSelfService> Times = _selfServiceService.ChildGetAll<TimeSelfService>(req);



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
            string seqNo = e.ExtraParams["seqNo"];
            string activityId = e.ExtraParams["activityId"];


            string notes = e.ExtraParams["notes"];

            TimeApprovalRecordRequest r = new TimeApprovalRecordRequest();
            r.seqNo = seqNo;
            //r.timeCode = timeCode;
            //r.shiftId = shiftId;
            r.tvId = activityId;
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
            // TimedayIdDate.Text = dayIdDate;
            TimeTimeCodeString.Text = timeCodeString;

            shiftIdTF.Text = shiftId;
            TimeemployeeIdTF.Text = employeeId;

            TimeTimeCodeTF.Text = timeCode;



            FillTimeApproval(activityId);
            tvId.Text = activityId;

            this.TimeWindow.Title = Resources.Common.EditWindowsTitle;
            this.TimeWindow.Show();

        }


        private void FillTimeApproval(string tvId)
        {



            string rep_params = "";
            try
            {
















                ReportGenericRequest r = new ReportGenericRequest();
                r.paramString = "12|" + tvId;




                ListResponse<Time> Times = _timeAttendanceService.ChildGetAll<Time>(r);
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
                //////List<ActiveLeave> leaves = new List<ActiveLeave>();
                ////leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


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
            string seqNo = e.ExtraParams["seqNo"];
            string tvId = e.ExtraParams["tvId"];
            Time TI = JsonConvert.DeserializeObject<Time>(obj);
            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 
                TimeApprovalRecordRequest recReq = new TimeApprovalRecordRequest();
                recReq.seqNo = seqNo;
                //r.timeCode = timeCode;
                //r.shiftId = shiftId;
                recReq.tvId = tvId;
                RecordResponse<TimeSelfService> recResp = _selfServiceService.ChildGetRecord<TimeSelfService>(recReq);
                if (!recResp.Success)
                {
                    Common.errorMessage(recResp);
                    return;

                }
                PostRequest<TimeSelfService> request = new PostRequest<TimeSelfService>();

                request.entity = recResp.result;
                if (recResp.result == null)
                    return;
                request.entity.status = TI.status;
                request.entity.notes = TI.notes;


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
            try
            {



                string approve = e.ExtraParams["approve"];
                string rep_params = vals.Text;
                TimeAttendanceViewListRequest req = new TimeAttendanceViewListRequest();
                req.paramString = rep_params;
                req.StartAt = "0";
                req.Size = "1000";
                req.sortBy = "dayId";
                ListResponse<TimeSelfService> Times = _selfServiceService.ChildGetAll<TimeSelfService>(req);

                //DashboardTimeListRequest r = new DashboardTimeListRequest();
                //r.dayId = "";
                //r.employeeId = 0;
                //if (!string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                //    r.approverId = Convert.ToInt32(_systemService.SessionHelper.GetEmployeeId());

                //else
                //{
                //    TimeStore.DataSource = new List<Time>();
                //    TimeStore.DataBind();
                //    return;
                //}
                //r.timeCode = timeVariationType.GetTimeCode();
                //r.shiftId = "0";
                //r.apStatus = "1";
                //r.BranchId = req.BranchId;
                //r.DivisionId = req.DivisionId;
                //r.PositionId = req.PositionId;
                //r.DepartmentId = req.DepartmentId;
                //r.EsId = req.EsId;
                //r.StartAt = "0";
                //r.Size = "50";

                //ListResponse<TimeSelfService> Times = _selfServiceService.ChildGetAll<TimeSelfService>(r);
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
                        throw new Exception();
                    }
                });
                TimeStore.Reload();

            }

            catch(Exception exp)
            {
                if (!string.IsNullOrEmpty(exp.Message))
                    X.MessageBox.Alert(Resources.Common.Error, exp.Message).Show();

            }
    }
       
    }
   
}