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

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class Documents : System.Web.UI.Page
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
                this.Viewport11.RTL = true;

            }
        }



        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<EmployeeDocument> response = _employeeService.ChildGetRecord<EmployeeDocument>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.EditDocumentForm.SetValues(response.result);
                    FillDocumentTypes();
                    dtId.Select(response.result.dtId.ToString());

                    this.EditDocumentWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditDocumentWindow.Show();
                    break;
             
                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteDocument({0})", id),
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
                default:
                    break;
            }


        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteDocument(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeDocument n = new EmployeeDocument();
                n.recordId = index;
                n.dtId = 0;
                n.employeeId = Convert.ToInt32(CurrentEmployee.Text);
                n.expiryDate = null;
                n.remarks = "";
                n.documentRef = "";


                PostRequest<EmployeeDocument> req = new PostRequest<EmployeeDocument>();
                req.entity = n;
                PostResponse<EmployeeDocument> res = _employeeService.ChildDelete<EmployeeDocument>(req);
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
                    employeeDocumentsStore.Remove(index);

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
            EditDocumentForm.Reset();
            this.EditDocumentWindow.Title = Resources.Common.AddNewRecord;
            FillDocumentTypes();
           
            this.EditDocumentWindow.Show();
        }
   

        protected void employeeDocumentsStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeDocumentsListRequest request = new EmployeeDocumentsListRequest();
            request.EmployeeId = Convert.ToInt32(CurrentEmployee.Text);
            ListResponse<EmployeeDocument> documents = _employeeService.ChildGetAll<EmployeeDocument>(request);
            if (!documents.Success)
            {
                X.Msg.Alert(Resources.Common.Error, documents.Summary).Show();
                return;
            }
            this.employeeDocumentsStore.DataSource = documents.Items;
            e.Total = documents.count;

            this.employeeDocumentsStore.DataBind();
        }
  


        protected void SaveDocument(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            EmployeeDocument b = JsonConvert.DeserializeObject<EmployeeDocument>(obj);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            // Define the object to add or edit as null
            b.dtName = dtId.SelectedItem.Text;
            b.expiryDate = new DateTime(b.expiryDate.Value.Year, b.expiryDate.Value.Month, b.expiryDate.Value.Day, 14, 0, 0);

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    EmployeeDocumentAddOrUpdateRequest request = new EmployeeDocumentAddOrUpdateRequest();

                    byte[] fileData = null;
                    if (documentFile.PostedFile != null && documentFile.PostedFile.ContentLength > 0)
                    {
                        //using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                        //{
                        //    fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                        //}
                        fileData = new byte[documentFile.PostedFile.ContentLength];
                        fileData = documentFile.FileBytes;
                        request.fileName = documentFile.PostedFile.FileName;
                        request.fileData = fileData;

                    }
                    else
                    {
                        request.fileData = fileData;
                        request.fileName = "";
                    }
                    request.documentData = b;

                    PostResponse<EmployeeDocument> r = _employeeService.AddOrUpdateEmployeeDocument(request);
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
                        this.employeeDocumentsStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditDocumentWindow.Close();
                        RowSelectionModel sm = this.employeeDocumentsGrid.GetSelectionModel() as RowSelectionModel;
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
                    EmployeeDocumentAddOrUpdateRequest request = new EmployeeDocumentAddOrUpdateRequest();

                    byte[] fileData = null;
                    if (documentFile.HasFile && documentFile.PostedFile.ContentLength > 0)
                    {
                        //using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                        // {
                        //    fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                        // }
                        fileData = new byte[documentFile.PostedFile.ContentLength];
                        fileData = documentFile.FileBytes;
                        request.fileName = documentFile.PostedFile.FileName;
                        request.fileData = fileData;



                    }
                    else
                    {
                        request.fileData = fileData;
                        request.fileName = "";
                    }
                    request.documentData = b;



                    PostResponse<EmployeeDocument> r = _employeeService.AddOrUpdateEmployeeDocument(request);


                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.employeeDocumentsStore.GetById(index);
                        
                        record.Set("statusName", b.dtName);
                        EditDocumentForm.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditDocumentWindow.Close();


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

        private void FillDocumentTypes()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<DocumentType> resp = _employeeService.ChildGetAll<DocumentType>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            dtStore.DataSource = resp.Items;
            dtStore.DataBind();

        }

        protected void addType(object sender, DirectEventArgs e)
        {
            DocumentType dept = new DocumentType();
            dept.name = dtId.Text;

            PostRequest<DocumentType> depReq = new PostRequest<DocumentType>();
            depReq.entity = dept;

            PostResponse<DocumentType> response = _employeeService.ChildAddOrUpdate<DocumentType>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillDocumentTypes();
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