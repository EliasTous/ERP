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
using AionHR.Model.Employees.Profile;
using System.Net;
using AionHR.Infrastructure.JSON;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class Contacts : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
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
                if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                CurrentEmployee.Text = Request.QueryString["employeeId"];
                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";

                Button7.Disabled = Button2.Disabled = btnAdd.Disabled = SaveEmergencyContactButton.Disabled = disabled;
                if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                    return;
                ApplySecurityContacts();

                ApplySecurityEmergencyContacts();
            }

        }

        private void ApplySecurityEmergencyContacts()
        {
            try
            {
                AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeeEmergencyContact), ecbasicInfo, emergencyContactsGrid, btnAdd, SaveEmergencyContactButton);
            }
            catch (AccessDeniedException exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                contactGrid.Hidden = true;

            }
            UserPropertiesPermissions req = new UserPropertiesPermissions();
            req.ClassId = (typeof(EmployeeEmergencyContact).GetCustomAttributes(typeof(ClassIdentifier), false).ToList()[0] as ClassIdentifier).ClassID;
            req.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ListResponse<UC> resp = _accessControlService.ChildGetAll<UC>(req);

            var att = resp.Items.Where(x =>

                x.propertyId == "3112107"
           );
            if(att.Count()>0)
            {
                switch(att.ToList()[0].accessLevel)
                {
                    case 0:
                        LimitEmergencyContactAddress(true); break;
                    case 1: LimitEmergencyContactAddress(false); break;
                    case 2:
                    default: break;

                }
            }
        }
        private void LimitContactAddress(bool isNoAccess)//true no access, false view only
        {
            //No Access
            cocity.Disabled = costId.Disabled = costreet1.Disabled = costreet2.Disabled = conaId.Disabled = copostalCode.Disabled = isNoAccess;
            cocity.InputType = costId.InputType = costreet1.InputType = costreet2.InputType = conaId.InputType = copostalCode.InputType = isNoAccess ? InputType.Password : InputType.Text;
            //View Only
            cocity.ReadOnly = costId.ReadOnly = costreet1.ReadOnly = costreet2.ReadOnly = conaId.ReadOnly = copostalCode.ReadOnly = !isNoAccess;
        }

        private void LimitEmergencyContactAddress(bool isNoAccess)//true no access, false view only
        {
            //No Access
            city.Disabled = ecstId.Disabled = street1.Disabled = street2.Disabled = ecnaId.Disabled = postalCode.Disabled = isNoAccess;
            city.InputType = ecstId.InputType = street1.InputType = street2.InputType = ecnaId.InputType = postalCode.InputType = isNoAccess ? InputType.Password : InputType.Text; ;
            //View Only
            city.ReadOnly = ecstId.ReadOnly = street1.ReadOnly = street2.ReadOnly = ecnaId.ReadOnly = postalCode.ReadOnly = !isNoAccess;

        }
        private void ApplySecurityContacts()
        {
            try
            {
                AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeeContact), ContactsForm, contactGrid, Button2, Button7);
            }
            catch (AccessDeniedException exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                contactGrid.Hidden = true;

            }
            UserPropertiesPermissions req = new UserPropertiesPermissions();
            req.ClassId = (typeof(EmployeeContact).GetCustomAttributes(typeof(ClassIdentifier), false).ToList()[0] as ClassIdentifier).ClassID;
            req.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ListResponse<UC> resp = _accessControlService.ChildGetAll<UC>(req);

            var att = resp.Items.Where(x =>

                x.propertyId == "3112202"
           );
            if (att.Count() > 0)
            {
                switch (att.ToList()[0].accessLevel)
                {
                    case 0:
                        LimitContactAddress(true); break;
                    case 1: LimitContactAddress(false); break;
                    case 2:
                    default: break;

                }
            }
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
            this.colAttach.Visible = false;
        }


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport11.RTL = true;

            }
        }

        private EmployeeContact GetCOById(string id)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = id.ToString();
            RecordResponse<EmployeeContact> response = _employeeService.ChildGetRecord<EmployeeContact>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return null;
            }
            return response.result;
        }
        private EmployeeEmergencyContact GetECById(string id)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = id.ToString();
            RecordResponse<EmployeeEmergencyContact> response = _employeeService.ChildGetRecord<EmployeeEmergencyContact>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return null;
            }
            return response.result;
        }
        private void FillRelationshipType()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<RelationshipType> resp = _employeeService.ChildGetAll<RelationshipType>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            rtStore.DataSource = resp.Items;
            rtStore.DataBind();

        }

        protected void PoPuPEC(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    EmployeeEmergencyContact entity = GetECById(id.ToString());
                    //Step 2 : call setvalues with the retrieved object
                    this.EmergencyContactsForm.SetValues(entity);
                    FillRelationshipType();
                    rtId.Select(entity.rtId.ToString());

                    FillECNationality();
                    ecnaId.Select(entity.addressId.countryId);
                    FillECState();
                    ecstId.Select(entity.addressId.stateId);
                    street1.Text = entity.addressId.street1;
                    street2.Text = entity.addressId.street2;
                    postalCode.Text = entity.addressId.postalCode;
                    city.Text = entity.addressId.city;
                    ecaddressId.Text = entity.addressId.recordId;


                    this.EditEmergencyContactWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditEmergencyContactWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteEC({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;

                default:
                    break;
            }


        }

        protected void PoPuPCO(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    EmployeeContact entity = GetCOById(id.ToString());
                    //Step 2 : call setvalues with the retrieved object
                    this.ContactsForm.SetValues(entity);
                    costreet1.Text = entity.addressId.street1;
                    costreet2.Text = entity.addressId.street2;
                    copostalCode.Text = entity.addressId.postalCode;
                    cocity.Text = entity.addressId.city;
                    coaddressId.Text = entity.addressId.recordId;
                    FillCOState();
                    costId.Select(entity.addressId.stateId);


                    FillCONationality();
                    conaId.Select(entity.addressId.countryId);


                    this.EditContactWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditContactWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteCO({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;

                default:
                    break;
            }


        }

        //[DirectMethod]
        //public void DeleteSkill(string index)
        //{
        //    try
        //    {
        //        //Step 1 Code to delete the object from the database 
        //        EmployeeContact n = new EmployeeContact();
        //        n.recordId = index;
        //        n.rtId = 0;
        //        n.employeeId = Convert.ToInt32(CurrentEmployee.Text);
        //        n.cellPhone = "";
        //        n.email = "";
        //        n.homePhone = "";
        //        n.workPhone = "";



        //        PostRequest<EmployeeContact> req = new PostRequest<EmployeeContact>();
        //        req.entity = n;
        //        PostResponse<EmployeeContact> res = _employeeService.ChildDelete<EmployeeContact>(req);
        //        if (!res.Success)
        //        {
        //            //Show an error saving...
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //            X.Msg.Alert(Resources.Common.Error, res.Summary).Show();
        //            return;
        //        }
        //        else
        //        {
        //            //Step 2 :  remove the object from the store
        //            contactStore.Remove(index);

        //            //Step 3 : Showing a notification for the user 
        //            Notification.Show(new NotificationConfig
        //            {
        //                Title = Resources.Common.Notification,
        //                Icon = Icon.Information,
        //                Html = Resources.Common.RecordDeletedSucc
        //            });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //In case of error, showing a message box to the user
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

        //    }

        //}


        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Direct method for removing multiple records
        /// </summary>


        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ADDNewCO(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            ContactsForm.Reset();
            this.EditContactWindow.Title = Resources.Common.AddNewRecord;

            FillCONationality();
            FillCOState();

            this.EditContactWindow.Show();
        }

        protected void ADDNewEC(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            EmergencyContactsForm.Reset();
            this.EditContactWindow.Title = Resources.Common.AddNewRecord;
            FillRelationshipType();
            FillECNationality();
            FillECState();

            this.EditEmergencyContactWindow.Show();
        }

        protected void contactStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeContactsListRequest request = new EmployeeContactsListRequest();
            request.EmployeeId = Convert.ToInt32(CurrentEmployee.Text);
            ListResponse<EmployeeContact> documents = _employeeService.ChildGetAll<EmployeeContact>(request);
            if (!documents.Success)
            {
                X.Msg.Alert(Resources.Common.Error, documents.Summary).Show();
                return;
            }
            this.contactStore.DataSource = documents.Items;
            e.Total = documents.count;

            this.contactStore.DataBind();
        }
        protected void emergencyContactsStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeContactsListRequest request = new EmployeeContactsListRequest();
            request.EmployeeId = Convert.ToInt32(CurrentEmployee.Text);
            ListResponse<EmployeeEmergencyContact> documents = _employeeService.ChildGetAll<EmployeeEmergencyContact>(request);
            if (!documents.Success)
            {
                X.Msg.Alert(Resources.Common.Error, documents.Summary).Show();
                return;
            }
            this.emergencyContactStore.DataSource = documents.Items;
            e.Total = documents.count;

            this.emergencyContactStore.DataBind();
        }


        protected void SaveCO(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            JsonSerializerSettings settings = new JsonSerializerSettings();
            CustomResolver res = new CustomResolver();
            res.AddRule("conaId", "naId");
            res.AddRule("costId", "stateId");
            res.AddRule("costreet1", "addressId.street1");
            settings.ContractResolver = res;
            EmployeeContact b = JsonConvert.DeserializeObject<EmployeeContact>(obj, settings);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;

            // Define the object to add or edit as null
            b.rtName = rtId.SelectedItem.Text;

            b.addressId = new AddressBook() { street1 = costreet1.Text, street2 = costreet2.Text, city = cocity.Text, postalCode = copostalCode.Text, countryId = b.naId, stateId = b.stateId, countryName = conaId.SelectedItem.Text };
            b.addressId.recordId = coaddressId.Text;
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    PostRequest<EmployeeContact> request = new PostRequest<EmployeeContact>();
                    request.entity = b;

                    PostResponse<EmployeeContact> r = _employeeService.ChildAddOrUpdate<EmployeeContact>(request);
                    b.recordId = r.recordId;


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                        return;
                    }
                    else
                    {

                        //Add this record to the store 
                        this.contactStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc,
                            HideDelay = 250
                        });

                        this.EditContactWindow.Close();
                        RowSelectionModel sm = this.contactGrid.GetSelectionModel() as RowSelectionModel;
                        sm.DeselectAll();
                        sm.Select(b.recordId.ToString());



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
                    int index = Convert.ToInt32(id);//getting the id of the record
                    PostRequest<EmployeeContact> request = new PostRequest<EmployeeContact>();
                    request.entity = b;
                    b.recordId = index.ToString();


                    PostResponse<EmployeeContact> r = _employeeService.ChildAddOrUpdate<EmployeeContact>(request);


                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                        return;
                    }
                    else
                    {


                        contactStore.Reload();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc,
                            HideDelay = 250
                        });
                        this.EditContactWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }

        protected void SaveEC(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            JsonSerializerSettings settings = new JsonSerializerSettings();
            CustomResolver res = new CustomResolver();
            res.AddRule("ecnaId", "naId");
            res.AddRule("ecstId", "stateId");
            settings.ContractResolver = res;
            EmployeeEmergencyContact b = JsonConvert.DeserializeObject<EmployeeEmergencyContact>(obj, settings);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            // Define the object to add or edit as null
            b.rtName = rtId.SelectedItem.Text;
            b.addressId = new AddressBook() { street1 = street1.Text, street2 = street2.Text, city = city.Text, postalCode = postalCode.Text, countryId = b.naId, countryName = ecnaId.SelectedItem.Text, stateId = b.stateId };
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.addressId.recordId = ecaddressId.Text;
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    PostRequest<EmployeeEmergencyContact> request = new PostRequest<EmployeeEmergencyContact>();
                    request.entity = b;

                    PostResponse<EmployeeEmergencyContact> r = _employeeService.ChildAddOrUpdate<EmployeeEmergencyContact>(request);
                    b.recordId = r.recordId;


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                        return;
                    }
                    else
                    {

                        //Add this record to the store 
                        this.emergencyContactStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc,
                            HideDelay = 250
                        });

                        this.EditEmergencyContactWindow.Close();
                        RowSelectionModel sm = this.emergencyContactsGrid.GetSelectionModel() as RowSelectionModel;
                        sm.DeselectAll();
                        sm.Select(b.recordId.ToString());



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
                    int index = Convert.ToInt32(id);//getting the id of the record
                    PostRequest<EmployeeEmergencyContact> request = new PostRequest<EmployeeEmergencyContact>();
                    request.entity = b;



                    PostResponse<EmployeeEmergencyContact> r = _employeeService.ChildAddOrUpdate<EmployeeEmergencyContact>(request);


                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                        return;
                    }
                    else
                    {


                        emergencyContactStore.Reload();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                            ,
                            HideDelay = 250

                        });
                        this.EditEmergencyContactWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
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


        protected void addRT(object sender, DirectEventArgs e)
        {
            RelationshipType dept = new RelationshipType();
            dept.name = rtId.Text;

            PostRequest<RelationshipType> depReq = new PostRequest<RelationshipType>();
            depReq.entity = dept;

            PostResponse<RelationshipType> response = _employeeService.ChildAddOrUpdate<RelationshipType>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillRelationshipType();
                rtId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        private void FillECNationality()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            ecnaStore.DataSource = resp.Items;
            ecnaStore.DataBind();

        }
        private void FillCONationality()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            conaStore.DataSource = resp.Items;
            conaStore.DataBind();

        }

        private void FillECState()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<State> resp = _systemService.ChildGetAll<State>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            ecstStore.DataSource = resp.Items;
            ecstStore.DataBind();

        }
        private void FillCOState()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<State> resp = _systemService.ChildGetAll<State>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            costStore.DataSource = resp.Items;
            costStore.DataBind();

        }

        protected void addECNA(object sender, DirectEventArgs e)
        {
            Nationality dept = new Nationality();
            dept.name = ecnaId.Text;



            PostRequest<Nationality> depReq = new PostRequest<Nationality>();
            depReq.entity = dept;

            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillECNationality();
                ecnaId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }


        protected void addCONA(object sender, DirectEventArgs e)
        {
            Nationality dept = new Nationality();
            dept.name = conaId.Text;



            PostRequest<Nationality> depReq = new PostRequest<Nationality>();
            depReq.entity = dept;

            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCONationality();
                conaId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        protected void addECST(object sender, DirectEventArgs e)
        {
            State dept = new State();
            dept.name = ecstId.Text;



            PostRequest<State> depReq = new PostRequest<State>();
            depReq.entity = dept;

            PostResponse<State> response = _systemService.ChildAddOrUpdate<State>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillECState();
                ecstId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }


        protected void addCOST(object sender, DirectEventArgs e)
        {
            State dept = new State();
            dept.name = costId.Text;



            PostRequest<State> depReq = new PostRequest<State>();
            depReq.entity = dept;

            PostResponse<State> response = _systemService.ChildAddOrUpdate<State>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCOState();
                costId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        [DirectMethod]
        public void DeleteCO(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeContact n = new EmployeeContact();
                n.recordId = index;

                PostRequest<EmployeeContact> req = new PostRequest<EmployeeContact>();
                req.entity = n;
                PostResponse<EmployeeContact> res = _employeeService.ChildDelete<EmployeeContact>(req);
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
                    contactStore.Remove(index);

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

        [DirectMethod]
        public void DeleteEC(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeEmergencyContact n = new EmployeeEmergencyContact();
                n.recordId = index;

                PostRequest<EmployeeEmergencyContact> req = new PostRequest<EmployeeEmergencyContact>();
                req.entity = n;
                PostResponse<EmployeeEmergencyContact> res = _employeeService.ChildDelete<EmployeeEmergencyContact>(req);
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
                    emergencyContactStore.Remove(index);

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