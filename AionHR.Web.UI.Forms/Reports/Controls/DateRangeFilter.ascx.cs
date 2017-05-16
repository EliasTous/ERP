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
                if (_DefaultStartDate == DateTime.MinValue)
                    dateFrom.SelectedDate = _systemService.SessionHelper.GetStartDate();
                if (_DefaultEndDate == DateTime.MinValue)
                    dateTo.SelectedDate = DateTime.Today;

                
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_DefaultStartDate == DateTime.MinValue)
                    dateFrom.SelectedDate = _systemService.SessionHelper.GetStartDate();
                if (_DefaultEndDate == DateTime.MinValue)
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

        private DateTime _DefaultStartDate;
        public DateTime DefaultStartDate
        {
            get
            {
                return _DefaultStartDate;
            }
            set
            {
                _DefaultStartDate = value;
                dateFrom.SelectedDate = _DefaultStartDate;
            }

        }
        private DateTime _DefaultEndDate;
        public DateTime DefaultEndDate
        {
            get
            {
                return _DefaultEndDate;
            }
            set
            {
                _DefaultEndDate = value;
                dateTo.SelectedDate = _DefaultEndDate;
            }
        }
    }
}