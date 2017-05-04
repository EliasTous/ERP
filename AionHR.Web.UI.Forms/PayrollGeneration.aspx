<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollGeneration.aspx.cs" Inherits="AionHR.Web.UI.Forms.PayrollGeneration" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/PayrollGeneration.js"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript">
        function setStartEnd(s, e) {

            App.startDate.setValue(s);
            App.endDate.setValue(e);
            var d = moment(e, document.getElementById("DateFormat").value);
            App.payDate.setValue(d.toDate());
        }
    </script>

</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />

        <ext:Hidden ID="DateFormat" runat="server" />
        <ext:Hidden ID="CurrentPayId" runat="server" />
        <ext:Hidden ID="PeriodStatus0" runat="server" Text="<%$ Resources: Status0 %>" />
        <ext:Hidden ID="PeriodStatus1" runat="server" Text="<%$ Resources: Status1 %>" />


        <ext:Viewport ID="Viewport1" runat="server" Layout="CardLayout" ActiveIndex="0">
            <Items>
                <ext:Panel runat="server" ID="payrolls">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button runat="server" ID="AddButton" Text="<%$ Resources:Common,Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();App.direct.AddPayroll();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ComboBox QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" EmptyText="<%$ Resources: FieldYear %>" Name="year" runat="server" DisplayField="fiscalYear" ValueField="fiscalYear" ID="year">
                                    <Store>
                                        <ext:Store runat="server" ID="yearStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>

                                                        <ext:ModelField Name="fiscalYear" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Select Handler="App.payrollsStore.reload();">
                                        </Select>
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox ID="salaryTypeFilter" Name="salaryTypeFilter" runat="server" EmptyText="<%$ Resources:FieldPeriodType%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: SalaryWeekly%>" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryBiWeekly%>" Value="3"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryFourWeekly%>" Value="4"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryMonthly%>" Value="5"></ext:ListItem>
                                    </Items>
                                    <Listeners>
                                        <Select Handler="App.payrollsStore.reload();">
                                        </Select>
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox ID="status" Name="status" runat="server" EmptyText="<%$ Resources:FieldStatus%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: Status0%>" Value="0"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Status1%>" Value="1"></ext:ListItem>

                                    </Items>
                                    <Listeners>
                                        <Select Handler="App.payrollsStore.reload();">
                                        </Select>
                                    </Listeners>
                                </ext:ComboBox>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel runat="server" ID="payrollsGrid">
                            <Store>
                                <ext:Store runat="server" ID="payrollsStore" OnReadData="payrollsStore_ReadData">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="recordId">
                                            <Fields>
                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="fiscalYear" />

                                                <ext:ModelField Name="salaryType" />
                                                <ext:ModelField Name="periodId" />
                                                <ext:ModelField Name="status" />
                                                <ext:ModelField Name="startDate" />
                                                <ext:ModelField Name="endDate" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>

                                    <ext:DateColumn runat="server" ID="periodFrom" Text="<%$ Resources: FieldFrom%>" DataIndex="startDate" Width="150" />

                                    <ext:DateColumn runat="server" ID="periodTo" Text="<%$ Resources: FieldTo%>" DataIndex="endDate" Width="150" />

                                    <ext:Column runat="server" DataIndex="status" Text="<%$ Resources: FieldStatus%>" Flex="1">
                                        <Renderer Handler="return getStatusText(record.data['status']);" />
                                    </ext:Column>
                                    <ext:Column runat="server"
                                        ID="colEdit" Visible="true"
                                        Text=""
                                        Width="100"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="return editRender(); " />

                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <Render Handler="this.on('cellclick', cellClick);" />
                            </Listeners>
                            <DirectEvents>
                                <CellClick OnEvent="PoPuPHeader">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView1" runat="server" />
                            </View>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel
                    ID="EditRecordWindow"
                    runat="server"
                    Icon="PageEdit"
                    Header="false"
                    Width="450"
                    Height="330"
                    AutoShow="false"
                    Modal="true"
                    Hidden="False"
                    Layout="Fit">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
                                    <Listeners>
                                        <Click Handler="CheckSession(); App.direct.PayrollPages();" />
                                    </Listeners>


                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>

                        <ext:FormPanel DefaultButton="SaveButton"
                            ID="BasicInfoTab"
                            runat="server"
                            Header="false"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField runat="server" ID="payRef" Name="payRef" FieldLabel="<%$ Resources: FieldPayRef %>" AllowBlank="true" />
                                <ext:ComboBox QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldYear %>" Name="fiscalYear" runat="server" DisplayField="fiscalYear" ValueField="fiscalYear" ID="fiscalYear">
                                    <Store>
                                        <ext:Store runat="server" ID="fiscalyearStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>

                                                        <ext:ModelField Name="fiscalYear" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Select Handler="App.fiscalPeriodsStore.reload();">
                                        </Select>
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox ID="salaryType" Name="salaryType" runat="server" FieldLabel="<%$ Resources:FieldPeriodType%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: SalaryWeekly%>" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryBiWeekly%>" Value="3"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryFourWeekly%>" Value="4"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryMonthly%>" Value="5"></ext:ListItem>
                                    </Items>
                                    <Listeners>
                                        <Select Handler="App.fiscalPeriodsStore.reload();">
                                        </Select>
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldPeriod %>" Name="periodId" DisplayField="name" ValueField="recordId" runat="server" ID="periodId">
                                    <Store>
                                        <ext:Store runat="server" ID="fiscalPeriodsStore" OnReadData="fiscalPeriodsStore_ReadData">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Select Handler="App.direct.UpdateStartEndDate();">
                                        </Select>
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:TextField runat="server" ReadOnly="true" ID="startDate" Name="startDate" FieldLabel="<%$ Resources: FieldFrom %>" AllowBlank="false" />
                                <ext:TextField runat="server" ReadOnly="true" ID="endDate" Name="endDate" FieldLabel="<%$ Resources: FieldTo %>" AllowBlank="false" />
                                <ext:DateField runat="server" ID="payDate" Name="payDate" FieldLabel="<%$ Resources: FieldPayDate %>" AllowBlank="false" />
                                <ext:TextArea runat="server" ID="notes" Name="notes" FieldLabel="<%$ Resources: FieldNotes %>" />
                            </Items>

                        </ext:FormPanel>


                    </Items>
                    <Buttons>
                        <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;}  " />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                                    <ExtraParams>

                                        <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>

                    </Buttons>
                </ext:Panel>
                <ext:Panel ID="detailsPanel" runat="server">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false" Dock="Top">

                            <Items>
                                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
                                    <Listeners>
                                        <Click Handler="CheckSession(); App.direct.PayrollPages();" />
                                    </Listeners>


                                </ext:Button>
                                <ext:ComboBox runat="server" Width="130" LabelAlign="Top" EmptyText="<%$ Resources:FieldBranch%>" ValueField="recordId" DisplayField="name" ID="branchId" Name="branchId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Store>
                                        <ext:Store runat="server" ID="branchStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Change Handler="#{Store1}.reload()" />
                                    </Listeners>

                                </ext:ComboBox>

                                <ext:ComboBox runat="server" Width="155" EmptyText="<%$ Resources:FieldDepartment%>" LabelAlign="Top" ValueField="recordId" DisplayField="name" ID="departmentId" Name="departmentId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Store>
                                        <ext:Store runat="server" ID="departmentStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Change Handler="#{Store1}.reload()" />
                                    </Listeners>


                                </ext:ComboBox>
                                <ext:ComboBox runat="server" ID="employeeId" Width="130" LabelAlign="Top"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" EmptyText="<%$ Resources: FieldEmployee%>"
                                    TriggerAction="Query" ForceSelection="false">
                                    <Store>
                                        <ext:Store runat="server" ID="EmployeeStore" AutoLoad="false">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="fullName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillEmployee"></ext:PageProxy>
                                            </Proxy>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>

                                </ext:ComboBox>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel runat="server" ID="employeePayrolls">
                            <Store>
                                <ext:Store ID="Store1" runat="server" OnReadData="Store1_ReadData">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="seqNo">
                                            <Fields>
                                                <ext:ModelField Name="basicAmount" />
                                                <ext:ModelField Name="taxAmount" />
                                                <ext:ModelField Name="netSalary" />
                                                <ext:ModelField Name="name" IsComplex="true" />
                                                <ext:ModelField Name="branchName" />
                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="currencyName" />
                                                <ext:ModelField Name="calendarDays" />
                                                <ext:ModelField Name="calendarMinutes" />
                                                <ext:ModelField Name="workingDays" />
                                                <ext:ModelField Name="workingMinutes" />
                                                <ext:ModelField Name="seqNo" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column runat="server" DataIndex="name.fullName" Text="<%$ Resources: FieldEmployee%>" Flex="2" >
                                        <Renderer Handler="return record.data['name'].fullName;" />
                                        </ext:Column>
                                    <ext:Column runat="server" DataIndex="branchName" Text="<%$ Resources: FieldBranch%>" Flex="1" />
                                    <ext:Column runat="server" DataIndex="departmentName" Text="<%$ Resources: FieldDepartment%>" Flex="1" />
                                    <ext:Column runat="server" DataIndex="calendarDays" Text="<%$ Resources: FieldCalDays%>" Width="100" />
                                    <ext:Column runat="server" DataIndex="calendarMinutes" Text="<%$ Resources: FieldCalMinutes%>" Width="150" />
                                    <ext:Column runat="server" DataIndex="workingDays" Text="<%$ Resources: FieldDays%>" Width="100" />
                                    <ext:Column runat="server" DataIndex="workingMinutes" Text="<%$ Resources: FieldMinutes%>" Width="150" />
                                    <ext:Column runat="server" DataIndex="basicAmount" Text="<%$ Resources: FieldBasicAmount%>" />
                                    <ext:Column runat="server" DataIndex="taxAmount" Text="<%$ Resources: FieldTaxAmount%>" />
                                    <ext:Column runat="server" DataIndex="netSalary" Text="<%$ Resources: FieldNetSalary%>" />
                                    <ext:Column runat="server"
                                        ID="Column1" Visible="true"
                                        Text=""
                                        Width="100"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="return editRender()+'&nbsp;&nbsp;' +attachRender(); " />

                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                             <Listeners>
                                <Render Handler="this.on('cellclick', cellClick);" />
                            </Listeners>
                            <DirectEvents>
                                <CellClick OnEvent="PoPuPEM">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                        <ext:Parameter Name="basicAmount" Value="record.data['basicAmount']" Mode="Raw" />
                                        <ext:Parameter Name="taxAmount" Value="record.data['taxAmount']" Mode="Raw" />
                                        <ext:Parameter Name="netSalary" Value="record.data['netSalary']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView2" runat="server" />
                            </View>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>

        </ext:Viewport>

        <ext:Window
            ID="EditEMWindow"
            runat="server"
            Icon="PageEdit"
             Draggable="false"
              Maximizable="false" Resizable="false"
            Width="300"
            Height="200"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:FormPanel
                            ID="EditEMForm" DefaultButton="SaveDocumentButton"
                            runat="server"
                            
                             Header="false"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField runat="server" Name="seqNo" ID="seqNo" Hidden="true" Disabled="true" />
                                
                                <ext:TextField runat="server" Name="basicAmount" ID="basicAmount" FieldLabel="<%$ Resources: FieldBasicAmount %>" />
                                <ext:TextField runat="server" Name="taxAmount" ID="taxAmount" FieldLabel="<%$ Resources: FieldTaxAmount %>" />
                                <ext:TextField runat="server" ReadOnly="true" Name="netSalary" ID="netSalary" FieldLabel="<%$ Resources: FieldNetSalary %>" />

                                
                           </Items>

                        </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveDocumentButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditEMForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveEM" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditEMWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{seqNo}.getValue()" Mode="Raw" />
                                
                                <ext:Parameter Name="values" Value="#{EditEMForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>




    </form>
</body>
</html>
