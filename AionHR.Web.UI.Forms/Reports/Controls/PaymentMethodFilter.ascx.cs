using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class PaymentMethodFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                paymentMethod.Select(0);
        }

        public PaymentMethodParameterSet GetPaymentMethod()
        {
            PaymentMethodParameterSet p = new PaymentMethodParameterSet();

            if (!string.IsNullOrEmpty(paymentMethod.Text) && paymentMethod.Value.ToString() != "0")
            {
                p.paymentMethod = Convert.ToInt32(paymentMethod.Value);



            }
            else
            {
                p.paymentMethod = 0;

            }
            return p;
        }

        public string GetPaymentMethodString()
        {
            if (paymentMethod.SelectedItem != null)
                return paymentMethod.SelectedItem.Text;
            else return "";
        }
    }
}