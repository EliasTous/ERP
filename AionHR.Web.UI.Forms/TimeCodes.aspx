﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeCodes.aspx.cs" Inherits="AionHR.Web.UI.Forms.TimeCodes" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/DocumentTypes.js" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
   
 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
         <ext:Hidden ID="currentEDtype" runat="server"  />
          <ext:Store runat="server" ID="entsStore"  AutoLoad="true">
            <Model>
                <ext:Model runat="server" IDProperty="recordId">
                    <Fields>
                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="name" />
                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>
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
                <ext:Model ID="Model1" runat="server" >
                    <Fields>

                        <ext:ModelField Name="edType" />   
                         <ext:ModelField Name="edTypeString" />                         
                        <ext:ModelField Name="timeCode" />
                          <ext:ModelField Name="name" />
                          <ext:ModelField Name="edId" />
                          <ext:ModelField Name="apId" />
                           <ext:ModelField Name="edName" />
                           <ext:ModelField Name="apName" />
                          <ext:ModelField Name="gracePeriod" />


                      
                   
                        <%--<ext:ModelField Name="intName" />--%>
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="timeCode" Direction="ASC" />
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
                                <ext:Button ID="btnAdd" Visible="false" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                     <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button ID="btnReload" runat="server"  Icon="Reload">       
                                     <Listeners>
                                        <Click Handler="CheckSession();#{Store1}.reload();" />
                                    </Listeners>                           
                                   
                                </ext:Button>
                                <ext:Button Visible="false" ID="btnDeleteSelected" runat="server" Text="<%$ Resources:Common , DeleteAll %>" Icon="Delete">
                                 <Listeners>
                                        <Click Handler="CheckSession();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnDeleteAll">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                 <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180" >
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                            <TriggerClick Handler="#{Store1}.reload();" />
                                        </Listeners>
                                    </ext:TextField>
                                  <%-- <ext:Button runat="server" Icon="Printer">
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
                                </ext:Button>
                            --%>
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                              <ext:Column    CellCls="cellLink" ID="ColEdType" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldType%>" DataIndex="edtype" Flex="1" Visible="false">
                    
                                </ext:Column>
                            <ext:Column    CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTimeCode%>" DataIndex="name" Flex="2" Hideable="false">
                    
                                </ext:Column>
                               
                               <ext:Column    CellCls="cellLink" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEdId%>" DataIndex="edName" Flex="1" Hideable="false">
                    
                                </ext:Column>
                               <ext:Column    CellCls="cellLink" ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldType%>" DataIndex="edTypeString" Flex="1" Hideable="false">
                    
                                </ext:Column>
                               <ext:Column    CellCls="cellLink" ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldApproval%>" DataIndex="apName" Flex="1" Hideable="false">
                    
                                </ext:Column>
                             <ext:Column    CellCls="cellLink" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldGracePeriod%>" DataIndex="gracePeriod" Flex="2" Hideable="false">
                    
                                </ext:Column>
                           
                           
                            

                            
                        
                           

                         
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
                                ID="colEdit"  Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return editRender(); " />

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
                                <ext:Parameter Name="edType" Value="record.data['edType']" Mode="Raw" />
                                   <ext:Parameter Name="timeCode" Value="record.data['timeCode']" Mode="Raw" />
                                   <ext:Parameter Name="edId" Value="record.data['edId']" Mode="Raw" />
                                   <ext:Parameter Name="apId" Value="record.data['apId']" Mode="Raw" />
                                   <ext:Parameter Name="gracePeriod" Value="record.data['gracePeriod']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>

                  
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single"  StopIDModeInheritance="true" />
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
                            ID="BasicInfoTab" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                
                              <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="edType" runat="server" FieldLabel="<%$ Resources:FieldType%>" Name="edType" IDMode="Static" SubmitValue="true" ForceSelection="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: Entitlement %>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Deduction %>" Value="2" ></ext:ListItem>
                                      
                                    </Items>
                                <Listeners>
                                    <Select Handler="#{currentEDtype}.setValue(this.value);App.entEdId.select();" />
                                </Listeners>
                                 <DirectEvents>
                                     <Select OnEvent="Unnamed_Event" />
                                 </DirectEvents>
                                </ext:ComboBox>
                            
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="timecode" runat="server" FieldLabel="<%$ Resources:FieldTimeVariationType%>" Name="timecode" IDMode="Static" SubmitValue="true" SimpleSubmit="true" ReadOnly="true"  >
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:Common ,  UnpaidLeaves %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_UNPAID_LEAVE %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  PaidLeaves %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_PAID_LEAVE %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  SHIFT_LEAVE_WITHOUT_EXCUSE %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_SHIFT_LEAVE_WITHOUT_EXCUSE %>" ></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  DAY_LEAVE_WITHOUT_EXCUSE  %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_DAY_LEAVE_WITHOUT_EXCUSE %>" ></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  LATE_CHECKIN %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_LATE_CHECKIN %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  DURING_SHIFT_LEAVE %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_DURING_SHIFT_LEAVE %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  EARLY_LEAVE   %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_EARLY_LEAVE   %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  EARLY_CHECKIN %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_EARLY_CHECKIN %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  OVERTIME %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_OVERTIME %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  MISSED_PUNCH %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_MISSED_PUNCH %>"></ext:ListItem>
                                      <ext:ListItem Text="<%$ Resources:Common ,  Day_Bonus %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_Day_Bonus %>"></ext:ListItem>
                                      
                                    </Items>
                                </ext:ComboBox>

                                   <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"  DisplayField="name" ID="entEdId" Name="edId" FieldLabel="<%$ Resources:FieldEdId%>" SimpleSubmit="true" StoreID="entsStore" />
                                <ext:ComboBox AutoScroll="true"  AnyMatch="true" CaseSensitive="false" EnableRegEx="true"     runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="apId" Name="apId" FieldLabel="<%$ Resources:FieldApproval%>" >
                              
                                                <Store>
                                                    <ext:Store runat="server" ID="ApprovalStore" >
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
                                <ext:NumberField ID="gracePeriod" runat="server" FieldLabel="<%$ Resources:FieldGracePeriod%>" Name="gracePeriod"   AllowBlank="false" MinValue="0" MaxValue="9999" />
                              
                                 
                                 
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
                              
                                <ext:Parameter Name="values" Value ="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
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



    </form>
</body>
</html>