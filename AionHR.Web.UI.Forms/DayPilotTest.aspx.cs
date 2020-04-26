using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UI.Forms
{
    public partial class DayPilotTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DayPilotScheduler1.StartDate = new DateTime(2017, 7, 1);
            DayPilotScheduler1.Days = 30;
        }

        protected void drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            DayPilotScheduler1.StartDate = new DateTime(2017,Convert.ToInt32(drop.SelectedValue), 1);
            DayPilotScheduler1.Days = DateTime.DaysInMonth(2017, Convert.ToInt32(drop.SelectedValue));
        }
    }
}