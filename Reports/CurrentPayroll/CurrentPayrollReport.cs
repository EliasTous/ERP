using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using AionHR.Model.Reports;
using System.Drawing.Printing;

namespace Reports.CurrentPayroll
{
    public partial class CurrentPayrollReport : DevExpress.XtraReports.UI.XtraReport
    {
        public CurrentPayrollReport(List<RT200> items, bool isArabic,Dictionary<string,string> parameters)
        {
            
            InitializeComponent();
            printHeader(parameters);
            if (isArabic)
            {
               // fieldSalaryDate.Caption = "التاريخ";
                fieldBranch.Caption = "الفرع";
                fieldEmployeeName.Caption = "الموظف";
                fieldCounrty.Caption = "البلد";
                fieldDepartment.Caption = "القسم";
                fieldBasicSalary.Caption = "الاسأسي";
               
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
                fieldItemValue.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                fieldBasicSalary.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                fieldCSS.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                fieldESS.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                fieldBasicSalary.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                fieldCSS.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                fieldESS.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;



            }
          
            dsSalaries1.DataTable1.AddDataTable1Row("ad");
            //var salaryDate = new DateTime();
            //if (items.Count!=0)
            // salaryDate = items.FirstOrDefault().payDate;

            foreach (var employee in items.GroupBy(u => u.employeeName))
            {
                var defaultSal = employee.First();
                
                foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 1))
                {

                    dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.employeeName,
                       salary.edName, salary.edAmount, isArabic ? "ضريبي" : "Taxable", salary.basicAmount, salary.cssAmount,
                     salary.essAmount, isArabic ? "الاستحقاقات" : "Entitlements", salary.branchName, salary.employeeRef, salary.countryName, salary.departmentName);

                 
                }

                foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 1))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.employeeName,
                      salary.edName, salary.edAmount, isArabic ? "غير ضريبي" : "Non Taxable", salary.basicAmount, salary.cssAmount,
                    salary.essAmount, isArabic ? "الاستحقاقات" : "Entitlements", salary.branchName, salary.employeeRef, salary.countryName, salary.departmentName);
                   
                }

                foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 2))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.employeeName,
                     salary.edName, salary.edAmount, isArabic ? "ضريبي" : "Taxable", salary.basicAmount, salary.cssAmount,
                   salary.essAmount, isArabic ? "استقطاعات" : "Deductions", salary.branchName, salary.employeeRef, salary.countryName, salary.departmentName);
                   
                }

                foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 2))
                {
                   

                    dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.employeeName,
                        salary.edName, salary.edAmount, isArabic ? "غير ضريبي" : "Non Taxable", salary.basicAmount, salary.cssAmount,
                      salary.essAmount, isArabic ? "استقطاعات" : "Deductions",salary.branchName,salary.employeeRef,salary.countryName,salary.departmentName);



                }

                dsSalaries1.SalariesItems.AddSalariesItemsRow(defaultSal.employeeName, isArabic ? "صافي الراتب" : "Net Salary",defaultSal.netSalary, "    ",defaultSal.basicAmount,defaultSal.cssAmount
                        ,defaultSal.essAmount," ",defaultSal.branchName,defaultSal.employeeRef,defaultSal.countryName,defaultSal.departmentName
                       
                         );
            }



        }
        private void grdAccountLedger_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            //if (e.Field.FieldName == "ItemName")
            //{

            //    object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "ColumneOrder"),
            //     orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "ColumneOrder");
            //    e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
            //    e.Handled = true;
            //}

            //if (e.Field.FieldName == "GroupName")
            //{

            //    object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "GroupOrder"),
            //     orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "GroupOrder");
            //    e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
            //    e.Handled = true;
            //}
        }

        private void EmployeePayrollCrossReport_AfterPrint(object sender, EventArgs e)
        {
          

        }

        private void xrLabel4_LocationChanged(object sender, ChangeEventArgs e)
        {

           
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
