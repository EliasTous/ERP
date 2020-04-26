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
    public partial class DivisionFilter : System.Web.UI.UserControl,IComboControl
    {
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        public ComboBox GetComboBox()
        {
            return divisionId;
        }

        public void Select(object id)
        {
            divisionId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            divisionId.FieldLabel = newLabel;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillDivision();
        }
        private void FillDivision()
        {

            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();


        }
    }
}