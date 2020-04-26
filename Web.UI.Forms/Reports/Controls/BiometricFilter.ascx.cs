using Model.Access_Control;
using Model.Attributes;
using Model.Company.Structure;
using Model.Employees.Profile;
using Model.TimeAttendance;
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
    public partial class BiometricFilter : System.Web.UI.UserControl,IComboControl
    {
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();

        public ComboBox GetComboBox()
        {
            return branchId;
        }

        public void Select(object id)
        {
            branchId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            branchId.FieldLabel = newLabel;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillBranch();
        }
        private void FillBranch()
        {

            ListRequest branchesRequest = new ListRequest();
            ListResponse<BiometricDevice> resp = _timeAttendanceService.ChildGetAll<BiometricDevice>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            if (resp.Items != null)
            {
                branchStore.DataSource = resp.Items;
                branchStore.DataBind();
            }

        }
    }
}