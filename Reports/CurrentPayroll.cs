using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace Reports
{
    public partial class CurrentPayroll : DevExpress.XtraReports.UI.XtraReport
    {
        public bool RepeatEveryPage { get; set; }
        public CurrentPayroll()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount > 0;
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            //if (!string.IsNullOrEmpty((sender as XRLabel).Text))
            //{
            //    xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.All;
            //}

            //string notes = GetCurrentColumnValue("xrLabel7") != null ? GetCurrentColumnValue("xrLabel7").ToString() : string.Empty;
            //if (!string.IsNullOrWhiteSpace(notes))
            //{
            //    xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.All;
            //}
            e.Cancel = RowCount == 0;
            
        }

        private void DetailReport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount == 0;
        }

        private void DetailReport4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount == 0;
        }

        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // (sender as DetailBand).MultiColumn.ColumnCount =Convert.ToInt32( this.Parameters["columnCount"].Value); 
        }

        private void xrLabel7_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void xrLabel7_LocationChanged(object sender, ChangeEventArgs e)
        {
            
        }

        private void xrPageInfo1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //xrPageInfo1.Format
        }

        private void xrPageInfo3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        //private void xrPageInfo3_BeforePrint(object sender, DrawEventArgs e)
        //{
        //    xrPageInfo1.Format = Parameters["DateFormat"].Value.ToString();
        //}

        //private void xrLabel31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    XRLabel label = sender as XRLabel;
        //    CultureInfo culture = new CultureInfo("en-US");
        //    label.Text = String.Format(culture,Parameters["DateFormat"].Value.ToString(), DateTime.Now);

        //}

        //private void xrPageInfo3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        //{
        //    XRPageInfo label = sender as XRPageInfo;
        //    label.Format = "  { 0:MM / dd / yy}";
          
        //}

        //private void xrLabel29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    XRLabel label = sender as XRLabel;
        //    CultureInfo culture = new CultureInfo("ar-SA");
        //    label.Text = String.Format(culture, "{0:dd MM yyyy}", DateTime.Now);
        //}
    }
}
