﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SocialSecuritySchedules.aspx.cs" Inherits="Web.UI.Forms.SocialSecuritySchedules" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/DocumentTypes.js?id=1"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript">
        function display(code) {
            return document.getElementById(code + "text").value;
        }
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";


            }
            return out;
        }
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
        <ext:Hidden ID="ScheduleID" runat="server" />
        <ext:Hidden ID="SocailSecurityrecordId" runat="server" /> 
        <ext:Hidden ID="SocailSecurityseqId" runat="server" text="0"/> 

       

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
                         <ext:ModelField Name="coPct" />
                         <ext:ModelField Name="emPct" />
                     
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
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
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
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{Store1}.reload();" />
                                    </Listeners>
                                </ext:TextField>
                              <%--     <ext:Button runat="server" Icon="Printer">
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

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName %>" DataIndex="name" Flex="2" Hideable="false"/>
                            <ext:Column CellCls="cellLink" ID="COLcoPct" MenuDisabled="true" runat="server" Text="<%$ Resources: CompanyPercentage %>" DataIndex="coPct" Flex="2" Hideable="false">
                                  <Renderer Handler="return record.data['coPct']+ '%'; " />
                                </ext:Column>
                            <ext:Column CellCls="cellLink" ID="ColemPct" MenuDisabled="true" runat="server" Text="<%$ Resources: EmployeePercentage %>" DataIndex="emPct" Flex="2" Hideable="false">
                             <Renderer Handler="return record.data['emPct']+ '%'; " />
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
                                ID="colEdit" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

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
            </Items>
        </ext:Viewport>



        <ext:Window
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="555"
            Height="320"
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
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:TextField  ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name" AllowBlank="false" LabelWidth="160" ReadOnly="false" />
                                <ext:NumberField Width="400" AllowBlank="true" runat="server" LabelWidth="160" ID="lcoPct" Name="coPct" FieldLabel="<%$ Resources: CompanyPercentage %>" MinValue="0" MaxValue="100" ReadOnly="true"/>
                                 <ext:NumberField Width="400" AllowBlank="true" runat="server" LabelWidth="160" ID="lemPct" Name="emPct" FieldLabel="<%$ Resources: EmployeePercentage %>" MinValue="0"  MaxValue="100" ReadOnly="true" />
                                  
                                <%--<ext:TextField ID="intName" runat="server" FieldLabel="<%$ Resources:IntName%>" Name="intName"   AllowBlank="false"/>--%>
                            </Items>
                            

                        </ext:FormPanel>
                        
                        <ext:GridPanel 
                    ID="socialSetupGrid" AutoUpdateLayout="true" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="true"
                    Title="<%$ Resources:EditSocialSecuritySetupWindowTitle %> "
                    Layout="FitLayout"
                    Flex="1"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="socialSetupStore"
                            runat="server"
                            RemoteSort="False"
                            RemoteFilter="true"
                                   OnReadData="socialSetupStore_RefreshData"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);"  />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model2" runat="server"  >
                                    <Fields>

                                        <ext:ModelField Name="ssId" />
                                        <ext:ModelField Name="seqNo" />
                                        <ext:ModelField Name="payCode" />
                                        <ext:ModelField Name="name" />
                                        <ext:ModelField Name="coPct" />
                                        <ext:ModelField Name="emPct" />
                                       <ext:ModelField Name="ceiling" />
                                     




                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter  Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                               <ext:Button ID="ADDNewsocialSetupBtn" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession(); " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewsocialSetupRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
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
                                        <TriggerClick Handler="#{socialSetupGrid}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ssIdCol" MenuDisabled="true" runat="server" Text="ssId" DataIndex="ssId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column  ID="seqNoCol" Visible="false" MenuDisabled="true" runat="server" Text=" seqNoCol " DataIndex="seqNoCol" Hideable="false" Flex="1" Align="Center"/>
                            

                            <ext:Column ID="payCodeCol" MenuDisabled="true" runat="server" Text="<%$ Resources:payCode%>" DataIndex="payCode" Hideable="false" Flex="1" Align="Center" />
                               <ext:Column ID="nameCol" MenuDisabled="true" runat="server" Text="<%$ Resources:FieldName%> " DataIndex="name" Hideable="false" Flex="1" Align="Center" />
                             <ext:Column ID="coPctCol" MenuDisabled="true" runat="server" Text=" <%$ Resources:CompanyPercentage%> " DataIndex="coPct" Hideable="false" Flex="1" Align="Center" />
                            <ext:Column ID="emPctCol" MenuDisabled="true" runat="server" Text=" <%$ Resources:EmployeePercentage%> " DataIndex="emPct" Hideable="false" Flex="1" Align="Center" />
                            <ext:Column ID="ceiling" MenuDisabled="true" runat="server" Text="<%$ Resources:ceiling%> " DataIndex="ceiling" Hideable="false" Flex="1" Align="Center" />
                               
                                   


                        <ext:Column runat="server"
                                ID="Column1" Visible="false"
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
                                ID="colAttachSetupGrid"
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
                                ID="Column3" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

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
                        <CellClick OnEvent="PoPuPSocialSetup">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.data['ssId']"  Mode="Raw" />
                                <ext:Parameter Name="seqNo" Value="record.data['seqNo']" Mode="Raw" />
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
            </Buttons>
        </ext:Window>
          <ext:Window
            ID="EditSocialSecuritySetupWindow"
            runat="server"
            Icon="PageEdit"
            Title=" <%$ Resources:EditSocialSecuritySetupWindowTitle %>"
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
                            ID="socialSetupForm" DefaultButton="saveSocialbutton"
                            runat="server"
                            Title="<%$ Resources:EditSocialSecuritySetupWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" Layout="AutoLayout"
                            BodyPadding="5">
                            <Items>

                                 <ext:ComboBox
                                      LabelWidth="160" 
                                         SubmitValue="true"
                                         AnyMatch="true"
                                        CaseSensitive="false" 
                                        runat="server" ValueField="payCode"
                                        QueryMode="Local" ForceSelection="true"
                                        TypeAhead="true" MinChars="1" AllowBlank="false"
                                        DisplayField="name"  ID="payCodeCB" 
                                       Name="payCode" FieldLabel="<%$ Resources:payCode %>" 
                                       SimpleSubmit="true"  ReadOnly="false">
                                       <Store>
                                        <ext:Store   ID="Store2"
                                            runat="server"
                                            RemoteSort="False"
                                            RemoteFilter="true"
                                            OnReadData="PayCode_ReadData"
                                            PageSize="50" IDMode="Explicit" Namespace="App">
                                            <Proxy>
                                                <ext:PageProxy>
                                                    <Listeners>
                                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                                    </Listeners>
                                                </ext:PageProxy>
                                            </Proxy>
                                            <Model>
                                             <ext:Model ID="Model3" runat="server" >
                                                <Fields>                                                                                                
                                                            <ext:ModelField Name="name" />
                                                            <ext:ModelField Name="payCode" />
                                                                               
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                                
                                                 </ext:Store>
                                           </Store>
                                       </ext:ComboBox>
                                <ext:TextField ID="ssIdTF" Hidden="true" runat="server" FieldLabel="ssIdTF"  Name="ssId" />
                                 <ext:TextField ID="seqNoTF" Hidden="true" runat="server" FieldLabel="seqNoTF" Name="seqNo" />
                           <%--  <ext:TextField ID="payCodeTF" Hidden="false" runat="server" FieldLabel=" <%$ Resources:payCode %>" Name="payCode" LabelWidth="160"  MaxLength="4" AllowBlank="false"/>--%>
                                <ext:TextField ID="nameTF" Hidden="false" runat="server" FieldLabel="<%$ Resources: FieldName %>" Name="name" LabelWidth="160" AllowBlank="false"/>
                                 <ext:NumberField Width="400" AllowBlank="false" runat="server" LabelWidth="160" ID="coPctNF" Name="coPct" FieldLabel="<%$ Resources: CompanyPercentage %>" MinValue="0" MaxValue="100" />
                                 <ext:NumberField  Width="400" AllowBlank="false" runat="server" LabelWidth="160" ID="emPctNF" Name="emPct" FieldLabel="<%$ Resources: EmployeePercentage %>" MinValue="0"  MaxValue="100" />
                                 <ext:NumberField  Width="400" AllowBlank="false" runat="server" LabelWidth="160" ID="ceilingTF" Name="ceiling" FieldLabel="<%$ Resources:ceiling %>" />
                                 
                               


                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="saveSocialbutton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{socialSetupForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="saveSocialButton" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditSocialSecuritySetupWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{ssIdTF}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{socialSetupForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            
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
