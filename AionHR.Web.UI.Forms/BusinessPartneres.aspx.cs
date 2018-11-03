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
using AionHR.Model.System;
using AionHR.Model.Employees.Profile;
using AionHR.Infrastructure.JSON;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;
using AionHR.Model.Attendance;
using AionHR.Model.AdminTemplates;

namespace AionHR.Web.UI.Forms
{
    public partial class BusinessPartneres : System.Web.UI.Page
    {
        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IAdministrationService _administrationService = ServiceLocator.Current.GetInstance<IAdministrationService>();
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
            


                if (_systemService.SessionHelper.CheckIfIsAdmin())
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(BusinessPartner), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                 
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

        private void FillNationality()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            naStore.DataSource = resp.Items;
            naStore.DataBind();

        }
        private void FillState()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<State> resp = _systemService.ChildGetAll<State>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            stStore.DataSource = resp.Items;
            stStore.DataBind();

        }

        protected void addST(object sender, DirectEventArgs e)
        {
            State dept = new State();
            dept.name = stId.Text;



            PostRequest<State> depReq = new PostRequest<State>();
            depReq.entity = dept;

            PostResponse<State> response = _systemService.ChildAddOrUpdate<State>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillState();
                stId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

        }
        protected void addNA(object sender, DirectEventArgs e)
        {
            Nationality dept = new Nationality();
            dept.name = naId.Text;



            PostRequest<Nationality> depReq = new PostRequest<Nationality>();
            depReq.entity = dept;

            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillNationality();
                naId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

        }
        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            FillBcId();
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<BusinessPartner> response = _administrationService.ChildGetRecord<BusinessPartner>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                

                   

                  
                    this.BasicInfoTab.SetValues(response.result);

                    //  timeZoneCombo.Select(response.result.timeZone.ToString());
                    FillNationality();
                    FillState();
                    naId.Select(response.result.address.countryId);
                    stId.Select(response.result.address.stateId);
                    address.Text = response.result.address.recordId;
                    addressForm.SetValues(response.result.address);
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
                default:
                    break;
            }


        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                BusinessPartner n = new BusinessPartner();
                n.recordId = index;
                n.name = "";
            
                PostRequest<BusinessPartner> req = new PostRequest<BusinessPartner>();
                req.entity = n;
                PostResponse<BusinessPartner> res = _administrationService.ChildDelete<BusinessPartner>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, res.Summary).Show();
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

            //Reset all values of the relative object
            BasicInfoTab.Reset();
       
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            FillBcId();
            FillNationality();
            FillState();
            //timeZoneCombo.Select(timeZoneOffset.Text);
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
            ListResponse<BusinessPartner> branches = _administrationService.ChildGetAll<BusinessPartner>(request);
            if (!branches.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + branches.LogId : branches.Summary).Show();
                return;
            }
            this.Store1.DataSource = branches.Items;
            e.Total = branches.count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            string addr = e.ExtraParams["address"];
            BusinessPartner b = JsonConvert.DeserializeObject<BusinessPartner>(obj);


            b.recordId = id;
            // Define the object to add or edit as null





            // Define the object to add or edit as null
            CustomResolver res = new CustomResolver();
            res.AddRule("naId", "countryId");
            res.AddRule("stId", "stateId");
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = res;

            AddressBook add = JsonConvert.DeserializeObject<AddressBook>(addr, settings);
            if (string.IsNullOrEmpty(add.city) && string.IsNullOrEmpty(add.countryId) && string.IsNullOrEmpty(add.street1) && string.IsNullOrEmpty(add.stateId) && string.IsNullOrEmpty(add.phone))
            {
                b.address = null;
            }
            else
            {
                if (string.IsNullOrEmpty(add.city) || string.IsNullOrEmpty(add.countryId) || string.IsNullOrEmpty(add.street1) || string.IsNullOrEmpty(add.stateId) || string.IsNullOrEmpty(add.phone))
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorAddressMissing")).Show();
                    return;
                }
                b.address = JsonConvert.DeserializeObject<AddressBook>(addr, settings);
                b.address.recordId = address.Text;


                if (string.IsNullOrEmpty(id))
                {

                    try
                    {
                        //New Mode
                        //Step 1 : Fill The object and insert in the store 
                        PostRequest<BusinessPartner> request = new PostRequest<BusinessPartner>();
                        request.entity = b;
                        PostResponse<BusinessPartner> r = _administrationService.ChildAddOrUpdate<BusinessPartner>(request);
                        b.recordId = r.recordId;

                        //check if the insert failed
                        if (!r.Success)//it maybe be another condition
                        {
                            //Show an error saving...
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + r.LogId : r.Summary).Show();
                            return;
                        }
                        else
                        {


                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordSavingSucc
                            });
                            //Add this record to the store 


                            //Display successful notification

                            Store1.Reload();
                            EditRecordWindow.Close();


                        }
                    }
                    catch (Exception ex)
                    {
                        //Error exception displaying a messsage box
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                    }


                }
                else
                {
                    //Update Mode

                    try
                    {

                        PostRequest<BusinessPartner> request = new PostRequest<BusinessPartner>();

                        request.entity = b;
                        PostResponse<BusinessPartner> r = _administrationService.ChildAddOrUpdate<BusinessPartner>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                        //Step 2 : saving to store

                        //Step 3 :  Check if request fails
                        if (!r.Success)//it maybe another check
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + r.LogId : r.Summary).Show();
                            return;
                        }
                        else
                        {


                            ModelProxy record = this.Store1.GetById(id);

                            BasicInfoTab.UpdateRecord(record);

                            record.Commit();
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordUpdatedSucc
                            });

                            //    this.EditRecordWindow.Close();

                        }

                    }
                    catch (Exception ex)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                    }
                }
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
       
       
     
     
      
     
       

       
     
       
        //protected void ADDNewlegalReferenceRecord(object sender, DirectEventArgs e)
        //{

        //    //Reset all values of the relative object
        //    legalReferenceForm.Reset();
        //    goNameTF.ReadOnly = false;



        //    this.EditlegalReferenceWindow.Title = Resources.Common.AddNewRecord;



        //    this.EditlegalReferenceWindow.Show();
        //}
       

      
        private void FillBcId()
        {
            ListRequest vsRequest = new ListRequest();
            ListResponse<BusinessPartnerCategory> resp = _administrationService.ChildGetAll<BusinessPartnerCategory>(vsRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            bcIdStore.DataSource = resp.Items;
            bcIdStore.DataBind();
        }



    }
}