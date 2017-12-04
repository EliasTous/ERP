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
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;

namespace AionHR.Web.UI.Forms
{
    public partial class Cases : System.Web.UI.Page
    {


        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = GetNameFormat();

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }

        private string GetNameFormat()
        {
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "nameFormat";
            RecordResponse<KeyValuePair<string, string>> response = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!response.Success)
            {

            }
            string paranthized = response.result.Value;
            paranthized = paranthized.Replace('{', ' ');
            paranthized = paranthized.Replace('}', ',');
            paranthized = paranthized.Substring(0, paranthized.Length - 1);
            paranthized = paranthized.Replace(" ", string.Empty);
            return paranthized;

        }

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();

        ICaseService _caseService = ServiceLocator.Current.GetInstance<ICaseService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();


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

             
                statusPref.Select(0);
                dateCol.Format = _systemService.SessionHelper.GetDateformat() + ": hh:mm:ss";
                CasesClassId.Text = ClassId.CMCA.ToString();

                date.Format = colDate.Format = colClosedDate.Format = closedDate.Format = _systemService.SessionHelper.GetDateformat();
                if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                    return;

                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Case), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                try
                   
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(CaseComment), null, caseCommentGrid, null, Button1);
                    ApplyAccessControlOnCaseComments();
                }
                catch (AccessDeniedException exp)
                {
                   
                    caseCommentsTab.Hidden = true;
                   
                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Attachement), EditDocumentForm, filesGrid, Button2, SaveDocumentButton);
                    var properties = AccessControlApplier.GetPropertiesLevels(typeof(Attachement));
                    if (properties.Where(x => x.index == "url").ToList()[0].accessLevel == 0)
                    {
                        var s = filesGrid.ColumnModel.Columns[filesGrid.ColumnModel.Columns.Count - 1];
                        s.Renderer.Handler = s.Renderer.Handler.Replace("attachRender()", "' '");
                    }
                }
                catch (AccessDeniedException exp)
                {
                    
                    filesGrid.Hidden = true;
                    
                }
               
               

                if (details.InputType == InputType.Password)
                {
                    details.Visible = false;
                    detailsField.Visible = true;
                }
            }

        }

        private void ApplyAccessControlOnCaseComments()
        {
            var properties = AccessControlApplier.GetPropertiesLevels(typeof(CaseComment));
            foreach (var item in properties)
            {
                if (item.propertyId == "4300103")
                {
                    if (item.accessLevel < 2)
                        caseCommentGrid.ColumnModel.Columns[caseCommentGrid.ColumnModel.Columns.Count - 1].Renderer.Handler = " return '';";
                }

                if (item.accessLevel == 0)
                {
                    if (item.propertyId == "4300102")
                    {
                        caseCommentGrid.ColumnModel.Columns[1].Renderer.Handler = caseCommentGrid.ColumnModel.Columns[1].Renderer.Handler.Replace("s.calendar()", "'***** '");
                    }
                    else
                    {
                        var indices = typeof(CaseComment).GetProperties().Where(x =>
                        {
                            var d = x.GetCustomAttributes(typeof(PropertyID), false);
                            if (d.Count() == 0)
                                return false;
                            return (x.GetCustomAttributes(typeof(PropertyID), false).ToList()[0] as PropertyID).ID == item.propertyId;
                        }).ToList();

                        indices.ForEach(x =>
                        {
                            caseCommentGrid.ColumnModel.Columns[1].Renderer.Handler = caseCommentGrid.ColumnModel.Columns[1].Renderer.Handler.Replace("record.data['" + x.Name + "']", "'***** '");
                        });
                    }

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



        protected void PoPuP(object sender, DirectEventArgs e)
        {

            panelRecordDetails.ActiveIndex = 0;
            SetTabPanelEnable(true);
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string empId = e.ExtraParams["empId"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 

                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<Case> response = _caseService.Get<Case>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
                        return;
                    }

                    currentCase.Text = id;

                    employeeId.GetStore().Add(new object[]
                       {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName.fullName
                                }
                       });
                    employeeId.SetValue(response.result.employeeId);

                    FillFilesStore(Convert.ToInt32(id));

                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    caseComments_RefreshData(Convert.ToInt32(id));
                    if (!response.result.closedDate.HasValue)
                        closedDate.SelectedDate = DateTime.Now;
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

                case "colAttach":

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }

        protected void PoPuPCase(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string index = e.ExtraParams["index"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    X.Call("App.caseCommentGrid.editingPlugin.startEdit", Convert.ToInt32(index), 0);
                    break;


                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteCase({0})", id),
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
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Case s = new Case();
                s.recordId = index;
                s.employeeId = 0;
                s.employeeName = new EmployeeName();
                s.details = "";
                s.date = DateTime.Now;
                s.closedDate = DateTime.Now;
                s.status = 0;


                PostRequest<Case> req = new PostRequest<Case>();
                req.entity = s;
                PostResponse<Case> r = _caseService.Delete<Case>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: "+r.ErrorCode + "<br> Summary: "+r.Summary : r.Summary).Show();
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
        public void DeleteCase(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                CaseComment s = new CaseComment();
                s.caseId = Convert.ToInt32(currentCase.Text);
                s.comment = "";
                s.seqNo = Convert.ToInt16(index);
                s.userId = 0;
                s.userName = "";
                s.date = DateTime.Now;



                PostRequest<CaseComment> req = new PostRequest<CaseComment>();
                req.entity = s;
                PostResponse<CaseComment> r = _caseService.ChildDelete<CaseComment>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: "+r.ErrorCode + "<br> Summary: "+r.Summary : r.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    caseCommentStore.Remove(index);

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
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {
            caseCommentStore.DataSource = new List<CaseComment>();
            caseCommentStore.DataBind();
            //Reset all values of the relative object
            BasicInfoTab.Reset();
            closedDate.SelectedDate = DateTime.Now;

            panelRecordDetails.ActiveIndex = 0;
            SetTabPanelEnable(false);
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }

        protected void caseComments_RefreshData(int cId)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            CaseCommentsListRequest req = new CaseCommentsListRequest();
            req.caseId = cId;
            ListResponse<CaseComment> notes = _caseService.ChildGetAll<CaseComment>(req);
            if (!notes.Success)
            {
                //   X.Msg.Alert(Resources.Common.Error, notes.Summary).Show();
            }
            this.caseCommentStore.DataSource = notes.Items;


            this.caseCommentStore.DataBind();
        }

        protected void ADDNewRecordComments(object sender, DirectEventArgs e)
        {
            string noteText = e.ExtraParams["noteText"];
            X.Call("ClearNoteText");
            PostRequest<CaseComment> req = new PostRequest<CaseComment>();
            CaseComment note = new CaseComment();
            note.recordId = null;
            note.comment = noteText;
            note.date = DateTime.Now;
            note.caseId = Convert.ToInt32(currentCase.Text);
            req.entity = note;


            PostResponse<CaseComment> resp = _caseService.ChildAddOrUpdate<CaseComment>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();

            }
            caseComments_RefreshData(Convert.ToInt32(currentCase.Text));

            //Reset all values of the relative object

        }

        private CaseManagementListRequest GetCaseManagementRequest()
        {
            CaseManagementListRequest req = new CaseManagementListRequest();

            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
            req.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value : 0;
            if (!string.IsNullOrEmpty(statusPref.Text) && statusPref.Value.ToString() != "")
            {
                req.Status = Convert.ToInt32(statusPref.Value);
            }
            else
            {
                req.Status = 0;
            }
            req.Size = "30";
            req.StartAt = "1";
            req.Filter = "";
            req.SortBy = "employeeId";
            req.EmployeeId = 0;
            return req;
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            //ListRequest request = new ListRequest();
            CaseManagementListRequest request = GetCaseManagementRequest();
            request.StartAt = e.Start.ToString();
            request.Size = e.Limit.ToString();
            ListResponse<Case> routers = _caseService.GetAll<Case>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() : routers.Summary).Show();
                return;
            }
            this.Store1.DataSource = routers.Items;
            e.Total = routers.count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            Case b = JsonConvert.DeserializeObject<Case>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            b.employeeName = new EmployeeName();
            if (employeeId.SelectedItem != null)

                b.employeeName.fullName = employeeId.SelectedItem.Text;

            if (closedDate.ReadOnly)
                b.closedDate = null;
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Case> request = new PostRequest<Case>();

                    request.entity = b;

                    PostResponse<Case> r = _caseService.AddOrUpdate<Case>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: "+r.ErrorCode + "<br> Summary: "+r.Summary : r.Summary).Show();
                        return;
                    }
                    else
                    {
                        b.recordId = r.recordId;

                        //Add this record to the store 
                        this.Store1.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                        recordId.Text = b.recordId;
                        SetTabPanelEnable(true);
                        currentCase.Text = b.recordId;
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
                    PostRequest<Case> request = new PostRequest<Case>();
                    request.entity = b;
                    PostResponse<Case> r = _caseService.AddOrUpdate<Case>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: "+r.ErrorCode + "<br> Summary: "+r.Summary : r.Summary).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.Store1.GetById(id);
                        BasicInfoTab.UpdateRecord(record);

                        if (closedDate.ReadOnly)
                            record.Set("closedDate", null);
                        record.Set("employeeName", b.employeeName);
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


       

        [DirectMethod]
        public object ValidateSave(bool isPhantom, string obj, JsonObject values)
        {


            if (!values.ContainsKey("comment"))
            {
                return new { valid = false, msg = "Error in call" };
            }

            PostRequest<CaseComment> req = new PostRequest<CaseComment>();
            CaseComment note = JsonConvert.DeserializeObject<List<CaseComment>>(obj)[0];
            //note.recordId = id;
            note.caseId = Convert.ToInt32(currentCase.Text);
            note.comment = values["comment"].ToString();
            int bulk;

            req.entity = note;

            PostResponse<CaseComment> resp = _caseService.ChildAddOrUpdate<CaseComment>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return new { valid = false };
            }
            caseComments_RefreshData(note.caseId);
            return new { valid = true };
        }

        private void SetTabPanelEnable(bool isEnable)
        {
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.ID == "BasicInfoTab")
                    continue;
                item.Disabled = !isEnable;
            }

        }

        #region AttachmentManagement
        [DirectMethod]
        public void FillFilesStore(int caseId)
        {
            //ListRequest request = new ListRequest();
            CaseAttachmentsListRequest request = new CaseAttachmentsListRequest();
            request.recordId = caseId;

            ListResponse<Attachement> routers = _systemService.ChildGetAll<Attachement>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() : routers.Summary).Show();
                return;
            }
            this.filesStore.DataSource = routers.Items;


            this.filesStore.DataBind();
        }

        private List<SystemFolder> GetFolders()
        {
            ListRequest req = new ListRequest();
            ListResponse<SystemFolder> docs = _systemService.ChildGetAll<SystemFolder>(req);
            return docs.Items;
        }
        protected void AddAttachments(object sender, DirectEventArgs e)
        {
            ListRequest req = new ListRequest();
            ListResponse<SystemFolder> docs = _systemService.ChildGetAll<SystemFolder>(req);
            if (!docs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, docs.Summary).Show();
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
        protected void PoPuPAttachement(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
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
                            Handler = String.Format("App.direct.DeleteAttachment({0},'{1}')", id, path),
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
                return;
            }

        }

        protected void SaveFolder(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            Attachement b = JsonConvert.DeserializeObject<Attachement>(obj);
            b.recordId = Convert.ToInt32(currentCase.Text);
            b.seqNo = Convert.ToInt16(id);
            b.fileName = e.ExtraParams["fileName"];
            b.classId = ClassId.CMCA;
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
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: "+r.ErrorCode + "<br> Summary: "+r.Summary : r.Summary).Show();
                    return;
                }
                else
                {


                    ModelProxy record = this.filesStore.GetById(id);

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



        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteAttachment(string index, string path)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Attachement n = new Attachement();
                n.classId = ClassId.CMCA;
                n.recordId = Convert.ToInt32(currentCase.Text);
                n.seqNo = Convert.ToInt16(index);
                n.url = path;

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
                    filesStore.Remove(index);

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
        #endregion
    }
}