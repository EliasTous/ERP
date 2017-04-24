using AionHR.Services.Interfaces;
using AionHR.Services.Messaging.Reports;
using Microsoft.Practices.ServiceLocation;
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
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dateFrom.SelectedDate = _systemService.SessionHelper.GetStartDate();
                dateTo.SelectedDate = DateTime.Today;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dateFrom.SelectedDate = _systemService.SessionHelper.GetStartDate();
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

        public void Clear()
        {
            dateFrom.SelectedDate = DateTime.Today;
            dateTo.SelectedDate = DateTime.Today;
        }
    }
}