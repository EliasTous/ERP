namespace Reports.CurrentPayroll
{
    partial class CurrentPayrollReport
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
            this.dsSalaries1 = new Reports.CurrentPayroll.dsSalaries();
            this.fieldItemName = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldItemValue = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSalaryDate = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCSS = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldEmployeeName = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldESS = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldBasicSalary = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldTaxableNoTaxable = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldEntitlementsvDeduction = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldBranch = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldRef = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDepartment = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCounrty = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.User = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.Ref = new DevExpress.XtraReports.Parameters.Parameter();
            this.Payment = new DevExpress.XtraReports.Parameters.Parameter();
            this.Position = new DevExpress.XtraReports.Parameters.Parameter();
            this.Branch = new DevExpress.XtraReports.Parameters.Parameter();
            this.Department = new DevExpress.XtraReports.Parameters.Parameter();
            this.Division = new DevExpress.XtraReports.Parameters.Parameter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo4 = new DevExpress.XtraReports.UI.XRPageInfo();
            ((System.ComponentModel.ISupportInitialize)(this.dsSalaries1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1});
            this.Detail.HeightF = 115F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseTextAlignment = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
            this.fieldItemName,
            this.fieldItemValue,
            this.fieldSalaryDate,
            this.fieldCSS,
            this.fieldEmployeeName,
            this.fieldESS,
            this.fieldBasicSalary,
            this.fieldTaxableNoTaxable,
            this.fieldEntitlementsvDeduction,
            this.fieldBranch,
            this.fieldRef,
            this.fieldDepartment,
            this.fieldCounrty});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
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
            this.xrPivotGrid1.OptionsView.ShowRowTotals = false;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(1131F, 115F);
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
            this.fieldItemName.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldItemName.AreaIndex = 2;
            this.fieldItemName.ColumnValueLineCount = 2;
            this.fieldItemName.FieldName = "ItemName";
            this.fieldItemName.Name = "fieldItemName";
            this.fieldItemName.RowValueLineCount = 2;
            this.fieldItemName.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.fieldItemName.Width = 60;
            // 
            // fieldItemValue
            // 
            this.fieldItemValue.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldItemValue.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemValue.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldItemValue.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemValue.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldItemValue.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldItemValue.AreaIndex = 0;
            this.fieldItemValue.CellFormat.FormatString = "N2";
            this.fieldItemValue.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldItemValue.FieldName = "ItemValue";
            this.fieldItemValue.Name = "fieldItemValue";
            this.fieldItemValue.ValueFormat.FormatString = "N2";
            this.fieldItemValue.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // fieldSalaryDate
            // 
            this.fieldSalaryDate.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldSalaryDate.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldSalaryDate.AreaIndex = 0;
            this.fieldSalaryDate.ColumnValueLineCount = 2;
            this.fieldSalaryDate.FieldName = "SalaryDate";
            this.fieldSalaryDate.Name = "fieldSalaryDate";
            this.fieldSalaryDate.RowValueLineCount = 2;
            this.fieldSalaryDate.ValueFormat.FormatString = "dd/MM/yyyy";
            this.fieldSalaryDate.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.fieldSalaryDate.Width = 80;
            // 
            // fieldCSS
            // 
            this.fieldCSS.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldCSS.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldCSS.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldCSS.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCSS.AreaIndex = 6;
            this.fieldCSS.CellFormat.FormatString = "N2";
            this.fieldCSS.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldCSS.FieldName = "CSS";
            this.fieldCSS.Name = "fieldCSS";
            this.fieldCSS.ValueFormat.FormatString = "N2";
            this.fieldCSS.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldCSS.Width = 50;
            // 
            // fieldEmployeeName
            // 
            this.fieldEmployeeName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldEmployeeName.AreaIndex = 1;
            this.fieldEmployeeName.FieldName = "EmployeeName";
            this.fieldEmployeeName.Name = "fieldEmployeeName";
            this.fieldEmployeeName.Width = 130;
            // 
            // fieldESS
            // 
            this.fieldESS.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldESS.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldESS.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldESS.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldESS.AreaIndex = 7;
            this.fieldESS.CellFormat.FormatString = "N2";
            this.fieldESS.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldESS.FieldName = "ESS";
            this.fieldESS.Name = "fieldESS";
            this.fieldESS.ValueFormat.FormatString = "N2";
            this.fieldESS.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldESS.Width = 50;
            // 
            // fieldBasicSalary
            // 
            this.fieldBasicSalary.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldBasicSalary.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldBasicSalary.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldBasicSalary.AreaIndex = 5;
            this.fieldBasicSalary.Caption = "Basic";
            this.fieldBasicSalary.CellFormat.FormatString = "N2";
            this.fieldBasicSalary.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldBasicSalary.FieldName = "BasicSalary";
            this.fieldBasicSalary.Name = "fieldBasicSalary";
            this.fieldBasicSalary.ValueFormat.FormatString = "N2";
            this.fieldBasicSalary.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldBasicSalary.Width = 50;
            // 
            // fieldTaxableNoTaxable
            // 
            this.fieldTaxableNoTaxable.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldTaxableNoTaxable.AreaIndex = 1;
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
            this.fieldEntitlementsvDeduction.FieldName = "EntitlementsvDeduction";
            this.fieldEntitlementsvDeduction.Name = "fieldEntitlementsvDeduction";
            this.fieldEntitlementsvDeduction.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending;
            // 
            // fieldBranch
            // 
            this.fieldBranch.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldBranch.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldBranch.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldBranch.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldBranch.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldBranch.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldBranch.Appearance.TotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldBranch.Appearance.TotalCell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldBranch.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldBranch.AreaIndex = 3;
            this.fieldBranch.FieldName = "Branch";
            this.fieldBranch.Name = "fieldBranch";
            // 
            // fieldRef
            // 
            this.fieldRef.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldRef.AreaIndex = 0;
            this.fieldRef.FieldName = "Ref";
            this.fieldRef.Name = "fieldRef";
            // 
            // fieldDepartment
            // 
            this.fieldDepartment.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDepartment.AreaIndex = 4;
            this.fieldDepartment.FieldName = "Department";
            this.fieldDepartment.Name = "fieldDepartment";
            // 
            // fieldCounrty
            // 
            this.fieldCounrty.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCounrty.AreaIndex = 2;
            this.fieldCounrty.FieldName = "Counrty";
            this.fieldCounrty.Name = "fieldCounrty";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 21F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo4,
            this.xrPageInfo3});
            this.BottomMargin.HeightF = 50.16668F;
            this.BottomMargin.LockedInUserDesigner = true;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 10, 100F);
            this.BottomMargin.StylePriority.UsePadding = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // User
            // 
            this.User.Description = "Parameter1";
            this.User.Name = "User";
            this.User.Visible = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
            this.ReportHeader.HeightF = 61.70834F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(409.8333F, 10.00001F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(167.7083F, 36.54167F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Current Payroll";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Ref
            // 
            this.Ref.Name = "Ref";
            this.Ref.Visible = false;
            // 
            // Payment
            // 
            this.Payment.Description = "Parameter1";
            this.Payment.Name = "Payment";
            this.Payment.Visible = false;
            // 
            // Position
            // 
            this.Position.Name = "Position";
            // 
            // Branch
            // 
            this.Branch.Description = "Parameter1";
            this.Branch.Name = "Branch";
            this.Branch.Visible = false;
            // 
            // Department
            // 
            this.Department.Description = "Parameter1";
            this.Department.Name = "Department";
            this.Department.Visible = false;
            // 
            // Division
            // 
            this.Division.Name = "Division";
            this.Division.Visible = false;
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 44.62499F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPageInfo3
            // 
            this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
            this.xrPageInfo3.Name = "xrPageInfo3";
            this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo3.SizeF = new System.Drawing.SizeF(191.6666F, 23F);
            // 
            // xrPageInfo4
            // 
            this.xrPageInfo4.LocationFloat = new DevExpress.Utils.PointFloat(1041F, 0F);
            this.xrPageInfo4.Name = "xrPageInfo4";
            this.xrPageInfo4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrPageInfo4.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrPageInfo4.TextFormatString = "Page {0} of {1}";
            // 
            // CurrentPayrollReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader});
            this.DataMember = "DataTable1";
            this.DataSource = this.dsSalaries1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(11, 7, 21, 50);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.Department,
            this.User,
            this.Branch,
            this.Payment,
            this.Ref,
            this.Position,
            this.Division});
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
        private DevExpress.XtraReports.Parameters.Parameter Ref;
        private DevExpress.XtraReports.Parameters.Parameter Payment;
        private DevExpress.XtraReports.Parameters.Parameter Department;
        private DevExpress.XtraReports.Parameters.Parameter Branch;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.Parameters.Parameter User;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldBranch;
        private DevExpress.XtraReports.Parameters.Parameter Position;
        private DevExpress.XtraReports.Parameters.Parameter Division;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldRef;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDepartment;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCounrty;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo4;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo3;
    }
}