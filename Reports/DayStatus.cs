using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using DevExpress.XtraPrinting;

/// <summary>
/// Summary description for DetailedAttendance
/// </summary>
public class DayStatus : XtraReport
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
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel7;
    private XRLabel xrLabel8;
    private XRLabel xrLabel5;
    private XRLabel xrLabel6;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel36;
    private XRLabel xrLabel37;
    private XRLabel xrLabel38;
    private XRLabel xrLabel39;
    private XRLabel xrLabel32;
    private XRLabel xrLabel33;
    private XRLabel xrLabel34;
    private XRLabel xrLabel35;
    private XRLabel xrLabel30;
    private XRLabel xrLabel31;
    private XRLabel xrLabel29;
    private XRLabel xrLabel28;
    private XRControlStyle centeredTitle;
    private ReportFooterBand ReportFooter;
    private XRLabel totalWorkingHoursLabel;
    private XRLabel totalLatenessLabel;
    private XRLabel unapprovedAbsenseLabel;
    private XRLabel xrLabel47;
    private XRLabel holidays;
    private XRLabel xrLabel43;
    private XRLabel xrLabel44;
    private XRLabel approvedAbsenseLabel;
    private XRLabel workingDays;
    private XRLabel xrLabel41;
    private XRLabel xrLabel26;
    private XRLabel xrLabel25;
    private XRLabel xrLabel48;
    private XRLabel xrLabel51;
    private XRLabel xrLabel52;
    private XRLabel xrLabel56;
    private XRLabel xrLabel57;
    private XRLabel xrLabel58;
    private XRLabel xrLabel76;
    private XRLabel xrLabel75;
    private XRLabel xrLabel49;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell13;
    private CalculatedField latenessDay;
    private DevExpress.XtraReports.Parameters.Parameter User;
    private DevExpress.XtraReports.Parameters.Parameter Branch;
    private DevExpress.XtraReports.Parameters.Parameter Division;
    private DevExpress.XtraReports.Parameters.Parameter From;
    private DevExpress.XtraReports.Parameters.Parameter To;
    private DevExpress.XtraReports.Parameters.Parameter Employee;
    private DevExpress.XtraReports.Parameters.Parameter Department;
    private DevExpress.XtraReports.Parameters.Parameter AllowedLateness;
    private XRLabel xrLabel20;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRLabel xrLabel21;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;
    //private XRLabel SpecialTaskLbl;
    //private XRLabel JobTaskLbl;
    private XRLabel approvedAbsenseLbl;
    private XRLabel xrLabel24;
    private XRLabel xrLabel61;
    private XRLabel xrLabel59;
    private XRLabel xrLabel55;
    private XRLabel xrLabel46;
    private XRLabel xrLabel42;
    private XRLabel xrLabel40;
    private XRLabel xrLabel15;
    private XRLabel xrLabel66;
    private XRLabel xrLabel65;
    private XRTableCell xrTableCell3;
    private XRLabel xrLabel14;
    private XRLabel xrLabel18;
    private XRLabel xrLabel64;
    private XRTableCell xrTableCell15;
    private XRLabel xrLabel67;
    private XRLabel xrLabel62;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell16;
    private XRLabel xrLabel19;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel13;
    private XRLabel xrLabel45;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRTableCell xrTableCell18;
    private XRLabel xrLabel50;
    private ReportFooterBand reportFooterBand1;
    private XRLabel xrLabel74;
    private XRLabel xrLabel94;
    private XRLabel xrLabel113;
    private XRLabel xrLabel131;
    private XRLabel xrLabel134;
    private XRLabel SpecialTaskLbl;
    private XRLabel JobTaskLbl;
    private XRLabel xrLabel130;
    private XRLabel xrLabel98;
    private XRLabel xrLabel127;
    private XRTableCell xrTableCell19;
    private XRLabel xrLabel53;
    private XRLabel xrLabel68;
    private XRTableCell xrTableCell20;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell29;
    private XRLabel xrLabel70;
    private XRLabel xrLabel72;
    private XRControlStyle xrControlStyle1;
    private TopMarginBand topMarginBand1;
    private XRLabel xrLabel79;
    private ReportHeaderBand reportHeaderBand2;
    private XRLabel xrLabel122;
    private XRLabel xrLabel85;
    private XRLabel xrLabel117;
    private XRLabel xrLabel137;
    private XRLabel xrLabel91;
    private XRLabel xrLabel100;
    private XRLabel xrLabel80;
    private XRLabel xrLabel81;
    private XRLabel xrLabel82;
    private XRLabel xrLabel84;
    private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource2;
    private XRControlStyle xrControlStyle2;
    private XRControlStyle xrControlStyle3;
    private DetailBand detailBand1;
    private XRControlStyle xrControlStyle4;
    private XRLabel xrLabel101;
    private BottomMarginBand bottomMarginBand1;
    private GroupHeaderBand groupHeaderBand2;
    private XRLabel xrLabel114;
    private XRLabel xrLabel123;
    private XRLabel xrLabel106;
    private XRLabel xrLabel110;
    private XRLabel xrLabel112;
    private XRLabel xrLabel129;
    private XRLabel xrLabel120;
    private XRLabel xrLabel133;
    private XRLabel xrLabel108;
    private XRLabel xrLabel128;
    private PageFooterBand pageFooterBand2;
    private XRLabel xrLabel111;
    private XRPageInfo xrPageInfo4;
    private XRPageInfo xrPageInfo3;
    private CalculatedField calculatedField1;
    private PageHeaderBand pageHeaderBand1;
    private XRControlStyle xrControlStyle5;
    private DevExpress.XtraReports.Parameters.Parameter FromParameter;
    private DevExpress.XtraReports.Parameters.Parameter ToParameter;
    private DevExpress.XtraReports.Parameters.Parameter UserParameter;
    private XRLabel xrLabel125;
    private XRLabel xrLabel116;
    private XRLabel xrLabel109;
    private XRLabel xrLabel87;
    private XRLabel xrLabel69;
    private XRLabel xrLabel54;
    private DevExpress.XtraReports.Parameters.Parameter dayStatusParameter;
    private DevExpress.XtraReports.Parameters.Parameter punchStatus;
    private DevExpress.XtraReports.Parameters.Parameter DepartmentName;
    private GroupHeaderBand GroupHeader1;
    private XRTableCell xrTableCell35;
    private XRLabel xrLabel60;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public DayStatus()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DayStatus));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            this.reportFooterBand1 = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel74 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel94 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel113 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel131 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel134 = new DevExpress.XtraReports.UI.XRLabel();
            this.SpecialTaskLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.JobTaskLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel130 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel98 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel127 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel68 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel70 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel72 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel79 = new DevExpress.XtraReports.UI.XRLabel();
            this.reportHeaderBand2 = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel125 = new DevExpress.XtraReports.UI.XRLabel();
            this.punchStatus = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel116 = new DevExpress.XtraReports.UI.XRLabel();
            this.ToParameter = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel109 = new DevExpress.XtraReports.UI.XRLabel();
            this.DepartmentName = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel87 = new DevExpress.XtraReports.UI.XRLabel();
            this.FromParameter = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel69 = new DevExpress.XtraReports.UI.XRLabel();
            this.dayStatusParameter = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel122 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel85 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel117 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel137 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel91 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel80 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel100 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel81 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel82 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel84 = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource2 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrControlStyle4 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrLabel101 = new DevExpress.XtraReports.UI.XRLabel();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.groupHeaderBand2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel114 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel123 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel106 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel110 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel112 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel129 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel120 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel133 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel108 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel128 = new DevExpress.XtraReports.UI.XRLabel();
            this.pageFooterBand2 = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.UserParameter = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel111 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo4 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
            this.pageHeaderBand1 = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrControlStyle5 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // reportFooterBand1
            // 
            this.reportFooterBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel74,
            this.xrLabel94,
            this.xrLabel113,
            this.xrLabel131,
            this.xrLabel134,
            this.SpecialTaskLbl,
            this.JobTaskLbl,
            this.xrLabel130,
            this.xrLabel98,
            this.xrLabel127});
            resources.ApplyResources(this.reportFooterBand1, "reportFooterBand1");
            this.reportFooterBand1.Name = "reportFooterBand1";
            this.reportFooterBand1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.reportFooterBand1_BeforePrint);
            // 
            // xrLabel74
            // 
            this.xrLabel74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lateness_D")});
            resources.ApplyResources(this.xrLabel74, "xrLabel74");
            this.xrLabel74.Name = "xrLabel74";
            this.xrLabel74.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel74.StylePriority.UseFont = false;
            this.xrLabel74.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel74.Summary = xrSummary1;
            this.xrLabel74.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel45_SummaryGetResult);
            this.xrLabel74.SummaryReset += new System.EventHandler(this.xrLabel45_SummaryReset);
            this.xrLabel74.SummaryRowChanged += new System.EventHandler(this.xrLabel45_SummaryRowChanged);
            // 
            // xrLabel94
            // 
            this.xrLabel94.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lateness_B")});
            resources.ApplyResources(this.xrLabel94, "xrLabel94");
            this.xrLabel94.Name = "xrLabel94";
            this.xrLabel94.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel94.StylePriority.UseFont = false;
            this.xrLabel94.StylePriority.UseTextAlignment = false;
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel94.Summary = xrSummary2;
            this.xrLabel94.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel23_SummaryGetResult);
            this.xrLabel94.SummaryReset += new System.EventHandler(this.xrLabel23_SummaryReset);
            this.xrLabel94.SummaryRowChanged += new System.EventHandler(this.xrLabel23_SummaryRowChanged);
            // 
            // xrLabel113
            // 
            this.xrLabel113.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lateness_A")});
            resources.ApplyResources(this.xrLabel113, "xrLabel113");
            this.xrLabel113.Name = "xrLabel113";
            this.xrLabel113.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel113.StylePriority.UseFont = false;
            this.xrLabel113.StylePriority.UseTextAlignment = false;
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel113.Summary = xrSummary3;
            this.xrLabel113.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel22_SummaryGetResult);
            this.xrLabel113.SummaryReset += new System.EventHandler(this.xrLabel22_SummaryReset);
            this.xrLabel113.SummaryRowChanged += new System.EventHandler(this.xrLabel22_SummaryRowChanged);
            // 
            // xrLabel131
            // 
            this.xrLabel131.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "overtime")});
            resources.ApplyResources(this.xrLabel131, "xrLabel131");
            this.xrLabel131.Name = "xrLabel131";
            this.xrLabel131.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel131.StylePriority.UseFont = false;
            this.xrLabel131.StylePriority.UseTextAlignment = false;
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel131.Summary = xrSummary4;
            this.xrLabel131.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.xrLabel64_SummaryCalculated);
            this.xrLabel131.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel64_SummaryGetResult_1);
            this.xrLabel131.SummaryReset += new System.EventHandler(this.xrLabel64_SummaryReset_1);
            this.xrLabel131.SummaryRowChanged += new System.EventHandler(this.xrLabel64_SummaryRowChanged_1);
            this.xrLabel131.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell3_BeforePrint);
            // 
            // xrLabel134
            // 
            this.xrLabel134.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel134, "xrLabel134");
            this.xrLabel134.Name = "xrLabel134";
            this.xrLabel134.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel134.StylePriority.UseBorders = false;
            this.xrLabel134.StylePriority.UseFont = false;
            // 
            // SpecialTaskLbl
            // 
            this.SpecialTaskLbl.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.SpecialTaskLbl.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "specialTasks")});
            resources.ApplyResources(this.SpecialTaskLbl, "SpecialTaskLbl");
            this.SpecialTaskLbl.Name = "SpecialTaskLbl";
            this.SpecialTaskLbl.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.SpecialTaskLbl.StylePriority.UseBorders = false;
            this.SpecialTaskLbl.StylePriority.UseFont = false;
            this.SpecialTaskLbl.StylePriority.UseTextAlignment = false;
            xrSummary5.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.SpecialTaskLbl.Summary = xrSummary5;
            this.SpecialTaskLbl.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.SpecialTaskLbl_SummaryGetResult);
            this.SpecialTaskLbl.SummaryReset += new System.EventHandler(this.SpecialTaskLbl_SummaryReset);
            this.SpecialTaskLbl.SummaryRowChanged += new System.EventHandler(this.SpecialTaskLbl_SummaryRowChanged);
            // 
            // JobTaskLbl
            // 
            this.JobTaskLbl.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.JobTaskLbl.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "jobTasks")});
            resources.ApplyResources(this.JobTaskLbl, "JobTaskLbl");
            this.JobTaskLbl.Name = "JobTaskLbl";
            this.JobTaskLbl.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.JobTaskLbl.StylePriority.UseBorders = false;
            this.JobTaskLbl.StylePriority.UseFont = false;
            this.JobTaskLbl.StylePriority.UseTextAlignment = false;
            xrSummary6.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.JobTaskLbl.Summary = xrSummary6;
            this.JobTaskLbl.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.JobTaskLbl_SummaryGetResult);
            this.JobTaskLbl.SummaryReset += new System.EventHandler(this.JobTaskLbl_SummaryReset);
            this.JobTaskLbl.SummaryRowChanged += new System.EventHandler(this.JobTaskLbl_SummaryRowChanged);
            // 
            // xrLabel130
            // 
            this.xrLabel130.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel130, "xrLabel130");
            this.xrLabel130.Name = "xrLabel130";
            this.xrLabel130.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel130.StylePriority.UseBorders = false;
            this.xrLabel130.StylePriority.UseFont = false;
            // 
            // xrLabel98
            // 
            this.xrLabel98.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel98.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHours")});
            resources.ApplyResources(this.xrLabel98, "xrLabel98");
            this.xrLabel98.Name = "xrLabel98";
            this.xrLabel98.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel98.StylePriority.UseBorders = false;
            this.xrLabel98.StylePriority.UseFont = false;
            this.xrLabel98.StylePriority.UseTextAlignment = false;
            xrSummary7.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel98.Summary = xrSummary7;
            this.xrLabel98.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.totalWorkingHoursLabel_SummaryGetResult);
            this.xrLabel98.SummaryReset += new System.EventHandler(this.totalWorkingHoursLabel_SummaryReset);
            this.xrLabel98.SummaryRowChanged += new System.EventHandler(this.totalWorkingHoursLabel_SummaryRowChanged);
            // 
            // xrLabel127
            // 
            this.xrLabel127.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel127.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lateness")});
            resources.ApplyResources(this.xrLabel127, "xrLabel127");
            this.xrLabel127.Name = "xrLabel127";
            this.xrLabel127.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel127.StylePriority.UseBorders = false;
            this.xrLabel127.StylePriority.UseFont = false;
            this.xrLabel127.StylePriority.UseTextAlignment = false;
            xrSummary8.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel127.Summary = xrSummary8;
            this.xrLabel127.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.totalLatenessLabel_SummaryGetResult);
            this.xrLabel127.SummaryReset += new System.EventHandler(this.totalLatenessLabel_SummaryReset);
            this.xrLabel127.SummaryRowChanged += new System.EventHandler(this.totalLatenessLabel_SummaryRowChanged);
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkIn2")});
            resources.ApplyResources(this.xrTableCell19, "xrTableCell19");
            this.xrTableCell19.Name = "xrTableCell19";
            // 
            // xrLabel53
            // 
            this.xrLabel53.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel53, "xrLabel53");
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel53.StylePriority.UseBorders = false;
            this.xrLabel53.StylePriority.UseFont = false;
            this.xrLabel53.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel68
            // 
            this.xrLabel68.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel68, "xrLabel68");
            this.xrLabel68.Name = "xrLabel68";
            this.xrLabel68.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel68.StylePriority.UseBorders = false;
            this.xrLabel68.StylePriority.UseFont = false;
            this.xrLabel68.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dateString")});
            resources.ApplyResources(this.xrTableCell20, "xrTableCell20");
            this.xrTableCell20.Name = "xrTableCell20";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTable2, "xrTable2");
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCell31,
            this.xrTableCell20,
            this.xrTableCell32,
            this.xrTableCell30,
            this.xrTableCell19,
            this.xrTableCell27,
            this.xrTableCell25,
            this.xrTableCell34,
            this.xrTableCell23,
            this.xrTableCell33,
            this.xrTableCell36,
            this.xrTableCell26,
            this.xrTableCell24,
            this.xrTableCell21,
            this.xrTableCell28,
            this.xrTableCell22,
            this.xrTableCell29});
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            this.xrTableRow2.Name = "xrTableRow2";
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "employeeName.firstName")});
            resources.ApplyResources(this.xrTableCell35, "xrTableCell35");
            this.xrTableCell35.Name = "xrTableCell35";
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dowString")});
            resources.ApplyResources(this.xrTableCell31, "xrTableCell31");
            this.xrTableCell31.Name = "xrTableCell31";
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkIn1")});
            resources.ApplyResources(this.xrTableCell32, "xrTableCell32");
            this.xrTableCell32.Name = "xrTableCell32";
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkOut1")});
            resources.ApplyResources(this.xrTableCell30, "xrTableCell30");
            this.xrTableCell30.Name = "xrTableCell30";
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkOut2")});
            resources.ApplyResources(this.xrTableCell27, "xrTableCell27");
            this.xrTableCell27.Name = "xrTableCell27";
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkIn3")});
            resources.ApplyResources(this.xrTableCell25, "xrTableCell25");
            this.xrTableCell25.Name = "xrTableCell25";
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkOut3")});
            resources.ApplyResources(this.xrTableCell34, "xrTableCell34");
            this.xrTableCell34.Name = "xrTableCell34";
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHours")});
            resources.ApplyResources(this.xrTableCell23, "xrTableCell23");
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "scName")});
            resources.ApplyResources(this.xrTableCell33, "xrTableCell33");
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "jobTasks")});
            resources.ApplyResources(this.xrTableCell36, "xrTableCell36");
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseBorders = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "specialTasks")});
            resources.ApplyResources(this.xrTableCell26, "xrTableCell26");
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseBorders = false;
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lateness_A")});
            resources.ApplyResources(this.xrTableCell24, "xrTableCell24");
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseBorders = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lateness_B")});
            resources.ApplyResources(this.xrTableCell21, "xrTableCell21");
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lateness_D")});
            resources.ApplyResources(this.xrTableCell28, "xrTableCell28");
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseBorders = false;
            this.xrTableCell28.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "lateness")});
            resources.ApplyResources(this.xrTableCell22, "xrTableCell22");
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "overtime")});
            resources.ApplyResources(this.xrTableCell29, "xrTableCell29");
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel70
            // 
            this.xrLabel70.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel70, "xrLabel70");
            this.xrLabel70.Name = "xrLabel70";
            this.xrLabel70.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel70.StylePriority.UseBorders = false;
            this.xrLabel70.StylePriority.UseFont = false;
            this.xrLabel70.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel72
            // 
            this.xrLabel72.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel72, "xrLabel72");
            this.xrLabel72.Name = "xrLabel72";
            this.xrLabel72.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel72.StylePriority.UseBorders = false;
            this.xrLabel72.StylePriority.UseFont = false;
            this.xrLabel72.StylePriority.UseTextAlignment = false;
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.BackColor = System.Drawing.Color.Transparent;
            this.xrControlStyle1.BorderColor = System.Drawing.Color.Black;
            this.xrControlStyle1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrControlStyle1.BorderWidth = 1F;
            this.xrControlStyle1.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrControlStyle1.ForeColor = System.Drawing.Color.Black;
            this.xrControlStyle1.Name = "xrControlStyle1";
            // 
            // topMarginBand1
            // 
            resources.ApplyResources(this.topMarginBand1, "topMarginBand1");
            this.topMarginBand1.Name = "topMarginBand1";
            this.topMarginBand1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrLabel79
            // 
            this.xrLabel79.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel79, "xrLabel79");
            this.xrLabel79.Name = "xrLabel79";
            this.xrLabel79.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel79.StylePriority.UseBorders = false;
            this.xrLabel79.StylePriority.UseFont = false;
            this.xrLabel79.StylePriority.UseTextAlignment = false;
            // 
            // reportHeaderBand2
            // 
            this.reportHeaderBand2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel125,
            this.xrLabel116,
            this.xrLabel109,
            this.xrLabel87,
            this.xrLabel69,
            this.xrLabel122,
            this.xrLabel85,
            this.xrLabel117,
            this.xrLabel137,
            this.xrLabel91,
            this.xrLabel80});
            resources.ApplyResources(this.reportHeaderBand2, "reportHeaderBand2");
            this.reportHeaderBand2.Name = "reportHeaderBand2";
            // 
            // xrLabel125
            // 
            this.xrLabel125.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.punchStatus, "Text", "")});
            resources.ApplyResources(this.xrLabel125, "xrLabel125");
            this.xrLabel125.Name = "xrLabel125";
            this.xrLabel125.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // punchStatus
            // 
            this.punchStatus.Name = "punchStatus";
            this.punchStatus.Visible = false;
            // 
            // xrLabel116
            // 
            this.xrLabel116.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.ToParameter, "Text", "")});
            resources.ApplyResources(this.xrLabel116, "xrLabel116");
            this.xrLabel116.Name = "xrLabel116";
            this.xrLabel116.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // ToParameter
            // 
            this.ToParameter.Name = "ToParameter";
            this.ToParameter.Visible = false;
            // 
            // xrLabel109
            // 
            this.xrLabel109.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.DepartmentName, "Text", "")});
            resources.ApplyResources(this.xrLabel109, "xrLabel109");
            this.xrLabel109.Name = "xrLabel109";
            this.xrLabel109.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // DepartmentName
            // 
            this.DepartmentName.Name = "DepartmentName";
            this.DepartmentName.Visible = false;
            // 
            // xrLabel87
            // 
            this.xrLabel87.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.FromParameter, "Text", "")});
            resources.ApplyResources(this.xrLabel87, "xrLabel87");
            this.xrLabel87.Name = "xrLabel87";
            this.xrLabel87.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // FromParameter
            // 
            this.FromParameter.Name = "FromParameter";
            this.FromParameter.Visible = false;
            // 
            // xrLabel69
            // 
            this.xrLabel69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.dayStatusParameter, "Text", "")});
            resources.ApplyResources(this.xrLabel69, "xrLabel69");
            this.xrLabel69.Name = "xrLabel69";
            this.xrLabel69.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // dayStatusParameter
            // 
            this.dayStatusParameter.Name = "dayStatusParameter";
            this.dayStatusParameter.Visible = false;
            // 
            // xrLabel122
            // 
            this.xrLabel122.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel122, "xrLabel122");
            this.xrLabel122.Multiline = true;
            this.xrLabel122.Name = "xrLabel122";
            this.xrLabel122.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel122.StylePriority.UseBorders = false;
            // 
            // xrLabel85
            // 
            this.xrLabel85.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel85, "xrLabel85");
            this.xrLabel85.Name = "xrLabel85";
            this.xrLabel85.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel85.StylePriority.UseBorders = false;
            // 
            // xrLabel117
            // 
            this.xrLabel117.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel117, "xrLabel117");
            this.xrLabel117.Name = "xrLabel117";
            this.xrLabel117.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel117.StylePriority.UseBorders = false;
            // 
            // xrLabel137
            // 
            this.xrLabel137.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel137, "xrLabel137");
            this.xrLabel137.Name = "xrLabel137";
            this.xrLabel137.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel137.StylePriority.UseBorders = false;
            // 
            // xrLabel91
            // 
            this.xrLabel91.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel91, "xrLabel91");
            this.xrLabel91.Name = "xrLabel91";
            this.xrLabel91.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel91.StylePriority.UseBorders = false;
            // 
            // xrLabel80
            // 
            this.xrLabel80.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Double;
            this.xrLabel80.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel80, "xrLabel80");
            this.xrLabel80.Name = "xrLabel80";
            this.xrLabel80.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel80.StylePriority.UseBorderDashStyle = false;
            this.xrLabel80.StylePriority.UseBorders = false;
            this.xrLabel80.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel100
            // 
            resources.ApplyResources(this.xrLabel100, "xrLabel100");
            this.xrLabel100.Name = "xrLabel100";
            this.xrLabel100.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel100.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel81
            // 
            resources.ApplyResources(this.xrLabel81, "xrLabel81");
            this.xrLabel81.Name = "xrLabel81";
            this.xrLabel81.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel81.StylePriority.UseFont = false;
            this.xrLabel81.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel82
            // 
            resources.ApplyResources(this.xrLabel82, "xrLabel82");
            this.xrLabel82.Name = "xrLabel82";
            this.xrLabel82.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel82.StylePriority.UseFont = false;
            this.xrLabel82.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel84
            // 
            this.xrLabel84.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel84, "xrLabel84");
            this.xrLabel84.Name = "xrLabel84";
            this.xrLabel84.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel84.StylePriority.UseBorders = false;
            this.xrLabel84.StylePriority.UseFont = false;
            this.xrLabel84.StylePriority.UseTextAlignment = false;
            // 
            // objectDataSource2
            // 
            this.objectDataSource2.DataSource = typeof(AionHR.Model.Reports.RT306);
            this.objectDataSource2.Name = "objectDataSource2";
            // 
            // xrControlStyle2
            // 
            this.xrControlStyle2.BackColor = System.Drawing.Color.Transparent;
            this.xrControlStyle2.BorderColor = System.Drawing.Color.Black;
            this.xrControlStyle2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrControlStyle2.BorderWidth = 1F;
            this.xrControlStyle2.Font = new System.Drawing.Font("Arial", 8F);
            this.xrControlStyle2.ForeColor = System.Drawing.Color.Black;
            this.xrControlStyle2.Name = "xrControlStyle2";
            // 
            // xrControlStyle3
            // 
            this.xrControlStyle3.BackColor = System.Drawing.Color.Transparent;
            this.xrControlStyle3.BorderColor = System.Drawing.Color.Black;
            this.xrControlStyle3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrControlStyle3.BorderWidth = 1F;
            this.xrControlStyle3.Font = new System.Drawing.Font("Arial", 9F);
            this.xrControlStyle3.ForeColor = System.Drawing.Color.Black;
            this.xrControlStyle3.Name = "xrControlStyle3";
            this.xrControlStyle3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // detailBand1
            // 
            this.detailBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            resources.ApplyResources(this.detailBand1, "detailBand1");
            this.detailBand1.Name = "detailBand1";
            this.detailBand1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.detailBand1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.detailBand1_BeforePrint);
            // 
            // xrControlStyle4
            // 
            this.xrControlStyle4.Name = "xrControlStyle4";
            this.xrControlStyle4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel101
            // 
            this.xrLabel101.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel101, "xrLabel101");
            this.xrLabel101.Name = "xrLabel101";
            this.xrLabel101.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel101.StylePriority.UseBorders = false;
            this.xrLabel101.StylePriority.UseFont = false;
            this.xrLabel101.StylePriority.UseTextAlignment = false;
            // 
            // bottomMarginBand1
            // 
            resources.ApplyResources(this.bottomMarginBand1, "bottomMarginBand1");
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            this.bottomMarginBand1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // groupHeaderBand2
            // 
            this.groupHeaderBand2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel60,
            this.xrLabel114,
            this.xrLabel81,
            this.xrLabel123,
            this.xrLabel106,
            this.xrLabel82,
            this.xrLabel53,
            this.xrLabel110,
            this.xrLabel112,
            this.xrLabel79,
            this.xrLabel129,
            this.xrLabel70,
            this.xrLabel120,
            this.xrLabel133,
            this.xrLabel84,
            this.xrLabel108,
            this.xrLabel101,
            this.xrLabel72,
            this.xrLabel128,
            this.xrLabel68});
            resources.ApplyResources(this.groupHeaderBand2, "groupHeaderBand2");
            this.groupHeaderBand2.Name = "groupHeaderBand2";
            this.groupHeaderBand2.RepeatEveryPage = true;
            // 
            // xrLabel60
            // 
            resources.ApplyResources(this.xrLabel60, "xrLabel60");
            this.xrLabel60.Name = "xrLabel60";
            this.xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel60.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel114
            // 
            this.xrLabel114.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel114, "xrLabel114");
            this.xrLabel114.Multiline = true;
            this.xrLabel114.Name = "xrLabel114";
            this.xrLabel114.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel114.StylePriority.UseBorders = false;
            this.xrLabel114.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel123
            // 
            resources.ApplyResources(this.xrLabel123, "xrLabel123");
            this.xrLabel123.Name = "xrLabel123";
            this.xrLabel123.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel123.StylePriority.UseFont = false;
            this.xrLabel123.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel106
            // 
            resources.ApplyResources(this.xrLabel106, "xrLabel106");
            this.xrLabel106.Name = "xrLabel106";
            this.xrLabel106.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel106.StylePriority.UseFont = false;
            this.xrLabel106.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel110
            // 
            this.xrLabel110.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel110, "xrLabel110");
            this.xrLabel110.Name = "xrLabel110";
            this.xrLabel110.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel110.StylePriority.UseBorders = false;
            this.xrLabel110.StylePriority.UseFont = false;
            this.xrLabel110.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel112
            // 
            this.xrLabel112.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel112, "xrLabel112");
            this.xrLabel112.Name = "xrLabel112";
            this.xrLabel112.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel112.StylePriority.UseBorders = false;
            this.xrLabel112.StylePriority.UseFont = false;
            this.xrLabel112.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel129
            // 
            this.xrLabel129.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel129, "xrLabel129");
            this.xrLabel129.Name = "xrLabel129";
            this.xrLabel129.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel129.StylePriority.UseBorders = false;
            this.xrLabel129.StylePriority.UseFont = false;
            this.xrLabel129.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel120
            // 
            this.xrLabel120.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel120, "xrLabel120");
            this.xrLabel120.Name = "xrLabel120";
            this.xrLabel120.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel120.StylePriority.UseBorders = false;
            this.xrLabel120.StylePriority.UseFont = false;
            this.xrLabel120.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel133
            // 
            this.xrLabel133.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel133, "xrLabel133");
            this.xrLabel133.Name = "xrLabel133";
            this.xrLabel133.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel133.StylePriority.UseBorders = false;
            this.xrLabel133.StylePriority.UseFont = false;
            this.xrLabel133.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel108
            // 
            this.xrLabel108.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel108, "xrLabel108");
            this.xrLabel108.Name = "xrLabel108";
            this.xrLabel108.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel108.StylePriority.UseBorders = false;
            this.xrLabel108.StylePriority.UseFont = false;
            this.xrLabel108.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel128
            // 
            this.xrLabel128.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel128, "xrLabel128");
            this.xrLabel128.Name = "xrLabel128";
            this.xrLabel128.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel128.StylePriority.UseBorders = false;
            this.xrLabel128.StylePriority.UseFont = false;
            this.xrLabel128.StylePriority.UseTextAlignment = false;
            // 
            // pageFooterBand2
            // 
            this.pageFooterBand2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel54,
            this.xrLabel111,
            this.xrPageInfo4,
            this.xrPageInfo3});
            resources.ApplyResources(this.pageFooterBand2, "pageFooterBand2");
            this.pageFooterBand2.Name = "pageFooterBand2";
            // 
            // xrLabel54
            // 
            this.xrLabel54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.UserParameter, "Text", "")});
            resources.ApplyResources(this.xrLabel54, "xrLabel54");
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel54.StylePriority.UseTextAlignment = false;
            // 
            // UserParameter
            // 
            this.UserParameter.Name = "UserParameter";
            this.UserParameter.Visible = false;
            // 
            // xrLabel111
            // 
            this.xrLabel111.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel111, "xrLabel111");
            this.xrLabel111.Name = "xrLabel111";
            this.xrLabel111.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel111.StylePriority.UseBorders = false;
            this.xrLabel111.StylePriority.UseTextAlignment = false;
            // 
            // xrPageInfo4
            // 
            resources.ApplyResources(this.xrPageInfo4, "xrPageInfo4");
            this.xrPageInfo4.Name = "xrPageInfo4";
            this.xrPageInfo4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo4.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo4.StylePriority.UseTextAlignment = false;
            // 
            // xrPageInfo3
            // 
            resources.ApplyResources(this.xrPageInfo3, "xrPageInfo3");
            this.xrPageInfo3.Name = "xrPageInfo3";
            this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // calculatedField1
            // 
            this.calculatedField1.Expression = "[OL_A] + [OL_B]+ [OL_D]";
            this.calculatedField1.Name = "calculatedField1";
            // 
            // pageHeaderBand1
            // 
            resources.ApplyResources(this.pageHeaderBand1, "pageHeaderBand1");
            this.pageHeaderBand1.Name = "pageHeaderBand1";
            // 
            // xrControlStyle5
            // 
            this.xrControlStyle5.BackColor = System.Drawing.Color.Transparent;
            this.xrControlStyle5.BorderColor = System.Drawing.Color.Black;
            this.xrControlStyle5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrControlStyle5.BorderWidth = 1F;
            this.xrControlStyle5.Font = new System.Drawing.Font("Times New Roman", 21F);
            this.xrControlStyle5.ForeColor = System.Drawing.Color.Black;
            this.xrControlStyle5.Name = "xrControlStyle5";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel100});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
            // 
            // DayStatus
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.detailBand1,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.groupHeaderBand2,
            this.pageFooterBand2,
            this.reportHeaderBand2,
            this.pageHeaderBand1,
            this.reportFooterBand1,
            this.GroupHeader1});
            this.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calculatedField1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource2});
            this.DataSource = this.objectDataSource2;
            resources.ApplyResources(this, "$this");
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.FromParameter,
            this.ToParameter,
            this.UserParameter,
            this.dayStatusParameter,
            this.punchStatus,
            this.DepartmentName});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle5,
            this.xrControlStyle1,
            this.xrControlStyle2,
            this.xrControlStyle3,
            this.xrControlStyle4});
            this.Version = "16.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void xrLabel74_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (!(bool)GetCurrentColumnValue("isWorkingDay"))
            (sender as XRLabel).BackColor = Color.Yellow;
    }
    private int GetPunchCount()
    {
        int count=0;
        if (GetCurrentColumnValue("checkIn1").ToString() != "")
            count++;
        if (GetCurrentColumnValue("checkIn2").ToString() != "")
            count++;
        if (GetCurrentColumnValue("checkIn3").ToString() != "")
            count++;
        if (GetCurrentColumnValue("checkOut1").ToString() != "")
            count++;
        if (GetCurrentColumnValue("checkOut2").ToString() != "")
            count++;
        if (GetCurrentColumnValue("checkOut3").ToString() != "")
            count++;

        return count;

    }
    private void xrTableRow1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int status = (int)GetCurrentColumnValue("dayStatus");
        switch(status)
        {
            case 0: (sender as XRTableRow).BackColor = Color.Yellow; break;
            case 1: (sender as XRTableRow).BackColor = Color.White; break;
            case 2: (sender as XRTableRow).BackColor = Color.White; break;
            case 3: (sender as XRTableRow).BackColor = Color.LightBlue; break;
            case 4: (sender as XRTableRow).BackColor = Color.White; break;
            case 5: (sender as XRTableRow).BackColor = Color.White; break;
        }
            
      
    }
    int totalworkingDays, totalHolidays;

    int totalWorkingHoursHH, totalWorkingHoursMM, totalPaidLeavesHH, totalPaidLeavesMM, totalUnpaidLeavesHH, totalUnpaidLeavesMM, totalOLAHH, totalOLAMM, totalOLDHH, totalOLDMM;
   int totalNetHH, totalNetMM = 0;
    private void totalLatenessLabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        int hours = total_latenessHH + (total_latenessMM / 60);
        int mins = total_latenessMM % 60;

        e.Result =  (Math.Abs(hours).ToString().PadLeft(2, '0') + ":" + Math.Abs(mins).ToString().PadLeft(2, '0'));
        e.Handled = true;

     
    }
    int total_latenessMM =0, total_latenessHH = 0;
    private void totalLatenessLabel_SummaryReset(object sender, EventArgs e)
    {
        total_latenessMM = total_latenessHH= 0;
    }

    private void totalLatenessLabel_SummaryRowChanged(object sender, EventArgs e)
    {
        string lateness = GetCurrentColumnValue("lateness").ToString();

        if (lateness == "00:00")
            return;
        total_latenessHH += Convert.ToInt32(GetCurrentColumnValue("lateness").ToString().Substring(0, 2));
        total_latenessMM += Convert.ToInt32(GetCurrentColumnValue("lateness").ToString().Substring(3, 2));
        
    }

    private void xrLabel51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        totalWorkingHoursHH += totalWorkingHoursMM / 60;
        totalWorkingHoursMM = totalWorkingHoursMM % 60;

        (sender as XRLabel).Text = (Math.Abs(totalWorkingHoursHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalWorkingHoursMM).ToString().PadLeft(2, '0'));
    }
    int totalAbsense, unapprovedAbsenseCount;
    private void approvedAbsenseLabel_SummaryRowChanged(object sender, EventArgs e)
    {
        if ((int)GetCurrentColumnValue("dayStatus")==2 || (int)GetCurrentColumnValue("dayStatus")>3)
            totalAbsense++;
    }

    private void approvedAbsenseLabel_SummaryReset(object sender, EventArgs e)
    {
        totalAbsense = 0;
    }

    private void approvedAbsenseLabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = totalAbsense.ToString();
        e.Handled = true;
    }

    private void unapprovedAbsenseLabel_SummaryRowChanged(object sender, EventArgs e)
    {
        if ((int)GetCurrentColumnValue("dayStatus")==5)
            unapprovedAbsenseCount++;
    }

    private void unapprovedAbsenseLabel_SummaryReset(object sender, EventArgs e)
    {
        unapprovedAbsenseCount = 0;
    }

    private void xrLabel49_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int totalCalHours = 0;
        totalCalHours = totalWorkingHoursHH + totalPaidLeavesHH + totalUnpaidLeavesHH;
        totalCalHours += (totalWorkingHoursMM + totalPaidLeavesMM + totalUnpaidLeavesMM) / 60;
        (sender as XRLabel).Text = totalCalHours.ToString();
    }


    private void xrLabel20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        e.Cancel = RowCount > 0;
    }

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        e.Cancel = RowCount == 0;
    }

    private void xrLabel49_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {


    }

    private void xrLabel49_SummaryCalculated(object sender, TextFormatEventArgs e)
    {

        int mins = Convert.ToInt32(e.Value);
        int hours = mins / 60;
        mins = mins % 60;

        e.Text = hours.ToString().PadLeft(2, '0') + ":" + mins.ToString().PadLeft(2, '0');

    }



    protected override void OnAfterPrint(EventArgs e)
    {
        PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth);
        base.OnAfterPrint(e);

    }
    int jobtasksHours, jobTasksMins, SpecTasksHours, SpecTasksMins;

    private void SpecialTaskLbl_SummaryReset(object sender, EventArgs e)
    {
        SpecTasksHours = SpecTasksMins = 0;
    }

    private void SpecialTaskLbl_SummaryRowChanged(object sender, EventArgs e)
    {
        SpecTasksHours += Convert.ToInt32(GetCurrentColumnValue("specialTasks").ToString().Substring(0, 2));
        SpecTasksMins += Convert.ToInt32(GetCurrentColumnValue("specialTasks").ToString().Substring(3, 2));

    }

    private void SpecialTaskLbl_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        SpecTasksHours += SpecTasksMins / 60;
        SpecTasksMins = SpecTasksMins % 60;
        e.Result = (Math.Abs(SpecTasksHours).ToString().PadLeft(2, '0') + ":" + Math.Abs(SpecTasksMins).ToString().PadLeft(2, '0'));
        SpecialTaskLbl.Text = e.Result.ToString();
        e.Handled = true;
    }

    int approvedAbsense;
    private void approvedAbsenseLbl_SummaryReset(object sender, EventArgs e)
    {
        approvedAbsense = 0;
    }

    private void approvedAbsenseLbl_SummaryRowChanged(object sender, EventArgs e)
    {

    }

    private void approvedAbsenseLbl_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = approvedAbsense.ToString();
        e.Handled=true;
    }

    private void xrLabel55_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        jobtasksHours += jobTasksMins / 60;
        jobTasksMins = jobTasksMins % 60;
        (sender as XRLabel).Text = (Math.Abs(jobtasksHours).ToString().PadLeft(2, '0') + ":" + Math.Abs(jobTasksMins).ToString().PadLeft(2, '0'));

    }

    private void xrLabel59_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        SpecTasksHours += SpecTasksMins / 60;
        SpecTasksMins = SpecTasksMins % 60;
        (sender as XRLabel).Text = (Math.Abs(SpecTasksHours).ToString().PadLeft(2, '0') + ":" + Math.Abs(SpecTasksMins).ToString().PadLeft(2, '0'));

    }

    //private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    //{
    //    if ((sender as XRLabel).Text == "00:00" || GetPunchCount()%2!=0)
    //        (sender as XRLabel).Text = "   ";
    //}
  
    private void xrLabel15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        (sender as XRLabel).Text = "";
    }

    int otMins, otHours;
   int pendingDays = 0;
    private void xrLabel66_SummaryReset(object sender, EventArgs e)
    {
        pendingDays = 0;
    }

    private void xrLabel66_SummaryRowChanged(object sender, EventArgs e)
    {
        if (GetPunchCount() % 2 != 0)
            pendingDays++;
        
    }

    //private void xrTableCell13_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
    //{
    //    if(GetPunchCount()%2!=0)

    //    {
            
    //    }
    //}
    int overtimeHH, overtimeMM;
    private void xrLabel64_SummaryReset_1(object sender, EventArgs e)
    {
        overtimeHH = overtimeMM = 0;
    }

    private void xrLabel64_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
    {
        overtimeHH += overtimeMM / 60;
        overtimeMM = overtimeMM % 60;
        e.Result = (Math.Abs(overtimeHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(overtimeMM).ToString().PadLeft(2, '0'));
      
        e.Handled = true;
    }

    private void xrLabel64_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        
    }

    private void xrLabel67_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        overtimeHH += overtimeMM / 60;
        overtimeMM = overtimeMM % 60;
        (sender as XRLabel).Text=(Math.Abs(overtimeHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(overtimeMM).ToString().PadLeft(2, '0'));
    }
    int totalLatenessHH, totalLatenessMM;
    private void xrLabel61_SummaryReset(object sender, EventArgs e)
    {
        totalLatenessHH = totalLatenessMM = 0;
    }
    int total_A_HH, total_A_MM;
    private void xrLabel22_SummaryReset(object sender, EventArgs e)
    {
        total_A_HH = total_A_MM = 0;
    }

    private void xrLabel22_SummaryRowChanged(object sender, EventArgs e)
    {
        string str = GetCurrentColumnValue("lateness_A").ToString();
        total_A_HH += Convert.ToInt32(str.Substring(0, 2));
        total_A_MM += Convert.ToInt32(str.Substring(3, 2));
    }
    int total_B_HH, total_B_MM;
    private void xrLabel23_SummaryReset(object sender, EventArgs e)
    {
        total_B_HH = total_B_MM = 0;
    }

    private void xrLabel23_SummaryRowChanged(object sender, EventArgs e)
    {
        string str = GetCurrentColumnValue("lateness_B").ToString();
        total_B_HH += Convert.ToInt32(str.Substring(0, 2));
        total_B_MM += Convert.ToInt32(str.Substring(3, 2));
    }
    int total_D_HH, total_D_MM;
    private void xrLabel45_SummaryReset(object sender, EventArgs e)
    {

    }

    private void xrLabel45_SummaryRowChanged(object sender, EventArgs e)
    {
        string str = GetCurrentColumnValue("lateness_D").ToString();
        total_D_HH += Convert.ToInt32(str.Substring(0, 2));
        total_D_MM += Convert.ToInt32(str.Substring(3, 2));
    }

    private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if ((sender as XRLabel).Text == "00:00")
            (sender as XRLabel).Text = " ";
    }

    private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        e.Cancel = RowCount > 0;
    }

    private void detailBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        e.Cancel = RowCount == 0;
    }

    private void reportFooterBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        e.Cancel = RowCount == 0;
    }

    private void xrLabel45_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        total_D_HH += total_D_MM / 60;
        total_D_MM = total_D_MM % 60;
        e.Result = total_D_HH.ToString().PadLeft(2, '0') + ":" + total_D_MM.ToString().PadLeft(2, '0');
        e.Handled = true;
    }

    private void xrLabel23_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        total_B_HH += total_B_MM / 60;
        total_B_MM = total_B_MM % 60;
        e.Result = total_B_HH.ToString().PadLeft(2, '0') + ":" + total_B_MM.ToString().PadLeft(2, '0');
        e.Handled = true;
    }

    private void xrLabel22_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        total_A_HH += total_A_MM / 60;
        total_A_MM = total_A_MM % 60;
        e.Result = total_A_HH.ToString().PadLeft(2, '0') + ":" + total_A_MM.ToString().PadLeft(2, '0');
        e.Handled = true;
    }

    private void xrLabel61_SummaryRowChanged(object sender, EventArgs e)
    {
        if (GetCurrentColumnValue("lateness").ToString().Length == 5)
        {
            totalLatenessHH += Convert.ToInt32(GetCurrentColumnValue("lateness").ToString().Substring(0, 2));
            totalLatenessMM += Convert.ToInt32(GetCurrentColumnValue("lateness").ToString().Substring(3, 2));
        }
        else
        {
            totalLatenessHH += Convert.ToInt32(GetCurrentColumnValue("lateness").ToString().Substring(1, 2));
            totalLatenessMM += Convert.ToInt32(GetCurrentColumnValue("lateness").ToString().Substring(4, 2));
        }
    }

    private void xrLabel56_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int hours = Convert.ToInt32(Parameters["AllowedLateness"].Value);

        int hoursLate = total_latenessHH + (total_latenessMM/60);
        (sender as XRLabel).Text = Math.Max(0, hoursLate - hours).ToString().PadLeft(2, '0') + (hoursLate - hours >= 0 ? (":" + totalLatenessMM % 60) : "");
    }

    private void xrLabel61_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        totalLatenessHH += totalLatenessMM / 60;
        totalLatenessMM = totalLatenessMM % 60;
        e.Result = "-"+(Math.Abs(totalLatenessHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalLatenessMM).ToString().PadLeft(2, '0'));

        e.Handled = true;
    }

    private void xrLabel64_SummaryRowChanged_1(object sender, EventArgs e)
    {
        overtimeHH += Convert.ToInt32(GetCurrentColumnValue("overtime").ToString().Substring(0, 2));
        overtimeMM += Convert.ToInt32(GetCurrentColumnValue("overtime").ToString().Substring(3, 2));
    }

 
    private void xrLabel66_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = pendingDays;
        e.Handled = true;
    }

  
   

    private void JobTaskLbl_SummaryReset(object sender, EventArgs e)
    {
        jobtasksHours = jobTasksMins = 0;
    }

    private void JobTaskLbl_SummaryRowChanged(object sender, EventArgs e)
    {
        jobtasksHours += Convert.ToInt32(GetCurrentColumnValue("jobTasks").ToString().Substring(0, 2));
        jobTasksMins += Convert.ToInt32(GetCurrentColumnValue("jobTasks").ToString().Substring(3, 2));
    }

    private void JobTaskLbl_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        jobtasksHours += jobTasksMins / 60;
        jobTasksMins = jobTasksMins % 60;
        e.Result = (Math.Abs(jobtasksHours).ToString().PadLeft(2, '0') + ":" + Math.Abs(jobTasksMins).ToString().PadLeft(2, '0'));
        JobTaskLbl.Text = e.Result.ToString();
        e.Handled = true;
    }

    private void unapprovedAbsenseLabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = unapprovedAbsenseCount.ToString();
        e.Handled = true;
    }

    private void workingDays_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = totalworkingDays;
        e.Handled = true;
        totalworkingDays = 0;
    }

    private void workingDays_SummaryReset(object sender, EventArgs e)
    {
        totalworkingDays = 0;
    }

    private void holidays_SummaryReset(object sender, EventArgs e)
    {
        totalHolidays = 0;
    }

    private void holidays_SummaryRowChanged(object sender, EventArgs e)
    {
        if ((int)GetCurrentColumnValue("dayStatus")==0)
            totalHolidays++;
    }

    private void holidays_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = totalHolidays;
        e.Handled = true;
    }

    private void xrTableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
      
        string s = (sender as XRLabel).Text;
        if (!string.IsNullOrEmpty(s))
        {
            var r = s.Split(':');
            s = r[0] + ':' + r[1];
            if (s == "00:00" || GetPunchCount() % 2 != 0)
                (sender as XRLabel).Text = "   ";
            else
                (sender as XRLabel).Text = s;
        }
    }

    private void totalWorkingHoursLabel_SummaryRowChanged(object sender, EventArgs e)
    {
        totalWorkingHoursHH += Convert.ToInt32(GetCurrentColumnValue("workingHours").ToString().Substring(0, 2));
        totalWorkingHoursMM += Convert.ToInt32(GetCurrentColumnValue("workingHours").ToString().Substring(3, 2));
    }

    private void totalWorkingHoursLabel_SummaryReset(object sender, EventArgs e)
    {
        totalWorkingHoursHH = 0;
        totalWorkingHoursMM = 0;
    }

    private void totalWorkingHoursLabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        totalWorkingHoursHH += totalWorkingHoursMM / 60;
        totalWorkingHoursMM = totalWorkingHoursMM % 60;
        e.Result = (Math.Abs(totalWorkingHoursHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalWorkingHoursMM).ToString().PadLeft(2, '0'));
        //xrLabel51.Text = e.Result.ToString();
        e.Handled = true;
    }

    private void workingDays_SummaryRowChanged(object sender, EventArgs e)
    {
        if ((int)GetCurrentColumnValue("dayStatus")==1)
            totalworkingDays++;
    }
}
