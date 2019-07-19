using AionHR.Model.AssetManagement;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
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
        public string AllowBlank { get; set; }
        public string  ReadOnly { get; set; }

        public string EmptyText { get; set; }


        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            fillApprovalStatus();
            if (!string.IsNullOrEmpty(FieldLabel))
                apStatus.FieldLabel = FieldLabel;
           

            if (!string.IsNullOrEmpty(AllowBlank))
                apStatus.AllowBlank = Convert.ToBoolean(AllowBlank);
            if (!string.IsNullOrEmpty(ReadOnly))
                apStatus.ReadOnly = Convert.ToBoolean(ReadOnly);

            if (!string.IsNullOrEmpty(EmptyText))
                apStatus.EmptyText = EmptyText;


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


        private void fillApprovalStatus()
        {
            List< XMLDictionary> approvalsStatusList  = Common.XMLDictionaryList(_systemService, "13");
            if (!string.IsNullOrEmpty(FieldType) && FieldType == "Filter")
            {

                approvalsStatusList.Add(new XMLDictionary { key = 0, value = Resources.Common.All });
            }
            apStatusStore.DataSource = approvalsStatusList;
            apStatusStore.DataBind();
          
        }
    }
}