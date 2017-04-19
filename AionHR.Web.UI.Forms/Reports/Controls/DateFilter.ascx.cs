using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class DateFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DateParameterSet GetDate()
        {
            DateParameterSet d = new DateParameterSet();
            d.Date = date.SelectedDate;
            return d;
        }
    }
}