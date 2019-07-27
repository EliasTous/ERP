using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using AionHR.Model.Reports;
using DevExpress.XtraPivotGrid;
using System.Drawing.Printing;
using DevExpress.XtraReports.UI.PivotGrid;

namespace Reports.ShiftLogs
{
    public partial class ShiftLogsReport : DevExpress.XtraReports.UI.XtraReport
    {

        List<RT309> items = new List<RT309>();
        public ShiftLogsReport(List<RT309> items, /*bool isArabic*/ string getLanguage, string DateFormat, Dictionary<string, string> parameters,int maxShiftCount)
        {
            this.items = items;
            this.PaperKind = PaperKind.A4;
           // this.Landscape = true;
            InitializeComponent();

            if (maxShiftCount > 0)
            {
                fieldShiftId1.Width = (this.PageWidth - this.Margins.Left - this.Margins.Right - fieldemployeeName1.Width - fielddayId1.Width) / ++maxShiftCount ;
                fieldShiftId1.MinWidth = (this.PageWidth - this.Margins.Left - this.Margins.Right - fieldemployeeName1.Width - fielddayId1.Width) / ++maxShiftCount ;
            }
           
            fielddayId1.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            fielddayId1.ValueFormat.FormatString = DateFormat;
            //if (isArabic)
            if (getLanguage == "ar")
            {
                fieldemployeeName1.Caption = "الموظف";
                fielddayId1.Caption = "التاريخ";
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
                xrPivotGrid1.RightToLeft = RightToLeft.Yes; ;

            }


            string dur = "";

            if (getLanguage == "ar")
            {
                dur = "المدة";
            }
            else if (getLanguage == "en")
            {
                dur = "Duration";
            }
            else if (getLanguage == "fr")
            {
                dur = "Durée";
            }

            shiftLogsDS1.DataTable1.AddDataTable1Row("ad");

            items.ForEach(x =>
            {
                shiftLogsDS1.ShiftItems.AddShiftItemsRow(x.employeeId, x.employeeName, x.dayIdDateTime,time(x.duration,true) , /*isArabic?"المدة":"Duration"*/ dur);
                shiftLogsDS1.ShiftItems.AddShiftItemsRow(x.employeeId,x.employeeName,x.dayIdDateTime,buildShiftValue(x.shiftLog),x.shiftId);
            });



            printHeader(parameters);
       
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
                string shift = "";

                shifts.ForEach(x => {

                    if (string.IsNullOrEmpty(x.start))
                        shift += x.end;
                    else
                    {
                        if (string.IsNullOrEmpty(x.end))
                        {
                            shift += x.start;
                        }
                        else
                            shift += x.start + " - " + x.end;
                    }
                  
                    shift += Environment.NewLine;

                });

                //if (shifts.Count != 0)
                //{
                //    if (string.IsNullOrEmpty(shifts[0].start))
                //        shift = shifts[0].end;
                //    else
                //    {
                //        if (string.IsNullOrEmpty(shifts[0].end))
                //        {
                //            shift = shifts[0].start;
                //        }
                //        else
                //            shift = shifts[0].start + " - " + shifts[0].end;
                //    }

                //    shift += Environment.NewLine;

                //}
                //if (shifts.Count==2)
                //{
                //    shift += Environment.NewLine;
                //    if (string.IsNullOrEmpty(shifts[1].start))
                //        shift += shifts[1].end;
                //    else
                //    {
                //        if (string.IsNullOrEmpty(shifts[1].end))
                //        {
                //            shift += shifts[1].start;
                //        }
                //        else
                //            shift += shifts[1].start + " - " + shifts[1].end;
                //    }
                //}
                return shift;
            }
            catch { return ""; }
        }

        private void ShiftLogsReport_AfterPrint(object sender, EventArgs e)
        {
        
        }

        private void xrPivotGrid1_CustomColumnWidth(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomColumnWidthEventArgs e)
        {
           
        }
        protected void ASPxPivotGrid1_CustomSummary(object sender,PivotGridCustomSummaryEventArgs e)
        {
           

            // A variable which counts the number of orders whose sum exceeds $500. 
            int order500Count = 0;

            // Get the record set corresponding to the current cell. 
            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();

            // Iterate through the records and count the orders. 
            for (int i = 0; i < ds.RowCount; i++)
            {
                PivotDrillDownDataRow row = ds[i];

                // Get the order's total sum. 
                //decimal orderSum = (decimal)row[fieldExtendedPrice];
                //if (orderSum >= minSum) order500Count++;
            }

            // Calculate the percentage. 
            if (ds.RowCount > 0)
            {
                e.CustomValue = 10;
            }
        }

        public static string time(int _minutes, bool _signed)
        {
            if (_minutes == 0)
                return "00:00";

            bool isNegative = _minutes < 0 ? true : false;

            _minutes = Math.Abs(_minutes);

            string hours = (_minutes / 60).ToString(), minutes = (_minutes % 60).ToString(), formattedTime;

            if (hours.Length == 1)
                hours = "0" + hours;

            if (minutes.Length == 1)
                minutes = "0" + minutes;

            formattedTime = hours + ':' + minutes;

            if (isNegative && _signed)
                formattedTime = "-" + formattedTime;

            return formattedTime;
        }

        private void xrPivotGrid1_CustomCellDisplayText(object sender, PivotCellDisplayTextEventArgs e)
        {
            e.GetFieldValue(fieldemployeeName1);
           if ( e.RowValueType == PivotGridValueType.Total && e.ColumnIndex==0)
            {
                e.DisplayText =time(items.Where(x => x.employeeName == e.GetFieldValue(fieldemployeeName1).ToString()).Sum(x => x.duration),true);
                return;

            }
            if (e.RowValueType == PivotGridValueType.Total)
                e.DisplayText = "";

        }

        private void xrPivotGrid1_CustomCellValue(object sender, PivotCellValueEventArgs e)
        {
            

        }
    }

}
