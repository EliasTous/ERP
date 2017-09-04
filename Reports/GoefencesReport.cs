using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class GoefencesReport : DevExpress.XtraReports.UI.XtraReport
    {
        public GoefencesReport()
        {
            InitializeComponent();
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int _shape = Convert.ToInt16(GetCurrentColumnValue("shape"));
            if (_shape == 0)
            {
                xrLabel5.Text = "Rectangle";
            }
            else
            {
                xrLabel5.Text = "Circle";
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount > 0;
        }
    }
}
