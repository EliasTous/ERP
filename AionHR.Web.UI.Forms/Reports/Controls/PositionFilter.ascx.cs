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
    public partial class PositionFilter : System.Web.UI.UserControl,IComboControl
    {
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();

        public void Select(object id)
        {
            positionId.Select(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillPosition();
        }
        private void FillPosition()
        {
            
            PositionListRequest branchesRequest = new PositionListRequest();
            branchesRequest.StartAt = "0";
            branchesRequest.Size = "1000";
            branchesRequest.SortBy = "recordId";

            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            positionStore.DataSource = resp.Items;
            positionStore.DataBind();
        }
    }
}