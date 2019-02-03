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
using AionHR.Services.Messaging.Reports;
using AionHR.Model.Dashboard;
using AionHR.Services.Messaging.TimeAttendance;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms
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
                dateRange1.DefaultStartDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                FillStatus();
               
             





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

        private TimeVariationListRequest GetAbsentRequest()
        {
          
            TimeVariationListRequest reqTV = new TimeVariationListRequest();
            reqTV.BranchId = jobInfo1.GetJobInfo().BranchId==null ? reqTV.BranchId=0 : jobInfo1.GetJobInfo().BranchId; 
            reqTV.DepartmentId= jobInfo1.GetJobInfo().DepartmentId==null ? reqTV.DepartmentId=0: jobInfo1.GetJobInfo().DepartmentId;
            reqTV.DivisionId= jobInfo1.GetJobInfo().DivisionId==null?reqTV.DivisionId=0 :jobInfo1.GetJobInfo().DivisionId;
             reqTV.PositionId = jobInfo1.GetJobInfo().PositionId == null ? reqTV.PositionId = 0 : jobInfo1.GetJobInfo().PositionId;
            reqTV.EsId =string.IsNullOrEmpty(esId.Value.ToString()) ? reqTV.EsId=0 :Convert.ToInt32( esId.Value);
            reqTV.fromDayId = dateRange1.GetRange().DateFrom;
            reqTV.toDayId = dateRange1.GetRange().DateTo;
            reqTV.employeeId = employeeCombo1.GetEmployee().employeeId.ToString();
            if (string.IsNullOrEmpty(apStatus.Value.ToString()))
            {

                reqTV.apStatus = "0";
            }
            else
                reqTV.apStatus = apStatus.Value.ToString();
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
            reqTV.timeCode = timeVariationType.GetTimeCode();

            //if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
            //{
            //    timeVariationType.Select(ConstTimeVariationType.LATE_CHECKIN.ToString());
            //    reqTV.timeCode = ConstTimeVariationType.LATE_CHECKIN.ToString();
            //}
            //else
            //reqTV.timeCode = timeVariationType.Value.ToString();

          
         
            return reqTV;
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                //GEtting the filter from the page

                TimeVariationListRequest req = GetAbsentRequest();

                ListResponse<DashBoardTimeVariation> daysResponse = _timeAttendanceService.ChildGetAll<DashBoardTimeVariation>(req);

                //ActiveAttendanceRequest r = GetActiveAttendanceRequest();

                //ListResponse<ActiveAbsence> daysResponse = _timeAttendanceService.ChildGetAll<ActiveAbsence>(r);
                if (!daysResponse.Success)
                {
                   Common.errorMessage( daysResponse);
                    return;
                }
                bool rtl = _systemService.SessionHelper.CheckIfArabicSession(); 
                daysResponse.Items.ForEach(
                    x =>
                    {
                        x.clockDurationString = time(x.clockDuration, true);
                        x.durationString = time(x.duration, true);
                        x.timeCodeString = ConstTimeVariationType.FillTimeCode(x.timeCode,_systemService);
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



            List<Employee> data = GetEmployeesFiltered(prms.Query);

            data.ForEach(s => s.fullName = s.name.fullName);
            //  return new
            // {
            return data;
            //};

        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 0;
            req.SortBy = "firstName";

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;


          

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            if (!response.Success)
                Common.errorMessage(response);
            return response.Items;
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
                string apStatus = e.ExtraParams["apStatus"];
                string type = e.ExtraParams["type"];


                switch (type)
                {
                    case "imgEdit":
                        //Step 1 : get the object from the Web Service 

                        //Step 2 : call setvalues with the retrieved object

                        damage.Select(damageLavel);
                        duration.Text = durationValue;
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

                    case "imgAttach":

                        //Here will show up a winow relatice to attachement depending on the case we are working on
                        break;

                    case "LinkRender":
                        FillTimeApproval(dayId, employeeId,timeCode,shiftId,apStatus);
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
                TimeVariationListRequest req = GetAbsentRequest();

                ListResponse<DashBoardTimeVariation> r = _timeAttendanceService.ChildGetAll<DashBoardTimeVariation>(req);                    //Step 1 Selecting the object or building up the object for update purpose

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
                        PostResponse<DashBoardTimeVariation> response = _timeAttendanceService.ChildAddOrUpdate<DashBoardTimeVariation>(request);
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
        protected void FillTimeApproval(int dayId, int employeeId,string timeCode, string shiftId , string apstatus)
        {
            try
            {
                DashboardTimeListRequest r = new DashboardTimeListRequest();
             r.fromDayId= dayId.ToString();
                r.toDayId= dayId.ToString();
                r.employeeId = employeeId;
                r.approverId = 0;
                r.timeCode = timeCode;
                r.shiftId = shiftId;
                // r.apStatus = apstatus.ToString();
                r.apStatus = "0";
                r.DepartmentId = 0;
                r.DivisionId = 0;
                r.BranchId = 0;
                r.PositionId = 0;
                r.EsId = 0;
                r.StartAt = "0";
                r.Size = "1000";
                

                ListResponse<Time> Times = _timeAttendanceService.ChildGetAll<Time>(r);
                if (!Times.Success)
                {
                    Common.errorMessage(Times);
                    return;
                }
                Times.Items.ForEach(x =>
                {
                    x.timeCodeString = ConstTimeVariationType.FillTimeCode(Convert.ToInt32(x.timeCode),_systemService);

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
