using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class DepartmentsReport : DevExpress.XtraReports.UI.XtraReport
    {
        public DepartmentsReport()
        {
            InitializeComponent();
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount > 0;
        }

        private void pageHeaderBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount == 0;
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((bool)GetCurrentColumnValue("isInactive"))
                (sender as XRLabel).Text = Parameters["Yes"].Value.ToString();
            else
                (sender as XRLabel).Text = Parameters["No"].Value.ToString();
        }
    }
}
