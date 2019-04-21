using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Drawing.Printing;
using System.Collections.Generic;

namespace Reports
{
    public partial class CurrentPayroll : DevExpress.XtraReports.UI.XtraReport
    {
        public bool RepeatEveryPage { get; set; }
        public CurrentPayroll(Dictionary<string, string> parameters)
        {
            InitializeComponent();
            printHeader(parameters);

        }
        private void printHeader(Dictionary<string, string> parameters)
        {
            if (parameters.Count == 0)
                return;


            XRTable table = new XRTable();
            table.BeginInit();


            table.LocationF = new PointF(0, 0);
            int count = 0;
            XRTableRow row = new XRTableRow();

            foreach (KeyValuePair<string, string> item in parameters)
            {

                XRTableCell cell = new XRTableCell();

                cell.Text = item.Key;

                cell.BackColor = Color.Gray;
                cell.ForeColor = Color.White;

                XRTableCell valueCell = new XRTableCell();

                valueCell.Text = item.Value;

                row.Cells.Add(cell);
                row.Cells.Add(valueCell);

                count++;
                if (count % 4 == 0)
                {
                    table.Rows.Add(row);
                    row = new XRTableRow();
                }





            }
            if (count % 4 != 0)
            {
                for (int i = 0; i < (4 - (count % 4)) * 2; i++)
                {
                    XRTableCell cell = new XRTableCell();



                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }
            table.BeforePrint += new PrintEventHandler(table_BeforePrint);
            table.AdjustSize();
            table.EndInit();



            this.pageHeaderBand1.Controls.Add(table);

        }
        private void table_BeforePrint(object sender, PrintEventArgs e)
        {
            XRTable table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
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
