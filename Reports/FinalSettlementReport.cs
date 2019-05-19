using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class FinalSettlementReport : DevExpress.XtraReports.UI.XtraReport
    {
        public FinalSettlementReport()
        {
            InitializeComponent();
            xrTableCell4.RightToLeft = RightToLeft.No;
            xrLabel44.RightToLeft = RightToLeft.No;
            xrTableCell6.RightToLeft = RightToLeft.No;

            xrLabel46.RightToLeft = RightToLeft.No;
            xrLabel42.RightToLeft = RightToLeft.No;
        }

    }
}
