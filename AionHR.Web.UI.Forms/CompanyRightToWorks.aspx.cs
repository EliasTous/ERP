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
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms
{
    public partial class CompanyRightToWorks : System.Web.UI.Page
    {
        ICompanyStructureService _companyService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();


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
                rwIssueDate.Format = rwExpiryDate.Format = _systemService.SessionHelper.GetDateformat();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(CompanyRightToWork), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                //issueDateMulti.InputType = issueDate.InputType;
                //issueDateMulti.Disabled = issueDate.Disabled;
                //issueDateMulti.ReadOnly = issueDate.ReadOnly;
                //issueDateDisabled.Text = issueDate.ReadOnly.ToString();


                //expiryDateMulti.InputType = expiryDate.InputType;
                //expiryDateMulti.Disabled = expiryDate.Disabled;
                //expiryDateMulti.ReadOnly = expiryDate.ReadOnly;
                //expiryDateDisabled.Text = expiryDate.ReadOnly.ToString();

                //hijriCal.LazyItems.ForEach(x => (x as Field).ReadOnly = expiryDate.ReadOnly || issueDate.ReadOnly);
                if (rwFile.InputType == InputType.Password)
                {
                    var s = GridPanel1.ColumnModel.Columns[GridPanel1.ColumnModel.Columns.Count - 1];
                    s.Renderer.Handler = s.Renderer.Handler.Replace("attachRender()", " ' '");
                }

                if (!_systemService.SessionHelper.GetHijriSupport())
                {
                    SetHijriInputState(false);
                    hijriCal.Visible = false;
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
                this.Viewport1.RTL = true;
                CurrentLanguage.Text = "ar";
            }
            else
                CurrentLanguage.Text = "en";
        }

        protected void addDocumentType(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(dtId.Text))
                return;
            CompanyDocumentType dept = new CompanyDocumentType();
            dept.name = dtId.Text;

            PostRequest<CompanyDocumentType> depReq = new PostRequest<CompanyDocumentType>();
            depReq.entity = dept;
            PostResponse<CompanyDocumentType> response = _systemService.ChildAddOrUpdate<CompanyDocumentType>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillDocumentType();
                dtId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        protected void addBranch(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(branchId.Text))
                return;
            Branch dept = new Branch();
            dept.name = branchId.Text;
            dept.timeZone = _systemService.SessionHelper.GetDefaultTimeZone();
            PostRequest<Branch> depReq = new PostRequest<Branch>();
            depReq.entity = dept;
            PostResponse<Branch> response = _companyService.ChildAddOrUpdate<Branch>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillBranch();
                branchId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }


        public void FillDocumentType()
        {

            ListRequest req = new ListRequest();

            ListResponse<CompanyDocumentType> response = _systemService.ChildGetAll<CompanyDocumentType>(req);
            if (!response.Success)
            {
                 Common.errorMessage(response);
                return;
            }
            dtStore.DataSource = response.Items;
            dtStore.DataBind();

        }


        public void FillBranch()
        {

            ListRequest req = new ListRequest();

            ListResponse<Branch> response = _companyService.ChildGetAll<Branch>(req);
            if (!response.Success)
            {
                 Common.errorMessage(response);
                return;
            }
            brStore.DataSource = response.Items;
            brStore.DataBind();

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

                    RecordResponse<CompanyRightToWork> response = _systemService.ChildGetRecord<CompanyRightToWork>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    recordId.Text = response.result.recordId;
                    url.Text = response.result.fileUrl;
                    this.BasicInfoTab.SetValues(response.result);

                    FillDocumentType();
                    dtId.Select(response.result.dtId.ToString());

                    FillBranch();
                    if (response.result.branchId.HasValue)
                        branchId.Select(response.result.branchId.Value.ToString());
                    SetHijriInputState(response.result.hijriCal);
                    if (_systemService.SessionHelper.GetHijriSupport())
                    {
                        SetHijriInputState(response.result.hijriCal);
                        if (response.result.hijriCal)
                        {
                            hijCal.Checked = true;

                            rwIssueDateMulti.Text = response.result.issueDate!=null  ? response.result.issueDate.ToString("yyyy/MM/dd", new CultureInfo("ar")) : "";
                            rwExpiryDateMulti.Text = response.result.expiryDate != null ?response.result.expiryDate.ToString("yyyy/MM/dd", new CultureInfo("ar")) : "";
                            hijriSelected.Text = "true";
                        }
                        else
                        {
                            gregCal.Checked = true;
                          
                            rwIssueDateMulti.Text = "";
                            rwExpiryDateMulti.Text = "";
                            hijriSelected.Text = "false";
                        }
                        X.Call("handleInputRender");
                    }
                    else
                    { SetHijriInputState(false); }
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
                CompanyRightToWork s = new CompanyRightToWork();
                s.recordId = index;
                s.remarks = "";
                s.documentRef = "";
                s.issueDate = DateTime.Now;
                s.expiryDate = DateTime.Now;
                s.dtId = 0;
                s.dtName = "";
                s.branchId = 0;
                s.branchName = "";



                PostRequest<CompanyRightToWork> req = new PostRequest<CompanyRightToWork>();
                req.entity = s;
                PostResponse<CompanyRightToWork> r = _systemService.ChildDelete<CompanyRightToWork>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                     Common.errorMessage(r);
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
            FillBranch();
            FillDocumentType();
            if (_systemService.SessionHelper.GetHijriSupport())
            {
                rwIssueDate.Text = DateTime.Today.ToShortDateString(); ;
                rwExpiryDate.Text = DateTime.Today.ToShortDateString();
                hijriSelected.Text = "false";
                SetHijriInputState(false);
                X.Call("handleInputRender");
            }

            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            this.EditRecordWindow.Show();
        }

        private void SetHijriInputState(bool hijriSupported)
        {
            X.Call("setInputState", hijriSupported);


        }
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;

            //Fetching the corresponding list

            //in this test will take a list of News
            CompanyRightToworkListRequest request = new CompanyRightToworkListRequest();

            request.Size = "50";
            request.StartAt = "1";
            request.SortBy = "dtName";
            request.DTid = 0;
            request.BranchId = 0;



            request.Filter = "";
            ListResponse<CompanyRightToWork> routers = _systemService.ChildGetAll<CompanyRightToWork>(request);
            if (!routers.Success)
                Common.errorMessage(routers);
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.Store1.DataBind();
        }

        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {

            string ExpiryDateString = e.ExtraParams["rwExpiryDate"];
            string IssueDateDateString = e.ExtraParams["rwIssueDate"];

            DateTime ExpiryDate = new DateTime();
            DateTime IssueDate = new DateTime();

            if (!string.IsNullOrEmpty(ExpiryDateString))
            {
                ExpiryDate = DateTime.Parse(e.ExtraParams["rwExpiryDate"]);
            }
            if (!string.IsNullOrEmpty(IssueDateDateString))
            {
                IssueDate = DateTime.Parse(e.ExtraParams["rwIssueDate"]);
            }

            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string obj = e.ExtraParams["values"];
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            CompanyRightToWork b = JsonConvert.DeserializeObject<CompanyRightToWork>(obj, settings);

            string id = e.ExtraParams["id"];
            string url = e.ExtraParams["url"];
            // Define the object to add or edit as null
        
            if (dtId.SelectedItem != null)
                b.dtName = dtId.SelectedItem.Text;

            if (branchId.SelectedItem != null)
                b.branchName = branchId.SelectedItem.Text;
            bool hijriSupported = _systemService.SessionHelper.GetHijriSupport();
            try
            {
                CultureInfo c = new CultureInfo("en");
                string format = "";
                if (hijriSupported)
                {
                    if (hijriSelected.Text == "true")
                    {
                        b.hijriCal = true;
                        c = new CultureInfo("ar");
                        format = "yyyy/MM/dd";
                        b.issueDate = DateTime.ParseExact(rwIssueDateMulti.Text, format, c);
                        b.expiryDate = DateTime.ParseExact(rwExpiryDateMulti.Text, format, c);
                    }
                    //else
                    //{
                    //    c = new CultureInfo("en");
                    //    if (_systemService.SessionHelper.CheckIfArabicSession())
                    //    {

                    //        format = "dd/MM/yyyy";
                    //    }
                    //    else
                    //    {
                    //        format = "MM/dd/yyyy";
                    //    }
                    //}
                    else
                    {

                        b.issueDate = IssueDate;


                        b.expiryDate = ExpiryDate;
                    }

                }

            }
            catch (Exception exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("DateFormatError")).Show();

                return;
            }
            //b.remarks = 
            if (b.issueDate != null && (b.issueDate > b.expiryDate))
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("DateRangeError")).Show();

                return;
            }

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 

                    PostRequest<CompanyRightToWork> request = new PostRequest<CompanyRightToWork>();
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





                    PostResponse<CompanyRightToWork> r = _systemService.ChildAddOrUpdate<CompanyRightToWork>(request);
                    b.recordId = r.recordId;

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
                        if (fileData != null)
                        {
                            SystemAttachmentsPostRequest req = new SystemAttachmentsPostRequest();
                            req.entity = new Model.System.Attachement() { date = DateTime.Now, classId = ClassId.SYRW, recordId = Convert.ToInt32(b.recordId), fileName = rwFile.PostedFile.FileName, seqNo = 0 };
                            req.FileNames.Add(rwFile.PostedFile.FileName);
                            req.FilesData.Add(fileData);
                            PostResponse<Attachement> resp = _systemService.UploadMultipleAttachments(req);
                            if (!resp.Success)//it maybe be another condition
                            {
                                //Show an error saving...
                                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                                 Common.errorMessage(r);
                                return;
                            }
                        }
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        CompanyRightToWork m = GetRWById(r.recordId);
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
                    PostRequest<CompanyRightToWork> request = new PostRequest<CompanyRightToWork>();
                    b.recordId = index.ToString(); ;
                    request.entity = b;

                    byte[] fileData = null;
                    if (rwFile.PostedFile != null && rwFile.PostedFile.ContentLength > 0)
                    {

                        fileData = new byte[rwFile.PostedFile.ContentLength];
                        fileData = rwFile.FileBytes;


                    }


                    PostResponse<CompanyRightToWork> r = _systemService.ChildAddOrUpdate<CompanyRightToWork>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
                        return;
                    }
                    else
                    {
                        if (fileData != null)
                        {
                            SystemAttachmentsPostRequest req = new SystemAttachmentsPostRequest();
                            req.entity = new Model.System.Attachement() { date = DateTime.Now, classId = ClassId.SYRW, recordId = Convert.ToInt32(b.recordId), fileName = rwFile.PostedFile.FileName, seqNo = 0 };
                            req.FileNames.Add(rwFile.PostedFile.FileName);
                            req.FilesData.Add(fileData);
                            PostResponse<Attachement> resp = _systemService.UploadMultipleAttachments(req);
                            if (!resp.Success)//it maybe be another condition
                            {
                                //Show an error saving...
                                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                                 Common.errorMessage(r);
                                return;
                            }
                        }
                        CompanyRightToWork m = GetRWById(id);

                        ModelProxy record = this.Store1.GetById(id);
                        BasicInfoTab.UpdateRecord(record);
                        record.Set("dtName", b.dtName);
                        record.Set("branchName", b.branchName);
                        record.Set("fileUrl", m.fileUrl);
                        record.Set("issueDateFormatted", m.issueDateFormatted);
                        record.Set("expireDateFormatted", m.expireDateFormatted);
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


        private CompanyRightToWork GetRWById(string id)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = id;
            return _systemService.ChildGetRecord<CompanyRightToWork>(req).result;
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
















    }
}