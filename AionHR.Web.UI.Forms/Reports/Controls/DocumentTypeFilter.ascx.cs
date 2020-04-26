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
    public partial class DocumentTypeFilter : System.Web.UI.UserControl, IComboControl
    {
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        public ComboBox GetComboBox()
        {
            return dtId;
        }

        public void Select(object id)
        {
            dtId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            dtId.FieldLabel = newLabel;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillDTs();
        }
        private void FillDTs()
        {

            ListRequest dtReq = new ListRequest();
            ListResponse<DocumentType> resp = _employeeService.ChildGetAll<DocumentType>(dtReq);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            dtStore.DataSource = resp.Items;
            dtStore.DataBind();


        }
    }
}