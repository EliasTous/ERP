using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;

using System.Reflection;

namespace Reports
{
    public partial class SalaryHistory : DevExpress.XtraReports.UI.XtraReport
    {
        public SalaryHistory()
        {
            InitializeComponent();
            
           

        }

        private void SalaryHistory_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = this.RowCount > 0;
        }

        private void groupHeaderBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = this.RowCount == 0;
        }

        private void xrLabel28_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty((sender as XRLabel).Text))
                {

                    (sender as XRLabel).Text = Convert.ToDouble((sender as XRLabel).Text).ToString("N2");


                }
            }
            catch
            {

            }
        }
    }
}
