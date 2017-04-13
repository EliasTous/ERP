<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainDashboard.aspx.cs" Inherits="AionHR.Web.UI.Forms.MainDashboard" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Dashboard.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/MainDashboard.js"></script>
    <!--  <script type="text/javascript" src="Scripts/app.js"></script>-->
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
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
        <ext:Hidden ID="item11Title" runat="server" Text="<%$ Resources:item11Title %>" />
        <ext:Hidden ID="item12Title" runat="server" Text="<%$ Resources:item12Title %>" />
        <ext:Hidden ID="item13Title" runat="server" Text="<%$ Resources:item13Title %>" />
        <ext:Hidden ID="item14Title" runat="server" Text="<%$ Resources:item14Title %>" />
        <ext:Hidden ID="item21Title" runat="server" Text="<%$ Resources:item21Title %>" />
        <ext:Hidden ID="item22Title" runat="server" Text="<%$ Resources:item22Title %>" />
        <ext:Hidden ID="item23Title" runat="server" Text="<%$ Resources:item23Title %>" />
        <ext:Hidden ID="item31Title" runat="server" Text="<%$ Resources:item31Title %>" />
        <ext:Hidden ID="item32Title" runat="server" Text="<%$ Resources:item32Title %>" />
        <ext:Hidden ID="item33Title" runat="server" Text="<%$ Resources:item33Title %>" />
        <ext:Hidden ID="item34Title" runat="server" Text="<%$ Resources:item34Title %>" />
        <ext:Hidden ID="item41Title" runat="server" Text="<%$ Resources:item41Title %>" />
        <ext:Hidden ID="item42Title" runat="server" Text="<%$ Resources:item42Title %>" />
        <ext:Hidden ID="item43Title" runat="server" Text="<%$ Resources:item43Title %>" />
        <ext:Hidden ID="item51Title" runat="server" Text="<%$ Resources:item51Title %>" />
        <ext:Hidden ID="item52Title" runat="server" Text="<%$ Resources:item52Title %>" />
        <ext:Hidden ID="item53Title" runat="server" Text="<%$ Resources:item53Title %>" />
        <ext:Hidden ID="item61Title" runat="server" Text="<%$ Resources:item61Title %>" />
        <ext:Hidden ID="item62Title" runat="server" Text="<%$ Resources:item62Title %>" />
        <ext:Hidden ID="item63Title" runat="server" Text="<%$ Resources:item63Title %>" />



        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">

            <Items>


                <ext:Panel runat="server" Layout="BorderLayout" Hidden="false" MarginSpec="100 0 0 0">
                    <Items>


                        <ext:Panel ID="Panel1" Flex="1" runat="server" Layout="FitLayout" Region="Center" AutoScroll="true">
                        </ext:Panel>
                        <ext:Panel AutoScroll="true" Header="false" ID="leftPanel" Flex="5" Region="West" Layout="FitLayout" runat="server" Width="500">
                            <Items>

                                <ext:GridPanel MarginSpec="0 0 0 0" Hidden="false"
                                    ID="missingPunchesGrid"
                                    runat="server" HideHeaders="true"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Layout="FitLayout"
                                    Scroll="Vertical" MaxHeight="200"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Listeners>
                                        <Render Handler="this.on('cellclick', cellClick);" />
                                    </Listeners>
                                    <DirectEvents>
                                        <CellClick OnEvent="PoPuP">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="record.data['itemId']" Mode="Raw" />
                                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                            </ExtraParams>

                                        </CellClick>
                                    </DirectEvents>
                                    <Store>
                                        <ext:Store
                                            ID="store1" PageSize="30"
                                            runat="server"
                                            RemoteSort="True"
                                            RemoteFilter="true">

                                            <Model>
                                                <ext:Model ID="Model5" runat="server" IDProperty="itemId">
                                                    <Fields>

                                                        <ext:ModelField Name="itemId" />
                                                        <ext:ModelField Name="count" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel5" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>


                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" DataIndex="itemId" Hideable="false">
                                                <Renderer Handler="  return GetItemTitle(record.data['itemId']);">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column Flex="1" MenuDisabled="true" runat="server" DataIndex="count" Hideable="false">
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>


                                    <View>
                                        <ext:GridView ID="GridView5" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel4" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>

                                <ext:GridPanel MarginSpec="0 0 0 0"
                                    ID="absenseGrid"
                                    runat="server" HideHeaders="true"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Layout="FitLayout"
                                    Scroll="Vertical" Height="200" MaxHeight="200"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                                    <Store>
                                        <ext:Store
                                            ID="store2"
                                            runat="server"
                                            PageSize="5">

                                            <Model>
                                                <ext:Model ID="Model2" runat="server" IDProperty="itemId">
                                                    <Fields>

                                                        <ext:ModelField Name="itemId" />
                                                        <ext:ModelField Name="count" />


                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>

                                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>


                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" DataIndex="itemId" Hideable="false">
                                                <Renderer Handler="  return GetItemTitle(record.data['itemId']);">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column Flex="1" MenuDisabled="true" runat="server" DataIndex="count" Hideable="false">
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>


                                    <View>
                                        <ext:GridView ID="GridView2" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>


                                <ext:GridPanel MarginSpec="0 0 0 0"
                                    ID="leaveGrid" HideHeaders="true"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store PageSize="30"
                                            ID="store3"
                                            runat="server"
                                            RemoteSort="True"
                                            RemoteFilter="true">

                                            <Model>
                                                <ext:Model ID="Model4" runat="server" IDProperty="itemId">
                                                    <Fields>

                                                        <ext:ModelField Name="itemId" />
                                                        <ext:ModelField Name="count" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>


                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" DataIndex="itemId" Hideable="false">
                                                <Renderer Handler="  return GetItemTitle(record.data['itemId']);">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column Flex="1" MenuDisabled="true" runat="server" DataIndex="count" Hideable="false">
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>


                                    <View>
                                        <ext:GridView ID="GridView4" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel3" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>

                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="rightPanel" Flex="5" runat="server" Layout="FitLayout" Region="East" AutoScroll="true" Width="500">
                            <Items>

                                <ext:GridPanel MarginSpec="0 17 0 0"
                                    ID="ggr"
                                    runat="server" HideHeaders="true"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Layout="FitLayout"
                                    Scroll="Vertical" Height="200" MaxHeight="200"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store
                                            ID="store4"
                                            runat="server"
                                            RemoteSort="True" PageSize="30"
                                            RemoteFilter="true">

                                            <Model>
                                                <ext:Model ID="Model1" runat="server" IDProperty="itemId">
                                                    <Fields>

                                                        <ext:ModelField Name="itemId" />
                                                        <ext:ModelField Name="count" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>

                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" DataIndex="itemId" Hideable="false">
                                                <Renderer Handler="   return GetItemTitle(record.data['itemId']);">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column Flex="1" MenuDisabled="true" runat="server" DataIndex="count" Hideable="false">
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>

                                    <View>
                                        <ext:GridView ID="GridView1" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>


                                <ext:GridPanel ExpandToolText="expand"
                                    ID="latenessGrid" MarginSpec="0 17 0 0" HideHeaders="true"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 0 0"
                                    Header="false" CollapseMode="Header" Collapsible="true" CollapseDirection="Right"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store PageSize="30"
                                            ID="store5"
                                            runat="server"
                                            RemoteSort="True"
                                            RemoteFilter="true">

                                            <Model>
                                                <ext:Model ID="Model3" runat="server" IDProperty="itemId">
                                                    <Fields>

                                                        <ext:ModelField Name="itemId" />
                                                        <ext:ModelField Name="count" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>

                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" DataIndex="itemId" Hideable="false">
                                                <Renderer Handler="  return GetItemTitle(record.data['itemId']);">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column Flex="1" MenuDisabled="true" runat="server" DataIndex="count" Hideable="false">
                                            </ext:Column>
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

                                <ext:GridPanel
                                    ID="outGrid" MarginSpec="0 17 0 0"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 0 0"
                                    Header="false" HideHeaders="true"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store PageSize="30"
                                            ID="store6"
                                            runat="server"
                                            RemoteSort="True"
                                            RemoteFilter="true">

                                            <Model>
                                                <ext:Model ID="Model6" runat="server" IDProperty="itemId">
                                                    <Fields>

                                                        <ext:ModelField Name="itemId" />
                                                        <ext:ModelField Name="count" />


                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel7" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>

                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" DataIndex="itemId" Hideable="false">
                                                <Renderer Handler="  return GetItemTitle(record.data['itemId']);">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column Flex="1" MenuDisabled="true" runat="server" DataIndex="count" Hideable="false">
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>

                                    <View>
                                        <ext:GridView ID="GridView7" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel6" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>


                            </Items>
                        </ext:Panel>

                    </Items>
                </ext:Panel>

            </Items>
        </ext:Viewport>
        <ext:Window
            ID="latenessWindow"
            runat="server"
            Icon="PageEdit"
            Width="450"
            Height="500"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="FitLayout">

            <Items>
               <ext:GridPanel ExpandToolText="expand"
                                    ID="GridPanel1" MarginSpec="0 17 0 0"
                                    runat="server" 
                                    PaddingSpec="0 0 0 0"
                                    Header="false"
                                    Title="<%$ Resources: LatenessGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store PageSize="30"
                                            ID="latenessStore"
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
                                                <ext:Model ID="Model7" runat="server" IDProperty="recordId">
                                                    <Fields>

                                                        <ext:ModelField Name="employeeId" />
                                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                                        <ext:ModelField Name="time" />
                                                        <ext:ModelField Name="positionName" />
                                                        <ext:ModelField Name="departmentName" />
                                                        <ext:ModelField Name="branchName" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel6" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>
                                            <ext:Column Visible="false" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75"  />
                                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false"/>
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTime%>" DataIndex="time" Width="55" Hideable="false" />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />


                                        </Columns>
                                    </ColumnModel>

                                    <View>
                                        <ext:GridView ID="GridView6" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel5" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>
            </Items>

        </ext:Window>





    </form>
</body>
</html>
