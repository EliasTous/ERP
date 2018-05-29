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
using AionHR.Model.System;
using AionHR.Model.Attendance;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Model.Payroll;
using AionHR.Infrastructure.JSON;
using AionHR.Services.Messaging.Reports;
using AionHR.Model.Reports;
using Reports;
using AionHR.Model.TimeAttendance;
using DevExpress;
using DevExpress.XtraPrinting;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;

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
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
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
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(PayrollSocialSecurity), null, payCodeGrid, null, null);
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return new List<FiscalYear>();
            }
            return resp.Items;
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
            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value.ToString() : "0";
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value.ToString() : "0";
            var d2 = employeeCombo1.GetEmployee();
            req.EmployeeId = d2.employeeId.ToString();




            req.PayId = CurrentPayId.Text;
            req.Size = "30";
            req.StartAt = "1";
            req.Filter = "";
            

            return req;
        }
        //private EmployeePayrollListRequest GetEmployeePayrollRequest1(string generatePayRef,string  departmentId, string branchId,string employeeId)
        //{
        //    EmployeePayrollListRequest req = new EmployeePayrollListRequest();
        //       req.PayId = CurrentPayId1.Text;
        //    if (branchId == "null")
        //        req.BranchId = "0";
        //    else
        //        req.BranchId = branchId;
        //    if (departmentId == "null")
        //        req.DepartmentId = "0";
        //    else
        //        req.DepartmentId = departmentId;
        //    //if (generatePayRef == "null")
        //    //    req.payRef = "0";
        //    //else
        //    //    req.payRef = generatePayRef;
        //    if (employeeId == "null")
        //        req.EmployeeId = "0";
        //    else
        //        req.EmployeeId = employeeId;

        //    req.Size = "30";
        //    req.StartAt = "1";
        //    req.Filter = "";

        //    return req;
        //}
        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 0;
            req.SortBy = "firstName";

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;




            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            if (!response.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
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

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.DateFormatString = _systemService.SessionHelper.GetDateformat();
            GenerationHeader h = JsonConvert.DeserializeObject<GenerationHeader>(s, settings);
            h.status = "1";
            PostRequest<GenerationHeader> req = new PostRequest<GenerationHeader>();
            req.entity = h;

            PostResponse<GenerationHeader> resp = _payrollService.ChildAddOrUpdate<GenerationHeader>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return;
            }
            CurrentPayId.Text = resp.recordId;
            Viewport1.ActiveIndex = 0;

            Store1.Reload();
            RecordRequest r = new RecordRequest();

            r.RecordID = resp.recordId;


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
            h.recordId = CurrentPayId.Text;
            PostRequest<GenerationHeader> req = new PostRequest<GenerationHeader>();
            req.entity = h;

            PostResponse<GenerationHeader> resp = _payrollService.ChildAddOrUpdate<GenerationHeader>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();

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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
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
            string payRef = e.ExtraParams["payRef"];
            string salaryType = e.ExtraParams["salaryType"];
            string fiscalYear = e.ExtraParams["fiscalYear"];
            CurrentPayId.Text = id;
            salaryTypeHidden.Text = salaryType;
            fiscalYearHidden.Text = fiscalYear;
            payRefHidden.Text = payRef;
            IsPayrollPosted.Text = status;
            AddEDButton.Disabled = AddENButton.Disabled = SaveEDButton.Disabled = status == "2";
            switch (type)
            {
                case "imgAttach":
                    //Step 1 : get the object from the Web Service 
                    CurrentPayId.Text = id;
                    Viewport1.ActiveIndex = 2;


                    Store1.Reload();

                    break;
                case "imgEdit":
                    RecordRequest req = new RecordRequest();
                    req.RecordID = id;
                    RecordResponse<GenerationHeader> resp = _payrollService.ChildGetRecord<GenerationHeader>(req);
                    if (!resp.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                        return;
                    }
                    statusCombo.Select(resp.result.status);
                    fromDate.SelectedDate = resp.result.startDate;
                    toDate.SelectedDate = resp.result.endDate;

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
                case "imgGenerate":
                    CurrentPayId1.Text = id;
                    //Step 1 : get the object from the Web Service 
                    EditGenerateForm.Reset();
                    FillDepartment();
                    FillBranch();
                    generatePayRef.Text = payRef;

                    EditGenerateWindow.Show();

                    // Store1.Reload();

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
                    //   EdRecordId.Text = detail.recordId;
                    edSeqNo.Text = detail.edSeqNo;
                    EDseqNoTF.Text = detail.seqNo;

                    EditEDWindow.Show();

                    break;
                case "imgDelete":
                    deduction = e.ExtraParams["values"];

                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteDE({0})", detail.EDrecordId),
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
        protected void PoPuPEN(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            string record = e.ExtraParams["values"];
            //string entitlement = "";

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
                    edSeqNo.Text = detail.edSeqNo;
                    EDseqNoTF.Text = detail.seqNo;

                    EditEDWindow.Show();
                    break;
                case "imgDelete":


                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteEN({0})",detail.EDrecordId),
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

       
        protected void PoPuPEM(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string entitlementAmount = e.ExtraParams["eAmount"];
            string deductionAmount = e.ExtraParams["dAmount"];
            string EmployeeSaocialAmount = e.ExtraParams["essAmount"];
            string CompanySaocialAmount = e.ExtraParams["cssAmount"];
            string basic = e.ExtraParams["basicAmount"];
            string tax = e.ExtraParams["taxAmount"];
            string net = e.ExtraParams["netSalary"];
            string EMseqNo = e.ExtraParams["seqNo"];
            string currencyRef = e.ExtraParams["currency"];
            CurrentCurrencyRef.Text = currencyRef;
            CurrentSeqNo.Text = id;

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    basicAmount.Text = basic;
                    // taxAmount.Text = tax;
                    netSalary.Text = net;
                    eAmount.Text = entitlementAmount;
                    dAmount.Text = deductionAmount;
                    essAmount.Text = EmployeeSaocialAmount;
                    cssAmount.Text = CompanySaocialAmount;
                    seqNo.Text = id;
                    EditEMWindow.Show();
                    break;

                case "imgAttach":
                    entitlementsStore.DataSource = GetPayrollEntitlements();
                    entitlementsStore.DataBind();
                    deductionStore.DataSource = GetPayrollDeductions();
                    deductionStore.DataBind();
                    PayCodeStore.DataSource = GetPayrollSocialSecurity();
                    PayCodeStore.DataBind();
                    EDWindow.Show();

                    break;
                case "imgDelete":

                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteEM({0})", EMseqNo),
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
        public void DeleteEN(string  record)
        {
            try
            {

                //Step 2 :  remove the object from the store
              

                entitlementsStore.Remove(record);

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
        public void DeleteEM(string seqNo)
        {
            try
            {
                EmployeePayroll EP = new EmployeePayroll();
                EP.payId = CurrentPayId.Text;
                EP.seqNo = seqNo;
                PostRequest<EmployeePayroll> delReq = new PostRequest<EmployeePayroll>();
                delReq.entity = EP;
                PostResponse<EmployeePayroll> res = _payrollService.ChildDelete<EmployeePayroll>(delReq);
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

                //Step 2 :  remove the object from the store
                Store1.Remove(seqNo);

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
        public void DeleteDE(string recordId)
        {
            try
            {
               

       
                deductionStore.Remove(recordId);

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
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
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
        private List<Model.Employees.Profile.EntitlementDeduction> GetAllEntitlements()
        {
            ListRequest req = new ListRequest();
            ListResponse<Model.Employees.Profile.EntitlementDeduction> eds = _employeeService.ChildGetAll<Model.Employees.Profile.EntitlementDeduction>(req);
            return eds.Items.Where(s => s.type == 1).ToList();



        }
        private List<Model.Employees.Profile.EntitlementDeduction> GetAllDeductions()
        {
            ListRequest req = new ListRequest();
            ListResponse<Model.Employees.Profile.EntitlementDeduction> eds = _employeeService.ChildGetAll<Model.Employees.Profile.EntitlementDeduction>(req);
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
            string recordID;
            string type = e.ExtraParams["type"];
            string seqNo = e.ExtraParams["seqNo"];
            string edSeqNo = e.ExtraParams["edSeqNo"];

            string obj = e.ExtraParams["values"];

            string insert = e.ExtraParams["isInsert"];
            
            PayrollEntitlementDeduction b = JsonConvert.DeserializeObject<PayrollEntitlementDeduction>(obj);
           
            b.seqNo = seqNo;
            b.edSeqNo = edSeqNo;
            recordID = b.EDrecordId;

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
                        record = this.entitlementsStore.GetById(b.EDrecordId);
                    else
                        record = this.deductionStore.GetById(b.EDrecordId);

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
              
                

                JsonSerializerSettings s = new JsonSerializerSettings();
               
                s.NullValueHandling = NullValueHandling.Ignore;
             
                List <PayrollEntitlementDeduction> entitlments = JsonConvert.DeserializeObject<List<PayrollEntitlementDeduction>>(ents,s);
                List<PayrollEntitlementDeduction> deductions = JsonConvert.DeserializeObject<List<PayrollEntitlementDeduction>>(deds,s);
              
                PostRequest<PayrollEntitlementDeduction> delReq = new PostRequest<PayrollEntitlementDeduction>();
                delReq.entity = new PayrollEntitlementDeduction();
                delReq.entity.edId = "0";
                delReq.entity.edSeqNo = "0";
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
                entitlments.ForEach(x => x.edSeqNo = null);
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return new List<PayrollEntitlementDeduction>();
            }
            return resp.Items;
        }
        private List<PayrollEntitlementDeduction> GetPayrollDeductions()
        {
           return GetPayrollEntitlementsDeductions().Where(x => x.type == 2).ToList();
        
        }
        private List<PayrollSocialSecurity> GetPayrollSocialSecurity()
        {

            PayrollSocialSecurityListRequest req = new PayrollSocialSecurityListRequest();

            req.payId = CurrentPayId.Text;
            req.seqNo = CurrentSeqNo.Text;

            ListResponse<PayrollSocialSecurity> resp = _payrollService.ChildGetAll<PayrollSocialSecurity>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return new List<PayrollSocialSecurity>();
            }
            return resp.Items;
        }


        public void printBtn_Click(object sender, EventArgs e)
        {
            MonthlyPayroll p = GetReport(payRefHidden.Text);
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
            Response.Clear();
            Response.Write("<script>");
            Response.Write("window.document.forms[0].target = '_blank';");
            Response.Write("setTimeout(function () { window.document.forms[0].target = ''; }, 0);");
            Response.Write("</script>");
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
        }
        protected void ExportPdfBtn_Click(object sender, EventArgs e)
        {
            MonthlyPayroll p = GetReport(payRefHidden.Text);
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms);
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }

        protected void ExportXLSBtn_Click(object sender, EventArgs e)
        {
            MonthlyPayroll p = GetReport(payRefHidden.Text);
            string format = "xls";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToXls(ms);

            Response.Clear();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }
        private MonthlyPayroll GetReport(string payRef)
        {


            ReportCompositeRequest req = GetRequest(payRef);

            ListResponse<AionHR.Model.Reports.RT501> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT501>(req);
            if (!resp.Success)
            {
                               
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                    return new MonthlyPayroll();
                
            }

            var d = resp.Items.GroupBy(x => x.employeeName.fullName);
            PayrollLineCollection lines = new PayrollLineCollection();
            HashSet<Model.Reports.EntitlementDeduction> ens = new HashSet<Model.Reports.EntitlementDeduction>(new EntitlementDeductionComparer());
            HashSet<Model.Reports.EntitlementDeduction> des = new HashSet<Model.Reports.EntitlementDeduction>(new EntitlementDeductionComparer());
            resp.Items.ForEach(x =>
            {
                Model.Reports.EntitlementDeduction DE = new Model.Reports.EntitlementDeduction();

                if (x.edType == 1)
                {

                    try
                    {
                        DE.name = GetLocalResourceObject(x.edName.Trim()).ToString().TrimEnd();
                        DE.amount = 0; DE.isTaxable = x.isTaxable;
                    }
                    catch { DE.name = x.edName; DE.amount = 0; DE.isTaxable = x.isTaxable; }
                    ens.Add(DE);
                }
                else
                {

                    try
                    {
                        DE.name = GetLocalResourceObject(x.edName.Trim()).ToString().TrimEnd();
                        DE.amount = 0;
                    }
                    catch { DE.name = x.edName; DE.amount = 0; }
                    des.Add(DE);
                }
            });
            foreach (var item in d)
            {
                var list = item.ToList();
                list.ForEach(y =>
                {
                    try
                    {
                        y.edName = GetLocalResourceObject(y.edName.Trim()).ToString().TrimEnd();

                    }
                    catch { y.edName = y.edName; }
                });
                PayrollLine line = new PayrollLine(ens, des, list, GetLocalResourceObject("taxableeAmount").ToString(), GetLocalResourceObject("eAmount").ToString(), GetLocalResourceObject("dAmount").ToString(), GetLocalResourceObject("netSalary").ToString(), GetLocalResourceObject("essString").ToString(), GetLocalResourceObject("cssString").ToString(), _systemService.SessionHelper.GetDateformat());
                lines.Add(line);
            }

            MonthlyPayrollCollection s = new MonthlyPayrollCollection();


            if (lines.Count > 0)
            {
                MonthlyPayrollSet p = new MonthlyPayrollSet(GetLocalResourceObject("Entitlements").ToString(), GetLocalResourceObject("Taxable").ToString(), GetLocalResourceObject("Deductions").ToString());
                p.PayPeriodString = resp.Items[0].startDate.ToString(_systemService.SessionHelper.GetDateformat()) + " - " + resp.Items[0].endDate.ToString(_systemService.SessionHelper.GetDateformat());
                p.PayDate = GetLocalResourceObject("PaidAt") + " " + resp.Items[0].payDate.ToString(_systemService.SessionHelper.GetDateformat());
                p.Names = (lines[0] as PayrollLine).Entitlements;
                p.DIndex = ens.Count;
                p.taxableIndex = ens.Count(x => x.isTaxable);
                p.Payrolls = lines;
                s.Add(p);
            }

            MonthlyPayroll h = new MonthlyPayroll();
            h.DataSource = s;

            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            string user = _systemService.SessionHelper.GetCurrentUser();
            h.Parameters["User"].Value = user;
            if (resp.Items.Count > 0)
            {



                h.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");



                h.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");



                h.Parameters["Payment"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_payRef"] != "0")
                    h.Parameters["Ref"].Value = req.Parameters["_payRef"];
                else
                    h.Parameters["Ref"].Value = GetGlobalResourceObject("Common", "All");

            }


            return h;



        }
        private ReportCompositeRequest GetRequest(string payRef)
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "1";
            PayRefParameterSet p = new PayRefParameterSet();
            p.payRef = payRef;
            PaymentMethodParameterSet Pm = new PaymentMethodParameterSet();
            Pm.paymentMethod = 0;
            //JobInfoParameterSet jp = new JobInfoParameterSet();
            //jp.BranchId = 0;
            //jp.DepartmentId = 0;
            req.Add(p);
            req.Add(Pm);
          req.Add(jobInfo1.GetJobInfo());
            req.Add(employeeCombo1.GetEmployee());

            return req;
        }

        protected void GeneratePayroll1(object sender, DirectEventArgs e)
        {

            string id = CurrentPayId1.Text;
            string departmentId = e.ExtraParams["departmentId"];
            string branchId = e.ExtraParams["branchId"];
            string employeeId = e.ExtraParams["employeeId"];
            GeneratePayroll h = new GeneratePayroll();
            if (departmentId == "" || departmentId == "null" || string.IsNullOrEmpty(departmentId))
                h.departmentId = 0;
            else
                h.departmentId =Convert.ToInt32(departmentId);
            if (branchId=="" || branchId == "null" || string.IsNullOrEmpty(branchId))
                h.branchId = 0; 
            else
                h.branchId = Convert.ToInt32(branchId);
            if (employeeId=="" || employeeId == "null" || string.IsNullOrEmpty(employeeId))
                h.employeeId = 0;
            else
              h.employeeId = Convert.ToInt32(employeeId);
            if (id != "" || id != "null" || !string.IsNullOrEmpty(id)) 
                h.payId = Convert.ToInt32(id); 
         
             
            PostRequest<GeneratePayroll> req = new PostRequest<GeneratePayroll>();
            req.entity = h;

            PostResponse<GeneratePayroll> resp = _payrollService.ChildAddOrUpdate<GeneratePayroll>(req);
            if (!resp.Success)
            {

                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return;
            }
           
            EditGenerateWindow.Close();
            Store1.Reload();
            Viewport1.ActiveIndex = 2; 

        }
        private void FillDepartment()
        {
            

            DepartmentListRequest req = new DepartmentListRequest();
            req.type = 0; 
            
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(req);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
         
        }
        private void FillBranch()
        {
         
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;
          
        }

    }
}