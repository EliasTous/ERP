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
using Newtonsoft.Json.Converters;

namespace Web.UI.Forms
{
    public partial class Absent : System.Web.UI.Page 
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IDashBoardService _dashBoardService = ServiceLocator.Current.GetInstance<IDashBoardService>();
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
 
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(RT305), null, GridPanel1, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                //dateRange1.DefaultStartDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                //FillStatus();


                isSuperUser.Text = _systemService.SessionHelper.GetUserType() == 1 ? "true" : "false";
             




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

        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //private TimeVariationListRequest GetAbsentRequest()
        //{

        //    TimeVariationListRequest reqTV = new TimeVariationListRequest();
        //    reqTV.BranchId = jobInfo1.GetJobInfo().BranchId==null ? reqTV.BranchId=0 : jobInfo1.GetJobInfo().BranchId; 
        //    reqTV.DepartmentId= jobInfo1.GetJobInfo().DepartmentId==null ? reqTV.DepartmentId=0: jobInfo1.GetJobInfo().DepartmentId;
        //    reqTV.DivisionId= jobInfo1.GetJobInfo().DivisionId==null?reqTV.DivisionId=0 :jobInfo1.GetJobInfo().DivisionId;
        //     reqTV.PositionId = jobInfo1.GetJobInfo().PositionId == null ? reqTV.PositionId = 0 : jobInfo1.GetJobInfo().PositionId;
        //    reqTV.EsId =string.IsNullOrEmpty(esId.Value.ToString()) ? reqTV.EsId=0 :Convert.ToInt32( esId.Value);
        //    reqTV.fromDayId = dateRange1.GetRange().DateFrom;
        //    reqTV.toDayId = dateRange1.GetRange().DateTo;
        //    reqTV.employeeId = employeeCombo1.GetEmployee().employeeId.ToString();
        //    if (string.IsNullOrEmpty(apStatus.Value.ToString()))
        //    {

        //        reqTV.apStatus = "0";
        //    }
        //    else
        //        reqTV.apStatus = apStatus.Value.ToString();
        //    if (string.IsNullOrEmpty(fromDuration.Text))
        //    {

        //        reqTV.fromDuration = "0";
        //    }
        //    else
        //        reqTV.fromDuration = fromDuration.Text;
        //    if (string.IsNullOrEmpty(toDuration.Text))
        //    {

        //        reqTV.toDuration = "0";
        //    }
        //    else
        //        reqTV.toDuration = toDuration.Text;
        //    reqTV.timeCode = timeVariationType.GetTimeCode();

        //    //if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
        //    //{
        //    //    timeVariationType.Select(ConstTimeVariationType.LATE_CHECKIN.ToString());
        //    //    reqTV.timeCode = ConstTimeVariationType.LATE_CHECKIN.ToString();
        //    //}
        //    //else
        //    //reqTV.timeCode = timeVariationType.Value.ToString();



        //    return reqTV;
        //}

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                //GEtting the filter from the page

                string rep_params = vals.Text;
                //ReportGenericRequest req = new ReportGenericRequest();
                TVListRequest req = new TVListRequest();
                req.paramString = rep_params;
                req.size = "5000";
                req.startAt = "0";
                req.sortBy = "date,employeeRef";

                ListResponse<DashBoardTimeVariation> daysResponse = _timeAttendanceService.ChildGetAll<DashBoardTimeVariation>(req);

