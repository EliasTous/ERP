using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class PayRefFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       public PayRefParameterSet GetPayRef()
        {
            PayRefParameterSet s = new PayRefParameterSet();
            s.payRef = payRef.Text;
            return s;
        }
    }
}