<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.Contacts" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Contacts.js?id=0"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server"  />

          <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
        


        <Items>

                <ext:GridPanel AutoUpdateLayout="true"
                    ID="contactsGrid" Collapsible="false"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    
                    Layout="FitLayout"
                    Scroll="Vertical" Flex="1"
                    Border="false" 
                    Icon="User" DefaultAnchor="100%"  
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="contactStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="contactsStore_RefreshData"
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
                                        <ext:ModelField Name="rtId" />
                                        <ext:ModelField Name="rtName" />
                                        <ext:ModelField Name="workPhone" />
                                        <ext:ModelField Name="homePhone" />
                                        <ext:ModelField Name="cellPhone" />
                                        <ext:ModelField Name="email" />
                                        <ext:ModelField Name="addressId" IsComplex="true" />
                                        

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{contactsGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180" Visible="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{contactsStore}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column CellCls="cellLink" ID="ColEHName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldRTName%>" DataIndex="rtName" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldWorkPhone%>" DataIndex="WorkPhone" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldHomePhone%>" DataIndex="homePhone" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Column5" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCellPhone%>" DataIndex="cellPhone" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmail%>" DataIndex="email" Flex="2" Hideable="false" />
                            <ext:Column ID="ColName" DataIndex="addressName.fullName" Text="<%$ Resources: FieldAddressName%>" runat="server" Flex="4">
                                <Renderer Handler=" return  record.data['addressId'].street1  + '&nbsp' + record.data['addressId'].countryName " />
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

                                <Renderer handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="ColEHDelete" Flex="1" Visible="false"
                                Text=""
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
                
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick  OnEvent="PoPuP">
                            <EventMask  ShowMask="false"  />
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
            ID="EditContactWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditContactWindowTitle %>"
            Width="570"
            Height="320"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="ContactsForm" DefaultButton="SaveContactButton"
                            runat="server" Layout="TableLayout"
                            Title="<%$ Resources: EditContactWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:Panel runat="server">
                                    <Items>
  <ext:TextField runat="server" Name="recordId"  ID="recordId" Hidden="true"  Disabled="true"/>
                                <ext:TextField  runat="server" Name="institution" AllowBlank="false"  ID="institution"  FieldLabel="<%$ Resources:FieldInst%>" />

                                <ext:ComboBox ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                     DisplayField="name" runat="server" ID="rtId" Name="rtId" FieldLabel="<%$ Resources:FieldRTName%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="rtStore">
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
                                                        <ext:Button ID="Button6" runat="server" Icon="Add" Hidden="true">
                                                            <Listeners>
                                                                <Click Handler="CheckSession();  " />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                <Click OnEvent="addRT">
                                                                    <ExtraParams>
                                                                        
                                                                        
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                </ext:ComboBox>
                                
                                <ext:TextField  runat="server" Name="workPhone" AllowBlank="false"  ID="TextField1"  FieldLabel="<%$ Resources:FieldWorkPhone%>" />                                
                                <ext:TextField  runat="server" Name="homePhone" AllowBlank="false"  ID="TextField2"  FieldLabel="<%$ Resources:FieldHomePhone%>" />
                                <ext:TextField  runat="server" Name="cellPhone" AllowBlank="false"  ID="TextField3"  FieldLabel="<%$ Resources:FieldCellPhone%>" />
                                <ext:TextField  runat="server" Name="email" AllowBlank="false"  ID="TextField4"  FieldLabel="<%$ Resources:FieldEmail%>" />
                                
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items> 
                                         <ext:TextField  runat="server" Name="street1" AllowBlank="false"  ID="street1"  FieldLabel="<%$ Resources:FieldStreet1%>" />
                                <ext:TextField  runat="server" Name="street2" AllowBlank="false"  ID="street2"  FieldLabel="<%$ Resources:FieldStreet2%>" />
                                <ext:TextField  runat="server" Name="city" AllowBlank="false"  ID="city"  FieldLabel="<%$ Resources:FieldCity%>" />
                                <ext:TextField  runat="server" Name="postalCode" AllowBlank="false"  ID="postalCode"  FieldLabel="<%$ Resources:FieldPostalCode%>" />
                                <%--<ext:TextField  runat="server" Name="countryName" AllowBlank="false"  ID="countryName"  FieldLabel="<%$ Resources:FieldCountryName%>" />--%>
                                
                                 <ext:ComboBox ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                     DisplayField="name" runat="server" ID="naId" Name="naId" FieldLabel="<%$ Resources:FieldCountryName%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="naStore">
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
                                                        <ext:Button ID="Button1" runat="server" Icon="Add" Hidden="true">
                                                            <Listeners>
                                                                <Click Handler="CheckSession();  " />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                <Click OnEvent="addNA">
                                                                    <ExtraParams>
                                                                        
                                                                        
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                </ext:ComboBox>
                         

                                    </Items>
                                </ext:Panel>
                              
                                <%--<ext:ComboBox runat="server" ID="addressId"
                                    DisplayField="fullName"
                                    ValueField="recordId"
                                    TypeAhead="false"
                                    FieldLabel="<%$ Resources: FieldAddressName%>"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3"
                                    TriggerAction="Query" ForceSelection="false">
                                    <Store>
                                        <ext:Store runat="server" ID="addressStore" AutoLoad="false">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="fullName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillAddress"></ext:PageProxy>
                                            </Proxy>

                                        </ext:Store>

                                    </Store>
                                </ext:ComboBox>--%>

                                    
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveContactButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{ContactsForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveDocument" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditContactWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{ContactsForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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

       

          

    </form>
</body>
</html>


