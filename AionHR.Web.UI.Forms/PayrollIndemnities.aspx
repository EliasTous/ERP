<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollIndemnities.aspx.cs" Inherits="AionHR.Web.UI.Forms.PayrollIndemnities" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
   
    <script type="text/javascript" src="Scripts/common.js"></script>
   
    <script type="text/javascript" src="Scripts/Payrollindemnities.js?id=95"></script>
</head>
<body style="background: url(Images/bg.png) repeat;" >
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        
        <ext:Hidden runat="server" ID="deleteDisabled" />
        <ext:Hidden runat="server" ID="editDisabled" />

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
                        <ext:ModelField Name="exemptMarriagePrd" />
                        <ext:ModelField Name="exemptDeliveryPrd" />




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

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server"  DataIndex="recordId" Hideable="false" Width="75" Align="Center" />

                            <ext:Column CellCls="cellLink" Sortable="true" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="1" Hideable="false">
                               <%-- <Renderer Handler="return record.data['name']">
                                </Renderer>--%>
                            </ext:Column>
                              <ext:Column CellCls="cellLink" Sortable="true" ID="exemptMarriagePrdCol" MenuDisabled="true" runat="server" Text="<%$ Resources: exemptMarriagePeriod%>" DataIndex="exemptMarriagePrd" Flex="1" Hideable="false">
                               <%-- <Renderer Handler="return record.data['name']">
                                </Renderer>--%>
                            </ext:Column>
                              <ext:Column CellCls="cellLink" Sortable="true" ID="exemptDeliveryPrdCol" MenuDisabled="true" runat="server" Text="<%$ Resources: exemptDeliveryPeriod%>" DataIndex="exemptDeliveryPrd" Flex="1" Hideable="false">
                               <%-- <Renderer Handler="return record.data['name']">
                                </Renderer>--%>
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
            Width="600"
            Height="330"
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
                                <ext:TextField ID="recordId" Hidden="true" runat="server"  Disabled="true" DataIndex="recordId" />
                                <ext:TextField LabelWidth="150" ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" DataIndex="name" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                 <ext:TextField LabelWidth="150" ID="exemptMarriagePrd" runat="server" FieldLabel="<%$ Resources: exemptMarriagePeriod%>" Name="exemptMarriagePrd" AllowBlank="false" BlankText="0"  />
                                 <ext:TextField LabelWidth="150" ID="exemptDeliveryPrd" runat="server" FieldLabel="<%$ Resources: exemptDeliveryPeriod%> " Name="exemptDeliveryPrd" AllowBlank="false" BlankText="0 " />

                            </Items>

                        </ext:FormPanel>
                        <ext:FormPanel
                            ID="periodsTab"
                            runat="server"
                            Title="<%$ Resources: PeriodsTab %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:GridPanel
                                    ID="periodsGrid"  
                                    runat="server"
                                    Width="600" Header="false"
                                    Height="210" Layout="FitLayout"
                                    Frame="true" TitleCollapse="true"  Scroll="Vertical"
                                    >
                                    <Store>
                                        <ext:Store ID="periodsStore" runat="server">
                                           <Model>
                                                <ext:Model runat="server" Name="Employee">
                                                    <Fields>
                                                        <ext:ModelField Name="from"  />
                                                        <ext:ModelField Name="to" />
                                                        <ext:ModelField Name="pct" />
                                                       
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Plugins>
                                        <ext:RowEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" SaveBtnText="<%$ Resources:Common , Save %>" CancelBtnText="<%$ Resources:Common , Cancel %>" >
                                            <Listeners>
                                                <BeforeEdit Handler="if(App.editDisabled.value=='1') return false;" />
                                            </Listeners>
                                            </ext:RowEditing>
                                    </Plugins>
                                    <TopBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:Button runat="server" Text="<%$ Resources: BtnAddPeriod %>" Icon="UserAdd" ID="addPeriod">
                                                    <Listeners>
                                                        <Click Fn="addEmployee" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button
                                                    ID="btnRemoveEmployee"
                                                    runat="server"
                                                    Text="<%$ Resources:Common , Delete %>" 
                                                    Icon="UserDelete"
                                                    Disabled="true">
                                                    <Listeners>
                                                        <Click Fn="removeEmployee" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:RowNumbererColumn runat="server" Width="25" />
                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:From %>" 
                                                DataIndex="from"
                                                 Flex="1"
                                                Align="Center">
                                                <Editor>
                                                     <%-- Vtype="numberrange"
                                                        EndNumberField="toField"--%>
                                                    <ext:TextField
                                                        runat="server"
                                                         ID="fromField"
                                                        AllowBlank="false"
                                                        InvalidText="<%$Resources:MonthsFieldError %>"
                                                         >
                                                        <Validator Handler="if(isNaN(this.value)) return false; return true;">
                                                            
                                                        </Validator>
                                                        </ext:TextField>
                                                </Editor>
                                            </ext:NumberColumn>
                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:To %>" 
                                                DataIndex="to"
                                                 Flex="1"
                                                Align="Center">
                                                <Editor>
                                                       <%--   StartNumberField="fromField"
                                                         Vtype="numberrange"--%>
                                                    <ext:TextField
                                                        runat="server"
                                                        ID="toField"
                                                        AllowBlank="false"
                                                        
                                                         InvalidText="<%$Resources:MonthsFieldError %>"
                                                        >
                                                        <Validator Handler="if(isNaN(this.value)) return false; return true;"/>
                                                        </ext:TextField>

                                                </Editor>
                                                
                                            </ext:NumberColumn>
                                            
                                            
                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$Resources:percentage %>" 
                                                DataIndex="pct"
                                                 Flex="1"
                                                Align="Center" >
                                                <Editor>
                                                    <ext:TextField
                                                        runat="server"
                                                        AllowBlank="false" 
                                                         >
                                                         <Validator Handler="if((!isNaN(this.value))&this.value<=100&this.value>=0) return true; return false;"/>
                                                        </ext:TextField>
                                                </Editor>
                                            </ext:NumberColumn>
                                            
                                        </Columns>
                                    </ColumnModel>
                                    <Listeners>
                                        <SelectionChange Handler="if(App.deleteDisabled.value=='1') return; App.btnRemoveEmployee.setDisabled(!selected.length);" />
                                    </Listeners>
                                </ext:GridPanel>
                            </Items>

                        </ext:FormPanel>
                        <ext:FormPanel
                            ID="IndemnityRecognitionForm"
                            runat="server"
                            Title="<%$ Resources: IndemnityRecognitiontabs %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:GridPanel
                                    ID="IndemnityRecognitionGrid"  
                                    runat="server"
                                    Width="600" Header="false"
                                    Height="210" Layout="FitLayout"
                                    Frame="true" TitleCollapse="true"  Scroll="Vertical"
                                    >
                                    <Store>
                                        <ext:Store ID="IndemnityRecognitionStore" runat="server">
                                           <Model>
                                                <ext:Model runat="server" Name="Employee">
                                                    <Fields>
                                                        <ext:ModelField Name="from"  />
                                                        <ext:ModelField Name="to" />
                                                        <ext:ModelField Name="pct" />
                                                          <ext:ModelField Name="inId" />
                                                          <ext:ModelField Name="seqNo" />
                                                                                                                                                                      
                                                       
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Plugins>
                                        <ext:RowEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" SaveBtnText="<%$ Resources:Common , Save %>" CancelBtnText="<%$ Resources:Common , Cancel %>" >
                                            <Listeners>
                                                <BeforeEdit Handler="if(App.editDisabled.value=='1') return false;" />
                                            </Listeners>
                                            </ext:RowEditing>
                                    </Plugins>
                                    <TopBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:Button runat="server" Text="<%$ Resources: BtnAddPeriod %>" Icon="UserAdd" ID="btnAddResignation">
                                                    <Listeners>
                                                        <Click Fn="addResignation" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button
                                                    ID="btnRemoveResignation"
                                                    runat="server"
                                                    Text="<%$ Resources:Common , Delete %>" 
                                                    Icon="UserDelete"
                                                    Disabled="true">
                                                    <Listeners>
                                                        <Click Fn="removeResignation" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:RowNumbererColumn runat="server" Width="25" />
                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:From %>" 
                                                DataIndex="from"
                                                 Flex="1"
                                                Align="Center">
                                                <Editor>
                                                     <%-- Vtype="numberrange"
                                                        EndNumberField="toField"--%>
                                                    <ext:TextField
                                                        runat="server"
                                                         ID="TextField1"
                                                        AllowBlank="false"
                                                        InvalidText="<%$Resources:MonthsFieldError %>"
                                                         >
                                                        <Validator Handler="if(isNaN(this.value)) return false; return true;">
                                                            
                                                        </Validator>
                                                        </ext:TextField>
                                                </Editor>
                                            </ext:NumberColumn>
                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:To %>" 
                                                DataIndex="to"
                                                 Flex="1"
                                                Align="Center">
                                                <Editor>
                                                       <%--   StartNumberField="fromField"
                                                         Vtype="numberrange"--%>
                                                    <ext:TextField
                                                        runat="server"
                                                        ID="TextField2"
                                                        AllowBlank="false"
                                                        
                                                         InvalidText="<%$Resources:MonthsFieldError %>"
                                                        >
                                                        <Validator Handler="if(isNaN(this.value)) return false; return true;"/>
                                                        </ext:TextField>

                                                </Editor>
                                                
                                            </ext:NumberColumn>
                                            
                                            
                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$Resources:IndemnityPercentage %>" 
                                                DataIndex="pct"
                                                 Flex="1"
                                                Align="Center" >
                                                <Editor>
                                                    <ext:TextField
                                                        runat="server"
                                                        AllowBlank="false" 
                                                         >
                                                         <Validator Handler="if((!isNaN(this.value))&this.value<=100&this.value>=0) return true; return false;"/>
                                                        </ext:TextField>
                                                </Editor>
                                            </ext:NumberColumn>
                                            
                                        </Columns>
                                    </ColumnModel>
                                    <Listeners>
                                        <SelectionChange Handler="if(App.deleteDisabled.value=='1') return; App.btnRemoveResignation.setDisabled(!selected.length);" />
                                    </Listeners>
                                </ext:GridPanel>
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
                                <ext:Parameter Name="periods" Value="Ext.encode(#{periodsGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw"  />
                                 <ext:Parameter Name="indemnities" Value="Ext.encode(#{IndemnityRecognitionGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw"  />
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


