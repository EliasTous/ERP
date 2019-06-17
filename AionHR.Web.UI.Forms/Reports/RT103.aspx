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
    <script type="text/javascript" src="../Scripts/ReportsCommon.js" ></script>
    <script type="text/javascript">
        function alertNow(s, e) {

            Ext.MessageBox.alert(App.Error.getValue(), e.message);
            e.handled = true;
        }
    </script>
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
          <ext:Hidden ID="MaximumValue" runat="server" Text="100" />
               <ext:Hidden ID="Error" runat="server" Text="<%$ Resources:Common , Error %>" />
        <ext:Hidden ID="rtl" runat="server" />
        <ext:Hidden ID="vals" runat="server" />
        <ext:Hidden ID="texts" runat="server" />
        <ext:Hidden ID="labels" runat="server" />
        <ext:Hidden ID="format" runat="server" />
        <ext:Hidden ID="loaderUrl" runat="server"  Text="../ReportParameterBrowser.aspx?_reportName=RT103&values="/>

       
        <ext:Store PageSize="30"
            ID="firstStore" OnReadData="firstStore_ReadData"
            runat="server"
            RemoteSort="False"
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
                   <Tools>
                       <ext:Tool Type="Print" Handler="window.print();" />
                   </Tools>
                    <DockedItems>
                        <ext:Toolbar runat="server" Height="30" Dock="Top">

                            <Items>
                             
                             
                                
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                          <ext:Button runat="server" Text="<%$ Resources:Common, Parameters%>"> 
                                       <Listeners>
                                           <Click Handler=" App.reportsParams.show();" />
                                       </Listeners>
                                        </ext:Button>
                                         <ext:Button runat="server" Text="<%$Resources:Common, Go %>" >
                                            <Listeners>
                                                <Click Handler="App.firstStore.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                       
                                    </Content>
                                </ext:Container>
                                       
                        

                            </Items>
                        </ext:Toolbar>
                           
                        <ext:Toolbar ID="labelbar" runat="server" Height="0" Dock="Top">

                            <Items>
                                 <ext:Label runat="server" ID="selectedFilters" />
                                </Items>
                            </ext:Toolbar>
                  </DockedItems>
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
                                            Minimum="0">
                                  
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
         <ext:Window runat="server"  Icon="PageEdit"
            ID="reportsParams"
            Width="600"
             MinHeight="400"
            Title="<%$Resources:Common,Parameters %>"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="FitLayout" Resizable="true">
            <Listeners>
                <Show Handler="App.Panel8.loader.load();"></Show>
            </Listeners>
            <Items>
                <ext:Panel runat="server" Layout="FitLayout"  ID="Panel8" DefaultAnchor="100%">
                    <Loader runat="server" Url="../ReportParameterBrowser.aspx?_reportName=RT103" Mode="Frame" ID="Loader8" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <Listeners>
                         </Listeners>
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            
                </Items>
        </ext:Window>





    </form>
</body>
</html>
