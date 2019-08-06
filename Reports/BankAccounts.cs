using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Collections.Generic;

namespace Reports
{
    public partial class BankAccounts : DevExpress.XtraReports.UI.XtraReport
    {
        public BankAccounts(Dictionary<string, string> parameters)
        {
            InitializeComponent();
            //xrTableCell13.RightToLeft = RightToLeft.No;
           // xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;


            printHeader(parameters);
            xrTable1.HeightF = 40;
            xrTableCell25.WidthF = (float)54.24;
            xrTableCell26.WidthF = (float)54.24;

            xrTableCell7.WidthF = (float)110.45;
            xrTableCell8.WidthF = (float)110.45;

            xrTableCell9.WidthF = (float)89.38;
            xrTableCell10.WidthF = (float)89.38;

            xrTableCell11.WidthF = (float)107.08;
            xrTableCell12.WidthF = (float)107.08;

            xrTableCell15.WidthF = (float)62.05;
            xrTableCell16.WidthF = (float)62.05;

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



        this.PageHeader.Controls.Add(table);

    }
    private void table_BeforePrint(object sender, PrintEventArgs e)
    {
        XRTable table = ((XRTable)sender);
        table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
        table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
    }

    private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount == 0;
        }

        private void xrTableCell18_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }
    }
}
