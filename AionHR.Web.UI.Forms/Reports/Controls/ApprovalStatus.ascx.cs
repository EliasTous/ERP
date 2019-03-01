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
   
    public partial class ApprovalStatus : System.Web.UI.UserControl
    {
        public string FieldLabel { get; set; }
        public string FieldType { get; set; }
        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(FieldLabel))
                apStatus.FieldLabel = FieldLabel;

            if (!string.IsNullOrEmpty(FieldType)&&FieldType=="Form")
                apStatus.RemoveByValue("0");
         
               

        }


        public string GetApprovalStatus()
        {
          

            if (!string.IsNullOrEmpty(apStatus.Value.ToString()))
            {
                return apStatus.Value.ToString();



            }
            else
            {
                return "0";

            }
          
        }
        public void setApprovalStatus(string status)
        {
            if(!string.IsNullOrEmpty(status))
            {
                apStatus.SetValue(status);
                apStatus.Select(status);
            }
          
        }
    }
}