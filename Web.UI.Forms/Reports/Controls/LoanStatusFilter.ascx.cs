using Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UI.Forms.Reports.Controls
{
    public partial class LoanStatusFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                statusPref.Select("0");
        }

        public LoanRequestStatusParameterSet GetStatus()
        {
            LoanRequestStatusParameterSet s = new LoanRequestStatusParameterSet();
            int bulk;
            if (statusPref.Value == null || !int.TryParse(statusPref.Value.ToString(), out bulk))

                s.status = 0;
            else
                s.status = bulk;

            return s;
        }
    }
}