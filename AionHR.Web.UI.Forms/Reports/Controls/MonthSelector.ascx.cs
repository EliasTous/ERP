using Services.Messaging.Reports;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UI.Forms.Reports.Controls
{
    public partial class MonthSelector : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !X.IsAjaxRequest)
                datefield1.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        public DateRangeParameterSet GetRange()
        {


            DateRangeParameterSet set = new DateRangeParameterSet();
            if (datefield1.SelectedDate != DateTime.MinValue)
            {
                set.DateFrom = datefield1.SelectedDate;
                set.DateTo = datefield1.SelectedDate.AddMonths(1).AddDays(-1);
            }
            else
            {
                int year, month;
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
                set.DateFrom = new DateTime(year, month, 1);
                set.DateTo = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }








            return set;
        }
    }
}