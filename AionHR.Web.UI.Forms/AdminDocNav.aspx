<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDocNav.aspx.cs" Inherits="AionHR.Web.UI.Forms.AdminDocNav" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/DocumentTypes.js?id=20"></script>
    <script type="text/javascript" src="Scripts/common.js?id=17"></script>


    <script type="text/javascript" src="Scripts/moment.js?id=19"></script>
    <script type="text/javascript">
        var dragged = false;
        var source = '';
        var targetDeptName = '';
        var targetDeptId = '';
        var sourceAssigned = '';
        function mouseDown(s, e) {
            source = e.data['recordId'];
            console.log(source);
            sourceAssigned = e.data['employeeId'];

        }

        function drag(s, e, d) {
            dragged = true;

            if (sourceAssigned != App.currentEmployeeId.value)
                Ext.MessageBox.alert(App.errorTitle.value, App.dragMessage.value);
            source = e.data['recordId'];
        }
        function openWindow() {
            App.DocumentTransferWindow.show();
        }

        function Drop(s, e) {

            var rec = this.selectionModel.selected.items[0].data;
            console.log(rec);
            var dept = this.ownerGrid.getTitle();
            setTimeout(function () {
                App.doId.setValue(rec['recordId']); console.log(App.departmentId.store); var deptId = -1; App.departmentId.store.data.items.forEach(function (x) { if (dept == x.data['name']) deptId = x.data['recordId']; }); console.log(deptId); App.departmentId.select(deptId); App.DocumentTransferWindow.show();
            }, 1000);
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
        <ext:Hidden ID="dragMessage" runat="server" Text="<%$ Resources: DragErrorMessage %>" />
        <ext:Hidden ID="errorTitle" runat="server" Text="<%$ Resources:Common,Error %>" />
        <ext:Hidden ID="departmentText" runat="server" Text="<%$ Resources:Department%>" />
        <ext:Hidden ID="dateFormat" runat="server" />
        <ext:Hidden ID="currentDocumentId" runat="server" />
        <ext:Hidden ID="currentEmployeeId" runat="server" />
        <ext:Hidden ID="DeptId1" runat="server" />
        <ext:Hidden ID="DeptId2" runat="server" />
        <ext:Hidden ID="DeptId3" runat="server" />
        <ext:Hidden ID="DeptId4" runat="server" />
        <ext:Hidden ID="DeptId5" runat="server" />
        <ext:Hidden ID="DeptId6" runat="server" />
        <ext:Store
            ID="documentsStore"
            runat="server"
            RemoteSort="False" GroupField="departmentName"
            RemoteFilter="true"
            OnReadData="documentsStore_ReadData"
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
                        <ext:ModelField Name="bpName" />
                        <ext:ModelField Name="dcName" />
                        <ext:ModelField Name="issueDate" />
                        <ext:ModelField Name="expiryDate" />
                        <ext:ModelField Name="bpId" />
                        <ext:ModelField Name="docRef" />
                        <ext:ModelField Name="binNo" />
                        <ext:ModelField Name="dcId" />
                        <ext:ModelField Name="notes" />
                        <ext:ModelField Name="oDocRef" />
                        <ext:ModelField Name="languageId" />
                        <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="departmentId" />
                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="employeeName"  />

                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
            </Sorters>
        </ext:Store>



        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FieldContainer runat="server" Layout="HBoxLayout" Height="400" AnchorVertical="true">
                    <LayoutConfig>
                        <ext:HBoxLayoutConfig Align="Stretch" Pack="Start" />
                    </LayoutConfig>
                    <Items>
                        <ext:GridPanel
                            ID="GridPanel1"
                            runat="server"
                            Hidden="true"
                            PaddingSpec="0 0 1 0"
                            Header="true"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="true"
                            Width="250"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                <ext:Store ID="Store1" runat="server" IDMode="Explicit" Namespace="App">
                                    <Model>
                                        <ext:Model ID="Model2" runat="server" IDProperty="recordId">
                                            <Fields>

                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="bpName" />
                                                <ext:ModelField Name="dcName" />
                                                <ext:ModelField Name="issueDate" />
                                                <ext:ModelField Name="expiryDate" />
                                                <ext:ModelField Name="bpId" />
                                                <ext:ModelField Name="docRef" />
                                                <ext:ModelField Name="binNo" />
                                                <ext:ModelField Name="dcId" />
                                                <ext:ModelField Name="notes" />
                                                <ext:ModelField Name="oDocRef" />
                                                <ext:ModelField Name="languageId" />
                                                <ext:ModelField Name="employeeId" />
                                                <ext:ModelField Name="departmentId" />
                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="employeeName"  />

                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                                    <ext:Column ID="ColbpId" Visible="false" DataIndex="bpId" runat="server" />
                                    <ext:Column CellCls="cellLink" ID="ColdocRef" MenuDisabled="true" runat="server" DataIndex="docRef" Flex="1" Hideable="false">
                                     <Renderer Handler="return record.data['docRef'] + ':' + record.data['bpName'] + '<br />'+ record.data['dcName'] + '<br/>'+ moment(record.data['expiryDate']).format(App.dateFormat.value); " />
                                    </ext:Column>
                                    <%--<ext:Column CellCls="cellLink" ID="ColbpName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBusinessPartner%>" DataIndex="bpName" Flex="2" Hideable="false" />--%>
                                    <%--<ext:Column CellCls="cellLink" ID="ColdcName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentCategory%>" DataIndex="dcName" Flex="2" Hideable="false" />--%>
                                    <%--               <ext:DateColumn CellCls="cellLink" ID="ColissueDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIssueDate%>" DataIndex="issueDate" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColexpiryDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldExpiryDate%>" DataIndex="expiryDate" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColbinNo" MenuDisabled="true" runat="server" Text="<%$ Resources: binNo%>" DataIndex="binNo" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Colnotes" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNotes%>" DataIndex="notes" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColoDocRef" MenuDisabled="true" runat="server" Text="<%$ Resources: oDocRef%>" DataIndex="oDocRef" Flex="2" Hideable="false" />--%>
                                    <ext:Column runat="server"
                                        ID="colEdit" Visible="true"
                                        Text=""
                                        Width="50"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="return editRender(); " />

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
                                    runat="server" Hidden="true"
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
                                <Drag Fn="drag" />


                                <RowMouseDown Fn="mouseDown" />

                            </Listeners>
                            <DirectEvents>
                                <CellClick OnEvent="PoPuP">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                        <ext:Parameter Name="bpId" Value="record.data['bpId']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop runat="server" />

                                    </Plugins>
                                    <Listeners>
                                        <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" Delay="1" />
                                        <Drop Fn="Drop" />
                                    </Listeners>
                                </ext:GridView>
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                        </ext:GridPanel>
                        <ext:GridPanel
                            ID="GridPanel2"
                            Hidden="true"
                            runat="server"
                            Border="true"
                            PaddingSpec="0 0 1 0"
                            Title="<%$ Resources:Common, OpenDocuments %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Width="250"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">

                            <Store>
                                <ext:Store ID="Store2" runat="server" IDMode="Explicit" Namespace="App">
                                    <Model>
                                        <ext:Model ID="Model3" runat="server" IDProperty="recordId">
                                            <Fields>

                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="bpName" />
                                                <ext:ModelField Name="dcName" />
                                                <ext:ModelField Name="issueDate" />
                                                <ext:ModelField Name="expiryDate" />
                                                <ext:ModelField Name="bpId" />
                                                <ext:ModelField Name="docRef" />
                                                <ext:ModelField Name="binNo" />
                                                <ext:ModelField Name="dcId" />
                                                <ext:ModelField Name="notes" />
                                                <ext:ModelField Name="oDocRef" />
                                                <ext:ModelField Name="languageId" />
                                                <ext:ModelField Name="employeeId" />
                                                <ext:ModelField Name="departmentId" />
                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="employeeName"  />

                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="Column1" Visible="false" DataIndex="recordId" runat="server" />
                                    <ext:Column ID="Column2" Visible="false" DataIndex="bpId" runat="server" />
                                    <ext:Column CellCls="cellLink" ID="Column3" MenuDisabled="true" runat="server"  DataIndex="docRef" Flex="1" Hideable="false" >
                                        <Renderer Handler="return record.data['docRef'] + ':' + record.data['bpName'] + '<br />'+ record.data['dcName'] + '<br/>'+ moment(record.data['expiryDate']).format(App.dateFormat.value); " />
                                    </ext:Column>
                                    <%--<ext:Column CellCls="cellLink" ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBusinessPartner%>" DataIndex="bpName" Flex="2" Hideable="false" />--%>
                                    <%--<ext:Column CellCls="cellLink" ID="ColdcName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentCategory%>" DataIndex="dcName" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColissueDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIssueDate%>" DataIndex="issueDate" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColexpiryDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldExpiryDate%>" DataIndex="expiryDate" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColbinNo" MenuDisabled="true" runat="server" Text="<%$ Resources: binNo%>" DataIndex="binNo" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Colnotes" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNotes%>" DataIndex="notes" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColoDocRef" MenuDisabled="true" runat="server" Text="<%$ Resources: oDocRef%>" DataIndex="oDocRef" Flex="2" Hideable="false" />--%>
                                    <ext:Column runat="server"
                                        ID="Column5" Visible="true"
                                        Text=""
                                        Width="50"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="return editRender(); " />

                                    </ext:Column>


                                </Columns>
                            </ColumnModel>
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
                                    runat="server" Hidden="true"
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
                                <Drag Fn="drag" />


                                <RowMouseDown Fn="mouseDown" />

                                <Render Handler="this.on('cellclick', cellClick);" />
                            </Listeners>
                            <DirectEvents>
                                <CellClick OnEvent="PoPuP">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                        <ext:Parameter Name="bpId" Value="record.data['bpId']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView2" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop runat="server" />

                                    </Plugins>
                                    <Listeners>
                                        <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" Delay="1" />
                                        <Drop Fn="Drop" />
                                    </Listeners>
                                </ext:GridView>
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                        </ext:GridPanel>
                        <ext:GridPanel
                            ID="GridPanel3"
                            Hidden="true"
                            runat="server"
                            Border="true"
                            PaddingSpec="0 0 1 0"
                            Title="<%$ Resources:Common, OpenDocuments %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Width="250"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">

                            <Store>
                                <ext:Store ID="Store3" runat="server" IDMode="Explicit" Namespace="App">
                                    <Model>
                                        <ext:Model ID="Model4" runat="server" IDProperty="recordId">
                                            <Fields>

                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="bpName" />
                                                <ext:ModelField Name="dcName" />
                                                <ext:ModelField Name="issueDate" />
                                                <ext:ModelField Name="expiryDate" />
                                                <ext:ModelField Name="bpId" />
                                                <ext:ModelField Name="docRef" />
                                                <ext:ModelField Name="binNo" />
                                                <ext:ModelField Name="dcId" />
                                                <ext:ModelField Name="notes" />
                                                <ext:ModelField Name="oDocRef" />
                                                <ext:ModelField Name="languageId" />
                                                <ext:ModelField Name="employeeId" />
                                                <ext:ModelField Name="departmentId" />
                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="employeeName"  />

                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="Column6" Visible="false" DataIndex="recordId" runat="server" />
                                    <ext:Column ID="Column7" Visible="false" DataIndex="bpId" runat="server" />
                                    <ext:Column CellCls="cellLink" ID="Column8" MenuDisabled="true" runat="server" DataIndex="docRef" Flex="1" Hideable="false">
                                        <Renderer Handler="return record.data['docRef'] + ':' + record.data['bpName'] + '<br />'+ record.data['dcName'] + '<br/>'+ moment(record.data['expiryDate']).format(App.dateFormat.value); " />
                                    </ext:Column>
                                    <%--<ext:Column CellCls="cellLink" ID="Column9" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBusinessPartner%>" DataIndex="bpName" Flex="2" Hideable="false" />--%>
                                    <%--<ext:Column CellCls="cellLink" ID="ColdcName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentCategory%>" DataIndex="dcName" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColissueDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIssueDate%>" DataIndex="issueDate" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColexpiryDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldExpiryDate%>" DataIndex="expiryDate" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColbinNo" MenuDisabled="true" runat="server" Text="<%$ Resources: binNo%>" DataIndex="binNo" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Colnotes" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNotes%>" DataIndex="notes" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColoDocRef" MenuDisabled="true" runat="server" Text="<%$ Resources: oDocRef%>" DataIndex="oDocRef" Flex="2" Hideable="false" />--%>
                                    <ext:Column runat="server"
                                        ID="Column10" Visible="true"
                                        Text=""
                                        Width="50"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="return editRender(); " />

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
                            <BottomBar>

                                <ext:PagingToolbar ID="PagingToolbar3"
                                    runat="server" Hidden="true"
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
                                        <ext:Parameter Name="bpId" Value="record.data['bpId']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView3" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop runat="server" />

                                    </Plugins>
                                    <Listeners>
                                        <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" Delay="1" />
                                        <Drop Fn="Drop" />
                                    </Listeners>
                                </ext:GridView>
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel2" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                        </ext:GridPanel>
                        <ext:GridPanel
                            ID="GridPanel4"
                            Hidden="true"
                            runat="server"
                            Border="true"
                            PaddingSpec="0 0 1 0"
                            Title="<%$ Resources:Common, OpenDocuments %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Width="250"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">

                            <Store>
                                <ext:Store ID="Store4" runat="server" IDMode="Explicit" Namespace="App">
                                    <Model>
                                        <ext:Model ID="Model5" runat="server" IDProperty="recordId">
                                            <Fields>

                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="bpName" />
                                                <ext:ModelField Name="dcName" />
                                                <ext:ModelField Name="issueDate" />
                                                <ext:ModelField Name="expiryDate" />
                                                <ext:ModelField Name="bpId" />
                                                <ext:ModelField Name="docRef" />
                                                <ext:ModelField Name="binNo" />
                                                <ext:ModelField Name="dcId" />
                                                <ext:ModelField Name="notes" />
                                                <ext:ModelField Name="oDocRef" />
                                                <ext:ModelField Name="languageId" />
                                                <ext:ModelField Name="employeeId" />
                                                <ext:ModelField Name="departmentId" />
                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="employeeName"  />

                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="Column11" Visible="false" DataIndex="recordId" runat="server" />
                                    <ext:Column ID="Column12" Visible="false" DataIndex="bpId" runat="server" />
                                    <ext:Column CellCls="cellLink" ID="Column13" MenuDisabled="true" runat="server" DataIndex="docRef" Flex="1" Hideable="false" >
                                    <Renderer Handler="return record.data['docRef'] + ':' + record.data['bpName'] + '<br />'+ record.data['dcName'] + '<br/>'+ moment(record.data['expiryDate']).format(App.dateFormat.value); " />
                                    </ext:Column>
                                    <%--<ext:Column CellCls="cellLink" ID="Column14" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBusinessPartner%>" DataIndex="bpName" Flex="2" Hideable="false" />--%>
                                    <%--<ext:Column CellCls="cellLink" ID="ColdcName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentCategory%>" DataIndex="dcName" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColissueDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIssueDate%>" DataIndex="issueDate" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColexpiryDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldExpiryDate%>" DataIndex="expiryDate" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColbinNo" MenuDisabled="true" runat="server" Text="<%$ Resources: binNo%>" DataIndex="binNo" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Colnotes" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNotes%>" DataIndex="notes" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColoDocRef" MenuDisabled="true" runat="server" Text="<%$ Resources: oDocRef%>" DataIndex="oDocRef" Flex="2" Hideable="false" />--%>
                                    <ext:Column runat="server"
                                        ID="Column15" Visible="true"
                                        Text=""
                                        Width="50"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="return editRender(); " />

                                    </ext:Column>


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
                            <BottomBar>

                                <ext:PagingToolbar ID="PagingToolbar4"
                                    runat="server" Hidden="true"
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
                                        <ext:Parameter Name="bpId" Value="record.data['bpId']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView4" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop runat="server" />

                                    </Plugins>
                                    <Listeners>
                                        <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" Delay="1" />
                                        <Drop Fn="Drop" />
                                    </Listeners>
                                </ext:GridView>
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel3" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                        </ext:GridPanel>
                        <ext:GridPanel
                            ID="GridPanel5"
                            Hidden="true"
                            runat="server"
                            Border="true"
                            PaddingSpec="0 0 1 0"
                            Title="<%$ Resources:Common, OpenDocuments %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Width="250"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">

                            <Store>
                                <ext:Store ID="Store5" runat="server" IDMode="Explicit" Namespace="App">
                                    <Model>
                                        <ext:Model ID="Model6" runat="server" IDProperty="recordId">
                                            <Fields>

                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="bpName" />
                                                <ext:ModelField Name="dcName" />
                                                <ext:ModelField Name="issueDate" />
                                                <ext:ModelField Name="expiryDate" />
                                                <ext:ModelField Name="bpId" />
                                                <ext:ModelField Name="docRef" />
                                                <ext:ModelField Name="binNo" />
                                                <ext:ModelField Name="dcId" />
                                                <ext:ModelField Name="notes" />
                                                <ext:ModelField Name="oDocRef" />
                                                <ext:ModelField Name="languageId" />
                                                <ext:ModelField Name="employeeId" />
                                                <ext:ModelField Name="departmentId" />
                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="employeeName"  />

                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            <ColumnModel ID="ColumnModel5" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="Column16" Visible="false" DataIndex="recordId" runat="server" />
                                    <ext:Column ID="Column17" Visible="false" DataIndex="bpId" runat="server" />
                                    <ext:Column CellCls="cellLink" ID="Column18" MenuDisabled="true" runat="server"  DataIndex="docRef" Flex="1" Hideable="false" >
                                        <Renderer Handler="return record.data['docRef'] + ':' + record.data['bpName'] + '<br />'+ record.data['dcName'] + '<br/>'+ moment(record.data['expiryDate']).format(App.dateFormat.value); " />
                                    </ext:Column>
                                    <%--<ext:Column CellCls="cellLink" ID="Column19" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBusinessPartner%>" DataIndex="bpName" Flex="2" Hideable="false" />--%>
                                    <%--<ext:Column CellCls="cellLink" ID="ColdcName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentCategory%>" DataIndex="dcName" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColissueDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIssueDate%>" DataIndex="issueDate" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColexpiryDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldExpiryDate%>" DataIndex="expiryDate" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColbinNo" MenuDisabled="true" runat="server" Text="<%$ Resources: binNo%>" DataIndex="binNo" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Colnotes" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNotes%>" DataIndex="notes" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColoDocRef" MenuDisabled="true" runat="server" Text="<%$ Resources: oDocRef%>" DataIndex="oDocRef" Flex="2" Hideable="false" />--%>
                                    <ext:Column runat="server"
                                        ID="Column20" Visible="true"
                                        Text=""
                                        Width="50"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="return editRender(); " />

                                    </ext:Column>


                                </Columns>
                            </ColumnModel>
                            <DockedItems>

                                <ext:Toolbar ID="Toolbar5" runat="server" Dock="Bottom">
                                    <Items>
                                        <ext:StatusBar ID="StatusBar5" runat="server" />
                                        <ext:ToolbarFill />

                                    </Items>
                                </ext:Toolbar>

                            </DockedItems>
                            <BottomBar>

                                <ext:PagingToolbar ID="PagingToolbar5"
                                    runat="server" Hidden="true"
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
                                        <ext:Parameter Name="bpId" Value="record.data['bpId']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView5" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop runat="server" />

                                    </Plugins>
                                    <Listeners>
                                        <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" Delay="1" />
                                        <Drop Fn="Drop" />
                                    </Listeners>
                                </ext:GridView>
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel4" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                        </ext:GridPanel>
                        <ext:GridPanel
                            ID="GridPanel6"
                            Hidden="true"
                            runat="server"
                            Border="true"
                            PaddingSpec="0 0 1 0"
                            Title="<%$ Resources:Common, OpenDocuments %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Width="250"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">

                            <Store>
                                <ext:Store ID="Store6" runat="server" IDMode="Explicit" Namespace="App">
                                    <Model>
                                        <ext:Model ID="Model7" runat="server" IDProperty="recordId">
                                            <Fields>

                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="bpName" />
                                                <ext:ModelField Name="dcName" />
                                                <ext:ModelField Name="issueDate" />
                                                <ext:ModelField Name="expiryDate" />
                                                <ext:ModelField Name="bpId" />
                                                <ext:ModelField Name="docRef" />
                                                <ext:ModelField Name="binNo" />
                                                <ext:ModelField Name="dcId" />
                                                <ext:ModelField Name="notes" />
                                                <ext:ModelField Name="oDocRef" />
                                                <ext:ModelField Name="languageId" />
                                                <ext:ModelField Name="employeeId" />
                                                <ext:ModelField Name="departmentId" />
                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="employeeName"  />

                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            <ColumnModel ID="ColumnModel6" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="Column21" Visible="false" DataIndex="recordId" runat="server" />
                                    <ext:Column ID="Column22" Visible="false" DataIndex="bpId" runat="server" />
                                    <ext:Column CellCls="cellLink" ID="Column23" MenuDisabled="true" runat="server"  DataIndex="docRef" Flex="1" Hideable="false" >
                                        <Renderer Handler="return record.data['docRef'] + ':' + record.data['bpName'] + '<br />'+ record.data['dcName'] + '<br/>'+ moment(record.data['expiryDate']).format(App.dateFormat.value); " />
                                    </ext:Column>
                                    <%--<ext:Column CellCls="cellLink" ID="Column24" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBusinessPartner%>" DataIndex="bpName" Flex="2" Hideable="false" />--%>
                                    <%--<ext:Column CellCls="cellLink" ID="ColdcName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentCategory%>" DataIndex="dcName" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColissueDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIssueDate%>" DataIndex="issueDate" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColexpiryDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldExpiryDate%>" DataIndex="expiryDate" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColbinNo" MenuDisabled="true" runat="server" Text="<%$ Resources: binNo%>" DataIndex="binNo" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Colnotes" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNotes%>" DataIndex="notes" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColoDocRef" MenuDisabled="true" runat="server" Text="<%$ Resources: oDocRef%>" DataIndex="oDocRef" Flex="2" Hideable="false" />--%>
                                    <ext:Column runat="server"
                                        ID="Column25" Visible="true"
                                        Text=""
                                        Width="50"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="return editRender(); " />

                                    </ext:Column>


                                </Columns>
                            </ColumnModel>
                            <DockedItems>

                                <ext:Toolbar ID="Toolbar6" runat="server" Dock="Bottom">
                                    <Items>
                                        <ext:StatusBar ID="StatusBar6" runat="server" />
                                        <ext:ToolbarFill />

                                    </Items>
                                </ext:Toolbar>

                            </DockedItems>
                            <BottomBar>

                                <ext:PagingToolbar ID="PagingToolbar6"
                                    runat="server" Hidden="true"
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
                                        <ext:Parameter Name="bpId" Value="record.data['bpId']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView6" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop runat="server" />

                                    </Plugins>
                                    <Listeners>
                                        <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" Delay="1" />
                                        <Drop Fn="Drop" />
                                    </Listeners>
                                </ext:GridView>
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel5" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                        </ext:GridPanel>

                    </Items>
                </ext:FieldContainer>
            </Items>
        </ext:Viewport>



        <ext:Window
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Width="700"
            Height="450"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="BasicInfoTab1" DefaultButton="SaveButton"
                            runat="server"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:TabPanel>

            </Items>
            <Buttons>

                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <ext:Window
            ID="DocumentTransferWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:DocumentTransferWindow %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            <Listeners>
                <Close Handler=" App.DocumentTransferForm.reset(); App.documentsStore.reload();" />
                <Hide Handler="App.DocumentTransferForm.reset(); App.documentsStore.reload();" />
            </Listeners>
            <Items>

                <ext:FormPanel
                    ID="DocumentTransferForm" DefaultButton="DT_Btn"
                    runat="server"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>

                        <ext:Label ID="statusLabel" runat="server" />
                        <ext:ComboBox AutoScroll="true" AnyMatch="true" CaseSensitive="false" EnableRegEx="true" runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="departmentId" Name="departmentId" FieldLabel="<%$ Resources:Department%>">
                            <ListConfig MaxHeight="100"></ListConfig>
                            <Store>
                                <ext:Store runat="server" ID="departmentStore">
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
                                <ext:Button ID="Button7" runat="server" Icon="Add" Hidden="true">
                                    <Listeners>
                                        <Click Handler="CheckSession();  " />
                                    </Listeners>
                                    <DirectEvents>

                                        <Click OnEvent="addDepartment">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </RightButtons>
                            <Listeners>
                                <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="employeeId"
                            DisplayField="fullName"
                            ValueField="recordId"
                            TypeAhead="false" Name="employeeId"
                            FieldLabel="<%$ Resources: Employee%>"
                            HideTrigger="true" SubmitValue="true"
                            MinChars="3"
                            TriggerAction="Query" ForceSelection="true" AllowBlank="true">
                            <Store>
                                <ext:Store runat="server" ID="supervisorStore" AutoLoad="false">
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
                        </ext:ComboBox>

                        <ext:TextField ID="seqNo" runat="server" Name="seqNo" Hidden="true" />
                        <ext:TextField ID="apStatus" runat="server" Name="apStatus" Hidden="true" />
                        <ext:TextField ID="doId" runat="server" Name="doId" Hidden="true" />
                        <ext:DateField ID="date" runat="server" FieldLabel="<%$ Resources:FieldDate%>" Name="date" AllowBlank="false" />
                        <ext:TextArea ID="DTnotes" runat="server" Name="notes" FieldLabel="<%$ Resources:FieldNotes%>" />
                    </Items>

                </ext:FormPanel>

            </Items>


            <Buttons>
                <ext:Button ID="DT_Btn" runat="server" Text="<%$ Resources:Common, save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{DocumentTransferForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="saveDocumentTransfer" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{DocumentTransferWindow}.body}" />
                            <ExtraParams>

                                <ext:Parameter Name="values" Value="#{DocumentTransferForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button6" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>


    </form>
</body>
</html>
