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
using AionHR.Services.Messaging.Reports;
using Reports;
using System.Threading;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT108 : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
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

                try
                {
                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Reports.RT104), null, null, null, null);
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
                    FillReport(false);
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




        private void FillReport(bool throwException = true)
        {
            ReportCompositeRequest req = GetRequest();
            ListResponse<AionHR.Model.Reports.RT108> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT108>(req);
            if (!resp.Success)
            {
                if (throwException)
                    throw new Exception(resp.Summary);
                else
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                    return;
                }
            }

            EmployeeDetails y = new EmployeeDetails();
            y.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            y.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            string format = _systemService.SessionHelper.GetDateformat();
            CultureInfo cul = new CultureInfo("en");
            resp.Items.ForEach(x =>
            {
                x.hireDateString = x.hireDate.HasValue? x.hireDate.Value.ToString(format, cul) :"";
                
                x.idExpiryString = x.resExpiryDate.HasValue? x.resExpiryDate.Value.ToString(format, cul) :"";
                x.passportExpiryString = x.passportExpiryDate.HasValue? x.passportExpiryDate.Value.ToString(format, cul) :"";
                x.terminationDateString = x.terminationDate.HasValue? x.terminationDate.Value.ToString(format, cul) :"";
                x.lastLeaveReturnString = x.lastLeaveReturnDate.HasValue? x.lastLeaveReturnDate.Value.ToString(format, cul) :"";
                x.termEndDateString = x.termEndDate.HasValue ? x.termEndDate.Value.ToString(format, cul) : "";
                x.genderString = x.gender==0?GetGlobalResourceObject("Common","Male").ToString(): GetGlobalResourceObject("Common", "Female").ToString();
                x.isInactiveString = x.isInactive?GetGlobalResourceObject("Common", "Inactive").ToString():GetGlobalResourceObject("Common", "Active").ToString();
            });
            y.DataSource = resp.Items;
            string user = _systemService.SessionHelper.GetCurrentUser();
            y.Parameters["User"].Value = user;
            if (resp.Items.Count > 0)
            {
                if (req.Parameters["_departmentId"] != "0")
                    y.Parameters["Department"].Value = jobInfo1.GetDepartment();
                else
                    y.Parameters["Department"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_branchId"] != "0")
                    y.Parameters["Branch"].Value = jobInfo1.GetBranch();
                else
                    y.Parameters["Branch"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_positionId"] != "0")
                    y.Parameters["Position"].Value = jobInfo1.GetPosition();
                else
                    y.Parameters["Position"].Value = GetGlobalResourceObject("Common", "All");

                if (req.Parameters["_divisionId"] != "0")
                    y.Parameters["Division"].Value = jobInfo1.GetDivision();
                else
                    y.Parameters["Division"].Value = GetGlobalResourceObject("Common", "All");
            }
            ASPxWebDocumentViewer1.DataBind();
            ASPxWebDocumentViewer1.OpenReport(y);
        }
        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest request = new ReportCompositeRequest();
            request.Size = "1000";
            request.StartAt = "1";
            request.SortBy = "hireDate";
            request.Add(jobInfo1.GetJobInfo());
            request.Add(activeControl.GetActiveStatus());
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
    }
}