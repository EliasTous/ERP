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
using AionHR.Infrastructure.JSON;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;
using AionHR.Model.Payroll;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Services.Messaging.System;

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class Payroll : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();


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
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                CurrentEmployee.Text = Request.QueryString["employeeId"];


                date.Format = effectiveDate.Format = cc.Format = ccc.Format = _systemService.SessionHelper.GetDateformat();

                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";

                Button6.Disabled = Button1.Disabled = Button11.Disabled = Button14.Disabled = Button12.Disabled = Button4.Disabled = SaveENButton.Disabled = Button15.Disabled = disabled;
                if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                    return;

                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeeSalary), firstPanel, SalaryGrid, Button6, Button12);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    SalaryGrid.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeeSalary), secondPanel, SalaryGrid, Button6, Button12);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    SalaryGrid.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Bonus), BOForm, BonusGrid, Button1, Button4);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    BonusGrid.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SalaryDetail), ENForm, entitlementsGrid, Button11, SaveENButton);
                    ApplyAccessControlEntitlements();
                }
                catch (AccessDeniedException exp)
                {

                    entitlementsForm.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SalaryDetail), DEForm, deductionGrid, Button14, Button15);
                    ApplyAccessControlDeductions();
                }
                catch (AccessDeniedException exp)
                {

                    DeductionForm.Hidden = true;

                }
                if (enComment.InputType == InputType.Password)
                {
                    enComment.Visible = false;
                    enCommentField.Visible = true;
                }
                if (deComment.InputType == InputType.Password)
                {
                    deComment.Visible = false;
                    deCommentField.Visible = true;
                }
            }

        }

        private void ApplyAccessControlEntitlements()
        {


            var properties = AccessControlApplier.GetPropertiesLevels(typeof(SalaryDetail));

            properties.ForEach(property =>
            {
                switch (property.propertyId)
                {
                    case "3106301": entEdId.Disabled = property.accessLevel < 1; entEdId.InputType = property.accessLevel < 1 ? InputType.Password : InputType.Text; entEdId.ReadOnly = property.accessLevel < 2 ? true : false; break;
                    default: break;
                }
            });

        }

        private void ApplyAccessControlDeductions()
        {
            var properties = AccessControlApplier.GetPropertiesLevels(typeof(SalaryDetail));
            properties.ForEach(property =>
            {
                switch (property.propertyId)
                {
                    case "3106301": dedEdId.Disabled = property.accessLevel < 1; dedEdId.InputType = property.accessLevel < 1 ? InputType.Password : InputType.Text; dedEdId.ReadOnly = property.accessLevel < 2 ? true : false; break;
                    default: break;
                }
            });

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
            // this.colAttach.Visible = false;
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



        protected void PoPuPSA(object sender, DirectEventArgs e)
        {

            fillSalaryType();
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            currentGrossSalary.Text = e.ExtraParams["grossSalary"];
            switch (type)
            {

                case "imgEdit":
                    entitlementsForm.Disabled = false;
                    DeductionForm.Disabled = false;
                    RecordRequest r3 = new RecordRequest();
                    r3.RecordID = id.ToString();
                    RecordResponse<EmployeeSalary> response3 = _employeeService.ChildGetRecord<EmployeeSalary>(r3);
                    if (!response3.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response3.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.EditSAForm.SetValues(response3.result);
                    FillCurrency();
                    FillScr();
                    FillBank();
                    dedsStore.Reload();
                    entsStore.Reload();
                    basicAmount.Text=response3.result.basicAmount.ToString("N");
                    finalAmount.Text = response3.result.finalAmount.ToString("N");
                    eAmount.Text= response3.result.eAmount.ToString("N2");
                    dAmount.Text = response3.result.dAmount.ToString("N2");
                    CurrentSalary.Text = r3.RecordID;
                    CurrentSalaryCurrency.Text = response3.result.currencyRef;
                    currencyId.Select(response3.result.currencyId.ToString());
                    bankId.Select(response3.result.bankId);
                    if (response3.result.scrId.HasValue)

                        scrId.Select(response3.result.scrId.Value.ToString());

                    X.Call("TogglePaymentMethod", response3.result.paymentMethod);
                   
                    FillEntitlements();
                    FillDeductions();
                    this.EditSAWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditSAWindow.Show();

                    TabPanel2.ActiveIndex = 0;
                    BasicSalary.Text = e.ExtraParams["sal"];
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteSA({0})", id),
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

        protected void PoPuPBO(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];

            switch (type)
            {


                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteBO({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;
                case "imgEdit":
                    RecordRequest r1 = new RecordRequest();
                    r1.RecordID = id.ToString();
                    RecordResponse<Bonus> response1 = _employeeService.ChildGetRecord<Bonus>(r1);
                    if (!response1.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response1.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BOForm.SetValues(response1.result);
                    FillCurrencyBO();

                    FillBT();
                    CurrencyCombo.Select(response1.result.currencyId.ToString());
                    btId.Select(response1.result.btId.ToString());
                    this.EditBOWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditBOWindow.Show();
                    break;

                default:
                    break;
            }


        }

        protected void PoPuPEN(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            string entitlement = "";
            SalaryDetail entDetail = null;
            switch (type)
            {


                case "imgEdit":

                    string record = e.ExtraParams["values"];
                    SalaryDetail detail = JsonConvert.DeserializeObject<List<SalaryDetail>>(record)[0];
                    if (!detail.includeInTotal.HasValue)
                        detail.includeInTotal = false;
                    //entsStore.Reload();
                    ENId.Text = detail.seqNo.ToString();
                    if (detail.fixedAmount == 0)
                        oldEntValue.Text = ((detail.pct / 100) * Convert.ToDouble(BasicSalary.Text)).ToString();
                    else
                        oldEntValue.Text = detail.fixedAmount.ToString();
                    oldENIncludeInFinal.Checked = detail.includeInTotal.Value;
                    ENForm.SetValues(detail);
                    entEdId.Select(detail.edId.ToString());
                    if (detail.pct != 0)
                    {
                        enPCT.Disabled = false;
                        enIsPct.Checked = true;
                        enFixedAmount.Disabled = true;

                    }
                    else
                    {
                        enIsPct.Checked = false;
                        enPCT.Disabled = true;
                        enFixedAmount.Disabled = false;

                    }
                    enFixedAmount.Text = detail.fixedAmount.ToString("N2");
                    EditENWindow.Show();
                    break;
                case "imgDelete":
                    entitlement = e.ExtraParams["values"];
                    entDetail = JsonConvert.DeserializeObject<List<SalaryDetail>>(entitlement)[0];
                    if (!entDetail.includeInTotal.HasValue)
                        entDetail.includeInTotal = false;
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteEN({0},{1},'{2}')", id, entDetail.fixedAmount, entDetail.includeInTotal.Value),
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
            SalaryDetail dedDetail = null;
            switch (type)
            {


                case "imgEdit":

                    deduction = e.ExtraParams["values"];
                    dedDetail = JsonConvert.DeserializeObject<List<SalaryDetail>>(deduction)[0];
                    if (!dedDetail.includeInTotal.HasValue)
                        dedDetail.includeInTotal = false;
                    DEId.Text = dedDetail.seqNo.ToString();
                    DEForm.SetValues(dedDetail);
                    if (dedDetail.fixedAmount == 0)
                        DEoldValue.Text = ((dedDetail.pct / 100) * Convert.ToDouble(BasicSalary.Text)).ToString();
                    else
                        DEoldValue.Text = dedDetail.fixedAmount.ToString();

                    oldDEIncludeInFinal.Checked = dedDetail.includeInTotal.Value;
                    dedEdId.Select(dedDetail.edId.ToString());

                    if (dedDetail.pct != 0)
                    {
                        dePCT.Disabled = false;
                        deFixedAmount.Disabled = true;
                        if (dedDetail.pctOf > 2 || dedDetail.pctOf < 1)
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorWithPCTOf")).Show();
                            dedDetail.pctOf = 1;
                        }
                        pctOf.Select(dedDetail.pctOf.ToString());
                        pctOf.Disabled = false;
                        deIsPCT.Checked = true;
                    }
                    else
                    {
                        dePCT.Disabled = true;
                        deFixedAmount.Disabled = false;
                        pctOf.Disabled = true;
                        deIsPCT.Checked = false;
                    }
                    deFixedAmount.Text = dedDetail.fixedAmount.ToString("N2");
                    EditDEWindow.Show();
                    break;
                case "imgDelete":
                    deduction = e.ExtraParams["values"];
                    dedDetail = JsonConvert.DeserializeObject<List<SalaryDetail>>(deduction)[0];
                    if (!dedDetail.includeInTotal.HasValue)
                        dedDetail.includeInTotal = false;
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteDE({0},{1},'{2}')", id, dedDetail.fixedAmount, dedDetail.includeInTotal.Value),
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
        public void DeleteSA(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeSalary n = new EmployeeSalary();
                n.recordId = index;
                n.isTaxable = 0;
                n.finalAmount = 0;
                n.employeeId = Convert.ToInt32(CurrentEmployee.Text);
                n.comments = "";
                n.accountNumber = "";
                n.basicAmount = 0;
                n.currencyId = 0;

                n.effectiveDate = DateTime.Now;
                n.paymentFrequency = n.paymentMethod = n.salaryType = 0;
                n.scrId = 0;


                PostRequest<EmployeeSalary> req = new PostRequest<EmployeeSalary>();
                req.entity = n;
                PostResponse<EmployeeSalary> res = _employeeService.ChildDelete<EmployeeSalary>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", res.ErrorCode) != null ? GetGlobalResourceObject("Errors", res.ErrorCode).ToString()+"<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + res.LogId : res.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    SAStore.Remove(index);

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
        public void DeleteBO(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Bonus n = new Bonus();
                n.amount = 0;
                n.btId = 0;
                n.comment = "";
                n.currencyId = 0;
                n.date = DateTime.Now;
                n.employeeId = 0;
                n.recordId = index;



                PostRequest<Bonus> req = new PostRequest<Bonus>();
                req.entity = n;
                PostResponse<Bonus> res = _employeeService.ChildDelete<Bonus>(req);
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
                    BOStore.Remove(index);

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
        public void DeleteEN(string index, double value, string includeInTotalString)
        {
            try
            {
                bool includeInTotal = Convert.ToBoolean(includeInTotalString);
                //Step 2 :  remove the object from the store
                entitlementsStore.Remove(index);

                //Step 3 : Showing a notification for the user 
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordDeletedSucc
                });
                if (includeInTotal)
                {
                    X.Call("ChangeFinalAmount", -value);
                    X.Call("ChangeEntitlementsAmount", -value);
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
        public void DeleteDE(string index, double value, string includeInTotalString)
        {
            try
            {
                bool includeInTotal = Convert.ToBoolean(includeInTotalString);
                //Step 2 :  remove the object from the store
                deductionStore.Remove(index);

                //Step 3 : Showing a notification for the user 
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordDeletedSucc
                });
                if (includeInTotal)
                {
                    X.Call("ChangeFinalAmount", value);
                    X.Call("ChangeDeductionsAmount", value);
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

        protected void ADDNewSA(object sender, DirectEventArgs e)
        {
            fillSalaryType();
            CurrentSalary.Text = "";
            entitlementsForm.Disabled = true;
            DeductionForm.Disabled = true; 

            //Reset all values of the relative object
            EditSAForm.Reset();
            entitlementsStore.DataSource = new List<SalaryDetail>();
            entitlementsStore.DataBind();
            deductionStore.DataSource = new List<SalaryDetail>();
            deductionStore.DataBind();
            TabPanel2.ActiveIndex = 0;
            CurrentSalaryCurrency.Text = "";
            this.EditSAWindow.Title = Resources.Common.AddNewRecord;
            FillCurrency();
            FillScr();
            FillBank();
            currencyId.Select(_systemService.SessionHelper.GetDefaultCurrency());
            dedsStore.Reload();
            entsStore.Reload();
            paymentMethod.Select("1");
           
            effectiveDate.SelectedDate = DateTime.Today;
            ENSeq.Text = "0";
            DESeq.Text = "0";

            this.EditSAWindow.Show();
        }

        protected void ADDNewBO(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            BOForm.Reset();
            this.EditBOWindow.Title = Resources.Common.AddNewRecord;
            FillCurrencyBO();
            date.SelectedDate = DateTime.Today;
            FillBT();
            CurrencyCombo.Select(_systemService.SessionHelper.GetDefaultCurrency());
            this.EditBOWindow.Show();
        }

        protected void ADDNewEN(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            ENForm.Reset();
            this.EditENWindow.Title = Resources.Common.AddNewRecord;
            entsStore.Reload();

            this.EditENWindow.Show();
        }

        protected void ADDNewDE(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            DEForm.Reset();
            this.EditDEWindow.Title = Resources.Common.AddNewRecord;
            dedsStore.Reload();

            this.EditDEWindow.Show();
        }

        protected void SAStore_Refresh(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                EmployeeSalaryListRequest request = new EmployeeSalaryListRequest();
                request.Filter = "";
                request.EmployeeId = CurrentEmployee.Text;

                request.Filter = "";

                request.Size = "50";
                request.StartAt = "0";

                ListResponse<EmployeeSalary> currencies = _employeeService.ChildGetAll<EmployeeSalary>(request);
                if (!currencies.Success)
                {
                    Common.errorMessage(currencies);
                    return;
                }
                this.SAStore.DataSource = currencies.Items;
                e.Total = currencies.count;

                this.SAStore.DataBind();
            }
            catch(Exception exp)
            {
                X.MessageBox.Alert(Resources.Common.Error, exp.Message);
            }
        }

        protected void BOStore_Refresh(object sender, StoreReadDataEventArgs e)
        {
            EmployeeBonusListRequest request = new EmployeeBonusListRequest();
            request.Filter = "";
            request.EmployeeId = Convert.ToInt32(CurrentEmployee.Text);
            request.CurrencyId = 0;
            request.BonusTypeId = 0;
            request.Filter = "";

            request.Size = "50";
            request.StartAt = "0";
            ListResponse<Bonus> currencies = _employeeService.ChildGetAll<Bonus>(request);
            if (!currencies.Success)
            {
                X.Msg.Alert(Resources.Common.Error, currencies.Summary).Show();
                this.BOStore.DataSource = new List<Bonus>();
                this.BOStore.DataBind();
                return;
            }
            this.BOStore.DataSource = currencies.Items;
            e.Total = currencies.count;

            this.BOStore.DataBind();
        }




        protected void SaveSA(object sender, DirectEventArgs e)
        {

            try
            { 
            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];
            string basicAmount = e.ExtraParams["basicAmount"];
            string finalAmount = e.ExtraParams["finalAmount"];
            //string eAmount = e.ExtraParams["eAmount"].Replace(",", string.Empty).ToString();
            //string dAmount = e.ExtraParams["dAmount"].Replace(",", string.Empty).ToString();

            string obj = e.ExtraParams["values"];
            string ents = e.ExtraParams["entitlements"];
            string deds = e.ExtraParams["deductions"];
            List<SalaryDetail> entitlements = null;
            List<SalaryDetail> deductions = null;
            EmployeeSalary b = JsonConvert.DeserializeObject<EmployeeSalary>(obj);
            b.basicAmount = Convert.ToDouble(basicAmount);
            b.finalAmount = Convert.ToDouble(finalAmount);
            
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
           
            b.recordId = id;
            b.effectiveDate = new DateTime(b.effectiveDate.Year, b.effectiveDate.Month, b.effectiveDate.Day, 14, 0, 0);
            try
            {
                if (currencyId.SelectedItem != null)
                    b.currencyRef = currencyId.SelectedItem.Text;
            }
            catch { }
            try
            {
                if (bankId.SelectedItem != null)
                    b.bankName = bankId.SelectedItem.Text;
            }
            catch { }
            try
            {
                if (scrId.SelectedItem != null)
                    b.scrName = scrId.SelectedItem.Text;
            }
            catch { }
            try
            {
                if (!b.isTaxable.HasValue) ;

                b.isTaxable = 0;
            }
            catch { }
                // Define the object to add or edit as null


                if (string.IsNullOrEmpty(id))
                {

                    try
                    {
                        //New Mode
                        //Step 1 : Fill The object and insert in the store 
                        PostRequest<EmployeeSalary> request = new PostRequest<EmployeeSalary>();
                        request.entity = b;
                        request.entity.recordId = CurrentSalary.Text;
                        PostResponse<EmployeeSalary> r = _employeeService.ChildAddOrUpdate<EmployeeSalary>(request);
                        if (!r.Success)//it maybe be another condition
                        {
                            //Show an error saving...
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(r);
                            return;
                        }
                        if (!string.IsNullOrEmpty(CurrentSalary.Text))
                        {
                            this.EditSAWindow.Close();
                            RowSelectionModel sm = this.SalaryGrid.GetSelectionModel() as RowSelectionModel;
                            sm.DeselectAll();
                            sm.Select(b.recordId.ToString());

                        }
                        entitlementsForm.Disabled = false;
                        DeductionForm.Disabled = false;
                        b.recordId = r.recordId;
                        CurrentSalary.Text = b.recordId;
                        entitlements = JsonConvert.DeserializeObject<List<SalaryDetail>>(ents);

                        deductions = JsonConvert.DeserializeObject<List<SalaryDetail>>(deds);

                        PostResponse<SalaryDetail[]> result = AddSalaryEntitlementsDeductions(b.recordId, entitlements, deductions);


                        if (!result.Success)
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", result.ErrorCode) != null ? GetGlobalResourceObject("Errors", result.ErrorCode).ToString() : result.Summary).Show();
                            return;
                        }
                        //check if the insert failed

                        else
                        {

                            //Add this record to the store 
                            //this.SAStore.Insert(0, b);
                            this.SAStore.Reload();
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
                        PostRequest<EmployeeSalary> request = new PostRequest<EmployeeSalary>();
                        request.entity = b;
                        PostResponse<EmployeeSalary> r = _employeeService.ChildAddOrUpdate<EmployeeSalary>(request);
                        if (!r.Success)//it maybe another check
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(r);
                            return;
                        }//Step 1 Selecting the object or building up the object for update purpose
                        var deleteDesponse = _employeeService.DeleteSalaryDetails(Convert.ToInt32(b.recordId));
                        if (!deleteDesponse.Success)//it maybe another check
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, deleteDesponse.Summary).Show();
                            return;
                        }
                        entitlements = JsonConvert.DeserializeObject<List<SalaryDetail>>(ents);
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        //CustomResolver resolver = new CustomResolver();
                        //resolver.AddRule("deductionId", "edId");
                        //settings.ContractResolver = resolver;
                        deductions = JsonConvert.DeserializeObject<List<SalaryDetail>>(deds);
                        PostResponse<SalaryDetail[]> result = AddSalaryEntitlementsDeductions(b.recordId, entitlements, deductions);


                        if (!result.Success)
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", result.ErrorCode) != null ? GetGlobalResourceObject("Errors", result.ErrorCode).ToString() : result.Summary).Show();
                            return;
                        }

                        else
                        {


                            ModelProxy record = this.SAStore.GetById(index);
                            //EditSAForm.UpdateRecord(record);
                            record.Set("currencyRef", b.currencyRef);
                            //record.Set("scrName", b.scrName);
                            record.Set("effectiveDate", b.effectiveDate);
                            record.Set("basicAmount", b.basicAmount);
                            record.Set("finalAmount", b.finalAmount);
                            record.Commit();
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordUpdatedSucc
                            });
                            this.EditSAWindow.Close();


                        }

                    }
                    catch (Exception ex)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                    }
                }
            } catch (Exception ex)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                    }
        }

        protected void SaveEN(object sender, DirectEventArgs e)
        {
            string id = e.ExtraParams["id"];
            string FixedAmount = e.ExtraParams["FixedAmount"];
            string obj = e.ExtraParams["values"];
            string oldAmount = e.ExtraParams["oldAmount"];
            double amount = 0;
            SalaryDetail b = JsonConvert.DeserializeObject<SalaryDetail>(obj);
            b.fixedAmount = Convert.ToDouble(FixedAmount);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.seqNo = Convert.ToInt16(ENSeq.Text);
            if (!b.includeInTotal.HasValue)
                b.includeInTotal = false;
            if (!b.isTaxable.HasValue)
                b.isTaxable = false;
            if (entEdId.SelectedItem != null)
                b.edName = entEdId.SelectedItem.Text;

            // Define the object to add or edit as null
            if (b.pct == 0 || !enIsPct.Checked)
            {
                b.pct = (b.fixedAmount / Convert.ToDouble(BasicSalary.Text)) * 100;
            }
            if (!enIsPct.Checked)
            {
                b.pct = 0;
                amount = b.fixedAmount;
            }
            else if (b.fixedAmount == 0 || enIsPct.Checked)
            {
                b.fixedAmount = (b.pct / 100) * Convert.ToDouble(BasicSalary.Text);
            }
            if (enIsPct.Checked)
            {
                amount = b.fixedAmount;
                //b.fixedAmount = 0;

            }
            if (string.IsNullOrEmpty(id))
            {

                try
                {

                    short curSeq = Convert.ToInt16(ENSeq.Text);
                    b.seqNo = curSeq++;
                    ENSeq.Text = curSeq.ToString();
                    if (!b.includeInTotal.HasValue)
                        b.includeInTotal = false;
                    //Add this record to the store 
                    this.entitlementsStore.Insert(0, b);

                    //Display successful notification
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });

                    this.EditENWindow.Close();
                    if (b.includeInTotal.Value)
                    {
                        X.Call("ChangeFinalAmount", amount);
                        X.Call("ChangeEntitlementsAmount", amount);
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
                double oldAmountDouble = Convert.ToDouble(oldAmount);
                bool oldInclude = Convert.ToBoolean(e.ExtraParams["oldInclude"]);
                try
                {
                    ModelProxy record = this.entitlementsStore.GetById(id);

                    record.Set("edName", b.edName);
                    ENForm.UpdateRecord(record);
                    record.Set("fixedAmount", b.fixedAmount);
                    record.Set("pct", b.pct);
                    record.Commit();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordUpdatedSucc
                    });
                    this.EditENWindow.Close();
                    RefreshFinalForEntitlement(oldAmountDouble, oldInclude, b.includeInTotal.Value, amount);

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }

        private static void RefreshFinalForEntitlement(double oldAmountDouble, bool oldInclude, bool include, double amount)
        {
            switch (include)
            {
                case false:
                    if (oldInclude)
                    {
                        X.Call("ChangeFinalAmount", -oldAmountDouble);
                        X.Call("ChangeEntitlementsAmount", -oldAmountDouble);
                    }
                    break;
                case true:
                    if (!oldInclude)
                    {
                        X.Call("ChangeFinalAmount", amount);
                        X.Call("ChangeEntitlementsAmount", amount);
                    }
                    else
                    {
                        double newChange = amount - oldAmountDouble;
                        X.Call("ChangeFinalAmount", newChange);
                        X.Call("ChangeEntitlementsAmount", newChange);
                    }
                    break;
                default:
                    break;
            }
        }

        private static void RefreshFinalForDeduction(double oldAmountDouble, bool oldInclude, bool include, double amount)
        {
            switch (include)
            {
                case false:
                    if (oldInclude)
                    {
                        X.Call("ChangeFinalAmount", oldAmountDouble);
                        X.Call("ChangeDeductionsAmount", oldAmountDouble);
                    }

                    break;
                case true:
                    if (!oldInclude)
                    {
                        X.Call("ChangeFinalAmount", -amount);
                        X.Call("ChangeDeductionsAmount", -amount);
                    }
                    else
                    {
                        double newChange = oldAmountDouble - amount;
                        X.Call("ChangeFinalAmount", newChange);
                        X.Call("ChangeDeductionsAmount", newChange);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void SaveDE(object sender, DirectEventArgs e)
        {
            
            string id = e.ExtraParams["id"];
            string FixedAmount = e.ExtraParams["FixedAmount"];
            string obj = e.ExtraParams["values"];
            string oldAmount = e.ExtraParams["oldAmount"];

            JsonSerializerSettings settings = new JsonSerializerSettings();
            CustomResolver res = new CustomResolver();
            res.AddRule("DEedId", "edId");
            settings.ContractResolver = res;
            SalaryDetail b = JsonConvert.DeserializeObject<SalaryDetail>(obj, settings);
            b.fixedAmount = Convert.ToDouble(FixedAmount);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            double amount = 0;
            b.seqNo = Convert.ToInt16(DESeq.Text);
            if (!b.includeInTotal.HasValue)
                b.includeInTotal = false;
            if (!b.isTaxable.HasValue)
                b.isTaxable = false;
            if (dedEdId.SelectedItem != null)
                b.edName = dedEdId.SelectedItem.Text;

            // Define the object to add or edit as null
            if (b.pct == 0 || !deIsPCT.Checked)
            {
                b.pct = (b.fixedAmount / Convert.ToDouble(BasicSalary.Text)) * 100;
            }
            if (!deIsPCT.Checked)
            {
                b.pct = 0;
                amount = b.fixedAmount;
            }
            else if (b.fixedAmount == 0 || deIsPCT.Checked)
            {
                if (b.pctOf == 1)
                    b.fixedAmount = (b.pct / 100) * Convert.ToDouble(BasicSalary.Text);
                else
                    b.fixedAmount = (b.pct / 100) * (Convert.ToDouble(BasicSalary.Text) + Convert.ToDouble(eAmount.Text));
            }
            if (deIsPCT.Checked)
            {
                amount = b.fixedAmount;
                //b.fixedAmount = 0;

            }
            if (string.IsNullOrEmpty(id))
            {

                try
                {

                    short curSeq = Convert.ToInt16(DESeq.Text);
                    b.seqNo = curSeq++;
                    DESeq.Text = curSeq.ToString();

                    //Add this record to the store 
                    this.deductionStore.Insert(0, b);

                    //Display successful notification
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });

                    this.EditDEWindow.Close();
                    if (b.includeInTotal.Value)
                    {
                        X.Call("ChangeFinalAmount", amount);
                        X.Call("ChangeDeductionsAmount", amount);
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
                    double oldAmountDouble = Convert.ToDouble(oldAmount);
                    bool oldInclude = Convert.ToBoolean(e.ExtraParams["oldInclude"]);
                    ModelProxy record = this.deductionStore.GetById(id);


                    DEForm.UpdateRecord(record);
                    record.Set("edName", b.edName);
                    record.Set("edId", b.edId);
                    record.Set("fixedAmount", b.fixedAmount);
                    record.Set("pct", b.pct);
                    record.Commit();
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordUpdatedSucc
                    });
                    this.EditDEWindow.Close();
                    RefreshFinalForDeduction(oldAmountDouble, oldInclude, b.includeInTotal.Value, amount);
                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }

        protected void SaveBO(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            Bonus b = JsonConvert.DeserializeObject<Bonus>(obj);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            b.date = new DateTime(b.date.Year, b.date.Month, b.date.Day, 14, 0, 0);
            if (CurrencyCombo.SelectedItem != null)
                b.currencyRef = CurrencyCombo.SelectedItem.Text;
            if (btId.SelectedItem != null)
                b.btName = btId.SelectedItem.Text;

            // Define the object to add or edit as null


            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Bonus> request = new PostRequest<Bonus>();
                    request.entity = b;
                    PostResponse<Bonus> r = _employeeService.ChildAddOrUpdate<Bonus>(request);
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
                        this.BOStore.Reload();
                        //Add this record to the store 
                        //this.BOStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditBOWindow.Close();
                        RowSelectionModel sm = this.BonusGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<Bonus> request = new PostRequest<Bonus>();
                    request.entity = b;
                    PostResponse<Bonus> r = _employeeService.ChildAddOrUpdate<Bonus>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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


                        ModelProxy record = this.BOStore.GetById(index);
                        BOForm.UpdateRecord(record);
                        record.Set("currencyRef", b.currencyRef);
                        record.Set("btName", b.btName);
                        record.Set("date", b.date.ToShortDateString());
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditBOWindow.Close();


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


        private void FillCurrency()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Currency> resp = _systemService.ChildGetAll<Currency>(branchesRequest);
            if (!resp.Success)
               Common.errorMessage(resp);
            currencyStore.DataSource = resp.Items;
            currencyStore.DataBind();
        }
        private void FillBank()
        {
            ListRequest bank = new ListRequest();
            ListResponse<Bank> resp = _payrollService.ChildGetAll<Bank>(bank);
            if (!resp.Success)
               Common.errorMessage(resp);
            bankStore.DataSource = resp.Items;
            bankStore.DataBind();
        }

        private void FillCurrencyBO()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Currency> resp = _systemService.ChildGetAll<Currency>(branchesRequest);
            if (!resp.Success)
               Common.errorMessage(resp);
            BOCurrencyStore.DataSource = resp.Items;
            BOCurrencyStore.DataBind();
        }
        private void FillScr()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<SalaryChangeReason> resp = _employeeService.ChildGetAll<SalaryChangeReason>(branchesRequest);
            if (!resp.Success)
               Common.errorMessage(resp);
            scrStore.DataSource = resp.Items;
            scrStore.DataBind();
        }

        private void FillBT()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<BonusType> resp = _employeeService.ChildGetAll<BonusType>(branchesRequest);
            if (!resp.Success)
               Common.errorMessage(resp);
            BTStore.DataSource = resp.Items;
            BTStore.DataBind();
        }


        private void FillEntitlements()
        {
            SalaryDetailsListRequest req = new SalaryDetailsListRequest();
            req.SalaryID = Convert.ToInt32(CurrentSalary.Text);
            req.Type = 1;
            ListResponse<SalaryDetail> details = _employeeService.ChildGetAll<SalaryDetail>(req);
            if (!details.Success)
            {
                X.Msg.Alert(Resources.Common.Error, details.Summary).Show();

                return;
            }
            entitlementsStore.DataSource = details.Items;
            entitlementsStore.DataBind();
            ENSeq.Text = (details.count + 1).ToString();


        }
        private void FillDeductions()
        {
            SalaryDetailsListRequest req = new SalaryDetailsListRequest();
            req.SalaryID = Convert.ToInt32(CurrentSalary.Text);
            req.Type = 2;
            ListResponse<SalaryDetail> details = _employeeService.ChildGetAll<SalaryDetail>(req);
            if (!details.Success)
            {
                X.Msg.Alert(Resources.Common.Error, details.Summary).Show();

                return;
            }
            deductionStore.DataSource = details.Items;
            deductionStore.DataBind();
            DESeq.Text = (details.count + 1).ToString();


        }
        #region Combo Dynamic Add
        protected void addBOCurrency(object sender, DirectEventArgs e)
        {
            Currency obj = new Currency();
            obj.name = CurrencyCombo.Text;
            obj.reference = CurrencyCombo.Text;

            PostRequest<Currency> req = new PostRequest<Currency>();
            req.entity = obj;
            PostResponse<Currency> response = _systemService.ChildAddOrUpdate<Currency>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillCurrencyBO();
                CurrencyCombo.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        protected void addSACurrency(object sender, DirectEventArgs e)
        {
            Currency obj = new Currency();
            obj.name = currencyId.Text;
            obj.reference = currencyId.Text;
            PostRequest<Currency> req = new PostRequest<Currency>();
            req.entity = obj;
            PostResponse<Currency> response = _systemService.ChildAddOrUpdate<Currency>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillCurrency();

                currencyId.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }


        }

        protected void addBT(object sender, DirectEventArgs e)
        {
            BonusType obj = new BonusType();
            obj.name = btId.Text;

            PostRequest<BonusType> req = new PostRequest<BonusType>();
            req.entity = obj;
            PostResponse<BonusType> response = _employeeService.ChildAddOrUpdate<BonusType>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillBT();
                btId.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }


        }

        protected void addSCR(object sender, DirectEventArgs e)
        {
            SalaryChangeReason obj = new SalaryChangeReason();
            obj.name = scrId.Text;

            PostRequest<SalaryChangeReason> req = new PostRequest<SalaryChangeReason>();
            req.entity = obj;
            PostResponse<SalaryChangeReason> response = _employeeService.ChildAddOrUpdate<SalaryChangeReason>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillScr();
                scrId.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }


        }

        #endregion

        private PostResponse<SalaryDetail[]> AddSalaryEntitlementsDeductions(string salaryIdString, List<SalaryDetail> entitlements, List<SalaryDetail> deductions)
        {
            short i = 1;
            int salaryId = Convert.ToInt32(salaryIdString);

            foreach (var detail in entitlements)
            {

                detail.seqNo = i++;
                detail.salaryId = salaryId;
                if (!detail.includeInTotal.HasValue)
                    detail.includeInTotal = false;


            }
            foreach (var detail in deductions)
            {

                detail.seqNo = i++;
                detail.salaryId = salaryId;
                if (!detail.includeInTotal.HasValue)
                    detail.includeInTotal = false;


            }
            entitlements.AddRange(deductions);
            PostResponse<SalaryDetail[]> response = new PostResponse<SalaryDetail[]>();
            if (entitlements.ToArray().Length != 0)

            {

                PostRequest<SalaryDetail[]> periodRequest = new PostRequest<SalaryDetail[]>();

                periodRequest.entity = entitlements.ToArray();
                response = _employeeService.ChildAddOrUpdate<SalaryDetail[]>(periodRequest);
            }
            else
                response.Success = true;
            return response;
        }
        //private PostResponse<SalaryDetail[]> AddSalaryEntitlements(string salaryIdString, List<SalaryDetail> details)
        //{
        //    short i = 1;
        //    int salaryId = Convert.ToInt32(salaryIdString);
        //    foreach (var detail in details)
        //    {

        //        detail.seqNo = i++;
        //        detail.salaryId = salaryId;
        //        if (!detail.includeInTotal.HasValue)
        //            detail.includeInTotal = false;
        //        detail.type = 1;

        //    }
        //    PostRequest<SalaryDetail[]> periodRequest = new PostRequest<SalaryDetail[]>();
        //    periodRequest.entity = details.ToArray();
        //    PostResponse<SalaryDetail[]> response = _employeeService.ChildAddOrUpdate<SalaryDetail[]>(periodRequest);
        //    return response;
        //}
        //private PostResponse<SalaryDetail[]> AddSalaryDeductions(string salaryIdString, List<SalaryDetail> details)
        //{
        //    short i = 1;
        //    int salaryId = Convert.ToInt32(salaryIdString);
        //    foreach (var detail in details)
        //    {

        //        detail.seqNo = i++;
        //        detail.salaryId = salaryId;
        //        if (!detail.includeInTotal.HasValue)
        //            detail.includeInTotal = false;

        //        detail.type = 2;

        //    }
        //    PostRequest<SalaryDetail[]> periodRequest = new PostRequest<SalaryDetail[]>();
        //    periodRequest.entity = details.ToArray();
        //    PostResponse<SalaryDetail[]> response = _employeeService.ChildAddOrUpdate<SalaryDetail[]>(periodRequest);
        //    return response;
        //}

        protected void ensStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
            entsStore.DataSource = eds.Items.Where(s => s.type == 1).ToList();
            entsStore.DataBind();

        }
        protected void dedsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
            dedsStore.DataSource = eds.Items.Where(s => s.type == 2).ToList();
            dedsStore.DataBind();

        }


        protected void addEnt(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(entEdId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = entEdId.Text;
            dept.type = 1;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                entsStore.Reload();
                entEdId.Value = response.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        protected void addDed(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(dedEdId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = dedEdId.Text;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                dedsStore.Reload();
                dedEdId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        private void  fillSalaryType()
        {
            XMLDictionaryListRequest request = new XMLDictionaryListRequest();

            request.database = "2";
            ListResponse<XMLDictionary> resp = _systemService.ChildGetAll<XMLDictionary>(request);
            if(!resp.Success)
            {
                Common.errorMessage(resp);
                return; 
            }
            salaryTypeStore.DataSource = resp.Items;
            salaryTypeStore.DataBind();
        }

    }
}