<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NationalQuotas.aspx.cs" Inherits="Web.UI.Forms.NationalQuotas" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    
    <script type="text/javascript" src="Scripts/common.js" ></script>
    <script type="text/javascript" src="Scripts/NationalQuotas.js?id=12"></script>
   <script type="text/javascript">
       function insertRecord(grid) {
           var store = grid.store,
               row = store.indexOf(store.insert(0, { days: 0, hiredPct: 0, terminatedPct : 0 })[0]);
       }
       function insertLevelAcquisitionRecord(grid) {
           var store = grid.store,
               row = store.indexOf(store.insert(0, { leName: '', pct: 0})[0]);
       }
        
        Ext.apply(Ext.form.VTypes, {
            numberrange : function(val, field) {
                if (!val) {
                    return;
                }
                
                if (field.startNumberField && (!field.numberRangeMax || (val != field.numberRangeMax))) {
                    var start = Ext.getCmp(field.startNumberField);
                    
                    if (start) {
                        start.setMaxValue(val);
                        field.numberRangeMax = val;
                        start.validate();
                    }
                } else if (field.endNumberField && (!field.numberRangeMin || (val != field.numberRangeMin))) {
                    var end = Ext.getCmp(field.endNumberField);
                    
                    if (end) {
                        end.setMinValue(val);
                        field.numberRangeMin = val;
                        end.validate();
                    }
                }
                
                return true;
            }
        });
    </script>
   
 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        
        <ext:Store
            ID="IndustryStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="IndustryStore_RefreshData"
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
                        <%--<ext:ModelField Name="intName" />--%>
                    </Fields>
                </ext:Model>
            </Model>
          <Sorters>
                <ext:DataSorter  Direction="ASC" />
            </Sorters>
        </ext:Store>
         <ext:Store
            ID="BusinessStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="BusinessStore_RefreshData"
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
                         <ext:ModelField Name="minEmployees" />
                         <ext:ModelField Name="maxEmployees" />
                        <%--<ext:ModelField Name="intName" />--%>
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>
                <ext:Store
            ID="levelStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="levelStore_RefreshData"
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
                        <ext:ModelField Name="name" />
                        <%--<ext:ModelField Name="intName" />--%>
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>

          <ext:Store
            ID="CitizenshipStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="CitizenshipStore_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model6" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />                       
                        <ext:ModelField Name="name" />
                        <ext:ModelField Name="ceiling" />
                         <ext:ModelField Name="points" />
                        <%--<ext:ModelField Name="intName" />--%>
                    </Fields>
                </ext:Model>
            </Model>
          <Sorters>
                <ext:DataSorter  Direction="ASC" />
            </Sorters>
        </ext:Store>
    
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">

            <Items>
                   <ext:TabPanel runat="server">
                    <Items>
                 <ext:GridPanel 
                    ID="IndustryGrid"
                    runat="server"
                    StoreID="IndustryStore" 
                    PaddingSpec="0 0 1 0"
                    Header="false" 
                    Title="<%$ Resources: IndustryWindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"  
                    Icon="BuildingGo"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnIndustryAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                     <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewIndustryRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{IndustryGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button ID="btnReload" runat="server"  Icon="Reload">       
                                     <Listeners>
                                        <Click Handler="CheckSession();#{IndustryStore}.reload();" />
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
                                 
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                            <ext:Column ID="industryColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column    CellCls="cellLink" ID="industryColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false" />
                           
                            
                        
                           

                         
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
                        <CellClick OnEvent="IndustryPoPuP">
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
                 <ext:GridPanel 
                    ID="BusinessGrid"
                    runat="server"
                    StoreID="BusinessStore" 
                    PaddingSpec="0 0 1 0"
                    Header="false" 
                    Title="<%$ Resources: BusinessWindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"  
                    Icon="BrickAdd"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                     <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewBusinessRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{BusinessGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button ID="Button2" runat="server"  Icon="Reload">       
                                     <Listeners>
                                        <Click Handler="CheckSession();#{BusinessStore}.reload();" />
                                    </Listeners>                           
                                   
                                </ext:Button>
                                <ext:Button Visible="false" ID="Button3" runat="server" Text="<%$ Resources:Common , DeleteAll %>" Icon="Delete">
                                 <Listeners>
                                        <Click Handler="CheckSession();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnDeleteAll">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                 <ext:TextField ID="TextField1" runat="server" EnableKeyEvents="true" Width="180" >
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                            <TriggerClick Handler="#{BusinessSizeStore}.reload();" />
                                        </Listeners>
                                    </ext:TextField>
                                 
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                            <ext:Column ID="Column1" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column    CellCls="cellLink" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Hideable="false" Flex="2"/>
                        
                            <ext:Column    CellCls="cellLink" ID="Column7" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldminEmployees%>" DataIndex="minEmployees"  Hideable="false" Flex="1"/>
                            <ext:Column    CellCls="cellLink" ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldmaxEmployees%>" DataIndex="maxEmployees"  Hideable="false" Flex="1"/>
                    
                               
                           

                         
                            <ext:Column runat="server"
                                ID="Column3" Visible="false"
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
                                ID="Column4"
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
                                ID="Column5"  Visible="true"
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

                        <ext:Toolbar ID="Toolbar4" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar2" runat="server" />
                                <ext:ToolbarFill />
                                
                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                    <BottomBar>

                        <ext:PagingToolbar ID="PagingToolbar2"
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
                        <CellClick OnEvent="BusinessPoPuP">
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
                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single"  StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
                 <ext:GridPanel 
                    ID="levelGrid"
                    runat="server"
                    StoreID="levelStore" 
                    PaddingSpec="0 0 1 0"
                    Header="false" 
                    Title="<%$ Resources: levelWindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"  
                    Icon="ChartBar"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <TopBar>
                        <ext:Toolbar ID="Toolbar5" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button4" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                     <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewLevelRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{levelGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button ID="Button6" runat="server"  Icon="Reload">       
                                     <Listeners>
                                        <Click Handler="CheckSession();#{levelStore}.reload();" />
                                    </Listeners>                           
                                   
                                </ext:Button>
                                <ext:Button Visible="false" ID="Button7" runat="server" Text="<%$ Resources:Common , DeleteAll %>" Icon="Delete">
                                 <Listeners>
                                        <Click Handler="CheckSession();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnDeleteAll">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFill2" runat="server" />
                                 <ext:TextField ID="TextField4" runat="server" EnableKeyEvents="true" Width="180" >
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                            <TriggerClick Handler="#{levelStore}.reload();" />
                                        </Listeners>
                                    </ext:TextField>
                                 
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                            <ext:Column ID="Column8" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column    CellCls="cellLink" ID="Column9" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false">
                    
                                </ext:Column>
                        
                           

                         
                            <ext:Column runat="server"
                                ID="Column10" Visible="false"
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
                                Visible="false"
                                ID="Column11"
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
                                ID="Column12"  Visible="true"
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

                        <ext:Toolbar ID="Toolbar6" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar3" runat="server" />
                                <ext:ToolbarFill />
                                
                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                    <BottomBar>

                        <ext:PagingToolbar ID="PagingToolbar3"
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
                        <CellClick OnEvent="levelPoPuP">
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
                        <ext:RowSelectionModel ID="rowSelectionModel2" runat="server" Mode="Single"  StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
                 <ext:GridPanel 
                    ID="CitizenshipGrid"
                    runat="server"
                    StoreID="CitizenshipStore" 
                    PaddingSpec="0 0 1 0"
                    Header="false" 
                    Title="<%$ Resources: CitizenshipWindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"  
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <TopBar>
                        <ext:Toolbar ID="Toolbar9" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button14" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                     <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewCitizenshipRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{CitizenshipGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button ID="Button15" runat="server"  Icon="Reload">       
                                     <Listeners>
                                        <Click Handler="CheckSession();#{CitizenshipStore}.reload();" />
                                    </Listeners>                           
                                   
                                </ext:Button>
                                <ext:Button Visible="false" ID="Button16" runat="server" Text="<%$ Resources:Common , DeleteAll %>" Icon="Delete">
                                 <Listeners>
                                        <Click Handler="CheckSession();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnDeleteAll">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFill3" runat="server" />
                                 <ext:TextField ID="TextField7" runat="server" EnableKeyEvents="true" Width="180" >
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                            <TriggerClick Handler="#{CitizenshipStore}.reload();" />
                                        </Listeners>
                                    </ext:TextField>
                                 
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel6" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                            <ext:Column ID="Column15" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column    CellCls="cellLink" ID="Column16" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false">
                    
                                </ext:Column>
                            <ext:Column    CellCls="cellLink" ID="Column23" MenuDisabled="true" runat="server" Text="<%$ Resources: ceiling%>" DataIndex="ceiling" Flex="2" Hideable="false" />
                            <ext:Column    CellCls="cellLink" ID="Column24" MenuDisabled="true" runat="server" Text="<%$ Resources: points%>" DataIndex="points" Flex="2" Hideable="false" />
                        
                           

                         
                            <ext:Column runat="server"
                                ID="Column17" Visible="false"
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
                                Visible="false"
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
                                ID="Column19"  Visible="true"
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

                        <ext:Toolbar ID="Toolbar10" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar4" runat="server" />
                                <ext:ToolbarFill />
                                
                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                    <BottomBar>

                        <ext:PagingToolbar ID="PagingToolbar4"
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
                        <CellClick OnEvent="CitizenshipPoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView6" runat="server" />
                    </View>

                  
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel5" runat="server" Mode="Single"  StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>

                 <ext:GridPanel
                            ID="PointAcquisitionGrid"
                            runat="server"
                            PaddingSpec="0 0 0 0"
                            Header="false"
                            Title="<%$ Resources: PointAcquisitionTitle %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            Icon="ImageStar"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                <ext:Store
                                    ID="PointAcquisitionStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="false"
                                    OnReadData="PointAcquisitionStore_ReadData"
                                    PageSize="50" IDMode="Explicit" Namespace="App">

                                    <Model>
                                        <ext:Model ID="Model4" runat="server"  >
                                            <Fields>

                                             
                                                <ext:ModelField Name="days" />
                                                <ext:ModelField Name="hiredPct" />
                                                 <ext:ModelField Name="terminatedPct" />
                                             
                                                

                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                 <%--  <Sorters>
                                        <ext:DataSorter Property="recordId" Direction="ASC" />
                                    </Sorters>--%>
                                </ext:Store>

                            </Store>
                         <TopBar>
                        <ext:Toolbar ID="Toolbar7" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button10" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                     <Listeners>
                                     <%--   <Click Handler="var myRecordDef = Ext.data.Record.create(['name']); App.citizenshipStore.insert(0, new myRecordDef({'name':'test'}));  " />--%>
                                        <Click Handler="App.PointAcquisitionGrid.getStore().add(new Ext.data.Record({ days: 0, hiredPct: 0.0, terminatedPct: 0.0}));" />
                                         <Click Handler="insertRecord(#{PointAcquisitionGrid})" />
                                         
                                        
                                    </Listeners>                           
                                   <%-- <DirectEvents>
                                        <Click OnEvent="ADDNewCitizenshipRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{citizenshipGrid}.body}" />
                                        </Click>
                                    </DirectEvents>--%>
                                </ext:Button>
                              
                         
                              
                                 
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                     

                            <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                  
                                   
                                  
                                    <ext:WidgetColumn ID="WidgetColumn2" Visible="true" DataIndex="days" runat="server" Text="<%$ Resources:FieldDays %>" Flex="1">
                                        <Widget>
                                            <ext:NumberField AllowBlank="false" runat="server" Name="days" MinValue="0" >
                                                <Listeners>

                                                    <Change Handler="this.getWidgetRecord().set('days',this.value); ">
                                                    </Change>
                                                </Listeners>
                                              
                                                </ext:NumberField>
                                        </Widget>
                                    </ext:WidgetColumn>
                                    <ext:WidgetColumn ID="WidgetColumn3" Visible="true" DataIndex="hiredPct" runat="server" Text="<%$ Resources: hiredPct %>" Flex="1">
                                        <Widget>
                                            <ext:NumberField  DecimalPrecision="2" runat="server" Name="hiredPct" MinValue="0" MaxValue="100" >
                                                 <Listeners>

                                                    <Change Handler="this.getWidgetRecord().set('hiredPct',this.value); ">
                                                    </Change>
                                                </Listeners>
                                           
                                                </ext:NumberField>
                                        </Widget>
                                    </ext:WidgetColumn>
                                    <ext:WidgetColumn ID="WidgetColumn4" Visible="true" DataIndex="terminatedPct" runat="server" Text="<%$ Resources: terminatedPct  %>" Flex="1">
                                        <Widget>
                                            <ext:NumberField DecimalPrecision="2" runat="server" Name="terminatedPct"  MinValue="0" MaxValue="100">
                                                 <Listeners>

                                                    <Change Handler="this.getWidgetRecord().set('terminatedPct',this.value); ">
                                                    </Change>
                                                </Listeners>
                                                <Validator Handler="return !isNaN(this.value)">

                                                </Validator>
                                                </ext:NumberField>
                                        </Widget>
                                    </ext:WidgetColumn>
                                      <ext:Column runat="server"
                                               ID="Column14"  Visible="true"
                                                Text=""
                                                Width="100"
                                                Hideable="false"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                MenuDisabled="true"
                                                Resizable="false">

                                                <Renderer handler="return deleteRender();" />

                            </ext:Column>






                                </Columns>
                            </ColumnModel>
                        <Listeners>
                           
                      <Render Handler="this.on('cellclick', cellClickPointAcquisition);" />
                        
                           
                    </Listeners>
                  <%--  <DirectEvents>
                        <CellClick OnEvent="CitizenShipPoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>--%>
                        <Buttons>
                             <ext:Button ID="SavePointAcquisitionButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                    <Click Handler="CheckSession();" />
                                </Listeners>
                                <DirectEvents>
                                    <Click OnEvent="SaveNewPointAcquisition" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{PointAcquisitionGrid}.body}" />
                                        <ExtraParams>
                                                                                   
                                              <ext:Parameter Name="codes" Value="Ext.encode(#{PointAcquisitionGrid}.getRowsValues({dirtyRowsOnly : false}))" Mode="Raw"  />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                         <%--   <ext:Button ID="Button8" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                                <Listeners>
                                    <Click Handler="this.up('window').hide();" />
                                </Listeners>
                            </ext:Button>--%>
                        </Buttons>


                            <View>
                                <ext:GridView ID="GridView4" runat="server" />
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel3" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>

                        </ext:GridPanel>
                 <ext:GridPanel
                            ID="LevelAcquisitionGrid"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            Title="<%$ Resources: LevelAcquisitionTitle %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            Icon="World"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                <ext:Store
                                    ID="LevelAcquisitionStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="false"
                                    OnReadData="LevelAcquisition_ReadData"
                                    PageSize="50" IDMode="Explicit" Namespace="App">

                                    <Model>
                                        <ext:Model ID="Model5" runat="server"  >
                                            <Fields>

                                           

                                                <ext:ModelField Name="inName" />
                                                <ext:ModelField Name="bsName" />
                                                 <ext:ModelField Name="leName" />
                                                 <ext:ModelField Name="inId" />
                                                <ext:ModelField Name="bsId" />
                                                 <ext:ModelField Name="pct" />
                                                  <ext:ModelField Name="leId" />
                                                

                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                 <%--  <Sorters>
                                        <ext:DataSorter Property="recordId" Direction="ASC" />
                                    </Sorters>--%>
                                </ext:Store>

                            </Store>
                         <TopBar>
                        <ext:Toolbar ID="Toolbar8" runat="server" ClassicButtonStyle="false">
                            <Items>
                             
                                    <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"  Width="120" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="bsId" Name="bsId"  EmptyText="<%$ Resources:FieldBusinessSize%>">
                                            <Store>
                                                <ext:Store runat="server" ID="bsIdStore">
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
                                      <ext:ComboBox    AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"  Width="120" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="inId" Name="inId"  EmptyText="<%$ Resources:FieldIndustry%>">
                                            <Store>
                                                <ext:Store runat="server" ID="inIdStore">
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
                                 <ext:Button runat="server" Text="<%$Resources:Common, Go %>">

                                            <DirectEvents>
                                                <Click OnEvent="FillLevelAcquisition"></Click>
                                            </DirectEvents>
                                        </ext:Button>
               
                              
                                 
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                     

                            <ColumnModel ID="ColumnModel5" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                  
                                   
                                  
                                        <ext:Column    CellCls="cellLink" ID="Column20" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldlevelName%>" DataIndex="leName" Flex="2" Hideable="false"/>
                                  
                                    <ext:WidgetColumn ID="WidgetColumn6" Visible="true" DataIndex="pct" runat="server" Text="<%$ Resources: pct  %>" Flex="1">
                                        <Widget>
                                            <ext:NumberField DecimalPrecision="2" runat="server" Name="pct"  MinValue="0" MaxValue="100">
                                                 <Listeners>

                                                    <Change Handler="this.getWidgetRecord().set('pct',this.value); ">
                                                    </Change>
                                                </Listeners>
                                                <Validator Handler="return !isNaN(this.value)">

                                                </Validator>
                                                </ext:NumberField>
                                        </Widget>
                                    </ext:WidgetColumn>
                                      <ext:Column runat="server" 
                                               ID="Column13"  Visible="false"
                                                Text=""
                                                Width="100"
                                                Hideable="false"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                MenuDisabled="true"
                                                Resizable="false">

                                                <Renderer handler="return deleteRender();" />

                            </ext:Column>






                                </Columns>
                            </ColumnModel>
                        <Listeners>
                           
                      <Render Handler="this.on('cellclick', cellClickLevelAcquisition);" />
                        
                           
                    </Listeners>
                  <%--  <DirectEvents>
                        <CellClick OnEvent="CitizenShipPoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>--%>
                        <Buttons>
                             <ext:Button ID="Button12" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                    <Click Handler="CheckSession();" />
                                </Listeners>
                                <DirectEvents>
                                    <Click OnEvent="SaveNewLevelAcquisition" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{LevelAcquisitionGrid}.body}" />
                                        <ExtraParams>
                                                                                   
                                              <ext:Parameter Name="codes" Value="Ext.encode(#{LevelAcquisitionGrid}.getRowsValues({dirtyRowsOnly : false}))" Mode="Raw"  />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                          <%--  <ext:Button ID="Button13" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                                <Listeners>
                                    <Click Handler="this.up('window').hide();" />
                                </Listeners>
                            </ext:Button>--%>
                        </Buttons>


                            <View>
                                <ext:GridView ID="GridView5" runat="server" />
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel4" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>

                        </ext:GridPanel>

                        </Items>
                        </ext:TabPanel>
             </Items>
        </ext:Viewport>

        

        <ext:Window 
            ID="EditIndustryWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditIndustryWindowsTitle %>"
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
                            ID="IndustryFrom" DefaultButton="SaveIndustryButton"
                            runat="server"
                            Title="<%$ Resources: IndustryInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" runat="server"  Name="recordId"  Hidden="true"/>
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name"   AllowBlank="false"/>
                                <%--<ext:TextField ID="intName" runat="server" FieldLabel="<%$ Resources:IntName%>" Name="intName"   AllowBlank="false"/>--%>
                            </Items>

                        </ext:FormPanel>
                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveIndustryButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{IndustryFrom}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewIndustry" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditIndustryWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{IndustryFrom}.getForm().getValues()" Mode="Raw" Encode="true" />
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
            ID="EditBusinessWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditBusinessWindowsTitle %>"
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
                            ID="businessForm" DefaultButton="SaveBusinessButton"
                            runat="server"
                            Title="<%$ Resources: EditBusinessWindowsTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="TextField2" runat="server"  Name="recordId"  Hidden="true"/>
                                <ext:TextField ID="TextField3" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name"   AllowBlank="false"/>
                               <ext:FieldSet Collapsible="true" runat="server" Title="<%$ Resources:EmployeeCount%>">
                                     <Items>
                                <ext:NumberField ID="minEmployees" runat="server" FieldLabel="<%$ Resources:FieldminEmployees%>" Name="minEmployees"  MinValue="0"   >
                              </ext:NumberField>
                                    
                                 <ext:NumberField ID="maxEmployees" runat="server" FieldLabel="<%$ Resources:FieldmaxEmployees%>" Name="maxEmployees"  MinValue="0"   >
                                     <Validator Handler=" return this.value>#{minEmployees}.value ; "> </Validator>
                                     </ext:NumberField>
                              </Items>
                             </ext:FieldSet>
                            </Items>

                        </ext:FormPanel>
                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveBusinessButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{businessForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewBusiness" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditBusinessWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{businessForm}.getForm().getValues()" Mode="Raw" Encode="true" />
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
            ID="EditLevelWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditLevelWindowsTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="levelForm" DefaultButton="SaveLevelButton"
                            runat="server"
                            Title="<%$ Resources: levelInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="TextField5" runat="server"  Name="recordId"  Hidden="true"/>
                                <ext:TextField ID="TextField6" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name"   AllowBlank="false"/>
                                <%--<ext:TextField ID="intName" runat="server" FieldLabel="<%$ Resources:IntName%>" Name="intName"   AllowBlank="false"/>--%>
                            </Items>

                        </ext:FormPanel>
                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveLevelButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{levelForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewLevel" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditLevelWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{levelForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button9" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
          <ext:Window 
            ID="EditCitizenshipWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditCitizenshipWindowsTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="TabPanel3" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="CitizenshipForm" DefaultButton="SaveCitizenshipButton"
                            runat="server"
                            Title="<%$ Resources: EditCitizenshipWindowsTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="TextField8" runat="server"  Name="recordId"  Hidden="true"/>
                                <ext:TextField ID="TextField9" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name"   AllowBlank="false"/>
                                 <ext:NumberField ID="points" runat="server" FieldLabel="<%$ Resources:points%>" Name="points"   AllowBlank="false" MinValue="0" MaxValue="9"/>
                                 <ext:NumberField ID="ceiling" runat="server" FieldLabel="<%$ Resources:ceiling%>" Name="ceiling"   MinValue="0" AllowBlank="false"/>
                            
                                <%--<ext:TextField ID="intName" runat="server" FieldLabel="<%$ Resources:IntName%>" Name="intName"   AllowBlank="false"/>--%>
                            </Items>

                        </ext:FormPanel>
                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button17" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{CitizenshipForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewCitizenship" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditCitizenshipWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{CitizenshipForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button18" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>




    </form>
</body>
</html>