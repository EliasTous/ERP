﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class NewSummaryPeriod : DevExpress.XtraReports.UI.XtraReport
    {
        public NewSummaryPeriod()
        {
            InitializeComponent();
        }
        public XRTable CreateXRTable()
        {
            int cellsInRow = 3;
            int rowsCount = 3;
            float rowHeight = 25f;

            XRTable table = new XRTable();
            table.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table.BeginInit();

            for (int i = 0; i < rowsCount; i++)
            {
                XRTableRow row = new XRTableRow();
                row.HeightF = rowHeight;
                for (int j = 0; j < cellsInRow; j++)
                {
                    XRTableCell cell = new XRTableCell();
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }

            table.BeforePrint += new PrintEventHandler(table_BeforePrint);
            table.AdjustSize();
            table.EndInit();
            return table;
        }

        // The following code makes the table span to the entire page width. 
        void table_BeforePrint(object sender, PrintEventArgs e)
        {
            XRTable table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
        }
        private void NewSummaryPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.Detail.Controls.Add(CreateXRTable());
        }
    }
}
