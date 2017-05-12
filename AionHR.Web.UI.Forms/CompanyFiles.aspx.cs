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
using AionHR.Model.LeaveManagement;
using AionHR.Services.Messaging.System;
using AionHR.Model.Company.Cases;
using System.Net;
using AionHR.Infrastructure.Domain;
using AionHR.Services.Messaging.CompanyStructure;

namespace AionHR.Web.UI.Forms
{
    public partial class CompanyFiles : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();

        ICaseService _caseService = ServiceLocator.Current.GetInstance<ICaseService>();



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


                dateCol.Format = _systemService.SessionHelper.GetDateformat();
                CompanyFilesClassId.Text = ClassId.DMDO.ToString();
         
            }

        }



        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

             CompanyFilesListRequest request = new CompanyFilesListRequest();
            request.recordId = 0;

            ListResponse<Attachement> routers = _systemService.ChildGetAll<Attachement>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, routers.Summary).Show();
                return;
            }
            this.Store1.DataSource = routers.Items;


            this.Store1.DataBind();
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
                CurrentLanguage.Text = "ar";
            }
            else
            {
                CurrentLanguage.Text = "en";
            }
        }

        protected void SaveFolder(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            Attachement b = JsonConvert.DeserializeObject<Attachement>(obj);
            b.recordId = 0;
            b.seqNo = Convert.ToInt16(id);
            b.classId = ClassId.DMDO;
            b.fileName = fileName.Text;
            // Define the object to add or edit as null
            b.folderName = folderId.SelectedItem.Text;


            try
            {
                //New Mode
                PostRequest<Attachement> req = new PostRequest<Attachement>();
                req.entity = b;



                PostResponse<Attachement> r = _systemService.ChildAddOrUpdate<Attachement>(req);



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


                    ModelProxy record = this.Store1.GetById(id);

                    EditDocumentForm.UpdateRecord(record);
                    record.Set("folderName", b.folderName);


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
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }



        }

        private List<SystemFolder> GetFolders()
        {
            ListRequest req = new ListRequest();
            ListResponse<SystemFolder> docs = _systemService.ChildGetAll<SystemFolder>(req);
            return docs.Items;
        }
        protected void PoPuP(object sender, DirectEventArgs e)
        {

            
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string path = e.ExtraParams["path"];
            string folder = e.ExtraParams["folderId"];
            string file = e.ExtraParams["fileName"];
            switch (type)
            {

                case "imgEdit":
                    dtStore.DataSource = GetFolders();
                    dtStore.DataBind();
                    folderId.Select(folder);
                    seqNo.Text = id.ToString();
                    fileName.Text = file;
                    this.EditDocumentWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditDocumentWindow.Show();
                    break;
                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteAttachment({0},'{1}')", id,path),
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
                resp.Flush();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message + "<br />" + exp.StackTrace).Show();
                return;
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

        protected void addFolder(object sender, DirectEventArgs e)
        {
            SystemFolder dept = new SystemFolder();
            dept.name = folderId.Text;

            PostRequest<SystemFolder> depReq = new PostRequest<SystemFolder>();
            depReq.entity = dept;

            PostResponse<SystemFolder> response = _systemService.ChildAddOrUpdate<SystemFolder>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                dtStore.DataSource = GetFolders();
                
                folderId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }


        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteAttachment(string index,string path)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Attachement n = new Attachement();
                n.classId = ClassId.DMDO;
                n.recordId = 0;
                n.seqNo = Convert.ToInt16(index);
                n.fileName = path;

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