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
    public partial class Dependants : System.Web.UI.Page
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
               
                if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                CurrentEmployee.Text = Request.QueryString["employeeId"];

                birthDate.Format = birthDateCol.Format = _systemService.SessionHelper.GetDateformat();
                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";
                Button2.Disabled = Button7.Disabled = disabled;
                if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Dependant), infoForm, dependandtsGrid, Button2, Button7);
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Dependant), addressForm, dependandtsGrid, Button2, Button7);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport11.Hidden = true;
                    return;
                }
                ApplyAccessControlOnAddress();
            }

        }
        private void ApplyAccessControlOnAddress()
        {
           
            var properties = AccessControlApplier.GetPropertiesLevels(typeof(Dependant));
            var att = properties.Where(x =>

                x.propertyId == "3115010"
           );
            int level = 0;
            if (att.Count() == 0 || att.ToList()[0].accessLevel == 2)
                return;
            level = att.ToList()[0].accessLevel;
            switch (level)
            {
                case 0:
                    addressForm.Items.ForEach(x =>
                    {
                        (x as Field).InputType = InputType.Password;
                        (x as Field).ReadOnly = true;
                    });
                    break;
                case 1:
                    addressForm.Items.ForEach(x =>
                    {

                        (x as Field).ReadOnly = true;
                    }); break;
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
      


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport11.RTL = true;

            }
        }

        private Dependant GetDEById(string id)
        {
            GetDependantRequest r = new GetDependantRequest();
            r.SeqNo = id.ToString();
            r.EmployeeId = CurrentEmployee.Text;
            RecordResponse<Dependant> response = _employeeService.ChildGetRecord<Dependant>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return null;
            }
            return response.result;
        }


       

        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    Dependant entity = GetDEById(id.ToString());
                    //Step 2 : call setvalues with the retrieved object
                    this.infoForm.SetValues(entity);
                    street1.Text = entity.address.street1;
                    street2.Text = entity.address.street2;
                    postalCode.Text = entity.address.postalCode;
                    city.Text = entity.address.city;
                    address.Text = entity.address.recordId;
                    FillState();
                   stateId.Select(entity.address.stateId);
                    phone.Text = entity.address.phone;

                    if (entity.gender == "0")
                        gender0.Checked = true;
                    else
                        gender1.Checked = true;
                    FillNationality();
                    countryId.Select(entity.address.countryId);
                    dependencyType.Select(entity.dependencyType);
                    recordId.Text = id.ToString();
                    this.EditContactWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditContactWindow.Show();
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

                default:
                    break;
            }


        }

        

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
        protected void ADDNew(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            infoForm.Reset();
            this.EditContactWindow.Title = Resources.Common.AddNewRecord;

            FillNationality();
            FillState();
       
            this.EditContactWindow.Show();
        }

      

        protected void dependandtsStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeContactsListRequest request = new EmployeeContactsListRequest();
            request.EmployeeId = Convert.ToInt32(CurrentEmployee.Text);
            ListResponse<Dependant> documents = _employeeService.ChildGetAll<Dependant>(request);
            if (!documents.Success)
            {
                X.Msg.Alert(Resources.Common.Error, documents.Summary).Show();
                return;
            }
           foreach (Dependant d in documents.Items)
            {
                switch (d.dependencyType)
                {
                    case "1": d.dependencyType = GetLocalResourceObject("Spouse").ToString(); 
                        break;
                    case "2":d.dependencyType = GetLocalResourceObject("Child").ToString();
                        break;
                    case "3":d.dependencyType = GetLocalResourceObject("DomesticPartner").ToString();
                        break;
                    case "4":d.dependencyType = GetLocalResourceObject("StepChild").ToString();
                        break;
                    case "5":d.dependencyType = GetLocalResourceObject("FosterChild").ToString();
                        break;
                        
                }
            }
            this.dependandtsStore.DataSource = documents.Items;
            e.Total = documents.count;

            this.dependandtsStore.DataBind();

        }
      

        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["info"];
            string address = e.ExtraParams["address"];
            string addressId = e.ExtraParams["addressId"];





            Dependant b = JsonConvert.DeserializeObject<Dependant>(obj);
            b.employeeId = CurrentEmployee.Text;
            b.seqNo = id;
            AddressBook add = JsonConvert.DeserializeObject<AddressBook>(address);
            // Define the object to add or edit as null
            if (!b.isCitizen.HasValue)
                b.isCitizen = false;
            if (!b.isStudent.HasValue)
                b.isStudent = false;
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
                b.address = JsonConvert.DeserializeObject<AddressBook>(address);
                b.address.recordId = addressId;
            }
            
          
            
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    PostRequest<Dependant> request = new PostRequest<Dependant>();
                    request.entity = b;

                    PostResponse<Dependant> r = _employeeService.ChildAddOrUpdate<Dependant>(request);
                    b.seqNo = r.recordId;


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {

                        //Add this record to the store 
                        //this.dependandtsStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc,
                            HideDelay = 250
                        });

                        this.EditContactWindow.Close();
                        //RowSelectionModel sm = this.dependandtsGrid.GetSelectionModel() as RowSelectionModel;
                        //sm.DeselectAll();
                        //sm.Select(b.seqNo.ToString());
                        dependandtsStore.Reload();



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
                    PostRequest<Dependant> request = new PostRequest<Dependant>();
                    request.entity = b;
                    b.seqNo = index.ToString();


                    PostResponse<Dependant> r = _employeeService.ChildAddOrUpdate<Dependant>(request);


                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {


                        dependandtsStore.Reload();
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
   [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }


      

  
        private void FillNationality()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+resp.LogId : resp.Summary).Show();
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+resp.LogId : resp.Summary).Show();
                return;
            }
            stStore.DataSource = resp.Items;
            stStore.DataBind();

        }

      


        protected void addNA(object sender, DirectEventArgs e)
        {
            Nationality dept = new Nationality();
            dept.name = countryId.Text;



            PostRequest<Nationality> depReq = new PostRequest<Nationality>();
            depReq.entity = dept;

            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillNationality();
                countryId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

        }

      

        protected void addST(object sender, DirectEventArgs e)
        {
            State dept = new State();
            dept.name = stateId.Text;



            PostRequest<State> depReq = new PostRequest<State>();
            depReq.entity = dept;

            PostResponse<State> response = _systemService.ChildAddOrUpdate<State>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillState();
                stateId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                return;
            }

        }

        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Dependant n = new Dependant();
                n.seqNo = index;
                n.employeeId= Request.QueryString["employeeId"];

                PostRequest<Dependant> req = new PostRequest<Dependant>();
                req.entity = n;
                PostResponse<Dependant> res = _employeeService.ChildDelete<Dependant>(req);
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
                    dependandtsStore.Remove(index);

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