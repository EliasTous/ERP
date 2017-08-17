<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Overtime_Lateness.aspx.cs" Inherits="AionHR.Web.UI.Forms.Overtime_Lateness" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Routers.js"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />

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
                        <ext:ModelField Name="aEnable" />
                        <ext:ModelField Name="aPeriodDays" />
                        <ext:ModelField Name="aMultiplier" />
                        <ext:ModelField Name="mEnable" />
                        <ext:ModelField Name="mPeriodDays" />
                        <ext:ModelField Name="mMultiplier" />
                        <ext:ModelField Name="dEnable" />
                        <ext:ModelField Name="dPeriodDays" />
                        <ext:ModelField Name="dMultiplier" />
                        <ext:ModelField Name="oEnable" />
                        <ext:ModelField Name="oPeriodHours" />
                        <ext:ModelField Name="oMultiplier" />
                        <ext:ModelField Name="lEnable" />
                        <ext:ModelField Name="lPeriodHours" />
                        <ext:ModelField Name="lAllowance" />
                        <ext:ModelField Name="lMultiplier" />
                        <ext:ModelField Name="name" />


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

                            <ext:Column CellCls="cellLink" ID="name" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false" />








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
                                ID="colDelete"  Visible="true"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="80"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
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
            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
            Width="450"
            Height="550"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
              
                        <ext:FormPanel DefaultButton="SaveButton"
                            ID="BasicInfoTab"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5"
                            AutoScroll="true">
                            <Items>
                                 <ext:TextField LabelWidth="300" Width="360" ID="recordId" runat="server"   Hidden="true" />
                                <ext:TextField LabelWidth="150" Width="360" ID="TextField1" runat="server" FieldLabel="<%$ Resources:schedualName%>" Name="name" flex="2"/>

                                <ext:FieldSet runat="server" Title="<%$Resources:Overtime %>">
                                    <Items>
                                        <ext:Checkbox LabelWidth="300" runat="server" ID="oEnable" Name="oEnable" InputValue="true" FieldLabel="<%$ Resources:lateness %>">
                                            <Listeners>
                                                <AfterRender Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);" />
                                                <Change Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);" />
                                            </Listeners>
                                        </ext:Checkbox>
                                        <ext:TextField LabelWidth="300" Width="360" ID="oPeriodHours" runat="server" FieldLabel="<%$ Resources:FieldPeriodHours%>" Name="oPeriodHours" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0" />
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="300" Width="360" ID="oMultiplier" runat="server" FieldLabel="<%$ Resources:FieldMultiplier%>" Name="oMultiplier" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0&&this.value<10" />
                                        </ext:TextField>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet runat="server" Title="<%$Resources:lateness %>">
                                    <Items>
                                        <ext:Checkbox LabelWidth="300" runat="server" ID="lEnable" Name="lEnable" InputValue="true" FieldLabel="<%$ Resources:lateness %>">
                                            <Listeners>
                                                <AfterRender Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);this.next().next().next().setDisabled(!this.value);" />
                                                <Change Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);this.next().next().next().setDisabled(!this.value);" />
                                            </Listeners>
                                        </ext:Checkbox>
                                        <ext:TextField LabelWidth="300" Width="360" ID="lPeriodHours" runat="server" FieldLabel="<%$ Resources:FieldPeriodHours%>" Name="lPeriodHours" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0" />
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="300" Width="360" ID="lMultiplier" runat="server" FieldLabel="<%$ Resources:FieldMultiplier%>" Name="lMultiplier" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0 &&this.value<10" />
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="300" Width="360" ID="lAllowance" runat="server" FieldLabel="<%$ Resources:AllowedLateness%>" Name="lAllowance" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0" />
                                        </ext:TextField>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet runat="server" Title="<%$Resources:Absence %>">
                                    <Items>
                                        <ext:Checkbox LabelWidth="300" runat="server" ID="aEnable" Name="aEnable" InputValue="true" FieldLabel="<%$ Resources:absence %>">
                                            <Listeners>
                                                <AfterRender Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);" />
                                                <Change Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);" />
                                            </Listeners>
                                        </ext:Checkbox>
                                        <ext:TextField LabelWidth="300" Width="360" ID="aPeriodDays" runat="server" FieldLabel="<%$ Resources:FieldPeriodDays%>" Name="aPeriodDays" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0" />
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="300" Width="360" ID="aMultiplier" runat="server" FieldLabel="<%$ Resources:FieldMultiplier%>" Name="aMultiplier" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0 &&this.value<10" />
                                        </ext:TextField>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet runat="server" Title="<%$Resources:disappear %>">
                                    <Items>
                                        <ext:Checkbox LabelWidth="300" runat="server" ID="dEnable" Name="dEnable" InputValue="true" FieldLabel="<%$ Resources:disappear %>">
                                            <Listeners>
                                                <AfterRender Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);" />
                                                <Change Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);" />
                                            </Listeners>
                                        </ext:Checkbox>
                                        <ext:TextField LabelWidth="300" Width="360" ID="dPeriodDays" runat="server" FieldLabel="<%$ Resources:FieldPeriodDays%>" Name="dPeriodDays" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0" />
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="300" Width="360" ID="dMultiplier" runat="server" FieldLabel="<%$ Resources:FieldMultiplier%>" Name="dMultiplier" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0 &&this.value<10" />
                                        </ext:TextField>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet runat="server" Title="<%$Resources:missingPunch %>">
                                    <Items>
                                        <ext:Checkbox LabelWidth="300" runat="server" ID="mEnable" Name="mEnable" InputValue="true" FieldLabel="<%$ Resources:missingPunch %>">
                                            <Listeners>
                                                <AfterRender Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);" />
                                                <Change Handler="this.next().setDisabled(!this.value);this.next().next().setDisabled(!this.value);" />
                                            </Listeners>
                                        </ext:Checkbox>
                                        <ext:TextField LabelWidth="300" Width="360" ID="mPeriodDays" runat="server" FieldLabel="<%$ Resources:FieldPeriodDays%>" Name="mPeriodDays" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0" />
                                        </ext:TextField>
                                        <ext:TextField LabelWidth="300" Width="360" ID="mMultiplier" runat="server" FieldLabel="<%$ Resources:FieldMultiplier%>" Name="mMultiplier" EmptyText="0">
                                            <Validator Handler="return !isNaN(this.value)&& this.value>=0 &&this.value<10" />
                                        </ext:TextField>
                                    </Items>
                                </ext:FieldSet>










                            </Items>

                        </ext:FormPanel>

                
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
