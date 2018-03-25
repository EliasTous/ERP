using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class EmployeesNotes : DevExpress.XtraReports.UI.XtraReport
    {
        public EmployeesNotes()
        {
            InitializeComponent();
        }

        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (string.IsNullOrEmpty((sender as XRLabel).Text))
                (sender as XRTableCell).Borders = DevExpress.XtraPrinting.BorderSide.Left;
            else
                (sender as XRTableCell).Borders = DevExpress.XtraPrinting.BorderSide.All;
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = RowCount > 0;
        }
    }
}