                //ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                //ListResponse<ActiveAbsence> daysResponse = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
                if (!daysResponse.Success)
                {
                   Common.errorMessage( daysResponse);
                    return;
                }
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                List<XMLDictionary> timeCode = ConstTimeVariationType.TimeCodeList(_systemService);
                List<XMLDictionary> statusList = Common.XMLDictionaryList(_systemService, "13");
                List<XMLDictionary> damageList = Common.XMLDictionaryList(_systemService, "22");
                daysResponse.Items.ForEach(
                    x =>
                    {
                        
                        x.clockDurationString = time(x.clockDuration, true);
                        x.durationString = time(x.duration, true);
                        x.timeCodeString = timeCode.Where(y => y.key == Convert.ToInt16(x.timeCode)).Count() != 0 ? timeCode.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;
                        
                        x.apStatusString = statusList.Where(y=> y.key == x.apStatus).Count() != 0 ? statusList.Where(y => y.key == x.apStatus).First().value : "";
                        x.damageLevelString = damageList.Where(y => y.key == x.damageLevel).Count() != 0 ? damageList.Where(y => y.key == x.damageLevel).First().value : ""; 
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

        //private void FillStatus()
        //{
        //    ListRequest statusReq = new ListRequest();
        //    ListResponse<EmploymentStatus> resp = _employeeService.ChildGetAll<EmploymentStatus>(statusReq);
        //    statusStore.DataSource = resp.Items;
        //    statusStore.DataBind();
        //}


        private string FillApprovalStatus(short? apStatus)
        {
            string R = "";
            List<XMLDictionary> statusList=    Common.XMLDictionaryList(_systemService, "13");
            if (apStatus!=null)
             R= statusList.Where(x => x.key == apStatus).Count() != 0 ? statusList.Where(x => x.key == apStatus).First().value : "";
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
                BasicInfoTab.Reset();
                FillDamageStore();
                string id = e.ExtraParams["id"];
                int dayId =Convert.ToInt32( e.ExtraParams["dayId"]);
                int employeeId = Convert.ToInt32(e.ExtraParams["employeeId"]);
                string damageLavel = e.ExtraParams["damage"];
                string durationValue = e.ExtraParams["duration"];
                string timeCodeParameter = e.ExtraParams["timeCode"];
                string shiftId = e.ExtraParams["shiftId"];
                string apStatus = e.ExtraParams["apStatus"];
                string type = e.ExtraParams["type"];
                string justificationParam = e.ExtraParams["justification"];
                string clockDuration = e.ExtraParams["clockDuration"];
                string arId = e.ExtraParams["arId"];
                

                switch (type)
                {
                    case "imgEdit":
                        //Step 1 : get the object from the Web Service 

                        //Step 2 : call setvalues with the retrieved object
                       
                        damage.Select(damageLavel);
                        duration.Text = durationValue;
                        clock.Text = clockDuration;
                       // recordId.Text = id;
                        justification.Text = justificationParam;
                        TimeApprovalReasonControl.setApprovalReason(arId);
                        if (apStatus=="2" || apStatus=="-1")
                        {
                            disableEditing(false);
                        }
                        else
                            disableEditing(true);

                        recordId.Text = id;
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



                    case "imgReject":
                        X.Msg.Confirm(Resources.Common.Confirmation, GetLocalResourceObject("RejectRecord").ToString(), new MessageBoxButtonsConfig
                        {
                            Yes = new MessageBoxButtonConfig
                            {
                                //We are call a direct request metho for deleting a record
                                Handler = String.Format("App.direct.RejectRecord({0})", id),
                                Text = Resources.Common.Yes
                            },
                            No = new MessageBoxButtonConfig
                            {
                                Text = Resources.Common.No
                            }

                        }).Show();
                        break;

                    case "imgAttach":
                        overrideForm.Reset();
                        RecordRequest req = new RecordRequest();
                        //req.employeeId = employeeId.ToString();
                        //req.dayId = dayId.ToString();
                        //req.shiftId = shiftId;
                        //req.timeCode = timeCodeParameter;
                        req.RecordID = id;
                        RecordResponse<DashBoardTimeVariation> resp = _timeAttendanceService.ChildGetRecord<DashBoardTimeVariation>(req);
                       if (!resp.Success)
                        {
                            Common.errorMessage(resp);
                            return;
                        }

                        ORId.Text = resp.result.recordId;
                        timeCodeStore.DataSource = ConstTimeVariationType.TimeCodeList(_systemService).Where(x => x.key == Convert.ToInt32(timeCodeParameter)).ToList();
                        timeCodeStore.DataBind();

                        FillBranch();
                        EmployeeQuickViewRecordRequest QVReq = new EmployeeQuickViewRecordRequest();
                        QVReq.RecordID = employeeId.ToString();

                        QVReq.asOfDate = DateTime.Now;
                        RecordResponse<EmployeeQuickView> qv = _employeeService.ChildGetRecord<EmployeeQuickView>(QVReq);

                        if (!qv.Success)
                        {
                            Common.errorMessage(qv);
                            return;
                        }
                        overrideForm.SetValues(resp.result);


                        branchId.Select(qv.result.branchId);

                        ListRequest UdIdReq = new ListRequest();

                        UdIdReq.Filter = "";
                        ListResponse<BiometricDevice> UdIdResp = _timeAttendanceService.ChildGetAll<BiometricDevice>(UdIdReq);
                        if (!UdIdResp.Success)
                        {
                            Common.errorMessage(UdIdResp);
                            return;
                        }
                        udIdStore.DataSource = UdIdResp.Items;
                        udIdStore.DataBind();
                        FlatPunchesListRequest FPreq = new FlatPunchesListRequest();
                        FPreq.shiftId = shiftId;
                        FPreq.sortBy = "clockStamp";
                        FPreq.StartAt = "0";
                        FPreq.Size = "30";
                        ListResponse <FlatPunch> FPresp = _timeAttendanceService.ChildGetAll<FlatPunch>(FPreq); 

                        if (!FPresp.Success)
                        {
                            Common.errorMessage(FPresp);
                            return;
                        }

                        punchesList.Text = "";
                        FPresp.Items.ForEach(x =>
                        {
                            punchesList.Text += x.clockStamp.ToString(_systemService.SessionHelper.GetDateformat()+ " HH:mm", CultureInfo.CurrentUICulture) + System.Environment.NewLine; 

                        }


                            );

                       
                        inOutStore.DataSource = Common.XMLDictionaryList(_systemService, "34");
                        inOutStore.DataBind();
                        overrideWindow.Show();

                        break;

                    case "LinkRender":
                        FillTimeApproval(id);
                        TimeApprovalWindow.Show();

                        break;

                    case "imgHistory":

                        TimeVariationHistoryControl1.Show("41203", id);
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

        [DirectMethod]
        public void RejectRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                RecordRequest req = new RecordRequest();
                req.RecordID = index;


                RecordResponse<DashBoardTimeVariation> r = _timeAttendanceService.ChildGetRecord<DashBoardTimeVariation>(req);                    //Step 1 Selecting the object or building up the object for update purpose

                if (!r.Success)
                {
                    Common.errorMessage(r);
                    return;
                }
                else
                {
                     RejectTimeVariationc rejectObject = new RejectTimeVariationc();
                    rejectObject.clockDuration = r.result.clockDuration;
                    rejectObject.damageLevel= r.result.damageLevel;
                    rejectObject.date= r.result.date;
                    rejectObject.dayId = r.result.dayId;
                    rejectObject.dtFrom = r.result.dtFrom;
                    rejectObject.dtTo = r.result.dtTo;
                    rejectObject.duration = r.result.duration;
                    rejectObject.employeeId = r.result.employeeId;
                    rejectObject.recordId = r.result.recordId;
                    rejectObject.shiftId = r.result.shiftId;
                    rejectObject.timeCode = r.result.timeCode;
                    rejectObject.justification = r.result.justification;
                    rejectObject.apId= r.result.apId;



                    PostRequest<RejectTimeVariationc> rejReq = new PostRequest<RejectTimeVariationc>();
                    rejReq.entity = rejectObject;


                    rejReq.entity.apStatus = -1;
                    PostResponse<RejectTimeVariationc> rejResp = _timeAttendanceService.ChildAddOrUpdate<RejectTimeVariationc>(rejReq);

                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordUpdatedSucc
                    });
                }
                Store1.Reload();

            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }

        private void FillBranch()
        {

            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
          

        }

        protected void FillUdId(object sender, DirectEventArgs e)
        {

            string branchIdParameter = e.ExtraParams["branchId"];

            ListRequest UdIdReq = new ListRequest();

            UdIdReq.Filter = "";
            ListResponse<BiometricDevice> UdIdResp = _timeAttendanceService.ChildGetAll<BiometricDevice>(UdIdReq);
            if (!UdIdResp.Success)
            {
                Common.errorMessage(UdIdResp);
                return;
            }
            udIdStore.DataSource = UdIdResp.Items.Where(x => x.branchId == branchIdParameter);
            udIdStore.DataBind();
        }


        protected void SelectedCheckType (object sender, DirectEventArgs e)
        {

            string inOut = e.ExtraParams["inOut"];
            string dtFromParameter = e.ExtraParams["dtFrom"];
            string dtToParameter = e.ExtraParams["dtTo"];

            DateTime temp = DateTime.Now;

            if (DateTime.TryParse(dtFromParameter, out temp))

            {
                clockStamp1.MinDate = temp.AddDays(-1);
                clockStamp1.SelectedDate = temp; 

            }
            if (DateTime.TryParse(dtToParameter, out temp))

            {
                clockStamp1.MaxDate = temp.AddDays(+1);


            }

          

            clockStamp1.MinDate = new DateTime(1930, 01, 01);
            clockStamp1.MaxDate = new DateTime(2050, 01, 01);
            switch (inOut)
            {
                case "1":

                    if (DateTime.TryParse(dtFromParameter, out temp))

                    {

                       

                        if (temp.TimeOfDay.Hours >= 6)
                            clockStamp1.MinDate = dtFrom.SelectedDate;
                        clockStamp1.Value = dtFromParameter;
                    }

                    break;
                case "2":
                    if (DateTime.TryParse(dtToParameter, out temp))
                    {
                       
                        if (temp.TimeOfDay.Hours <= 6)
                            clockStamp1.MaxDate = dtTo.SelectedDate;
                        clockStamp1.Value = dtToParameter;
                    }
                   
                    break;
            }

           
        }
        private void disableEditing(bool isActive)
        {
            duration.ReadOnly = !isActive;
            damage.ReadOnly = !isActive;
         //  TimeApprovalReasonControl.changeReadOnlyStatus (!isActive);


        }

        protected void SaveOverrideNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            try
            {


             string obj = e.ExtraParams["values"];
                string clockStamp = e.ExtraParams["clockStamp"];
                string shiftId = e.ExtraParams["shiftId"];
            string employeeId = e.ExtraParams["employeeId"];
                string recordId = e.ExtraParams["recordId"];
                
            OverrideTimeVariation b = JsonConvert.DeserializeObject<OverrideTimeVariation>(obj);


            RecordRequest recReq = new RecordRequest();
                recReq.RecordID = recordId; 
            //recReq.employeeId = employeeId.ToString();
            //recReq.dayId = dtFrom.SelectedDate.ToString("yyyyMMdd");
            //recReq.shiftId = shiftId;
            //recReq.timeCode = b.timeCode.ToString(); 
            RecordResponse<DashBoardTimeVariation> recResp = _timeAttendanceService.ChildGetRecord<DashBoardTimeVariation>(recReq);

            if (!recResp.Success)
            {
                Common.errorMessage(recResp);
                return; 
            }

                PostRequest<OverrideTimeVariation> req = new PostRequest<OverrideTimeVariation>();
                req.entity = b; 
                req.entity.apStatus = recResp.result.apStatus.ToString();
               
                req.entity.clockDuration = recResp.result.clockDuration.ToString();
                req.entity.damageLevel = recResp.result.damageLevel.ToString();
                req.entity.duration = recResp.result.duration.ToString();
                req.entity.shiftId = shiftId;
                req.entity.employeeId = recResp.result.employeeId.ToString();
                req.entity.date = recResp.result.date;
                req.entity.recordId = recordId;
                DateTime temp = DateTime.Now; 
                if (DateTime.TryParse(clockStamp,out temp))
                    {
                    req.entity.clockStamp = temp; 

                }
                else
                {
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
                PostResponse<OverrideTimeVariation> resp = _timeAttendanceService.ChildAddOrUpdate<OverrideTimeVariation>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return; 
                }

                Store1.Reload();
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
                overrideWindow.Close();
                InitializeCulture();
            }


            catch (Exception ex)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
            }
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
             
