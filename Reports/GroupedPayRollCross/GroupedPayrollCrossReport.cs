using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using AionHR.Model.Reports;
using System.Drawing.Printing;

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


        public GroupedPayrollCrossReport(List<RT503> items, bool isArabic, GroupType grpType, Dictionary<string,string> parameters)
        {
            try
            {
                InitializeComponent();
                printHeader(parameters);
                if (isArabic)
                {
                    this.RightToLeft = RightToLeft.Yes;
                    this.RightToLeftLayout = RightToLeftLayout.Yes;
                    fieldBasicSalary1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    fieldCSS1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    fieldESS1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    fieldItemValue1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    fieldItemValue1.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    fieldBasicSalary1.Caption = "الراتب";

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
                    basicAmount = 0;
                    cssAmount = 0;
                    essAmount = 0;
                    netSalary = 0;
                    string primary = "";

                    employee.GroupBy(x => x.primaryKey).ToList().ForEach(y =>
                    {
                        primary = y.ToList().First().primaryKey;
                        basicAmount += y.ToList().First().basicAmount;
                        cssAmount += y.ToList().First().cssAmount;
                        essAmount += y.ToList().First().essAmount;
                        netSalary += y.ToList().First().netSalary;
                    });

                    foreach (var salary in employee.Where(u => u.isTaxable && u.edType == 1).GroupBy(u => u.edName))
                    {
                        dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.Key, salary.ToList().Sum(x => x.edAmount), isArabic ? "ضريبي" : "Taxable", basicAmount, cssAmount, essAmount, isArabic ? "الاستحقاقات" : "Entitlements", employee.Key
                               );
                       
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
                        dsSalaries1.SalariesItems.AddSalariesItemsRow(salary.Key, salary.ToList().Sum(x => x.edAmount), isArabic ? "غير ضريبي" : "Non Taxable", basicAmount, cssAmount, essAmount, isArabic ? "استقطاعات" : "Deductions", employee.Key
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
