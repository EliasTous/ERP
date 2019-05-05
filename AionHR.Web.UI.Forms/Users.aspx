<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="AionHR.Web.UI.Forms.Users" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css?id=2" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Users.js?id=2"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/jquery-new.js"></script>
    <script type="text/javascript">
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";


            }
            return out;
        }
        Ext.define("Ext.plugin.extjs.form.PasswordStrength", {
            extend: "Ext.AbstractPlugin",
            alias: "plugin.passwordstrength",
            colors: ["C11B17", "FDD017", "4AA02C", "6AFB92", "00FF00"],

            init: function (cmp) {
                var me = this;

                App.PasswordField.on("change", me.onFieldChange, me);


            },

            onFieldChange: function (field, newVal, oldVal) {
                if (newVal === "") {


                    App.pro.updateText('');
                    $("#pro-bar")[0].style.backgroundColor = "white";
                    App.pro.setStyle({ "background-color": "white" });
                    return;
                }
                var me = this,
                    score = me.scorePassword(field.value);



                me.processValue(field, score);


            },

            processValue: function (field, score) {

                var me = this,
                    colors = me.colors,
                    color;
                var i;

                if (score < 16) {
                    i = 1;
                    color = colors[0]; //very weak
                } else if (score > 15 && score < 25) {
                    i = 2;
                    color = colors[1]; //weak
                } else if (score > 24 && score < 35) {
                    i = 3;

                    color = colors[2]; //mediocre
                } else if (score > 34 && score < 45) {
                    i = 4;
                    color = colors[3]; //strong
                } else {
                    i = 5;

                    color = colors[4]; //very strong
                }



                App.pro.setValue(i / 5);





                App.pro.updateText(document.getElementById("level" + i).value);


                $("#pro-bar")[0].style.backgroundColor = "#" + colors[i];

            },

            scorePassword: function (passwd) {
                var score = 0;

                if (passwd.length < 5) {
                    score += 3;
                } else if (passwd.length > 4 && passwd.length < 8) {
                    score += 6;
                } else if (passwd.length > 7 && passwd.length < 16) {
                    score += 12;
                } else if (passwd.length > 15) {
                    score += 18;
                }

                if (passwd.match(/[a-z]/)) {
                    score += 1;
                }

                if (passwd.match(/[A-Z]/)) {
                    score += 5;
                }

                if (passwd.match(/\d+/)) {
                    score += 5;
                }

                if (passwd.match(/(.*[0-9].*[0-9].*[0-9])/)) {
                    score += 5;
                }

                if (passwd.match(/.[!,@,#,$,%,^,&,*,?,_,~]/)) {
                    score += 5;
                }

                if (passwd.match(/(.*[!,@,#,$,%,^,&,*,?,_,~].*[!,@,#,$,%,^,&,*,?,_,~])/)) {
                    score += 5;
                }

                if (passwd.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {
                    score += 2;
                }

                if (passwd.match(/([a-zA-Z])/) && passwd.match(/([0-9])/)) {
                    score += 2;
                }

                if (passwd.match(/([a-zA-Z0-9].*[!,@,#,$,%,^,&,*,?,_,~])|([!,@,#,$,%,^,&,*,?,_,~].*[a-zA-Z0-9])/)) {
                    score += 2;
                }

                return score;
            }
        });
        function SetNameEnabled(status, name) {


            App.fullName.setDisabled(!status);
            if (!status)
                App.fullName.setValue(name);

        }
        function show() {

            var fromStore = App.UserGroupsStore;
            var toStore = App.userSelector.toField.getStore();

            for (var i = 0; i < fromStore.getCount() ; i++) {
                var s = fromStore.getAt(i);
              
                toStore.add(s);

                var d = App.userSelector.fromField.getStore().getById(s.getId());

                if (d != null) {
                 
                    App.userSelector.fromField.getStore().remove(d);

                }
            }



        }
        function AddSource(items) {
            var fromStore = App.userSelector.fromField.getStore();
            var toStore = App.userSelector.toField.getStore();

            while (fromStore.getCount() > 0)
                fromStore.removeAt(0);



            for (i = 0; i < items.length ; i++) {
                if (fromStore.getById(items[i].userId) == null && toStore.getById(items[i].userId) == null) {

                    fromStore.add(items[i]);
                }

            }
        }
        function SwapRTL() {
            if (document.getElementById("isRTL").value == '1') {

                $(".x-form-itemselector-add").css('background-image', 'url(ux/resources/images/itemselector/left-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(ux/resources/images/itemselector/right-gif/ext.axd)');

            }
            else {

                $(".x-form-itemselector-add").css('background-image', 'url(ux/resources/images/itemselector/right-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(ux/resources/images/itemselector/left-gif/ext.axd)');

            }
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
        <ext:Hidden ID="timeZoneOffset" runat="server" EnableViewState="true" />
        <ext:Hidden ID="rtl" runat="server" />
        <ext:Hidden ID="hide1" runat="server" />
        <ext:Hidden ID="hide2" runat="server" />
        <ext:Hidden ID="hide3" runat="server" />
        <ext:Hidden ID="hide4" runat="server" />
        <ext:Hidden ID="hide5" runat="server" />
        <ext:Hidden ID="hide6" runat="server" />
        <ext:Hidden ID="hide7" runat="server" />
        <ext:Hidden ID="hide8" runat="server" />
        <ext:Hidden ID="isRTL" runat="server" />
        <ext:Hidden ID="CurrentUser" runat="server" />
        <ext:Hidden runat="server" ID="level1" Text="<%$ Resources:VeryWeak %>" />
        <ext:Hidden runat="server" ID="level2" Text="<%$ Resources:Weak %>" />
        <ext:Hidden runat="server" ID="level3" Text="<%$ Resources:Mediocre %>" />
        <ext:Hidden runat="server" ID="level4" Text="<%$ Resources:Strong %>" />
        <ext:Hidden runat="server" ID="level5" Text="<%$ Resources:VeryStrong %>" />
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
                        <ext:ModelField Name="fullName" />
                        <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="languageId" />
                        <ext:ModelField Name="email" />

                         <ext:ModelField Name="userTypeString" />
                        <ext:ModelField Name="isInactive" />
                       <%-- <ext:ModelField Name="isAdmin" />--%>
                         <ext:ModelField Name="branchName" />
                         <ext:ModelField Name="departmentName" />
                         <ext:ModelField Name="positionName" />
                         <ext:ModelField Name="userTypeName" />
                         <ext:ModelField Name="employeeName"  />
                      



                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
                <ext:DataSorter Property="name" Direction="ASC" />
                <ext:DataSorter Property="reference" Direction="ASC" />
            </Sorters>
        </ext:Store>
           <ext:Store
            ID="userTypeStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="userTypeStore_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model4" runat="server" IDProperty="key">
                    <Fields>

                        <ext:ModelField Name="key" />
                        <ext:ModelField Name="value" />
                     
                      



                    </Fields>
                </ext:Model>
            </Model>
        
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
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" FieldWidth="150" EnableBranch="false" EnableDivision="false" />

                                    </Content>

                                </ext:Container>
                                <ext:Button runat="server" Text="Go">
                                    <Listeners>
                                        <Click Handler="App.Store1.reload();" />
                                    </Listeners>
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
                            <ext:Column Visible="false" ID="ColEmployeeId" MenuDisabled="true" runat="server" DataIndex="employeeId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Visible="false" ID="ColPassword" MenuDisabled="true" runat="server" DataIndex="password" Hideable="false" Width="75" Align="Center" />

                            <ext:Column ID="ColFullName" MenuDisabled="true" Sortable="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="fullName" Flex="1" Hideable="false">
                                <Renderer Handler="if(App.hide1.value=='True') return '****'; return  record.data['fullName'];">
                                </Renderer>
                            </ext:Column>

                            <ext:Column Sortable="true" ID="ColEmail" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmail%>" DataIndex="email" Flex="1" Hideable="false">
                                <Renderer Handler="if(App.hide2.value=='True') return '****'; return record.data['email'];" />
                            </ext:Column>

                           <ext:Column ID="Column7" MenuDisabled="true" Sortable="true" runat="server" Text="<%$ Resources: FieldEmployeeName%>" DataIndex="employeeName" Flex="1" Hideable="false" />
                           <%-- <ext:CheckColumn ID="ColIsAdmin" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIsAdmin %>" DataIndex="isAdmin" Hideable="false" />--%>
                             <ext:Column ID="ColUserType" MenuDisabled="true" Sortable="true" runat="server" Text="<%$ Resources: FieldUserType%>" DataIndex="userTypeName" Flex="1" Hideable="false" />
                                  <ext:Column ID="Column3" MenuDisabled="true" Sortable="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="1" Hideable="false" />
                              <ext:Column ID="Column5" MenuDisabled="true" Sortable="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="1" Hideable="false" />
                              <ext:Column ID="Column6" MenuDisabled="true" Sortable="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="1" Hideable="false" />

                           
                              <ext:CheckColumn ID="ColIsInactive" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldIsInactive %>" DataIndex="isInactive" Hideable="false" />

                            <ext:Column runat="server"
                                ID="colAttach" Visible="true"
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
            Height="400"
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
                                <ext:TextField ID="fullName" TabIndex="1" runat="server" FieldLabel="<%$ Resources: FieldFullName %>" DataIndex="fullName" Name="fullName" AllowBlank="false" />

                                <ext:TextField ID="email" TabIndex="2" runat="server" FieldLabel="<%$ Resources: FieldEmail %>" DataIndex="email" Name="email" InputType="Email" Vtype="email" AllowBlank="false" />
                                <ext:TextField ID="recordId" TabIndex="3" Disabled="true" Hidden="true" runat="server" DataIndex="recordId" AllowBlank="false" />


                                <ext:Checkbox ID="isInactiveCheck" TabIndex="4" runat="server" FieldLabel="<%$ Resources: FieldIsInActive%>" DataIndex="isInactive" Name="isInactive" InputValue="true" />
                           <%--     <ext:Checkbox ID="isAdminCheck" TabIndex="5" runat="server" FieldLabel="<%$ Resources: FieldIsAdmin%>" DataIndex="isAdmin" Name="isAdmin" InputValue="true" />--%>

                                   <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="userType" AllowBlank="false"  Name="userType" DisplayField="value" ValueField="key" 
                                    SubmitValue="true"  StoreID="userTypeStore"
                                    TypeAhead="false"
                                    FieldLabel="<%$ Resources: FieldUserType%>">
                                   <%-- <Items>
                                      
                                   <%--     <ext:ListItem Text="<%$Resources:FieldSuperUser %>" Value="1" />
                                          <ext:ListItem Text="<%$Resources:FieldAdministrator %>" Value="2" />
                                        <ext:ListItem Text="<%$Resources:FieldOperator %>" Value="3" />
                                        
                                          <ext:ListItem Text="<%$Resources:FieldSelfService %>" Value="4" />

                                    </Items>--%>
                                       <Listeners>
                                           <Select Handler="if (this.value!=1) #{employeeId}.allowBlank = false; else #{employeeId}.allowBlank = true; #{employeeId}.validate(); "></Select>
                                       </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="employeeId" TabIndex="6" Name="employeeId"
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
                                        <Change Handler="if (App.userType.getValue()!=1) #{employeeId}.allowBlank = false; else #{employeeId}.allowBlank = true;" />
                                        <FocusLeave Handler=" if(this.value==null|| isNaN(this.value) )SetNameEnabled(true,'');  if(isNaN(this.value)) this.setValue(null);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="languageId" AllowBlank="false" TabIndex="7" Name="languageId"
                                    SubmitValue="true"
                                    TypeAhead="false"
                                    FieldLabel="<%$ Resources: FieldLanguageId%>">
                                    <Items>
                                        <ext:ListItem Text="<%$Resources:Common,EnglishLanguage %>" Value="1" />
                                        <ext:ListItem Text="<%$Resources:Common,ArabicLanguage %>" Value="2" />
                                         <ext:ListItem Text="<%$Resources:Common,FrenchLanguage %>" Value="3" />
                                          <ext:ListItem Text="<%$Resources:Common,DeutschLanguage %>" Value="4" />
                                    </Items>
                                </ext:ComboBox>

                                <ext:TextField
                                    ID="PasswordField" TabIndex="8"
                                    runat="server"
                                    FieldLabel="<%$ Resources: FieldPassword%>"
                                    InputType="Password"
                                    Name="password"
                                    DataIndex="password"
                                    AllowBlank="false"
                                    AnchorHorizontal="100%">
                                    <Listeners>
                                        <ValidityChange Handler="this.next().next().validate();" />
                                        <Blur Handler="this.next().next().validate();" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:GenericPlugin TypeName="passwordstrength" />
                                    </Plugins>
                                </ext:TextField>
                                <%--<Plugins>
                               <%-- <ext:GenericPlugin TypeName="passwordstrength" />
                            </Plugins>
                            <RightButtons>
                                <ext:HyperlinkButton runat="server" ID="rightLink"   />
                            </RightButtons>--%>
                                <ext:ProgressBar runat="server" ID="pro" Width="295" MarginSpec="0 0 0 105">
                                    <Listeners>
                                        <Render Handler="this.updateText('');" />
                                    </Listeners>
                                </ext:ProgressBar>

                                <ext:TextField
                                    ID="PasswordConfirmation" TabIndex="9"
                                    runat="server"
                                    Vtype="password"
                                    FieldLabel="<%$ Resources: FieldConfirmPassword%>"
                                    InputType="Password"
                                    AnchorHorizontal="100%">
                                    <Validator Handler="if(this.value!= this.prev().prev().value) return false; else return true;">
                                    </Validator>
                                    <CustomConfig>
                                        <ext:ConfigItem Name="initialPassField" Value="PasswordField" Mode="Value" />
                                    </CustomConfig>
                                </ext:TextField>

                            </Items>
                            <Buttons>
                                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk" TabIndex="10">

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
                                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel" TabIndex="11">
                                    <Listeners>
                                        <Click Handler="this.up('window').hide();" />
                                    </Listeners>
                                </ext:Button>
                            </Buttons>
                        </ext:FormPanel>
                        <ext:FormPanel ID="userGroups" runat="server" Title="<%$ Resources: Groups %>">
                            <Items>
                                <ext:GridPanel runat="server" ID="groupsGrid" Layout="FitLayout">
                                    <TopBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:ComboBox Hidden="true" AnyMatch="true" CaseSensitive="false" runat="server" ID="GroupsCombo" DisplayField="name" ValueField="recordId" QueryMode="Local" Width="120" ForceSelection="true" TypeAhead="true" MinChars="1">
                                                    <Store>
                                                        <ext:Store runat="server" ID="AllGroupsStore" OnReadData="AllGroupsStore_ReadData">
                                                            <Model>
                                                                <ext:Model ID="Model2" runat="server" IDProperty="recordId">
                                                                    <Fields>

                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="name" />

                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                </ext:ComboBox>
                                                <ext:Button Hidden="true" runat="server" ID="addToGroupButton" Icon="BulletPlus">
                                                    <DirectEvents>
                                                        <Click OnEvent="addUserToGroup" />
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="ADDGroups">
                                                            <EventMask ShowMask="true" CustomTarget="={#{groupsGrid}.body}" />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Store>
                                        <ext:Store runat="server" ID="UserGroupsStore" OnReadData="UserGroupsStore_ReadData">
                                            <Model>
                                                <ext:Model ID="Model3" runat="server" IDProperty="sgId">
                                                    <Fields>

                                                        <ext:ModelField Name="sgId" />
                                                        <ext:ModelField Name="userId" />
                                                        <ext:ModelField Name="sgName" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                                        <Columns>



                                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                                            <ext:Column CellCls="cellLink" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldGroup%>" DataIndex="sgName" Flex="2" Hideable="false">
                                            </ext:Column>




                                            <ext:Column runat="server"
                                                ID="Column4" Visible="true"
                                                Text=""
                                                Width="100"
                                                Hideable="false"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                MenuDisabled="true"
                                                Resizable="false">

                                                <Renderer Handler="return deleteRender(); " />
                                            </ext:Column>

                                        </Columns>
                                    </ColumnModel>

                                    <BottomBar>
                                    </BottomBar>
                                    <Listeners>
                                     
                                        <Render Handler="this.on('cellclick', cellClick);" />
                                    </Listeners>
                                    <DirectEvents>
                                        <CellClick OnEvent="PoPuPGroup">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                            </ExtraParams>

                                        </CellClick>
                                    </DirectEvents>
                                </ext:GridPanel>

                            </Items>
                            <Listeners>
                                   <Activate Handler="#{UserGroupsStore}.reload();" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                </ext:TabPanel>
            </Items>

        </ext:Window>
        <ext:Window
            ID="groupUsersWindow"
            runat="server"
            Icon="PageEdit"
            Title=""
            Width="550"
            Height="450"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel
                    ID="groupUsersForm" DefaultButton="Button3"
                    runat="server" Header="false"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">

                    <Items>
                        <ext:ItemSelector runat="server" MaxHeight="300" MinHeight="300" AutoScroll="true" ID="userSelector" FromTitle="All" DisplayField="sgName" ValueField="sgId"
                            ToTitle="<%$Resources:Selected %>">
                            <Listeners>
                                <AfterRender Handler="SwapRTL(); " />
                            </Listeners>
                            <Store>
                                <ext:Store 
                                    PageSize="30" IDMode="Explicit" Namespace="App" runat="server" ID="groupSelectorGroup" OnReadData="groupSelectorGroup_ReadData">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="sgId">
                                            <Fields>
                                                <ext:ModelField Name="sgId" />
                                                <ext:ModelField Name="sgName" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>

                                  
                                </ext:Store>
                            </Store>

                        </ext:ItemSelector>

                    </Items>

                </ext:FormPanel>


            </Items>
            <Buttons>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{GroupUsersForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveGroupUsers" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{GroupUsersWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{GroupUsersForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                           <ext:Parameter 
                            Name="selectedGroups"                                  
                            Value="App.userSelector.toField.getStore().getRecordsValues()" 
                            Mode="Raw" 
                            Encode="true" />
                       
                  
            
                           
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


    </form>
</body>
</html>

