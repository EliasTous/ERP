using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class LeavePaymentsReport : DevExpress.XtraReports.UI.XtraReport
    {
        public LeavePaymentsReport()
        {
            InitializeComponent();
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }
    }
}
