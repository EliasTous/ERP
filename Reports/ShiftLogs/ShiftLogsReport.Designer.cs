namespace Reports.ShiftLogs
{
    partial class ShiftLogsReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShiftLogsReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.shiftLogsDS1 = new Reports.ShiftLogs.ShiftLogsDS();
            this.fieldemployeeId1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldemployeeName1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fielddayId1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldShift1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldShiftId1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.To = new DevExpress.XtraReports.Parameters.Parameter();
            this.From = new DevExpress.XtraReports.Parameters.Parameter();
            this.Branch = new DevExpress.XtraReports.Parameters.Parameter();
            this.User = new DevExpress.XtraReports.Parameters.Parameter();
            this.Employee = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.shiftLogsDS1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.Detail.StylePriority.UseTextAlignment = false;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid1.Appearance.Lines.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid1.Appearance.TotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid1.DataMember = "ShiftItems";
            this.xrPivotGrid1.DataSource = this.shiftLogsDS1;
            resources.ApplyResources(this.xrPivotGrid1, "xrPivotGrid1");
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldemployeeId1,
            this.fieldemployeeName1,
            this.fielddayId1,
            this.fieldShift1,
            this.fieldShiftId1});
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsChartDataSource.ProvideColumnTotals = true;
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowTotalsForSingleValues = true;
            this.xrPivotGrid1.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.grdAccountLedger_CustomFieldSort);
            this.xrPivotGrid1.CustomCellDisplayText += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotCellDisplayTextEventArgs>(this.xrPivotGrid1_CustomCellDisplayText);
            this.xrPivotGrid1.CustomCellValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotCellValueEventArgs>(this.xrPivotGrid1_CustomCellValue);
            this.xrPivotGrid1.CustomColumnWidth += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotCustomColumnWidthEventArgs>(this.xrPivotGrid1_CustomColumnWidth);
            this.xrPivotGrid1.CustomSummary += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomSummaryEventArgs>(this.ASPxPivotGrid1_CustomSummary);
            // 
            // shiftLogsDS1
            // 
            this.shiftLogsDS1.DataSetName = "shiftLogsDS";
            this.shiftLogsDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fieldemployeeId1
            // 
            this.fieldemployeeId1.AreaIndex = 0;
            this.fieldemployeeId1.FieldName = "employeeId";
            this.fieldemployeeId1.Name = "fieldemployeeId1";
            // 
            // fieldemployeeName1
            // 
            this.fieldemployeeName1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldemployeeName1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldemployeeName1.Appearance.FieldValue.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.fieldemployeeName1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Azure;
            this.fieldemployeeName1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldemployeeName1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldemployeeName1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldemployeeName1.AreaIndex = 0;
            resources.ApplyResources(this.fieldemployeeName1, "fieldemployeeName1");
            this.fieldemployeeName1.FieldName = "employeeName";
            this.fieldemployeeName1.Name = "fieldemployeeName1";
            this.fieldemployeeName1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;
            // 
            // fielddayId1
            // 
            this.fielddayId1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fielddayId1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fielddayId1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fielddayId1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fielddayId1.Appearance.FieldValue.WordWrap = true;
            this.fielddayId1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fielddayId1.AreaIndex = 1;
            resources.ApplyResources(this.fielddayId1, "fielddayId1");
            this.fielddayId1.FieldName = "dayId";
            this.fielddayId1.Name = "fielddayId1";
            // 
            // fieldShift1
            // 
            this.fieldShift1.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldShift1.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldShift1.Appearance.Cell.WordWrap = true;
            this.fieldShift1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldShift1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldShift1.Appearance.FieldValue.WordWrap = true;
            this.fieldShift1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldShift1.AreaIndex = 0;
            this.fieldShift1.FieldName = "Shift";
            this.fieldShift1.Name = "fieldShift1";
            this.fieldShift1.Options.ShowCustomTotals = false;
            this.fieldShift1.Options.ShowGrandTotal = false;
            this.fieldShift1.Options.ShowTotals = false;
            this.fieldShift1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            this.fieldShift1.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            resources.ApplyResources(this.fieldShift1, "fieldShift1");
            // 
            // fieldShiftId1
            // 
            this.fieldShiftId1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldShiftId1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldShiftId1.Appearance.FieldValue.WordWrap = true;
            this.fieldShiftId1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldShiftId1.AreaIndex = 0;
            this.fieldShiftId1.FieldName = "ShiftId";
            this.fieldShiftId1.Name = "fieldShiftId1";
            this.fieldShiftId1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;
            // 
            // TopMargin
            // 
            resources.ApplyResources(this.TopMargin, "TopMargin");
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            // 
            // BottomMargin
            // 
            resources.ApplyResources(this.BottomMargin, "BottomMargin");
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            // 
            // To
            // 
            this.To.Name = "To";
            this.To.Visible = false;
            // 
            // From
            // 
            this.From.Name = "From";
            this.From.Visible = false;
            // 
            // Branch
            // 
            resources.ApplyResources(this.Branch, "Branch");
            this.Branch.Name = "Branch";
            this.Branch.Visible = false;
            // 
            // User
            // 
            resources.ApplyResources(this.User, "User");
            this.User.Name = "User";
            this.User.Visible = false;
            // 
            // Employee
            // 
            this.Employee.Name = "Employee";
            this.Employee.Visible = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
            resources.ApplyResources(this.ReportHeader, "ReportHeader");
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel1
            // 
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            // 
            // PageHeader
            // 
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1,
            this.xrLabel3,
            this.xrLabel2});
            resources.ApplyResources(this.PageFooter, "PageFooter");
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo2
            // 
            resources.ApplyResources(this.xrPageInfo2, "xrPageInfo2");
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            // 
            // xrPageInfo1
            // 
            resources.ApplyResources(this.xrPageInfo1, "xrPageInfo1");
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            // 
            // xrLabel3
            // 
            resources.ApplyResources(this.xrLabel3, "xrLabel3");
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?User")});
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            // 
            // xrLabel2
            // 
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            // 
            // ShiftLogsReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.PageFooter});
            this.DataMember = "DataTable1";
            this.DataSource = this.shiftLogsDS1;
            this.DefaultPrinterSettingsUsing.UsePaperKind = true;
            resources.ApplyResources(this, "$this");
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.User,
            this.Branch,
            this.From,
            this.To,
            this.Employee});
            this.Version = "18.2";
            this.AfterPrint += new System.EventHandler(this.ShiftLogsReport_AfterPrint);
            ((System.ComponentModel.ISupportInitialize)(this.shiftLogsDS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private ShiftLogsDS shiftLogsDS1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.Parameters.Parameter Branch;
        private DevExpress.XtraReports.Parameters.Parameter User;
        private DevExpress.XtraReports.Parameters.Parameter From;
        private DevExpress.XtraReports.Parameters.Parameter To;
        private DevExpress.XtraReports.Parameters.Parameter Employee;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldemployeeId1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldemployeeName1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fielddayId1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldShift1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldShiftId1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
    }
}
