﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class TimeAttendanceViewReport : DevExpress.XtraReports.UI.XtraReport
    {
        public TimeAttendanceViewReport()
        {
            InitializeComponent();
        }

        private void tableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            ((XRTableCell)sender).Text = ((XRTableCell)sender).Text.Replace("\\n", Environment.NewLine);
        }
    }
}
