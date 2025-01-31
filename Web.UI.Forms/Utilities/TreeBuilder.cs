﻿using Ext.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.UI.Forms.Utilities
{
    public class TreeBuilder
    {
        private static TreeBuilder instance;
        public static TreeBuilder Instance
        {
            get
            {
                if (instance == null)
                    instance = new TreeBuilder();
                return instance;
            }
        }

        public NodeCollection BuildCaseManagementTree(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();



            Ext.Net.Node rootParent = BuildRootParentNode("rootParent", Resources.Common.CaseManagement, true);
            Ext.Net.Node Cases = BuildParentNode("rootParent_cases", Resources.Common.Cases, true, rootParent);
            Ext.Net.Node CaseLeaf = BuildLeafNode("rootParent_casesLeaf", Resources.Common.CasesLeaf, "User", true, Cases);
            //FillConfigItem(CaseLeaf, "manageCaseManagement", "CM.aspx", Resources.Common.CasesLeaf, "icon-CaseManagement", "1");



            nodes.Add(rootParent);
            return nodes;
        }

        public NodeCollection BuildEmployeeDetailsTree(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();
            Node bulk = new Node();
            nodes.Add(bulk);



            Ext.Net.Node employeesLeaf = BuildLeafNode("rootParent_Employee_Leaf", Resources.Common.EmployeeLeaf, "UsetSuit", true, nodes[0]);
            FillConfigItem(employeesLeaf, "1", "EmployeePages/EmployeeProfile.aspx", Resources.Common.EmployeeLeaf, "icon-Employees", "1");
            Ext.Net.Node sponsors = BuildLeafNode("rootParent_Employee_Sponsors", Resources.Common.Sponsors, "Group", true, nodes[0]);
            Ext.Net.Node EntitlementDeductions = BuildLeafNode("rootParent_Employee_EntitlementDeductions", Resources.Common.EntitlementDeduction, "Group", true, nodes[0]);
            FillConfigItem(sponsors, "2", "Sponsors.aspx", Resources.Common.Sponsors, "icon-Employees", "1");
            FillConfigItem(EntitlementDeductions, "3", "Sponsors.aspx", Resources.Common.EntitlementDeduction, "icon-Employees", "1");




            return nodes;
        }
        public NodeCollection BuildEmployeeFilesTree(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();



            Ext.Net.Node rootParent = BuildRootParentNode("rootParent", Resources.Common.EmployeeFiles, true);
            Ext.Net.Node employees = BuildParentNode("rootParent_Employee", Resources.Common.Employee, true, rootParent);
            Ext.Net.Node employeesLeaf = BuildLeafNode("rootParent_Employee_Leaf", Resources.Common.EmployeeLeaf, "Vcard", true, employees);
         //   Ext.Net.Node casesList = BuildLeafNode("rootParent_Cases_List", Resources.Common.CaseManagement, "Group", true, employees);
            Ext.Net.Node loansList = BuildLeafNode("rootParent_Loans_List", Resources.Common.Loans, "MoneyDelete", true, employees);
            Ext.Net.Node EmployeePenalty = BuildLeafNode("rootParent_Employee_Penalty", Resources.Common.EmployeePenalty, "UserRed", true, employees);

     //       Ext.Net.Node assetAllowance = BuildLeafNode("rootParent_Employee_AssetAllowance", Resources.Common.AssetAllowances, "UserStar", true, employees);
            //Ext.Net.Node employeeComplaint = BuildLeafNode("rootParent_Employee_EmployeeComplaint", Resources.Common.EmployeeComplaints, "UserComment", true, employees);
           // Ext.Net.Node tasksList = BuildLeafNode("rootParent_Tasks_List", Resources.Common.Tasks, "Group", true, employees);
           // FillConfigItem(casesList, "Cases", "Cases.aspx", Resources.Common.Cases, "icon-Employees", "1");
            FillConfigItem(loansList, "Loans", "LoanRequests.aspx", Resources.Common.Loans, "icon-Employees", "1");
        //    FillConfigItem(EmployeePenalty, "EmployeePenalty", "EmployeePenalties.aspx", Resources.Common.EmployeePenalty, "icon-Employees", "1");
  //          FillConfigItem(assetAllowance, "assetAllowances", "AssetAllowances.aspx", Resources.Common.AssetAllowances, "icon-Employees", "1");
            FillConfigItem(employeesLeaf, "manageemployees", "Employees.aspx", Resources.Common.EmployeeLeaf, "icon-Employees", "1");
          //  FillConfigItem(employeeComplaint, "employeeComplaints", "EmployeeComplaints.aspx", Resources.Common.EmployeeComplaints, "icon-Employees", "1");
          // FillConfigItem(tasksList, "Tasks", "Tasks.aspx", Resources.Common.Tasks, "icon-Employees", "1");

            //Ext.Net.Node setup = BuildParentNode("rootParent_setup", Resources.Common.Setup, false, rootParent);
            //Ext.Net.Node sponsors = BuildLeafNode("rootParent_Employee_Sponsors", Resources.Common.Sponsors, "Group", true, setup);
            ////Ext.Net.Node allowanceTypes = BuildLeafNode("rootParent_Employee_allowance", Resources.Common.AllowanceTypes, "Group", true, setup);
            //Ext.Net.Node certificateLevels = BuildLeafNode("rootParent_Employee_certificate", Resources.Common.CertificateLevels, "Group", true, setup);
            //Ext.Net.Node relationshipTypes = BuildLeafNode("rootParent_Employee_relationship", Resources.Common.RelationshipTypes, "Group", true, setup);

            //Ext.Net.Node EntitlementDeductions = BuildLeafNode("rootParent_Employee_EntitlementDeductions", Resources.Common.EntitlementDeduction, "Group", true, setup);
            //Ext.Net.Node DocumentTypes = BuildLeafNode("rootParent_Employee_DocumentTypes", Resources.Common.DocumentTypes, "Group", true, setup);
            //Ext.Net.Node SalaryChangeReasons = BuildLeafNode("rootParent_Employee_SalaryChangeReason", Resources.Common.SalaryChangeReasons, "Group", true, setup);
            //Ext.Net.Node AssetCategories = BuildLeafNode("rootParent_Employee_AssetCategories", Resources.Common.AssetCategories, "Group", true, setup);
            //Ext.Net.Node BonusTypes = BuildLeafNode("rootParent_Employee_BonusTypes", Resources.Common.BonusTypes, "Group", true, setup);
            //Ext.Net.Node CheckTypes = BuildLeafNode("rootParent_Employee_CheckType", Resources.Common.CheckTypes, "Group", true, setup);


            // FillConfigItem(sponsors, "sponsors", "Sponsors.aspx", Resources.Common.Sponsors, "icon-Employees", "1");
            ////FillConfigItem(allowanceTypes, "allowanceTypes", "AllowanceTypes.aspx", Resources.Common.AllowanceTypes, "icon-Employees", "1");
            //FillConfigItem(certificateLevels, "certificateLevels", "CertificateLevels.aspx", Resources.Common.CertificateLevels, "icon-Employees", "1");
            //FillConfigItem(relationshipTypes, "relationshipTypes", "RelationshipTypes.aspx", Resources.Common.RelationshipTypes, "icon-Employees", "1");

            //FillConfigItem(EntitlementDeductions, "entitlementDeductions", "EntitlementDeductions.aspx", Resources.Common.EntitlementDeduction, "icon-Employees", "1");
            //FillConfigItem(DocumentTypes, "documentTypes", "DocumentTypes.aspx", Resources.Common.DocumentTypes, "icon-Employees", "1");
            //FillConfigItem(SalaryChangeReasons, "salaryChangeReasons", "SalaryChangeReasons.aspx", Resources.Common.SalaryChangeReasons, "icon-Employees", "1");
            //FillConfigItem(AssetCategories, "assetCategories", "AssetCategories.aspx", Resources.Common.AssetCategories, "icon-Employees", "1");
            //FillConfigItem(BonusTypes, "bonusTypes", "BonusTypes.aspx", Resources.Common.BonusTypes, "icon-Employees", "1");
            //FillConfigItem(CheckTypes, "checkTypes", "CheckTypes.aspx", Resources.Common.CheckTypes, "icon-Employees", "1");





            nodes.Add(rootParent);
            return nodes;
        }



        private Node BuildLeafNode(string ID, string Label, string icon, bool isExpanded, Ext.Net.Node parentNode)
        {
            Ext.Net.Node node = new Ext.Net.Node();
            node.Text = Label;
            node.NodeID = ID;
            if (icon != "")
                node.Icon = (Icon)Enum.Parse(typeof(Icon), icon);
            node.Expandable = isExpanded;
            node.Leaf = true;
            parentNode.Children.Add(node);
            return node;
        }

        private Node BuildParentNode(string ID, string Label, bool isExpanded, Ext.Net.Node parentNode, string icon = "")
        {
            Ext.Net.Node node = new Ext.Net.Node();
            node.Text = Label;
            node.NodeID = ID;
            if (icon != "")
                node.Icon = (Icon)Enum.Parse(typeof(Icon), icon);
            node.Expanded = isExpanded;
            parentNode.Children.Add(node);
            return node;
        }

        private Node BuildRootParentNode(string ID, string Label, bool isExpanded)
        {
            Ext.Net.Node rootParent = new Ext.Net.Node();
            rootParent.Text = Label;
            rootParent.NodeID = ID;
            rootParent.Expanded = isExpanded;
            return rootParent;
        }

        private void FillConfigItem(Node node, string id, string url, string title, string css, string ifClick)
        {
            node.CustomAttributes.Add(new ConfigItem("idTab", id, ParameterMode.Value));
            node.CustomAttributes.Add(new ConfigItem("url", url, ParameterMode.Value));
            node.CustomAttributes.Add(new ConfigItem("title", title, ParameterMode.Value));
            node.CustomAttributes.Add(new ConfigItem("css", css, ParameterMode.Value));
            node.CustomAttributes.Add(new ConfigItem("click", ifClick, ParameterMode.Value));
        }

        internal NodeCollection BuildCompanyStructureTree(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();



            Ext.Net.Node rootParent = BuildRootParentNode("rootParent", Resources.Common.Company, true);
            Ext.Net.Node companyStructure = BuildParentNode("rootParent_CS", Resources.Common.CompanyStructure, true, rootParent);
            //Ext.Net.Node dashboard = BuildLeafNode("dashboard", Resources.Common.Dashboard, "TableLightning", true, companyStructure);
            Ext.Net.Node org = BuildLeafNode("rootParent_CS_ORG", Resources.Common.OrganizationChart, "ChartOrganisation", true, companyStructure);
            Ext.Net.Node emporg = BuildLeafNode("rootParent_CS_EMPORG", Resources.Common.EmpOrganizationChart, "ChartOrganisation", true, companyStructure);
         //   Ext.Net.Node mediaItem = BuildLeafNode("rootParent_CS_MediaItem", Resources.Common.MediaItems, "Photos", true, companyStructure);
            //Ext.Net.Node setup = BuildParentNode("rootParent_setup", Resources.Common.Setup, false, rootParent);
            Ext.Net.Node files = BuildLeafNode("rootParent_CS_Files", Resources.Common.Files, "FolderTable", true, companyStructure);
         //   Ext.Net.Node companyNews = BuildLeafNode("rootParent_CS_CompanyNews", Resources.Common.CompanyNews, "Newspaper", true, companyStructure);
            Ext.Net.Node companyRightToWork = BuildLeafNode("rootParent_CS_CompanyRightTowork", Resources.Common.CompanyRightToWorks, "ScriptStart", true, companyStructure);
         //   Ext.Net.Node companyLetter = BuildLeafNode("rootParent_CS_CompanyLetter", Resources.Common.Letters, "EmailLink" , true, companyStructure);
            

            //Ext.Net.Node departments = BuildLeafNode("rootParent_CS_DE", Resources.Common.Departments, "Group", true, setup);
            //Ext.Net.Node branches = BuildLeafNode("rootParent_CS_BR", Resources.Common.Branches, "Group", true, setup);
            //Ext.Net.Node divisions = BuildLeafNode("rootParent_CS_DI", Resources.Common.Divisions, "Group", true, setup);
            //Ext.Net.Node positions = BuildLeafNode("rootParent_CS_PO", Resources.Common.Positions, "Group", true, setup);
            ////Ext.Net.Node activities = BuildParentNode("rootParent_AC", Resources.Common.Activities, true, rootParent);
            ////Ext.Net.Node transfers = BuildLeafNode("rootParent_AC_TR", Resources.Common.Transfers, "Group", true, activities);
            //Ext.Net.Node systemSettings = BuildParentNode("rootParent_SY", Resources.Common.SystemSettings, true, setup);
            //Ext.Net.Node nationalities = BuildLeafNode("rootParent_SY_NA", Resources.Common.Nationalities, "Group", true, systemSettings);

            //Ext.Net.Node defaults = BuildLeafNode("rootParent_SY_DE", Resources.Common.SystemDefaults, "Group", true, systemSettings);
            //Ext.Net.Node currencies = BuildLeafNode("rootParent_CS_CU", Resources.Common.Currencies, "Group", true, systemSettings);
            //Ext.Net.Node users = BuildLeafNode("rootParent_CS_US", Resources.Common.Users, "Group", true, systemSettings);

            FillConfigItem(emporg, "emporg", "EmployeesOrgChart.aspx", Resources.Common.EmpOrganizationChart, "icon-Employees", "1");
            FillConfigItem(org, "orgChart", "OrganizationChart.aspx", Resources.Common.OrganizationChart, "icon-Employees", "1");
          //  FillConfigItem(mediaItem, "mediaItem", "MediaItems.aspx", Resources.Common.MediaItems, "icon-Employees", "1");
         //   FillConfigItem(files, "csFiles", "CompanyFiles.aspx", Resources.Common.Files, "icon-Employees", "1");
        //    FillConfigItem(companyNews, "companyNews", "CompanyNews.aspx", Resources.Common.CompanyNews, "icon-Employees", "1");
            //FillConfigItem(dashboard, "dashboard", "MainDashboard.aspx", Resources.Common.Dashboard, "icon-Employees", "1");
       //     FillConfigItem(companyRightToWork, "companyRightToWorks", "CompanyRightToWorks.aspx", Resources.Common.CompanyRightToWorks, "icon-Employees", "1");
         //   FillConfigItem(companyLetter, "companyLetter", "Letters.aspx", Resources.Common.Letters, "icon-Employees", "1");

            //FillConfigItem(branches, "branches", "Branches.aspx", Resources.Common.Branches, "icon-Employees", "1");
            //FillConfigItem(divisions, "divisions", "Divisions.aspx", Resources.Common.Divisions, "icon-Employees", "1");
            //FillConfigItem(departments, "departments", "Departments.aspx", Resources.Common.Departments, "icon-Employees", "1");
            //FillConfigItem(positions, "positions", "Positions.aspx", Resources.Common.Positions, "icon-Employees", "1");
            //FillConfigItem(currencies, "currencies", "Currencies.aspx", Resources.Common.Currencies, "icon-Employees", "1");

            //FillConfigItem(nationalities, "nationalities", "Nationalities.aspx", Resources.Common.Nationalities, "icon-Employees", "1");
            //FillConfigItem(defaults, "defaults", "SystemDefaults.aspx", Resources.Common.SystemDefaults, "icon-Employees", "1");
            //FillConfigItem(users, "users", "Users.aspx", Resources.Common.Users, "icon-Employees", "1");


            //Ext.Net.Node files = BuildParentNode("rootParent_files", Resources.Common.Files, true, rootParent);
            //Ext.Net.Node mediaCategory = BuildLeafNode("rootParent_files_MC", Resources.Common.MediaCategory, "Group", true, files);
            ////Ext.Net.Node media = BuildLeafNode("rootParent_files_Media", Resources.Common.Media, "Group", true, files);

            //FillConfigItem(mediaCategory, "mediaCategories", "MediaCategories.aspx", Resources.Common.MediaCategory, "icon-Employees", "1");


            nodes.Add(rootParent);
            return nodes;
        }
        internal NodeCollection BuildTimeManagementTree(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();



            Ext.Net.Node rootParent = BuildRootParentNode("rootParent", Resources.Common.Company, true);
            Ext.Net.Node timeAt = BuildParentNode("rootParent_TA", Resources.Common.TimeAttendance, true, rootParent);
            Ext.Net.Node leaveMgmt = BuildParentNode("rootParent_LM", Resources.Common.LeaveManagement, true, rootParent);
            Ext.Net.Node lr = BuildLeafNode("rootParent_LM_LR", Resources.Common.LeaveRequests, "UserGo", true, leaveMgmt);
            Ext.Net.Node lc = BuildLeafNode("rootParent_LM_LC", Resources.Common.LeaveCalendar, "DateNext", true, leaveMgmt);
            Ext.Net.Node Re = BuildLeafNode("rootParent_LM_RE", Resources.Common.LeaveReturn, "ResultsetFirst", true, leaveMgmt);



            Ext.Net.Node ad = BuildLeafNode("rootParent_TA_AD", Resources.Common.AttendanceDay, "CalendarViewDay", true, timeAt);
            Ext.Net.Node ab = BuildLeafNode("rootParent_TA_AB", Resources.Common.timeVariations, "CalendarViewDay", true, timeAt);
            Ext.Net.Node TA = BuildLeafNode("rootParent_TA_TA", Resources.Common.TimeApprovals, "CalendarViewDay", true, timeAt);
            //Ext.Net.Node GA = BuildLeafNode("rootParent_TA_GA", Resources.Common.GenerateAttendanceDays, "UserGo", true, timeAt);
            Ext.Net.Node DS = BuildLeafNode("rootParent_TA_DS", Resources.Common.DailySchedule, "DateMagnify", true, timeAt);
            Ext.Net.Node BA = BuildLeafNode("rootParent_TA_BA", Resources.Common.BranchAvailability, "DateMagnify", true, timeAt);
            Ext.Net.Node DA = BuildLeafNode("rootParent_TA_DA", Resources.Common.DayAvailability, "DateEdit", true, timeAt);
            Ext.Net.Node TP = BuildLeafNode("rootParent_TA_TP", Resources.Common.TimePerformances, "DateEdit", true, timeAt);
          Ext.Net.Node UP = BuildLeafNode("rootParent_TA_UP", Resources.Common.UnschedulePunches, "DateEdit", true, timeAt);
            Ext.Net.Node ML = BuildLeafNode("rootParent_TA_ML", Resources.Common.MonthlyLateness, "DateEdit", true, timeAt);
            //Ext.Net.Node ATV = BuildLeafNode("rootParent_TA_ATV", Resources.Common.ChangeAutoApprovedVariations, "DateEdit", true, timeAt);
            // Ext.Net.Node GS = BuildLeafNode("rootParent_TA_GS", Resources.Common.GenerateAttendanceShift, "DateEdit", true, timeAt);
            // Ext.Net.Node SA = BuildLeafNode("rootParent_TA_SA", Resources.Common.SynchronizeAttendanceDays, "UserGo", true, timeAt);

            //Ext.Net.Node EC = BuildLeafNode("rootParent_TA_EC", Resources.Common.EmployeeCalender, "UserGo", true, timeAt);


            FillConfigItem(lr, "lr", "LeaveRequests.aspx", Resources.Common.LeaveRequests, "icon-Employees", "1");
            FillConfigItem(lc, "lc", "LeaveCalendar.aspx", Resources.Common.LeaveCalendar, "icon-Employees", "1");
            FillConfigItem(Re, "Re", "LeaveReturns.aspx", Resources.Common.LeaveReturn, "icon-Employees", "1");
            FillConfigItem(ad, "ad", "TimeAttendanceView.aspx", Resources.Common.AttendanceDay, "icon-Employees", "1");
            FillConfigItem(ab, "ab", "Absent.aspx", Resources.Common.timeVariations, "icon-Employees", "1");
            FillConfigItem(TA, "TA", "TimeAttendanceApprovals.aspx", Resources.Common.TimeApprovals, "icon-Employees", "1");
            //FillConfigItem(GA, "GA", "GenerateAttendanceDays.aspx", Resources.Common.GenerateAttendanceDays, "icon-Employees", "1");
            FillConfigItem(DS, "DS", "DailySchedule.aspx", Resources.Common.DailySchedule, "icon-Employees", "1");
            FillConfigItem(DA, "DA", "DaysAvailability.aspx", Resources.Common.DayAvailability, "icon-Employees", "1");
            FillConfigItem(BA, "BA", "BranchAvailabilities.aspx", Resources.Common.BranchAvailability, "icon-Employees", "1");
            FillConfigItem(TP, "Tp", "TimePerformances.aspx", Resources.Common.TimePerformances, "icon-Employees", "1");
            FillConfigItem(UP, "Up", "UnschedulePunches.aspx", Resources.Common.UnschedulePunches, "icon-Employees", "1");
            FillConfigItem(ML, "ML", "MonthlyLatenesses.aspx", Resources.Common.MonthlyLateness, "icon-Employees", "1");
            //FillConfigItem(ATV, "ATV", "ChangeAutoApprovedVariations.aspx", Resources.Common.ChangeAutoApprovedVariations, "icon-Employees", "1");
            //FillConfigItem(EC, "EC", "EmployeeCals.aspx", Resources.Common.EmployeeCalender, "icon-Employees", "1");
            //   FillConfigItem(GS, "GS", "GenerateAttendanceShifts.aspx", Resources.Common.GenerateAttendanceShift, "icon-Employees", "1");
            // FillConfigItem(SA, "SA", "SynchronizeAttendanceDays.aspx", Resources.Common.SynchronizeAttendanceDays, "icon-Employees", "1");


            nodes.Add(rootParent);
            return nodes;
            //Ext.Net.Node lvsetup = BuildParentNode("rootParent_LMS", Resources.Common.Setup, false, leaveMgmt);
            //Ext.Net.Node tasetup = BuildParentNode("rootParent_TAS", Resources.Common.Setup, false, timeAt);
            //Ext.Net.Node vs = BuildLeafNode("rootParent_LM_VS", Resources.Common.VacationSchedules, "Group", true, lvsetup);
            //Ext.Net.Node lt = BuildLeafNode("rootParent_LM_LT", Resources.Common.LeaveTypes, "Group", true, lvsetup);

            //Ext.Net.Node dts = BuildLeafNode("rootParent_TA_DT", Resources.Common.DayTypes, "Group", true, tasetup);

            //Ext.Net.Node sc = BuildLeafNode("rootParent_TA_SC", Resources.Common.AttendanceSchedule, "Group", true, tasetup);
            //Ext.Net.Node ca = BuildLeafNode("rootParent_TA_CA", Resources.Common.WorkingCalendars, "Group", true, tasetup);
            //Ext.Net.Node bm = BuildLeafNode("rootParent_TA_BM", Resources.Common.BiometricDevices, "Group", true, tasetup);
            //Ext.Net.Node ro = BuildLeafNode("rootParent_TA_RO", Resources.Common.Routers, "Group", true, tasetup);
            //Ext.Net.Node dashboard = BuildLeafNode("rootParent_TA_dshboard", Resources.Common.Dashboard, "TableLightning", true, timeAt);
            //Ext.Net.Node gf = BuildLeafNode("rootParent_TA_GF", Resources.Common.Geofences, "Group", true, tasetup);
            //FillConfigItem(vs, "vacationSchedules", "VacationSchedules.aspx", Resources.Common.VacationSchedules, "icon-Employees", "1");
            //FillConfigItem(lt, "lt", "LeaveTypes.aspx", Resources.Common.LeaveTypes, "icon-Employees", "1");

            //FillConfigItem(dts, "dayTypes", "DayTypes.aspx", Resources.Common.DayTypes, "icon-Employees", "1");
            //FillConfigItem(sc, "schedules", "Schedules.aspx", Resources.Common.AttendanceSchedule, "icon-Employees", "1");
            //FillConfigItem(ca, "calendars", "WorkingCalendars.aspx", Resources.Common.WorkingCalendars, "icon-Employees", "1");
            //FillConfigItem(bm, "bm", "BiometricDevices.aspx", Resources.Common.BiometricDevices, "icon-Employees", "1");
            //FillConfigItem(ro, "ro", "Routers.aspx", Resources.Common.Routers, "icon-Employees", "1");

            //FillConfigItem(dashboard, "dashboard", "Dashboard.aspx", Resources.Common.Dashboard, "icon-Employees", "1");
            //FillConfigItem(gf, "geofences", "Geofences.aspx", Resources.Common.Geofences, "icon-Employees", "1");

        }

        internal NodeCollection BuildReportsTree(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();



            Ext.Net.Node rootParent = BuildRootParentNode("rootParent", Resources.Common.Reports, true);
            Ext.Net.Node timeAt = BuildParentNode("standard", Resources.Common.StandardReports, true, rootParent);
            Ext.Net.Node rt1 = BuildParentNode("report_rt1", Resources.Common.EmployeeFiles, true, timeAt, "Group");
            Ext.Net.Node rt101 = BuildLeafNode("report_rt101", Resources.Common.RT01, "", true, rt1);
            Ext.Net.Node rt102 = BuildLeafNode("report_rt102", Resources.Common.RT102, "", true, rt1);
            Ext.Net.Node rt102B = BuildLeafNode("report_rt102B", Resources.Common.RT102B, "", true, rt1);
            Ext.Net.Node rt103 = BuildLeafNode("report_rt103", Resources.Common.RT103, "", true, rt1);
            Ext.Net.Node rt104 = BuildLeafNode("report_rt104", Resources.Common.RT104, "", true, rt1);
            Ext.Net.Node rt105 = BuildLeafNode("report_rt105", Resources.Common.RT105, "", true, rt1);
            Ext.Net.Node rt106 = BuildLeafNode("report_rt106", Resources.Common.RT106, "", true, rt1);
            Ext.Net.Node rt107 = BuildLeafNode("report_rt107", Resources.Common.RT107, "", true, rt1);
            Ext.Net.Node rt108 = BuildLeafNode("report_rt108", Resources.Common.RT108, "", true, rt1);
            Ext.Net.Node rt111 = BuildLeafNode("report_rt111", Resources.Common.RT111, "", true, rt1);
            Ext.Net.Node rt112 = BuildLeafNode("report_rt112", Resources.Common.RT112, "", true, rt1);
            Ext.Net.Node rt113 = BuildLeafNode("report_rt113", Resources.Common.RT113, "", true, rt1);
            Ext.Net.Node rt114 = BuildLeafNode("report_rt114", Resources.Common.RT114, "", true, rt1);
            Ext.Net.Node rt115 = BuildLeafNode("report_rt115", Resources.Common.RT115, "", true, rt1);
            



            //Ext.Net.Node rt2 = BuildParentNode("report_rt2", Resources.Common.Salary,  true, timeAt);

            Ext.Net.Node rt201 = BuildLeafNode("report_rt201", Resources.Common.RT201, "", true, rt1);
            Ext.Net.Node rt202 = BuildLeafNode("report_rt202", Resources.Common.RT202, "", true, rt1);
            Ext.Net.Node rt203 = BuildLeafNode("report_rt203", Resources.Common.RT203, "", true, rt1);
            Ext.Net.Node rt109 = BuildLeafNode("report_rt109", Resources.Common.RT109, "", true, rt1);
            Ext.Net.Node rt200 = BuildLeafNode("report_rt200", Resources.Common.RT200, "", true, rt1);


            Ext.Net.Node rt3 = BuildParentNode("report_rt3", Resources.Common.TimeAttendance, true, timeAt, "CalendarViewMonth");
        //    Ext.Net.Node rt301 = BuildLeafNode("report_rt301", Resources.Common.RT301, "", true, rt3);
            //Ext.Net.Node rt301B = BuildLeafNode("report_rt301B", Resources.Common.RT301B, "", true, rt3);
            Ext.Net.Node rt302 = BuildLeafNode("report_rt302", Resources.Common.RT302, "", true, rt3);
            Ext.Net.Node rt303= BuildLeafNode("report_rt303", Resources.Common.RT303, "", true, rt3);

            Ext.Net.Node rt303b = BuildLeafNode("report_rt303b", Resources.Common.RT303b, "", true, rt3);

            //    Ext.Net.Node rt304 = BuildLeafNode("report_rt304", Resources.Common.RT304, "", true, rt3);
            Ext.Net.Node rt305 = BuildLeafNode("report_rt305", Resources.Common.RT305, "", true, rt3);
       //    Ext.Net.Node rt306 = BuildLeafNode("report_rt306", Resources.Common.RT306, "", true, rt3);
            Ext.Net.Node rt306 = BuildLeafNode("report_rt306", Resources.Common.RT306, "", true, rt3);
            Ext.Net.Node rt307 = BuildLeafNode("report_rt307", Resources.Common.RT307, "", true, rt3);
            Ext.Net.Node rt308 = BuildLeafNode("report_rt308", Resources.Common.RT308, "", true, rt3);
            Ext.Net.Node rt115A = BuildLeafNode("report_rt115A", Resources.Common.RT115A, "", true, rt3);
            Ext.Net.Node rt308A = BuildLeafNode("report_rt308A", Resources.Common.RT308A, "", true, rt3);

            Ext.Net.Node rt309 = BuildLeafNode("report_rt309", Resources.Common.RT309, "", true, rt3);
            Ext.Net.Node rt310 = BuildLeafNode("report_rt310", Resources.Common.RT310, "", true, rt3);
            Ext.Net.Node rt311 = BuildLeafNode("report_rt311", Resources.Common.RT311, "", true, rt3);

            Ext.Net.Node rt4 = BuildParentNode("report_rt4", Resources.Common.Loans, true, timeAt, "MoneyDelete");
            Ext.Net.Node rt401 = BuildLeafNode("report_rt401", Resources.Common.RT401, "", true, rt4);
            Ext.Net.Node rt402 = BuildLeafNode("report_rt402", Resources.Common.RT402, "", true, rt4);

            Ext.Net.Node rt5 = BuildParentNode("report_rt5", Resources.Common.Payroll, true, timeAt, "MoneyDollar");
            Ext.Net.Node rt501 = BuildLeafNode("report_rt501", Resources.Common.RT501, "", true, rt5);
            Ext.Net.Node rt502 = BuildLeafNode("report_rt502", Resources.Common.RT502, "", true, rt5);
           
            Ext.Net.Node rt503 = BuildLeafNode("report_rt503", Resources.Common.RT503, "", true, rt5);
            Ext.Net.Node rt504 = BuildLeafNode("report_rt504", Resources.Common.RT504, "", true, rt5);
            Ext.Net.Node rt506 = BuildLeafNode("report_rt506", Resources.Common.RT506, "", true, rt5);
            Ext.Net.Node rt507 = BuildLeafNode("report_rt507", Resources.Common.RT507, "", true, rt5);
            Ext.Net.Node rt508 = BuildLeafNode("report_rt508", Resources.Common.LeavePayment, "", true, rt5);


            Ext.Net.Node rt6 = BuildParentNode("report_rt6", Resources.Common.LeaveReports, true, timeAt, "UserGo");
            Ext.Net.Node rt601 = BuildLeafNode("report_rt601", Resources.Common.RT601, "", true, rt6);
            Ext.Net.Node rt602 = BuildLeafNode("report_rt602", Resources.Common.RT602, "", true, rt6);

            Ext.Net.Node rt8 = BuildParentNode("report_rt8", Resources.Common.SystemSettings, true, timeAt, "Monitor");
          //  Ext.Net.Node rt801 = BuildLeafNode("report_rt801", Resources.Common.RT801, "", true, rt8);
            Ext.Net.Node rt802 = BuildLeafNode("report_rt802", Resources.Common.RT802, "", true, rt8);
            Ext.Net.Node rt803 = BuildLeafNode("report_rt803", Resources.Common.Users, "", true, rt8);
            Ext.Net.Node rt804 = BuildLeafNode("report_rt804", Resources.Common.SecurityGroups, "", true, rt8);

            FillConfigItem(rt101, "rt101", "Reports/RT01.aspx", Resources.Common.RT01, "icon-Employees", "1");
            FillConfigItem(rt102, "rt102", "Reports/RT102.aspx", Resources.Common.RT102, "icon-Employees", "1");
            FillConfigItem(rt102B, "rt102B", "Reports/RT102B.aspx", Resources.Common.RT102B, "icon-Employees", "1");
            FillConfigItem(rt103, "rt103", "Reports/RT103.aspx", Resources.Common.RT103, "icon-Employees", "1");
            FillConfigItem(rt104, "rt104", "Reports/RT104.aspx", Resources.Common.RT104, "icon-Employees", "1");
            FillConfigItem(rt105, "rt105", "Reports/RT105.aspx", Resources.Common.RT105, "icon-Employees", "1");
            FillConfigItem(rt106, "rt106", "Reports/RT106.aspx", Resources.Common.RT106, "icon-Employees", "1");
            FillConfigItem(rt107, "rt107", "RT107.aspx", Resources.Common.RT107, "icon-Employees", "1");
            FillConfigItem(rt108, "rt108", "Reports/RT108.aspx", Resources.Common.RT108, "icon-Employees", "1");
            FillConfigItem(rt111, "rt111", "Reports/RT111.aspx", Resources.Common.RT111, "icon-Employees", "1");
            FillConfigItem(rt112, "rt112", "Reports/RT112.aspx", Resources.Common.RT112, "icon-Employees", "1");
            FillConfigItem(rt113, "rt113", "Reports/RT113.aspx", Resources.Common.RT113, "icon-Employees", "1");
            FillConfigItem(rt114, "rt114", "Reports/RT114.aspx", Resources.Common.RT114, "icon-Employees", "1");
            FillConfigItem(rt115, "rt115", "Reports/RT115.aspx", Resources.Common.RT115, "icon-Employees", "1");
            



            FillConfigItem(rt201, "rt201", "Reports/RT201.aspx", Resources.Common.RT201, "icon-Employees", "1");
            FillConfigItem(rt202, "rt202", "Reports/RT202.aspx", Resources.Common.RT202, "icon-Employees", "1");
            FillConfigItem(rt203, "rt203", "Reports/RT203.aspx", Resources.Common.RT203, "icon-Employees", "1");
            FillConfigItem(rt109, "rt109", "Reports/RT109.aspx", Resources.Common.RT109, "icon-Employees", "1");

         //   FillConfigItem(rt301, "rt301", "Reports/RT301.aspx", Resources.Common.RT301, "icon-Employees", "1");
            //FillConfigItem(rt301B, "rt301b", "Reports/RT301B.aspx", Resources.Common.RT301B, "icon-Employees", "1");
            FillConfigItem(rt302, "rt302", "Reports/RT302.aspx", Resources.Common.RT302, "icon-Employees", "1");
            FillConfigItem(rt303, "rt303", "Reports/RT303.aspx", Resources.Common.RT303, "icon-Employees", "1");
            FillConfigItem(rt303b, "rt303b", "Reports/RT303b.aspx", Resources.Common.RT303b, "icon-Employees", "1");
            //   FillConfigItem(rt304, "rt304", "Reports/RT304.aspx", Resources.Common.RT304, "icon-Employees", "1");
            FillConfigItem(rt305, "rt305", "Reports/RT305.aspx", Resources.Common.RT305, "icon-Employees", "1");
            // FillConfigItem(rt306, "rt306", "Reports/RT306.aspx", Resources.Common.RT306, "icon-Employees", "1");
            FillConfigItem(rt306, "rt306", "Reports/RT306.aspx", Resources.Common.RT306, "icon-Employees", "1");
            FillConfigItem(rt307, "rt307", "Reports/RT307.aspx", Resources.Common.RT307, "icon-Employees", "1");
            FillConfigItem(rt308, "rt308", "Reports/RT308.aspx?id=308", Resources.Common.RT308, "icon-Employees", "1");
            FillConfigItem(rt115A, "rt115A", "Reports/RT115A.aspx", Resources.Common.RT115A, "icon-Employees", "1");
            FillConfigItem(rt308A, "rt308A", "Reports/RT308A.aspx", Resources.Common.RT308A, "icon-Employees", "1");
            FillConfigItem(rt309, "rt309", "RT309.aspx", Resources.Common.RT309, "icon-Employees", "1");
            FillConfigItem(rt310, "rt310", "Reports/RT310.aspx", Resources.Common.RT310, "icon-Employees", "1");
            FillConfigItem(rt311, "rt311", "Reports/RT308.aspx?id=311", Resources.Common.RT311, "icon-Employees", "1");
                
            FillConfigItem(rt401, "rt401", "Reports/RT401.aspx", Resources.Common.RT401, "icon-Employees", "1");
            FillConfigItem(rt402, "rt402", "Reports/RT402.aspx", Resources.Common.RT402, "icon-Employees", "1");


            FillConfigItem(rt200, "rt200", "Reports/RT200.aspx", Resources.Common.RT200, "icon-Employees", "1");
            FillConfigItem(rt501, "rt501", "Reports/RT501.aspx", Resources.Common.RT501, "icon-Employees", "1");
            FillConfigItem(rt502, "rt502", "Reports/RT502.aspx", Resources.Common.RT502, "icon-Employees", "1");
            FillConfigItem(rt503, "rt503", "Reports/RT503.aspx", Resources.Common.RT503, "icon-Employees", "1");
            FillConfigItem(rt504, "rt504", "Reports/RT504.aspx", Resources.Common.RT504, "icon-Employees", "1");
            FillConfigItem(rt506, "rt506", "Reports/RT506.aspx", Resources.Common.RT506, "icon-Employees", "1");
            FillConfigItem(rt507, "rt507", "Reports/RT507.aspx", Resources.Common.RT507, "icon-Employees", "1");
            FillConfigItem(rt508, "rt508", "Reports/RT508.aspx", Resources.Common.LeavePayment, "icon-Employees", "1");

            FillConfigItem(rt601, "rt601", "Reports/RT601.aspx", Resources.Common.RT601, "icon-Employees", "1");
            FillConfigItem(rt602, "rt602", "Reports/RT602.aspx", Resources.Common.RT602, "icon-Employees", "1");

          //  FillConfigItem(rt801, "rt801", "Reports/RT801.aspx", Resources.Common.RT801, "icon-Employees", "1");
            FillConfigItem(rt802, "rt802", "Reports/RT802.aspx", Resources.Common.RT802, "icon-Employees", "1");
            FillConfigItem(rt803, "rt803", "Reports/RT803.aspx", Resources.Common.Users, "icon-Employees", "1");
            FillConfigItem(rt804, "rt804", "Reports/RT804.aspx", Resources.Common.SecurityGroups, "icon-Employees", "1");
            FillConfigItem(rt111, "rt111", "Reports/RT111.aspx", Resources.Common.RT111, "icon-Employees", "1");

            nodes.Add(rootParent);
            return nodes;
        }
        internal NodeCollection BuildPayrollTree(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();



            Ext.Net.Node rootParent = BuildRootParentNode("rootParent", Resources.Common.Payroll, true);
            Ext.Net.Node timeAt = BuildParentNode("standard", Resources.Common.Payroll, true, rootParent);

            Ext.Net.Node gen = BuildLeafNode("gen", Resources.Common.GeneratePayroll, "Group", true, timeAt);
            FillConfigItem(gen, "gen", "PayrollGeneration.aspx", Resources.Common.GeneratePayroll, "icon-Employees", "1");
            Ext.Net.Node LeavePayment = BuildLeafNode("LeavePayments", Resources.Common.LeavePayment, "Group", true, timeAt);

            FillConfigItem(LeavePayment, "LeavePayments", "LeavePayments.aspx", Resources.Common.LeavePayment, "icon-Employees", "1");


            Ext.Net.Node finalSettlement = BuildLeafNode("FinalSettlements", Resources.Common.FinalSettlements, "Group", true, timeAt);       

            FillConfigItem(finalSettlement, "FinalSettlements", "FinalSettlements.aspx", Resources.Common.FinalSettlements, "icon-Employees", "1");

            Ext.Net.Node BenefitAcquisition = BuildLeafNode("BenefitAcquisition", Resources.Common.BenefitAcquisition, "Group", true, timeAt);

            FillConfigItem(BenefitAcquisition, "BenefitAcquisition", "BenefitAcquisitions.aspx", Resources.Common.BenefitAcquisition, "icon-Employees", "1");



            nodes.Add(rootParent);
            return nodes;
        }

        internal NodeCollection BuildSelftService(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();



            Ext.Net.Node rootParent = BuildRootParentNode("rootParent", Resources.Common.SelfService, true);
            Ext.Net.Node ss = BuildParentNode("standard", Resources.Common.SelfService, true, rootParent);

            Ext.Net.Node pi = BuildLeafNode("pi", Resources.Common.PersonalInfo, "PagePortraitShot", true, ss);
            //Ext.Net.Node at = BuildLeafNode("at", Resources.Common.Attendance, "Group", true, ss);
            Ext.Net.Node lv = BuildLeafNode("lv", Resources.Common.Leaves, "UserGo", true, ss);
            Ext.Net.Node ln = BuildLeafNode("ln", Resources.Common.Loan, "MoneyDelete", true, ss);
            //Ext.Net.Node sl = BuildLeafNode("sl", Resources.Common.Salary, "Group", true, ss);
         //   Ext.Net.Node lt = BuildLeafNode("lt", Resources.Common.Letters, "EmailLink", true, ss);
          //  Ext.Net.Node AA = BuildLeafNode("AA", Resources.Common.AssetAllowancesSelfService, "UserStar", true, ss);
          //  Ext.Net.Node CO = BuildLeafNode("CO", Resources.Common.EmployeeComplaintsSelfServices, "UserComment", true, ss);
            Ext.Net.Node DS = BuildLeafNode("DS", Resources.Common.DailySchedule, "UserComment", true, ss);
            Ext.Net.Node TVSS = BuildLeafNode("TVSS", Resources.Common.timeVariations, "UserComment", true, ss);
            Ext.Net.Node TASS = BuildLeafNode("TASS", Resources.Common.TimeAttendance, "UserComment", true, ss);
            Ext.Net.Node PY = BuildLeafNode("PY", Resources.Common.Payroll, "UserComment", true, ss);
            Ext.Net.Node TimeApprovals = BuildLeafNode("TimeApprovals", Resources.Common.TimeApprovalsSelfService, "UserComment", true, ss);
            Ext.Net.Node AL = BuildLeafNode("AL", Resources.Common.AssetLoans, "UserComment", true, ss);
            Ext.Net.Node RP = BuildLeafNode("RP", Resources.Common.ChangePassword, "UserComment", true, ss);
            Ext.Net.Node TR = BuildLeafNode("TR", Resources.Common.PendingTransfers, "ArrowSwitchBluegreen", true, ss);
            Ext.Net.Node LRP = BuildLeafNode("LRP", Resources.Common.leaveReplacementApproval, "UserComment", true, ss);



            //  FillConfigItem(lt, "LettersSelfServices", "LettersSelfServices.aspx", Resources.Common.Letters, "icon-Employees", "1");
            FillConfigItem(pi, "MyInfos","Myinfos.aspx", Resources.Common.PersonalInfo, "icon-Employees", "1");
            FillConfigItem(lv, "LeaveRequestsSelfService", "LeaveRequestsSelfServices.aspx", Resources.Common.LeaveRequests, "icon-Employees", "1");
            FillConfigItem(ln, "LoanSelfService", "LoanSelfServices.aspx", Resources.Common.Loan, "icon-Employees", "1");
       //     FillConfigItem(AA, "AssetAllowanceSelfService", "AssetAllowanceSelfServices.aspx", Resources.Common.AssetAllowancesSelfService, "icon-Employees", "1");
        //    FillConfigItem(CO, "EmployeeComplaintsSelfService", "EmployeeComplaintSelfServices.aspx", Resources.Common.EmployeeComplaintsSelfServices, "icon-Employees", "1");
            FillConfigItem(DS, "DailyScheduleSelfServices", "DailyScheduleSelfServices.aspx", Resources.Common.DailySchedule, "icon-Employees", "1");
            FillConfigItem(TVSS, "TimeVariationSelfServices", "TimeVariationSelfServices.aspx", Resources.Common.timeVariations, "icon-Employees", "1");
            FillConfigItem(TASS, "TimeAttendanceViewSelfServices", "TimeAttendanceView.aspx?_fromSelfService=true", Resources.Common.TimeAttendance, "icon-Employees", "1");
            FillConfigItem(PY, "PayrollsSelfServices", "PayrollGenerationSelfServices.aspx", Resources.Common.Payroll, "icon-Employees", "1");
            FillConfigItem(RP, "SelfServiceResetPasswords", "SelfServiceResetPasswords.aspx", Resources.Common.ChangePassword, "icon-Employees", "1");
            FillConfigItem(TimeApprovals, "TimeApprovalsSelfServices", "TimeApprovalsSelfServices.aspx", Resources.Common.TimeApprovalsSelfService, "icon-Employees", "1");
            FillConfigItem(TR, "TR", "SSTransfers.aspx", Resources.Common.PendingTransfers, "ArrowSwitchBluegreen", "1");
            FillConfigItem(AL, "AL", "AssetLoans.aspx", Resources.Common.AssetLoans, "icon-Employees", "1");
            FillConfigItem(LRP, "LRP", "LeaveReplacementApprovals.aspx", Resources.Common.leaveReplacementApproval, "icon-Employees", "1");


            nodes.Add(rootParent);
            return nodes;
        }

        internal NodeCollection BuildHelp(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();



            Ext.Net.Node rootParent = BuildRootParentNode("rootParent", Resources.Common.SelfService, true);
            Ext.Net.Node ss = BuildParentNode("standard", Resources.Common.SelfService, true, rootParent);
            DirectoryInfo info = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Help"));
            var files = info.GetFiles("*.pdf");
            int i = 0;
            foreach (var item in files)
            {
                Ext.Net.Node temp = BuildLeafNode("help"+i.ToString(), item.Name, "Group", true, ss);
                FillConfigItem(temp, "help"+i.ToString(), "HelpBrowser.aspx?file="+item.Name, Resources.Common.Letters, "icon-Employees", "1");
            }

         
            nodes.Add(rootParent);
            return nodes;
        }

        internal NodeCollection BuildAdminTemplates(NodeCollection nodes)
        {
            if (nodes == null)
                nodes = new Ext.Net.NodeCollection();


            Ext.Net.Node rootParent = BuildRootParentNode("rootParent", Resources.Common.AdministrationAffairs, true);
            Ext.Net.Node adminTemplates = BuildParentNode("rootParent_CS", Resources.Common.AdministrationAffairs, true, rootParent);
            Ext.Net.Node adminBusiness = BuildParentNode("rootParent_BP", Resources.Common.adminBusinessPartner, true, rootParent);
            Ext.Net.Node adminDocument = BuildParentNode("rootParent_DC", Resources.Common.Documents, true, rootParent);
            Ext.Net.Node adminAssetManagement = BuildParentNode("rootParent_AS", Resources.Common.AssetManagement, true, rootParent);



            Ext.Net.Node templatesLeave = BuildLeafNode("admintemplates_root", Resources.Common.AdminTemplates, "Photos", true, adminTemplates);
            Ext.Net.Node businessPartnerCategory = BuildLeafNode("BusinessPartnerCategories", Resources.Common.BusinessPartnerCategory, "Building", true, adminBusiness);
            Ext.Net.Node businessPartner = BuildLeafNode("businessPartneres", Resources.Common.BusinessPartner, "Building", true, adminBusiness);
            Ext.Net.Node DocumentCategory = BuildLeafNode("DocumentCategories", Resources.Common.DocumentCategory, "Building", true, adminDocument);
            Ext.Net.Node DocumentNavigation = BuildLeafNode("DocumentNavigation", Resources.Common.OpenDocuments, "Building", true, adminDocument);
            Ext.Net.Node Document = BuildLeafNode("AdminDocuments", Resources.Common.Documents, "Building", true, adminDocument);
            Ext.Net.Node Asset = BuildLeafNode("AssetManagementAssets", Resources.Common.Assets, "Building", true, adminAssetManagement);
            Ext.Net.Node AssetManagementPurchaseOrder = BuildLeafNode("AssetManagementPurchaseOrders", Resources.Common.PurchaseOrders, "Building", true, adminAssetManagement);
            Ext.Net.Node AssetManagementOnboarding = BuildLeafNode("AssetManagementOnboarding", Resources.Common.Onboarding, "Building", true, adminAssetManagement);
            Ext.Net.Node AssetManagementloan = BuildLeafNode("AssetManagementloan", Resources.Common.Loan, "Building", true, adminAssetManagement);
            Ext.Net.Node AdminDepts = BuildLeafNode("admindepts", Resources.Common.AdminDepts, "Building", true, adminDocument);

            FillConfigItem(templatesLeave, "admintemplates_root", "AdminTemplates.aspx", Resources.Common.AdminTemplates, "icon-Employees", "1");
            FillConfigItem(businessPartnerCategory, "BusinessPartnerCategories", "BusinessPartnerCategories.aspx", Resources.Common.BusinessPartnerCategory, "icon-Employees", "1");
            FillConfigItem(businessPartner, "businessPartneres", "BusinessPartneres.aspx", Resources.Common.BusinessPartner, "icon-Employees", "1");
            FillConfigItem(DocumentCategory, "DocumentCategories", "DocumentCategories.aspx", Resources.Common.DocumentCategory, "icon-Employees", "1");
            FillConfigItem(Document, "AdminDocuments", "AdminDocuments.aspx", Resources.Common.Documents, "icon-Employees", "1");
            FillConfigItem(DocumentNavigation, "DocumentNavigation", "AdminDocNav.aspx", Resources.Common.OpenDocuments, "icon-Employees", "1");
            FillConfigItem(Asset, "AssetManagementAssets", "AssetManagementAssets.aspx", Resources.Common.Assets, "icon-Employees", "1");
            FillConfigItem(AssetManagementPurchaseOrder, "AssetManagementPurchaseOrders", "AssetManagementPurchaseOrders.aspx", Resources.Common.PurchaseOrders, "icon-Employees", "1");
            FillConfigItem(AssetManagementOnboarding, "AssetManagementOnboarding", "AssetManagementOnboardings.aspx", Resources.Common.Onboarding, "icon-Employees", "1");
            FillConfigItem(AssetManagementloan, "AssetManagementloan", "AssetManagementloans.aspx", Resources.Common.Loan, "icon-Employees", "1");
            FillConfigItem(AdminDepts, "admindepts", "AdminDepts.aspx", Resources.Common.AdminDepts, "icon-Employees", "1");




            nodes.Add(rootParent);


            return nodes;
        }
    }
}