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
using AionHR.Model.NationalQuota;
using AionHR.Web.UI.Forms.ConstClasses;

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

        INationalQuotaService _nationalQuotaService = ServiceLocator.Current.GetInstance<INationalQuotaService>();

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
            if (!_systemService.SessionHelper.GetHijriSupport())
            {
                hijriCalbutton.Hidden = true;
                SetHijriInputState(false);
                //gregCal.Checked = true;
                bdHijriCal.Text = "false";
            }
            else
            {
                hijriCalbutton.Hidden = false;
                SetHijriInputState(true);
                //hijCalBirthDate.Hidden = true;
                //gregCal.Checked = true;
                bdHijriCal.Text = "true";
            }
            SaveButton.Disabled = false;
            BasicInfoTab.Reset();
            panelRecordDetails.ActiveIndex = 0;
            picturePath.Clear();
        
            imgControl.ImageUrl = "Images/empPhoto.jpg";
            InitCombos(true);
            CurrentEmployeePhotoName.Text = "Images/empPhoto.jpg";
            CurrentEmployee.Text = "";
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            hireDate.ReadOnly = false;
            // timeZoneCombo.Select(_systemService.SessionHelper.GetTimeZone());
            this.EditRecordWindow.Show();
        }
        public void Update(string id ,string fullName)
        {
            if (id == _systemService.SessionHelper.GetEmployeeId())
                deleteGear.Disabled = true;
            else
                deleteGear.Disabled = false;

          



            if (!_systemService.SessionHelper.GetHijriSupport())
            {
                hijriCalbutton.Hidden = true;
                SetHijriInputState(false);
                bdHijriCal.Text = "false";
            }
            else
            {
                hijriCalbutton.Hidden = false;
                bdHijriCal.Text = "true";
            }
        
            imgControl.Src = "Images\\empPhoto.jpg";
            //Step 1 : get the object from the Web Service 
            FillProfileInfo(id.ToString());
            CurrentEmployee.Text = id.ToString();
            CurrentEmployeeFullName.Text = fullName;
            FillLeftPanel();
           
            hireDate.ReadOnly = false;

            //employeePanel.Loader.Url = "EmployeePages/EmployeeProfile.aspx?employeeId="+CurrentEmployee.Text;
            //employeePanel.Loader.LoadContent();

            panelRecordDetails.ActiveIndex = 0;
            //timeZoneCombo.Select(response.result.timeZone.ToString());
            this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
            reference.IsRemoteValidation = false;
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

                date.Format= gregCalBirthDate.Format = hireDate.Format = _systemService.SessionHelper.GetDateformat();
                civilStatus.Select(0);

                pRTL.Text = _systemService.SessionHelper.CheckIfArabicSession().ToString();
                if (_systemService.SessionHelper.CheckIfIsAdmin())
                    return;
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
            //caId.Select(result.caId);
            //scId.Select(result.scId.ToString());
            divisionId.Select(result.divisionId);
            nqciId.Select(result.nqciId.ToString());
            if (string.IsNullOrEmpty(result.civilStatus.ToString()))
                result.civilStatus = 0;                
            civilStatus.Select(result.civilStatus.ToString());
            if (result.gender == 1)
                gender1.Checked = true;
            else
                gender2.Checked = true;
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
            FillSchedules();
            panelRecordDetails.Enabled = !isAdd;
            FillCitizenShip();

            //FillWorkingCalendar();
            
            SetTabPanelActivated(!isAdd);


        }

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
            SaveButton.Disabled = !active;
            //SetTabPanelActivated(active);


            DeleteButton.Hidden = !active;
            //terminationGear.Disabled = !active;
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
            reference.IsRemoteValidation = true;
            // timeZoneCombo.Select(_systemService.SessionHelper.GetTimeZone());
        

            this.EditRecordWindow.Show();
        }





        private void FillNationality()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(nationalityRequest);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
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
               Common.errorMessage(resp);
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
        private void FillSecurityGroup()
        {

            securityGroupStore.DataSource = GetSecurityGrop();
            securityGroupStore.DataBind();
        }
        private List<Department> GetDepartments()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
                return new List<Department>();
            }
            return resp.Items;
        }
        private List<SecurityGroup> GetSecurityGrop()
        {
            ListRequest Request = new ListRequest();
            ListResponse<SecurityGroup> resp = _accessControlService.ChildGetAll<SecurityGroup>(Request);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
                return new List<SecurityGroup>();
            }
            return resp.Items;
        }
        private List<Division> GetDivisions()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
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
               Common.errorMessage(resp);
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
               Common.errorMessage(resp);
            //SponsorStore.DataSource = resp.Items;
            //SponsorStore.DataBind();
        }
        private void FillVacationSchedule()
        {
            ListRequest vsRequest = new ListRequest();
            ListResponse<VacationSchedule> resp = _leaveManagementService.ChildGetAll<VacationSchedule>(vsRequest);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
                return;
            }
            VacationScheduleStore.DataSource = resp.Items;
            VacationScheduleStore.DataBind();
        }
        //private void FillWorkingCalendar()
        //{
        //    ListRequest caRequest = new ListRequest();
        //    ListResponse<WorkingCalendar> resp = _timeAttendanceService.ChildGetAll<WorkingCalendar>(caRequest);
        //    if (!resp.Success)
        //    {
        //       Common.errorMessage(resp);
        //        return;
        //    }
        //    CalendarStore.DataSource = resp.Items;
        //    CalendarStore.DataBind();

        //}


        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            //string id = e.ExtraParams["id"];
            string id = CurrentEmployee.Text;
            //string hijCalBirthDate = e.ExtraParams["hijCalBirthDate"];
            string gregCalBirthDate = e.ExtraParams["gregCalBirthDate"];
            string obj = e.ExtraParams["values"];
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            if (civilStatus.Value.ToString() == "0")
                civilStatus.Value = string.Empty;
            Employee b = JsonConvert.DeserializeObject<Employee>(obj, settings);
            b.name = new EmployeeName() { firstName = firstName.Text, lastName = lastName.Text, familyName = familyName.Text, middleName = middleName.Text, reference = reference.Text.ToString().Replace(" ","") };

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
            //if (b.birthDate.HasValue)
            //    b.birthDate = new DateTime(b.birthDate.Value.Year, b.birthDate.Value.Month, b.birthDate.Value.Day, 14, 0, 0);
            bool hijriSupported = _systemService.SessionHelper.GetHijriSupport();
            // Define the object to add or edit as null
            b.bdHijriCal =Convert.ToBoolean(bdHijriCal.Text);

            try
            {
                CultureInfo c = new CultureInfo("en");
                string format = "";
                if (hijriSupported)
                {
                    if (b.bdHijriCal)
                    {
                        c = new CultureInfo("ar");
                        format = "yyyy/MM/dd";

                        if (!string.IsNullOrEmpty(hijCalBirthDate.Text))
                            b.birthDate = DateTime.ParseExact(b.hijCalBirthDate, format, c);
                        else
                            b.birthDate = null;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(gregCalBirthDate))
                            b.birthDate = DateTime.Parse(gregCalBirthDate);
                        //if (gregCalBirthDate.SelectedDate != null)
                        //    b.birthDate = gregCalBirthDate.SelectedDate;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(gregCalBirthDate))
                        b.birthDate = DateTime.Parse(gregCalBirthDate);
                    //if (gregCalBirthDate.SelectedDate != null)
                    //    b.birthDate = gregCalBirthDate.SelectedDate; 
                }

                }
            catch (Exception exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("DateFormatError")).Show();

                return;
            }
            //    if (b.bdHijriCal)
            //{
            //    b.birthDate = b.hijCalBirthDate;
            //    b.hijCalBirthDate = null;
            //    b.gregCalBirthDate = null;
            //}

            //else
            //{
            //    b.birthDate = b.gregCalBirthDate.ToString();
            //    b.hijCalBirthDate = null;
            //    b.gregCalBirthDate = null;
            //}

            //b.hireDate = new DateTime(b.hireDate.Value.Year, b.hireDate.Value.Month, b.hireDate.Value.Day, 14, 0, 0);

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


                    request.entity.reference = request.entity.reference.Replace(" ", "");
                        PostResponse<Employee> r = _employeeService.AddOrUpdate<Employee>(request);
                        CurrentEmployeeFullName.Text = b.name.fullName;
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
                                //     Common.errorMessage(r);
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


                    request.entity.name.reference = request.entity.reference.Replace(" ", "");
                    PostResponse<Employee> r = _employeeService.AddOrUpdate<Employee>(request);

                        //Step 2 : saving to store

                        //Step 3 :  Check if request fails
                        if (!r.Success)//it maybe another check
                        {
                            string message = GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary;
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                             Common.errorMessage(r);
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

                            //record.Commit();
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordUpdatedSucc
                                ,
                                CloseVisible = true
                            });

                            //this.EditRecordWindow.Close();

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
            string terminationRecordId= e.ExtraParams["terminationRecordId"];
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.DateFormatString = "dd/MM/yyyy";
            EmployeeTermination t = JsonConvert.DeserializeObject<EmployeeTermination>(obj, setting);
            t.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            PostResponse<EmployeeTermination> resp;
            if (terminated.Text=="0")
            {
                PostRequest<EmployeeTermination> request = new PostRequest<EmployeeTermination>();
                request.entity = t;

               resp = _employeeService.ChildAddOrUpdate<EmployeeTermination>(request);
            }
            else
            {
                PostRequest<EmployeeTermination> request = new PostRequest<EmployeeTermination>();
                t.recordId = terminationRecordId;
                request.entity = t;


                resp = _employeeService.ChildDelete<EmployeeTermination>(request);
            }
            if (!resp.Success)
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
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
        protected void SaveSelfService(object sender, DirectEventArgs e)
        {

            string userId="";
            bool isInactive; 
            string obj = e.ExtraParams["values"];
            UserByEmailRequest req = new UserByEmailRequest();
            req.Email = workEmailHF.Text;
            RecordResponse<UserInfo> response = _systemService.Get<UserInfo>(req);
            if (!response.Success)
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(response);
                return;
            }


                if (enableSS.Checked)
                   isInactive = true;
                else
                isInactive = false;

            if (response.result == null)
            {


                response.result = new UserInfo { employeeId = CurrentEmployee.Text, email = workEmailHF.Text, isInactive = isInactive, userType = 4, languageId = 1, password = "1", fullName = CurrentEmployeeFullName.Text };
            }
            PostRequest<UserInfo> request = new PostRequest<UserInfo>();

            request.entity = response.result;
            request.entity.userType = 4;
            request.entity.isInactive = isInactive;
            PostResponse<UserInfo> r = _systemService.ChildAddOrUpdate<UserInfo>(request);
            if (!r.Success)
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
              Common.errorMessage(r);
                return;
            }
            userId = r.recordId;

            if (sgId.SelectedItem.Value != null)
                {
                    SecurityGroupUser s = new SecurityGroupUser();
                    s.email = workEmailHF.Text;
                    s.fullName = response.result.fullName;
                    s.sgId = sgId.SelectedItem.Value.ToString();
                    s.userId = userId;
                    PostRequest<SecurityGroupUser> SecurityGroupUserReq = new PostRequest<SecurityGroupUser>();
                    SecurityGroupUserReq.entity = s;
                    PostResponse<SecurityGroupUser> resp = _accessControlService.ChildAddOrUpdate<SecurityGroupUser>(SecurityGroupUserReq);
                if (!resp.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+resp.LogId : resp.Summary).Show();
                    return;
                }
            }
            AccountRecoveryRequest recoverRequest = new AccountRecoveryRequest();
            recoverRequest.Email = workEmailHF.Text;
            PasswordRecoveryResponse recoveryRsponse = _systemService.RequestPasswordRecovery(recoverRequest);
            if (!recoveryRsponse.Success)
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", recoveryRsponse.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + recoveryRsponse.Summary : recoveryRsponse.Summary).Show();
                return;
            }
            selfServiceWindow.Close();
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });
             

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
            try
            {
                RecordRequest r = new RecordRequest();
                r.RecordID = employeeId.ToString();
                RecordResponse<Employee> response = _employeeService.Get<Employee>(r);
                BasicInfoTab.Reset();
                //picturePath.Clear();
                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(response);
                    return;
                }
                //Step 2 : call setvalues with the retrieved object
                this.BasicInfoTab.SetValues(response.result);



                if (_systemService.SessionHelper.GetHijriSupport())
                {

                    if (response.result.bdHijriCal)
                    {
                        SetHijriInputState(true);
                        //hijCal.Checked = true;
                        bdHijriCal.Text = "true";

                        hijCalBirthDate.Text = response.result.birthDate.HasValue ? response.result.birthDate.Value.ToString("yyyy/MM/dd", new CultureInfo("ar")) : "";
                        X.Call("handleInputRender");

                    }


                }
                if (!response.result.bdHijriCal)
                {
                    SetHijriInputState(false);
                    //gregCal.Checked = true;
                    bdHijriCal.Text = "false";
                    gregCalBirthDate.Value = response.result.birthDate.HasValue ? response.result.birthDate : new DateTime();
                }



                //if (response.result.bdHijriCal)
                // {
                //     hijCal.Checked = true;
                //     hijCalBirthDate.Text = response.result.birthDate;
                // }
                //else
                // {
                //     gregCal.Checked = true;
                //     if (!string.IsNullOrEmpty(response.result.birthDate)) ;
                //     //gregCalBirthDate.Value = DateTime.ParseExact(response.result.birthDate, "yyyyMMdd", new CultureInfo("en"));
                // }

                FillNameFields(response.result.name);

                InitCombos(false);
                SelectCombos(response.result);
                SetActivated(!response.result.isInactive);
                setTerminationWindow(response.result.isInactive);


                FixLoaderUrls(r.RecordID, response.result.hireDate.HasValue ? response.result.hireDate.Value.ToString("yyyy/MM/dd") : "", response.result.isInactive);
            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error,exp.Message).Show();
            }

        }
        private void setTerminationWindow (bool isInactive)
        {
            if (isInactive)
                saveTerminationButton.Text = GetLocalResourceObject("undoTermination").ToString();
            else
            {
                saveTerminationButton.Text= GetGlobalResourceObject("Common", "Save").ToString();
                saveTerminationButton.Hidden = isInactive;
            }
            //    TextField1.ReadOnly = isInactive;
                date.ReadOnly = isInactive;
                ttId.ReadOnly = isInactive;
                trId.ReadOnly = isInactive;
                rehire.ReadOnly = isInactive;

           
        }
        [DirectMethod]
        public void FillLeftPanel(bool shouldUpdateGrid = false)
        {

            EmployeeQuickViewRecordRequest r = new EmployeeQuickViewRecordRequest();
            r.RecordID = CurrentEmployee.Text.ToString();
            r.asOfDate = DateTime.Now;
            
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

            if (forSummary.terminationDate!=null)
            {
                TerminatedLbl.Hidden = false;
                TerminationDateLbl.Hidden = false;
            }
            else
            {
                TerminatedLbl.Hidden = true;
                TerminationDateLbl.Hidden = true;
            }

            X.Call("FillLeftPanel",

                forSummary.departmentName + "<br />",
              forSummary.branchName + "<br />",
               forSummary.positionName + "<br />",
               (forSummary.reportToName == null && !string.IsNullOrEmpty(forSummary.reportToName.fullName.Trim())) ? "" : GetLocalResourceObject("FieldReportsTo").ToString() + " :<br/>" + forSummary.reportToName.fullName + "<br />",

               forSummary.indemnity + "<br />",
               forSummary.usedLeavesLeg + "<br/>",
                forSummary.LastLeave(_systemService.SessionHelper.GetDateformat()) + "<br />",
               forSummary.leaveBalance + "<br />",
               forSummary.earnedLeavesLeg + "<br />",
               forSummary.esName,

                forSummary.usedLeaves,
                 forSummary.paidLeaves,
                  forSummary.salary,


               forSummary.serviceDuractionFriendly(GetGlobalResourceObject("Common", "Day").ToString(), GetGlobalResourceObject("Common", "Month").ToString(), GetGlobalResourceObject("Common", "Year").ToString()),
               forSummary.terminationDate != null ? forSummary.terminationDate.Value.ToString(_systemService.SessionHelper.GetDateformat()) :"",
               forSummary.unpaidLeaves
            );
            //            fullNameLbl.Html = forSummary.name.fullName + "<br />";

            departmentLbl.Html = forSummary.departmentName + "<br />";
            branchLbl.Html = forSummary.branchName + "<br />";
            positionLbl.Html = forSummary.positionName + "<br />";
            esName.Html = forSummary.esName + "<br /><br />";
            eosBalanceLbl.Html = forSummary.indemnity.ToString("N2") +" "+forSummary.currencyName+ "<br />";
            serviceDuration.Html = forSummary.serviceDuration + "<br />";// Friendly(GetGlobalResourceObject("Common", "Day").ToString(), GetGlobalResourceObject("Common", "Month").ToString(), GetGlobalResourceObject("Common", "Year").ToString())+"<br />";

            paidLeavesYTDLbl.Html = forSummary.usedLeavesLeg + "<br/>";
            //lastLeaveStartDateLbl.Html = forSummary.LastLeave(_systemService.SessionHelper.GetDateformat()) + "<br />";
            leavesBalance.Html = forSummary.leaveBalance + "<br />";
            allowedLeaveYtd.Html = forSummary.earnedLeavesLeg + "<br />";
            if (forSummary.reportToName != null && !string.IsNullOrEmpty(forSummary.reportToName.fullName.Trim()))
            {
                reportsToLbl.Html = GetLocalResourceObject("FieldReportsTo").ToString() +":"+ forSummary.reportToName.fullName + "<br />";
            }
            else
            {
                reportsToLbl.Html = "";
            }

            usedLeavesLbl.Html = forSummary.usedLeaves + "<br />";
            paidLeavesLbl.Html = forSummary.paidLeaves + "<br />";
            unpaidLeavesLbl.Html = forSummary.unpaidLeaves + "<br />";
            salaryLbl.Html = forSummary.salary.ToString("N2") +" "+forSummary.currencyName+ "<br />";
            TerminationDateLbl.Html = forSummary.terminationDate != null ? forSummary.terminationDate.Value.ToString(_systemService.SessionHelper.GetDateformat()) : "";
            //employeeName.Text = resp.result.name.firstName + resp.result.name.lastName;

            imgControl.ImageUrl = forSummary.pictureUrl + "?x=" + DateTime.Now.Ticks;
            employeePhoto.ImageUrl = forSummary.pictureUrl + "?x=" + DateTime.Now.Ticks; 

            //here
            X.Call("InitCropper", forSummary.pictureUrl + "?x=" + DateTime.Now.Ticks);

            CurrentEmployeePhotoName.Text = forSummary.pictureUrl;
          //  Store1.Reload();
            //ModelProxy record = Store1.GetById(CurrentEmployee.Text);
            //record.Set("pictureUrl", imgControl.ImageUrl);
            //record.Set("departmentName", forSummary.departmentName);
            //record.Set("branchName", forSummary.branchName);
            //record.Set("divisionName", forSummary.divisionName);
            //record.Set("positionName", forSummary.positionName);
            //record.Commit();
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
                 Common.errorMessage(response);
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
                 Common.errorMessage(response);
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
                 Common.errorMessage(response);
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
                 Common.errorMessage(response);
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
                 Common.errorMessage(response);
                return null;
            }

        }


        #endregion


        
        protected void ShowTermination(object sender, DirectEventArgs e)
        {
            
            try
            {
                AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeeTermination), terminationForm, null, null, saveTerminationButton);

            }
            catch (AccessDeniedException exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();

                return;
            }
            terminationForm.Reset();
            FillTerminationReasons1(CurrentEmployee.Text);
            //date.SelectedDate = DateTime.Today;
            terminationWindow.Show();
        }
        protected void ShowSelfService(object sender, DirectEventArgs e)
        {
            RecordRequest empRecord = new RecordRequest();
            empRecord.RecordID = CurrentEmployee.Text.ToString();
            RecordResponse<Employee> empResponse = _employeeService.Get<Employee>(empRecord);
            
       
            if (!empResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", empResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", empResponse.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId").ToString() + empResponse.LogId : empResponse.Summary).Show();
                return;
            }
            if(string.IsNullOrEmpty(empResponse.result.workMail))
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors","EmptyWorkEmail")).Show();
                return;
            }
            workEmailHF.Text = empResponse.result.workMail;
            UserByEmailRequest req = new UserByEmailRequest();
            req.Email = workEmailHF.Text;
            RecordResponse<UserInfo> response = _systemService.Get<UserInfo>(req);
            FillSecurityGroup();
            if (response.result != null)
            {
                if (response.result.isInactive == true)
                {
                    enableSS.Checked = true;
                }
                else
                    enableSS.Checked = false;

              
                GroupUsersListRequest GroupUserReq = new GroupUsersListRequest();
                GroupUserReq.Size = "";
                GroupUserReq.StartAt = "";
                GroupUserReq.Filter = "";
                GroupUserReq.GroupId = "0";
                GroupUserReq.UserId = response.result.recordId;




                //Fetching the corresponding list

                //in this test will take a list of News


                ListResponse<SecurityGroupUser> groups = _accessControlService.ChildGetAll<SecurityGroupUser>(GroupUserReq);
                if (!groups.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, groups.Summary).Show();

                    return;
                }
                if (groups.Items != null && groups.Items.Count!=0)
                    sgId.Select(groups.Items[0].sgId.ToString());
             
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeeTermination), terminationForm, null, null, saveTerminationButton);

                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();

                //    return;
                //}
                //selfServiceForm.Reset();

                //date.SelectedDate = DateTime.Today;

            }
            selfServiceWindow.Show();
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
                eosBalance = qv.result.indemnity,
                paidLeavesYTD = qv.result.usedLeavesLeg,
                leavesBalance = qv.result.leaveBalance,
                allowedLeaveYtd = qv.result.earnedLeavesLeg,
                lastleave = qv.result.LastLeave(_systemService.SessionHelper.GetDateformat())


            };

        }

        private void FillTerminationReasons()
        {
            ListRequest caRequest = new ListRequest();
            ListResponse<TerminationReason> resp = _employeeService.ChildGetAll<TerminationReason>(caRequest);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
                return;
            }
            trStore.DataSource = resp.Items;
            trStore.DataBind();

        }
        private void FillTerminationReasons1(string id)
        {
            RecordRequest caRequest = new RecordRequest();
            caRequest.RecordID = id;
            RecordResponse<EmployeeTermination> resp = _employeeService.ChildGetRecord<EmployeeTermination>(caRequest);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
                return;
            }
            ListRequest caRequest1 = new ListRequest();
            ListResponse<TerminationReason> resp1 = _employeeService.ChildGetAll<TerminationReason>(caRequest1);
            if (!resp1.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp1.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp1.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp1.Summary : resp1.Summary).Show();
                return;
            }
            trStore.DataSource = resp1.Items;
            trStore.DataBind();

            if (resp.result!=null)
            {
                if (resp.result.date != null)
                    date.Value = resp.result.date;
                terminationRecordId.Text = resp.result.recordId;
                ttId.Select(resp.result.ttId);
                rehire.Select(resp.result.rehire);

                //switch (resp.result.ttId)
                //{
                //    case 0:
                //        ttId.AddItem( GetLocalResourceObject("Worker").ToString(),0);
                //        ttId.Select(0);

                //        break;
                //    case 1:
                //        ttId.AddItem(GetLocalResourceObject("Company").ToString(),1);
                //        ttId.Select(1);
                //        break;
                //    case 2:
                //        ttId.AddItem(GetLocalResourceObject("Other").ToString(),2);
                //        ttId.Select(2);
                //        break;
                //    default:
                                           
                //        break;

                //}
               
                if (!string.IsNullOrEmpty(resp.result.trId.ToString()))
                    trId.Select(resp.result.trId);

                //switch (resp.result.rehire)
                //{
                //    case 0:
                //        rehire.Text = GetLocalResourceObject("No").ToString();

                //        break;
                //    case 1:
                //        rehire.Text = GetLocalResourceObject("Yes").ToString();

                //        break;
                //    case 2:
                //        rehire.Text = GetLocalResourceObject("NotYetKnown").ToString();

                //        break;
                //    default:
                //        rehire.Text = "";
                //        break;

                //}

            }
            else
            {
                date.Value = DateTime.Now;
                setTerminationWindow(false);
            }

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
            if (obj.ToLower() != "delete" && obj.ToLower() != "حذف")
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
                 Common.errorMessage(response);

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
                X.Msg.Alert(Resources.Common.ResetPassword, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors","ErrorLogId")+resp.LogId : resp.Summary).Show();
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
                 Common.errorMessage(response);
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
        [DirectMethod]
        public int CheckReference(string r)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = r;
            RecordResponse<Employee> resp = _employeeService.ChildGetRecord<Employee>(req);
            if (resp.Success)
            {
                if (resp.result != null)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ReferenceAlreadyExist")).Show();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        protected void Reference_Validation(object sender, RemoteValidationEventArgs e)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = reference.Text;
            RecordResponse<Employee> resp = _employeeService.ChildGetRecord<Employee>(req);
            if (resp.Success)
            {
                if (resp.result != null)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ReferenceAlreadyExist")).Show();
                    e.Success = false;

                    return;
                }
                else
                {
                    e.Success = true;
                }
            }
        }
        private void SetHijriInputState(bool hijriSupported)
        {
          
            X.Call("setInputState", hijriSupported);


        }
        private void FillCitizenShip()
        {
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Citizenship> routers = _nationalQuotaService.ChildGetAll<Citizenship>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            this.nqciIdStore.DataSource = routers.Items;
          
            this.nqciIdStore.DataBind();
        }

    
        protected void setCitizenship(object sender, DirectEventArgs e)
        {
           
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "countryId";
            RecordResponse<KeyValuePair<string, string>> response = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (nationalityId.SelectedItem.Value== response.result.Value)
            {
                nqciId.Disabled = false;
            }
            else
                nqciId.Disabled = true;
        }
    }
}