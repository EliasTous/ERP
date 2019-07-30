﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Reports
{
    public partial class DetailedAttendance : DevExpress.XtraReports.UI.XtraReport
    {
        public DetailedAttendance(Dictionary<string, string> parameters, string getLang)
        {
               InitializeComponent();
            printHeader(parameters);

            if (getLang == "fr")
            {
                xrLabel1.Text = "Présence détaillée";
                xrLabel13.Text = "Nom :";
                xrTableCell9.Text = "Heures de travail";
                xrTableCell10.Text = "Heures prévues";
                xrTableCell29.Text = "Congé sans excuse";
                xrTableCell39.Text = "Enregistrement anticipé";
                xrTableCell41.Text = "Heures supplementaires";

                xrTable1.HeightF = 60;
            }

            

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

        private void xrTableCell18_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((sender as XRTableCell).Text.ToLower() == "false")
                (sender as XRTableCell).Text = " ";
        }

        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((sender as XRTableCell).Text.ToLower() == "false")
                (sender as XRTableCell).Text = " ";

        }
    }
}
