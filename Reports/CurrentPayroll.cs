using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using AionHR.Model.Reports;

namespace Reports
{
    public partial class CurrentPayroll : DevExpress.XtraReports.UI.XtraReport
    {
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
            if (RowCount == 0)
            {
                e.Cancel = true;
                return;
            }
            CurrentPayrollSet at = (CurrentPayrollSet)GetCurrentRow();
            if (at != null)
            {

                Detail1.MultiColumn.ColumnCount = at.Names.Count +7;
                Detail2.MultiColumn.ColumnCount =at.Names.Count + 7;
                Detail4.MultiColumn.ColumnCount = at.Names.Count + 7;
                Detail5.MultiColumn.ColumnCount = at.Names.Count +7;

            }
        }
    }
}
