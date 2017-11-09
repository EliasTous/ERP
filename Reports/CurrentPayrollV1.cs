using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Reports
{
    public partial class CurrentPayrollV1 : DevExpress.XtraReports.UI.XtraReport
    {
        public CurrentPayrollV1()
        {
            InitializeComponent();
        }

        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPivotGrid1.BestFit();
            
        }

        private void xrPivotGrid1_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.Field == null)
                return;
            if (e.Field.FieldName == "isTaxable" && Convert.ToBoolean(e.Value)==true )
                e.DisplayText = "Taxable";
            else if (e.Field.FieldName == "isTaxable" && Convert.ToBoolean(e.Value) == false)
                e.DisplayText = "Non Taxable";

            if (e.Field.FieldName == "edType" && Convert.ToInt16(e.Value) == 1)
                e.DisplayText = "Entitlements";
            else if (e.Field.FieldName == "edType" && Convert.ToInt16(e.Value) == 2)
                e.DisplayText = "Deductions";
        }
    }
}
