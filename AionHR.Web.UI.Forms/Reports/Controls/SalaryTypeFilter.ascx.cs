using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class SalaryTypeFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                salaryTypeId.Select(0);
        }

        public SalaryTypeParameterSet GetSalaryType()
        {
            SalaryTypeParameterSet t = new SalaryTypeParameterSet();
            t.SalaryTypeId = Convert.ToInt32(salaryTypeId.SelectedItem.Value);
            return t;
        }
        public string GetSalaryTypeString()

        {
            return salaryTypeId.SelectedItem.Text;
        }
    }
}