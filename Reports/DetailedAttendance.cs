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
            if ((sender as XRTableCell).Text == "False")
                (sender as XRTableCell).Text = " ";
        }
    }
}
