﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using AionHR.Model.Reports;

namespace Reports.GroupedPayRollCross
{
    public partial class GroupedPayrollCrossReport : DevExpress.XtraReports.UI.XtraReport
    {
       

        public GroupedPayrollCrossReport(List<RT503> items, bool isArabic)
        {
            InitializeComponent();
            if (isArabic)
            {
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
            }

            var groupedItems = items.GroupBy(u => u.branchName);

            foreach (var employee in groupedItems)
            {
                
                int order = 0;
                foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 1).GroupBy(u => u.edName))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key,
                        order, salary.Key, salary.Sum(u => u.edAmount),
                        isArabic ? "ضريبي" : "Taxable", 1, employee.Sum(u => u.basicAmount), employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), isArabic ? "الاستحقاقات" : "Entitlements"
                        );
                    order++;
                }

                foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 1).GroupBy(u => u.edName))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key,
                         order, salary.Key, salary.Sum(u => u.edAmount),
                       isArabic ? "غير ضريبي" : "Non Taxable", 1, employee.Sum(u => u.basicAmount), employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), isArabic ? "الاستحقاقات" : "Entitlements"
                        );
                    order++;
                }

                foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 2).GroupBy(u => u.edName))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key,
                         order, salary.Key, salary.Sum(u => u.edAmount),
                        isArabic ? "ضريبي" : "Taxable", 1, employee.Sum(u => u.basicAmount), employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), isArabic ? "استقطاعات" : "Deductions"
                        );
                    order++;
                }

                foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 2).GroupBy(u => u.edName))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key,
                         order, salary.Key, salary.Sum(u => u.edAmount),
                       isArabic ? "غير ضريبي" : "Non Taxable", 1, employee.Sum(u => u.basicAmount), employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), isArabic ? "استقطاعات" : "Deductions"
                        );
                    order++;
                }

                dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key
                         , order, "", employee.Sum(u => u.basicAmount) + employee.Where(u => u.edType == 1).Sum(u => u.edAmount) - employee.Where(u => u.edType == 2).Sum(u => u.edAmount) - employee.Sum(u => u.essAmount),
                        isArabic ? "صافي الراتب" : "Net Salary", 1, employee.Sum(u => u.basicAmount), employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), "    "
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
    }


}