                RecordRequest req = new RecordRequest();
                req.RecordID = id; 


                RecordResponse<DashBoardTimeVariation> r = _timeAttendanceService.ChildGetRecord<DashBoardTimeVariation>(req);                    //Step 1 Selecting the object or building up the object for update purpose

                if (!r.Success)
                {
                    Common.errorMessage(r);
                    return; 
                }
                   if (r.result!=null)
                { 
                  
                        PostRequest<DashBoardTimeVariation> request = new PostRequest<DashBoardTimeVariation>();
                        request.entity = r.result;
                        request.entity.damageLevel = Convert.ToInt16(damage.Value);
                        request.entity.duration = Convert.ToInt16(duration.Text);
                        request.entity.justification = b.justification;
                        request.entity.arId = TimeApprovalReasonControl.getApprovalReason()=="0"?null: TimeApprovalReasonControl.getApprovalReason();


                        PostResponse<DashBoardTimeVariation> response = _timeAttendanceService.ChildAddOrUpdate<DashBoardTimeVariation>(request);
                        if (!response.Success)//it maybe another check
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(response);
                            return;
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

                TimeStore.DataSource = Times.Items;
                //////List<ActiveLeave> leaves = new List<ActiveLeave>();
                ////leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


                TimeStore.DataBind();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }


