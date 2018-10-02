<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AionHR.Web.UI.Forms.Dashboard" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Dashboard.css?id=31" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Dashboard.js?id=741"></script>
    <!--  <script type="text/javascript" src="Scripts/app.js"></script>-->
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js?id=10"></script>
    <script type="text/javascript" src="Scripts/CircileProgress.js?id=91"></script>
    <script type="text/javascript" src="Scripts/jquery-new.js?id=101"></script>
    <script type="text/javascript" src="Scripts/plugins/highcharts.js?id=100"></script>


    <style type="text/css">
        .styleContainer {
            padding: 5px;
            border: 1px solid #eee;
        }

            .styleContainer:hover {
                background-color: #eee;
            }

        .lblStyle {
            font-size: 10pt;
            text-decoration: none !important;
            font-weight: bold;
            /*color: #159fcc !important;*/
            color: rgb(124, 181, 236) !important;
        }
    </style>
    <script type="text/javascript">
        var getTimeStatus = function (statusId)
        {
             
                        switch (statusId) {
                            case 1: return App.new.value; 
      
                    break;
                            case 2: return App.approved.value;
       
                                break;
                            case -1: return App.rejected.value;
       
                    break;
                default:
      
            } 
        }
        var drawActiveHightChartPie = function (dataObject, rtl, normal) {
            // Build the chart
            var divName = 'activeHighChart';
            if (!normal)
                divName = 'maximumChart';
            Highcharts.chart(divName, {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                legend: {
                    itemStyle: { fontSize: '11px' },
                    itemDistance: 5,
                    useHTML: rtl,
                    rtl: rtl



                },
                tooltip: {
                    enabled: false
                },
                subtitle: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                plotOptions: {
                    pie: {

                        showInLegend: true,
                        point: {
                            events: {

                                legendItemClick: function (e) {

                                    if (e.target.options.y > 0)
                                        clickActiveHightChartPieSeries(e.target.options.index);
                                    return false;
                                }
                            }
                        }
                    },
                    series: {
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                return this.point.y;
                            }

                        }
                    }
                },
                series: [{
                    data: dataObject
                }]
            });

        };
        var clickActiveHightChartPieSeries = function (val) {

            switch (val) {
                case 0: App.PendingWindow.show(); App.PendingStore.reload(); break;
                case 1: App.NoShowUpWindow.show(); App.NoShowUpStore.reload(); break;
                case 2: App.CheckedWindow.show(); App.CheckedStore.reload(); break;
                case 3: App.LeaveWithoutExcuseWindow.show(); App.LeaveWithoutExcuseStore.reload(); break;
                case 4: App.LeaveWindow.show(); App.LeaveStore.reload(); break;
                case 5: App.DayOffWindow.show(); App.DayOffStore.reload(); break;
            }

        };

        
     

        var drawLateHightChartPie = function (dataObject, rtl, normal) {
            // Build the chart
            var divName = 'lateHighChart';
            if (!normal)
                divName = 'maximumChart';
            Highcharts.chart(divName, {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                legend: {
                    itemStyle: { fontSize: '11px' },
                    itemDistance: 5,
                    useHTML: rtl,
                    rtl: rtl



                },
                tooltip: {
                    enabled: false
                },
                subtitle: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                plotOptions: {
                    pie: {

                        showInLegend: true,
                        point: {
                            events: {

                                legendItemClick: function (e) {

                                    if (e.target.options.y > 0)
                                        clickLateHightChartPieSeries(e.target.options.index);
                                    return false;
                                }
                            }
                        }
                    },
                    series: {
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                return this.point.y;
                            }

                        }
                    }
                },
                series: [{
                    data: dataObject
                }]
            });

        };
        var clickLateHightChartPieSeries = function (val) {
        
            App.CurrentTimeVariationType.setValue(val);
            App.TimeVariationStore.reload();
            App.TimeVariationWindow.show();
        }

        var drawBreakHightChartPie = function (dataObject, rtl, normal) {
            // Build the chart
            var divName = 'breakHighChart';
            if (!normal)
                divName = 'maximumChart';
            Highcharts.chart(divName, {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                legend: {
                    itemStyle: { fontSize: '11px' },
                    itemDistance: 5,
                    useHTML: rtl,
                    rtl: rtl



                },
                tooltip: {
                    enabled: false
                },
                subtitle: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                plotOptions: {
                    pie: {

                        showInLegend: true,
                        point: {
                            events: {
                                legendItemClick: function (e) {
                                    clickBreakHightChartPieSeries(e.target.options.index);
                                    return false;
                                }
                            }
                        }
                    },
                    series: {
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                return this.point.y;
                            },
                            distance: -30,

                        }
                    }
                },
                series: [{
                    data: dataObject
                }]
            })
        };
        var clickBreakHightChartPieSeries = function (val) {
            //alert(val);

            if (val == 0) {
                App.OutStore.reload();
                App.OutWindow.show();
            }
            else {
                App.InStore.reload();
                App.InWindow.show();

            }
        }


        // Second Tab

        var Load = function (id) {
            if (id == "pnlDepartmentCount") {
                Ext.net.Mask.show({ el: App.att.id });
                App.Store1.reload({
                    callback: function () {
                        Ext.net.Mask.hide();
                    }
                });
            }

            if (id == "pnlRate") {
                Ext.net.Mask.show({ el: App.pnlRate.id });
                App.LocalRateStore.reload({
                    callback: function () {
                        Ext.net.Mask.hide();
                    }
                });
            }
            if (id == "pnlAttendancePeriod") {


                App.periodToDate.selectedDate = (new Date());

                Ext.net.Mask.show({ el: App.pnlAttendancePeriod.id });
                App.AttendancePeriodStore.reload({
                    callback: function () {
                        Ext.net.Mask.hide();
                    }
                });
            }
            if (id == "pnlHeadCount") {


                App.CountDateTo.selectedDate = (new Date());

                Ext.net.Mask.show({ el: App.pnlHeadCount.id });
                App.DimensionalHeadCountStore.reload({
                    callback: function () {
                        Ext.net.Mask.hide();
                    }
                });
                App.CompanyHeadCountStore.reload({
                    callback: function () {
                        Ext.net.Mask.hide();
                    }
                });
            }
        }




        var drawDepartmentsCountHightChartColumn = function (IN, OUT, objectIn, objectOut, dataCategoriesObject, rtl, normal) {
            // Build the chart  deparmentsCountHighChart
            var divName = 'deparmentsCountHighChart';
            if (!normal)
                divName = 'maximumChart';
            Highcharts.chart(divName, {
                chart: {
                    type: 'column'
                },
                title: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: dataCategoriesObject,
                    reversed: rtl,
                    title: {
                        text: null
                    },
                    labels: {
                        useHTML: Highcharts.hasBidiBug,
                        useHTML: rtl,
                        rotation: -45,
                        style: {
                            fontSize: '8px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    opposite: rtl,
                    title: { text: null }

                },
                tooltip: {
                    useHTML: true

                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                legend: {

                    useHTML: rtl,
                    rtl: rtl
                },
                credits: {
                    enabled: false
                },
                series: [{
                    name: IN,
                    data: objectIn
                }, {
                    name: OUT,
                    data: objectOut
                }]
            });


        };
        var drawCompanyHeadCountChart = function (objectIn, dataCategoriesObject, rtl, normal) {
            // Build the chart  deparmentsCountHighChart
            var divName = 'CompanyHeadCountChart';
            if (!normal)
                divName = 'maximumChart';
            Highcharts.chart(divName, {
                chart: {
                    type: 'column',
                    zoomType: 'x'
                },
                legend: {
                    enabled: false
                },
                title: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: dataCategoriesObject,
                    reversed: rtl,
                    title: {
                        text: null
                    },
                    labels: {
                        useHTML: Highcharts.hasBidiBug,
                        useHTML: rtl,
                        rotation: -45,
                        style: {
                            fontSize: '8px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    opposite: rtl,
                    title: { text: null }

                },
                tooltip: {
                    useHTML: true

                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },

                credits: {
                    enabled: false
                },
                series: [{

                    data: objectIn
                }]
            });


        };

        var drawDimensionalHeadCountChart = function (objectIn, dataCategoriesObject, rtl, normal) {
            // Build the chart  deparmentsCountHighChart
            var divName = 'DimensionalHeadCountChart';
            if (!normal)
                divName = 'maximumChart';
            Highcharts.chart(divName, {
                chart: {
                    type: 'column',
                    zoomType: 'x'
                },
                title: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                subtitle: {
                    text: ''
                },
                legend: {
                    enabled: false
                },
                xAxis: {
                    categories: dataCategoriesObject,
                    reversed: rtl,
                    title: {
                        text: null
                    },
                    labels: {
                        useHTML: Highcharts.hasBidiBug,
                        useHTML: rtl,
                        rotation: -45,
                        style: {
                            fontSize: '8px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    opposite: rtl,
                    title: { text: null }

                },
                tooltip: {
                    useHTML: true

                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },

                credits: {
                    enabled: false
                },
                series: [{

                    data: objectIn
                }]
            });


        };

        var drawMinLocalRateCountHightChartColumn = function (objectValues, dataCategoriesObject, rtl, normal) {
            // Build the chart  deparmentsCountHighChart

            var divName = 'localRateCountHighChart';
            // if (!normal)
            //  divName = 'maximumChart';

            Highcharts.chart(divName, {
                chart: {
                    type: 'column'
                },
                title: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: dataCategoriesObject,
                    reversed: rtl,
                    title: {
                        text: null
                    },
                    labels: {
                        useHTML: Highcharts.hasBidiBug,
                        useHTML: rtl

                    }
                },
                yAxis: {
                    min: 0,
                    opposite: rtl,

                    title: { text: null }


                },
                tooltip: {
                    useHTML: true,
                    formatter: function () {
                        return this.x + '</b> : <b>' + this.y;
                    }
                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                legend: {
                    enabled: false,
                    useHTML: rtl,
                    rtl: rtl

                },
                credits: {
                    enabled: false
                },
                series: [{

                    data: objectValues
                }]
            });


        };


        var drawLocalCountHightChartColumn = function (objectValues, dataCategoriesObject, rtl, normal) {
            // Build the chart  deparmentsCountHighChart

            var divName = 'localCountHighChart';
            // if (!normal)
            //    divName = 'maximumChart';

            Highcharts.chart(divName, {
                chart: {
                    type: 'column'
                },
                title: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: dataCategoriesObject,
                    reversed: rtl,
                    title: {
                        text: null
                    },
                    labels: {
                        useHTML: Highcharts.hasBidiBug,
                        useHTML: rtl

                    }
                },
                yAxis: {
                    min: 0,
                    opposite: rtl,
                    title: { text: null }

                },
                tooltip: {
                    useHTML: true,
                    formatter: function () {
                        return this.x + '</b> : <b>' + this.y;
                    }
                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                legend: {
                    enabled: false,
                    useHTML: rtl,
                    rtl: rtl
                },
                credits: {
                    enabled: false
                },
                series: [{

                    data: objectValues
                }]
            });


        };

        var drawAttendancePeriodChart = function (dataCategoriesObject, IN1, IN2, IN3, IN4, objectIn1, objectIn2, objectIn3, objectIn4, rtl, normal) {
            // Build the chart  deparmentsCountHighChart
            var divName = 'AttendancePeriodChart';

            if (!normal)
                divName = 'maximumChart';
            Highcharts.chart(divName, {
                chart: { zoomType: 'x' }
                ,
                title: {
                    text: '',
                    style: {
                        display: 'none'
                    }
                },
                subtitle: {
                    text: ''
                },
                xAxis: {

                    categories: dataCategoriesObject,
                    reversed: rtl,
                    title: {
                        text: null
                    },
                    labels: {
                        useHTML: Highcharts.hasBidiBug,
                        useHTML: rtl,
                        rotation: -45,
                        style: {
                            fontSize: '8px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    opposite: rtl,
                    title: { text: null }

                },
                tooltip: {
                    useHTML: true

                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                legend: {

                    useHTML: rtl,
                    rtl: rtl
                },
                credits: {
                    enabled: false
                },
                series: [{
                    name: IN1,
                    data: objectIn1
                },
                {
                    name: IN2,
                    data: objectIn2
                },
                {
                    name: IN3,
                    data: objectIn3
                },
                {
                    name: IN4,
                    data: objectIn4
                }
                ]
            });


        };

        //



        var tipRendererActive = function (toolTip, record, context) {
            var total = 0;

            App.activeChart.getStore().each(function (rec) {
                total += rec.get('Count');
            });

            toolTip.setTitle(record.get('Name') + ': ' + Math.round(record.get('Count') / total * 100) + '%');
        };
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
            setInterval(RefreshAllGrids, 15 * 60 * 1000);
        }
        function RefreshAllGrids() {
            CheckSession();

            if (window.
                parent.App.tabPanel.getActiveTab().id == "dashboard" || (window.parent.App.tabPanel.getActiveTab().id == "tabHome" && (window.parent.App.activeModule.value == 4 || window.parent.App.activeModule.value == 5 || window.parent.App.activeModule.value == 1 || window.parent.App.activeModule.value == 7))) {
                ////Not Chained
               
                //App.activeStore.reload();
                App.LocalRateStore.reload();
                //// App.Store1.reload();

                //App.LeaveRequestsStore.reload();
                //App.LoansStore.reload();
                //App.OverDueStore.reload();
                // App.LocalRateStore.reload();
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
<body style="background-color: #fff">
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


          <ext:Hidden ID="Atext" runat="server" Text="<%$ Resources: atext%>" />
        <ext:Hidden ID="Dtext" runat="server" Text="<%$ Resources: dtext%>" />
        <ext:Hidden ID="Ltext" runat="server" Text="<%$ Resources: ltext%>" />
        <ext:Hidden ID="Otext" runat="server" Text="<%$ Resources: otext%>" />
        <ext:Hidden ID="Mtext" runat="server" Text="<%$ Resources: mtext%>" />

        <ext:Hidden ID="daysLeft" runat="server" Text="<%$ Resources: FieldDaysLeft %>" />
        <ext:Hidden ID="userSessionEmployeeId" runat="server" Text="" />


         <ext:Hidden ID="approved" runat="server" Text="<%$ Resources: FieldApproved%>" />
        <ext:Hidden ID="rejected" runat="server" Text="<%$ Resources: FieldRejected%>" />
         <ext:Hidden ID="new" runat="server" Text="<%$ Resources: FieldNew%>" />



         <ext:Hidden ID="StatusNew" runat="server" Text="<%$ Resources:FieldNew %>" />
        <ext:Hidden ID="StatusInProcess" runat="server" Text="<%$ Resources: FieldInProcess %>" />
        <ext:Hidden ID="StatusApproved" runat="server" Text="<%$ Resources: FieldApproved %>" />
        <ext:Hidden ID="StatusRejected" runat="server" Text="<%$ Resources: FieldRejected %>" />
            <ext:Hidden ID="CurrentTimeVariationType" runat="server"  />

        <ext:Store PageSize="30"
            ID="OverDueStore"
            runat="server" OnReadData="OverDueStore_ReadData"
            RemoteSort="false"
            RemoteFilter="false">
          <%--  <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>--%>
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
        <ext:Store
            ID="InStore"
            runat="server" OnReadData="InStore_ReadData"
            RemoteSort="false" PageSize="200"
            RemoteFilter="false">

            <Model>
                <ext:Model ID="Model20" runat="server" >
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
        <ext:Store
            ID="OutStore"
            runat="server" OnReadData="OutStore_ReadData"
            RemoteSort="false" PageSize="200"
            RemoteFilter="false">

            <Model>
                <ext:Model ID="Model21" runat="server" IDProperty="recordId">
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

        <ext:Store ID="Store1"
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
        <ext:Store ID="AttendancePeriodStore" OnReadData="AttendancePeriodStore_ReadData"
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
        <ext:Store ID="CompanyHeadCountStore" OnReadData="CompanyHeadCountStore_ReadData"
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
        <ext:Store ID="DimensionalHeadCountStore" OnReadData="DimensionalHeadCountStore_ReadData"
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

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">

            <Items>

                <ext:Panel runat="server" Layout="FitLayout" ID="root" PaddingSpec="5 5 5 5">
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
                                     <DirectEvents>
                                     <Click OnEvent="RefreshAllGrid" ></Click>
                                   </DirectEvents>
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

                                <%--      <ext:Panel ID="spacePanel" UI="Danger" runat="server" Flex="1" ></ext:Panel>--%>
                                <ext:Panel runat="server" ID="right" BodyCls="withBackground" Cls="withBackground" StyleHtmlCls="withBackground" Layout="FitLayout" Flex="6" MarginSpec="0 5 0 5">
                                    <Items>
                                        <ext:Panel ID="rightPanel" BodyCls="withBackground" Cls="withBackground" StyleHtmlCls="withBackground" runat="server" AutoScroll="true" Layout="VBoxLayout" Flex="1" StyleSpec="padding-top:20px;">
                                            <%--  <Listeners>
                                                <AfterLayout Handler="  App.active.setWidth(App.att.getWidth()/7);App.active.setHeight(App.att.getWidth()/7); App.PolarChart6.setWidth(App.att.getWidth()/4); App.PolarChart6.setHeight(App.att.getWidth()/4); App.absense.setWidth(App.att.getWidth()/7);App.late.setWidth(App.att.getWidth()/7);App.overdue.setWidth(App.belowt.getWidth()/4);App.today.setWidth(App.belowt.getWidth()/4);"></AfterLayout>
                                            </Listeners>--%>
                                            <LayoutConfig>
                                                <ext:VBoxLayoutConfig Pack="End" Align="Stretch"></ext:VBoxLayoutConfig>
                                            </LayoutConfig>
                                            <Items>

                                                <ext:TabPanel ID="att" Plain="true" Flex="1" Layout="FitLayout" StyleHtmlCls="withBackground" BodyCls="withBackground topper" runat="server" PaddingSpec="0 0 0 0" StyleSpec="border-radius: 0px; " MinHeight="250" MarginSpec="0 0 5 0" DeferredRender="false">
                                                    <Items>
                                                        <ext:Panel runat="server" Title="<%$Resources:Today %>" StyleSpec=" border: 1px solid #add2ed !important;" Layout="HBoxLayout" Flex="1">
                                                            <Defaults>
                                                                <ext:Parameter Name="margin" Value="0 5 0 0" Mode="Value" />
                                                            </Defaults>
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch" />
                                                            </LayoutConfig>

                                                            <Items>

                                                                <ext:Panel
                                                                    runat="server"
                                                                    Header="false" ID="pnlActiveHighChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                                                                    Layout="FitLayout" Flex="1" Html="<div id='activeHighChart' style='width:100%;height:100%' />">
                                                                </ext:Panel>

                                                                <ext:Panel
                                                                    runat="server"
                                                                    Header="false" ID="pnlLateHighChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                                                                    Layout="FitLayout" Flex="1" Html="<div id='lateHighChart' style='width:100%;height:100%' />" >
                                                                </ext:Panel>

                                                                <ext:Panel
                                                                    runat="server"
                                                                    Header="false" ID="pnlBreakHighChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                                                                    Layout="FitLayout" Flex="1" Html="<div id='breakHighChart' style='width:100%;height:100%' />">
                                                                </ext:Panel>







                                                                <%--   <%--           <ext:Panel runat="server" Layout="BorderLayout" Flex="1">
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
                                                                </ext:Panel>--%>

                                                                <%--                                                                <ext:Panel runat="server" Layout="BorderLayout" ID="leavesHighChart" Flex="1">
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

                                                                <ext:Panel runat="server" Layout="BorderLayout" Flex="1" Hidden="true">
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
                                                                    </Items>
                                                                </ext:Panel>--%>


                                                                <%--                                                                <ext:Panel runat="server" Layout="BorderLayout" Flex="1" Hidden="true">
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
                                                                                <ext:ItemHighlightInteraction />
                                                                            </Interactions>
                                                                            <Series>
                                                                                <ext:PieSeries
                                                                                    AngleField="Count"
                                                                                    ShowInLegend="true"
                                                                                    Donut="50"
                                                                                    HighlightMargin="0">

                                                                                    <%--                <Label Field="Name" Display="Rotate" FontSize="18" FontFamily="Arial" />
                                                            <Label Field="Name" Display="Over" FontSize="18" FontFamily="Arial" />
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
                                                                </ext:Panel>--%>
                                                            </Items>

                                                        </ext:Panel>



                                                        <ext:Panel ID="pnlDepartmentCount" AutoUpdateLayout="true" Hidden="true" runat="server" Title="<%$Resources: DepartmentsCount %>" StyleSpec=" border: 1px solid #add2ed !important;" Layout="HBoxLayout" Flex="1">
                                                            <Defaults>
                                                                <ext:Parameter Name="margin" Value="0 5 0 0" Mode="Value" />
                                                            </Defaults>
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch" />
                                                            </LayoutConfig>
                                                            <TopBar>
                                                                <ext:Toolbar runat="server">
                                                                    <Items>
                                                                        <ext:Button runat="server" Icon="ZoomIn">
                                                                            <Listeners>
                                                                                <Click Handler="App.MaximumChartWindow.show();" />
                                                                            </Listeners>
                                                                            <DirectEvents>

                                                                                <Click OnEvent="zoomDepartmentCount" />
                                                                            </DirectEvents>

                                                                        </ext:Button>


                                                                    </Items>
                                                                </ext:Toolbar>
                                                            </TopBar>
                                                            <Items>
                                                                <ext:Panel AutoUpdateLayout="true"
                                                                    runat="server"
                                                                    Header="false" ID="pnlDeparmentsCountHighChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                                                                    Layout="FitLayout" Flex="1" Html="<div id='deparmentsCountHighChart' style='width:100%;height:100%' />">
                                                                </ext:Panel>
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel Header="true" Resizable="true" ID="pnlAttendancePeriod" AutoUpdateLayout="true" runat="server" Title="<%$Resources: AttendancePeriod %>" StyleSpec=" border: 1px solid #add2ed !important;" Layout="HBoxLayout" Flex="1">
                                                            <TopBar>
                                                                <ext:Toolbar runat="server">
                                                                    <Items>
                                                                        <ext:DateField runat="server" ID="periodToDate" Width="150" LabelWidth="30" FieldLabel="<%$Resources:FieldFrom %>">
                                                                            <Listeners>
                                                                                <Change Handler="App.AttendancePeriodStore.reload()" />
                                                                            </Listeners>
                                                                        </ext:DateField>
                                                                        <ext:Button runat="server" Icon="ZoomIn">
                                                                            <Listeners>
                                                                                <Click Handler="App.MaximumChartWindow.show();" />
                                                                            </Listeners>
                                                                            <DirectEvents>

                                                                                <Click OnEvent="zoomAttendancePeriod" />
                                                                            </DirectEvents>

                                                                        </ext:Button>
                                                                    </Items>
                                                                </ext:Toolbar>
                                                            </TopBar>
                                                            <Defaults>
                                                                <ext:Parameter Name="margin" Value="0 5 0 0" Mode="Value" />
                                                            </Defaults>
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:Panel AutoUpdateLayout="true"
                                                                    runat="server"
                                                                    Header="true" ID="pnlAttendancePeriodChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                                                                    Layout="FitLayout" Flex="1" Html="<div id='AttendancePeriodChart' style='width:100%;height:100%' />">
                                                                 
                                                                </ext:Panel>
                                                            </Items>
                                                        </ext:Panel>

                                                        <ext:Panel ID="pnlHeadCount" AutoUpdateLayout="true" runat="server" Title="<%$Resources: HeadCount %>" StyleSpec=" border: 1px solid #add2ed !important;" Layout="HBoxLayout" Flex="1">
                                                            <TopBar>
                                                                <ext:Toolbar runat="server">
                                                                    <Items>
                                                                        <ext:DateField runat="server" ID="CountDateTo" Width="150" LabelWidth="30" FieldLabel="<%$Resources:FieldTo %>">
                                                                            <Listeners>
                                                                                <Change Handler="App.CompanyHeadCountStore.reload(); App.DimensionalHeadCountStore.reload();" />
                                                                            </Listeners>

                                                                        </ext:DateField>
                                                                        <ext:Button runat="server" Icon="ZoomIn">
                                                                            <Listeners>
                                                                                <Click Handler="App.MaximumChartWindow.show();" />
                                                                            </Listeners>
                                                                            <DirectEvents>

                                                                                <Click OnEvent="zoomCompanyHeadCount" />
                                                                            </DirectEvents>
                                                                        </ext:Button>
                                                                        <ext:ToolbarFill runat="server" />
                                                                        <ext:ComboBox Name="dimension" runat="server" FieldLabel="<%$Resources: Group %>" ID="dimension" Width="150" LabelWidth="40">
                                                                            <Items>
                                                                                <ext:ListItem Text="<%$Resources: FieldDepartment %>" Value="<%$Resources:ComboBoxValues,  RT110DimensionDepartment %>"  />
                                                                                <ext:ListItem Text="<%$Resources: FieldBranch %>" Value="<%$Resources:ComboBoxValues,  RT110DimensionBranch %>" />
                                                                                <ext:ListItem Text="<%$Resources: FieldDivision  %>" Value="<%$Resources:ComboBoxValues,  RT110DimensionDivision %>" />
                                                                                <ext:ListItem Text="<%$Resources: FieldPosition %>" Value="<%$Resources:ComboBoxValues,  RT110DimensionPosition %>" />
                                                                                <ext:ListItem Text="<%$Resources: FieldEHStatus %>" Value="<%$Resources:ComboBoxValues,  RT110DimensionEHStatus %>" />
                                                                            </Items>
                                                                            <Listeners>
                                                                                <Select Handler="App.DimensionalHeadCountStore.reload();" />
                                                                            </Listeners>


                                                                        </ext:ComboBox>
                                                                        <ext:Button runat="server" Icon="ZoomIn">
                                                                            <Listeners>
                                                                                <Click Handler="App.MaximumChartWindow.show();" />
                                                                            </Listeners>
                                                                            <DirectEvents>

                                                                                <Click OnEvent="zoomDimensionalHeadCount" />
                                                                            </DirectEvents>

                                                                        </ext:Button>
                                                                    </Items>
                                                                </ext:Toolbar>
                                                            </TopBar>
                                                            <Defaults>
                                                                <ext:Parameter Name="margin" Value="0 5 0 0" Mode="Value" />
                                                            </Defaults>
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:Panel AutoUpdateLayout="true"
                                                                    runat="server"
                                                                    Header="false" ID="pnlCompanyHeadCountChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                                                                    Layout="FitLayout" Flex="1" Html="<div id='CompanyHeadCountChart' style='width:100%;height:100%' />" >
                                                                </ext:Panel>
                                                                <ext:Panel AutoUpdateLayout="true"
                                                                    runat="server"
                                                                    Header="false" ID="pnlDimensionalHeadCountChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                                                                    Layout="FitLayout" Flex="1" Html="<div id='DimensionalHeadCountChart' style='width:100%;height:100%' />">
                                                                </ext:Panel>
                                                            </Items>
                                                        </ext:Panel>
                                                          <ext:Panel runat="server" AutoUpdateLayout="true" AutoScroll="true" ID="pnlRate" Title="<%$Resources: LocalRate %>" StyleSpec=" border: 1px solid #add2ed !important;" Layout="HBoxLayout" Flex="1">
                                                            <Defaults>
                                                                <ext:Parameter Name="margin" Value="0 5 0 0" Mode="Value" />
                                                            </Defaults>
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch" />
                                                            </LayoutConfig>
                                                              <TopBar>
                                                                        <ext:Toolbar runat="server">
                                                                            <Items>
                                                                             <ext:TextField ID="inName" runat="server" FieldLabel="<%$ Resources:industryName %>" ReadOnly="true"  LabelWidth="70" />
                                                                                     <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                                                                <ext:TextField ID="bsName" runat="server" FieldLabel="<%$ Resources:FieldbusinessSizeName%>" ReadOnly="true"   />
                                                                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                                                                  <ext:TextField ID="leName" runat="server" FieldLabel="<%$ Resources:FieldlevelName %>" ReadOnly="true"  LabelWidth="70"   />
                                                                            </Items>
                                                                        </ext:Toolbar>
                                                                    </TopBar>
                                                            <Items>

                                                                <ext:Panel AutoUpdateLayout="true"
                                                                    runat="server"
                                                                    Header="false" ID="pnllocalRateCountHighChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                                                                    Layout="FitLayout" Flex="1" Html="<div id='localRateCountHighChart' style='width:100%;height:90%' />">
                                                                    <TopBar>
                                                                        
                                                                    </TopBar>
                                                                </ext:Panel>
                                                                <ext:Panel AutoUpdateLayout="true"
                                                                    runat="server"
                                                                    Header="false" ID="pnllocalCountHighChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                                                                    Layout="FitLayout" Flex="1" Html="<div id='localCountHighChart' style='width:100%;height:90%' />">
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
                                                    <Listeners>

                                                        <TabChange Handler="Load(#{att}.activeTab.id);" />
                                                    </Listeners>
                                                </ext:TabPanel>

                                                <ext:TabPanel ID="belowt" IDMode="Client" EnableTheming="false" BodyCls="topper" Plain="true" runat="server" Flex="15" PaddingSpec="0 0 0 0" StyleSpec="border-radius: 0px;">
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
                                                            ColumnLines="True" IDMode="Explicit" RenderXType="True" StyleSpec=" border: 1px solid #add2ed !important;">
                                                            <Store>
                                                                <ext:Store PageSize="30"
                                                                    ID="LeaveRequestsStore"
                                                                    runat="server" OnReadData="LeaveRequestsStore_ReadData"
                                                                    RemoteSort="false"
                                                                    RemoteFilter="false">
                                                                   <%-- <Proxy>
                                                                        <ext:PageProxy>
                                                                            <Listeners>
                                                                                <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                                                            </Listeners>
                                                                        </ext:PageProxy>
                                                                    </Proxy>--%>
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
                                                                        ID="colEdit" Visible="true"
                                                                        Text=""
                                                                        Width="100"
                                                                        Hideable="false"
                                                                        Align="Center"
                                                                        Fixed="true"
                                                                        Filterable="false"
                                                                        MenuDisabled="true"
                                                                        Resizable="false">

                                                                        <Renderer Handler="return editRender()+'&nbsp;&nbsp;' + appendRender(); " />
                                                                    </ext:Column>



                                                                </Columns>
                                                            </ColumnModel>
                                                            <Listeners>
                                                                <Render Handler="this.on('cellclick', cellClick);" />
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
                                                            Border="false" Visible="false"
                                                            ColumnLines="True" IDMode="Explicit" RenderXType="True" StyleSpec=" border: 1px solid #add2ed !important;">
                                                             
                                                            <Store>
                                                                    
                   
          
                                                                <ext:Store

                                                                    ID="LoansStore"
                                                                    runat="server"  RemoteFilter="false" RemoteSort="false"
                                                                    PageSize="30"  >
                                                                   
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
                                                      
                                                        <ext:Panel runat="server" Hidden="true" Layout="HBoxLayout" Title="<%$Resources:Tasks %>" StyleSpec=" border: 1px solid #add2ed !important;">
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

                                                        <ext:GridPanel MarginSpec="0 0 0 0"
                                                            ID="TimeGridPanel"
                                                            runat="server"
                                                            PaddingSpec="0 0 1 0"
                                                            Header="false"
                                                            Title="<%$ Resources:Time %>"
                                                            Layout="FitLayout"
                                                            Scroll="Vertical"
                                                            Border="false"
                                                            ColumnLines="True" IDMode="Explicit" RenderXType="True" StyleSpec=" border: 1px solid #add2ed !important;">
                                                            <Store>
                                                                <ext:Store PageSize="30"
                                                                    ID="TimeStore"
                                                                    runat="server" OnReadData="TimeStore_ReadData"
                                                                    RemoteSort="false"
                                                                    RemoteFilter="false">
                                                                    <%--<Proxy>
                                                                        <ext:PageProxy>
                                                                            <Listeners>
                                                                                <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                                                            </Listeners>
                                                                        </ext:PageProxy>
                                                                    </Proxy>--%>
                                                                    <Model>
                                                                        <ext:Model ID="Model24" runat="server" >
                                                                            <Fields>
                                                                                                                                                            
                                                                                <ext:ModelField Name="employeeId" />
                                                                                <ext:ModelField Name="employeeName" IsComplex="true" />
                                                                                <ext:ModelField Name="dayId" />
                                                                                <ext:ModelField Name="dayIdDate"  />
                                                                           
                                                                                <ext:ModelField Name="timeCode" />
                                                                                <ext:ModelField Name="timeCodeString" />
                                                                                <ext:ModelField Name="approverId" />
                                                                                <ext:ModelField Name="status" />
                                                                                <ext:ModelField Name="notes" />
                                                                            

                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>

                                                                </ext:Store>
                                                            </Store>


                                                            <ColumnModel ID="ColumnModel24" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                                <Columns>
                                                                   <ext:Column ID="ColTimedayId" DataIndex="dayId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="ColTimeEmployeeId" DataIndex="employeeId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="ColtimeCode" DataIndex="timeCode"  runat="server" Visible="false" />


                                                                    <ext:Column ID="Column27" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                                                                    <Renderer Handler=" return record.data['employeeName'].fullName;" />
                                                                    </ext:Column>
                                                                
                                                                    <ext:DateColumn ID="DateColumn5" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate %>" DataIndex="dayIdDate" Hideable="false" Width="100" />
                          

                                                                     <ext:Column ID="Column26" DataIndex="timeCodeString" Text="<%$ Resources: FieldTimeCode %>"  runat="server" Flex="1" />
                                                                     <ext:Column ID="Column30" DataIndex="status" Text="<%$ Resources: FieldStatus %>" Flex="1" runat="server" >
                                                                         <Renderer Handler=" return getTimeStatus(record.data['status']); " />
                                                                    </ext:Column>
                                                                     <ext:Column ID="Column28" DataIndex="notes" Text="<%$ Resources: FieldNotes %>" runat="server" Flex="2" />

                                                                
                                                                    <ext:Column runat="server"
                                                                        ID="Column29" Visible="true"
                                                                        Text=""
                                                                        Width="100"
                                                                        Hideable="false"
                                                                        Align="Center"
                                                                        Fixed="true"
                                                                        Filterable="false"
                                                                        MenuDisabled="true"
                                                                        Resizable="false">

                                                                        <Renderer Handler="return  appendRender(); " />
                                                                    </ext:Column>



                                                                </Columns>
                                                            </ColumnModel>
                                                            <Listeners>
                                                                <Render Handler="this.on('cellclick', cellClick);" />
                                                                <Activate Handler="#{TimeStore}.reload();" />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                
                                                                <CellClick OnEvent="TimePoPUP">
                                                                    <EventMask ShowMask="true" />
                                                                      <ExtraParams>
                                                                         <ext:Parameter Name="employeeName" Value="record.data['employeeName'].fullName" Mode="Raw" />
                                                                         <ext:Parameter Name="employeeId" Value="record.data['employeeId']" Mode="Raw" />
                                                                         <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                                                           <ext:Parameter Name="dayIdDate" Value="record.data['dayIdDate']" Mode="Raw" />
                                                                            <ext:Parameter Name="timeCode" Value="record.data['timeCode']" Mode="Raw" />
                                                                          <ext:Parameter Name="timeCodeString" Value="record.data['timeCodeString']" Mode="Raw" />
                                                                         <ext:Parameter Name="notes" Value="record.data['notes']" Mode="Raw" />
                                                                           <ext:Parameter Name="status" Value="record.data['status']" Mode="Raw" />
                                                                    
                                                                      
                                                                        
                                                                         
                                                                    </ExtraParams>

                                                             

                                                                </CellClick>
                                                            </DirectEvents>


                                                            <View>
                                                                <ext:GridView ID="GridView24" runat="server" />
                                                            </View>


                                                            <SelectionModel>
                                                                <ext:RowSelectionModel ID="rowSelectionModel23" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                               
                                                            </SelectionModel>
                                                        </ext:GridPanel>
                                                         <ext:GridPanel MarginSpec="0 0 0 0"
                                                            ID="ApprovalLoanGrid"
                                                            runat="server"
                                                            PaddingSpec="0 0 1 0"
                                                            Header="false"
                                                            Title="<%$ Resources:ApprovalLoan %>"
                                                            Layout="FitLayout"
                                                            Scroll="Vertical"
                                                            Border="false"
                                                            ColumnLines="True" IDMode="Explicit" RenderXType="True" StyleSpec=" border: 1px solid #add2ed !important;">
                                                            <Store>
                                                                <ext:Store PageSize="30"
                                                                    ID="ApprovalLoanStore"
                                                                    runat="server" OnReadData="ApprovaLoan_ReadData"
                                                                    RemoteSort="false"
                                                                    RemoteFilter="false">
                                                                  <%--  <Proxy>
                                                                        <ext:PageProxy>
                                                                            <Listeners>
                                                                                <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                                                            </Listeners>
                                                                        </ext:PageProxy>
                                                                    </Proxy>--%>
                                                                    <Model>
                                                                        <ext:Model ID="Model25" runat="server" >
                                                                            <Fields>
                                                                                                                                                            
                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="employeeId" />
                                                                        <ext:ModelField Name="loanRef" />
                                                                        <ext:ModelField Name="ltId" />
                                                                        <ext:ModelField Name="date" />
                                                                        <ext:ModelField Name="effectiveDate" />
                                                                        <ext:ModelField Name="branchId" />
                                                                        <ext:ModelField Name="branchName" />
                                                                        <ext:ModelField Name="purpose" />
                                                                        <ext:ModelField Name="status" />
                                                                        <ext:ModelField Name="statusString" />
                                                                        <ext:ModelField Name="currencyId" />
                                                                        <ext:ModelField Name="amount" />
                                                                        <ext:ModelField Name="payments" />
                                                                        <ext:ModelField Name="ltName" />
                                                                        <ext:ModelField Name="currencyRef" />
                                                                        <ext:ModelField Name="deductedAmount" />
                                                                         <ext:ModelField Name="ldMethod" />
                                                                         <ext:ModelField Name="ldValue" />
                                                                        <ext:ModelField Name="employeeName" IsComplex="true" />
                                                                            

                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>

                                                                </ext:Store>
                                                            </Store>


                                                            <ColumnModel ID="ColumnModel25" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                                <Columns>
                                                                     <ext:Column ID="Column31" Visible="false" DataIndex="recordId" runat="server" />
                                                 <ext:Column ID="Column32" DataIndex="loanRef" Text="<%$ Resources: FieldReference%>" runat="server" Hidden="true" />
                                                <ext:Column ID="Column33" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                                                    <Renderer Handler=" return record.data['employeeName'].fullName; ">
                                                    </Renderer>
                                                </ext:Column>
                           
                                               <%-- <ext:Column ID="Column7" DataIndex="ltName" Text="<%$ Resources: FieldLtName%>" runat="server" Flex="1" />--%>
                                                <ext:Column ID="Column34" DataIndex="branchName" Text="<%$ Resources: FieldBranch%>" runat="server" Flex="1" />

                                                <ext:DateColumn ID="c" DataIndex="date" Text="<%$ Resources: FieldDate%>" runat="server" Width="100" Hidden="true" />

                                                <ext:Column ID="Column35" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldAmount %>" DataIndex="amount" Hideable="false" Width="140">
                                                    <Renderer Handler="return record.data['currencyRef']+ '&nbsp;'+record.data['amount']; "></Renderer>
                                                </ext:Column>
                                             <%--   <ext:Column ID="Column36" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDeductedAmount %>" DataIndex="deductedAmount" Hideable="false" Width="140">
                                                    <Renderer Handler="return  record.data['currencyRef']+ '&nbsp;'+record.data['deductedAmount'] ;"></Renderer>
                                                </ext:Column>--%>

                                               <%-- <ext:Column ID="Column12" DataIndex="purpose" Text="<%$ Resources: FieldPurpose%>" runat="server" Flex="2" />--%>


                                                <ext:Column ID="colStatus" DataIndex="statusString" Text="<%$ Resources: FieldStatus%>" runat="server" Width="100">
                                                   
                                                </ext:Column>

                                               <%-- <ext:DateColumn ID="cc" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEffectiveDate %>" DataIndex="effectiveDate" Hideable="false" Width="120" Align="Center">
                                                </ext:DateColumn>--%>
                                                <%-- <ext:Column ID="ldMethodCo" DataIndex="ldMethod " Text="<%$ Resources: LoanCoverageType %>" runat="server" Flex="2"   >
                                                     <Renderer Handler="return GetldMethodName(record.data['ldMethod']);" />
                                                     </ext:Column>--%>

                                               <%--  <ext:Column ID="ldValueCo" DataIndex="ldValue" Text="<%$ Resources: PaymentValue %>" runat="server" Flex="2" Visible="false" />--%>
                             
                           



                                                <ext:Column runat="server"
                                                    ID="Column37" Visible="false"
                                                    Text="<%$ Resources:Common, Edit %>"
                                                    Width="60"
                                                    Hideable="false"
                                                    Align="Center"
                                                    Fixed="true"
                                                    Filterable="false"
                                                    MenuDisabled="true"
                                                    Resizable="false">

                                                    <Renderer Fn="editRender" />

                                                </ext:Column>
                           
                                                <ext:Column runat="server"
                                                    ID="colAttach"
                                                    Text="<%$ Resources:Common, Attach %>"
                                                    Hideable="false"
                                                    Width="60"
                                                    Align="Center"
                                                    Fixed="true"
                                                    Filterable="false"
                                                    MenuDisabled="true" Visible="false"
                                                    Resizable="false">
                                                    <Renderer Fn="attachRender" />
                                                </ext:Column>
                                                 <ext:Column runat="server"
                                                    ID="colDelete"  Visible="true"
                                                    Text=""
                                                    MinWidth="60"
                                                    Align="Center"
                                                    Fixed="true"
                                                    Filterable="false"
                                                    Hideable="false"
                                                    MenuDisabled="true"
                                                    Resizable="false">
                                                    <Renderer Handler="return appendRender() ;" />

                                                  </ext:Column>


                                                                </Columns>
                                                            </ColumnModel>
                                                            <Listeners>
                                                                <Activate Handler="#{ApprovalLoanStore}.reload();" />
                                                                <Render Handler="this.on('cellclick', cellClick);" />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                
                                                                <CellClick OnEvent="ApprovalLoanPoPUP">
                                                                    <EventMask ShowMask="true" />
                                                                      <ExtraParams>
                                                                        <ext:Parameter Name="id" Value="record.data['recordId']" Mode="Raw" />
                                                                                                                                 
                                                                                                                                                
                                                                    </ExtraParams>

                                                             

                                                                </CellClick>
                                                            </DirectEvents>


                                                            <View>
                                                                <ext:GridView ID="GridView25" runat="server" />
                                                            </View>


                                                            <SelectionModel>
                                                                <ext:RowSelectionModel ID="rowSelectionModel24" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                               
                                                            </SelectionModel>
                                                        </ext:GridPanel>
                                                         
                                                    </Items>
                                                    <Listeners>

                                                        <TabChange Handler="Load(#{belowt}.activeTab.id);" />
                                                    </Listeners>
                                                </ext:TabPanel>


                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>

                                <ext:Panel runat="server" Flex="3" Layout="FitLayout" Region="West" PaddingSpec="10 0 0 0" BodyCls="withBackground" Cls="withBackground" StyleHtmlCls="withBackground" StyleSpec="border-radius: 0px;">

                                    <Listeners>
                                        <AfterRender Handler="startRefresh(); $('.flashing').fadeTo(1000, 0.1, function() { $(this).fadeTo(2000, 1.0); });" />
                                        <%--<AfterLayout Handler="$('.flashing').fadeTo(1000, 0.1, function() { $(this).fadeTo(2000, 1.0); });" />--%>
                                    </Listeners>
                                    <Items>

                                        <ext:TabPanel ID="alerts" runat="server" Layout="FitLayout" Plain="true" BodyCls="topper" StyleSpec="border-radius: 0px;">
                                            <Items>
                                                <ext:Panel runat="server" Layout="VBoxLayout" Title="<%$Resources:Alerts %>" StyleSpec=" border: 1px solid #add2ed !important;">
                                                    <LayoutConfig>
                                                        <ext:VBoxLayoutConfig Align="Stretch" />
                                                    </LayoutConfig>
                                                    <Items>


                                                        <ext:Panel runat="server" Layout="HBoxLayout" Flex="1">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch"></ext:HBoxLayoutConfig>
                                                            </LayoutConfig>
                                                            <Items>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="companyRW" Cls="number flashing" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:ComapnyRightToWork %>" ShrinkWrap="Both" PaddingSpec="0 0 0 0" Cls="lblStyle">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.CompanyRightToWorkWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>
                                                                    </Items>
                                                                </ext:Panel>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="employeeRW" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />
                                                                                <ext:HyperlinkButton runat="server" PaddingSpec="0 0 0 0" Text="<%$Resources:EmployeeRightToWork %>" StyleSpec="font-size:12pt;" Cls="lblStyle">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.EmployeeRightToWorkWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>

                                                                    </Items>
                                                                </ext:Panel>

                                                            </Items>
                                                        </ext:Panel>

                                                        <ext:Panel runat="server" Layout="HBoxLayout" Flex="1">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch"></ext:HBoxLayoutConfig>
                                                            </LayoutConfig>

                                                            <Items>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="salaryChange" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:SalaryChange %>" StyleSpec="font-size:12pt;" Cls="lblStyle">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.SCRWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>

                                                                    </Items>
                                                                </ext:Panel>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="probation" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:Probation %>" StyleSpec="font-size:12pt;" StyleHtmlCls="flashing" Cls="lblStyle">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.ProbationWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>
                                                                    </Items>
                                                                </ext:Panel>

                                                            </Items>
                                                        </ext:Panel>

                                                        <ext:Panel runat="server" Layout="HBoxLayout" Flex="1">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch"></ext:HBoxLayoutConfig>
                                                            </LayoutConfig>

                                                            <Items>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="totalLoansLbl" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:TotalLoans %>" StyleSpec="font-size:12pt;" Cls="lblStyle">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.totalLoansWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>
                                                                    </Items>
                                                                </ext:Panel>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>
                                                                                <ext:Label runat="server" ID="EmploymentReviewDateLbl" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />
                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:EmploymentReviewDate %>" StyleSpec="font-size:12pt;" StyleHtmlCls="flashing" Cls="lblStyle">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.completedLoansWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>
                                                                    </Items>
                                                                </ext:Panel>

                                                            </Items>
                                                        </ext:Panel>

                                                        <ext:Panel runat="server" Layout="HBoxLayout" Flex="1">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch"></ext:HBoxLayoutConfig>
                                                            </LayoutConfig>
                                                            <Items>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>

                                                                                <ext:Label runat="server" ID="annversaries" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />

                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:Anneversaries %>" PaddingSpec="0 0 0 0" StyleSpec="font-size:12pt;" Cls="lblStyle">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.anniversaryWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>
                                                                    </Items>
                                                                </ext:Panel>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>

                                                                                <ext:Label runat="server" ID="birthdays" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />
                                                                                <ext:HyperlinkButton runat="server" PaddingSpec="0 0 0 0" Text="<%$Resources:Birthdays %>" StyleSpec="font-size:12pt;" Cls="lblStyle">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.BirthdaysWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>
                                                                    </Items>
                                                                </ext:Panel>

                                                            </Items>
                                                        </ext:Panel>

                                                         <ext:Panel runat="server" Layout="HBoxLayout" Flex="1">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig Align="Stretch"></ext:HBoxLayoutConfig>
                                                            </LayoutConfig>
                                                            <Items>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>

                                                                                <ext:Label runat="server" ID="retirementAgeLBL" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />

                                                                                <ext:HyperlinkButton runat="server" Text="<%$Resources:retirementAge %>" PaddingSpec="0 0 0 0" StyleSpec="font-size:12pt;" Cls="lblStyle">
                                                                                    <Listeners>
                                                                                        <Click Handler="App.retirementAgeWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>
                                                                    </Items>
                                                                </ext:Panel>

                                                                <ext:Panel runat="server" Layout="VBoxLayout" Flex="1" PaddingSpec="2 2 2 2">
                                                                    <LayoutConfig>
                                                                        <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                    </LayoutConfig>
                                                                    <Items>
                                                                        <ext:Container runat="server" Cls="styleContainer">
                                                                            <LayoutConfig>
                                                                                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
                                                                            </LayoutConfig>
                                                                            <Items>

                                                                                <ext:Label runat="server" ID="termEndDateLBL" Cls="number flashing" StyleHtmlCls="number" PaddingSpec="0 0 0 0" MarginSpec="10 0 20 0" />
                                                                                <ext:HyperlinkButton runat="server" PaddingSpec="0 0 0 0" Text="<%$Resources:TermEndDate %>" StyleSpec="font-size:12pt;" Cls="lblStyle">
                                                                                   <Listeners>
                                                                                        <Click Handler="App.TermEndDateWindow.show();" />
                                                                                    </Listeners>
                                                                                </ext:HyperlinkButton>
                                                                            </Items>
                                                                        </ext:Container>
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
                </ext:Panel>

            </Items>
        </ext:Viewport>
        <ext:Window
            ID="MaximumChartWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:HeadCountMaximumWindow%>"
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
                    ID="pnlMaximumChart" ClientIDMode="Static" PaddingSpec="5 5 5 5"
                    Layout="FitLayout" Flex="1"  Html="<div id='maximumChart' style='width:100%;height:100%' />">
                </ext:Panel>
            </Items>

        </ext:Window>
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
                                <Renderer Handler="return record.data['days'] + ' ' + #{daysLeft}.value;" />


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
                                <Renderer Handler="return record.data['days'] + ' ' + #{daysLeft}.value;" />


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

                                <Renderer Handler="return record.data['days'] + ' ' + #{daysLeft}.value;" />

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
                                <Renderer Handler="return record.data['days'] + ' ' + #{daysLeft}.value;" />


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
                                <Renderer Handler="return record.data['days'] + ' ' + #{daysLeft}.value;" />

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
                           <%-- <Proxy>
                               <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>--%>

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
        <ext:Window runat="server" Modal="true" Title="<%$ Resources: UnLatenessGridTitle %>" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="UnlateWindow" Width="650" Height="300">
            <Items>
                <ext:GridPanel ExpandToolText="expand"
                    ID="GridPanel14" MarginSpec="0 0 0 0"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false" CollapseMode="Header" Collapsible="true" CollapseDirection="Right"
                    Title="<%$ Resources: UnLatenessGridTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store PageSize="30"
                            ID="UnlateStore"
                            runat="server" OnReadData="UnlateStore_ReadData"
                            RemoteSort="false"
                            RemoteFilter="false">
                          <%--  <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>--%>

                            <Model>
                                <ext:Model ID="Model19" runat="server" IDProperty="recordId">
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


                    <ColumnModel ID="ColumnModel19" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column Visible="false" ID="Column13" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
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
                        <ext:GridView ID="GridView19" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel18" runat="server" Mode="Single" StopIDModeInheritance="true" />
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

        <ext:Window runat="server" Modal="true" Title="<%$ Resources: ActiveGridTitle %>"
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="InWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel15" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Title="<%$ Resources: ActiveGridTitle %>"
                    Scroll="Vertical"
                    Border="false"
                    StoreID="InStore"
                    ColumnLines="True">




                    <ColumnModel ID="ColumnModel20" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                         <%--   <ext:Column Visible="false" ID="Column22" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />--%>
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
                        <ext:GridView Border="false" ID="GridView20" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel19" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
        <ext:Window runat="server" Modal="true" Title="<%$ Resources: Breaks %>"
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="OutWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel16" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Title="<%$ Resources: Breaks %>"
                    Scroll="Vertical"
                    Border="false"
                    StoreID="OutStore"
                    ColumnLines="True">




                    <ColumnModel ID="ColumnModel21" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column23" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
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
                        <ext:GridView Border="false" ID="GridView21" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel20" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
        <ext:Window
            ID="LeaveRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:LeaveRecordForm%>"
            Width="450"
            Height="500"
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
                            ID="LeaveRecordForm" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources:LeaveRecordForm%>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="LeaveRecordTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="TextField1" runat="server" Name="recordId" Hidden="true" />
                                <ext:TextField ID="leaveRef" runat="server" Name="leaveRef" FieldLabel="<%$ Resources:FieldLeaveRef%>" ReadOnly="true" />
                                <ext:TextField ID="employeeName" runat="server" Name="employeeName.fullName" FieldLabel="<%$ Resources:FieldEmployeeName%>" ReadOnly="true" />


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
                                <ext:DateField ID="startDate" runat="server" FieldLabel="<%$ Resources:FieldStartDate%>" Name="startDate" ReadOnly="true" />
                                <ext:DateField ID="endDate" runat="server" FieldLabel="<%$ Resources:FieldendDate%>" Name="endDate" ReadOnly="true" />




                                <ext:TextField ID="ltName" runat="server" FieldLabel="<%$ Resources:FieldLtName %>" Name="ltName" ReadOnly="true" />
                                <ext:TextField ID="destination" runat="server" FieldLabel="<%$ Resources:FieldDestination%>" Name="destination" ReadOnly="true" />





                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="status" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" Name="status"
                                    FieldLabel="<%$ Resources: FieldStatus %>">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="<%$ Resources:ComboBoxValues, SYLMLRStatusNew %>"  />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="<%$ Resources:ComboBoxValues, SYLMLRStatusApproved %>" />
                                        <ext:ListItem Text="<%$ Resources: FieldRefused %>" Value="<%$ Resources:ComboBoxValues, SYLMLRStatusRefused %>" />
                                    </Items>

                                </ext:ComboBox>





                                <ext:TextArea runat="server" FieldLabel="<%$Resources: ReturnNotes %>" ID="returnNotes" Name="returnNotes" MaxHeight="5" />


                            </Items>

                        </ext:FormPanel>


                    </Items>
                </ext:TabPanel>
            </Items>

            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{LeaveRecordForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{LeaveRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{LeaveRecordForm}.getForm().getValues()" Mode="Raw" Encode="true" />
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

         <ext:Window runat="server" Modal="true" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="retirementAgeWindow" Width="400" Height="250" Title="<%$ Resources: retirementAge %>">
            <Listeners>
                <AfterLayout Handler="App.retirementAgeStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel17"
                    runat="server" HideHeaders="true"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <Store>
                        <ext:Store
                            ID="retirementAgeStore"
                            runat="server" OnReadData="retirementAge_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model22" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="hireDate" />
                                        <ext:ModelField Name="days" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>

                    <ColumnModel ID="ColumnModel22" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column24" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayAnniversary(record.data);  ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDaysLeft %>" DataIndex="days" Hideable="false" Width="120">
                                <Renderer Handler="return record.data['days'] + ' ' + #{daysLeft}.value;" />


                            </ext:Column>









                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView ID="GridView22" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel21" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
         <ext:Window runat="server" Modal="true" Layout="FitLayout"
            Hidden="true" AutoShow="false" ID="TermEndDateWindow" Width="400" Height="250" Title="<%$ Resources: TermEndDate %>">
            <Listeners>
                <AfterLayout Handler="App.TermEndDateStore.reload()" />
            </Listeners>
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel18"
                    runat="server" HideHeaders="true"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <Store>
                        <ext:Store
                            ID="TermEndDateStore"
                            runat="server" OnReadData="TermEndDate_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model23" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="hireDate" />
                                        <ext:ModelField Name="days" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>

                    <ColumnModel ID="ColumnModel23" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column25" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayAnniversary(record.data);  ">
                                </Renderer>
                            </ext:Column>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDaysLeft %>" DataIndex="days" Hideable="false" Width="120">
                                <Renderer Handler="return record.data['days'] + ' ' + #{daysLeft}.value;" />


                            </ext:Column>









                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView ID="GridView23" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel22" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
         <ext:Window
            ID="TimeWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:Time%>"
            Width="450"
            Height="500"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Resizable="false"
            Maximizable="false"
            Draggable="false"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">

                   
          
           
                    <Items>
                        <ext:FormPanel
                            ID="TimeFormPanel" DefaultButton="SaveTimeButton"
                            runat="server"
                            Title="<%$ Resources:Time %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="TimeTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="TimeemployeeIdTF" runat="server" Name="employeeId"  ReadOnly="true"  Hidden="true"/>
                                 <ext:TextField ID="TimedayIdTF" runat="server" Name="dayId"  ReadOnly="true" Hidden="true" />
                                  <ext:TextField ID="TimeTimeCodeTF" runat="server" Name="timeCode"  ReadOnly="true" Hidden="true" />
                               <ext:TextField ID="TimeEmployeeName" runat="server" FieldLabel="<%$ Resources:FieldName%>" ReadOnly="true" />
                                <ext:DateField ID="TimedayIdDate" runat="server" Name="dayIdDate"  FieldLabel="<%$ Resources:FieldDate%>"  ReadOnly="true" />
                                <ext:TextField ID="TimeTimeCodeString" runat="server" Name="timeCodeString" FieldLabel="<%$ Resources: FieldTimeCode%>"   ReadOnly="true" />
                                 
                                
                               
                              


                            
                              



                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="TimeStatus" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" Name="status"
                                    FieldLabel="<%$ Resources: FieldStatus %>">
                                    <Items>

                                       <%-- <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="1" />--%>
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="<%$ Resources:ComboBoxValues,  SYTATATimeStatusApproved %>" />
                                        <ext:ListItem Text="<%$ Resources: FieldRefused %>" Value="<%$ Resources:ComboBoxValues,  SYTATATimeStatusRefused %>" />
                                    </Items>

                                </ext:ComboBox>





                                <ext:TextArea runat="server" FieldLabel="<%$Resources: FieldNotes %>" ID="TimeNotes" Name="notes" MaxHeight="5" />


                            </Items>

                        </ext:FormPanel>


                    </Items>
                </ext:TabPanel>
            </Items>

            <Buttons>
                <ext:Button ID="SaveTimeButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{TimeFormPanel}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveTimeRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{TimeWindow}.body}" />



                            <ExtraParams>
                               
                                <ext:Parameter Name="values" Value="#{TimeFormPanel}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>

        </ext:Window>

         <ext:Window
            ID="approvalLoanWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:ApprovalLoan %>"
            Width="600"
            Height="526"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Maximizable="false"
            Resizable="false"
            Draggable="True"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="ApprovalLoanForm" DefaultButton="SaveApprovalButton"
                            runat="server"
                            Title="<%$ Resources: ApprovalLoan %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="ApprovalLoanTab_load"
                            BodyPadding="5"  AutoScroll="true">
                            <Items>
                                <ext:TextField  ID="ApprovalRecordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:TextField  ID="ApprovalLoanEmployeeName" FieldLabel="<%$ Resources: FieldEmployeeName %>"  runat="server" Name="employeeName" ReadOnly="true" />
                          
                                <ext:TextField runat="server" ID="loanRef" Name="loanRef" FieldLabel="<%$ Resources: FieldReference %>" ReadOnly="true" />

                                

                              

                                <%--<ext:TextField ID="employeeName" runat="server" FieldLabel="<%$ Resources:FieldEmployeeName%>" Name="employeeName"   AllowBlank="false"/>--%>
                                <ext:DateField ID="date" runat="server" FieldLabel="<%$ Resources:FieldDate%>" Name="date" AllowBlank="false"  ReadOnly="true">
                                     <%--<CustomConfig>
                        <ext:ConfigItem Name="endDateField" Value="effectiveDate" Mode="Value" />
                    </CustomConfig>--%>
                                    </ext:DateField>

                              

                                <ext:TextField  ID="amount" AllowBlank="false" runat="server" FieldLabel="<%$ Resources:FieldAmount%>" Name="amount" ReadOnly="true">
                                
                                </ext:TextField>


                                <ext:TextArea ID="purpose" runat="server" FieldLabel="<%$ Resources:FieldPurpose%>" Name="purpose" AllowBlank="false" ReadOnly="true" />
                                 <ext:TextField ID="purposeField" InputType="Password" Visible="false" runat="server" FieldLabel="<%$ Resources:FieldPurpose%>" Name="purpose" AllowBlank="false" ReadOnly="true" />
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="ApprovalLoanStatus" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    FieldLabel="<%$ Resources: FieldStatus %>" AllowBlank="false" SubmitValue="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="<%$ Resources:ComboBoxValues,  SYLTLRApprovalLoanStatusNew %>" />
                                        <ext:ListItem Text="<%$ Resources: FieldInProcess %>" Value="<%$ Resources:ComboBoxValues,  SYLTLRApprovalLoanStatusInProcess %>" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="<%$ Resources:ComboBoxValues,  SYLTLRApprovalLoanStatusApproved %>" />
                                        <ext:ListItem Text="<%$ Resources: FieldRejected %>" Value="<%$ Resources:ComboBoxValues,  SYLTLRApprovalLoanStatusRejected %>" />
                                    </Items>
                                    <Listeners>
                                        <%--<Change Handler="if(this.value==3) {this.next().setDisabled(false); this.next().setValue(new Date());} else {this.next().setDisabled(true); this.next().clear();}">
                                        </Change>--%>
                                    </Listeners>
                                </ext:ComboBox>

                                <ext:DateField AllowBlank="false"  runat="server" ID="effectiveDate" Name="effectiveDate" FieldLabel="<%$ Resources:FieldEffectiveDate%>" Vtype="daterange" ReadOnly="true"  >
                                   <%-- <CustomConfig>
                        <ext:ConfigItem Name="startDateField" Value="date" Mode="Value" />
                    </CustomConfig>--%>
                                    </ext:DateField>
                                
                                      
                                        <ext:NumberField Width="400"  runat="server"  ID="ldValue" Name="ldValue" FieldLabel="<%$ Resources: PaymentValue %>"  AllowBlank="false" ReadOnly="true" >
                                        
                                      
                                           
                                            </ext:NumberField>
                                   <ext:TextArea ID="LoanNotes" runat="server" FieldLabel="<%$ Resources:FieldNotes%>" Name="notes" AllowBlank="true"   MaxHeight="200"  Height="100"/>
                                

                            </Items>
                            <Buttons>
                                <ext:Button ID="Button1"  runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{ApprovalLoanForm}.getForm().isValid()) {return false;}  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveApprovalLoanRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{ApprovalLoanWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="id" Value="#{ApprovalRecordId}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="status" Value="#{ApprovalLoanStatus}.getValue()" Mode="Raw" />
                                                  <ext:Parameter Name="notes" Value="#{LoanNotes}.getValue()" Mode="Raw" />
                                                 
                                                <ext:Parameter Name="values" Value="#{ApprovalLoanForm}.getForm().getValues()" Mode="Raw" Encode="true" />
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

                        </ext:FormPanel>

                    

                  


                      



                    </Items>
                </ext:TabPanel>
            </Items>

        </ext:Window>


        <ext:Window runat="server" Modal="true" 
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="CheckedWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="CheckedGridPanel" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                   
                    Scroll="Vertical"
                    Border="false"
                  
                    ColumnLines="True">

                    <Store>
                    <ext:Store
                            ID="CheckedStore"
                            runat="server" OnReadData="Checked_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model26" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="firstPunch" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>


                    <ColumnModel ID="ColumnModel26" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFirstPunch%>" DataIndex="firstPunch" Width="55" Hideable="false" />
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                           <%-- <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />--%>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            <%--  <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayActive(record.data);  ">
                                </Renderer>
                            </ext:Column>--%>
                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView Border="false" ID="GridView26" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel25" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
          <ext:Window runat="server" Modal="true" 
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="PendingWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="PendingGrid" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                   
                    Scroll="Vertical"
                    Border="false"
                  
                    ColumnLines="True">

                    <Store>
                    <ext:Store
                            ID="PendingStore"
                            runat="server" OnReadData="Pending_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model27" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="dayStart" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="dayEnd" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>


                    <ColumnModel ID="ColumnModel27" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                           
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDayStart%>" DataIndex="dayStart" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDayEnd%>" DataIndex="dayEnd" Flex="3" Hideable="false" />
                        <%--    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />--%>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            <%--  <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayActive(record.data);  ">
                                </Renderer>
                            </ext:Column>--%>
                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView Border="false" ID="GridView27" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel26" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
           <ext:Window runat="server" Modal="true" 
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="NoShowUpWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="NoShowUpGrid" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                   
                    Scroll="Vertical"
                    Border="false"
                  
                    ColumnLines="True">

                    <Store>
                    <ext:Store
                            ID="NoShowUpStore"
                            runat="server" OnReadData="NoShowUp_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model28" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="dayStart" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="dayEnd" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>


                    <ColumnModel ID="ColumnModel28" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                           
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDayStart%>" DataIndex="dayStart" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDayEnd%>" DataIndex="dayEnd" Flex="3" Hideable="false" />
                        <%--    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />--%>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            <%--  <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayActive(record.data);  ">
                                </Renderer>
                            </ext:Column>--%>
                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView Border="false" ID="GridView28" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel27" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
            <ext:Window runat="server" Modal="true" 
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="LeaveWithoutExcuseWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="LeaveWithoutExcuseGrid" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                   
                    Scroll="Vertical"
                    Border="false"
                  
                    ColumnLines="True">

                    <Store>
                    <ext:Store
                            ID="LeaveWithoutExcuseStore"
                            runat="server" OnReadData="LeaveWithoutExcuse_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model29" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="dayStart" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="dayEnd" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>


                    <ColumnModel ID="ColumnModel29" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                           
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDayStart%>" DataIndex="dayStart" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDayEnd%>" DataIndex="dayEnd" Flex="3" Hideable="false" />
                        <%--    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />--%>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            <%--  <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayActive(record.data);  ">
                                </Renderer>
                            </ext:Column>--%>
                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView Border="false" ID="GridView29" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel28" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
        <ext:Window runat="server" Modal="true" 
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="LeaveWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="LeaveGridPanel" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                   
                    Scroll="Vertical"
                    Border="false"
                  
                    ColumnLines="True">

                    <Store>
                    <ext:Store
                            ID="LeaveStore"
                            runat="server" OnReadData="Leave_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model30" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="from" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="to" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>


                    <ColumnModel ID="ColumnModel30" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                           
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFrom%>" DataIndex="from" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldTo%>" DataIndex="to" Flex="3" Hideable="false" />
                        <%--    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />--%>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            <%--  <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayActive(record.data);  ">
                                </Renderer>
                            </ext:Column>--%>
                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView Border="false" ID="GridView30" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel29" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
          <ext:Window runat="server" Modal="true" 
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="DayOffWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="DayOffGrid" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                   
                    Scroll="Vertical"
                    Border="false"
                  
                    ColumnLines="True">

                    <Store>
                    <ext:Store
                            ID="DayOffStore"
                            runat="server" OnReadData="DayOff_ReadData"
                            PageSize="30">

                            <Model>
                                <ext:Model ID="Model31" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                        <ext:ModelField Name="dayStart" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="dayEnd" />

                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>


                    <ColumnModel ID="ColumnModel31" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                           
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDayStart%>" DataIndex="dayStart" Flex="3" Hideable="false" />
                                          <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDayEnd%>" DataIndex="dayEnd" Flex="3" Hideable="false" />
                        <%--    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false" />--%>
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                            <%--  <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" Width="75">
                                <Renderer Handler=" return displayActive(record.data);  ">
                                </Renderer>
                            </ext:Column>--%>
                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView Border="false" ID="GridView31" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel30" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>
        <ext:Window runat="server" Modal="true" 
            Hidden="true" Layout="FitLayout" AutoShow="false" ID="TimeVariationWindow" Width="600" Height="300">
            <Items>
                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="TimeVariationGrid" Layout="FitLayout"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                   
                    Scroll="Vertical"
                    Border="false"
                  
                    ColumnLines="True">

                    <Store>
                    <ext:Store
                            ID="TimeVariationStore"
                            runat="server" 
                            PageSize="30" OnReadData="TimeVariationStore_ReadData">

                            <Model>
                                <ext:Model ID="Model32" runat="server">
                                    <Fields>


                                        <ext:ModelField Name="name" ServerMapping="employeeName.fullName" />
                                      
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="positionName" />
                                        <ext:ModelField Name="branchName" />
                                        <ext:ModelField Name="dayId" />
                                          <ext:ModelField Name="shiftId" />
                                           <ext:ModelField Name="timeCode" />
                                            <ext:ModelField Name="clockDuration" />   
                                            <ext:ModelField Name="apStatus" />
                                            <ext:ModelField Name="duration" />
                                           <ext:ModelField Name="apStatusString" />


                                    </Fields>
                                </ext:Model>
                            </Model>

                        </ext:Store>
                    </Store>


                    <ColumnModel ID="ColumnModel32" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            
                            <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployee %>" DataIndex="name" Hideable="false" />
                           
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                         
                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                              <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldClockDuration%>" DataIndex="clockDuration" Flex="3" Hideable="false" />
                              <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldStatus%>" DataIndex="apStatusString" Flex="3" Hideable="false" />
                                 <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDuration%>" DataIndex="duration" Flex="3" Hideable="false" />     
                          
                        </Columns>
                    </ColumnModel>


                    <View>
                        <ext:GridView Border="false" ID="GridView32" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel31" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Window>

        <uc:leaveControl runat="server" ID="leaveRequest1" />
      


    </form>
</body>
</html>
