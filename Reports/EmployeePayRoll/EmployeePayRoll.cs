using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
using AionHR.Model.Reports;
using Tools;

namespace Reports.EmployeePayRoll
{
    public partial class EmployeePayRoll : DevExpress.XtraReports.UI.XtraReport
    {
        public EmployeePayRoll(List<RT501> details, bool isArabic)
        {
            InitializeComponent();

            if (details.Count == 0)
            { return; }

            employeePayrollDataSet1.ReportHeader.AddReportHeaderRow(details.FirstOrDefault().startDate, details.FirstOrDefault().endDate);

            foreach (var employee in details.GroupBy(g => g.employeeName.reference))
            {
                var benifits = employee.Where(u => u.edType == 1).ToList();

                var deductions = employee.Where(u => u.edType == 2).ToList();

                var parentRow = employeePayrollDataSet1.Employee.AddEmployeeRow(
                    employee.Key,
                    employee.FirstOrDefault().employeeName.fullName,
                    employee.Key,
                   Convert.ToDecimal(employee.FirstOrDefault().basicAmount + benifits.Sum(u => u.edAmount) - deductions.Sum(u => u.edAmount)),
                    employee.FirstOrDefault().workingDays,
                   Convert.ToDecimal(benifits.Sum(u => u.edAmount)),
                    Convert.ToDecimal(deductions.Sum(u => u.edAmount)),
                    NumberToWords.multiLingualNumberInText(Convert.ToDecimal(employee.FirstOrDefault().basicAmount + benifits.Sum(u => u.edAmount) - deductions.Sum(u => u.edAmount)), 2, isArabic));

                employeePayrollDataSet1.SalaryDetails.AddSalaryDetailsRow(parentRow, isArabic ? "الراتب الاساسي" : "Basic Salary", 1, Convert.ToDecimal(employee.FirstOrDefault().basicAmount));

                benifits.ForEach(u => employeePayrollDataSet1.SalaryDetails.AddSalaryDetailsRow(parentRow, u.edName, 1, Convert.ToDecimal(u.edAmount)));

                deductions.ForEach(u => employeePayrollDataSet1.SalaryDetails.AddSalaryDetailsRow(parentRow, u.edName, 2, Convert.ToDecimal(u.edAmount)));
            }

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var employeeNumber = GetCurrentColumnValue<string>("reference");

            EmployeePayrollDataSet.SalaryDetailsDataTable tbl = new EmployeePayrollDataSet.SalaryDetailsDataTable();

            var benifits = employeePayrollDataSet1.SalaryDetails
                .Where(u => u.reference == employeeNumber && u.edType == 1).Select(x => x).ToList();

            benifits.ForEach(r => tbl.ImportRow(r));

            xrSubBenifits.ReportSource = new EmployeePayrollSub(tbl);
            xrSubBenifits.ReportSource.RightToLeftLayout = this.RightToLeftLayout;
            xrSubBenifits.ReportSource.RightToLeft = this.RightToLeft;

            EmployeePayrollDataSet.SalaryDetailsDataTable tbl2 = new EmployeePayrollDataSet.SalaryDetailsDataTable();

            var deductions = employeePayrollDataSet1.SalaryDetails
                .Where(u => u.reference == employeeNumber && u.edType == 2).Select(x => x).ToList();

            deductions.ForEach(r => tbl2.ImportRow(r));

            xrSubDeductions.ReportSource = new EmployeePayrollSub(tbl2);
            xrSubDeductions.ReportSource.RightToLeftLayout = this.RightToLeftLayout;
            xrSubDeductions.ReportSource.RightToLeft = this.RightToLeft;
        }
    }
}
