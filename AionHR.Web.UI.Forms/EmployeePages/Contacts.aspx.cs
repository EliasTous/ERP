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

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class Contacts : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
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

        private EmployeeContact GetById(string id)
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

        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            string path = e.ExtraParams["path"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    EmployeeContact entity = GetById(id.ToString());
                    //Step 2 : call setvalues with the retrieved object
                    this.ContactsForm.SetValues(entity);
                    FillRelationshipType();
                    rtId.Select(entity.rtId.ToString());

                    FillNationality();
                    naId.Select(entity.addressId.countryId.ToString());


                    this.EditContactWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditContactWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteSkill({0})", id),
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
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            ContactsForm.Reset();
            this.EditContactWindow.Title = Resources.Common.AddNewRecord;
            FillRelationshipType();
            FillNationality();     

            this.EditContactWindow.Show();
        }

        protected void contactsStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeDocumentsListRequest request = new EmployeeDocumentsListRequest();
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


        protected void SaveDocument(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            EmployeeContact b = JsonConvert.DeserializeObject<EmployeeContact>(obj);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            // Define the object to add or edit as null
            b.rtName = rtId.SelectedItem.Text;
            b.addressId = new AddressBook() { street1 = street1.Text, street2 = street2.Text, city = city.Text, postalCode = postalCode.Text, countryId = b.naId };
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
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditContactWindow.Close();
                        RowSelectionModel sm = this.contactsGrid.GetSelectionModel() as RowSelectionModel;
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


                        ModelProxy record = this.contactStore.GetById(index);

                        ContactsForm.UpdateRecord(record);
                        record.Set("rtName", b.rtName);

                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
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

        private void FillNationality()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            naStore.DataSource = resp.Items;
            naStore.DataBind();

        }

        protected void addNA(object sender, DirectEventArgs e)
        {
            Nationality dept = new Nationality();
            dept.name = naId.Text;

            

            PostRequest<Nationality> depReq = new PostRequest<Nationality>();
            depReq.entity = dept;

            PostResponse<Nationality> response = _employeeService.ChildAddOrUpdate<Nationality>(depReq);
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



















    }
}