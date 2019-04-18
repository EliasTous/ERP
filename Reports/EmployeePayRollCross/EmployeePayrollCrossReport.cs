using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using AionHR.Model.Reports;

namespace Reports.EmployeePayRollCross
{
    public partial class EmployeePayrollCrossReport : DevExpress.XtraReports.UI.XtraReport
    {
        public EmployeePayrollCrossReport(List<RT501> items, bool isArabic)
        {
            
            InitializeComponent();
           
            if (isArabic)
            {
                fieldSalaryDate.Caption = "التاريخ";
                fieldBranch.Caption = "الفرع";
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
                fieldItemValue.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                fieldBasicSalary.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                fieldCSS.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                fieldESS.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            }
          
            dsSalaries1.DataTable1.AddDataTable1Row("ad");
            var salaryDate = new DateTime();
            if (items.Count!=0)
             salaryDate = items.FirstOrDefault().payDate;

            foreach (var employee in items.GroupBy(u => u.employeeName.reference))
            {
                var defaultSal = employee.First();
                int order = 0;
                foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 1))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.employeeName.fullName,
                        salaryDate, order, salary.edName, salary.edAmount,
                        isArabic ? "ضريبي" : "Taxable", 1, salary.basicAmount, salary.cssAmount,
                        salary.essAmount, isArabic ? "الاستحقاقات" : "Entitlements",salary.branchName
                        );
                    order++;
                }

                foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 1))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.employeeName.fullName,
                        salaryDate, order, salary.edName, salary.edAmount,
                       isArabic ? "غير ضريبي" : "Non Taxable", 1, salary.basicAmount, salary.cssAmount,
                        salary.essAmount, isArabic ? "الاستحقاقات" : "Entitlements", salary.branchName
                        );
                    order++;
                }

                foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 2))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.employeeName.fullName,
                        salaryDate, order, salary.edName, salary.edAmount,
                        isArabic ? "ضريبي" : "Taxable", 1, salary.basicAmount, salary.cssAmount,
                        salary.essAmount, isArabic ? "استقطاعات" : "Deductions", salary.branchName
                        );
                    order++;
                }

                foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 2))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.employeeName.fullName,
                        salaryDate, order, salary.edName, salary.edAmount,
                       isArabic ? "غير ضريبي" : "Non Taxable", 1, salary.basicAmount, salary.cssAmount,
                        salary.essAmount, isArabic ? "استقطاعات" : "Deductions", salary.branchName
                        );
                    order++;
                }

                dsSalaries1.SalariesItems.AddSalariesItemsRow(defaultSal.employeeName.fullName,
                         salaryDate, order, "", defaultSal.netSalary,
                        isArabic ? "صافي الراتب" : "Net Salary", 1, defaultSal.basicAmount, defaultSal.cssAmount, 
                         defaultSal.essAmount, "    ", defaultSal.branchName
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
            this.xrLabel4.LocationF = new PointF((float)23.38, (float)82.38);

        }

        private void xrLabel4_LocationChanged(object sender, ChangeEventArgs e)
        {

           
        }
    }


}
