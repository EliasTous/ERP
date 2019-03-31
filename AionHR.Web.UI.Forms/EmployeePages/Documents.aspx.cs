
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
using AionHR.Web.UI.Forms.ConstClasses;
using System.Drawing;

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class Documents : System.Web.UI.Page
    {
        private Bitmap bitmap;
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();


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
                if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                CurrentEmployee.Text = Request.QueryString["employeeId"];
                CurrentDateFormat.Text = _systemService.SessionHelper.GetDateformat();
                EmployeeClassId.Text = ClassId.EPDO.ToString();
                dateCol.Format = _systemService.SessionHelper.GetDateformat();
                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";
                btnAdd.Disabled = disabled;
                if (_systemService.SessionHelper.CheckIfIsAdmin())
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Attachement), EditDocumentForm, employeeDocumentsGrid, btnAdd, SaveDocumentButton);

                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    employeeDocumentsGrid.Hidden = true;
                    return;
                }
                var properties = AccessControlApplier.GetPropertiesLevels(typeof(Attachement));
                if (properties.Where(x => x.index == "url").ToList()[0].accessLevel == 0)
                {
                    var s = employeeDocumentsGrid.ColumnModel.Columns[employeeDocumentsGrid.ColumnModel.Columns.Count - 1];
                    s.Renderer.Handler = s.Renderer.Handler.Replace("attachRender()", "' '");
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
            switch (_systemService.SessionHelper.getLangauge())
            {
                case "ar":
                    {
                        CurrentLanguage.Text = "ar";
                    }
                    break;
                case "en":
                    {
                        CurrentLanguage.Text = "en";
                    }
                    break;

                case "fr":
                    {
                        CurrentLanguage.Text = "fr";
                    }
                    break;
                case "de":
                    {
                        CurrentLanguage.Text = "de";
                    }
                    break;
                default:
                    {

                        CurrentLanguage.Text = "en";

                    }
                    break;
            }
        }



        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            string path = e.ExtraParams["path"];
            string folder = e.ExtraParams["folderId"];
            string file = e.ExtraParams["fileName"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 

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
                            Handler = String.Format("App.direct.DeleteDocument({0},'{1}')", id, path),
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
                case "preAttach":
                    ImageUrl.Text = path;
                    var values = path.Split('.');
                    if (values[values.Length - 1].ToString().ToLower() == "jpg" || values[values.Length - 1].ToString().ToLower() == "png" || values[values.Length - 1].ToString().ToLower() == "jpg")
                    {
                        imgControl.Src = path;
                        Window1.Show();
                    }
                    else
                        X.MessageBox.Alert(GetGlobalResourceObject("Common", "Error").ToString(), GetLocalResourceObject("previewImage").ToString()).Show();
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
        public void DeleteDocument(string index, string name)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Attachement n = new Attachement();
                n.classId = ClassId.EPDO;
                n.recordId = Convert.ToInt32(CurrentEmployee.Text);
                n.seqNo = Convert.ToInt16(index);
                n.fileName = name;

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
                        Icon = Ext.Net.Icon.Information,
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

            List<SystemFolder> docs = GetFolders();

            List<object> options = new List<object>();
            foreach (var item in docs)
            {
                options.Add(new { text = item.name, value = item.recordId });
            }
            X.Call("InitTypes", options);
            AttachmentsWindow.Show();
        }

        private List<SystemFolder> GetFolders()
        {
            ListRequest req = new ListRequest();
            ListResponse<SystemFolder> docs = _systemService.ChildGetAll<SystemFolder>(req);
            return docs.Items;
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



        protected void SaveFolder(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            Attachement b = JsonConvert.DeserializeObject<Attachement>(obj);
            b.recordId = Convert.ToInt32(CurrentEmployee.Text);
            b.seqNo = Convert.ToInt16(id);
            b.classId = ClassId.EPDO;
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
                     Common.errorMessage(r);
                    return;
                }
                else
                {


                    ModelProxy record = this.employeeDocumentsStore.GetById(id);

                    EditDocumentForm.UpdateRecord(record);
                    record.Set("folderName", b.folderName);


                    record.Commit();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Ext.Net.Icon.Information,
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
               Common.errorMessage(resp);
                return;
            }
            dtStore.DataSource = resp.Items;
            dtStore.DataBind();

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
                FillDocumentTypes();
                folderId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
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
                 Common.errorMessage(response);
                return null;
            }
            return response.result;
        }
     


    }
  
}
