<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Branches.aspx.cs" Inherits="AionHR.Web.UI.Forms.Branches" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Branches.js?id=1" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
    <script  type="text/javascript">
        var GetTimeZone = function () {
            
            var d = new Date();


            var n = d.getTimezoneOffset();
            
          
            
            s = n / -60;
            App.direct.StoreTimeZone(s);
            
        }
        function setNullable(d)
        {
            App.city.allowBlank = d;
            App.costreet1.allowBlank = d;
            App.stId.allowBlank=d;
            App.naId.allowBlank = d;
            App.phone.allowBlank = d;
        }
        function openInNewTab() {
            window.document.forms[0].target = '_blank';

        }
    </script>
    
       
  

 
</head>
<body style="background: url(Images/bg.png) repeat;" onload="GetTimeZone();">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="timeZoneOffset" runat="server" EnableViewState="true" />
          <ext:Hidden ID="Yes" runat="server" Text="<%$ Resources:Common , Yes %>"  />
          <ext:Hidden ID="No" runat="server" Text="<%$ Resources:Common , No %>"  />
          <ext:Hidden ID="branchId" runat="server" Text=""  />
        
      
      
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App" >
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
                        <ext:ModelField Name="reference" />
                        <ext:ModelField Name="timeZone" />
                        <ext:ModelField Name="segmentCode" />
                          <ext:ModelField Name="scName" />
                        <ext:ModelField Name="isInactive" Type="Boolean" DefaultValue="false"/>
                         
                       


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
                                <ext:Button ID="btnReload" runat="server"  Icon="Reload">       
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
                                 <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180" >
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                            <TriggerClick Handler="#{Store1}.reload();" />
                                        </Listeners>
                                    </ext:TextField>
                                  <ext:Button runat="server" Icon="Printer">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server"  Text="<%$ Resources:Common , Print %>" AutoPostBack="true" OnClick="printBtn_Click" OnClientClick="openInNewTab();"  >
                                            
                                                    <Listeners>
                                                        <Click Handler="openInNewTab();" />
                                                    </Listeners>
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server"  Text="Pdf" AutoPostBack="true" OnClick="ExportPdfBtn_Click"  >
                                            
                                                    
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server"  Text="Excel" AutoPostBack="true" OnClick="ExportXLSBtn_Click"  >
                                            
                                                    
                                                </ext:MenuItem>
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>
                              
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>

                              <ext:Column  Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" width="75" Align="Center"/>
                            <ext:Column ID="ColReference" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldReference%>" DataIndex="reference" width="150" Hideable="false"/>
                            <ext:Column   CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false">
                        
                                </ext:Column>
                            <ext:Column ID="ColTimeZone" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTimeZone%>" DataIndex="timeZone" width="100" Hideable="false">
                                <Renderer Handler="var sign = ''; if(record.data['timeZone']>=0) sign = '+'; return 'UTC '+sign + record.data['timeZone'] + ':00 ' " />     
                                </ext:Column>
                      
                            <ext:Column   CellCls="cellLink" ID="scName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSchedule%>" DataIndex="scName" Flex="2" />
                            <ext:CheckColumn ID="ColInactive" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIsInactive %>" DataIndex="isInactive" Width="100" Hideable="false" />
                               <ext:Column   CellCls="cellLink" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldWorkingCalendar%>" DataIndex="caName" Flex="2" />
                        
                               

                         

                        
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
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single"  StopIDModeInheritance="true" />
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
                                <ext:TextField ID="recordId" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Disabled="true" Name="recordId" />
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                <ext:TextField ID="reference" runat="server" FieldLabel="<%$ Resources: FieldReference %>" DataIndex="reference"  />
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="timeZoneCombo"  AllowBlank="false" SimpleSubmit="true" IDMode="Static" Name="timeZone" FieldLabel="<%$ Resources:FieldTimeZone%>" QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" >
                                    <Items>
                                        <ext:ListItem Text="-12 UTC" Value="-12" />
                                        <ext:ListItem Text="-11 UTC" Value="-11" />
                                        <ext:ListItem Text="-10 UTC" Value="-10" />
                                        <ext:ListItem Text="-9 UTC" Value="-9" />
                                        <ext:ListItem Text="-8 UTC" Value="-8" />
                                        <ext:ListItem Text="-7 UTC" Value="-7" />
                                        <ext:ListItem Text="-6 UTC" Value="-6" />
                                        <ext:ListItem Text="-5 UTC" Value="-5" />
                                        <ext:ListItem Text="-4 UTC" Value="-4" />
                                        <ext:ListItem Text="-3 UTC" Value="-3" />
                                        <ext:ListItem Text="-2 UTC" Value="-2" />
                                        <ext:ListItem Text="-1 UTC" Value="-1" />
                                        <ext:ListItem Text=" UTC" Value="0" />
                                        <ext:ListItem Text="+1 UTC" Value="1" />
                                        <ext:ListItem Text="+2 UTC" Value="2" />
                                        <ext:ListItem Text="+3 UTC" Value="3" />
                                        <ext:ListItem Text="+4 UTC" Value="4" />
                                        <ext:ListItem Text="+5 UTC" Value="5" />
                                        <ext:ListItem Text="+6 UTC" Value="6" />
                                        <ext:ListItem Text="+7 UTC" Value="7" />
                                        <ext:ListItem Text="+8 UTC" Value="8" />
                                        <ext:ListItem Text="+9 UTC" Value="9" />
                                        <ext:ListItem Text="+10 UTC" Value="10" />
                                        <ext:ListItem Text="+11 UTC" Value="11" />
                                        <ext:ListItem Text="+12 UTC" Value="12" />
                                    </Items>
                                </ext:ComboBox>
                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="scId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" Name="scId" FieldLabel="<%$ Resources:FieldSchedule%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="scheduleStore">
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
                                 <ext:ComboBox   AnyMatch="true"  CaseSensitive="false"  runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="caId" Name="caId" FieldLabel="<%$ Resources:FieldWorkingCalendar%>" >
                                
                                    <Store>
                                        <ext:Store  runat="server" ID="Store3" >
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
                                
                                <ext:Checkbox ID="isInactive" runat="server" FieldLabel="<%$ Resources: FieldIsInactive%>" Name="isInactive" InputValue="true" />
                               

                            </Items>

                        </ext:FormPanel>
                        <ext:FormPanel runat="server" ID="addressForm" Title="<%$ Resources: AddressPage %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5"> 
                            <Items>
                                <ext:TextField runat="server" Name="addressId" ID="addressId" Hidden="true" Disabled="true" />
                                        <ext:TextField runat="server" Name="street1" AllowBlank="true" ID="costreet1" FieldLabel="<%$ Resources:FieldStreet1%>" >
                                            <Listeners>
                                                <Change Handler="if(this.value=='') setNullable(true); else setNullable(false);" />
                                            </Listeners>
                                            </ext:TextField>
                                             
                                        <ext:TextField runat="server" Name="street2" AllowBlank="true" ID="street2" FieldLabel="<%$ Resources:FieldStreet2%>" />
                                        <ext:TextField runat="server" Name="city" AllowBlank="true" ID="city" FieldLabel="<%$ Resources:FieldCCity%>" >
                                            <Listeners>
                                                <Change Handler="if(this.value=='') setNullable(true); else setNullable(false);" />
                                            </Listeners>
                                            </ext:TextField>
                                        <ext:TextField runat="server" Name="postalCode" AllowBlank="true" MaxLength="6" ID="postalCode" FieldLabel="<%$ Resources:FieldCPostalCode%>" />
                                        <%--<ext:TextField  runat="server" Name="countryName" AllowBlank="false"  ID="countryName"  FieldLabel="<%$ Resources:FieldCountryName%>" />--%>
                                         <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ValueField="recordId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                            DisplayField="name" runat="server" ID="stId" Name="stId" FieldLabel="<%$ Resources:FieldState%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="stStore">
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
                                                <ext:Button ID="Button4" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addST">
                                                            <ExtraParams>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                <Select Handler="if(this.value=='') setNullable(true); else setNullable(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ValueField="recordId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                            DisplayField="name" runat="server" ID="naId" Name="naId" FieldLabel="<%$ Resources:FieldCCountryName%>" SimpleSubmit="true">
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
                                                <ext:Button ID="Button5" runat="server" Icon="Add" Hidden="true">
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
                                                 <Select Handler="if(this.value=='') setNullable(true); else setNullable(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                 <ext:TextField runat="server" Name="phone" AllowBlank="true" ID="phone" FieldLabel="<%$ Resources:phone%>" >
                                      <Validator Handler="return !isNaN(this.value);" />
                                     </ext:TextField>


                            </Items>
                        </ext:FormPanel>
                        <ext:GridPanel
                            ID="legalReferenceGrid" AutoUpdateLayout="true" Collapsible="true"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="true"
                            Title="<%$ Resources:legalReferenceWindowTitle %> "
                            Layout="FitLayout"
                            Flex="1"
                            Scroll="Vertical"
                            Border="false"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                <ext:Store
                                    ID="legalReferenceStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="true"
                                    OnReadData="legalReference_RefreshData"
                                    PageSize="50" IDMode="Explicit" Namespace="App">
                                     <Model>
                                        <ext:Model ID="Model2" runat="server">
                                            <Fields>

                                                <ext:ModelField Name="goId" />
                                                <ext:ModelField Name="goName" />
                                                <ext:ModelField Name="reference" />
                                                <ext:ModelField Name="releaseDate" />






                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Sorters>
                                        <ext:DataSorter Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <TopBar>
                            <%--    <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                                    <Items>
                                        
                                      

                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                        <ext:TextField ID="TextField1" runat="server" EnableKeyEvents="true" Width="180">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                                <TriggerClick Handler="#{socialSetupGrid}.reload();" />
                                            </Listeners>
                                        </ext:TextField>

                                    </Items>
                                </ext:Toolbar>--%>

                            </TopBar>

                            <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>

                                    <ext:Column Visible="false" ID="goId" MenuDisabled="true" runat="server" Text="goId" DataIndex="goId" Hideable="false" Width="75" Align="Center" />



                                    <ext:Column ID="goName" MenuDisabled="true" runat="server" Text="<%$ Resources:goName%>" DataIndex="goName" Hideable="false" Flex="1" Align="Center" />
                                    <ext:Column ID="referenceCol" MenuDisabled="true" runat="server" Text="<%$ Resources:FieldReference%> " DataIndex="reference" Hideable="false" Flex="1" Align="Center" />
                                    <ext:DateColumn ID="releaseDate" MenuDisabled="true" runat="server" Text=" <%$ Resources:releaseDate%> " DataIndex="releaseDate" Hideable="false" Flex="1" Align="Center" />



                                    <ext:Column runat="server"
                                        ID="Column1" Visible="false"
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
                                        Visible="false"
                                        ID="colAttachSetupGrid"
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
                                        ID="Column3" Visible="true"
                                        Text=""
                                        Width="100"
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
                                        <ext:StatusBar ID="StatusBar2" runat="server" />
                                        <ext:ToolbarFill />

                                    </Items>
                                </ext:Toolbar>

                            </DockedItems>

                            <Listeners>
                                <Render Handler="this.on('cellclick', cellClick);" />
                            </Listeners>
                            <DirectEvents>
                                <CellClick OnEvent="PoPuPlegalReference">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="id" Value="record.data['goId']" Mode="Raw" />
                                       
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
                    
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()|| !#{addressForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{BasicInfoTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                                <ext:Parameter Name="address" Value ="#{addressForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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
            ID="EditlegalReferenceWindow"
            runat="server"
            Icon="PageEdit"
            Title=" <%$ Resources:EditlegalReferenceWindowTitle %>"
            Width="400"
            Height="300"
            AutoShow="false"
            Modal="true"
            Closable="false"
            Resizable="false"
            Floatable="false"
            Draggable="false"
            FocusOnToFront="true"
            Header="false"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="legalReferenceForm" DefaultButton="saveLegalReference"
                            runat="server"
                            Title="<%$ Resources:EditlegalReferenceWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" Layout="AutoLayout"
                            BodyPadding="5">
                            <Items>

                                  <ext:TextField ID="goIdd" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Disabled="true" Name="goId" />
                              <ext:TextField ID="branchIdd" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Disabled="true" Name="goId" />
                                <ext:TextField  ID="goNameTF" runat="server" ReadOnly="true" FieldLabel="<%$ Resources:goName %>"  Name="goName" />
                                 <ext:TextField   ID="referenceTF"  runat="server" FieldLabel="<%$ Resources:FieldReference %>" Name="reference" />
                                <ext:DateField   ID="releaseDateDF" AllowBlank="false"   runat="server" FieldLabel="<%$ Resources:releaseDate %>" Name="releaseDate" />
                         
                                 
                               


                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="saveLegalbutton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{legalReferenceForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="saveLegalRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditlegalReferenceWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{goIdd}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{legalReferenceForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

        


    </form>
</body>
</html>
