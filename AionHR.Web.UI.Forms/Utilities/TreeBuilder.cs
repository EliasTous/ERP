using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AionHR.Web.UI.Forms.Utilities
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
            Ext.Net.Node CaseLeaf = BuildLeafNode("rootParent_casesLeaf", Resources.Common.CasesLeaf,"User", true, Cases);
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

            
            
            Ext.Net.Node employeesLeaf = BuildLeafNode("rootParent_Employee_Leaf", Resources.Common.EmployeeLeaf, "Group", true, nodes[0]);
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
            Ext.Net.Node employeesLeaf = BuildLeafNode("rootParent_Employee_Leaf", Resources.Common.EmployeeLeaf, "Group", true, employees);
            FillConfigItem(employeesLeaf, "manageemployees", "Employees.aspx", Resources.Common.EmployeeLeaf, "icon-Employees", "1");
            Ext.Net.Node sponsors = BuildLeafNode("rootParent_Employee_Sponsors", Resources.Common.Sponsors, "Group", true, employees);
            Ext.Net.Node allowanceTypes = BuildLeafNode("rootParent_Employee_allowance", Resources.Common.AllowanceTypes, "Group", true, employees);
            Ext.Net.Node certificateLevels = BuildLeafNode("rootParent_Employee_certificate", Resources.Common.CertificateLevels, "Group", true, employees);
            Ext.Net.Node trainingTypes = BuildLeafNode("rootParent_Employee_trainingTypes", Resources.Common.TrainingTypes, "Group", true, employees);
            Ext.Net.Node EntitlementDeductions = BuildLeafNode("rootParent_Employee_EntitlementDeductions", Resources.Common.EntitlementDeduction, "Group", true, employees);
            Ext.Net.Node DocumentTypes = BuildLeafNode("rootParent_Employee_DocumentTypes", Resources.Common.DocumentTypes, "Group", true, employees);
            Ext.Net.Node SalaryChangeReasons = BuildLeafNode("rootParent_Employee_SalaryChangeReason", Resources.Common.SalaryChangeReasons, "Group", true, employees);
            Ext.Net.Node AssetCategories = BuildLeafNode("rootParent_Employee_AssetCategories", Resources.Common.AssetCategories, "Group", true, employees);
            Ext.Net.Node BonusTypes = BuildLeafNode("rootParent_Employee_BonusTypes", Resources.Common.BonusTypes, "Group", true, employees);
            FillConfigItem(sponsors, "sponsors", "Sponsors.aspx", Resources.Common.Sponsors, "icon-Employees", "1");
            FillConfigItem(allowanceTypes, "allowanceTypes", "AllowanceTypes.aspx", Resources.Common.AllowanceTypes, "icon-Employees", "1");
            FillConfigItem(certificateLevels, "certificateLevels", "CertificateLevels.aspx", Resources.Common.CertificateLevels, "icon-Employees", "1");
            FillConfigItem(trainingTypes, "trainingTypes", "TrainingTypes.aspx", Resources.Common.TrainingTypes, "icon-Employees", "1");
            FillConfigItem(EntitlementDeductions, "entitlementDeductions", "EntitlementDeductions.aspx", Resources.Common.EntitlementDeduction, "icon-Employees", "1");
            FillConfigItem(DocumentTypes, "documentTypes", "DocumentTypes.aspx", Resources.Common.DocumentTypes, "icon-Employees", "1");
            FillConfigItem(SalaryChangeReasons, "salaryChangeReasons", "SalaryChangeReasons.aspx", Resources.Common.SalaryChangeReasons, "icon-Employees", "1");
            FillConfigItem(AssetCategories, "assetCategories", "AssetCategories.aspx", Resources.Common.AssetCategories, "icon-Employees", "1");
            FillConfigItem(BonusTypes, "bonusTypes", "BonusTypes.aspx", Resources.Common.BonusTypes, "icon-Employees", "1");


            nodes.Add(rootParent);
            return nodes;
        }



        private Node BuildLeafNode(string ID, string Label,string icon, bool isExpanded, Ext.Net.Node parentNode)
        {
            Ext.Net.Node node = new Ext.Net.Node();
            node.Text = Label;
            node.NodeID = ID;
            
            node.Icon = (Icon)Enum.Parse(typeof(Icon), icon);
            node.Expandable = isExpanded;
            node.Leaf = true;
            parentNode.Children.Add(node);
            return node;
        }

        private Node BuildParentNode(string ID, string Label, bool isExpanded, Ext.Net.Node parentNode)
        {
            Ext.Net.Node node = new Ext.Net.Node();
            node.Text = Label;
            node.NodeID = ID;
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
            Ext.Net.Node org = BuildLeafNode("rootParent_CS_ORG", Resources.Common.OrganizationChart, "Group", true, companyStructure);
            Ext.Net.Node departments = BuildLeafNode("rootParent_CS_DE", Resources.Common.Departments, "Group", true, companyStructure);
            Ext.Net.Node branches = BuildLeafNode("rootParent_CS_BR", Resources.Common.Branches, "Group", true, companyStructure);
            Ext.Net.Node divisions = BuildLeafNode("rootParent_CS_DI", Resources.Common.Divisions, "Group", true, companyStructure);
            Ext.Net.Node positions = BuildLeafNode("rootParent_CS_PO", Resources.Common.Positions, "Group", true, companyStructure);
            //Ext.Net.Node activities = BuildParentNode("rootParent_AC", Resources.Common.Activities, true, rootParent);
            //Ext.Net.Node transfers = BuildLeafNode("rootParent_AC_TR", Resources.Common.Transfers, "Group", true, activities);
            Ext.Net.Node systemSettings = BuildParentNode("rootParent_SY", Resources.Common.SystemSettings, true, rootParent);
            Ext.Net.Node nationalities = BuildLeafNode("rootParent_SY_NA", Resources.Common.Nationalities, "Group", true, systemSettings);
            
            Ext.Net.Node defaults = BuildLeafNode("rootParent_SY_DE", Resources.Common.SystemDefaults, "Group", true, systemSettings);
            Ext.Net.Node currencies = BuildLeafNode("rootParent_CS_CU", Resources.Common.Currencies, "Group", true, systemSettings);
            Ext.Net.Node users = BuildLeafNode("rootParent_CS_US", Resources.Common.Users, "Group", true, systemSettings);

            FillConfigItem(org, "org", "OrganizationChart.aspx", Resources.Common.OrganizationChart, "icon-Employees", "1");
            FillConfigItem(branches, "branches", "Branches.aspx", Resources.Common.Branches, "icon-Employees", "1");
            FillConfigItem(divisions, "divisions", "Divisions.aspx", Resources.Common.Divisions, "icon-Employees", "1");
            FillConfigItem(departments, "departments", "Departments.aspx", Resources.Common.Departments, "icon-Employees", "1");
            FillConfigItem(positions, "positions", "Positions.aspx", Resources.Common.Positions, "icon-Employees", "1");
            FillConfigItem(currencies, "currencies", "Currencies.aspx", Resources.Common.Currencies, "icon-Employees", "1");
            
            FillConfigItem(nationalities, "nationalities", "Nationalities.aspx", Resources.Common.Nationalities, "icon-Employees", "1");
            FillConfigItem(defaults, "defaults", "SystemDefaults.aspx", Resources.Common.SystemDefaults, "icon-Employees", "1");
            FillConfigItem(users, "users", "Users.aspx", Resources.Common.Users, "icon-Employees", "1");



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

            Ext.Net.Node vs = BuildLeafNode("rootParent_LM_VS", Resources.Common.VacationSchedules, "Group", true, leaveMgmt);
            Ext.Net.Node lt = BuildLeafNode("rootParent_LM_LT", Resources.Common.LeaveTypes, "Group", true, leaveMgmt);
            Ext.Net.Node lr = BuildLeafNode("rootParent_LM_LR", Resources.Common.LeaveRequests, "Group", true, leaveMgmt);
            Ext.Net.Node dts = BuildLeafNode("rootParent_TA_DT", Resources.Common.DayTypes, "Group", true, timeAt);
            Ext.Net.Node ad = BuildLeafNode("rootParent_TA_AD", Resources.Common.AttendanceDay, "Group", true, timeAt);
            Ext.Net.Node sc = BuildLeafNode("rootParent_TA_SC", Resources.Common.AttendanceSchedule, "Group", true, timeAt);
            Ext.Net.Node ca = BuildLeafNode("rootParent_TA_CA", Resources.Common.WorkingCalendars, "Group", true, timeAt);
            Ext.Net.Node bm = BuildLeafNode("rootParent_TA_BM", Resources.Common.BiometricDevices, "Group", true, timeAt);
            Ext.Net.Node ro = BuildLeafNode("rootParent_TA_RO", Resources.Common.Routers, "Group", true, timeAt);
            Ext.Net.Node dashboard = BuildLeafNode("rootParent_TA_dshboard", Resources.Common.Dashboard, "Group", true, timeAt);
            FillConfigItem(vs, "vacationSchedules", "VacationSchedules.aspx", Resources.Common.VacationSchedules, "icon-Employees", "1");
            FillConfigItem(lt, "lt", "LeaveTypes.aspx", Resources.Common.LeaveTypes, "icon-Employees", "1");
            FillConfigItem(lr, "lr", "LeaveRequests.aspx", Resources.Common.LeaveRequests, "icon-Employees", "1");
            FillConfigItem(dts, "dayTypes", "DayTypes.aspx", Resources.Common.DayTypes, "icon-Employees", "1");
            FillConfigItem(sc, "schedules", "Schedules.aspx", Resources.Common.AttendanceSchedule, "icon-Employees", "1");
            FillConfigItem(ca, "calendars", "WorkingCalendars.aspx", Resources.Common.WorkingCalendars, "icon-Employees", "1");
            FillConfigItem(bm, "bm", "BiometricDevices.aspx", Resources.Common.BiometricDevices, "icon-Employees", "1");
            FillConfigItem(ro, "ro", "Routers.aspx", Resources.Common.Routers, "icon-Employees", "1");
            FillConfigItem(ad, "ad", "TimeAttendanceView.aspx", Resources.Common.AttendanceDay, "icon-Employees", "1");
            FillConfigItem(dashboard, "dashboard", "Dashboard.aspx", Resources.Common.Dashboard, "icon-Employees", "1");
            nodes.Add(rootParent);
            return nodes;
        }
    }
}