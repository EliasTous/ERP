<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT304.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT304" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />

    <script type="text/javascript" src="../Scripts/moment.js"></script>

    <script type="text/javascript" src="../Scripts/common.js"></script>


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
                        <ext:Toolbar runat="server" Height="60">
                            <Items>


                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:dateRange runat="server" ID="dateRange1" />
                                    </Content>
                                </ext:Container>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <ext:Button runat="server" Text="<%$Resources:Common, Go %>">
                                            <Listeners>
                                                <Click Handler="App.firstStore.reload(); App.secondStore.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Content>
                                </ext:Container>


                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <Items>
                        <ext:Panel runat="server" Height="200" Layout="FitLayout" Width="1000" AutoScroll="true" ID="toPrint">
                            <Items>

                                <ext:CartesianChart
                                    ID="Chart1"
                                    runat="server"
                                    StoreID="firstStore"
                                    Layout="FitLayout"
                                    StyleSpec="background:#fff;"
                                    InsetPadding="40">


                                    <Axes>
                                        <ext:NumericAxis Title="<%$ Resources: EmployeeCount %>"
                                            Fields="headCount"
                                            Position="Left"
                                            Grid="true"
                                            Minimum="0"
                                            Maximum="100">
                                            <Renderer Handler="return layoutContext.renderer(label) ;" />
                                        </ext:NumericAxis>

                                        <ext:CategoryAxis Title="<%$ Resources: FieldDate %>"
                                            Position="Bottom"
                                            Fields="date"
                                            Grid="true">
                                            <Renderer Handler="var s = moment(label); return s.format(#{format}.value);" />
                                            <Label RotationDegrees="-45" />
                                        </ext:CategoryAxis>
                                    </Axes>
                                    <Series>
                                        <ext:AreaSeries
                                            XField="date"
                                            YField="headCount">
                                            <StyleSpec>
                                                <ext:Sprite GlobalAlpha="0.8" />
                                            </StyleSpec>
                                            <Marker>
                                                <ext:CircleSprite GlobalAlpha="0" ScalingX="0.01" ScalingY="0.01" Duration="200" Easing="EaseOut" />
                                            </Marker>
                                            <HighlightDefaults>
                                                <ext:CircleSprite GlobalAlpha="1" ScalingX="1.5" ScalingY="1.5" />
                                            </HighlightDefaults>
                                            <Tooltip runat="server" TrackMouse="true" StyleSpec="background: #fff">
                                                <Renderer Handler=" var s = moment(record.get('date'));  toolTip.setHtml(s.format(#{format}.value) + ': ' + record.get('headCount') );" />
                                            </Tooltip>
                                        </ext:AreaSeries>
                                    </Series>
                                </ext:CartesianChart>


                            </Items>

                        </ext:Panel>
                    </Items>
                </ext:Panel>


            </Items>
        </ext:Viewport>



    </form>
</body>
</html>



