using Model.Access_Control;
using Model.Attributes;
using Model.Company.Structure;
using Model.Employees.Profile;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.CompanyStructure;
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