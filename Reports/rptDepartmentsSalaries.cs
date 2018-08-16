using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using AionHR.Model.Reports;

namespace Reports
{
    public partial class rptDepartmentsSalaries : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDepartmentsSalaries(List<SalaryItem> items, bool isArabic)
        {
            InitializeComponent();
            if (isArabic)
            {
                fieldDepartmentName.Caption = "القسم";
                fieldSalaryDate.Caption = "التاريخ";
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
            }
            dsSalaries1.DataTable1.AddDataTable1Row("ad");

            var salaryDate = items.FirstOrDefault().payDate;

            var depts =
                items.Select(x => x.departmentName).Distinct()
                .ToList();

            var taxable =
                items.Where(u => u.isTaxable && u.edType == 1)
                .Select(x => x.edName).Distinct()
                .ToList();

            var nonTaxable =
               items.Where(u => !u.isTaxable && u.edType == 1)
               .Select(x => x.edName).Distinct()
               .ToList();

            var deductions =
               items.Where(u => u.edType == 2)
               .Select(x => x.edName).Distinct()
               .ToList();


            foreach (var dept in depts)
            {
                //add basic salary
                dsSalaries1.SalariesItems.AddSalariesItemsRow(
                    dept,
                    salaryDate,
                    1,
                   isArabic ? "الراتب الاساسي" : "basic sal",
                    items.FirstOrDefault(u => u.departmentName == dept).basicAmount,
                   isArabic ? "خاضع للضريبة" : "Taxable", 1
                    );

                //add taxable

                for (int i = 0; i < taxable.Count; i++)
                {
                    if (items.FirstOrDefault(u => u.departmentName == dept && u.edName == taxable[i]) != null)
                    {
                        dsSalaries1.SalariesItems.AddSalariesItemsRow(
                        dept,
                        salaryDate,
                        i + 2,
                       taxable[i],
                        items.FirstOrDefault(u => u.departmentName == dept && u.edName == taxable[i]).edAmount,
                       isArabic ? "خاضع للضريبة" : "Taxable", 1
                        );
                    }
                }

                dsSalaries1.SalariesItems.AddSalariesItemsRow(
                       dept,
                       salaryDate,
                       taxable.Count + 2,
                       isArabic ? "الاجمالي" : "Total",
                     items.FirstOrDefault(u => u.departmentName == dept).basicAmount + items.Where(u => u.departmentName == dept && u.isTaxable && u.edType == 1).Sum(u => u.edAmount),
                      isArabic ? "خاضع للضريبة" : "Taxable", 1
                       );

                //add non taxable

                for (int i = 0; i < nonTaxable.Count; i++)
                {
                    if (items.FirstOrDefault(u => u.departmentName == dept && u.edName == nonTaxable[i]) != null)
                    {
                        dsSalaries1.SalariesItems.AddSalariesItemsRow(
                    dept,
                    salaryDate,
                    i + 3 + taxable.Count,
                   nonTaxable[i],
                    items.FirstOrDefault(u => u.departmentName == dept && u.edName == nonTaxable[i]).edAmount,
                   isArabic ? "غير خاضع للضريبة" : "Non Taxable", 2
                    );
                    }
                }


                dsSalaries1.SalariesItems.AddSalariesItemsRow(
                       dept,
                       salaryDate,
                       nonTaxable.Count + taxable.Count + 3,
                       isArabic ? "الاجمالي" : "Total",
                       items.Where(u => u.departmentName == dept && !u.isTaxable && u.edType == 1).Sum(u => u.edAmount),
                      isArabic ? "غير خاضع للضريبة" : "Non Taxable", 2
                       );

                //add deduction

                for (int i = 0; i < deductions.Count; i++)
                {
                    if (items.FirstOrDefault(u => u.departmentName == dept && u.edName == deductions[i]) != null)
                    {
                        dsSalaries1.SalariesItems.AddSalariesItemsRow(
                    dept,
                    salaryDate,
                    i + 4 + taxable.Count + nonTaxable.Count,
                   deductions[i],
                    items.FirstOrDefault(u => u.departmentName == dept && u.edName == deductions[i]).edAmount,
                   isArabic ? "استقطاعات" : "Deductions", 3
                    );
                    }
                }
                dsSalaries1.SalariesItems.AddSalariesItemsRow(
                       dept,
                       salaryDate,
                       deductions.Count + nonTaxable.Count + taxable.Count + 4,
                       isArabic ? "الاجمالي" : "Total",
                       items.Where(u => u.departmentName == dept && u.edType == 2).Sum(u => u.edAmount),
                      isArabic ? "استقطاعات" : "Deductions", 3
                       );

                dsSalaries1.SalariesItems.AddSalariesItemsRow(
                       dept,
                       salaryDate,
                       deductions.Count + nonTaxable.Count + taxable.Count + 5,
                       isArabic ? "صافي الراتب" : "Net Salary",
                       items.FirstOrDefault(u => u.departmentName == dept).basicAmount + items.Where(u => u.departmentName == dept).Sum(u => u.edAmount),
                      isArabic ? "صافي الراتب" : "Net Salary", 4
                       );

                dsSalaries1.SalariesItems.AddSalariesItemsRow(
                       dept,
                       salaryDate,
                       deductions.Count + nonTaxable.Count + taxable.Count + 6,
                       isArabic ? "التامين" : "Insurance",
                       items.FirstOrDefault(u => u.departmentName == dept).essAmount,
                      isArabic ? "صافي الراتب" : "Net Salary", 4
                       );

                dsSalaries1.SalariesItems.AddSalariesItemsRow(
                      dept,
                      salaryDate,
                      deductions.Count + nonTaxable.Count + taxable.Count + 7,
                      isArabic ? "التامين الاجتماعي" : "Social Insurance",
                      items.FirstOrDefault(u => u.departmentName == dept).cssAmount,
                     isArabic ? "صافي الراتب" : "Net Salary", 4
                      );

                dsSalaries1.SalariesItems.AddSalariesItemsRow(
                      dept,
                      salaryDate,
                      deductions.Count + nonTaxable.Count + taxable.Count + 8,
                      isArabic ? "الصافي " : "Net",
                      items.FirstOrDefault(u => u.departmentName == dept).netSalary,
                     isArabic ? "صافي الراتب" : "Net Salary", 4
                      );
            }

        }
        private void grdAccountLedger_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "ItemName")
            {

                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "ColumneOrder"),
                 orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "ColumneOrder");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
                e.Handled = true;
            }

            if (e.Field.FieldName == "GroupName")
            {

                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "GroupOrder"),
                 orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "GroupOrder");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
                e.Handled = true;
            }
        }
    }


}
