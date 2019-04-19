using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using AionHR.Model.Reports;
using DevExpress.XtraPivotGrid;

namespace Reports.PunchLog
{
    public partial class PunchLogReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PunchLogReport(List<RT308> items, bool isArabic,string DateFormat)
        {
            InitializeComponent();
          
            fielddayId1.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            fielddayId1.ValueFormat.FormatString = DateFormat;
            if (isArabic)
            {
                fieldemployeeName1.Caption = "الموظف";
                fielddayId1.Caption = "التاريخ";
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
                xrPivotGrid1.RightToLeft = RightToLeft.Yes; ;

            }
            dsSalaries1.DataTable1.AddDataTable1Row("ad");

            items.ForEach(x =>
            {
                dsSalaries1.SalariesItems.AddSalariesItemsRow(x.employeeId,x.employeeName,x.dayIdDateTime,x.punchString,x.punchId);
            });





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
