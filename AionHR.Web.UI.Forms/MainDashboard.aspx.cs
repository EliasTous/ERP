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
namespace AionHR.Web.UI.Forms
{
    public partial class MainDashboard : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();


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

                    FillDashboard();
                    format.Text = _systemService.SessionHelper.GetDateformat().ToUpper();
                }
                catch { }
            }

        }

     private void FillDashboard()
        {
            ListRequest req = new ListRequest();


            ListResponse<DashboardItem> dashoard = _systemService.ChildGetAll<DashboardItem>(req);
            if (!dashoard.Success)
            {
                X.Msg.Alert(Resources.Common.Error, dashoard.Summary).Show();
                return;
            }

            store1.DataSource = dashoard.Items.Where(d => d.itemId / 10 == 1).ToList();
            store1.DataBind();

            store2.DataSource = dashoard.Items.Where(d => d.itemId / 10 == 2).ToList();
            store2.DataBind();

            store3.DataSource = dashoard.Items.Where(d => d.itemId / 10 == 3).ToList();
            store3.DataBind();

            store4.DataSource = dashoard.Items.Where(d => d.itemId / 10 == 4).ToList();
            store4.DataBind();

            store5.DataSource = dashoard.Items.Where(d => d.itemId / 10 == 5).ToList();
            store5.DataBind();

            store6.DataSource = dashoard.Items.Where(d => d.itemId / 10 == 6).ToList();
            store6.DataBind();
        }


        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }

        private void FillLatenessGrid()
        {
            ActiveAttendanceRequest r = new ActiveAttendanceRequest();
            r.BranchId = r.DepartmentId = r.PositionId = "0";
            r.StartAt = "0";
            r.Size = "1000";
            
            ListResponse<ActiveLate> ALs = _timeAttendanceService.ChildGetAll<ActiveLate>(r);
            if (!ALs.Success)
            {
                X.Msg.Alert(Resources.Common.Error, ALs.Summary).Show();
                return;
            }
            latenessStore.DataSource = ALs.Items;
            latenessStore.DataBind();
        }
        protected void PoPuP(object sender, DirectEventArgs e)
        {
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            switch (id)
            {
                case "12":
                    FillLatenessGrid();
                    latenessWindow.Show();
                    
                    break;
            }
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





       
    }
}