        //protected void FillTimeApproval(int dayId, int employeeId,string timeCode, string shiftId , string apstatus)
        //{
        //    try
        //    {
        //        //DashboardTimeListRequest r = new DashboardTimeListRequest();
        //        //r.fromDayId= dayId.ToString();
        //        //r.toDayId= dayId.ToString();
        //        //r.employeeId = employeeId;
        //        //r.approverId = 0;
        //        //r.timeCode = timeCode;
        //        //r.shiftId = shiftId;
        //        //// r.apStatus = apstatus.ToString();
        //        //r.apStatus = "0";
        //        //r.DepartmentId = "0";
        //        //r.DivisionId = "0";
        //        //r.BranchId = "0";
        //        //r.PositionId = "0";
        //        //r.EsId = "0";
        //        //r.StartAt = "0";
        //        //r.Size = "1000";
        //        //string rep_params = "";
        //        //Dictionary<string, string> parameters = new Dictionary<string, string>();
        //        //parameters.Add("1", employeeId.ToString());
        //        //parameters.Add("2", dayId.ToString());
        //        //parameters.Add("3", dayId.ToString());
        //        //if (!string.IsNullOrEmpty(shiftId))
        //        //parameters.Add("4", shiftId);
        //        //parameters.Add("5", timeCode);
                
