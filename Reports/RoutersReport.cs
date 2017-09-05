using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class RoutersReport : DevExpress.XtraReports.UI.XtraReport
    {
        public RoutersReport()
        {
            InitializeComponent();
        }

        private void reportHeaderBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //e.Cancel = RowCount > 0;
        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string result = string.Empty;
            string currentColumnValue = ((XRLabel)sender).Text.ToLower();
            if (currentColumnValue != null)
            {
                switch (currentColumnValue)
                {
                    case "false": result = "No"; break;
                    case "true": result = "Yes"; break;
                    default: result = string.Empty; break;
                }
            }
            if (!string.IsNullOrEmpty(result))
                ((XRLabel)sender).Text = result;
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount > 0;
        }
    }
}
