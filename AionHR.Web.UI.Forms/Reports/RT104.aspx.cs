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

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class RT104 : System.Web.UI.Page
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



                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();

                    ASPxCallbackPanel1_Callback(null, null);
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
            ReportCompositeRequest request = new ReportCompositeRequest();
            request.Size = "1000";
            request.StartAt = "1";
            request.SortBy = "yearsInService";
            request.Add(jobInfoFilter1.GetJobInfo());
            return request;
           
        }


        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] parameters = e.Parameter.Split('|');
            int pageIndex = Convert.ToInt32(parameters[0]);

            if (pageIndex == 1)
            {
                ReportCompositeRequest req = GetRequest();
                ListResponse<AionHR.Model.Reports.RT104> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT104>(req);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                    return;
                }

                YearsInService y = new YearsInService();

                List<AionHR.Model.Reports.RT104> reordered = resp.Items.Where(x => x.hiredMonth >= DateTime.Now.Month).ToList();
                reordered.AddRange(resp.Items.Where(x => x.hiredMonth < DateTime.Now.Month).OrderBy(h => h.hiredMonth).ToList());
                reordered.ForEach(x => x.HiredMonthString = GetGlobalResourceObject("Common", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.hiredMonth)).ToString());
                y.DataSource = reordered;


                ASPxWebDocumentViewer1.DataBind();
                ASPxWebDocumentViewer1.OpenReport(y);
            }

        }
     
    }
}