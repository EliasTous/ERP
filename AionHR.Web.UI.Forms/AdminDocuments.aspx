<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDocuments.aspx.cs" Inherits="AionHR.Web.UI.Forms.AdminDocuments" %>

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

        var validateSave = function () {
            var plugin = this.editingPlugin;

            if (this.getForm().isValid()) { // local validation
                App.direct.ValidateSave(plugin.context.record.phantom, Ext.encode(App.DocumentNotesGrid.getRowsValues({ selectedOnly: true })), this.getValues(false, false, false, true), {
                    success: function (result) {
                        if (!result.valid) {
                            Ext.Msg.alert("Error", result.msg);
                            return;
                        }

                        plugin.completeEdit();
                    }
                });
            }
        };

        function addNote() {
            var DocumentNotesGrid = App.DocumentNotesGrid,
                store = DocumentNotesGrid.getStore();

            DocumentNotesGrid.editingPlugin.cancelEdit();

            store.getSorters().removeAll();
            DocumentNotesGrid.getView().headerCt.setSortState(); // To update columns sort UI

            store.insert(0, {

                employeeId: document.getElementById("CurrentEmployee").value,
                note: 'Add Your Note Here',


            });

            DocumentNotesGrid.editingPlugin.startEdit(0, 0);
        }
        function ClearNoteText() {
            App.newNoteText.setValue("");
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
        <ext:Hidden ID="currentDocumentId" runat="server" />
        <ext:Hidden ID="CurrentDXCount" runat="server" />
        <ext:Hidden ID="isDrag" runat="server" />

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
                                <ext:Button ID="btnAdd1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
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
                                <ext:Button ID="btnReload" runat="server" Icon="Reload">
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
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{Store1}.reload();" />
                                    </Listeners>
                                </ext:TextField>
                                <ext:Button Visible="false" runat="server" Icon="Printer">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , Print %>" AutoPostBack="true" OnClick="printBtn_Click" OnClientClick="openInNewTab();">

                                                    <Listeners>
                                                        <Click Handler="openInNewTab();" />
                                                    </Listeners>
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server" Text="Pdf" AutoPostBack="true" OnClick="ExportPdfBtn_Click">
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server" Text="Excel" AutoPostBack="true" OnClick="ExportXLSBtn_Click">
                                                </ext:MenuItem>
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>

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
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <%--    <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name"   AllowBlank="false"/>--%>
                                <ext:DateField ID="issueDate" runat="server" FieldLabel="<%$ Resources:FieldIssueDate%>" Name="issueDate" AllowBlank="false">
                                    <Validator Handler="return this.value<#{expiryDate}.getValue();"></Validator>
                                    <Listeners>
                                        <Change Handler="#{expiryDate}.validate();"></Change>
                                    </Listeners>
                                </ext:DateField>
                                <ext:DateField ID="expiryDate" runat="server" FieldLabel="<%$ Resources:FieldExpiryDate%>" Name="expiryDate">
                                    <Validator Handler="return this.value>#{issueDate}.getValue();"></Validator>
                                    <Listeners>
                                        <Change Handler="#{issueDate}.validate();"></Change>
                                    </Listeners>
                                </ext:DateField>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="bpId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" Name="bpId" FieldLabel="<%$ Resources:FieldBusinessPartner%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="bpIdStore">
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
                                <ext:TextField ID="docRef" runat="server" FieldLabel="<%$ Resources:FieldRef%>" Name="docRef" />
                                <ext:TextField ID="binNo" runat="server" FieldLabel="<%$ Resources:binNo%>" Name="binNo" />
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="dcId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" Name="dcId" FieldLabel="<%$ Resources:FieldDocumentCategory%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="dcStore">
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
                                <ext:TextField ID="oDocRef" runat="server" FieldLabel="<%$ Resources:oDocRef%>" Name="oDocRef" />
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="languageId" AllowBlank="false" Name="languageId"
                                    SubmitValue="true"
                                    TypeAhead="false"
                                    FieldLabel="<%$ Resources: FieldLanguage%>">
                                    <Items>
                                        <ext:ListItem Text="<%$Resources:Common,EnglishLanguage %>" Value="1" />
                                        <ext:ListItem Text="<%$Resources:Common,ArabicLanguage %>" Value="2" />
                                        <ext:ListItem Text="<%$Resources:Common,FrenchLanguage %>" Value="3" />

                                    </Items>
                                </ext:ComboBox>
                                <ext:TextArea ID="notes" runat="server" FieldLabel="<%$ Resources:FieldNotes %>" Name="notes" Height="50" />
                            </Items>

                        </ext:FormPanel>
                        <ext:FormPanel runat="server" ID="documentNotesPanel" OnLoad="documentNotesPanel_Load" Title="<%$ Resources: DNGridTitle %>">
                            <Listeners>
                                <Activate Handler="App.DocumentNotesStore.reload();" />
                            </Listeners>
                            <Items>

                                <ext:Button Region="East" ID="btnAdd" Disabled="true" Height="30" Width="40" MaxWidth="40" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewDNRecord">
                                            <ExtraParams>
                                                <ext:Parameter Name="noteText" Value="#{newNoteText}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                            <EventMask ShowMask="true" CustomTarget="{#{DocumentNotesGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                    <Listeners>
                                        <AfterRender Handler="this.setDisabled(true);" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Panel runat="server" Layout="FitLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:TextArea runat="server" ID="newNoteText" Region="North" Width="400" DefaultAnchor="100%">
                                            <Listeners>
                                                <Change Handler="App.btnAdd.setDisabled(this.value==''); " />
                                            </Listeners>
                                            <LeftButtons>
                                            </LeftButtons>
                                        </ext:TextArea>

                                    </Items>
                                </ext:Panel>

                                <ext:GridPanel AutoUpdateLayout="true"
                                    ID="DocumentNotesGrid" Collapsible="false"
                                    runat="server"
                                    Height="300"
                                    Header="false"
                                    Title="<%$ Resources: DNGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical" Flex="1"
                                    Border="false"
                                    Icon="User" DefaultAnchor="100%" HideHeaders="true"
                                    ColumnLines="false" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store
                                            ID="DocumentNotesStore"
                                            runat="server"
                                            RemoteSort="False"
                                            RemoteFilter="true"
                                            OnReadData="DocumentNotesStore_RefreshData"
                                            PageSize="50" IDMode="Explicit" Namespace="App">
                                            <Proxy>
                                                <ext:PageProxy>
                                                    <Listeners>
                                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                                    </Listeners>
                                                </ext:PageProxy>
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="Model2" runat="server" IDProperty="rowId">
                                                    <Fields>


                                                        <ext:ModelField Name="email" />
                                                        <ext:ModelField Name="fullName" />
                                                        <ext:ModelField Name="doId" />
                                                        <ext:ModelField Name="seqNo" />
                                                        <ext:ModelField Name="userId" />
                                                        <ext:ModelField Name="notes" />
                                                        <ext:ModelField Name="date" />
                                                        <ext:ModelField Name="rowId" />


                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Sorters>
                                                <ext:DataSorter Property="rowId" Direction="ASC" />
                                            </Sorters>
                                        </ext:Store>
                                    </Store>
                                    <TopBar>

                                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false ">

                                            <Items>
                                            </Items>
                                        </ext:Toolbar>

                                    </TopBar>

                                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>


                                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="rowId" Hideable="false" Width="75" Align="Center" />

                                            <ext:Column CellCls="cellLink" ID="ColEHName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNotes%>" DataIndex="notes" Flex="6" Hideable="false">
                                                <Renderer Handler="var s = moment(record.data['date']);  return '<b>'+ record.data['fullName']+'</b>- '+ s.calendar()+'<br />'+ record.data['notes'];">
                                                </Renderer>
                                                <Editor>

                                                    <ext:TextArea runat="server" ID="notesEditor" Name="notes">
                                                    </ext:TextArea>
                                                </Editor>
                                            </ext:Column>

                                            <ext:Column runat="server"
                                                ID="Column2" Visible="false"
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
                                                ID="Column3" Visible="false"
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
                                                ID="ColEHDelete" Flex="2" Visible="true"
                                                Text="<%$ Resources: Common , Delete %>"
                                                Width="70"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                Hideable="false"
                                                MenuDisabled="true"
                                                Resizable="false">
                                                <Renderer Handler="return editRender() + '&nbsp;&nbsp;'+ deleteRender();">
                                                </Renderer>

                                            </ext:Column>



                                        </Columns>
                                    </ColumnModel>
                                    <Plugins>
                                        <ext:RowEditing runat="server" SaveHandler="validateSave" SaveBtnText="<%$ Resources:Common , Save %>" CancelBtnText="<%$ Resources:Common , Cancel %>" />

                                    </Plugins>
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
                                        <RowBodyDblClick Handler="App.DocumentNotesGrid.editingPlugin.cancelEdit();" />
                                        <RowDblClick Handler="App.DocumentNotesGrid.editingPlugin.cancelEdit();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <CellClick OnEvent="PoPuPDN">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>



                                                <ext:Parameter Name="index" Value="rowIndex" Mode="Raw" />
                                                <ext:Parameter Name="date" Value="record.data['date']" Mode="Raw" />
                                                <ext:Parameter Name="rowId" Value="record.data['rowId']" Mode="Raw" />

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
                        </ext:FormPanel>
                        <ext:GridPanel
                            ID="DocumentDuesGrid"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            Title="<%$ Resources: DocumentDuesWindowTitle %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                <ext:Store
                                    ID="DocumentDueStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="true"
                                    PageSize="50" IDMode="Explicit" Namespace="App" OnReadData="DocumentDueStore_RefreshData">
                                    <%--  <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>--%>
                                    <Model>
                                        <ext:Model ID="Model3" runat="server">
                                            <Fields>


                                                <ext:ModelField Name="dayId" />
                                                <ext:ModelField Name="dayIdDate" />
                                                <ext:ModelField Name="amount" />
                                                <ext:ModelField Name="doId" />
                                                <ext:ModelField Name="rowId" />


                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <%-- <Sorters>
                                <ext:DataSorter Property="rowId" Direction="ASC" />
                            </Sorters>--%>
                                </ext:Store>
                            </Store>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar5" runat="server" ClassicButtonStyle="false">
                                    <Items>
                                        <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Generate %>" Icon="Add">
                                            <Listeners>
                                                <Click Handler="CheckSession(); App.GenerateDocumentDuesWindow.show();#{startingDate}.setValue(new date());" />

                                            </Listeners>

                                        </ext:Button>



                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />

                                    </Items>
                                </ext:Toolbar>

                            </TopBar>

                            <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>

                                    <ext:DateColumn CellCls="cellLink" ID="ColdayIdDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDueDate%>" DataIndex="dayIdDate" Flex="2" Hideable="false">
                                    </ext:DateColumn>
                                    <ext:NumberColumn CellCls="cellLink" ID="Colamount" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldAmount%>" DataIndex="amount" Flex="2" Hideable="false" />




                                    <ext:Column runat="server"
                                        ID="Column5" Visible="false"
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
                                    <ext:Column runat="server" Visible="false"
                                        ID="Column6"
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
                                        ID="Column7" Visible="true"
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

                                <ext:Toolbar ID="Toolbar6" runat="server" Dock="Bottom">
                                    <Items>
                                        <ext:StatusBar ID="StatusBar3" runat="server" />
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

                                <Activate Handler="#{DocumentDueStore}.reload();" />
                            </Listeners>
                            <DirectEvents>
                                <CellClick OnEvent="PoPuPDD">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="rowId" Value="record.data['rowId']" Mode="Raw" />
                                        <ext:Parameter Name="doId" Value="record.data['doId']" Mode="Raw" />
                                        <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                        <ext:Parameter Name="amount" Value="record.data['amount']" Mode="Raw" />
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
                            ID="DocumentTransfersGrid"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            Title="<%$ Resources: DocumentsTransfers %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            Icon="ArrowSwitchBluegreen"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                <ext:Store
                                    ID="documentsTransfersStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="true"
                                    PageSize="50" IDMode="Explicit" Namespace="App" OnReadData="documentsTransfersStore_RefreshData">
                                    <%--  <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>--%>
                                    <Model>
                                        <ext:Model ID="Model4" runat="server" IDProperty="seqNo">
                                            <Fields>


                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="employeeName" IsComplex="true" />
                                                <ext:ModelField Name="date" />
                                                <ext:ModelField Name="seqNo" />
                                                <ext:ModelField Name="statusName" />
                                                <ext:ModelField Name="apStatus" />



                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <%-- <Sorters>
                                <ext:DataSorter Property="rowId" Direction="ASC" />
                            </Sorters>--%>
                                </ext:Store>
                            </Store>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar7" runat="server" ClassicButtonStyle="false">
                                    <Items>
                                        <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                            <Listeners>
                                                <Click Handler="CheckSession();" />

                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="AddNewTransfer">
                                                    <EventMask ShowMask="true" CustomTarget="={#{DocumentTransfersGrid}.body}" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>

                                    </Items>
                                </ext:Toolbar>

                            </TopBar>

                            <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="ColumnDept" runat="server" MenuDisabled="true" Text="<%$ Resources: Department%>" DataIndex="departmentName" Flex="2" Hideable="false" />
                                    <ext:Column ID="ColumnEmp" runat="server" MenuDisabled="true" Text="<%$ Resources: Employee%>" DataIndex="employeeName.fullName" Flex="2" Hideable="false">
                                        <Renderer Handler="return record.data['employeeName'].fullName;" />
                                    </ext:Column>
                                    <ext:DateColumn CellCls="cellLink" ID="DateColumn1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Width="150" Hideable="false">
                                    </ext:DateColumn>
                                    <ext:Column CellCls="cellLink" ID="ColumnStatus" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldStatus%>" DataIndex="statusName" Width="100" Hideable="false" />


                                    <ext:Column runat="server"
                                        ID="Column9" Visible="true"
                                        Text=""
                                        Width="100"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler=" return editRender()+'&nbsp;&nbsp;' +((record.data['apStatus']=='1')?deleteRender():'&nbsp;'); " />

                                    </ext:Column>


                                </Columns>
                            </ColumnModel>
                            <DockedItems>

                                <ext:Toolbar ID="Toolbar8" runat="server" Dock="Bottom">
                                    <Items>
                                        <ext:StatusBar ID="StatusBar4" runat="server" />
                                        <ext:ToolbarFill />

                                    </Items>
                                </ext:Toolbar>

                            </DockedItems>

                            <Listeners>
                                <Render Handler="this.on('cellclick', cellClick);" />

                                <Activate Handler="#{documentsTransfersStore}.reload();" />
                            </Listeners>
                            <DirectEvents>
                                <CellClick OnEvent="PoPuPDT">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="seqNo" Value="record.data['seqNo']" Mode="Raw" />
                                        <ext:Parameter Name="doId" Value="record.data['doId']" Mode="Raw" />
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
                         <ext:GridPanel
                            ID="DXGrid"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            Title="<%$ Resources: DocumentsDX %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            Icon="CheckError"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                <ext:Store
                                    ID="DXStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="true"
                                    PageSize="50" IDMode="Explicit" Namespace="App" OnReadData="DXStore_ReadData">
                                    <%--  <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>--%>
                                    <Model>
                                        <ext:Model ID="Model5" runat="server" IDProperty="seqNo">
                                            <Fields>


                                                <ext:ModelField Name="seqNo" />
                                                <ext:ModelField Name="priority" />
                                                <ext:ModelField Name="description" />
                                                <ext:ModelField Name="isDone" />
                                                <ext:ModelField Name="doId" />
                                                  </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Sorters>
                                
                            </Sorters>
                                </ext:Store>
                            </Store>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar9" runat="server" ClassicButtonStyle="false">
                                    <Items>
                                        <ext:Button ID="Button5" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                            <Listeners>
                                                <Click Handler="CheckSession();" />

                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="AddDX">
                                                    <EventMask ShowMask="true" CustomTarget="={#{DXGrid}.body}" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                            <ext:Button ID="dxSaveBtn" runat="server" Icon="Disk" Disabled="true">
                                            <Listeners>
                                                <Click Handler="CheckSession(); App.dxSaveBtn.setDisabled(true);" />
                                                
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="SaveDXGrid" Failure="App.dxSaveBtn.setDisabled(false);">
                                                    
                                                    <EventMask ShowMask="true" CustomTarget="={#{DXGrid}.body}" />
                                                    <ExtraParams>
                                                      <ext:Parameter Name="items" Value="Ext.encode(#{DXGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>

                            </TopBar>

                            <ColumnModel ID="ColumnModel5" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                     <ext:WidgetColumn ID="WidgetColumn1" MenuDisabled="true"  runat="server" Text="<%$ Resources: Done %>" DataIndex="isDone" Hideable="false" Width="75" Align="Center">
                                <Widget>
                                    <ext:Checkbox runat="server" Name="isDone"  ID="isDoneChk">
                                        <Listeners>
                                            
                                         <Change Handler="var rec = this.getWidgetRecord(); rec.set('isDone',this.value); if(rec.dirty) App.dxSaveBtn.setDisabled(false);" >
                                                
                                            </Change>
                                        </Listeners>
                                    </ext:Checkbox>

                                </Widget>
                              
                            </ext:WidgetColumn>
                                    <ext:Column ID="Column4" runat="server" MenuDisabled="true" Text="<%$ Resources: Description%>" DataIndex="description" Flex="2" Hideable="false" />
                                   
                                    <ext:Column runat="server"
                                        ID="Column11" Visible="true"
                                        Text=""
                                        Width="100"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler=" return editRender()+'&nbsp;&nbsp;'+deleteRender();  " />

                                    </ext:Column>


                                </Columns>
                            </ColumnModel>
                            <DockedItems>

                                <ext:Toolbar ID="Toolbar10" runat="server" Dock="Bottom">
                                    <Items>
                                        <ext:StatusBar ID="StatusBar5" runat="server" />
                                        <ext:ToolbarFill />

                                    </Items>
                                </ext:Toolbar>

                            </DockedItems>

                            <Listeners>
                               
                                
                                <Render Handler="this.on('cellclick', cellClick);" />

                                <Activate Handler="#{DXStore}.reload();" />
                                <Drag Handler="App.dxSaveBtn.setDisabled(false);" />
                            </Listeners>
                            <DirectEvents>
                                
                               
                                <CellClick OnEvent="PoPuPDX">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="seqNo" Value="record.data['seqNo']" Mode="Raw" />
                                        <ext:Parameter Name="doId" Value="record.data['doId']" Mode="Raw" />
                                        <ext:Parameter Name="priority" Value="record.data['priority']" Mode="Raw" />
                                        <ext:Parameter Name="description" Value="record.data['description']" Mode="Raw" />
                                        <ext:Parameter Name="isDone" Value="record.data['isDone']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView5" runat="server" >
                                     <Plugins>
                        <ext:GridDragDrop runat="server" DragText="Drag and drop to reorganize" >
                           
                            </ext:GridDragDrop>
                    </Plugins>
                                    </ext:GridView>
                            </View>



                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel4" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab1}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{BasicInfoTab1}.getForm().getValues()" Mode="Raw" Encode="true" />
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
            ID="GenerateDocumentDuesWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:DocumentDuesWindowTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel
                    ID="BasicInfoTab" DefaultButton="BTGenerateDocumentDues"
                    runat="server"
                    DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                    BodyPadding="5">
                    <Items>

                        <ext:DateField ID="startingDate" runat="server" FieldLabel="<%$ Resources:FieldStartingDate%>" Name="startingDate" AllowBlank="false" />
                        <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="frequency" AllowBlank="false" Name="frequency"
                            SubmitValue="true"
                            TypeAhead="false"
                            FieldLabel="<%$ Resources: FieldFrequency%>">
                            <Items>



                                <ext:ListItem Text="<%$Resources:DAILY %>" Value="1" />
                                <ext:ListItem Text="<%$Resources:WEEKLY %>" Value="2" />
                                <ext:ListItem Text="<%$Resources:MONTHLY %>" Value="3" />
                                <ext:ListItem Text="<%$Resources:QUARTERLY %>" Value="4" />
                                <ext:ListItem Text="<%$Resources:HALF_YEALRY %>" Value="5" />
                                <ext:ListItem Text="<%$Resources:YEARLY %>" Value="6" />

                            </Items>
                        </ext:ComboBox>
                        <ext:NumberField ID="GDDAmount" runat="server" FieldLabel="<%$ Resources:FieldAmount%>" Name="amount" AllowBlank="true" />
                        <ext:NumberField ID="count" runat="server" FieldLabel="<%$ Resources:FieldCount%>" Name="count" AllowBlank="false" MinValue="1" MaxValue="12" />
                    </Items>

                </ext:FormPanel>

            </Items>


            <Buttons>
                <ext:Button ID="BTGenerateDocumentDues" runat="server" Text="<%$ Resources:Common, Generate %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="generateDocument" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{GenerateDocumentDuesWindow}.body}" />
                            <ExtraParams>

                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
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
            ID="documentDueWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:DocumentDuesWindowTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel
                    ID="documentDueForm" DefaultButton="DocumentDueBtn"
                    runat="server"
                    DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                    BodyPadding="5">
                    <Items>

                        <ext:DateField ID="dayIdDate" runat="server" FieldLabel="<%$ Resources:FieldDueDate%>" Name="dayIdDate" AllowBlank="false" />

                        <ext:NumberField ID="amount" runat="server" FieldLabel="<%$ Resources:FieldAmount%>" Name="amount" AllowBlank="false" />


                        <ext:TextField ID="rowId" runat="server" Name="rowId" Hidden="true" />
                        <ext:TextField ID="dayId" runat="server" Name="dayId" Hidden="true" />
                    </Items>

                </ext:FormPanel>

            </Items>


            <Buttons>
                <ext:Button ID="DocumentDueBtn" runat="server" Text="<%$ Resources:Common, save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{documentDueForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="saveDocumentDue" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{documentDueWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="rowId" Value="#{rowId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="dayId" Value="#{dayId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{documentDueForm}.getForm().getValues()" Mode="Raw" Encode="true" />
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
        <ext:Window
            ID="DXWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:DXWindow %>"
            Width="300"
            Height="150"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel
                    ID="DXForm" DefaultButton="Button9"
                    runat="server"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>

                        

                        <ext:TextArea TabIndex="0" ID="description" runat="server" Name="description"  FieldLabel="<%$ Resources:Description%>" />
                       
                        <ext:TextField ID="DXseqNo" runat="server" Name="seqNo" Hidden="true" />
                        <ext:TextField ID="priority" runat="server" Name="priority" Hidden="true" />
                       <ext:Checkbox ID="isDone" runat="server" Name="isDone" InputValue="true" Hidden="true"  SubmitValue="true"/>
                        
                        
                    </Items>

                </ext:FormPanel>

            </Items>


            <Buttons>
                <ext:Button ID="Button9" runat="server" Text="<%$ Resources:Common, save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{DXForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveDX" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{DXWindow}.body}" />
                            <ExtraParams>

                                <ext:Parameter Name="values" Value="#{DXForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button10" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

    </form>
</body>
</html>
