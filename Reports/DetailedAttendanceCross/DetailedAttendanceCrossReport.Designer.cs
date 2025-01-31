﻿namespace Reports.DetailedAttendanceCross
{
    partial class DetailedAttendanceCrossReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailedAttendanceCrossReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.dsSalaries1 = new Reports.DetailedAttendanceCross.dsSalaries();
            this.fieldemployeeName1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldbranchRef1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldfirstPunch1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldlastPunch1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldeffectiveTime1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldtimeCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldduration1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldtimeVariations = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.To = new DevExpress.XtraReports.Parameters.Parameter();
            this.From = new DevExpress.XtraReports.Parameters.Parameter();
            this.Branch = new DevExpress.XtraReports.Parameters.Parameter();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.User = new DevExpress.XtraReports.Parameters.Parameter();
            this.Employee = new DevExpress.XtraReports.Parameters.Parameter();
            this.fielddayId = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.dsSalaries1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
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
            this.xrPivotGrid1.DataMember = "SalariesItems";
            this.xrPivotGrid1.DataSource = this.dsSalaries1;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldemployeeName1,
            this.fieldbranchRef1,
            this.fieldfirstPunch1,
            this.fieldlastPunch1,
            this.fieldeffectiveTime1,
            this.fieldtimeCode1,
            this.fieldduration1,
            this.fieldtimeVariations,
            this.fielddayId});
            resources.ApplyResources(this.xrPivotGrid1, "xrPivotGrid1");
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsChartDataSource.ProvideColumnTotals = true;
            this.xrPivotGrid1.OptionsDataField.ColumnValueLineCount = 2;
            this.xrPivotGrid1.OptionsDataField.RowValueLineCount = 3;
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowRowTotals = false;
            this.xrPivotGrid1.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.grdAccountLedger_CustomFieldSort);
            // 
            // dsSalaries1
            // 
            this.dsSalaries1.DataSetName = "dsSalaries";
            this.dsSalaries1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fieldemployeeName1
            // 
            this.fieldemployeeName1.Appearance.Cell.WordWrap = true;
            this.fieldemployeeName1.Appearance.FieldHeader.WordWrap = true;
            this.fieldemployeeName1.Appearance.FieldValue.WordWrap = true;
            this.fieldemployeeName1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldemployeeName1.AreaIndex = 1;
            resources.ApplyResources(this.fieldemployeeName1, "fieldemployeeName1");
            this.fieldemployeeName1.FieldName = "employeeName";
            this.fieldemployeeName1.Name = "fieldemployeeName1";
            // 
            // fieldbranchRef1
            // 
            this.fieldbranchRef1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldbranchRef1.AreaIndex = 2;
            resources.ApplyResources(this.fieldbranchRef1, "fieldbranchRef1");
            this.fieldbranchRef1.FieldName = "branchRef";
            this.fieldbranchRef1.Name = "fieldbranchRef1";
            // 
            // fieldfirstPunch1
            // 
            this.fieldfirstPunch1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldfirstPunch1.AreaIndex = 3;
            resources.ApplyResources(this.fieldfirstPunch1, "fieldfirstPunch1");
            this.fieldfirstPunch1.FieldName = "firstPunch";
            this.fieldfirstPunch1.Name = "fieldfirstPunch1";
            // 
            // fieldlastPunch1
            // 
            this.fieldlastPunch1.Appearance.FieldValue.WordWrap = true;
            this.fieldlastPunch1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldlastPunch1.AreaIndex = 4;
            resources.ApplyResources(this.fieldlastPunch1, "fieldlastPunch1");
            this.fieldlastPunch1.FieldName = "lastPunch";
            this.fieldlastPunch1.Name = "fieldlastPunch1";
            // 
            // fieldeffectiveTime1
            // 
            this.fieldeffectiveTime1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldeffectiveTime1.AreaIndex = 5;
            resources.ApplyResources(this.fieldeffectiveTime1, "fieldeffectiveTime1");
            this.fieldeffectiveTime1.FieldName = "effectiveTime";
            this.fieldeffectiveTime1.Name = "fieldeffectiveTime1";
            // 
            // fieldtimeCode1
            // 
            this.fieldtimeCode1.Appearance.Cell.WordWrap = true;
            this.fieldtimeCode1.Appearance.FieldHeader.WordWrap = true;
            this.fieldtimeCode1.Appearance.FieldValue.WordWrap = true;
            this.fieldtimeCode1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldtimeCode1.AreaIndex = 1;
            resources.ApplyResources(this.fieldtimeCode1, "fieldtimeCode1");
            this.fieldtimeCode1.FieldName = "timeCode";
            this.fieldtimeCode1.Name = "fieldtimeCode1";
            // 
            // fieldduration1
            // 
            this.fieldduration1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldduration1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldduration1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldduration1.AreaIndex = 0;
            resources.ApplyResources(this.fieldduration1, "fieldduration1");
            this.fieldduration1.FieldName = "duration";
            this.fieldduration1.Name = "fieldduration1";
            this.fieldduration1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            // 
            // fieldtimeVariations
            // 
            this.fieldtimeVariations.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Top;
            this.fieldtimeVariations.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldtimeVariations.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldtimeVariations.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldtimeVariations.AreaIndex = 0;
            resources.ApplyResources(this.fieldtimeVariations, "fieldtimeVariations");
            this.fieldtimeVariations.FieldName = "timeVariations";
            this.fieldtimeVariations.Name = "fieldtimeVariations";
            // 
            // TopMargin
            // 
            resources.ApplyResources(this.TopMargin, "TopMargin");
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // BottomMargin
            // 
            resources.ApplyResources(this.BottomMargin, "BottomMargin");
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
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
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
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
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1,
            this.xrLabel11,
            this.xrLabel2});
            resources.ApplyResources(this.PageFooter, "PageFooter");
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo2
            // 
            resources.ApplyResources(this.xrPageInfo2, "xrPageInfo2");
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            // 
            // xrPageInfo1
            // 
            resources.ApplyResources(this.xrPageInfo1, "xrPageInfo1");
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel11
            // 
            resources.ApplyResources(this.xrLabel11, "xrLabel11");
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.StylePriority.UseBackColor = false;
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.User, "Text", "")});
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.StylePriority.UseTextAlignment = false;
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
            // fielddayId
            // 
            this.fielddayId.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fielddayId.AreaIndex = 0;
            resources.ApplyResources(this.fielddayId, "fielddayId");
            this.fielddayId.FieldName = "dayId";
            this.fielddayId.Name = "fielddayId";
            // 
            // DetailedAttendanceReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageFooter});
            this.DataMember = "DataTable1";
            this.DataSource = this.dsSalaries1;
            resources.ApplyResources(this, "$this");
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.User,
            this.Branch,
            this.From,
            this.To,
            this.Employee});
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.dsSalaries1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private dsSalaries dsSalaries1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.Parameters.Parameter Branch;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.Parameters.Parameter User;
        private DevExpress.XtraReports.Parameters.Parameter From;
        private DevExpress.XtraReports.Parameters.Parameter To;
        private DevExpress.XtraReports.Parameters.Parameter Employee;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldbranchRef1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldfirstPunch1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldlastPunch1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldemployeeName1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldeffectiveTime1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldtimeCode1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldduration1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldtimeVariations;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fielddayId;
    }
}
