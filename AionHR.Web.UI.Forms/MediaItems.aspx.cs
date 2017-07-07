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
using AionHR.Model.MediaGallery;
using System.Net;
using AionHR.Infrastructure.Domain;
using AionHR.Services.Messaging.System;

namespace AionHR.Web.UI.Forms
{
    public partial class MediaItems : System.Web.UI.Page
    {

        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IMediaGalleryService _mediaGalleryService = ServiceLocator.Current.GetInstance<IMediaGalleryService>();

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
                date.Format = DateColumn1.Format = _systemService.SessionHelper.GetDateformat();
            }

            try
            {
                AccessControlApplier.ApplyAccessControlOnPage(typeof(MediaItem), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
            }
            catch (AccessDeniedException exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                Viewport1.Hidden = true;
                return;
            }
            if (rwFile.InputType == InputType.Password)
            {
                var s = GridPanel1.ColumnModel.Columns[GridPanel1.ColumnModel.Columns.Count - 1];
                s.Renderer.Handler = s.Renderer.Handler.Replace("attachRender()", " ' '");
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
                this.Viewport1.RTL = true;

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

        protected void PoPuP(object sender, DirectEventArgs e)
        {
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string path = e.ExtraParams["path"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<MediaItem> response = _mediaGalleryService.ChildGetRecord<MediaItem>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    url.Text = response.result.pictureUrl;
                    this.BasicInfoTab.SetValues(response.result);

                    FillMediaCategory();
                    mcId.Select(response.result.mcId.ToString());

                    FillDepartment();
                    departmentId.Select(response.result.departmentId.Value.ToString());

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
                    DownloadFile(path);
                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }

        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                MediaItem s = new MediaItem();
                s.recordId = index;
                s.description = "";
                s.date = DateTime.Now;
                s.mcId = 0;
                s.mcName = "";
                s.departmentId = 0;
                s.departmentName = "";
                s.type = 0;


                PostRequest<MediaItem> req = new PostRequest<MediaItem>();
                req.entity = s;
                PostResponse<MediaItem> r = _mediaGalleryService.ChildDelete<MediaItem>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
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

        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {
            //Reset all values of the relative object
            BasicInfoTab.Reset();
            FillDepartment();
            FillMediaCategory();
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
            MediaItemsListRequest request = new MediaItemsListRequest();

            request.Size = "50";
            request.StartAt = "1";
            request.SortBy = "mcName";
            request.MCId = 0;
            request.DepartmentId = 0;
            request.Type = 0;
            

            request.Filter = "";
            ListResponse<MediaItem> routers = _mediaGalleryService.ChildGetAll<MediaItem>(request);
            if (!routers.Success)
                X.Msg.Alert(Resources.Common.Error, routers.Summary).Show();
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.Store1.DataBind();
        }


        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {
            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string obj = e.ExtraParams["values"];
            MediaItem b = JsonConvert.DeserializeObject<MediaItem>(obj);

            string id = e.ExtraParams["id"];
            string url = e.ExtraParams["url"];
            // Define the object to add or edit as null

            if (mcId.SelectedItem != null)
                b.mcName = mcId.SelectedItem.Text;

            if (departmentId.SelectedItem != null)
                b.departmentName = departmentId.SelectedItem.Text;


            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 

                    PostRequest<MediaItem> request = new PostRequest<MediaItem>();
                    request.entity = b;
                    byte[] fileData = null;
                    if (rwFile.PostedFile != null && rwFile.PostedFile.ContentLength > 0)
                    {
                        //using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                        //{
                        //    fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                        //}
                        fileData = new byte[rwFile.PostedFile.ContentLength];
                        fileData = rwFile.FileBytes;
                        

                    }
                    



                   
                    PostResponse<MediaItem> r = _mediaGalleryService.ChildAddOrUpdate<MediaItem>(request);
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
                        if (fileData != null)
                        {
                            SystemAttachmentsPostRequest req = new SystemAttachmentsPostRequest();
                            req.entity = new Model.System.Attachement() { date = DateTime.Now, classId = ClassId.MGME, recordId = Convert.ToInt32(b.recordId), fileName = rwFile.PostedFile.FileName, seqNo = null };
                            req.FileNames.Add(rwFile.PostedFile.FileName);
                            req.FilesData.Add(fileData);
                            PostResponse<Attachement> resp = _systemService.UploadMultipleAttachments(req);
                            if (!resp.Success)//it maybe be another condition
                            {
                                //Show an error saving...
                                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                                X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                                return;
                            }
                        }
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        MediaItem m = GetMEById(r.recordId);
                        Store1.Insert(0, m);
                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditRecordWindow.Close();
                        RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
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
                    //getting the id of the record
                    int index = Convert.ToInt32(id);//getting the id of the record
                    PostRequest<MediaItem> request = new PostRequest<MediaItem>();
                    request.entity = b;
                    byte[] fileData = null;
                    if (rwFile.PostedFile != null && rwFile.PostedFile.ContentLength > 0)
                    {
                        
                       
                        fileData = new byte[rwFile.PostedFile.ContentLength];
                        fileData = rwFile.FileBytes;
                  

                    }
                  

                    PostResponse<MediaItem> r = _mediaGalleryService.ChildAddOrUpdate<MediaItem>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                        return;
                    }
                    else
                    {
                        if (fileData != null)
                        {
                            SystemAttachmentsPostRequest req = new SystemAttachmentsPostRequest();
                            req.entity = new Model.System.Attachement() { date = DateTime.Now, classId = ClassId.MGME, recordId = Convert.ToInt32(b.recordId), fileName = rwFile.PostedFile.FileName, seqNo = 0 };
                            req.FileNames.Add(rwFile.PostedFile.FileName);
                            req.FilesData.Add(fileData);
                            PostResponse<Attachement> resp = _systemService.UploadMultipleAttachments(req);
                            if (!resp.Success)//it maybe be another condition
                            {
                                //Show an error saving...
                                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                                X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                                return;
                            }
                        }
                        MediaItem m = GetMEById(id);

                        ModelProxy record = this.Store1.GetById(id);
                        BasicInfoTab.UpdateRecord(record);
                        record.Set("mcName", b.mcName);
                        record.Set("departmentName", b.departmentName);
                        recordId.Set("pictureUrl", m.pictureUrl);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditRecordWindow.Close();
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

        protected void BasicInfoTab_Load(object sender, EventArgs e)
        {

        }



        protected void addMediaCategory(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(mcId.Text))
                return;
            MediaCategory dept = new MediaCategory();
            dept.name = mcId.Text;

            PostRequest<MediaCategory> depReq = new PostRequest<MediaCategory>();
            depReq.entity = dept;
            PostResponse<MediaCategory> response = _mediaGalleryService.ChildAddOrUpdate<MediaCategory>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillMediaCategory();
                mcId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        protected void addDepartment(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(departmentId.Text))
                return;
            Department dept = new Department();
            dept.name = departmentId.Text;

            PostRequest<Department> depReq = new PostRequest<Department>();
            depReq.entity = dept;
            PostResponse<Department> response = _branchService.ChildAddOrUpdate<Department>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillDepartment();
                departmentId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }


        public void FillMediaCategory()
        {

            ListRequest req = new ListRequest();

            ListResponse<MediaCategory> response = _mediaGalleryService.ChildGetAll<MediaCategory>(req);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }
            mcStore.DataSource = response.Items;
            mcStore.DataBind();

        }


        public void FillDepartment()
        {

            ListRequest req = new ListRequest();

            ListResponse<Department> response = _branchService.ChildGetAll<Department>(req);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }
            deStore.DataSource = response.Items;
            deStore.DataBind();

        }


        private MediaItem GetMEById(string id)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = id;
            return _mediaGalleryService.ChildGetRecord<MediaItem>(req).result;
        }


    }
}