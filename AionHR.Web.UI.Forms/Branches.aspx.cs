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
using AionHR.Infrastructure.Session;
using AionHR.Model.System;
using AionHR.Model.Employees.Profile;
using AionHR.Infrastructure.JSON;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;
using AionHR.Model.Attendance;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms
{
    public partial class Branches : System.Web.UI.Page
    {
        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
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
                FillSchedules();
                FillWorkingCalendar();
                releaseDate.Format = expiryDate.Format = _systemService.SessionHelper.GetDateformat();



                if (_systemService.SessionHelper.CheckIfIsAdmin())
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Branch), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                    ApplyAccessControlOnAddress();
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
              
            }


        }

        private void ApplyAccessControlOnAddress()
        {
            
            
            var properties = AccessControlApplier.GetPropertiesLevels(typeof(Branch));
            var level = properties.Where(x =>

                x.propertyId == "2102005"
           ).ToList()[0].accessLevel;
            
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
                default: break;
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
                    RecordResponse<Branch> response = _branchService.ChildGetRecord<Branch>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    branchId.Text = response.result.recordId;
                    managerId.Disabled = false;

                    if (response.result.managerId != "0")
                    {

                        managerId.GetStore().Add(new object[]
                           {
                                new
                                {
                                    recordId = response.result.managerId,
                                    fullName =response.result.managerName.fullName
                                }
                           });
                        managerId.SetValue(response.result.managerId);

                    }

                    legalReferenceStore.Reload();
                    this.BasicInfoTab.SetValues(response.result);
                    if (response.result.managerId == "0")
                        managerId.Text = "";
                        //  timeZoneCombo.Select(response.result.timeZone.ToString());
                        FillNationality();
                    FillState();
                    naId.Select(response.result.address.countryId);
                    stId.Select(response.result.address.stateId);
                    address.Text = response.result.address.recordId;
                    addressForm.SetValues(response.result.address);
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
                Branch n = new Branch();
                n.recordId = index;
                n.name = "";
                n.branchRef = "";
                n.timeZone = 0;

                PostRequest<Branch> req = new PostRequest<Branch>();
                req.entity = n;
                PostResponse<Branch> res = _branchService.ChildDelete<Branch>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(res);
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
        public void DeleteLegalReferenceRecord(string index,string branchId)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                if (string.IsNullOrEmpty(index) || string.IsNullOrEmpty(branchId))
                    return;
                LegalReference n = new LegalReference();
                n.branchId = Convert.ToInt32(branchId);
                n.goId = Convert.ToInt32(index);

                PostRequest<LegalReference> req = new PostRequest<LegalReference>();
                req.entity = n;
                PostResponse<LegalReference> res = _branchService.ChildDelete<LegalReference>(req);
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
                    legalReferenceStore.Reload();

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

            //Reset all values of the relative object
            BasicInfoTab.Reset();
            managerId.Disabled = true;
            branchId.Text = "";
            addressForm.Reset();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            FillNationality();
            FillState();

            //timeZoneCombo.Select(timeZoneOffset.Text);
            this.EditRecordWindow.Show();
        }

       
      
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Branch> branches = _branchService.ChildGetAll<Branch>(request);
            if (!branches.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString()+"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+ branches.LogId : branches.Summary).Show();
                return;
            }
            this.Store1.DataSource = branches.Items;
            e.Total = branches.count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            string addr = e.ExtraParams["address"];
            Branch b = JsonConvert.DeserializeObject<Branch>(obj);

            b.managerId = managerId.Value.ToString(); 

            b.isInactive = isInactive.Checked;
            b.recordId = id;
            // Define the object to add or edit as null
            CustomResolver res = new CustomResolver();
            res.AddRule("naId", "countryId");
            res.AddRule("stId", "stateId");
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = res;
            b.caName = caId.SelectedItem.Text;
            AddressBook add = JsonConvert.DeserializeObject<AddressBook>(addr, settings);
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
                b.address = JsonConvert.DeserializeObject<AddressBook>(addr, settings);
                b.address.recordId = address.Text;
            }

            if (string.IsNullOrEmpty(branchId.Text))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Branch> request = new PostRequest<Branch>();
                    request.entity = b;
                    PostResponse<Branch> r = _branchService.ChildAddOrUpdate<Branch>(request);
                    b.recordId = r.recordId;

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId :r.Summary).Show();
                        return;
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(branchId.Text))
                        {
                            //this.Store1.Insert(0, b);
                          

                            this.EditRecordWindow.Close();
                            //RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                            //sm.DeselectAll();
                            //sm.Select(b.recordId.ToString());
                        }

                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                        //Add this record to the store 
                        managerId.Disabled = false;

                        //Display successful notification
                        branchId.Text = b.recordId;
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
                    int index = Convert.ToInt32(branchId.Text);//getting the id of the record
                    PostRequest<Branch> request = new PostRequest<Branch>();
                    b.recordId = branchId.Text;
                    request.entity = b;
                    PostResponse<Branch> r = _branchService.ChildAddOrUpdate<Branch>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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


                        ModelProxy record = this.Store1.GetById(index);
                        record.Set("caName", b.caName);
                        BasicInfoTab.UpdateRecord(record);
                        
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });

                    //    this.EditRecordWindow.Close();

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
        public void StoreTimeZone(string z)
        {
            timeZoneOffset.Text = z;
        }

        protected void addNA(object sender, DirectEventArgs e)
        {
            Nationality dept = new Nationality();
            dept.name = naId.Text;



            PostRequest<Nationality> depReq = new PostRequest<Nationality>();
            depReq.entity = dept;

            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillNationality();
                naId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        private void FillNationality()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(documentTypes);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + resp.LogId : resp.Summary).Show();
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
                Common.errorMessage(resp);
                return;
            }
            stStore.DataSource = resp.Items;
            stStore.DataBind();

        }

        protected void addST(object sender, DirectEventArgs e)
        {
            State dept = new State();
            dept.name = stId.Text;



            PostRequest<State> depReq = new PostRequest<State>();
            depReq.entity = dept;

            PostResponse<State> response = _systemService.ChildAddOrUpdate<State>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillState();
                stId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        protected void printBtn_Click(object sender, EventArgs e)
        {
            BranchesReport p = GetReport();
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
            BranchesReport p = GetReport();
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
            BranchesReport p = GetReport();
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
        private BranchesReport GetReport()
        {

            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Branch> resp = _branchService.ChildGetAll<Branch>(request);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return null;
            }
            BranchesReport p = new BranchesReport();
            p.DataSource = resp.Items;
            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            p.Parameters["Yes"].Value = Yes.Text;
            p.Parameters["No"].Value = No.Text;
            return p;



        }

        protected void legalReference_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            LegalReferenceListRequest request = new LegalReferenceListRequest();

            request.Filter = "";
            request.branchId = branchId.Text;
            ListResponse<LegalReference> branches = _branchService.ChildGetAll<LegalReference>(request);
            if (!branches.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") +branches.LogId : branches.Summary).Show();
                return;
            }
          
            this.legalReferenceStore.DataSource = branches.Items;
            e.Total = branches.count;

            this.legalReferenceStore.DataBind();
        }
        protected void PoPuPlegalReference(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    LegalReferenceRecordRequest r = new LegalReferenceRecordRequest();
                    r.branchId = branchId.Text;
                    r.goId = id.ToString();
                    RecordResponse<LegalReference> response = _branchService.ChildGetRecord<LegalReference>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    goNameTF.ReadOnly = true;

                    this.legalReferenceForm.SetValues(response.result);
                                      
                    this.EditlegalReferenceWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditlegalReferenceWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteLegalReferenceRecord({0},{1})", id, branchId.Text),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;

                    //case "imgAttach":

                    //    //Here will show up a winow relatice to attachement depending on the case we are working on
                    //    break;
                    //default:
                    //    break;
            }


        }
        protected void saveLegalRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
           
            LegalReference b = JsonConvert.DeserializeObject<LegalReference>(obj);
            b.branchId =Convert.ToInt32( branchId.Text);
          

            // Define the object to add or edit as null


            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                  
                    PostRequest<LegalReference> request = new PostRequest<LegalReference>();
                    request.entity = b;
                    PostResponse<LegalReference> r = _branchService.ChildAddOrUpdate<LegalReference>(request);
                 

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

                        legalReferenceStore.Reload(); 


                        this.EditlegalReferenceWindow.Close();
                     
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                        //Add this record to the store 


                        //Display successful notification
                     



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

                    b.goId= Convert.ToInt32(id);
                    PostRequest<LegalReference> request = new PostRequest<LegalReference>();
                    request.entity = b;
                    PostResponse<LegalReference> r = _branchService.ChildAddOrUpdate<LegalReference>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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

                        legalReferenceStore.Reload(); 

                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });

                        this.EditlegalReferenceWindow.Close();

                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }
        //protected void ADDNewlegalReferenceRecord(object sender, DirectEventArgs e)
        //{

        //    //Reset all values of the relative object
        //    legalReferenceForm.Reset();
        //    goNameTF.ReadOnly = false;



        //    this.EditlegalReferenceWindow.Title = Resources.Common.AddNewRecord;



        //    this.EditlegalReferenceWindow.Show();
        //}
        private void FillSchedules()
        {
            ListRequest vsRequest = new ListRequest();
            ListResponse<AttendanceSchedule> resp = _timeAttendanceService.ChildGetAll<AttendanceSchedule>(vsRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            scheduleStore.DataSource = resp.Items;
            scheduleStore.DataBind();
        }
      
        public void FillWorkingCalendar()
        {
            ListRequest req = new ListRequest();

            ListResponse<WorkingCalendar> response = _timeAttendanceService.ChildGetAll<WorkingCalendar>(req);
            if (!response.Success)
            {
               Common.errorMessage(response);
                Store3.DataSource = new List<Department>();
            }
            Store3.DataSource = response.Items;
            Store3.DataBind();
        }
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
            req.BranchId = branchId.Text;
            req.IncludeIsInactive = 0;
            req.SortBy = GetNameFormat();

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }


        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }



    }
}