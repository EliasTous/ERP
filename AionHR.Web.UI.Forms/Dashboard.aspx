<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AionHR.Web.UI.Forms.Dashboard" %>

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
    <script type="text/javascript" src="Scripts/Dashboard.js"></script>
    <!--  <script type="text/javascript" src="Scripts/app.js"></script>-->
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript">
        function getStyle() {
            var dir = document.getElementById('rtl').value == 'True' ? 'right' : 'left';
            var s = 'text-align:' + dir;
            return s;
        }
        function displayMissedPunchRecord(s) {
            var str = "<div style=" + getStyle() + ">" + s.name;
            
            var friendlydate = moment(s['dayId'], 'YYYYMMDD');
            
            str += '<br/>';
            str += friendlydate.format(document.getElementById("format").value) + ', Missed';
            var flag = false;
            if (s.missedIn) {
                str += document.getElementById("MissedIn").value;
                flag = true;
            }
            if (s.missedOut) {
                if (flag)
                    str += ' and ';
                str += document.getElementById("MissedOut").value;
            }
            return str + "</div>";

        }
        function displayAbsense(s) {
            var str = "<div style=" + getStyle() + ">" + s.name;

            str += '<br/>';
            str += s.positionName + ', ' + s.branchName;

            return str;

        }
        function displayLeaves(s) {
            var str = "<div style=" + getStyle() + ">" + s.name;

            str += '<br/>';
            str += s.destination + ', ' + moment(s.endDate).format(document.getElementById('format').value);

            return str;

        }
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";
            }
            return out;
        }
        function startRefresh() {
            setInterval(RefreshAllGrids, 60000);
        }
        function RefreshAllGrids() {
            if (window.
                parent.App.tabPanel.getActiveTab().id == "dashboard" || (window.parent.App.tabPanel.getActiveTab().id == "tabHome" && window.parent.App.commonTree.getTitle() == 'Time Management')) {
                /* Not Chained
                App.activeStore.reload();
                App.absenseStore.reload();
                App.latenessStore.reload();
                App.missingPunchesStore.reload();
                App.checkMontierStore.reload();
                App.outStore.reload();*/
                /*Chained*/
                App.activeStore.reload({ callback: function () { App.absenseStore.reload({ callback: function () { App.latenessStore.reload({ callback: function () { App.missingPunchesStore.reload({ callback: function () { App.checkMontierStore.reload({ callback: function () { App.outStore.reload(); } }); } }); } }); } }); } });
            }
            else {
                // alert('No Refresh');
            }
        }
    </script>
