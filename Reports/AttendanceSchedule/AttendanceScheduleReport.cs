using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using AionHR.Model.Reports;
using DevExpress.XtraPivotGrid;
using System.Globalization;
using System.Threading;
using System.Drawing.Printing;

namespace Reports.AttendanceSchedule
{
    public partial class AttendanceScheduleReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AttendanceScheduleReport(List<RT310> items, bool isArabic,string DateFormat, Dictionary<string, string> parameters)
        {

            printHeader(parameters);
            if (isArabic)
            {
                CultureInfo culture = new CultureInfo("ar-LB");
                culture.DateTimeFormat.ShortDatePattern = "dddd" + Environment.NewLine + DateFormat;
                culture.DateTimeFormat.LongTimePattern = "dddd" + Environment.NewLine + DateFormat;
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            InitializeComponent();
          
            fielddayId.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            fielddayId.ValueFormat.FormatString = "dddd"+ Environment.NewLine + DateFormat ;
            if (isArabic)
            {

                fieldemployeeName.Caption = "الموظف";
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
                xrPivotGrid1.RightToLeft = RightToLeft.Yes;
                


            }
            dsSalaries1.DataTable1.AddDataTable1Row("ad");

            items.ForEach(x =>
            {
                dsSalaries1.SalariesItems.AddSalariesItemsRow(x.employeeId, x.employeeName, x.dayIdDateTime, buildShiftValue(x.shiftLog));
            });






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
        private string buildShiftValue(List<ShiftLog> shifts)
        {
            try
            {
                string shift = " ";
                if (shifts.Count != 0)
                {
                    if (string.IsNullOrEmpty(shifts[0].start))
                        shift = shifts[0].end;
                    else
                    {
                        if (string.IsNullOrEmpty(shifts[0].end))
                        {
                            shift = shifts[0].start;
                        }
                        else
                            shift = shifts[0].start + " - " + shifts[0].end;
                    }



                }
                if (shifts.Count==2)
                {
                    shift += Environment.NewLine;
                    if (string.IsNullOrEmpty(shifts[1].start))
                        shift += shifts[1].end;
                    else
                    {
                        if (string.IsNullOrEmpty(shifts[1].end))
                        {
                            shift += shifts[1].start;
                        }
                        else
                            shift += shifts[1].start + " - " + shifts[1].end;
                    }
                }
                return shift;
            }
            catch { return ""; }
        }
      
    }

}
