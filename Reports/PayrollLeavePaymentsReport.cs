﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Reports
{
    public partial class PayrollLeavePaymentsReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PayrollLeavePaymentsReport(Dictionary<string, string> parameters, string _getLan)
        {
            InitializeComponent();
            xrLabel21.RightToLeft = RightToLeft.No;
            xrLabel22.RightToLeft = RightToLeft.No;
            xrLabel19.RightToLeft = RightToLeft.No;
            xrLabel3.HeightF = (float)50.71;
            xrLabel2.HeightF = (float)50.71;
            xrLabel4.HeightF = (float)50.71;
            xrLabel5.HeightF = (float)50.71;
            xrLabel6.HeightF = (float)50.71;
            xrLabel7.HeightF = (float)50.71;
            xrLabel8.HeightF = (float)50.71;
            xrLabel9.HeightF = (float)50.71;
            xrLabel10.HeightF = (float)50.71;
            xrLabel11.HeightF = (float)50.71;
            xrLabel23.HeightF = (float)50.71;

            if (_getLan == "fr")
            { xrLabel1.Text = "Paiement Congés"; }

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

        private void groupHeaderBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount == 0;
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount > 0;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }
    }
}
