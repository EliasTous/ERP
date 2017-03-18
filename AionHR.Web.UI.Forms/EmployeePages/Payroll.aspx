<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payroll.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.Payroll" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Payroll.js?id=22"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="CurrentSalary" runat="server" />
        <ext:Hidden ID="PaymentTypeWeekly" runat="server" Text="<%$ Resources: PaymentTypeWeekly %>" />
        <ext:Hidden ID="PaymentTypeMonthly" runat="server" Text="<%$ Resources: PaymentTypeMonthly %>" />
        <ext:Hidden ID="PaymentTypeDaily" runat="server" Text="<%$ Resources: PaymentTypeDaily %>" />
        <ext:Hidden ID="PaymentMethodCash" runat="server" Text="<%$ Resources: PaymentMethodCash %>" />
        <ext:Hidden ID="PaymentMethodBank" runat="server" Text="<%$ Resources: PaymentMethodBank %>" />
        <ext:Hidden ID="CurrentDetail" runat="server" Text="" />


        <ext:Store runat="server" ID="edStore" OnReadData="edStore_ReadData" >
            <Model>
                <ext:Model runat="server" IDProperty="recordId">
                    <Fields>
                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="name" />
                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>


        <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>



            <Items>


                <ext:GridPanel Visible="True"
                    ID="SalaryGrid" AutoUpdateLayout="true" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"
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
                            <ext:Column Flex="3" ID="ColSAName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldScrName %>" DataIndex="scrName" Hideable="false" Width="75" Align="Center">
                                <Renderer Handler="return '<u>' + record.data['scrName']+'</u>';" />
                            </ext:Column>
                            <ext:DateColumn Format="dd-MM-yyyy" Flex="3" ID="cc" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEffectiveDate %>" DataIndex="effectiveDate" Hideable="false" Width="75" Align="Center">
                            </ext:DateColumn>


                            <ext:Column Visible="false" Flex="2" ID="Column13" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSalaryType %>" DataIndex="salaryType" Hideable="false" Width="75" Align="Center">
                                <Renderer Handler="return getPaymentTypeString(record.data['salaryType'])" />
                            </ext:Column>

                            <ext:Column Visible="false" Flex="2" ID="Column14" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPaymentFrequency %>" DataIndex="paymentFrequency" Hideable="false" Width="75" Align="Center">
                                <Renderer Handler="return getPaymentTypeString(record.data['paymentFrequency'])" />
                            </ext:Column>
                            <ext:Column Flex="3" ID="Column15" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPaymentMethod %>" DataIndex="paymentMethod" Hideable="false" Width="75" Align="Center">
                                <Renderer Handler="return getPaymentMethodString(record.data['paymentMethod'])" />
                            </ext:Column>

                            <ext:Column Flex="3" ID="Column20" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBasicAmount %>" DataIndex="basicAmount" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="3" ID="Column21" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFinalAmount %>" DataIndex="finalAmount" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="3" ID="Column22" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSACurrencyName %>" DataIndex="currencyName" Hideable="false" Width="75" Align="Center" />
                            <ext:CheckColumn Flex="2" ID="Column19" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIsTaxable %>" DataIndex="isTaxable" Hideable="false" Width="75" Align="Center" />


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
                                ID="ColSADelete" Flex="2" Visible="true"
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

                <ext:GridPanel Visible="True"
                    ID="BonusGrid" AutoUpdateLayout="true" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="true"
                    Title="<%$ Resources: BOGridTitle %>"
                    Layout="FitLayout"
                    Flex="1"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="BOStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="BOStore_Refresh"
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
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="currencyId" />
                                        <ext:ModelField Name="currencyName" />
                                        <ext:ModelField Name="btId" />
                                        <ext:ModelField Name="btName" />
                                        <ext:ModelField Name="date" ServerMapping="date.ToShortDateString()" />
                                        <ext:ModelField Name="comment" />
                                        <ext:ModelField Name="amount" />




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
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewBO">
                                            <EventMask ShowMask="true" CustomTarget="={#{BonusGrid}.body}" />
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
                                        <TriggerClick Handler="#{BonusGrid}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Flex="3" ID="ColBOName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBTName %>" DataIndex="btName" Hideable="false" Width="75" Align="Center">
                                <Renderer Handler="return '<u>' + record.data['btName']+'</u>';" />
                            </ext:Column>
                            <ext:DateColumn Flex="3" Format="dd-MM-yyyy" ID="ccc" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate %>" DataIndex="date" Hideable="false" Width="75" Align="Center">
                            </ext:DateColumn>


                            <ext:Column Flex="3" ID="Column9" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSACurrencyName %>" DataIndex="currencyName" Hideable="false" Width="75" Align="Center" >
                              <Renderer Handler="return record.data['currencyName'];" />    
                            </ext:Column>


                            <ext:Column Flex="3" ID="Column7" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldAmount %>" DataIndex="amount" Hideable="false" Width="75" Align="Center" />


                            <ext:Column runat="server"
                                ID="Column10" Visible="false"
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
                                ID="ColBODelete" Flex="2" Visible="true"
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
                                ID="Column12"
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
                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>



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
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:TextField ID="SAId" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Disabled="true" Name="recordId" />

                                        <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="currencyId" Name="currencyId" FieldLabel="<%$ Resources:FieldSACurrencyName%>" SimpleSubmit="true">

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
                                                <ext:Button ID="Button7" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addSACurrency">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="scrId" Name="scrId" FieldLabel="<%$ Resources:FieldScrName%>" SimpleSubmit="true">
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
                                                <ext:Button ID="Button8" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addSCR">
                                                        </Click>
                                                    </DirectEvents>
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
                                            <Listeners>
                                                <Select Handler="TogglePaymentMethod(this.value)" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:TextField ID="bankName" runat="server" FieldLabel="<%$ Resources:FieldBankName%>" Name="bankName" />

                                        <ext:TextField ID="accountNumber" runat="server" FieldLabel="<%$ Resources:FieldAccountNumber%>" Name="accountNumber" />
                                        <ext:TextField ID="comments" runat="server" FieldLabel="<%$ Resources:FieldComments%>" Name="comments" />
                                        <ext:TextField ID="basicAmount" runat="server" FieldLabel="<%$ Resources:FieldBasicAmount%>" Name="basicAmount" />
                                        <ext:TextField ID="finalAmount" runat="server" FieldLabel="<%$ Resources:FieldFinalAmount%>" Name="finalAmount" />
                                        <ext:Checkbox ID="isTaxable" runat="server" FieldLabel="<%$ Resources: FieldIsTaxable%>" DataIndex="isTaxable" Name="isTaxable" InputValue="1" />
                                    </Items>
                                </ext:Panel>
                            </Items>

                        </ext:FormPanel>
                        <ext:FormPanel
                            ID="entitlementsForm"
                            runat="server"
                            Title="<%$ Resources: Entitlements %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:GridPanel
                                    ID="entitlementsGrid"
                                    runat="server"
                                    Width="600" Header="false"
                                    Height="210" Layout="FitLayout"
                                    Frame="true" TitleCollapse="true" Scroll="Vertical">
                                    <Store>
                                        <ext:Store ID="entitlementsStore" runat="server">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="seqNo" />
                                                        <ext:ModelField Name="edId" />
                                                        <ext:ModelField Name="edName" />
                                                        <ext:ModelField Name="includeInTotal" />
                                                        <ext:ModelField Name="comments" />
                                                        <ext:ModelField Name="pct" />
                                                        <ext:ModelField Name="fixedAmount" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Plugins>
                                        <ext:RowEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false">
                                        </ext:RowEditing>
                                    </Plugins>
                                    <TopBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:Button runat="server" Text="Add Detail" Icon="MoneyAdd">
                                                    <Listeners>
                                                        <Click Fn="addEntitlement" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button
                                                    ID="btnRemoveEmployee"
                                                    runat="server"
                                                    Text="Remove Detail"
                                                    Icon="MoneyDelete"
                                                    Disabled="true">
                                                    <Listeners>
                                                        <Click Fn="removeEntitlement" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:RowNumbererColumn runat="server" Width="25" />
                                            <ext:Column
                                                runat="server"
                                                DataIndex="seqNo"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField runat="server" DataIndex="seqNo" Enabled="false" ReadOnly="true" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column
                                                runat="server"
                                                Text="<%$ Resources:FieldEntitlementDeduction%>"
                                                DataIndex="edId"
                                                Align="Center">
                                                <Renderer Fn="nameRenderer" />
                                                <Editor>

                                                    <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" DisplayField="name" ID="edId" Name="edId" StoreID="edStore">
                                                    </ext:ComboBox>
                                                </Editor>
                                            </ext:Column>
                                            <ext:CheckColumn
                                                runat="server"
                                                Text="<%$ Resources:FieldIncludeInTotal%>"
                                                DataIndex="includeInTotal"
                                                Align="Center">
                                                <Editor>
                                                    <ext:Checkbox ID="includeInTotal" runat="server" DataIndex="includeInTotal" Name="includeInTotal" InputValue="true" />
                                                </Editor>

                                            </ext:CheckColumn>


                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:FieldPCT%>"
                                                DataIndex="pct"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField
                                                        runat="server"
                                                        AllowBlank="false"
                                                        MinValue="0"
                                                        MaxValue="100" />
                                                </Editor>
                                            </ext:NumberColumn>

                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:FieldFixedAmount%>"
                                                DataIndex="fixedAmount"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField
                                                        runat="server"
                                                        AllowBlank="false" />
                                                </Editor>
                                            </ext:NumberColumn>
                                            <ext:Column
                                                runat="server"
                                                Text="<%$ Resources:FieldComment%>"
                                                DataIndex="comments"
                                                Align="Center">
                                                <Editor>
                                                    <ext:TextField
                                                        runat="server"
                                                        AllowBlank="false" />
                                                </Editor>
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <Listeners>
                                        <SelectionChange Handler="App.btnRemoveEmployee.setDisabled(!selected.length);" />
                                    </Listeners>
                                </ext:GridPanel>
                            </Items>

                        </ext:FormPanel>
                        <ext:FormPanel
                            ID="DeductionForm"
                            runat="server"
                            Title="<%$ Resources: Deductions %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:GridPanel
                                    ID="deductionGrid"
                                    runat="server"
                                    Width="600" Header="false"
                                    Height="210" Layout="FitLayout"
                                    Frame="true" TitleCollapse="true" Scroll="Vertical">
                                    <Store>
                                        <ext:Store ID="deductionStore" runat="server">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="seqNo" />
                                                        <ext:ModelField Name="edId" />
                                                        <ext:ModelField Name="edName" />
                                                        <ext:ModelField Name="includeInTotal" />
                                                        <ext:ModelField Name="comments" />
                                                        <ext:ModelField Name="pct" />
                                                        <ext:ModelField Name="fixedAmount" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Plugins>
                                        <ext:RowEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false">
                                        </ext:RowEditing>
                                    </Plugins>
                                    <TopBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:Button runat="server" Text="Add Detail" Icon="MoneyAdd">
                                                    <Listeners>
                                                        <Click Fn="addDeduction" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button
                                                    ID="btnRemoveDeduction"
                                                    runat="server"
                                                    Text="Remove Detail"
                                                    Icon="MoneyDelete"
                                                    Disabled="true">
                                                    <Listeners>
                                                        <Click Fn="removeDeduction" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:RowNumbererColumn runat="server" Width="25" />
                                            <ext:Column
                                                runat="server"
                                                DataIndex="seqNo"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField runat="server" DataIndex="seqNo" Enabled="false" ReadOnly="true" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column
                                                runat="server"
                                                Text="<%$ Resources:FieldEntitlementDeduction%>"
                                                DataIndex="edId"
                                                Align="Center">
                                                <Renderer fn="nameRenderer" />
                                                <Editor>
                                                   
                                                    <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId"  DisplayField="name" ID="deductionedId" Name="edId"  StoreID="edStore">
            
                                                    </ext:ComboBox>
                                                </Editor>
                                            </ext:Column>
                                            <ext:CheckColumn
                                                runat="server"
                                                Text="<%$ Resources:FieldIncludeInTotal%>"
                                                DataIndex="includeInTotal"
                                                Align="Center">
                                                <Editor>
                                                    <ext:Checkbox ID="Checkbox1" runat="server" DataIndex="includeInTotal" Name="includeInTotal" InputValue="true" />
                                                </Editor>

                                            </ext:CheckColumn>


                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:FieldPCT%>"
                                                DataIndex="pct"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField
                                                        runat="server"
                                                        AllowBlank="false"
                                                        MinValue="0"
                                                        MaxValue="100" />
                                                </Editor>
                                            </ext:NumberColumn>

                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:FieldFixedAmount%>"
                                                DataIndex="fixedAmount"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField
                                                        runat="server"
                                                        AllowBlank="false" />
                                                </Editor>
                                            </ext:NumberColumn>
                                            <ext:Column
                                                runat="server"
                                                Text="<%$ Resources:FieldComment%>"
                                                DataIndex="comments"
                                                Align="Center">
                                                <Editor>
                                                    <ext:TextField
                                                        runat="server" Name="comments" DataIndex="comments"
                                                        AllowBlank="false" />
                                                </Editor>
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <Listeners>
                                        <SelectionChange Handler="App.btnRemoveDeduction.setDisabled(!selected.length);" />
                                    </Listeners>
                                </ext:GridPanel>
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
                                <ext:Parameter Name="entitlements" Value="Ext.encode(#{entitlementsGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw" />
                                <ext:Parameter Name="deductions" Value="Ext.encode(#{deductionGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw" />
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


        <ext:Window
            ID="EditBOWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditBOWindowTitle %>"
            Width="650"
            Height="300"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="BOForm" DefaultButton="SaveSAButton"
                            runat="server"
                            Title="<%$ Resources: EditBOWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" Layout="AutoLayout"
                            BodyPadding="5">
                            <Items>

                                <ext:TextField ID="BOId" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Disabled="true" Name="recordId" />

                                <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="CurrencyCombo" DataIndex="currencyId" FieldLabel="<%$ Resources:FieldSACurrencyName%>" SimpleSubmit="true">

                                    <Store>
                                        <ext:Store runat="server" ID="BOCurrencyStore">
                                            <Model>
                                                <ext:Model runat="server" IDProperty="recordId">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <RightButtons>
                                        <ext:Button ID="Button2" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addBOCurrency">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="btId" Name="btId" FieldLabel="<%$ Resources:FieldBTName%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="BTStore">
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
                                        <ext:Button ID="Button3" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addBT">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:DateField runat="server" ID="date" Name="date" FieldLabel="<%$ Resources:FieldDate%>" />
                                <ext:TextField ID="TextField7" runat="server" FieldLabel="<%$ Resources:FieldAmount%>" Name="amount" />
                                <ext:TextField ID="TextField9" runat="server" FieldLabel="<%$ Resources:FieldComment%>" Name="comment" />


                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button4" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BOForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveBO" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditBOWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{BOId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{BOForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button5" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

    </form>
</body>
</html>

