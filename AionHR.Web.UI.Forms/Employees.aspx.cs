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
using AionHR.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using AionHR.Web.UI.Forms.Utilities;
using AionHR.Model.Company.News;
using AionHR.Services.Messaging;
using AionHR.Model.Company.Structure;
using AionHR.Infrastructure.Session;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Services.Messaging.System;

namespace AionHR.Web.UI.Forms
{
    public partial class Employees : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();


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
                case "ColName":

                    //Step 1 : get the object from the Web Service 
                    FillProfileInfo(id.ToString());
                    CurrentEmployee.Text = id.ToString();
                    FillLeftPanel();
                    FixLoaderUrls(id.ToString());
                    //employeePanel.Loader.Url = "EmployeePages/EmployeeProfile.aspx?employeeId="+CurrentEmployee.Text;
                    //employeePanel.Loader.LoadContent();

                    panelRecordDetails.ActiveIndex = 0;
                    //timeZoneCombo.Select(response.result.timeZone.ToString());
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();
                    break;

                case "colDelete":
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
                case "colEdit":

                    break;

                default:
                    break;
            }


        }

        private void FixLoaderUrls(string v)
        {
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.Loader != null)
                    item.Loader.Url = item.Loader.Url + "?employeeId=" + v;
            }
        }

        private void FillNameFields(EmployeeName name)
        {
            firstName.Text = name.firstName;
            lastName.Text = name.lastName;
            middleName.Text = name.middleName;
            familyName.Text = name.familyName;
            reference.Text = name.reference;
        }

        private void SelectCombos(Employee result)
        {
            branchId.Select(result.branchId);
            departmentId.Select(result.departmentId);
            positionId.Select(result.positionId);
            nationalityId.Select(result.nationalityId);
            //sponsorId.Select(result.sponsorId);
            vsId.Select(result.vsId);
            caId.Select(result.caId);
            divisionId.Select(result.divisionId);
            if (result.gender == 1)
                gender1.Checked = true;
            else
                gender0.Checked = true;
            //if (!string.IsNullOrEmpty(result.pictureUrl))
            //    imgControl.ImageUrl = result.pictureUrl;

        }
        private void InitCombos(bool isAdd)
        {
            FillBranch();
            branchId.Enabled = isAdd;
            branchId.ReadOnly = !isAdd;

            FillDepartment();

            departmentId.Enabled = isAdd;
            departmentId.ReadOnly = !isAdd;
            FillPosition();

            positionId.Enabled = isAdd;
            positionId.ReadOnly = !isAdd;
            FillDivision();
            divisionId.Enabled = isAdd;
            divisionId.ReadOnly = !isAdd;
            FillNationality();


            FillSponsor();


            FillVacationSchedule();
            panelRecordDetails.Enabled = !isAdd;

            FillWorkingCalendar();

            if (isAdd)
            {
                branchLbl.Text = "";
                positionLbl.Text = "";
                departmentLbl.Text = "";
                fullNameLbl.Text = "";
                imgControl.ImageUrl = "";

            }
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.ID == "BasicInfoTab")
                    continue;
                item.Disabled = isAdd;
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
            panelRecordDetails.ActiveIndex = 0;
            //picturePath.Clear();
            //imgControl.ImageUrl = "";
            InitCombos(true);
            CurrentEmployee.Text = "";
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;

            // timeZoneCombo.Select(_systemService.SessionHelper.GetTimeZone());
            this.EditRecordWindow.Show();
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page

            EmployeeListRequest empRequest = new EmployeeListRequest();
            empRequest.BranchId = "0";
            empRequest.DepartmentId = "0";
            empRequest.Filter = searchTrigger.Text;
            empRequest.IncludeIsInactive = false;
            if (e.Sort[0].Property == "name.fullName")
                empRequest.SortBy = GetNameFormat();
            else
                empRequest.SortBy = e.Sort[0].Property;
            empRequest.Size = e.Limit.ToString();
            empRequest.StartAt = e.Start.ToString();

            ListResponse<Employee> emps = _employeeService.GetAll<Employee>(empRequest);
            if (!emps.Success)
            {
                X.Msg.Alert(Resources.Common.Error, emps.Summary).Show();
                return;
            }
            e.Total = emps.count;
            if (emps.Items != null)
            {
                this.Store1.DataSource = emps.Items;
                this.Store1.DataBind();
            }
        }

        private void FillNationality()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(nationalityRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();

            }
            NationalityStore.DataSource = resp.Items;
            NationalityStore.DataBind();

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
        private void FillSponsor()
        {
            ListRequest sponsorsRequest = new ListRequest();
            ListResponse<Sponsor> resp = _employeeService.ChildGetAll<Sponsor>(sponsorsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            //SponsorStore.DataSource = resp.Items;
            //SponsorStore.DataBind();
        }
        private void FillVacationSchedule()
        {
            ListRequest vsRequest = new ListRequest();
            ListResponse<VacationSchedule> resp = _leaveManagementService.ChildGetAll<VacationSchedule>(vsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            VacationScheduleStore.DataSource = resp.Items;
            VacationScheduleStore.DataBind();
        }
        private void FillWorkingCalendar()
        {
            ListRequest caRequest = new ListRequest();
            ListResponse<WorkingCalendar> resp = _timeAttendanceService.ChildGetAll<WorkingCalendar>(caRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            CalendarStore.DataSource = resp.Items;
            CalendarStore.DataBind();

        }



        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            //string id = e.ExtraParams["id"];
            string id = CurrentEmployee.Text;
            string obj = e.ExtraParams["values"];
            Employee b = JsonConvert.DeserializeObject<Employee>(obj);
            b.name = new EmployeeName() { firstName = firstName.Text, lastName = lastName.Text, familyName = familyName.Text, middleName = middleName.Text, reference = reference.Text };

            b.recordId = id;
            // Define the object to add or edit as null
            if (branchId.SelectedItem != null)
                b.branchName = branchId.SelectedItem.Text;
            if (departmentId.SelectedItem != null)
                b.departmentName = departmentId.SelectedItem.Text;
            if (positionId.SelectedItem != null)
                b.positionName = positionId.SelectedItem.Text;
            if (divisionId.SelectedItem != null)
                b.divisionName = positionId.SelectedItem.Text;
            b.name.fullName = b.name.firstName + " " + b.name.middleName + " " + b.name.lastName + " ";
            b.birthDate = new DateTime(b.birthDate.Value.Year, b.birthDate.Value.Month, b.birthDate.Value.Day, 14, 0, 0);
            b.hireDate = new DateTime(b.hireDate.Value.Year, b.hireDate.Value.Month, b.hireDate.Value.Day, 14, 0, 0);

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    EmployeeAddOrUpdateRequest request = new EmployeeAddOrUpdateRequest();

                    byte[] fileData = null;
                    if (picturePath.PostedFile != null && picturePath.PostedFile.ContentLength > 0)
                    {
                        //using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                        //{
                        //    fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                        //}
                        fileData = new byte[picturePath.PostedFile.ContentLength];
                        fileData = picturePath.FileBytes;
                        request.fileName = picturePath.PostedFile.FileName;
                        request.imageData = fileData;

                    }
                    else
                    {
                        request.imageData = fileData;
                        request.fileName = "";
                    }
                    request.empData = b;



                    PostResponse<Employee> r = _employeeService.AddOrUpdateEmployeeWithPhoto(request);
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
                        RecordRequest req = new RecordRequest();
                        req.RecordID = b.recordId.ToString();
                        RecordResponse<Employee> response = _employeeService.Get<Employee>(req);
                        if(response.Success)
                        {
                            b.pictureUrl = response.result.pictureUrl;
                        }
                        //Add this record to the store 
                        this.Store1.Insert(0, b);



                        CurrentEmployee.Text = b.recordId;
                        FillLeftPanel();
                        InitCombos(false);

                        RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        sm.DeselectAll();
                        sm.Select(b.recordId.ToString());

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

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
                    EmployeeAddOrUpdateRequest request = new EmployeeAddOrUpdateRequest();

                    byte[] fileData = null;
                    if (picturePath.HasFile && picturePath.PostedFile.ContentLength > 0)
                    {
                        //using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                        // {
                        //    fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                        // }
                        fileData = new byte[picturePath.PostedFile.ContentLength];
                        fileData = picturePath.FileBytes;
                        request.fileName = picturePath.PostedFile.FileName;
                        request.imageData = fileData;



                    }
                    else
                    {
                        request.imageData = fileData;
                        request.fileName = "";
                    }
                    request.empData = b;



                    PostResponse<Employee> r = _employeeService.AddOrUpdateEmployeeWithPhoto(request);

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

                        RecordRequest req = new RecordRequest();
                        req.RecordID = b.recordId.ToString();
                        RecordResponse<Employee> response = _employeeService.Get<Employee>(req);
                        if (response.Success)
                        {
                            b.pictureUrl = response.result.pictureUrl;
                        }
                        ModelProxy record = this.Store1.GetById(index);
                        //BasicInfoTab.UpdateRecord(record);
                        record.Set("branchName", b.branchName);
                        record.Set("departmentName", b.departmentName);
                        record.Set("positionName", b.positionName);
                        record.Set("name", b.name);
                        record.Set("reference", b.reference);
                        record.Set("pictureUrl", b.pictureUrl);
                        record.Set("hireDate", b.hireDate.Value.ToShortDateString());

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


        protected void Unnamed_Load(object sender, EventArgs e)
        {
        }
        private void FillProfileInfo(string employeeId)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = employeeId.ToString();
            RecordResponse<Employee> response = _employeeService.Get<Employee>(r);
            BasicInfoTab.Reset();
            //picturePath.Clear();
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }
            //Step 2 : call setvalues with the retrieved object
            this.BasicInfoTab.SetValues(response.result);
            FillNameFields(response.result.name);
            InitCombos(false);
            SelectCombos(response.result);


        }

        private void FillLeftPanel()
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = CurrentEmployee.Text.ToString();
            RecordResponse<Employee> response = _employeeService.Get<Employee>(r);

            Employee forSummary = response.result;
            forSummary.firstName = forSummary.name.firstName;
            forSummary.middleName = forSummary.name.middleName;
            forSummary.lastName = forSummary.name.lastName;
            forSummary.fullName = forSummary.name.fullName;
            X.Call("FillLeftPanel",
                forSummary.fullName + "<br />",
                forSummary.departmentName + "<br />",
              forSummary.branchName + "<br />",
               forSummary.positionName + "<br />"
            );
            fullNameLbl.Html = forSummary.fullName + "<br />";
            departmentLbl.Html = forSummary.departmentName + "<br />";
            branchLbl.Html = forSummary.branchName + "<br />";
            positionLbl.Html = forSummary.positionName + "<br />";
            //employeeName.Text = resp.result.name.firstName + resp.result.name.lastName;
            imgControl.ImageUrl = response.result.pictureUrl;
        }

        #region combobox dynamic insert

        protected void addDepartment(object sender, DirectEventArgs e)
        {
            Department dept = new Department();
            dept.name = departmentId.Text;
            dept.isSegmentHead = false;
            PostRequest<Department> depReq = new PostRequest<Department>();
            depReq.entity = dept;
            PostResponse<Department> response = _companyStructureService.ChildAddOrUpdate<Department>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                departmentStore.Insert(0, dept);
                departmentId.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }
        protected void addBranch(object sender, DirectEventArgs e)
        {
            Branch dept = new Branch();
            dept.name = branchId.Text;
            dept.isInactive = false;
            PostRequest<Branch> depReq = new PostRequest<Branch>();
            depReq.entity = dept;
            PostResponse<Branch> response = _companyStructureService.ChildAddOrUpdate<Branch>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                BranchStore.Insert(0, dept);
                branchId.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
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
                positionStore.Insert(0, dept);
                positionId.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
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
                divisionStore.Insert(0, dept);
                divisionId.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        protected void addNationality(object sender, DirectEventArgs e)
        {
            Nationality obj = new Nationality();
            obj.name = nationalityId.Text;
            
            PostRequest<Nationality> req = new PostRequest<Nationality>();
            req.entity = obj;

            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                NationalityStore.Insert(0, obj);
                nationalityId.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        protected void addCalendar(object sender, DirectEventArgs e)
        {
            WorkingCalendar obj = new WorkingCalendar();
            obj.name = caId.Text;
            
            PostRequest<WorkingCalendar> req = new PostRequest<WorkingCalendar>();
            req.entity = obj;

            PostResponse<WorkingCalendar> response = _timeAttendanceService.ChildAddOrUpdate<WorkingCalendar>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                CalendarStore.Insert(0, obj);
                caId.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        protected void addVS(object sender, DirectEventArgs e)
        {
            VacationSchedule obj = new VacationSchedule();
            obj.name = vsId.Text;
            
            PostRequest<VacationSchedule> req = new PostRequest<VacationSchedule>();
            req.entity = obj;

            PostResponse<VacationSchedule> response = _leaveManagementService.ChildAddOrUpdate<VacationSchedule>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                VacationScheduleStore.Insert(0, obj);
                vsId.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        #endregion
    }
}