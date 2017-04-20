using AionHR.Services.Messaging.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class TransactionTypeFilter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            trxType.Select(0);
        }

        public TransactionTypeParameterSet GetTransactionType()
        {
            TransactionTypeParameterSet s = new TransactionTypeParameterSet();
            int bulk;
            if (!int.TryParse(trxType.Value.ToString(), out bulk))
                s.TransactionType = 1;
            else
                s.TransactionType = bulk;

            return s;
        }
    }
}