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
using Model.System;
using Model.Attendance;
using Model.Employees.Leaves;
using Model.Employees.Profile;
using Model.LeaveManagement;
using Services.Messaging.System;
using Model.TimeAttendance;
using Infrastructure.JSON;

namespace Web.UI.Forms
{
    public partial class LeaveRequests : System.Web.UI.Page
    {
        [DirectMethod]
        public object FillReplacementEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<EmployeeSnapShot> employees=   Common.GetEmployeesFiltered(prms.Query);
            employees = employees.Where(x => x.activeStatus == Convert.ToInt16(ActiveStatus.ACTIVE)).ToList();


            return employees; 

        }
       
       

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<EmployeeSnapShot> employees = Common.GetEmployeesFiltered(prms.Query);
            employees = employees.Where(x => x.activeStatus == Convert.ToInt16(ActiveStatus.ACTIVE)).ToList();
            return employees;

        }


        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }


       

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
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
                HideShowColumns();
        
               
               
              Column2.Format = Column1.Format = _systemService.SessionHelper.GetDateformat();
                try
                {
                   // AccessControlApplier.ApplyAccessControlOnPage(typeof(LeaveRequest), null, GridPanel1, btnAdd, null);

                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
               
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            
            leaveRequest1.Store1 = this.Store1;
            leaveRequest1.GrigPanel1 = this.GridPanel1;
            
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


       
        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    leaveRequest1.Update(id);
                   
                   
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

                case "imgHistory":

                    TimeVariationHistoryControl1.Show("42201", id);
                    break;
                default:
                    break;
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
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            leaveRequest1.Add();
            //Reset all values of the relative object
        
        }

        private LeaveRequestListRequest GetFilteredRequest()
        {
            LeaveRequestListRequest req = new LeaveRequestListRequest();


            //var d = jobInfo1.GetJobInfo();
            //req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            //req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
            //req.raEmployeeId = 0;
          
            //if (!string.IsNullOrEmpty(employeeFilter.Text) && employeeFilter.Value.ToString() != "0")
            //{
            //    req.EmployeeId = Convert.ToInt32(employeeFilter.Value);


            //}
            //else
            //{
            //    req.EmployeeId = 0;

            //}

            //if (!string.IsNullOrEmpty(LeveApprovalStatusFilter.GetApprovalStatus()))
            //{
            //    req.status = Convert.ToInt32(LeveApprovalStatusFilter.GetApprovalStatus());


            //}
            //else
            //{
            //    req.status = 0;

            //}



            return req;
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
          List<XMLDictionary> statusList=  Common.XMLDictionaryList(_systemService, "13");

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            LeaveRequestListRequest request = GetFilteredRequest();

            request.Size = "50";
            request.StartAt = e.Start.ToString();
            request.SortBy = "recordId";
            request.Params = vals.Text;

            //request.Filter = "";
            ListResponse<LeaveRequest> routers = _leaveManagementService.ChildGetAll<LeaveRequest>(request);

           if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            routers.Items.ForEach(x =>
            {
                x.statusString = statusList.Where(y => y.key == x.apStatus).Count() != 0 ? statusList.Where(y => y.key == x.apStatus).First().value : "";

            }

               );

            this.Store1.DataSource = routers.Items;
            e.Total = routers.count ;

            this.Store1.DataBind();
        }


        protected void ReturnLeave(object sender, DirectEventArgs e)
        {
            leaveRequest1.Return();
        }

        private List<LeaveDay> GenerateLeaveDays(string encoded)
        {
            List<LeaveDay> days = JsonConvert.DeserializeObject<List<LeaveDay>>(encoded);

            return days;
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

     

       



        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                LeaveRequest s = new LeaveRequest();
                s.recordId = index;
                s.destination = "";
                s.employeeId = "0";
                s.endDate = DateTime.Now;
                s.startDate = DateTime.Now;
                s.apStatus = 0;
                s.isPaid = false;
                s.justification = "";
                s.ltId = 0;

                PostRequest<LeaveRequest> req = new PostRequest<LeaveRequest>();
                req.entity = s;
                PostResponse<LeaveRequest> r = _leaveManagementService.ChildDelete<LeaveRequest>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                     Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    Store1.Remove(index);

                    //Step 3 : Showing a notification for the user 
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordDeletedSucc
                    });
                }

            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }


    }
}