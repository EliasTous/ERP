﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using DevExpress.XtraPrinting;

/// <summary>
/// Summary description for DetailedAttendance
/// </summary>
public class DetailedAttendance : DevExpress.XtraReports.UI.XtraReport
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
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel13;
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
    private XRLabel totalOLALabel;
    private XRLabel totalOLDLabel;
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
    private XRLabel xrLabel50;
    private XRLabel xrLabel51;
    private XRLabel xrLabel52;
    private XRLabel xrLabel53;
    private XRLabel xrLabel54;
    private XRLabel xrLabel56;
    private XRLabel xrLabel57;
    private XRLabel xrLabel58;
    private XRLabel xrLabel76;
    private XRLabel xrLabel75;
    private XRLabel xrLabel49;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell3;
    private CalculatedField latenessDay;
    private XRLabel xrLabel19;
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
    private XRLabel SpecialTaskLbl;
    private XRLabel JobTaskLbl;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRLabel approvedAbsenseLbl;
    private XRLabel xrLabel24;
    private XRLabel xrLabel61;
    private XRLabel xrLabel60;
    private XRLabel xrLabel59;
    private XRLabel xrLabel55;
    private XRLabel xrLabel46;
    private XRLabel xrLabel45;
    private XRLabel xrLabel42;
    private XRLabel xrLabel40;
    private XRTableCell xrTableCell18;
    private XRLabel xrLabel63;
    private XRLabel totalOLBLbl;
    private XRLabel xrLabel15;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public DetailedAttendance()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailedAttendance));
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
            DevExpress.XtraReports.UI.XRSummary xrSummary11 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary12 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary13 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary14 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary15 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.groupHeaderBand1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel63 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.pageFooterBand1 = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel76 = new DevExpress.XtraReports.UI.XRLabel();
            this.User = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel75 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.Branch = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
            this.Division = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.From = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.To = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.AllowedLateness = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.Employee = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.centeredTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.totalOLBLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel61 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.approvedAbsenseLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.SpecialTaskLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.JobTaskLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
            this.unapprovedAbsenseLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
            this.holidays = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
            this.approvedAbsenseLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.workingDays = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.totalWorkingHoursLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.totalOLALabel = new DevExpress.XtraReports.UI.XRLabel();
            this.totalOLDLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.totalLatenessLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.latenessDay = new DevExpress.XtraReports.UI.CalculatedField();
            this.Department = new DevExpress.XtraReports.Parameters.Parameter();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StyleName = "DataField";
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell6,
            this.xrTableCell5,
            this.xrTableCell4,
            this.xrTableCell14,
            this.xrTableCell13,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell10,
            this.xrTableCell16,
            this.xrTableCell18,
            this.xrTableCell3});
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableRow1_BeforePrint);
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dowString")});
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Name = "xrTableCell1";
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dateString")});
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Name = "xrTableCell2";
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkIn1")});
            resources.ApplyResources(this.xrTableCell7, "xrTableCell7");
            this.xrTableCell7.Name = "xrTableCell7";
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkOut1")});
            resources.ApplyResources(this.xrTableCell8, "xrTableCell8");
            this.xrTableCell8.Name = "xrTableCell8";
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkIn2")});
            resources.ApplyResources(this.xrTableCell9, "xrTableCell9");
            this.xrTableCell9.Name = "xrTableCell9";
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkOut2")});
            resources.ApplyResources(this.xrTableCell6, "xrTableCell6");
            this.xrTableCell6.Name = "xrTableCell6";
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkIn3")});
            resources.ApplyResources(this.xrTableCell5, "xrTableCell5");
            this.xrTableCell5.Name = "xrTableCell5";
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "checkOut3")});
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            this.xrTableCell4.Name = "xrTableCell4";
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHours")});
            resources.ApplyResources(this.xrTableCell14, "xrTableCell14");
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell13_BeforePrint);
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LeaveType")});
            resources.ApplyResources(this.xrTableCell13, "xrTableCell13");
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell13_BeforePrint);
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "jobTasks")});
            resources.ApplyResources(this.xrTableCell11, "xrTableCell11");
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell13_BeforePrint);
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "specialTasks")});
            resources.ApplyResources(this.xrTableCell12, "xrTableCell12");
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell13_BeforePrint);
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OL_A", "{0}")});
            resources.ApplyResources(this.xrTableCell10, "xrTableCell10");
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell3_BeforePrint);
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OL_D", "{0}")});
            resources.ApplyResources(this.xrTableCell16, "xrTableCell16");
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell3_BeforePrint);
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OLBFinal")});
            resources.ApplyResources(this.xrTableCell18, "xrTableCell18");
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell3_BeforePrint);
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "latenessDay", "{0}")});
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell3_BeforePrint);
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
            // groupHeaderBand1
            // 
            this.groupHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel63,
            this.xrLabel21,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel7,
            this.xrLabel8,
            this.xrLabel5,
            this.xrLabel6,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
            resources.ApplyResources(this.groupHeaderBand1, "groupHeaderBand1");
            this.groupHeaderBand1.Name = "groupHeaderBand1";
            this.groupHeaderBand1.RepeatEveryPage = true;
            this.groupHeaderBand1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // xrLabel63
            // 
            this.xrLabel63.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel63, "xrLabel63");
            this.xrLabel63.Name = "xrLabel63";
            this.xrLabel63.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel63.StylePriority.UseBorders = false;
            this.xrLabel63.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel21, "xrLabel21");
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel9, "xrLabel9");
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel10, "xrLabel10");
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel18, "xrLabel18");
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.StylePriority.UseBorders = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel17, "xrLabel17");
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.StylePriority.UseBorders = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel16, "xrLabel16");
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel13, "xrLabel13");
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.StylePriority.UseBorders = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel12, "xrLabel12");
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel11, "xrLabel11");
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.StyleName = "centeredTitle";
            this.xrLabel11.StylePriority.UseBorders = false;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel7, "xrLabel7");
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.StyleName = "centeredTitle";
            this.xrLabel7.StylePriority.UseBorders = false;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel8, "xrLabel8");
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.StyleName = "centeredTitle";
            this.xrLabel8.StylePriority.UseBorders = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel5, "xrLabel5");
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.StyleName = "centeredTitle";
            this.xrLabel5.StylePriority.UseBorders = false;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel6, "xrLabel6");
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.StyleName = "centeredTitle";
            this.xrLabel6.StylePriority.UseBorders = false;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel4, "xrLabel4");
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.StyleName = "centeredTitle";
            this.xrLabel4.StylePriority.UseBorders = false;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel3, "xrLabel3");
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.StyleName = "centeredTitle";
            this.xrLabel3.StylePriority.UseBorders = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.StyleName = "centeredTitle";
            this.xrLabel2.StylePriority.UseBorders = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.StyleName = "centeredTitle";
            this.xrLabel1.StylePriority.UseBorders = false;
            // 
            // pageFooterBand1
            // 
            this.pageFooterBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel76,
            this.xrLabel75,
            this.xrPageInfo1,
            this.xrPageInfo2});
            resources.ApplyResources(this.pageFooterBand1, "pageFooterBand1");
            this.pageFooterBand1.Name = "pageFooterBand1";
            this.pageFooterBand1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // xrLabel76
            // 
            this.xrLabel76.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.User, "Text", "")});
            resources.ApplyResources(this.xrLabel76, "xrLabel76");
            this.xrLabel76.Name = "xrLabel76";
            this.xrLabel76.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // User
            // 
            resources.ApplyResources(this.User, "User");
            this.User.Name = "User";
            this.User.Visible = false;
            // 
            // xrLabel75
            // 
            resources.ApplyResources(this.xrLabel75, "xrLabel75");
            this.xrLabel75.Name = "xrLabel75";
            this.xrLabel75.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
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
            this.xrLabel20,
            this.xrLabel27});
            resources.ApplyResources(this.reportHeaderBand1, "reportHeaderBand1");
            this.reportHeaderBand1.Name = "reportHeaderBand1";
            // 
            // xrLabel20
            // 
            resources.ApplyResources(this.xrLabel20, "xrLabel20");
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel20_BeforePrint);
            // 
            // xrLabel27
            // 
            resources.ApplyResources(this.xrLabel27, "xrLabel27");
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel27.StyleName = "Title";
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
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36,
            this.xrLabel37,
            this.xrLabel38,
            this.xrLabel39,
            this.xrLabel32,
            this.xrLabel33,
            this.xrLabel34,
            this.xrLabel35,
            this.xrLabel30,
            this.xrLabel31,
            this.xrLabel29,
            this.xrLabel28});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // xrLabel36
            // 
            this.xrLabel36.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel36, "xrLabel36");
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel36.StylePriority.UseBorders = false;
            // 
            // xrLabel37
            // 
            this.xrLabel37.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Branch, "Text", "")});
            resources.ApplyResources(this.xrLabel37, "xrLabel37");
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel37.StylePriority.UseBorders = false;
            // 
            // Branch
            // 
            resources.ApplyResources(this.Branch, "Branch");
            this.Branch.Name = "Branch";
            this.Branch.Visible = false;
            // 
            // xrLabel38
            // 
            this.xrLabel38.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel38, "xrLabel38");
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel38.StylePriority.UseBorders = false;
            // 
            // xrLabel39
            // 
            this.xrLabel39.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Division, "Text", "")});
            resources.ApplyResources(this.xrLabel39, "xrLabel39");
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel39.StylePriority.UseBorders = false;
            // 
            // Division
            // 
            resources.ApplyResources(this.Division, "Division");
            this.Division.Name = "Division";
            this.Division.Visible = false;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel32, "xrLabel32");
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel32.StylePriority.UseBorders = false;
            // 
            // xrLabel33
            // 
            this.xrLabel33.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.From, "Text", "")});
            resources.ApplyResources(this.xrLabel33, "xrLabel33");
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel33.StylePriority.UseBorders = false;
            // 
            // From
            // 
            resources.ApplyResources(this.From, "From");
            this.From.Name = "From";
            // 
            // xrLabel34
            // 
            this.xrLabel34.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel34, "xrLabel34");
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.StylePriority.UseBorders = false;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.To, "Text", "")});
            resources.ApplyResources(this.xrLabel35, "xrLabel35");
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.StylePriority.UseBorders = false;
            // 
            // To
            // 
            resources.ApplyResources(this.To, "To");
            this.To.Name = "To";
            this.To.Visible = false;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.AllowedLateness, "Text", "")});
            resources.ApplyResources(this.xrLabel30, "xrLabel30");
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel30.StylePriority.UseBorders = false;
            // 
            // AllowedLateness
            // 
            resources.ApplyResources(this.AllowedLateness, "AllowedLateness");
            this.AllowedLateness.Name = "AllowedLateness";
            this.AllowedLateness.Visible = false;
            // 
            // xrLabel31
            // 
            this.xrLabel31.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel31, "xrLabel31");
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel31.StylePriority.UseBorders = false;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Employee, "Text", "")});
            resources.ApplyResources(this.xrLabel29, "xrLabel29");
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel29.StylePriority.UseBorders = false;
            // 
            // Employee
            // 
            resources.ApplyResources(this.Employee, "Employee");
            this.Employee.Name = "Employee";
            this.Employee.Visible = false;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel28, "xrLabel28");
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel28.StylePriority.UseBorders = false;
            // 
            // centeredTitle
            // 
            this.centeredTitle.Name = "centeredTitle";
            this.centeredTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15,
            this.totalOLBLbl,
            this.xrLabel61,
            this.xrLabel60,
            this.xrLabel59,
            this.xrLabel55,
            this.xrLabel46,
            this.xrLabel45,
            this.xrLabel42,
            this.xrLabel40,
            this.approvedAbsenseLbl,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel22,
            this.SpecialTaskLbl,
            this.JobTaskLbl,
            this.xrLabel19,
            this.xrLabel58,
            this.xrLabel48,
            this.xrLabel49,
            this.xrLabel50,
            this.xrLabel51,
            this.xrLabel52,
            this.xrLabel53,
            this.xrLabel54,
            this.xrLabel56,
            this.xrLabel57,
            this.unapprovedAbsenseLabel,
            this.xrLabel47,
            this.holidays,
            this.xrLabel43,
            this.xrLabel44,
            this.approvedAbsenseLabel,
            this.workingDays,
            this.xrLabel41,
            this.xrLabel26,
            this.xrLabel25,
            this.totalWorkingHoursLabel,
            this.totalOLALabel,
            this.totalOLDLabel,
            this.totalLatenessLabel});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // totalOLBLbl
            // 
            this.totalOLBLbl.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.totalOLBLbl.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OLBFinal")});
            resources.ApplyResources(this.totalOLBLbl, "totalOLBLbl");
            this.totalOLBLbl.Name = "totalOLBLbl";
            this.totalOLBLbl.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.totalOLBLbl.StylePriority.UseBorders = false;
            this.totalOLBLbl.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.totalOLBLbl.Summary = xrSummary1;
            this.totalOLBLbl.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.totalOLBLbl_SummaryGetResult);
            this.totalOLBLbl.SummaryReset += new System.EventHandler(this.totalOLBLbl_SummaryReset);
            this.totalOLBLbl.SummaryRowChanged += new System.EventHandler(this.totalOLBLbl_SummaryRowChanged);
            // 
            // xrLabel61
            // 
            this.xrLabel61.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel61, "xrLabel61");
            this.xrLabel61.Name = "xrLabel61";
            this.xrLabel61.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel61.StylePriority.UseBorders = false;
            this.xrLabel61.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel61_BeforePrint);
            // 
            // xrLabel60
            // 
            this.xrLabel60.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel60, "xrLabel60");
            this.xrLabel60.Name = "xrLabel60";
            this.xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel60.StylePriority.UseBorders = false;
            this.xrLabel60.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel60_BeforePrint);
            // 
            // xrLabel59
            // 
            this.xrLabel59.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel59, "xrLabel59");
            this.xrLabel59.Name = "xrLabel59";
            this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel59.StylePriority.UseBorders = false;
            this.xrLabel59.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel59_BeforePrint);
            // 
            // xrLabel55
            // 
            this.xrLabel55.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel55, "xrLabel55");
            this.xrLabel55.Name = "xrLabel55";
            this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel55.StylePriority.UseBorders = false;
            this.xrLabel55.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel55_BeforePrint);
            // 
            // xrLabel46
            // 
            this.xrLabel46.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel46, "xrLabel46");
            this.xrLabel46.Name = "xrLabel46";
            this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel46.StylePriority.UseBorders = false;
            // 
            // xrLabel45
            // 
            this.xrLabel45.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel45, "xrLabel45");
            this.xrLabel45.Name = "xrLabel45";
            this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel45.StylePriority.UseBorders = false;
            // 
            // xrLabel42
            // 
            this.xrLabel42.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel42, "xrLabel42");
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel42.StylePriority.UseBorders = false;
            // 
            // xrLabel40
            // 
            this.xrLabel40.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel40, "xrLabel40");
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel40.StylePriority.UseBorders = false;
            // 
            // approvedAbsenseLbl
            // 
            this.approvedAbsenseLbl.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.approvedAbsenseLbl.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "paidLeaves")});
            resources.ApplyResources(this.approvedAbsenseLbl, "approvedAbsenseLbl");
            this.approvedAbsenseLbl.Name = "approvedAbsenseLbl";
            this.approvedAbsenseLbl.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.approvedAbsenseLbl.StylePriority.UseBorders = false;
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.approvedAbsenseLbl.Summary = xrSummary2;
            this.approvedAbsenseLbl.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.approvedAbsenseLbl_SummaryGetResult);
            this.approvedAbsenseLbl.SummaryReset += new System.EventHandler(this.approvedAbsenseLbl_SummaryReset);
            this.approvedAbsenseLbl.SummaryRowChanged += new System.EventHandler(this.approvedAbsenseLbl_SummaryRowChanged);
            // 
            // xrLabel24
            // 
            this.xrLabel24.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel24, "xrLabel24");
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.StylePriority.UseBorders = false;
            // 
            // xrLabel23
            // 
            this.xrLabel23.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel23, "xrLabel23");
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel23.StylePriority.UseBorders = false;
            this.xrLabel23.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel23_BeforePrint);
            // 
            // xrLabel22
            // 
            this.xrLabel22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel22, "xrLabel22");
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.StylePriority.UseBorders = false;
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
            this.SpecialTaskLbl.StylePriority.UseTextAlignment = false;
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.SpecialTaskLbl.Summary = xrSummary3;
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
            this.JobTaskLbl.StylePriority.UseTextAlignment = false;
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.JobTaskLbl.Summary = xrSummary4;
            this.JobTaskLbl.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.JobTaskLbl_SummaryGetResult);
            this.JobTaskLbl.SummaryReset += new System.EventHandler(this.JobTaskLbl_SummaryReset);
            this.JobTaskLbl.SummaryRowChanged += new System.EventHandler(this.JobTaskLbl_SummaryRowChanged);
            // 
            // xrLabel19
            // 
            this.xrLabel19.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel19, "xrLabel19");
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.StylePriority.UseBorders = false;
            this.xrLabel19.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel19_BeforePrint);
            // 
            // xrLabel58
            // 
            this.xrLabel58.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel58, "xrLabel58");
            this.xrLabel58.Name = "xrLabel58";
            this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel58.StylePriority.UseBorders = false;
            this.xrLabel58.StylePriority.UseFont = false;
            // 
            // xrLabel48
            // 
            this.xrLabel48.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel48, "xrLabel48");
            this.xrLabel48.Name = "xrLabel48";
            this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel48.StylePriority.UseBorders = false;
            // 
            // xrLabel49
            // 
            this.xrLabel49.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "calHours")});
            resources.ApplyResources(this.xrLabel49, "xrLabel49");
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel49.StylePriority.UseBorders = false;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel49.Summary = xrSummary5;
            this.xrLabel49.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.xrLabel49_SummaryCalculated);
            this.xrLabel49.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel49_SummaryGetResult);
            this.xrLabel49.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel49_BeforePrint);
            // 
            // xrLabel50
            // 
            this.xrLabel50.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel50, "xrLabel50");
            this.xrLabel50.Name = "xrLabel50";
            this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel50.StylePriority.UseBorders = false;
            // 
            // xrLabel51
            // 
            this.xrLabel51.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel51, "xrLabel51");
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel51.StylePriority.UseBorders = false;
            this.xrLabel51.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel51_BeforePrint);
            // 
            // xrLabel52
            // 
            this.xrLabel52.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel52, "xrLabel52");
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel52.StylePriority.UseBorders = false;
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
            // 
            // xrLabel54
            // 
            this.xrLabel54.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHours")});
            resources.ApplyResources(this.xrLabel54, "xrLabel54");
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel54.StylePriority.UseBorders = false;
            xrSummary6.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel54.Summary = xrSummary6;
            this.xrLabel54.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel54_SummaryGetResult);
            this.xrLabel54.SummaryReset += new System.EventHandler(this.xrLabel54_SummaryReset);
            this.xrLabel54.SummaryRowChanged += new System.EventHandler(this.xrLabel54_SummaryRowChanged);
            // 
            // xrLabel56
            // 
            this.xrLabel56.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel56, "xrLabel56");
            this.xrLabel56.Name = "xrLabel56";
            this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel56.StylePriority.UseBorders = false;
            this.xrLabel56.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel56_BeforePrint);
            // 
            // xrLabel57
            // 
            this.xrLabel57.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel57, "xrLabel57");
            this.xrLabel57.Name = "xrLabel57";
            this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel57.StylePriority.UseBorders = false;
            // 
            // unapprovedAbsenseLabel
            // 
            this.unapprovedAbsenseLabel.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.unapprovedAbsenseLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "unpaidLeaves")});
            resources.ApplyResources(this.unapprovedAbsenseLabel, "unapprovedAbsenseLabel");
            this.unapprovedAbsenseLabel.Name = "unapprovedAbsenseLabel";
            this.unapprovedAbsenseLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.unapprovedAbsenseLabel.StylePriority.UseBorders = false;
            xrSummary7.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.unapprovedAbsenseLabel.Summary = xrSummary7;
            this.unapprovedAbsenseLabel.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.unapprovedAbsenseLabel_SummaryGetResult);
            this.unapprovedAbsenseLabel.SummaryReset += new System.EventHandler(this.unapprovedAbsenseLabel_SummaryReset);
            this.unapprovedAbsenseLabel.SummaryRowChanged += new System.EventHandler(this.unapprovedAbsenseLabel_SummaryRowChanged);
            // 
            // xrLabel47
            // 
            this.xrLabel47.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel47, "xrLabel47");
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel47.StylePriority.UseBorders = false;
            // 
            // holidays
            // 
            this.holidays.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.holidays.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "isWorkingDay")});
            resources.ApplyResources(this.holidays, "holidays");
            this.holidays.Name = "holidays";
            this.holidays.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.holidays.StylePriority.UseBorders = false;
            xrSummary8.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.holidays.Summary = xrSummary8;
            this.holidays.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.holidays_SummaryGetResult);
            this.holidays.SummaryReset += new System.EventHandler(this.holidays_SummaryReset);
            this.holidays.SummaryRowChanged += new System.EventHandler(this.holidays_SummaryRowChanged);
            // 
            // xrLabel43
            // 
            this.xrLabel43.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel43, "xrLabel43");
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel43.StylePriority.UseBorders = false;
            // 
            // xrLabel44
            // 
            this.xrLabel44.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel44, "xrLabel44");
            this.xrLabel44.Name = "xrLabel44";
            this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel44.StylePriority.UseBorders = false;
            // 
            // approvedAbsenseLabel
            // 
            this.approvedAbsenseLabel.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.approvedAbsenseLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "paidLeaves")});
            resources.ApplyResources(this.approvedAbsenseLabel, "approvedAbsenseLabel");
            this.approvedAbsenseLabel.Name = "approvedAbsenseLabel";
            this.approvedAbsenseLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.approvedAbsenseLabel.StylePriority.UseBorders = false;
            xrSummary9.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.approvedAbsenseLabel.Summary = xrSummary9;
            this.approvedAbsenseLabel.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.approvedAbsenseLabel_SummaryGetResult);
            this.approvedAbsenseLabel.SummaryReset += new System.EventHandler(this.approvedAbsenseLabel_SummaryReset);
            this.approvedAbsenseLabel.SummaryRowChanged += new System.EventHandler(this.approvedAbsenseLabel_SummaryRowChanged);
            // 
            // workingDays
            // 
            this.workingDays.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.workingDays.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "isWorkingDay")});
            resources.ApplyResources(this.workingDays, "workingDays");
            this.workingDays.Name = "workingDays";
            this.workingDays.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.workingDays.StylePriority.UseBorders = false;
            xrSummary10.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.workingDays.Summary = xrSummary10;
            this.workingDays.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.workingDays_SummaryGetResult);
            this.workingDays.SummaryReset += new System.EventHandler(this.workingDays_SummaryReset);
            this.workingDays.SummaryRowChanged += new System.EventHandler(this.workingDays_SummaryRowChanged);
            // 
            // xrLabel41
            // 
            this.xrLabel41.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel41, "xrLabel41");
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel41.StylePriority.UseBorders = false;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dayId")});
            resources.ApplyResources(this.xrLabel26, "xrLabel26");
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.StylePriority.UseBorders = false;
            xrSummary11.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel26.Summary = xrSummary11;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel25, "xrLabel25");
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.StylePriority.UseBorders = false;
            // 
            // totalWorkingHoursLabel
            // 
            this.totalWorkingHoursLabel.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.totalWorkingHoursLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHours")});
            resources.ApplyResources(this.totalWorkingHoursLabel, "totalWorkingHoursLabel");
            this.totalWorkingHoursLabel.Name = "totalWorkingHoursLabel";
            this.totalWorkingHoursLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.totalWorkingHoursLabel.StylePriority.UseBorders = false;
            this.totalWorkingHoursLabel.StylePriority.UseTextAlignment = false;
            xrSummary12.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.totalWorkingHoursLabel.Summary = xrSummary12;
            this.totalWorkingHoursLabel.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.totalWorkingHoursLabel_SummaryGetResult);
            this.totalWorkingHoursLabel.SummaryReset += new System.EventHandler(this.totalWorkingHoursLabel_SummaryReset);
            this.totalWorkingHoursLabel.SummaryRowChanged += new System.EventHandler(this.totalWorkingHoursLabel_SummaryRowChanged);
            // 
            // totalOLALabel
            // 
            this.totalOLALabel.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.totalOLALabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OL_A")});
            resources.ApplyResources(this.totalOLALabel, "totalOLALabel");
            this.totalOLALabel.Name = "totalOLALabel";
            this.totalOLALabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.totalOLALabel.StylePriority.UseBorders = false;
            this.totalOLALabel.StylePriority.UseTextAlignment = false;
            xrSummary13.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary13.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.totalOLALabel.Summary = xrSummary13;
            this.totalOLALabel.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.totalOLALabel_SummaryGetResult);
            this.totalOLALabel.SummaryReset += new System.EventHandler(this.totalOLALabel_SummaryReset);
            this.totalOLALabel.SummaryRowChanged += new System.EventHandler(this.totalOLALabel_SummaryRowChanged);
            // 
            // totalOLDLabel
            // 
            this.totalOLDLabel.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.totalOLDLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OL_D")});
            resources.ApplyResources(this.totalOLDLabel, "totalOLDLabel");
            this.totalOLDLabel.Name = "totalOLDLabel";
            this.totalOLDLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.totalOLDLabel.StylePriority.UseBorders = false;
            this.totalOLDLabel.StylePriority.UseTextAlignment = false;
            xrSummary14.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary14.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.totalOLDLabel.Summary = xrSummary14;
            this.totalOLDLabel.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.totalOLDLabel_SummaryGetResult);
            this.totalOLDLabel.SummaryReset += new System.EventHandler(this.totalOLDLabel_SummaryReset);
            this.totalOLDLabel.SummaryRowChanged += new System.EventHandler(this.totalOLDLabel_SummaryRowChanged);
            // 
            // totalLatenessLabel
            // 
            this.totalLatenessLabel.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.totalLatenessLabel, "totalLatenessLabel");
            this.totalLatenessLabel.Name = "totalLatenessLabel";
            this.totalLatenessLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.totalLatenessLabel.StylePriority.UseBorders = false;
            this.totalLatenessLabel.StylePriority.UseTextAlignment = false;
            xrSummary15.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary15.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.totalLatenessLabel.Summary = xrSummary15;
            this.totalLatenessLabel.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.totalLatenessLabel_SummaryGetResult);
            // 
            // latenessDay
            // 
            this.latenessDay.Expression = "[OL_A] + [OL_D] + [OLBFinal]";
            this.latenessDay.Name = "latenessDay";
            // 
            // Department
            // 
            resources.ApplyResources(this.Department, "Department");
            this.Department.Name = "Department";
            this.Department.Visible = false;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(AionHR.Model.Reports.RT303);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // xrLabel15
            // 
            this.xrLabel15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel15, "xrLabel15");
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel15.StylePriority.UseBorders = false;
            // 
            // DetailedAttendance
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.groupHeaderBand1,
            this.pageFooterBand1,
            this.reportHeaderBand1,
            this.PageHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.latenessDay});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            resources.ApplyResources(this, "$this");
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.User,
            this.Employee,
            this.From,
            this.To,
            this.Branch,
            this.Department,
            this.Division,
            this.AllowedLateness});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField,
            this.centeredTitle});
            this.Version = "16.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
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

    private void xrTableRow1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (!(bool)GetCurrentColumnValue("isWorkingDay"))
            (sender as XRTableRow).BackColor = Color.Yellow;
        else
            (sender as XRTableRow).BackColor = Color.White; ;
    }
    int totalworkingDays, totalHolidays;

    int totalWorkingHoursHH, totalWorkingHoursMM, totalPaidLeavesHH, totalPaidLeavesMM, totalUnpaidLeavesHH, totalUnpaidLeavesMM, totalOLAHH, totalOLAMM, totalOLDHH, totalOLDMM;

    private void totalPaidLeavesLabel_SummaryRowChanged(object sender, EventArgs e)
    {
        totalPaidLeavesHH += Convert.ToInt32(GetCurrentColumnValue("paidLeaves").ToString().Substring(0, 2));
        totalPaidLeavesMM += Convert.ToInt32(GetCurrentColumnValue("paidLeaves").ToString().Substring(3, 2));
    }

    private void totalPaidLeavesLabel_SummaryReset(object sender, EventArgs e)
    {
        totalPaidLeavesHH = totalPaidLeavesMM = 0;
    }

    private void totalPaidLeavesLabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        totalPaidLeavesHH += totalPaidLeavesMM / 60;
        totalPaidLeavesMM = totalPaidLeavesMM % 60;
        e.Result = (Math.Abs(totalPaidLeavesHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalPaidLeavesMM).ToString().PadLeft(2, '0'));
        xrLabel19.Text = e.Result.ToString();
        e.Handled = true;
    }

    private void totalUnpaidLeavesLabel_SummaryRowChanged(object sender, EventArgs e)
    {
        totalUnpaidLeavesHH += Convert.ToInt32(GetCurrentColumnValue("unpaidLeaves").ToString().Substring(0, 2));
        totalUnpaidLeavesMM += Convert.ToInt32(GetCurrentColumnValue("unpaidLeaves").ToString().Substring(3, 2));
    }

    private void totalUnpaidLeavesLabel_SummaryReset(object sender, EventArgs e)
    {
        totalUnpaidLeavesHH = totalUnpaidLeavesMM = 0;
    }

    private void totalUnpaidLeavesLabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        totalUnpaidLeavesHH += totalUnpaidLeavesMM / 60;
        totalUnpaidLeavesMM = totalUnpaidLeavesMM % 60;
        e.Result = (Math.Abs(totalUnpaidLeavesHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalUnpaidLeavesMM).ToString().PadLeft(2, '0'));
        xrLabel54.Text = e.Result.ToString();
        e.Handled = true;
    }

    private void totalOLALabel_SummaryRowChanged(object sender, EventArgs e)
    {
        totalOLAHH += Convert.ToInt32(GetCurrentColumnValue("OL_A").ToString().Substring(0, 2));
        totalOLAMM += Convert.ToInt32(GetCurrentColumnValue("OL_A").ToString().Substring(3, 2));
    }

    private void totalOLALabel_SummaryReset(object sender, EventArgs e)
    {
        totalOLAHH = totalOLAMM = 0;
    }

    private void totalOLALabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        totalOLAHH += totalOLAMM / 60;
        totalOLAMM = totalOLAMM % 60;
        e.Result = (Math.Abs(totalOLAHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalOLAMM).ToString().PadLeft(2, '0'));
        e.Handled = true;
    }

    private void totalOLDLabel_SummaryRowChanged(object sender, EventArgs e)
    {
        totalOLDHH += Convert.ToInt32(GetCurrentColumnValue("OL_D").ToString().Substring(0, 2));
        totalOLDMM += Convert.ToInt32(GetCurrentColumnValue("OL_D").ToString().Substring(3, 2));
    }


    private void totalOLDLabel_SummaryReset(object sender, EventArgs e)
    {
        totalOLDHH = totalOLDMM = 0;
    }

    private void totalOLDLabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        totalOLDHH += totalOLDMM / 60;
        totalOLDMM = totalOLDMM % 60;
        e.Result = (Math.Abs(totalOLDHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalOLDMM).ToString().PadLeft(2, '0'));
        e.Handled = true;
    }

    private void totalLatenessLabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        int totalLatenessHH, totalLatenessMM;
        totalLatenessHH = totalOLDHH + totalOLAHH;
        totalLatenessMM = totalOLAMM + totalOLDMM;
        totalLatenessHH += totalLatenessMM / 60;
        totalLatenessMM = totalLatenessMM % 60;
        e.Result = (Math.Abs(totalLatenessHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalLatenessMM).ToString().PadLeft(2, '0'));
        e.Handled = true;
    }

    private void xrLabel51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        totalWorkingHoursHH += totalWorkingHoursMM / 60;
        totalWorkingHoursMM = totalWorkingHoursMM % 60;

        (sender as XRLabel).Text = (Math.Abs(totalWorkingHoursHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalWorkingHoursMM).ToString().PadLeft(2, '0'));
    }
    int approvedAbsenseCount, unapprovedAbsenseCount;
    private void approvedAbsenseLabel_SummaryRowChanged(object sender, EventArgs e)
    {
        if (GetCurrentColumnValue("workingHours").ToString() == "00:00" && (bool)GetCurrentColumnValue("isWorkingDay") && string.IsNullOrEmpty((string)GetCurrentColumnValue("checkIn1")))
            approvedAbsenseCount++;
    }

    private void approvedAbsenseLabel_SummaryReset(object sender, EventArgs e)
    {
        approvedAbsenseCount = 0;
    }

    private void approvedAbsenseLabel_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = approvedAbsenseCount.ToString();
        e.Handled = true;
    }

    private void unapprovedAbsenseLabel_SummaryRowChanged(object sender, EventArgs e)
    {
        if (GetCurrentColumnValue("workingHours").ToString() == "00:00" && GetCurrentColumnValue("paidLeaves").ToString() == "00:00" && (bool)GetCurrentColumnValue("isWorkingDay") && string.IsNullOrEmpty((string)GetCurrentColumnValue("checkIn1")))
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

    private void xrLabel19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int totalLeaveHH = 0, totalLeaveMM = 0;
        totalLeaveHH = totalPaidLeavesHH + totalUnpaidLeavesHH;
        totalLeaveMM = totalPaidLeavesMM + totalUnpaidLeavesMM;
        totalLeaveHH += totalLeaveMM / 60;

        totalLeaveMM = totalLeaveMM % 60;


        (sender as XRLabel).Text = (Math.Abs(totalLeaveHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalLeaveMM).ToString().PadLeft(2, '0'));
    }

    private void xrLabel54_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

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
    int absenseMM;
    private void xrLabel54_SummaryReset(object sender, EventArgs e)
    {
        absenseMM = 0;
    }

    private void xrLabel54_SummaryRowChanged(object sender, EventArgs e)
    {
        if ((bool)GetCurrentColumnValue("isWorkingDay") && GetCurrentColumnValue("workingHours").ToString() == "00:00" && GetCurrentColumnValue("paidLeaves").ToString() == "00:00" && GetCurrentColumnValue("unpaidLeaves").ToString() == "00:00")
            absenseMM += (int)GetCurrentColumnValue("calHours");
    }

    private void xrLabel54_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        int hours = absenseMM / 60;
        absenseMM = absenseMM % 60;
        e.Result = hours.ToString().PadLeft(2, '0') + ":" + absenseMM.ToString().PadLeft(2, '0');
        e.Handled = true;
    }

    private void xrLabel56_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int totalLatenessHH = totalOLAHH + totalOLDHH + SpecTasksHours;
        int totalLatenessMM = totalOLAMM + totalOLDMM + SpecTasksMins;
        totalLatenessHH += totalLatenessMM / 60;

        totalLatenessMM = totalLatenessMM % 60;
        int hours = Convert.ToInt32(Parameters["AllowedLateness"].Value);

        xrLabel56.Text = Math.Max(0, totalLatenessHH - hours).ToString();
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

    private void xrLabel23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        SpecTasksHours += SpecTasksMins / 60;
        SpecTasksMins = SpecTasksMins % 60;
        (sender as XRLabel).Text = (Math.Abs(SpecTasksHours).ToString().PadLeft(2, '0') + ":" + Math.Abs(SpecTasksMins).ToString().PadLeft(2, '0'));

    }
    int approvedAbsense;
    private void approvedAbsenseLbl_SummaryReset(object sender, EventArgs e)
    {
        approvedAbsense = 0;
    }

    private void approvedAbsenseLbl_SummaryRowChanged(object sender, EventArgs e)
    {
        if (GetCurrentColumnValue("workingHours").ToString() == "00:00" && GetCurrentColumnValue("paidLeaves").ToString() != "00:00" && (bool)GetCurrentColumnValue("isWorkingDay"))
            approvedAbsense++;
    }

    private void approvedAbsenseLbl_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = approvedAbsense.ToString();
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

    private void xrLabel60_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int totalLatenessHH = totalOLAHH + totalOLDHH;
        int totalLatenessMM = totalOLAMM + totalOLDMM;
        totalLatenessHH += totalLatenessMM / 60;

        totalLatenessMM = totalLatenessMM % 60;
        int hours = Convert.ToInt32(Parameters["AllowedLateness"].Value);

        (sender as XRLabel).Text = (Math.Abs(totalLatenessHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalLatenessMM).ToString().PadLeft(2, '0'));

    }

    private void xrLabel61_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int totalLatenessHH = totalOLAHH + totalOLDHH + SpecTasksHours;
        int totalLatenessMM = totalOLAMM + totalOLDMM + SpecTasksMins;
        totalLatenessHH += totalLatenessMM / 60;

        totalLatenessMM = totalLatenessMM % 60;
        int hours = Convert.ToInt32(Parameters["AllowedLateness"].Value);

        (sender as XRLabel).Text = (Math.Abs(totalLatenessHH).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalLatenessMM).ToString().PadLeft(2, '0'));
    }

    private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if ((sender as XRLabel).Text == "00:00")
            (sender as XRLabel).Text = "---";
    }
    int totalolbMINS, totalolbHours;
    private void totalOLBLbl_SummaryReset(object sender, EventArgs e)
    {
        totalolbMINS = totalolbHours = 0;
    }

    private void totalOLBLbl_SummaryRowChanged(object sender, EventArgs e)
    {
        string temp = GetCurrentColumnValue("OLBFinal").ToString();
        if (temp.Split(':').Length==0)
            return;
        totalolbHours += Convert.ToInt32(GetCurrentColumnValue("OLBFinal").ToString().Substring(0, 2));
        totalolbMINS += Convert.ToInt32(GetCurrentColumnValue("OLBFinal").ToString().Substring(3, 2));
    }

    private void totalOLBLbl_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        totalolbHours += totalolbMINS / 60;
        totalolbMINS = totalolbMINS % 60;
        e.Result = (Math.Abs(totalolbHours).ToString().PadLeft(2, '0') + ":" + Math.Abs(totalolbMINS).ToString().PadLeft(2, '0'));
        totalOLBLbl.Text = e.Result.ToString();
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
        if (!((bool)GetCurrentColumnValue("isWorkingDay")))
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
        var r = s.Split(':');
        s = r[0] + ':' + r[1];
        if (s == "00:00")
            (sender as XRLabel).Text = "---";
        else
            (sender as XRLabel).Text = s;
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
        xrLabel51.Text = e.Result.ToString();
        e.Handled = true;
    }

    private void workingDays_SummaryRowChanged(object sender, EventArgs e)
    {
        if (((bool)GetCurrentColumnValue("isWorkingDay")))
            totalworkingDays++;
    }
}
