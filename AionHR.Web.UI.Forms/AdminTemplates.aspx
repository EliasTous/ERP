<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminTemplates.aspx.cs" Inherits="AionHR.Web.UI.Forms.AdminTemplates" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/AdminTemplates.js?id=4"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/css/bootstrap.min.css" />
    
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <%--<script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.min.js"></script>--%>
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/js/bootstrap.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote-bs4.css" rel="stylesheet" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote-bs4.js"></script>
    <style type="text/css">
        .note-toolbar-wrapper {
            height: 66px !important;
        }
        .tags-dropdown{
            height:220px;
            overflow:auto;
            overflow-x: auto;
            width:300px;
        }
    </style>
    <script type="text/javascript">
        var empTags = null;
        function InitEmpTags(tags) {
            console.log(tags);
            empTags = tags;
        }
        function escapeHtml(unsafe) {
            return unsafe
                .replace(/&/g, "&amp;")
                .replace(/</g, "&lt;")
                .replace(/>/g, "&gt;")
                .replace(/"/g, "&quot;")
                .replace(/'/g, "&#039;");
        }
        var employeeParams = function (context) {
  var ui = $.summernote.ui;

            // create button
            var buttongroup = ui.buttonGroup([ui.button({
                contents: '<i class="material-icons">person</i>',
               
                data: {
                        toggle: 'dropdown'
                    }
            }), ui.dropdown({
                    className: 'tags-dropdown',
                items: empTags,
                
                    click: function (d) {
                        console.log(d);
                       
                    // invoke insertText method with 'hello' on editor module.
                        context.invoke('editor.insertText',"@@"+ d.target.innerText.split('(')[0].trim());
                }
            })]);

  return buttongroup.render();   // return button as jquery object
        }
        var companyParams = function (context) {
  var ui = $.summernote.ui;

            // create button
            var buttongroup = ui.buttonGroup([ui.button({
                contents: '<i class="fa fa-child"/> Company Parameters',
                tooltip: 'hello',
                data: {
                        toggle: 'dropdown'
                    }
            }), ui.dropdown({
                items: ['companyname (company  name)', 'lastname (employee last name)', 'email (employee email)'],
                tooltip: 'hello',
                    click: function (d) {
                        console.log(d);
                       
                    // invoke insertText method with 'hello' on editor module.
                        context.invoke('editor.insertText',"@@"+ d.target.innerText.split('(')[0].trim());
                }
            })]);

  return buttongroup.render();   // return button as jquery object
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
        <ext:Hidden ID="UsageText1" runat="server" Text="<%$ Resources:UsageCompany %>" />
        <ext:Hidden ID="UsageText2" runat="server" Text="<%$ Resources:UsageEmployee %>" />
        <ext:Hidden ID="UsageText3" runat="server" Text="<%$ Resources:UsageThirdParty %>" />
        <ext:Hidden ID="Language1" runat="server" Text="<%$ Resources:English %>" />
        <ext:Hidden ID="Language2" runat="server" Text="<%$ Resources:Arabic %>" />
        <ext:Hidden ID="currentTemplate" runat="server" />
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
                        <ext:ModelField Name="usage" />

                        <%--<ext:ModelField Name="intName" />--%>
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

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false">
                            </ext:Column>
                            <ext:Column CellCls="cellLink" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldUsage%>" DataIndex="usage" Width="200" Hideable="false">
                                <Renderer Handler="return document.getElementById('UsageText'+record.data['usage']).value;" />
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
            Width="450"
            Height="330"
            AutoShow="false"
            
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
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load" Layout="VBoxLayout"
                            BodyPadding="5">
                            <LayoutConfig>
                                <ext:VBoxLayoutConfig Align="Stretch" />
                            </LayoutConfig>
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name" AllowBlank="false" />
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" ID="usage" runat="server" FieldLabel="<%$ Resources:FieldUsage%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" Name="usage" AllowBlank="false">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:UsageCompany%>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:UsageEmployee%>" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:UsageThirdParty%>" Value="3"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                                <ext:GridPanel
                                    ID="GridPanel2"
                                    runat="server"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Title="<%$ Resources: WindowTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store
                                            ID="Store2" OnReadData="Store2_ReadData"
                                            runat="server">

                                            <Model>
                                                <ext:Model ID="Model2" runat="server" IDProperty="languageId">
                                                    <Fields>

                                                        <ext:ModelField Name="languageId" />
                                                        <ext:ModelField Name="textBody" />

                                                        <%--<ext:ModelField Name="intName" />--%>
                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                                            <Items>
                                                <ext:Button ID="AddBodyButton" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="AddNewBody">
                                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel2}.body}" />
                                                            <ExtraParams>
                                                                <ext:Parameter Name="teId" Value="#{recordId}.getValue()" Mode="Raw" />
                                                            </ExtraParams>
                                                        </Click>

                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>



                                    </TopBar>

                                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>
                                            <ext:Column ID="Column2" Visible="true" DataIndex="languageId" runat="server" Text="<%$ Resources: FieldLanguage %>" Flex="1">
                                                <Renderer Handler="return(document.getElementById('Language'+record.data['languageId']).value);" />
                                            </ext:Column>



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


                                    <Listeners>
                                        <Render Handler="this.on('cellclick', cellClick);" />
                                    </Listeners>
                                    <DirectEvents>
                                        <CellClick OnEvent="PoPuPBody">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>
                                                <ext:Parameter Name="languageId" Value="record.getId()" Mode="Raw" />

                                                <ext:Parameter Name="teId" Value="#{recordId}.getValue()" Mode="Raw" />
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

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;} var  markup = $('#summernote').summernote('code');  " />

                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="html_text" Value="escapeHtml($('#summernote').summernote('code'))" Mode="Raw" Encode="true" />
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
            ID="TemplateBodyWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:TemplateBodyWindow %>"
            Width="1000"
            Height="500"
           
            AutoShow="false"
           Maximizable="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="TemplateBodyForm" DefaultButton="SaveBody"
                            runat="server"
                            Title="<%$ Resources: TemplateBodyWindow %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" Layout="VBoxLayout"
                            BodyPadding="5">
                            <LayoutConfig>
                                <ext:VBoxLayoutConfig Align="Stretch" />
                            </LayoutConfig>
                            <Items>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" ID="languageId" runat="server" FieldLabel="<%$ Resources:FieldLanguage%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" Name="languageId" AllowBlank="false">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:English%>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Arabic%>" Value="2"></ext:ListItem>

                                    </Items>
                                </ext:ComboBox>
                                <ext:TextField ID="bodyText" Hidden="true" runat="server" />
                                <ext:TextField ID="teId" Hidden="true" runat="server" />
                                <%--<ext:TextField ID="intName" runat="server" FieldLabel="<%$ Resources:IntName%>" Name="intName"   AllowBlank="false"/>--%>
                                <ext:Panel runat="server" Layout="FitLayout" Flex="1" ID="editorHolder">
                                    <Items>
                                        <ext:Container runat="server">
                                            <Content>
                                                <div id="summernote">
                                                </div>
                                                
                                            </Content>
                                        </ext:Container>
                                    </Items>
                                    <Listeners>

                                        <AfterLayout Handler=" $('#summernote').summernote('reset'); setWidth(); var s = unescape( #{bodyText}.getValue());  $('#summernote').summernote('code',s);" />
                                        <AfterRender Handler=" App.TemplateBodyWindow.setMaxHeight(0.92*window.innerHeight);$('#summernote').summernote({
                                                height: 270,
                                              toolbar: [['mybutton', ['hello']],
            ['style', ['style']],
            ['font', ['bold', 'underline', 'clear']],
            ['fontname', ['fontname']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'video']],
            ['view', ['fullscreen', 'codeview', 'help']],
         
    
  ],

  buttons: {
    hello: employeeParams,other:companyParams
                                          
  }
                                                }); "/>
                                        <Resize Handler="setWidth();" />
                                    </Listeners>
                                </ext:Panel>
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveBody" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{TemplateBodyForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveTemplateBody" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{TemplateBodyWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="teId" Value="#{teId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="html_text" Value="escape($('#summernote').summernote('code'))" Mode="Raw" Encode="true" />
                                <ext:Parameter Name="values" Value="#{TemplateBodyForm}.getForm().getValues()" Mode="Raw" Encode="true" />
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
