﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Schedules.aspx.cs" Inherits="Web.UI.Forms.Schedules" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript" src="Scripts/Schedules.js?id=58"></script>
     <script type="text/javascript" src="Scripts/common.js?id=32"></script>
   
   
   
  

    <script type="text/javascript">
       
        function openEmployeesPage(scId) {
        var url = 'Employees.aspx?scId='.concat(scId);
            parent.App.tabPanel.add({
                id: "rootParent_Employee_Leaf",
                title: App.EmployeeLeafTitle.getValue(),
                iconCls: "icon-Employees",
                closable: true,
                loader: {
                    url: url,
                    renderer: "frame",
                    loadMask: {
                        showMask: true,
                        msg: "test"
                    }
                }
            });

            var tab = parent.App.tabPanel.getComponent("rootParent_Employee_Leaf");
            parent.App.tabPanel.setActiveTab(tab);
            }
            
        function setReadOnly(attr, state) {











            Ext.getCmp(attr).setDisabled(!state);

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
        <ext:Hidden ID="SundayText" runat="server" Text="<%$ Resources:Common , SundayText %>" />
        <ext:Hidden ID="MondayText" runat="server" Text="<%$ Resources:Common , MondayText %>" />
        <ext:Hidden ID="TuesdayText" runat="server" Text="<%$ Resources:Common , TuesdayText %>" />
        <ext:Hidden ID="WednesdayText" runat="server" Text="<%$ Resources:Common , WednesdayText %>" />
        <ext:Hidden ID="ThursdayText" runat="server" Text="<%$ Resources:Common , ThursdayText %>" />
        <ext:Hidden ID="FridayText" runat="server" Text="<%$ Resources:Common , FridayText %>" />
        <ext:Hidden ID="SaturdayText" runat="server" Text="<%$ Resources:Common , SaturdayText %>" />
        <ext:Hidden ID="CurrentSchedule" runat="server" />
        <ext:Hidden ID="CurrentDow" runat="server" />
        <ext:Hidden ID="IsWorkingDay" runat="server" />
        <ext:Hidden ID="editDisabled" runat="server" />
        <ext:Hidden ID="deleteDisabled" runat="server" />
          <ext:Hidden ID="CurrentScId" runat="server" />
           <ext:Hidden ID="EmployeeLeafTitle" runat="server" Text="<%$ Resources:Common , EmployeeLeaf %>" />
      

        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App">
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
                        <ext:ModelField Name="fci_min_ot" />
                        <ext:ModelField Name="fci_max_lt" />
                        <ext:ModelField Name="lco_max_el" />
                        <ext:ModelField Name="lco_min_ot" />
                        <ext:ModelField Name="lco_max_ot" />
                        <ext:ModelField Name="b_min_ot" />
                        <ext:ModelField Name="b_max_ot" />





                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
                <ext:DataSorter Property="name" Direction="ASC" />
                <ext:DataSorter Property="reference" Direction="ASC" />
            </Sorters>
        </ext:Store>



        <ext:Viewport ID="Viewport1" runat="server" Layout="CardLayout" ActiveIndex="0">
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

                                <ext:Button ID="Button5" runat="server" Text="<%$ Resources:Common , Employee %>" >
                                     <Listeners>
                                                        <Click Handler=" if (App.GridPanel1.getSelectionModel().hasSelection()) {
                                                               var row = App.GridPanel1.getSelectionModel().getSelection()[0];
                                                                App.CurrentScId.setValue(row.get('recordId'));
                                                            }
                                                            else
                                                            App.CurrentScId.setValue('0');
                                                          
                                                            " />
                                                                          </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ShowEmployees">
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

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />

                            <ext:Column CellCls="cellLink" Sortable="true" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false">
                            </ext:Column>

                            <ext:Column ID="Colfci_min_ot" MenuDisabled="true" runat="server" Text="<%$ Resources: Fieldfci_min_ot%>" DataIndex="fci_min_ot" Hideable="false" Width="75" Align="Center" Flex="1" Visible="false" />
                            <ext:Column ID="Colfci_max_lt" CellWrap="true" ShrinkWrap="Height" MenuDisabled="true" runat="server" Text="<%$ Resources: Fieldfci_max_lt%>" DataIndex="fci_max_lt" Hideable="false" Width="75" Align="Center" Flex="1" Visible="false" />
                            <ext:Column ID="Collco_max_el" CellWrap="true" MenuDisabled="true" runat="server" Text="<%$ Resources: Fieldlco_max_el%>" DataIndex="lco_max_el" Hideable="false" Width="75" Align="Center" Flex="1" Visible="false" />
                            <ext:Column ID="Collco_min_ot" MenuDisabled="true" runat="server" Text="<%$ Resources: Fieldlco_min_ot%>" DataIndex="lco_min_ot" Hideable="false" Width="75" Align="Center" Flex="1" Visible="false" />
                            <ext:Column ID="Collco_max_ot" MenuDisabled="true" runat="server" Text="<%$ Resources: Fieldlco_max_ot%>" DataIndex="lco_max_ot" Hideable="false" Width="75" Align="Center" Flex="1" Visible="false" />
                            <ext:Column ID="Colb_min_ot" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEd_min_ot%>" DataIndex="b_min_ot" Hideable="false" Width="75" Align="Center" Flex="1" Visible="false" />
                            <ext:Column ID="Colb_max_ot" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEd_max_ot%>" DataIndex="b_max_ot" Hideable="false" Width="75" Align="Center" Flex="1" Visible="false" />


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
                                ID="colDelete" Flex="1" Visible="false"
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
                            <ext:Column runat="server"
                                ID="colDetails"
                                Text="<%$ Resources:EditDaysButton %>"
                                Hideable="false"
                                Width="120"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;'+attachRender()+'&nbsp;&nbsp;'+deleteRender(); " />
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
                                <ext:Parameter Name="ScheduleNamePar" Value="record.data['name']" Mode="Raw" />
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

                <ext:GridPanel runat="server" Title="<%$ Resources: WindowTitle %>" Header="false" ID="scheduleDays">
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPDay">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <Store>
                        <ext:Store ID="scheduleStore" runat="server">
                            <Model>
                                <ext:Model runat="server" IDProperty="dow">
                                    <Fields>
                                        <ext:ModelField Name="dow" />
                                        <ext:ModelField Name="firstIn" />
                                        <ext:ModelField Name="lastOut" />
                                        <ext:ModelField Name="dayTypeId" />
                                        <ext:ModelField Name="durationFormatted" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="Prev_Click">
                                            <ExtraParams>
                                                <ext:Parameter Name="index" Value="#{viewport1}.items.indexOf(#{viewport1}.layout.activeItem)" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="patternButton" runat="server" Text="<%$ Resources:ApplyPattern %>" Icon="Help" Hidden="true">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="selectPattern_click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator runat="server" />
                                   <ext:Button runat="server" Text="<%$Resources: Batch %>" >
                                    <Listeners>
                                        <Click Handler="CheckSession();" />

                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="batchClicked" ></Click>
                                    </DirectEvents>
                                    </ext:Button>
                                <ext:Label ID="ScheduleNamelb" runat="server" />
                             
                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <ColumnModel>
                        <Columns>
                            <ext:Column runat="server" ID="colDayName" Text="<%$ Resources: FieldDow %>" DataIndex="dow" Flex="1">
                                <Renderer Handler="return getDay(record.data['dow'])" />
                            </ext:Column>
                            <ext:Column runat="server" ID="firstInCol" Text="<%$ Resources: FieldFirstIn %>" DataIndex="firstIn" Width="100" />
                            <ext:Column runat="server" ID="lastOutCol" Text="<%$ Resources: FieldLastOut %>" DataIndex="lastOut" Width="100" />
                            <ext:Column runat="server" ID="Column2" Text="<%$ Resources: FieldDuration %>" DataIndex="durationFormatted" Width="100" />
                            <ext:Column runat="server"
                                ID="Column1" Visible="true"
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
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
                 <ext:GridPanel
                    ID="EmployeesGrid"
                    runat="server"
                   
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: EmployeesGrid %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                         <Store>
                        <ext:Store
                            ID="EmployeesStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true" >
                    
                        
                            <Model>
                                <ext:Model ID="Model2" runat="server" IDProperty="recordId">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="pictureUrl" />
                                        <ext:ModelField Name="name" IsComplex="true" />
                                        <ext:ModelField Name="reference" />
                                        <ext:ModelField Name="departmentName" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="divisionName" />
                                        <ext:ModelField Name="hireDate" />






                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>

                   <TopBar>
                        <ext:Toolbar ID="Toolbar4" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button6" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
                                    <Listeners>
                                        <Click Handler="CheckSession(); #{Viewport1}.layout.setActiveItem(0);" />
                                    </Listeners>
                                  <%--  <DirectEvents>
                                        <Click OnEvent="Prev_Click">
                                            <ExtraParams>
                                                <ext:Parameter Name="index" Value="#{viewport1}.layout.activeItem.setValue(0)" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>--%>
                                </ext:Button>
                               
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>

                            

                            <ext:Column ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name" Flex="3" Hideable="false">
                                <Renderer Handler=" return  record.data['name'].fullName ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column ID="ColDepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="2" Hideable="false">
                            </ext:Column>
                            <ext:Column ID="ColPositionName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="2" Hideable="false" />
                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="2" Hideable="false" />


                          <%--  <ext:Column runat="server"
                                ID="Column12" Visible="false"
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
                                ID="Column13" Flex="1" Visible="false"
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
                                ID="Column14"
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
                                ID="Column15"
                                Text="<%$ Resources:EditDaysButton %>"
                                Hideable="false"
                                Width="120"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;'+attachRender()+'&nbsp;&nbsp;'+deleteRender(); " />
                            </ext:Column>--%>


                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar5" runat="server" Dock="Bottom">
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
                 <%--   <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="ScheduleNamePar" Value="record.data['name']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>--%>
                    <View>
                        <ext:GridView ID="GridView2" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
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
            Height="200"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Listeners>
                        <TabChange Fn="App.direct.panelRecordDetails_TabChanged">
                        </TabChange>
                    </Listeners>
                    <Items>
                        <ext:FormPanel DefaultButton="SaveButton"
                            ID="BasicInfoTab"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" Hidden="true" runat="server" Disabled="true" DataIndex="recordId" />
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" DataIndex="name" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>" />

                                <ext:FieldSet runat="server" Title="<%$Resources:Lateness %>" Collapsible="true" Visible="false">
                                    <Items>

                                        <ext:TextField LabelWidth="300" Width="360" ID="fci_max_lt" runat="server" FieldLabel="<%$ Resources:Fieldfci_max_lt%>" DataIndex="fci_max_lt" Name="fci_max_lt" EmptyText="0">
                                            <Validator Handler="return this.value=='' || (!isNaN(this.value)&& this.value>=0);" />
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="300" Width="360" ID="lco_max_el" runat="server" FieldLabel="<%$ Resources:Fieldlco_max_el%>" DataIndex="lco_max_el" Name="lco_max_el" EmptyText="0">
                                            <Validator Handler="return this.value=='' || (!isNaN(this.value)&& this.value>=0);" />
                                        </ext:TextField>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet runat="server" Title="<%$Resources:ArrivalOvertime %>" Collapsible="true" Visible="false">
                                    <Items>
                                        <ext:Checkbox LabelWidth="300" runat="server" ID="enableAOT" Name="enableAOT" InputValue="true" FieldLabel="<%$ Resources:EnableAOT %>">
                                            <Listeners>
                                                <AfterRender Handler="this.next().setDisabled(!this.value);" />
                                                <Change Handler="this.next().setDisabled(!this.value);" />
                                            </Listeners>
                                        </ext:Checkbox>
                                        <ext:TextField LabelWidth="300" Width="360" ID="fci_min_ot" runat="server" FieldLabel="<%$ Resources:Fieldfci_min_ot%>" DataIndex="fci_min_ot" Name="fci_min_ot" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0" />
                                        </ext:TextField>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet runat="server" Title="<%$Resources:EntryDayOverTime %>" Collapsible="true" Visible="false">
                                    <Items>
                                        <ext:Checkbox LabelWidth="300" runat="server" ID="enableEntryDayCheckbox" Name="enableBOT" InputValue="true" FieldLabel="<%$ Resources:EnableEntryDayOverTime %>">
                                            <Listeners>
                                                <AfterRender Handler="this.next().setDisabled(!this.value); this.next().next().setDisabled(!this.value);" />
                                                <Change Handler="this.next().setDisabled(!this.value); this.next().next().setDisabled(!this.value);" />
                                            </Listeners>
                                        </ext:Checkbox>
                                        <ext:TextField LabelWidth="300" Width="360" ID="minEntryDayTextField" runat="server" FieldLabel="<%$ Resources:FieldEd_min_ot%>" Name="b_min_ot" EmptyText="0" DataIndex="b_min_ot">
                                            <Validator Handler="return this.value=='' || (!isNaN(this.value)&& this.value>=0);" />
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="300" Width="360" ID="maxEntryDayTextField" runat="server" FieldLabel="<%$ Resources:FieldEd_max_ot%>" Name="b_max_ot" DataIndex="b_max_ot" EmptyText="0">
                                            <Validator Handler="return this.value=='' || (!isNaN(this.value)&& this.value>=0);" />
                                        </ext:TextField>
                                    </Items>
                                </ext:FieldSet>


                                <ext:FieldSet runat="server" Title="<%$Resources:DepartureOvertime %>" Collapsible="true" Visible="false">
                                    <Items>
                                        <ext:Checkbox LabelWidth="300" runat="server" ID="enableDOT" Name="enableDOT" InputValue="true" FieldLabel="<%$ Resources:EnableDOT %>">
                                            <Listeners>
                                                <AfterRender Handler="this.next().setDisabled(!this.value); this.next().next().setDisabled(!this.value);" />
                                                <Change Handler="this.next().setDisabled(!this.value); this.next().next().setDisabled(!this.value);" />
                                            </Listeners>
                                        </ext:Checkbox>
                                        <ext:TextField LabelWidth="300" Width="360" ID="lco_min_ot" runat="server" FieldLabel="<%$ Resources:Fieldlco_min_ot%>" Name="lco_min_ot" EmptyText="0">
                                            <Validator Handler="return this.value=='' || (!isNaN(this.value)&& this.value>=0);" />
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="300" Width="360" ID="lco_max_ot" runat="server" FieldLabel="<%$ Resources:Fieldlco_max_ot%>" Name="lco_max_ot" EmptyText="0">
                                            <Validator Handler="return this.value=='' || (!isNaN(this.value)&& this.value>=0);" />
                                        </ext:TextField>
                                    </Items>
                                </ext:FieldSet>


                            </Items>

                        </ext:FormPanel>


                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="schedule" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />

                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            <%--    <ext:Button ID="setDefaultBtn" runat="server" Text="<%$ Resources:SetDefault %>" Hidden="true">
                    <Listeners>
                        <Click Handler="CheckSession();"></Click>
                    </Listeners>
                    <DirectEvents>

                        <Click OnEvent="SetDefaultClick">
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>--%>
            </Buttons>
        </ext:Window>

        <ext:Window
            ID="EditDayBreaks"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:DayBreaksForm %>"
            Width="450"
            Height="370"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">

                    <Items>
                        <ext:FormPanel DefaultButton="Button3"
                            ID="dayBreaksForm"
                            runat="server"
                            Title="<%$ Resources:DayBreaksForm %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="isBatch" runat="server" Hidden="true" />
                                <ext:TextField ID="fieldScId" Hidden="true" runat="server" Disabled="true" DataIndex="scId" />
                                <ext:TextField ID="fieldDow" Hidden="true" runat="server" Disabled="true" DataIndex="dow" />
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" AllowBlank="false" DisplayField="name" ValueField="recordId" Name="dayTypeId" ID="dayTypeId" FieldLabel="<%$ Resources:DayType %>" SubmitValue="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Store>
                                        <ext:Store runat="server" ID="dayTypesStore">
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
                                    <DirectEvents>
                                        <Change OnEvent="Unnamed_Event" />
                                    </DirectEvents>
                                    <Listeners>
                                        <Change Handler="CheckSession();" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:TextField ID="firstIn" FieldLabel="<%$Resources: FieldFirstIn %>" runat="server" Name="firstIn" DataIndex="firstIn" AllowBlank="false">
                                    <%--       <Plugins>
                                        <ext:InputMask Mask="99:99" />

                                    </Plugins>--%>
                                    <Validator Handler="return validateFrom(this.getValue()); " />
                                    <Listeners>
                                        <Change Handler="App.lastOut.validate();" />
                                    </Listeners>
                                </ext:TextField>
                                
                                <ext:TextField ID="lastOut" runat="server" FieldLabel="<%$Resources: FieldLastOut %>" Name="lastOut" DataIndex="lastOut" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>">
                                    <%--<Plugins>
                                        <ext:InputMask Mask="99:99" />
                                    </Plugins>--%>
                                    <Validator Handler="return validateTo(this.getValue(),this.prev().getValue());" />
                                </ext:TextField>

                                <ext:GridPanel
                                    ID="periodsGrid"
                                    runat="server" Scroll="Vertical"
                                    Width="600" Header="false"
                                    Height="170 " Layout="FitLayout"
                                    Frame="true" TitleCollapse="true">
                                    <Store>
                                        <ext:Store ID="periodsStore" runat="server">
                                            <Model>
                                                <ext:Model runat="server" Name="Employee">
                                                    <Fields>
                                                        <ext:ModelField Name="name" />
                                                        <ext:ModelField Name="start" />
                                                        <ext:ModelField Name="end" />

                                                     

                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Plugins>
                                        <ext:RowEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" SaveBtnText="<%$ Resources:Common , Save %>" CancelBtnText="<%$ Resources:Common , Cancel %>" ErrorsText="" ValidateRequestMode="Enabled">
                                            <Listeners>
                                                <BeforeEdit Handler="if (App.editDisabled.value == '1')
        return false;" />
                                            </Listeners>
                                        </ext:RowEditing>
                                    </Plugins>
                                    <TopBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:Button runat="server" Text="<%$ Resources:BtnAddPeriod %>" Icon="UserAdd" ID="AddBreakButton">
                                                    <Listeners>
                                                        <Click Fn="addBreak" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button
                                                    ID="Button2"
                                                    runat="server"
                                                    Text="<%$ Resources:BtnRemovePeriod %>"
                                                    Icon="UserDelete"
                                                    Disabled="true">
                                                    <Listeners>
                                                        <Click Fn="removeBreak" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:RowNumbererColumn runat="server" Width="25" />
                                            <ext:Column runat="server"
                                                Text="<%$ Resources:Break %>"
                                                DataIndex="name"
                                                Align="Center">
                                                <Editor>
                                                    <ext:TextField ID="breakNameField" AllowBlank="false" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column
                                                runat="server"
                                                Text="<%$ Resources:FieldFrom %>"
                                                DataIndex="start"
                                                Align="Center">
                                                <Editor>
                                                    <%-- Vtype="numberrange"
                                                        EndNumberField="toField"--%>
                                                    <ext:TextField runat="server" ID="fromField" InvalidText="<%$ Resources:Common,TimeFieldError %>">

                                                        <%--       <Plugins>
                                                            <ext:InputMask Mask="99:99"></ext:InputMask>
                                                        </Plugins>--%>
                                                        <Validator Handler="return validateFrom(this.getValue());"></Validator>

                                                    </ext:TextField>
                                                </Editor>
                                                <%--<Renderer Handler="if(isValidDate(record.data['start'])){var dt = new Date(record.data['start']); var dtString = moment(dt).format('HH:mm'); return dtString; } else return record.data['start']; ">--%>
                                                <%--</Renderer>--%>
                                            </ext:Column>
                                            <ext:Column
                                                runat="server"
                                                Text="<%$ Resources:FieldTo %>"
                                                DataIndex="end"
                                                Align="Center">
                                                <Editor>
                                                    <%--   StartNumberField="fromField"
                                                         Vtype="numberrange"--%>
                                                    <ext:TextField
                                                        runat="server"
                                                        ID="toField" InvalidText="<%$ Resources:Common,TimeFieldError %>"
                                                        AllowBlank="false">
                                                        <%--   <Plugins>
                                                            <ext:InputMask runat="server" Mask="99:99">
                                                            </ext:InputMask>

                                                        </Plugins>--%>
                                                        <Validator Handler="return validateTo(this.getValue(),this.prev().getValue());" />

                                                    </ext:TextField>
                                                </Editor>
                                                <%--<Renderer Handler="if(isValidDate(record.data['end'])){var dt = new Date(record.data['end']); var dtString = moment(dt).format('HH:mm'); return dtString;} else return record.data['end']; "/>--%>
                                            </ext:Column>
                                          
                                            <ext:Column runat="server" Visible="false"></ext:Column>


                                        </Columns>
                                    </ColumnModel>
                                    <Listeners>
                                        <SelectionChange Handler="if (App.deleteDisabled.value == '1')
        return; App.Button2.setDisabled(!selected.length);" />
                                    </Listeners>
                                </ext:GridPanel>
                            </Items>

                        </ext:FormPanel>


                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{dayBreaksForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveDayBreaks" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="scId" Value="#{fieldScId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="dow" Value="#{fieldDow}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="day" Value="#{dayBreaksForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                                <ext:Parameter Name="breaks" Value="Ext.encode(#{periodsGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw" />
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
        <ext:Window
            ID="patternWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:PatternWindowTitle %>"
            Width="500"
            Height="470"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel DefaultButton="Button8"
                    ID="patternFormPanel"
                    runat="server"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" AllowBlank="false" MinChars="1" FieldLabel="<%$ Resources: FieldFirstDayOfWeek %>" Name="fdow" runat="server" ID="fdowCombo">
                            <Items>
                                <ext:ListItem Text="<%$ Resources:Common, MondayText %>" Value="1" />
                                <ext:ListItem Text="<%$ Resources:Common, TuesdayText %>" Value="2" />
                                <ext:ListItem Text="<%$ Resources:Common, WednesdayText %>" Value="3" />
                                <ext:ListItem Text="<%$ Resources:Common, ThursdayText %>" Value="4" />
                                <ext:ListItem Text="<%$ Resources:Common, FridayText %>" Value="5" />
                                <ext:ListItem Text="<%$ Resources:Common, SaturdayText %>" Value="6" />
                                <ext:ListItem Text="<%$ Resources:Common, SundayText %>" Value="7" />
                            </Items>
                        </ext:ComboBox>
                        <ext:TextField ID="TextField1" FieldLabel="<%$ Resources: WorkingTimeFrom %>" runat="server" DataIndex="timeFrom" Name="timeFrom" AllowBlank="false">
                            <Plugins>
                                <ext:InputMask Mask="99:99" />

                            </Plugins>
                            <Validator Handler="return validateFrom(this.getValue());" />
                        </ext:TextField>
                        <ext:TextField ID="TextField2" runat="server" FieldLabel="<%$ Resources: WorkingTimeTo %>" DataIndex="timeTo" Name="timeTo" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>">
                            <Plugins>
                                <ext:InputMask Mask="99:99" />
                            </Plugins>
                            <Validator Handler="return validateTo(this.getValue(),this.prev().getValue());" />
                        </ext:TextField>
                        <ext:TextField ID="TextField3" FieldLabel="<%$ Resources: BreakTimeFrom %>" runat="server" DataIndex="breakFrom" Name="breakFrom" AllowBlank="false">
                            <Plugins>
                                <ext:InputMask Mask="99:99" />

                            </Plugins>
                            <Validator Handler="return validateFrom(this.getValue());" />
                        </ext:TextField>
                        <ext:TextField ID="TextField4" runat="server" FieldLabel="<%$ Resources: BreakTimeTo %>" DataIndex="breakTo" Name="breakTo" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>">
                            <Plugins>
                                <ext:InputMask Mask="99:99" />
                            </Plugins>
                            <Validator Handler="return validateTo(this.getValue(),this.prev().getValue());" />
                        </ext:TextField>
                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" ForceSelection="true" AllowBlank="false" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldworkingDays %>" Name="workingDays" runat="server" ID="ComboBox1">
                            <Items>

                                <ext:ListItem Text="5" Value="5" />
                                <ext:ListItem Text="6" Value="6" />
                                <ext:ListItem Text="7" Value="7" />
                            </Items>
                        </ext:ComboBox>
                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" ForceSelection="true" AllowBlank="false" TypeAhead="true" MinChars="1" DisplayField="name" ValueField="recordId" ID="workingDayTypeId" FieldLabel="<%$ Resources:WorkingDayType %>" SubmitValue="true">
                            <Store>
                                <ext:Store runat="server" ID="workDTStore">
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
                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" ForceSelection="true" AllowBlank="false" TypeAhead="true" MinChars="1" DisplayField="name" ValueField="recordId" ID="weekendDayTypeId" FieldLabel="<%$ Resources:WeekendDayType %>" SubmitValue="true">
                            <Store>
                                <ext:Store runat="server" ID="WeekendDTStore">
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
                    </Items>

                </ext:FormPanel>


            </Items>

            <Buttons>
                <ext:Button ID="Button8" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{patternFormPanel}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SavePattern" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{patternFormPanel}.body}" />
                            <ExtraParams>


                                <ext:Parameter Name="pattern" Value="#{patternFormPanel}.getForm().getValues()" Mode="Raw" Encode="true" />

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
    </form>
</body>
</html>


