<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutomatedEmails.aspx.cs" Inherits="AionHR.Web.UI.Forms.AutomatedEmails" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/EntitlementDeductions.js" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
   
 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
     
        <ext:Hidden ID="currentRuId" runat="server"  />
        <ext:Hidden ID="lineSeq" runat="server" />
        
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
                        <ext:ModelField Name="frequencyName" />
                        <ext:ModelField Name="frequency" />
                        <ext:ModelField Name="time" />
                        <ext:ModelField Name="flags" />
                        <ext:ModelField Name="name" />
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>


         <ext:Store
            ID="LanguageStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="LanguageStore_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model2" runat="server" IDProperty="key">
                    <Fields>

                        <ext:ModelField Name="key" />
                        <ext:ModelField Name="value" />

                    </Fields>
                </ext:Model>
            </Model>
        
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
                            <ext:Column ID="Colfrequency" Visible="false" DataIndex="frequency" runat="server" />
                            <ext:Column    CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false" />                               
                            <ext:Column    CellCls="cellLink" ID="ColfrequencyName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldfrequencyName%>" DataIndex="frequencyName" Flex="2" Hideable="false" />
                            <ext:Column    CellCls="cellLink" ID="Coltime" MenuDisabled="true" runat="server" Text="<%$ Resources: Fieldtime%>" DataIndex="time" Flex="1" Hideable="false" />
                            <ext:Column    CellCls="cellLink" ID="Colflags" MenuDisabled="true" runat="server" Text="<%$ Resources: Fieldflags%>" DataIndex="flags" Flex="1" Hideable="false" />
                            

                               
                           

                           
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
                                <ext:TextField ID="recordId" runat="server"  Name="recordId"  Hidden="true"/>
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name"  />
                                <ext:ComboBox AllowBlank="false" AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldfrequencyName%>"  runat="server" DisplayField="value" ValueField="key"   Name="frequency" ID="frequency" >
                                             <Store>
                                                <ext:Store runat="server" ID="frequencyStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                       </ext:ComboBox>


                                <%--<ext:DateField runat="server" ID="time" FieldLabel="<%$ Resources:Fieldtime%>" Name="time" AllowBlank="false" Format="HH:mm" />--%>
                                <ext:TextField ID="time" runat="server" FieldLabel="<%$ Resources:Fieldtime%>" Name="time"  FormatText="HH:MM" MinLengthText="5" MaxLengthText="5">
                                    <DirectEvents>
                                        <FocusLeave OnEvent="CheckTime">
                                            <ExtraParams>
                                                   <ext:Parameter Name="timeId" Value="#{time}.getValue()" Mode="Raw" />                                                                            
                                            </ExtraParams>
                                        </FocusLeave>
                                    </DirectEvents>
                                        
                                    </ext:TextField>
                                <%--<ext:NumberField ID="flags" runat="server" FieldLabel="<%$ Resources:Fieldflags%>" Name="flags" AllowBlank="true" AllowDecimals="false" MinValue="0" />--%>
                                                                
                            </Items>

                        </ext:FormPanel>

              <ext:GridPanel
                    ID="reportsGrid"
                    runat="server"
                   
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: reportsTab %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                  
                    ColumnLines="True" IDMode="Explicit" RenderXType="True" >
                   <TopBar>
                        <ext:Toolbar ID="Toolbar5" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="addReport" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewReportRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{reportsGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                             <Store>
                                 <ext:Store ID="reportsStore" runat="server" IDMode="Explicit">
          
                                            <Model>
                                                <ext:Model runat="server" >
                                                    <Fields>
                                                        <ext:ModelField Name="reportName" />
                                                        <ext:ModelField Name="taskId" />
                                                        <ext:ModelField Name="reportId" />
                                                        
                                                    
                       
                       
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <%--<Sorters>
                                                <ext:DataSorter Property="taskId" Direction="ASC" />
              
                                            </Sorters>--%>
                                        </ext:Store>
                             </Store>

                   

                    <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                             <ext:Column ID="ColtaskId" Visible="false" DataIndex="taskId" runat="server" />
                             <ext:Column    CellCls="cellLink" ID="ColreportName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldreportName%>" DataIndex="reportName" Flex="2" Hideable="false" /> 


                                                      
                            
                             <ext:Column runat="server"
                                ID="Column17"  Visible="true"
                                Text=""
                                MinWidth="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return  deleteRender();" />

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
                        <CellClick OnEvent="PoPuPReports">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <%--<ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />--%>
                                <ext:Parameter Name="id" Value="record.data['taskId']" Mode="Raw" />
                                <ext:Parameter Name="reportId" Value="record.data['reportId']" Mode="Raw" />
                                
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


                <ext:GridPanel
                    ID="ReceiversGrid"
                    runat="server"
                   
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: receiversTab %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                  
                    ColumnLines="True" IDMode="Explicit" RenderXType="True" >

                      <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewReceiverRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{ReceiversGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                             <Store>
                                 <ext:Store ID="receiversStore" runat="server" IDMode="Explicit">
          
                                            <Model>
                                                <ext:Model  runat="server" >
                                                    <Fields>
                                                        <ext:ModelField Name="taskId" />
                                                        <ext:ModelField Name="seqNo" />
                                                        <ext:ModelField Name="receiverType" />
                                                        <ext:ModelField Name="sgId" />
                                                        <ext:ModelField Name="email" />
                                                        <ext:ModelField Name="rtName" />
                                                        <ext:ModelField Name="sgName" />
                                                        <ext:ModelField Name="languageId" />
                                                        <ext:ModelField Name="pdf" />
                                                        <ext:ModelField Name="xls" />
                                                        <ext:ModelField Name="csv" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                           <%-- <Sorters>
                                                <ext:DataSorter Property="taskId" Direction="ASC" />
              
                                            </Sorters>--%>
                                        </ext:Store>
                             </Store>

                  

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            
                             <ext:Column ID="ColRtaskId" Visible="false" DataIndex="taskId" runat="server" />   
                            <ext:Column ID="ColseqNo" Visible="false" DataIndex="seqNo" runat="server" />   

                            <ext:Column    CellCls="cellLink" ID="ColrtName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrtName%>" DataIndex="rtName" Flex="2" Hideable="false" /> 
                            <ext:Column    CellCls="cellLink" ID="ColsgName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldsgName%>" DataIndex="sgName" Flex="2" Hideable="false" />                             
                            <ext:Column    CellCls="cellLink" ID="Colemail" MenuDisabled="true" runat="server" Text="<%$ Resources: Fieldemail%>" DataIndex="email" Flex="2" Hideable="false" /> 



                            <ext:Column runat="server"
                                ID="Column5" Visible="false"
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
                                ID="Column7"  Visible="true"
                                Text=""
                                MinWidth="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return  editRender()+ '&nbsp&nbsp'+ deleteRender();" />

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
                        <CellClick OnEvent="PoPuPReceiver">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="taskId" Value="record.data['taskId']" Mode="Raw" />
                                <ext:Parameter Name="seqNo" Value="record.data['seqNo']" Mode="Raw" />
                                <ext:Parameter Name="email" Value="record.data['email']" Mode="Raw" />
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
            ID="reportsWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditReportsTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="reportsTabPaneL" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="reportsForm" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: reportsFormTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5">
                            <Items>
                                <ext:TextField  runat="server"  Name="taskId"  Hidden="true" />
                                  
                                   <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" 
                                       FieldLabel="<%$ Resources: FieldClass%>"  runat="server" DisplayField="className" ValueField="classId"   
                                       Name="classId" ID="classId"  Disabled="false" >
                                             <Store>
                                                <ext:Store runat="server" ID="ReportComboStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="className" />
                                                                <ext:ModelField Name="classId" />
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
                <ext:Button ID="saveReport" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{reportsForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewReportRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{reportsWindow}.body}" />
                            <ExtraParams>
                                <%--<ext:Parameter Name="taskId" Value="#{taskId}.getValue()" Mode="Raw" />--%>
                                <ext:Parameter Name="values" Value ="#{reportsForm}.getForm().getValues()" Mode="Raw" Encode="true" />
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
            ID="receiversWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditMessageTitle %>"
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
                            ID="receiversForm" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: receiversFormTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5">
                            <Items>

                                 <ext:TextField runat="server" ID="seqNo" Name="seqNo" Hidden="true" />
                                <ext:TextField runat="server" ID="taskId" Name="taskId" Hidden="true" />

                                <ext:ComboBox AllowBlank="false" AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" 
                                    FieldLabel="<%$ Resources: FieldrtName%>"  runat="server" DisplayField="value" ValueField="key"   Name="receiverType" ID="receiverType" >
                                             <Store>
                                                <ext:Store runat="server" ID="receiverTypeStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <DirectEvents>                                                       
                                                <Change OnEvent="EnableLang">
                                                </Change>
                                    </DirectEvents>
                                       </ext:ComboBox>

                                 <ext:ComboBox   AnyMatch="true" CaseSensitive="false" runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" 
                                     ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="sgId" Name="sgId" FieldLabel="<%$ Resources:FieldsgName%>" >
                                    <Store>
                                        <ext:Store runat="server" ID="sgStore" >
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
                                </ext:ComboBox>

                                 <ext:ComboBox AnyMatch="true" TabIndex="5" CaseSensitive="false" runat="server" ID="languageId" AllowBlank="false"  
                                    Name="languageId" DisplayField="value" ValueField="key" 
                                    SubmitValue="true"  StoreID="LanguageStore"
                                    TypeAhead="false"
                                    FieldLabel="<%$ Resources: languageName%>"/>

                                                            
                                <ext:TextArea ID="email" runat="server" FieldLabel="<%$ Resources:Fieldemail%>" Name="email" MinHeight="20" AllowBlank="true"  />

                                <ext:Checkbox FieldLabel="<%$ Resources: pdf %>" LabelWidth="150" runat="server" InputValue="True" Name="pdf" ID="pdf" Hidden="false"/>

                                <ext:Checkbox FieldLabel="<%$ Resources: xls %>" LabelWidth="150" runat="server" InputValue="True" Name="xls" ID="xls" Hidden="false"/>

                                <ext:Checkbox FieldLabel="<%$ Resources: csv %>" LabelWidth="150" runat="server" InputValue="True" Name="csv" ID="csv" Hidden="false"/>
                              
                            </Items>

                        </ext:FormPanel>

                      

                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{receiversForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewReceiverRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{receiversWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="taskId" Value="#{taskId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="seqNo" Value="#{seqNo}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{receiversForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button4" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

    </form>
</body>
</html>
