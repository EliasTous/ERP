﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BenefitAcquisitions.aspx.cs" Inherits="Web.UI.Forms.BenefitAcquisitions" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
  
    <script type="text/javascript" src="Scripts/common.js?id=10"></script>
    <script type="text/javascript" src="Scripts/Payroll.js?id=5"></script>
    <script  type="text/javascript" src="Scripts/FinalSettlements.js?id=78"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>


    <script  type="text/javascript">
       
        function openInNewTab() {
            window.document.forms[0].target = '_blank';

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
         <ext:Hidden ID="totalFsCount" runat="server" />
        <ext:Hidden ID="dateFormat" runat="server" />
         <ext:Hidden ID="ENSeq" runat="server" Text="0" />
        <ext:Hidden ID="DESeq" runat="server" Text="1000" />
        <ext:Hidden ID="Seq" runat="server" Text="0" />
        <ext:Hidden ID="isAddEn" runat="server" Text="" />
         <ext:Hidden ID="bsIdHidden" runat="server" Text="" />
         <ext:Hidden ID="EditMode" runat="server" Text="" />
              <ext:Hidden ID="oldDays" runat="server" />


          <ext:Hidden ID="finalSetlemntRecordId" runat="server" Text="" />
         
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
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
                        <ext:ModelField Name="employeeName"  />
                        <ext:ModelField Name="benefitName" />  
                        <ext:ModelField Name="bsName" />  
                        <ext:ModelField Name="period" />  
                        <ext:ModelField Name="employeeId" />  
                        <ext:ModelField Name="bsId" />  
                        <ext:ModelField Name="benefitId" />  
                        <ext:ModelField Name="aqType" />  
                        <ext:ModelField Name="aqDate" />  
                        <ext:ModelField Name="dateFrom" />  
                        <ext:ModelField Name="dateTo" />  
                        <ext:ModelField Name="isHijriDate" />  
                        <ext:ModelField Name="amount" />  
                        <ext:ModelField Name="aqRatio" />  
                        <ext:ModelField Name="aqAmount" />  
                        <ext:ModelField Name="amountDue" />  
                        <ext:ModelField Name="notes" />  

                      
                      
                      
                        
                      

                    </Fields>
                </ext:Model>
            </Model>
             <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>

        </ext:Store>



          <ext:Store
            ID="benefitsFilterStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="benefitsFilterStore_RefreshData"
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
                            <ext:ModelField Name="name" />                
                            
                                 
                                          
                        
                      

                    </Fields>
                </ext:Model>
            </Model>
             <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>

        </ext:Store>
      


        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:GridPanel
                    ID="GridPanel1"
                    runat="server"
                    StoreID="Store1"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                 
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                               
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <%--<Click Handler="CheckSession(); App.totalCount.value=GridPanel1.getStore().getTotalCount();" />--%>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                          
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                          
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                 <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="employeeFilter" Name="employeeId"
                                    DisplayField="fullName"
                                     LabelWidth="60"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" FieldLabel="<%$ Resources: FieldName%>"
                                    TriggerAction="Query" ForceSelection="true" >
                                    <Store>
                                        <ext:Store runat="server" ID="Store4" AutoLoad="false">
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
                                    <Items>
                                    </Items>
                                   
                                </ext:ComboBox>

                                    <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                   
                            <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="benefitFilter" 
                                    DisplayField="name"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                   SubmitValue="true"
                                    MinChars="1" FieldLabel="<%$ Resources: benefitId%>"
                                    ForceSelection="true" StoreID="benefitsFilterStore"  LabelWidth="70">
                               
                                <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>
                                                                     
                                </ext:ComboBox>
                               
                               

                            <%--    <ext:Button runat="server" Icon="Printer">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server"  Text="<%$Resources:Common,Print%>" AutoPostBack="true" OnClick="printBtn_Click" OnClientClick="openInNewTab();"  >
                                            
                                                    <Listeners>
                                                        <Click Handler="openInNewTab();" />
                                                    </Listeners>
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server"  Text="Pdf" AutoPostBack="true" OnClick="ExportPdfBtn_Click"  >
                                            
                                                    
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server"  Text="Excel" AutoPostBack="true" OnClick="ExportXLSBtn_Click"  >
                                            
                                                    
                                                </ext:MenuItem>
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>--%>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                             <ext:Column CellCls="cellLink" ID="ColrecordId" MenuDisabled="true" runat="server" Text="" DataIndex="recordId" Width="150" Hideable="false" Visible="false">
                            </ext:Column>
                          
                            <ext:Column CellCls="cellLink" ID="ColEmployeeName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="employeeName" Flex="2" Hideable="false">
                              
                            </ext:Column>
                           <ext:Column runat="server" ID="ColBenefitName" Text="<%$ Resources: benefitName%>" DataIndex="benefitName" Width="150" />
                           <ext:DateColumn runat="server" ID="ColAqDate" Text="<%$ Resources: aqDate%>" DataIndex="aqDate" Width="150" />
                           <ext:DateColumn runat="server" ID="ColdateFrom" Text="<%$ Resources: dateFrom%>" DataIndex="dateFrom" Width="150" />
                           <ext:DateColumn runat="server" ID="ColDateTo" Text="<%$ Resources: dateTo%>" DataIndex="dateTo" Width="150" />
                            
                           


                            <ext:Column runat="server"
                                ID="colDelete" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="100"
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
                                Visible="false"
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
                                ID="colEdit" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                    <Renderer Handler="var x = editRender(); x=x+'&nbsp&nbsp'; return x;" />
                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

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
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                 <ext:Parameter Name="recordId" Value="record.data['recordId']" Mode="Raw" />
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
            </Items>
        </ext:Viewport>



        <ext:Window
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="600"
            Height="600"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit" Draggable="true">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="BasicInfoTab" DefaultButton="SaveButton"
                            runat="server"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5"
                            Layout="TableLayout"
                             Title="<%$ Resources: WindowTitle%>">
                            <Items>
                                   <ext:Panel runat="server" MarginSpec="0 20 0 0" ID="left">
                                       <Items>
                                  <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="employeeId" Name="employeeId"
                                    DisplayField="fullName"
                                    
                                    ValueField="recordId" AllowBlank="false"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" FieldLabel="<%$ Resources: FieldName%>"
                                    TriggerAction="Query" ForceSelection="true">
                                    <Store>
                                        <ext:Store runat="server" ID="Store5" AutoLoad="false">
                                            <Model>
                                                <ext:Model runat="server" IDProperty="recordId">
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
                                    <DirectEvents>

                                        <Select OnEvent="FillEmployeeInfo">
                                            <ExtraParams>
                                                    <ext:Parameter Name="EmpId" Value="#{employeeId}.getValue()" Mode="Raw" />
                                                                                 
                                                </ExtraParams>
                                            </Select>
                                                                                     
                                      
                                    </DirectEvents>
                                    <Items>
                                    </Items>
                                   
                                </ext:ComboBox>
                           <%-- <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="employeeId" Name="employeeId"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="false"
                                    TypeAhead="false"
                                    HideTrigger="true" 
                                SubmitValue="true"
                                    MinChars="3" FieldLabel="<%$ Resources: FieldName%>"
                                    TriggerAction="Query" ForceSelection="true" QueryMode="Local">

                                                                                                     
                                    <Store>
                                        <ext:Store runat="server" ID="Store2">
                                            <Model>
                                                <ext:Model runat="server"  IDProperty="recordId">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" Type="int"/>
                                                        <ext:ModelField Name="fullName" Type="String" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillEmployee"></ext:PageProxy>
                                            </Proxy>
                                        </ext:Store>
                                    </Store>
                                 <%--   <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>--
                                    <Items>
                                    </Items>
                                    <DirectEvents>
                                        <Select OnEvent="FillEmployeeInfo">
                                             
                                         
                                        </Select>
                                    </DirectEvents>
                                </ext:ComboBox>--%>
                            <ext:TextField  ReadOnly="true"  ID="bsName" runat="server" FieldLabel="<%$ Resources:bsName%>" Name="bsName" AllowBlank="true" />
                            <ext:TextField Visible="false"  ID="bsId" runat="server"  Name="bsId" />
                         
                      
                            <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="ComboBenefitId" 
                                    DisplayField="benefitName"
                                    ValueField="benefitId" AllowBlank="false"
                                    TypeAhead="false"
                                   SubmitValue="true"
                                    MinChars="1" FieldLabel="<%$ Resources: benefitId%>"
                                    ForceSelection="true">
                                    <Store>
                                        <ext:Store runat="server" ID="Store3" AutoLoad="false">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="benefitId" />
                                                        <ext:ModelField Name="benefitName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                          </ext:Store>
                                    </Store>
                                <DirectEvents> 
                                    <Change OnEvent="selectAqType">
                                        <ExtraParams>
                                         <ext:Parameter Name="benefitId" Value="this.value" Mode="Raw" />
                                            </ExtraParams>
                                    </Change>
                                </DirectEvents>
                              <Listeners>
                                  <Select Handler="if(this.value!=null){ #{dateFrom}.setDisabled(false); }"></Select>
                              </Listeners>
                                                                     
                                </ext:ComboBox>
                                            
                                             <ext:DateField    ID="aqDate" runat="server" FieldLabel="<%$ Resources:aqDate%>" Name="aqDate" AllowBlank="false" />
                                             <ext:DateField    ID="dateFrom" runat="server" FieldLabel="<%$ Resources:dateFrom%>" Name="dateFrom" AllowBlank="false" Disabled="true"  >
                                                 <Validator Handler="if(App.dateTo.value ==null) return true;   if(this.value> App.dateTo.value) return false; else return true;" />
                                                 <Listeners>
                                                     <Change Handler="if(this.value!=null) #{dateTo}.setDisabled(false);" />
                                                 </Listeners>
                                                 <DirectEvents>
                                                     <Select  OnEvent="getAqRatio" >
                                                         <ExtraParams> 
                                                             <ext:Parameter Name="dateFrom" Value="this.value" Mode="Raw" />
                                                                <ext:Parameter Name="benefitId" Value="#{ComboBenefitId}.getValue()" Mode="Raw" />
                                                              <ext:Parameter Name="employeeId" Value="#{employeeId}.getValue()" Mode="Raw" />
                                                               <ext:Parameter Name="dateTo" Value="#{dateTo}.value" Mode="Raw" />
                                                         </ExtraParams>
                                                         </Select>
                                                     
                                                 </DirectEvents>
                                                 </ext:DateField>
                                             <ext:DateField    ID="dateTo" runat="server" FieldLabel="<%$ Resources:dateTo%>" Name="dateTo" AllowBlank="false"  Disabled="true">
                                                   <Validator Handler="if(App.dateFrom.value ==null) return true; if(this.value< App.dateFrom.value) return false; else return true;" />
                                                          <DirectEvents>
                                                              <Change OnEvent="returnPeriodDate" >
                                                                  <ExtraParams> 
                                                                       <ext:Parameter Name="dateTo" Value="this.value" Mode="Raw" />
                                                                         <ext:Parameter Name="dateFrom" Value="#{dateFrom}.value" Mode="Raw" />
                                                                  </ExtraParams>
                                                                  </Change>
                                                              
                                                          </DirectEvents>                            
                                              <%--   <Listeners>
                                                      <Change Handler="calcDays();" />
                                                     </Listeners>--%>
                                                 </ext:DateField>
                                             <ext:TextField  ID="period" runat="server" FieldLabel="<%$ Resources:period%>" Name="days" AllowBlank="true" ReadOnly="true" />
                                              
                                              
                                           <ext:TextField  ID="aqRatio" runat="server" FieldLabel="<%$ Resources:aqRatio%>" Name="aqRatio" AllowBlank="true" ReadOnly="true">
                                               <Listeners>
                                                <Change Handler="  #{aqAmount}.setValue(this.value * App.amount.value / 100); "/>
                                                   </Listeners>
                                          
                                               </ext:TextField>
                                           
                                              <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="deliveryType" runat="server" FieldLabel="<%$ Resources: deliveryType%>" Name="deliveryType" IDMode="Static" SubmitValue="true" AllowBlank="false" >
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: service%>" Value="<%$ Resources:ComboBoxValues, BEBADeliveryTypeService %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: cash%>" Value="<%$ Resources:ComboBoxValues, BEBADeliveryTypeCash %>"></ext:ListItem>
                                       
                                    </Items>
                                </ext:ComboBox>

                                             <ext:NumberField  ID="amount" runat="server" FieldLabel="<%$ Resources:amount%>" Name="amount" AllowBlank="false" MinValue="0" AllowDecimals="false"  >
                                                 <Listeners >
                                                     <Change Handler="  #{aqAmount}.setValue(this.value * App.aqRatio.value / 100 );#{amountDue}.setValue(this.value - App.aqAmount.value );  "/>
                                                 <%--     <Focus Handler="this.setValue(this.getValue().replace(/[\$,]/g, ''));" />
                                                  <Blur Handler="this.setValue(Ext.util.Format.number(#{amount}.getValue(), '0,000.00'));" />--%>
                                                 </Listeners>
                                                     <Validator Handler="if (this.value>0) return true; else return false;" />
                                                 
                                                 </ext:NumberField>
                                          
       
                                              <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="aqType" runat="server" FieldLabel="<%$ Resources: aqType%>" Name="aqType" IDMode="Static" SubmitValue="true" ReadOnly="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: Progressive%>" Value="<%$ Resources:ComboBoxValues, BEBAAqtypeProgressive %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Unbound%>" Value="<%$ Resources:ComboBoxValues, BEBAAqtypeUnbound %>"></ext:ListItem>
                                       
                                    </Items>
                                </ext:ComboBox>
                                             <ext:NumberField  ID="aqAmount" runat="server" FieldLabel="<%$ Resources:aqAmount%>" Name="aqAmount" AllowBlank="true" AllowDecimals="false"  >
                                                 <Listeners> 
                                                     <Change Handler="#{amountDue}.setValue(App.amount.value - this.value); " />
                                                   <%--  <Focus Handler="this.setValue(this.getValue().replace(/[\$,]/g, ''));" />
                                                  <Blur Handler="this.setValue(Ext.util.Format.number(#{amount}.getValue(), '0,000.00'));" />--%>
                                                 </Listeners>
                                                 </ext:NumberField>
                                          <ext:NumberField  ID="amountDue" runat="server" FieldLabel="<%$ Resources:amountDue%>" Name="amountDue" AllowBlank="true" MinValue="0" AllowDecimals="false" > 
                                              <%--      <ext:NumberField  ID="amountDue" runat="server" FieldLabel="<%$ Resources:amountDue%>" Name="amountDue" AllowBlank="true" MaskRe="/[0-9\$\.]/">   
                                                  <Listeners> 
                                                     
                                                     <Focus Handler="this.setValue(this.getValue().replace(/[\$,]/g, ''));" />
                                                  <Blur Handler="this.setValue(Ext.util.Format.number(#{amountDue}.getValue(), '0,000.00'));" />
                                                 </Listeners>--%>
                                                 </ext:NumberField>
                                           <ext:TextArea  ID="notes" runat="server" FieldLabel="<%$ Resources:notes%>" Name="notes" AllowBlank="true" />

                                              </Items>
                                   </ext:Panel>
                                   <ext:Panel runat="server" MarginSpec="0 0 0 0" ID="rightPanel">
                                  <Items>
                                 <ext:TextField runat="server" ID="recordId" Name="recordId" Hidden="true" />
                        
                                 
                                 
                             

                                <ext:TextField  ReadOnly="true"  ID="nationalityTx" runat="server" FieldLabel="<%$ Resources:nationality%>" Name="nationality" AllowBlank="true">
                                    
                                </ext:TextField>
                                <ext:TextField ReadOnly="true"  ID="branchNameTx" runat="server" FieldLabel="<%$ Resources:branchName%>" Name="branchName" AllowBlank="true">
                                    
                                </ext:TextField>
                                 <ext:TextField ReadOnly="true"  ID="departmentNameTx" runat="server" FieldLabel="<%$ Resources:departmentName%>" Name="departmentName" AllowBlank="true">
                                    
                                </ext:TextField>
                                 <ext:TextField ReadOnly="true"  ID="positionNameTx" runat="server" FieldLabel="<%$ Resources:positionName%>" Name="positionName" AllowBlank="true">
                                    
                                </ext:TextField>
                                 <ext:DateField ReadOnly="true"   ID="hireDateDf" runat="server" FieldLabel="<%$ Resources:hireDate%>" Name="hireDate" AllowBlank="true">
                                    
                                </ext:DateField>
                                        
                                 <ext:TextField ReadOnly="true"  ID="esName" runat="server" FieldLabel="<%$ Resources:esName%>" Name="esName" AllowBlank="true">
                                    
                                </ext:TextField>
                                <ext:TextField ReadOnly="true"  ID="loanBalance" runat="server" FieldLabel="<%$ Resources:loanBalance%>" Name="loanBalance" AllowBlank="true">
                                    
                                </ext:TextField>
                                 <ext:TextField ReadOnly="true"  ID="divisionName" runat="server" FieldLabel="<%$ Resources:FieldDivision%>" Name="divisionName" AllowBlank="true">
                                    
                                </ext:TextField>
                                 <ext:TextField ReadOnly="true"  ID="reportToName" runat="server" FieldLabel="<%$ Resources:FieldReportsTo%>" Name="reportToName" AllowBlank="true">
                                    
                                </ext:TextField>
                                  <ext:TextField ReadOnly="true"  ID="eosBalance" runat="server" FieldLabel="<%$ Resources:eosBalanceTitle%>" Name="eosBalance" AllowBlank="true">
                                    
                                </ext:TextField>
                                  <ext:DateField  ReadOnly="true"   ID="lastLeaveStartDate" runat="server" FieldLabel="<%$ Resources:lastLeaveStartDateTitle%>" Name="lastLeaveStartDateTitle" AllowBlank="true">
                                    
                                </ext:DateField>
                                <ext:DateField  ReadOnly="true"  ID="lastLeaveEndDate" runat="server" FieldLabel="<%$ Resources:lastLeaveEndDateTitle%>" Name="lastLeaveEndDate" AllowBlank="true">
                                    
                                </ext:DateField>
                                 <ext:TextField ReadOnly="true"  ID="paidLeavesYTD" runat="server" FieldLabel="<%$ Resources:paidLeavesYTDTitle%>" Name="paidLeavesYTD" AllowBlank="true">
                                    
                                </ext:TextField>
                                  <ext:NumberField ReadOnly="true"  ID="leavesBalanceNF" runat="server" FieldLabel="<%$ Resources:leavesBalanceTitle%>" Name="leavesBalance" AllowBlank="true"  AllowDecimals="true" DecimalPrecision="2" >
                                    
                                </ext:NumberField>
                                  <ext:TextField ReadOnly="true"  ID="allowedLeaveYtd" runat="server" FieldLabel="<%$ Resources:allowedLeaveYtdTitle%>" Name="allowedLeaveYtd" AllowBlank="true">
                                    
                                </ext:TextField>
                                <ext:TextField  ReadOnly="true"  ID="serviceDuration" runat="server" FieldLabel="<%$ Resources:serviceDuration%>" Name="serviceDuration" AllowBlank="true">
                                    
                                </ext:TextField>
                                </Items>
                                     </ext:Panel>
                                 



                               

                                
                                <%--<ext:TextField ID="intName" runat="server" FieldLabel="<%$ Resources:IntName%>" Name="intName"   AllowBlank="false"/>--%>
                            </Items>

                        </ext:FormPanel>
                          
                            
                       

                    </Items>
                </ext:TabPanel>
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
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="ComboBenefitId" Value="#{ComboBenefitId}.getValue()" Mode="Raw" />
                         
                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
              
           <%--   <ext:Button runat="server" Icon="Printer" Text="<%$ Resources:Common , Print %>">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server"  Text="<%$ Resources:Common , Print %>" AutoPostBack="true" OnClick="printBtn_Click" OnClientClick="openInNewTab();"  >
                                            
                                                    <Listeners>
                                                        <Click Handler="openInNewTab();" />
                                                    </Listeners>
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server"  Text="Pdf" AutoPostBack="true" OnClick="ExportPdfBtn_Click"  >
                                            
                                                    
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server"  Text="Excel" AutoPostBack="true" OnClick="ExportXLSBtn_Click"  >
                                            
                                                    
                                                </ext:MenuItem>
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>--%>
                        
            </Buttons>
        </ext:Window>
      <%--  <ext:Window
            ID="EditENWindow"
            runat="server"
            Icon="PageEdit"
             Title="<%$ Resources:EntitlementEditWindow %>"
            Width="400"
            Height="150"
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

                              
                                              <ext:ComboBox  SubmitValue="true"  AnyMatch="true" CaseSensitive="false"  runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" DisplayField="name"  ID="entEdId" Name="edId" FieldLabel="<%$ Resources:FieldEntitlement%>" SimpleSubmit="true" StoreID="entsStore" ReadOnly="true">
                                            
                                             
                                            <Listeners>
                                                <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                <ext:TextField ID="seqNo" Hidden="true" runat="server" FieldLabel="seqNo" Disabled="false" Name="seqNo" />
                                
                                <ext:TextField ID="amount" Hidden="false" runat="server" FieldLabel="<%$ Resources: amount%>" Disabled="false" Name="amount" />
                                

                                
                              
               
                            

                               

                            
                            
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
            Title="<%$ Resources:DeductionEditWindow %>"
            Width="400"
            Height="150"
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
                <ext:TabPanel ID="TabPanel4"  runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" Header="false"  TitleCollapse="true">
                    <Items>
                        <ext:FormPanel
                            ID="DEForm" DefaultButton="SaveDEButton"
                            runat="server"
                            Title="<%$ Resources:DeductionEditWindow %>"
                            Icon="ApplicationSideList"
                            Header="false" TitleCollapse="true"
                            DefaultAnchor="100%" Layout="AutoLayout"
                            BodyPadding="5">
                            <Items>
                                    <ext:ComboBox    AnyMatch="true" CaseSensitive="false"  runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" DisplayField="name" ID="dedEdId" Name="edId" FieldLabel="<%$ Resources:FieldDeduction%>" SimpleSubmit="true" StoreID="dedsStore">
                      
                                   
                                    <Listeners>
                                        <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                 <ext:TextField ID="DeSeqNo" Hidden="true" runat="server" FieldLabel="seqNo" Disabled="false" Name="seqNo" />
                                
                                <ext:TextField ID="DeAmount" Hidden="false" runat="server" FieldLabel="<%$ Resources:amount %>" Disabled="false" Name="amount" AllowBlank="false"/>
                                

                                                        
                            
                                
                               
                             
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveDEButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{DEForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveDE" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditDEWindow}.body}" />
                            <ExtraParams>
                                 <ext:Parameter Name="isAddEn" Value="1" Mode="Raw" Encode="true" />
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
        </ext:Window>--%>



    </form>
</body>
</html>
