﻿using System;
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
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Web.UI.Forms.Utilities;
using Model.Company.News;
using Services.Messaging;
using Model.Company.Structure;
using Infrastructure.Session;
using Model.Employees.Profile;
using Model.System;
using Model.Employees.Leaves;
using Model.Attendance;
using Model.TimeAttendance;
using Services.Messaging.Reports;
using Model.Dashboard;
using Services.Messaging.TimeAttendance;
using Web.UI.Forms.ConstClasses;
using Model.SelfService;

namespace Web.UI.Forms
{
   
    public partial class TimeVariationSelfServices : System.Web.UI.Page 
    {
        public bool URLRouting { get; set; }
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IDashBoardService _dashBoardService = ServiceLocator.Current.GetInstance<IDashBoardService>();
        ISelfServiceService _selfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();

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

                if (!string.IsNullOrEmpty(Request.QueryString["_employeeId"]) && !string.IsNullOrEmpty(Request.QueryString["_fromDayId"]) && !string.IsNullOrEmpty(Request.QueryString["_toDayId"]))
                {
                    dateRange1.DefaultStartDate = DateTime.ParseExact(Server.UrlDecode(Request.QueryString["_fromDayId"]).Trim('\''), "yyyyMMdd", new CultureInfo("en"));
                    dateRange1.DefaultEndDate= DateTime.ParseExact(Server.UrlDecode(Request.QueryString["_toDayId"]).Trim('\''), "yyyyMMdd", new CultureInfo("en"));

                }
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(RT305), null, GridPanel1, null, null);
                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}
                if (!string.IsNullOrEmpty(Request.QueryString["_employeeId"]) && !string.IsNullOrEmpty(Request.QueryString["_fromDayId"]) && !string.IsNullOrEmpty(Request.QueryString["_toDayId"]))

                    Store1.Reload();
              
                dateRange1.DefaultStartDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                FillStatus();
                timeVariationType.Select(0);
             





            }


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

            }
        }






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

       

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
                 
             
             
                   



         



            try
            {
                if (string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
                    return;

                string rep_params = "";
                Dictionary<string, string> parameters = new Dictionary<string, string>();





                //  parameters.Add("1", "1");
                if (!string.IsNullOrEmpty(Request.QueryString["_employeeId"]) && !string.IsNullOrEmpty(Request.QueryString["_fromDayId"]) && !string.IsNullOrEmpty(Request.QueryString["_toDayId"]))
                {
                    parameters.Add("2", Request.QueryString["_fromDayId"]);
                    parameters.Add("3", Request.QueryString["_toDayId"]);
                    parameters.Add("1", Request.QueryString["_employeeId"]);
                }
                else
                {
                    parameters.Add("1", _systemService.SessionHelper.GetEmployeeId());
                    parameters.Add("2", dateRange1.GetRange().DateFrom.ToString("yyyyMMdd"));
                    parameters.Add("3", dateRange1.GetRange().DateTo.ToString("yyyyMMdd"));
                    if (!string.IsNullOrEmpty(apStatus.Value.ToString()))
                        parameters.Add("5", apStatus.Value.ToString());
                                        

                    if (!string.IsNullOrEmpty(timeVariationType.Value.ToString()))
                        parameters.Add("4", timeVariationType.Value.ToString());
                   

                }

              


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
         

               

                ListResponse<TimeVariationSelfService> daysResponse = _selfServiceService.ChildGetAll<TimeVariationSelfService>(req);

                //ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                //ListResponse<ActiveAbsence> daysResponse = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
                if (!daysResponse.Success)
                {
                   Common.errorMessage( daysResponse);
                    return;
                }
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                List<XMLDictionary> timeCodeList = ConstTimeVariationType.TimeCodeList(_systemService);
               
                daysResponse.Items.ForEach(
                    x =>
                    {
                        x.clockDurationString = time(x.clockDuration, true);
                        x.durationString = time(x.duration, true);
                      
                            x.timeCodeString = timeCodeList.Where(y => y.key == Convert.ToInt16(x.timeCode)).Count() != 0 ? timeCodeList.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;
                        
                        x.apStatusString = FillApprovalStatus(x.apStatus);
                        x.damageLevelString = FillDamageLevelString(x.damageLevel);
                       if (rtl)
                        x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("ar-AE"));
                       else
                          x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("en-US"));


                    }
                    );
                Store1.DataSource = daysResponse.Items;
                Store1.DataBind();
                e.Total = daysResponse.count;
                format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
            }
            catch(Exception exp)
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



        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }
        //private ActiveAttendanceRequest GetActiveAttendanceRequest()
        //{
        //    ActiveAttendanceRequest req = new ActiveAttendanceRequest();
        //    var d = jobInfo1.GetJobInfo();
        //    req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
        //    req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
        //    req.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value : 0;
        //    req.PositionId = d.PositionId.HasValue ? d.PositionId.Value : 0;
        //    req.fromDayId = dateRange1.GetRange().DateFrom.ToString("yyyyMMdd");
        //    req.toDayId = dateRange1.GetRange().DateTo.ToString("yyyyMMdd");
        //    int intResult;

        //    //if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0" && int.TryParse(branchId.Value.ToString(), out intResult))
        //    //{
        //    //    req.BranchId = intResult;



        //    //}
        //    //else
        //    //{
        //    //    req.BranchId = 0;

        //    //}

        //    //if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0" && int.TryParse(departmentId.Value.ToString(), out intResult))
        //    //{
        //    //    req.DepartmentId = intResult;


        //    //}
        //    //else
        //    //{
        //    //    req.DepartmentId = 0;

        //    //}
        //    //if (!string.IsNullOrEmpty(ComboBox1.Text) && ComboBox1.Value.ToString() != "0" && int.TryParse(ComboBox1.Value.ToString(), out intResult))
        //    //{
        //    //    req.PositionId = intResult;


        //    //}
        //    //else
        //    //{
        //    //    req.PositionId = 0;

        //    //}

        //    //if (!string.IsNullOrEmpty(divisionId.Text) && divisionId.Value.ToString() != "0" && int.TryParse(divisionId.Value.ToString(), out intResult))
        //    //{
        //    //    req.DivisionId = intResult;


        //    //}
        //    //else
        //    //{
        //    //    req.DivisionId = 0;

        //    //}

        //    //if (!string.IsNullOrEmpty(esId.Text) && esId.Value.ToString() != "0" && int.TryParse(esId.Value.ToString(), out intResult))
        //    //{
        //    //    req.StatusId = intResult;


        //    //}
        //    //else
        //    //{
        //    //    req.StatusId = 0;

        //    //}




        //    return req;
        //}

        private void FillStatus()
        {
            ListRequest statusReq = new ListRequest();
            ListResponse<EmploymentStatus> resp = _employeeService.ChildGetAll<EmploymentStatus>(statusReq);
            statusStore.DataSource = resp.Items;
            statusStore.DataBind();
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
                   R= string.Empty;
                    break;


            }
            return R;
        }
        [DirectMethod]
        protected void updateDuration(object sender, DirectEventArgs e)
        {


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
        protected void PoPuP(object sender, DirectEventArgs e)
        {

            try {

                string id = e.ExtraParams["id"];
                int dayId =Convert.ToInt32( e.ExtraParams["dayId"]);
                int employeeId = Convert.ToInt32(e.ExtraParams["employeeId"]);
                string damageLavel = e.ExtraParams["damage"];
                string durationValue = e.ExtraParams["duration"];
                string timeCode = e.ExtraParams["timeCode"];
                string shiftId = e.ExtraParams["shiftId"];
                string justificationParam = e.ExtraParams["justification"];
                string type = e.ExtraParams["type"];


                switch (type)
                {
                    case "imgEdit":
                        //Step 1 : get the object from the Web Service 

                        //Step 2 : call setvalues with the retrieved object

                        damage.Select(damageLavel);
                        duration.Text = durationValue;
                        recordId.Text = id;
                        justification.Text = justificationParam;

                        this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                        this.EditRecordWindow.Show();
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

                    case "imgAttach":

                        //Here will show up a winow relatice to attachement depending on the case we are working on
                        break;

                    case "LinkRender":
                        FillTimeApproval(dayId, employeeId,timeCode,shiftId);
                        TimeApprovalWindow.Show();

                        break;
                    default:
                        break;
                }
            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }

        private TimeVariationListRequest GetAbsentRequest()
        {

            TimeVariationListRequest reqTV = new TimeVariationListRequest();
            reqTV.BranchId = "0";
            reqTV.DepartmentId = "0";
            reqTV.DivisionId = "0";
            reqTV.PositionId = "0";
            reqTV.EsId = "0";
            reqTV.fromDayId = dateRange1.GetRange().DateFrom;
            reqTV.toDayId = dateRange1.GetRange().DateTo;
            reqTV.employeeId = _systemService.SessionHelper.GetEmployeeId();

            if (string.IsNullOrEmpty(apStatus.Value.ToString()))
            {

                reqTV.apStatus = "0";
            }
            else
                reqTV.apStatus = apStatus.Value.ToString();
            if (!string.IsNullOrEmpty(Request.QueryString["_employeeId"]) && !string.IsNullOrEmpty(Request.QueryString["_fromDayId"]) && !string.IsNullOrEmpty(Request.QueryString["_toDayId"]))
            {
                reqTV.fromDayId = DateTime.ParseExact(Request.QueryString["_fromDayId"], "yyyyMMdd", new CultureInfo("en"));
                reqTV.toDayId = DateTime.ParseExact(Request.QueryString["_toDayId"], "yyyyMMdd", new CultureInfo("en"));
                reqTV.employeeId = Request.QueryString["_employeeId"];
            }
            if (string.IsNullOrEmpty(fromDuration.Text))
            {

                reqTV.fromDuration = "0";
            }
            else
                reqTV.fromDuration = fromDuration.Text;
            if (string.IsNullOrEmpty(toDuration.Text))
            {

                reqTV.toDuration = "0";
            }
            else
                reqTV.toDuration = toDuration.Text;

            if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
            {

                reqTV.timeCode = "0";
            }
            else
                reqTV.timeCode = timeVariationType.Value.ToString();



            return reqTV;
        }
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            DashBoardTimeVariation b = JsonConvert.DeserializeObject<DashBoardTimeVariation>(obj);
        


            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null




            try
            {
                //getting the id of the record
                TimeVariationListRequest req1 = GetAbsentRequest();

                string rep_params = "";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("1", req1.employeeId);
                parameters.Add("2", req1.fromDayId.ToString("yyyyMMdd"));
                parameters.Add("3", req1.toDayId.ToString("yyyyMMdd"));
                parameters.Add("4","0");
                parameters.Add("5", req1.timeCode);
                parameters.Add("6", req1.apStatus);
                parameters.Add("7", req1.BranchId);
                parameters.Add("8", req1.DepartmentId);
                parameters.Add("9", req1.EsId);
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



                ListResponse<DashBoardTimeVariation> r = _selfServiceService.ChildGetAll<DashBoardTimeVariation>(req);                   //Step 1 Selecting the object or building up the object for update purpose

                //Step 2 : saving to store

                //Step 3 :  Check if request fails
                if (!r.Success)//it maybe another check
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }
                else
                {

                    if (r.Items.Where(x => x.recordId == id).Count() == 0)
                        return;
                    else
                    {
                        PostRequest<DashBoardTimeVariation> request = new PostRequest<DashBoardTimeVariation>();
                        request.entity = r.Items.Where(x => x.recordId == id).First();
                        request.entity.damageLevel = Convert.ToInt16(damage.Value);
                        request.entity.duration = Convert.ToInt16(duration.Text);
                        request.entity.justification = b.justification;
                       // request.entity.recordId = null;
                        PostResponse<DashBoardTimeVariation> response = _selfServiceService.ChildAddOrUpdate<DashBoardTimeVariation>(request);
                        if (!response.Success)//it maybe another check
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(response);
                            return;
                        }
                    }
                }


                Store1.Reload();
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
                this.EditRecordWindow.Close();

            }

                 
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        private string FillDamageLevelString (short? DamageLevel)
        {
            string R; 
            switch (DamageLevel)
            {
                case 1: R= GetLocalResourceObject("DamageWITHOUT_DAMAGE").ToString(); 
                    break;
                case 2:
                    R = GetLocalResourceObject("DamageWITH_DAMAGE").ToString();
                    break;
                default: R = string.Empty;
                    break;

            }
            return R; 
        }
        protected void FillTimeApproval(int dayId, int employeeId,string timeCode, string shiftId)
        {
            try
            {
                //DashboardTimeListRequest r = new DashboardTimeListRequest();
                //r.dayId = dayId.ToString();
           
             
                //r.timeCode = timeCode;
                //r.shiftId = shiftId;
             
           
                string rep_params = "";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("1", employeeId.ToString());
                parameters.Add("2", dayId.ToString());
                parameters.Add("3", dayId.ToString());
                parameters.Add("4", shiftId);
                parameters.Add("5", timeCode);
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

                TimeStore.DataSource = Times.Items;
                ////List<ActiveLeave> leaves = new List<ActiveLeave>();
                //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


                TimeStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
    }

    }
