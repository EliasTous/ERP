using Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UI.Forms.Reports.Controls
{
    public partial class WeekPicker : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       public DateRangeParameterSet GetWeek()

        {
            DateRangeParameterSet s = new DateRangeParameterSet();

            return s;
        }
    }
}