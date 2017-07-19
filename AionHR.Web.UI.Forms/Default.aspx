<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AionHR.Web.UI.Forms.Default" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Tools.css" />

    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/app.js?id=3"></script>
    <script type="text/javascript" src="Scripts/Common.js"></script>
    <script type="text/javascript" src="Scripts/default.js?id=13"></script>

    <title>
        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Common , ApplicationTitle%>" />
    </title>
</head>
<body>
    <ext:Hidden runat="server" ID="lblChangePassword" Text="<%$Resources:Common , ChangePassword %>" />
    <ext:Hidden runat="server" ID="lblError" Text="<%$Resources:Common , Error %>" />
    <ext:Hidden runat="server" ID="lblOk" Text="<%$Resources:Common , Ok %>" />
    <ext:Hidden runat="server" ID="lblErrorOperation" Text="<%$Resources:Common , ErrorOperation %>" />
    <ext:Hidden runat="server" ID="lblLoading" Text="<%$Resources:Common , Loading %>" />
    <ext:Hidden runat="server" ID="sponsorsTitle" Text="<%$Resources: Common , Sponsors%>" />
    <ext:Hidden runat="server" ID="clTitle" Text="<%$Resources: Common , CertificateLevels%>" />
    <ext:Hidden runat="server" ID="edTitle" Text="<%$Resources: Common , EntitlementDeduction%>" />
    <ext:Hidden runat="server" ID="dtTitle" Text="<%$Resources: Common , DocumentTypes%>" />
    <ext:Hidden runat="server" ID="scrTitle" Text="<%$Resources: Common , SalaryChangeReasons%>" />
    <ext:Hidden runat="server" ID="acTitle" Text="<%$Resources: Common , AssetCategories%>" />
    <ext:Hidden runat="server" ID="btTitle" Text="<%$Resources: Common , BonusTypes%>" />

    <ext:Hidden runat="server" ID="ctTitle" Text="<%$Resources: Common , CheckTypes%>" />
    <ext:Hidden runat="server" ID="deTitle" Text="<%$Resources:Common , Departments %>" />
    <ext:Hidden runat="server" ID="brTitle" Text="<%$Resources:Common , Branches %>" />
    <ext:Hidden runat="server" ID="diTitle" Text="<%$Resources:Common , Divisions %>" />
    <ext:Hidden runat="server" ID="poTitle" Text="<%$Resources:Common , Positions %>" />
    <ext:Hidden runat="server" ID="naTitle" Text="<%$Resources:Common , Nationalities %>" />
    <ext:Hidden runat="server" ID="sysdeTitle" Text="<%$Resources:Common , SystemDefaults %>" />
    <ext:Hidden runat="server" ID="cuTitle" Text="<%$Resources:Common , Currencies %>" />
    <ext:Hidden runat="server" ID="usTitle" Text="<%$Resources:Common , Users %>" />
    <ext:Hidden runat="server" ID="mcTitle" Text="<%$Resources:Common , MediaCategory %>" />
    <ext:Hidden runat="server" ID="caTitle" Text="<%$Resources:Common , WorkingCalendars %>" />
    <ext:Hidden runat="server" ID="roTitle" Text="<%$Resources:Common , Routers %>" />
    <ext:Hidden runat="server" ID="scTitle" Text="<%$Resources:Common , AttendanceSchedule %>" />
    <ext:Hidden runat="server" ID="bmTitle" Text="<%$Resources:Common , BiometricDevices %>" />
    <ext:Hidden runat="server" ID="tadtTitle" Text="<%$Resources:Common , DayTypes %>" />
    <ext:Hidden runat="server" ID="gfTitle" Text="<%$Resources:Common , Geofences %>" />
    <ext:Hidden runat="server" ID="vsTitle" Text="<%$Resources:Common , VacationSchedules %>" />
    <ext:Hidden runat="server" ID="pyye" Text="<%$Resources:Common , FiscalYears %>" />
    <ext:Hidden runat="server" ID="ltTitle" Text="<%$Resources:Common , LeaveTypes %>" />
    <ext:Hidden runat="server" ID="loTitle" Text="<%$Resources:Common , LoanOverrides %>" />
    <ext:Hidden runat="server" ID="pyde" Text="<%$Resources:Common , PayrollDefaults %>" />
    <ext:Hidden runat="server" ID="foTitle" Text="<%$Resources:Common , Folders %>" />
    <ext:Hidden runat="server" ID="rtTitle" Text="<%$Resources:Common , RelationshipTypes %>" />
    <ext:Hidden runat="server" ID="ltltTitle" Text="<%$Resources:Common , LoanTypes %>" />
    <ext:Hidden runat="server" ID="cdtTitle" Text="<%$Resources:Common , CompanyDocumentTypes %>" />
    <ext:Hidden runat="server" ID="systTitle" Text="<%$Resources:Common , States %>" />
    <ext:Hidden runat="server" ID="loansync" Text="<%$Resources:Common , LoanSync %>" />
    <ext:Hidden runat="server" ID="importAt" Text="<%$Resources:Common , ImportAttendance %>" />
    <ext:Hidden runat="server" ID="importDE" Text="<%$Resources:Common , ImportDepartments %>" />
    <ext:Hidden runat="server" ID="importLoansTitle" Text="<%$Resources:Common , ImportLoans %>" />
    <ext:Hidden runat="server" ID="importLeavesTitle" Text="<%$Resources:Common , ImportLeaves %>" />
    <ext:Hidden runat="server" ID="importNotesTitle" Text="<%$Resources:Common , ImportNotes %>" />
    <ext:Hidden runat="server" ID="alsgTitle" Text="<%$Resources:Common , SecurityGroups %>" />
    <ext:Hidden runat="server" ID="importEmployeesTitle" Text="<%$Resources:Common , ImportEmployees %>" />
    <ext:Hidden runat="server" ID="importJobInfoTitle" Text="<%$Resources:Common , ImportJobInfo %>" />
    
    <ext:Hidden runat="server" ID="aaTitle" Text="<%$Resources:Common , SystemAlerts %>" />
    <ext:Hidden runat="server" ID="ttTitle" Text="<%$Resources:Common , TaskTypes %>" />
    <ext:Hidden runat="server" ID="TrType1" Text="<%$Resources:Common , TrType1 %>" />
    <ext:Hidden runat="server" ID="TrType2" Text="<%$Resources:Common , TrType2 %>" />
    <ext:Hidden runat="server" ID="TrType3" Text="<%$Resources:Common , TrType3 %>" />
    <ext:Hidden runat="server" ID="TrType4" Text="<%$Resources:Common , TrType4 %>" />
    <ext:Hidden runat="server" ID="TrType5" Text="<%$Resources:Common , TrType5 %>" />
    <ext:Hidden runat="server" ID="TrType11" Text="<%$Resources:Common , TrType11 %>" />
    <ext:Hidden runat="server" ID="TrType12" Text="<%$Resources:Common , TrType12 %>" />
    <ext:Hidden runat="server" ID="TrType21" Text="<%$Resources:Common , TrType21 %>" />

    <ext:Hidden runat="server" ID="CurrentClassRef" />
    <ext:Hidden runat="server" ID="CurrentRecordId" />
    <ext:Hidden runat="server" ID="activeModule" />
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" IDMode="Explicit" AjaxTimeout="1200000" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="topPanel" runat="server" Title="AionHR" Height="60" Header="false" Region="North" PaddingSpec="0 0 1 0" BodyStyle="background-color:#157fcc">
                <Items>
                    <ext:Container runat="server">
                        <Content>
                            <div class="logoImage">
                                <img src="Images/logo-light.png" style="margin-top: 20px; margin-left: 5px; margin-right: 5px;" width="73" height="20" />
                            </div>
                            <div class="title">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Common ,ApplicationModule%>" /><asp:Literal ID="CompanyNameLiteral" runat="server" Text="" /></span>
                                <br />
                                <asp:Literal ID="username" runat="server" Text="" />
                            </div>
                            <div class="buttons">

                                <a id="btnChangeLanguage" class="button" href="#">
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Common ,LanguageSwitch%>" /></a>
                                <a id="btnLogout" class="button" href="#">
                                    <img src="Images/logoff.png" />
                                </a>

                            </div>
                        </Content>
                    </ext:Container>
                </Items>


            </ext:Panel>
            <ext:Panel ID="leftPanel" runat="server" Region="West" Layout="FitLayout" AutoUpdateLayout="true" Width="260" PaddingSpec="0 0 0 0" Padding="0"
                Header="true" Collapsible="true" Split="true" CollapseMode="Mini" StyleSpec="border-bottom:2px solid #2A92D4;"
                Title="<%$ Resources:Common , NavigationPane %>" CollapseToolText="<%$ Resources:Common , CollapsePanel %>" ExpandToolText="<%$ Resources:Common , ExpandPanel %>" Icon="ApplicationTileVertical" BodyBorder="0">
                <HeaderConfig Height="40">
                    <Items>
                        <ext:Button ID="b1" runat="server" Icon="PageGear" ToolTip="<%$ Resources:Common , EmployeeFiles %>">
                            <Listeners>
                                <%--<Click Handler="#{commonTree}.setTitle(this.tooltip);openModule(4);" />--%>
                            </Listeners>
                            <Menu>

                                <ext:Menu runat="server">

                                    <Items>
                                        <ext:MenuItem runat="server" Text="<%$ Resources:Common , EmployeeFiles %>">

                                            <Menu>
                                                <ext:Menu runat="server">
                                                    <Items>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Sponsors%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('sponsors', 'Sponsors.aspx', #{sponsorsTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , CertificateLevels%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('certificateLevels', 'CertificateLevels.aspx', #{clTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , EntitlementDeduction%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('entitlementDeductions', 'EntitlementDeductions.aspx', #{edTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , DocumentTypes%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('documentTypes', 'DocumentTypes.aspx', #{dtTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , SalaryChangeReasons%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('salaryChangeReasons', 'SalaryChangeReasons.aspx', #{scrTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , AssetCategories%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('assetCategories', 'AssetCategories.aspx', #{acTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , BonusTypes%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('bonusTypes', 'BonusTypes.aspx', #{btTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , CheckTypes%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('checkTypes', 'CheckTypes.aspx', #{ctTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>

                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , RelationshipTypes%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('relationshipTypes', 'RelationshipTypes.aspx', #{rtTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>

                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , LoanTypes%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('loanTypes', 'LoanTypes.aspx', #{ltltTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>

                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , TaskTypes%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('taskTypes', 'TaskTypes.aspx', #{ttTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , ImportLoans %>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('importLoans', 'ImportLoans.aspx', #{importLoansTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , ImportLeaves%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('importLeaves', 'ImportLeaves.aspx', #{importLeavesTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , ImportNotes%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('importNotes', 'ImportEmployeeNotes.aspx', #{importNotesTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                            <ext:MenuItem runat="server" Text="<%$Resources: Common , ImportEmployees%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('importEmployees', 'ImportEmployees.aspx', #{importEmployeesTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , ImportJobInfo%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('importJobInfo', 'ImportJobInfo.aspx', #{importJobInfoTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                        </ext:MenuItem>
                                        <ext:MenuItem runat="server" Text="<%$ Resources:Common , Company %>">

                                            <Menu>
                                                <ext:Menu runat="server">
                                                    <Items>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , CompanyStructure%>">
                                                            <Menu>
                                                                <ext:Menu runat="server">
                                                                    <Items>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Departments%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('departments', 'Departments.aspx', #{deTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Branches%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('branches', 'Branches.aspx', #{brTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Divisions%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('divisions', 'Divisions.aspx', #{diTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Positions%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('positions', 'Positions.aspx', #{poTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , ImportDepartments%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('import-departments', 'ImportDepartments.aspx', #{importDE}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                    </Items>
                                                                </ext:Menu>
                                                            </Menu>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , SystemSettings%>">
                                                            <Menu>
                                                                <ext:Menu runat="server">
                                                                    <Items>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Nationalities%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('nationalieis', 'Nationalities.aspx', #{naTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , States%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('states', 'States.aspx', #{systTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , SystemAlerts%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('SystemAlerts', 'SystemAlerts.aspx', #{aaTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , SystemDefaults%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('systemDefaults', 'SystemDefaults.aspx', #{sysdeTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Currencies%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('currencies', 'Currencies.aspx', #{cuTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Users%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('users', 'Users.aspx', #{usTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources:Common,AccessControl %>">
                                                                            <Menu>
                                                                                <ext:Menu runat="server">
                                                                                    <Items>
                                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , SecurityGroups%>">
                                                                                            <Listeners>
                                                                                                <Click Handler="openNewTab('sg', 'SecurityGroups.aspx', #{alsgTitle}.value, 'icon-Employees')" />
                                                                                            </Listeners>
                                                                                        </ext:MenuItem>
                                                                                    </Items>
                                                                                </ext:Menu>
                                                                            </Menu>
                                                                        </ext:MenuItem>
                                                                    </Items>
                                                                </ext:Menu>
                                                            </Menu>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Files%>">
                                                            <Menu>
                                                                <ext:Menu runat="server">
                                                                    <Items>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , MediaCategory%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('mediaCategory', 'MediaCategories.aspx', #{mcTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>

                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Folders%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('folders', 'Folders.aspx', #{foTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>

                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , CompanyDocumentTypes%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('companyDocumentTypes', 'CompanyDocumentTypes.aspx', #{cdtTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>

                                                                    </Items>
                                                                </ext:Menu>
                                                            </Menu>
                                                        </ext:MenuItem>
                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                        </ext:MenuItem>
                                        <ext:MenuItem runat="server" Text="<%$ Resources:Common , Scheduler%>">

                                            <Menu>
                                                <ext:Menu runat="server">
                                                    <Items>
                                                        <ext:MenuItem runat="server" Text="<%$ Resources:Common , TimeAttendance%>">
                                                            <Menu>
                                                                <ext:Menu runat="server">
                                                                    <Items>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Routers%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('ro', 'Routers.aspx', #{roTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , WorkingCalendars%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('calendars', 'WorkingCalendars.aspx', #{caTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , AttendanceSchedule%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('schedules', 'Schedules.aspx', #{scTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , BiometricDevices%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('biometricDevices', 'BiometricDevices.aspx', #{bmTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , DayTypes%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('dayTypes', 'DayTypes.aspx', #{tadtTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , Geofences%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('Geofences', 'Geofences.aspx', #{gfTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , ImportAttendance%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('Import', 'ImportAttendance.aspx',#{importAt}.value , 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                    </Items>
                                                                </ext:Menu>
                                                            </Menu>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$ Resources:Common , LeaveManagement%>">
                                                            <Menu>
                                                                <ext:Menu runat="server">
                                                                    <Items>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , VacationSchedules%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('VacationSchedules', 'VacationSchedules.aspx', #{vsTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , LeaveTypes%>">
                                                                            <Listeners>
                                                                                <Click Handler="openNewTab('LeaveTypes', 'LeaveTypes.aspx', #{ltTitle}.value, 'icon-Employees')" />
                                                                            </Listeners>
                                                                        </ext:MenuItem>
                                                                    </Items>
                                                                </ext:Menu>

                                                            </Menu>
                                                        </ext:MenuItem>
                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                        </ext:MenuItem>
                                        <ext:MenuItem runat="server" Text="<%$ Resources:Common , Payroll %>">
                                            <Menu>
                                                <ext:Menu runat="server">
                                                    <Items>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , PayrollDefaults%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('payrollDefaults', 'PayrollDefaults.aspx', #{pyde}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , LoanOverrides%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('loanOverrides', 'LoanOverrides.aspx', #{loTitle}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , FiscalYears%>">
                                                            <Listeners>
                                                                <Click Handler="openNewTab('fiscalYears', 'FiscalYears.aspx', #{pyye}.value, 'icon-Employees')" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="<%$Resources: Common , LoanSync%>">
                                                            <DirectEvents>
                                                                <Click OnEvent="SyncLoanDeductions" />
                                                            </DirectEvents>
                                                        </ext:MenuItem>

                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                        </ext:MenuItem>

                                    </Items>
                                </ext:Menu>
                            </Menu>
                        </ext:Button>
                    </Items>
                </HeaderConfig>
                <BottomBar>
                    <ext:Toolbar runat="server">
                        <Items>
                        </Items>
                    </ext:Toolbar>
                </BottomBar>
                <TopBar>
                    <ext:Toolbar ID="Toolbar1" runat="server" Border="true">
                        <Items>
                            <ext:Label runat="server" Text="<%$ Resources:Common , Modules %>" />
                            <ext:Button ID="btnEmployeeFiles" runat="server" Icon="Group" ToolTip="<%$ Resources:Common , EmployeeFiles %>">
                                <Listeners>
                                    <%--<Click Handler="#{commonTree}.setTitle(this.tooltip);openModule(1);" />--%>
                                    <Click Handler="openModule(1);" />
                                </Listeners>
                                <Menu>
                                </Menu>
                            </ext:Button>



                            <ext:ToolbarSeparator runat="server"></ext:ToolbarSeparator>
                            <ext:Button ID="btnCompany" runat="server" Icon="Building" ToolTip="<%$ Resources:Common , Company %>">
                                <Listeners>
                                    <%--<Click Handler="#{commonTree}.setTitle(this.tooltip);openModule(3);" />--%>
                                    <Click Handler="openModule(3);" />
                                </Listeners>


                            </ext:Button>
                            <ext:ToolbarSeparator runat="server"></ext:ToolbarSeparator>
                            <ext:Button ID="btnScheduler" runat="server" Icon="CalendarSelectDay" ToolTip="<%$ Resources:Common , Scheduler %>">
                                <Listeners>
                                    <%--<Click Handler="#{commonTree}.setTitle(this.tooltip);openModule(4);" />--%>
                                    <Click Handler="openModule(4);  " />
                                </Listeners>
                                <Menu>
                                </Menu>
                            </ext:Button>
                            <ext:ToolbarSeparator runat="server"></ext:ToolbarSeparator>
                            <ext:Button ID="btnReport" runat="server" Icon="ChartBar" ToolTip="<%$ Resources:Common , Reports %>">
                                <Listeners>
                                    <Click Handler="openModule(5);" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator runat="server"></ext:ToolbarSeparator>
                            <ext:Button ID="btnPayroll" runat="server" Icon="MoneyDollar" ToolTip="<%$ Resources:Common , Payroll %>">
                                <Listeners>
                                    <Click Handler="openModule(6);" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarFill runat="server" />



                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:TreePanel runat="server" AutoUpdateLayout="true" RootVisible="false" ID="commonTree" Scroll="Vertical">
                        <SelectionModel>
                            <ext:TreeSelectionModel runat="server" ID="selModel">
                            </ext:TreeSelectionModel>
                        </SelectionModel>
                        <TopBar>
                            <ext:Toolbar ID="Toolbar2" runat="server" Hidden="true">
                                <Items>
                                    <ext:TextField ID="filerTreeTrigger" runat="server" EnableKeyEvents="true" Width="150" EmptyText="<%$ Resources:Common , Filter %>">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyUp Fn="filterCommonTree" Buffer="100" />
                                            <TriggerClick Handler="clearFilter();" />
                                        </Listeners>
                                    </ext:TextField>
                                    <ext:Button ID="btnExpandAll" runat="server" ToolTip="<%$ Resources:Common , ExpandAll %>" IconCls="icon-expand-all">
                                        <Listeners>
                                            <Click Handler="#{commonTree}.expandAll();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:ToolbarSeparator>
                                    </ext:ToolbarSeparator>
                                    <ext:Button ID="btnCollapseAll" runat="server" ToolTip="<%$ Resources:Common , CollapseAll %>" IconCls="icon-collapse-all">
                                        <Listeners>
                                            <Click Handler="#{commonTree}.collapseAll();" />
                                        </Listeners>
                                    </ext:Button>

                                </Items>
                            </ext:Toolbar>
                        </TopBar>


                        <Fields>
                            <ext:ModelField Name="idTab" />
                            <ext:ModelField Name="url" />
                            <ext:ModelField Name="title" />
                            <ext:ModelField Name="css" />
                            <ext:ModelField Name="click" />
                        </Fields>

                        <Listeners>
                            <ItemClick Handler="CheckSession(); onTreeItemClick(record, e);" />
                        </Listeners>

                    </ext:TreePanel>
                    <%--    <ext:Panel
                        ID="pnlAlignMiddle"
                        runat="server"
                        Title="Buttons"
                        Layout="VBoxLayout"
                        BodyPadding="5">
                        <Defaults>
                            <ext:Parameter Name="margin" Value="0 0 5 0" Mode="Value" />
                        </Defaults>
                        <LayoutConfig>
                            <ext:VBoxLayoutConfig Align="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Button
                                runat="server"
                                Text="Paste"
                                Icon="User"
                                Scale="Large"
                                IconAlign="Top"
                                Cls="x-btn-as-arrow"
                                RowSpan="3" />
                            <ext:Button
                                runat="server"
                                Text="Paste"
                                Icon="User"
                                Scale="Large"
                                IconAlign="Top"
                                Cls="x-btn-as-arrow"
                                RowSpan="3" />
                            <ext:Button
                                runat="server"
                                Text="Paste"
                                Icon="User"
                                Scale="Large"
                                IconAlign="Top"
                                Cls="x-btn-as-arrow"
                                RowSpan="3" />

                        </Items>
                    </ext:Panel>

                    <ext:Panel runat="server" Title="Settings" />
                    <ext:Panel runat="server" Title="Even More Stuff" />
                    <ext:Panel runat="server" Title="My Stuff" />--%>
                </Items>
            </ext:Panel>
            <ext:TabPanel ID="tabPanel" runat="server" Region="Center" EnableTabScroll="true" MinTabWidth="100" BodyBorder="0" StyleSpec="border-bottom:2px solid #2A92D4;">
                <Items>
                    <ext:Panel ID="tabHome" Closable="false" runat="server" Title="<%$ Resources:Common , Home %>" Icon="House">
                        <Loader ID="Loader1"
                            runat="server"
                            Url="MainDashboard.aspx"
                            Mode="Frame"
                            ShowMask="true">
                            <LoadMask ShowMask="true" />
                        </Loader>
                    </ext:Panel>



                </Items>
            </ext:TabPanel>
        </Items>
    </ext:Viewport>
    <ext:Window
        ID="TransationLogScreen"
        runat="server" Title="<%$ Resources:Common,History %>"
        Icon="PageEdit"
        Width="450"
        Height="500"
        AutoShow="false"
        Modal="true"
        Hidden="true"
        Layout="Fit">

        <Items>
            <ext:GridPanel runat="server"
                ID="transactionLogGrid"
                PaddingSpec="0 0 1 0"
                Header="false"
                Layout="FitLayout"
                Scroll="Vertical"
                Border="false"
                Icon="User"
                ColumnLines="True" IDMode="Explicit" RenderXType="True">
                <Store>
                    <ext:Store runat="server" ID="transactionLogStore"
                        RemoteSort="True"
                        RemoteFilter="true"
                        OnReadData="TransactionLog_RefreshData"
                        PageSize="30" IDMode="Explicit" Namespace="App" IsPagingStore="true">
                        <Model>
                            <ext:Model runat="server" IDProperty="recordId">
                                <Fields>
                                    <ext:ModelField Name="recordId" />
                                    <ext:ModelField Name="userName" />
                                    <ext:ModelField Name="classId" />
                                    <ext:ModelField Name="pk" />
                                    <ext:ModelField Name="userId" />
                                    <ext:ModelField Name="type" />
                                    <ext:ModelField Name="eventDt" />
                                    <ext:ModelField Name="data" />

                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel runat="server">
                    <Columns>
                        <ext:DateColumn ID="transactionDate" runat="server" DataIndex="eventDt" Text="<%$ Resources:Common , FieldDate %>" Width="180" />
                        <ext:Column runat="server" DataIndex="userName" Text="<%$ Resources:Common , FieldUsername %>" Flex="1" />
                        <ext:Column runat="server" DataIndex="type" Text="<%$ Resources:Common , FieldChangeType %>" Width="75">
                            <Renderer Handler="return GetChangeTypeString(record.data['type']);" />
                        </ext:Column>
                        <ext:Column runat="server"
                            ID="colEdit" Visible="true"
                            Text=""
                            Width="60"
                            Hideable="false"
                            Align="Center"
                            Fixed="true"
                            Filterable="false"
                            MenuDisabled="true"
                            Resizable="false">

                            <Renderer Handler="return attachRender(); " />
                        </ext:Column>

                    </Columns>

                </ColumnModel>
                <Listeners>
                    <Render Handler="this.on('cellclick', cellClick);" />
                </Listeners>
                <DirectEvents>
                    <CellClick OnEvent="PoPuP">
                        <EventMask ShowMask="true" />
                        <ExtraParams>
                            <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                            <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                        </ExtraParams>

                    </CellClick>
                </DirectEvents>
                <BottomBar>

                    <ext:PagingToolbar ID="PagingToolbar1"
                        runat="server"
                        FirstText="<%$ Resources:Common , FirstText %>"
                        NextText="<%$ Resources:Common , NextText %>"
                        PrevText="<%$ Resources:Common , PrevText %>"
                        LastText="<%$ Resources:Common , LastText %>"
                        RefreshText="<%$ Resources:Common ,RefreshText  %>"
                        BeforePageText="<%$ Resources:Common ,BeforePageText  %>"
                        AfterPageText="<%$ Resources:Common , AfterPageText %>"
                        DisplayInfo="true"
                        DisplayMsg="<%$ Resources:Common , DisplayMsg %>"
                        Border="true"
                        EmptyMsg="<%$ Resources:Common , EmptyMsg %>">
                        <Items>
                        </Items>
                        <Listeners>
                            <BeforeRender Handler="this.items.removeAt(this.items.length - 2);" />
                        </Listeners>
                    </ext:PagingToolbar>

                </BottomBar>
            </ext:GridPanel>
        </Items>

    </ext:Window>

    <ext:Window ID="logBodyScreen" runat="server" Title="<%$ Resources:Common,History %>"
        Icon="PageEdit"
        Width="450"
        Height="500"
        AutoShow="false"
        Modal="true"
        Hidden="true"
        Layout="Fit">
        <Items>
            <ext:FormPanel runat="server" ID="logBodyForm" Layout="FitLayout">
                <Items>
                    <ext:TextArea runat="server" ID="bodyText" Name="data" ReadOnly="true" />
                </Items>
            </ext:FormPanel>
        </Items>
    </ext:Window>
</body>
</html>
