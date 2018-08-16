namespace Reports
{
    partial class rptDepartmentsSalaries
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.dsSalaries1 = new dsSalaries();
            this.fieldDepartmentName = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldItemName = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldItemValue = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSalaryDate = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldGroupName = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.dsSalaries1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1});
            this.Detail.Dpi = 100F;
            this.Detail.HeightF = 115F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.DataMember = "SalariesItems";
            this.xrPivotGrid1.DataSource = this.dsSalaries1;
            this.xrPivotGrid1.Dpi = 100F;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldDepartmentName,
            this.fieldItemName,
            this.fieldItemValue,
            this.fieldSalaryDate,
            this.fieldGroupName});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(1131F, 115F);
            this.xrPivotGrid1.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.grdAccountLedger_CustomFieldSort);
            // 
            // dsSalaries1
            // 
            this.dsSalaries1.DataSetName = "dsSalaries";
            this.dsSalaries1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fieldDepartmentName
            // 
            this.fieldDepartmentName.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldDepartmentName.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldDepartmentName.Appearance.Cell.WordWrap = true;
            this.fieldDepartmentName.Appearance.CustomTotalCell.WordWrap = true;
            this.fieldDepartmentName.Appearance.FieldHeader.WordWrap = true;
            this.fieldDepartmentName.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldDepartmentName.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldDepartmentName.Appearance.FieldValue.WordWrap = true;
            this.fieldDepartmentName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDepartmentName.AreaIndex = 0;
            this.fieldDepartmentName.ColumnValueLineCount = 2;
            this.fieldDepartmentName.FieldName = "DepartmentName";
            this.fieldDepartmentName.Name = "fieldDepartmentName";
            this.fieldDepartmentName.RowValueLineCount = 2;
            // 
            // fieldItemName
            // 
            this.fieldItemName.Appearance.Cell.WordWrap = true;
            this.fieldItemName.Appearance.CustomTotalCell.WordWrap = true;
            this.fieldItemName.Appearance.FieldHeader.WordWrap = true;
            this.fieldItemName.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemName.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemName.Appearance.FieldValue.WordWrap = true;
            this.fieldItemName.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldItemName.AreaIndex = 1;
            this.fieldItemName.ColumnValueLineCount = 2;
            this.fieldItemName.FieldName = "ItemName";
            this.fieldItemName.Name = "fieldItemName";
            this.fieldItemName.RowValueLineCount = 2;
            this.fieldItemName.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.fieldItemName.Width = 60;
            // 
            // fieldItemValue
            // 
            this.fieldItemValue.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemValue.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemValue.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemValue.Appearance.CustomTotalCell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemValue.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemValue.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemValue.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemValue.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemValue.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldItemValue.AreaIndex = 0;
            this.fieldItemValue.CellFormat.FormatString = "N1";
            this.fieldItemValue.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldItemValue.FieldName = "ItemValue";
            this.fieldItemValue.Name = "fieldItemValue";
            this.fieldItemValue.ValueFormat.FormatString = "N1";
            this.fieldItemValue.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // fieldSalaryDate
            // 
            this.fieldSalaryDate.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldSalaryDate.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldSalaryDate.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldSalaryDate.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldSalaryDate.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldSalaryDate.AreaIndex = 1;
            this.fieldSalaryDate.ColumnValueLineCount = 2;
            this.fieldSalaryDate.FieldName = "SalaryDate";
            this.fieldSalaryDate.Name = "fieldSalaryDate";
            this.fieldSalaryDate.RowValueLineCount = 2;
            this.fieldSalaryDate.ValueFormat.FormatString = "dd/MM/yyyy";
            this.fieldSalaryDate.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.fieldSalaryDate.Width = 80;
            // 
            // fieldGroupName
            // 
            this.fieldGroupName.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldGroupName.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldGroupName.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldGroupName.AreaIndex = 0;
            this.fieldGroupName.FieldName = "GroupName";
            this.fieldGroupName.Name = "fieldGroupName";
            this.fieldGroupName.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 21F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 13.54167F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rptDepartmentsSalaries
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataMember = "DataTable1";
            this.DataSource = this.dsSalaries1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(11, 7, 21, 14);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "16.1";
            ((System.ComponentModel.ISupportInitialize)(this.dsSalaries1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private dsSalaries dsSalaries1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDepartmentName;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItemName;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItemValue;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSalaryDate;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGroupName;
    }
}
