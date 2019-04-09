using AionHR.Model.AssetManagement;
using AionHR.Model.Reports;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Asset_Management;
using AionHR.Services.Messaging.Reports;
using AionHR.Web.UI.Forms.Reports;
using AionHR.Web.UI.Forms.Reports.Controls;
using AionHR.Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms
{
    public partial class ReportParameterBrowser : System.Web.UI.Page
    {
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
        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                

            }
        }
        private List<UserControl> activeControls;
        private string GetControlNameByClassId(int classId)
        {
            switch (classId)
            {
                case 31421: return "BranchFilter.ascx";
                case 51201: return "ScheduleFilter.ascx";
                default: X.Msg.Alert("Error","unknown control"); return "";

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest && !IsPostBack)
            {
                activeControls = new List<UserControl>();
                SetExtLanguage();
                if (string.IsNullOrEmpty(Request.QueryString["_reportName"]))
                {
                    X.Msg.Alert("Error", "Error");
                }
                ReportParametersListRequest req = new ReportParametersListRequest();
                req.ReportName = Request.QueryString["_reportName"];
                ListResponse<ReportParameter> parameters = _systemService.ChildGetAll<ReportParameter>(req);
                if (!parameters.Success)
                {
                    X.Msg.Alert("Error", "Error");
                }
                int i = -1;
                foreach (var item in parameters.Items)
                {
                    i += 1;
                    if (i == 2)
                        continue;
                    switch (item.controlType)
                    {
                        
                        case 1: TextField tf = new TextField() { ID = item.key, FieldLabel=item.caption };
                            FormPanel1.Items.Add(tf); break;
                        case 3:
                        case 2:NumberField nf = new NumberField() { ID = item.key, FieldLabel = item.caption };
                            FormPanel1.Items.Add(nf);break;
                        case 4: DateField d = new DateField() { ID = item.key, FieldLabel = item.caption }; FormPanel1.Items.Add(d); break;
                        
                        case 5: Control cont = LoadControl("Reports/Controls/" + GetControlNameByClassId(item.classId));
                            cont.ID = item.key;
                            Container c = new Container() { Layout = "FitLayout" };
                            c.ContentControls.Add(cont);
                            FormPanel1.Items.Add(c);
                            break;
                        case 6: Checkbox cb = new Checkbox() { ID = item.key, FieldLabel = item.caption }; cb.InputValue = "true"; break;
                        default: X.Msg.Alert("Error", "unknown control"); break;
                    }
                    
                }
                
            }
        }


        protected void SaveProperties(object sender, DirectEventArgs e)
        {
            string x = "";
            string[] values = e.ExtraParams["values"].Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                x += values[i].Split(':')[0].Replace("\"", "").Replace("{", "").Split('_')[0] + '=' + values[i].Split(':')[1].Replace("\"", "").Replace("}", "")+"&";
            }
            x = x.TrimEnd('&');
            X.Call("parent.ShowParamSelection", x);
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
        protected void CancelClicked(object sender, DirectEventArgs e)
        {
            X.Call("parent.hideWindow");
        }
    }
}