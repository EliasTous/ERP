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
using AionHR.Infrastructure.JSON;

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
                if (_systemService.SessionHelper.CheckIfIsAdmin())
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(GenerationHeader), BasicInfoTab, payrollsGrid, AddButton, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeePayroll), EditEMForm, employeePayrolls, null, SaveDocumentButton);
                }
                catch (AccessDeniedException exp)
                {
                    employeePayrolls.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(PayrollEntitlementDeduction), EditEDForm, entitlementsGrid, AddENButton, Button4);
                }
                catch (AccessDeniedException exp)
                {
                    entitlementsGrid.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(PayrollEntitlementDeduction), EditEDForm, deductionGrid, AddEDButton, Button4);
                }
                catch (AccessDeniedException exp)
                {

                    deductionGrid.Hidden = true;

                }
                var properties = AccessControlApplier.GetPropertiesLevels(typeof(PayrollEntitlementDeduction));

                entitlementDisabled.Text = deductionDisabled.Text = (properties.Where(x => x.index == "amount").ToList()[0].accessLevel == 0).ToString();
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
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return new List<FiscalYear>();
            }
            return resp.Items;
        }

        private void FillDepartment()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() : response.Summary).Show();
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
            h.status = "1";
            PostRequest<GenerationHeader> req = new PostRequest<GenerationHeader>();
            req.entity = h;

            PostResponse<GenerationHeader> resp = _payrollService.ChildAddOrUpdate<GenerationHeader>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            CurrentPayId.Text = resp.recordId;
            Viewport1.ActiveIndex = 2;
            FillBranch();
            FillDepartment();
            Store1.Reload();
            RecordRequest r = new RecordRequest();

            r.RecordID = resp.recordId;

            payrollHeader.Text = _payrollService.ChildGetRecord<GenerationHeader>(r).result.payRef;
            AddEDButton.Disabled = AddENButton.Disabled = SaveEDButton.Disabled = false;
        }

        protected void SaveHE(object sender, DirectEventArgs e)
        {

            string s = e.ExtraParams["values"];
            JsonSerializerSettings settings = new JsonSerializerSettings();
            CustomResolver res = new CustomResolver();
            settings.ContractResolver = res;
            res.AddRule("statusCombo", "status");
            GenerationHeader h = JsonConvert.DeserializeObject<GenerationHeader>(s, settings);
            h.recordId = recordId.Text;
            PostRequest<GenerationHeader> req = new PostRequest<GenerationHeader>();
            req.entity = h;

            PostResponse<GenerationHeader> resp = _payrollService.ChildAddOrUpdate<GenerationHeader>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();

                return;
            }
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });
            payrollsStore.Reload();
            EditHeaderWindow.Close();
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
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
            string status = e.ExtraParams["status"];
            IsPayrollPosted.Text = status;
            AddEDButton.Disabled = AddENButton.Disabled = SaveEDButton.Disabled = status == "2";
            switch (type)
            {
                case "imgAttach":
                    //Step 1 : get the object from the Web Service 
                    CurrentPayId.Text = id;
                    Viewport1.ActiveIndex = 2;
                    FillBranch();
                    FillDepartment();
                    payrollHeader.Text = id.ToString();
                    Store1.Reload();

                    break;
                case "imgEdit":
                    RecordRequest req = new RecordRequest();
                    req.RecordID = id;
                    RecordResponse<GenerationHeader> resp = _payrollService.ChildGetRecord<GenerationHeader>(req);
                    if (!resp.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                        return;
                    }
                    statusCombo.Select(resp.result.status);
                    fromDate.SelectedDate = resp.result.startDate;
                    toDate.SelectedDate = resp.result.endDate;
                    recordId.Text = id;
                    EditHeaderWindow.Show();

                    break;
                case "imgDelete":

                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteHeader({0})", id),
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
        public void DeleteHeader(string id)
        {
            GenerationHeader h = new GenerationHeader();
            h.recordId = id;
            PostRequest<GenerationHeader> delReq = new PostRequest<GenerationHeader>();
            delReq.entity = h;
            PostResponse<GenerationHeader> res = _payrollService.ChildDelete<GenerationHeader>(delReq);
            if (!res.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();
                return;
            }
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });
            payrollsStore.Remove(id);

        }
        protected void PoPuPEM(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string basic = e.ExtraParams["basicAmount"];
            string tax = e.ExtraParams["taxAmount"];
            string net = e.ExtraParams["netSalary"];
            string currencyRef = e.ExtraParams["currency"];
            CurrentCurrencyRef.Text = currencyRef;
            CurrentSeqNo.Text = id;

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
                    entitlementsStore.DataSource = GetPayrollEntitlements();
                    entitlementsStore.DataBind();
                    deductionStore.DataSource = GetPayrollDeductions();
                    deductionStore.DataBind();
                    EDWindow.Show();

                    break;
                default:
                    break;
            }


        }

        protected void PoPuPEN(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            string record = e.ExtraParams["values"];
            string entitlement = "";

            PayrollEntitlementDeduction detail = JsonConvert.DeserializeObject<List<PayrollEntitlementDeduction>>(record)[0];
            switch (type)
            {


                case "imgEdit":




                    edStore.DataSource = GetAllEntitlements();
                    edStore.DataBind();
                    edId.Select(detail.edId.ToString());
                    isInsert.Text = "0";
                    this.type.Text = "1";
                    edId.RightButtons[0].Enabled = false;
                    edId.ReadOnly = true;
                    edId.FieldLabel = GetLocalResourceObject("FieldEntitlement").ToString();
                    amount.Text = detail.amount.ToString();

                    EditEDWindow.Show();
                    break;
                case "imgDelete":


                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteEN({0})", id),
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

        protected void PoPuPDE(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            string deduction = "";
            string record = e.ExtraParams["values"];
            PayrollEntitlementDeduction detail = JsonConvert.DeserializeObject<List<PayrollEntitlementDeduction>>(record)[0];
            switch (type)
            {


                case "imgEdit":



                    edStore.DataSource = GetAllDeductions();
                    edStore.DataBind();
                    isInsert.Text = "0";
                    this.type.Text = "2";
                    edId.RightButtons[0].Enabled = false;
                    edId.FieldLabel = GetLocalResourceObject("FieldDeduction").ToString();
                    edId.Select(detail.edId.ToString());
                    amount.Text = Math.Abs(detail.amount).ToString();

                    EditEDWindow.Show();

                    break;
                case "imgDelete":
                    deduction = e.ExtraParams["values"];

                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteDE({0})", id),
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
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }

        [DirectMethod]
        public void DeleteEN(string index)
        {
            try
            {

                //Step 2 :  remove the object from the store
                entitlementsStore.Remove(index);

                //Step 3 : Showing a notification for the user 


            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }
        }
        [DirectMethod]
        public void DeleteDE(string index)
        {
            try
            {

                deductionStore.Remove(index);

                //Step 3 : Showing a notification for the user 


            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }
        }



        protected void fiscalPeriodsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            FiscalPeriodsListRequest req = new FiscalPeriodsListRequest();
            try
            {
                req.Year = fiscalYear.Value.ToString();
                req.PeriodType = (SalaryType)Convert.ToInt32(salaryType.Value.ToString());
                req.Status = "1";

                ListResponse<FiscalPeriod> resp = _payrollService.ChildGetAll<FiscalPeriod>(req);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                    return;
                }
                List<object> obs = new List<Object>();
                resp.Items.ForEach(x => obs.Add(new { recordId = x.periodId, name = x.GetFriendlyName(GetLocalResourceObject("Month").ToString(), GetLocalResourceObject("Week").ToString(), GetLocalResourceObject("Weeks").ToString(), _systemService.SessionHelper.GetDateformat()) }));
                fiscalPeriodsStore.DataSource = obs;
                fiscalPeriodsStore.DataBind();


            }
            catch { }
        }

        public void UpdateStartEndDate(object sender, DirectEventArgs e)
        {
            string s = e.ExtraParams["period"];
            try
            {
                string d = s.Split('(')[1].Split(')')[0];
                X.Call("setStartEnd", d.Split('-')[0], d.Split('-')[1]);
            }
            catch { }
        }

        protected void Store1_ReadData(object sender, StoreReadDataEventArgs e)
        {
            EmployeePayrollListRequest req = GetEmployeePayrollRequest();
            ListResponse<EmployeePayroll> resp = _payrollService.ChildGetAll<EmployeePayroll>(req);
            if (!resp.Success)
            {

                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
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
            if (!headers.Success)
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
            BasicInfoTab.Reset();
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

        protected void ADDNewEN(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            EditEDForm.Reset();
            this.EditEDWindow.Title = Resources.Common.AddNewRecord;
            isInsert.Text = "1";
            this.type.Text = "1";
            edStore.DataSource = GetAllEntitlements();
            edStore.DataBind();
            edId.RightButtons[0].Enabled = true;
            edId.ReadOnly = false;
            edId.FieldLabel = GetLocalResourceObject("FieldEntitlement").ToString();
            this.EditEDWindow.Show();
        }
        private List<EntitlementDeduction> GetAllEntitlements()
        {
            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
            return eds.Items.Where(s => s.type == 1).ToList();



        }
        private List<EntitlementDeduction> GetAllDeductions()
        {
            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
            return eds.Items.Where(s => s.type == 2).ToList();



        }
        protected void ADDNewDE(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            EditEDForm.Reset();
            this.EditEDWindow.Title = Resources.Common.AddNewRecord;
            edStore.DataSource = GetAllDeductions();
            isInsert.Text = "1";
            this.type.Text = "2";
            edStore.DataBind();
            edId.ReadOnly = false;
            edId.RightButtons[0].Enabled = true;
            GetLocalResourceObject("FieldDeduction").ToString();
            this.EditEDWindow.Show();
        }

        protected void SaveED(object sender, DirectEventArgs e)
        {
            string type = e.ExtraParams["type"];

            string obj = e.ExtraParams["values"];

            string insert = e.ExtraParams["isInsert"];
            PayrollEntitlementDeduction b = JsonConvert.DeserializeObject<PayrollEntitlementDeduction>(obj);

            if (edId.SelectedItem != null)
                b.edName = edId.SelectedItem.Text;
            if (type == "2")
                b.amount = -Math.Abs(b.amount);
            else
                b.amount = Math.Abs(b.amount);

            // Define the object to add or edit as null

            if (insert == "1")
            {

                try
                {
                    b.seqNo = CurrentSeqNo.Text;
                    b.payId = CurrentPayId.Text;

                    if (type == "1")
                        this.entitlementsStore.Insert(0, b);
                    else
                        this.deductionStore.Insert(0, b);
                    //Display successful notification


                    this.EditEDWindow.Close();

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
                    ModelProxy record = null;
                    if (type == "1")
                        record = this.entitlementsStore.GetById(b.edId);
                    else
                        record = this.deductionStore.GetById(b.edId);

                    record.Set("edName", b.edName);

                    record.Set("amount", b.amount);

                    record.Commit();

                    this.EditEDWindow.Close();


                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }

        protected void SaveAll(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            string ents = e.ExtraParams["entitlements"];
            string deds = e.ExtraParams["deductions"];

            // Define the object to add or edit as null




            try
            {
                List<PayrollEntitlementDeduction> entitlments = JsonConvert.DeserializeObject<List<PayrollEntitlementDeduction>>(ents);
                List<PayrollEntitlementDeduction> deductions = JsonConvert.DeserializeObject<List<PayrollEntitlementDeduction>>(deds);
                PostRequest<PayrollEntitlementDeduction> delReq = new PostRequest<PayrollEntitlementDeduction>();
                delReq.entity = new PayrollEntitlementDeduction();
                delReq.entity.edId = "0";
                delReq.entity.payId = CurrentPayId.Text;
                delReq.entity.seqNo = CurrentSeqNo.Text;
                PostResponse<PayrollEntitlementDeduction> delResp = _payrollService.ChildDelete<PayrollEntitlementDeduction>(delReq);

                if (!delResp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                    return;

                }
                entitlments.AddRange(deductions);
                PostRequest<PayrollEntitlementDeduction[]> req = new PostRequest<PayrollEntitlementDeduction[]>();
                req.entity = entitlments.ToArray();
                PostResponse<PayrollEntitlementDeduction[]> resp = _payrollService.ChildAddOrUpdate<PayrollEntitlementDeduction[]>(req);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                    return;

                }
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
                Store1.Reload();
                EDWindow.Close();
            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }



        }

        private List<PayrollEntitlementDeduction> GetPayrollEntitlements()
        {
            return GetPayrollEntitlementsDeductions().Where(x => x.type == 1).ToList();
        }
        private List<PayrollEntitlementDeduction> GetPayrollEntitlementsDeductions()
        {
            PayrollEntitlementsDeductionListRequest req = new PayrollEntitlementsDeductionListRequest();
            req.PayId = CurrentPayId.Text;
            req.SeqNo = CurrentSeqNo.Text;

            ListResponse<PayrollEntitlementDeduction> resp = _payrollService.ChildGetAll<PayrollEntitlementDeduction>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return new List<PayrollEntitlementDeduction>();
            }
            return resp.Items;
        }
        private List<PayrollEntitlementDeduction> GetPayrollDeductions()
        {
            return GetPayrollEntitlementsDeductions().Where(x => x.type == 2).ToList();
        }

    }
}