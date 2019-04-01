﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using AionHR.Model.Reports;
using DevExpress.XtraPivotGrid;

namespace Reports.AttendanceSchedule
{
    public partial class AttendanceScheduleReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AttendanceScheduleReport(List<RT310> items, bool isArabic,string DateFormat)
        {
            InitializeComponent();
          
            fielddayId.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            fielddayId.ValueFormat.FormatString = DateFormat;
            if (isArabic)
            {
                fieldemployeeName.Caption = "الموظف";
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = RightToLeftLayout.Yes;
                xrPivotGrid1.RightToLeft = RightToLeft.Yes; ;

            }
            dsSalaries1.DataTable1.AddDataTable1Row("ad");

            items.ForEach(x =>
            {
                dsSalaries1.SalariesItems.AddSalariesItemsRow(x.employeeId, x.employeeName, x.dayIdDateTime, buildShiftValue(x.shiftLog));
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
