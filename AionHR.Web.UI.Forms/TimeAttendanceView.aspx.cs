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
using Reports;

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
        ISelfServiceService _selfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();
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

                StartAt.Text = "0";
                if (!string.IsNullOrEmpty(Request.QueryString["_fromSelfService"]))
                {
                    FromSelfService.Text = Request.QueryString["_fromSelfService"];
                    loaderUrl.Text = "ReportParameterBrowser.aspx?_reportName=SSAD&values=";
                    Panel8.Loader.Url = "ReportParameterBrowser.aspx?_reportName=SSAD";

                    vals.Text = "6|" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToString("yyyyMMdd") + "^7|" + DateTime.Today.ToString("yyyyMMdd");
                }
                else
                {
                    vals.Text = "6|" + DateTime.Today.ToString("yyyyMMdd") + "^7|" + DateTime.Today.ToString("yyyyMMdd");
                    FromSelfService.Text = "false";
                }

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
            filterStartAT.Text = "true";
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



                string rep_params = vals.Text;
                TimeAttendanceViewListRequest req = new TimeAttendanceViewListRequest();
                req.paramString = rep_params;
                req.StartAt =filterStartAT.Text=="true"?"0": e.Start.ToString();
                StartAt.Text= e.Start.ToString();
                req.Size = "30";
                req.sortBy = "dayId";
                ListResponse<AttendanceDay> daysResponse;
                if (FromSelfService.Text == "true")
                    daysResponse = _selfServiceService.ChildGetAll<AttendanceDay>(req);
                else
                    daysResponse = _timeAttendanceService.ChildGetAll<AttendanceDay>(req);
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
                CultureInfo c = new CultureInfo("en");
                switch (_systemService.SessionHelper.getLangauge())
                {
                    case "ar":
                        {

                            c = new CultureInfo("en");
                        }
                        break;
                    case "en":
                        {

                            c = new CultureInfo("en");
                        }
                        break;

                    case "fr":
                        {

                            c = new CultureInfo("fr-FR");
                        }
                        break;
                    case "de":
                        {

                            c = new CultureInfo("de-DE");
                        }
                        break;


                }

                Dictionary<String, string> parameter = Common.FetchParametersAsDictionary(rep_params);
                string timeCode = "";
                if (parameter.ContainsKey("4"))
                    timeCode = parameter["4"].ToString();
                foreach (var x in daysResponse.Items)
                {
                    max++;
                    x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat(), c);
                    //ReportGenericRequest r = new ReportGenericRequest();
                    //r.paramString = "1| " + x.employeeId + "^2|" + x.dayId + "^3|" + x.dayId;
                    //ListResponse<FlatSchedule> fsresponse = _timeAttendanceService.ChildGetAll<FlatSchedule>(r);
                    //if (!fsresponse.Success)
                    //{
                    //    Common.errorMessage(fsresponse);
                    //    return;
                    //}
                    //fsresponse.Items.ForEach(fs => { fsstring += fs.from + " - " + fs.to + "|"; });
                    //if (fsstring.Length > 1)
                    //{
                    //    fsstring = fsstring.Substring(0, fsstring.Length - 1);
                    //    fsstring = "<span style='vertical-align:middle!important;'>" + fsstring + "</span>";
                    //}
                    //AttendanceShiftListRequest asReq = new AttendanceShiftListRequest();
                    //asReq.EmployeeId = Convert.ToInt32(x.employeeId);
                    //asReq.DayId = x.dayId;
                    //ListResponse<AttendanceShift> asResp = _timeAttendanceService.ChildGetAll<AttendanceShift>(asReq);
                    //if (!asResp.Success)
                    //{
                    //    Common.errorMessage(asResp);
                    //    return;

                    //}
                    //asResp.Items.ForEach(asItem => { asstring += asItem.checkIn + " - " + asItem.checkOut + "|"; });
                    //if (asstring.Length > 1)
                    //    asstring = asstring.Substring(0, asstring.Length - 1);
                    ReportGenericRequest tvReq = new ReportGenericRequest();
                    if (string.IsNullOrEmpty(timeCode))
                    tvReq.paramString = "1|" + x.employeeId + "^2|" + x.dayId + "^3|" + x.dayId + "&_startAt=0&_size=5000&_sortBy=date,employeeRef";
                    else
                        tvReq.paramString = "1|" + x.employeeId + "^2|" + x.dayId + "^3|" + x.dayId+ "^4|"+timeCode+"&_startAt=0&_size=5000&_sortBy=date,employeeRef";
                    ListResponse<DashBoardTimeVariation> tvResp;
                    if (FromSelfService.Text == "true")
                        tvResp = _selfServiceService.ChildGetAll<DashBoardTimeVariation>(tvReq);
                    else
                        tvResp = _timeAttendanceService.ChildGetAll<DashBoardTimeVariation>(tvReq);
                    if (!tvResp.Success)
                    {
                        Common.errorMessage(tvResp);
                        return;
                    }
                    tvResp.Items.ForEach(tv => {
                        string color = "black";
                        if (tv.apStatus.HasValue)
                            switch (tv.apStatus.Value)
                            {
                                case 1: color = "black"; break;
                                case 2: color = "green"; break;
                                case -1: color = "red"; break;


                            }
                        string scriptCode = "App.direct.DisplayApprovals(\"" + x.dayId + "\",\"" + x.employeeId + "\",\"" + tv.shiftId + "\",\"" + tv.timeCode + "\");";
                        if (tv.timeCode == 20 || tv.timeCode == 41)
                        {
                            tvstring += "<span class='time-variation-link' style='color:" + color + "' onclick='" + scriptCode + "'>" + tv.timeName + "</span>" + "|";
                        }
                        else
                        {
                            tvstring += "<span class='time-variation-link' style='color:" + color + "' onclick='" + scriptCode + "'>" + tv.timeName + " : " + tv.duration.ToString() + " " + minutesText.Text + "</span>" + "|";
                        }
                         });
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
                        positionName = x.positionName,
                        attendance = x.attendance,
                        schedule = x.schedule,
                        effectiveTime = x.effectiveTime
                       

                    });
                    if (max > 30)
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
        public void DisplayApprovals(string dayId, string employeeId, string shiftId, string code)
        {
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = "1|" + employeeId + "^2|" + dayId + "^3|" + dayId + "^4|" + shiftId + "^5|" + code;
            ListResponse<Time> resp = _timeAttendanceService.ChildGetAll<Time>(req);

            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;

            }
            timeCode = ConstTimeVariationType.TimeCodeList(_systemService);
            resp.Items.ForEach(x =>
            {

                x.timeCodeString = timeCode.Where(y => y.key == Convert.ToInt16(x.timeCode)).Count() != 0 ? timeCode.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;

                x.statusString = FillApprovalStatus(x.status);
            });
            Store3.DataSource = resp.Items;
            Store3.DataBind();
            TimeApprovalWindow.Show();
        }

        protected void FillTimeApproval(int dayId, int employeeId)
        {
            try
            {



                string rep_params = "";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("1", employeeId.ToString());
                parameters.Add("2", dayId.ToString());
                parameters.Add("3", dayId.ToString());
                parameters.Add("4", "0");
                parameters.Add("5", "0");
                parameters.Add("6", "0");
                parameters.Add("7", "0");
                parameters.Add("8", "0");
                parameters.Add("9", "0");
                parameters.Add("10", "0");
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params = rep_params.Remove(rep_params.Length - 1);
                }



                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;


                ListResponse<Time> Times = _timeAttendanceService.ChildGetAll<Time>(req);
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

        private TimeAttendanceViewReport GetReport()
        {

            string rep_params = vals.Text;
            TimeAttendanceViewListRequest req = new TimeAttendanceViewListRequest();
            req.paramString = rep_params;
            req.StartAt = "0";
            req.Size = "10000";
            req.sortBy = "dayId";
            ListResponse<AttendanceDay> resp = _timeAttendanceService.ChildGetAll<AttendanceDay>(req);


            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return new TimeAttendanceViewReport();
            }
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            DateTime temp = new DateTime();
            resp.Items.ForEach(x =>
            {
               if ( DateTime.TryParseExact(x.dayId,"yyyyMMdd", new CultureInfo("en"), DateTimeStyles.None,out temp))
                x.employeeName  = x.employeeName + System.Environment.NewLine +temp.ToString(_systemService.SessionHelper.GetDateformat()) + System.Environment.NewLine + x.branchName + System.Environment.NewLine + x.positionName;
               else
                x.employeeName = x.employeeName  + System.Environment.NewLine + x.branchName + System.Environment.NewLine + x.positionName;
            });
           
           


            TimeAttendanceViewReport p = new TimeAttendanceViewReport();

        
            p.DataSource = resp.Items;

        //    p.DataSource = resp.Items;
            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
           
            return p;



        }

        protected void printBtn_Click(object sender, EventArgs e)
        {
            TimeAttendanceViewReport p = GetReport();
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
            Response.Clear();
            Response.Write("<script>");
            Response.Write("window.document.forms[0].target = '_blank';");
            Response.Write("setTimeout(function () { window.document.forms[0].target = ''; }, 0);");
            Response.Write("</script>");
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }

        protected void ExportPdfBtn_Click(object sender, EventArgs e)
        {
            TimeAttendanceViewReport p = GetReport();
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms);
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }

        protected void ExportXLSBtn_Click(object sender, EventArgs e)
        {
            TimeAttendanceViewReport p = GetReport();
            string format = "xls";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToXls(ms);

            Response.Clear();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }
    }
}