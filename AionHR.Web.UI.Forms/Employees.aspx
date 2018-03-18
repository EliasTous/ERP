<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="AionHR.Web.UI.Forms.Employees" %>


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
    <script type="text/javascript"  src="Scripts/jquery-new.js"></script>
   

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

        <ext:Hidden ID="reportTo" runat="server" EnableViewState="true" Text="reports" />
        <ext:Hidden ID="CurrentEmployeePhotoName" runat="server" EnableViewState="true" />
        <ext:Hidden ID="FileName" runat="server" EnableViewState="true" />
        <ext:Hidden runat="server" ID="lblLoading" Text="<%$Resources:Common , Loading %>" />
        <ext:Hidden runat="server" ID="pRTL" />
        <ext:Hidden runat="server" ID="imageData" />

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
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="1" />
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
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                        <ext:FieldTrigger Handler="this.setValue('');" Icon="Clear" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />

                                        <TriggerClick Handler="#{Store1}.reload();" />
                                        <SpecialKey Handler="if(e.keyCode==13) #{Store1}.Reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />

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
                            <ext:Column ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDivision%>" DataIndex="divisionName" Flex="2" Hideable="false" />
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
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>




    </form>

</body>
</html>
