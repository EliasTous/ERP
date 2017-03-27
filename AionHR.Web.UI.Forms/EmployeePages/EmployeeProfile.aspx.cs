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


namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class EmployeeProfile : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!X.IsAjaxRequest && !IsPostBack)
            {
                string employeeId = Request.QueryString["employeeId"];
                LoadEmployee(employeeId);
            }


        }
       
        private void LoadEmployee(string id)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = id;
            RecordResponse<Employee> response = _employeeService.Get<Employee>(req);
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
            InitCombos();
            SelectCombos(response.result);
        }
        private void FillNameFields(EmployeeName name)
        {
            X.Call("setNameFields", name.firstName, name.lastName, name.middleName);
            firstName.Text = name.firstName;
            lastName.Text = name.lastName;
            middleName.Text = name.middleName;
            familyName.Text = name.familyName;
            
        }

        private void SelectCombos(Employee result)
        {
            branchId.Select(result.branchId);
            departmentId.Select(result.departmentId);
            positionId.Select(result.positionId);
            nationalityId.Select(result.nationalityId);
            sponsorId.Select(result.sponsorId);
            vsId.Select(result.vsId);
            caId.Select(result.caId);

            if (result.gender == 1)
                gender1.Checked = true;
            else
                gender0.Checked = true;
            //if (!string.IsNullOrEmpty(result.pictureUrl))
            //    imgControl.ImageUrl = result.pictureUrl;

        }
        private void InitCombos()
        {
            FillBranch();


            FillDepartment();


            FillPosition();


            FillNationality();


            FillSponsor();


            FillVacationSchedule();


            FillWorkingCalendar();



        }

        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            Employee b = JsonConvert.DeserializeObject<Employee>(obj);
            b.name = new EmployeeName() { firstName = firstName.Text, lastName = lastName.Text, familyName = familyName.Text, middleName = middleName.Text };
            b.isInactive = isInactive.Checked;
            b.recordId = id;
            // Define the object to add or edit as null
            if (branchId.SelectedItem != null)
                b.branchName = branchId.SelectedItem.Text;
            if (departmentId.SelectedItem != null)
                b.departmentName = departmentId.SelectedItem.Text;
            if (positionId.SelectedItem != null)
                b.positionName = positionId.SelectedItem.Text;
            b.name.fullName = b.name.firstName + " " + b.name.middleName + " " + b.name.lastName + " ";
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequestWithAttachment<Employee> request = new PostRequestWithAttachment<Employee>();

                    byte[] fileData = null;
                    //if (picturePath.PostedFile != null && picturePath.PostedFile.ContentLength > 0)
                    //{
                    //    using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                    //    {
                    //        fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                    //    }
                    //    request.fileName = picturePath.PostedFile.FileName;
                    //    request.imageData = fileData;
                    //}
                    //else
                    //{
                    request.FileData = fileData;
                        request.FileName = "";
                    //}
                    request.entity = b;



                    PostResponse<Employee> r = _employeeService.AddOrUpdateWithAttachment<Employee>(request);
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
                    //if (picturePath.HasFile && picturePath.PostedFile.ContentLength > 0)
                    //{
                    //    //using (var binaryReader = new BinaryReader(picturePath.PostedFile.InputStream))
                    //    // {
                    //    //    fileData = binaryReader.ReadBytes(picturePath.PostedFile.ContentLength);
                    //    // }
                    //    fileData = new byte[picturePath.PostedFile.ContentLength];
                    //    fileData = picturePath.FileBytes;
                    //    request.fileName = picturePath.PostedFile.FileName;
                    //    request.imageData = fileData;



                    //}
                    //else
                    //{
                        request.imageData = fileData;
                        request.fileName = "";
                    //}
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


                       
                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }
        private void FillNationality()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(nationalityRequest);
            NationalityStore.DataSource = resp.Items;
            NationalityStore.DataBind();

        }

        private void FillPosition()
        {
            ListRequest positionsRequest = new ListRequest();
            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(positionsRequest);
            positionStore.DataSource = resp.Items;
            positionStore.DataBind();
        }
        private void FillDepartment()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            BranchStore.DataSource = resp.Items;
            BranchStore.DataBind();
        }
        private void FillSponsor()
        {
            ListRequest sponsorsRequest = new ListRequest();
            ListResponse<Sponsor> resp = _employeeService.ChildGetAll<Sponsor>(sponsorsRequest);
            SponsorStore.DataSource = resp.Items;
            SponsorStore.DataBind();
        }
        private void FillVacationSchedule()
        {
            ListRequest vsRequest = new ListRequest();
            ListResponse<VacationSchedule> resp = _leaveManagementService.ChildGetAll<VacationSchedule>(vsRequest);
            VacationScheduleStore.DataSource = resp.Items;
            VacationScheduleStore.DataBind();
        }
        private void FillWorkingCalendar()
        {
            ListRequest caRequest = new ListRequest();
            ListResponse<WorkingCalendar> resp = _timeAttendanceService.ChildGetAll<WorkingCalendar>(caRequest);
            CalendarStore.DataSource = resp.Items;
            CalendarStore.DataBind();

        }
    }
}