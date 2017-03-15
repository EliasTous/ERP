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
    public partial class JobInformation : System.Web.UI.Page
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
                case "ColEHName":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<EmploymentHistory> response = _employeeService.ChildGetRecord<EmploymentHistory>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.EditEHForm.SetValues(response.result);
                    FillEHStatus();
                    statusId.Select(response.result.statusId.ToString());

                    this.EditEHwindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditEHwindow.Show();
                    break;
                case "ColJIName":
                    RecordRequest r2 = new RecordRequest();
                    r2.RecordID = id.ToString();
                    RecordResponse<JobInfo> response2 = _employeeService.ChildGetRecord<JobInfo>(r2);
                    if (!response2.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response2.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.EditJobInfoTab.SetValues(response2.result);
                    FillDepartment();
                    FillBranch();
                    FillDivision();
                    FillPosition();
                    departmentId.Select(response2.result.departmentId.ToString());
                    branchId.Select(response2.result.branchId.ToString());
                    positionId.Select(response2.result.positionId.ToString());
                    divisionId.Select(response2.result.divisionId.ToString());
                    this.EditJobInfoWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditJobInfoWindow.Show();
                    break;
                case "ColSAName":
                    RecordRequest r3 = new RecordRequest();
                    r3.RecordID = id.ToString();
                    RecordResponse<EmployeeSalary> response3 = _employeeService.ChildGetRecord<EmployeeSalary>(r3);
                    if (!response3.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response3.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.EditSAForm.SetValues(response3.result);
                    FillCurrency();
                    FillScr();
                    currencyId.Select(response3.result.currencyId.ToString());
                    scrId.Select(response3.result.scrId.ToString());
                    this.EditSAWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditSAWindow.Show();
                    break;
                case "ColJIDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteJI{0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;
                case "ColSADelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteSA({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;
                case "ColEHDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteEH({0})", id),
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
        public void DeleteEH(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmploymentHistory n = new EmploymentHistory();
                n.recordId = index;
                n.comment = "";
                n.employeeId = 0;
                n.date = DateTime.Now;
                n.statusId = 0;


                PostRequest<EmploymentHistory> req = new PostRequest<EmploymentHistory>();
                req.entity = n;
                PostResponse<EmploymentHistory> res = _employeeService.ChildDelete<EmploymentHistory>(req);
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
                    employeementHistoryStore.Remove(index);

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
        public void DeleteJI(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                JobInfo n = new JobInfo();
                n.recordId = index;
                n.positionId = 0;
                n.branchId = 0;
                n.departmentId = 0;
                n.divisionId = 0;
                n.employeeId = 0;
                n.date = DateTime.Now;
                n.notes = " ";


                PostRequest<JobInfo> req = new PostRequest<JobInfo>();
                req.entity = n;
                PostResponse<JobInfo> res = _employeeService.ChildDelete<JobInfo>(req);
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
                    JIStore.Remove(index);

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
        public void DeleteSA(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeSalary n = new EmployeeSalary();
                n.recordId = index;
                n.isTaxable = 0;
                n.finalAmount = 0;
                n.employeeId = 0;
                n.comments = "";
                n.accountNumber = "";
                n.basicAmount = 0;
                n.currencyId = 0;
                n.effectiveDate = DateTime.Now;
                n.paymentFrequency = n.paymentMethod = n.salaryType = 0;
                n.scrId = 0;


                PostRequest<EmployeeSalary> req = new PostRequest<EmployeeSalary>();
                req.entity = n;
                PostResponse<EmployeeSalary> res = _employeeService.ChildDelete<EmployeeSalary>(req);
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
                    SAStore.Remove(index);

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
        protected void ADDNewEH(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            EditEHForm.Reset();
            this.EditEHwindow.Title = Resources.Common.AddNewRecord;
            FillEHStatus();

            this.EditEHwindow.Show();
        }
        protected void ADDNewJI(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            EditJobInfoTab.Reset();
            this.EditJobInfoWindow.Title = Resources.Common.AddNewRecord;
            FillDivision();
            FillDepartment();
            FillBranch();
            FillPosition();

            this.EditJobInfoWindow.Show();
        }

        protected void ADDNewSA(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            EditSAForm.Reset();
            this.EditSAWindow.Title = Resources.Common.AddNewRecord;
            FillCurrency();
            FillScr();

            this.EditSAWindow.Show();
        }

        protected void employeementHistory_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeementHistoryListRequest request = new EmployeementHistoryListRequest();

            request.Filter = "";
            request.employeeId = CurrentEmployee.Text;
            request.BranchId = "0";
            request.DepartmentId = "0";
            request.divisionId = "0";
            request.positionId = "0";
            request.Filter = "";
            request.SortBy = "firstName,lastName";
            request.Size = "50";
            request.StartAt = "1";
            ListResponse<EmploymentHistory> currencies = _employeeService.ChildGetAll<EmploymentHistory>(request);
            if (!currencies.Success)
                X.Msg.Alert(Resources.Common.Error, currencies.Summary).Show();
            this.employeementHistoryStore.DataSource = currencies.Items;
            e.Total = currencies.count;

            this.employeementHistoryStore.DataBind();
        }
        protected void JIStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            JobInfoListRequest request = new JobInfoListRequest();
            request.Filter = "";
            request.employeeId = CurrentEmployee.Text;
            request.BranchId = "0";
            request.DepartmentId = "0";
            request.divisionId = "0";
            request.positionId = "0";
            request.Filter = "";
            request.SortBy = "firstName,lastName";
            request.Size = "50";
            request.StartAt = "1";
            ListResponse<JobInfo> currencies = _employeeService.ChildGetAll<JobInfo>(request);
            if (!currencies.Success)
                X.Msg.Alert(Resources.Common.Error, currencies.Summary).Show();
            this.JIStore.DataSource = currencies.Items;
            e.Total = currencies.count;

            this.JIStore.DataBind();
        }

        protected void SAStore_Refresh(object sender, StoreReadDataEventArgs e)
        {
            EmployeeSalaryListRequest request = new EmployeeSalaryListRequest();
            request.Filter = "";
            request.EmployeeId = CurrentEmployee.Text;

            request.Filter = "";

            request.Size = "50";
            request.StartAt = "1";
            ListResponse<EmployeeSalary> currencies = _employeeService.ChildGetAll<EmployeeSalary>(request);
            if (!currencies.Success)
                X.Msg.Alert(Resources.Common.Error, currencies.Summary).Show();
            this.SAStore.DataSource = currencies.Items;
            e.Total = currencies.count;

            this.SAStore.DataBind();
        }



        protected void SaveEH(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            EmploymentHistory b = JsonConvert.DeserializeObject<EmploymentHistory>(obj);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<EmploymentHistory> request = new PostRequest<EmploymentHistory>();
                    request.entity = b;
                    PostResponse<EmploymentHistory> r = _employeeService.ChildAddOrUpdate<EmploymentHistory>(request);
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
                        this.employeementHistoryStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditEHwindow.Close();
                        RowSelectionModel sm = this.employeementHistoryGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<EmploymentHistory> request = new PostRequest<EmploymentHistory>();
                    request.entity = b;
                    PostResponse<EmploymentHistory> r = _employeeService.ChildAddOrUpdate<EmploymentHistory>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.employeementHistoryStore.GetById(index);
                        EditEHForm.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditEHwindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }

        protected void SaveJI(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            JobInfo b = JsonConvert.DeserializeObject<JobInfo>(obj);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            if (branchId.SelectedItem != null)
                b.branchName = branchId.SelectedItem.Text;
            if (departmentId.SelectedItem != null)
                b.departmentName = departmentId.SelectedItem.Text;
            if (positionId.SelectedItem != null)
                b.positionName = positionId.SelectedItem.Text;
            if (divisionId.SelectedItem != null)
                b.divisionName = positionId.SelectedItem.Text;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<JobInfo> request = new PostRequest<JobInfo>();
                    request.entity = b;
                    PostResponse<JobInfo> r = _employeeService.ChildAddOrUpdate<JobInfo>(request);
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
                        this.JIStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditJobInfoWindow.Close();
                        RowSelectionModel sm = this.JobInfoGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<JobInfo> request = new PostRequest<JobInfo>();
                    request.entity = b;
                    PostResponse<JobInfo> r = _employeeService.ChildAddOrUpdate<JobInfo>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.JIStore.GetById(index);
                        EditJobInfoTab.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditJobInfoWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }

        protected void SaveSA(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            EmployeeSalary b = JsonConvert.DeserializeObject<EmployeeSalary>(obj);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            if (currencyId.SelectedItem != null)
                b.currencyName = currencyId.SelectedItem.Text;
            if (scrId.SelectedItem != null)
                b.scrName = scrId.SelectedItem.Text;
            if (!b.isTaxable.HasValue)
                b.isTaxable = 0;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<EmployeeSalary> request = new PostRequest<EmployeeSalary>();
                    request.entity = b;
                    PostResponse<EmployeeSalary> r = _employeeService.ChildAddOrUpdate<EmployeeSalary>(request);
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
                        this.SAStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditSAWindow.Close();
                        RowSelectionModel sm = this.SalaryGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<EmployeeSalary> request = new PostRequest<EmployeeSalary>();
                    request.entity = b;
                    PostResponse<EmployeeSalary> r = _employeeService.ChildAddOrUpdate<EmployeeSalary>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.SAStore.GetById(index);
                        EditSAForm.UpdateRecord(record);
                        record.Set("currencyName", b.currencyName);
                        record.Set("scrName", b.scrName);
                        record.Set("effectiveDate", b.effectiveDate.ToShortDateString());
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditSAWindow.Close();


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

        private void FillEHStatus()
        {
            ListRequest EHStatus = new ListRequest();
            ListResponse<EmploymentStatus> resp = _employeeService.ChildGetAll<EmploymentStatus>(EHStatus);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            EHStatusStore.DataSource = resp.Items;
            EHStatusStore.DataBind();

        }

        private void FillPosition()
        {
            ListRequest positionsRequest = new ListRequest();
            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(positionsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            positionStore.DataSource = resp.Items;
            positionStore.DataBind();
        }
        private void FillDepartment()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            BranchStore.DataSource = resp.Items;
            BranchStore.DataBind();
        }
        private void FillDivision()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();
        }

        private void FillCurrency()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Currency> resp = _systemService.ChildGetAll<Currency>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            currencyStore.DataSource = resp.Items;
            currencyStore.DataBind();
        }

        private void FillScr()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<SalaryChangeReason> resp = _employeeService.ChildGetAll<SalaryChangeReason>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            scrStore.DataSource = resp.Items;
            scrStore.DataBind();
        }


    }
}