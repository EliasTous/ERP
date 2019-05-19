using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports.EmployeePayRoll
{
    public partial class EmployeePaySlipSub : DevExpress.XtraReports.UI.XtraReport
    {
        public EmployeePaySlipSub(EmployeePayrollDataSet.SalaryDetailsDataTable salaryDetails)
        {
            InitializeComponent();
            xrTableCell1.RightToLeft = RightToLeft.No;
            xrTableCell2.RightToLeft = RightToLeft.No;
            employeePayrollDataSet1.SalaryDetails.Merge(salaryDetails);
        }

    }
}
