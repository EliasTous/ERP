using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class MoudleFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            moduleId.Select(0);
        }

        public ClassIdParameterSet GetModule()
        {
            ClassIdParameterSet s = new ClassIdParameterSet();
            int bulk;
            if (moduleId.Value == null || !int.TryParse(moduleId.Value.ToString(), out bulk))
                s.ClassId = 20;
            else
                s.ClassId = bulk;

            return s;
        }
    }
}