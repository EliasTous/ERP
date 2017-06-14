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
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Services.Messaging.System;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Access_Control;

namespace AionHR.Web.UI.Forms
{
    public partial class EmployeeProfileControl : System.Web.UI.UserControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();

        //protected override void InitializeCulture()
        //{

        //    bool rtl = true;
        //    if (!_systemService.SessionHelper.CheckIfArabicSession())
        //    {
        //        rtl = false;
        //        base.InitializeCulture();
        //        LocalisationManager.Instance.SetEnglishLocalisation();
        //        Culture = "en";
        //    }

        //    if (rtl)
        //    {
        //        base.InitializeCulture();
        //        LocalisationManager.Instance.SetArabicLocalisation();
        //        Culture = "ar-eg";
        //    }

        //}

        public void Add()
        {
            BasicInfoTab.Reset();
            panelRecordDetails.ActiveIndex = 0;
            picturePath.Clear();
            imgControl.ImageUrl = "";
            InitCombos(true);
            CurrentEmployeePhotoName.Text = "Images/empPhoto.jpg";
            CurrentEmployee.Text = "";
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            hireDate.ReadOnly = false;
            // timeZoneCombo.Select(_systemService.SessionHelper.GetTimeZone());
            this.EditRecordWindow.Show();
        }
        public void Update(string id)
        {
            imgControl.Src = "Images\\empPhoto.jpg";
            //Step 1 : get the object from the Web Service 
            FillProfileInfo(id.ToString());
            CurrentEmployee.Text = id.ToString();
            FillLeftPanel();

            hireDate.ReadOnly = true;

            //employeePanel.Loader.Url = "EmployeePages/EmployeeProfile.aspx?employeeId="+CurrentEmployee.Text;
            //employeePanel.Loader.LoadContent();

            panelRecordDetails.ActiveIndex = 0;
            //timeZoneCombo.Select(response.result.timeZone.ToString());
            this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
            this.EditRecordWindow.Show();
        }
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!X.IsAjaxRequest && !IsPostBack)
            {

                //SetExtLanguage();
                HideShowButtons();
                //HideShowColumns();

                CurrentClassId.Text = ClassId.EPEM.ToString();

                birthDate.Format = hireDate.Format = _systemService.SessionHelper.GetDateformat();

                pRTL.Text = _systemService.SessionHelper.CheckIfArabicSession().ToString();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Employee), left, null, null, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    BasicInfoTab.Hidden = true;
                    return;
                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Employee), rightPanel, null, null, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    BasicInfoTab.Hidden = true;
                    return;
                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeeTermination), terminationForm, null, null, Button6);

                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    terminationGear.Disabled = true;
                    return;
                }
                var properties = AccessControlApplier.GetPropertiesLevels(typeof(Employee));
                int level = properties.Where(x => x.index == "pictureUrl").ToList()[0].accessLevel;
                if (level == 0)
                {
                    imgControl.Hidden = true;
                    noImage.Hidden = false;

                }

                ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
                classReq.ClassId = "31000";
                classReq.UserId = _systemService.SessionHelper.GetCurrentUserId();
                RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
                if (modClass.result.accessLevel < 3)
                    deleteGear.Disabled = terminationGear.Disabled = true;

            }


        }

        public Store Store1 { get; set; }



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



        //private void SetExtLanguage()
        //{
        //    bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
        //    if (rtl)
        //    {
        //        this.ResourceManager1.RTL = true;
        //        this.Viewport1.RTL = true;
        //        BuildQuickViewTemplate();
        //    }
        //    pRTL.Text = rtl.ToString();
        //}




        private void FixLoaderUrls(string v)
        {
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.Loader != null)
                    item.Loader.Url = item.Loader.Url + "?employeeId=" + v;
            }
        }

        private void FixLoaderUrls(string employeeId, string hireDate)
        {
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.Loader != null)
                    item.Loader.Url = item.Loader.Url + "?employeeId=" + employeeId + "&hireDate=" + hireDate;
            }
        }
        private void FixLoaderUrls(string employeeId, string hireDate, bool terminated)
        {
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.Loader != null)
                    item.Loader.Url = item.Loader.Url + "?employeeId=" + employeeId + "&hireDate=" + hireDate + "&terminated=" + (terminated ? "1" : "0");
            }
        }
        private void FillNameFields(EmployeeName name)
        {
            firstName.Text = name.firstName;
            lastName.Text = name.lastName;
            middleName.Text = name.middleName;
            familyName.Text = name.familyName;
            reference.Text = name.reference;

            fullNameLbl.Html = name.fullName + "<br />";
            X.Call("FillFullName", name.fullName + "<br />");
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
            // // FillBranch();
            //  branchId.Enabled = isAdd;
            //  branchId.ReadOnly = !isAdd;

            // // FillDepartment();

            //  departmentId.Enabled = isAdd;
            //  departmentId.ReadOnly = !isAdd;
            // // FillPosition();

            //  positionId.Enabled = isAdd;
            //  positionId.ReadOnly = !isAdd;
            /////  FillDivision();
            //  divisionId.Enabled = isAdd;
            //  divisionId.ReadOnly = !isAdd;
            FillNationality();


            gearButton.Hidden = isAdd;

            FillSponsor();

            img.Hidden = isAdd;
            FillVacationSchedule();
            panelRecordDetails.Enabled = !isAdd;

            FillWorkingCalendar();

            SetTabPanelActivated(!isAdd);


        }

        private void ClearLeftPanel()
        {
            img.Visible = false;
            img.Hidden = true;
        }
        private void SetTabPanelActivated(bool isActive)
        {
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.ID == "BasicInfoTab")
                    continue;
                item.Disabled = !isActive;
            }
        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteRecord(object sender, DirectEventArgs e)
        {
            try

            {
                string index = CurrentEmployee.Text;
                //Step 1 Code to delete the object from the database 

                //Step 2 :  remove the object from the store



                PostRequest<Employee> req = new PostRequest<Employee>();
                Employee emp = new Employee();
                req.entity = emp;
                emp.recordId = index;
                emp.branchId = emp.departmentId = emp.divisionId = emp.vsId = emp.sponsorId = emp.caId = emp.nationalityId = emp.positionId = 0;
                emp.hireDate = DateTime.Now;
                PostResponse<Employee> post = _employeeService.Delete(req);
                if (!post.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, post.Summary).Show();
                    return;
                }
                //Store1.Remove(index);

                //Step 3 : Showing a notification for the user 
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordDeletedSucc
                });
                EditRecordWindow.Close();

            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }


        }

        private void SetActivated(bool active)
        {
            BasicInfoTab.Enabled = active;
            //SetTabPanelActivated(active);


            DeleteButton.Hidden = !active;
            terminationGear.Disabled = !active;
            resetPasswordGear.Disabled = !active;
            terminated.Text = active ? "0" : "1";

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
            BasicInfoTab.Reset();
            panelRecordDetails.ActiveIndex = 0;
            picturePath.Clear();
            imgControl.ImageUrl = "";
            InitCombos(true);
            CurrentEmployeePhotoName.Text = "Images/empPhoto.jpg";
            CurrentEmployee.Text = "";
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;

            // timeZoneCombo.Select(_systemService.SessionHelper.GetTimeZone());
            this.EditRecordWindow.Show();
        }





        private void FillNationality()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(nationalityRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            NationalityStore.DataSource = resp.Items;
            NationalityStore.DataBind();

        }

        private List<Model.Company.Structure.Position> GetPositions()
        {
            ListRequest positionsRequest = new ListRequest();
            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(positionsRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new List<Model.Company.Structure.Position>();
            }
            return resp.Items;
        }
        private void FillPosition()
        {

            positionStore.DataSource = GetPositions();
            positionStore.DataBind();
        }

        private void FillDepartment()
        {

            departmentStore.DataSource = GetDepartments();
            departmentStore.DataBind();
        }
        private List<Department> GetDepartments()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new List<Department>();
            }
            return resp.Items;
        }
        private List<Division> GetDivisions()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new List<Division>();
            }
            return resp.Items;
        }
        private List<Branch> GetBranches()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new List<Branch>();
            }

            return resp.Items;
        }
        private void FillBranch()
        {

            BranchStore.DataSource = GetBranches();
            BranchStore.DataBind();
        }

        private void FillDivision()
        {

            divisionStore.DataSource = GetDivisions();
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
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            VacationScheduleStore.DataSource = resp.Items;
            VacationScheduleStore.DataBind();
        }
        private void FillWorkingCalendar()
        {
            ListRequest caRequest = new ListRequest();
            ListResponse<WorkingCalendar> resp = _timeAttendanceService.ChildGetAll<WorkingCalendar>(caRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            CalendarStore.DataSource = resp.Items;
            CalendarStore.DataBind();

        }


        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            //string id = e.ExtraParams["id"];
            string id = CurrentEmployee.Text;
            string obj = e.ExtraParams["values"];
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            Employee b = JsonConvert.DeserializeObject<Employee>(obj, settings);
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
                b.divisionName = divisionId.SelectedItem.Text;
            b.name.fullName = b.name.firstName + " " + b.name.middleName + " " + b.name.lastName + " ";
            if (b.birthDate.HasValue)
                b.birthDate = new DateTime(b.birthDate.Value.Year, b.birthDate.Value.Month, b.birthDate.Value.Day, 14, 0, 0);
            b.hireDate = new DateTime(b.hireDate.Value.Year, b.hireDate.Value.Month, b.hireDate.Value.Day, 14, 0, 0);

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Employee> request = new PostRequest<Employee>();

                    byte[] fileData = null;
                    if (FileUploadField1.PostedFile != null && FileUploadField1.PostedFile.ContentLength > 0)
                    {
                        //using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                        //{
                        //    fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                        //}
                        fileData = new byte[FileUploadField1.PostedFile.ContentLength];
                        fileData = FileUploadField1.FileBytes;


                    }

                    request.entity = b;



                    PostResponse<Employee> r = _employeeService.AddOrUpdate<Employee>(request);
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
                        if (fileData != null)
                        {
                            X.Call("GetCroppedImage");
                            //SystemAttachmentsPostRequest reqAT = new SystemAttachmentsPostRequest();
                            //reqAT.entity = new Model.System.Attachement() { date = DateTime.Now, classId = ClassId.EPEM, recordId = Convert.ToInt32(b.recordId), fileName = FileUploadField1.PostedFile.FileName, seqNo = null };
                            //reqAT.FileNames.Add(FileUploadField1.PostedFile.FileName);
                            //reqAT.FilesData.Add(fileData);
                            //PostResponse<Attachement> resp = _systemService.UploadMultipleAttachments(reqAT);
                            //if (!resp.Success)//it maybe be another condition
                            //{
                            //    //Show an error saving...
                            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            //    X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                            //    return;
                            //}
                        }
                        RecordRequest req = new RecordRequest();
                        req.RecordID = b.recordId.ToString();
                        RecordResponse<Employee> response = _employeeService.Get<Employee>(req);
                        if (response.Success)
                        {
                            b.pictureUrl = response.result.pictureUrl + "?x=" + DateTime.Now;
                            b.name = response.result.name;
                        }
                        //Add this record to the store 
                        this.Store1.Insert(0, b);


                        CurrentEmployee.Text = req.RecordID.ToString();
                        FillLeftPanel();

                        FillLeftPanel();
                        InitCombos(false);
                        FillProfileInfo(b.recordId);
                        recordId.Text = b.recordId;
                        //RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        //sm.DeselectAll();
                        //sm.Select(b.recordId.ToString());

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
                    PostRequest<Employee> request = new PostRequest<Employee>();



                    request.entity = b;



                    PostResponse<Employee> r = _employeeService.AddOrUpdate<Employee>(request);

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

                        RecordRequest req = new RecordRequest();
                        req.RecordID = b.recordId.ToString();
                        RecordResponse<Employee> response = _employeeService.Get<Employee>(req);

                        if (response.Success)
                        {
                            b.pictureUrl = response.result.pictureUrl + "?x=" + DateTime.Now;
                            b.name = response.result.name;
                        }
                        Employee n = response.result;
                        ModelProxy record = this.Store1.GetById(index);
                        BasicInfoTab.UpdateRecord(record);
                        record.Set("branchName", n.branchName);
                        record.Set("departmentName", n.departmentName);
                        record.Set("positionName", n.positionName);
                        record.Set("divisionName", n.divisionName);
                        record.Set("name", n.name);
                        record.Set("reference", n.reference);
                        record.Set("pictureUrl", n.pictureUrl);
                        record.Set("hireDate", n.hireDate.Value.ToShortDateString());

                        //record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                            ,
                            CloseVisible = true
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


        protected void SaveTermination(object sender, DirectEventArgs e)
        {
            string id = CurrentEmployee.Text;
            string obj = e.ExtraParams["values"];
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.DateFormatString = "dd/MM/yyyy";
            EmployeeTermination t = JsonConvert.DeserializeObject<EmployeeTermination>(obj, setting);
            t.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            PostRequest<EmployeeTermination> request = new PostRequest<EmployeeTermination>();
            request.entity = t;

            PostResponse<EmployeeTermination> resp = _employeeService.ChildAddOrUpdate<EmployeeTermination>(request);
            if (!resp.Success)
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }

            else
            {
                terminationWindow.Close();
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });
                EditRecordWindow.Close();
                Store1.Reload();

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
            return _systemService.SessionHelper.Get("nameFormat").ToString();

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
            SetActivated(!response.result.isInactive);
            FixLoaderUrls(r.RecordID, response.result.hireDate.Value.ToString("yyyy/MM/dd"), response.result.isInactive);

        }

        [DirectMethod]
        public void FillLeftPanel(bool shouldUpdateGrid = false)
        {

            RecordRequest r = new RecordRequest();
            r.RecordID = CurrentEmployee.Text.ToString();
            RecordResponse<EmployeeQuickView> qv = _employeeService.ChildGetRecord<EmployeeQuickView>(r);
            if (!qv.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, qv.Summary).Show();
                return;
            }
            EmployeeQuickView forSummary = qv.result;
            FileName.Text = qv.result.pictureFileName;
            if (string.IsNullOrEmpty(forSummary.pictureUrl))
                forSummary.pictureUrl = "Images/empPhoto.jpg";
            X.Call("FillLeftPanel",

                forSummary.departmentName + "<br />",
              forSummary.branchName + "<br />",
               forSummary.positionName + "<br />",
               (forSummary.reportToName == null && !string.IsNullOrEmpty(forSummary.reportToName.fullName.Trim())) ? "" : "<br />" + GetLocalResourceObject("FieldReportsTo").ToString() + " :" + forSummary.reportToName.fullName + "<br />",

               forSummary.eosBalance + "<br />",
               forSummary.paidLeavesYTD + "<br/>",
                forSummary.LastLeave(_systemService.SessionHelper.GetDateformat()) + "<br />",
               forSummary.leavesBalance + "<br />",
               forSummary.allowedLeaveYtd + "<br />",
               forSummary.esName,
               forSummary.serviceDuractionFriendly(GetGlobalResourceObject("Common", "Day").ToString(), GetGlobalResourceObject("Common", "Month").ToString(), GetGlobalResourceObject("Common", "Year").ToString())
            );
            //            fullNameLbl.Html = forSummary.name.fullName + "<br />";

            departmentLbl.Html = forSummary.departmentName + "<br />";
            branchLbl.Html = forSummary.branchName + "<br />";
            positionLbl.Html = forSummary.positionName + "<br />";
            esName.Html = forSummary.esName + "<br /><br />";
            eosBalanceLbl.Html = forSummary.eosBalance + "<br />";
            serviceDuration.Html = forSummary.serviceDuration + "<br />";// Friendly(GetGlobalResourceObject("Common", "Day").ToString(), GetGlobalResourceObject("Common", "Month").ToString(), GetGlobalResourceObject("Common", "Year").ToString())+"<br />";

            paidLeavesYTDLbl.Html = forSummary.paidLeavesYTD + "<br/>";
            lastLeaveStartDateLbl.Html = forSummary.LastLeave(_systemService.SessionHelper.GetDateformat()) + "<br />";
            leavesBalance.Html = forSummary.leavesBalance + "<br />";
            allowedLeaveYtd.Html = forSummary.allowedLeaveYtd + "<br />";
            if (forSummary.reportToName != null && !string.IsNullOrEmpty(forSummary.reportToName.fullName.Trim()))
            {
                reportsToLbl.Html = GetLocalResourceObject("FieldReportsTo").ToString() + " :<br/ >" + forSummary.reportToName.fullName + "<br />";
            }
            else
            {
                reportsToLbl.Html = "";
            }
            //employeeName.Text = resp.result.name.firstName + resp.result.name.lastName;

            imgControl.ImageUrl = forSummary.pictureUrl + "?x=" + DateTime.Now.Ticks;
            employeePhoto.ImageUrl = forSummary.pictureUrl + "?x=" + DateTime.Now.Ticks; ;

            //here
            X.Call("InitCropper", forSummary.pictureUrl + "?x=" + DateTime.Now.Ticks);

            CurrentEmployeePhotoName.Text = forSummary.pictureUrl;
            ModelProxy record = Store1.GetById(CurrentEmployee.Text);
            record.Set("pictureUrl", imgControl.ImageUrl);
            record.Set("departmentName", forSummary.departmentName);
            record.Set("branchName", forSummary.branchName);
            record.Set("divisionName", forSummary.divisionName);
            record.Set("positionName", forSummary.positionName);
            record.Commit();
            img.Visible = true;
        }

        #region combobox dynamic insert

        protected void addDepartment(object sender, DirectEventArgs e)
        {
            Department dept = new Department();
            if (string.IsNullOrEmpty(departmentId.Text))
                return;
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
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
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
            dept.isInactive = false;
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
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        protected void addPosition(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(positionId.Text))
                return;
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
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        protected void addDivision(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(divisionId.Text))
                return;
            Division dept = new Division();
            dept.name = divisionId.Text;
            dept.isInactive = false;
            PostRequest<Division> depReq = new PostRequest<Division>();
            depReq.entity = dept;

            PostResponse<Division> response = _companyStructureService.ChildAddOrUpdate<Division>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;

                //When updating a store on server side via a directmethod, it is mandatory to re DataBind() so for that we called FillDivision() 
                FillDivision();
                //  divisionStore.Insert(0,dept);
                //  divisionStore.Add(new { recordId = dept.recordId, name = dept.name });

                divisionId.Value = dept.recordId;
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
            if (string.IsNullOrEmpty(nationalityId.Text))
                return;
            Nationality obj = new Nationality();
            obj.name = nationalityId.Text;

            PostRequest<Nationality> req = new PostRequest<Nationality>();
            req.entity = obj;

            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillNationality();
                nationalityId.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

        }

        [DirectMethod]
        public object addTR()
        {
            if (string.IsNullOrEmpty(trId.Text))
                return null;
            TerminationReason obj = new TerminationReason();
            obj.name = trId.Text;

            PostRequest<TerminationReason> req = new PostRequest<TerminationReason>();
            req.entity = obj;

            PostResponse<TerminationReason> response = _employeeService.ChildAddOrUpdate<TerminationReason>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                return obj;


            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return null;
            }

        }


        #endregion

        protected void ShowTermination(object sender, DirectEventArgs e)
        {
            terminationForm.Reset();
            FillTerminationReasons();
            date.SelectedDate = DateTime.Today;
            terminationWindow.Show();
        }

        [DirectMethod]
        public object GetQuickView(Dictionary<string, string> parameters)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = parameters["id"];
            RecordResponse<EmployeeQuickView> qv = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
            if (!qv.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, qv.Summary).Show();
                return null;
            }
            return new
            {
                reportsTo = qv.result.reportToName.fullName,
                eosBalance = qv.result.eosBalance,
                paidLeavesYTD = qv.result.paidLeavesYTD,
                leavesBalance = qv.result.leavesBalance,
                allowedLeaveYtd = qv.result.allowedLeaveYtd,
                lastleave = qv.result.LastLeave(_systemService.SessionHelper.GetDateformat())


            };

        }

        private void FillTerminationReasons()
        {
            ListRequest caRequest = new ListRequest();
            ListResponse<TerminationReason> resp = _employeeService.ChildGetAll<TerminationReason>(caRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            trStore.DataSource = resp.Items;
            trStore.DataBind();

        }

        [DirectMethod]
        public void promptDelete(object sender, DirectEventArgs e)
        {
            confirmForm.Reset();
            confirmWindow.Show();


        }

        protected void CompleteDelete(object sender, DirectEventArgs e)
        {
            string id = CurrentEmployee.Text;
            string obj = e.ExtraParams["delText"];
            if (obj.ToLower() != "delete")
            {
                return;
            }

            try

            {
                string index = CurrentEmployee.Text;
                //Step 1 Code to delete the object from the database 

                //Step 2 :  remove the object from the store



                PostRequest<Employee> req = new PostRequest<Employee>();
                Employee emp = new Employee();
                req.entity = emp;
                emp.recordId = index;

                PostResponse<Employee> post = _employeeService.Delete<Employee>(req);
                if (!post.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, post.Summary).Show();
                    return;
                }
                Store1.Remove(index);

                //Step 3 : Showing a notification for the user 
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordDeletedSucc
                });
                confirmWindow.Close();
                EditRecordWindow.Close();

            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }


        }

        protected void ResetPassword(object sender, DirectEventArgs e)
        {
            AccountRecoveryRequest recoverRequest = new AccountRecoveryRequest();
            recoverRequest.Email = workEmail.Text;
            if (string.IsNullOrEmpty(recoverRequest.Email))
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ResetPassword, Resources.Common.EmailNotFound).Show();

            }
            PasswordRecoveryResponse response = _systemService.RequestPasswordRecovery(recoverRequest);
            if (response.Success)
            {

                X.Msg.Alert(Resources.Common.ResetPassword, Resources.Common.RecoverySentSucc).Show();


            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();

            }


        }
        protected void UploadImage(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            //string id = e.ExtraParams["id"];
            string id = CurrentEmployee.Text;
            string obj = e.ExtraParams["values"];
            string dd = e.ExtraParams["image"];

            if (string.IsNullOrEmpty(id))
            {
                imageSelectionWindow.Hide();
                return;
            }
            PostResponse<Attachement> resp = null;

            if (FileUploadField1.PostedFile != null && FileUploadField1.PostedFile.ContentLength > 0)
            {

                EmployeeUploadPhotoRequest upreq = new EmployeeUploadPhotoRequest();

                upreq.entity.fileName = FileUploadField1.FileName;
                upreq.photoName = FileUploadField1.FileName;
                upreq.photoData = FileUploadField1.FileBytes;
                upreq.entity.recordId = Convert.ToInt32(CurrentEmployee.Text);

                resp = _employeeService.UploadEmployeePhoto(upreq);
            }
            else
            {
                PostRequest<Attachement> req = new PostRequest<Attachement>();

                Attachement at = new Attachement();
                at.classId = ClassId.EPEM;
                at.recordId = Convert.ToInt32(CurrentEmployee.Text);
                at.seqNo = 0;
                at.folderId = null;

                at.fileName = CurrentEmployeePhotoName.Text;
                req.entity = at;
                resp = _systemService.ChildDelete<Attachement>(req);
            }



            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.ResetPassword, resp.Summary).Show();
                return;
            }

            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
                           ,
                HideDelay = 1000,
                CloseVisible = true
            });


            imageSelectionWindow.Hide();
            FillLeftPanel(true);


        }
        [DirectMethod]
        public void FillLeftPanelDirect()
        {
            FillLeftPanel(true);
        }
        protected void DisplayImage(object sender, DirectEventArgs e)
        {

            if (string.IsNullOrEmpty(CurrentEmployee.Text))
            {
                employeePhoto.ImageUrl = "Images/empPhoto.jpg";
                return;
            }
            RecordRequest r = new RecordRequest();
            r.RecordID = CurrentEmployee.Text.ToString();
            RecordResponse<Employee> response = _employeeService.Get<Employee>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                return;
            }

            employeePhoto.ImageUrl = response.result.pictureUrl + "?x=" + DateTime.Now.Ticks;
        }

        protected void DisplayTeam(object sender, DirectEventArgs e)
        {
            TeamMembersListRequest empRequest = new TeamMembersListRequest();
            empRequest.EmployeeId = Convert.ToInt32(CurrentEmployee.Text);


            empRequest.Size = "30";
            empRequest.StartAt = "1";


            ListResponse<TeamMember> emps = _employeeService.ChildGetAll<TeamMember>(empRequest);
            TeamStore.DataSource = emps.Items;
            TeamStore.DataBind();
            TeamWindow.Show();
        }
    }
}