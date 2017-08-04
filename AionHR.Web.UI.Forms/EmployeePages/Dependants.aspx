<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dependants.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.Dependants" %>

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
    <script type="text/javascript">
        function setNullable(d) {
            App.city.allowBlank = d;
            App.street1.allowBlank = d;
            App.stateId.allowBlank = d;
            App.countryId.allowBlank = d;
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
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="EmployeeTerminated" runat="server" />
        <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>



            <Items>
                <ext:GridPanel Visible="True"
                    ID="dependandtsGrid" AutoUpdateLayout="true" Collapsible="false"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Layout="FitLayout" Flex="1"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="dependandtsStore"
                            runat="server"
                            RemoteSort="False"
                            RemoteFilter="true"
                            OnReadData="dependandtsStore_RefreshData"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model2" runat="server" IDProperty="seqNo">
                                    <Fields>

                                        <ext:ModelField Name="seqNo" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="dependencyType" />
                                        <ext:ModelField Name="firstName" />
                                        <ext:ModelField Name="middleName" />
                                        <ext:ModelField Name="lastName" />
                                        <ext:ModelField Name="birthDate" />
                                        <ext:ModelField Name="gender" />
                                        <ext:ModelField Name="phoneNumber" />
                                        <ext:ModelField Name="isStudent" />
                                        <ext:ModelField Name="isCitizen" />
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
                                        <Click OnEvent="ADDNew">
                                            <EventMask ShowMask="true" CustomTarget="={#{dependandtsGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFill1" runat="server" />


                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column runat="server" DataIndex="seqNo" Visible="false" />
                            <ext:Column runat="server" Text="<%$ Resources: FieldFullName %>" Flex="1">
                                <Renderer Handler="return record.data['firstName'] +'&nbsp;'+ record.data['lastName']" />
                            </ext:Column>
                            <ext:DateColumn runat="server" ID="birthDateCol" Text="<%$ Resources: FieldBirthDate %>" DataIndex="birthDate" Width="100" />

                         
                           
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
                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;'+deleteRender(); " />

                            </ext:Column>
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

                                <Renderer Handler="var d =(App.EmployeeTerminated.value=='0')?deleteRender():' '; return editRender()+'&nbsp;&nbsp;' +d; " />

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
                        <CellClick OnEvent="PoPuP">

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
            ID="EditContactWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditContactWindowTitle %>"
            Width="340"
            Height="400"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Resizable="false"
            Maximizable="false"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="infoForm" DefaultButton="SaveContactButton"
                            runat="server" 
                            Title="<%$ Resources: EditContactWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                
                                        <ext:TextField runat="server" Name="recordId" ID="recordId" Hidden="true" Disabled="true" />
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="dependencyType" Name="dependencyType" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false"
                                            FieldLabel="<%$ Resources: FieldDependency %>">
                                            <Items>

                                                <ext:ListItem Text="<%$ Resources: Spouse %>" Value="1" />
                                                <ext:ListItem Text="<%$ Resources: Child %>" Value="2" />
                                                <ext:ListItem Text="<%$ Resources: DomesticPartner %>" Value="3" />
                                                <ext:ListItem Text="<%$ Resources: StepChild %>" Value="4" />
                                                <ext:ListItem Text="<%$ Resources: FosterChild %>" Value="5" />
                                            </Items>

                                        </ext:ComboBox>
                                        <ext:TextField runat="server" Name="firstName" AllowBlank="false" ID="firstName" FieldLabel="<%$ Resources:FieldFirstName%>" />
                                        <ext:TextField runat="server" Name="middleName" AllowBlank="true" ID="middleName" FieldLabel="<%$ Resources:FieldMiddleName%>" />
                                        <ext:TextField runat="server" Name="lastName" AllowBlank="false" ID="lastName" FieldLabel="<%$ Resources:FieldLastName%>" />
                                        <ext:DateField runat="server" Name="birthDate" AllowBlank="true" ID="birthDate" FieldLabel="<%$ Resources:FieldBirthDate%>" />
                                        <ext:RadioGroup ID="gender" AllowBlank="true" runat="server" GroupName="gender"  FieldLabel="<%$ Resources:FieldGender%>">
                                            <Items>
                                                <ext:Radio runat="server" ID="gender0" Name="gender" InputValue="0"  BoxLabel="<%$ Resources:Common ,Male%>" />
                                                <ext:Radio runat="server" ID="gender1" Name="gender" InputValue="1" BoxLabel="<%$ Resources:Common ,Female%>" />
                                            </Items>
                                        </ext:RadioGroup>
                                        <ext:TextField runat="server" Name="phoneNumber" AllowBlank="true" MaxLength="12" MinLength="6" ID="phoneNumber" FieldLabel="<%$ Resources:FieldPhoneNumber%>">
                                            <Validator Handler="return !isNaN(this.value);" />
                                        </ext:TextField>

                                        <ext:Checkbox ID="isStudent" runat="server" FieldLabel="<%$ Resources: FieldIsStudent%>" DataIndex="isStudent" Name="isStudent" InputValue="true" />
                                        <ext:Checkbox ID="isCitizen" runat="server" FieldLabel="<%$ Resources: FieldIsCitizen%>" DataIndex="isCitizen" Name="isCitizen" InputValue="true" />
                                  
                            </Items>
                        </ext:FormPanel>
                        <ext:FormPanel runat="server" ID="addressForm" Title="<%$ Resources: AddressForm %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField runat="server" Name="addressId" ID="addressId" Hidden="true" Disabled="true" />
                                <ext:TextField runat="server" Name="street1" AllowBlank="true" ID="street1" FieldLabel="<%$ Resources:FieldStreet1%>">
                                    <Listeners>
                                        <Change Handler="if(this.value=='') setNullable(true); else setNullable(false);" />
                                    </Listeners>
                                </ext:TextField>
                                <ext:TextField runat="server" Name="street2" AllowBlank="true" ID="street2" FieldLabel="<%$ Resources:FieldStreet2%>">
                                    <Listeners>
                                        <Change Handler="if(this.value=='') setNullable(true); else setNullable(false);" />
                                    </Listeners>
                                </ext:TextField>
                                <ext:TextField runat="server" Name="city" AllowBlank="true" ID="city" FieldLabel="<%$ Resources:FieldCity%>" />
                                <ext:TextField runat="server" Name="postalCode" AllowBlank="true" MaxLength="6" ID="postalCode" FieldLabel="<%$ Resources:FieldPostalCode%>" />
                                <%--<ext:TextField  runat="server" Name="countryName" AllowBlank="false"  ID="countryName"  FieldLabel="<%$ Resources:FieldCountryName%>" />--%>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ValueField="recordId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name" runat="server" ID="stateId" Name="stateId" FieldLabel="<%$ Resources:FieldState%>" SimpleSubmit="true">
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
                                    DisplayField="name" runat="server" ID="countryId" Name="countryId" FieldLabel="<%$ Resources:FieldCountryName%>" SimpleSubmit="true">
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

                            </Items>
                        </ext:FormPanel>




                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button7" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{infoForm}.getForm().isValid()|| !#{addressForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditContactWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="addressId" Value="#{addressId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="info" Value="#{infoForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                                <ext:Parameter Name="address" Value="#{addressForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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


