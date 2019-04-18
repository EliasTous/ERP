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
using AionHR.Model.Dashboard;
using AionHR.Web.UI.Forms.ConstClasses;
using System.Text.RegularExpressions;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT305 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IDashBoardService _dashBoardService = ServiceLocator.Current.GetInstance<IDashBoardService>();


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
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT302), null, null, null, null);
                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }

                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                    ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                   
                    
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
        private void FillReport(bool isInitial = false, bool throwException = true)
        {
            try
            {
                string rep_params = vals.Text;
                ReportGenericRequest req = new ReportGenericRequest();
                req.paramString = rep_params;
                

                ListResponse<Model.Reports.RT305> resp = _reportsService.ChildGetAll<Model.Reports.RT305>(req);
                if (!resp.Success)
                    Common.ReportErrorMessage(resp, GetGlobalResourceObject("Errors", "Error_1").ToString(), GetGlobalResourceObject("Errors", "ErrorLogId").ToString());


                // var edAmountList = resp.Items.GroupBy(x => new { x.employeeId, x.timeCode });
                //// List<Model.Reports.RT305> RT305 = new List<Model.Reports.RT305>();
                // foreach (var item in edAmountList)
                // {


                //     var sums = item.ToList().GroupBy(x => new {x.employeeId , x.timeCode })
                //                      .Select(group => group.Sum(x => x.edAmount)).First();

                //         resp.Items.Where(x => x.employeeId == item.ToList().First().employeeId && x.timeCode == item.ToList().First().timeCode).ToList().ForEach(y => y.edAmount = Math.Round(sums, 3));
                //     //item.ToList().First().edAmount = Convert.ToDouble(sums);
                //     //RT305.Add(item.First());


                // }

                bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
                

                Absense h = new Absense();
                h.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
                h.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
                h.DataSource = resp.Items;

                
                string user = _systemService.SessionHelper.GetCurrentUser();

             //   h.Parameters["Filters"].Value = texts.Text;
                h.Parameters["User"].Value = user;



                var values = texts.Text.Split(']');
                string[] filter = new string[values.Length - 1];

                for (int i = 0; i < values.Length - 1; i++)
                {
                    filter[i] = values[i];
                    filter[i] = Regex.Replace(filter[i], @"\[", "");
                    string[] parametrs = filter[i].Split(':');
                    for (int x = 0; x <= parametrs.Length - 1; x = +2)
                    {
                        if (parametrs[x] == "department")
                        {
                            h.Parameters["Department"].Value = parametrs[x + 1];
                            break;
                        }
                        if (parametrs[x] == "branch")
                        {
                            h.Parameters["Branch"].Value = parametrs[x + 1];
                            break;
                        }
                        if (parametrs[x] == "division")
                        {
                            h.Parameters["Division"].Value = parametrs[x + 1];
                            break;
                        }
                        if (parametrs[x] == "position")
                        {
                            h.Parameters["Position"].Value = parametrs[x + 1];
                            break;
                        }
                        if (parametrs[x] == "employment status")
                        {
                            h.Parameters["EmploymentStatus"].Value = parametrs[x + 1];
                            break;
                        }
                        if (parametrs[x] == "time code")
                        {
                            h.Parameters["TimeCode"].Value = parametrs[x + 1];
                            break;
                        }
                        if (parametrs[x] == "approval status")
                        {
                            h.Parameters["ApprovalStatus"].Value = parametrs[x + 1];
                            break;
                        }
                        if (parametrs[x] == "employee")
                        {
                            h.Parameters["Employee"].Value = parametrs[x + 1];
                            break;
                        }






                    }

                }
                if (string.IsNullOrEmpty(h.Parameters["Department"].Value.ToString()))
                    h.Parameters["Department"].Value = GetGlobalResourceObject("Common", "All");
                if (string.IsNullOrEmpty(h.Parameters["Branch"].Value.ToString()))
                    h.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");
                if (string.IsNullOrEmpty(h.Parameters["Division"].Value.ToString()))
                    h.Parameters["Division"].Value = GetGlobalResourceObject("Common", "All");

                if (string.IsNullOrEmpty(h.Parameters["Position"].Value.ToString()))
                    h.Parameters["Position"].Value = GetGlobalResourceObject("Common", "All");
                if (string.IsNullOrEmpty(h.Parameters["EmploymentStatus"].Value.ToString()))
                    h.Parameters["EmploymentStatus"].Value = GetGlobalResourceObject("Common", "All");
                if (string.IsNullOrEmpty(h.Parameters["TimeCode"].Value.ToString()))
                    h.Parameters["TimeCode"].Value = GetGlobalResourceObject("Common", "All");
                if (string.IsNullOrEmpty(h.Parameters["ApprovalStatus"].Value.ToString()))
                    h.Parameters["ApprovalStatus"].Value = GetGlobalResourceObject("Common", "All");





                h.PrintingSystem.Document.ScaleFactor = 4;
                h.CreateDocument();


                ASPxWebDocumentViewer1.OpenReport(h);
            }
            catch (Exception exp)
            {
                X.MessageBox.Alert(GetGlobalResourceObject("Common","Error").ToString(),exp.Message).Show();
                
            }

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
            ASPxWebDocumentViewer1.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            //FillReport(true);
        }

      

  
    }
}