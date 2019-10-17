using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
using AionHR.Model.Reports;
using Tools;
using System.Drawing.Printing;

namespace Reports.EmployeePayRoll
{
    public partial class EmployeesPaySlip : DevExpress.XtraReports.UI.XtraReport
    {
        public EmployeesPaySlip(List<RT501> details, bool isArabic, Dictionary<string, string> parameters)
        {
            InitializeComponent();
            xrTableCell8.RightToLeft = RightToLeft.No;
            xrTableCell6.RightToLeft = RightToLeft.No; 
            printHeader(parameters);
            if (details.Count == 0)
            { return; }

            employeePayrollDataSet1.ReportHeader.AddReportHeaderRow(details.FirstOrDefault().startDate, details.FirstOrDefault().endDate);

            foreach (var employee in details.GroupBy(g => g.employeeName))
            {
                var benifits = employee.Where(u => u.edType == 1).ToList();

                var deductions = employee.Where(u => u.edType == 2).ToList();

                var parentRow = employeePayrollDataSet1.Employee.AddEmployeeRow(
                    employee.Key,
                    employee.FirstOrDefault().employeeName,
                    employee.FirstOrDefault().idRef, employee.FirstOrDefault().netSalary
                 /*  Convert.ToDecimal(employee.FirstOrDefault().basicAmount + benifits.Sum(u => u.edAmount) - deductions.Sum(u => u.edAmount))*/,
                    employee.FirstOrDefault().workingDays,
                   Convert.ToDecimal(benifits.Sum(u => u.edAmount)+employee.FirstOrDefault().basicAmount),
                    Convert.ToDecimal(deductions.Sum(u => u.edAmount)+employee.FirstOrDefault().essAmount),
                    NumberToWords.multiLingualNumberInText(Convert.ToDecimal(employee.FirstOrDefault().netSalary), (short)employee.FirstOrDefault().currencyProfileId, isArabic),
                    employee.FirstOrDefault().departmentName,
                    employee.FirstOrDefault().positionName
                    );

                employeePayrollDataSet1.SalaryDetails.AddSalaryDetailsRow(parentRow, isArabic ? "الراتب الاساسي" : "Basic Salary", 1, Convert.ToDecimal(employee.FirstOrDefault().basicAmount));
                employeePayrollDataSet1.SalaryDetails.AddSalaryDetailsRow(parentRow, isArabic ? "التأمينات الاجتماعية" : "Social Security", 2, Convert.ToDecimal(employee.FirstOrDefault().essAmount));
                
                benifits.ForEach(u => employeePayrollDataSet1.SalaryDetails.AddSalaryDetailsRow(parentRow, u.edName, 1, Convert.ToDecimal(u.edAmount)));

                deductions.ForEach(u => employeePayrollDataSet1.SalaryDetails.AddSalaryDetailsRow(parentRow, u.edName, 2, Convert.ToDecimal(u.edAmount)));
            }
          
           
            //
            // TODO: Add constructor logic here
            //
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
                if (count % 2 == 0)
                {
                    table.Rows.Add(row);
                    row = new XRTableRow();
                }





            }
            if (count % 2 != 0)
            {
                for (int i = 0; i < (2 - (count % 2)) * 2; i++)
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
        /// <summary> 
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var employeeNumber = GetCurrentColumnValue<string>("reference");

            EmployeePayrollDataSet.SalaryDetailsDataTable tbl = new EmployeePayrollDataSet.SalaryDetailsDataTable();

            var benifits = employeePayrollDataSet1.SalaryDetails
                .Where(u => u.reference == employeeNumber && u.edType == 1).Select(x => x).ToList();

            benifits.ForEach(r => tbl.ImportRow(r));

            xrSubBenifits.ReportSource = new EmployeePaySlipSub(tbl);
            xrSubBenifits.ReportSource.RightToLeftLayout = this.RightToLeftLayout;
            xrSubBenifits.ReportSource.RightToLeft = this.RightToLeft;

            EmployeePayrollDataSet.SalaryDetailsDataTable tbl2 = new EmployeePayrollDataSet.SalaryDetailsDataTable();

            var deductions = employeePayrollDataSet1.SalaryDetails
                .Where(u => u.reference == employeeNumber && u.edType == 2).Select(x => x).ToList();

            deductions.ForEach(r => tbl2.ImportRow(r));

            xrSubDeductions.ReportSource = new EmployeePaySlipSub(tbl2);
            xrSubDeductions.ReportSource.RightToLeftLayout = this.RightToLeftLayout;
            xrSubDeductions.ReportSource.RightToLeft = this.RightToLeft;
        }
    }
}
