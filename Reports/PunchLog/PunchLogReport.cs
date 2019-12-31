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

namespace Reports.PunchLog
{
    public partial class PunchLogReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PunchLogReport(List<RT308> items, /*bool isArabic*/ string getLanguage, string DateFormat, Dictionary<string, string> parameters,int maxPunchCount,string reportId)
        {
            InitializeComponent();

            if (reportId=="RT308")
            {

            }



            if (maxPunchCount > 0)
            {
                fieldpunchId.Width = (this.PageWidth - this.Margins.Left - this.Margins.Right - fieldemployeeName1.Width - fielddayId1.Width) / maxPunchCount;
                fieldpunchId.MinWidth = (this.PageWidth - this.Margins.Left - this.Margins.Right - fieldemployeeName1.Width - fielddayId1.Width) / maxPunchCount;
            }
            this.PaperKind = PaperKind.A4;
            this.Landscape = false;
            fielddayId1.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //fielddayId1.ValueFormat.FormatString = DateFormat;
            fielddayId1.ValueFormat.FormatString = "yyyy-MM-dd";

            //if (isArabic)
            if (getLanguage == "ar")
            {
                xrLabel1.Text =reportId=="RT308"? "سجل البصمات " : "بصمات غير معالجة";
                fieldemployeeName1.Caption = "الموظف";
                fielddayId1.Caption = "التاريخ";
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
                xrPivotGrid1.RightToLeft = RightToLeft.Yes; ;

            }
            else if (getLanguage == "fr")
            {
                xrLabel1.Text = xrLabel1.Text = reportId == "RT308" ? "Journal de poinçon": "Poinçon non traités";
                fieldemployeeName1.Caption = "Employe";
                fielddayId1.Caption = "Date";
            }
            else if (getLanguage == "en")
            {
                xrLabel1.Text = xrLabel1.Text = reportId == "RT308" ? "Punch Log" : "Raws Punches";
            }
            else if (getLanguage == "de")
            {
                xrLabel1.Text = xrLabel1.Text = reportId == "RT308" ? "Log schieben" : "Unbehandeltes Poicon";
            }



            dsSalaries1.DataTable1.AddDataTable1Row("ad");
            
            items.ForEach(x =>
            {
                dsSalaries1.SalariesItems.AddSalariesItemsRow(x.employeeId,x.employeeName,x.dayIdDateTime,x.punchString,x.punchId);
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

        private void TopMargin_AfterPrint(object sender, EventArgs e)
        {

        }

        private void PunchLogReport_AfterPrint(object sender, EventArgs e)
        {
           
        }
    }

}
