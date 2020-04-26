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
    public partial class ChangeAutoApprovedVariations : System.Web.UI.Page
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
                FillAppReasStore();

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


               this.ApprovalStatusStore.DataSource = new object[]
               {
                   new object[] { 0, "No Change" },
                   new object[] { -1, "Reject" },
                   new object[] { 2, "Approve" }

               };

               this.Store2.DataSource = new object[]
               {
                   new object[] { 0, "No Change" },
                   new object[] { 1, "Without Damage" },
                   new object[] { 2, "with damage" }

               };


            }


        }


        private void FillAppReasStore()
        {
            ListRequest request = new ListRequest();

            request.Filter = "";

            ListResponse<ApprovalReason> Items = _companyStructureService.ChildGetAll<ApprovalReason>(request);
            if (!Items.Success)
            {
                Common.errorMessage(Items);
                return;
            }
            this.Store3.DataSource = Items.Items;

            this.Store3.DataBind();
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
            //this.colAttach.Visible = false;
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

                ListResponse<DashBoardTimeVariation3> daysResponse = _timeAttendanceService.ChildGetAll<DashBoardTimeVariation3>(req);

                //ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                //ListResponse<ActiveAbsence> daysResponse = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
                if (!daysResponse.Success)
                {
                    Common.errorMessage(daysResponse);
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

                        x.apStatusString = statusList.Where(y => y.key == x.apStatus).Count() != 0 ? statusList.Where(y => y.key == x.apStatus).First().value : "";
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
            catch (Exception exp)
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


        private string FillApprovalStatus(short? apStatus)
        {
            string R = "";
            List<XMLDictionary> statusList = Common.XMLDictionaryList(_systemService, "13");
            if (apStatus != null)
                R = statusList.Where(x => x.key == apStatus).Count() != 0 ? statusList.Where(x => x.key == apStatus).First().value : "";
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



        protected void Apply_Click(object sender, DirectEventArgs e)
        {
            try
            {


                if (ApprovalStatusId.SelectedItem.Value == null && DamageLevelId.SelectedItem.Value == null && justification.Text == "")
                {
                    if (ApprovalStatusId.SelectedItem.Value == null)
                    {
                        X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectApproval")).Show();
                        return;
                    }
                    else if (DamageLevelId.SelectedItem.Value == null)
                    {
                        X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectDamage")).Show();
                        return;
                    }
                    else if (justification.Text == null)
                    {
                        X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("FillJustification")).Show();
                        return;
                    }

                }
                else
                {
                    string rep_params = vals.Text;
                    //ReportGenericRequest req = new ReportGenericRequest();
                    TVListRequest req = new TVListRequest();
                    req.paramString = rep_params;
                    req.size = "5000";
                    req.startAt = "0";
                    req.sortBy = "date,employeeRef";

                    ListResponse<DashBoardTimeVariation3> daysResponse = _timeAttendanceService.ChildGetAll<DashBoardTimeVariation3>(req);
                    if (!daysResponse.Success)
                    {
                        Common.errorMessage(daysResponse);
                        return;
                    }

                    DashBoardTimeVariation b = new DashBoardTimeVariation();

                    if (daysResponse.Items.Count > 0)
                    {

                        daysResponse.Items.ForEach(
                           x =>
                           {
                               b.apId = x.apId;
                               if (ApprovalStatusId.SelectedItem.Value == null)
                               {
                                   b.apStatus = x.apStatus;
                                   b.apStatusString = x.apStatusString;
                               }
                               else
                               {
                                   b.apStatus = Convert.ToInt16(ApprovalStatusId.SelectedItem.Value);
                                   b.apStatusString = ApprovalStatusId.SelectedItem.Text;
                               }
                               b.branchName = x.branchName;
                               b.clockDuration = x.clockDuration;
                               b.clockDurationString = x.clockDurationString;
                               if (arId.SelectedItem.Value == null)
                               {
                                   b.arId = x.arId;
                               }
                               else
                               {
                                   b.arId = arId.SelectedItem.Value;
                               }

                               if (DamageLevelId.SelectedItem.Value == null)
                               {
                                   b.damageLevel = x.damageLevel;
                                   b.damageLevelString = x.damageLevelString;
                               }
                               else
                               {
                                   b.damageLevel = Convert.ToInt16(DamageLevelId.SelectedItem.Value);
                                   b.damageLevelString = DamageLevelId.SelectedItem.Text;
                               }

                               b.date = x.date;
                               b.dayId = x.dayId;
                               b.dayIdDate = x.dayIdDate;
                               b.dayIdString = x.dayIdString;
                               b.dtFrom = x.dtFrom;
                               b.dtTo = x.dtTo;
                               b.duration = x.duration;
                               b.durationString = x.durationString;
                               b.employeeId = x.employeeId;
                               b.employeeName = x.employeeName;
                               b.employeeRef = x.employeeRef;
                               if (justification.Text == "")
                               {
                                   b.justification = x.justification;
                               }
                               else
                               { b.justification = justification.Text; }
                               b.positionName = x.positionName;
                               //b.primaryKey = x.primaryKey;
                               b.recordId = x.recordId;
                               b.shiftId = x.shiftId;
                               b.timeCode = x.timeCode;
                               b.timeCodeString = x.timeCodeString;

                               PostRequest<DashBoardTimeVariation> request = new PostRequest<DashBoardTimeVariation>();
                               request.entity = b;

                               PostResponse<DashBoardTimeVariation> response = _timeAttendanceService.ChildAddOrUpdate<DashBoardTimeVariation>(request);
                               if (!response.Success)//it maybe another check
                               {
                                   X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                                   Common.errorMessage(response);
                                   return;
                               }
                           }
                           );
                        //ReloadData(new object(), new DirectEventArgs(new Ext.Net.ParameterCollection()));
                    }

                }

            }
            catch (Exception exp)
            {
                X.MessageBox.Alert(Resources.Common.Error, exp.Message);
            }

            //ReloadData(new object(), new DirectEventArgs(new Ext.Net.ParameterCollection()));

            Store1.RemoveAll();


        }

        private void ReloadData(object sender, DirectEventArgs e)
        {
            try
            {
                string rep_params = vals.Text;
                //ReportGenericRequest req = new ReportGenericRequest();
                TVListRequest req = new TVListRequest();
                req.paramString = rep_params;
                req.size = "5000";
                req.startAt = "0";
                req.sortBy = "date,employeeRef";

                ListResponse<DashBoardTimeVariation3> daysResponse = _timeAttendanceService.ChildGetAll<DashBoardTimeVariation3>(req);

                //ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                //ListResponse<ActiveAbsence> daysResponse = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
                if (!daysResponse.Success)
                {
                    Common.errorMessage(daysResponse);
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

                        x.apStatusString = statusList.Where(y => y.key == x.apStatus).Count() != 0 ? statusList.Where(y => y.key == x.apStatus).First().value : "";
                        x.damageLevelString = damageList.Where(y => y.key == x.damageLevel).Count() != 0 ? damageList.Where(y => y.key == x.damageLevel).First().value : "";
                        //x.arString = damageList.Where(y => y.key == x.arId).Count() != 0 ? damageList.Where(y => y.key == x.arId).First().value : "";
                        if (rtl)
                            x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("ar-AE"));
                        else
                            x.dayIdString = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en")).ToString("dddd  dd MMMM yyyy ", new System.Globalization.CultureInfo("en-US"));


                    }
                    );
                Store1.DataSource = daysResponse.Items;
                Store1.DataBind();
                //e.Total = daysResponse.count;
                format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }







    }
}