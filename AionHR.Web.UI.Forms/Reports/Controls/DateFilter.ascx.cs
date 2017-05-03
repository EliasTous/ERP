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
            date.SelectedDate = DateTime.Now;
        }

        public DateParameterSet GetDate()
        {
            DateParameterSet d = new DateParameterSet();
            d.Date = date.SelectedDate;
            return d;
        }

        public DateRangeParameterSet GetAsRange()
        {
            DateRangeParameterSet set = new DateRangeParameterSet();
            if (date.SelectedDate != DateTime.MinValue)
            {
                set.DateFrom = date.SelectedDate;
                set.DateTo = date.SelectedDate;
            }
            else
            {
                set.DateFrom = DateTime.Now;
                set.DateTo = DateTime.Now;
            }
           
            return set;
        }
    }
}