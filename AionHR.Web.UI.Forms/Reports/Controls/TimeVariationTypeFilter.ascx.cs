
using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class TimeVariationTypeFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                timeVariationType.Select(0);
        }

        public string GetTimeCode()
        {
            if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
                return "0";
            return timeVariationType.Value.ToString();
        }
        public string GetTimeCodeString()

        {
            if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
                return " ";
            return timeVariationType.SelectedItem.Text;
        }
    }
}