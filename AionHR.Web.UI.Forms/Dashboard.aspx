<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AionHR.Web.UI.Forms.Dashboard" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Dashboard.css?id=17" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Dashboard.js"></script>
    <!--  <script type="text/javascript" src="Scripts/app.js"></script>-->
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript" src="Scripts/CircileProgress.js"></script>

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

            RefreshAllGrids();
            setInterval(RefreshAllGrids, 60000);
        }
        function RefreshAllGrids() {

            if (window.
                parent.App.tabPanel.getActiveTab().id == "dashboard" || (window.parent.App.tabPanel.getActiveTab().id == "tabHome" && window.parent.App.activeModule.value == 4)) {
                //Not Chained

                App.activeStore.reload();

                //App.absenseStore.reload();
                //App.latenessStore.reload();
                //App.missingPunchesStore.reload();
                //App.checkMontierStore.reload();
                //App.outStore.reload();
                //App.attendanceChartStore.reload();
                //App.InChartStore.reload();
                //App.OutChartStore.reload();
                App.LeaveRequestsStore.reload();
                App.LoansStore.reload();
                App.OverDueStore.reload();
                App.AlertsStore.reload();
                /*Chained*/

                //App.activeStore.reload(
                //       { 
                //           callback: function () {
                //               alert('active called');
                //               App.absenseStore.reload(
                //                    {
                //                        callback: function () {
                //                            alert('absenseStore called');
                //                            App.latenessStore.reload(
                //                                {
                //                                    callback: function () {
                //                                        App.missingPunchesStore.reload(
                //                                            {
                //                                                callback: function () {
                //                                                    App.checkMontierStore.reload(
                //                                                        {
                //                                                            callback: function () {
                //                                                                App.outStore.reload(
                //                                                                       {
                //                                                                           callback: function () {
                //                                                                               App.attendanceChartStore.reload(
                //                                                                                {
                //                                                                                    callback: function () {
                //                                                                                        App.InChartStore.reload(
                //                                                                                            {
                //                                                                                                callback: function () {
                //                                                                                                    App.OutChartStore.reload(
                //                                                                                                        {
                //                                                                                                            callback: function () {
                //                                                                                                                App.LeaveRequestsStore.reload(
                //                                                                                                                    {
                //                                                                                                                        callback: function () {
                //                                                                                                                            App.LoansStore.reload(
                //                                                                                                                                {
                //                                                                                                                                    callback: function () {
                //                                                                                                                                            App.AlertsStore.reload();
                //                                                                                                                                    }
                //                                                                                                                                });
                //                                                                                                                        }
                //                                                                                                                    });
                //                                                                                                            }
                //                                                                                                        });
                //                                                                                                }
                //                                                                                            });
                //                                                                                    }
                //                                                                                });
                //                                                                           }
                //                                                                       });
                //                                                            }
                //                                                        });
                //                                                }
                //                                            });
                //                                    }
                //                                });
                //                        }
                //                    });
                //           }
                //       });
            }
            else {
                // alert('No Refresh');
            }
        }
        var tipRenderer = function (toolTip, record, context) {
            var total = 0;

            App.Chart1.getStore().each(function (rec) {
                total += rec.get('Count');
            });

            toolTip.setTitle(record.get('Name') + ': ' + Math.round(record.get('Count') / total * 100) + '%');
        };
        var bar2, bar3, bar4, bar5;
        function drawChart(wrapper, value, of, divHandle) {

            if (wrapper.bar != null) {

                wrapper.bar.animate(value / of);
                return;
            }
            var bar = new ProgressBar.Circle(divHandle, {
                color: '#000',

                strokeWidth: 5,
                trailWidth: 4,
                easing: 'easeInOut',
                duration: 1400,
                text: {
                    autoStyleContainer: false,

                },
                from: { color: '#3AA8CB', width: 5 },
                to: { color: '#333', width: 4 },

                step: function (state, circle) {
                    circle.path.setAttribute('stroke', state.color);
                    circle.path.setAttribute('stroke-width', state.width);

                    var v = value;
                    if (value === 0) {
                        circle.setText(v);
                    } else {
                        circle.setText(v);
                    }

                }
            });

            bar.text.style.fontSize = '4rem';



            bar.animate(value / of);  // Number from 0.0 to 1.0
            wrapper.bar = bar;
        }
        function chart2(t, of) {

            var wrapper = { bar: bar2 };
            drawChart(wrapper, t, of, Chart2Container);
            bar2 = wrapper.bar;

        }
        function chart3(t, of) {

            var wrapper = { bar: bar3 };
            drawChart(wrapper, t, of, Chart3Container);
            bar3 = wrapper.bar;
        }
        function loansChart(t, of) {

            var wrapper = { bar: bar4 };
            drawChart(wrapper, t, of, loansChartCont);
            bar4 = wrapper.bar;

        }
        function leavesChart(t, of) {

            var wrapper = { bar: bar5 };
            drawChart(wrapper, t, of, leavesChartCont);
            bar5 = wrapper.bar;
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
        <ext:Hidden ID="rtl" runat="server" />
        <ext:Hidden ID="activeCount" runat="server" />
        <ext:Hidden ID="abbsenseCount" runat="server" />
        <ext:Hidden ID="leaveCount" runat="server" />
        <ext:Hidden ID="latensessCount" runat="server" />
        <ext:Hidden ID="outCount" runat="server" />
        <ext:Hidden ID="mpCount" runat="server" />
        <ext:Hidden ID="format" runat="server" />



        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">

            <Items>

                <ext:Panel
                    ID="Center"
                    runat="server"
                    Border="false"
                    Layout="HBoxLayout" AutoScroll="true"
                    Margins="0 0 0 0" StyleHtmlCls="withBackground" Cls="withBackground" BodyCls="withBackground"
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
                        <ext:Panel runat="server" Width="20" Height="770" StyleHtmlCls="withBackground" BodyCls="withBackground"></ext:Panel>
                        <ext:Panel AutoScroll="true" Border="true" Split="false" Header="false" Resizable="false" ID="leftPanel" Region="East" MaxWidth="600" Layout="FitLayout" runat="server" Collapsible="true" CollapseMode="Mini" CollapseDirection="Left">
                            <Items>

                                <ext:GridPanel MarginSpec="0 0 0 0" Hidden="true"
                                    ID="missingPunchesGrid"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Title="<%$ Resources: MissingPunchesGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User" HideHeaders="true"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store
                                            ID="missingPunchesStore" PageSize="30"
                                            runat="server" OnReadData="missingPunchesStore_ReadData"
                                            RemoteSort="false"
                                            RemoteFilter="false">
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


                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="rightPanel" StyleHtmlCls="withBackground" BodyCls="withBackground" Region="Center" runat="server" AutoScroll="true" Layout="VBoxLayout" AnchorHorizontal="true" Width="960" StyleSpec="padding-top:20px;">
                            <Items>
                                <ext:GridPanel
                                    ID="outGrid" MarginSpec="0 17 0 0"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 0 0" Hidden="true"
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
                                            RemoteSort="false"
                                            RemoteFilter="false">
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
                                            <ext:Column Visible="false" ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
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

                                <ext:Label Hidden="true" runat="server" Text="<%$ Resources: CheckMoniterGridTitle %>" StyleSpec="color:darkorange;font-weight:bold" />
                                <ext:GridPanel
                                    ID="checkMoniterGrid" MarginSpec="0 17 0 0"
                                    runat="server" Height="200" MaxHeight="200"
                                    PaddingSpec="0 0 0 0"
                                    Header="false" Hidden="true"
                                    Title="<%$ Resources: CheckMoniterGridTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store runat="server" PageSize="30" ID="checkMontierStore" RemoteFilter="false" RemoteSort="false" OnReadData="checkMontierStore_ReadData" AutoLoad="false" AutoSync="false">
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


                                            <ext:Column Flex="3" ID="Column26" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldMonitor %>" DataIndex="figureTitle" Hideable="false" Width="75" />
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

                                <ext:TabPanel Plain="true" StyleHtmlCls="withBackground" BodyCls="withBackground" runat="server" PaddingSpec="0 0 0 0" Width="960" Height="400" StyleSpec="border-radius: 25px;">
                                    <Items>
                                        <ext:Panel runat="server" Title="<%$Resources:Attendance %>" Layout="HBoxLayout" Height="400">
                                            <Items>
                                                <ext:Panel runat="server" Layout="VBoxLayout">
                                                    <Items>

                                                        <ext:PolarChart
                                                            ID="Chart1"
                                                            runat="server"
                                                            Shadow="true" Width="300" Height="300"
                                                            InsetPadding="20"
                                                            MarginSpec="30 0 0 30"
                                                            PaddingSpec="10 0 0 10"
                                                            InnerPadding="30">

                                                            <Store>
                                                                <ext:Store
                                                                    runat="server"
                                                                    ID="attendanceChartStore"
                                                                    OnReadData="attendanceChartStore_ReadData"
                                                                    AutoDataBind="false">
                                                                    <Model>
                                                                        <ext:Model runat="server">
                                                                            <Fields>
                                                                                <ext:ModelField Name="Name" />
                                                                                <ext:ModelField Name="Count" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                </ext:Store>
                                                            </Store>
                                                            <AnimationConfig Duration="500"></AnimationConfig>
                                                            <Interactions>
                                                            </Interactions>
                                                            <Series>
                                                                <ext:PieSeries
                                                                    AngleField="Count"
                                                                    ShowInLegend="true"
                                                                    Donut="35"
                                                                    HighlightMargin="20">
                                                                    <%--                <Label Field="Name" Display="Rotate" FontSize="18" FontFamily="Arial" />
                                                            <Label Field="Name" Display="Over" FontSize="18" FontFamily="Arial" />--%>
                                                                    <Label Field="Name" Display="Under" FontSize="18" FontFamily="Arial" />
                                                                    <Tooltip runat="server" TrackMouse="true" Width="140" Height="28">
                                                                        <Items>
                                                                        </Items>
                                                                        <Renderer Fn="tipRenderer" />
                                                                    </Tooltip>
                                                                    <Listeners>
                                                                        <ItemDblClick Handler="alert('hi');" />
                                                                    </Listeners>
                                                                </ext:PieSeries>
                                                            </Series>
                                                            <Items>
                                                            </Items>
                                                        </ext:PolarChart>
                                                        <ext:Label runat="server" Text="<%$Resources:All %>" PaddingSpec="0 0 0 100" />

                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel runat="server" Layout="VBoxLayout">
                                                    <Items>
                                                        <ext:PolarChart
                                                            ID="PolarChart6"
                                                            runat="server"
                                                            Shadow="true"
                                                            Width="300" Height="300"
                                                            InsetPadding="20"
                                                            MarginSpec="30 0 0 30"
                                                            InnerPadding="30">

                                                            <Store>
                                                                <ext:Store
                                                                    runat="server"
                                                                    ID="InChartStore"
                                                                    OnReadData="InChartStore_ReadData">
                                                                    <Model>
                                                                        <ext:Model runat="server">
                                                                            <Fields>
                                                                                <ext:ModelField Name="Name" />
                                                                                <ext:ModelField Name="Count" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                </ext:Store>
                                                            </Store>
                                                            <AnimationConfig Duration="500"></AnimationConfig>
                                                            <Interactions>
                                                            </Interactions>
                                                            <Series>
                                                                <ext:PieSeries
                                                                    AngleField="Count"
                                                                    ShowInLegend="true"
                                                                    Donut="35"
                                                                    HighlightMargin="20">
                                                                    <%--                <Label Field="Name" Display="Rotate" FontSize="18" FontFamily="Arial" />
                                                            <Label Field="Name" Display="Over" FontSize="18" FontFamily="Arial" />--%>
                                                                    <Label Field="Name" Display="Under" FontSize="18" FontFamily="Arial" />
                                                                    <Tooltip runat="server" TrackMouse="true" Width="140" Height="28">
                                                                        <Items>
                                                                        </Items>
                                                                        <Renderer Fn="tipRenderer" />
                                                                    </Tooltip>
                                                                    <Listeners>
                                                                        <ItemDblClick Handler="alert('hi');" />
                                                                    </Listeners>
                                                                </ext:PieSeries>
                                                            </Series>
                                                            <Items>
                                                            </Items>
                                                        </ext:PolarChart>
                                                        <ext:Label runat="server" Text="<%$Resources:In %>" PaddingSpec="0 0 0 100" />
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel runat="server" Layout="VBoxLayout">
                                                    <Items>
                                                        <ext:PolarChart
                                                            ID="PolarChart7"
                                                            runat="server"
                                                            Shadow="true"
                                                            Width="300" Height="300"
                                                            InsetPadding="20"
                                                            MarginSpec="30 0 0 30"
                                                            InnerPadding="30">

                                                            <Store>
                                                                <ext:Store
                                                                    runat="server"
                                                                    ID="OutChartStore"
                                                                    OnReadData="OutChartStore_ReadData"
                                                                    AutoDataBind="false">
                                                                    <Model>
                                                                        <ext:Model runat="server">
                                                                            <Fields>
                                                                                <ext:ModelField Name="Name" />
                                                                                <ext:ModelField Name="Count" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                </ext:Store>
                                                            </Store>
                                                            <AnimationConfig Duration="500"></AnimationConfig>
                                                            <Interactions>
                                                            </Interactions>
                                                            <Series>
                                                                <ext:PieSeries
                                                                    AngleField="Count"
                                                                    ShowInLegend="true"
                                                                    Donut="35"
                                                                    HighlightMargin="20">
                                                                    <%--                <Label Field="Name" Display="Rotate" FontSize="18" FontFamily="Arial" />
                                                            <Label Field="Name" Display="Over" FontSize="18" FontFamily="Arial" />--%>
                                                                    <Label Field="Name" Display="Under" FontSize="18" FontFamily="Arial" />
                                                                    <Tooltip runat="server" TrackMouse="true" Width="140" Height="28">
                                                                        <Items>
                                                                        </Items>
                                                                        <Renderer Fn="tipRenderer" />
                                                                    </Tooltip>
                                                                    <Listeners>
                                                                        <ItemDblClick Handler="alert('hi');" />
                                                                    </Listeners>
                                                                </ext:PieSeries>
                                                            </Series>
                                                            <Items>
                                                            </Items>
                                                        </ext:PolarChart>
                                                        <ext:Label runat="server" Text="<%$Resources:Out %>" PaddingSpec="0 0 0 100" />
                                                    </Items>
                                                </ext:Panel>






                                            </Items>

                                        </ext:Panel>
                                        <ext:GridPanel MarginSpec="0 0 0 0"
                                            ID="activeGrid" Layout="FitLayout"
                                            runat="server"
                                            PaddingSpec="0 0 0 0"
                                            Header="false"
                                            Title="<%$ Resources: ActiveGridTitle %>"
                                            Scroll="Vertical"
                                            Border="false"
                                            
                                            ColumnLines="True" >
                                            <Store>
                                                <ext:Store
                                                    ID="activeStore"
                                                    runat="server" OnReadData="activeStore_refresh"
                                                    RemoteSort="false" PageSize="30"
                                                    RemoteFilter="false">
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

                                                    <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTime%>" DataIndex="time" Width="55" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />











                                                </Columns>
                                            </ColumnModel>


                                            <View>
                                                <ext:GridView Border="false" ID="GridView1" runat="server" />
                                            </View>


                                            <SelectionModel>
                                                <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                            </SelectionModel>
                                        </ext:GridPanel>
                                        <ext:GridPanel ExpandToolText="expand"
                                            ID="latenessGrid" MarginSpec="0 0 0 0"
                                            runat="server"
                                            PaddingSpec="0 0 0 0"
                                            Header="false" CollapseMode="Header" Collapsible="true" CollapseDirection="Right"
                                            Title="<%$ Resources: LatenessGridTitle %>"
                                            Layout="FitLayout"
                                            Scroll="Vertical"
                                            Border="false"
                                            
                                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                            <Store>
                                                <ext:Store PageSize="30"
                                                    ID="latenessStore"
                                                    runat="server" OnReadData="latenessStore_ReadData"
                                                    RemoteSort="false"
                                                    RemoteFilter="false">
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
                                                    <ext:Column Visible="false" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
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
                                        <ext:GridPanel MarginSpec="0 0 0 0"
                                            ID="leaveGrid" 
                                            runat="server"
                                            PaddingSpec="0 0 1 0"
                                            Header="false"
                                            Title="<%$ Resources: LeavesGridTitle %>"
                                            Layout="FitLayout"
                                            Scroll="Vertical"
                                            Border="false"
                                            
                                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                            <Store>
                                                <ext:Store PageSize="30"
                                                    ID="leavesStore"
                                                    runat="server" OnReadData="leavesStore_ReadData"
                                                    RemoteSort="false"
                                                    RemoteFilter="false">
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
                                                    <ext:Column Visible="false" ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
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
                                        <ext:GridPanel MarginSpec="0 0 0 0"
                                            ID="absenseGrid"
                                            runat="server" 
                                            PaddingSpec="0 0 0 0"
                                            Header="false"
                                            Title="<%$ Resources: AbsenseGridTitle %>"
                                            Layout="FitLayout"
                                            Scroll="Vertical"
                                            Border="false"
                                          
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

                                                    <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
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
                                    </Items>
                                </ext:TabPanel>
                                <ext:Panel runat="server" Width="950" Height="20" BodyCls="withBackground" PaddingSpec="0 0 0 0" />
                                <ext:Panel runat="server" Layout="HBoxLayout" BodyCls="withBackground" PaddingSpec="0 0 0 0">
                                    <Items>
                                        <ext:TabPanel Plain="true" runat="server" Width="470" Height="300" StyleSpec="border-radius: 25px;">
                                            <Items>
                                                <ext:Panel runat="server" Layout="HBoxLayout" Title="<%$Resources:Tasks %>">
                                                    <Items>
                                                        <ext:Panel runat="server" Width="30" />
                                                        <ext:Panel runat="server" Layout="VBoxLayout">

                                                            <Items>
                                                                <ext:Panel runat="server" Height="20" />
                                                                <ext:Label runat="server" Text="223" X="100" Y="200" PaddingSpec="200 0 0 200" Floating="true" />
                                                                <ext:Panel runat="server" Width="200" Height="200">
                                                                    <Content>
                                                                        <div id="Chart2Container" style="margin: 20px;"></div>
                                                                    </Content>
                                                                </ext:Panel>
                                                                <ext:PolarChart Hidden="true"
                                                                    ID="PolarChart2" Width="200" Height="200"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">
                                                                    <Listeners>
                                                                    </Listeners>
                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />
                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="Store3"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd" Title="ss">

                                                                            <Listeners>
                                                                                <ChartAttached Handler=" this.setTitle('ss'); " />
                                                                            </Listeners>

                                                                            <Label SpriteID="dee" Field="Count"
                                                                                Text="d" Display="over" CalloutLine="false" ZIndex="10" FontSize="40" X="120" FillOpacity="0.5">
                                                                                <%--<Renderer Handler="alert(App.PolarChart2.sprites[0].text); App.PolarChart2.sprites[0].text='fef'; if(App.PolarChart2.sprites[0].text=='d')App.PolarChart2.redraw();  sprite.x=75; sprite.y=120; alert(sprite.x);  if(index!=0) return' '; else return text;" />--%>
                                                                            </Label>
                                                                        </ext:PieSeries>
                                                                        <%--   <ext:GaugeSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd" TotalAngleDegrees="170" >
                                                     <Label SpriteID="dee" Field="Count"  Text="ddd" Display="over" CalloutLine="false" ZIndex="10" FontSize="40" TranslationX="-50" TranslationY="70" X="75" Y="120" FillOpacity="0.5" >
                                                         <Renderer Handler="alert(text);">
                                                             
                                                         </Renderer>
                                                         </Label>
                                                    </ext:GaugeSeries>--%>
                                                                    </Series>

                                                                    <Items>
                                                                        <ext:ChartLabel X="75" Y="120" Field="Count" Text="d" SpriteID="der" Display="over">

                                                                            <Renderer Handler="alert('hi'); text='ddd';"></Renderer>
                                                                        </ext:ChartLabel>

                                                                    </Items>

                                                                </ext:PolarChart>
                                                                <ext:Label runat="server" PaddingSpec="0 0 0 80" Text="<%$Resources:OverDue %>" />
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel runat="server" Layout="VBoxLayout">

                                                            <Items>
                                                                <ext:Panel runat="server" Height="20" />
                                                                <ext:Panel runat="server" Width="200" Height="200">
                                                                    <Content>
                                                                        <div id="Chart3Container" style="margin: 20px;"></div>
                                                                    </Content>
                                                                </ext:Panel>
                                                                <ext:PolarChart Hidden="true"
                                                                    ID="PolarChart3" Width="200" Height="200"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">

                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />
                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="Store6"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="20" Colors="#3AA8CB,#ddd">

                                                                            <Label SpriteID="dee" Field="Count"
                                                                                Text="ddd" Display="over" CalloutLine="false" ZIndex="10" FontSize="40" TranslationX="-70" TranslationY="50" X="50" Y="80" FillOpacity="0.5">

                                                                                <Renderer Handler=" if(index!=0) return' '; else return text;" />
                                                                            </Label>
                                                                        </ext:PieSeries>
                                                                        <%--     <ext:GaugeSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd" TotalAngleDegrees="359" >
                                                     <Label Field="Name" Display="Over" />
                                                    </ext:GaugeSeries>--%>
                                                                    </Series>

                                                                    <Items>
                                                                    </Items>
                                                                </ext:PolarChart>
                                                                <ext:Label runat="server" PaddingSpec="0 0 0 80" Text="<%$Resources:DueToday %>" />
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>

                                                <ext:GridPanel MarginSpec="0 0 0 0"
                                                    ID="GridPanel2" HideHeaders="false"
                                                    runat="server"
                                                    PaddingSpec="0 0 1 0"
                                                    Header="false"
                                                    Title="<%$Resources:OverDue %>"
                                                    Layout="FitLayout"
                                                    Scroll="Vertical"
                                                    Border="false"
                                                    
                                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                                    <Store>
                                                        <ext:Store PageSize="30"
                                                            ID="OverDueStore"
                                                            runat="server" OnReadData="OverDueStore_ReadData"
                                                            RemoteSort="false"
                                                            RemoteFilter="false">
                                                            <Proxy>
                                                                <ext:PageProxy>
                                                                    <Listeners>
                                                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                                                    </Listeners>
                                                                </ext:PageProxy>
                                                            </Proxy>
                                                            <Model>
                                                                <ext:Model ID="Model8" runat="server" IDProperty="recordId">
                                                                    <Fields>

                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="assignToName" ServerMapping="assignToName.fullName" />
                                                                        <ext:ModelField Name="dueDate" />
                                                                        <ext:ModelField Name="name" />

                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>

                                                        </ext:Store>
                                                    </Store>


                                                    <ColumnModel ID="ColumnModel9" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                        <Columns>
                                                            <ext:Column Visible="false" ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                            <ext:Column  MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTaskName %>" DataIndex="name" Hideable="false"  Flex="1">
                                                            </ext:Column>
                                                            <ext:Column  MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="assignToName" Hideable="false" Flex="1">
                                                            </ext:Column>
                                                            <ext:DateColumn ID="DateColumn4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDueDate %>" DataIndex="dueDate" Hideable="false"  Width="100">
                                                            </ext:DateColumn>






                                                        </Columns>
                                                    </ColumnModel>


                                                    <View>
                                                        <ext:GridView ID="GridView9" runat="server" />
                                                    </View>


                                                    <SelectionModel>
                                                        <ext:RowSelectionModel ID="rowSelectionModel8" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                                    </SelectionModel>
                                                </ext:GridPanel>
                                                <ext:GridPanel MarginSpec="0 0 0 0"
                                                    ID="GridPanel3"
                                                    runat="server" HideHeaders="false"
                                                    PaddingSpec="0 0 0 0"
                                                    Header="false"
                                                    Title="<%$Resources:DueToday %>"
                                                    Layout="FitLayout"
                                                    Scroll="Vertical"
                                                    Border="false"
                                                    
                                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                                                    <Store>
                                                        <ext:Store
                                                            ID="DueTodayStore"
                                                            runat="server"
                                                            PageSize="20">

                                                            <Model>
                                                                <ext:Model ID="Model9" runat="server" IDProperty="recordId">
                                                                    <Fields>

                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="assignToName" ServerMapping="assignToName.fullName" />
                                                                        <ext:ModelField Name="dueDate" />
                                                                        <ext:ModelField Name="name" />


                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>

                                                        </ext:Store>
                                                    </Store>

                                                    <ColumnModel ID="ColumnModel10" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                        <Columns>

                                                            <ext:Column Visible="false" ID="Column5" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTaskName %>" DataIndex="name" Hideable="false" Width="75">
                                                            </ext:Column>
                                                            <ext:Column Flex="3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="assignToName" Hideable="false" Width="75">
                                                            </ext:Column>









                                                        </Columns>
                                                    </ColumnModel>


                                                    <View>
                                                        <ext:GridView ID="GridView10" runat="server" />
                                                    </View>


                                                    <SelectionModel>
                                                        <ext:RowSelectionModel ID="rowSelectionModel9" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                                    </SelectionModel>
                                                </ext:GridPanel>
                                            </Items>

                                        </ext:TabPanel>
                                        <ext:Panel runat="server" Width="20" Height="300" BodyCls="withBackground" StyleHtmlCls="withBackground" />
                                        <ext:TabPanel IDMode="Client" EnableTheming="false" Plain="true" runat="server" Width="470" Height="300" PaddingSpec="0 0 0 0" StyleSpec="top:29px!important;border-radius: 25px;">
                                            <Defaults>
                                            </Defaults>
                                            <Items>
                                                <ext:Panel runat="server" Layout="HBoxLayout" Title="<%$Resources:Requests %>">
                                                    <Items>
                                                        <ext:Panel runat="server" Width="30" />
                                                        <ext:Panel runat="server" Layout="VBoxLayout">
                                                            <Items>
                                                                <ext:Panel runat="server" Height="20" />
                                                                <ext:Panel runat="server" Width="200" Height="200">
                                                                    <Content>
                                                                        <div id="leavesChartCont" style="margin: 20px;"></div>
                                                                    </Content>
                                                                </ext:Panel>
                                                                <ext:PolarChart Hidden="true"
                                                                    ID="PolarChart4" Width="200" Height="200"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">
                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />
                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="Store7"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd">
                                                                        </ext:PieSeries>
                                                                        <%--     <ext:GaugeSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd" TotalAngleDegrees="359" >
                                                     <Label Field="Name" Display="Over" />
                                                    </ext:GaugeSeries>--%>
                                                                    </Series>

                                                                    <Items>
                                                                        <ext:ChartLabel Width="100" Text="32" X="75" Y="120" FontSize="50" Color="blue" />

                                                                    </Items>
                                                                </ext:PolarChart>
                                                                <ext:Label runat="server" PaddingSpec="0 0 0 80" Text="<%$Resources:Leaves %>" />
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel runat="server" Layout="VBoxLayout">
                                                            <Items>
                                                                <ext:Panel runat="server" Height="20" />
                                                                <ext:Panel runat="server" Width="200" Height="200">
                                                                    <Content>
                                                                        <div id="loansChartCont" style="margin: 20px;"></div>
                                                                    </Content>
                                                                </ext:Panel>
                                                                <ext:PolarChart
                                                                    ID="PolarChart5" Width="200" Height="200" Hidden="true"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">
                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />
                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="Store8"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd">
                                                                        </ext:PieSeries>
                                                                        <%--     <ext:GaugeSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd" TotalAngleDegrees="359" >
                                                     <Label Field="Name" Display="Over" />
                                                    </ext:GaugeSeries>--%>
                                                                    </Series>

                                                                    <Items>
                                                                        <ext:ChartLabel Width="100" Text="32" X="75" Y="120" FontSize="50" Color="blue" />

                                                                    </Items>
                                                                </ext:PolarChart>
                                                                <ext:Label runat="server" PaddingSpec="0 0 0 80" Text="<%$Resources:Loans %>" />
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>

                                                <ext:GridPanel MarginSpec="0 0 0 0"
                                                    ID="GridPanel4" 
                                                    runat="server"
                                                    PaddingSpec="0 0 1 0"
                                                    Header="false"
                                                    Title="<%$Resources:Leaves %>"
                                                    Layout="FitLayout"
                                                    Scroll="Vertical"
                                                    Border="false"
                                                   
                                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                                    <Store>
                                                        <ext:Store PageSize="30"
                                                            ID="LeaveRequestsStore"
                                                            runat="server" OnReadData="LeaveRequestsStore_ReadData"
                                                            RemoteSort="false"
                                                            RemoteFilter="false">
                                                            <Proxy>
                                                                <ext:PageProxy>
                                                                    <Listeners>
                                                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                                                    </Listeners>
                                                                </ext:PageProxy>
                                                            </Proxy>
                                                            <Model>
                                                                <ext:Model ID="Model10" runat="server" IDProperty="recordId">
                                                                    <Fields>

                                                                        <ext:ModelField Name="employeeId" />
                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="employeeName" IsComplex="true" />
                                                                        <ext:ModelField Name="destination" />
                                                                        <ext:ModelField Name="ltName" />
                                                                        <ext:ModelField Name="startDate"/>
                                                                        <ext:ModelField Name="endDate" />

                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>

                                                        </ext:Store>
                                                    </Store>


                                                    <ColumnModel ID="ColumnModel11" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                        <Columns>
                                                            <ext:Column Visible="false" ID="Column9" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" />
                                                            <ext:Column ID="ColName" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                                                                <Renderer Handler=" return  record.data['employeeName'].fullName" />
                                                            </ext:Column>
                                                            <ext:DateColumn ID="DateColumn1" DataIndex="startDate" Text="<%$ Resources: FieldStartDate%>" runat="server" Width="100" />
                                                            <ext:DateColumn ID="DateColumn2" DataIndex="endDate" Text="<%$ Resources: FieldEndDate%>" runat="server" Width="100" />

                                                            <ext:Column ID="Column8" DataIndex="ltName" Text="<%$ Resources: FieldLtName%>" runat="server"  Flex="1" />






                                                        </Columns>
                                                    </ColumnModel>


                                                    <View>
                                                        <ext:GridView ID="GridView11" runat="server" />
                                                    </View>


                                                    <SelectionModel>
                                                        <ext:RowSelectionModel ID="rowSelectionModel10" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                                    </SelectionModel>
                                                </ext:GridPanel>
                                                <ext:GridPanel MarginSpec="0 0 0 0"
                                                    ID="GridPanel5"
                                                    runat="server" 
                                                    PaddingSpec="0 0 0 0"
                                                    Header="false"
                                                    Title="<%$Resources:Loans %>"
                                                    Layout="FitLayout"
                                                    Scroll="Vertical"
                                                    Border="false"
                                                    
                                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                                                    <Store>
                                                        <ext:Store
                                                            ID="LoansStore"
                                                            runat="server" OnReadData="LoansStore_ReadData"
                                                            PageSize="30">

                                                            <Model>
                                                                <ext:Model ID="Model11" runat="server" IDProperty="recordId">
                                                                    <Fields>

                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="employeeId" />
                                                                      
                                                                        <ext:ModelField Name="date" />
                                                                        <ext:ModelField Name="branchName" />
                                                                        <ext:ModelField Name="purpose" />
                                                                        <ext:ModelField Name="amount" />
                                                                        <ext:ModelField Name="ltName" />
                                                                        <ext:ModelField Name="currencyRef" />
                                                                        <ext:ModelField Name="employeeName" IsComplex="true" />

                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>

                                                        </ext:Store>
                                                    </Store>

                                                    <ColumnModel ID="ColumnModel12" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                        <Columns>

                                                            <ext:Column Visible="false" ID="Column10" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                            <ext:Column ID="Column11" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                                                                <Renderer Handler=" return record.data['employeeName'].fullName; ">
                                                                </Renderer>
                                                            </ext:Column>
                                                           <ext:Column ID="Column14" DataIndex="branchName" Text="<%$ Resources: FieldBranch%>" runat="server" Flex="1" />

                                                            <ext:DateColumn ID="DateColumn3" DataIndex="date" Text="<%$ Resources: FieldDate%>" runat="server" Width="100" />

                                                            <ext:Column ID="Column20" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldAmount %>" DataIndex="amount" Hideable="false" Width="100">
                                                                <Renderer Handler="return record.data['currencyRef']+ '&nbsp;'+record.data['amount']; "></Renderer>
                                                            </ext:Column>
                                                          
                                                            <ext:Column ID="Column16" DataIndex="purpose" Text="<%$ Resources: FieldPurpose%>" runat="server" Flex="1" />










                                                        </Columns>
                                                    </ColumnModel>


                                                    <View>
                                                        <ext:GridView ID="GridView12" runat="server" />
                                                    </View>


                                                    <SelectionModel>
                                                        <ext:RowSelectionModel ID="rowSelectionModel11" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                                    </SelectionModel>
                                                </ext:GridPanel>
                                            </Items>

                                        </ext:TabPanel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" Width="20" Height="790" BodyCls="withBackground" Cls="withBackground" StyleHtmlCls="withBackground" />
                        <ext:Panel runat="server" Region="West" Width="420" PaddingSpec="20 0 0 0" Height="740" BodyCls="withBackground" Cls="withBackground" StyleHtmlCls="withBackground" StyleSpec="border-radius: 25px;">
                            <Items>
                                <ext:TabPanel runat="server" Height="770" Plain="true" StyleSpec="border-radius: 25px;">
                                    <Items>
                                        <ext:Panel runat="server" Layout="VBoxLayout" Title="<%$Resources:Alerts %>">
                                            <Items>
                                                <ext:Panel runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Panel runat="server" Layout="VBoxLayout">
                                                            <Items>
                                                                <ext:Panel runat="server" Width="20" />
                                                                <ext:PolarChart
                                                                    ID="Chart3" Width="200" Height="200"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">
                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />

                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="AlertsStore"
                                                                            OnReadData="AlertsStore_ReadData"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                        <ext:NumericAxis Position="Gauge" Minimum="0" Maximum="100" MajorTickSteps="10" Margin="7">
                                                                        </ext:NumericAxis>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd">
                                                                        </ext:PieSeries>
                                                                    </Series>

                                                                    <Items>


                                                                        <ext:ChartLabel Width="100" Text="13" X="75" Y="120" FontSize="50" Color="blue" />
                                                                    </Items>
                                                                </ext:PolarChart>
                                                                <ext:Label runat="server" Text="<%$Resources:Anneversaries %>" PaddingSpec="0 0 0 80" />
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel runat="server" Layout="VBoxLayout">
                                                            <Items>
                                                                <ext:Panel runat="server" Width="20" /> 
                                                                <ext:PolarChart
                                                                    ID="PolarChart1" Width="200" Height="200"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">
                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />
                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="Store2"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd">
                                                                        </ext:PieSeries>
                                                                        <%--     <ext:GaugeSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd" TotalAngleDegrees="359" >
                                                     <Label Field="Name" Display="Over" />
                                                    </ext:GaugeSeries>--%>
                                                                    </Series>

                                                                    <Items>
                                                                        <ext:ChartLabel Width="100" Text="32" X="75" Y="120" FontSize="50" Color="blue" />

                                                                    </Items>
                                                                </ext:PolarChart>
                                                                <ext:HyperlinkButton runat="server" PaddingSpec="0 0 0 80" Text="<%$Resources:Birthdays %>">
                                                                    <Listeners>
                                                                        <Click Handler="App.BirthdaysWindow.show();" />
                                                                    </Listeners>
                                                                </ext:HyperlinkButton>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Panel runat="server" Layout="VBoxLayout">
                                                            <Items>
                                                                <ext:PolarChart
                                                                    ID="PolarChart8" Width="200" Height="200"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">
                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />

                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="Store1"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                        <ext:NumericAxis Position="Gauge" Minimum="0" Maximum="100" MajorTickSteps="10" Margin="7">
                                                                        </ext:NumericAxis>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd">
                                                                        </ext:PieSeries>
                                                                    </Series>

                                                                    <Items>
                                                                        <ext:ChartLabel Width="100" Text="17" X="75" Y="120" FontSize="50" Color="blue" />

                                                                        <ext:ChartLabel Width="100" Text="13" />
                                                                    </Items>
                                                                </ext:PolarChart>
                                                                <ext:Label runat="server" Text="<%$Resources:ComapnyRightToWork %>" PaddingSpec="0 0 0 80" />
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel runat="server" Layout="VBoxLayout">
                                                            <Items>
                                                                <ext:PolarChart
                                                                    ID="PolarChart9" Width="200" Height="200"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">
                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />
                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="Store12"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd">
                                                                        </ext:PieSeries>
                                                                        <%--     <ext:GaugeSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd" TotalAngleDegrees="359" >
                                                     <Label Field="Name" Display="Over" />
                                                    </ext:GaugeSeries>--%>
                                                                    </Series>

                                                                    <Items>
                                                                        <ext:ChartLabel Width="100" Text="31" X="75" Y="120" FontSize="50" Color="blue" />

                                                                    </Items>
                                                                </ext:PolarChart>
                                                                <ext:Label runat="server" PaddingSpec="0 0 0 80" Text="<%$Resources:EmployeeRightToWork %>" />
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Panel runat="server" Layout="VBoxLayout">
                                                            <Items>
                                                                <ext:PolarChart
                                                                    ID="PolarChart10" Width="200" Height="200"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">
                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />

                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="Store14"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                        <ext:NumericAxis Position="Gauge" Minimum="0" Maximum="100" MajorTickSteps="10" Margin="7">
                                                                        </ext:NumericAxis>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd">
                                                                        </ext:PieSeries>
                                                                    </Series>

                                                                    <Items>
                                                                        <ext:ChartLabel Width="100" Text="22" X="75" Y="120" FontSize="50" Color="blue" />

                                                                        <ext:ChartLabel Width="100" Text="13" />
                                                                    </Items>
                                                                </ext:PolarChart>
                                                                <ext:Label runat="server" Text="<%$Resources:SalaryChange %>" PaddingSpec="0 0 0 80" />
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel runat="server" Layout="VBoxLayout">
                                                            <Items>
                                                                <ext:PolarChart
                                                                    ID="PolarChart11" Width="200" Height="200"
                                                                    runat="server"
                                                                    StyleSpec="background:#fff;"
                                                                    InsetPadding="25"
                                                                    Flex="1">
                                                                    <AnimationConfig Easing="BounceOut" Duration="500" />
                                                                    <Store>
                                                                        <ext:Store
                                                                            runat="server"
                                                                            ID="Store15"
                                                                            AutoDataBind="false">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>

                                                                                        <ext:ModelField Name="Count" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <Axes>
                                                                    </Axes>
                                                                    <Series>
                                                                        <ext:PieSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd">
                                                                        </ext:PieSeries>
                                                                        <%--     <ext:GaugeSeries AngleField="Count" Donut="80" Colors="#3AA8CB,#ddd" TotalAngleDegrees="359" >
                                                     <Label Field="Name" Display="Over" />
                                                    </ext:GaugeSeries>--%>
                                                                    </Series>

                                                                    <Items>
                                                                        <ext:ChartLabel Width="100" Text="32" X="75" Y="120" FontSize="50" Color="blue" />

                                                                    </Items>
                                                                </ext:PolarChart>
                                                                <ext:Label runat="server" PaddingSpec="0 0 0 80" Text="<%$Resources:Probation %>" />
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:TabPanel>

                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>


            </Items>
        </ext:Viewport>
        <ext:Window runat="server" Modal="true"
            Hidden="true" AutoShow="false" ID="BirthdaysWindow" Width="200" Height="200">
        </ext:Window>





    </form>
</body>
</html>
