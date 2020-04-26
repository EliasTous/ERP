<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT304.aspx.cs" Inherits="Web.UI.Forms.Reports.RT304" %>

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

    <style type="text/css">
        #ui-datepicker-div{z-index:99999!important;}
    </style>
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />




        <ext:Store ID="firstStore"  runat="server">
            <Model>
                <ext:Model runat="server">
                    <Fields>
                        <ext:ModelField Name="period" />
                        <ext:ModelField Name="day1" />
                        <ext:ModelField Name="day2" />
                        <ext:ModelField Name="day3" />
                        <ext:ModelField Name="day4" />
                        <ext:ModelField Name="day5" />
                        <ext:ModelField Name="day6" />
                        <ext:ModelField Name="day7" />
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
                        <ext:Toolbar runat="server" Height="60">
                            <Items>


                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" EnableBranch="false" EnableDivision="false" EnablePosition="false" />
                                    </Content>
                                </ext:Container>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:weekControl runat="server" ID="weekControl1" />
                                    </Content>
                                </ext:Container>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <ext:Button runat="server" Text="<%$Resources:Common, Go %>">
                                            <Listeners>
                                                <Click Handler="CheckSession(); App.Chart1.setHidden(false);"  />
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="Unnamed_Event">
                                                    <ExtraParams>
                                                        <ext:Parameter Name="week" Value="App.weekControl1_week.value" Mode="Raw" />
                                                    </ExtraParams>

                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Content>
                                </ext:Container>


                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <Items>
                        <ext:Panel MarginSpec="50 0 0 0" runat="server" Height="200" Layout="FitLayout" Width="1000" AutoScroll="true" ID="toPrint">

                            <Items>
                                <ext:CartesianChart Hidden="true"
                                    StoreID="firstStore"
                                    ID="Chart1"
                                    runat="server"
                                    InsetPadding="40"
                                    InnerPadding="40 40 0 40">
                                    <Interactions>
                                        <ext:PanZoomInteraction ZoomOnPanGesture="true" />
                                    </Interactions>
                                    <LegendConfig runat="server" Dock="Right" />
                                    <Axes>
                                        <ext:NumericAxis MinorTickSteps="1" 
                                            Position="Left"
                                            Fields="day1,day2,day3,day4,day5,day6,day7"
                                            Grid="true"
                                            Minimum="0"
                                           >
                                            <Renderer Handler="return layoutContext.renderer(label)" />
                                        </ext:NumericAxis>

                                        <ext:CategoryAxis
                                            Position="Bottom"
                                            Fields="period"
                                            Grid="true">
                                            <Label RotationDegrees="-90" />
                                        </ext:CategoryAxis>
                                    </Axes>
                                     <Series>
                        <ext:LineSeries XField="period" YField="day1" Title="<%$Resources:Common,MondayText %>"  >
                            <StyleSpec>
                                <ext:Sprite LineWidth="4" />
                            </StyleSpec>

                            <HighlightConfig>
                                <ext:Sprite FillStyle="#000" Radius="5" LineWidth="2" StrokeStyle="#fff" />
                            </HighlightConfig>

                            <Marker>
                                <ext:Sprite Radius="4" />
                            </Marker>

                            <Label Field="day1" Display="Over" />

                            <Tooltip runat="server" TrackMouse="true" ShowDelay="0" DismissDelay="0" HideDelay="0">
                                
                            </Tooltip>
                        </ext:LineSeries>
                                         <ext:LineSeries XField="period" YField="day2"  Title="<%$Resources:Common,TuesdayText %>" >
                            <StyleSpec>
                                <ext:Sprite LineWidth="3" />
                            </StyleSpec>

                            <HighlightConfig>
                                <ext:Sprite FillStyle="#000" Radius="5" LineWidth="2" StrokeStyle="#fff" />
                            </HighlightConfig>

                            <Marker>
                                <ext:Sprite Radius="4" />
                            </Marker>

                            <Label Field="day2" Display="Over" />

                            <Tooltip runat="server" TrackMouse="true" ShowDelay="0" DismissDelay="0" HideDelay="0">
                                
                            </Tooltip>
                        </ext:LineSeries>
                                         <ext:LineSeries XField="period" YField="day3" Title="<%$Resources:Common,WednesdayText %>" >
                            <StyleSpec>
                                <ext:Sprite LineWidth="4" />
                            </StyleSpec>

                            <HighlightConfig>
                                <ext:Sprite FillStyle="#000" Radius="5" LineWidth="2" StrokeStyle="#fff" />
                            </HighlightConfig>

                            <Marker>
                                <ext:Sprite Radius="4" />
                            </Marker>

                            <Label Field="day3" Display="Over" />

                            <Tooltip runat="server" TrackMouse="true" ShowDelay="0" DismissDelay="0" HideDelay="0">
                                
                            </Tooltip>
                        </ext:LineSeries>
                                          <ext:LineSeries XField="period" YField="day4" Title="<%$Resources:Common,ThursdayText %>" >
                            <StyleSpec>
                                <ext:Sprite LineWidth="4" />
                            </StyleSpec>

                            <HighlightConfig>
                                <ext:Sprite FillStyle="#000" Radius="5" LineWidth="2" StrokeStyle="#fff" />
                            </HighlightConfig>

                            <Marker>
                                <ext:Sprite Radius="4" />
                            </Marker>

                            <Label Field="day4" Display="Over" />

                            <Tooltip runat="server" TrackMouse="true" ShowDelay="0" DismissDelay="0" HideDelay="0">
                                
                            </Tooltip>
                        </ext:LineSeries>
                                          <ext:LineSeries XField="period" YField="day5" Title="<%$Resources:Common,FridayText %>" >
                            <StyleSpec>
                                <ext:Sprite LineWidth="4" />
                            </StyleSpec>

                            <HighlightConfig>
                                <ext:Sprite FillStyle="#000" Radius="5" LineWidth="2" StrokeStyle="#fff" />
                            </HighlightConfig>

                            <Marker>
                                <ext:Sprite Radius="4" />
                            </Marker>

                            <Label Field="day5" Display="Over" />

                            <Tooltip runat="server" TrackMouse="true" ShowDelay="0" DismissDelay="0" HideDelay="0">
                                
                            </Tooltip>
                        </ext:LineSeries>
                                          <ext:LineSeries XField="period" YField="day6" Title="<%$Resources:Common,SaturdayText %>" >
                            <StyleSpec>
                                <ext:Sprite LineWidth="4" />
                            </StyleSpec>

                            <HighlightConfig>
                                <ext:Sprite FillStyle="#000" Radius="5" LineWidth="2" StrokeStyle="#fff" />
                            </HighlightConfig>

                            <Marker>
                                <ext:Sprite Radius="4" />
                            </Marker>

                            <Label Field="day6" Display="Over" />

                            <Tooltip runat="server" TrackMouse="true" ShowDelay="0" DismissDelay="0" HideDelay="0">
                                
                            </Tooltip>
                        </ext:LineSeries>
                                          <ext:LineSeries XField="period" YField="day7" Title="<%$Resources:Common,SundayText %>" >
                            <StyleSpec>
                                <ext:Sprite LineWidth="4" />
                            </StyleSpec>

                            <HighlightConfig>
                                <ext:Sprite FillStyle="#000" Radius="5" LineWidth="2" StrokeStyle="#fff" />
                            </HighlightConfig>

                            <Marker>
                                <ext:Sprite Radius="4" />
                            </Marker>

                            <Label Field="day7" Display="Over" />

                            <Tooltip runat="server" TrackMouse="true" ShowDelay="0" DismissDelay="0" HideDelay="0">
                                
                            </Tooltip>
                        </ext:LineSeries>
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



