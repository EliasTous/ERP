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
        public enum GroupType
        {
            Department = 1,
            Branch = 2,
            Position = 3
        }
        private double basicAmount { get; set; }
        private double essAmount { get; set; }
        private double cssAmount { get; set; }
        private double netSalary { get; set; }


        public GroupedPayrollCrossReport(List<RT503> items, bool isArabic, GroupType grpType)
        {
            try
            {
                InitializeComponent();
                if (isArabic)
                {
                    this.RightToLeft = RightToLeft.Yes;
                    this.RightToLeftLayout = RightToLeftLayout.Yes;
                    fieldBasicSalary1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    fieldCSS1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    fieldESS1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                }
                if (isArabic)
                {
                    switch (grpType)
                    {
                        case GroupType.Branch:
                            fieldBranch1.Caption = "الفرع";
                            break;
                        case GroupType.Department:
                            fieldBranch1.Caption = "القسم";
                            break;
                        case GroupType.Position:
                            fieldBranch1.Caption = "الوظيفة";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    fieldBranch1.Caption = grpType.ToString();
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



                foreach (var employee in groupedItems)
                {

                    employee.GroupBy(x => x.primaryKey).ToList().ForEach(y =>
                    {

                        basicAmount += y.ToList().First().basicAmount;
                        cssAmount += y.ToList().First().cssAmount;
                        essAmount += y.ToList().First().essAmount;
                        netSalary += y.ToList().First().netSalary;
                    });

                    foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 1).GroupBy(u => u.edName))
                    {
                        dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.Key, salary.ToList().Sum(x => x.edAmount), isArabic ? "غير ضريبي" : "Non Taxable", basicAmount, cssAmount, essAmount, isArabic ? "الاستحقاقات" : "Entitlements", employee.Key
                               );
                        break;






                    }





                    foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 1).GroupBy(u => u.edName))
                    {
                        dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.Key, salary.ToList().Sum(x => x.edAmount), isArabic ? "غير ضريبي" : "Non Taxable", basicAmount, cssAmount, essAmount, isArabic ? "الاستحقاقات" : "Entitlements", employee.Key
                            );



                    }



                    foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 2).GroupBy(u => u.edName))
                    {
                        dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.Key, salary.ToList().Sum(x => x.edAmount), isArabic ? "ضريبي" : "Taxable", basicAmount, cssAmount, essAmount, isArabic ? "استقطاعات" : "Deductions", employee.Key
                      );



                    }

                    foreach (var salary in employee.Where(u => !u.isTaxable && u.edType == 2).GroupBy(u => u.edName))
                    {
                        dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.Key, salary.ToList().Sum(x => x.edAmount), isArabic ? "ضريبي" : "Taxable", basicAmount, cssAmount, essAmount, isArabic ? "استقطاعات" : "Deductions", employee.Key
                     );



                    }
                    dsSalaries1.SalariesItems.AddSalariesItemsRow(isArabic ? "صافي الراتب" : "Net Salary", netSalary, " ", basicAmount, cssAmount, essAmount,
                             " ", employee.Key);


                }
            }
            catch(Exception exp)
            {

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