        //        //foreach (KeyValuePair<string, string> entry in parameters)
        //        //{
        //        //    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
        //        //}
        //        //if (rep_params.Length > 0)
        //        //{
        //        //    if (rep_params[rep_params.Length - 1] == '^')
        //        //        rep_params = rep_params.Remove(rep_params.Length - 1);
        //        //}



        //        //ReportGenericRequest req = new ReportGenericRequest();
        //        //req.paramString = rep_params;



        //        //ListResponse<Time> Times = _timeAttendanceService.ChildGetAll<Time>(req);
        //        //if (!Times.Success)
        //        //{
        //        //    Common.errorMessage(Times);
        //        //    return;
        //        //}
        //        List<XMLDictionary> timeCodeList = ConstTimeVariationType.TimeCodeList(_systemService);
        //        //int currentTimeCode;
        //        //Times.Items.ForEach(x =>
        //        //{
        //        //    if (Int32.TryParse(x.timeCode, out currentTimeCode))
        //        //    {
        //        //        x.timeCodeString = timeCodeList.Where(y => y.key == Convert.ToInt32(x.timeCode)).Count() != 0 ? timeCodeList.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;
        //        //    }

        //        //    x.statusString = FillApprovalStatus(x.status);
        //        //});

        //        TimeStore.DataSource = Times.Items;
        //        ////List<ActiveLeave> leaves = new List<ActiveLeave>();
        //        //leaves.Add(new ActiveLeave() { destination = "dc", employeeId = 8, employeeName = new Model.Employees.Profile.EmployeeName() { fullName = "vima" }, endDate = DateTime.Now.AddDays(10) });


        //        TimeStore.DataBind();
        //    }
        //    catch (Exception exp)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //    }

        //}
        private void FillDamageStore()
        {
            damageStore.DataSource = Common.XMLDictionaryList(_systemService, "22");
            damageStore.DataBind();
        }
    }

    }
