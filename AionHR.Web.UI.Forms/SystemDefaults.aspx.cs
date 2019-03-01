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
using AionHR.Model.NationalQuota;
using AionHR.Model.Benefits;
using AionHR.Services.Messaging.System;
using AionHR.Infrastructure.Domain;

namespace AionHR.Web.UI.Forms
{
    public partial class SystemDefaults : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        INationalQuotaService _nationalQuotaService = ServiceLocator.Current.GetInstance<INationalQuotaService>();
        IBenefitsService _benefitsService = ServiceLocator.Current.GetInstance<IBenefitsService>();




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
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SystemDefault), EmployeeSettings, null, null, SaveButton);
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SystemDefault), GeneralSettings, null, null, SaveButton);
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SystemDefault), PayrollSettings, null, null, SaveButton);
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SystemDefault), AttendanceSettings, null, null, SaveButton);
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SystemDefault), SecuritySettings, null, null, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                ListRequest req = new ListRequest();
                ListResponse<KeyValuePair<string, string>> defaults = _systemService.ChildGetAll<KeyValuePair<string, string>>(req);
                if (!defaults.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, defaults.Summary).Show();
                    return;
                }
                FillCombos();
                FillDefaults(defaults.Items);
            }

        }

        private void FillCombos()
        {
            FillNationality();
            FillCurrency();
            FillCalendars();
            FillVS();
            FillIDs();
            FillPassports();
            FillSchedules();
            //FillTsId();
            FillSsid();
            FillIndustry();
            FillBsid();
            FillbasicSalaryPayCodeStore();
            fillFSWDEDId();


          absenceStore.DataSource= GetDeductions();
            absenceStore.DataBind();
            disappearanceStore.DataSource = GetDeductions();
            disappearanceStore.DataBind();
            missedPunchesStore.DataSource = GetDeductions();
            missedPunchesStore.DataBind();
            overtimeStore.DataSource = GetEntitlements();
            overtimeStore.DataBind();
            latenessStore.DataSource = GetDeductions();
            latenessStore.DataBind();

           // ssDeductionStore.DataSource = GetDeductions();
            //ssDeductionStore.DataBind();

            //peDeductionStore.DataSource = GetDeductions();
            //peDeductionStore.DataBind();

            loanDeductionStore.DataSource = GetDeductions();
            loanDeductionStore.DataBind();
            PYFSLeaveBalEDId_Store.DataSource = GetEntitlements();
            PYFSLeaveBalEDId_Store.DataBind();
            PYINEDId_store.DataSource= GetEntitlements();
            PYINEDId_store.DataBind();
            PYISmale_store.DataSource = GetIndemnitySchedules();
            PYISmale_store.DataBind();
            PYISfemale_store.DataSource = GetIndemnitySchedules();
            PYISfemale_store.DataBind();
         //   exemptMarriageTR_Store.DataSource = GetTerminationReasons();
          //  exemptMarriageTR_Store.DataBind();
            //exemptDeliveryTR_Store.DataSource = GetTerminationReasons();
            //exemptDeliveryTR_Store.DataBind();

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

        private void FillIDs()
        {
            idStore.DataSource = GetRTW();
            idStore.DataBind();
        }
        private List<DocumentType> GetRTW()
        {
            ListRequest RWDocumentType = new ListRequest();
            ListResponse<DocumentType> resp = _employeeService.ChildGetAll<DocumentType>(RWDocumentType);
            if (!resp.Success)
            {
               X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId")+ resp.LogId : resp.Summary).Show();
                return new List<DocumentType>();
            }
            return resp.Items;
           
        }
        private void FillPassports()
        {
            passportStore.DataSource = GetRTW();
            passportStore.DataBind();
        }
        [DirectMethod]
        public void FillCompanyLogo()
        {
            try
            {
                AttachementRecordRequest req = new AttachementRecordRequest();
                req.RecordID = "1";
                req.seqNo = "0";
                req.classId = ClassId.SYDE.ToString();

                RecordResponse<Attachement> resp = _systemService.ChildGetRecord<Attachement>(req);

                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(resp);
                    imgControl.ImageUrl = "Images/empPhoto.jpg";
                    CurrentEmployeePhotoName.Text = "Images/empPhoto.jpg";
                    return;
                }
                if (resp.result != null)
                {
                    imgControl.ImageUrl = resp.result.url;
                    CurrentEmployeePhotoName.Text = resp.result.url;
                }
              if (string.IsNullOrEmpty(imgControl.ImageUrl))
                {
                    imgControl.ImageUrl = "Images/empPhoto.jpg";
                    CurrentEmployeePhotoName.Text = "Images/empPhoto.jpg";
                  
                }
            }

            catch (Exception exp )
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                
            }

        }
        private void FillDefaults(List<KeyValuePair<string, string>> items)
        {
            try
            {
                py_aEDId.Select(items.Where(s => s.Key == "py_aEDId").First().Value);
            }
            catch { }
            try
            {
                py_dEDId.Select(items.Where(s => s.Key == "py_dEDId").First().Value);
            }
            catch { }
            try
            {
                py_lEDId.Select(items.Where(s => s.Key == "py_lEDId").First().Value);
            }
            catch { }
            try
            {
                py_oEDId.Select(items.Where(s => s.Key == "py_oEDId").First().Value);
            }
            catch { }
            try
            {
                py_mEDId.Select(items.Where(s => s.Key == "py_mEDId").First().Value);
            }
            catch { }
            try
            {
               ssId.Select(items.Where(s => s.Key == "ssId").First().Value);
            }
            catch { }
            //try
            //{
            //    //ssDeductionId.Select(items.Where(s => s.Key == "ssDeductionId").First().Value);
            //}
            //catch { }
            try
            {
                loanDeductionId.Select(items.Where(s => s.Key == "loanDeductionId").First().Value);
            }
            catch { }
            //try { peDeductionId.Select(items.Where(s => s.Key == "penaltyDeductionId").First().Value); }
            //catch { }
          
            try
            {
                ldMethod.Select(items.Where(s => s.Key == "ldMethod").First().Value);
            }

            catch { }
          


            try
            {
                ldValue.Text = (items.Where(s => s.Key == "ldValue").First().Value);
            }

            catch { }
            


            try
            {
                currencyIdCombo.Select(items.Where(s => s.Key == "currencyId").First().Value);
            }
            catch { }
            try
            {
                countryIdCombo.Select(items.Where(s => s.Key == "countryId").First().Value);
            }
            catch { }
            try
            {
                dateFormatCombo.Select(items.Where(s => s.Key == "dateFormat").First().Value);
            }
            catch { }
            try { nameFormatCombo.Select(items.Where(s => s.Key == "nameFormat").First().Value.ToString()); }
            catch { }
            try
            {
                timeZoneCombo.Select(items.Where(s => s.Key == "timeZone").First().Value);
            }

            catch { }
            try { fdowCombo.Select(items.Where(s => s.Key == "fdow").First().Value); }

            catch { }
            try { caId.Select(items.Where(s => s.Key == "caId").First().Value); }

            catch { }
            try { idCombo.Select(items.Where(s => s.Key == "resDocTypeId").First().Value); }

            catch { }
            try { passportCombo.Select(items.Where(s => s.Key == "passportDocTypeId").First().Value); }

            catch { }
            try { scId.Select(items.Where(s => s.Key == "scId").First().Value); }

            catch { }
            try { vsId.Select(items.Where(s => s.Key == "vsId").First().Value); }

            catch { }
            //try { tsId.Select(items.Where(s => s.Key == "tsId").First().Value); }

            //catch { }
            try

            {
                enableCameraCheck.Checked = items.Where(s => s.Key == "enableCamera").First().Value == "true";
            }
            catch { }
            try

            {
                apply_ALDA_CSBR.Checked = items.Where(s => s.Key == "apply-ALDA-CSBR").First().Value == "true";
            }
            catch { }
            try

            {
                apply_ALDA_CSDE.Checked = items.Where(s => s.Key == "apply-ALDA-CSDE").First().Value == "true";
            }
            catch { }
            try

            {
                apply_ALDA_CSDI.Checked = items.Where(s => s.Key == "apply-ALDA-CSDI").First().Value == "true";
            }
            catch { }
            try

            {
                enableHijri.Checked = items.Where(s => s.Key == "enableHijri").First().Value == "true";
            }
            catch { }
            try

            {
                localServerIP.Text = items.Where(s => s.Key == "localServerIP").First().Value.Split('/')[0];
            }
            catch { }

            //try

            //{
            //    lastGeneratedTADayId.Text = DateTime.ParseExact(items.Where(s => s.Key == "lastGeneratedTADayId").First().Value, "yyyyMMdd", new CultureInfo("en")).ToString(_systemService.SessionHelper.GetDateformat()); ;
            //}
            //catch { }
            try { PYFSLeaveBalEDId.Select(items.Where(s => s.Key == "PYFSLeaveBalEDId").First().Value); }

            catch { }
            try { PYINEDId.Select(items.Where(s => s.Key == "PYINEDId").First().Value); }

            catch { }

            try { PYISmale.Select(items.Where(s => s.Key == "PYISmale").First().Value); }

            catch { }
            try { PYISfemale.Select(items.Where(s => s.Key == "PYISfemale").First().Value); }

            catch { }


            //try { exemptMarriageTRId.Select(items.Where(s => s.Key == "exemptMarriageTRId").First().Value); }

            //catch { }
            //try { exemptDeliveryTRId.Select(items.Where(s => s.Key == "exemptDeliveryTRId").First().Value); }

            //catch { }
            try { languageId.Select(items.Where(s => s.Key == "languageId").First().Value.ToString()); }
        
            catch { }
            try { sourceTASC.Select(items.Where(s => s.Key == "sourceTASC").First().Value.ToString());
                   if (string.IsNullOrEmpty(items.Where(s => s.Key == "sourceTASC").First().Value.ToString()))
                    {
                    sourceTASC.Select(2);
                }
                 }

            catch { }
            try
            {
                sourceTACA.Select(items.Where(s => s.Key == "sourceTACA").First().Value.ToString());
                if (string.IsNullOrEmpty(items.Where(s => s.Key == "sourceTACA").First().Value.ToString()))
                {
                    sourceTACA.Select(1);
                }
            }

            catch { }
            try { NQINid.Select(items.Where(s => s.Key == "NQINid").First().Value.ToString()); }

            catch { }
            try
            {
                yearDays.Text = (items.Where(s => s.Key == "yearDays").First().Value);
                if (string.IsNullOrEmpty(items.Where(s => s.Key == "yearDays").First().Value.ToString()))
                {
                    yearDays.Text = "365";
                }
            }

            catch { }
            try
            {
                retirementAge.Text = (items.Where(s => s.Key == "retirementAge").First().Value);
                if (string.IsNullOrEmpty(items.Where(s => s.Key == "retirementAge").First().Value.ToString()))
                {
                   retirementAge.Text = "60";
                }
            }

            catch { }
            try
            { 
                if (string.IsNullOrEmpty(items.Where(s => s.Key == "minPunchInterval").First().Value.ToString()))
                minPunchInterval.Text = "60";
                else
                minPunchInterval.Text = (items.Where(s => s.Key == "minPunchInterval").First().Value);
              
            }

            catch { }
            try
            {
                employeeRefSize.Text = (items.Where(s => s.Key == "employeeRefSize").First().Value);

            }

            catch { }

            try
            {
                monthWorkDays.Text = (items.Where(s => s.Key == "monthWorkDays ").First().Value);
                if (string.IsNullOrEmpty(items.Where(s => s.Key == "monthWorkDays ").First().Value.ToString()))
                {
                    monthWorkDays.Text = "30";
                }
            }

            catch { }
            try
            {
                bsId.Select(items.Where(s => s.Key == "bsId").First().Value.ToString());
            }

            catch { }
            try
            {
                basicSalaryPayCode.Select(items.Where(s => s.Key == "basicSalaryPayCode").First().Value.ToString());
            }

            catch { }
            try
            {
                FSWDEDId.Select(items.Where(s => s.Key == "FSWDEDId").First().Value);
            }
            catch { }

            try
            {
                if (string.IsNullOrEmpty(items.Where(s => s.Key == "MaxLoanDeduction").First().Value.ToString()))
                    MaxLoanDeduction.Text = "30";
                MaxLoanDeduction.Text = (items.Where(s => s.Key == "MaxLoanDeduction").First().Value);

            }

            catch { }



            try
            {
                pyalDays.Text = (items.Where(s => s.Key == "pyalDays").First().Value);
            }

            catch { }
            try
            {
                pyaaDays.Text = (items.Where(s => s.Key == "pyaaDays").First().Value);
            }

            catch { }
            try
            {
                pylvDays.Text = (items.Where(s => s.Key == "pylvDays").First().Value);
            }

            catch { }

            try { dailySchedule.Select(items.Where(s => s.Key == "dailySchedule").First().Value); }

            catch { }
            try
            {
                punchSource.Select(items.Where(s => s.Key == "punchSource").First().Value);
            }
            catch { }
            try

            {
                backofficeEmail.Text = items.Where(s => s.Key == "backofficeEmail").First().Value;
            }
            catch { }



            try { storeType.Select(items.Where(s => s.Key == "storeType").First().Value); }

            catch { }
            try

            {
                storeConnection.Text = items.Where(s => s.Key == "storeConnection").First().Value;
            }
            catch { }
            try

            {
                pull.Checked = items.Where(s => s.Key == "pull").First().Value == "true";
            }
            catch { }
            try

            {
                push.Checked = items.Where(s => s.Key == "push").First().Value == "true";
            }
            catch { }
            try

            {
                clearOnSuccess.Checked = items.Where(s => s.Key == "clearOnSuccess").First().Value == "true";
            }
            catch { }

            try

            {
                sleepTime.Text = items.Where(s => s.Key == "sleepTime").First().Value;
            }
            catch { }
            try

            {
                serialNo.Text = items.Where(s => s.Key == "serialNo").First().Value;
            }
            catch { }
            try

            {
                clearOnSuccess.Checked = items.Where(s => s.Key == "debugMode").First().Value == "true";
            }
            catch { }
            try

            {
                shiftData.Checked = items.Where(s => s.Key == "shiftData").First().Value == "true";
            }
            catch { }
            try

            {
                pendingPunchesFolder.Text = items.Where(s => s.Key == "pendingPunchesFolder").First().Value;
            }
            catch { }
            try

            {
                punchInterface.Text = items.Where(s => s.Key == "punchInterface").First().Value;
            }
            catch { }
            


            FillCompanyLogo(); 


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

        private List<KeyValuePair<string,string>> GetEmployeeSettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(values.nameFormat.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("nameFormat", values.nameFormat.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("nameFormat", ""));

            if (!string.IsNullOrEmpty(values.vsId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("vsId", values.vsId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("vsId", ""));
            if (!string.IsNullOrEmpty(values.passportCombo.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("passportDocTypeId", values.passportCombo.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("passportDocTypeId",""));
            if (!string.IsNullOrEmpty(values.idCombo.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("resDocTypeId", values.idCombo.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("resDocTypeId", ""));
            if (values.retirementAge != null && !string.IsNullOrEmpty(values.retirementAge.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("retirementAge", values.retirementAge.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("retirementAge", ""));
            if (values.employeeRefSize!= null && !string.IsNullOrEmpty(values.employeeRefSize.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("employeeRefSize", values.employeeRefSize.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("employeeRefSize", ""));






            return submittedValues;
        }
        private void SaveEmployeeSettings(dynamic values)
        {
            List<KeyValuePair<string,string>> submittedValues = GetEmployeeSettings(values);
            KeyValuePair<string, string>[] valArr = submittedValues.ToArray();
            PostRequest<KeyValuePair<string, string>[]> req = new PostRequest<KeyValuePair<string, string>[]>();
            req.entity = valArr;
            PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            else
            {
                FillDefaults(submittedValues);

                if (!string.IsNullOrEmpty(values.nameFormat.ToString()))
                    _systemService.SessionHelper.SetNameFormat(values.nameFormat.ToString());


                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
            }
        }
        private List<KeyValuePair<string, string>> GetGeneralSettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(values.countryId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("countryId", values.countryId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("countryId", ""));
            if (!string.IsNullOrEmpty(values.currencyId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("currencyId", values.currencyId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("currencyId", ""));
            if (!string.IsNullOrEmpty(values.dateFormat.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("dateFormat", values.dateFormat.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("dateFormat", ""));
            if (!string.IsNullOrEmpty(values.timeZone.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("timeZone", values.timeZone.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("timeZone", ""));
            if (!string.IsNullOrEmpty(values.languageId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("languageId", values.languageId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("languageId", ""));
            if (!string.IsNullOrEmpty(values.NQINid.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("NQINid", values.NQINid.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("NQINid",""));
            if (!string.IsNullOrEmpty(values.backofficeEmail.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("backofficeEmail", values.backofficeEmail.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("backofficeEmail", ""));

            submittedValues.Add(new KeyValuePair<string, string>("enableHijri", values.enableHijri == null ? "false" : "true"));
            return submittedValues;
        }
        private void SaveGeneralSettings(dynamic values )
        {
            List<KeyValuePair<string, string>> submittedValues = GetGeneralSettings(values);
            KeyValuePair<string, string>[] valArr = submittedValues.ToArray();
            PostRequest<KeyValuePair<string, string>[]> req = new PostRequest<KeyValuePair<string, string>[]>();
            req.entity = valArr;
            PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            else
            {
                FillDefaults(submittedValues);
                if (!string.IsNullOrEmpty(values.dateFormat.ToString()))
                    _systemService.SessionHelper.SetDateformat(values.dateFormat.ToString());

                if (!string.IsNullOrEmpty(values.countryId.ToString()))
                    _systemService.SessionHelper.SetDefaultCountry(values.countryId.ToString());
                
                if (!string.IsNullOrEmpty(values.timeZone.ToString()))
                    _systemService.SessionHelper.SetDefaultTimeZone(Convert.ToInt32(values.timeZone.ToString()));
                _systemService.SessionHelper.SetHijriSupport(values.enableHijri == null ? false : true);

                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
            }
        }
        private List<KeyValuePair<string, string>> GetAttendanceSettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(values.fdow.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("fdow", values.fdow.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("fdow",""));
            if (!string.IsNullOrEmpty(values.caId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("caId", values.caId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("caId",""));
           

            if (!string.IsNullOrEmpty(values.scId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("scId", values.scId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("scId", " "));
            if (values.localServerIP != null && !string.IsNullOrEmpty(values.localServerIP.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("localServerIP", values.localServerIP.ToString() + "/AionWSLocal"));
            else
                submittedValues.Add(new KeyValuePair<string, string>("localServerIP", ""));
            if (!string.IsNullOrEmpty(values.sourceTASC.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("sourceTASC", values.sourceTASC.ToString()));
           else
                submittedValues.Add(new KeyValuePair<string, string>("sourceTASC", ""));
            if (!string.IsNullOrEmpty(values.sourceTACA.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("sourceTACA", values.sourceTACA.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("sourceTACA", ""));

            if (!string.IsNullOrEmpty(values.minPunchInterval.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("minPunchInterval", values.minPunchInterval.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("minPunchInterval", "60"));

            if (!string.IsNullOrEmpty(values.dailySchedule.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("dailySchedule", values.dailySchedule.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("dailySchedule", GetGlobalResourceObject("ComboBoxValues", "dailySchedule_15").ToString()));

            submittedValues.Add(new KeyValuePair<string, string>("enableCamera", values.enableCamera == null ? "false" : "true"));

            if (!string.IsNullOrEmpty(values.punchSource.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("punchSource", values.punchSource.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("punchSource", values.punchSource.ToString()));

            return submittedValues;
        }

        private void SaveAttendanceSettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = GetAttendanceSettings(values);
            KeyValuePair <string, string>[] valArr = submittedValues.ToArray();
            PostRequest<KeyValuePair<string, string>[]> req = new PostRequest<KeyValuePair<string, string>[]>();
            req.entity = valArr;
            PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            else
            {
                FillDefaults(submittedValues);


                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
            }
        }

        private List<KeyValuePair<string, string>> GetPayrollSettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = new List<KeyValuePair<string, string>>();
            if (values.ulDeductionId != null && !string.IsNullOrEmpty(values.ulDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ulDeductionId", values.ulDeductionId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("ulDeductionId",""));
            if (values.ssDeductionId != null && !string.IsNullOrEmpty(values.ssDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ssDeductionId", values.ssDeductionId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("ssDeductionId",""));
            if (values.loanDeductionId != null && !string.IsNullOrEmpty(values.loanDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("loanDeductionId", values.loanDeductionId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("loanDeductionId", ""));
            if (values.peDeductionId != null && !string.IsNullOrEmpty(values.peDeductionId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("penaltyDeductionId", values.peDeductionId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("penaltyDeductionId",""));
            if (values.ssId != null && !string.IsNullOrEmpty(values.ssId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ssId", values.ssId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("ssId", ""));
            if (values.ldMethod != null && !string.IsNullOrEmpty(values.ldMethod.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ldMethod", values.ldMethod.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("ldMethod", ""));
            if (values.ldValue != null && !string.IsNullOrEmpty(values.ldValue.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("ldValue", values.ldValue.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("ldValue", "0"));
            if (values.py_aEDId != null && !string.IsNullOrEmpty(values.py_aEDId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("py_aEDId", values.py_aEDId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("py_aEDId", ""));
            if (values.py_dEDId != null && !string.IsNullOrEmpty(values.py_dEDId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("py_dEDId", values.py_dEDId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("py_dEDId", ""));
            if (values.py_lEDId != null && !string.IsNullOrEmpty(values.py_lEDId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("py_lEDId", values.py_lEDId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("py_lEDId", ""));
            if (values.py_oEDId != null && !string.IsNullOrEmpty(values.py_oEDId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("py_oEDId", values.py_oEDId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("py_oEDId", ""));
            if (values.py_mEDId != null && !string.IsNullOrEmpty(values.py_mEDId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("py_mEDId", values.py_mEDId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("py_mEDId", ""));
            if (values.PYFSLeaveBalEDId != null && !string.IsNullOrEmpty(values.PYFSLeaveBalEDId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("PYFSLeaveBalEDId", values.PYFSLeaveBalEDId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("PYFSLeaveBalEDId", ""));
            if (values.PYINEDId != null && !string.IsNullOrEmpty(values.PYINEDId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("PYINEDId", values.PYINEDId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("PYINEDId", ""));
            if (values.monthWorkDays != null && !string.IsNullOrEmpty(values.monthWorkDays.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("monthWorkDays ", values.monthWorkDays.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("monthWorkDays ", ""));



            if (values.PYISmale != null && !string.IsNullOrEmpty(values.PYISmale.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("PYISmale", values.PYISmale.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("PYISmale", ""));
            if (values.PYISfemale != null && !string.IsNullOrEmpty(values.PYISfemale.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("PYISfemale", values.PYISfemale.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("PYISfemale", ""));
            //if (!string.IsNullOrEmpty(values.exemptMarriageTRId.ToString()))
            //    submittedValues.Add(new KeyValuePair<string, string>("exemptMarriageTRId", values.exemptMarriageTRId.ToString()));
            //else
            //    submittedValues.Add(new KeyValuePair<string, string>("exemptMarriageTRId", ""));
            //if (!string.IsNullOrEmpty(values.exemptDeliveryTRId.ToString()))
            //    submittedValues.Add(new KeyValuePair<string, string>("exemptDeliveryTRId", values.exemptDeliveryTRId.ToString()));
            //else
            //    submittedValues.Add(new KeyValuePair<string, string>("exemptDeliveryTRId",""));
            if (!string.IsNullOrEmpty(values.yearDays.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("yearDays", values.yearDays.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("yearDays", "365"));
            //if (!string.IsNullOrEmpty(values.tsId.ToString()))
            //    submittedValues.Add(new KeyValuePair<string, string>("tsId", values.tsId.ToString()));
            //else
            //    submittedValues.Add(new KeyValuePair<string, string>("tsId", ""));
            if (!string.IsNullOrEmpty(values.bsId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("bsId", values.bsId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("bsId", ""));
            if (!string.IsNullOrEmpty(values.basicSalaryPayCode.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("basicSalaryPayCode", values.basicSalaryPayCode.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("basicSalaryPayCode", ""));

            if (!string.IsNullOrEmpty(values.FSWDEDId.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("FSWDEDId", values.FSWDEDId.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("FSWDEDId", ""));
            if (!string.IsNullOrEmpty(values.MaxLoanDeduction.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("MaxLoanDeduction", values.MaxLoanDeduction.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("MaxLoanDeduction", "30"));



            if (values.pyalDays != null && !string.IsNullOrEmpty(values.pyalDays.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("pyalDays", values.pyalDays.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("pyalDays", "30"));
            if (values.pyaaDays != null && !string.IsNullOrEmpty(values.pyaaDays.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("pyaaDays", values.pyaaDays.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("pyaaDays", "30"));
            if (values.pylvDays != null && !string.IsNullOrEmpty(values.pylvDays.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("pylvDays", values.pylvDays.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("pylvDays", "30"));


            return submittedValues;
        }
        private void SavePayrollSettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = GetPayrollSettings(values);
            KeyValuePair<string, string>[] valArr = submittedValues.ToArray();
            PostRequest<KeyValuePair<string, string>[]> req = new PostRequest<KeyValuePair<string, string>[]>();
            req.entity = valArr;
            PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            else
            {
                FillDefaults(submittedValues);

                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
            }
        }

        private List<KeyValuePair<string, string>> GetSecuritySettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = new List<KeyValuePair<string, string>>();
            submittedValues.Add(new KeyValuePair<string, string>("apply-ALDA-CSBR", values.apply_ALDA_CSBR == null ? "false" : "true"));
            submittedValues.Add(new KeyValuePair<string, string>("apply-ALDA-CSDE", values.apply_ALDA_CSDE == null ? "false" : "true"));
            submittedValues.Add(new KeyValuePair<string, string>("apply-ALDA-CSDI", values.apply_ALDA_CSDI == null ? "false" : "true"));
            return submittedValues;
        }
        private void SaveSecuritySettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = GetSecuritySettings(values);
            KeyValuePair <string, string>[] valArr = submittedValues.ToArray();
            PostRequest<KeyValuePair<string, string>[]> req = new PostRequest<KeyValuePair<string, string>[]>();
            req.entity = valArr;
            PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId")  + resp.LogId : resp.Summary).Show();
                return;
            }
            else
            {
                FillDefaults(submittedValues);



                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
            }
        }


        private List<KeyValuePair<string, string>> GetBiometricSettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(values.storeType.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("storeType", values.storeType.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("storeType", ""));

            if (!string.IsNullOrEmpty(values.storeConnection.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("storeConnection", values.storeConnection.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("storeConnection", ""));

            submittedValues.Add(new KeyValuePair<string, string>("pull", values.pull == null ? "false" : "true"));
            submittedValues.Add(new KeyValuePair<string, string>("push", values.push == null ? "false" : "true"));
            submittedValues.Add(new KeyValuePair<string, string>("clearOnSuccess", values.clearOnSuccess == null ? "false" : "true"));

            submittedValues.Add(new KeyValuePair<string, string>("debugMode", values.debugMode == null ? "false" : "true"));
            submittedValues.Add(new KeyValuePair<string, string>("shiftData", values.shiftData == null ? "false" : "true"));

            if (!string.IsNullOrEmpty(values.sleepTime.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("sleepTime", values.sleepTime.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("sleepTime", ""));
            if (!string.IsNullOrEmpty(values.serialNo.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("serialNo", values.serialNo.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("serialNo", ""));
            if (!string.IsNullOrEmpty(values.pendingPunchesFolder.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("pendingPunchesFolder", values.pendingPunchesFolder.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("pendingPunchesFolder", ""));
            if (!string.IsNullOrEmpty(values.punchInterface.ToString()))
                submittedValues.Add(new KeyValuePair<string, string>("punchInterface", values.punchInterface.ToString()));
            else
                submittedValues.Add(new KeyValuePair<string, string>("punchInterface", ""));

            return submittedValues;
        }
        private void SaveBiometricSettings(dynamic values)
        {
            List<KeyValuePair<string, string>> submittedValues = GetBiometricSettings(values);
            KeyValuePair<string, string>[] valArr = submittedValues.ToArray();
            PostRequest<KeyValuePair<string, string>[]> req = new PostRequest<KeyValuePair<string, string>[]>();
            req.entity = valArr;
            PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId : resp.Summary).Show();
                return;
            }
            else
            {
                FillDefaults(submittedValues);



                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
            }
        }


        protected void SaveEmployeeSettings(object sender, DirectEventArgs e)
        {
            List<KeyValuePair<string, string>> submittedValues = new List<KeyValuePair<string, string>>();
            dynamic values = JsonConvert.DeserializeObject(e.ExtraParams["values"]);
            SaveEmployeeSettings(values);
           
        }
        protected void SaveGeneralSettings(object sender, DirectEventArgs e)
        {
            
            dynamic values = JsonConvert.DeserializeObject(e.ExtraParams["values"]);
            string backofficeEmail = e.ExtraParams["backofficeEmail"];
            SaveGeneralSettings(values);
          
        }
        protected void SaveAttendanceSettings(object sender, DirectEventArgs e)
        {
           
            dynamic values = JsonConvert.DeserializeObject(e.ExtraParams["values"]);
            SaveAttendanceSettings(values);
           
        }

        protected void SavePayrollSettings(object sender, DirectEventArgs e)
        {
           
            dynamic values = JsonConvert.DeserializeObject(e.ExtraParams["values"]);
            SavePayrollSettings(values);
           
        }

        

        protected void SaveSecuritySettings(object sender, DirectEventArgs e)
        {
            
            dynamic values = JsonConvert.DeserializeObject(e.ExtraParams["values"]);
            SaveSecuritySettings(values);
            
        }
        protected void SaveBiometricSettings(object sender, DirectEventArgs e)
        {

            dynamic values = JsonConvert.DeserializeObject(e.ExtraParams["values"]);
            SaveBiometricSettings(values);

        }

        protected void SaveAll(object sender, DirectEventArgs e)
        {
           
            List<KeyValuePair<string, string>> allValues = new List<KeyValuePair<string, string>>();
           

            dynamic empValues = JsonConvert.DeserializeObject(e.ExtraParams["emp"]);
            allValues.AddRange(GetEmployeeSettings(empValues));

            dynamic taValues = JsonConvert.DeserializeObject(e.ExtraParams["ta"]);
            allValues.AddRange(GetAttendanceSettings(taValues));
            dynamic pyValues = JsonConvert.DeserializeObject(e.ExtraParams["py"]);
            allValues.AddRange(GetPayrollSettings(pyValues));
            dynamic genValues = JsonConvert.DeserializeObject(e.ExtraParams["gen"]);
            allValues.AddRange(GetGeneralSettings(genValues));
            dynamic secValues = JsonConvert.DeserializeObject(e.ExtraParams["sec"]);

            allValues.AddRange(GetSecuritySettings(secValues));
            dynamic bioValues = JsonConvert.DeserializeObject(e.ExtraParams["bio"]);
            allValues.AddRange(GetBiometricSettings(bioValues));

            KeyValuePair<string, string>[] valArr = allValues.ToArray();
            PostRequest<KeyValuePair<string, string>[]> req = new PostRequest<KeyValuePair<string, string>[]>();
            req.entity = valArr;
            PostResponse<KeyValuePair<string, string>[]> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>[]>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
                return;
            }
            else
            {
                FillDefaults(allValues);
                if (!string.IsNullOrEmpty(genValues.dateFormat.ToString()))
                    _systemService.SessionHelper.SetDateformat(genValues.dateFormat.ToString());

                if (!string.IsNullOrEmpty(genValues.countryId.ToString()))
                    _systemService.SessionHelper.SetDefaultCountry(genValues.countryId.ToString());
                if (!string.IsNullOrEmpty(genValues.timeZone.ToString()))
                    _systemService.SessionHelper.SetDefaultTimeZone(Convert.ToInt32(genValues.timeZone.ToString()));
                _systemService.SessionHelper.SetHijriSupport(empValues.enableHijri == null ? false : true);

                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });

                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Hint, GetGlobalResourceObject("Common", "systemDefaultAlert")).Show();
            }
        }

        private void FillNationality()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(nationalityRequest);
            NationalityStore.DataSource = resp.Items;
            NationalityStore.DataBind();

        }
        private void FillCurrency()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Currency> resp = _systemService.ChildGetAll<Currency>(nationalityRequest);
            CurrencyStore.DataSource = resp.Items;
            CurrencyStore.DataBind();

        }

        protected void addNationality(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(countryIdCombo.Text))
                return;
            Nationality obj = new Nationality();
            obj.name = countryIdCombo.Text;

            PostRequest<Nationality> req = new PostRequest<Nationality>();
            req.entity = obj;

            PostResponse<Nationality> response = _systemService.ChildAddOrUpdate<Nationality>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillNationality();
                countryIdCombo.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        private void FillCalendars()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<WorkingCalendar> resp = _timeAttendanceService.ChildGetAll<WorkingCalendar>(nationalityRequest);
            caStore.DataSource = resp.Items;
            caStore.DataBind();
        }
        private void FillVS()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<VacationSchedule> resp = _leaveManagementService.ChildGetAll<VacationSchedule>(nationalityRequest);
            vsStore.DataSource = resp.Items;
            vsStore.DataBind();
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

        protected void addCurrency(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(countryIdCombo.Text))
                return;
            Currency dept = new Currency();
            dept.name = currencyIdCombo.Text;

            PostRequest<Currency> depReq = new PostRequest<Currency>();
            depReq.entity = dept;
            PostResponse<Currency> response = _systemService.ChildAddOrUpdate<Currency>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCurrency();
                currencyIdCombo.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        protected void addCalendar(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(caId.Text))
                return;
            WorkingCalendar dept = new WorkingCalendar();
            dept.name = caId.Text;

            PostRequest<WorkingCalendar> depReq = new PostRequest<WorkingCalendar>();
            depReq.entity = dept;
            PostResponse<WorkingCalendar> response = _timeAttendanceService.ChildAddOrUpdate<WorkingCalendar>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCalendars();
                caId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }


        protected void addVS(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(vsId.Text))
                return;
            VacationSchedule dept = new VacationSchedule();
            dept.name = vsId.Text;

            PostRequest<VacationSchedule> depReq = new PostRequest<VacationSchedule>();
            depReq.entity = dept;
            PostResponse<VacationSchedule> response = _leaveManagementService.ChildAddOrUpdate<VacationSchedule>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillVS();
                vsId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        protected void addId(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(idCombo.Text))
                return;
            DocumentType dept = new DocumentType();
            dept.name = idCombo.Text;

            PostRequest<DocumentType> depReq = new PostRequest<DocumentType>();
            depReq.entity = dept;

            PostResponse<DocumentType> response = _employeeService.ChildAddOrUpdate<DocumentType>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                idStore.DataSource = GetRTW();
                idStore.DataBind();
                idCombo.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        protected void addPassport(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(passportCombo.Text))
                return;
            DocumentType dept = new DocumentType();
            dept.name = passportCombo.Text;

            PostRequest<DocumentType> depReq = new PostRequest<DocumentType>();
            depReq.entity = dept;

            PostResponse<DocumentType> response = _employeeService.ChildAddOrUpdate<DocumentType>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                passportStore.DataSource = GetRTW();
                passportStore.DataBind();
                passportCombo.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

 
     
        //protected void addDedSS(object sender, DirectEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(ssDeductionId.Text))
        //        return;
        //    EntitlementDeduction dept = new EntitlementDeduction();
        //    dept.name = ssDeductionId.Text; ;
        //    dept.type = 2;

        //    PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
        //    depReq.entity = dept;
        //    PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
        //    if (response.Success)
        //    {
        //        dept.recordId = response.recordId;
        //        FillCombos();
        //        ssDeductionId.Value = dept.recordId;
        //    }
        //    else
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //         Common.errorMessage(response);
        //        return;
        //    }

        //}
        //protected void addDedpe(object sender, DirectEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(peDeductionId.Text))
        //        return;
        //    EntitlementDeduction dept = new EntitlementDeduction();
        //    dept.name = peDeductionId.Text; ;
        //    dept.type = 2;

        //    PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
        //    depReq.entity = dept;
        //    PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
        //    if (response.Success)
        //    {
        //        dept.recordId = response.recordId;
        //        FillCombos();
        //        peDeductionId.Value = dept.recordId;
        //    }
        //    else
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //         Common.errorMessage(response);
        //        return;
        //    }

        //}
        protected void addDedloan(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(loanDeductionId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = loanDeductionId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCombos();
                loanDeductionId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        private List<EntitlementDeduction> GetEntitlements()
        {
            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
            return eds.Items.Where(s => s.type == 1).ToList();
        }
        private List<EntitlementDeduction> GetDeductions()
        {
            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
            return eds.Items.Where(s => s.type == 2).ToList();
        }
        protected void addAbsenceDed(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(py_aEDId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = py_aEDId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                absenceStore.DataSource = GetDeductions();
                absenceStore.DataBind();
                py_aEDId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
    
        protected void addDisappearanceDed(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(py_dEDId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = py_dEDId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
               disappearanceStore.DataSource = GetDeductions();
               disappearanceStore.DataBind();
                py_dEDId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        protected void addmissedPunchesDed(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(py_mEDId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = py_mEDId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                missedPunchesStore.DataSource = GetDeductions();
                missedPunchesStore.DataBind();
                py_mEDId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        protected void addovertimee(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(py_oEDId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = py_oEDId.Text; ;
            dept.type = 1;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                overtimeStore.DataSource = GetEntitlements();
                overtimeStore.DataBind();
                py_oEDId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        protected void addLatenessDed(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(py_lEDId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = py_lEDId.Text; ;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                latenessStore.DataSource = GetDeductions();
                latenessStore.DataBind();
                py_lEDId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        private void FillSsid()
        {
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<SocialSecuritySchedule> routers = _payrollService.ChildGetAll<SocialSecuritySchedule>(request);
            if (!routers.Success)
            {
                 Common.errorMessage(routers);
                return;
            }
            this.ssIdstore.DataSource = routers.Items;
            this.ssIdstore.DataBind();


        }
        private List<PayrollIndemnity> GetIndemnitySchedules()
        {
            ListRequest req = new ListRequest();
            ListResponse<PayrollIndemnity> eds = _payrollService.ChildGetAll<PayrollIndemnity>(req);
            if (!eds.Success)
            {
                Common.errorMessage(eds);

                return new List<PayrollIndemnity>(); 

            }
            if (eds.Items != null)
                return eds.Items.ToList();
            else
                return new List<PayrollIndemnity>();
        }
        private List<TerminationReason> GetTerminationReasons()
        {
            ListRequest caRequest = new ListRequest();
            ListResponse<TerminationReason> resp = _employeeService.ChildGetAll<TerminationReason>(caRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);

                return new List<TerminationReason>();

            }
            if (resp.Items != null)
                return resp.Items.ToList();
            else
                return new List<TerminationReason>();

        }
        private void FillIndustry()
        {
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Industry> routers = _nationalQuotaService.ChildGetAll<Industry>(request);
            if (!routers.Success)
            {
                 Common.errorMessage(routers);
                return;
            }
            this.NQINidStore.DataSource = routers.Items;

            this.NQINidStore.DataBind();
        }
        private void FillBsid()
        {

            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<BenefitsSchedule> routers = _benefitsService.ChildGetAll<BenefitsSchedule>(request);



            if (!routers.Success)
            {
                 Common.errorMessage(routers);
                return;
            }
            this.bsIdStore.DataSource = routers.Items;
            this.bsIdStore.DataBind();


        }
        private void FillbasicSalaryPayCodeStore()
        {

            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Model.Payroll.PayCode> routers = _payrollService.ChildGetAll<Model.Payroll.PayCode>(request);



            if (!routers.Success)
            {
                 Common.errorMessage(routers);
                return;
            }
            this.basicSalaryPayCodeStore.DataSource = routers.Items;
            this.basicSalaryPayCodeStore.DataBind();


        }

        private void fillFSWDEDId()
        {
            try
            {
                ListRequest req = new ListRequest();
                ListResponse<EntitlementDeduction> routers = _employeeService.ChildGetAll<EntitlementDeduction>(req);
                if (!routers.Success)
                {
                     Common.errorMessage(routers);
                    return;
                }
                this.FSWDEDIdStore.DataSource = routers.Items;
                this.FSWDEDIdStore.DataBind();
            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error,exp.Message).Show();
            }


        }
        protected void DisplayImage(object sender, DirectEventArgs e)
        {

          
                employeePhoto.ImageUrl = "Images/empPhoto.jpg";
                return;
          
         
        }

        protected void RemovePicture(object sender, DirectEventArgs e)
        {
            Attachement at = new Attachement();
            at.classId = ClassId.SYDE;
            at.recordId = 1;
            at.seqNo = 0;
            at.fileName = "companyLogo"; 

            PostRequest<Attachement> req = new PostRequest<Attachement>();
            req.entity = at;
            PostResponse<Attachement> resp = _systemService.ChildDelete<Attachement>(req);

            if (!resp.Success)
            {
               
                    Common.errorMessage(resp);
                    return;
                
            }
            else
            {
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });
                FillCompanyLogo();
                imageSelectionWindow.Close();
            }

        }
    }
}