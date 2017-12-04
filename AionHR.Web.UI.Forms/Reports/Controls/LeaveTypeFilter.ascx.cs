using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
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
    public partial class LeaveTypeFilter : System.Web.UI.UserControl
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
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
    }
}