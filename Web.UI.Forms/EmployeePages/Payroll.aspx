﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payroll.aspx.cs" Inherits="Web.UI.Forms.EmployeePages.Payroll" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=2" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css?id=123" />
    <script type="text/javascript" src="../Scripts/Payroll.js?id=160"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=12"></script>
    <script type="text/javascript">
         function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
        String.prototype.replaceAll = function(search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
        };
      
       function thousandSeparator(num) {
           var nf = new Intl.NumberFormat();
         
          
          num= num.toString().replaceAll(",", "");
         
          
          
          
            return  nf.format(num);
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
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="CurrentSalary" runat="server" />
        <ext:Hidden ID="CurrentSalaryCurrency" runat="server" />
        <ext:Hidden ID="PaymentTypeWeekly" runat="server" Text="<%$ Resources: PaymentTypeWeekly %>" />
        <ext:Hidden ID="PaymentTypeMonthly" runat="server" Text="<%$ Resources: PaymentTypeMonthly %>" />
        <ext:Hidden ID="PaymentTypeDaily" runat="server" Text="<%$ Resources: PaymentTypeDaily %>" />
        <ext:Hidden ID="PaymentMethodCash" runat="server" Text="<%$ Resources: PaymentMethodCash %>" />
        <ext:Hidden ID="PaymentMethodBank" runat="server" Text="<%$ Resources: PaymentMethodBank %>" />
        <ext:Hidden ID="CurrentDetail" runat="server" Text="" />
        <ext:Hidden ID="BasicSalary" runat="server" Text="" />
        <ext:Hidden ID="ENSeq" runat="server" Text="" />
        <ext:Hidden ID="DESeq" runat="server" Text="" />
        <ext:Hidden ID="EmployeeTerminated" runat="server" />
 <%--       <ext:Hidden ID="eAmount" runat="server" Text="" />
        <ext:Hidden ID="dAmount" runat="server" Text="" />--%>
        <ext:Hidden ID="currentGrossSalary" runat="server"  />

        <ext:Store runat="server" ID="entsStore" OnReadData="ensStore_ReadData" AutoLoad="true">
            <Model>
                <ext:Model runat="server" IDProperty="recordId">
                    <Fields>
                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="name" />
                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>

       
                                              <ext:Store runat="server" ID="edCalcTypeStore" >
                                                            <Model>
                                                                <ext:Model runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="key" />
                                                                        <ext:ModelField Name="value" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                           
        <ext:Store runat="server" ID="dedsStore" OnReadData="dedsStore_ReadData" AutoLoad="true">
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
                            RemoteSort="False"
                            RemoteFilter="true"
                            OnReadData="SAStore_Refresh"
                            PageSize="50" IDMode="Explicit" Namespace="App" >
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
                                        <ext:ModelField Name="currencyRef" />
                                        <ext:ModelField Name="scrId" />
                                        <ext:ModelField Name="scrName" />
                                        <ext:ModelField Name="effectiveDate"  />
                                        <ext:ModelField Name="salaryType" />
                                        <ext:ModelField Name="paymentFrequency" />
                                        <ext:ModelField Name="paymentMethod" />
                                        <ext:ModelField Name="isTaxable" />
                                        <ext:ModelField Name="bankName" />
                                        <ext:ModelField Name="accountNumber" />
                                        <ext:ModelField Name="comments" />
                                        <ext:ModelField Name="basicAmount" />
                                        <ext:ModelField Name="finalAmount" />
                                        <ext:ModelField Name="grossSalary" />



                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="effectiveDate" Direction="ASC" />
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

                            <ext:Column Visible="false" ID="recID" MenuDisabled="true" runat="server"  DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                           
                            <ext:DateColumn   ID="cc" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEffectiveDate %>" DataIndex="effectiveDate" Hideable="false" Width="100" Align="Center">
                            </ext:DateColumn>


                            <ext:Column Visible="false"  ID="Column13" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSalaryType %>" DataIndex="salaryType" Hideable="false"  Flex="1" Align="Center">
                                <Renderer Handler="return getPaymentTypeString(record.data['salaryType'])" />
                            </ext:Column>

                            <ext:Column Visible="false"  ID="Column14" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPaymentFrequency %>" DataIndex="paymentFrequency" Flex="1" Hideable="false" Width="75" Align="Center">
                                <Renderer Handler="return getPaymentTypeString(record.data['paymentFrequency'])" />
                            </ext:Column>
                    
                            <ext:Column  ID="Column20" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBasicAmount %>" DataIndex="basicAmount" Hideable="false" Flex="1" Width="75" Align="Center" >
                                <Renderer Handler="return record.data['currencyRef']+ '&nbsp;'+numberWithCommas(record.data['basicAmount'].toFixed(2))  ;">

                                </Renderer>
                                </ext:Column>
                            <ext:Column  ID="Column21" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFinalAmount %>" DataIndex="finalAmount" Hideable="false" Flex="1" Width="75" Align="Center" >
                                   <Renderer Handler="return record.data['currencyRef']+ '&nbsp;'+numberWithCommas(record.data['finalAmount'].toFixed(2))  ;">

                                </Renderer>
                                </ext:Column>
                       

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
                                <ext:Column runat="server"
                                ID="ColSADelete"  Visible="true"
                                Text=""
                                Width="80"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer handler="var d =(App.EmployeeTerminated.value=='0')?deleteRender():' '; return editRender()+'&nbsp;&nbsp;' +d; " />

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
                        <CellClick OnEvent="PoPuPSA">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="sal" Value="record.data['basicAmount']" Mode="Raw" />
                                <ext:Parameter Name="grossSalary" Value="record.data['grossSalary']" Mode="Raw" />
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
                            RemoteSort="False"
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
                                        <ext:ModelField Name="currencyRef" />
                                        <ext:ModelField Name="btId" />
                                        <ext:ModelField Name="btName" />
                                        <ext:ModelField Name="date"  />
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

                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server"  DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column  ID="ColBOName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBTName %>" DataIndex="btName" Hideable="false" Flex="1" Align="Center"/>
                            
                            <ext:DateColumn   ID="ccc" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate %>" DataIndex="date" Hideable="false" Width="100" Align="Center">
                            </ext:DateColumn>


                        


                            <ext:Column ID="Column7" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldAmount %>" DataIndex="amount" Hideable="false" Flex="1" Align="Center" >
                                <Renderer Handler="return  record.data['currencyRef']+ '&nbsp;'+record.data['amount'];"/>
                                    </ext:Column>


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
                            <ext:Column runat="server"
                                ID="ColBODelete"  Visible="true"
                                Text=""
                                Width="80"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer handler="var d =(App.EmployeeTerminated.value=='0')?deleteRender():' '; return editRender()+'&nbsp;&nbsp;' +d;" />

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
                        <CellClick OnEvent="PoPuPBO">
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
            Height="340"
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
                                <ext:Panel runat="server" ID="firstPanel">
                                    <Items>
                                        <ext:TextField ID="SAId" Hidden="true" runat="server" LabelWidth="130" Width="275"  Disabled="true" Name="recordId" />

                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" LabelWidth="130" Width="275" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="reference" ID="currencyId" Name="currencyId" FieldLabel="<%$ Resources:FieldSACurrencyName%>" SimpleSubmit="true">

                                            <Store>
                                                <ext:Store runat="server" ID="currencyStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="recordId" />
                                                                <ext:ModelField Name="reference" />
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
                                                <Change Handler="CurrentSalaryCurrency.value = App.currencyId.getRawValue();" />
                                                <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="true" LabelWidth="130" Width="275" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="scrId" Name="scrId" FieldLabel="<%$ Resources:FieldScrName%>" SimpleSubmit="true">
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
                                        <ext:DateField AllowBlank="false" runat="server" LabelWidth="130" Width="275" ID="effectiveDate" Name="effectiveDate" FieldLabel="<%$ Resources:FieldEffectiveDate%>" />

                                     
                                         <ext:ComboBox  AllowBlank="false"  AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"   ForceSelection="true" Width="275" LabelWidth="130" TypeAhead="true" MinChars="1" ValueField="key" DisplayField="value" ID="salaryType"  FieldLabel="<%$ Resources:FieldSalaryType%>" SubmitValue="true"  Name="salaryType" >
                                              <Store>
                                              <ext:Store runat="server" ID="salaryTypeStore" >
                                                            <Model>
                                                                <ext:Model runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="key" />
                                                                        <ext:ModelField Name="value" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                            </Store>
        
                                              </ext:ComboBox>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  AllowBlank="false" ID="paymentFrequency" LabelWidth="130" Width="275" runat="server" FieldLabel="<%$ Resources:FieldPaymentFrequency%>" Name="paymentFrequency" IDMode="Static" SubmitValue="true">
                                            <Items>
                                                   <ext:ListItem Text="<%$ Resources: SalaryDaily%>" Value="<%$ Resources:ComboBoxValues , PYEMPaymentFrequencyDaily %>"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources: SalaryWeekly%>" Value="<%$ Resources:ComboBoxValues , PYEMPaymentFrequencyWeekly %>"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources: SalaryBiWeekly%>" Value="<%$ Resources:ComboBoxValues , PYEMPaymentFrequencyBiWeekly %>"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources: SalaryFourWeekly%>" Value="<%$ Resources:ComboBoxValues , PYEMPaymentFrequencyFourWeekly %>"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources: SalaryMonthly%>" Value="<%$ Resources:ComboBoxValues , PYEMPaymentFrequencyMonthly %>"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>

                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  LabelWidth="130" Width="275" AllowBlank="false" ID="paymentMethod" runat="server" FieldLabel="<%$ Resources:FieldPaymentMethod%>" Name="paymentMethod" IDMode="Static" SubmitValue="true">
                                            <Items>
                                                <ext:ListItem Text="<%$ Resources: SalaryCash%>" Value="<%$ Resources:ComboBoxValues , PYEMPaymentMethodCash %>"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources: SalaryBank%>" Value="<%$ Resources:ComboBoxValues , PYEMPaymentMethodBank %>"></ext:ListItem>
                                            </Items>
                                            <Listeners>
                                                <Select Handler="TogglePaymentMethod(this.value)" />
                                            </Listeners>
                                        </ext:ComboBox>


                                      <%--  <ext:TextField LabelWidth="130" Disabled="true" Width="275" ID="bankName" runat="server" FieldLabel="<%$ Resources:FieldBankName%>" Name="bankName" AllowBlank="false" />--%>
                                          <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" LabelWidth="130" Width="275" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="bankId" Name="bankId" FieldLabel="<%$ Resources:FieldBankName%>" Disabled="true" >
                                            <Store>
                                                <ext:Store runat="server" ID="bankStore">
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
                                    </Items>
                                </ext:Panel>
                                
                                <ext:Panel runat="server" PaddingSpec="0 0 0 5" ID="secondPanel">
                                    <Items>
                                        
                                        <%-- <ext:TextField LabelWidth="130" Width="350" Disabled="true"  ID="swiftCode" runat="server" FieldLabel="<%$ Resources:FieldswiftCode%>" Name="swiftCode" AllowBlank="false" />--%>
                                        <ext:TextField LabelWidth="130" Width="350" Disabled="true"  ID="accountNumber" runat="server" FieldLabel="<%$ Resources:FieldAccountNumber%>" Name="accountNumber" AllowBlank="false" />
                                        <ext:TextField LabelWidth="130" Width="350" ID="comments" runat="server" FieldLabel="<%$ Resources:FieldComments%>" Name="comments" />
                                        <ext:TextField LabelWidth="130" Width="350" ID="basicAmount" AllowBlank="false" runat="server" FieldLabel="<%$ Resources:FieldBasicAmount%>" Name="basicAmount" EnableKeyEvents="true">
                                            <Listeners>
                                                 <Change Handler="App.BasicSalary.setValue(this.getValue()); this.next().setValue(this.value); ChangeEntitlementsAmount(0); ChangeDeductionsAmount();" />
                                           
                                               
                                            </Listeners>
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="130" Width="350" ID="finalAmount"  ReadOnly="true" AllowBlank="true" runat="server" FieldLabel="<%$ Resources:FieldFinalAmount%>" Name="finalAmount" >
                                            
                                            </ext:TextField>
                                        <ext:TextField LabelWidth="130" Width="350" ID="eAmount"  ReadOnly="true" AllowBlank="true" runat="server" FieldLabel="<%$ Resources:TotalEntitlements%>" Name="eAmount" >
                                            
                                            </ext:TextField>
                                        <ext:TextField LabelWidth="130" Width="350" ID="dAmount"  ReadOnly="true" AllowBlank="true" runat="server" FieldLabel="<%$ Resources:TotalDeductions%>" Name="dAmount" >
                                         
                                            </ext:TextField>
                                        
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
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                                            <Items>
                                                <ext:Button ID="Button11" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="ADDNewEN">
                                                            <EventMask ShowMask="true" CustomTarget="={#{entitlementsGrid}.body}" />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>


                                            </Items>
                                        </ext:Toolbar>

                                    </TopBar>
                                    <Store>
                                        <ext:Store ID="entitlementsStore" runat="server">
                                            <Model>
                                                <ext:Model runat="server" IDProperty="seqNo">
                                                    <Fields>
                                                        <ext:ModelField Name="seqNo" />
                                                        <ext:ModelField Name="edId" />
                                                        <ext:ModelField Name="edName" />
                                                        <ext:ModelField Name="includeInTotal" />
                                                        <ext:ModelField Name="comments" />
                                                        <ext:ModelField Name="pct" />
                                                        <ext:ModelField Name="fixedAmount" />
                                                        <ext:ModelField Name="isTaxable" />
                                                        <ext:ModelField Name="edCalcType" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>



                                    <ColumnModel>
                                        <Columns>

                                            <%--<ext:Column
                                                runat="server"
                                                DataIndex="seqNo"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField runat="server" DataIndex="seqNo" Enabled="false" ReadOnly="true" />
                                                </Editor>
                                            </ext:Column>--%>
                                            <ext:Column ID="ColENName"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:FieldEntitlement%>"
                                                DataIndex="edId"
                                                Align="Center">
                                                <Renderer Fn="entnameRenderer" />
                                                <Editor>

                                                    <%-- <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="false" ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="entEdId" Name="edId" StoreID="entsStore">
                                                        <RightButtons>
                                                            <ext:Button ID="Button10" runat="server" Icon="Add" Hidden="true">
                                                                <Listeners>
                                                                    <Click Handler="CheckSession();  " />
                                                                </Listeners>
                                                                <DirectEvents>

                                                                    <Click OnEvent="addEnt">
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:Button>
                                                        </RightButtons>
                                                        <Listeners>
                                                            <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                            <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                        </Listeners>
                                                    </ext:ComboBox>--%>
                                                </Editor>
                                            </ext:Column>
                                            <ext:CheckColumn
                                                runat="server" Flex="1" Visible="false"
                                                Text="<%$ Resources:FieldIncludeInTotal%>"
                                                DataIndex="includeInTotal"
                                                Align="Center">
                                                <Editor>
                                                    <%-- <ext:Checkbox ID="includeInTotal" runat="server" DataIndex="includeInTotal" Name="includeInTotal" InputValue="true" />--%>
                                                </Editor>

                                            </ext:CheckColumn>
                                                     <ext:CheckColumn
                                                runat="server"
                                               Visible="false"
                                                DataIndex="isTaxable"
                                                Align="Center">
                                                <Editor>
                                                    <%--<ext:Checkbox ID="Checkbox1" runat="server" DataIndex="includeInTotal" Name="includeInTotal" InputValue="true" />--%>
                                                </Editor>

                                            </ext:CheckColumn>

                                            <ext:NumberColumn
                                                runat="server" Flex="1"
                                                Text="<%$ Resources:FieldPCT%>"
                                                DataIndex="pct"   Format="0.00%"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField
                                                        runat="server"
                                                        AllowBlank="false"
                                                        MinValue="0"
                                                        MaxValue="100" />
                                                </Editor>
                                                 <%--<Renderer Handler="return record.data['pct'] +'%';"/>--%>
                                            </ext:NumberColumn>

                                            <ext:NumberColumn
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:FieldFixedAmount%>"
                                                DataIndex="fixedAmount"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField
                                                        runat="server"
                                                        AllowBlank="false" />
                                                </Editor>
                                                <Renderer Handler=" return App.currencyId.getRawValue()+ '&nbsp;' +numberWithCommas(record.data['fixedAmount'].toFixed(2));"/>
                                            </ext:NumberColumn>
                                            <ext:Column
                                                runat="server" 
                                                Text="<%$ Resources:FieldComment%>"
                                                DataIndex="comments" Visible="false"
                                                Align="Center">
                                                <Editor>
                                                    <ext:TextField
                                                        runat="server"
                                                        AllowBlank="true" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column runat="server"
                                                ID="ColENDelete"  Visible="true"
                                                Text=""
                                                Width="80"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                Hideable="false"
                                                MenuDisabled="true"
                                                Resizable="false">
                                                <Renderer handler="var d =(App.EmployeeTerminated.value=='0')?deleteRender():' '; return editRender()+'&nbsp;&nbsp;' +d;" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <Listeners>
                                        <Render Handler="this.on('cellclick', cellClick);" />
                                    </Listeners>
                                    <DirectEvents>

                                        <CellClick OnEvent="PoPuPEN">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>

                                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                                <ext:Parameter Name="values" Value="Ext.encode(#{entitlementsGrid}.getRowsValues({selectedOnly : true}))" Mode="Raw" />
                                            </ExtraParams>

                                        </CellClick>
                                    </DirectEvents>


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
                                                <ext:Model runat="server" IDProperty="seqNo">
                                                    <Fields>
                                                        <ext:ModelField Name="seqNo" />
                                                        <ext:ModelField Name="edId" />
                                                        <ext:ModelField Name="edName" />
                                                        <ext:ModelField Name="includeInTotal" />
                                                        <ext:ModelField Name="comments" />
                                                        <ext:ModelField Name="isTaxable" />
                                                        <ext:ModelField Name="pct" />
                                                        <ext:ModelField Name="pctOf" />
                                                        <ext:ModelField Name="fixedAmount" />
                                                          <ext:ModelField Name="edCalcType" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Plugins>
                                        <%--<ext:RowEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" SaveBtnText="<%$ Resources:Common , Save %>" CancelBtnText="<%$ Resources:Common , Cancel %>">
                                        </ext:RowEditing>--%>
                                    </Plugins>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar4" runat="server" ClassicButtonStyle="false">
                                            <Items>
                                                <ext:Button ID="Button14" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="ADDNewDE">
                                                            <EventMask ShowMask="true" CustomTarget="={#{deductionGrid}.body}" />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>


                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <ColumnModel>
                                        <Columns>

                                            <%--  <ext:Column
                                                runat="server"
                                                DataIndex="seqNo"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField runat="server" DataIndex="seqNo" Enabled="false" ReadOnly="true" />
                                                </Editor>
                                            </ext:Column>--%>
                                            <ext:Column ID="ColDEName"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:FieldDeduction%>"
                                                DataIndex="edId"
                                                Align="Center">
                                                <Renderer Fn="dednameRenderer" />
                                                <Editor>

                                                    <%--  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="false" ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="dedEdId" Name="deductionId" StoreID="dedsStore">
                                                        <RightButtons>
                                                            <ext:Button ID="Button9" runat="server" Icon="Add" Hidden="true">
                                                                <Listeners>
                                                                    <Click Handler="CheckSession();  " />
                                                                </Listeners>
                                                                <DirectEvents>

                                                                    <Click OnEvent="addDed">
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:Button>
                                                        </RightButtons>
                                                        <Listeners>
                                                            <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                            <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                        </Listeners>
                                                    </ext:ComboBox>--%>
                                                </Editor>
                                            </ext:Column>
                                            <ext:CheckColumn
                                                runat="server" Flex="1"
                                                Text="<%$ Resources:FieldIncludeInTotal%>"  Visible="false"
                                                DataIndex="includeInTotal"
                                                Align="Center">
                                                <Editor>
                                                    <%--<ext:Checkbox ID="Checkbox1" runat="server" DataIndex="includeInTotal" Name="includeInTotal" InputValue="true" />--%>
                                                </Editor>

                                            </ext:CheckColumn>
                                             <ext:CheckColumn
                                                runat="server"
                                               Visible="false"
                                                DataIndex="isTaxable"
                                                Align="Center">
                                                <Editor>
                                                    <%--<ext:Checkbox ID="Checkbox1" runat="server" DataIndex="includeInTotal" Name="includeInTotal" InputValue="true" />--%>
                                                </Editor>

                                            </ext:CheckColumn>

                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:FieldPCT%>"
                                                DataIndex="pct" Flex="1" Format="0.00%"
                                                Align="Center"> 
                                                <Editor>
                                                    <ext:NumberField
                                                        runat="server"
                                                        AllowBlank="false"
                                                        MinValue="0"
                                                        MaxValue="100" />
                                                </Editor>
                                                <%--<Renderer Handler="return record.data['pct'] +'%';"/>--%>
                                            </ext:NumberColumn>

                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:FieldFixedAmount%>"
                                                DataIndex="fixedAmount" Flex="2"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField
                                                        runat="server"
                                                        AllowBlank="false" />
                                                </Editor>
                                                <Renderer Handler="return App.currencyId.getRawValue()+ '&nbsp;'+numberWithCommas(record.data['fixedAmount'].toFixed(2));">

                                </Renderer>
                               
                                            </ext:NumberColumn>

                                            <ext:Column
                                                runat="server"
                                                Text="<%$ Resources:FieldComment%>"
                                                DataIndex="comments" Visible="false"
                                                Align="Center">
                                                <Editor>
                                                    <ext:TextField
                                                        runat="server" Name="comments" DataIndex="comments"
                                                        AllowBlank="true" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column runat="server"
                                                ID="ColDEDelete"  Visible="true"
                                                Text=""
                                                Width="60"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                Hideable="false"
                                                MenuDisabled="true"
                                                Resizable="false">
                                               <Renderer handler="var d =(App.EmployeeTerminated.value=='0')?deleteRender():' '; return editRender()+'&nbsp;&nbsp;' +d;" />
                                            </ext:Column>

                                        </Columns>
                                    </ColumnModel>
                                       <Listeners>
                                        <Render Handler="this.on('cellclick', cellClick);" />
                                    </Listeners>
                                    <DirectEvents>

                                        <CellClick OnEvent="PoPuPDE">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>

                                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                                <ext:Parameter Name="values" Value="Ext.encode(#{deductionGrid}.getRowsValues({selectedOnly : true}))" Mode="Raw" />
                                            </ExtraParams>

                                        </CellClick>
                                    </DirectEvents>
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
                                <ext:Parameter Name="basicAmount" Value="#{basicAmount}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="finalAmount" Value="#{finalAmount}.getValue()" Mode="Raw" />
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
            Width="400"
            Height="300"
            AutoShow="false"
            Modal="true"
            Closable="false"
            Resizable="false"
            Floatable="false"
            Draggable="false"
            FocusOnToFront="true"
            Header="false"
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

                                <ext:TextField ID="BOId" Hidden="true" runat="server"  Disabled="true" Name="recordId" />

                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="reference" ID="CurrencyCombo" DataIndex="currencyId" FieldLabel="<%$ Resources:FieldSACurrencyName%>" SimpleSubmit="true">

                                    <Store>
                                        <ext:Store runat="server" ID="BOCurrencyStore">
                                            <Model>
                                                <ext:Model runat="server" IDProperty="recordId">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="reference" />
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
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="btId" Name="btId" FieldLabel="<%$ Resources:FieldBTName%>" SimpleSubmit="true">
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
                                <ext:DateField runat="server" ID="date" Name="date" FieldLabel="<%$ Resources:FieldDate%>" AllowBlank="false" />
                                <ext:TextField ID="TextField7" runat="server" FieldLabel="<%$ Resources:FieldAmount%>" Name="amount" AllowBlank="false" />
                                <ext:TextArea ID="comment" runat="server" FieldLabel="<%$ Resources:FieldComment%>" Name="comment" />


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

        <ext:Window
            ID="EditENWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:FieldEntitlement %>"
            Width="650"
            Height="450"
            AutoShow="false"
            Modal="true"
            Closable="false"
            Resizable="false"
            Floatable="false"
            Draggable="false"
            FocusOnToFront="true"
            Header="false"
            Hidden="true"
            Layout="FitLayout" >

            <Items>
                <ext:TabPanel ID="TabPanel3" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" Header="false">
                    <Items>
                        <ext:FormPanel
                            ID="ENForm" DefaultButton="SaveENButton"
                            runat="server"
                            Header="false"
                            Title="<%$ Resources: FieldEntitlement %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" Layout="AutoLayout"
                            BodyPadding="5">
                            <Items>

                                <ext:TextField ID="ENId" Hidden="true" runat="server"  Disabled="true" Name="recordId" />
                                <ext:TextField ID="oldEntValue" Hidden="true" runat="server"  Disabled="true" Name="recordId" />
                                <ext:Checkbox ID="oldENIncludeInFinal" Hidden="true" runat="server" disabled="true" />
                              
                              <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" DisplayField="name" ID="entEdId" Name="edId" FieldLabel="<%$ Resources:FieldEntitlement%>" SimpleSubmit="true" StoreID="entsStore">
                                            
                                        
                                            
                                        </ext:ComboBox>
                              <ext:Checkbox ID="EnIncludeInTotal" FieldLabel="<%$ Resources: FieldIncludeInTotal %>" runat="server" DataIndex="includeInTotal" Name="includeInTotal" InputValue="true" />
                                <ext:Checkbox ID="enIsPct" FieldLabel="<%$ Resources:FieldIsPercentage%>" runat="server">
                                    <Listeners>
                                        <Change Handler="TogglePerc(this.value)" />
                                    </Listeners>
                                </ext:Checkbox>

                                <ext:TextField
                                    runat="server"
                                    AllowBlank="false" ID="enPCT" Name="pct"
                                     Disabled="true"
                                    FieldLabel="<%$ Resources:FieldPCT%>"
                                    >
                                    <Listeners>
                                        <Change Handler=" if(this.prev().value==true) this.next().setValue(CalculateFixed(this.value,1));" />
                                    </Listeners>
                                    <Validator Handler="return !isNaN(this.value) && this.value>0 && this.value<100;" />
                                </ext:TextField>

                                <ext:NumberField
                                    runat="server" Name="fixedAmount"
                                    AllowBlank="false"
                                    MinValue="0"
                                    ID="enFixedAmount"
                                    FieldLabel="<%$ Resources:FieldFixedAmount%>" AllowDecimals="true" DecimalPrecision="2">
                                   <Listeners>
                                                 
                                           
                                               
                                            </Listeners>
                                    <Validator Handler="return this.value >0 ;" />
                                </ext:NumberField>
                                <ext:TextArea runat="server" ID="enComment" Name="comment" DataIndex="comments" FieldLabel="<%$ Resources:FieldComment%>" />
                                <ext:TextField runat="server" InputType="Password" Visible="false" ID="enCommentField" Name="comment" DataIndex="comments" FieldLabel="<%$ Resources:FieldComment%>" />
                                <ext:Checkbox ID="isTaxable" FieldLabel="<%$ Resources: FieldIsTaxable %>" runat="server" DataIndex="isTaxable" Name="isTaxable" InputValue="true" />
                                   <ext:ComboBox  AllowBlank="false"  AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"   ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="key" DisplayField="value" ID="entitlementCalcType"  FieldLabel="<%$ Resources:FieldEdCalcType%>" SubmitValue="true"  Name="edCalcType" StoreID="edCalcTypeStore" >
                                             
        
                                              </ext:ComboBox>
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveENButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{ENForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveEN" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditENWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{ENId}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="FixedAmount" Value="#{enFixedAmount}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="oldAmount" Value="#{oldEntValue}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="oldInclude" Value="#{oldENIncludeInFinal}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{ENForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button17" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

        <ext:Window
            ID="EditDEWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:FieldDeduction %>"
             Width="650"
            Height="450"
            AutoShow="false"
            Modal="true"
            Closable="false"
            Resizable="false"
            Floatable="false"
            Draggable="false"
            FocusOnToFront="true"
            Header="false"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel4" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" Header="false"  TitleCollapse="true">
                    <Items>
                        <ext:FormPanel
                            ID="DEForm" DefaultButton="SaveDEButton"
                            runat="server"
                            
                            Icon="ApplicationSideList"
                            Header="false" TitleCollapse="true"
                            DefaultAnchor="100%" Layout="AutoLayout"
                            BodyPadding="5">
                            <Items>
                                 <ext:TextField ID="DEoldValue" Hidden="true" runat="server"  Disabled="true" Name="recordId" />
                                <ext:TextField ID="DEId" Hidden="true" runat="server"  Disabled="true" Name="recordId" />
                                <ext:Checkbox ID="oldDEIncludeInFinal" Hidden="true" runat="server" disabled="true" />
                                
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"   runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" DisplayField="name" ID="dedEdId" Name="DEedId" FieldLabel="<%$ Resources:FieldDeduction%>" SimpleSubmit="true" StoreID="dedsStore">
                                    
                                   

                                </ext:ComboBox>
                                <ext:Checkbox ID="Checkbox1" FieldLabel="<%$ Resources: FieldIncludeInTotal %>" runat="server" DataIndex="includeInTotal" Name="includeInTotal" InputValue="true" />
                                
                                <ext:Checkbox ID="deIsPCT" FieldLabel="<%$ Resources:FieldIsPercentage%>" runat="server">
                                    <Listeners>
                                        <Change Handler="DETogglePerc(this.value)" />
                                    </Listeners>
                                </ext:Checkbox>
                                 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Disabled="true" runat="server" ID="pctOf" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="true" Name="pctOf"
                                    FieldLabel="<%$ Resources: FieldPctOf %>">
                                    <Items>

                                        
                                        <ext:ListItem Text="<%$ Resources: pctof1 %>" Value="<%$ Resources:ComboBoxValues, PYEMpctOf1 %>" />
                                        <ext:ListItem Text="<%$ Resources: pctof2 %>" Value="<%$ Resources:ComboBoxValues, PYEMpctOf2 %>" />
                                    </Items>
                                      <Listeners>
                                        <Select Handler=" if(this.prev().value==true) this.next().next().setValue(CalculateFixed(this.next().value,this.value));" />
                                    </Listeners>
                                </ext:ComboBox>
                               <ext:TextField
                                    runat="server"
                                    AllowBlank="false" ID="dePCT" Name="pct"
                                     Disabled="true"
                                    FieldLabel="<%$ Resources:FieldPCT%>"
                                    >
                                    <Listeners>
                                        <Change Handler=" if(this.prev().prev().value==true) this.next().setValue(CalculateFixed(this.value,this.prev().value));" />
                                    </Listeners>
                                  <Validator Handler="return !isNaN(this.value) && this.value>0 && this.value<100;" />
                                </ext:TextField>

                                <ext:NumberField
                                    runat="server" Name="fixedAmount"
                                    AllowBlank="false"
                                    MinValue="0"
                                    ID="deFixedAmount"
                                    FieldLabel="<%$ Resources:FieldFixedAmount%>" DecimalPrecision="2" AllowDecimals="true" >
                                  
                                  <Validator Handler="return this.value>0;" />
                                </ext:NumberField>
                                <ext:TextArea runat="server" Name="comment" DataIndex="comment" ID="deComment" FieldLabel="<%$ Resources:FieldComment%>" />
                                <ext:TextField InputType="Password" Visible="false" runat="server" Name="comment" DataIndex="comment" ID="deCommentField" FieldLabel="<%$ Resources:FieldComment%>" />
                                <ext:Checkbox ID="deIsTaxable" FieldLabel="<%$ Resources: FieldIsTaxable %>" runat="server" DataIndex="isTaxable" Name="isTaxable" InputValue="true" />
                                  <ext:ComboBox  AllowBlank="false"  AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"   ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="key" DisplayField="value" ID="deductionCalcType"  FieldLabel="<%$ Resources:FieldEdCalcType%>" SubmitValue="true"  Name="edCalcType" StoreID="edCalcTypeStore" >
                                             
        
                                              </ext:ComboBox>
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button15" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{DEForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveDE" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditDEWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{DEId}.getValue()" Mode="Raw" />
                                   <ext:Parameter Name="FixedAmount" Value="#{deFixedAmount}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="oldAmount" Value="#{DEoldValue}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="oldInclude" Value="#{oldDEIncludeInFinal}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{DEForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button16" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>

