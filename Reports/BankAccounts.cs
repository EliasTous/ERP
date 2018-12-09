using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class BankAccounts : DevExpress.XtraReports.UI.XtraReport
    {
        public BankAccounts()
        {
            InitializeComponent();
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount > 0; 
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount == 0;
        }

        private void xrTableCell18_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty((sender as XRTableCell).Text))
                {
                 
                    (sender as XRTableCell).Text = Convert.ToDouble((sender as XRTableCell).Text).ToString("N0");
                   
                   
                }
            }
            catch
            {
             
            }
        }
    }
}
