using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Collections.Generic;

namespace Reports
{
    public partial class PunchLogDetails : DevExpress.XtraReports.UI.XtraReport
    {
        public PunchLogDetails(Dictionary<string, string> parameters, string getLan)
        {
            InitializeComponent();
            printHeader(parameters);

            if (getLan == "fr")
            {
                tableCell1.Text = "Reference";
                tableCell2.Text = "Employé";
                tableCell3.Text = "Date";
                tableCell4.Text = "UDID";
                tableCell5.Text = "Numero Serie";
                tableCell6.Text = "ID";
                label1.Text = "Journal de poinçon détaillé";

            }
            else if (getLan == "ar")
            {
                tableCell1.Text = "الرمز";
                tableCell2.Text = "موظف";
                tableCell3.Text = "تاريخ";
                tableCell4.Text = "UDID";
                tableCell5.Text = "التسلسل";
                tableCell6.Text = "هوية";
                label1.Text = "سجل البصمات مفصل";

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



    }
}
