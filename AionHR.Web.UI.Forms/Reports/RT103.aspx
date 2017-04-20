<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT103.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT103" %>

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



                        <ext:ModelField Name="headCount" />
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
                        <ext:Toolbar runat="server" Height="60">
                            <Items>

                               
                                        <ext:Container runat="server">
                                            <Content>
                                                <uc:dateRange runat="server" ID="dateRange1" />
                                            </Content>
                                        </ext:Container>
                                <ext:ToolbarFill runat="server" />
                                        <ext:Button runat="server" Text="<%$Resources:Common, Go %>">
                                            <Listeners>
                                                <Click Handler="App.firstStore.reload(); App.secondStore.reload();" />
                                            </Listeners>
                                        </ext:Button>
                             <ext:ToolbarFill runat="server" />
                                <ext:ToolbarFill runat="server" />
                                 <ext:ToolbarFill runat="server" />
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
