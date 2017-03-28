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
    public partial class Legals : System.Web.UI.Page
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


        protected void PoPuPRW(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<EmployeeRightToWork> response = _employeeService.ChildGetRecord<EmployeeRightToWork>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.EditRWForm.SetValues(response.result);
                    FillRWDocumentType();
                    dtId.Select(response.result.dtId.ToString());

                    this.EditRWwindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRWwindow.Show();
                    break;
                case "ColBCName":
                    RecordRequest r2 = new RecordRequest();
                    r2.RecordID = id.ToString();
                    RecordResponse<EmployeeBackgroundCheck> response2 = _employeeService.ChildGetRecord<EmployeeBackgroundCheck>(r2);
                    if (!response2.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response2.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.EditBCTab.SetValues(response2.result);
                    FillBCCheckType();
                    ctId.Select(response2.result.ctId.ToString());
                    this.EditRWwindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRWwindow.Show();
                    break;

                case "ColBCDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteBC({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRW({0})", id),
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


        protected void PoPuPBC(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {

                case "imgEdit":
                    RecordRequest r2 = new RecordRequest();
                    r2.RecordID = id.ToString();
                    RecordResponse<EmployeeBackgroundCheck> response2 = _employeeService.ChildGetRecord<EmployeeBackgroundCheck>(r2);
                    if (!response2.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response2.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.EditBCTab.SetValues(response2.result);
                    FillBCCheckType();
                    ctId.Select(response2.result.ctId.ToString());
                    this.EditBCWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditBCWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteBC({0})", id),
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



        [DirectMethod]
        public void DeleteRW(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeRightToWork n = new EmployeeRightToWork();
                n.recordId = index;
                n.remarks = "";
                n.employeeId = 0;
                n.expiryDate = DateTime.Now;
                n.issueDate = DateTime.Now;
                n.documentRef = "";


                PostRequest<EmployeeRightToWork> req = new PostRequest<EmployeeRightToWork>();
                req.entity = n;
                PostResponse<EmployeeRightToWork> res = _employeeService.ChildDelete<EmployeeRightToWork>(req);
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
                    rightToWorkStore.Remove(index);

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
        public void DeleteBC(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeBackgroundCheck n = new EmployeeBackgroundCheck();
                n.recordId = index;
                n.employeeId = 0;
                n.date = DateTime.Now;
                n.expiryDate = DateTime.Now;
                n.remarks = "";


                PostRequest<EmployeeBackgroundCheck> req = new PostRequest<EmployeeBackgroundCheck>();
                req.entity = n;
                PostResponse<EmployeeBackgroundCheck> res = _employeeService.ChildDelete<EmployeeBackgroundCheck>(req);
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
                    BCStore.Remove(index);

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
        protected void ADDNewRW(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            EditRWForm.Reset();
            this.EditRWwindow.Title = Resources.Common.AddNewRecord;
            FillRWDocumentType();
            rwIssueDate.SelectedDate = DateTime.Today;
            rwExpiryDate.SelectedDate = DateTime.Today;
            this.EditRWwindow.Show();
        }
        protected void ADDNewBC(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            EditBCTab.Reset();
            this.EditBCWindow.Title = Resources.Common.AddNewRecord;
            FillBCCheckType();
            //FillDepartment();
            //FillBranch();
            //FillPosition();
            //.SelectedDate = DateTime.Today;
            DateField1.SelectedDate = DateTime.Now;
            DateField2.SelectedDate = DateTime.Now;

            this.EditBCWindow.Show();
        }



        protected void rightToWork_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeRightToWorkListRequest request = new EmployeeRightToWorkListRequest();

            request.EmployeeId = CurrentEmployee.Text;
            
            ListResponse<EmployeeRightToWork> currencies = _employeeService.ChildGetAll<EmployeeRightToWork>(request);
            if (!currencies.Success)
                X.Msg.Alert(Resources.Common.Error, currencies.Summary).Show();
            this.rightToWorkStore.DataSource = currencies.Items;
            e.Total = currencies.count;

            this.rightToWorkStore.DataBind();
        }
        protected void BCStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;

            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeBackgroundCheckListRequest request = new EmployeeBackgroundCheckListRequest();

            request.EmployeeId = CurrentEmployee.Text;

            ListResponse<EmployeeBackgroundCheck> currencies = _employeeService.ChildGetAll<EmployeeBackgroundCheck>(request);
            if (!currencies.Success)
                X.Msg.Alert(Resources.Common.Error, currencies.Summary).Show();
            this.BCStore.DataSource = currencies.Items;
            e.Total = currencies.count;

            this.BCStore.DataBind();
        }

        protected void SaveRW(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            EmployeeRightToWork b = JsonConvert.DeserializeObject<EmployeeRightToWork>(obj);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            // Define the object to add or edit as null
            b.dtName = dtId.SelectedItem.Text;
            b.issueDate = new DateTime(b.issueDate.Year, b.issueDate.Month, b.issueDate.Day, 14, 0, 0);
            b.expiryDate = new DateTime(b.expiryDate.Year, b.expiryDate.Month, b.expiryDate.Day, 14, 0, 0);
            //b.remarks = 

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<EmployeeRightToWork> request = new PostRequest<EmployeeRightToWork>();
                    request.entity = b;
                    PostResponse<EmployeeRightToWork> r = _employeeService.ChildAddOrUpdate<EmployeeRightToWork>(request);
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
                        this.rightToWorkStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditRWwindow.Close();
                        RowSelectionModel sm = this.rightToWorkGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<EmployeeRightToWork> request = new PostRequest<EmployeeRightToWork>();
                    request.entity = b;
                    PostResponse<EmployeeRightToWork> r = _employeeService.ChildAddOrUpdate<EmployeeRightToWork>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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


                        ModelProxy record = this.rightToWorkStore.GetById(index);
                        record.Set("issueDate", b.issueDate.ToShortDateString());
                        record.Set("expiryDate", b.expiryDate.ToShortDateString());
                        record.Set("remarks", b.remarks);
                        record.Set("documentRef", b.documentRef);
                        EditRWForm.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditRWwindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }

        protected void SaveBC(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            EmployeeBackgroundCheck b = JsonConvert.DeserializeObject<EmployeeBackgroundCheck>(obj);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            b.date = new DateTime(b.date.Year, b.date.Month, b.date.Day, 14, 0, 0);
            b.expiryDate = new DateTime(b.expiryDate.Year, b.expiryDate.Month, b.expiryDate.Day, 14, 0, 0);
            
            if (ctId.SelectedItem != null)
                b.ctName = ctId.SelectedItem.Text;
           
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<EmployeeBackgroundCheck> request = new PostRequest<EmployeeBackgroundCheck>();
                    request.entity = b;
                    PostResponse<EmployeeBackgroundCheck> r = _employeeService.ChildAddOrUpdate<EmployeeBackgroundCheck>(request);
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
                        this.BCStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditBCWindow.Close();
                        RowSelectionModel sm = this.BackgroundCheckGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<EmployeeBackgroundCheck> request = new PostRequest<EmployeeBackgroundCheck>();
                    request.entity = b;
                    PostResponse<EmployeeBackgroundCheck> r = _employeeService.ChildAddOrUpdate<EmployeeBackgroundCheck>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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


                        ModelProxy record = this.BCStore.GetById(index);
                        EditBCTab.UpdateRecord(record);
                        record.Set("ctName", b.ctName);

                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditBCWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
            //if (b.Date.Date == DateTime.Today)
            //{
            //    X.Call("parent.FillLeftPanel", b.departmentName + "<br/>", b.branchName + "<br/>", b.positionName + "<br/>");
            //    X.Call("parent.SelectJICombos", b.departmentId, b.branchId, b.positionId, b.divisionId);
            //}

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


        private void FillRWDocumentType()
        {
            ListRequest RWDocumentType = new ListRequest();
            ListResponse<DocumentType> resp = _employeeService.ChildGetAll<DocumentType>(RWDocumentType);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            RWDocumentTypeStore.DataSource = resp.Items;
            RWDocumentTypeStore.DataBind();

        }

        private void FillBCCheckType()
        {
            ListRequest BCCheckType = new ListRequest();
            ListResponse<CheckType> resp = _employeeService.ChildGetAll<CheckType>(BCCheckType);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            checkTypeStore.DataSource = resp.Items;
            checkTypeStore.DataBind();

        }

        protected void addDocumentType(object sender, DirectEventArgs e)
        {
            DocumentType dept = new DocumentType();
            dept.name = dtId.Text;

            PostRequest<DocumentType> depReq = new PostRequest<DocumentType>();
            depReq.entity = dept;

            PostResponse<DocumentType> response = _employeeService.ChildAddOrUpdate<DocumentType>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                RWDocumentTypeStore.Insert(0, dept);
                dtId.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }


        protected void addCheckType(object sender, DirectEventArgs e)
        {
            //DocumentType dept = new DocumentType();
            //dept.name = dtName.Text;

            //PostRequest<DocumentType> depReq = new PostRequest<DocumentType>();
            //depReq.entity = dept;

            //PostResponse<DocumentType> response = _employeeService.ChildAddOrUpdate<DocumentType>(depReq);
            //if (response.Success)
            //{
            //    dept.recordId = response.recordId;
            //    RWDocumentTypeStore.Insert(0, dept);
            //    dtId.Select(0);
            //}
            //else
            //{
            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //    X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
            //    return;
            //}

        }













    }
}