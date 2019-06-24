using AionHR.Model.Access_Control;
using AionHR.Model.Attributes;
using AionHR.Model.Company.Structure;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.CompanyStructure;
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
    public partial class SecurityGroupsFilter : System.Web.UI.UserControl, IComboControl
    {
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();

        public ComboBox GetComboBox()
        {
            return groupId;
        }

        public void Select(object id)
        {
            groupId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            groupId.FieldLabel = newLabel;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillGroups();
        }

        private void FillGroups()
        {

            ListRequest groupsRequest = new ListRequest();
            groupsRequest.StartAt = "0";
            groupsRequest.Size = "1000";
            

            ListResponse<SecurityGroup> resp = _accessControlService.ChildGetAll<SecurityGroup>(groupsRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            groupsStore.DataSource = resp.Items;
            groupsStore.DataBind();
        }
    }
}