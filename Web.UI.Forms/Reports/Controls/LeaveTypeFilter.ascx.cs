using Model.Employees.Leaves;
using Model.Employees.Profile;
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
    public partial class LeaveTypeFilter : System.Web.UI.UserControl, IComboControl
    {
        ILeaveManagementService _employeeService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillLT();
        }

        private void FillLT()
        {
            ListRequest req = new ListRequest();
            ListResponse<LeaveType> resp = _employeeService.ChildGetAll<LeaveType>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
                return;
            }

            ltStore.DataSource = resp.Items;
            ltStore.DataBind();
        }

        public LeaveTypeParameterSet GetLeaveType()
        {
            LeaveTypeParameterSet p = new LeaveTypeParameterSet();

            if (!string.IsNullOrEmpty(ltId.Text) && ltId.Value.ToString() != "0")
            {
                p.LeaveTypeId = Convert.ToInt32(ltId.Value);



            }
            else
            {
                p.LeaveTypeId = 0;

            }
            return p;
        }

        public void Select(object id)
        {
            ltId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            ltId.FieldLabel = newLabel;
        }

        public ComboBox GetComboBox()
        {
            return ltId;
        }
    }
}