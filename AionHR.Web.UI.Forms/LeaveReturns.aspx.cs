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
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Model.LeaveManagement;
using AionHR.Services.Messaging.LeaveManagement;
using AionHR.Services.Messaging.Reports;

namespace AionHR.Web.UI.Forms
{
    public partial class LeaveReturns : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();

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
        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<EmployeeSnapShot> employees = Common.GetEmployeesFiltered(prms.Query);
            employees = employees.Where(x => x.activeStatus == Convert.ToInt16(ActiveStatus.ACTIVE)).ToList();
            return employees;

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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(CertificateLevel), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                date.Format = ColDate.Format = _systemService.SessionHelper.GetDateformat();
                returnTypeStore.DataSource = Common.XMLDictionaryList(_systemService, "41");
                returnTypeStore.DataBind();


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

      

        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["leaveId"];
            
            string employeeIdParam = e.ExtraParams["employeeId"];
            string returnTypeParam = e.ExtraParams["returnType"];
            string type = e.ExtraParams["type"];
            ApprovalsGridPanel.Disabled = false;
            switch (type)
            {
                case "imgEdit":


                    BasicInfoTab.Reset();

                    //LeaveRequestListRequest leaveReq = new LeaveRequestListRequest();



                    //leaveReq.BranchId = 0;
                    //leaveReq.DepartmentId = 0;
                    //leaveReq.raEmployeeId = 0;


                    //leaveReq.EmployeeId = Convert.ToInt32(employeeId.SelectedItem.Value);





                    //leaveReq.status = 2;



                    //leaveReq.Size = "50";
                    //leaveReq.StartAt = "0";
                    //leaveReq.SortBy = "recordId";

                    //leaveReq.Filter = "";
                    //ListResponse<LeaveRequest> leaveResp = _leaveManagementService.ChildGetAll<LeaveRequest>(leaveReq);
                   
                    //if (!leaveResp.Success)
                    //{
                    //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    //    Common.errorMessage(leaveResp);
                    //    return;
                    //}
                    //leaveIdStore.DataSource = leaveResp.Items;
                    //leaveIdStore.DataBind();

                    LeaveReturnRecordRequest r = new LeaveReturnRecordRequest();
                    r.leaveId = id; 
                    RecordResponse<LeaveReturn> response = _leaveManagementService.ChildGetRecord<LeaveReturn>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    employeeId.GetStore().Add(new object[]
                      {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName
                                }
                      });
                    employeeId.SetValue(response.result.employeeId);
                    employeeId.Select(response.result.employeeId);
                    fillLeavesStore();
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    FillApprovalsStore(id, employeeIdParam, returnTypeParam);

                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRecord({0},{1},{2})", id, employeeIdParam, returnTypeParam),
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
                default:
                    break;
            }


        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteRecord(string id,string employeeId,string returnType)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                LeaveReturn s = new LeaveReturn();
                s.leaveId = id;
                s.employeeId = employeeId;
                s.returnType = returnType;
                //s.reference = "";

                
                PostRequest<LeaveReturn> req = new PostRequest<LeaveReturn>();
                req.entity = s;
                PostResponse<LeaveReturn> r = _leaveManagementService.ChildDelete<LeaveReturn>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    Store1.Reload();

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

        private void FillApprovalsStore(string leaveId,string employeeId,string returnType)
        {
            
           



            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = "8|" + leaveId;



            ListResponse<AionHR.Model.LeaveManagement.LeaveReturnApproval> response = _leaveManagementService.ChildGetAll<AionHR.Model.LeaveManagement.LeaveReturnApproval>(req);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return;
            }
            List<XMLDictionary> statusList = Common.XMLDictionaryList(_systemService, "13");
            response.Items.ForEach(x =>
            {
                x.stringStatus = statusList.Where(y => y.key == x.status).Count() != 0 ? statusList.Where(y => y.key == x.status).First().value : string.Empty;
            }
            );
            ApprovalsStore.DataSource = response.Items;
            ApprovalsStore.DataBind();

        }



        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Direct method for removing multiple records
        /// </summary>

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            BasicInfoTab.Reset();
            panelRecordDetails.ActiveIndex = 0;
            ApprovalsGridPanel.Disabled = true;
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<LeaveReturn> resp = _leaveManagementService.ChildGetAll<LeaveReturn>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            List<XMLDictionary> statusList = Common.XMLDictionaryList(_systemService, "13");
            resp.Items.ForEach(x => x.apStatus = statusList.Where(y => y.key.ToString() == x.apStatus).Count() != 0 ? statusList.Where(y => y.key.ToString() == x.apStatus).First().value : "");
            this.Store1.DataSource = resp.Items;
                e.Total = resp.Items.Count; ;

                this.Store1.DataBind();
            
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

           
                 string dateParam = e.ExtraParams["date"];
            string obj = e.ExtraParams["values"];
            LeaveReturn b = JsonConvert.DeserializeObject<LeaveReturn>(obj);
            b.apStatus = "1";
            DateTime temp = new DateTime();
            if (DateTime.TryParse(dateParam, out temp))
                b.date = temp;


            PostRequest<LeaveReturn> request = new PostRequest<LeaveReturn>();

            request.entity = b;
            PostResponse<LeaveReturn> r = _leaveManagementService.ChildAddOrUpdate<LeaveReturn>(request);


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

                //Add this record to the store 
                Store1.Reload();

                //Display successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });

                this.EditRecordWindow.Close();

                //if (string.IsNullOrEmpty(id))
                //{

                //    try
                //    {
                //        //New Mode
                //        //Step 1 : Fill The object and insert in the store 
                //        PostRequest<CertificateLevel> request = new PostRequest<CertificateLevel>();

                //        request.entity = b;
                //        PostResponse<CertificateLevel> r = _employeeService.ChildAddOrUpdate<CertificateLevel>(request);


                //        //check if the insert failed
                //        if (!r.Success)//it maybe be another condition
                //        {
                //            //Show an error saving...
                //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //            Common.errorMessage(r);
                //            return;
                //        }
                //        else
                //        {
                //            b.recordId = r.recordId;
                //            //Add this record to the store 
                //            this.Store1.Insert(0, b);

                //            //Display successful notification
                //            Notification.Show(new NotificationConfig
                //            {
                //                Title = Resources.Common.Notification,
                //                Icon = Icon.Information,
                //                Html = Resources.Common.RecordSavingSucc
                //            });

                //            this.EditRecordWindow.Close();
                //            RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                //            sm.DeselectAll();
                //            sm.Select(b.recordId.ToString());



                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        //Error exception displaying a messsage box
                //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                //    }


                //}
                //else
                //{
                //    //Update Mode

                //    try
                //    {
                //        //getting the id of the record
                //        PostRequest<CertificateLevel> request = new PostRequest<CertificateLevel>();
                //        request.entity = b;
                //        PostResponse<CertificateLevel> r = _employeeService.ChildAddOrUpdate<CertificateLevel>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                //        //Step 2 : saving to store

                //        //Step 3 :  Check if request fails
                //        if (!r.Success)//it maybe another check
                //        {
                //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //            Common.errorMessage(r);
                //            return;
                //        }
                //        else
                //        {


                //            ModelProxy record = this.Store1.GetById(id);
                //            BasicInfoTab.UpdateRecord(record);
                //            record.Commit();
                //            Notification.Show(new NotificationConfig
                //            {
                //                Title = Resources.Common.Notification,
                //                Icon = Icon.Information,
                //                Html = Resources.Common.RecordUpdatedSucc
                //            });
                //            this.EditRecordWindow.Close();


                //        }

                //    }
                //    catch (Exception ex)
                //    {
                //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                //    }
                //}
            }
        }
        
       [DirectMethod]
        protected void changeDateField(object sender, DirectEventArgs e)
        {
            string type = returnType.SelectedItem.Value;

            RecordRequest req = new RecordRequest();
            req.RecordID = leaveId.SelectedItem.Value;         

            RecordResponse<LeaveRequest> resp = _leaveManagementService.ChildGetRecord<LeaveRequest>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            if (resp.result == null)
                return;
            date.Disabled = false;
            date.MinDate = new DateTime(1980, 01, 1);
            date.MaxDate = new DateTime(2050, 01, 01);
            date.Value = DateTime.Now;

            switch (type)
            {
                case "1":
                    date.Value = resp.result.startDate;
                    date.Disabled = true;
                    break;
                case "2":date.MinDate= resp.result.startDate;
                    date.MaxDate= resp.result.endDate;
                    break;
                case "3":
                    date.Value = resp.result.endDate.AddDays(1);
                    date.Disabled = true;
                    break;
                case "4": date.MinDate = resp.result.endDate;
                    break;

            }

           
        }
        [DirectMethod]
        protected void fillLeaves(object sender, DirectEventArgs e)
        {
            

            LeaveRequestListRequest req = new LeaveRequestListRequest();


          
            req.BranchId =0;
            req.DepartmentId = 0;
            req.raEmployeeId = 0;

          
           req.EmployeeId = Convert.ToInt32(employeeId.SelectedItem.Value);





            req.status = 2;
                            
         

            req.Size = "50";
            req.StartAt = "0";
            req.SortBy = "recordId";

            req.Filter = "";
            ListResponse<LeaveRequest> resp = _leaveManagementService.ChildGetAll<LeaveRequest>(req);


           


            //check if the insert failed
            if (!resp.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            else
            {

                resp.Items.ForEach(x =>
                {
                    x.leaveRef = x.leaveRef + " " + x.startDate.ToString(_systemService.SessionHelper.GetDateformat()) + " " + x.endDate.ToString(_systemService.SessionHelper.GetDateformat());
                });
            }
            leaveIdStore.DataSource = resp.Items.Where(x => x.endDate > DateTime.Today).ToList();
            leaveIdStore.DataBind();
        }
       
        protected void fillLeavesStore()
        {


            LeaveRequestListRequest req = new LeaveRequestListRequest();



            req.BranchId = 0;
            req.DepartmentId = 0;
            req.raEmployeeId = 0;


            req.EmployeeId = Convert.ToInt32(employeeId.SelectedItem.Value);





            req.status = 2;



            req.Size = "50";
            req.StartAt = "0";
            req.SortBy = "recordId";

            req.Filter = "";
            ListResponse<LeaveRequest> resp = _leaveManagementService.ChildGetAll<LeaveRequest>(req);





            //check if the insert failed
            if (!resp.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            else
            {

                resp.Items.ForEach(x =>
                {
                    x.leaveRef = x.leaveRef + " " + x.startDate.ToString(_systemService.SessionHelper.GetDateformat()) + " " + x.endDate.ToString(_systemService.SessionHelper.GetDateformat());
                });
            }
            leaveIdStore.DataSource = resp.Items;
            leaveIdStore.DataBind();
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

        protected void Unnamed_Event()
        {

        }

        protected void Unnamed_Event1()
        {

        }
    }
}