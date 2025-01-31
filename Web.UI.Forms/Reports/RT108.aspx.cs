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
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Web.UI.Forms.Utilities;
using Model.Company.News;
using Services.Messaging;
using Model.Company.Structure;
using Model.System;
using Model.Attendance;
using Services.Messaging.Reports;
using Reports;
using System.Threading;
using Model.Employees.Profile;
using Model.Reports;
using System.Text.RegularExpressions;

namespace Web.UI.Forms.Reports
{
    public partial class RT108 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();



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

                try
                {
                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(Model.Reports.RT104), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }
                    
                    //GenderCombo.Select(0);
                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                    //FillReport(false);
                }
                catch { }
            }

        }


       
        private void ActivateFirstFilterSet()
        {



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
                this.rtl.Text = rtl.ToString();
                Culture = "ar";
                UICulture = "ar-SA";
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-AE");
            }
            else
            {
                Culture = "en";
                UICulture = "en-US";
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
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
        public void SetLabels(string labels)
        {
            this.labels.Text = labels;
        }

        [DirectMethod]
        public void SetVals(string labels)
        {
            this.vals.Text = labels;
        }

        [DirectMethod]
        public void SetTexts(string labels)
        {
            this.texts.Text = labels;
        }
        private void FillReport(bool throwException = true)
        {
            string rep_params = vals.Text;
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = rep_params;
           
            ListResponse<Model.Reports.RT108> resp = _reportsService.ChildGetAll<Model.Reports.RT108>(req);
            //if (resp == null || string.IsNullOrEmpty(resp.Error))
            //{
            //    throw new Exception(GetGlobalResourceObject("Errors", "Error_1").ToString());
            //}
            //if (!resp.Success)
            //{
            //    throw new Exception(resp.Error + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId + "</br>");
            //}
            if (!resp.Success)
                Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());
            Dictionary<string, string> parameters = Web.UI.Forms.Common.FetchReportParameters(texts.Text);
            EmployeeDetails y = new EmployeeDetails(parameters);
            y.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            y.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            string format = _systemService.SessionHelper.GetDateformat();
            CultureInfo cul = CultureInfo.CurrentCulture; 
            resp.Items.ForEach(x =>
            {
                x.hireDateString = x.hireDate.HasValue? x.hireDate.Value.ToString(format, cul) :"";
                
                x.idExpiryString = x.resExpiryDate.HasValue? x.resExpiryDate.Value.ToString(format, cul) :"";
                x.passportExpiryString = x.passportExpiryDate.HasValue? x.passportExpiryDate.Value.ToString(format, cul) :"";
                x.terminationDateString = x.terminationDate.HasValue? x.terminationDate.Value.ToString(format, cul) :"";
                x.lastLeaveReturnString = x.lastLeaveReturnDate.HasValue? x.lastLeaveReturnDate.Value.ToString(format, cul) :"";
                x.termEndDateString = x.termEndDate.HasValue ? x.termEndDate.Value.ToString(format, cul) : "";
                x.genderString = x.gender==1?GetGlobalResourceObject("Common","Male").ToString(): GetGlobalResourceObject("Common", "Female").ToString();
              switch (x.religion)
                {
                   
                    case 0 :x.religionString = GetGlobalResourceObject("Common", "Religion0").ToString();
                        break;
                    case 1:x.religionString = GetGlobalResourceObject("Common", "Religion1").ToString();
                        break;
                    case 2:
                        x.religionString = GetGlobalResourceObject("Common", "Religion2").ToString();
                        break;
                    case 3:
                        x.religionString = GetGlobalResourceObject("Common", "Religion3").ToString();
                        break;
                    case 4:
                        x.religionString = GetGlobalResourceObject("Common", "Religion4").ToString();
                        break;
                    case 5:
                        x.religionString = GetGlobalResourceObject("Common", "Religion5").ToString();
                        break;
                    case 6:
                        x.religionString = GetGlobalResourceObject("Common", "Religion6").ToString();
                        break;
                    default: x.religionString = "";
                        break; 

                }
                x.isInactiveString =(ActiveStatus) x.activeStatus==ActiveStatus.INACTIVE?GetGlobalResourceObject("Common", "Inactive").ToString():GetGlobalResourceObject("Common", "Active").ToString();
            });
            y.DataSource = resp.Items;
            string user = _systemService.SessionHelper.GetCurrentUser();
            y.Parameters["User"].Value = user;
           

          
           
            ASPxWebDocumentViewer1.DataBind();
            ASPxWebDocumentViewer1.OpenReport(y);
        }
       private GenderParameterSet GetGenderFilter()
        {
            GenderParameterSet s = new GenderParameterSet();
            s.gender = Convert.ToInt16(GenderCombo.SelectedItem.Value);
            return s; 
        }
        private SponserParameterSet GetSponserFilter()
        {
            SponserParameterSet s = new SponserParameterSet();
            s.sponsorId = Convert.ToInt16(sponsorId.SelectedItem.Value);
            return s;
        }
        private LocalsParameterSet GetLocalsFilter()
        {
            LocalsParameterSet s = new LocalsParameterSet();
            s.locals = Convert.ToInt16(locals.SelectedItem.Value);
            return s;
        }
        private ReportCompositeRequest GetRequest()
        {
          

            ReportCompositeRequest request = new ReportCompositeRequest();
            request.Size = "1000";
            request.StartAt = "0";
            request.SortBy = "hireDate";
            request.Add(jobInfo1.GetJobInfo());
            request.Add(activeControl.GetActiveStatus());
            request.Add(GetCountryInfo());
            request.Add(employeeFilter.GetEmployee());
            request.Add(GetGenderFilter());
            request.Add(GetSponserFilter());
            request.Add(GetLocalsFilter());



            return request;

        }


        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] parameters = e.Parameter.Split('|');
            int pageIndex = Convert.ToInt32(parameters[0]);

            if (pageIndex == 1)
            {
                FillReport();
            }

        }

        protected void ASPxCallbackPanel1_Load(object sender, EventArgs e)
        {
            //ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            //FillReport();
        }
        [DirectMethod]
        protected void addCountry(object sender, DirectEventArgs e)
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
                FillCountry();
                countryIdCombo.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }
        private void FillCountry()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(nationalityRequest);
            NationalityStore.DataSource = resp.Items;
            NationalityStore.DataBind();

        }
        public CountryParameterSet GetCountryInfo()
        {
            CountryParameterSet p = new CountryParameterSet();
           
           
                if (!string.IsNullOrEmpty(countryIdCombo.Text) && countryIdCombo.Value.ToString() != "0")
                {
                    p.countryId = Convert.ToInt32(countryIdCombo.Value);
                

                }
                else
                {
                p.countryId = 0;

                }
           
            return p;
        }
        //[DirectMethod]
        //public object FillEmployee(string action, Dictionary<string, object> extraParams)
        //{
        //    StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        //    List<Employee> data = GetEmployeesFiltered(prms.Query);
        //    data.ForEach(s => { s.fullName = s.name.fullName; });
        //    //  return new
        //    // {
        //    return data;
        //}
        //private List<Employee> GetEmployeesFiltered(string query)
        //{

        //    EmployeeListRequest req = new EmployeeListRequest();
        //    req.DepartmentId = "0";
        //    req.BranchId = "0";
        //    req.IncludeIsInactive = 2;
        //    req.SortBy = GetNameFormat();

        //    req.StartAt = "0";
        //    req.Size = "20";
        //    req.Filter = query;

        //    ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
        //    return response.Items;
        //}
        //private string GetNameFormat()
        //{
        //    return _systemService.SessionHelper.Get("nameFormat").ToString();
        //}

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }
    }
}