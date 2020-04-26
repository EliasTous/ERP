using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for DailyAttendance
/// </summary>
public class DailyAttendance : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    private GroupHeaderBand groupHeaderBand1;
    private PageFooterBand pageFooterBand1;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
    private ReportHeaderBand reportHeaderBand1;
    private XRLabel xrLabel27;
    private XRControlStyle Title;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private GroupFooterBand GroupFooter1;
    private XRLine xrLine1;
    private XRLabel xrLabel21;
    private XRLabel xrLabel20;
    private XRLabel xrLabel25;
    private ReportFooterBand ReportFooter;
    private XRLabel xrLabel26;
    private XRLine xrLine3;
    private XRLabel xrLabel23;
    private XRLabel xrLabel24;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel28;
    private GroupHeaderBand GroupHeader1;
    private XRLine xrLine4;
    private GroupHeaderBand GroupHeader2;
    private XRRichText xrRichText1;
    private DevExpress.XtraReports.Parameters.Parameter From;
    private DevExpress.XtraReports.Parameters.Parameter To;
    private XRLabel xrLabel30;
    private DevExpress.XtraReports.Parameters.Parameter User;
    private XRLabel xrLabel29;
    private XRLine xrLine2;
    private XRLabel xrLabel19;
    private XRLabel xrLabel22;
    private XRLabel xrLabel32;
    private DevExpress.XtraReports.Parameters.Parameter Employee;
    private XRLabel xrLabel31;
    private XRLabel xrLabel33;
    private CalculatedField totalLate;
    private XRLabel xrLabel34;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private XRControlStyle Numbers;
    private XRLabel xrLabel37;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public DailyAttendance()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyAttendance));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary9 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary10 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.groupHeaderBand1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.pageFooterBand1 = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.User = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.Employee = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.From = new DevExpress.XtraReports.Parameters.Parameter();
            this.To = new DevExpress.XtraReports.Parameters.Parameter();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.totalLate = new DevExpress.XtraReports.UI.CalculatedField();
            this.Numbers = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel33,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StyleName = "DataField";
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
            // 
            // xrLabel33
            // 
            this.xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalLate", "{0}")});
            resources.ApplyResources(this.xrLabel33, "xrLabel33");
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel33.StyleName = "Numbers";
            this.xrLabel33.StylePriority.UseTextAlignment = false;
            this.xrLabel33.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel33_BeforePrint);
            // 
            // xrLabel9
            // 
            this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "earlyString", "{0}")});
            resources.ApplyResources(this.xrLabel9, "xrLabel9");
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.StyleName = "Numbers";
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // xrLabel8
            // 
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "latenessString", "{0}")});
            resources.ApplyResources(this.xrLabel8, "xrLabel8");
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.StyleName = "Numbers";
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DOW")});
            resources.ApplyResources(this.xrLabel7, "xrLabel7");
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Suppress;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHoursString", "{0}")});
            resources.ApplyResources(this.xrLabel6, "xrLabel6");
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.StyleName = "Numbers";
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.xrLabel6.Summary = xrSummary1;
            this.xrLabel6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "divisionName")});
            resources.ApplyResources(this.xrLabel5, "xrLabel5");
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Suppress;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DateString")});
            resources.ApplyResources(this.xrLabel4, "xrLabel4");
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Suppress;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel3
            // 
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "departmentName")});
            resources.ApplyResources(this.xrLabel3, "xrLabel3");
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Suppress;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "name")});
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Suppress;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "branchName")});
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Suppress;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            resources.ApplyResources(this.TopMargin, "TopMargin");
            // 
            // BottomMargin
            // 
            resources.ApplyResources(this.BottomMargin, "BottomMargin");
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // groupHeaderBand1
            // 
            this.groupHeaderBand1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.groupHeaderBand1, "groupHeaderBand1");
            this.groupHeaderBand1.Name = "groupHeaderBand1";
            this.groupHeaderBand1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
            // 
            // pageFooterBand1
            // 
            this.pageFooterBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel30,
            this.xrLabel29,
            this.xrPageInfo1,
            this.xrPageInfo2});
            resources.ApplyResources(this.pageFooterBand1, "pageFooterBand1");
            this.pageFooterBand1.Name = "pageFooterBand1";
            // 
            // xrLabel30
            // 
            this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.User, "Text", "")});
            resources.ApplyResources(this.xrLabel30, "xrLabel30");
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel30.StyleName = "PageInfo";
            // 
            // User
            // 
            resources.ApplyResources(this.User, "User");
            this.User.Name = "User";
            this.User.ValueInfo = "All";
            this.User.Visible = false;
            // 
            // xrLabel29
            // 
            resources.ApplyResources(this.xrLabel29, "xrLabel29");
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel29.StyleName = "PageInfo";
            // 
            // xrPageInfo1
            // 
            resources.ApplyResources(this.xrPageInfo1, "xrPageInfo1");
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.StyleName = "PageInfo";
            // 
            // xrPageInfo2
            // 
            resources.ApplyResources(this.xrPageInfo2, "xrPageInfo2");
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.StyleName = "PageInfo";
            // 
            // reportHeaderBand1
            // 
            this.reportHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel27});
            resources.ApplyResources(this.reportHeaderBand1, "reportHeaderBand1");
            this.reportHeaderBand1.Name = "reportHeaderBand1";
            // 
            // xrLabel27
            // 
            resources.ApplyResources(this.xrLabel27, "xrLabel27");
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel27.StyleName = "Title";
            // 
            // xrLabel18
            // 
            resources.ApplyResources(this.xrLabel18, "xrLabel18");
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel17
            // 
            resources.ApplyResources(this.xrLabel17, "xrLabel17");
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel16
            // 
            resources.ApplyResources(this.xrLabel16, "xrLabel16");
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel15
            // 
            resources.ApplyResources(this.xrLabel15, "xrLabel15");
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel14
            // 
            resources.ApplyResources(this.xrLabel14, "xrLabel14");
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel13
            // 
            resources.ApplyResources(this.xrLabel13, "xrLabel13");
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel12
            // 
            resources.ApplyResources(this.xrLabel12, "xrLabel12");
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel11
            // 
            resources.ApplyResources(this.xrLabel11, "xrLabel11");
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel10
            // 
            resources.ApplyResources(this.xrLabel10, "xrLabel10");
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.BorderColor = System.Drawing.Color.Black;
            this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Title.BorderWidth = 1F;
            this.Title.Font = new System.Drawing.Font("Times New Roman", 21F);
            this.Title.ForeColor = System.Drawing.Color.Black;
            this.Title.Name = "Title";
            // 
            // FieldCaption
            // 
            this.FieldCaption.BackColor = System.Drawing.Color.Transparent;
            this.FieldCaption.BorderColor = System.Drawing.Color.Black;
            this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.FieldCaption.BorderWidth = 1F;
            this.FieldCaption.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.FieldCaption.ForeColor = System.Drawing.Color.Black;
            this.FieldCaption.Name = "FieldCaption";
            // 
            // PageInfo
            // 
            this.PageInfo.BackColor = System.Drawing.Color.Transparent;
            this.PageInfo.BorderColor = System.Drawing.Color.Black;
            this.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.PageInfo.BorderWidth = 1F;
            this.PageInfo.Font = new System.Drawing.Font("Arial", 8F);
            this.PageInfo.ForeColor = System.Drawing.Color.Black;
            this.PageInfo.Name = "PageInfo";
            // 
            // DataField
            // 
            this.DataField.BackColor = System.Drawing.Color.Transparent;
            this.DataField.BorderColor = System.Drawing.Color.Black;
            this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DataField.BorderWidth = 1F;
            this.DataField.Font = new System.Drawing.Font("Arial", 9F);
            this.DataField.ForeColor = System.Drawing.Color.Black;
            this.DataField.Name = "DataField";
            this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35,
            this.xrLabel34,
            this.xrLabel25,
            this.xrLabel21,
            this.xrLabel20,
            this.xrLabel19,
            this.xrLine1});
            resources.ApplyResources(this.GroupFooter1, "GroupFooter1");
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
            // 
            // xrLabel35
            // 
            this.xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalLate")});
            resources.ApplyResources(this.xrLabel35, "xrLabel35");
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.StyleName = "Numbers";
            this.xrLabel35.StylePriority.UseFont = false;
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel35.Summary = xrSummary2;
            this.xrLabel35.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel35_SummaryGetResult);
            this.xrLabel35.SummaryReset += new System.EventHandler(this.xrLabel35_SummaryReset);
            this.xrLabel35.SummaryRowChanged += new System.EventHandler(this.xrLabel35_SummaryRowChanged);
            // 
            // xrLabel34
            // 
            this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DOW")});
            resources.ApplyResources(this.xrLabel34, "xrLabel34");
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.StyleName = "Numbers";
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel34.Summary = xrSummary3;
            // 
            // xrLabel25
            // 
            resources.ApplyResources(this.xrLabel25, "xrLabel25");
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.StylePriority.UseFont = false;
            // 
            // xrLabel21
            // 
            this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "earlyString")});
            resources.ApplyResources(this.xrLabel21, "xrLabel21");
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.StyleName = "Numbers";
            this.xrLabel21.StylePriority.UseFont = false;
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel21.Summary = xrSummary4;
            this.xrLabel21.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel21_SummaryGetResult);
            this.xrLabel21.SummaryReset += new System.EventHandler(this.xrLabel21_SummaryReset);
            this.xrLabel21.SummaryRowChanged += new System.EventHandler(this.xrLabel21_SummaryRowChanged);
            this.xrLabel21.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // xrLabel20
            // 
            this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "latenessString")});
            resources.ApplyResources(this.xrLabel20, "xrLabel20");
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.StyleName = "Numbers";
            this.xrLabel20.StylePriority.UseFont = false;
            xrSummary5.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel20.Summary = xrSummary5;
            this.xrLabel20.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel20_SummaryGetResult);
            this.xrLabel20.SummaryReset += new System.EventHandler(this.xrLabel20_SummaryReset);
            this.xrLabel20.SummaryRowChanged += new System.EventHandler(this.xrLabel20_SummaryRowChanged);
            this.xrLabel20.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // xrLabel19
            // 
            this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHoursString")});
            resources.ApplyResources(this.xrLabel19, "xrLabel19");
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.StyleName = "Numbers";
            this.xrLabel19.StylePriority.UseFont = false;
            resources.ApplyResources(xrSummary6, "xrSummary6");
            xrSummary6.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel19.Summary = xrSummary6;
            this.xrLabel19.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel19_SummaryGetResult);
            this.xrLabel19.SummaryReset += new System.EventHandler(this.xrLabel19_SummaryReset);
            this.xrLabel19.SummaryRowChanged += new System.EventHandler(this.xrLabel19_SummaryRowChanged);
            this.xrLabel19.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // xrLine1
            // 
            this.xrLine1.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            resources.ApplyResources(this.xrLine1, "xrLine1");
            this.xrLine1.Name = "xrLine1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37,
            this.xrLabel26,
            this.xrLine3,
            this.xrLabel22,
            this.xrLabel23,
            this.xrLabel24});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
            // 
            // xrLabel37
            // 
            this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totalLate")});
            resources.ApplyResources(this.xrLabel37, "xrLabel37");
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel37.StyleName = "Numbers";
            this.xrLabel37.StylePriority.UseFont = false;
            xrSummary7.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel37.Summary = xrSummary7;
            this.xrLabel37.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel37_SummaryGetResult);
            this.xrLabel37.SummaryReset += new System.EventHandler(this.xrLabel37_SummaryReset);
            this.xrLabel37.SummaryRowChanged += new System.EventHandler(this.xrLabel37_SummaryRowChanged);
            // 
            // xrLabel26
            // 
            resources.ApplyResources(this.xrLabel26, "xrLabel26");
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.StylePriority.UseFont = false;
            // 
            // xrLine3
            // 
            resources.ApplyResources(this.xrLine3, "xrLine3");
            this.xrLine3.Name = "xrLine3";
            // 
            // xrLabel22
            // 
            this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHoursString")});
            resources.ApplyResources(this.xrLabel22, "xrLabel22");
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.StyleName = "Numbers";
            this.xrLabel22.StylePriority.UseFont = false;
            xrSummary8.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel22.Summary = xrSummary8;
            this.xrLabel22.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel22_SummaryGetResult);
            this.xrLabel22.SummaryReset += new System.EventHandler(this.xrLabel22_SummaryReset);
            this.xrLabel22.SummaryRowChanged += new System.EventHandler(this.xrLabel22_SummaryRowChanged);
            this.xrLabel22.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // xrLabel23
            // 
            this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "latenessString")});
            resources.ApplyResources(this.xrLabel23, "xrLabel23");
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel23.StyleName = "Numbers";
            this.xrLabel23.StylePriority.UseFont = false;
            xrSummary9.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel23.Summary = xrSummary9;
            this.xrLabel23.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel23_SummaryGetResult);
            this.xrLabel23.SummaryReset += new System.EventHandler(this.xrLabel23_SummaryReset);
            this.xrLabel23.SummaryRowChanged += new System.EventHandler(this.xrLabel23_SummaryRowChanged);
            this.xrLabel23.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // xrLabel24
            // 
            this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "earlyString")});
            resources.ApplyResources(this.xrLabel24, "xrLabel24");
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.StyleName = "Numbers";
            this.xrLabel24.StylePriority.UseFont = false;
            xrSummary10.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel24.Summary = xrSummary10;
            this.xrLabel24.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel24_SummaryGetResult);
            this.xrLabel24.SummaryReset += new System.EventHandler(this.xrLabel24_SummaryReset);
            this.xrLabel24.SummaryRowChanged += new System.EventHandler(this.xrLabel24_SummaryRowChanged);
            this.xrLabel24.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel28});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // xrLabel28
            // 
            resources.ApplyResources(this.xrLabel28, "xrLabel28");
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36,
            this.xrLine4,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            this.GroupHeader1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
            // 
            // xrLabel36
            // 
            resources.ApplyResources(this.xrLabel36, "xrLabel36");
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel36.StylePriority.UseTextAlignment = false;
            // 
            // xrLine4
            // 
            resources.ApplyResources(this.xrLine4, "xrLine4");
            this.xrLine4.Name = "xrLine4";
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel32,
            this.xrLabel31,
            this.xrLine2,
            this.xrRichText1});
            resources.ApplyResources(this.GroupHeader2, "GroupHeader2");
            this.GroupHeader2.Level = 2;
            this.GroupHeader2.Name = "GroupHeader2";
            this.GroupHeader2.RepeatEveryPage = true;
            // 
            // xrLabel32
            // 
            this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Employee, "Text", "")});
            resources.ApplyResources(this.xrLabel32, "xrLabel32");
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // Employee
            // 
            resources.ApplyResources(this.Employee, "Employee");
            this.Employee.Name = "Employee";
            // 
            // xrLabel31
            // 
            resources.ApplyResources(this.xrLabel31, "xrLabel31");
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // xrLine2
            // 
            resources.ApplyResources(this.xrLine2, "xrLine2");
            this.xrLine2.Name = "xrLine2";
            // 
            // xrRichText1
            // 
            resources.ApplyResources(this.xrRichText1, "xrRichText1");
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.StyleName = "PageInfo";
            // 
            // From
            // 
            resources.ApplyResources(this.From, "From");
            this.From.Name = "From";
            this.From.ValueInfo = "All";
            this.From.Visible = false;
            // 
            // To
            // 
            resources.ApplyResources(this.To, "To");
            this.To.Name = "To";
            this.To.ValueInfo = "All";
            this.To.Visible = false;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(Model.Reports.DailyAttendance);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // totalLate
            // 
            this.totalLate.Expression = "[lateness] + [early]";
            this.totalLate.Name = "totalLate";
            // 
            // Numbers
            // 
            this.Numbers.Name = "Numbers";
            this.Numbers.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Numbers.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // DailyAttendance
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.groupHeaderBand1,
            this.pageFooterBand1,
            this.reportHeaderBand1,
            this.GroupFooter1,
            this.ReportFooter,
            this.PageHeader,
            this.GroupHeader1,
            this.GroupHeader2});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.totalLate});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            resources.ApplyResources(this, "$this");
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.From,
            this.To,
            this.User,
            this.Employee});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField,
            this.Numbers});
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
   
    }

    private void xrLabel21_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if(e.Text.Length>4)
        {
            e.Text = e.Text.Remove(e.Text.Length - 3, 3);
        }
    }

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        e.Cancel = RowCount > 0;
    }

    private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        e.Cancel = RowCount == 0;
    }
    int hoursWorked, minsWorked, hoursLateness, minsLateness,hoursLeave,minsLeave,empLatenessHours,allLatenessHours;
    int totalHoursWorked, totalMinsWorked,totalHoursLeave,toalMinsLeave,totalHoursLatenss,totalMinsLateness,empLatenessMins,allLatenessMins;

    private void xrLabel21_SummaryReset(object sender, EventArgs e)
    {
        hoursLeave = minsLeave = 0;
    }

    private void xrLabel21_SummaryRowChanged(object sender, EventArgs e)
    {
        string lateness = GetCurrentColumnValue("earlyString").ToString();
        if (lateness[0] == '-')
        {
            hoursLeave -= Convert.ToInt32(GetCurrentColumnValue("earlyString").ToString().Substring(1, 2));
            minsLeave -= Convert.ToInt32(GetCurrentColumnValue("earlyString").ToString().Substring(4, 2));
        }
        else
        {
            hoursLeave += Convert.ToInt32(GetCurrentColumnValue("earlyString").ToString().Substring(0, 2));
            minsLeave += Convert.ToInt32(GetCurrentColumnValue("earlyString").ToString().Substring(3, 2));
        }
    }

    private void xrLabel23_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        char sign = ' ';
        if (totalHoursLatenss < 0 || totalMinsLateness < 0)
            sign = '-';
        totalHoursLatenss += totalMinsLateness / 60;
        totalMinsLateness = totalMinsLateness % 60;
        e.Result = sign + (Math.Abs(totalHoursLatenss).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalMinsLateness).ToString().PadLeft(2, '0'));
        e.Handled = true;

        totalHoursLatenss = totalMinsLateness = 0;
    }

    private void xrLabel23_SummaryReset(object sender, EventArgs e)
    {
        totalHoursLatenss = totalMinsLateness = 0;
    }

    private void xrLabel23_SummaryRowChanged(object sender, EventArgs e)
    {
        string lateness = GetCurrentColumnValue("latenessString").ToString();

        if (lateness[0] == '-')
        {
            totalHoursLatenss -= Convert.ToInt32(GetCurrentColumnValue("latenessString").ToString().Substring(1, 2));
            totalMinsLateness -= Convert.ToInt32(GetCurrentColumnValue("latenessString").ToString().Substring(4, 2));
        }
        else
        {
            totalHoursLatenss += Convert.ToInt32(GetCurrentColumnValue("latenessString").ToString().Substring(0, 2));
            totalMinsLateness += Convert.ToInt32(GetCurrentColumnValue("latenessString").ToString().Substring(3, 2));
        }
    }

    private void xrLabel24_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        char sign = ' ';
        if (totalHoursLeave < 0 || toalMinsLeave < 0)
            sign = '-';
        totalHoursLeave += toalMinsLeave / 60;
        toalMinsLeave = toalMinsLeave % 60;
        e.Result = sign + (Math.Abs(totalHoursLeave).ToString().PadLeft(2, '0') + ":" + Math.Abs(toalMinsLeave).ToString().PadLeft(2, '0'));
        e.Handled = true;

        totalHoursLeave = toalMinsLeave = 0;
    }

    private void xrLabel24_SummaryReset(object sender, EventArgs e)
    {
        totalHoursLeave = toalMinsLeave = 0;
    }

    private void xrLabel24_SummaryRowChanged(object sender, EventArgs e)
    {
        string lateness = GetCurrentColumnValue("earlyString").ToString();
        if (lateness[0] == '-')
        {
            totalHoursLeave -= Convert.ToInt32(GetCurrentColumnValue("earlyString").ToString().Substring(1, 2));
            toalMinsLeave -= Convert.ToInt32(GetCurrentColumnValue("earlyString").ToString().Substring(4, 2));
        }
        else
        {
            totalHoursLeave += Convert.ToInt32(GetCurrentColumnValue("earlyString").ToString().Substring(0, 2));
            toalMinsLeave += Convert.ToInt32(GetCurrentColumnValue("earlyString").ToString().Substring(3, 2));
        }
    }

    private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        string org = xrLabel33.Text;
        if (xrLabel33.Text.Length > 6)
        {
            xrLabel33.Text = org.Split(':')[0];
            xrLabel33.Text +=":"+ org.Split(':')[1];
        }
    }

    private void xrLabel35_SummaryReset(object sender, EventArgs e)
    {
        empLatenessHours = empLatenessMins = 0;
    }

    private void xrLabel37_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        char sign = ' ';
        if (allLatenessHours < 0 || allLatenessMins < 0)
            sign = '-';
        allLatenessHours += allLatenessMins / 60;
        allLatenessMins = allLatenessMins % 60;
        e.Result = sign + (Math.Abs(allLatenessHours).ToString().PadLeft(2, '0') + ":" + Math.Abs(allLatenessMins).ToString().PadLeft(2, '0'));
        e.Handled = true;

        allLatenessHours = allLatenessMins = 0;
    }

    private void xrLabel37_SummaryReset(object sender, EventArgs e)
    {
        allLatenessHours = allLatenessMins = 0;
    }

    private void xrLabel37_SummaryRowChanged(object sender, EventArgs e)
    {
        string lateness = GetCurrentColumnValue("totalLate").ToString();
        if (lateness[0] == '-')
        {
            allLatenessHours -= Convert.ToInt32(GetCurrentColumnValue("totalLate").ToString().Substring(1, 2));
            allLatenessMins -= Convert.ToInt32(GetCurrentColumnValue("totalLate").ToString().Substring(4, 2));
        }
        else
        {
            allLatenessHours += Convert.ToInt32(GetCurrentColumnValue("totalLate").ToString().Substring(0, 2));
            allLatenessMins += Convert.ToInt32(GetCurrentColumnValue("totalLate").ToString().Substring(3, 2));
        }
    }

    private void xrLabel35_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        char sign = ' ';
        if (empLatenessHours < 0 || empLatenessMins < 0)
            sign = '-';
        empLatenessHours += empLatenessMins / 60;
        empLatenessMins = empLatenessMins % 60;
        e.Result = sign + (Math.Abs(empLatenessHours).ToString().PadLeft(2, '0') + ":" + Math.Abs(empLatenessMins).ToString().PadLeft(2, '0'));
        e.Handled = true;

        empLatenessHours = empLatenessMins = 0;
    }

    private void xrLabel35_SummaryRowChanged(object sender, EventArgs e)
    {
        string lateness = GetCurrentColumnValue("totalLate").ToString();
        if (lateness[0] == '-')
        {
            empLatenessHours -= Convert.ToInt32(GetCurrentColumnValue("totalLate").ToString().Substring(1, 2));
            empLatenessMins -= Convert.ToInt32(GetCurrentColumnValue("totalLate").ToString().Substring(4, 2));
        }
        else
        {
            empLatenessHours += Convert.ToInt32(GetCurrentColumnValue("totalLate").ToString().Substring(0, 2));
            empLatenessMins += Convert.ToInt32(GetCurrentColumnValue("totalLate").ToString().Substring(3, 2));
        }
    }

    private void xrLabel21_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        char sign = ' ';
        if (hoursLeave < 0 || minsLeave < 0)
            sign = '-';
        hoursLeave += minsLeave / 60;
        minsLeave = minsLeave % 60;
        e.Result = sign + (Math.Abs(hoursLeave).ToString().PadLeft(2, '0') + ":" + Math.Abs(minsLeave).ToString().PadLeft(2, '0'));
        e.Handled = true;

        hoursLeave = minsLeave = 0;
    }

    private void xrLabel20_SummaryRowChanged(object sender, EventArgs e)
    {
        string lateness = GetCurrentColumnValue("latenessString").ToString();
        if (lateness[0] == '-')
        {
            hoursLateness -= Convert.ToInt32(GetCurrentColumnValue("latenessString").ToString().Substring(1, 2));
            minsLateness -= Convert.ToInt32(GetCurrentColumnValue("latenessString").ToString().Substring(4, 2));
        }
        else
        {
            hoursLateness += Convert.ToInt32(GetCurrentColumnValue("latenessString").ToString().Substring(0, 2));
            minsLateness += Convert.ToInt32(GetCurrentColumnValue("latenessString").ToString().Substring(3, 2));
        }

    }

    private void xrLabel20_SummaryReset(object sender, EventArgs e)
    {
        hoursLateness = minsLateness = 0;
    }

    private void xrLabel20_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        char sign = ' ';
        if (hoursLateness < 0 || minsLateness < 0)
            sign = '-';
        hoursLateness += minsLateness / 60;
        minsLateness = minsLateness % 60;
        e.Result = sign+(Math.Abs(hoursLateness).ToString().PadLeft(2, '0') + ":" + Math.Abs(minsLateness).ToString().PadLeft(2, '0'));
        e.Handled = true;

        hoursLateness = minsLateness = 0;
    }

    private void xrLabel22_SummaryRowChanged(object sender, EventArgs e)
    {
        totalHoursWorked += Convert.ToInt32(GetCurrentColumnValue("workingHoursString").ToString().Substring(0, 2));
        totalMinsWorked += Convert.ToInt32(GetCurrentColumnValue("workingHoursString").ToString().Substring(3, 2));
    }

    private void xrLabel22_SummaryReset(object sender, EventArgs e)
    {
        totalHoursWorked = totalMinsWorked = 0;
    }

    private void xrLabel22_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        totalHoursWorked += totalMinsWorked / 60;
        totalMinsWorked = totalMinsWorked % 60;
        e.Result = (totalHoursWorked.ToString().PadLeft(2, '0') + ":" + totalMinsWorked.ToString().PadLeft(2, '0'));
        e.Handled = true;

        totalHoursWorked = totalMinsWorked = 0;
    }

    private void xrLabel19_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        hoursWorked += minsWorked / 60;
        minsWorked = minsWorked % 60;
        e.Result = (hoursWorked.ToString().PadLeft(2, '0') + ":" + minsWorked.ToString().PadLeft(2,'0'));
        e.Handled = true;
        
        hoursWorked = minsWorked = 0;
    }

    private void xrLabel19_SummaryRowChanged(object sender, EventArgs e)
    {
        hoursWorked += Convert.ToInt32(GetCurrentColumnValue("workingHoursString").ToString().Substring(0, 2));
        minsWorked += Convert.ToInt32(GetCurrentColumnValue("workingHoursString").ToString().Substring(3, 2));
    }

    private void xrLabel19_SummaryReset(object sender, EventArgs e)
    {
        hoursWorked = minsWorked  = 0;
    }
}
