<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobInformation.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.JobInformation" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/JobInformation.js?id=9"></script>
    <script type="text/javascript" src="../Scripts/common.js"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server"  />
        <ext:Hidden ID="PaymentTypeWeekly" runat="server" Text="<%$ Resources: PaymentTypeWeekly %>" />
        <ext:Hidden ID="PaymentTypeMonthly" runat="server" Text="<%$ Resources: PaymentTypeMonthly %>" />
        <ext:Hidden ID="PaymentTypeDaily" runat="server" Text="<%$ Resources: PaymentTypeDaily %>" />
        <ext:Hidden ID="PaymentMethodCash" runat="server" Text="<%$ Resources: PaymentMethodCash %>" />
        <ext:Hidden ID="PaymentMethodBank" runat="server" Text="<%$ Resources: PaymentMethodBank %>" />
          <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
        


        <Items>

                <ext:GridPanel AutoUpdateLayout="true"
                    ID="employeementHistoryGrid" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="true"
                    Title="<%$ Resources: EHGridTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical" Flex="1"
                    Border="false" MaxHeight="200"
                    Icon="User" DefaultAnchor="100%"  
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="employeementHistoryStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="employeementHistory_RefreshData"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="statusName" />
                                        <ext:ModelField Name="date" ServerMapping="date.ToShortDateString()" />

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewEH">
                                            <EventMask ShowMask="true" CustomTarget="={#{employeementHistoryGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{employeementHistoryStore}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column CellCls="cellLink" ID="ColEHName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEHStatus%>" DataIndex="statusName" Flex="2" Hideable="false">
                                <Renderer Handler="return '<u>'+ record.data['statusName']+'</u>'">
                                </Renderer>
                            </ext:Column>
                            <ext:Column CellCls="cellLink" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEHDate%>" DataIndex="date" Flex="2" Hideable="false" />



                            <ext:Column runat="server"
                                ID="colEdit" Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="ColEHDelete" Flex="1" Visible="true"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="deleteRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colAttach"
                                Text="<%$ Resources:Common, Attach %>"
                                Hideable="false"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="attachRender" />
                            </ext:Column>




                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar2" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar1" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                
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
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
                <ext:GridPanel Visible="True"
                    ID="JobInfoGrid" AutoUpdateLayout="true" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"   MaxHeight="200"
                    Header="true"
                    Title="<%$ Resources: JIGridTitle %>"
                    Layout="FitLayout" Flex="1"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="JIStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="JIStore_RefreshData"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model2" runat="server" IDProperty="recordId">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="date" />
                                        <ext:ModelField Name="departmentName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="divisionName" />
                                        
                                        

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewJI">
                                            <EventMask ShowMask="true" CustomTarget="={#{JobInfoGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                <ext:TextField ID="TextField1" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{JIGrid}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="1" ID="ColJIName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldJIDate %>" DataIndex="date" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="1" ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldJIDepartment %>" DataIndex="departmentName" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="1" ID="Column7" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldJIBranch%>" DataIndex="branchName" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="1" ID="Column8" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldJIPosition%>" DataIndex="positionName" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="1" ID="Column9" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldJIDivision%>" DataIndex="divisionName" Hideable="false" Width="75" Align="Center" />
                          



                            <ext:Column runat="server"
                                ID="Column4" Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="ColJIDelete" Flex="1" Visible="true"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="deleteRender" />

                            </ext:Column>
                            <ext:Column runat="server" Visible="false"
                                ID="Column6"
                                Text="<%$ Resources:Common, Attach %>"
                                Hideable="false"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="attachRender" />
                            </ext:Column>




                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar4" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar2" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                    
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
                    <View>
                        <ext:GridView ID="GridView2" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
                  <ext:GridPanel Visible="True"
                    ID="SalaryGrid" AutoUpdateLayout="true" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"   MaxHeight="200"
                    Header="true"
                    Title="<%$ Resources: SAGridTitle %>"
                    Layout="FitLayout"
                    Flex="1"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="SAStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="SAStore_Refresh"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model3" runat="server" IDProperty="recordId">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="currencyId" />
                                        <ext:ModelField Name="currencyName" />
                                        <ext:ModelField Name="scrId" />
                                        <ext:ModelField Name="scrName" />
                                        <ext:ModelField Name="effectiveDate" ServerMapping="effectiveDate.ToShortDateString()" />
                                        <ext:ModelField Name="salaryType" />
                                        <ext:ModelField Name="paymentFrequency" />
                                        <ext:ModelField Name="paymentMethod" />
                                        <ext:ModelField Name="isTaxable" />
                                        <ext:ModelField Name="bankName" />
                                        <ext:ModelField Name="accountNumber" />
                                        <ext:ModelField Name="comments" />
                                        <ext:ModelField Name="basicAmount" />
                                        <ext:ModelField Name="finalAmount" />
                                        
                                        

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar5" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button6" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewSA">
                                            <EventMask ShowMask="true" CustomTarget="={#{SalaryGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFill2" runat="server" />
                                <ext:TextField ID="TextField2" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{SalaryGrid}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="recID" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="1" ID="cc" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldScrName %>" DataIndex="scrName" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="1" ID="ColSAName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEffectiveDate %>" DataIndex="effectiveDate" Hideable="false" Width="75" Align="Center" >
                                     <Renderer Handler="return '<u>'+ record.data['effectiveDate']+'</u>'">
                                </Renderer>
                           
                                </ext:Column>
                            <ext:Column Flex="1" ID="Column13" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSalaryType %>" DataIndex="salaryType" Hideable="false" Width="75" Align="Center" >
                                    <Renderer Handler="return getPaymentTypeString(record.data['salaryType'])" />
                                </ext:Column>
                           
                            <ext:Column Flex="1" ID="Column14" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPaymentFrequency %>" DataIndex="paymentFrequency" Hideable="false" Width="75" Align="Center" >
                                <Renderer Handler="return getPaymentTypeString(record.data['paymentFrequency'])" />
                                </ext:Column>
                            <ext:Column Flex="1" ID="Column15" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPaymentMethod %>" DataIndex="paymentMethod" Hideable="false" Width="75" Align="Center" >
                                <Renderer Handler="return getPaymentMethodString(record.data['paymentMethod'])" />
                                </ext:Column>
                            <ext:CheckColumn Flex="1" ID="Column19" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIsTaxable %>" DataIndex="isTaxable" Hideable="false" Width="75" Align="Center" />
                          <ext:Column Flex="1" ID="Column20" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBasicAmount %>" DataIndex="basicAmount" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="1" ID="Column21" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFinalAmount %>" DataIndex="finalAmount" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="1" ID="Column22" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSACurrencyName %>" DataIndex="currencyName" Hideable="false" Width="75" Align="Center" />



                            <ext:Column runat="server"
                                ID="Column16" Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="ColSADelete" Flex="1" Visible="true"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="deleteRender" />

                            </ext:Column>
                            <ext:Column runat="server" Visible="false"
                                ID="Column18"
                                Text="<%$ Resources:Common, Attach %>"
                                Hideable="false"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="attachRender" />
                            </ext:Column>




                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar6" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar3" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                    
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
                    <View>
                        <ext:GridView ID="GridView3" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel2" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
       </ext:Viewport>

        <ext:Window
            ID="EditEHwindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditEHWindowTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="EditEHForm" DefaultButton="SaveEHButton"
                            runat="server"
                            Title="<%$ Resources: EditEHWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField runat="server" Name="recordId"  ID="EHID" Hidden="true"  Disabled="true"/>
                                <ext:ComboBox ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="statusId" Name="statusId" FieldLabel="<%$ Resources:FieldEHStatus%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="EHStatusStore">
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
                                </ext:ComboBox>
                                <ext:DateField runat="server" Name="date" FieldLabel="<%$ Resources:FieldEHDate%>" AllowBlank="false" />
                                <ext:TextArea runat="server" Name="comment" FieldLabel="<%$ Resources:FieldEHComment%>" />
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveEHButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditEHForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveEH" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditJobInfoWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{EHID}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditEHForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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

        <ext:Window
            ID="EditJobInfoWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditJobInfoWindowsTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="EditJobInfoTab" DefaultButton="SaveJIButton"
                            runat="server"
                            Title="<%$ Resources: EditJobInfoWindow %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="JIID" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Disabled="true" Name="recordId" />
                                <ext:DateField runat="server" ID="date" Name="date" FieldLabel="<%$ Resources:FieldJIDate%>" />
                           <ext:ComboBox    runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="departmentId" Name="departmentId" FieldLabel="<%$ Resources:FieldJIDepartment%>" SimpleSubmit="true">
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
                                                    <RightButtons>
                                                    <ext:Button ID="Button2" runat="server" Icon="Add" Hidden="true"  >
                                                    </ext:Button>
                                                </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                            </ext:ComboBox>
                           <ext:ComboBox  runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldJIBranch%>" SimpleSubmit="true">
                                                <Store>
                                                    <ext:Store runat="server" ID="BranchStore">
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
                                                    <RightButtons>
                                                    <ext:Button ID="Button4" runat="server" Icon="Add" Hidden="true"  >
                                                    </ext:Button>
                                                </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                            </ext:ComboBox>
                                  <ext:ComboBox   runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="divisionId" Name="divisionId" FieldLabel="<%$ Resources:FieldJIDivision%>" SimpleSubmit="true">
                                                <Store>
                                                    <ext:Store runat="server" ID="divisionStore">
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
                                                    <RightButtons>
                                                    <ext:Button ID="Button7" runat="server" Icon="Add" Hidden="true"  >
                                                    </ext:Button>
                                                </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                            </ext:ComboBox>
                        <ext:ComboBox   ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="positionId" Name="positionId" FieldLabel="<%$ Resources:FieldJIPosition%>" SimpleSubmit="true">
                                                <Store>
                                                    <ext:Store runat="server" ID="positionStore">
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
                                                    <RightButtons>
                                                    <ext:Button ID="Button5" runat="server" Icon="Add" Hidden="true"  >
                                                    </ext:Button>
                                                </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                            </ext:ComboBox>

                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveJIButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditJobInfoTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveJI" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditJobInfoWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{JIID}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditJobInfoTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

            <ext:Window
            ID="EditSAWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditSAWindowTitle %>"
            Width="650"
            Height="300"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="EditSAForm" DefaultButton="SaveSAButton"
                            runat="server"
                            Title="<%$ Resources: EditSAWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" Layout="ColumnLayout"
                            BodyPadding="5">
                            <Items>
                                <ext:Panel runat="server" ><Items> <ext:TextField ID="SAId" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Disabled="true" Name="recordId" />
                                
                           <ext:ComboBox    runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="currencyId" Name="currencyId" FieldLabel="<%$ Resources:FieldSACurrencyName%>" SimpleSubmit="true">
                                                <Store>
                                                    <ext:Store runat="server" ID="currencyStore">
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
                                                    <RightButtons>
                                                    <ext:Button ID="Button8" runat="server" Icon="Add" Hidden="true"  >
                                                    </ext:Button>
                                                </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                            </ext:ComboBox>
                                 <ext:ComboBox    runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="scrId" Name="scrId" FieldLabel="<%$ Resources:FieldScrName%>" SimpleSubmit="true">
                                                <Store>
                                                    <ext:Store runat="server" ID="scrStore">
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
                                                    <RightButtons>
                                                    <ext:Button ID="Button9" runat="server" Icon="Add" Hidden="true"  >
                                                    </ext:Button>
                                                </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                            </ext:ComboBox>
                      <ext:DateField runat="server" ID="effectiveDate" Name="effectiveDate" FieldLabel="<%$ Resources:FieldEffectiveDate%>" />
                                
                                 <ext:ComboBox ID="salaryType" runat="server" FieldLabel="<%$ Resources:FieldSalaryType%>" Name="salaryType" IDMode="Static" SubmitValue="true">
                                                    <Items>
                                                        <ext:ListItem Text="<%$ Resources: SalaryDaily%>" Value="0"></ext:ListItem>
                                                        <ext:ListItem Text="<%$ Resources: SalaryWeekly%>" Value="1"></ext:ListItem>
                                                        <ext:ListItem Text="<%$ Resources: SalaryMonthly%>" Value="2"></ext:ListItem>
                                                        </Items>
                                                </ext:ComboBox>
                                <ext:ComboBox ID="paymentFrequency" runat="server" FieldLabel="<%$ Resources:FieldPaymentFrequency%>" Name="paymentFrequency" IDMode="Static" SubmitValue="true">
                                                    <Items>
                                                        <ext:ListItem Text="<%$ Resources: SalaryDaily%>" Value="0"></ext:ListItem>
                                                        <ext:ListItem Text="<%$ Resources: SalaryWeekly%>" Value="1"></ext:ListItem>
                                                        <ext:ListItem Text="<%$ Resources: SalaryMonthly%>" Value="2"></ext:ListItem>
                                                        </Items>
                                                </ext:ComboBox>
                                    
                                 <ext:ComboBox ID="paymentMethod" runat="server" FieldLabel="<%$ Resources:FieldPaymentMethod%>" Name="paymentMethod" IDMode="Static" SubmitValue="true">
                                                    <Items>
                                                        <ext:ListItem Text="<%$ Resources: SalaryCash%>" Value="0"></ext:ListItem>
                                                        <ext:ListItem Text="<%$ Resources: SalaryBank%>" Value="1"></ext:ListItem>
                                                        </Items>
                                                </ext:ComboBox>
                             </Items></ext:Panel>
                                <ext:Panel runat="server" ><Items>        <ext:TextField ID="bankName"  runat="server" FieldLabel="<%$ Resources:FieldBankName%>"  Name="bankName" />
                               
                            <ext:TextField ID="accountNumber"  runat="server" FieldLabel="<%$ Resources:FieldAccountNumber%>"  Name="accountNumber" />
                                <ext:TextField ID="comments"  runat="server" FieldLabel="<%$ Resources:FieldComments%>"  Name="comments" />
                                <ext:TextField ID="basicAmount"  runat="server" FieldLabel="<%$ Resources:FieldBasicAmount%>"  Name="basicAmount" />
                                <ext:TextField ID="finalAmount"  runat="server" FieldLabel="<%$ Resources:FieldFinalAmount%>"  Name="finalAmount" />
                                    <ext:Checkbox ID="isTaxable" runat="server" FieldLabel="<%$ Resources: FieldIsTaxable%>" DataIndex="isTaxable" Name="isTaxable" InputValue="1" />
                         </Items></ext:Panel>
                                   </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button12" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditSAForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveSA" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditSAWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{SAId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditSAForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button13" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>


    </form>
</body>
</html>
