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
using DevExpress.XtraReports.Web;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT202 : System.Web.UI.Page
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



                    format.Text = _systemService.SessionHelper.GetDateformat();
                    filterSet1.Hidden = false;
                    FillJobInfo();
                    filterSet7.Hidden = false;
                    cc.Format = format.Text;
                    SalaryChanges h = new SalaryChanges();

                    ASPxWebDocumentViewer1.OpenReport(h);
                }
                catch { }
            }

        }

        private void FillJobInfo()
        {
            FillDepartment();
            FillPosition();
            FillBranch();
            FillDivision();

        }
        private void FillDepartment()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();

        }

        private void FillDivision()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();
        }
        private void FillPosition()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            positionStore.DataSource = resp.Items;
            positionStore.DataBind();
        }

        private void ActivateFirstFilterSet()
        {
            filterSet1.Hidden = false;


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

        private JobInfoParameterSet GetJobInfo()
        {
            JobInfoParameterSet p = new JobInfoParameterSet();
            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
            {
                p.BranchId = Convert.ToInt32(branchId.Value);



            }
            else
            {
                p.BranchId = 0;

            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
            {
                p.DepartmentId = Convert.ToInt32(departmentId.Value);


            }
            else
            {
                p.DepartmentId = 0;

            }
            if (!string.IsNullOrEmpty(positionId.Text) && positionId.Value.ToString() != "0")
            {
                p.PositionId = Convert.ToInt32(positionId.Value);


            }
            else
            {
                p.PositionId = 0;

            }
            if (!string.IsNullOrEmpty(divisionId.Text) && divisionId.Value.ToString() != "0")
            {
                p.DivisionId = Convert.ToInt32(divisionId.Value);


            }
            else
            {
                p.DivisionId = 0;

            }

            return p;
        }
        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "1";
            req.SortBy = "firstName";
            JobInfoParameterSet p = GetJobInfo();

            req.Add(p);

            ActiveStatusParameterSet s = new ActiveStatusParameterSet();
            int bulk;
            if (!int.TryParse(inactivePref.Value.ToString(), out bulk))
                s.active = 1;
            else
                s.active = bulk;
            req.Add(s);
            return req;
        }
        protected void firstStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            ReportCompositeRequest req = GetRequest();
            ListResponse<AionHR.Model.Reports.RT202> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT202>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }



            //firstStore.DataSource = resp.Items;
            //firstStore.DataBind();

        }

        [DirectMethod]
        public void FillReport()
        {
            ReportCompositeRequest req = GetRequest();
            ListResponse<AionHR.Model.Reports.RT202> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT202>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }

            SalaryChanges h = new SalaryChanges();
            h.DataSource = resp.Items;
            ASPxWebDocumentViewer1 = new ASPxWebDocumentViewer();


            h.CreateDocument();
            ASPxWebDocumentViewer1.OpenReport(h);
            ASPxWebDocumentViewer1.DataBind();

        }

        protected void Unnamed_Event(Object sender, DirectEventArgs e)
        {
            ReportCompositeRequest req = GetRequest();
            ListResponse<AionHR.Model.Reports.RT202> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT202>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }

            SalaryChanges h = new SalaryChanges();
            h.DataSource = resp.Items;



            h.CreateDocument();
            ASPxWebDocumentViewer1.OpenReport(h);
            ASPxWebDocumentViewer1.DataBind();
        }
    }
}