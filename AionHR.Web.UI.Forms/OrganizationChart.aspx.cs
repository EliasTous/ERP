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

namespace AionHR.Web.UI.Forms
{
    public partial class OrganizationChart : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
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
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.System.CompanyOrgChart), null, null, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    topBar.Hidden = true;
                    return;
                }
                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                type.Select(0);
                FillHirarichy(sender, new DirectEventArgs(new Ext.Net.ParameterCollection()));

            }

        }

        private List<object> GetDeptsJson(List<Department> depts)
        {



            List<object> result = new List<object>();
            foreach (var item in depts)
            {
                result.Add(new { name = item.name, parent = item.parentName, tooltip = "''" });

            }
            return result;
        }

        protected void FillHirarichy(object sender, DirectEventArgs e)
        {

            DepartmentListRequest req = new DepartmentListRequest();

            try
            {
                req.type = Convert.ToInt32(type.SelectedItem.Value);
            } 
            catch (Exception)
            {

                
            }
            ListResponse<Department> response = _companyStructureService.ChildGetAll<Department>(req);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() : response.Summary).Show();
                return;
            }

            X.Call("Init", GetDeptsJson(response.Items));
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
            //this.colAttach.Visible = false;
        }


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;


            }
        }






        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>





        [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }

        protected void Unnamed_Event()
        {

        }
    }
}