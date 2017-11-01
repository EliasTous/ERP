<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AionHR.Web.UI.Forms.Dashboard" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Dashboard.css?id=29" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Dashboard.js"></script>
    <!--  <script type="text/javascript" src="Scripts/app.js"></script>-->
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript" src="Scripts/CircileProgress.js?id=110"></script>
    <script type="text/javascript" src="Scripts/jquery-new.js?id=1"></script>
    <script type="text/javascript">
        function fixWidth(s) {
            //App.CartesianChart1.setWidth(s * 50);
            //App.CartesianChart1.setHeight(App.att.getHeight()-40);
            //App.barPanel.setWidth(s * 50);
            //App.barPanel.setHeight(App.att.getHeight()-40);
            //alert(App.att.getWidth());

            App.CartesianChart1.setWidth(Math.max(App.att.getWidth(), s * 50));
            App.CartesianChart1.setHeight(App.att.getHeight() - 40);
            App.LocalRateChart.setHeight(App.att.getHeight() - 40);
            App.LocalCount.setHeight(App.att.getHeight() - 40);
            App.barPanel.setWidth(Math.max(App.att.getWidth(), s * 50));

            App.barPanel.setHeight(App.att.getHeight() - 40);
        }
        
        function getStyle() {
            var dir = document.getElementById('rtl').value == 'True' ? 'right' : 'left';
            var s = 'text-align:' + dir;
            return s;
        }
        function getDateFormatted(d) {
            var friendlydate = moment(d, 'YYYYMMDD');
            return friendlydate.format(document.getElementById("format").value);
        }
        function displayLeaveFirstCell(d) {

            var str = "<div style= " + getStyle() + ">" + d.name;
            str += "<br/>";
            str += d.branchName + ", " + d.departmentName;
            str += "</div>";
            return str;
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
        function displayActive(s) {
            var str = "<div style=" + getStyle() + ">" + s.name + '- ' + s.time;
            str += '<br/>';
            str += s.positionName + ', ' + s.branchName;
            return str;
        }
        function displayRTW(s) {
            var str = "<div style=" + getStyle() + ">";
            str += s.documentRef + '- ' + getDateFormatted(s.expiryDate);
            return str;
        }
        function displayEmployeeRTW(s) {
            var str = "<div style=" + getStyle() + ">" + s.name;
            str += '<br/>';
            str += s.documentRef + '- ' + getDateFormatted(s.expiryDate);
            return str;
        }
        function displayBirthday(s) {
            var str = "<div style=" + getStyle() + ">" + s.name + '(' + moment().diff(s.birthDate, 'years') + ')';
            str += '<br/>';
            var nowD = moment();
            str += getDateFormatted(s.birthDate);
            return str;
        }
        function displayAnniversary(s) {
            var str = "<div style=" + getStyle() + ">" + s.name + '(' + moment().diff(s.hireDate, 'years') + ')';
            str += '<br/>';
            var nowD = moment();
            str += getDateFormatted(s.hireDate);
            return str;
        }
        function displayLate(s) {
            var str = "<div style=" + getStyle() + ">" + s.name + '- ' + s.time;
            str += '<br/>';
            str += s.positionName + ', ' + s.branchName;
            str += '</div>';
            return str;
        }
        function displaySCR(s) {
            var str = "<div style=" + getStyle() + ">" + s.name;
            str += '<br/>';
            str += getDateFormatted(s.effectiveDate) + ', ' + s.currencyRef + s.finalAmount;
            str += '</div>';
            return str;
        }
        function displayProbation(s) {
            var str = "<div style=" + getStyle() + ">" + s.name;
            str += '<br/>';
            str += getDateFormatted(s.probationEndDate);
            str += '</div>';
            return str;
        }
        function displayLeaves(s) {

            var str = "<div style=" + getStyle() + ">" + s.name;
            str += '<br/>';
            str += s.destination + ', ' + moment(s.endDate).format(document.getElementById('format').value);
            return str;
        }
        function displayTotalLoans(s) {
            var str = "<div style=" + getStyle() + ">" + s.name;
            str += '<br/>';
            str += getDateFormatted(s.date) + ', ' + s.currencyRef + s.amount;
            str += '</div>';
            return str;
        }
        function displayCompletedLoans(s) {
            var str = "<div style=" + getStyle() + ">" + s.name;
            str += '<br/>';
            str += getDateFormatted(s.date) + ', ' + s.currencyRef + s.amount;
            str += '</div>';
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
            CheckSession();

            if (window.
                parent.App.tabPanel.getActiveTab().id == "dashboard" || (window.parent.App.tabPanel.getActiveTab().id == "tabHome" && (window.parent.App.activeModule.value == 4 || window.parent.App.activeModule.value == 5 || window.parent.App.activeModule.value == 1))) {
                //Not Chained

                App.activeStore.reload();
                App.Store1.reload();

                App.LeaveRequestsStore.reload();
                App.LoansStore.reload();
                App.OverDueStore.reload();
                App.LocalRateStore.reload();
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

                $('.flashing').fadeTo(1000, 0.1, function () { $(this).fadeTo(2000, 1.0); });
            }
            else {
                // alert('No Refresh');
            }
        }
        var tipRenderer = function (toolTip, record, context) {
            var total = 0;



            toolTip.setTitle(context.record.get('emps') + ': ' + context.record.get('Count'));
        };
        var bar2, bar3, bar4, bar5, absentBar, activeBar;
        var annBar, birthdaysBar, empRWBar, compRWBar, scrBar, probBar;
        function drawChart(wrapper, value, of, divHandle) {

            if (wrapper.bar != null) {


                wrapper.bar.destroy();
            }

            var bar = new ProgressBar.Circle(divHandle, {
                color: '#33ABAA',

                strokeWidth: 2,
                trailWidth: 1,
                easing: 'easeInOut',
                duration: 2000,
                text: {
                    autoStyleContainer: false,

                },
                from: { color: '#70BF73', width: 2 },
                to: { color: '#70BF73', width: 2 },

                step: function (state, circle) {
                    circle.path.setAttribute('stroke', state.color);
                    circle.path.setAttribute('stroke-width', state.width);

                    var v = value;
                    var tex = value + '<br/>' + '<br/>' + '<br/>' + '<br/>' + '<br/>' + Math.round((value / of) * 100) + '%';
                    var perc = Math.round((value / of) * 100) + '%';
                    circle.setText(value);

                }
            });

            bar.text.style.fontSize = '4rem';


            if (of == 0)
                of = 1;
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

            //var wrapper = { bar: bar4 };
            //drawChart(wrapper, t, of, loansChartCont);
            //bar4 = wrapper.bar;

        }
        function lateChart(t, of) {

            var wrapper = { bar: bar5 };
            drawChart(wrapper, t, of, leavesChartCont);
            bar5 = wrapper.bar;
        }
        function absentChart(t, of) {

            var wrapper = { bar: absentBar };
            drawChart(wrapper, t, of, absentChartCont);
            absentBar = wrapper.bar;
        }
        function activeChart(t, of) {

            var wrapper = { bar: activeBar };
            drawChart(wrapper, t, of, activeChartCont);
            activeBar = wrapper.bar;
        }
        function suppressZeros(text, sprite, config, rendererData, index) {

            if (text == 0 || text == '0') {

                return '';
            }
        }
        var segmentRenderer = function (sprite, config, rendererData, index) {


            var color = ["#6AA5DC", "#EE929D", "#FDBF00"][index];

            return {
                fillStyle: color
            };
        };
    </script>
</head>
<body style="background: url(Images/bg.png) repeat;">
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
        <ext:Hidden ID="absent" runat="server" Text="<%$ Resources:Absent %>" />
        <ext:Hidden ID="onleave" runat="server" Text="<%$ Resources:OnLeave %>" />

        <ext:Hidden ID="daysLeft" runat="server" Text="<%$ Resources: FieldDaysLeft %>" />
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
        <ext:Store
            ID="activeStore"
            runat="server" OnReadData="activeStore_refresh"
            RemoteSort="false" PageSize="200"
            RemoteFilter="false">

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
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">

            <Items>
                <ext:Panel runat="server" Layout="FitLayout" ID="root">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" />
                                    </Content>
                                </ext:Container>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="esId" Name="esId" EmptyText="<%$ Resources:FieldEHStatus%>">
                                    <Store>
                                        <ext:Store runat="server" ID="statusStore">
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

                                    <Items>
                                        <ext:ListItem Text="<%$Resources:All %>" Value="0" />
                                    </Items>
                                </ext:ComboBox>


                                <ext:ToolbarSeparator runat="server" />

                                <ext:Button runat="server" Text="<%$Resources:Go %>">
                                    <Listeners>
                                        <Click Handler="RefreshAllGrids();" />
                                    </Listeners>
                                </ext:Button>

                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel
                            ID="Center" AnchorVertical="true"
                            runat="server"
                            Border="false"
                            Layout="HBoxLayout" AutoScroll="true"
                            Margins="0 0 0 0" StyleHtmlCls="withBackground" Cls="withBackground" BodyCls="withBackground">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Pack="End" Align="Stretch"></ext:HBoxLayoutConfig>
                            </LayoutConfig>
                            <Items>

                                <ext:Panel runat="server" Flex="1" StyleHtmlCls="withBackground" BodyCls="withBackground"></ext:Panel>
                                <ext:Panel runat="server" ID="right" BodyCls="withBackground" Cls="withBackground" StyleHtmlCls="withBackground" Layout="FitLayout" Flex="30">
                                    <Items>
                                        <ext:Panel ID="rightPanel" BodyCls="withBackground" Cls="withBackground" StyleHtmlCls="withBackground" runat="server" AutoScroll="true" Layout="VBoxLayout" Flex="1" StyleSpec="padding-top:20px;">
                                            <Listeners>
                                                <AfterLayout Handler="  App.active.setWidth(App.att.getWidth()/7);App.active.setHeight(App.att.getWidth()/7); App.PolarChart6.setWidth(App.att.getWidth()/4); App.PolarChart6.setHeight(App.att.getWidth()/4); App.absense.setWidth(App.att.getWidth()/7);App.late.setWidth(App.att.getWidth()/7);App.overdue.setWidth(App.belowt.getWidth()/4);App.today.setWidth(App.belowt.getWidth()/4);"></AfterLayout>
                                            </Listeners>
                                            <LayoutConfig>
                                                <ext:VBoxLayoutConfig Pack="End" Align="Stretch"></ext:VBoxLayoutConfig>
                                            </LayoutConfig>
                                            <Items>

                                                <ext:TabPanel ID="att" Plain="true" Flex="15" Layout="FitLayout" StyleHtmlCls="withBackground" BodyCls="withBackground" runat="server" PaddingSpec="0 0 0 0" StyleSpec="border-radius: 25px;" MinHeight="250">
                                                    <Items>
                                                        <ext:Panel runat="server" Title="<%$Resources:Attendance %>" Layout="HBoxLayout" Flex="1">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch"></ext:HBoxLayoutConfig>
                                                            </LayoutConfig>
                                                            <Items>

                                                                <ext:Panel runat="server" Layout="BorderLayout" Flex="4">
                                                                    <LayoutConfig>
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Panel runat="server" ID="active" Region="Center">
                                                                            <Content>
                                                                                <div id="activeChartCont" style="padding: 10px; width: 100%; height: 100%;"></div>
                                                                            </Content>
                                                                        </ext:Panel>

                                                                        <ext:Panel runat="server" Layout="FitLayout" Region="South">
                                                                            <Items>

                                                                                <ext:Panel runat="server" Layout="HBoxLayout" PaddingSpec="0 0 10 0">
                                                                                    <LayoutConfig>
                                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                                    </LayoutConfig>
                                                                                    <Items>
                                                                                        <ext:Panel runat="server" Flex="1" />

                                                                                        <ext:Panel runat="server" Flex="1">
                                                                                            <Items>
                                                                                                <ext:HyperlinkButton Height="20" runat="server" PaddingSpec="0 0 0 0" Flex="1" Text="<%$Resources:ActiveGridTitle %>" StyleSpec="font-size:16pt;">
                                                                                                    <Listeners>
                                                                                                        <Click Handler="App.activeWindow.show();" />
                                                                                                    </Listeners>
                                                                                                </ext:HyperlinkButton>
                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                    </Items>
                                                                                </ext:Panel>

                                                                            </Items>
                                                                        </ext:Panel>

                                                                    </Items>
                                                                </ext:Panel>
                                                                <ext:Panel runat="server" Flex="1" />
                                                                <ext:Panel runat="server" Layout="BorderLayout" Flex="4">
                                                                    <LayoutConfig>
                                                                    </LayoutConfig>
                                                                    <Items>

                                                                        <ext:Panel runat="server" ID="late" Region="Center">
                                                                            <Content>
                                                                                <div id="leavesChartCont" style="padding: 10px; width: 100%; height: 100%;"></div>
                                                                            </Content>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Layout="FitLayout" Region="South">
                                                                            <Items>
                                                                                <ext:Panel runat="server" Layout="HBoxLayout" PaddingSpec="0 0 10 0">
                                                                                    <LayoutConfig>
                                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                                    </LayoutConfig>
                                                                                    <Items>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                        <ext:Panel runat="server" Flex="1">
                                                                                            <Items>
                                                                                                <ext:HyperlinkButton runat="server" Height="20" PaddingSpec="0 0 0 0" Flex="1" Text="<%$Resources:Late %>" StyleSpec="font-size:16pt;">
                                                                                                    <Listeners>
                                                                                                        <Click Handler="App.lateWindow.show();" />
                                                                                                    </Listeners>
                                                                                                </ext:HyperlinkButton>
                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                    </Items>
                                                                                </ext:Panel>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Height="10" />

                                                                    </Items>
                                                                </ext:Panel>
                                                                <ext:Panel runat="server" Flex="1" />
                                                                <ext:Panel runat="server" Layout="BorderLayout" Flex="4 " Hidden="true">
                                                                    <LayoutConfig>
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Panel runat="server" ID="absense" Region="Center">
                                                                            <Content>
                                                                                <div id="absentChartCont" style="padding: 10px; width: 100%; height: 100%;"></div>
                                                                            </Content>
                                                                        </ext:Panel>

                                                                        <ext:Panel runat="server" Layout="FitLayout" Region="South">
                                                                            <Items>
                                                                                <ext:Panel runat="server" Layout="HBoxLayout" PaddingSpec="0 0 10 0">
                                                                                    <LayoutConfig>
                                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                                    </LayoutConfig>
                                                                                    <Items>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                        <ext:Panel runat="server" Flex="1">
                                                                                            <Items>
                                                                                                <ext:HyperlinkButton runat="server" Height="20" PaddingSpec="0 0 10 0" Flex="1" Text="<%$Resources:Out %>" StyleSpec="font-size:16pt;">
                                                                                                    <Listeners>
                                                                                                        <Click Handler="App.absentWindow.show();" />
                                                                                                    </Listeners>
                                                                                                </ext:HyperlinkButton>
                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                    </Items>
                                                                                </ext:Panel>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Height="10" />
                                                                    </Items>
                                                                </ext:Panel>


                                                                <ext:Panel runat="server" Layout="BorderLayout" Flex="4">
                                                                    <Items>
                                                                        <ext:PolarChart Region="Center"
                                                                            ID="PolarChart6"
                                                                            runat="server"
                                                                            Shadow="true"
                                                                            InsetPadding="1"
                                                                            InnerPadding="1">

                                                                            <Store>
                                                                                <ext:Store runat="server" ID="AbsentLeaveStore">
                                                                                    <Model>
                                                                                        <ext:Model runat="server">
                                                                                            <Fields>
                                                                                                <ext:ModelField Name="Count" />
                                                                                                <ext:ModelField Name="emps" />
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
                                                                                    Donut="50"
                                                                                    HighlightMargin="0">

                                                                                    <%--                <Label Field="Name" Display="Rotate" FontSize="18" FontFamily="Arial" />
                                                            <Label Field="Name" Display="Over" FontSize="18" FontFamily="Arial" />--%>
                                                                                    <Label Field="emps" Display="Inside" FontSize="12" FontFamily="Arial">
                                                                                        <Renderer Handler=" " />
                                                                                    </Label>
                                                                                    <Tooltip runat="server" TrackMouse="true" Width="140" Height="28">

                                                                                        <Renderer Fn="tipRenderer" />
                                                                                    </Tooltip>
                                                                                    <Renderer Fn="segmentRenderer" />
                                                                                </ext:PieSeries>

                                                                            </Series>
                                                                            <Items>
                                                                            </Items>
                                                                        </ext:PolarChart>

                                                                        <ext:Panel runat="server" Layout="FitLayout" Region="South">
                                                                            <Items>
                                                                                <ext:Panel runat="server" Layout="HBoxLayout" PaddingSpec="0 0 10 0">
                                                                                    <LayoutConfig>
                                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                                    </LayoutConfig>
                                                                                    <Items>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                        <ext:Panel runat="server" Flex="4">
                                                                                            <Items>
                                                                                                <ext:HyperlinkButton runat="server" Height="20" PaddingSpec="0 0 10 0" Flex="1" Text="<%$Resources:Out %>" StyleSpec="font-size:16pt;">
                                                                                                    <Listeners>
                                                                                                        <Click Handler="App.absenseStore.reload();App.absentWindow.show();" />
                                                                                                    </Listeners>
                                                                                                </ext:HyperlinkButton>

                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="1">
                                                                                            <Items>
                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="3">
                                                                                            <Items>
                                                                                                <ext:HyperlinkButton runat="server" Height="20" PaddingSpec="0 0 10 0" Flex="1" Text="<%$Resources:Paid %>" StyleSpec="font-size:16pt;">
                                                                                                    <Listeners>
                                                                                                        <Click Handler="App.leavesStore.reload();App.onLeavewindow.show();" />
                                                                                                    </Listeners>
                                                                                                </ext:HyperlinkButton>

                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="1">
                                                                                            <Items>
                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="5">
                                                                                            <Items>
                                                                                                <ext:HyperlinkButton runat="server" Height="20" PaddingSpec="0 0 10 0" Flex="1" Text="<%$Resources:Unpaid %>" StyleSpec="font-size:16pt;">
                                                                                                    <Listeners>
                                                                                                        <Click Handler="App.UnpaidLeavesStore.reload();App.unpaidLeavesWindow.show();" />
                                                                                                    </Listeners>
                                                                                                </ext:HyperlinkButton>

                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                    </Items>
                                                                                </ext:Panel>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Height="10" />
                                                                    </Items>
                                                                </ext:Panel>



                                                            </Items>

                                                        </ext:Panel>
                                                        <ext:Panel runat="server" AutoScroll="true" ID="barPanel" Title="<%$Resources: DepartmentsCount %>">
                                                            <Listeners>
                                                                <AfterLayout Handler=" " />
                                                            </Listeners>
                                                            <Items>
                                                                <ext:CartesianChart
                                                                    ID="CartesianChart1"
                                                                    runat="server"
                                                                    FlipXY="false"
                                                                    AutoScroll="true">
                                                                    <Store>
                                                                        <ext:Store ID="Store1" OnReadData="departments2Count_ReadData"
                                                                            runat="server">
                                                                            <Model>
                                                                                <ext:Model runat="server">
                                                                                    <Fields>
                                                                                        <ext:ModelField Name="departmentName" />
                                                                                        <ext:ModelField Name="checkedOut" />
                                                                                        <ext:ModelField Name="checkedIn" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <LegendConfig runat="server" Dock="Bottom" />

                                                                    <AnimationConfig Duration="500" Easing="EaseOut" />

                                                                    <Items>
                                                                    </Items>
                                                                    <Axes>
                                                                        <%--   <ext:NumericAxis
                                                                            Fields="Count"
                                                                            Position="Left" 
                                                                            Grid="true"
                                                                            >
                                                                            <Renderer Handler="return label.toFixed(0);" />
                                                                        </ext:NumericAxis>--%>

                                                                        <ext:CategoryAxis
                                                                            Fields="departmentName"
                                                                            Position="Bottom">
                                                                            <Label RotationDegrees="-45" />
                                                                        </ext:CategoryAxis>
                                                                    </Axes>

                                                                    <Series>
                                                                        <ext:BarSeries
                                                                            XField="departmentName" Stacked="true"
                                                                            YField="checkedIn,checkedOut" Titles="In,Out">

                                                                            <StyleSpec>

                                                                                <ext:SeriesSprite Opacity="0.8" BarWidth="50" MinBarWidth="50" MinGapWidth="10" BaseColor="#33ABAA" />
                                                                            </StyleSpec>
                                                                            <%--<Renderer Handler="return {fill:'rgb(51, 171, 170)'};" />--%>
                                                                            <Tooltip runat="server">
                                                                                <Renderer Handler="var browser = context.series.getTitle()[Ext.Array.indexOf(context.series.getYField(), context.field)]; toolTip.setHtml(browser + ' for ' + record.get('departmentName') + ': ' + record.get(context.field));" />
                                                                            </Tooltip>
                                                                            <HighlightConfig>
                                                                                <ext:Sprite FillStyle="rgba(69, 143, 210, 1.0)" StrokeStyle="black" LineWidth="2" />
                                                                            </HighlightConfig>
                                                                            <%--<Tooltip runat="server" TrackMouse="true">
                                                                                <Renderer Handler="toolTip.setHtml(record.get('Department') + ': ' + record.get('In')+' /'+ parseInt(parseInt(record.get('In'))+parseInt(record.get('Out' ))));" />
                                                                            </Tooltip>--%>

                                                                            <Label
                                                                                Display="Under" Field="checkedOut,checkedIn" Orientation="Horizontal">
                                                                                <Renderer Fn="suppressZeros" />
                                                                            </Label>

                                                                        </ext:BarSeries>

                                                                    </Series>
                                                                </ext:CartesianChart>
                                                            </Items>
                                                        </ext:Panel>

                                                        <ext:Panel runat="server" AutoScroll="true" ID="Panel1" Layout="FitLayout" Title="<%$Resources: LocalRate %>">
                                                            <Listeners>
                                                                <AfterLayout Handler=" " />

                                                            </Listeners>

                                                            <Items>
                                                                <ext:Panel runat="server" Layout="HBoxLayout">
                                                                    <LayoutConfig>
                                                                        <ext:HBoxLayoutConfig Align="Stretch" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Panel runat="server" Flex="1">

                                                                            <Items>
                                                                                <ext:CartesianChart
                                                                                    ID="LocalRateChart"
                                                                                    runat="server"
                                                                                    FlipXY="false" 
                                                                                    AutoScroll="true">
                                                                                    <Store>
                                                                                        <ext:Store ID="LocalRateStore" OnReadData="LocalRateStore_ReadData"
                                                                                            runat="server">
                                                                                            <Model>
                                                                                                <ext:Model runat="server">
                                                                                                    <Fields>
                                                                                                        <ext:ModelField Name="category" />
                                                                                                        <ext:ModelField Name="number" />

                                                                                                    </Fields>
                                                                                                </ext:Model>
                                                                                            </Model>
                                                                                        </ext:Store>
                                                                                    </Store>
                                                                                   

                                                                                    <AnimationConfig Duration="500" Easing="EaseOut" />

                                                                                    <Items>
                                                                                    </Items>
                                                                                    <Axes>
                                                                                        <%--   <ext:NumericAxis
                                                                            Fields="Count"
                                                                            Position="Left" 
                                                                            Grid="true"
                                                                            >
                                                                            <Renderer Handler="return label.toFixed(0);" />
                                                                        </ext:NumericAxis>--%>

                                                                                        <ext:CategoryAxis
                                                                                            Fields="category"
                                                                                            Position="Bottom">
                                                                                        </ext:CategoryAxis>
                                                                                    </Axes>

                                                                                    <Series>
                                                                                        <ext:BarSeries
                                                                                            XField="category"
                                                                                            YField="number">

                                                                                            <StyleSpec>

                                                                                                <ext:SeriesSprite Opacity="0.8" BarWidth="50" MinBarWidth="50" MinGapWidth="10" BaseColor="#33ABAA" />
                                                                                            </StyleSpec>
                                                                                            <%--<Renderer Handler="return {fill:'rgb(51, 171, 170)'};" />--%>
                                                                                             <Tooltip runat="server">
                                                                                <Renderer Handler="var browser = context.series.getTitle()[Ext.Array.indexOf(context.series.getYField(), context.field)]; toolTip.setHtml(browser + ' for ' + record.get('category') + ': ' + record.get(context.field));" />
                                                                            </Tooltip>
                                                                                            <HighlightConfig>
                                                                                                <ext:Sprite FillStyle="rgba(69, 143, 210, 1.0)" StrokeStyle="black" LineWidth="2" />
                                                                                            </HighlightConfig>
                                                                                            <Tooltip runat="server" TrackMouse="true">
                                                                                <Renderer Handler="toolTip.setHtml(record.get('category') + ': '+record.get('number' ));" />
                                                                            </Tooltip>
                                                                                        </ext:BarSeries>

                                                                                    </Series>
                                                                                </ext:CartesianChart>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Flex="1">
                                                                            <Items>
                                                                                <ext:CartesianChart
                                                                                    ID="LocalCount" 
                                                                                    runat="server"
                                                                                    FlipXY="false"
                                                                                    AutoScroll="true">
                                                                                    <Store>
                                                                                        <ext:Store ID="LocalCountStore"
                                                                                            runat="server">
                                                                                            <Model>
                                                                                                <ext:Model runat="server">
                                                                                                    <Fields>
                                                                                                        <ext:ModelField Name="category" />
                                                                                                        <ext:ModelField Name="number" />

                                                                                                    </Fields>
                                                                                                </ext:Model>
                                                                                            </Model>
                                                                                        </ext:Store>
                                                                                    </Store>

                                                                                    <AnimationConfig Duration="500" Easing="EaseOut" />

                                                                                    <Items>
                                                                                    </Items>
                                                                                    <Axes>
                                                                                        <%--   <ext:NumericAxis
                                                                            Fields="Count"
                                                                            Position="Left" 
                                                                            Grid="true"
                                                                            >
                                                                            <Renderer Handler="return label.toFixed(0);" />
                                                                        </ext:NumericAxis>--%>

                                                                                        <ext:CategoryAxis
                                                                                            Fields="category"
                                                                                            Position="Bottom">
                                                                                            
                                                                                        </ext:CategoryAxis>
                                                                                    </Axes>

                                                                                    <Series>
                                                                                        <ext:BarSeries
                                                                                            XField="category"
                                                                                            YField="number">

                                                                                            <StyleSpec>

                                                                                                <ext:SeriesSprite Opacity="0.8" BarWidth="50" MinBarWidth="50" MinGapWidth="10" BaseColor="#33ABAA" />
                                                                                            </StyleSpec>
                                                                                            <Renderer Handler="return {fill:'rgb(51, 171, 170)'};" />
                                                                                             <Tooltip runat="server">
                                                                                <Renderer Handler="var browser = context.series.getTitle()[Ext.Array.indexOf(context.series.getYField(), context.field)]; toolTip.setHtml(browser + ' for ' + record.get('category') + ': ' + record.get(context.field));" />
                                                                            </Tooltip>
                                                                                            <HighlightConfig>
                                                                                                <ext:Sprite FillStyle="rgba(69, 143, 210, 1.0)" StrokeStyle="black" LineWidth="2" />
                                                                                            </HighlightConfig>
                                                                                            <Tooltip runat="server" TrackMouse="true">
                                                                                <Renderer Handler="toolTip.setHtml(record.get('category') + ': ' + record.get('number'));" />
                                                                            </Tooltip>
                                                                                        </ext:BarSeries>

                                                                                    </Series>
                                                                                </ext:CartesianChart>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                    </Items>
                                                                </ext:Panel>


                                                            </Items>
                                                        </ext:Panel>


                                                        <ext:GridPanel MarginSpec="0 0 0 0"
                                                            ID="missingPunchesGrid" Hidden="true"
                                                            runat="server" HideHeaders="true"
                                                            PaddingSpec="0 0 1 0"
                                                            Header="false"
                                                            Title="<%$ Resources: MissingPunchesGridTitle %>"
                                                            Layout="FitLayout"
                                                            Scroll="Vertical"
                                                            Border="false"
                                                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                                            <Store>
                                                                <ext:Store
                                                                    ID="missingPunchesStore" PageSize="30"
                                                                    runat="server" OnReadData="missingPunchesStore_ReadData"
                                                                    RemoteSort="false"
                                                                    RemoteFilter="false">

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
                                                </ext:TabPanel>
                                                <ext:Panel runat="server" Flex="1" StyleHtmlCls="withBackground" BodyCls="withBackground" />
                                                <ext:TabPanel ID="belowt" IDMode="Client" EnableTheming="false" Plain="true" runat="server" Flex="15" PaddingSpec="0 0 0 0" StyleSpec="border-radius: 25px;">
                                                    <Defaults>
                                                    </Defaults>
                                                    <Items>


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
                                                                                <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                                                                <ext:ModelField Name="destination" />
                                                                                <ext:ModelField Name="ltName" />
                                                                                <ext:ModelField Name="startDate" />
                                                                                <ext:ModelField Name="endDate" />
                                                                                <ext:ModelField Name="branchName" />
                                                                                <ext:ModelField Name="departmentName" />
                                                                                 <ext:ModelField Name="elias" />

                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>

                                                                </ext:Store>
                                                            </Store>


                                                            <ColumnModel ID="ColumnModel11" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                                <Columns>
                                                                    <ext:Column Visible="false" ID="Column9" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" />
                                                                    <ext:Column ID="ColName" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                                                                        <Renderer Handler=" return  displayLeaveFirstCell(record.data);" />
                                                                    </ext:Column>
                                                                    <ext:DateColumn ID="DateColumn1" DataIndex="startDate" Text="<%$ Resources: FieldStartDate%>" runat="server" Width="100" />
                                                                    <ext:DateColumn ID="DateColumn2" DataIndex="endDate" Text="<%$ Resources: FieldEndDate%>" runat="server" Width="100" />

                                                                    <ext:Column ID="Column8" DataIndex="ltName" Text="<%$ Resources: FieldLtName%>" runat="server" Flex="1" />
                                                                      <ext:Column runat="server"
                                                                            ID="colEdit"  Visible="true"
                                                                            Text=""
                                                                            Width="100"
                                                                            Hideable="false"
                                                                            Align="Center"
                                                                            Fixed="true"
                                                                            Filterable="false"
                                                                            MenuDisabled="true"
                                                                            Resizable="false">

                                                                            <Renderer handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />
                                                                          </ext:Column>
                                                                                                                                 


                                                                                    </Columns>
                                                                                </ColumnModel>
                                                                                  <Listeners>
                                                                                    <Render Handler="this.on('cellclick', cellClick1);" />
                                                                                </Listeners>
                                                                                <DirectEvents>
                                                                                    <CellClick OnEvent="leavePoPUP">
                                                                                        <EventMask ShowMask="true" />
                                                                                        <ExtraParams>
                                                                                            <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                                                                            <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                                                                        </ExtraParams>

                                                                                    </CellClick>
                                                                                </DirectEvents>


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
                                                        <ext:Panel runat="server" Layout="HBoxLayout" Title="<%$Resources:Tasks %>">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch"></ext:HBoxLayoutConfig>
                                                            </LayoutConfig>
                                                            <Items>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>

                                                                        <ext:Panel runat="server" ID="today" Flex="7">
                                                                            <Content>
                                                                                <div id="Chart2Container" style="padding: 10px; width: 100%; height: 100%;"></div>
                                                                            </Content>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Layout="FitLayout" Height="20">
                                                                            <Items>
                                                                                <ext:Panel runat="server" Layout="HBoxLayout">
                                                                                    <LayoutConfig>
                                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                                    </LayoutConfig>
                                                                                    <Items>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                        <ext:Panel runat="server" Flex="1">
                                                                                            <Items>
                                                                                                <ext:HyperlinkButton runat="server" Height="20" PaddingSpec="0 0 0 0" Flex="1" Text="<%$Resources:OverDue %>" StyleSpec="font-size:16pt;">
                                                                                                    <Listeners>
                                                                                                        <Click Handler="App.overDueWindow.show();" />
                                                                                                    </Listeners>
                                                                                                </ext:HyperlinkButton>
                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                    </Items>
                                                                                </ext:Panel>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Height="10" />
                                                                    </Items>
                                                                </ext:Panel>
                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>

                                                                        <ext:Panel runat="server" ID="overdue" Width="250" Height="250" Flex="7">
                                                                            <Content>
                                                                                <div id="Chart3Container" style="padding: 10px; width: 100%; height: 100%;"></div>
                                                                            </Content>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Layout="FitLayout" Height="20">
                                                                            <Items>
                                                                                <ext:Panel runat="server" Layout="HBoxLayout">
                                                                                    <LayoutConfig>
                                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                                    </LayoutConfig>
                                                                                    <Items>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                        <ext:Panel runat="server" Flex="1">
                                                                                            <Items>
                                                                                                <ext:HyperlinkButton runat="server" PaddingSpec="0 0 0 0" Flex="1" Text="<%$Resources:DueToday %>" Height="20" StyleSpec="font-size:16pt;">
                                                                                                    <Listeners>
                                                                                                        <Click Handler="App.DueTodayWindow.show();" />
                                                                                                    </Listeners>
                                                                                                </ext:HyperlinkButton>
                                                                                            </Items>
                                                                                        </ext:Panel>
                                                                                        <ext:Panel runat="server" Flex="1" />
                                                                                    </Items>
                                                                                </ext:Panel>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Height="10" />
                                                                    </Items>
                                                                </ext:Panel>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>

                                                </ext:TabPanel>
                                                <ext:Panel runat="server" Flex="1" StyleHtmlCls="withBackground" BodyCls="withBackground" />

                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server" Flex="1" BodyCls="withBackground" Cls="withBackground" StyleHtmlCls="withBackground" />
                                <ext:Panel runat="server" Flex="15" Layout="FitLayout" Region="West" PaddingSpec="20 0 0 0" BodyCls="withBackground" Cls="withBackground" AnchorHorizontal="true" AnchorVertical="true" StyleHtmlCls="withBackground" StyleSpec="border-radius: 25px;">

                                    <Listeners>
                                        <AfterRender Handler="startRefresh(); $('.flashing').fadeTo(1000, 0.1, function() { $(this).fadeTo(2000, 1.0); });" />
                                        <%--<AfterLayout Handler="$('.flashing').fadeTo(1000, 0.1, function() { $(this).fadeTo(2000, 1.0); });" />--%>
                                    </Listeners>
                                    <Items>
                                        <ext:Panel runat="server" Layout="VBoxLayout" BodyCls="withBackground" Cls="withBackground" StyleHtmlCls="withBackground">
                                            <LayoutConfig>
                                                <ext:VBoxLayoutConfig Align="Stretch" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:TabPanel ID="alerts" runat="server" Flex="30" Layout="FitLayout" Plain="true" StyleSpec="border-radius: 25px;">
                                                    <Items>
                                                        <ext:Panel runat="server" Layout="VBoxLayout" Title="<%$Resources:Alerts %>">
                                                            <LayoutConfig>
                                                                <ext:VBoxLayoutConfig Align="Stretch" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:Panel runat="server" Flex="1" />

                                                                <ext:Panel runat="server" Layout="HBoxLayout" Flex="10">
                                                                    <LayoutConfig>
                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                        <ext:Panel runat="server" Layout="VBoxLayout" Flex="10">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="companyRW" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="30 0 0 0" Height="100" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:ComapnyRightToWork %>" ShrinkWrap="Both" PaddingSpec="0 0 0 0" StyleSpec="font-size:10pt;" Height="80">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.CompanyRightToWorkWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                        <ext:Panel runat="server" Layout="VBoxLayout" Flex="10">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="employeeRW" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="30 0 0 0" Height="100" />
                                                                                <ext:HyperlinkButton runat="server" PaddingSpec="0 0 0 0" Text="<%$Resources:EmployeeRightToWork %>" StyleSpec="font-size:10pt;" Height="80">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.EmployeeRightToWorkWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                    </Items>
                                                                </ext:Panel>
                                                                <ext:Panel runat="server" Flex="1" />
                                                                <ext:Panel runat="server" Layout="HBoxLayout" Flex="10">
                                                                    <LayoutConfig>
                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                    </LayoutConfig>

                                                                    <Items>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                        <ext:Panel runat="server" Layout="VBoxLayout" Flex="10">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>

                                                                                <ext:Label runat="server" ID="salaryChange" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="30 0 0 0" Height="100" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:SalaryChange %>" StyleSpec="font-size:12pt;" Height="80">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.SCRWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                        <ext:Panel runat="server" Layout="VBoxLayout" Flex="10">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="probation" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="30 0 0 0" Height="100" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:Probation %>" StyleSpec="font-size:12pt;" Height="80" StyleHtmlCls="flashing">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.ProbationWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                    </Items>
                                                                </ext:Panel>
                                                                <ext:Panel runat="server" Flex="1" />
                                                                <ext:Panel runat="server" Layout="HBoxLayout" Flex="10">
                                                                    <LayoutConfig>
                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                    </LayoutConfig>

                                                                    <Items>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                        <ext:Panel runat="server" Layout="VBoxLayout" Flex="10">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>

                                                                                <ext:Label runat="server" ID="totalLoansLbl" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="30 0 0 0" Height="100" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:TotalLoans %>" StyleSpec="font-size:12pt;" Height="80">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.totalLoansWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                        <ext:Panel runat="server" Layout="VBoxLayout" Flex="10">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="deductedLoansLbl" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="30 0 0 0" Height="100" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:CompletedLoans %>" StyleSpec="font-size:12pt;" Height="80" StyleHtmlCls="flashing">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.completedLoansWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                    </Items>
                                                                </ext:Panel>
                                                                <ext:Panel runat="server" Layout="HBoxLayout" Flex="10">
                                                                    <LayoutConfig>
                                                                        <ext:HBoxLayoutConfig Align="Middle"></ext:HBoxLayoutConfig>
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                        <ext:Panel runat="server" Layout="VBoxLayout" Flex="10">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>


                                                                                <ext:Label runat="server" ID="annversaries" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="30 0 0 0" Height="100" />

                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:Anneversaries %>" Height="80" PaddingSpec="0 0 0 0" StyleSpec="font-size:12pt;">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.anniversaryWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                        <ext:Panel runat="server" Layout="VBoxLayout" Flex="10">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>

                                                                                <ext:Label runat="server" ID="birthdays" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="30 0 0 0" Height="100" />
                                                                                <ext:HyperlinkButton runat="server" PaddingSpec="0 0 0 0" Text="<%$Resources:Birthdays %>" StyleSpec="font-size:12pt;" Height="80">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.BirthdaysWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Panel>
                                                                        <ext:Panel runat="server" Flex="1" />
                                                                    </Items>
                                                                </ext:Panel>
                                                                <ext:Panel runat="server" Flex="1" />

                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:TabPanel>
                                                <ext:Panel runat="server" Flex="1" BodyCls="withBackground" PaddingSpec="0 0 0 0" />
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server" Flex="1" BodyCls="withBackground" PaddingSpec="0 0 0 0" />
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>



            </Items>
        </ext:Viewport>
        <ext:Window runat="server" Modal="true" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="BirthdaysWindow" Width="400" Height="250" Title="<%$ Resources: Birthdays %>">
            <Listeners>
                <AfterLayout Handler="App.BirthdaysStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel1"
                    runat="server" HideHeaders="true"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <Store>
                        <ext:Store
                            ID="BirthdaysStore"
                            runat="server" OnReadData="BirthdaysStore_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model6" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="birthDate" />
                                        <ext:ModelField Name="days" />


                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>

                    <ColumnModel ID="ColumnModel6" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayBirthday(record.data);  ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDaysLeft %>" DataIndex="days" Hideable="false" Width="120">
                                <Renderer Handler="return record.data['days'] + ' ' + document.getElementById('daysLeft').value;" />


                            </ext:Column>








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
        <ext:Window runat="server" Modal="true" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="anniversaryWindow" Width="400" Height="250" Title="<%$ Resources: Anneversaries %>">
            <Listeners>
                <AfterLayout Handler="App.AnniversaryStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel6"
                    runat="server" HideHeaders="true"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <Store>
                        <ext:Store
                            ID="AnniversaryStore"
                            runat="server" OnReadData="AnniversaryStore_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model7" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="hireDate" />
                                        <ext:ModelField Name="days" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>

                    <ColumnModel ID="ColumnModel7" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayAnniversary(record.data);  ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDaysLeft %>" DataIndex="days" Hideable="false" Width="120">
                                <Renderer Handler="return record.data['days'] + ' ' + document.getElementById('daysLeft').value;" />


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
        </ext:Window>
        <ext:Window runat="server" Modal="true"
            Hidden="true" AutoShow="false" ID="CompanyRightToWorkWindow" Layout="FitLayout" Width="400" Height="250" Title="<%$ Resources: ComapnyRightToWork %>">
            <Listeners>
                <AfterLayout Handler="App.CompanyRightToWorkStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel7"
                    runat="server" HideHeaders="true"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <Store>
                        <ext:Store
                            ID="CompanyRightToWorkStore"
                            runat="server" OnReadData="CompanyRightToWorkStore_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model12" runat="server">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="documentRef" />
                                        <ext:ModelField Name="expiryDate" />
                                        <ext:ModelField Name="days" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>

                    <ColumnModel ID="ColumnModel8" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column12" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayRTW(record.data);  ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDaysLeft %>" DataIndex="days" Hideable="false" Width="120">

                                <Renderer Handler="return record.data['days'] + ' ' + document.getElementById('daysLeft').value;" />

                            </ext:Column>








                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView ID="GridView8" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel7" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
        <ext:Window runat="server" Modal="true" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="EmployeeRightToWorkWindow" Width="600" Height="200" Title="<%$ Resources: EmployeeRightToWork %>">
            <Listeners>
                <AfterLayout Handler="App.EmployeeRightToWorkStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel8"
                    runat="server" 
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <Store>
                        <ext:Store
                            ID="EmployeeRightToWorkStore"
                            runat="server" OnReadData="EmployeeRightToWorkStore_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model13" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="documentRef" />
                                        <ext:ModelField Name="expiryDate" />
                                        <ext:ModelField Name="days" />
                                          <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="departmentName" />
                                         <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="dtName" />
                                       

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>

                    <ColumnModel ID="ColumnModel13" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                             <ext:Column Visible="false" ID="Column21" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />

                                                    
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                              <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocument %>" DataIndex="dtName" Hideable="false" />
                              <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentRef %>" DataIndex="documentRef" Hideable="false" />

                            <%--<ext:Column Visible="false" ID="Column13" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayEmployeeRTW(record.data);  ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDaysLeft %>" DataIndex="days" Hideable="false" Width="120">

                                <Renderer Handler="return record.data['days'] + ' ' + document.getElementById('daysLeft').value;" />

                            </ext:Column>--%>








                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView ID="GridView13" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel12" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
        <ext:Window runat="server" Modal="true" Title="<%$Resources:DueToday %>" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="DueTodayWindow" Width="400" Height="300">
            <Items>
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
        </ext:Window>
        <ext:Window runat="server" Modal="true" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="SCRWindow" Width="400" Height="200" Title="<%$ Resources: SalaryChange %>">
            <Listeners>
                <AfterLayout Handler="App.SCRStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel9"
                    runat="server" HideHeaders="true"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <Store>
                        <ext:Store
                            ID="SCRStore"
                            runat="server" OnReadData="SCRStore_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model14" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="effectiveDate" />
                                        <ext:ModelField Name="finalAmount" />
                                        <ext:ModelField Name="currencyRef" />
                                        <ext:ModelField Name="days" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>

                    <ColumnModel ID="ColumnModel14" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column15" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displaySCR(record.data);  ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDaysLeft %>" DataIndex="days" Hideable="false" Width="120">
                                <Renderer Handler="return record.data['days'] + ' ' + document.getElementById('daysLeft').value;" />


                            </ext:Column>








                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView ID="GridView14" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel13" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>

        <ext:Window runat="server" Modal="true" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="ProbationWindow" Width="400" Height="200" Title="<%$ Resources: Probation %>">
            <Listeners>
                <AfterLayout Handler="App.ProbationStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel10"
                    runat="server" HideHeaders="true"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <Store>
                        <ext:Store
                            ID="ProbationStore"
                            runat="server" OnReadData="ProbationStore_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model15" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="probationEndDate" />
                                        <ext:ModelField Name="days" />


                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>

                    <ColumnModel ID="ColumnModel15" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column17" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayProbation(record.data);  ">
                                </Renderer>
                            </ext:Column>

                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDaysLeft %>" DataIndex="days" Hideable="false" Width="120">
                                <Renderer Handler="return record.data['days'] + ' ' + document.getElementById('daysLeft').value;" />

                            </ext:Column>







                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView ID="GridView15" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel14" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>

        <ext:Window runat="server" Modal="true" Title="<%$Resources:OverDue %>" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="overDueWindow" Width="400" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel2" HideHeaders="false"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$Resources:OverDue %>"
                    Layout="FitLayout"
                    Scroll="Vertical" StoreID="OverDueStore"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">



                    <ColumnModel ID="ColumnModel9" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column Visible="false" ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTaskName %>" DataIndex="name" Hideable="false" Flex="1">
                            </ext:Column>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="assignToName" Hideable="false" Flex="1">
                            </ext:Column>
                            <ext:DateColumn ID="DateColumn4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDueDate %>" DataIndex="dueDate" Hideable="false" Width="100">
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
            </Items>
        </ext:Window>

        <ext:Window runat="server" Modal="true" Title="<%$ Resources: LatenessGridTitle %>" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="lateWindow" Width="650" Height="300">
            <Items>
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
                              <ext:Column Visible="false" ID="Column19" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTime%>" DataIndex="time" Width="55" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                          <%--  <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayLate(record.data);  ">
                                </Renderer>
                            </ext:Column>--%>
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
            </Items>
        </ext:Window>
        <ext:Window runat="server" Modal="true" Title="<%$ Resources: AbsenseGridTitle %>"
            Hidden="true" AutoShow="false" ID="absentWindow" Width="400" Height="300" Layout="FitLayout">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="absenseGrid"
                    runat="server" HideHeaders="true"
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
                            PageSize="30">

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
        </ext:Window>
        <ext:Window runat="server" Modal="true" Title="<%$ Resources: PaidLeaves %>"
            Hidden="true" AutoShow="false" ID="onLeavewindow" Width="400" Height="300" Layout="FitLayout">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="leaveGrid"
                    runat="server" HideHeaders="true"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: PaidLeaves %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" RenderXType="True">
                    <Store>
                        <ext:Store PageSize="30"
                            ID="leavesStore"
                            runat="server" OnReadData="leavesStore_ReadData"
                            RemoteSort="false"
                            RemoteFilter="false">

                            <Model>
                                <ext:Model ID="Model4" runat="server" IDProperty="employeeId">
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
                            <ext:Column Visible="false" ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="employeeId" Hideable="false" Width="75" />
                            <ext:Column Flex="3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="employeeName" Hideable="false" Width="75">
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
        </ext:Window>
        <ext:Window runat="server" Modal="true" Title="<%$ Resources: UnpaidLeaves %>"
            Hidden="true" AutoShow="false" ID="unpaidLeavesWindow" Width="400" Height="300" Layout="FitLayout">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel13"
                    runat="server" HideHeaders="true"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: UnpaidLeaves %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" RenderXType="True">
                    <Store>
                        <ext:Store PageSize="30"
                            ID="UnpaidLeavesStore"
                            runat="server" OnReadData="UnpaidLeavesStore_ReadData"
                            RemoteSort="false"
                            RemoteFilter="false">

                            <Model>
                                <ext:Model ID="Model18" runat="server" IDProperty="employeeId">
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


                    <ColumnModel ID="ColumnModel18" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column Visible="false" ID="Column18" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="employeeId" Hideable="false" Width="75" />
                            <ext:Column Flex="3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="employeeName" Hideable="false" Width="75">
                                <Renderer Handler=" return displayLeaves(record.data);" />
                            </ext:Column>





                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView ID="GridView18" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel17" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
        <ext:Window runat="server" Modal="true" Title="<%$ Resources: ActiveGridTitle %>"
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="activeWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="activeGrid" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Title="<%$ Resources: ActiveGridTitle %>"
                    Scroll="Vertical"
                    Border="false"
                    StoreID="activeStore"
                    ColumnLines="True">




                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                                                                              <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTime%>" DataIndex="time" Width="55" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                          <%--  <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayActive(record.data);  ">
                                </Renderer>
                            </ext:Column>--%>










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
            </Items>
        </ext:Window>
        <ext:Window runat="server" Modal="true" Title="<%$ Resources: TotalLoans %>"
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="totalLoansWindow" Width="400" Height="300">
            <Listeners>
                <AfterLayout Handler="App.totalLoansStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel11" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Title="<%$ Resources: TotalLoans %>"
                    Scroll="Vertical" HideHeaders="true"
                    Border="false"
                    ColumnLines="True">
                    <Store>
                        <ext:Store
                            ID="totalLoansStore"
                            runat="server" OnReadData="totalLoansStore_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model16" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="amount" />
                                        <ext:ModelField Name="date" />
                                        <ext:ModelField Name="currencyRef" />


                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>



                    <ColumnModel ID="ColumnModel16" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <%--                                                   <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTime%>" DataIndex="time" Width="55" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            --%><ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: Loan %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayTotalLoans(record.data);  ">
                                </Renderer>
                            </ext:Column>










                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView Border="false" ID="GridView16" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel15" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>

        <ext:Window runat="server" Modal="true" Title="<%$ Resources: CompletedLoans %>"
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="completedLoansWindow" Width="400" Height="300">
            <Listeners>
                <AfterLayout Handler="App.CompletedLoansStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel12" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Title="<%$ Resources: CompletedLoans %>"
                    Scroll="Vertical" HideHeaders="true"
                    Border="false"
                    ColumnLines="True">
                    <Store>
                        <ext:Store
                            ID="CompletedLoansStore"
                            runat="server" OnReadData="CompletedLoansStore_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model17" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="amount" />
                                        <ext:ModelField Name="date" />
                                        <ext:ModelField Name="currencyRef" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>



                    <ColumnModel ID="ColumnModel17" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <%--                                                   <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTime%>" DataIndex="time" Width="55" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />
                                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            --%><ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: Loan %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayCompletedLoans(record.data);  ">
                                </Renderer>
                            </ext:Column>










                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView Border="false" ID="GridView17" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel16" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
       
      <ext:Window
            ID="LeaveRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="LeaveRecordWindow"
            Width="450"
            Height="650"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Resizable="false"
            Maximizable="false"
            Draggable="false"
            Layout="Fit">

    <Items>
        <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
           

           
            <Items>
                <ext:FormPanel
                    ID="LeaveRecordTab" DefaultButton="SaveButton"
                    runat="server"
                    Title="LeaveRecordTab"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%" OnLoad="LeaveRecordTab_Load"
                    BodyPadding="5">
                    <Items>
                        <ext:TextField ID="TextField1" runat="server" Name="recordId" Hidden="true" />
                        <ext:TextField ID="leaveRef" runat="server" Name="leaveRef"  FieldLabel="<%$ Resources:FieldLeaveRef%>" />
                         <ext:TextField ID="employeeName" runat="server" Name="employeeName.fullName"  FieldLabel="employeeName" />


                        <%--   <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="employeeId" AllowBlank="false"
                            DisplayField="fullName" Name="employeeId"
                            ValueField="recordId"
                            TypeAhead="false"
                            FieldLabel="<%$ Resources: FieldEmployeeName%>"
                            HideTrigger="true" SubmitValue="true"
                            MinChars="3"
                            TriggerAction="Query" ForceSelection="false">
                            <Store>
                                <ext:Store runat="server" ID="employeeStore" AutoLoad="false">
                                    <Model>
                                        <ext:Model runat="server">
                                            <Fields>
                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="fullName" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.FillEmployee"></ext:PageProxy>
                                    </Proxy>

                                </ext:Store>

                            </Store>
                            <DirectEvents>
                                <Select OnEvent="MarkLeaveChanged">
                                    <ExtraParams>
                                        <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                    </ExtraParams>
                                </Select>
                            </DirectEvents>

                        </ext:ComboBox>--%>
                        <ext:DateField ID="startDate"   runat="server" FieldLabel="<%$ Resources:FieldStartDate%>" Name="startDate" AllowBlank="false"/>
                        <ext:DateField ID="endDate"   runat="server" FieldLabel="<%$ Resources:FieldendDate%>" Name="endDate" AllowBlank="false"/>
                                                 
                                            
                     
                        <ext:TextField runat="server" ID="leavePeriod" Name="leavePeriod" ReadOnly="true" FieldLabel="<%$ Resources:TotalText%>" />
                        <ext:TextArea ID="justification" runat="server" FieldLabel="<%$ Resources:FieldJustification%>" Name="justification" MaxHeight="5" />
                        <ext:TextField ID="destination" runat="server" FieldLabel="<%$ Resources:FieldDestination%>" Name="destination" AllowBlank="false" />


                        <ext:Checkbox runat="server" Name="isPaid" InputValue="true" ID="isPaid" DataIndex="isPaid" FieldLabel="<%$ Resources:FieldIsPaid%>" />


                        <ext:TextField ID="ltName" runat="server" FieldLabel="ltName" Name="ltName" AllowBlank="true" />

                     

                        <ext:ComboBox Disabled="true"   AnyMatch="true" CaseSensitive="false"  runat="server" ID="status" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" Name="status"
                            FieldLabel="<%$ Resources: FieldStatus %>">
                            <Items>

                                <ext:ListItem Text="<%$ Resources: FieldPending %>" Value="1" />
                                <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="2" />
                                <ext:ListItem Text="<%$ Resources: FieldUsed %>" Value="3" />
                                <ext:ListItem Text="<%$ Resources: FieldRefused %>" Value="-1" />
                            </Items>
                            <Listeners>
                                <Change Handler="SetReturnDateState();" />
                            </Listeners>
                        </ext:ComboBox>
                       
                                <ext:TextField runat="server" ID="leaveBalance" FieldLabel="<%$Resources: LeaveBalance %>" Name="leaveBalance" />
                                <ext:TextField runat="server" ID="yearsInService" FieldLabel="<%$Resources: YearsInService %>" Name="yearsInService" />
                                 <ext:DateField Disabled="true" runat="server" Name="returnDate" ID="returnDate" FieldLabel="<%$ Resources: FieldReturnDate %>">
                                  
                                   
                                </ext:DateField>
                                <ext:TextArea runat="server" FieldLabel="<%$Resources: ReturnNotes %>" ID="returnNotes" Name="returnNotes" MaxHeight="5" />
                               
                          
                    </Items>

                </ext:FormPanel>
               

            </Items>
        </ext:TabPanel>
    </Items>
   
     <Buttons>
               
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
