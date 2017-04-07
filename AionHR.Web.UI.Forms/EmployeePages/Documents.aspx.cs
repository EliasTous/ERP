
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
using AionHR.Infrastructure.Domain;

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
                CurrentDateFormat.Text = _systemService.SessionHelper.GetDateformat();
                EmployeeClassId.Text = ClassId.EPDO.ToString();
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

                CurrentLanguage.Text = "ar";
            }
            else
            {
                CurrentLanguage.Text = "en";
            }
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
                    EmployeeDocument entity = GetById(id.ToString());
                    //Step 2 : call setvalues with the retrieved object
                    this.EditDocumentForm.SetValues(entity);
                    FillDocumentTypes();
                    dtId.Select(entity.dtId.ToString());

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

                case "imgAttach":
                    DownloadFile(path);

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }
        [DirectMethod]
        public void DownloadFile(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;

            Stream stream = null;

            //This controls how many bytes to read at a time and send to the client
            int bytesToRead = 10000;

            // Buffer to read bytes in chunk size specified above
            byte[] buffer = new Byte[bytesToRead];

            // The number of bytes read
            try
            {
                //Create a WebRequest to get the file
                HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(url);

                //Create a response for this request
                HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

                if (fileReq.ContentLength > 0)
                    fileResp.ContentLength = fileReq.ContentLength;

                //Get the Stream returned from the response
                stream = fileResp.GetResponseStream();

                // prepare the response to the client. resp is the client Response
                var resp = HttpContext.Current.Response;

                //Indicate the type of data being sent
                resp.ContentType = "application/octet-stream";
                string[] segments = url.Split('/');
                string fileName = segments[segments.Length - 1];
                //Name the file 
                resp.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                resp.AddHeader("Content-Length", fileResp.ContentLength.ToString());

                int length;
                do
                {
                    // Verify that the client is connected.
                    if (resp.IsClientConnected)
                    {
                        // Read data into the buffer.
                        length = stream.Read(buffer, 0, bytesToRead);

                        // and write it out to the response's output stream
                        resp.OutputStream.Write(buffer, 0, length);

                        // Flush the data


                        //Clear the buffer
                        buffer = new Byte[bytesToRead];
                    }
                    else
                    {
                        // cancel the download if client has disconnected
                        length = -1;
                    }
                } while (length > 0); //Repeat until no data is read
            }
            finally
            {
                if (stream != null)
                {
                    //Close the input stream
                    stream.Close();
                }
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
                Attachement n = new Attachement();
                n.classId = ClassId.EPDO;
                n.recordId = Convert.ToInt32(CurrentEmployee.Text);
                n.seqNo = Convert.ToInt16(index);


                PostRequest<Attachement> req = new PostRequest<Attachement>();
                req.entity = n;
                PostResponse<Attachement> res = _systemService.ChildDelete<Attachement>(req);
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
            //EditDocumentForm.Reset();
            //this.EditDocumentWindow.Title = Resources.Common.AddNewRecord;
            //FillDocumentTypes();

            //this.EditDocumentWindow.Show();

            ListRequest req = new ListRequest();
            ListResponse<SystemFolder> docs = _systemService.ChildGetAll<SystemFolder>(req);
            if (!docs.Success)
            {
                return;
            }
            List<object> options = new List<object>();
            foreach (var item in docs.Items)
            {
                options.Add(new { text = item.name, value = item.recordId });
            }
            X.Call("InitTypes", options);
            AttachmentsWindow.Show();
        }


        protected void employeeDocumentsStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeAttachmentsListRequest request = new EmployeeAttachmentsListRequest();
            request.recordId = Convert.ToInt32(CurrentEmployee.Text);
            ListResponse<Attachement> documents = _systemService.ChildGetAll<Attachement>(request);
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
            if (b.expiryDate.HasValue)
                b.expiryDate = new DateTime(b.expiryDate.Value.Year, b.expiryDate.Value.Month, b.expiryDate.Value.Day, 14, 0, 0);

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    PostRequestWithAttachment<EmployeeDocument> request = new PostRequestWithAttachment<EmployeeDocument>();

                    byte[] fileData = null;
                    if (documentFile.PostedFile != null && documentFile.PostedFile.ContentLength > 0)
                    {
                        //using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                        //{
                        //    fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                        //}
                        fileData = new byte[documentFile.PostedFile.ContentLength];
                        fileData = documentFile.FileBytes;
                        request.FileName = documentFile.PostedFile.FileName;
                        request.FileData = fileData;

                    }
                    else
                    {
                        request.FileData = fileData;
                        request.FileName = "";
                    }
                    request.entity = b;

                    PostResponse<EmployeeDocument> r = _employeeService.ChildAddOrUpdateWithAttachment<EmployeeDocument>(request);
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
                        EmployeeDocument insertedEntity = GetById(b.recordId);
                        //Add this record to the store 

                        this.employeeDocumentsStore.Insert(0, insertedEntity);

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
                    PostRequestWithAttachment<EmployeeDocument> request = new PostRequestWithAttachment<EmployeeDocument>();

                    byte[] fileData = null;
                    if (documentFile.HasFile && documentFile.PostedFile.ContentLength > 0)
                    {
                        //using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                        // {
                        //    fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                        // }
                        fileData = new byte[documentFile.PostedFile.ContentLength];
                        fileData = documentFile.FileBytes;
                        request.FileName = documentFile.PostedFile.FileName;
                        request.FileData = fileData;



                    }
                    else
                    {
                        request.FileData = fileData;
                        request.FileName = "";
                    }
                    request.entity = b;



                    PostResponse<EmployeeDocument> r = _employeeService.ChildAddOrUpdateWithAttachment<EmployeeDocument>(request);


                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                        return;
                    }
                    else
                    {

                        EmployeeDocument updated = GetById(b.recordId);
                        ModelProxy record = this.employeeDocumentsStore.GetById(index);

                        EditDocumentForm.UpdateRecord(record);
                        record.Set("dtName", updated.dtName);
                        record.Set("fileUrl", updated.fileUrl);
                        if (b.expiryDate.HasValue)
                            record.Set("expiryDate", updated.expiryDate.Value.ToShortDateString());
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
                dtId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        private EmployeeDocument GetById(string id)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = id.ToString();
            RecordResponse<EmployeeDocument> response = _employeeService.ChildGetRecord<EmployeeDocument>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return null;
            }
            return response.result;
        }


    }
}
