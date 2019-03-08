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

       
    </script>

</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="currentDocumentId" runat="server" />

        <ext:Store
            ID="documentsStore"
            runat="server"
            RemoteSort="False" GroupField="bpName"
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
                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="employeeName" IsComplex="true" />

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
                    StoreID="documentsStore"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                     <Features>
                    <ext:Grouping
                        runat="server"
                        HideGroupedHeader="true"
                        GroupHeaderTplString="Business Partner: {departmentName} ({rows.length} Item{[values.rows.length > 1 ? 's' : '']})"
                        StartCollapsed="true" />
                </Features>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                              
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button ID="btnReload" runat="server" Icon="Reload">
                                    <Listeners>
                                        <Click Handler="CheckSession();#{documentsStore}.reload();" />
                                    </Listeners>

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

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column ID="ColbpId" Visible="false" DataIndex="bpId" runat="server" />
                            <ext:Column CellCls="cellLink" ID="ColdocRef" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldRef%>" DataIndex="docRef" Flex="1" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColbpName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBusinessPartner%>" DataIndex="bpName" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColdcName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentCategory%>" DataIndex="dcName" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColissueDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIssueDate%>" DataIndex="issueDate" Flex="2" Hideable="false" />
                            <ext:DateColumn CellCls="cellLink" ID="ColexpiryDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldExpiryDate%>" DataIndex="expiryDate" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColbinNo" MenuDisabled="true" runat="server" Text="<%$ Resources: binNo%>" DataIndex="binNo" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Colnotes" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNotes%>" DataIndex="notes" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColoDocRef" MenuDisabled="true" runat="server" Text="<%$ Resources: oDocRef%>" DataIndex="oDocRef" Flex="2" Hideable="false" />






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
                                <ext:Parameter Name="bpId" Value="record.data['bpId']" Mode="Raw" />
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
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
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



    </form>
</body>
</html>
