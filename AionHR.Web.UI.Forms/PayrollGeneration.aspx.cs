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
using AionHR.Model.Payroll;

namespace AionHR.Web.UI.Forms
{
    public partial class PayrollGeneration : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
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


                DateFormat.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                Viewport1.ActiveIndex = 0;
                yearStore.DataSource = GetYears();
                yearStore.DataBind();
              
            }

        }

        private void FillCombos()
        {
            fiscalPeriodsStore.DataSource = GetYears();
            fiscalPeriodsStore.DataBind();
        }

        private List<FiscalYear> GetYears()
        {
            ListRequest l = new ListRequest();
            ListResponse<FiscalYear> resp = _payrollService.ChildGetAll<FiscalYear>(l);
            if(!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new List<FiscalYear>();
            }
            return resp.Items;
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
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
        }


        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<Employee> data = GetEmployeesFiltered(prms.Query);

            data.ForEach(s => s.fullName = s.name.fullName);
            //  return new
            // {
            return data;
            //};

        }
        private EmployeePayrollListRequest GetEmployeePayrollRequest()
        {
            EmployeePayrollListRequest req = new EmployeePayrollListRequest();

            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
            {
                req.BranchId = branchId.Value.ToString();


            }
            else
            {
                req.BranchId = "0";
            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
            {
                req.DepartmentId = departmentId.Value.ToString();

            }
            else
            {
                req.DepartmentId = "0";
            }

           

            if (!string.IsNullOrEmpty(employeeId.Text) && employeeId.Value.ToString() != "0")
            {
                req.EmployeeId = employeeId.Value.ToString();


            }
            else
            {
                req.EmployeeId = "0";

            }

            req.PayId = CurrentPayId.Text;
            req.Size = "30";
            req.StartAt = "1";
            req.Filter = "";
          
            return req;
        }
        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = "firstName";

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;




            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            if (!response.Success)
                X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
            return response.Items;
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


        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {
            string s = e.ExtraParams["values"];
            GenerationHeader h = JsonConvert.DeserializeObject<GenerationHeader>(s);
            h.status = "0";
            PostRequest<GenerationHeader> req = new PostRequest<GenerationHeader>();
            req.entity = h;

            PostResponse<GenerationHeader> resp = _payrollService.ChildAddOrUpdate<GenerationHeader>(req);
            if(!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            CurrentPayId.Text = resp.recordId;
            Viewport1.ActiveIndex = 2;
            FillBranch();
            FillDepartment();
        }

        protected void SaveEM(object sender, DirectEventArgs e)
        {
            string id = e.ExtraParams["id"];
            string s = e.ExtraParams["values"];
            EmployeePayroll h = JsonConvert.DeserializeObject<EmployeePayroll>(s);
            h.payId = CurrentPayId.Text;
            h.seqNo = id;
            PostRequest<EmployeePayroll> req = new PostRequest<EmployeePayroll>();
            req.entity = h;

            PostResponse<EmployeePayroll> resp = _payrollService.ChildAddOrUpdate<EmployeePayroll>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            ModelProxy record = this.Store1.GetById(id);
            EditEMForm.UpdateRecord(record);
            record.Commit();
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });
            EditEMWindow.Close();
        }

        protected void PoPuPHeader(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    CurrentPayId.Text = id;
                    Viewport1.ActiveIndex = 2;
                    FillBranch();
                    FillDepartment();
                    Store1.Reload();
                    break;

             
                default:
                    break;
            }


        }
        protected void PoPuPEM(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string basic = e.ExtraParams["basicAmount"];
            string tax = e.ExtraParams["taxAmount"];
            string net = e.ExtraParams["netSalary"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    basicAmount.Text = basic;
                    taxAmount.Text = tax;
                    netSalary.Text = net;
                    seqNo.Text = id;
                    EditEMWindow.Show();
                    break;

                case "imgAttach":
                    break;
                default:
                    break;
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

        protected void fiscalPeriodsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            FiscalPeriodsListRequest req = new FiscalPeriodsListRequest();
            try
            {
                req.Year = fiscalYear.Value.ToString();
                req.PeriodType = (SalaryType)Convert.ToInt32(salaryType.Value.ToString());
                req.Status = 0;
            
            ListResponse<FiscalPeriod> resp = _payrollService.ChildGetAll<FiscalPeriod>(req);
                if (!resp.Success)
                    return;
                List<object> obs = new List<Object>();
                resp.Items.ForEach(x => obs.Add(new {recordId=x.periodId, name=x.GetFriendlyName(GetLocalResourceObject("Month").ToString(),GetLocalResourceObject("Week").ToString(), GetLocalResourceObject("Weeks").ToString(),_systemService.SessionHelper.GetDateformat()) }));
                fiscalPeriodsStore.DataSource = obs;
            fiscalPeriodsStore.DataBind();
                
                
            }
            catch { }
        }
        [DirectMethod]
        public void UpdateStartEndDate()
        {
            string s = periodId.SelectedItem.Text;
            string d = s.Split('(')[1].Split(')')[0];
            X.Call("setStartEnd", d.Split('-')[0], d.Split('-')[1]);
        }

        protected void Store1_ReadData(object sender, StoreReadDataEventArgs e)
        {
            EmployeePayrollListRequest req = GetEmployeePayrollRequest();
            ListResponse<EmployeePayroll> resp = _payrollService.ChildGetAll<EmployeePayroll>(req);
            if(!resp.Success)
            {

                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            Store1.DataSource = resp.Items;
            Store1.DataBind();
        }

        private PayrollListRequest GetPayrollListRequest()
        {
            PayrollListRequest req = new PayrollListRequest();

            if (!string.IsNullOrEmpty(year.Text) && !string.IsNullOrEmpty(year.Value.ToString()))
            {
                req.Year = year.Value.ToString();


            }
            else
            {
                req.Year = "0";
            }

            if (!string.IsNullOrEmpty(salaryTypeFilter.Text) && !string.IsNullOrEmpty(salaryTypeFilter.Value.ToString()))
            {
                req.PeriodType = salaryTypeFilter.Value.ToString();

            }
            else
            {
                req.PeriodType = "1";
            }



            if (!string.IsNullOrEmpty(status.Text) && !string.IsNullOrEmpty(status.Value.ToString()))
            {
                req.Status = status.Value.ToString();


            }
            else
            {
                req.Status = "0";

            }

            
            req.Size = "30";
            req.StartAt = "1";
            req.Filter = "";

            return req;
        }
        protected void payrollsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            PayrollListRequest req = GetPayrollListRequest();

            ListResponse<GenerationHeader> headers = _payrollService.ChildGetAll<GenerationHeader>(req);
            if(!headers.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, headers.Summary).Show();
                return;
            }
            payrollsStore.DataSource = headers.Items;
        }
        [DirectMethod]
        public void AddPayroll()
        {
            fiscalyearStore.DataSource = GetYears();
            fiscalyearStore.DataBind();
            Viewport1.ActiveIndex = 1;
        }

        [DirectMethod]
        public void PayrollPages()
        {
            
            yearStore.DataSource = GetYears();
            yearStore.DataBind();
            Viewport1.ActiveIndex = 0;
        }


    }
}