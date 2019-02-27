using AionHR.Model.AssetManagement;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
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
   
    public partial class AssetCategoryFilter : System.Web.UI.UserControl
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

                FillCategories();

            if (disabled != null && disabled == true)
            {
                categoryId.Disable(true);
            }

            if (!string.IsNullOrEmpty(width))
            {
                categoryId.Width = Convert.ToInt32(width);
               // categoryIdPanel.Width = Convert.ToInt32(width);
            }
            if (!string.IsNullOrEmpty(labelWidth))
                categoryId.LabelWidth = Convert.ToInt32(labelWidth);
            if (!string.IsNullOrEmpty(FieldLabel))
                categoryId.FieldLabel = FieldLabel;
            if (!string.IsNullOrEmpty(AllowBlank))
                categoryId.AllowBlank = Convert.ToBoolean(AllowBlank) ;
            //if (!string.IsNullOrEmpty(Layout) && Layout == "Auto")
            //    categoryIdPanel.UpdateLayout(LayoutType.Auto);
            //if (!string.IsNullOrEmpty(Layout) && Layout == "Fit")
            //    categoryIdPanel.UpdateLayout(LayoutType.Fit);


        }

        private void FillCategories()
        {
            ListRequest req = new ListRequest();
            ListResponse<AssetManagementCategory> resp = _assetManagementService.ChildGetAll<AssetManagementCategory>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
                return;
            }

            categoryIdStore.DataSource = resp.Items;
            categoryIdStore.DataBind();
        }

        public string GetCategoryId()
        {
          

            if (!string.IsNullOrEmpty(categoryId.Text) && categoryId.Value.ToString() != "0")
            {
                return categoryId.Value.ToString();



            }
            else
            {
                return "0";

            }
          
        }
        public void setCategory(string category)
        {
            if(!string.IsNullOrEmpty(category))
            {
                categoryId.SetValue(category);
                categoryId.Select(category);
            }
          
        }
        public void ADDHandler(string Event, string Function)
        {

            this.categoryId.AddListener(Event, "function() {" + Function + "}");
        }
    }
}