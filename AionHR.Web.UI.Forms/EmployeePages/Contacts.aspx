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
                    ID="emergencyContactsGrid" Collapsible="false"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: ECGridTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical" Flex="1"
                    Border="false" 
                    Icon="User" DefaultAnchor="100%"  
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="emergencyContactStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="emergencyContactsStore_RefreshData"
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
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="rtId" />
                                        <ext:ModelField Name="rtName" />
                                        <ext:ModelField Name="workPhone" />
                                        <ext:ModelField Name="homePhone" />
                                        <ext:ModelField Name="cellPhone" />
                                        <ext:ModelField Name="email" />
                                        <ext:ModelField Name="name" />
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
                                        <Click OnEvent="ADDNewEC">
                                            <EventMask ShowMask="true" CustomTarget="={#{emergencyContactsGrid}.body}" />
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
                                        <TriggerClick Handler="#{emergencyContactStore}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            
                            <ext:Column CellCls="cellLink" ID="ColEHName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEMRTName%>" DataIndex="rtName" width="100" Hideable="false" />
                            <ext:Column  CellCls="cellLink" ID="Column8" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEMInst%>" DataIndex="name" width="140" Hideable="false" />
                            
                            <ext:Column CellCls="cellLink" ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEMHomePhone%>" DataIndex="homePhone" width="120" Hideable="false" />
                            
                            
                           <%-- <ext:Column ID="ColName" DataIndex="addressName.fullName" Text="<%$ Resources: FieldAddressName%>" runat="server" Flex="4">
                                <Renderer Handler=" return  record.data['addressId'].street1  + '&nbsp' + record.data['addressId'].countryName " />
                                </ext:Column>--%>

                            <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text=""
                                Width="80"
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
                        <CellClick  OnEvent="PoPuPEC">
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


                <ext:GridPanel Visible="True"
                    ID="contactGrid" AutoUpdateLayout="true" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"   
                    Header="true"
                    Title="<%$ Resources: CGridTitle %>"
                    Layout="FitLayout" Flex="1"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="contactStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="contactStore_RefreshData"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model2" runat="server" IDProperty="recordId">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="phone" />
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
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewCO">
                                            <EventMask ShowMask="true" CustomTarget="={#{contactGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                <ext:TextField ID="TextField5" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{contactGrid}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                           <ext:Column ID="Column2" Visible="false" DataIndex="recordId" runat="server" />
                            
                            <ext:Column ID="Column4" DataIndex="addressId.street1" Text="<%$ Resources: FieldCStreet1%>" runat="server" Flex="1" >
                                <Renderer Handler="return record.data['addressId'].street1;" />
                                </ext:Column>
                            
                            <ext:Column ID="Column11" DataIndex="addressId.city" Text="<%$ Resources: FieldCCity%>" runat="server" Flex="1" >
                                <Renderer Handler="return record.data['addressId'].city;" />
                                </ext:Column>
                      
                            
                            <ext:Column ID="Column13" DataIndex="phone" Text="<%$ Resources: FieldCCountryName%>" runat="server" Flex="1" />
                                                     



                            <ext:Column runat="server"
                                ID="ColCName" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return editRender()+'&nbsp;&nbsp;'+deleteRender(); " />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="ColCDelete" Flex="1" Visible="false"
                                Text=""
                                Width="100"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                              <Renderer handler="return editRender()+'&nbsp;&nbsp;'+deleteRender(); " />

                            </ext:Column>
                            <ext:Column runat="server" Visible="false"
                                ID="Column7"
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

                        <ext:Toolbar ID="Toolbar4" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar2" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                    
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPCO">
                            
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                
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
       </ext:Viewport>

        <ext:Window
            ID="EditEmergencyContactWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditEmergencyContactWindowTitle %>"
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
                            ID="EmergencyContactsForm" DefaultButton="SaveEmergencyContactButton"
                            runat="server" Layout="TableLayout"
                            Title="<%$ Resources: EditEmergencyContactWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:Panel runat="server">
                                    <Items>
  <ext:TextField runat="server" Name="recordId"  ID="recordId" Hidden="true"  Disabled="true"/>
                                <ext:TextField  runat="server" Name="name" AllowBlank="false"  ID="name"  FieldLabel="<%$ Resources:FieldEMInst%>" />

                                <ext:ComboBox ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                     DisplayField="name" runat="server" ID="rtId" Name="rtId" FieldLabel="<%$ Resources:FieldEMRTName%>" SimpleSubmit="true">
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
                                
                                <ext:TextField  runat="server" Name="workPhone" AllowBlank="false"  ID="TextField1"  FieldLabel="<%$ Resources:FieldEMWorkPhone%>" />                                
                                <ext:TextField  runat="server" Name="homePhone" AllowBlank="false"  ID="TextField2"  FieldLabel="<%$ Resources:FieldEMHomePhone%>" />
                                <ext:TextField  runat="server" Name="cellPhone" AllowBlank="false"  ID="TextField3"  FieldLabel="<%$ Resources:FieldEMCellPhone%>" />
                                <ext:TextField  runat="server" Name="email" AllowBlank="false"  ID="TextField4"  FieldLabel="<%$ Resources:FieldEMEmail%>" />
                                
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items> 
                                         <ext:TextField  runat="server" Name="street1" AllowBlank="false"  ID="street1"  FieldLabel="<%$ Resources:FieldEMStreet1%>" />
                                <ext:TextField  runat="server" Name="street2" AllowBlank="false"  ID="street2"  FieldLabel="<%$ Resources:FieldEMStreet2%>" />
                                <ext:TextField  runat="server" Name="city" AllowBlank="false"  ID="city"  FieldLabel="<%$ Resources:FieldEMCity%>" />
                                <ext:TextField  runat="server" Name="postalCode" AllowBlank="false"  ID="postalCode"  FieldLabel="<%$ Resources:FieldEMPostalCode%>" />
                                <%--<ext:TextField  runat="server" Name="countryName" AllowBlank="false"  ID="countryName"  FieldLabel="<%$ Resources:FieldCountryName%>" />--%>
                                
                                 <ext:ComboBox ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                     DisplayField="name" runat="server" ID="ecnaId" Name="ecnaId" FieldLabel="<%$ Resources:FieldEMCountryName%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="ecnaStore">
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

                                                                <Click OnEvent="addECNA">
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
                <ext:Button ID="SaveEmergencyContactButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EmergencyContactsForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveEC" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditEmergencyContactWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EmergencyContactsForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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
                <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
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
                                <ext:TextField runat="server" Name="recordId"  ID="TextField6" Hidden="true"  Disabled="true"/>
                                <ext:TextField  runat="server" Name="cellPhone" AllowBlank="false"  ID="cocellPhone"  FieldLabel="<%$ Resources:FieldCCellPhone%>" />
                                <ext:TextField  runat="server" Name="street1" AllowBlank="false"  ID="costreet1"  FieldLabel="<%$ Resources:FieldCStreet1%>" />
                                <ext:TextField  runat="server" Name="street2" AllowBlank="false"  ID="costreet2"  FieldLabel="<%$ Resources:FieldCStreet2%>" />
                                <ext:TextField  runat="server" Name="city" AllowBlank="false"  ID="cocity"  FieldLabel="<%$ Resources:FieldCCity%>" />
                                <ext:TextField  runat="server" Name="postalCode" AllowBlank="false"  ID="copostalCode"  FieldLabel="<%$ Resources:FieldCPostalCode%>" />
                                <%--<ext:TextField  runat="server" Name="countryName" AllowBlank="false"  ID="countryName"  FieldLabel="<%$ Resources:FieldCountryName%>" />--%>
                                
                                 <ext:ComboBox ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                     DisplayField="name" runat="server" ID="conaId" Name="conaId" FieldLabel="<%$ Resources:FieldCCountryName%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="conaStore">
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
                                                        <ext:Button ID="Button5" runat="server" Icon="Add" Hidden="true">
                                                            <Listeners>
                                                                <Click Handler="CheckSession();  " />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                <Click OnEvent="addCONA">
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
                              
                               

                                    
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button7" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{ContactsForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveCO" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditContactWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{ContactsForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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

       

          

    </form>
</body>
</html>


