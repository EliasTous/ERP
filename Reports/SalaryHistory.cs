using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;

using System.Reflection;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Reports
{
    public partial class SalaryHistory : DevExpress.XtraReports.UI.XtraReport
    {
        public SalaryHistory(Dictionary<string, string> parameters)
        {
            InitializeComponent();
            xrLabel28.RightToLeft = RightToLeft.No;
            xrLabel25.RightToLeft = RightToLeft.No;
            xrLabel26.RightToLeft = RightToLeft.No;
            xrLabel27.RightToLeft = RightToLeft.No;

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



            this.PageHeader.Controls.Add(table);

        }
        private void table_BeforePrint(object sender, PrintEventArgs e)
        {
            XRTable table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
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
