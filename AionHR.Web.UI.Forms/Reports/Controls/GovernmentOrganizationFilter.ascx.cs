using Model.Company.Structure;
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
    public partial class GovernmentOrganizationFilter : System.Web.UI.UserControl, IComboControl
    {
        ISystemService systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillGOs();
        }
        private void FillGOs()
        {
            ListRequest goRequest = new ListRequest();
            ListResponse<GovernmentOrganisation> resp = systemService.ChildGetAll<GovernmentOrganisation>(goRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
            }
            if (resp.Items != null)
            {
                goStore.DataSource = resp.Items;
                goStore.DataBind();
            }
        }
      

        public void Select(object id)
        {
            goId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            goId.FieldLabel = newLabel;
        }

        public ComboBox GetComboBox()
        {
            return goId;
        }
    }
}