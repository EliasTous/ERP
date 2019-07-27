using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class SecurityGroupsReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SecurityGroupsReport()
        {
            InitializeComponent();
            xrLabel4.WidthF = (float)190;
            xrLabel2.LocationF = new PointF(190, 37);

        }

    }
}
