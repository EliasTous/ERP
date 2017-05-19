<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT107.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT107" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript" src="../Scripts/moment.js"></script>

    <script type="text/javascript" src="../Scripts/common.js"></script>

    <script type="text/javascript">
        var editRender = function () {
            return '<img class="imgEdit" style="cursor:pointer;" src="/Images/Tools/edit.png" />';
        };
        var attachRender = function () {
            return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/application_edit.png" />';
        };
        var commandName;
        var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

            CheckSession();



            var t = e.getTarget(),
                columnId = this.columns[columnIndex].id; // Get column id

            if (t.className == "imgEdit") {
                //the ajax call is allowed
                commandName = t.className;
                return true;
            }

            if (t.className == "imgDelete") {
                //the ajax call is allowed
                commandName = t.className;
                return true;
            }
            if (t.className == "imgAttach") {
                //the ajax call is allowed
                commandName = t.className;
                return true;
            }
            commandName = "";

            //forbidden
            return false;
        };


        var getCellType = function (grid, rowIndex, cellIndex) {
            if (cellIndex == 0)
                return "";
            if (commandName != "")
                return commandName;
            var columnId = grid.columns[cellIndex].id; // Get column id

            return columnId;
        };


        var enterKeyPressSearchHandler = function (el, event) {

            var enter = false;
            if (event.getKey() == event.ENTER) {
                if (el.getValue().length > 0)
                { enter = true; }
            }

            if (enter === true) {
                App.Store1.reload();
            }
        };
        function GetFieldName(x) {
            return document.getElementById('Field' + x).value;
        }
    </script>
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />
          <uc:employeeControl ID="employeeControl1" runat="server" />
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="Field1" runat="server" Text="<%$ Resources:Field1 %>" />
        <ext:Hidden ID="Field2" runat="server" Text="<%$ Resources:Field2 %>" />
        <ext:Hidden ID="Field3" runat="server" Text="<%$ Resources:Field3 %>" />
        <ext:Hidden ID="Field4" runat="server" Text="<%$ Resources:Field4 %>" />
        <ext:Hidden ID="Field5" runat="server" Text="<%$ Resources:Field5 %>" />
        <ext:Hidden ID="Field6" runat="server" Text="<%$ Resources:Field6 %>" />
        <ext:Hidden ID="Field7" runat="server" Text="<%$ Resources:Field7 %>" />
        <ext:Hidden ID="Field8" runat="server" Text="<%$ Resources:Field8 %>" />
        <ext:Hidden ID="Field9" runat="server" Text="<%$ Resources:Field9 %>" />
        <ext:Hidden ID="Field10" runat="server" Text="<%$ Resources:Field10 %>" />
        <ext:Hidden ID="Field11" runat="server" Text="<%$ Resources:Field11 %>" />
        <ext:Hidden ID="Field12" runat="server" Text="<%$ Resources:Field12 %>" />
        <ext:Hidden ID="Field13" runat="server" Text="<%$ Resources:Field13 %>" />
        <ext:Hidden ID="Field14" runat="server" Text="<%$ Resources:Field14 %>" />
        <ext:Hidden ID="Field15" runat="server" Text="<%$ Resources:Field15 %>" />

        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="True"
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
                <ext:Model ID="Model1" runat="server" IDProperty="fieldId">
                    <Fields>

                        <ext:ModelField Name="fieldId" />
                        <ext:ModelField Name="count" />






                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>



        <ext:Viewport ID="Viewport1" runat="server" Layout="CardLayout" ActiveIndex="0">
            <Items>
                <ext:GridPanel
                    ID="GridPanel1"
                    runat="server"
                    StoreID="Store1"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical" MinHeight="500"
                    Border="false" SortableColumns="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                               <ext:ComboBox runat="server" ID="inactivePref" Editable="false" FieldLabel="<%$ Resources: Status %>">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="1" />
                                    </Items>
                                    <Listeners>
                                        <Change Handler="App.Store1.reload()" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180" >
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


                            <ext:Column ID="fff" MenuDisabled="true" runat="server" Text="<%$ Resources: MissingField%>" DataIndex="fieldId" Hideable="false" Flex="1">
                                <Renderer Handler="return GetFieldName(record.data['fieldId']);" />
                            </ext:Column>
                            <ext:Column ID="cc" runat="server" Text="<%$ Resources: Count%>" DataIndex="count" Hideable="false" Width="200" />


                           
                            <ext:Column runat="server"
                                
                                Visible="true"
                                
                                Width="100"
                                Align="Center"
                                Text="<%$ Resources: Details%>"
                                
                                
                                Resizable="false">
                                <Renderer handler="if(record.data['count']>0) return  '<img class=imgAttach  style=cursor:pointer; src=../Images/Tools/application_edit.png />';" />
                            </ext:Column>


                        </Columns>
                    </ColumnModel>
                 
                    <BottomBar>
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

                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>

                <ext:GridPanel runat="server"  Header="false" ID="employeesGrid" Title="<%$ Resources: EmployeesGrid%>">
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPEM">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <Store>
                        <ext:Store ID="employeesStore" runat="server">
                            <Model>
                                <ext:Model runat="server" IDProperty="recordId">
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
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="Prev_Click">
                                            <ExtraParams>
                                                <ext:Parameter Name="index" Value="#{viewport1}.items.indexOf(#{viewport1}.layout.activeItem)" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>


                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <ColumnModel>
                        <Columns>
                          <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />

                            <ext:ComponentColumn runat="server" DataIndex="pictureUrl" Sortable="false">
                                <Component>
                                    <ext:Image runat="server" Height="100" Width="50">
                                    </ext:Image>

                                </Component>
                                <Listeners>
                                    <Bind Handler=" cmp.setImageUrl(record.get('pictureUrl')+'?id='+new Date().getTime()); " />
                                </Listeners>
                            </ext:ComponentColumn>
                            <ext:Column ID="ColReference" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldRef%>" DataIndex="name.reference" Width="60" Hideable="false">
                                <Renderer Handler=" return  record.data['name'].reference ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name.fullName" Flex="4" Hideable="false">
                                <Renderer Handler=" return  record.data['name'].fullName ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column ID="ColDepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false">
                            </ext:Column>
                            <ext:Column ID="ColPositionName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            <ext:Column ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDivision%>" DataIndex="divisionName" Flex="3" Hideable="false" />
                            <ext:DateColumn ID="ColHireDate" Format="dd-MMM-yyyy" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldHireDate%>" DataIndex="hireDate" Width="120" Hideable="false">
                            </ext:DateColumn>  <ext:Column runat="server"
                                ID="Column1" Visible="true"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="return '<img class=imgEdit style=cursor:pointer; src=../Images/Tools/edit.png />';" />
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
      



    </form>
</body>
</html>


