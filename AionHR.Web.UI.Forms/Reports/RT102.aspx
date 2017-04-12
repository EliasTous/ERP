<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT102.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT102" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="../CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="../CSS/RT101.css?id=2" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Dashboard.js"></script>
    <!--  <script type="text/javascript" src="Scripts/app.js"></script>-->
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script src="https://superal.github.io/canvas2image/canvas2image.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/moment.js"></script>
    <script type="text/javascript" src="../Scripts/RT101.js?id=18"></script>
    <script type="text/javascript">
    
    </script>
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />

        <ext:Hidden ID="rtl" runat="server" />
        <ext:Hidden ID="format" runat="server" />

        <ext:Store PageSize="30"
            ID="secondStore" OnReadData="secondStore_ReadData"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server">
                    <Fields>

                        <ext:ModelField Name="name" IsComplex="true" />

                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="positionName" />
                        <ext:ModelField Name="branchName" />
                        <ext:ModelField Name="divisionName" />
                        <ext:ModelField Name="date" />
                        <ext:ModelField Name="esName" />


                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>
        <ext:Store PageSize="30"
            ID="firstStore" OnReadData="firstStore_ReadData"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model3" runat="server">
                    <Fields>

                        <ext:ModelField Name="name" IsComplex="true" />

                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="positionName" />
                        <ext:ModelField Name="branchName" />
                        
                        <ext:ModelField Name="date" />
                        


                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">

            <Items>

                <ext:Panel
                    ID="Center"
                    runat="server"
                    Border="false"
                    Layout="FitLayout" AutoScroll="true"
                    Margins="0 0 0 0"
                    Region="Center">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Panel runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:Panel runat="server" Layout="HBoxLayout" ID="filterSet1" Hidden="true">
                                            <Items>
                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="branchId" Name="branchId" EmptyText="<%$ Resources:FieldBranch%>">
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

                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="departmentId" Name="departmentId" EmptyText="<%$ Resources:FieldDepartment%>">
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
                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="ComboBox1" Name="positionId" EmptyText="<%$ Resources:FieldPosition%>">
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
                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="ComboBox2" Name="positionId" EmptyText="Division">
                                                    <Store>
                                                        <ext:Store runat="server" ID="Store1">
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
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet2" Hidden="true">
                                            <Items>
                                                <ext:DateField runat="server"></ext:DateField>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" Layout="HBoxLayout" ID="filterSet3" Hidden="true">
                                            <Items>
                                                <ext:DateField ID="dateFrom" runat="server" EmptyText="<%$ Resources: DateFrom %>"></ext:DateField>
                                                <ext:DateField ID="dateTo" runat="server" EmptyText="<%$ Resources: DateTo %>"></ext:DateField>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet4" Hidden="true">
                                            <Items>
                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="ComboBox3" Name="branchId" EmptyText="Employment History">
                                                    <Store>
                                                        <ext:Store runat="server" ID="Store2">
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
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet5" Hidden="true">
                                            <Items>
                                                <ext:ComboBox runat="server" ID="employeeId" Width="130" LabelAlign="Top"
                                                    DisplayField="fullName"
                                                    ValueField="recordId" AllowBlank="true"
                                                    TypeAhead="false"
                                                    HideTrigger="true" SubmitValue="true"
                                                    MinChars="3" EmptyText="Employee"
                                                    TriggerAction="Query" ForceSelection="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="EmployeeStore" AutoLoad="false">
                                                            <Model>
                                                                <ext:Model runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="fullName" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>

                                                        </ext:Store>
                                                    </Store>


                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:ToolbarFill runat="server" />
                                <ext:ToolbarFill runat="server" />
                                <ext:Panel runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:Panel runat="server" Hidden="true">
                                            <Items>
                                                <ext:DateField runat="server" ID="filterSet6" />
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel runat="server" ID="filterSet7" Hidden="true">
                                            <Items>
                                                <ext:ComboBox runat="server" ID="inactivePref" Editable="false" FieldLabel="">
                                                    <Items>
                                                        <ext:ListItem Text="<%$ Resources: All %>" Value="2" />
                                                        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="0" />
                                                        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="1" />
                                                    </Items>
                                                    <Listeners>
                                                        <Change Handler="App.Store1.reload()" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet8" Hidden="true">
                                            <Items>
                                                <ext:ComboBox EmptyText="Leave Type" runat="server" Width="130" LabelAlign="Top" ValueField="recordId" DisplayField="name" ID="divisionId" Name="divisionId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
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
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet9" Hidden="true">
                                            <Items>

                                                <ext:CheckboxGroup runat="server">
                                                    <Items>
                                                        <ext:Checkbox runat="server" FieldLabel="Add"></ext:Checkbox>
                                                        <ext:Checkbox runat="server" FieldLabel="Edit"></ext:Checkbox>
                                                        <ext:Checkbox runat="server" FieldLabel="Delete"></ext:Checkbox>
                                                    </Items>
                                                </ext:CheckboxGroup>
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel runat="server" ID="filterSet10" Hidden="true">
                                            <Items>

                                                <ext:CheckboxGroup runat="server">
                                                    <Items>
                                                        <ext:Checkbox runat="server" FieldLabel="Employee"></ext:Checkbox>
                                                        <ext:Checkbox runat="server" FieldLabel="Time Management"></ext:Checkbox>
                                                        <ext:Checkbox runat="server" FieldLabel="Company Info"></ext:Checkbox>
                                                    </Items>
                                                </ext:CheckboxGroup>
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel runat="server" ID="filterSet11" Hidden="true">
                                            <Items>
                                                <ext:ComboBox EmptyText="User" runat="server" Width="130" LabelAlign="Top" ValueField="recordId" DisplayField="name" ID="ComboBox4" Name="divisionId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                                    <Store>
                                                        <ext:Store runat="server" ID="Store3">
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
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet12" Hidden="true">
                                            <Items>
                                                <ext:ComboBox EmptyText="Class Ref" runat="server" Width="130" LabelAlign="Top" ValueField="recordId" DisplayField="name" ID="ComboBox5" Name="divisionId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                                    <Store>
                                                        <ext:Store runat="server" ID="Store4">
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
                                            </Items>
                                        </ext:Panel>
                                        <ext:Button runat="server" Text="Go" >
                                            <Listeners>
                                                <Click Handler="App.firstStore.reload(); App.secondStore.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>

                                </ext:Panel>
                                <ext:ToolbarFill runat="server" />
                                <ext:ToolbarFill runat="server" />
                                <ext:Button Icon="PageGear" runat="server">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , Print %>" Icon="Printer">
                                                    <Listeners>
                                                        <Click Handler="PrintElem('Center');" />
                                                    </Listeners>
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , ExportAsPdf %>" Icon="DiskDownload" />
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , EmailReport %>" Icon="EmailAttach" />
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel runat="server" Height="200" Layout="AutoLayout" Width="1000" AutoScroll="true" ID="toPrint">
                            <Items>


                                <ext:GridPanel ExpandToolText="expand"
                                    ID="firstGrid" MarginSpec="0 17 0 0"
                                    runat="server" StoreID="firstStore"
                                    PaddingSpec="0 0 0 0" MinHeight="400" MaxHeight="400"
                                    Header="true" CollapseMode="Header" Collapsible="true" CollapseDirection="Top"
                                    Title="<%$ Resources: Additions %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>
                                            <ext:Column Visible="false" ID="Column2" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" />
                                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name.fullName" Flex="4" Hideable="false">
                                                <Renderer Handler=" return  record.data['name'].fullName; ">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column ID="ColDepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false">
                                            </ext:Column>
                                            <ext:Column ID="ColPositionName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                                           
                                            <ext:DateColumn ID="ColHireDate" Format="dd-MMM-yyyy" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Width="120" Hideable="false">
                                            </ext:DateColumn>


                                        </Columns>
                                    </ColumnModel>

                                    <View>
                                        <ext:GridView ID="GridView3" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel2" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>

                                <ext:GridPanel ExpandToolText="expand" MinHeight="400"
                                    ID="secondGrid" MarginSpec="0 17 0 0" MaxHeight="400"
                                    runat="server" StoreID="secondStore"
                                    PaddingSpec="0 0 0 0"
                                    Header="true" CollapseMode="Header" Collapsible="true" CollapseDirection="Top"
                                    Title="<%$ Resources: Terminations %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>
                                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" />
                                            <ext:Column ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name.fullName" Flex="4" Hideable="false">
                                                <Renderer Handler=" return  record.data['name'].fullName ">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column5" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                            <ext:Column ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                                           
                                            <ext:DateColumn ID="DateColumn1" Format="dd-MMM-yyyy" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Width="120" Hideable="false">
                                            </ext:DateColumn>


                                        </Columns>
                                    </ColumnModel>

                                    <View>
                                        <ext:GridView ID="GridView1" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>
                            </Items>

                        </ext:Panel>
                    </Items>
                </ext:Panel>


            </Items>
        </ext:Viewport>






    </form>
</body>
</html>
