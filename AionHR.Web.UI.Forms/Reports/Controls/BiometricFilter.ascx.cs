using AionHR.Model.Access_Control;
using AionHR.Model.Attributes;
using AionHR.Model.Company.Structure;
using AionHR.Model.Employees.Profile;
using AionHR.Model.TimeAttendance;
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