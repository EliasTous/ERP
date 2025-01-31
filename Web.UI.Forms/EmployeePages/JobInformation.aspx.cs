﻿using System;
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
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Web.UI.Forms.Utilities;
using Model.Company.News;
using Services.Messaging;
using Model.Company.Structure;
using Model.System;
using Model.Employees.Profile;
using Services.Messaging.System;
using Services.Messaging.CompanyStructure;
using Web.UI.Forms.ConstClasses;

namespace Web.UI.Forms.EmployeePages
{
    public partial class JobInformation : System.Web.UI.Page
    {
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
                {
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                    return;
                }
                CurrentEmployee.Text = Request.QueryString["employeeId"];
                if (string.IsNullOrEmpty(Request.QueryString["hireDate"]))
                {
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.MissingHireDate).Show();
                    Viewport11.Hidden = true;
                    return;
                }
                CurrentHireDate.Text = Request.QueryString["hireDate"];
                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";
                btnAdd.Disabled = Button1.Disabled = SaveEHButton.Disabled = SaveJIButton.Disabled = disabled;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(JobInfo), EditJobInfoTab, JobInfoGrid, Button1, SaveJIButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    JobInfoGrid.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(EmploymentHistory), EditEHForm, employeementHistoryGrid, btnAdd, SaveEHButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    employeementHistoryGrid.Hidden = true;

                }
            }
            Column2.Format = ColDate.Format = ehDate.Format = date.Format = _systemService.SessionHelper.GetDateformat();
            if (comment.InputType == InputType.Password)
            {
                comment.Visible = false;
                commentField.Visible = true;
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



        protected void PoPuPEH(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<EmploymentHistory> response = _employeeService.ChildGetRecord<EmploymentHistory>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
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

                case "ColJIDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteJI({0})", id),
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

        protected void PoPuPJI(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {

                case "imgEdit":
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
                    if (response2.result.reportToId.HasValue)
                    {

                        reportToId.GetStore().Add(new object[]
                           {
                                new
                                {
                                    recordId = response2.result.reportToId,
                                    fullName =response2.result.reportToName
                                }
                           });
                        reportToId.SetValue(response2.result.reportToId);

                    }
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

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteJI({0})", id),
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
                    EHCount.Text = (Convert.ToInt32(EHCount.Text) - 1).ToString();
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
                n.employeeId =Convert.ToInt32( CurrentEmployee.Text);
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
                  
                    JIStore.Reload();

                    //Step 3 : Showing a notification for the user 
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordDeletedSucc
                    });
                    X.Call("parent.refreshQV");
                    X.Call("parent.SetJobInfo", null, null, null, null,null);
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
            if (EHCount.Text == "0")
                ehDate.SelectedDate = DateTime.ParseExact(CurrentHireDate.Text, "yyyy/MM/dd", new CultureInfo("en"));
            else
                ehDate.SelectedDate = DateTime.Today;

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
            date.SelectedDate = DateTime.Today;
            RecordRequest request = new RecordRequest();
            request.RecordID = CurrentEmployee.Text;
            RecordResponse<Employee> qv = _employeeService.Get<Employee>(request);
            departmentId.Select(qv.result.departmentId);
            branchId.Select(qv.result.branchId);
            divisionId.Select(qv.result.divisionId);
            positionId.Select(qv.result.positionId);
            reportToId.Select(qv.result.reportToId);
            if (!string.IsNullOrEmpty(TotalJIRecords.Text))
            {
                if (TotalJIRecords.Text == "0")
                {
                    if (qv.result.hireDate!=null)
                        date.SelectedDate =(DateTime) qv.result.hireDate;

                }

            }
            this.EditJobInfoWindow.Show();
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
            request.StartAt = "0";
            ListResponse<EmploymentHistory> currencies = _employeeService.ChildGetAll<EmploymentHistory>(request);
            if (!currencies.Success)
                X.Msg.Alert(Resources.Common.Error, currencies.Summary).Show();
            this.employeementHistoryStore.DataSource = currencies.Items;
            e.Total = currencies.count;
            EHCount.Text = currencies.count.ToString();
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
            request.StartAt = "0";
            ListResponse<JobInfo> currencies = _employeeService.ChildGetAll<JobInfo>(request);
            if (!currencies.Success)
                X.Msg.Alert(Resources.Common.Error, currencies.Summary).Show();
            this.JIStore.DataSource = currencies.Items;
            e.Total = currencies.count;
            TotalJIRecords.Text = currencies.count.ToString();
            this.JIStore.DataBind();
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
            b.statusName = statusId.SelectedItem.Text;
            b.date = new DateTime(b.date.Year, b.date.Month, b.date.Day, 14, 0, 0);

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
                         Common.errorMessage(r);
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
                        EHCount.Text = (Convert.ToInt32(EHCount.Text) + 1).ToString();


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
                         Common.errorMessage(r);
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.employeementHistoryStore.GetById(index);
                        record.Set("date", b.date.ToShortDateString());
                        record.Set("statusName", b.statusName);
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
            X.Call("parent.refreshQV");
        }

        protected void SaveJI(object sender, DirectEventArgs e)
        {
            try
            {


                //Getting the id to check if it is an Add or an edit as they are managed within the same form.
                string id = e.ExtraParams["id"];

                string obj = e.ExtraParams["values"];
                JobInfo b = JsonConvert.DeserializeObject<JobInfo>(obj);
                b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
                b.recordId = id;
                b.date = new DateTime(b.date.Year, b.date.Month, b.date.Day, 14, 0, 0);



                if (branchId.SelectedItem != null)
                    b.branchName = branchId.SelectedItem.Text;
                if (departmentId.SelectedItem != null)
                    b.departmentName = departmentId.SelectedItem.Text;
                if (positionId.SelectedItem != null)
                    b.positionName = positionId.SelectedItem.Text;
                if (divisionId.SelectedItem != null)
                    b.divisionName = divisionId.SelectedItem.Text;

               // b.reportToName = new EmployeeName();
                if (reportToId.SelectedItem != null)
                    b.reportToName = reportToId.SelectedItem.Text;
                // Define the object to add or edit as null
                if (b.reportToId == 0)
                    b.reportToId = null;
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
                           Common.errorMessage(r);;
                            return;
                        }
                        else
                        {

                            //Add this record to the store 
                           
                            JIStore.Reload();

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
                           Common.errorMessage(r);;
                            return;
                        }
                        else
                        {


                            ModelProxy record = this.JIStore.GetById(index);
                            EditJobInfoTab.UpdateRecord(record);
                            record.Set("departmentName", b.departmentName);
                            record.Set("branchName", b.branchName);
                            record.Set("positionName", b.positionName);
                            record.Set("divisionName", b.divisionName);
                            record.Set("reportToName", b.reportToName);
                            record.Commit();
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordUpdatedSucc
                            });
                            JIStore.Reload();
                            this.EditJobInfoWindow.Close();


                        }

                    }
                    catch (Exception ex)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                    }
                }

                X.Call("parent.refreshQV");
                X.Call("parent.SetJobInfo", b.departmentId,b.branchId,b.positionId,b.divisionId,b.reportToId);

            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error,exp.Message).Show();
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
               Common.errorMessage(resp);
            EHStatusStore.DataSource = resp.Items;
            EHStatusStore.DataBind();

        }

        private void FillPosition()
        {
            PositionListRequest positionsRequest = new PositionListRequest();
            positionsRequest.StartAt = "0";
            positionsRequest.Size = "1000";
            positionsRequest.SortBy = "positionRef";
            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(positionsRequest);
            if (!resp.Success)
               Common.errorMessage(resp);
            positionStore.DataSource = resp.Items;
            positionStore.DataBind();
        }
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
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
               Common.errorMessage(resp);
            BranchStore.DataSource = resp.Items;
            BranchStore.DataBind();
        }
        private void FillDivision()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
               Common.errorMessage(resp);
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();
        }
        [DirectMethod]
        public object FillSupervisor(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }
       
        

        #region combobox dynamic insert

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
        protected void addBranch(object sender, DirectEventArgs e)
        {
            Branch dept = new Branch();
            dept.name = branchId.Text;
            dept.isInactive = false;
       //     dept.timeZone = _systemService.SessionHelper.GetDefaultTimeZone();
            PostRequest<Branch> depReq = new PostRequest<Branch>();
            depReq.entity = dept;
            PostResponse<Branch> response = _companyStructureService.ChildAddOrUpdate<Branch>(depReq);
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

        protected void addPosition(object sender, DirectEventArgs e)
        {
            Model.Company.Structure.Position dept = new Model.Company.Structure.Position();
            dept.name = positionId.Text;

            PostRequest<Model.Company.Structure.Position> depReq = new PostRequest<Model.Company.Structure.Position>();
            depReq.entity = dept;
            PostResponse<Model.Company.Structure.Position> response = _companyStructureService.ChildAddOrUpdate<Model.Company.Structure.Position>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillPosition();
                positionId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        protected void addDivision(object sender, DirectEventArgs e)
        {
            Division dept = new Division();
            dept.name = divisionId.Text;
            dept.isInactive = false;
            PostRequest<Division> depReq = new PostRequest<Division>();
            depReq.entity = dept;

            PostResponse<Division> response = _companyStructureService.ChildAddOrUpdate<Division>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillDivision();
                divisionId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        protected void addStatus(object sender, DirectEventArgs e)
        {
            EmploymentStatus dept = new EmploymentStatus();
            dept.name = statusId.Text;

            PostRequest<EmploymentStatus> depReq = new PostRequest<EmploymentStatus>();
            depReq.entity = dept;

            PostResponse<EmploymentStatus> response = _employeeService.ChildAddOrUpdate<EmploymentStatus>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillEHStatus();
                statusId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        #endregion

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
            paranthized = paranthized.Replace(" ", string.Empty);
            paranthized = paranthized.Substring(0, paranthized.Length - 1);
          
            return paranthized;

        }

    }
}