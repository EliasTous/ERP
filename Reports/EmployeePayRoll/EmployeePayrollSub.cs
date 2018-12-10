using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports.EmployeePayRoll
{
    public partial class EmployeePayrollSub : DevExpress.XtraReports.UI.XtraReport
    {
        public EmployeePayrollSub(EmployeePayrollDataSet.SalaryDetailsDataTable salaryDetails)
        {
            InitializeComponent();
            employeePayrollDataSet1.SalaryDetails.Merge(salaryDetails);
        }

    }
}
