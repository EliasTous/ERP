using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class DateRangeFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dateFrom.SelectedDate = DateTime.Today;
                dateTo.SelectedDate = DateTime.Today;
            }
        }

        public DateRangeParameterSet GetRange()
        {
            DateRangeParameterSet set = new DateRangeParameterSet();
            if (dateFrom.SelectedDate != DateTime.MinValue)
                set.DateFrom = dateFrom.SelectedDate;
            else
                set.DateFrom = DateTime.Now;
            if (dateTo.SelectedDate != DateTime.MinValue)
                set.DateTo = dateTo.SelectedDate;
            else
                set.DateTo = DateTime.Now;
            return set;
        }
    }
}