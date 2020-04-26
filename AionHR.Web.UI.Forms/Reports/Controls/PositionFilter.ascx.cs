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
    public partial class PositionFilter : System.Web.UI.UserControl,IComboControl
    {
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();

        public ComboBox GetComboBox()
        {
            return positionId;
        }

        public void Select(object id)
        {
            positionId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            positionId.FieldLabel = newLabel;
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