﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecurityGroups.aspx.cs" Inherits="AionHR.Web.UI.Forms.SecurityGroups" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />

    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script src="Scripts/SecurityGroups.js?id=1" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript">
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
                if (fromStore.getById(items[i].userId) == null && toStore.getById(items[i].userId)==null) {
                    
                    fromStore.add(items[i]);
                }
                    
            }
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

        <ext:Store
            ID="groupsStore"
            runat="server"
            RemoteSort="True"
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

                                <Renderer Handler="return attachRender()+'&nbsp;&nbsp;'+ editRender()+'&nbsp;&nbsp;' +deleteRender(); " />
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
                            ID="GroupForm" DefaultButton="SaveButton"
                            runat="server"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" Hidden="true" runat="server" Disabled="true" Name="recordId" />
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextArea ID="description" runat="server" FieldLabel="<%$ Resources:FieldDescription%>" Name="description" />


                            </Items>

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
                                    RemoteSort="True"
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
                                            </Fields>
                                        </ext:Model>
                                    </Model>

                                </ext:Store>
                            </Store>
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
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
                                    <ext:Column CellCls="cellLink" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="fullName" Flex="2" Hideable="false">
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
                    </Items>
                </ext:TabPanel>
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
        </ext:Window>

        <ext:Window
            ID="groupUsersWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditGroupUsers %>"
            Width="450"
            Height="450"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="groupUsersForm" DefaultButton="SaveButton"
                            runat="server"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Container runat="server" Layout="FitLayout">
                                            <Content>
                                                <uc:jobInfo runat="server" ID="jobInfo1" EnableBranch="false" EnableDivision="false" />

                                            </Content>

                                        </ext:Container>
                                        <ext:Button runat="server" Text="Go">
                                            <Listeners>
                                                <Click Handler="App.direct.GetFilteredUsers();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:ItemSelector runat="server" MaxHeight="300" MinHeight="300" AutoScroll="true" ID="userSelector" FromTitle="<%$Resources:All %>" DisplayField="fullName" ValueField="userId"
                                    ToTitle="<%$Resources:Selected %>">

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
                </ext:TabPanel>
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


    </form>
</body>
</html>


