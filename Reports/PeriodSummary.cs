using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Drawing.Printing;

/// <summary>
/// Summary description for PeriodSummary
/// </summary>
public class PeriodSummary : DevExpress.XtraReports.UI.XtraReport
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
    private XRLabel xrLabel21;
    private XRControlStyle Title;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;
    private XRLabel xrLabel1;
    private XRLabel xrLabel20;
    private XRLabel xrLabel2;
    private XRLabel xrLabel23;
    private XRLabel xrLabel25;
    private XRLabel xrLabel24;
    private DevExpress.XtraReports.Parameters.Parameter User;
    private DevExpress.XtraReports.Parameters.Parameter DateFrom;
    private DevExpress.XtraReports.Parameters.Parameter DateTo;
    private DevExpress.XtraReports.Parameters.Parameter Employee;
    private DevExpress.XtraReports.Parameters.Parameter Department;
    private DevExpress.XtraReports.Parameters.Parameter Branch;
    private XRLabel xrLabel33;
    private XRLabel xrLabel32;
    private XRControlStyle xrControlStyle1;
    private XRLabel xrLabel8;
    private XRLabel xrLabel4;
    private XRLabel xrLabel6;
    private XRLabel xrLabel3;
    private XRLabel xrLabel41;
    private XRLabel xrLabel40;
    private XRLabel xrLabel48;
    private XRLabel xrLabel34;
    private XRLabel xrLabel50;
    private XRLabel xrLabel46;
    private XRLabel xrLabel45;
    private XRLabel xrLabel44;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel42;
    private XRLabel xrLabel36;
    private XRLabel xrLabel22;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel13;
    private XRLabel xrLabel14;
    private XRLabel xrLabel10;
    private ReportFooterBand ReportFooter;
    private XRLabel xrLabel7;
    private XRLabel xrLabel5;
    private XRLabel xrLabel37;
    private XRLabel xrLabel35;
    private XRLabel xrLabel18;
    private XRLabel xrLabel54;
    private XRLabel xrLabel53;
    private XRLabel xrLabel52;
    private XRLabel xrLabel51;
    private XRLabel xrLabel47;
    private XRLabel xrLabel43;
    private XRLabel xrLabel39;
    private FormattingRule formattingRule1;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel9;
    private XRLabel xrLabel26;
    private XRLabel xrLabel19;
    private XRLabel xrLabel28;
    private XRLabel xrLabel27;
    private XRLabel xrLabel30;
    private XRLabel xrLabel29;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public PeriodSummary(Dictionary<string, string> parameters, string getLang)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        printHeader(parameters);
        this.ExportOptions.PrintPreview.DefaultFileName = "Time Variation";
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 10, 0);
        xrLabel21.Font = new Font(xrLabel21.Font.FontFamily, xrLabel21.Font.Size, FontStyle.Bold);
        //
        // TODO: Add constructor logic here
        //

        //if (getLang == "fr")
        //{

        xrLabel2.WidthF = (float)128.39;
        xrLabel2.LocationF = new PointF((float)39.17, 0);
        xrLabel25.WidthF = (float)128.39;
        xrLabel25.LocationF = new PointF((float)39.17, 0);

        xrLabel9.WidthF = (float)52.59;
        xrLabel9.LocationF = new PointF((float)167.56, 0);
        xrLabel48.LocationF = new PointF((float)167.56, 0);
        xrLabel48.WidthF = (float)52.59;

        xrLabel20.WidthF = (float)143.86;
        xrLabel20.LocationF = new PointF((float)220.15, 0);
        xrLabel1.LocationF = new PointF((float)220.15, 0);
        xrLabel1.WidthF = (float)143.86;

        xrLabel23.WidthF = (float)153.69;
        xrLabel24.WidthF = (float)153.69;
        xrLabel23.LocationF = new PointF((float)364.01, 0);
        xrLabel24.LocationF = new PointF((float)364.01, 0);

        xrLabel3.WidthF = (float)61.04;
        xrLabel4.WidthF = (float)61.04;
        xrLabel3.LocationF = new PointF((float)517.7, 0);
        xrLabel4.LocationF = new PointF((float)517.7, 0);
        xrLabel7.WidthF = (float)61.04;
        xrLabel7.LocationF = new PointF((float)517.7, 0);


        xrLabel6.WidthF = (float)66.81;
        xrLabel8.WidthF = (float)66.81;
        xrLabel6.LocationF = new PointF((float)578.74, 0);
        xrLabel8.LocationF = new PointF((float)578.74, 0);
        xrLabel18.WidthF = (float)66.81;
        xrLabel18.LocationF = new PointF((float)578.74, 0);

        xrLabel36.WidthF = (float)59.38;
        xrLabel11.WidthF = (float)59.38;
        xrLabel36.LocationF = new PointF((float)645.55, 0);
        xrLabel11.LocationF = new PointF((float)645.55, 0);
        xrLabel35.WidthF = (float)59.38;
        xrLabel35.LocationF = new PointF((float)645.55, 0);


        xrLabel42.WidthF = (float)73.19;
        xrLabel12.WidthF = (float)73.19;
        xrLabel42.LocationF = new PointF((float)704.92, 0);
        xrLabel12.LocationF = new PointF((float)704.92, 0);
        xrLabel37.WidthF = (float)73.19;
        xrLabel37.LocationF = new PointF((float)704.92, 0);


        xrLabel10.WidthF = (float)76.53;
        xrLabel40.WidthF = (float)76.53;
        xrLabel10.LocationF = new PointF((float)778.12, 0);
        xrLabel40.LocationF = new PointF((float)778.12, 0);
        xrLabel39.WidthF = (float)76.53;
        xrLabel39.LocationF = new PointF((float)778.12, 0);


        xrLabel13.WidthF = (float)85.1;
        xrLabel41.WidthF = (float)85.1;
        xrLabel13.LocationF = new PointF((float)854.64, 0);
        xrLabel41.LocationF = new PointF((float)854.64, 0);
        xrLabel43.WidthF = (float)85.1;
        xrLabel43.LocationF = new PointF((float)854.64, 0);


        xrLabel14.WidthF = (float)50.14;
        xrLabel44.WidthF = (float)50.14;
        xrLabel14.LocationF = new PointF((float)939.75, 0);
        xrLabel44.LocationF = new PointF((float)939.75, 0);
        xrLabel47.WidthF = (float)50.14;
        xrLabel47.LocationF = new PointF((float)939.75, 0);


        xrLabel15.WidthF = (float)96.74;
        xrLabel45.WidthF = (float)96.74;
        xrLabel15.LocationF = new PointF((float)989.89, 0);
        xrLabel45.LocationF = new PointF((float)989.89, 0);
        xrLabel51.WidthF = (float)96.74;
        xrLabel51.LocationF = new PointF((float)989.89, 0);


        xrLabel16.WidthF = (float)60;
        xrLabel46.WidthF = (float)60;
        xrLabel16.LocationF = new PointF((float)1086.62, 0);
        xrLabel46.LocationF = new PointF((float)1086.62, 0);
        xrLabel52.WidthF = (float)60;
        xrLabel52.LocationF = new PointF((float)1086.62, 0);


        xrLabel17.WidthF = (float)59.38;
        xrLabel50.WidthF = (float)59.38;
        xrLabel17.LocationF = new PointF((float)1146.62, 0);
        xrLabel50.LocationF = new PointF((float)1146.62, 0);
        xrLabel53.WidthF = (float)59.38;
        xrLabel53.LocationF = new PointF((float)1146.62, 0);


        xrLabel22.WidthF = (float)55;
        xrLabel34.WidthF = (float)55;
        xrLabel22.LocationF = new PointF((float)1206, 0);
        xrLabel34.LocationF = new PointF((float)1206, 0);
        xrLabel54.WidthF = (float)55;
        xrLabel54.LocationF = new PointF((float)1206, 0);


        xrLabel19.WidthF = (float)60.67;
        xrLabel27.WidthF = (float)60.67;
        xrLabel19.LocationF = new PointF((float)1260.67, 0);
        xrLabel27.LocationF = new PointF((float)1260.67, 0);
        xrLabel29.WidthF = (float)60.67;
        xrLabel29.LocationF = new PointF((float)1260.67, 0);


        xrLabel26.WidthF = (float)55;
        xrLabel28.WidthF = (float)55;
        xrLabel26.LocationF = new PointF((float)1321.34, 0);
        xrLabel28.LocationF = new PointF((float)1321.34, 0);
        xrLabel30.WidthF = (float)55;
        xrLabel30.LocationF = new PointF((float)1321.34, 0);


        xrLabel5.WidthF = (float)297.55;
        xrLabel5.LocationF = new PointF((float)220.15, 0);

        xrLabel48.HeightF = 52;
            xrLabel20.HeightF = 52;
            xrLabel23.HeightF = 52;
            xrLabel2.HeightF = 52;
            xrLabel3.HeightF = 52;
            xrLabel6.HeightF = 52;
            xrLabel36.HeightF = 52;
            xrLabel42.HeightF = 52;
            xrLabel10.HeightF = 52;
            xrLabel13.HeightF = 52;
            xrLabel14.HeightF = 52;
            xrLabel15.HeightF = 52;
            xrLabel16.HeightF = 52;
            xrLabel17.HeightF = 52;
            xrLabel22.HeightF = 52;
            xrLabel26.HeightF = 52;
            xrLabel28.HeightF = 52;


            xrLabel27.HeightF = 52;
            xrLabel19.HeightF = 52;
            xrLabel9.HeightF = 52;
            xrLabel1.HeightF = 52;
            xrLabel24.HeightF = 52;
            xrLabel25.HeightF = 52;
            xrLabel4.HeightF = 52;
            xrLabel8.HeightF = 52;
            xrLabel11.HeightF = 52;
            xrLabel12.HeightF = 52;
            xrLabel40.HeightF = 52;
            xrLabel41.HeightF = 52;
            xrLabel44.HeightF = 52;
            xrLabel45.HeightF = 52;
            xrLabel46.HeightF = 52;
            xrLabel50.HeightF = 52;
            xrLabel34.HeightF = 52;

            xrLabel5.HeightF = 32;
            xrLabel7.HeightF = 32;
            xrLabel18.HeightF = 32;
            xrLabel35.HeightF = 32;
            xrLabel37.HeightF = 32;
            xrLabel39.HeightF = 32;
            xrLabel43.HeightF = 32;
            xrLabel47.HeightF = 32;
            xrLabel51.HeightF = 32;
            xrLabel52.HeightF = 32;
            xrLabel53.HeightF = 32;
            xrLabel54.HeightF = 32;
        //}

        if (getLang == "ar")
        {
            xrLabel21.Text = "ملخص الفترة";
            xrLabel26.Text = "إجمالي الإضافي";
            xrLabel19.Text = "إجمالي التأخير";
            xrLabel5.Text = "المجموع";
        }

    }
    private void printHeader(Dictionary<string, string> parameters)
    {
        if (parameters.Count == 0)
            return;


        XRTable table = new XRTable();
        table.BeginInit();


        table.LocationF = new PointF(0, 0);
        int count = 0;
        XRTableRow row = new XRTableRow();

        foreach (KeyValuePair<string, string> item in parameters)
        {

            XRTableCell cell = new XRTableCell();

            cell.Text = item.Key;

            cell.BackColor = Color.Gray;
            cell.ForeColor = Color.White;

            XRTableCell valueCell = new XRTableCell();

            valueCell.Text = item.Value;

            row.Cells.Add(cell);
            row.Cells.Add(valueCell);

            count++;
            if (count % 4 == 0)
            {
                table.Rows.Add(row);
                row = new XRTableRow();
            }





        }
        if (count % 4 != 0)
        {
            for (int i = 0; i < (4 - (count % 4)) * 2; i++)
            {
                XRTableCell cell = new XRTableCell();

                

                row.Cells.Add(cell);
            }
            table.Rows.Add(row);
        }
        table.BeforePrint += new PrintEventHandler(table_BeforePrint);
        table.AdjustSize();
        table.EndInit();

        table.Font = new Font(table.Font.FontFamily, table.Font.Size, FontStyle.Bold);
        

        this.PageHeader.Controls.Add(table);

    }
    private void table_BeforePrint(object sender, PrintEventArgs e)
    {
        XRTable table = ((XRTable)sender);
        table.LocationF = new DevExpress.Utils.PointFloat(40F, 20F);        
        table.WidthF = this.PageWidth - 100 - this.Margins.Left - this.Margins.Right;
    }
    /// <summary> 

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeriodSummary));
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.groupHeaderBand1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.pageFooterBand1 = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.User = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Branch = new DevExpress.XtraReports.Parameters.Parameter();
            this.Employee = new DevExpress.XtraReports.Parameters.Parameter();
            this.Department = new DevExpress.XtraReports.Parameters.Parameter();
            this.DateFrom = new DevExpress.XtraReports.Parameters.Parameter();
            this.DateTo = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel28,
            this.xrLabel27,
            this.xrLabel9,
            this.xrLabel50,
            this.xrLabel46,
            this.xrLabel45,
            this.xrLabel44,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel34,
            this.xrLabel41,
            this.xrLabel40,
            this.xrLabel8,
            this.xrLabel4,
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel1});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("branchName", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.Detail.StyleName = "DataField";
            // 
            // xrLabel28
            // 
            this.xrLabel28.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totOvertime")});
            resources.ApplyResources(this.xrLabel28, "xrLabel28");
            this.xrLabel28.Multiline = true;
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel28.StylePriority.UseBorders = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel28_BeforePrint);
            // 
            // xrLabel27
            // 
            this.xrLabel27.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totLateness")});
            resources.ApplyResources(this.xrLabel27, "xrLabel27");
            this.xrLabel27.Multiline = true;
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel27.StylePriority.UseBorders = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel27_BeforePrint);
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "name.reference")});
            resources.ApplyResources(this.xrLabel9, "xrLabel9");
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel50
            // 
            this.xrLabel50.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EARLY_CHECKIN")});
            resources.ApplyResources(this.xrLabel50, "xrLabel50");
            this.xrLabel50.Name = "xrLabel50";
            this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel50.StylePriority.UseBorders = false;
            this.xrLabel50.StylePriority.UseTextAlignment = false;
            this.xrLabel50.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.XrLabel50_BeforePrint);
            // 
            // xrLabel46
            // 
            this.xrLabel46.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EARLY_LEAVE")});
            resources.ApplyResources(this.xrLabel46, "xrLabel46");
            this.xrLabel46.Name = "xrLabel46";
            this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel46.StylePriority.UseBorders = false;
            this.xrLabel46.StylePriority.UseTextAlignment = false;
            this.xrLabel46.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.XrLabel46_BeforePrint);
            // 
            // xrLabel45
            // 
            this.xrLabel45.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DURING_SHIFT_LEAVE")});
            resources.ApplyResources(this.xrLabel45, "xrLabel45");
            this.xrLabel45.Name = "xrLabel45";
            this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel45.StylePriority.UseBorders = false;
            this.xrLabel45.StylePriority.UseTextAlignment = false;
            this.xrLabel45.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel45_BeforePrint);
            // 
            // xrLabel44
            // 
            this.xrLabel44.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LATE_CHECKIN")});
            resources.ApplyResources(this.xrLabel44, "xrLabel44");
            this.xrLabel44.Name = "xrLabel44";
            this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel44.StylePriority.UseBorders = false;
            this.xrLabel44.StylePriority.UseTextAlignment = false;
            this.xrLabel44.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.XrLabel44_BeforePrint);
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHours")});
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
            this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "calendarHours")});
            resources.ApplyResources(this.xrLabel11, "xrLabel11");
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel34
            // 
            this.xrLabel34.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OVERTIME")});
            resources.ApplyResources(this.xrLabel34, "xrLabel34");
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.StylePriority.UseBorders = false;
            this.xrLabel34.StylePriority.UseTextAlignment = false;
            this.xrLabel34.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel34_BeforePrint);
            // 
            // xrLabel41
            // 
            this.xrLabel41.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MISSED_PUNCH")});
            resources.ApplyResources(this.xrLabel41, "xrLabel41");
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel41.StylePriority.UseBorders = false;
            this.xrLabel41.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel40
            // 
            this.xrLabel40.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SHIFT_LEAVE_WITHOUT_EXCUSE")});
            resources.ApplyResources(this.xrLabel40, "xrLabel40");
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel40.StylePriority.UseBorders = false;
            this.xrLabel40.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingDays")});
            resources.ApplyResources(this.xrLabel8, "xrLabel8");
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "calendarDays")});
            resources.ApplyResources(this.xrLabel4, "xrLabel4");
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "branchName")});
            resources.ApplyResources(this.xrLabel25, "xrLabel25");
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.StylePriority.UseBorders = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "departmentName")});
            resources.ApplyResources(this.xrLabel24, "xrLabel24");
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.StylePriority.UseBorders = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "name.fullName")});
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
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
            this.xrLabel26,
            this.xrLabel19,
            this.xrLabel22,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel13,
            this.xrLabel14,
            this.xrLabel10,
            this.xrLabel42,
            this.xrLabel36,
            this.xrLabel48,
            this.xrLabel6,
            this.xrLabel3,
            this.xrLabel23,
            this.xrLabel2,
            this.xrLabel20});
            resources.ApplyResources(this.groupHeaderBand1, "groupHeaderBand1");
            this.groupHeaderBand1.Name = "groupHeaderBand1";
            this.groupHeaderBand1.RepeatEveryPage = true;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel26, "xrLabel26");
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.StylePriority.UseBorders = false;
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
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
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
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
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
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
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel16, "xrLabel16");
            this.xrLabel16.Multiline = true;
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel15, "xrLabel15");
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.StylePriority.UseBorders = false;
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
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
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel14, "xrLabel14");
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
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
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel42
            // 
            this.xrLabel42.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel42, "xrLabel42");
            this.xrLabel42.Multiline = true;
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel42.StylePriority.UseBorders = false;
            this.xrLabel42.StylePriority.UseFont = false;
            this.xrLabel42.StylePriority.UseTextAlignment = false;
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
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.StylePriority.UseTextAlignment = false;
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
            this.xrLabel48.StylePriority.UseFont = false;
            this.xrLabel48.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel6, "xrLabel6");
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel3, "xrLabel3");
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
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
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel20, "xrLabel20");
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.StylePriority.UseBorders = false;
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            // 
            // pageFooterBand1
            // 
            this.pageFooterBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel33,
            this.xrLabel32,
            this.xrPageInfo1,
            this.xrPageInfo2});
            resources.ApplyResources(this.pageFooterBand1, "pageFooterBand1");
            this.pageFooterBand1.Name = "pageFooterBand1";
            // 
            // xrLabel33
            // 
            this.xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.User, "Text", "")});
            resources.ApplyResources(this.xrLabel33, "xrLabel33");
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // User
            // 
            resources.ApplyResources(this.User, "User");
            this.User.Name = "User";
            // 
            // xrLabel32
            // 
            resources.ApplyResources(this.xrLabel32, "xrLabel32");
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
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
            this.xrLabel21});
            resources.ApplyResources(this.reportHeaderBand1, "reportHeaderBand1");
            this.reportHeaderBand1.Name = "reportHeaderBand1";
            // 
            // xrLabel21
            // 
            this.xrLabel21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel21, "xrLabel21");
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.StyleName = "Title";
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
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
            // Branch
            // 
            resources.ApplyResources(this.Branch, "Branch");
            this.Branch.Name = "Branch";
            // 
            // Employee
            // 
            resources.ApplyResources(this.Employee, "Employee");
            this.Employee.Name = "Employee";
            // 
            // Department
            // 
            resources.ApplyResources(this.Department, "Department");
            this.Department.Name = "Department";
            // 
            // DateFrom
            // 
            resources.ApplyResources(this.DateFrom, "DateFrom");
            this.DateFrom.Name = "DateFrom";
            // 
            // DateTo
            // 
            resources.ApplyResources(this.DateTo, "DateTo");
            this.DateTo.Name = "DateTo";
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel30,
            this.xrLabel29,
            this.xrLabel54,
            this.xrLabel53,
            this.xrLabel52,
            this.xrLabel51,
            this.xrLabel47,
            this.xrLabel43,
            this.xrLabel39,
            this.xrLabel37,
            this.xrLabel35,
            this.xrLabel18,
            this.xrLabel7,
            this.xrLabel5});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel30
            // 
            this.xrLabel30.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totOvertime")});
            resources.ApplyResources(this.xrLabel30, "xrLabel30");
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel30.StylePriority.UseBorders = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel30.Summary = xrSummary1;
            this.xrLabel30.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.xrLabel30_SummaryCalculated);
            // 
            // xrLabel29
            // 
            this.xrLabel29.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "totLateness")});
            resources.ApplyResources(this.xrLabel29, "xrLabel29");
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel29.StylePriority.UseBorders = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel29.Summary = xrSummary2;
            this.xrLabel29.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.xrLabel29_SummaryCalculated);
            // 
            // xrLabel54
            // 
            this.xrLabel54.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OVERTIME")});
            resources.ApplyResources(this.xrLabel54, "xrLabel54");
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel54.StylePriority.UseBorders = false;
            this.xrLabel54.StylePriority.UseTextAlignment = false;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel54.Summary = xrSummary3;
            this.xrLabel54.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.XrLabel54_SummaryCalculated);
            this.xrLabel54.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.XrLabel54_SummaryGetResult);
            this.xrLabel54.SummaryRowChanged += new System.EventHandler(this.XrLabel54_SummaryRowChanged);
            this.xrLabel54.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.XrLabel54_BeforePrint);
            this.xrLabel54.AfterPrint += new System.EventHandler(this.XrLabel54_AfterPrint);
            this.xrLabel54.TextChanged += new System.EventHandler(this.XrLabel54_TextChanged);
            // 
            // xrLabel53
            // 
            this.xrLabel53.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EARLY_CHECKIN")});
            resources.ApplyResources(this.xrLabel53, "xrLabel53");
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel53.StylePriority.UseBorders = false;
            this.xrLabel53.StylePriority.UseTextAlignment = false;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel53.Summary = xrSummary4;
            this.xrLabel53.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.XrLabel53_SummaryCalculated);
            // 
            // xrLabel52
            // 
            this.xrLabel52.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EARLY_LEAVE")});
            resources.ApplyResources(this.xrLabel52, "xrLabel52");
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel52.StylePriority.UseBorders = false;
            this.xrLabel52.StylePriority.UseTextAlignment = false;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel52.Summary = xrSummary5;
            this.xrLabel52.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.XrLabel52_SummaryCalculated);
            // 
            // xrLabel51
            // 
            this.xrLabel51.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DURING_SHIFT_LEAVE")});
            resources.ApplyResources(this.xrLabel51, "xrLabel51");
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel51.StylePriority.UseBorders = false;
            this.xrLabel51.StylePriority.UseTextAlignment = false;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel51.Summary = xrSummary6;
            this.xrLabel51.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.xrLabel51_SummaryCalculated);
            this.xrLabel51.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel51_BeforePrint);
            // 
            // xrLabel47
            // 
            this.xrLabel47.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LATE_CHECKIN")});
            resources.ApplyResources(this.xrLabel47, "xrLabel47");
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel47.StylePriority.UseBorders = false;
            this.xrLabel47.StylePriority.UseTextAlignment = false;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel47.Summary = xrSummary7;
            this.xrLabel47.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.XrLabel47_SummaryCalculated);
            // 
            // xrLabel43
            // 
            this.xrLabel43.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MISSED_PUNCH")});
            resources.ApplyResources(this.xrLabel43, "xrLabel43");
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel43.StylePriority.UseBorders = false;
            this.xrLabel43.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary8, "xrSummary8");
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel43.Summary = xrSummary8;
            // 
            // xrLabel39
            // 
            this.xrLabel39.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SHIFT_LEAVE_WITHOUT_EXCUSE")});
            resources.ApplyResources(this.xrLabel39, "xrLabel39");
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel39.StylePriority.UseBorders = false;
            this.xrLabel39.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary9, "xrSummary9");
            xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel39.Summary = xrSummary9;
            // 
            // xrLabel37
            // 
            this.xrLabel37.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingHours")});
            resources.ApplyResources(this.xrLabel37, "xrLabel37");
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel37.StylePriority.UseBorders = false;
            this.xrLabel37.StylePriority.UseTextAlignment = false;
            xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel37.Summary = xrSummary10;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "calendarHours")});
            resources.ApplyResources(this.xrLabel35, "xrLabel35");
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.StylePriority.UseBorders = false;
            this.xrLabel35.StylePriority.UseTextAlignment = false;
            xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel35.Summary = xrSummary11;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "workingDays")});
            resources.ApplyResources(this.xrLabel18, "xrLabel18");
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.StylePriority.UseBorders = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel18.Summary = xrSummary12;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "calendarDays")});
            resources.ApplyResources(this.xrLabel7, "xrLabel7");
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            xrSummary13.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel7.Summary = xrSummary13;
            this.xrLabel7.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.xrLabel7_SummaryCalculated);
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrLabel5, "xrLabel5");
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // PageHeader
            // 
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(AionHR.Model.Reports.RT302);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // PeriodSummary
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.groupHeaderBand1,
            this.pageFooterBand1,
            this.reportHeaderBand1,
            this.ReportFooter,
            this.PageHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            resources.ApplyResources(this, "$this");
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.User,
            this.DateFrom,
            this.DateTo,
            this.Employee,
            this.Department,
            this.Branch});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField,
            this.xrControlStyle1});
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void xrLabel49_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        (sender as XRLabel).Text = (CurrentRowIndex + 1).ToString();
    }

    private void xrLabel45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((sender as XRLabel).Text == "0" || (sender as XRLabel).Text == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {
            (sender as XRLabel).Text = TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Hours.ToString().PadLeft(2, '0')
                + ":" + TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Minutes.ToString().PadLeft(2, '0');
        }
    }

    private string FormatTime(int v)
    {
        string sign = v < 0?"-":"";
        return sign + (Math.Abs(v )/60).ToString().PadLeft(2, '0') + ":" + (Math.Abs(v )% 60).ToString().PadLeft(2, '0');
    }

    private void xrLabel34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //int TotalMinutes = Convert.ToInt32(GetCurrentColumnValue("TotalMinutesField"));

        //(sender as DevExpress.XtraReports.UI.XRControl).Text = new TimeSpan(0, TotalMinutes, 0).ToString("c");

        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }

        if ((sender as XRLabel).Text == "0" || (sender as XRLabel).Text == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {
            //int mins = Convert.ToInt32((sender as XRLabel).Text);
            //(sender as XRLabel).Text = (mins / 60).ToString().PadLeft(2, '0') + ":" + (mins % 60).ToString().PadLeft(2, '0');

            (sender as XRLabel).Text = TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Hours.ToString().PadLeft(2, '0')
                 + ":" + TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Minutes.ToString().PadLeft(2, '0');

        }
    } 

    private void xrLabel11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //if ((sender as XRLabel).Text == "0")
        //    (sender as XRLabel).Text = "";
        //else
        //{
        //    int mins = Convert.ToInt32((sender as XRLabel).Text);
        //    (sender as XRLabel).Text = (mins / 60).ToString().PadLeft(2, '0') + ":" + (mins % 60).ToString().PadLeft(2, '0');
        //}
    }

    private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void xrLabel5_SummaryCalculated(object sender, TextFormatEventArgs e)
    {

    }

    private void xrLabel7_SummaryCalculated(object sender, TextFormatEventArgs e)
    {

    }

    private void xrLabel38_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        
    }

    private void XrLabel34_BeforePrint_1(object sender, PrintEventArgs e)
    {
        //TimeSpan.FromHours().ToString("hh:mm");
    }

    private void XrLabel54_BeforePrint(object sender, PrintEventArgs e)
    {
        
    }

    private void XrLabel54_AfterPrint(object sender, EventArgs e)
    {

    }

    private void XrLabel54_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {

            string hours = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
            string minutes = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
            e.Text = hours + ":" + minutes;
        }

        
    }

    private void XrLabel54_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {

    }

    private void XrLabel54_SummaryRowChanged(object sender, EventArgs e)
    {

    }

    private void XrLabel54_TextChanged(object sender, EventArgs e)
    {

    }

    private void XrLabel50_BeforePrint(object sender, PrintEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((sender as XRLabel).Text == "0" || (sender as XRLabel).Text == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {
            (sender as XRLabel).Text = TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Hours.ToString().PadLeft(2, '0')
                + ":" + TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Minutes.ToString().PadLeft(2, '0');
        }
    }

    private void XrLabel53_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {

            string hours = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
            string minutes = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
            e.Text = hours + ":" + minutes;
        }
    }

    private void XrLabel46_BeforePrint(object sender, PrintEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((sender as XRLabel).Text == "0" || (sender as XRLabel).Text == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {
            (sender as XRLabel).Text = TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Hours.ToString().PadLeft(2, '0')
                + ":" + TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Minutes.ToString().PadLeft(2, '0');
        }
    }

    private void XrLabel52_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {

            string hours = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
            string minutes = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
            e.Text = hours + ":" + minutes;
        }
    }

    private void XrLabel44_BeforePrint(object sender, PrintEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((sender as XRLabel).Text == "0" || (sender as XRLabel).Text == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {
            (sender as XRLabel).Text = TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Hours.ToString().PadLeft(2, '0')
                + ":" + TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Minutes.ToString().PadLeft(2, '0');
        }
    }

    private void XrLabel47_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {

            string hours = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
            string minutes = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
            e.Text = hours + ":" + minutes;
        }
    }

    private void xrLabel27_BeforePrint(object sender, PrintEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((sender as XRLabel).Text == "0" || (sender as XRLabel).Text == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {
            (sender as XRLabel).Text = TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Hours.ToString().PadLeft(2, '0')
                + ":" + TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Minutes.ToString().PadLeft(2, '0');
        }

    }

    private void xrLabel28_BeforePrint(object sender, PrintEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((sender as XRLabel).Text == "0" || (sender as XRLabel).Text == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {
            (sender as XRLabel).Text = TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Hours.ToString().PadLeft(2, '0')
                + ":" + TimeSpan.FromHours(Convert.ToDouble((sender as XRLabel).Text)).Minutes.ToString().PadLeft(2, '0');
        }
    }

    private void xrLabel29_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {

            string hours = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
            string minutes = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
            e.Text = hours + ":" + minutes;
        }
    }

    private void xrLabel30_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {

            string hours = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
            string minutes = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
            e.Text = hours + ":" + minutes;
        }
    }

    private void xrLabel51_BeforePrint(object sender, PrintEventArgs e)
    {

    }

    private void xrLabel51_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as XRLabel).Text))
        {
            return;
        }
        if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
            (sender as XRLabel).Text = "00:00";
        else
        {

            string hours = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
            string minutes = TimeSpan.FromHours(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
            e.Text = hours + ":" + minutes;
        }
    }
}
