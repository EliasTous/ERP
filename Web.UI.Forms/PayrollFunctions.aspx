﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollFunctions.aspx.cs" Inherits="Web.UI.Forms.PayrollFunctions" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/CertificateLevels.js" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
   
 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentFunctionId" runat="server" />
        <ext:Hidden ID="CurrentDelFuncID" runat="server" />


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
                        
                        <ext:ModelField Name="name" />
                          <ext:ModelField Name="functionBody" />
                        <%--<ext:ModelField Name="reference" />--%>
                      
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
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column    CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="1" Hideable="false" />
                                <ext:Column    CellCls="cellLink" ID="ColFunctionBody" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBody%>" DataIndex="functionBody" Flex="1" Hideable="false" />

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

                                <Renderer handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

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
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
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
            Width="550"
            Height="400"
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
                                <ext:TextField ID="recordId" runat="server"  Name="recordId"  Hidden="true"/>
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name"   AllowBlank="false"/>
                                <ext:TextArea ID="functionBody" runat="server" FieldLabel="<%$ Resources:FieldBody%>" Name="functionBody"   AllowBlank="false" MinHeight="150" MinWidth="200" />
 
                            </Items>

                        </ext:FormPanel>
                        

                        <ext:GridPanel
                            ID="FunConstGridPanel"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            MaxHeight="350"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                             Title="<%$ Resources: FUNConstTitle %>"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True" >
                                  <TopBar>
                                    <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                                     <Items>
                                    <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                            <Listeners>
                                                 <Click Handler="CheckSession();" />
                                            </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDFunConstNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{FunConstGridPanel}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                            
                            <Store>
                                <ext:Store runat="server" ID="FunctionConstStore" OnReadData="FUNConstStore_ReadData">
                                    <Model>
                                        <ext:Model runat="server">
                                            <Fields>
                                               <ext:ModelField Name="functionId" />
                                                <ext:ModelField Name="constant" />
                                                   
                                                
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            
                            <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="ColFunctionId" Visible="false" DataIndex="functionId" runat="server" />
                                   
                                     <ext:Column ID="Colconstant" DataIndex="constant" Text="<%$ Resources: Fieldconstant%>" runat="server" Flex="1"/>

                                   
                                    
                            <ext:Column runat="server"
                                ID="Column1"  Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return deleteRender(); " />

                            </ext:Column>


                                </Columns>
                            </ColumnModel>

                          
                          <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    
                               <DirectEvents>
                        <CellClick OnEvent="PoPuPFUNConst">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="constant" Value="record.data['constant']" Mode="Raw" />
                                <ext:Parameter Name="functionId" Value="record.data['functionId']" Mode="Raw" />
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView4" runat="server" />
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel3" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                         
                     </ext:GridPanel>


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
                                <ext:Parameter Name="values" Value ="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                 

                 <ext:Button ID="checkEX" runat="server" Text="<%$ Resources: checkEX %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="checkRecordExpression" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="name" Value="#{name}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="functionBody" Value="#{functionBody}.getValue()" Mode="Raw" />
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

        <ext:Window 
            ID="EditFUNConstWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:FUNConstEditWindowTitle %>"
            Width="450"
            Height="400"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="FUNConstForm" DefaultButton="SaveFUNConstButton"
                            runat="server"
                            Title="<%$ Resources: FUNConstEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5">
                            <Items>
                              <ext:TextField ID="functionId" Hidden="true" runat="server"  Disabled="true" Name="functionId" />


                                 <ext:ComboBox   AnyMatch="true" CaseSensitive="false" runat="server" AllowBlank="true" ValueField="constant" QueryMode="Local" ForceSelection="true" 
                                    TypeAhead="true" MinChars="1" DisplayField="constant" ID="constant" Name="constant" FieldLabel="<%$ Resources:Filedconstant%>" >
                                    <Store>
                                        <ext:Store runat="server" ID="ConstantStore" >
                                            <Model>
                                                <ext:Model runat="server" IDProperty="constant">
                                                    <Fields>                                                        
                                                        <ext:ModelField Name="constant" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                         </ext:Store>
                                    </Store>
                                  
                                </ext:ComboBox>


                                
                                
                                  
                            </Items>
                          
                        </ext:FormPanel>
                          

                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveFUNConstButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession();  if (!#{FUNConstForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewFUNConstRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditFUNConstWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="constant" Value="#{constant}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="functionId" Value="#{functionId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{FUNConstForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>


    </form>
</body>
</html>