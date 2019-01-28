using AionHR.Model.Employees.Profile;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class EmploymentStatusFilter : System.Web.UI.UserControl
    {
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FillStatus();
                esId.Select("0");
            }
        }

        public EmploymentStatusParameterSet GetStatus()
        {
            EmploymentStatusParameterSet s = new EmploymentStatusParameterSet();
            int bulk;
            if (esId.Value == null || !int.TryParse(esId.Value.ToString(), out bulk))

                s.esId = 0;
            else
                s.esId = bulk;

            return s;
        }


        private void FillStatus()
        {
            ListRequest statusReq = new ListRequest();
            ListResponse<EmploymentStatus> resp = _employeeService.ChildGetAll<EmploymentStatus>(statusReq);
            statusStore.DataSource = resp.Items;
            statusStore.DataBind();
        }
    }
}