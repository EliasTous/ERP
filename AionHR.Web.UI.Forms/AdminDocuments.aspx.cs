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
using AionHR.Model.AdminTemplates;
using AionHR.Services.Messaging.AdministrativeAffairs;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Model;
using AionHR.Services.Messaging.System;

namespace AionHR.Web.UI.Forms
{
    public partial class AdminDocuments : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IAdministrationService _administrationService = ServiceLocator.Current.GetInstance<IAdministrationService>();
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
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AdminDocument), BasicInfoTab1, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();
                    Viewport1.Hidden = true;





                    return;
                }

                ColdayIdDate.Format = dayIdDate.Format = ColissueDate.Format = ColexpiryDate.Format = _systemService.SessionHelper.GetDateformat();
                FillDepartment();

            }

        }


        private void FillBpId()
        {
            ListRequest vsRequest = new ListRequest();
            ListResponse<BusinessPartner> resp = _administrationService.ChildGetAll<BusinessPartner>(vsRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            bpIdStore.DataSource = resp.Items;
            bpIdStore.DataBind();
        }
        private void FilldcStore()
        {
            ListRequest vsRequest = new ListRequest();
            ListResponse<DocumentCategory> resp = _administrationService.ChildGetAll<DocumentCategory>(vsRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            dcStore.DataSource = resp.Items;
            dcStore.DataBind();
        }
        private void FilllanguageStore()
        {
            //TemplateBodyListReuqest req = new TemplateBodyListReuqest();
            //req.TemplateId = Convert.ToInt32(recordId.Text);
            //ListResponse<TemplateBody> bodies = _administrationService.ChildGetAll<TemplateBody>(req);
            //if (!bodies.Success)
            //{
            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", bodies.ErrorCode) != null ? GetGlobalResourceObject("Errors", bodies.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + bodies.LogId : bodies.Summary).Show();
            //    return;
            //}
            //languageStore.DataSource = bodies.Items;
            //languageStore.DataBind();
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



        protected void PoPuP(object sender, DirectEventArgs e)
        {

            panelRecordDetails.ActiveIndex = 0;
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            FilllanguageStore();
            FillBpId();
            FilldcStore();
            currentDocumentId.Text = id;
            switch (type)
            {
                case "imgEdit":
                    //Important to enable the grids every time, in case an add attempt was made without saving
                    documentNotesPanel.Disabled = false;
                    DocumentDuesGrid.Disabled = false;
                    DocumentTransfersGrid.Disabled = false;
                    DXGrid.Disabled = false;

                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<AdminDocument> response = _administrationService.ChildGetRecord<AdminDocument>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }

                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab1.SetValues(response.result);


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
        protected void PoPuPDD(object sender, DirectEventArgs e)
        {






            string rowId = e.ExtraParams["rowId"];
            string doId = e.ExtraParams["doId"];
            string dayId = e.ExtraParams["dayId"];
            string amount = e.ExtraParams["amount"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    DocumentDueRecordRequest r = new DocumentDueRecordRequest();
                    r.dayId = dayId;
                    r.doId = doId;

                    RecordResponse<AdminDocumentDue> response = _administrationService.ChildGetRecord<AdminDocumentDue>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }

                    //Step 2 : call setvalues with the retrieved object
                    this.documentDueForm.SetValues(response.result);
                    if (!string.IsNullOrEmpty(response.result.dayId))
                        dayIdDate.Value = DateTime.ParseExact(response.result.dayId, "yyyyMMdd", new CultureInfo("en"));

                    this.documentDueWindow.Title = Resources.Common.EditWindowsTitle;
                    this.documentDueWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteDD({0})", dayId),
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
        public void DeleteDN(string rowId, string seqNo, string date)
        {

            try
            {
                //Step 1 Code to delete the object from the database 
                AdminDocumentNote n = new AdminDocumentNote();

                n.notes = "";
                n.date = DateTime.ParseExact(date, "yyyyMMdd", new CultureInfo("en"));
                n.doId = Convert.ToInt32(currentDocumentId.Text);
                n.seqNo = seqNo;
                n.rowId = rowId;
                n.userId = _systemService.SessionHelper.GetCurrentUserId();
                PostRequest<AdminDocumentNote> req = new PostRequest<AdminDocumentNote>();
                req.entity = n;
                PostResponse<AdminDocumentNote> res = _administrationService.ChildDelete<AdminDocumentNote>(req);
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
                    DocumentNotesStore.Reload();

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
        public void DeleteDT(string seqNo, string docId)
        {

            try
            {
                //Step 1 Code to delete the object from the database 
                AdminDocTransfer n = new AdminDocTransfer();

                n.notes = "";

                n.doId = Convert.ToInt32(currentDocumentId.Text);
                n.seqNo = seqNo;

                PostRequest<AdminDocTransfer> req = new PostRequest<AdminDocTransfer>();
                req.entity = n;
                PostResponse<AdminDocTransfer> res = _administrationService.ChildDelete<AdminDocTransfer>(req);
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
                    documentsTransfersStore.Reload();

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
        public void DeleteDX(string seqNo, string docId)
        {

            try
            {
                //Step 1 Code to delete the object from the database 
                AdminDocumentDX n = new AdminDocumentDX();



                n.doId = Convert.ToInt32(currentDocumentId.Text);
                n.seqNo = seqNo;

                PostRequest<AdminDocumentDX> req = new PostRequest<AdminDocumentDX>();
                req.entity = n;
                PostResponse<AdminDocumentDX> res = _administrationService.ChildDelete<AdminDocumentDX>(req);
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
                    DXStore.Reload();

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
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                AdminDocument s = new AdminDocument();
                s.recordId = index;

                //s.intName = "";

                PostRequest<AdminDocument> req = new PostRequest<AdminDocument>();
                req.entity = s;
                PostResponse<AdminDocument> r = _administrationService.ChildDelete<AdminDocument>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r); ;
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
        [DirectMethod]
        public void DeleteDD(string dayId)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                AdminDocumentDue s = new AdminDocumentDue();
                s.dayId = dayId;
                s.doId = currentDocumentId.Text;

                //s.intName = "";

                PostRequest<AdminDocumentDue> req = new PostRequest<AdminDocumentDue>();
                req.entity = s;
                PostResponse<AdminDocumentDue> r = _administrationService.ChildDelete<AdminDocumentDue>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r); ;
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    DocumentDueStore.Reload();

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
        protected void ADDNewDNRecord(object sender, DirectEventArgs e)
        {
            try
            {

                string noteText = e.ExtraParams["noteText"];
                X.Call("ClearNoteText");
                PostRequest<AdminDocumentNote> req = new PostRequest<AdminDocumentNote>();
                AdminDocumentNote note = new AdminDocumentNote();
                //note.recordId = id;
                note.doId = Convert.ToInt32(currentDocumentId.Text);
                note.notes = noteText;
                note.userId = _systemService.SessionHelper.GetCurrentUserId();
                note.date = DateTime.Now;
                note.seqNo = null;
                note.rowId = null;
                req.entity = note;



                PostResponse<AdminDocumentNote> resp = _administrationService.ChildAddOrUpdate<AdminDocumentNote>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);

                }
                DocumentNotesStore.Reload();
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

            //Reset all values of the relative object

        }
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {
            panelRecordDetails.ActiveIndex = 0;
            //Reset all values of the relative object
            BasicInfoTab1.Reset();
            FillBpId();
            FilllanguageStore();
            FilldcStore();
            documentNotesPanel.Disabled = true;
            DocumentDuesGrid.Disabled = true;
            DocumentTransfersGrid.Disabled = true;
            DXGrid.Disabled = true;
            currentDocumentId.Text = "";
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }
        protected void AddNewTransfer(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            DocumentTransferForm.Reset();
            FillDepartment();
            seqNo.Text = "";



            this.DocumentTransferWindow.Show();
        }
        protected void AddDX(object sender, DirectEventArgs e)
        {
            DXForm.Reset();

            DXseqNo.Text = "";



            this.DXWindow.Show();

        }
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            DocumentListRequest request = new DocumentListRequest();
            request.Status = 0;
            request.Filter = "";

            ListResponse<AdminDocument> routers = _administrationService.ChildGetAll<AdminDocument>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.Store1.DataBind();
        }

        protected void DocumentDueStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page

            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            DocumentNoteListRequest req = new DocumentNoteListRequest();
            req.documentId = Convert.ToInt32(currentDocumentId.Text);
            req.Filter = string.Empty;
            ListResponse<AdminDocumentDue> routers = _administrationService.ChildGetAll<AdminDocumentDue>(req);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            routers.Items.ForEach(x =>
            {
                if (string.IsNullOrEmpty(x.dayId))
                    x.dayIdDate = null;
                else
                {
                    x.dayIdDate = DateTime.ParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en"));
                }
            });
            this.DocumentDueStore.DataSource = routers.Items;
            e.Total = routers.count;

            this.DocumentDueStore.DataBind();
        }
        protected void DocumentNotesStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page

            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            if (string.IsNullOrEmpty(currentDocumentId.Text))
                return;
            DocumentNoteListRequest req = new DocumentNoteListRequest();
            req.documentId = Convert.ToInt32(currentDocumentId.Text);
            ListResponse<AdminDocumentNote> notes = _administrationService.ChildGetAll<AdminDocumentNote>(req);
            if (!notes.Success)
                X.Msg.Alert(Resources.Common.Error, notes.Summary).Show();
            this.DocumentNotesStore.DataSource = notes.Items;
            e.Total = notes.count;

            this.DocumentNotesStore.DataBind();
        }
        protected void documentsTransfersStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page

            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            if (string.IsNullOrEmpty(currentDocumentId.Text))
                return;
            DocumentTransfersListRequest req = new DocumentTransfersListRequest();
            req.DocumentId = currentDocumentId.Text;
            ListResponse<AdminDocTransfer> transfers = _administrationService.ChildGetAll<AdminDocTransfer>(req);
            if (!transfers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, transfers.Summary).Show();
                return;
            }
            this.documentsTransfersStore.DataSource = transfers.Items;
            e.Total = transfers.count;

            this.documentsTransfersStore.DataBind();
        }
        protected void DXStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            if (string.IsNullOrEmpty(currentDocumentId.Text))
                return;
            DocumentDXListRequest req = new DocumentDXListRequest();

            req.DocumentId = currentDocumentId.Text;
            ListResponse<AdminDocumentDX> transfers = _administrationService.ChildGetAll<AdminDocumentDX>(req);
            if (!transfers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, transfers.Summary).Show();
                return;
            }
            List<AdminDocumentDX> SortedList = transfers.Items.OrderBy(x => x.priority).ToList();
            this.DXStore.DataSource = SortedList;
            e.Total = transfers.count;
            this.CurrentDXCount.Text = transfers.count.ToString();
            this.DXStore.DataBind();
        }

        protected void PoPuPDN(object sender, DirectEventArgs e)
        {


            string seqNo = e.ExtraParams["seqNo"];
            string type = e.ExtraParams["type"];
            string index = e.ExtraParams["index"];
            string date = DateTime.Parse(e.ExtraParams["date"]).ToString("yyyyMMdd");
            string rowId = e.ExtraParams["rowId"];
            documentNotesPanel.Disabled = false;
            DocumentDuesGrid.Disabled = false;


            switch (type)
            {


                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteDN({0},{1},{2})", rowId, seqNo, date),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;
                case "imgEdit":
                    X.Call("App.DocumentNotesGrid.editingPlugin.startEdit", Convert.ToInt32(index), 0);
                    break;
                case "colAttach":

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }
        protected void PoPuPDT(object sender, DirectEventArgs e)
        {

            string type = e.ExtraParams["type"];
            string seqNo = e.ExtraParams["seqNo"];
            string docId = e.ExtraParams["doId"];

            switch (type)
            {


                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteDT('{0}','{1}')", seqNo, docId),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;
                case "imgEdit":

                    DocumentTransfersRecordRequest req = new DocumentTransfersRecordRequest();
                    req.SeqNo = Convert.ToInt32(seqNo);
                    req.DocumentId = Convert.ToInt32(currentDocumentId.Text);
                    RecordResponse<AdminDocTransfer> resp = _administrationService.ChildGetRecord<AdminDocTransfer>(req);
                    if (!resp.Success)

                    {
                        Common.errorMessage(resp);
                        return;
                    }
                    DocumentTransferForm.SetValues(resp.result);
                    employeeId.GetStore().Add(new object[]
                           {
                                new
                                {
                                    recordId = resp.result.employeeId,
                                    fullName =resp.result.employeeName.fullName
                                }
                           });
                    employeeId.SetValue(resp.result.employeeId);
                    DocumentTransferWindow.Show();
                    break;
                case "colAttach":

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }

        protected void PoPuPDX(object sender, DirectEventArgs e)
        {

            string type = e.ExtraParams["type"];
            string seqNo = e.ExtraParams["seqNo"];
            string docId = e.ExtraParams["doId"];
            string description = e.ExtraParams["description"];
            string priority = e.ExtraParams["priority"];
            string isDone = e.ExtraParams["isDone"];
            switch (type)
            {


                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteDX('{0}','{1}')", seqNo, docId),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;
                case "imgEdit":

                    DXForm.Reset();
                    AdminDocumentDX d = new AdminDocumentDX();
                    d.seqNo = seqNo;
                    d.description = description;

                    d.isDone = isDone == "true";
                    d.priority = Convert.ToInt32(priority);


                    DXForm.SetValues(d);
                    DXWindow.Show();
                    break;
                case "colAttach":

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }

        protected void generateDocument(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            GenerateAdminDocumentDue b = JsonConvert.DeserializeObject<GenerateAdminDocumentDue>(obj);
            b.amount = string.IsNullOrEmpty(GDDAmount.Text) ? 0 : Convert.ToDouble(GDDAmount.Text);

            try
            {

                PostRequest<GenerateAdminDocumentDue> request = new PostRequest<GenerateAdminDocumentDue>();
                b.doId = currentDocumentId.Text;
                request.entity = b;
                PostResponse<GenerateAdminDocumentDue> r = _administrationService.ChildAddOrUpdate<GenerateAdminDocumentDue>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r); ;
                    return;
                }
                else
                {


                    //Display successful notification
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });

                    DocumentDueStore.Reload();



                }
            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }



        }
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            try
            {

                string obj = e.ExtraParams["values"];
                AdminDocument b = JsonConvert.DeserializeObject<AdminDocument>(obj);

                string id = e.ExtraParams["id"];
                // Define the object to add or edit as null

                if (string.IsNullOrEmpty(currentDocumentId.Text))
                {

                    try
                    {
                        //New Mode
                        //Step 1 : Fill The object and insert in the store 
                        PostRequest<AdminDocument> request = new PostRequest<AdminDocument>();

                        request.entity = b;
                        PostResponse<AdminDocument> r = _administrationService.ChildAddOrUpdate<AdminDocument>(request);


                        //check if the insert failed
                        if (!r.Success)//it maybe be another condition
                        {
                            //Show an error saving...
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(r); ;
                            return;
                        }
                        else
                        {
                            b.recordId = r.recordId;
                            currentDocumentId.Text = b.recordId;
                            //Add this record to the store 
                            this.Store1.Insert(0, b);

                            //Display successful notification
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordSavingSucc
                            });

                            documentNotesPanel.Disabled = false;
                            DocumentDuesGrid.Disabled = false;
                            DocumentTransfersGrid.Disabled = false;
                            Store1.Reload();



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
                        PostRequest<AdminDocument> request = new PostRequest<AdminDocument>();
                        request.entity = b;
                        PostResponse<AdminDocument> r = _administrationService.ChildAddOrUpdate<AdminDocument>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                        //Step 2 : saving to store

                        //Step 3 :  Check if request fails
                        if (!r.Success)//it maybe another check
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(r); ;
                            return;
                        }
                        else
                        {

                            currentDocumentId.Text = r.recordId;
                            Store1.Reload();
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
            catch (Exception ex)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
            }
        }
        protected void saveDocumentDue(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            try
            {


                string obj = e.ExtraParams["values"];
                string rowId = e.ExtraParams["rowId"];
                string dayId = e.ExtraParams["dayId"];
                AdminDocumentDue b = JsonConvert.DeserializeObject<AdminDocumentDue>(obj);

                if (b.dayIdDate == null)
                {
                    return;
                }
                else
                    b.dayId = ((DateTime)b.dayIdDate).ToString("yyyyMMdd");

                b.rowId = rowId;
                b.doId = currentDocumentId.Text;


                // Define the object to add or edit as null



                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AdminDocumentDue> request = new PostRequest<AdminDocumentDue>();

                    request.entity = b;
                    PostResponse<AdminDocumentDue> r = _administrationService.ChildAddOrUpdate<AdminDocumentDue>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(r); ;
                        return;
                    }
                    else
                    {
                        DocumentDueStore.Reload();

                        //Add this record to the store 


                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });



                        documentDueWindow.Close();

                    }
                }
                catch (Exception ex)
                {
                    //Error exception displaying a messsage box
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                }



                //else
                //{
                //    //Update Mode

                //    try
                //    {
                //        //getting the id of the record
                //        PostRequest<AdminDocument> request = new PostRequest<AdminDocument>();
                //        request.entity = b;
                //        PostResponse<AdminDocument> r = _administrationService.ChildAddOrUpdate<AdminDocument>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                //        //Step 2 : saving to store

                //        //Step 3 :  Check if request fails
                //        if (!r.Success)//it maybe another check
                //        {
                //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //            Common.errorMessage(r); ;
                //            return;
                //        }
                //        else
                //        {

                //            currentDocumentId.Text = r.recordId;
                //            Store1.Reload();
                //            Notification.Show(new NotificationConfig
                //            {
                //                Title = Resources.Common.Notification,
                //                Icon = Icon.Information,
                //                Html = Resources.Common.RecordUpdatedSucc
                //            });
                //            this.EditRecordWindow.Close();


                //        }

                //    }
                //    catch (Exception ex)
                //    {
                //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                //    }
                //}
            }
            catch (Exception ex)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
            }
        }
        protected void saveDocumentTransfer(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            try
            {


                string obj = e.ExtraParams["values"];

                AdminDocTransfer b = JsonConvert.DeserializeObject<AdminDocTransfer>(obj);



                b.doId = Convert.ToInt32(currentDocumentId.Text);


                // Define the object to add or edit as null



                if (b.seqNo == "")
                {
                    b.apStatus = "1";
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AdminDocTransfer> request = new PostRequest<AdminDocTransfer>();

                    request.entity = b;
                    PostResponse<AdminDocTransfer> r = _administrationService.ChildAddOrUpdate<AdminDocTransfer>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(r); ;
                        return;
                    }
                    else
                    {
                        documentsTransfersStore.Reload();

                        //Add this record to the store 


                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });



                        DocumentTransferWindow.Close();

                    }
                }



                else
                {
                    //Update Mode

                    try
                    {
                        //getting the id of the record
                        PostRequest<AdminDocTransfer> request = new PostRequest<AdminDocTransfer>();
                        request.entity = b;
                        PostResponse<AdminDocTransfer> r = _administrationService.ChildAddOrUpdate<AdminDocTransfer>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                        //Step 2 : saving to store

                        //Step 3 :  Check if request fails
                        if (!r.Success)//it maybe another check
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(r); ;
                            return;
                        }
                        else
                        {

                            documentsTransfersStore.Reload();
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordUpdatedSucc
                            });
                            this.DocumentTransferWindow.Close();


                        }

                    }
                    catch (Exception ex)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                    }
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
            }
        }
        protected void SaveDX(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            try
            {


                string obj = e.ExtraParams["values"];

                AdminDocumentDX b = JsonConvert.DeserializeObject<AdminDocumentDX>(obj);



                b.doId = Convert.ToInt32(currentDocumentId.Text);


                // Define the object to add or edit as null



                if (b.seqNo == "")
                {
                    b.priority = Convert.ToInt32(CurrentDXCount.Text) + 1;
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AdminDocumentDX> request = new PostRequest<AdminDocumentDX>();

                    request.entity = b;
                    PostResponse<AdminDocumentDX> r = _administrationService.ChildAddOrUpdate<AdminDocumentDX>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(r); ;
                        return;
                    }
                    else
                    {
                        DXStore.Reload();

                        //Add this record to the store 


                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });



                        DXWindow.Close();

                    }
                }



                else
                {
                    //Update Mode

                    try
                    {
                        //getting the id of the record

                        PostRequest<AdminDocumentDX> request = new PostRequest<AdminDocumentDX>();
                        request.entity = b;

                        PostResponse<AdminDocumentDX> r = _administrationService.ChildAddOrUpdate<AdminDocumentDX>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                        //Step 2 : saving to store

                        //Step 3 :  Check if request fails
                        if (!r.Success)//it maybe another check
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(r); ;
                            return;
                        }
                        else
                        {

                            DXStore.Reload();
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordUpdatedSucc
                            });
                            this.DXWindow.Close();


                        }

                    }
                    catch (Exception ex)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                    }
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
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
        protected void documentNotesPanel_Load(object sender, EventArgs e)
        {

        }

        protected void printBtn_Click(object sender, EventArgs e)
        {
            DocumentTypesReport p = GetReport();
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
            Response.Clear();
            Response.Write("<script>");
            Response.Write("window.document.forms[0].target = '_blank';");
            Response.Write("setTimeout(function () { window.document.forms[0].target = ''; }, 0);");
            Response.Write("</script>");
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }
        protected void ExportPdfBtn_Click(object sender, EventArgs e)
        {
            DocumentTypesReport p = GetReport();
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms);
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }

        protected void ExportXLSBtn_Click(object sender, EventArgs e)
        {
            DocumentTypesReport p = GetReport();
            string format = "xls";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToXls(ms);

            Response.Clear();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }
        private DocumentTypesReport GetReport()
        {

            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<DocumentType> resp = _employeeService.ChildGetAll<DocumentType>(request);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return null;
            }
            DocumentTypesReport p = new DocumentTypesReport();
            p.DataSource = resp.Items;
            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            return p;



        }
        [DirectMethod]
        public object ValidateSave(bool isPhantom, string obj, JsonObject values)
        {



            if (!values.ContainsKey("notes"))
            {
                return new { valid = false, msg = "error in note formating " };
            }
            PostRequest<AdminDocumentNote> req = new PostRequest<AdminDocumentNote>();
            AdminDocumentNote note = JsonConvert.DeserializeObject<List<AdminDocumentNote>>(obj)[0];
            //note.recordId = id;
            note.doId = Convert.ToInt32(currentDocumentId.Text);
            note.notes = values["notes"].ToString();
            //note.notes = 
            //note.userId = _systemService.SessionHelper.GetCurrentUserId();
            //note.rowId= values["rowId"].ToString();
            //note.seqNo = values["seqNo"].ToString(); 

            req.entity = note;

            PostResponse<AdminDocumentNote> resp = _administrationService.ChildAddOrUpdate<AdminDocumentNote>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return new { valid = false };
            }
            DocumentNotesStore.Reload();
            return new { valid = true };
        }


        #region comboboxFilling
        private void FillDepartment()
        {
            DepartmentListRequest departmentsRequest = new DepartmentListRequest();
            departmentsRequest.type = 0;
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private string GetNameFormat()
        {
            string format = "";
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "nameFormat";
            RecordResponse<KeyValuePair<string, string>> r = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!r.Success)
            {
                Common.errorMessage(r); ;
                return null;
            }
            format = r.result.Value;
            if (string.IsNullOrEmpty(r.result.Value))
            {

                PostRequest<KeyValuePair<string, string>> request = new PostRequest<KeyValuePair<string, string>>();
                request.entity = new KeyValuePair<string, string>("nameFormat", "{firstName} {lastName}");
                PostResponse<KeyValuePair<string, string>> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>>(request);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return null;
                }
                format = "{firstName} {lastName}";

            }


            string paranthized = format;
            paranthized = paranthized.Replace('{', ' ');
            paranthized = paranthized.Replace('}', ',');
            paranthized = paranthized.Substring(0, paranthized.Length - 1);
            paranthized = paranthized.Replace(" ", string.Empty);
            return paranthized;

        }
        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();

            req.DepartmentId = "0";

            req.BranchId = "0";
            req.IncludeIsInactive = 1;
            req.SortBy = GetNameFormat();

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;




            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }
        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<Employee> data = GetEmployeesFiltered(prms.Query);
            if (data == null)
                data = new List<Employee>();
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
            //};

        }
        protected void addDepartment(object sender, DirectEventArgs e)
        {
            Department dept = new Department();
            dept.name = departmentId.Text;

            PostRequest<Department> depReq = new PostRequest<Department>();
            depReq.entity = dept;
            PostResponse<Department> response = _companyStructureService.ChildAddOrUpdate<Department>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillDepartment();
                departmentId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(response);
                return;
            }

        }

        #endregion



        protected void SaveDXGrid(object sender, DirectEventArgs e)
        {
            string itemsString = e.ExtraParams["items"];
            List<AdminDocumentDX> items = JsonConvert.DeserializeObject<List<AdminDocumentDX>>(itemsString);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].priority = i + 1;

            }

            PostRequest<AdminDocumentDX[]> req2 = new PostRequest<AdminDocumentDX[]>();
            req2.entity = items.ToArray<AdminDocumentDX>();
            PostResponse<AdminDocumentDX[]> resp2 = _administrationService.ChildAddOrUpdate<AdminDocumentDX[]>(req2);
            if (!resp2.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp2); ;
                return;
            }
            DXStore.Reload();

        }
    }
}