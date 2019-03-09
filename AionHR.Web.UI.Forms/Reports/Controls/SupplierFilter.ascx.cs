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
    public partial class SupplierFilter : System.Web.UI.UserControl
    {
        public bool? disabled { get; set; }
        public string width { get; set; }
        public string labelWidth { get; set; }
        public string FieldLabel { get; set; }
        public string Layout { get; set; }
        public string AllowBlank { get; set; }

        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                FillSupplier();

            if (disabled != null && disabled == true)
            {
                supplierId.Disable(true);
            }

            if (!string.IsNullOrEmpty(width))
            {
                supplierId.Width = Convert.ToInt32(width);
                //supplierIdPanel.Width = Convert.ToInt32(width);
            }
            if (!string.IsNullOrEmpty(labelWidth))
                supplierId.LabelWidth = Convert.ToInt32(labelWidth);
            if (!string.IsNullOrEmpty(FieldLabel))
                supplierId.FieldLabel = FieldLabel;
            if (!string.IsNullOrEmpty(AllowBlank))
                supplierId.AllowBlank = Convert.ToBoolean(AllowBlank);
            //if (!string.IsNullOrEmpty(Layout) && Layout == "Auto")
            //    supplierIdPanel.UpdateLayout(LayoutType.Auto);
            //if (!string.IsNullOrEmpty(Layout) && Layout == "Fit")
            //    supplierIdPanel.UpdateLayout(LayoutType.Fit);


        }

        private void FillSupplier()
        {
            ListRequest req = new ListRequest();
            ListResponse<AssetManagementSupplier> resp = _assetManagementService.ChildGetAll<AssetManagementSupplier>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
                return;
            }

            supplierIdStore.DataSource = resp.Items;
            supplierIdStore.DataBind();
        }

        public string GetSupplierId()
        {


            if (!string.IsNullOrEmpty(supplierId.Text) && supplierId.Value.ToString() != "0")
            {
                return supplierId.Value.ToString();



            }
            else
            {
                return "0";

            }
          
        }
        public void setSupplier(string Supplier)
        {
            if (!string.IsNullOrEmpty(Supplier))
            {
                supplierId.SetValue(Supplier);
                supplierId.Select(Supplier);
            }

        }
    }
}