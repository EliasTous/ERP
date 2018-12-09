using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class ApproverPerformance : DevExpress.XtraReports.UI.XtraReport
    {
        public ApproverPerformance()
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

        private void xrLabel6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty((sender as XRLabel).Text))
                {

                    (sender as XRLabel).Text = Convert.ToDouble((sender as XRLabel).Text).ToString("N0");


                }
            }
            catch
            {

            }
        }
    }
}
