using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class PayRefFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       public PayRefParameterSet GetPayRef()
        {
            PayRefParameterSet p = new PayRefParameterSet();


            if (!string.IsNullOrEmpty(payRef.Text) && payRef.Value.ToString() != "0")
            {
                p.payRef = payRef.Value.ToString(); ;



            }
            else
            {
                p.payRef = "0";

            }
            return p;
        }
        public string GetStringPayRef()
        {
            return payRef.Text;
        }
        public void SetAllowBlank(bool value)
        {
            payRef.AllowBlank = value;
        }
    }
}