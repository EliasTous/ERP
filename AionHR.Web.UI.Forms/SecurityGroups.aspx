<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecurityGroups.aspx.cs" Inherits="AionHR.Web.UI.Forms.SecurityGroups" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <script src="Scripts/jquery-new.js"></script>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />

    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script src="Scripts/SecurityGroups.js?id=3" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript">
        function ResetPropertiesAccessLevels() {
            App.propertiesStore.each(function (record) {

                record.set("accessLevel", Math.min(2, App.CurrentClassLevel.value));
                record.commit();
            });
        }
        function getAccessLevelText(index) {
            return document.getElementById("accessLevel" + index).value;
        }
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";


            }
            return out;
        }
        function show() {
            
            var fromStore = App.usersStore;
            var toStore = App.userSelector.toField.getStore();


            for (var i = 0; i < fromStore.getCount() ; i++) {
                var s = fromStore.getAt(i);
                toStore.add(s);

                var d = App.userSelector.fromField.getStore().getById(s.getId());

                if (d != null) {
                    App.userSelector.fromField.getStore().remove(d);

                }
            }



        }
        function AddSource(items) {
           
            var fromStore = App.userSelector.fromField.getStore();
            var toStore = App.userSelector.toField.getStore();

            while (fromStore.getCount() > 0)
                fromStore.removeAt(0);



            for (i = 0; i < items.length ; i++) {
                if (fromStore.getById(items[i].userId) == null && toStore.getById(items[i].userId) == null) {

                    fromStore.add(items[i]);
                }

            }
        }
        function SwapRTL()
        {
            if(document.getElementById("isRTL").value=='1')
            {
                
                $(".x-form-itemselector-add").css('background-image', 'url(ux/resources/images/itemselector/left-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(ux/resources/images/itemselector/right-gif/ext.axd)');
               
            }
           else {
                
                $(".x-form-itemselector-add").css('background-image', 'url(ux/resources/images/itemselector/right-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(ux/resources/images/itemselector/left-gif/ext.axd)');

            }
        }
        function clearDirty()
        {
            App.GridPanel1.getStore().each(function (record) {

                record.commit();
            });
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
        <ext:Hidden ID="CurrentGroup" runat="server" />
        <ext:Hidden ID="CurrentModule" runat="server" />
        <ext:Hidden ID="CurrentClass" runat="server" />
        <ext:Hidden ID="CurrentClassLevel" runat="server" />
        <ext:Hidden ID="isRTL" runat="server" />
        <ext:Hidden ID="accessLevel0" Text="<%$ Resources: NoAccess %>" runat="server" />
        <ext:Hidden ID="accessLevel1" Text="<%$ Resources: Read %>" runat="server" />
        <ext:Hidden ID="accessLevel2" Text="<%$ Resources: WriteClass %>" runat="server" />
        <ext:Hidden ID="accessLevel3" Text="<%$ Resources: FullControl %>" runat="server" />
         <ext:Hidden ID="daSaveTitle" Text="<%$ Resources: DaSaveTitle %>" runat="server" />
          <ext:Hidden ID="daSaveText" Text="<%$ Resources: DaSaveText %>" runat="server" />
        <ext:Hidden ID="YesText" Text="<%$ Resources: Common,Yes %>" runat="server" />
        <ext:Hidden ID="NoText" Text="<%$ Resources: Common,No %>" runat="server" />
        <ext:Store
            ID="groupsStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="groupsStore_RefreshData"
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
                        <ext:ModelField Name="description" />
                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>
        <ext:Store runat="server" ID="accessLevelsStore">
            <Model>
                <ext:Model runat="server">
                    <Fields>
                        <ext:ModelField Name="text" />
                        <ext:ModelField Name="value" />
                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>
        <ext:Store runat="server" ID="classAccessLevelsStore">
            <Model>
                <ext:Model runat="server">
                    <Fields>
                        <ext:ModelField Name="text" />
                        <ext:ModelField Name="value" />
                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>


        <ext:Viewport ID="Viewport1" runat="server" Layout="CardLayout" ActiveIndex="0">
            <Items>
                <ext:GridPanel
                    ID="groupsGrid"
                    runat="server"
                    StoreID="groupsStore"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: Groups %>"
                    Layout="FitLayout"
                    Scroll="Vertical" MinHeight="500"
                    Border="false" SortableColumns="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button ID="groupAddButton" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewGroup">
                                            <EventMask ShowMask="true" CustomTarget="={#{groupsGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                  <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button ID="btnReload" runat="server"  Icon="Reload">       
                                     <Listeners>
                                        <Click Handler="CheckSession();#{groupsStore}.reload();" />
                                    </Listeners>                           
                                   
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>


                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>



                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false">
                            </ext:Column>
                            <ext:Column ID="ColReference" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDescription %>" DataIndex="description" Hideable="false" Width="150" Align="Center" />




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

                                <Renderer Handler="return  editRender()+'&nbsp;&nbsp;' +deleteRender(); " />
                            </ext:Column>

                        </Columns>
                    </ColumnModel>

                    <BottomBar>
                    </BottomBar>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPGroup">
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

                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>


            </Items>
        </ext:Viewport>

        <ext:Window
            ID="GroupWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditGroup %>"
            Width="600"
            Height="500"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="GroupForm" DefaultButton="SaveButton"
                            runat="server"
                            Icon="ApplicationSideList" Title="<%$ Resources:GroupInfo%>"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField LabelWidth="150" ID="recordId" Hidden="true" runat="server" Disabled="true" Name="recordId" />
                                <ext:TextField LabelWidth="150" ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextArea ID="description" LabelWidth="150" runat="server" FieldLabel="<%$ Resources:FieldDescription%>" Name="description" />
                              

                            </Items>
                            <Buttons>
                                <ext:Button ID="SaveGroupButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{GroupForm}.getForm().isValid()) {return false;} " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveGroup" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{GroupWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="values" Value="#{GroupForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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
                        </ext:FormPanel>

                        <ext:GridPanel
                            ID="usersGrid"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            Title="<%$ Resources: Users %>"
                            Layout="FitLayout"
                            Scroll="Vertical" MinHeight="500"
                            Border="false" SortableColumns="false"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                <ext:Store
                                    ID="usersStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="true"
                                    AutoLoad="false"
                                    OnReadData="usersStore_RefreshData"
                                    PageSize="30" IDMode="Explicit" Namespace="App">
                                    <Proxy>
                                        <ext:PageProxy>
                                            <Listeners>
                                                <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                            </Listeners>
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="Model2" runat="server" IDProperty="userId">
                                            <Fields>

                                                <ext:ModelField Name="userId" />
                                                <ext:ModelField Name="sgId" />
                                                <ext:ModelField Name="fullName" />
                                                 <ext:ModelField Name="email" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>

                                </ext:Store>
                            </Store>
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>

                                        <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                            <Listeners>
                                                <Click Handler="CheckSession();" />
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="ADDUsers">
                                                    <EventMask ShowMask="true" CustomTarget="={#{usersGrid}.body}" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>

                                    </Items>
                                </ext:Toolbar>
                            </TopBar>


                            <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                                <Columns>



                                    <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                                    <ext:Column CellCls="cellLink" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="fullName" Flex="1" Hideable="false">

                                    </ext:Column>
                                     <ext:Column Sortable="true" ID="ColEmail" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmail%>" DataIndex="email" Flex="2" Hideable="false"  >
                                         </ext:Column>
                             




                                    <ext:Column runat="server"
                                        ID="Column4" Visible="true"
                                        Text=""
                                        Width="100"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="return deleteRender(); " />
                                    </ext:Column>

                                </Columns>
                            </ColumnModel>

                            <BottomBar>
                            </BottomBar>
                            <Listeners>
                                <Render Handler="this.on('cellclick', cellClick);" />
                            </Listeners>
                            <DirectEvents>
                                <CellClick OnEvent="PoPuPUser">
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
                            <DockedItems>

                                <ext:Toolbar ID="Toolbar1" runat="server" Dock="Bottom">
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

                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                        </ext:GridPanel>

                        <ext:FormPanel runat="server" ID="AccessLevelsForm" Layout="FitLayout" Title="<%$ Resources:AccessLevels%>">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" Editable="false" ID="modulesCombo" ValueField="id" DisplayField="name" FieldLabel="<%$ Resources:SelectModule%>">
                                            <Listeners>
                                                
                                                <Select Handler="App.CurrentModule.value = this.value; App.classesStore.reload(); " />
                                            </Listeners>
                                            <Items>
                                                 <ext:ListItem Text="<%$ Resources: Common,Mod10  %>" Value="10" />
                                                <ext:ListItem Text="<%$ Resources: Common,Mod20  %>" Value="20" />
                                                <ext:ListItem Text="<%$ Resources:  Common,Mod21  %>" Value="21" />
                                              <%--  <ext:ListItem Text="<%$ Resources:  Common,Mod22  %>" Value="22" />--%>
                                         <%--       <ext:ListItem Text="<%$ Resources:  Common,Mod23 %>" Value="23" />--%>
                                          <%--      <ext:ListItem Text="<%$ Resources:  Common,Mod24  %>" Value="24" />--%>
                                               <ext:ListItem Text="<%$ Resources:  Common,Mod25  %>" Value="25" />
                                              <%--  <ext:ListItem Text="<%$ Resources: Common,Mod30  %>" Value="30" />--%>
                                                <ext:ListItem Text="<%$ Resources:  Common,Mod31 %>" Value="31" />
                                              <%--  <ext:ListItem Text="<%$ Resources:  Common,Mod32  %>" Value="32" />--%>
                                                <ext:ListItem Text="<%$ Resources: Common,Mod41  %>" Value="41" />
                                                <ext:ListItem Text="<%$ Resources:  Common,Mod42  %>" Value="42" />
                                            <%--    <ext:ListItem Text="<%$ Resources:  Common,Mod43  %>" Value="43" />--%>
                                              <%--  <ext:ListItem Text="<%$ Resources:  Common,Mod44 %>" Value="44" />--%>
                                                <ext:ListItem Text="<%$ Resources:  Common,Mod45  %>" Value="45" />
                                                <ext:ListItem Text="<%$ Resources:  Common,Mod51  %>" Value="51" />
                                                 <ext:ListItem Text="<%$ Resources: Common,Mod60  %>" Value="60" />
                                                  <ext:ListItem Text="<%$ Resources: Common,Mod70  %>" Value="70" />
                                                <ext:ListItem Text="<%$ Resources: Common,Mod80  %>" Value="80" />
                                                <ext:ListItem Text="<%$ Resources: Common,Mod90  %>" Value="90" />


                                            </Items>

                                        </ext:ComboBox>

                                        <ext:Button runat="server" Icon="ApplicationSideExpand" ToolTip="<%$ Resources:ApplyModule%>" ID="openModuleLevelForm">
                                            <Listeners>
                                                <Click Handler="App.ApplyModuleLevelWindow.show();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>

                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:GridPanel
                                    ID="classesGrid"
                                    runat="server"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User" 
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                                    <Store>
                                        <ext:Store runat="server" ID="classesStore" OnReadData="classesStore_ReadData" AutoLoad="true">
                                            <Model>
                                                <ext:Model runat="server" IDProperty="id">
                                                    <Fields>
                                                        <ext:ModelField Name="id" />
                                                        <ext:ModelField Name="name" />
                                                        <ext:ModelField Name="accessLevel" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>



                                    <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>

                                            <ext:Column Visible="false" ID="Column3" MenuDisabled="true" runat="server" DataIndex="classId" Hideable="false" Width="75" />
                                            <ext:Column ID="Column5" MenuDisabled="true" runat="server" Text="<%$ Resources:Class%>" DataIndex="name" Hideable="false" Flex="1" />
                                            <ext:Column ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources:AccessLevel%>" DataIndex="accessLevel" Hideable="false" Width="100">
                                                <Renderer Handler="return getAccessLevelText(record.data['accessLevel']);" />
                                            </ext:Column>
                                            <ext:Column runat="server"
                                                ID="Column7" Visible="true"
                                                Text=""
                                                Width="90"
                                                Hideable="false"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                MenuDisabled="true"
                                                Resizable="false">

                                                <Renderer Handler="var r = record.data['accessLevel']<1?'&nbsp;&nbsp;&nbsp;&nbsp;':propertiesRender();return classRender()+'&nbsp;&nbsp;'+ r; " />
                                            </ext:Column>

                                        </Columns>
                                    </ColumnModel>
                                    <DockedItems>

                                        <ext:Toolbar ID="Toolbar3" runat="server" Dock="Bottom">
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
                                        <CellClick OnEvent="PoPuPClass">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                                <ext:Parameter Name="accessLevel" Value="record.data['accessLevel']" Mode="Raw" />
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
                        </ext:FormPanel>

                                  <ext:FormPanel runat="server" ID="dataAccessForm" Layout="FitLayout" Title="<%$ Resources:DataAccess%>">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Hidden runat="server" ID="currentSelection"></ext:Hidden>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" Editable="false" ID="classIdCombo" ValueField="id" DisplayField="name" FieldLabel="<%$ Resources:SelectModule%>">
                                      <Listeners>
                                          <BeforeSelect Handler="App.currentSelection.value=this.value">

                                          </BeforeSelect>
                                          <Select Handler=" if(#{GridPanel1}.getRowsValues({dirtyRowsOnly : true}).length>0) return true; else {App.dataStore.reload(); return false;}">
                                              
                                          </Select>
                                          
                                      </Listeners>
                                       <DirectEvents>
                                           <Select OnEvent="PropmptSave">
                                               <ExtraParams>
                                                   <ext:Parameter Name="selected" Value="#{classIdCombo}.value" Mode="Raw" />
                                                   <ext:Parameter Name="current" Value="#{currentSelection}.value" Mode="Raw" />
                                               </ExtraParams>
                                           </Select>
                                       </DirectEvents>
                                            <Items>

                                                <ext:ListItem Text="<%$ Resources: Classes,Class21010  %>" Value="21010" />
                                                <ext:ListItem Text="<%$ Resources:  Classes,Class21020  %>" Value="21020" />
                                                <ext:ListItem Text="<%$ Resources:  Classes,Class21040  %>" Value="21040" />
                                               

                                            </Items>

                                        </ext:ComboBox>
                                        <ext:Checkbox runat="server" ID="superUserCheck" FieldLabel="<%$Resources:IsSuperUser %>">
                                            <DirectEvents>
                                                <Change OnEvent="ToggleSuperuser" />
                                            </DirectEvents>
                                        </ext:Checkbox>
                                      
                                    </Items>

                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:GridPanel
                                    ID="GridPanel1"
                                    runat="server"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User" 
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                                    <Store>
                                        <ext:Store runat="server" ID="dataStore" OnReadData="dataStore_ReadData" AutoLoad="true">
                                            <Model>
                                                <ext:Model runat="server" IDProperty="recordId">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                        <ext:ModelField Name="hasAccess" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>



                                    <ColumnModel ID="ColumnModel5" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>

                                            <ext:Column Visible="false" ID="Column10" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" />
                                            <ext:Column ID="Column11" MenuDisabled="true" runat="server" Text="<%$ Resources:Record%>" DataIndex="name" Hideable="false" Flex="1" />
                                           <ext:WidgetColumn ID="WidgetColumn2" MenuDisabled="true"  runat="server" Text="<%$ Resources: Active %>" DataIndex="hasAccess" Hideable="false" Width="75" Align="Center">
                                <Widget>
                                    <ext:Checkbox runat="server" Name="hasAccess" ID="chk">
                                        <Listeners>
                                            
                                            <Change Handler="var rec = this.getWidgetRecord(); rec.set('hasAccess',this.value);  "  >
                                                
                                            </Change>
                                            
                                        </Listeners>
                                   <%--     <DirectEvents>
                                            <Change OnEvent ="onChangeActive_Event">
                                                <ExtraParams>
                                                    <ext:Parameter Name="recordId" Value="this.getWidgetRecord().data['recordId']" Mode="Raw" />
                                                    <ext:Parameter Name="state" Value="this.value" Mode="Raw" />
                                                </ExtraParams>
                                            </Change>
                                        </DirectEvents>--%>
                                    </ext:Checkbox>

                                </Widget>
                              
                            </ext:WidgetColumn>
                                        </Columns>
                                    </ColumnModel>
                                   
                                    <Listeners>
                                        <Render Handler="this.on('cellclick', cellClick);" />
                                    </Listeners>
                                    

                                    <View>
                                        <ext:GridView ID="GridView5" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel4" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>

                                </ext:GridPanel>
                            </Items>
                                      <Buttons>
                                          <ext:Button runat="server" ID="saveDABtn" Text="<%$Resources: Common,Save %>">
                                              
                                              <DirectEvents>
                                                  <Click OnEvent="SaveDA">
                                                      <ExtraParams>
                                                          <ext:Parameter Name="values" Value="Ext.encode(#{GridPanel1}.getRowsValues({dirtyRowsOnly : true}))" Mode="Raw"  />
                                                      </ExtraParams>
                                                  </Click>
                                              </DirectEvents>
                                          </ext:Button>
                                      </Buttons>
                        </ext:FormPanel>
                    </Items>
                </ext:TabPanel>
            </Items>


        </ext:Window>

        <ext:Window
            ID="groupUsersWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditGroupUsers %>"
            Width="550"
            Height="450"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel
                    ID="groupUsersForm" DefaultButton="SaveButton"
                    runat="server" Header="false"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" FieldWidth="160" ID="jobInfo1" EnableBranch="false" EnableDivision="false" />

                                    </Content>

                                </ext:Container>
                                <ext:Button runat="server" Text="<%$Resources:Filter %>">
                                    <Listeners>
                                        <Click Handler="App.direct.GetFilteredUsers();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:ItemSelector runat="server"  MaxHeight="300" MinHeight="300" AutoScroll="true" ID="userSelector" FromTitle="<%$Resources:All %>" DisplayField="fullName" ValueField="userId"
                            ToTitle="<%$Resources:Selected %>" >
                            <Listeners>
                                <AfterRender Handler="SwapRTL(); " />
                            </Listeners>
                            <Store>
                                <ext:Store runat="server" ID="userSelectorStore" OnReadData="userSelectorStore_ReadData">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="userId">
                                            <Fields>
                                                <ext:ModelField Name="fullName" />
                                                <ext:ModelField Name="userId" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <Listeners>

                                <Change Handler="App.direct.GetFilteredUsers();" />
                            </Listeners>
                        </ext:ItemSelector>

                    </Items>

                </ext:FormPanel>


            </Items>
            <Buttons>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{GroupUsersForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveGroupUsers" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{GroupUsersWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{GroupUsersForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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
            ID="EditClassLevelWindow"
            runat="server"
            Icon="PageEdit"
            Draggable="false"
            Maximizable="false" Resizable="false"
            Width="400"
            Height="120"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:FormPanel
                    ID="EditClassLevelForm"
                    runat="server"
                    Header="false"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        <ext:TextField runat="server" Name="classId" ID="classId" Hidden="true" Disabled="true" />

                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" FieldLabel="<%$ Resources:AccessLevel%>" DisplayField="text" ValueField="value" Editable="false" StoreID="classAccessLevelsStore" ID="accessLevel" Name="accessLevel">
                         <%--   <Items>

                                <ext:ListItem Text="<%$ Resources: NoAccess %>" Value="0" />
                                <ext:ListItem Text="<%$ Resources: Read %>" Value="1" />
                                <ext:ListItem Text="<%$ Resources: WriteClass %>" Value="2"  />
                                <ext:ListItem Text="<%$ Resources: FullControl %>" Value="3" />
                            </Items>--%>


                        </ext:ComboBox>
                    </Items>

                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveClassLevelButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditClassLevelForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveClassLevel" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditClassLevelWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="classId" Value="#{classId}.getValue()" Mode="Raw" />

                                <ext:Parameter Name="values" Value="#{EditClassLevelForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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
            ID="ApplyModuleLevelWindow"
            runat="server"
            Icon="PageEdit"
            Draggable="false"
            Maximizable="false" Resizable="false"
            Width="400"
            Height="120"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:FormPanel
                    ID="ApplyModuleLevelForm"
                    runat="server"
                    Header="false"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>

                        <ext:TextField runat="server" Name="moduleId" ID="moduleId" Hidden="true" Disabled="true" />
                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" FieldLabel="<%$ Resources:AccessLevel%>" Editable="false" ID="moduleAccessLevel" Name="moduleAccessLevel">
                            <Items>

                                <ext:ListItem Text="<%$ Resources: NoAccess %>" Value="0" />
                                <ext:ListItem Text="<%$ Resources: Read %>" Value="1" />
                                <ext:ListItem Text="<%$ Resources: WriteClass %>" Value="2" />
                                <ext:ListItem Text="<%$ Resources: FullControl %>" Value="3" />
                            </Items>


                        </ext:ComboBox>
                    </Items>

                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="ApplyModuleButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{ApplyModuleLevelForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveModuleLevel" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{ApplyModuleLevelWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="moduleId" Value="#{moduleId}.getValue()" Mode="Raw" />

                                <ext:Parameter Name="values" Value="#{ApplyModuleLevelForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button8" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <ext:Window
            ID="EditClassPropertiesWindow"
            runat="server"
            Icon="PageEdit"
            Draggable="false"
            Maximizable="false" Resizable="false"
            Width="500"
            Height="350 "
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:FormPanel
                    ID="EditClassPropertiesForm"
                    runat="server"
                    Header="false"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>

                        <ext:GridPanel
                            ID="propertiesGrid"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false" Height="300" MinHeight="300" MaxHeight="300"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Button runat="server" Icon="Cancel" ToolTip="<%$ Resources:ResetProperties%>" >
                                            <Listeners>
                                                <Click Handler="ResetPropertiesAccessLevels();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store runat="server" ID="propertiesStore" OnReadData="propertyStore_ReadData" AutoLoad="true">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="propertyId">
                                            <Fields>
                                                <ext:ModelField Name="propertyId" />
                                                <ext:ModelField Name="name" />
                                                <ext:ModelField Name="accessLevel" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>



                            <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>

                                    <ext:Column Visible="false" ID="Column8" MenuDisabled="true" runat="server" DataIndex="propertyId" Hideable="false" Width="75" />
                                    <ext:Column ID="Column9" MenuDisabled="true" runat="server" DataIndex="name" Text="<%$ Resources:Property%>" Hideable="false" Flex="1" />
                                    <ext:WidgetColumn ID="WidgetColumn1" MenuDisabled="true" runat="server" Text="<%$ Resources:AccessLevel%>" DataIndex="accessLevel" Hideable="false" Width="150" Align="Center">
                                        <Widget>
                                            <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" Editable="false" DisplayField="text" ValueField="value" StoreID="accessLevelsStore">

                                                <%--<Items>
                                                    <ext:ListItem Text="<%$ Resources: Write %>" Value="2" />
                                                    <ext:ListItem Text="<%$ Resources: NoAccess %>" Value="0"  />
                                                    <ext:ListItem Text="<%$ Resources: Read %>" Value="1" />
                                                </Items>--%>
                                                <Listeners>
                                                    <Select Handler="var rec = this.getWidgetRecord(); rec.set('accessLevel',Math.min(this.value,App.CurrentClassLevel.value)); rec.commit();" />
                                                </Listeners>
                                            </ext:ComboBox>

                                        </Widget>

                                    </ext:WidgetColumn>


                                </Columns>
                            </ColumnModel>
                            <DockedItems>

                                <ext:Toolbar ID="Toolbar4" runat="server" Dock="Bottom">
                                    <Items>
                                        <ext:StatusBar ID="StatusBar4" runat="server" />
                                        <ext:ToolbarFill />

                                    </Items>
                                </ext:Toolbar>

                            </DockedItems>



                            <View>
                                <ext:GridView ID="GridView4" runat="server" />
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel3" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>

                        </ext:GridPanel>
                    </Items>

                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button6" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditClassPropertiesForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveClassProperties" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditClassPropertiesWindow}.body}" />
                            <ExtraParams>


                                <ext:Parameter Name="values" Value="Ext.encode(#{propertiesGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button7" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>


