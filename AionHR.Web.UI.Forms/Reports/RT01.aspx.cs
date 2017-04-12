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
    public partial class RT01 : System.Web.UI.Page
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

                }
                catch { }
            }

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

        protected void reportStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "1";
            req.SortBy = "departmentName";
            ListResponse<AionHR.Model.Reports.RT01> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT01>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            List<object> l = new List<object>();


            l.Add(new { id =GetLocalResourceObject("age00_18").ToString(), count = resp.Items.Sum(x => x.age00_18) });
            l.Add(new { id = GetLocalResourceObject("age18_25").ToString(), count = resp.Items.Sum(x => x.age18_25) });
            l.Add(new { id = GetLocalResourceObject("age26_30").ToString(), count = resp.Items.Sum(x => x.age26_30) });
            l.Add(new { id = GetLocalResourceObject("age30_40").ToString(), count = resp.Items.Sum(x => x.age30_40) });
            l.Add(new { id = GetLocalResourceObject("age40_50").ToString(), count = resp.Items.Sum(x => x.age40_50) });
            l.Add(new { id = GetLocalResourceObject("age50_60").ToString(), count = resp.Items.Sum(x => x.age50_60) });
            l.Add(new { id = GetLocalResourceObject("age60_99").ToString(), count = resp.Items.Sum(x => x.age60_99) });
            summaryStore.DataSource = l;
            summaryStore.DataBind();
            reportStore.DataSource = resp.Items;
            reportStore.DataBind();
            

        }
    }
}