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
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Web.UI.Forms.Utilities;
using Model.Company.News;
using Services.Messaging;
using Model.System;
using Model.Attendance;
using Model.Employees.Leaves;
using Model.Employees.Profile;
using Model.Payroll;
using Model.NationalQuota;
using Model.Benefits;
using Services.Messaging.System;
using Infrastructure.Domain;
using Model.Employees;
using Model.AdminTemplates;

namespace Web.UI.Forms
{
    public partial class AdminDepts : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IAdministrationService _administrationService = ServiceLocator.Current.GetInstance<IAdministrationService>();
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

                DepartmentListRequest alldeptsreq = new DepartmentListRequest();
                ListResponse<Model.Company.Structure.Department> alldepts = _companyStructureService.ChildGetAll<Model.Company.Structure.Department>(alldeptsreq);
                if (!alldepts.Success)
                {
                    Common.errorMessage(alldepts);
                    return;
                }
                List<AdminDepartment> all = new List<AdminDepartment>();
                alldepts.Items.ForEach(x => all.Add(new AdminDepartment() { departmentId = x.recordId, departmentName = x.name }));
                departmentStore.DataSource = all;
                departmentStore.DataBind();
                this.departmentSelector.SelectedItems.Clear();

                ListRequest req = new ListRequest();
                ListResponse<AdminDepartment> resp = _administrationService.ChildGetAll<AdminDepartment>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }

                resp.Items.ForEach(x =>
                {
                    this.departmentSelector.SelectedItems.Add(new Ext.Net.ListItem() { Value = x.departmentId });
                });


                this.departmentSelector.UpdateSelectedItems();
            }
            
            
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
        protected void SaveDepartments(object sender, DirectEventArgs e)
        {
            ListRequest req = new ListRequest();
            ListResponse<AdminDepartment> resp = _administrationService.ChildGetAll<AdminDepartment>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            PostRequest<AdminDepartment> delReq = new PostRequest<AdminDepartment>();
            PostResponse<AdminDepartment> delResp;
            resp.Items.ForEach(old =>
            {
                delReq.entity = old;
                delResp = _administrationService.ChildDelete<AdminDepartment>(delReq);
                if (!delResp.Success)
                {
                    Common.errorMessage(delResp);
                    throw new Exception();
                }

            });
            List<AdminDepartment> depts = JSON.Deserialize<List<AdminDepartment>>(e.ExtraParams["selectedDepts"]);
            foreach (AdminDepartment dept in depts) {
                PostRequest<AdminDepartment> postReq = new PostRequest<AdminDepartment>();
                postReq.entity = dept;
                PostResponse<AdminDepartment> postResp = _administrationService.ChildAddOrUpdate<AdminDepartment>(postReq);
                if(!postResp.Success)
                {
                    Common.errorMessage(postResp);
                    return;
                }
            }
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordSavingSucc
            });
            departmentStore.Reload();
        }
        protected void departmentStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
           
        }
    }
}