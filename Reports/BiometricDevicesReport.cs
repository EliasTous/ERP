using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;


namespace Reports
{
    public partial class BiometricDevicesReport : DevExpress.XtraReports.UI.XtraReport
    {
        public BiometricDevicesReport()
        {
            InitializeComponent();
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount > 0;
        }

        //private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    string bah = "FFFF00";            
        //    (sender as XRLabel).BackColor = System.Drawing.Color.FromArgb(int.Parse(bah.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
        //    int.Parse(bah.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
        //    int.Parse(bah.Substring(4, 2), System.Globalization.NumberStyles.HexNumber));
        //}
    }
}
