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
using AionHR.Services.Messaging.Reports;
using DevExpress.XtraReports.Web;
using DevExpress.XtraPrinting.Localization;
using System.Threading;
using AionHR.Services.Messaging.System;
using AionHR.Model.Employees.Profile;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT802 : System.Web.UI.Page
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
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;

            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();

                try
                {
                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT802), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }


                    dateRange1.DefaultStartDate = DateTime.Today;
                    format.Text = _systemService.SessionHelper.GetDateformat();
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
                UICulture = "ar-AE";
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


        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "0";
            req.SortBy = "eventDt";


            req.Add(dateRange1.GetRange());
            req.Add(userCombo1.GetUser());
            req.Add(transactionCombo1.GetTransactionType());
            req.Add(moduleCombo1.GetModule());
            req.Add(getMasterRef());
            req.Add(getData());


            //req.Add();
            return req;
        }
        [DirectMethod]
        public object FillUsers(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
          List<UserInfo> data = GetUsersFiltered(prms.Query);

            //  return new
            // {
            return data;
        }

        private List<UserInfo> GetUsersFiltered(string query)
        {
            UsersListRequest req = new UsersListRequest();
            req.Size = "100";
            req.StartAt = "0";
         


            req.DepartmentId = "0";
            req.PositionId = "0";
            req.BranchId = "0";
            req.SortBy = "fullName";
            req.Filter = query;

            ListResponse<UserInfo> users = _systemService.ChildGetAll<UserInfo>(req);
            return users.Items;
        }

        private void FillReport(bool throwException = true)
        {
            int count = 0; 
            ReportCompositeRequest req = GetRequest();


            foreach (KeyValuePair<string, string> entry in req.Parameters)
            {
                if (entry.Key == "_fromDate" || entry.Key == "_toDate" || entry.Key == "_size" || entry.Key == "_startAt"|| entry.Key == "_sortBy" || entry.Key == "_filter")
                {
                    continue;
                }
                if (entry.Key == "_masterRef" && !string.IsNullOrEmpty(entry.Value))
                {
                    count++;
                    continue;
                }
                if (entry.Key == "_data" && !string.IsNullOrEmpty(entry.Value))
                {
                    count++;
                    continue;
                }
                if (entry.Key == "_data" && string.IsNullOrEmpty(entry.Value))
                {
                   continue;
                }
                if (entry.Value != "0"   && entry.Key != "_masterRef")
                    count++;
               
            }



            if (dateRange1.DifferenceBetweenDates()!=null && dateRange1.DifferenceBetweenDates()>30&& count<2)
            {
                throw new Exception(FilterSelection.Value.ToString());
              
            }

            ListResponse<AionHR.Model.Reports.RT802> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT802>(req);
            if (!resp.Success)
            {

                throw new Exception(resp.Error + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId + "</br>");

            }
            resp.Items.ForEach(x => { x.TypeString = GetGlobalResourceObject("Common", "TrType" + x.type.ToString()).ToString(); x.ClassIdString = GetGlobalResourceObject("Classes", "Class" + x.classId.ToString())!=null? GetGlobalResourceObject("Classes", "Class" + x.classId.ToString()).ToString():"NA"; x.DateString = x.eventDt.ToString(_systemService.SessionHelper.GetDateformat()+ " HH:mm", new CultureInfo("en")); });
            AuditTrail h = new AuditTrail();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            h.Parameters["username"].Value = _systemService.SessionHelper.Get("CurrentUserName");
            h.Parameters["From"].Value = DateTime.Parse(req.Parameters["_fromDate"]).ToString(_systemService.SessionHelper.GetDateformat());
            h.Parameters["To"].Value = DateTime.Parse(req.Parameters["_toDate"]).ToString(_systemService.SessionHelper.GetDateformat());
            if (resp.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(req.Parameters["_trxType"]))
                    h.Parameters["TrType"].Value = GetGlobalResourceObject("Common", "TrType" + req.Parameters["_trxType"]);
                else
                    h.Parameters["TrType"].Value = GetGlobalResourceObject("Common", "All");

                if (!string.IsNullOrEmpty(req.Parameters["_moduleId"]))
                    h.Parameters["Module"].Value = GetGlobalResourceObject("Common", "Mod" + req.Parameters["_moduleId"]);
                else
                    h.Parameters["Module"].Value = GetGlobalResourceObject("Common", "All");

                if (resp.Items.Count > 0 && req.Parameters["_userId"] != "0")
                    h.Parameters["User"].Value = resp.Items[0].userName;
                else
                    h.Parameters["User"].Value = GetGlobalResourceObject("Common", "All");

            }
            h.DataSource = resp.Items;


            h.CreateDocument();
            ASPxWebDocumentViewer1.OpenReport(h);
            ASPxWebDocumentViewer1.DataBind();
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
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            data.ForEach(s => { s.fullName = s.name.fullName; s.reference = s.name.reference; });
            //  return new
            // {
            return data;
        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 0;
            req.SortBy = GetNameFormat();

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }


        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }
        private MasterIdParameterSet getMasterRef()
        {
            MasterIdParameterSet s = new MasterIdParameterSet();
         
            if (string.IsNullOrEmpty(masterRef.Value.ToString() ))

                s.masterRef = "";
            else
                s.masterRef = masterRef.Value.ToString();
            return s;



        }
        private DataParameterSet getData()
        {
            DataParameterSet s = new DataParameterSet();

            if (string.IsNullOrEmpty(data.Text))

                s.data = "";
            else
                s.data = data.Text;
            return s;



        }

    }
}