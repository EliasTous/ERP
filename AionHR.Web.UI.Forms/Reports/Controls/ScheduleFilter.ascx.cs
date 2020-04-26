using Model.Attendance;
using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.Reports;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UI.Forms.Reports.Controls
{
    public partial class ScheduleFilter : System.Web.UI.UserControl,IComboControl
    {
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                FillCurrency();

        }
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        private void FillCurrency()
        {
            try
            {
                ListRequest request = new ListRequest();
                ListResponse<AttendanceSchedule> resp = _timeAttendanceService.ChildGetAll<AttendanceSchedule>(request);
                if (!resp.Success)

                    Common.errorMessage(resp);
                scheduleStore.DataSource = resp.Items;
                scheduleStore.DataBind();
            }
            catch (Exception exp)
            {
                Common.errorMessage(new ListResponse<Currency>());
            }
        }


        public string getSchedule()
        {


            if (!string.IsNullOrEmpty(scheduleId.Text) && scheduleId.Value.ToString() != "0")
            {
                return scheduleId.Value.ToString();



            }
            else
            {
                return "0";

            }

        }

        public void setSchedule(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                scheduleId.SetValue(id);
                scheduleId.Select(id);

            }

        }

        public string GetValue()
        {
            return "scheduleId=" + getSchedule();
        }

        public void Select(object id)
        {
            scheduleId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            scheduleId.FieldLabel = newLabel;
        }

        public ComboBox GetComboBox()
        {
            return scheduleId;
        }
    }
}