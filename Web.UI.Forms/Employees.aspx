﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="Web.UI.Forms.Employees" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="CSS/Common.css?id=11" />

    <link rel="stylesheet" type="text/css" href="CSS/Employees.css?id=16" />
    <link rel="stylesheet" type="text/css" href="CSS/LiveSearch.css" />
    <link  rel="stylesheet" type="text/css" href="CSS/cropper.css" />

      <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/css/bootstrap.min.css" />
       <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
    <script type="text/javascript"  src="Scripts/jquery-new.js"></script>
    <script type="text/javascript" src="Scripts/ReportsCommon.js" ></script>

    <script type="text/javascript" src="Scripts/cropper.js"></script>

    <script type="text/javascript" src="Scripts/common.js?id=1"></script>

    <script type="text/javascript" src="Scripts/Employees.js?id=24"></script>

    <script type="text/javascript">

        function setDepartmentAllowBlank(allowBlank) {
            App.departmentId.allowBlank = allowBlank;

        }
        function setBranchAllowBlank(allowBlank) {
            App.branchId.allowBlank = allowBlank;

        }
        function setPositionAllowBlank(allowBlank) {
            App.positionId.allowBlank = allowBlank;

        }
        function setDivisionAllowBlank(allowBlank) {
            App.divisionId.allowBlank = allowBlank;
        }
    </script>

       <link rel="stylesheet" href="../Scripts/HijriCalender/redmond.calendars.picker.css" />

    <script src="../Scripts/HijriCalender/jquery.plugin.js" type="text/javascript"></script>

    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.js"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars-ar.js"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.picker.js"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.plus.js"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.islamic.js"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.islamic-ar.js"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.lang.js"></script>
    <script type="text/javascript" src="../Scripts/HijriCalender/jquery.calendars.picker-ar.js"></script>
    <script type="text/javascript" >
        
    </script>
    
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" >
        </ext:ResourceManager>

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />

        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="timeZoneOffset" runat="server" EnableViewState="true" />
        <ext:Hidden ID="CurrentEmployee" runat="server" EnableViewState="true" />
        <ext:Hidden ID="CurrentClassId" runat="server" EnableViewState="true" />

        <ext:Hidden ID="reportTo" runat="server" EnableViewState="true" Text="reports" />
        <ext:Hidden ID="CurrentEmployeePhotoName" runat="server" EnableViewState="true" />
        <ext:Hidden ID="FileName" runat="server" EnableViewState="true" />
        <ext:Hidden runat="server" ID="lblLoading" Text="<%$Resources:Common , Loading %>" />
        <ext:Hidden runat="server" ID="pRTL" />
        <ext:Hidden runat="server" ID="imageData" />
         <ext:Hidden runat="server" ID="storeSize" />
          <ext:Hidden runat="server" ID="previousStartAt" />
          <ext:Hidden ID="vals" runat="server" />
        <ext:Hidden ID="texts" runat="server" />
        <ext:Hidden ID="labels" runat="server" />
           <ext:Hidden ID="loaderUrl" runat="server"  Text="ReportParameterBrowser.aspx?_reportName=RT108&values="/>
         <ext:Hidden ID="textBody" runat="server" />
        
        
        <ext:Hidden runat="server" ID="imageVisible" />
        <ext:Viewport runat="server" Layout="FitLayout" ID="Viewport1">
            <Items>
                <ext:GridPanel
                    ID="GridPanel1"
                    runat="server"
                    Header="false"
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Region="Center"
                    Border="false"
                    Icon="User" HideHeaders="false"
                    ColumnLines="false" IDMode="Explicit" RenderXType="True" ForceFit="true" >
                    <Plugins>
                        <ext:RowExpander ID="RowExpander1" runat="server" HiddenColumn="true" ExpandOnEnter="false" ExpandOnDblClick="false" SingleExpand="true">
                            <Loader runat="server" Mode="Data" DirectMethod="App.direct.GetQuickView">
                                <LoadMask ShowMask="true" />
                                <Params>
                                    <ext:Parameter Name="id" Value="this.record.getId()" Mode="Raw" />

                                </Params>
                            </Loader>

                            <Template ID="Template1" runat="server">

                                <Html>
                                    <table width="200">

                                    </table>
                                </Html>
                            </Template>
                            <Listeners>

                                <%--<Expand Handler="doTranslations();" />--%>
                            </Listeners>
                        </ext:RowExpander>
                    </Plugins>

                    <Store>
                        <ext:Store
                            ID="Store1"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="Store1_RefreshData"
                            PageSize="30" IDMode="Explicit" Namespace="App" IsPagingStore="true" >
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
                                        <ext:ModelField Name="scTypeName" />
                                        <ext:ModelField Name="hireDate" />
                                          <ext:ModelField Name="scName" />





                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>


                       <DockedItems>
                        <ext:Toolbar runat="server" Height="30" Dock="Top">

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

                                

                                       <ext:Button ID="mailEmployees" runat="server" Icon="Mail">
                                    <Listeners>
                                        <Click Handler="CheckSession();App.pnlMaximumTamplate.body.update('');    App.Panel1.body.update('');  App.filter1.setText(App.texts.getValue());  App.mailForm.reset(); App.mailWindow.show(); " />
                                    </Listeners>
                                           <DirectEvents>
                                               <Click OnEvent="fillMailEmployeeForm" />
                                           </DirectEvents>

                                          
                                </ext:Button>
                             <%--   <ext:Button ID="Button1" runat="server" Icon="DiskMultiple">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="openBatchEM">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>--%>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                 <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                        <ext:FieldTrigger Handler="this.setValue('');" Icon="Clear" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" />

                                        <TriggerClick Handler="#{Store1}.reload();" />
                                        <SpecialKey Handler="if(e.keyCode==13)  #{Store1}.reload();App.selectedFilters.setText(' '); " />
                                    </Listeners>
                                </ext:TextField>
                                  <ext:ToolbarSeparator></ext:ToolbarSeparator>
                             
                                
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                          <ext:Button runat="server" Text="<%$ Resources:Common, Parameters%>"> 
                                       <Listeners>
                                           <Click Handler=" App.reportsParams.show();" />
                                       </Listeners>
                                        </ext:Button>
                                         <ext:Button runat="server" Text="<%$Resources:Common, Go %>" >
                                            <Listeners>
                                                <Click Handler="App.Store1.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                       
                                    </Content>
                                </ext:Container>
                                       
                        

                            </Items>
                        </ext:Toolbar>
                           
                        <ext:Toolbar ID="labelbar" runat="server" Height="0" Dock="Top">

                            <Items>
                                 <ext:Label runat="server" ID="selectedFilters" />
                                </Items>
                            </ext:Toolbar>
                  </DockedItems>
                  <%--  <TopBar>
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
                                <ext:Button ID="Button1" runat="server" Icon="DiskMultiple">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="openBatchEM">
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
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="inactivePref" Editable="false" FieldLabel="<%$ Resources: Filter %>">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="<%$ Resources:ComboBoxValues, InactivePref_All %>"/>
                                        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="<%$ Resources:ComboBoxValues, InactivePref_ActiveOnly %>" />
                                        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="<%$ Resources:ComboBoxValues, InactivePref_InactiveOnly %>" />
                                    </Items>
                                 
                                </ext:ComboBox>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" EnablePosition="false"  EnableDivision="false"/>

                                    </Content>

                                </ext:Container>
                                <ext:Button runat="server" Text="<%$ Resources: Common,Go%>" MarginSpec="0 0 0 0" Width="100">
                                    <Listeners>
                                        <Click Handler=" #{Store1}.reload(); ">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                    <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="filterField" Editable="false" EmptyText="<%$ Resources: filterField %>">
                                    <Items>
                                           <ext:ListItem Text="<%$ Resources: FieldAny %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldRef %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldIdRef %>" Value="2" />
                                         <ext:ListItem Text="<%$ Resources: FieldMobile %>" Value="3" />
                                         <ext:ListItem Text="<%$ Resources: FieldName %>" Value="4" />
                                         <ext:ListItem Text="<%$ Resources: FieldTimeAttendance  %>" Value="5" />
                                    </Items>
                                 
                                </ext:ComboBox>
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                        <ext:FieldTrigger Handler="this.setValue('');" Icon="Clear" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" />

                                        <TriggerClick Handler="#{Store1}.reload(); #{storeSize}.setValue('unlimited');" />
                                        <SpecialKey Handler="if(e.keyCode==13)  {#{Store1}.reload(); #{storeSize}.setValue('unlimited');}" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>--%>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server"  DataIndex="recordId" Hideable="false" Width="75" Align="Center" />

                            <ext:ComponentColumn runat="server" DataIndex="pictureUrl" Sortable="false">
                                <Component>
                                    <ext:Image runat="server" Height="100" Width="50">
                                    </ext:Image>

                                </Component>
                                <Listeners>
                                    <Bind Handler="if(App.imageVisible.value=='True') cmp.setImageUrl(record.get('pictureUrl')+'?id='+new Date().getTime()); else cmp.setImageUrl('../Images/empPhoto.jpg'); " />
                                </Listeners>

                            </ext:ComponentColumn>
                            <ext:Column ID="ColReference" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldRef%>" DataIndex="reference" Width="60" Hideable="false">
                                <Renderer Handler=" return  record.data['name'].reference ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name" Flex="3" Hideable="false">
                                <Renderer Handler=" return  record.data['name'].fullName ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column ID="ColDepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="2" Hideable="false">
                            </ext:Column>
                            <ext:Column ID="ColPositionName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="2" Hideable="false" />
                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="2" Hideable="false" />
                              <ext:Column ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSchedule%>" DataIndex="scName" Flex="2" Hideable="false" />
                              <ext:Column ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldScheduleType%>" DataIndex="scTypeName" Flex="2" Hideable="false" />
                          
                            <ext:DateColumn ID="ColHireDate" Format="dd-MMM-yyyy" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldHireDate%>" DataIndex="hireDate" Width="120" Hideable="false">
                            </ext:DateColumn>



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


                            <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text="<%$ Resources:Common, Edit %>"
                                MinWidth="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="var x = editRender(); x=x+'&nbsp&nbsp'; return x;" />

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


                        <AfterLayout Handler="item.getView().setScrollX(30);" />

                    </Listeners>
                    <DirectEvents>

                        <CellClick OnEvent="PoPuP">

                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                 <ext:Parameter Name="fullName" Value="record.data['name'].fullName" Mode="Raw" />

                              
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
        <uc:employeeControl runat="server" ID="employeeControl1" />

        <ext:Window
            ID="batchWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:BatchEmployeeTitle %>"
            Width="400"
            Height="350"
            AutoShow="false"
            Modal="true"
            Hidden="true"
             Resizable="false"
            Maximizable="false"
            Layout="Fit">

            <Items>
            
                        <ext:FormPanel
                            ID="batchForm" DefaultButton="SaveButton"
                            runat="server"
                          
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:FieldSet runat="server" Title="<%$Resources:Condition %>"><Items>
                                <ext:ComboBox AnyMatch="true" Width="330" CaseSensitive="false" runat="server" ID="departmentId" Name="departmentId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldDepartment %>">
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
                                   
                                </ext:ComboBox>
                                   <ext:ComboBox AnyMatch="true" Width="330" CaseSensitive="false" runat="server" ID="branchId" Name="branchId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldBranch %>">
                                    <Store>
                                        <ext:Store runat="server" ID="branchStore">
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
                                   <ext:ComboBox AnyMatch="true" CaseSensitive="false" Width="330" runat="server" ID="positionId" Name="positionId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldPosition %>">
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
                                   
                                </ext:ComboBox>
                                <ext:ComboBox AnyMatch="true" Width="330" CaseSensitive="false" runat="server" ID="divisionId" Name="divisionId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldDivision %>">
                                    <Store>
                                        <ext:Store runat="server" ID="divisionStore">
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
                          </Items></ext:FieldSet>
                                <ext:FieldSet runat="server" Title="<%$Resources:Batch %>"><Items>
                                     <ext:ComboBox AnyMatch="true" Width="330" CaseSensitive="false" runat="server" ID="caId" Name="caId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldCalendar %>">
                                    <Store>
                                        <ext:Store runat="server" ID="calendarStore">
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
                                   <ext:ComboBox AnyMatch="true" Width="330" CaseSensitive="false" runat="server" ID="scId" Name="scId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldSchedule %>">
                                    <Store>
                                        <ext:Store runat="server" ID="ScheduleStore">
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
                                <ext:ComboBox AnyMatch="true" Width="330" CaseSensitive="false" runat="server" ID="vsId" Name="vsId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldVacationSchedule %>">
                                    <Store>
                                        <ext:Store runat="server" ID="VSStore">
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
                    </Items></ext:FieldSet>
                            </Items>
                            
                        </ext:FormPanel>

  
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession();   if (!#{batchForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveBatch" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{batchWindow}.body}" />
                            <ExtraParams>
                               
                                <ext:Parameter Name="values" Value="#{batchForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                            
                        <Click Handler="this.up('window').hide(); App.Store1.reload();" />
                    </Listeners>
                    
                </ext:Button>
            </Buttons>
          
        </ext:Window>
           <ext:Window runat="server"  Icon="PageEdit"
            ID="reportsParams"
            Width="600"
            Height="500"
            Title="<%$Resources:Common,Parameters %>"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="FitLayout" Resizable="true">
            <Listeners>
                <Show Handler="App.Panel8.loader.load();"></Show>
            </Listeners>
            <Items>
                <ext:Panel runat="server" Layout="FitLayout"  ID="Panel8" DefaultAnchor="100%">
                    <Loader runat="server" Url="ReportParameterBrowser.aspx?_reportName=RT108" Mode="Frame" ID="Loader8" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <Listeners>
                         </Listeners>
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            
                </Items>
        </ext:Window>

           <ext:Window
            ID="mailWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:mailWindowTitle %>"
            Width="400"
            Height="350"
            AutoShow="false"
            Modal="true"
            Hidden="true"
             Resizable="true"
            Maximizable="false"
            Layout="Fit">

            <Items>
            
                        <ext:FormPanel
                            ID="mailForm" DefaultButton="saveMailButton"
                            runat="server"
                         
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                            <ext:Label ID="filter1"  runat="server" FieldLabel="<%$ Resources: FieldFilter %>" />
                                <ext:ComboBox AnyMatch="true" Width="330" CaseSensitive="false" runat="server" ID="templateId" Name="templateId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldTemplate %>">
                                    <Store>
                                        <ext:Store runat="server" ID="templateStore">
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
                                 <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldLangaugeId %>"  runat="server" DisplayField="value" ValueField="key"   Name="langaugeId" ID="langaugeId" >
                                             <Store>
                                                <ext:Store runat="server" ID="langaugeStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                     <DirectEvents>
                                           
                                            
                                         <Select OnEvent="FillPreview" >
                                             <EventMask ShowMask="true" />
                                             </Select>
                                     </DirectEvents>
                                       </ext:ComboBox>

                                <ext:Panel  runat="server" Layout="FitLayout" Flex="1" ID="Panel1" RTL="false" Border="true" Scrollable="Both" Height="150" Width="100"   >
                                    <Buttons>
                                        <ext:Button   Text="<%$ Resources: FieldTemplate %>" runat="server">
                                            <Listeners>
                                                <Click Handler=" App.MaximumTamplateWindow.show();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Buttons>
                                    </ext:Panel>
                                  
                                  
                                
                                 <ext:ProgressBar ID="mailEmployeesProgressBar" runat="server"   />
                        
                             
                            </Items>
                            
                        </ext:FormPanel>

  
            </Items>
            <Buttons>
                <ext:Button ID="saveMailButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession();   if (!#{mailForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="StartLongAction" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{mailWindow}.body}" />
                            <ExtraParams>
                               
                                <ext:Parameter Name="values" Value="#{mailForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                            
                        <Click Handler="this.up('window').hide(); " />
                    </Listeners>
                    
                </ext:Button>
            </Buttons>
          
        </ext:Window>
          <ext:TaskManager ID="TaskManager1" runat="server">
            <Tasks>
                <ext:Task 
                    TaskID="longactionprogress"
                    Interval="2000" 
                    AutoRun="false" 
                    OnStart="#{saveMailButton}.setDisabled(true);"
                       OnStop="#{saveMailButton}.setDisabled(false);" >
                 
                    <DirectEvents>
                        <Update OnEvent="RefreshProgress" />
                    </DirectEvents>                    
                </ext:Task>
            </Tasks>
        </ext:TaskManager>

          <ext:Window
            ID="MaximumTamplateWindow"
            runat="server"
            Icon="PageEdit"
            Title=""
            Width="450"
            Height="500"
            IDMode="Static"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Resizable="false"
            Maximized="true"
            Draggable="false"
            Layout="Fit">
            <Items>
                <ext:Panel AutoUpdateLayout="true"
                    runat="server"
                    ID="pnlMaximumTamplate" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                    Layout="FitLayout" Flex="1" Anchor="100% 100%">
                </ext:Panel>
            </Items>

        </ext:Window>

    </form>

</body>
</html>
