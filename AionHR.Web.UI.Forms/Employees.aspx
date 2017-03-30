﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="AionHR.Web.UI.Forms.Employees"  %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
   <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css?id=11" />
    <link rel="stylesheet" type="text/css" href="CSS/Employees.css?id=13" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    
    
    <script type="text/javascript" src="Scripts/common.js?id=1"></script>
    <script type="text/javascript" src="Scripts/Employees.js?id=28"></script>
    <script type="text/javascript">
       
    </script>

</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" Locale="ar-eg">
        </ext:ResourceManager>

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="timeZoneOffset" runat="server" EnableViewState="true" />
        <ext:Hidden ID="CurrentEmployee" runat="server" EnableViewState="true" />
        <ext:Hidden ID="CurrentClassId" runat="server" EnableViewState="true" />
        <ext:Hidden runat="server" ID="lblLoading" Text="<%$Resources:Common , Loading %>" />
        <ext:Viewport runat="server" Layout="BorderLayout" ID="Viewport1">
            <Items>
                <ext:GridPanel
                    ID="GridPanel1"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Region="Center"
                    Border="false"
                    Icon="User" HideHeaders="false"
                    ColumnLines="false" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="Store1"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="Store1_RefreshData"
                            PageSize="30" IDMode="Explicit" Namespace="App" IsPagingStore="true">
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
                                        <ext:ModelField Name="pictureUrl" />
                                        <ext:ModelField Name="name" IsComplex="true" />
                                        <ext:ModelField Name="reference" />
                                        <ext:ModelField Name="departmentName" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="divisionName" />
                                        <ext:ModelField Name="hireDate" />






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
                                <ext:ComboBox runat="server" ID="inactivePref" Editable="false" FieldLabel="<%$ Resources: Filter %>"  >
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="2"  />
                                        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="1" />
                                    </Items>
                                    <Listeners>
                                        <Change Handler="App.Store1.reload()" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <FocusLeave Handler="#{Store1}.reload();" />
                                        <TriggerClick Handler="#{Store1}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    
                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            
                            <ext:ComponentColumn runat="server" DataIndex="pictureUrl">
                                <Component>
                                    <ext:Image runat="server" Height="100" Width="50">
                                    </ext:Image>

                                </Component>
                                <Listeners>
                                    <Bind Handler=" cmp.setImageUrl(record.get('pictureUrl'));" />
                                </Listeners>
                            </ext:ComponentColumn>
                            <ext:Column ID="ColReference" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldReference%>" DataIndex="reference"  Width="60" Hideable="false" />
                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name.fullName" Flex="4" Hideable="false">
                                <Renderer Handler=" return  record.data['name'].fullName ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column ID="ColDepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false">
                            </ext:Column>
                            <ext:Column ID="ColPositionName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            <ext:Column ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDivision%>" DataIndex="divisionName" Flex="3" Hideable="false" />
                            <ext:DateColumn ID="ColHireDate"  Format="dd-MMM-yyyy" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldHireDate%>" DataIndex="hireDate" Width="120" Hideable="false">
                                
                            </ext:DateColumn>


                            <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="70"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="var x = editRender(); x=x+'&nbsp&nbsp'; return x;"/>

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colDelete" Flex="1" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
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
                        <Render Handler="CheckSession(); this.on('cellclick', cellClick);" />
                        
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
            Width="900"
            Height="500"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Maximizable="false"
            Header="true"
            Draggable="false"
             Resizable="false"
            Maximized="false"
            Layout="BorderLayout">


            <Items>
                <ext:Panel ID="leftPanel" runat="server" Region="West" PaddingSpec="0 0 0 0" Padding="0" TitleAlign="Center" DefaultAnchor="100%"
                    Header="false" Collapsible="false"  BodyPadding="5" Width="150" StyleSpec="border-left:2px solid #2A92D4;border-right:2px solid #2A92D4;" 
                    Title="<%$ Resources:Common , NavigationPane %>" CollapseToolText="<%$ Resources:Common , CollapsePanel %>" ExpandToolText="<%$ Resources:Common , ExpandPanel %>" BodyBorder="0">

                    <Items>
                        <ext:Panel runat="server" ID="alignedPanel" Header="false">

                            <Items>
                                
                                <ext:Image runat="server" ID="imgControl" Width="100" Height="100" Align="Middle" MarginSpec="15 0 0 20 " >
                                    <Listeners>
                                        <Click Handler="triggierImageClick(App.picturePath.fileInputEl.id); " />
                                    </Listeners>
                                  
                                </ext:Image>
                                
                                <ext:HyperlinkButton runat="server" Text="<%$ Resources:Clear %>" >
                                    <Listeners>
                                        <Click Handler=" ClearImage();" />

                                    </Listeners>
                                </ext:HyperlinkButton>
                                <ext:FileUploadField ID="picturePath" runat="server" ButtonOnly="true" Hidden="true">

                                    <Listeners>
                                        <Change Handler="showImagePreview(App.picturePath.fileInputEl.id);" />
                                    </Listeners>
                                    <DirectEvents>
                                       
                                    </DirectEvents>
                                </ext:FileUploadField>
                                <ext:Panel runat="server" ID="img" MarginSpec="50 0 0 0"><Items>
                                <ext:Label ID="fullNameLbl" runat="server" />
                                <ext:Label ID="departmentLbl" runat="server" />
                                <ext:Label ID="branchLbl" runat="server" />
                                <ext:Label ID="positionLbl" runat="server" />
                                    </Items></ext:Panel>



                                <%--<ext:Label runat="server" ID="employeeName" />--%>
                            </Items>

                        </ext:Panel>

                    </Items>

                </ext:Panel>




                <ext:TabPanel ID="panelRecordDetails" Layout="FitLayout" DefaultAnchor="100%" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" Region="Center">
                    <Items>
                        <ext:FormPanel DefaultButton="SaveButton" 
                            ID="BasicInfoTab"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5" Layout="TableLayout">
            
                            
                            <Items>
                                <ext:Panel runat="server" Margin="20">
                                    <Items>
                                        <ext:TextField ID="recordId" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Name="recordId" />
                                        <ext:TextField ID="reference" runat="server" FieldLabel="<%$ Resources:FieldReference%>" Name="reference" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                        <ext:TextField ID="firstName" runat="server" FieldLabel="<%$ Resources:FieldFirstName%>" Name="firstName" AllowBlank="false" BlankText="<%$ Resources:Common, MandatoryField%>">
                                        </ext:TextField>
                                        <ext:TextField ID="middleName"  runat="server" FieldLabel="<%$ Resources:FieldMiddleName%>" Name="middleName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                        <ext:TextField ID="lastName" AllowBlank="false" runat="server" FieldLabel="<%$ Resources:FieldLastName%>" Name="lastName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                        <ext:TextField ID="familyName" runat="server" FieldLabel="<%$ Resources:FieldFamilyName%>" Name="familyName" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                        <ext:TextField ID="homeEmail" runat="server" FieldLabel="<%$ Resources:FieldHomeEmail%>" Name="homeMail" Vtype="email" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                        <ext:TextField ID="workEmail" runat="server" FieldLabel="<%$ Resources:FieldWorkEmail%>" Name="workMail" Vtype="email" BlankText="<%$ Resources:Common, MandatoryField%>" />
                                        <ext:TextField ID="mobile" runat="server" FieldLabel="<%$ Resources:FieldMobile%>" Name="mobile" BlankText="<%$ Resources:Common, MandatoryField%>">
                                            <Plugins>
                                                <ext:InputMask Mask="99999999" />
                                            </Plugins>
                                        </ext:TextField>
                                        <ext:RadioGroup ID="gender" AllowBlank="true" runat="server" GroupName="gender" FieldLabel="<%$ Resources:FieldGender%>">
                                            <Items>
                                                <ext:Radio runat="server" ID="gender0" Name="gender" InputValue="0" BoxLabel="<%$ Resources:Common ,Male%>" />
                                                <ext:Radio runat="server" ID="gender1" Name="gender" InputValue="1" BoxLabel="<%$ Resources:Common ,Female%>" />
                                            </Items>
                                        </ext:RadioGroup>
                                        <ext:ComboBox ID="religionCombo" runat="server" FieldLabel="<%$ Resources:FieldReligion%>" Name="religion" IDMode="Static" SubmitValue="true">
                                            <Items>
                                                <ext:ListItem Text="<%$ Resources:Common, Religion0%>" Value="0"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources:Common, Religion1%>" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources:Common, Religion2%>" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources:Common, Religion3%>" Value="3"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources:Common, Religion4%>" Value="4"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources:Common, Religion5%>" Value="5"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources:Common, Religion6%>" Value="6"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server" MarginSpec="0 0 0 100">
                                    <Items>
                                        <ext:DateField
                                            runat="server"
                                            Name="birthDate"
                                            FieldLabel="<%$ Resources:FieldDateOfBirth%>"
                                            MsgTarget="Side"
                                            AllowBlank="false" />
                                        <ext:ComboBox runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" DisplayField="name" ID="nationalityId" Name="nationalityId" FieldLabel="<%$ Resources:FieldNationality%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="NationalityStore">
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

                                                                <Click OnEvent="addNationality">
                                                            
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:FieldContainer runat="server" Border="true">
                                            <Items>
                                                <ext:ComboBox Enabled="false" runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="departmentId" Name="departmentId" FieldLabel="<%$ Resources:FieldDepartment%>" SimpleSubmit="true">
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
                                                        <ext:Button ID="Button2" runat="server" Icon="Add" Hidden="true">
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
                                                        <FocusEnter Handler="  if(!this.readOnly) this.rightButtons[0].setHidden(false);" />
                                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:ComboBox Enabled="false" runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" SimpleSubmit="true">
                                                    <Store>
                                                        <ext:Store runat="server" ID="BranchStore">
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
                                                        <ext:Button ID="Button3" runat="server" Icon="Add" Hidden="true" >
                                                            <Listeners>
                                                                <Click Handler="CheckSession();" />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                <Click OnEvent="addBranch">
                                                                 
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                                    <Listeners>
                                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                    </Listeners>
                                                </ext:ComboBox>



                                                <ext:ComboBox Enabled="false" runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="divisionId" Name="divisionId" FieldLabel="<%$ Resources:FieldDivision%>" SimpleSubmit="true">
                                                    <Store>
                                                        <ext:Store runat="server" ID="divisionStore" IDMode="Explicit">
                                                            <Model>
                                                                <ext:Model runat="server" IDProperty="recordId">
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

                                                                <Click OnEvent="addDivision">
                                                               
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                                    <Listeners>
                                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:ComboBox Enabled="false" ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="positionId" Name="positionId" FieldLabel="<%$ Resources:FieldPosition%>" SimpleSubmit="true">
                                                    <Store>
                                                        <ext:Store runat="server" ID="positionStore">
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

                                                                <Click OnEvent="addPosition">
                                                              
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                                    <Listeners>
                                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:FieldContainer>


                                        <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="vsId" Name="vsId" FieldLabel="<%$ Resources:FieldVacationSchedule%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="VacationScheduleStore">
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
                                        <ext:ComboBox runat="server" ID="caId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" Name="caId" FieldLabel="<%$ Resources:FieldWorkingCalendar%>" SimpleSubmit="true">
                                            <Store>
                                                <ext:Store runat="server" ID="CalendarStore">
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
                                        <ext:TextField ID="birthPlace" runat="server" FieldLabel="<%$ Resources:FieldBirthPlace%>" Name="placeOfBirth" AllowBlank="true" />



                                        <ext:DateField
                                            runat="server"
                                            Name="hireDate"
                                            FieldLabel="<%$ Resources:FieldHireDate%>"
                                            MsgTarget="Side"
                                            AllowBlank="false" />
                                    </Items>
                                </ext:Panel>
                                <%-- <ext:Panel runat="server" Margin="20" Visible="false">
                                    <Items>
                                        <ext:Image runat="server" ID="imgControl" Width="200" Height="200">
                                            <Listeners>
                                                <Click Handler="triggierImageClick(App.picturePath.fileInputEl.id); " />
                                            </Listeners>
                                        </ext:Image>
                                        <ext:FileUploadField ID="picturePath" runat="server" ButtonOnly="true" Hidden="true">

                                            <Listeners>
                                                <Change Handler="showImagePreview(App.picturePath.fileInputEl.id);" />
                                            </Listeners>
                                        </ext:FileUploadField>

                                    </Items>
                                </ext:Panel>--%>
                            </Items>
                            <BottomBar>
                                <ext:Toolbar runat="server"  ClassicButtonStyle="false" Cls="tlb-BackGround">

                                    <Items>
                                         <ext:Button Cls="x-btn-left"  ID="DeleteButton" Text="Delete"   DefaultAlign="Left"  AlignTarget="Left"  Icon="Delete"  Region="West" runat="server" >
                                    <Listeners>
                                        <Click Handler="CheckSession();  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="DeleteRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                    </ext:Button>
                                           <ext:Button   Cls="x-btn-right" ID="Button8" runat="server" Text="History" Icon="Clock">
                                    <Listeners>
                                        <Click Handler="CheckSession(); parent.OpenTransactionLog(#{CurrentClassId}.value,#{CurrentEmployee}.value);" />
                                    </Listeners>
                                </ext:Button>
                                        <ext:ToolbarFill runat="server"/>
                                <ext:Button Cls="x-btn-left" ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {  return false;} " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button   Cls="x-btn-right" ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                                    <Listeners>
                                        <Click Handler="this.up('window').hide();" />
                                    </Listeners>
                                </ext:Button>
                                     
                                    </Items>
                                </ext:Toolbar>
                            </BottomBar>
                        
                        </ext:FormPanel>
                        <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: JobInformationTab %>" ID="profilePanel" DefaultAnchor="100%">
                            <Loader runat="server" Url="EmployeePages/JobInformation.aspx" Mode="Frame" ID="profileLoader" TriggerEvent="show"
                                ReloadOnEvent="true"
                                DisableCaching="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>
                        <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: PayrollTab %>" ID="Panel1" DefaultAnchor="100%">
                            <Loader runat="server" Url="EmployeePages/Payroll.aspx" Mode="Frame" ID="Loader1" TriggerEvent="show"
                                ReloadOnEvent="true"
                                DisableCaching="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>
                        <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: NotesTab %>" ID="Panel2" DefaultAnchor="100%">
                            <Loader runat="server" Url="EmployeePages/Notes.aspx" Mode="Frame" ID="Loader2" TriggerEvent="show"
                                ReloadOnEvent="true"
                                DisableCaching="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>
                         <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: DocumentsTab %>" ID="Panel3" DefaultAnchor="100%">
                            <Loader runat="server" Url="EmployeePages/Documents.aspx" Mode="Frame" ID="Loader3" TriggerEvent="show"
                                ReloadOnEvent="true"
                                DisableCaching="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>
                              <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: SkillsTab %>" ID="Panel4" DefaultAnchor="100%">
                            <Loader runat="server" Url="EmployeePages/Skills.aspx" Mode="Frame" ID="Loader4" TriggerEvent="show"
                                ReloadOnEvent="true"
                                DisableCaching="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>

                         <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: LegalsTab %>" ID="Panel5" DefaultAnchor="100%">
                            <Loader runat="server" Url="EmployeePages/Legals.aspx" Mode="Frame" ID="Loader5" TriggerEvent="show"
                                ReloadOnEvent="true"
                                DisableCaching="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>

                    </Items>
                </ext:TabPanel>

            </Items>

        </ext:Window>



    </form>
</body>
</html>
