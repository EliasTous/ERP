using AionHR.Model.AssetManagement;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Asset_Management;
using AionHR.Services.Messaging.Reports;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
   
    public partial class AssetFilter : System.Web.UI.UserControl
    {
        public bool? disabled { get; set; }
        public string width { get; set; }
        public string AllowBlank { get; set; }
        public string labelWidth { get; set; }
        public string FieldLabel { get; set; }
        public string Layout { get; set; }
        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                FillAssets();

            if (disabled != null && disabled == true)
            {
                assetId.Disable(true);
            }

            if (!string.IsNullOrEmpty(width))
            {
                assetId.Width = Convert.ToInt32(width);
               // categoryIdPanel.Width = Convert.ToInt32(width);
            }
            if (!string.IsNullOrEmpty(labelWidth))
                assetId.LabelWidth = Convert.ToInt32(labelWidth);
            if (!string.IsNullOrEmpty(FieldLabel))
                assetId.FieldLabel = FieldLabel;
            if (!string.IsNullOrEmpty(AllowBlank))
                assetId.AllowBlank = Convert.ToBoolean(AllowBlank) ;
            //if (!string.IsNullOrEmpty(Layout) && Layout == "Auto")
            //    categoryIdPanel.UpdateLayout(LayoutType.Auto);
            //if (!string.IsNullOrEmpty(Layout) && Layout == "Fit")
            //    categoryIdPanel.UpdateLayout(LayoutType.Fit);


        }

        private void FillAssets()
        {
            AssetManagementAssetListRequest request1 = new AssetManagementAssetListRequest();

            request1.branchId = "0";
            request1.departmentId = "0";
            request1.positionId = "0";
            request1.categoryId = "0";
            request1.employeeId = "0";
            request1.supplierId = "0";
            request1.PurchaseOrderId = "0";
            request1.Filter = "";
            ListResponse<AssetManagementAsset> resp = _assetManagementService.ChildGetAll<AssetManagementAsset>(request1);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
                return;
            }

            assetIdStore.DataSource = resp.Items;
            assetIdStore.DataBind();
        }

        public string GetAssetId()
        {
          

            if (!string.IsNullOrEmpty(assetId.Value.ToString()))
            {
                return assetId.Value.ToString();



            }
            else
            {
                return "0";

            }
          
        }
        public void setAsset(string asset)
        {
            if(!string.IsNullOrEmpty(asset))
            {
                assetId.SetValue(asset);
                assetId.Select(asset);
            }
          
        }
        public void ADDHandler(string Event, string Function)
        {

            this.assetId.AddListener(Event, "function() {" + Function + "}");
        }
    }
}