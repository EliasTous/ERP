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
using AionHR.Model.Employees.Profile;

namespace AionHR.Web.UI.Forms
{
    public partial class EmployeesOrgChart : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
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
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.System.EmpOrgChart), null, null, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    topBar.Hidden= true;
                    return;
                }
                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();

                FillHirarichy();

            }

        }

        private List<object> GetEmpsJson(List<Employee> emps)
        {



            List<object> result = new List<object>();
            foreach (var item in emps)
            {
                result.Add(new {id=item.recordId,reportToId=item.reportToId, name = item.name.fullName, parent = item.reportToName==null?"": item.reportToName.fullName, position=item.positionName,picture=item.pictureUrl, tooltip = "''" });

            }
            return result;
        }
        private void FillHirarichy()
        {

            EmployeeListRequest empRequest = new EmployeeListRequest();
            empRequest.BranchId = "0";
            empRequest.DepartmentId = "0";
         
            empRequest.IncludeIsInactive = 0;
            string sortby= GetNameFormat();
            empRequest.SortBy = "lastName";

            empRequest.Size = "1000";
            empRequest.StartAt = "0";

            ListResponse<Employee> emps = _employeeService.GetAll<Employee>(empRequest);
            if (!emps.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, emps.Summary).Show();
                return;
            }

            X.Call("Init", GetEmpsJson(emps.Items));
        }


        private string GetNameFormat()
        {
            string nameformat= _systemService.SessionHelper.Get("nameFormat").ToString();
            return _systemService.SessionHelper.Get("nameFormat").ToString();

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
    }
}