</head>
<body style="background: url(Images/bg.png) repeat;" onload="startRefresh();">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="MissedIn" runat="server" Text="<%$ Resources: FieldMissedIn %>" />
        <ext:Hidden ID="MissedOut" runat="server" Text="<%$ Resources:FieldMissedOut %>" />
        <ext:Hidden ID="rtl" runat="server"  />
        <ext:Hidden ID="format" runat="server" />



        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">

            <Items>

                <ext:Panel
                    ID="Center"
                    runat="server"
                    Border="false"
                    Layout="BorderLayout" AutoScroll="true"
                    Margins="0 0 0 0"
                    Region="Center">
                    <TopBar>
                        <ext:Toolbar runat="server">
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
                                    <Listeners>
                                        <FocusLeave Handler="RefreshAllGrids();" />
                                    </Listeners>
                                    <Items>
                                        <ext:ListItem Text="-----All-----" Value="0" />
                                    </Items>
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
                                    <Listeners>
                                        <FocusLeave Handler="RefreshAllGrids();" />
                                        
                                    </Listeners>
                                    <Items>
                                        <ext:ListItem Text="-----All-----" Value="0" />
                                    </Items>

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
                                    <Listeners>
                                        <FocusLeave Handler="RefreshAllGrids();" />
                                    </Listeners>
                                    <Items>
                                        <ext:ListItem Text="-----All-----" Value="0" />
                                    </Items>
                                </ext:ComboBox>

                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel AutoScroll="true" Border="true" Split="true" Header="false" Resizable="true" ID="leftPanel" Region="East" MaxWidth="600" Layout="FitLayout" runat="server" Collapsible="true" CollapseMode="Mini" CollapseDirection="Left">
                            <Items>
                                <ext:Label runat="server" Text="<%$ Resources: MissingPunchesGridTitle %>" StyleSpec="color:darkorange;font-weight:bold" />
                                <ext:GridPanel MarginSpec="0 0 0 0"
                                    ID="missingPunchesGrid"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Title="<%$ Resources: MissingPunchesGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"   HideHeaders="true"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store
                                            ID="missingPunchesStore" PageSize="30"
                                            runat="server" OnReadData="missingPunchesStore_ReadData"
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
                                                <ext:Model ID="Model5" runat="server" IDProperty="recordId">
                                                    <Fields>

                                                        <ext:ModelField Name="employeeId" />
                                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                                        <ext:ModelField Name="dayId" />
                                                        <ext:ModelField Name="missedIn" />
                                                        <ext:ModelField Name="missedOut" />
                                                        <ext:ModelField Name="time" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel5" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>

                                            <ext:Column Visible="false" ID="Column7" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75" Align="Center">
                                                <Renderer Handler="  return displayMissedPunchRecord(record.data);">
                                                </Renderer>
                                            </ext:Column>
                                            <%--   <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Flex="2" Hideable="false">
                                        <Renderer Handler="try {var s = record.data['date'].split('T'); return s[0];} catch(err){return record.data['date'];}"></Renderer>
                                    </ext:Column>
                                    <ext:CheckColumn MenuDisabled="true" runat="server" Text="<%$ Resources: FieldMissedIn%>" DataIndex="missedIn" Flex="2" Hideable="false" />
                                    <ext:CheckColumn MenuDisabled="true" runat="server" Text="<%$ Resources: FieldMissedOut%>" DataIndex="missedOut" Flex="2" Hideable="false" />
                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTime%>" DataIndex="time" Flex="2" Hideable="false" />--%>
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
                                <ext:Label runat="server" Text="<%$ Resources: AbsenseGridTitle %>" StyleSpec="color:darkorange;font-weight:bold" />
                                <ext:GridPanel MarginSpec="0 0 0 0"
                                    ID="absenseGrid"
                                    runat="server" HideHeaders="true"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Title="<%$ Resources: AbsenseGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical" Height="200" MaxHeight="200"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                                    <Store>
                                        <ext:Store
                                            ID="absenseStore"
                                            runat="server" OnReadData="absenseStore_ReadData"
                                            PageSize="5">

                                            <Model>
                                                <ext:Model ID="Model2" runat="server" IDProperty="recordId">
                                                    <Fields>

                                                        <ext:ModelField Name="employeeId" />
                                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                                        <ext:ModelField Name="positionName" />
                                                        <ext:ModelField Name="departmentName" />
                                                        <ext:ModelField Name="branchName" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>

                                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>

                                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75"/>
                                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75" >
                                                <Renderer Handler=" return displayAbsense(record.data);  ">
                                                </Renderer>
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
                                <ext:Label runat="server" Text="<%$ Resources: LeavesGridTitle %>" StyleSpec="color:darkorange;font-weight:bold" />
                                <ext:GridPanel MarginSpec="0 0 0 0"
                                    ID="leaveGrid" HideHeaders="true"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Title="<%$ Resources: LeavesGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store PageSize="30"
                                            ID="leavesStore"
                                            runat="server" OnReadData="leavesStore_ReadData"
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
                                                <ext:Model ID="Model4" runat="server" IDProperty="recordId">
                                                    <Fields>

                                                        <ext:ModelField Name="employeeId" />
                                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                                        <ext:ModelField Name="destination" />
                                                        <ext:ModelField Name="ltName" />
                                                        <ext:ModelField Name="startDate" ServerMapping="startDate.ToShortDateString()" />
                                                        <ext:ModelField Name="endDate" ServerMapping="endDate.ToShortDateString()" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>
                                            <ext:Column Visible="false" ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75"  />
                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                                <Renderer Handler=" return displayLeaves(record.data);" />
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
                        <ext:Panel AnchorHorizontal="true" ID="rightPanel" Region="Center" runat="server" Layout="FitLayout" AutoScroll="true">
                            <Items>
                                <ext:Label runat="server" Text="<%$ Resources: ActiveGridTitle %>" StyleSpec="color:darkorange;font-weight:bold" />
                                <ext:GridPanel MarginSpec="0 17 0 0"
                                    ID="activeGrid" MaxHeight="200"
                                    runat="server"
                                    PaddingSpec="0 0 0 0"
                                    Header="false"
                                    Title="<%$ Resources: ActiveGridTitle %>"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit">
                                    <Store>
                                        <ext:Store
                                            ID="activeStore"
                                            runat="server" OnReadData="activeStore_refresh"
                                            RemoteSort="True" PageSize="30"
                                            RemoteFilter="true">
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

                                                        <ext:ModelField Name="employeeId" />
                                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                                        <ext:ModelField Name="time" />
                                                        <ext:ModelField Name="checkStatus" />
                                                        <ext:ModelField Name="positionName" />
                                                        <ext:ModelField Name="departmentName" />
                                                        <ext:ModelField Name="branchName" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>

                                        </ext:Store>
                                    </Store>



                                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>

                                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75"  />
                                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false"   />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTime%>" DataIndex="time" Width="55" Hideable="false" />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />











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

                                <ext:Label runat="server" Text="<%$ Resources: LatenessGridTitle %>" StyleSpec="color:darkorange;font-weight:bold" />
                                <ext:GridPanel ExpandToolText="expand"
                                    ID="latenessGrid" MarginSpec="0 17 0 0"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 0 0"
                                    Header="false" CollapseMode="Header" Collapsible="true" CollapseDirection="Right"
                                    Title="<%$ Resources: LatenessGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store PageSize="30"
                                            ID="latenessStore"
                                            runat="server" OnReadData="latenessStore_ReadData"
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
                                                <ext:Model ID="Model3" runat="server" IDProperty="recordId">
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


                                    <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
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
                                        <ext:GridView ID="GridView3" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel2" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>
                                <ext:Label runat="server" Text="<%$ Resources: OutGridTitle %>" StyleSpec="color:darkorange;font-weight:bold" />
                                <ext:GridPanel
                                    ID="outGrid" MarginSpec="0 17 0 0"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 0 0"
                                    Header="false"
                                    Title="<%$ Resources: OutGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store PageSize="30"
                                            ID="outStore"
                                            runat="server" OnReadData="outStore_ReadData"
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
                                                <ext:Model ID="Model6" runat="server" IDProperty="recordId">
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


                                    <ColumnModel ID="ColumnModel7" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>
                                            <ext:Column Visible="false" ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75"  />
                                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false"   />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTime%>" DataIndex="time" Width="55" Hideable="false" />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />
                                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />


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

                                <ext:Label runat="server" Text="<%$ Resources: CheckMoniterGridTitle %>" StyleSpec="color:darkorange;font-weight:bold" />
                                <ext:GridPanel
                                    ID="checkMoniterGrid" MarginSpec="0 17 0 0"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 0 0"
                                    Header="false"
                                    Title="<%$ Resources: CheckMoniterGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store runat="server" PageSize="30" ID="checkMontierStore" RemoteFilter="true" RemoteSort="true" OnReadData="checkMontierStore_ReadData" AutoLoad="true" AutoSync="true">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="figureTitle" />
                                                        <ext:ModelField Name="count" />
                                                        <ext:ModelField Name="rate" />
                                                    </Fields>
                                                </ext:Model>

                                            </Model>
                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel6" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>


                                            <ext:Column Flex="3" ID="Column26" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldMonitor %>" DataIndex="figureTitle" Hideable="false" Width="75"  />
                                            <ext:Column CellCls="cellLink" ID="Column27" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCount%>" DataIndex="count" Flex="1" Hideable="false" />

                                            <ext:ProgressBarColumn Flex="2" DataIndex="rate" runat="server" />



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
                        </ext:Panel>
                    </Items>
                </ext:Panel>


            </Items>
        </ext:Viewport>






    </form>
</body>
</html>