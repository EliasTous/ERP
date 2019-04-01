using AionHR.Model.AssetManagement;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Asset_Management;
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
    public partial class AssetPropertyExplorer : System.Web.UI.Page
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
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest && !IsPostBack)
            {
                SetExtLanguage();
                if (string.IsNullOrEmpty(Request.QueryString["_catId"]) || string.IsNullOrEmpty(Request.QueryString["_assetId"]))
                {
                    X.Msg.Alert("Error", "Error");
                }
                currentAsset.Text = Request.QueryString["_assetId"];
                currentCat.Text = Request.QueryString["_catId"];
                AssetManagementCategoryPropertyListRequest propReq = new AssetManagementCategoryPropertyListRequest();
                propReq.categoryId = Request.QueryString["_catId"].ToString();
                AssetPropertyValueListRequest valReq = new AssetPropertyValueListRequest();
                valReq.assetId = Request.QueryString["_assetId"].ToString();
                valReq.categoryId = Request.QueryString["_catId"].ToString();
                ListResponse<AssetManagementCategoryProperty> propResp = _assetManagementService.ChildGetAll<AssetManagementCategoryProperty>(propReq);
                if (!propResp.Success)
                    Common.errorMessage(propResp);
                ListResponse<AssetPropertyValue> valResp = _assetManagementService.ChildGetAll<AssetPropertyValue>(valReq);
                if (!valResp.Success)
                    Common.errorMessage(valResp);
                try
                {
                    propResp.Items.ForEach(x =>
                    {
                        if (x.mask.HasValue)
                        {
                            AssetPropertyValue corrVal = valResp.Items.Where(d => d.propertyId == x.propertyId).ToList().Count > 0 ? valResp.Items.Where(d => d.propertyId == x.propertyId).ToList()[0] : null;
                            switch (x.mask)
                            {
                                case 1: propertiesForm.Items.Add(new TextField() { Text = corrVal != null && !string.IsNullOrEmpty(corrVal.value) ? corrVal.value : "", FieldLabel = x.caption, Name = x.propertyId }); break; //text
                            case 2: propertiesForm.Items.Add(new NumberField() { Value = corrVal != null && !string.IsNullOrEmpty(corrVal.value) ? corrVal.GetValueDouble() : 0, FieldLabel = x.caption, Name = x.propertyId }); break; //number
                            case 3: propertiesForm.Items.Add(new DateField() { SelectedDate = corrVal != null && !string.IsNullOrEmpty(corrVal.value) ? corrVal.GetValueDateTime() : DateTime.Now, Name = x.propertyId, FieldLabel = x.caption }); break;//datetime

                            case 5: propertiesForm.Items.Add(new Checkbox() { Checked = corrVal != null && !string.IsNullOrEmpty(corrVal.value) ? corrVal.GetValueBool() : false, FieldLabel = x.caption, Name = x.propertyId, InputValue = "true" }); break;//checkbox
                        }
                        }
                    });
                }
                catch(Exception exp)
                {
                    X.Msg.Alert("Error", exp.Message);
                }



            }
        }
        
        
        protected void SaveProperties(object sender, DirectEventArgs e)
        {
            string AssetId =currentAsset.Text;
            string catId = currentCat.Text;
            string[] values = e.ExtraParams["values"].Split(',');
            List<string> sentIds = new List<string>();
            for (int i = 0; i < values.Length; i++)
            {
                
                sentIds.Add(values[i].Split(':')[0].Replace("\"", "").Replace("{", ""));
                PostRequest<AssetPropertyValue> req = new PostRequest<AssetPropertyValue>();
                req.entity = new AssetPropertyValue() { categoryId = catId, assetId = AssetId, propertyId = values[i].Split(':')[0].Replace("\"", "").Replace("{", ""), value = values[i].Split(':')[1].Replace("\"", "").Replace("}", "") };
                PostResponse<AssetPropertyValue> resp = _assetManagementService.ChildAddOrUpdate<AssetPropertyValue>(req);
                if (!resp.Success)
                    Common.errorMessage(resp);
            }
            AssetManagementCategoryPropertyListRequest propReq = new AssetManagementCategoryPropertyListRequest();
            propReq.categoryId = catId;
            propReq.categoryId = catId;
            ListResponse<AssetManagementCategoryProperty> propResp = _assetManagementService.ChildGetAll<AssetManagementCategoryProperty>(propReq);
            if (!propResp.Success)
                Common.errorMessage(propResp);
            propResp.Items.ForEach(x =>
            {
                if (x.mask == 5 && !sentIds.Contains(x.propertyId))
                {
                    PostRequest<AssetPropertyValue> req = new PostRequest<AssetPropertyValue>();
                    req.entity = new AssetPropertyValue() { categoryId = catId, assetId = AssetId, propertyId = x.propertyId, value = "false" };
                    PostResponse<AssetPropertyValue> resp = _assetManagementService.ChildAddOrUpdate<AssetPropertyValue>(req);
                    if (!resp.Success)
                        Common.errorMessage(resp);
                }

            });

            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordSavingSucc
            });
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