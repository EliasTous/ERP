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
using AionHR.Model.TimeAttendance;
using AionHR.Model.HelpFunction;
using AionHR.Services.Messaging.TimeAttendance;
using AionHR.Model.Dashboard;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Services.Messaging.System;
using AionHR.Services.Messaging.Reports;

namespace AionHR.Web.UI.Forms
{
    public partial class TimeAttendanceView : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IHelpFunctionService _helpFunctionService = ServiceLocator.Current.GetInstance<IHelpFunctionService>();
        List<XMLDictionary> timeCode = new List<XMLDictionary>();

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

                format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();

                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AttendanceDay), null, GridPanel1, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }

                List<XMLDictionary> timeCode = ConstTimeVariationType.TimeCodeList(_systemService);

            }


        }



        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }

        private TimeVariationListRequest GetAbsentRequest(DateTime day, int employeeId)
        {

            TimeVariationListRequest reqTV = new TimeVariationListRequest();
            reqTV.BranchId = "0";
            reqTV.DepartmentId = "0";
            reqTV.DivisionId = "0";
            reqTV.PositionId = "0";
            reqTV.EsId = "0";
            reqTV.fromDayId = day;
            reqTV.toDayId = day;
            reqTV.employeeId = employeeId.ToString();
            reqTV.apStatus = "0";
            reqTV.fromDuration = "0";
            reqTV.toDuration = "0";
            reqTV.timeCode = "0";


            return reqTV;
        }
        private string FillApprovalStatus(short? apStatus)
        {
            string R;
            switch (apStatus)
            {
                case 1:
                    R = GetGlobalResourceObject("Common", "FieldNew").ToString();
                    break;
                case 2:
                    R = GetGlobalResourceObject("Common", "FieldApproved").ToString();
                    break;
                case -1:
                    R = GetGlobalResourceObject("Common", "FieldRejected").ToString();
                    break;
                default:
                    R = string.Empty;
                    break;


            }
            return R;
        }
        private string FillDamageLevelString(short? DamageLevel)
        {
            string R;
            switch (DamageLevel)
            {
                case 1:
                    R = GetLocalResourceObject("DamageWITHOUT_DAMAGE").ToString();
                    break;
                case 2:
                    R = GetLocalResourceObject("DamageWITH_DAMAGE").ToString();
                    break;
                default:
                    R = string.Empty;
                    break;

            }
            return R;
        }



        protected void PoPuP(object sender, DirectEventArgs e)
        {
            try
            {

                int dayId = Convert.ToInt32(e.ExtraParams["dayId"]);
                int employeeId = Convert.ToInt32(e.ExtraParams["employeeId"]);
                CurrentDay.Text = dayId.ToString();
                CurrentEmployee.Text = employeeId.ToString();
                CurrentCA.Text = e.ExtraParams["ca"];
                CurrentSC.Text = e.ExtraParams["sc"];
                string type = e.ExtraParams["type"];
                switch (type)
                {
                    case "imgEdit":



                        break;
                    case "LinkRender":

                        TimeApprovalWindow.Show();

                        break;

                    default:
                        break;
                }
            }
            catch (Exception exp)
            {
                X.MessageBox.Alert(GetGlobalResourceObject("Common", "Error").ToString(), exp.Message);
            }


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

            }
        }


        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAll(object sender, DirectEventArgs e)
        {


            //RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
            //if (sm.SelectedRows.Count() <= 0)
            //    return;
            X.Msg.Confirm(Resources.Common.Confirmation, GetLocalResourceObject("DeleteBranchAttendances").ToString(), new MessageBoxButtonsConfig
            {
                //Calling DoYes the direct method for removing selecte record
                Yes = new MessageBoxButtonConfig
                {
                    Handler = "App.direct.deleteBranchAttendances()",
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
        ///    [DirectMethod(ShowMask = true)]
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
        /// </summary>
        [DirectMethod(ShowMask = true)]
        public void deleteBranchAttendances()
        {
            BranchAttendance BA = new BranchAttendance();


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
                    Common.errorMessage(r);
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

        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        [DirectMethod]
        public void SetLabels(string labels)
        {
            this.labels.Text = labels;
        }

        [DirectMethod]
        public void SetVals(string labels)
        {
            this.vals.Text = labels;
        }

        [DirectMethod]
        public void SetTexts(string labels)
        {
            this.texts.Text = labels;
        }
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                //GEtting the filter from the page

                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                req.StartAt = e.Start.ToString();
                ListResponse<AttendanceDay> daysResponse = _timeAttendanceService.ChildGetAll<AttendanceDay>(req);
                if (!daysResponse.Success)
                {
                    Common.errorMessage(daysResponse);
                    return;
                }
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                List<TimeAttendanceCompositeObject> objs = new List<TimeAttendanceCompositeObject>();
                int max = 0;
                string fsstring = "";
                string asstring = "";
                string tvstring = "";
                foreach (var x in daysResponse.Items)
                {
                    max++;
                    x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
                    ReportGenericRequest r = new ReportGenericRequest();
                    r.paramString = "2|" + x.dayId + "^1|" + x.employeeId;
                    ListResponse<FlatSchedule> fsresponse = _timeAttendanceService.ChildGetAll<FlatSchedule>(r);
                    if (!fsresponse.Success)
                    {
                        Common.errorMessage(fsresponse);
                        return;
                    }
                    fsresponse.Items.ForEach(fs => { fsstring += fs.from + " - " + fs.to + "|"; });
                    if (fsstring.Length > 1)
                        fsstring = fsstring.Substring(0, fsstring.Length - 1);
                    AttendanceShiftListRequest asReq = new AttendanceShiftListRequest();
                    asReq.EmployeeId = Convert.ToInt32(x.employeeId);
                    asReq.DayId = x.dayId;
                    ListResponse<AttendanceShift> asResp = _timeAttendanceService.ChildGetAll<AttendanceShift>(asReq);
                    if (!asResp.Success)
                    {
                        Common.errorMessage(asResp);
                        return;

                    }
                    asResp.Items.ForEach(asItem => { asstring += asItem.checkIn + " - " + asItem.checkOut + "|"; });
                    if (asstring.Length > 1)
                        asstring = asstring.Substring(0, asstring.Length - 1);
                    ReportGenericRequest tvReq = new ReportGenericRequest();
                    tvReq.paramString = "1|" + x.dayId + "^2|" + x.dayId + "^9|" + x.employeeId;
                    ListResponse<DashBoardTimeVariation> tvResp = _timeAttendanceService.ChildGetAll<DashBoardTimeVariation>(tvReq);
                    if(!tvResp.Success)
                    {
                        Common.errorMessage(tvResp);
                        return;
                    }
                    tvResp.Items.ForEach(tv => {
                        string color = "black";
                        if(tv.apStatus.HasValue)
                        switch(tv.apStatus.Value)
                        {
                            case 0: color = "black"; break;
                            case 1: color = "green"; break;
                            case 2: color = "red"; break;

                                   
                        }
                        string scriptCode = "alert(\"the chosen link was" + tv.shiftId + " ok\");";
                        tvstring += "<span style='cursor:pointer;font-weight: bold;color:" + color+"' onclick='"+ scriptCode+"'>"+tv.timeName +" : "+tv.duration.ToString()+" "+ minutesText.Text+"</span>"+"|"; });
                    if (tvstring.Length > 1)
                        tvstring = tvstring.Substring(0, tvstring.Length - 1);
                    objs.Add(new TimeAttendanceCompositeObject()
                    {
                        FSString = fsstring,
                        ASString = asstring,
                        TVString = tvstring,
                        dayId = x.dayId,
                        branchName = x.branchName,
                        departmentName = x.departmentName,
                        employeeName = x.employeeName,
                        employeeId = x.employeeId.ToString(),
                        dayIdString = x.dayIdString,
                        positionName = x.positionName
                    });
                    if (max > 20)
                        break;
                    fsstring = "";
                    asstring = "";
                    tvstring = "";
                }

                Store1.DataSource = objs;
                Store1.DataBind();
                e.Total = daysResponse.count;
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }


        public static string time(int _minutes, bool _signed)
        {
            if (_minutes == 0)
                return "00:00";

            bool isNegative = _minutes < 0 ? true : false;

            _minutes = Math.Abs(_minutes);

            string hours = (_minutes / 60).ToString(), minutes = (_minutes % 60).ToString(), formattedTime;

            if (hours.Length == 1)
                hours = "0" + hours;

            if (minutes.Length == 1)
                minutes = "0" + minutes;

            formattedTime = hours + ':' + minutes;

            if (isNegative && _signed)
                formattedTime = "-" + formattedTime;

            return formattedTime;
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



        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }

        [DirectMethod]
        public void DisplayApprovals(string dayId, string employeeId, string shiftId, string timeCode)
        {
        }

        protected void FillTimeApproval(int dayId, int employeeId)
        {
            try
            {
                DashboardTimeListRequest r = new DashboardTimeListRequest();
                r.dayId = dayId.ToString();
                r.employeeId = employeeId;
                r.approverId = 0;
                r.StartAt = "0";
                r.Size = "1000";
                r.shiftId = "0";
                r.timeCode = "0";
                r.apStatus = "0";
                r.fromDayId = dayId.ToString();
                r.toDayId = dayId.ToString();



                ListResponse<Time> Times = _timeAttendanceService.ChildGetAll<Time>(r);
                timeCode = ConstTimeVariationType.TimeCodeList(_systemService);
                if (!Times.Success)
                {
                    Common.errorMessage(Times);
                    return;
                }
                Times.Items.ForEach(x =>
                {

                    x.timeCodeString = timeCode.Where(y => y.key == Convert.ToInt16(x.timeCode)).Count() != 0 ? timeCode.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;

                    x.statusString = FillApprovalStatus(x.status);
                });

                Store3.DataSource = Times.Items;
                ////List<ActiveLeave> leaves = new List<ActiveLeave>();
                //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


                Store3.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
    }
}