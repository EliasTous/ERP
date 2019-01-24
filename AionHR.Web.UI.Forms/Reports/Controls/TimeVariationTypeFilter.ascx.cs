
using AionHR.Model.Payroll;
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
    public partial class TimeVariationTypeFilter : System.Web.UI.UserControl
    {
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FilltimeCodeStore();
               
            }
           
              
        }

        public string GetTimeCode()
        {
            if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
                return "0";
            return timeVariationType.Value.ToString();
        }
        public string GetTimeCodeString()

        {
            if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
                return " ";
            return timeVariationType.SelectedItem.Text;
        }
        private void FilltimeCodeStore()
        {
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<TimeCode> response = _payrollService.ChildGetAll<TimeCode>(request);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return;

            }
            timeVariationStore.DataSource = response.Items;
            timeVariationStore.DataBind();

        }
    }
}