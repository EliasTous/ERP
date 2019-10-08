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
using AionHR.Services.Messaging.Reports;
using AionHR.Model.Reports;
using Reports;
using AionHR.Model.TimeAttendance;
using DevExpress;
using DevExpress.XtraPrinting;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;
using AionHR.Services.Messaging.HelpFunction;
using AionHR.Model.HelpFunction;
using AionHR.Infrastructure.Session;
using System.Threading;
using AionHR.Infrastructure.Tokens;
using AionHR.Services.Implementations;
using AionHR.Repository.WebService.Repositories;
using Reports.EmployeePayRoll;
using Newtonsoft.Json.Linq;
using DevExpress.XtraReports.UI;

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
        IHelpFunctionService _helpFunctionService= ServiceLocator.Current.GetInstance<IHelpFunctionService>();
        SessionHelper h;


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


                DateFormat.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                Viewport1.ActiveIndex = 0;
                yearStore.DataSource = GetYears();
                yearStore.DataBind();
                salaryTypeId.setSalaryType("5");
                salaryTypeId.ADDHandler("select", "App.payrollsStore.reload();");
                FillBranch();
                FillDepartment();
                status.Select("0");
                payrollsStore.Reload();

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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(PayrollEntitlementDeduction), EditEDForm, entitlementsGrid, AddENButton, saveEDBT);
                }
                catch (AccessDeniedException exp)
                {
                    entitlementsGrid.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(PayrollEntitlementDeduction), EditEDForm, deductionGrid, AddEDButton, saveEDBT);
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
                //var properties = AccessControlApplier.GetPropertiesLevels(typeof(PayrollEntitlementDeduction));

                //entitlementDisabled.Text = deductionDisabled.Text = (properties.Where(x => x.index == "amount").ToList()[0].accessLevel == 0).ToString();

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
                Common.errorMessage(resp);
                return new List<FiscalYear>();
            }
            return resp.Items;
        }




        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }
        private EmployeePayrollListRequest GetEmployeePayrollRequest()
        {
            EmployeePayrollListRequest req = new EmployeePayrollListRequest();
            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value.ToString() : "0";
         //   req.PositionId = d.PositionId.HasValue ? d.PositionId.Value.ToString() : "0";
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value.ToString() : "0";
           // req.Position = d.PositionId.HasValue ? d.PositionId.Value.ToString() : "0";
            var d2 = employeeCombo1.GetEmployee();
            req.EmployeeId = d2.employeeId.ToString();




            req.PayId = CurrentPayId.Text;
            req.Size = "50";
           
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
            req.entity.salaryType = SalaryType2.GetSalaryTypeId();

            PostResponse<GenerationHeader> resp = _payrollService.ChildAddOrUpdate<GenerationHeader>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            CurrentPayId.Text = resp.recordId;
            Viewport1.ActiveIndex = 0;

            Store1.Reload();
            payrollsStore.Reload();
            RecordRequest r = new RecordRequest();

            r.RecordID = resp.recordId;


            AddEDButton.Disabled = AddENButton.Disabled = saveEDBT.Disabled = false;
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
                Common.errorMessage(resp);

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
                Common.errorMessage(resp);
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
            if (status == "1")
                DeleteAll.Disabled = false;
            else
                DeleteAll.Disabled = true;
            CurrentPayId.Text = id;
            salaryTypeHidden.Text = salaryType;
            fiscalYearHidden.Text = fiscalYear;

            payRefHidden.Text = payRef;
            IsPayrollPosted.Text = status;
            AddEDButton.Disabled = AddENButton.Disabled = /*SaveEDButton.Disabled*/  status == "2";
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
                        Common.errorMessage(resp);
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
                    GenerateCurrentPayroll.Text = "true";
                    double progress = 0;
                    string prog = (float.Parse(progress.ToString()) * 100).ToString();
                    string message = GetGlobalResourceObject("Common", "working").ToString();
                    this.Progress1.UpdateProgress(float.Parse(progress.ToString()), string.Format(message + " {0}%", (int)(float.Parse(progress.ToString()) * 100)));

                    CurrentPayId.Text = id;
                    //Step 1 : get the object from the Web Service 
                    EditGenerateForm.Reset();
                    FillDepartment();
                    FillBranch();
                    generatePayRef.Text = payRef;
                    GenerateButton.Text = Resources.Common.Generate;
                    GenerateButton.ID = "ApplicationGo";
                    this.Progress1.Reset();
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
                Common.errorMessage(res);
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
                            Handler = String.Format("App.direct.DeleteED({0},{1},{2},{3},{4})", detail.payId, detail.seqNo, detail.edId, detail.edSeqNo,"2"),
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
                            Handler = String.Format("App.direct.DeleteED({0},{1},{2},{3},{4})", detail.payId, detail.seqNo, detail.edId, detail.edSeqNo, "1"),
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
        public void DeleteED(string payId,string seqNo,string edId, string edSeqNo,string type)
        {
            PayrollEntitlementDeduction ED = new PayrollEntitlementDeduction();
            ED.payId = payId;
            ED.seqNo = seqNo;
            ED.edId = edId;
            ED.edSeqNo = edSeqNo;
            ED.type = Convert.ToInt32(type);


            try
            {
                PostRequest<PayrollEntitlementDeduction> delReq = new PostRequest<PayrollEntitlementDeduction>();
                delReq.entity = ED;
                PostResponse<PayrollEntitlementDeduction> resp = _payrollService.ChildDelete<PayrollEntitlementDeduction>(delReq);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(resp);
                    return;
                }
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });

                if (type == "1")
                {
                    entitlementsStore.DataSource = GetPayrollEntitlements();
                    entitlementsStore.DataBind();
                }
                else
                {
                    deductionStore.DataSource = GetPayrollDeductions();
                    deductionStore.DataBind();
                }
            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, ex.Message).Show();

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
                    Common.errorMessage(res);
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
        //public void DeleteDE(PayrollEntitlementDeduction DE)
        //{
        //    try
        //    {



        //        try
        //        {
        //            PostRequest<PayrollEntitlementDeduction> delReq = new PostRequest<PayrollEntitlementDeduction>();
        //            delReq.entity = DE;
        //            PostResponse<PayrollEntitlementDeduction> resp = _payrollService.ChildDelete<PayrollEntitlementDeduction>(delReq);
        //            if (!resp.Success)
        //            {
        //                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //                Common.errorMessage(resp);
        //                return;
        //            }
        //            Notification.Show(new NotificationConfig
        //            {
        //                Title = Resources.Common.Notification,
        //                Icon = Icon.Information,
        //                Html = Resources.Common.RecordUpdatedSucc
        //            });
        //            deductionStore.Reload();

        //        }
        //        catch (Exception ex)
        //        {
        //            //In case of error, showing a message box to the user
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //            X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

        //        }

        //        //Step 3 : Showing a notification for the user 

        //    }
          
        //}



        protected void fiscalPeriodsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            FiscalPeriodsListRequest req = new FiscalPeriodsListRequest();
            try
            {
                req.Year = fiscalYear.Value.ToString();
                if (string.IsNullOrEmpty(req.Year))
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Errors.FillFiscalYear).Show();
                    Viewport1.ActiveIndex = 0;
                    return;
                }
                req.PeriodType = Convert.ToInt32(SalaryType2.GetSalaryTypeId());
                req.Status = "1";

                ListResponse<FiscalPeriod> resp = _payrollService.ChildGetAll<FiscalPeriod>(req);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(resp);
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
        [DirectMethod]
        public void UpdateStartEndDateFromController()
        {
            fiscalPeriodsStore.Reload();
            string s = periodId.Value.ToString();
            try
            {
                string d = s.Split('(')[1].Split(')')[0];
                X.Call("setStartEnd", d.Split('-')[0], d.Split('-')[1]);
            }
            catch { }
        }

        protected void Store1_ReadData(object sender, StoreReadDataEventArgs e)
        {
            paySlipsStartAt.Text = "0";
            EmployeePayrollListRequest req = GetEmployeePayrollRequest();
            req.StartAt = e.Start.ToString();
            ListResponse<EmployeePayroll> resp = _payrollService.ChildGetAll<EmployeePayroll>(req);
            if (!resp.Success)
            {

                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
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

            if (!string.IsNullOrEmpty(salaryTypeId.GetSalaryTypeId()) )
            {
                req.PeriodType = salaryTypeId.GetSalaryTypeId();

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
            req.StartAt = "0";
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
                Common.errorMessage(headers);
                return;
            }
            payrollsStore.DataSource = headers.Items;
        }
        [DirectMethod]
        public void AddPayroll()
        {
            BasicInfoTab.Reset();
           
            SalaryType2.setSalaryType("5");
            SalaryType2.ADDHandler("select", "App.direct.UpdateStartEndDateFromController()");
            fiscalyearStore.DataSource = GetYears();
            fiscalyearStore.DataBind();
            Viewport1.ActiveIndex = 1;
        }

        [DirectMethod]
        public void PayrollPages()
        {

            yearStore.DataSource = GetYears();
            yearStore.DataBind();
            salaryTypeId.setSalaryType("5");
          
            status.Select("0");
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
            edId.FieldLabel=GetLocalResourceObject("FieldDeduction").ToString();
            this.EditEDWindow.Show();
        }

        protected void SaveED(object sender, DirectEventArgs e)
        {
            string recordID;
            string type = e.ExtraParams["type"];
            string seqNo = e.ExtraParams["seqNo"];
            string edSeqNo = e.ExtraParams["edSeqNo"];
            string masterId = e.ExtraParams["masterId"];
           
            string obj = e.ExtraParams["values"];

            string insert = e.ExtraParams["isInsert"];
            
            PayrollEntitlementDeduction b = JsonConvert.DeserializeObject<PayrollEntitlementDeduction>(obj);
           
            b.seqNo = seqNo;
            b.edSeqNo = edSeqNo;
            b.masterId = masterId;
            recordID = b.EDrecordId;
            b.payId = CurrentPayId.Text;
        

            if (edId.SelectedItem != null)
                b.edName = edId.SelectedItem.Text;

            b.type = Convert.ToInt32(type);
            //if (type == "2")
            //    b.amount = -Math.Abs(b.amount);
            //else
            //    b.amount = Math.Abs(b.amount);

            // Define the object to add or edit as null

            if (insert == "1")
            {

                try
                {
                    b.seqNo = CurrentSeqNo.Text;
                    b.payId = CurrentPayId.Text;



                    PostRequest<PayrollEntitlementDeduction> req = new PostRequest<PayrollEntitlementDeduction>();
                    req.entity = b;
                    PostResponse<PayrollEntitlementDeduction> resp = _payrollService.ChildAddOrUpdate<PayrollEntitlementDeduction>(req);
                    if (!resp.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(resp);
                        return;

                    }
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordUpdatedSucc
                    });

                    if (type == "1")

                    {
                        entitlementsStore.DataSource = GetPayrollEntitlements();
                        entitlementsStore.DataBind();
                    }

                    else
                    {
                        deductionStore.DataSource = GetPayrollDeductions();
                        deductionStore.DataBind();
                    }
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

                    PostRequest<PayrollEntitlementDeduction> req = new PostRequest<PayrollEntitlementDeduction>();
                    req.entity = b;
                    PostResponse<PayrollEntitlementDeduction> resp = _payrollService.ChildAddOrUpdate<PayrollEntitlementDeduction>(req);
                    if (!resp.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(resp);
                        return;

                    }
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordUpdatedSucc
                    });

                    if (type == "1")
                    {
                        entitlementsStore.DataSource = GetPayrollEntitlements();
                        entitlementsStore.DataBind();
                    }  
                    else
                    {
                        deductionStore.DataSource = GetPayrollDeductions();
                        deductionStore.DataBind();
                    }
                   

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

                List<PayrollEntitlementDeduction> entitlments = JsonConvert.DeserializeObject<List<PayrollEntitlementDeduction>>(ents, s);
                List<PayrollEntitlementDeduction> deductions = JsonConvert.DeserializeObject<List<PayrollEntitlementDeduction>>(deds, s);

                //PostRequest<PayrollEntitlementDeduction> delReq = new PostRequest<PayrollEntitlementDeduction>();
                //delReq.entity = new PayrollEntitlementDeduction();
                //delReq.entity.edId = "0";
                //delReq.entity.edSeqNo = "0";
                //delReq.entity.payId = CurrentPayId.Text;
                //delReq.entity.seqNo = CurrentSeqNo.Text;
                //PostResponse<PayrollEntitlementDeduction> delResp = _payrollService.ChildDelete<PayrollEntitlementDeduction>(delReq);

                //if (!delResp.Success)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", delResp.ErrorCode) != null ? GetGlobalResourceObject("Errors", delResp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + delResp.LogId : delResp.Summary).Show();
                //    return;

                //}
                entitlments.AddRange(deductions);
                //entitlments.ForEach(x => x.edSeqNo = null);
                PostRequest<PayrollEntitlementDeduction[]> req = new PostRequest<PayrollEntitlementDeduction[]>();
                req.entity = entitlments.ToArray();
                PostResponse<PayrollEntitlementDeduction[]> resp = _payrollService.ChildAddOrUpdate<PayrollEntitlementDeduction[]>(req);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(resp);
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
                Common.errorMessage(resp);
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
                Common.errorMessage(resp);
                return new List<PayrollSocialSecurity>();
            }
            return resp.Items;
        }


        //public void printBtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        MonthlyPayroll p = GetReport(payRefHidden.Text);

        //        string format = "Pdf";
        //        string fileName = String.Format("Report.{0}", format);

        //        MemoryStream ms = new MemoryStream();
        //        p.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
        //        Response.Clear();
        //        Response.Write("<script>");
        //        Response.Write("window.document.forms[0].target = '_blank';");
        //        Response.Write("setTimeout(function () { window.document.forms[0].target = ''; }, 0);");
        //        Response.Write("</script>");
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
        //        Response.BinaryWrite(ms.ToArray());
        //        Response.Flush();
        //        Response.Close();
        //    }
        //    catch(Exception exp)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //    }
        //}
        //protected void ExportPdfBtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        MonthlyPayroll p = GetReport(payRefHidden.Text);
        //        string format = "Pdf";
        //        string fileName = String.Format("Report.{0}", format);

        //        MemoryStream ms = new MemoryStream();
        //        p.ExportToPdf(ms);
        //        Response.Clear();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
        //        Response.BinaryWrite(ms.ToArray());
        //        Response.Flush();
        //        Response.Close();
        //    }
        //    catch (Exception exp)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //    }
        //    //Response.Redirect("Reports/RT301.aspx");
        //}

        //protected void ExportXLSBtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        MonthlyPayroll p = GetReport(payRefHidden.Text);
        //        string format = "xls";
        //        string fileName = String.Format("Report.{0}", format);

        //        MemoryStream ms = new MemoryStream();
        //        p.ExportToXls(ms);

        //        Response.Clear();

        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
        //        Response.BinaryWrite(ms.ToArray());
        //        Response.Flush();
        //        Response.Close();




        //    }
        //    catch (Exception exp)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //    }

        //}
        protected void printPaySlip_Click(object sender, EventArgs e)
      
        {
            try
            {
               EmployeesPaySlip p = GetReport(CurrentPayId.Text);

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
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        protected void ExportPdfPaySlip_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeesPaySlip p = GetReport(CurrentPayId.Text);
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
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
            //Response.Redirect("Reports/RT301.aspx");
        }

        //protected void ExportXLSPaySlip_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        MonthlyPayroll p = GetReport(payRefHidden.Text);
        //        string format = "xls";
        //        string fileName = String.Format("Report.{0}", format);

        //        MemoryStream ms = new MemoryStream();
        //        p.ExportToXls(ms);

        //        Response.Clear();

        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
        //        Response.BinaryWrite(ms.ToArray());
        //        Response.Flush();
        //        Response.Close();




        //    }
        //    catch (Exception exp)
        //    {
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //    }

        //}
        private EmployeesPaySlip GetReport(string payId)
        {
           
          
            try
            {
                string rep_params = "";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("1", payId);
                parameters.Add("7", string.IsNullOrEmpty(selectedEmployeeId.Text) ? "0" : selectedEmployeeId.Text);

                // ReportCompositeRequest req = GetRequest(payId);


                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
                }
                if (rep_params.Length > 0)
                {
                    if (rep_params[rep_params.Length - 1] == '^')
                        rep_params = rep_params.Remove(rep_params.Length - 1);
                }


                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                
                ListResponse<AionHR.Model.Reports.RT507> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT507>(req);


            
                if (!resp.Success)
                {

                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(resp);
                  
                    return new EmployeesPaySlip(new List<RT501>(), _systemService.SessionHelper.CheckIfArabicSession(), new Dictionary<string, string>());

                }
                List<RT501> newlist = new List<RT501>();
                resp.Items.ForEach(x =>
                {
                    newlist.Add(x);

                });


                parameters = new Dictionary<string, string>();
                EmployeesPaySlip h = new EmployeesPaySlip(newlist, _systemService.SessionHelper.CheckIfArabicSession(), parameters);


              

                h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
                h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
                string user = _systemService.SessionHelper.GetCurrentUser();
                h.Parameters["user"].Value = user;


                return h;
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                return new EmployeesPaySlip(new List<RT501>(), _systemService.SessionHelper.CheckIfArabicSession(),new Dictionary<string, string>());

            }



        }
        private ReportCompositeRequest GetRequest(string payId)
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "50";
            req.StartAt = paySlipsStartAt.Text;
            PayIdParameterSet p = new PayIdParameterSet();
            p.payId = payId;
            PaymentMethodParameterSet Pm = new PaymentMethodParameterSet();
            Pm.paymentMethod = 0;
            //JobInfoParameterSet jp = new JobInfoParameterSet();
            //jp.BranchId = 0;
            //jp.DepartmentId = 0;
            req.Add(p);
            req.Add(Pm);
            JobInfoParameterSet j = jobInfo1.GetJobInfo();
            j.DivisionId = 0;
             req.Add(j);
            DateRangeParameterSet dateRange = new DateRangeParameterSet();
            dateRange.DateFrom = DateTime.Parse("1970-01-01");
            dateRange.DateTo = DateTime.Parse("2050-01-01");
            req.Add(dateRange);
            EmployeeParameterSet s = new EmployeeParameterSet();
            s.employeeId =string.IsNullOrEmpty(selectedEmployeeId.Text)?0: Convert.ToInt32(selectedEmployeeId.Text);
            req.Add(s);

            return req;
        }

      
        private void FillDepartment()
        {
            

            DepartmentListRequest req = new DepartmentListRequest();
            req.type = 0; 
            
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(req);
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
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;
          
        }

        protected void ExportExcel_Click(object sender, EventArgs e)
        {
            try
            {

                //MonthlyPayroll p = GetReport(payRefHidden.Text);
                //string format = "xls";
                //string fileName = String.Format("Report.{0}", format);

                //MemoryStream ms = new MemoryStream();
                //p.ExportToXls(ms);

                //Response.Clear();

                //Response.ContentType = "application/vnd.ms-excel";
                //Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
                //Response.BinaryWrite(ms.ToArray());
                //Response.Flush();
                //Response.Close();


                PayrollExportListRequest PX = new PayrollExportListRequest();
                PX.payRef = payRefHidden.Text;

                ListResponse<PayrollExport> resp = _helpFunctionService.ChildGetAll<PayrollExport>(PX);
                PayrollsExport p = new PayrollsExport();
                p.DataSource = resp.Items;

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
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }

        protected void deleteAllEmployeePayrolls(object sender, DirectEventArgs e)
        {
            double  progress = 0;
            string prog = (float.Parse(progress.ToString()) * 100).ToString();
            string message = GetGlobalResourceObject("Common", "working").ToString();
            this.Progress1.UpdateProgress(float.Parse(progress.ToString()), string.Format(message + " {0}%", (int)(float.Parse(progress.ToString()) * 100)));


            GenerateCurrentPayroll.Text="false";
                generatePayRef.Text = payRefHidden.Text; 
                GenerateButton.Text = Resources.Common.DeleteAll;
            this.Progress1.Reset();
            EditGenerateWindow.Show();
                //    EmployeePayrollListRequest req = GetEmployeePayrollRequest();
                //    ListResponse<EmployeePayroll> resp = _payrollService.ChildGetAll<EmployeePayroll>(req);
                //    if (!resp.Success)
                //    {

                //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                //        return;
                //    }
                //    resp.Items.ForEach(x =>
                //    {


                //        PostRequest<EmployeePayroll> delReq = new PostRequest<EmployeePayroll>();
                //        delReq.entity = x;
                //        PostResponse<EmployeePayroll> res = _payrollService.ChildDelete<EmployeePayroll>(delReq);
                //        if (!res.Success)
                //        {

                //            throw new Exception(res.Message,new Exception(res.ErrorCode, new Exception (res.LogId)));
                //        }


                //    });


                //    Notification.Show(new NotificationConfig
                //    {
                //        Title = Resources.Common.Notification,
                //        Icon = Icon.Information,
                //        Html = Resources.Common.RecordUpdatedSucc
                //    });

                //    Store1.Reload();
                //}
                //catch(Exception exp)
                //{
                //    if (exp.InnerException!=null)
                //        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", exp.InnerException.Message) != null ? GetGlobalResourceObject("Errors", exp.InnerException.Message).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + exp.InnerException.InnerException.Message: exp.Message).Show();
                //    else
                //    X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                //}

            }
     
        protected void StartPayList(object sender, DirectEventArgs e)
        {
            try
            {
               


               
                this.payListProgressBar.Reset();
                double progress = 0;
                string prog = (float.Parse(progress.ToString()) * 100).ToString();
                string message = GetGlobalResourceObject("Common", "working").ToString();
                this.payListProgressBar.UpdateProgress(float.Parse(progress.ToString()), string.Format(message + " {0}%", (int)(float.Parse(progress.ToString()) * 100)));
                payListWidow.Show();

             



                DictionarySessionStorage storage = new DictionarySessionStorage();
                storage.Save("AccountId", _systemService.SessionHelper.Get("AccountId"));
                storage.Save("UserId", _systemService.SessionHelper.Get("UserId"));
                storage.Save("key", _systemService.SessionHelper.Get("Key"));
                h = new SessionHelper(storage, new APIKeyBasedTokenGenerator());

                //HttpRuntime.Cache.Insert("TotalRecords", 0);
                //HttpRuntime.Cache.Insert("LongActionProgress", 0);
                //HttpRuntime.Cache.Insert("finished", "0");

                ThreadPool.QueueUserWorkItem(GeneratPayList, new object[] { h });



                this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", TaskManager1.ClientID);



            }
            catch (Exception exp)
            {
                if (exp.InnerException != null)
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", exp.InnerException.Message) != null ? GetGlobalResourceObject("Errors", exp.InnerException.Message).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + exp.InnerException.InnerException.Message : exp.Message).Show();
                else
                    X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }

        protected void StartLongAction(object sender, DirectEventArgs e)
        {
            string id = CurrentPayId.Text;
            string departmentId = e.ExtraParams["departmentId"];
            string branchId = e.ExtraParams["branchId"];
            string employeeId = e.ExtraParams["employeeId"];
            GeneratePayroll G = new GeneratePayroll();
            if (departmentId == "" || departmentId == "null" || string.IsNullOrEmpty(departmentId))
                G.departmentId = 0;
            else
                G.departmentId = Convert.ToInt32(departmentId);
            if (branchId == "" || branchId == "null" || string.IsNullOrEmpty(branchId))
                G.branchId = 0;
            else
                G.branchId = Convert.ToInt32(branchId);
            if (employeeId == "" || employeeId == "null" || string.IsNullOrEmpty(employeeId))
                G.employeeId = 0;
            else
                G.employeeId = Convert.ToInt32(employeeId);
            if (id != "" || id != "null" || !string.IsNullOrEmpty(id))
                G.payId = Convert.ToInt32(id);
            //this.Session["LongActionProgressGenAD"] = 0;
            DictionarySessionStorage storage = new DictionarySessionStorage();
            storage.Save("AccountId", _systemService.SessionHelper.Get("AccountId"));
            storage.Save("UserId", _systemService.SessionHelper.Get("UserId"));
            storage.Save("key", _systemService.SessionHelper.Get("Key"));
            storage.Save("LanguageId", _systemService.SessionHelper.Get("Language").ToString() == "en" ? "1" : "2");
            h = new SessionHelper(storage, new APIKeyBasedTokenGenerator());

            //HttpRuntime.Cache.Insert("TotalRecords", 0);
            //HttpRuntime.Cache.Insert("LongActionProgress", 0);
            //HttpRuntime.Cache.Insert("finished", "0");

            ThreadPool.QueueUserWorkItem(GeneratePayroll1, new object[] { h,G });



            this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", TaskManager1.ClientID);



        }

        protected void RefreshProgress(object sender, DirectEventArgs e)
        {

            try
            {


                double progress = 0;
                if (
                HttpRuntime.Cache.Get("ErrorMsgGenEM")!=null ||
                HttpRuntime.Cache.Get("ErrorLogIdGenEM") != null ||
                HttpRuntime.Cache.Get("ErrorErrorCodeGenEM") != null )
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", "Error_" + HttpRuntime.Cache.Get("ErrorErrorCodeGenEM")) != null ? GetGlobalResourceObject("Errors", "Error_" + HttpRuntime.Cache.Get("ErrorErrorCodeGenEM").ToString()).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + HttpRuntime.Cache.Get("ErrorLogIdGenEM").ToString() : HttpRuntime.Cache.Get("ErrorMsgGenEM").ToString()).Show();
                    HttpRuntime.Cache.Remove("genEM_RecordId");
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    EditGenerateWindow.Close();
                    payListWidow.Close();
                    Viewport1.ActiveIndex = 0;
                  if  ( HttpRuntime.Cache.Get("ErrorErrorCodeGenEM") != null)
                        HttpRuntime.Cache.Remove("ErrorErrorCodeGenEM");
                    if (HttpRuntime.Cache.Get("ErrorLogIdGenEM") != null)
                        HttpRuntime.Cache.Remove("ErrorLogIdGenEM");
                    if (HttpRuntime.Cache.Get("ErrorErrorCodeGenEM") != null)
                        HttpRuntime.Cache.Remove("ErrorErrorCodeGenEM");



                }


                if (!Common.isThreadSafe())
                {
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    HttpRuntime.Cache.Remove("isThreadSafe");
                    Common.errorMessage(null);
                }
                else
                {
                    Common.increasThreadSafeCounter();
                }
                RecordRequest req = new RecordRequest();
                if (HttpRuntime.Cache["genEM_RecordId"] != null)
                    req.RecordID = HttpRuntime.Cache["genEM_RecordId"].ToString();
                else
                {
                   // this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    return;
                }
                RecordResponse<BackgroundJob> resp = _systemService.ChildGetRecord<BackgroundJob>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    HttpRuntime.Cache.Remove("genEM_RecordId");
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    EditGenerateWindow.Close();
                    Viewport1.ActiveIndex = 0;
                    payListWidow.Close();
                    return; 
                }
                if ( resp.result.errorId != null)
                {
                

                    X.Msg.Alert(Resources.Common.Error,resp.result.errorName ).Show();
                    HttpRuntime.Cache.Remove("genEM_RecordId");
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    EditGenerateWindow.Close();
                    Viewport1.ActiveIndex = 0;
                    payListWidow.Close();
                }
                else
                {


                    if (resp.result.taskSize == 0)
                    {
                        progress = 0;
                        this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                        EditGenerateWindow.Close();
                        Store1.Reload();
                        Viewport1.ActiveIndex = 2;
                        payListWidow.Close();
                        X.Msg.Alert("",GenerateCurrentPayroll.Text=="true"?Resources.Common.GenerateAttendanceDaySucc : Resources.Common.deleteAttendanceDaySucc).Show();
                    }
                    else
                    {
                        progress = (double)resp.result.completed / resp.result.taskSize;
                        string prog = (float.Parse(progress.ToString()) * 100).ToString();
                        string message = GetGlobalResourceObject("Common", "working").ToString();
                        this.Progress1.UpdateProgress(float.Parse(progress.ToString()), string.Format(message + " {0}%", (int)(float.Parse(progress.ToString()) * 100)));
                        this.payListProgressBar.UpdateProgress(float.Parse(progress.ToString()), string.Format(message + " {0}%", (int)(float.Parse(progress.ToString()) * 100)));
                        
                    }


                    if (resp.result.taskSize == resp.result.completed)
                    {
                        this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                        HttpRuntime.Cache.Remove("genEM_RecordId");
                        EditGenerateWindow.Close();
                        Store1.Reload();
                        Viewport1.ActiveIndex = 2;
                        payListWidow.Close();
                        X.Msg.Alert("",Resources.Common.operationCompleted ).Show();

                    }
                }
            }
            catch (Exception exp)
            {
                this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();

            }









        }

        protected void GeneratePayroll1(object state)
        {
            try
            {
                object[] array = state as object[];
                SessionHelper h = (SessionHelper)array[0];

                PayrollService   payrollService = new PayrollService(new PayrollRepository(),h);
                GeneratePayroll G = (GeneratePayroll)array[1];
                if (GenerateCurrentPayroll.Text == "true")
                {
                    PostRequest<GeneratePayroll> req = new PostRequest<GeneratePayroll>();
                    req.entity = G;


                    PostResponse<GeneratePayroll> resp = payrollService.ChildAddOrUpdate<GeneratePayroll>(req);
                    if (!resp.Success)
                    { //Show an error saving...

                        HttpRuntime.Cache.Insert("ErrorMsgGenEM", resp.Summary);
                        HttpRuntime.Cache.Insert("ErrorLogIdGenEM", resp.LogId);
                        HttpRuntime.Cache.Insert("ErrorErrorCodeGenEM", resp.ErrorCode);

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(resp.recordId))
                            HttpRuntime.Cache.Insert("genEM_RecordId", resp.recordId);
                    }
                }
                else
                {
                    PostRequest<DeletePayroll> req = new PostRequest<DeletePayroll>();
                    req.entity = new DeletePayroll { branchId = G.branchId, departmentId = G.departmentId, employeeId = G.employeeId, payId = G.payId ,positionId=0};


                    PostResponse<DeletePayroll> resp = payrollService.ChildAddOrUpdate<DeletePayroll>(req);
                      if (!resp.Success)
                    { //Show an error saving...

                        HttpRuntime.Cache.Insert("ErrorMsgGenEM", resp.Summary);
                        HttpRuntime.Cache.Insert("ErrorLogIdGenEM", resp.LogId);
                        HttpRuntime.Cache.Insert("ErrorErrorCodeGenEM", resp.ErrorCode);

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(resp.recordId))
                            HttpRuntime.Cache.Insert("genEM_RecordId", resp.recordId);
                    }
                }

              

            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
              
            }
        }
        protected void GeneratPayList(object state)
        {
            try
            {
                object[] array = state as object[];
                SessionHelper h = (SessionHelper)array[0];

                PayrollService payrollService = new PayrollService(new PayrollRepository(), h);

                PostRequest<MailEmployee> req = new PostRequest<MailEmployee>();
                req.entity = new MailEmployee { recordId = CurrentPayId.Text };
                PostResponse<MailEmployee> resp = payrollService.ChildAddOrUpdate<MailEmployee>(req);
              
                if (!resp.Success)
                    { //Show an error saving...

                    HttpRuntime.Cache.Insert("ErrorMsgGenEM", resp.Summary);
                    HttpRuntime.Cache.Insert("ErrorLogIdGenEM", resp.LogId);
                    HttpRuntime.Cache.Insert("ErrorErrorCodeGenEM", resp.ErrorCode);

                }
                else
                {
                    if (!string.IsNullOrEmpty(resp.recordId))
                        HttpRuntime.Cache.Insert("genEM_RecordId", resp.recordId);
                }





            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();

            }
        }

        protected void fiscalYearSelected(object sender, DirectEventArgs e)
        {
            salaryTypeId.setSalaryType("5");
            payrollsStore.Reload();

        }
        public byte[] GetBuffer(XtraReport report)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                report.ExportToImage(stream, new DevExpress.XtraPrinting.ImageExportOptions(System.Drawing.Imaging.ImageFormat.Jpeg));
                return stream.ToArray();
            }
        }
    }
}