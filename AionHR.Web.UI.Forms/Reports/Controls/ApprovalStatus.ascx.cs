using Model.AssetManagement;
using Model.Employees.Leaves;
using Model.Employees.Profile;
using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.Reports;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UI.Forms.Reports.Controls
{
   
    public partial class ApprovalStatus : System.Web.UI.UserControl
    {
        public string FieldLabel { get; set; }
        public string FieldType { get; set; }
        public string AllowBlank { get; set; }
        public string  ReadOnly { get; set; }

        public string InQueue { get; set; }


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

            if (!string.IsNullOrEmpty(InQueue) && InQueue.ToUpper() == "FALSE")
            {
                if (approvalsStatusList.Where(x => x.key == 3).Count() != 0)
                 approvalsStatusList.Remove(approvalsStatusList.Where(x => x.key == 3).ToList().First());
                 }
            apStatusStore.DataSource = approvalsStatusList;
            apStatusStore.DataBind();
          
        }


        public void disableApprovalStatus(bool disabled)
        {
            apStatus.Disabled = disabled;
        }
    }
}