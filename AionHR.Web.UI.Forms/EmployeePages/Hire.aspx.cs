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
using AionHR.Model.Benefits;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Model.TimeAttendance;

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class Hire : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IBenefitsService _benefitsService = ServiceLocator.Current.GetInstance<IBenefitsService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();


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
                try
                {

                    SetExtLanguage();
                    HideShowButtons();
                    holidayCount.Text = "0";
                    if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                    CurrentEmployee.Text = Request.QueryString["employeeId"];
                    FillNoticePeriod();
                    FillBranchField();
                    FillSponseCombo();
                    FillPrevRecordIdField();
                    FillBenefitSchedules();


                    HireInfoRecordRequest req = new HireInfoRecordRequest();
                    req.EmployeeId = Request.QueryString["employeeId"];
                    RecordResponse<HireInfo> resp = _employeeService.ChildGetRecord<HireInfo>(req);
                    if (!resp.Success)
                    {
                        Common.errorMessage(resp);
                        return;
                    }


                    employeeCaId.Text = resp.result.caId.ToString();



                    probationPeriod.SuspendEvent("Change");
                    if (resp.result != null)
                    {
                        hireInfoForm.SetValues(resp.result);
                        npId.Select(resp.result.npId.ToString());
                        recBranchId.Select(resp.result.recBranchId.ToString());
                        sponsorId.Select(resp.result.sponsorId.ToString());
                        prevRecordId.Select(resp.result.prevRecordId.ToString());


                        pyActiveDate.Value = resp.result.pyActiveDate;
                        probationPeriod.Value = resp.result.probationPeriod;
                        //if(resp.result.probationEndDate!=null)
                        //    probationEndDateHidden.Value = resp.result.probationEndDate;
                        //else
                        //    probationEndDateHidden.Value = resp.result.hireDate;
                        hireDate.Text = resp.result.hireDate.Value.ToShortDateString();
                        probationEndDate.MinDate = Convert.ToDateTime(resp.result.hireDate);
                        bsId.Select(resp.result.bsId.ToString());


                    }
                    else
                    {
                        RecordRequest r = new RecordRequest();
                        r.RecordID = Request.QueryString["employeeId"];
                        RecordResponse<Employee> response = _employeeService.Get<Employee>(r);

                        probationEndDateHidden.Value = response.result.hireDate;
                        hireDate.Text = response.result.hireDate.Value.ToShortDateString();
                        probationEndDate.Value = response.result.hireDate;
                        probationEndDate.MinDate = Convert.ToDateTime(response.result.hireDate);
                        pyActiveDate.Value = response.result.hireDate;
                    }

                    probationEndDate.Format = nextReviewDate.Format = termEndDate.Format = pyActiveDate.Format = _systemService.SessionHelper.GetDateformat();

                    EmployeeTerminated.Text = Request.QueryString["terminated"];

                    bool disabled = EmployeeTerminated.Text == "1";

                    saveButton.Disabled = disabled;
                    //probationPeriod.Value = 0;

                    probationPeriod.ResumeEvent("Change");



                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(HireInfo), left, null, null, saveButton);

                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport11.Hidden = true;
                        return;
                    }
                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(HireInfo), rightPanel, null, null, saveButton);

                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport11.Hidden = true;
                        return;
                    }
                    if (recruitmentInfo.InputType == InputType.Password)
                    {
                        recruitmentInfo.Visible = false;
                        infoField.Visible = true;
                    }
                }
                catch (Exception exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
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


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport11.RTL = true;

            }
        }


        protected void SaveHire(object sender, DirectEventArgs e)
        {
            string info = e.ExtraParams["values"];
            HireInfo h = JsonConvert.DeserializeObject<HireInfo>(info);

            PostRequest<HireInfo> req = new PostRequest<HireInfo>();
            req.entity = h;
            h.employeeId = CurrentEmployee.Text;

            PostResponse<HireInfo> resp = _employeeService.ChildAddOrUpdate<HireInfo>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }

            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });


        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>


        [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }

        private void FillNoticePeriod()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<NoticePeriod> resp = _employeeService.ChildGetAll<NoticePeriod>(departmentsRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            npStore.DataSource = resp.Items;
            npStore.DataBind();
        }
        protected void addNP(object sender, DirectEventArgs e)
        {
            NoticePeriod dept = new NoticePeriod();
            dept.name = npId.Text;

            PostRequest<NoticePeriod> depReq = new PostRequest<NoticePeriod>();
            depReq.entity = dept;
            PostResponse<NoticePeriod> response = _employeeService.ChildAddOrUpdate<NoticePeriod>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillNoticePeriod();
                npId.Select(dept.recordId);
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
            Branch obj = new Branch();
            obj.name = recBranchId.Text;
            obj.isInactive = false;
            PostRequest<Branch> req = new PostRequest<Branch>();
            req.entity = obj;

            PostResponse<Branch> response = _companyStructureService.ChildAddOrUpdate<Branch>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillBranchField();
                recBranchId.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(response);
                return;
            }


        }
        private void FillSponseCombo()
        {
            ListRequest sponserRequest = new ListRequest();
            ListResponse<Sponsor> resp = _employeeService.ChildGetAll<Sponsor>(sponserRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            sponsorStore.DataSource = resp.Items;
            sponsorStore.DataBind();
        }
        private void FillBranchField()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
        }
        [DirectMethod]
        protected void Unnamed_Event()
        {


        }
        private void FillPrevRecordIdField()
        {

            EmployeeListRequest req = new EmployeeListRequest();
            //req.DepartmentId = "0";
            //req.BranchId = "0";
            //req.IncludeIsInactive = 1;
            req.SortBy = GetNameFormat();

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = "";

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            response.Items.ForEach(x => x.fullName = x.name.fullName);
            prevRecordStore.DataSource = response.Items;
            prevRecordStore.DataBind();
        }


        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }

        private void FillBenefitSchedules()
        {
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<BenefitsSchedule> routers = _benefitsService.ChildGetAll<BenefitsSchedule>(request);
            if (!routers.Success)
                return;
            this.Store2.DataSource = routers.Items;


            this.Store2.DataBind();

        }

        private  string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '-' || c == ':' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        [DirectMethod]
        public void calHolidays(string probationEndDate)
        {

            probationEndDate = RemoveSpecialCharacters(probationEndDate);

            WorkingDayListRequest reqCD2 = new WorkingDayListRequest();
            DateTime date = DateTime.Now;
          if (!DateTime.TryParse(hireDate.Value.ToString(), out date))
              {
                return;
              }
            reqCD2.StartDayId = date.ToString("yyyyMMdd");
            if (!DateTime.TryParse(probationEndDate, out date))
            {
                return;
            }

            reqCD2.EndDayId = date.ToString("yyyyMMdd");
            reqCD2.IsWorkingDay = false;
            if (string.IsNullOrEmpty(employeeCaId.Text))
            {
                return;
            }
            reqCD2.caId = Convert.ToInt32(employeeCaId.Text);


                reqCD2.employeeId = Request.QueryString["employeeId"];

                ListResponse<LeaveCalendarDay> holidayRespone = _timeAttendanceService.ChildGetAll<LeaveCalendarDay>(reqCD2);
                if (!holidayRespone.Success)
                {
                    Common.errorMessage(holidayRespone);
                return;
                }
            holidayCount.Text = holidayRespone.count.ToString();






        }

       
    }
}