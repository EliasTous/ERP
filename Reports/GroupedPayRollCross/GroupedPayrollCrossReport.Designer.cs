namespace Reports.GroupedPayRollCross
{
    partial class GroupedPayrollCrossReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupedPayrollCrossReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.dsSalaries1 = new dsSalaries();
            this.fieldItemName = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldItemValue = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSalaryDate = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCSS = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldEmployeeName = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldESS = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldBasicSalary = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldTaxableNoTaxable = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldEntitlementsvDeduction = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.Ref = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.Payment = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.Department = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.Branch = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.User = new DevExpress.XtraReports.Parameters.Parameter();
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
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.Appearance.TotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.DataMember = "SalariesItems";
            this.xrPivotGrid1.DataSource = this.dsSalaries1;
            resources.ApplyResources(this.xrPivotGrid1, "xrPivotGrid1");
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldItemName,
            this.fieldItemValue,
            this.fieldSalaryDate,
            this.fieldCSS,
            this.fieldEmployeeName,
            this.fieldESS,
            this.fieldBasicSalary,
            this.fieldTaxableNoTaxable,
            this.fieldEntitlementsvDeduction});
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsChartDataSource.ProvideColumnTotals = true;
            this.xrPivotGrid1.OptionsDataField.Caption = resources.GetString("xrPivotGrid1.OptionsDataField.Caption");
            this.xrPivotGrid1.OptionsDataField.ColumnValueLineCount = 2;
            this.xrPivotGrid1.OptionsDataField.RowValueLineCount = 2;
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.grdAccountLedger_CustomFieldSort);
            // 
            // dsSalaries1
            // 
            this.dsSalaries1.DataSetName = "dsSalaries";
            this.dsSalaries1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fieldItemName
            // 
            this.fieldItemName.Appearance.Cell.WordWrap = true;
            this.fieldItemName.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemName.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemName.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemName.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemName.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldItemName.AreaIndex = 2;
            resources.ApplyResources(this.fieldItemName, "fieldItemName");
            this.fieldItemName.FieldName = "ItemName";
            this.fieldItemName.Name = "fieldItemName";
            this.fieldItemName.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // fieldItemValue
            // 
            this.fieldItemValue.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemValue.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemValue.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemValue.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemValue.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemValue.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemValue.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldItemValue.AreaIndex = 0;
            resources.ApplyResources(this.fieldItemValue, "fieldItemValue");
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
            this.fieldSalaryDate.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldSalaryDate.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldSalaryDate.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldSalaryDate.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldSalaryDate.AreaIndex = 0;
            resources.ApplyResources(this.fieldSalaryDate, "fieldSalaryDate");
            this.fieldSalaryDate.FieldName = "SalaryDate";
            this.fieldSalaryDate.Name = "fieldSalaryDate";
            this.fieldSalaryDate.ValueFormat.FormatString = "dd/MM/yyyy";
            this.fieldSalaryDate.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // fieldCSS
            // 
            this.fieldCSS.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldCSS.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldCSS.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldCSS.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldCSS.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCSS.AreaIndex = 2;
            resources.ApplyResources(this.fieldCSS, "fieldCSS");
            this.fieldCSS.FieldName = "CSS";
            this.fieldCSS.Name = "fieldCSS";
            // 
            // fieldEmployeeName
            // 
            this.fieldEmployeeName.Appearance.Cell.WordWrap = true;
            this.fieldEmployeeName.Appearance.CustomTotalCell.WordWrap = true;
            this.fieldEmployeeName.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldEmployeeName.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldEmployeeName.Appearance.FieldHeader.WordWrap = true;
            this.fieldEmployeeName.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldEmployeeName.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldEmployeeName.Appearance.FieldValue.WordWrap = true;
            this.fieldEmployeeName.Appearance.FieldValueGrandTotal.WordWrap = true;
            this.fieldEmployeeName.Appearance.FieldValueTotal.WordWrap = true;
            this.fieldEmployeeName.Appearance.GrandTotalCell.WordWrap = true;
            this.fieldEmployeeName.Appearance.TotalCell.WordWrap = true;
            this.fieldEmployeeName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldEmployeeName.AreaIndex = 0;
            resources.ApplyResources(this.fieldEmployeeName, "fieldEmployeeName");
            this.fieldEmployeeName.FieldName = "EmployeeName";
            this.fieldEmployeeName.Name = "fieldEmployeeName";
            // 
            // fieldESS
            // 
            this.fieldESS.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldESS.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldESS.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldESS.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldESS.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldESS.AreaIndex = 3;
            resources.ApplyResources(this.fieldESS, "fieldESS");
            this.fieldESS.FieldName = "ESS";
            this.fieldESS.Name = "fieldESS";
            // 
            // fieldBasicSalary
            // 
            this.fieldBasicSalary.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldBasicSalary.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldBasicSalary.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldBasicSalary.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldBasicSalary.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldBasicSalary.AreaIndex = 1;
            resources.ApplyResources(this.fieldBasicSalary, "fieldBasicSalary");
            this.fieldBasicSalary.FieldName = "BasicSalary";
            this.fieldBasicSalary.Name = "fieldBasicSalary";
            // 
            // fieldTaxableNoTaxable
            // 
            this.fieldTaxableNoTaxable.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldTaxableNoTaxable.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldTaxableNoTaxable.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldTaxableNoTaxable.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldTaxableNoTaxable.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldTaxableNoTaxable.AreaIndex = 1;
            resources.ApplyResources(this.fieldTaxableNoTaxable, "fieldTaxableNoTaxable");
            this.fieldTaxableNoTaxable.FieldName = "TaxableNoTaxable";
            this.fieldTaxableNoTaxable.Name = "fieldTaxableNoTaxable";
            // 
            // fieldEntitlementsvDeduction
            // 
            this.fieldEntitlementsvDeduction.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldEntitlementsvDeduction.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldEntitlementsvDeduction.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldEntitlementsvDeduction.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldEntitlementsvDeduction.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldEntitlementsvDeduction.AreaIndex = 0;
            resources.ApplyResources(this.fieldEntitlementsvDeduction, "fieldEntitlementsvDeduction");
            this.fieldEntitlementsvDeduction.FieldName = "EntitlementsvDeduction";
            this.fieldEntitlementsvDeduction.Name = "fieldEntitlementsvDeduction";
            this.fieldEntitlementsvDeduction.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending;
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
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel1});
            resources.ApplyResources(this.ReportHeader, "ReportHeader");
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel10
            // 
            resources.ApplyResources(this.xrLabel10, "xrLabel10");
            this.xrLabel10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.StylePriority.UseBackColor = false;
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel9
            // 
            resources.ApplyResources(this.xrLabel9, "xrLabel9");
            this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.StylePriority.UseBackColor = false;
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel8
            // 
            resources.ApplyResources(this.xrLabel8, "xrLabel8");
            this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.StylePriority.UseBackColor = false;
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel7
            // 
            resources.ApplyResources(this.xrLabel7, "xrLabel7");
            this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.StylePriority.UseBackColor = false;
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Ref, "Text", "")});
            resources.ApplyResources(this.xrLabel6, "xrLabel6");
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            // 
            // Ref
            // 
            resources.ApplyResources(this.Ref, "Ref");
            this.Ref.Name = "Ref";
            this.Ref.Visible = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Payment, "Text", "")});
            resources.ApplyResources(this.xrLabel5, "xrLabel5");
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            // 
            // Payment
            // 
            resources.ApplyResources(this.Payment, "Payment");
            this.Payment.Name = "Payment";
            this.Payment.Visible = false;
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Department, "Text", "")});
            resources.ApplyResources(this.xrLabel4, "xrLabel4");
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            // 
            // Department
            // 
            resources.ApplyResources(this.Department, "Department");
            this.Department.Name = "Department";
            this.Department.Visible = false;
            // 
            // xrLabel3
            // 
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Branch, "Text", "")});
            resources.ApplyResources(this.xrLabel3, "xrLabel3");
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            // 
            // Branch
            // 
            resources.ApplyResources(this.Branch, "Branch");
            this.Branch.Name = "Branch";
            this.Branch.Visible = false;
            // 
            // xrLabel1
            // 
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11,
            this.xrLabel2});
            resources.ApplyResources(this.PageFooter, "PageFooter");
            this.PageFooter.Name = "PageFooter";
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
            // 
            // User
            // 
            resources.ApplyResources(this.User, "User");
            this.User.Name = "User";
            this.User.Visible = false;
            // 
            // GroupedPayrollCrossReport
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
            this.Department,
            this.User,
            this.Branch,
            this.Payment,
            this.Ref});
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
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItemName;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItemValue;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSalaryDate;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCSS;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldEmployeeName;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldESS;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldBasicSalary;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldTaxableNoTaxable;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldEntitlementsvDeduction;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.Parameters.Parameter Ref;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.Parameters.Parameter Payment;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.Parameters.Parameter Department;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.Parameters.Parameter Branch;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.Parameters.Parameter User;
    }
}
