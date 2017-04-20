using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class ActiveStatusFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            inactivePref.Select("0");
        }

        public ActiveStatusParameterSet GetActiveStatus()
        {
            ActiveStatusParameterSet s = new ActiveStatusParameterSet();
            int bulk;
            if (!int.TryParse(inactivePref.Value.ToString(), out bulk))
                s.active = 1;
            else
                s.active = bulk;

            return s;
        }
    }
}