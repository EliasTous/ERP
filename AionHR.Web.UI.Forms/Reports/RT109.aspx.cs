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
using System.Threading;
using Reports;
using AionHR.Model.Reports;
using AionHR.Model.Employees.Profile;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT109 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
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

                statusCombo.Select(0);

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();

                try
                {

                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT109), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;

                        return;
                    }
                    //dateRange1.DefaultStartDate = DateTime.Now.AddDays(-DateTime.Now.Day);
                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                    FillRWDocumentType();
                    FillStatus();
                    //FillReport(false, false);
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
            req.StartAt = "1";


            // req.Add(dateRange1.GetRange());
            //  req.Add(employeeCombo1.GetEmployee());
            
            req.Add(jobInfo1.GetJobInfo());
            req.Add(getStatus());
           


            return req;
        }

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = GetNameFormat();

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }

        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }
        private void FillReport(bool isInitial = false, bool throwException = true)
        {

            ReportCompositeRequest req = GetRequest();

            ListResponse<AionHR.Model.Reports.RT109> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT109>(req);
            if (!resp.Success)
            {

                throw new Exception(resp.Error + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + resp.LogId + "</br>");

            }

           
            resp.Items.ForEach(s =>
            {
           
                s.expiryDateStringFormat = s.expiryDate.ToString(_systemService.SessionHelper.GetDateformat());
                
                
              
            }
            );
            RightToWork h = new RightToWork();
            h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            h.DataSource = resp.Items;

            //  string from = DateTime.Parse(req.Parameters["_fromDate"]).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            //  string to = DateTime.Parse(req.Parameters["_toDate"]).ToString(_systemService.SessionHelper.GetDateformat(), new CultureInfo("en"));
            string user = _systemService.SessionHelper.GetCurrentUser();


            h.Parameters["BranchName"].Value = jobInfo1.GetBranch();
            h.Parameters["PositionName"].Value = jobInfo1.GetPosition();
            h.Parameters["User"].Value = user;
            h.Parameters["DepartmentName"].Value = jobInfo1.GetDepartment();
            h.Parameters["Status"].Value = statusCombo.SelectedItem.Text;






            if (resp.Items.Count > 0)
            {
                if (req.Parameters["_departmentId"] != "0")
                    h.Parameters["DepartmentName"].Value = resp.Items[0].departmentName;
                else
                    h.Parameters["DepartmentName"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_branchId"] != "0")
                    h.Parameters["BranchName"].Value = resp.Items[0].branchName;
                else
                    h.Parameters["BranchName"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_positionId"] != "0")
                    h.Parameters["PositionName"].Value = resp.Items[0].positionName;
                else
                    h.Parameters["PositionName"].Value = GetGlobalResourceObject("Common", "All");


                //if (req.Parameters["_status"] != "0")
                //    h.Parameters["Status"].Value = resp.Items[0];
                //else
                //    h.Parameters["Status"].Value = GetGlobalResourceObject("Common", "All");


                //if (req.Parameters["_employeeId"] != "0")
                //    h.Parameters["Employee"].Value = resp.Items[0].name.fullName;
                //else
                //    h.Parameters["Employee"].Value = GetGlobalResourceObject("Common", "All");
            }

            h.CreateDocument();


            ASPxWebDocumentViewer1.DataBind();
            ASPxWebDocumentViewer1.OpenReport(h);
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
        protected void Unnamed_Click(object sender, EventArgs e)
        {



        }

        protected void ASPxCallbackPanel1_Load(object sender, EventArgs e)
        {
          // ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
           // FillReport(true);
        }
        public RightToworkReportParameter getStatus()
        {
            
            RightToworkReportParameter r = new RightToworkReportParameter();
            if (statusCombo.SelectedItem.Value == null)
                r.status = "0";
            else
            r.status = statusCombo.SelectedItem.Value;
            if (dtId.SelectedItem.Value == null)
                r.dtId = "0";
            else
                r.dtId = dtId.SelectedItem.Value;
            if (esId.SelectedItem.Value == null)
                r.esId = "0";
            else
                r.esId = esId.SelectedItem.Value;


            return r;
        }
        private void FillRWDocumentType()
        {
            ListRequest RWDocumentType = new ListRequest();
            ListResponse<DocumentType> resp = _employeeService.ChildGetAll<DocumentType>(RWDocumentType);
            if (!resp.Success)
               Common.errorMessage(resp);
            RWDocumentTypeStore.DataSource = resp.Items;
            RWDocumentTypeStore.DataBind();

        }
        private void FillStatus()
        {
            ListRequest statusReq = new ListRequest();
            ListResponse<EmploymentStatus> resp = _employeeService.ChildGetAll<EmploymentStatus>(statusReq);
            statusStore.DataSource = resp.Items;
            statusStore.DataBind();
        }

       

    }
}