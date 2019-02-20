using System;
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
        public enum GroupType
        {
            Branch = 2,
            Department = 1,
            Position = 3
        }

        public GroupedPayrollCrossReport(List<RT503> items, bool isArabic, GroupType grpType)
        {
            InitializeComponent();
            if (isArabic)
            {
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
            }
            if (isArabic)
            {
                switch (grpType)
                {
                    case GroupType.Branch:
                        fieldEmployeeName.Caption = "الفرع";
                        break;
                    case GroupType.Department:
                        fieldEmployeeName.Caption = "القسم";
                        break;
                    case GroupType.Position:
                        fieldEmployeeName.Caption = "الوظيفة";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                fieldEmployeeName.Caption = grpType.ToString();
            }
            var groupedItems = items.GroupBy(u => u.branchName);

            if (grpType == GroupType.Department)
            {
                groupedItems = items.GroupBy(u => u.departmentName);
            }

            if (grpType == GroupType.Position)
            {
                groupedItems = items.GroupBy(u => u.positionName);
            }
            double basic1; 
            foreach (var employee in groupedItems)
            {

                int order = 0;
                foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 1).GroupBy(u => u.edName))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key,
                        order, salary.Key, salary.Sum(u => u.edAmount),
                        isArabic ? "ضريبي" : "Taxable", 1, employee.ToList().First().basicAmount, employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), isArabic ? "الاستحقاقات" : "Entitlements"
                        );
                    basic1 = employee.ToList().First().basicAmount;
                    order++;
                }

                foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 1).GroupBy(u => u.edName))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key,
                         order, salary.Key, salary.Sum(u => u.edAmount),
                       isArabic ? "غير ضريبي" : "Non Taxable", 1, employee.ToList().First().basicAmount, employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), isArabic ? "الاستحقاقات" : "Entitlements"
                        );
                    basic1 = employee.ToList().First().basicAmount;
                    order++;
                }

                foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 2).GroupBy(u => u.edName))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key,
                         order, salary.Key, salary.Sum(u => u.edAmount),
                        isArabic ? "ضريبي" : "Taxable", 1, employee.ToList().First().basicAmount, employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), isArabic ? "استقطاعات" : "Deductions"
                        );
                    basic1 = employee.ToList().First().basicAmount;
                    order++;
                }

                foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 2).GroupBy(u => u.edName))
                {
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key,
                         order, salary.Key, salary.Sum(u => u.edAmount),
                       isArabic ? "غير ضريبي" : "Non Taxable", 1, employee.ToList().First().basicAmount, employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), isArabic ? "استقطاعات" : "Deductions"
                        );
                    basic1 = employee.ToList().First().basicAmount;
                    order++;
                }

                dsSalaries1.SalariesItems.AddSalariesItemsRow(employee.Key
                         , order, "", employee.Sum(u => u.basicAmount) + employee.Where(u => u.edType == 1).Sum(u => u.edAmount) - employee.Where(u => u.edType == 2).Sum(u => u.edAmount) - employee.Sum(u => u.essAmount),
                        isArabic ? "صافي الراتب" : "Net Salary", 1, employee.ToList().First().basicAmount, employee.Sum(u => u.cssAmount),
                        employee.Sum(u => u.essAmount), "    "
                         );
                basic1 = employee.ToList().First().basicAmount;
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
