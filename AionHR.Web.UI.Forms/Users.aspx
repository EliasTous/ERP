<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="AionHR.Web.UI.Forms.Users" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Users.js?id=1"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript">
        function SetNameEnabled( status,name)
        {
            
            
            App.fullName.setDisabled(!status);
            if (!status)
                App.fullName.setValue(name);
           

        }
    </script>

</head>
<body style="background: url(Images/bg.png) repeat;" >
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="timeZoneOffset" runat="server" EnableViewState="true" />
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="True"
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
                        <ext:ModelField Name="fullName" />
                        <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="languageId" />
                        <ext:ModelField Name="email" />


                        <ext:ModelField Name="isInactive" />
                        <ext:ModelField Name="isAdmin" />




                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
                <ext:DataSorter Property="name" Direction="ASC" />
                <ext:DataSorter Property="reference" Direction="ASC" />
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

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Visible="false" ID="ColEmployeeId" MenuDisabled="true" runat="server" DataIndex="employeeId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Visible="false" ID="ColPassword" MenuDisabled="true" runat="server" DataIndex="password" Hideable="false" Width="75" Align="Center" />
                            
                            <ext:Column ID="ColFullName" MenuDisabled="true" Sortable="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="fullName" Flex="1" Hideable="false">
                                <Renderer Handler="return '<u>'+ record.data['fullName']+'</u>'">
                                </Renderer>
                            </ext:Column>

                            <ext:Column Sortable="true" ID="ColEmail" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmail%>" DataIndex="email" Flex="1" Hideable="false" />

                            <ext:CheckColumn ID="ColIsInactive" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIsInactive %>" DataIndex="isInactive" Hideable="false" />
                            <ext:CheckColumn ID="ColIsAdmin" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIsAdmin %>" DataIndex="isAdmin" Hideable="false" />



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
            Height="350"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                       <ext:FormPanel DefaultButton="SaveButton"
                            ID="BasicInfoTab"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="fullName" runat="server" FieldLabel="<%$ Resources: FieldFullName %>" DataIndex="fullName" AllowBlank="false" />
                                <ext:TextField ID="email" runat="server" FieldLabel="<%$ Resources: FieldEmail %>" DataIndex="email" InputType="Email" Vtype="email" AllowBlank="false" />
                                <ext:TextField ID="recordId" Disabled="true" Hidden="true" runat="server" DataIndex="recordId" AllowBlank="false" />


                                <ext:Checkbox ID="isInactiveCheck" runat="server" FieldLabel="<%$ Resources: FieldIsInActive%>" DataIndex="isInactive" Name="isInactive" InputValue="true" />
                                <ext:Checkbox ID="isAdminCheck" runat="server" FieldLabel="<%$ Resources: FieldIsAdmin%>" DataIndex="isAdmin" Name="isAdmin" InputValue="true" />
                                <ext:ComboBox runat="server" ID="employeeId"
                                    DisplayField="fullName"
                                    ValueField="recordId"
                                    TypeAhead="false"
                                    FieldLabel="<%$ Resources: FieldEmployeeFullName%>"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3"
                                    TriggerAction="Query" ForceSelection="false">
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
                                                <ext:PageProxy DirectFn="App.direct.FillSupervisor"></ext:PageProxy>
                                            </Proxy>

                                        </ext:Store>

                                    </Store>

                                    <Listeners>
                                        <Select Handler="App.direct.SetFullName();" />
                                        <FocusLeave Handler=" if(this.value==null|| isNaN(this.value) )SetNameEnabled(true,'');  if(isNaN(this.value)) this.setValue(null);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox runat="server" ID="languageId" AllowBlank="false"
                                    SubmitValue="true"
                                    TypeAhead="false"
                                    FieldLabel="<%$ Resources: FieldLanguageId%>">
                                    <Items>
                                        <ext:ListItem Text="<%$Resources:Common,EnglishLanguage %>" Value="1" />
                                        <ext:ListItem Text="<%$Resources:Common,ArabicLanguage %>" Value="3" />

                                    </Items>
                                </ext:ComboBox>

                                <ext:TextField
                                    ID="PasswordField"
                                    runat="server"
                                    FieldLabel="<%$ Resources: FieldPassword%>"
                                    InputType="Password"
                                    Name="password"
                                    DataIndex ="password"
                                    AllowBlank="false"
                                    AnchorHorizontal="100%">
                                    <Listeners>
                                        <ValidityChange Handler="this.next().validate();" />
                                        <Blur Handler="this.next().validate();" />
                                    </Listeners>
                                </ext:TextField>
                                <ext:TextField
                                    ID="PasswordConfirmation" 
                                    runat="server"
                                    Vtype="password"
                                    FieldLabel="<%$ Resources: FieldConfirmPassword%>"
                                    InputType="Password"
                                    MsgTarget="Side"
                                    AnchorHorizontal="100%">
                                    <CustomConfig>
                                        <ext:ConfigItem Name="initialPassField" Value="PasswordField" Mode="Value" />
                                    </CustomConfig>
                                </ext:TextField>
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) { return false;}" />
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



    </form>
</body>
</html>

