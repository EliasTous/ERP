using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class DetailedAttendance : DevExpress.XtraReports.UI.XtraReport
    {
        public DetailedAttendance()
        {
            InitializeComponent();
        }

        private void xrTableCell18_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((sender as XRTableCell).Text.ToLower() == "false")
                (sender as XRTableCell).Text = " ";
        }

        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((sender as XRTableCell).Text.ToLower() == "false")
                (sender as XRTableCell).Text = " ";

        }
    }
}
