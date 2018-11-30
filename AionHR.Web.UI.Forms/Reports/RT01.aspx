<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT01.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT01" %>

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
<body style="background: url(Images/bg.png) repeat;" >
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />

        <ext:Hidden ID="rtl" runat="server" />
        <ext:Hidden ID="format" runat="server" />


        <ext:Store PageSize="30"
                                    ID="reportStore" OnReadData="reportStore_ReadData"
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
                                        <ext:Model ID="Model3" runat="server" >
                                            <Fields>

                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="age00_18" />
                                                <ext:ModelField Name="age18_25" />
                                                <ext:ModelField Name="age26_30" />
                                                <ext:ModelField Name="age30_40" />
                                                <ext:ModelField Name="age40_50" />
                                                <ext:ModelField Name="age50_60" />
                                                <ext:ModelField Name="age60_99" />
                                                

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
                        <ext:Toolbar runat="server"  Height="75">
                            
                            <Items>
                                <ext:Container runat="server">
                                    <Content>
                                       
                               
                      
                                    </Content>
                                </ext:Container>
                                <ext:ToolbarFill runat="server" />
                                <ext:Button Icon="PageGear" runat="server"
                                    >
                                    <Menu>
                                        <ext:Menu runat="server" >
                                            <Items>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , Print %>"  Icon="Printer">
                                                    <Listeners>
                                                        <Click Handler="App.Center.print();" />
                                                    </Listeners>
                                                    </ext:MenuItem>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , ExportAsPdf %>"  Icon="DiskDownload"/>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , EmailReport %>"  Icon="EmailAttach"/>
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>   
                
                    <Items>
                        <ext:Panel runat="server"  Height="200" Layout="AutoLayout" Width="1000" AutoScroll="true" ID="toPrint" >
                            <Items>
                         <ext:CartesianChart
                    ID="CartesianChart1"  PaddingSpec="0 0 0 60"  Height="500"
                    runat="server">
                    <Store>
                        <ext:Store
                            runat="server"
                           ID="summaryStore"
                            AutoDataBind="true">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="id" />
                                        <ext:ModelField Name="count" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>

                    <Axes>
                        <ext:NumericAxis
                            Position="Left"
                            Fields="count"
                            Grid="true"
                            Title="<%$ Resources:EmployeeCount %>"
                            Minimum="0">
                            <Renderer Handler="return Ext.util.Format.number(label, '0,0');" />
                        </ext:NumericAxis>

                        <ext:CategoryAxis Position="Bottom" Fields="id" Title="<%$ Resources:FieldMonth %>">
                            <Label RotationDegrees="-45" />
                        </ext:CategoryAxis>
                    </Axes>
                    <Series>
                        <ext:BarSeries
                            Highlight="true"
                            XField="id"
                            YField="count">
                            <Tooltip runat="server" TrackMouse="true">
                                <Renderer Handler="toolTip.setTitle(record.get('id') + ': ' + record.get('count'));" />
                            </Tooltip>
                            <Label
                                Display="InsideEnd"
                                Field="count"
                                Orientation="Horizontal"
                                Color="#333"
                                TextAlign="Center"
                                RotationDegrees="45">
                                <Renderer Handler="return Ext.util.Format.number(text, '0');" />
                            </Label>
                        </ext:BarSeries>
                    </Series>
                </ext:CartesianChart>
                          
                            <ext:CartesianChart
                    ID="Chart2" StoreID="reportStore"
                    runat="server" Flex="1" Height="500"
                    FlipXY="true"
                    >
                                 

                   
                                <LegendConfig Dock="Right" runat="server" />
                    <Axes>
                        <ext:NumericAxis
                            Fields="age00_18"
                            Position="Bottom"
                            Grid="true"
                            AdjustByMajorUnit="true"
                            Minimum="0">
                            <Renderer Handler="return label;" />
                        </ext:NumericAxis>

                        <ext:CategoryAxis Fields="departmentName" Position="Left" Grid="true" />
                    </Axes>

                    <Series>
                        <ext:BarSeries
                            XField="departmentName"
                            YField="age00_18,age18_25,age26_30,age30_40,age40_50,age50_60,age60_99"
                            Titles="0-18,18-25,26-30,30-40,40-50,50-60,60-99"
                             
                            Stacked="true">
                           
                            <StyleSpec>
                                <ext:Sprite Opacity="0.8" />
                            </StyleSpec>
                            <HighlightConfig>
                                <ext:Sprite FillStyle="yellow" />
                            </HighlightConfig>
                           
                        </ext:BarSeries>
                    </Series>
                                </ext:CartesianChart>
                        
                        <ext:GridPanel ExpandToolText="expand"
                            ID="reportGrid" MarginSpec="0 17 0 0"
                            runat="server" StoreID="reportStore"
                            PaddingSpec="0 0 0 0"
                            Header="false" CollapseMode="Header" Collapsible="true" CollapseDirection="Right"
                            Title="<%$ Resources: LatenessGridTitle %>"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                
                            </Store>


                            <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column Visible="false" ID="Column2" MenuDisabled="true" runat="server"  DataIndex="recordId" Hideable="false" Width="75" />
                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment %>" DataIndex="departmentName" Hideable="false"  />
                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: age00_18%>" DataIndex="age00_18" Width="70" Hideable="false" />

                                 <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: age18_25%>" DataIndex="age18_25" Width="70" Hideable="false" />
                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: age26_30%>" DataIndex="age26_30" Width="70" Hideable="false" />
                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: age30_40%>" DataIndex="age30_40" Width="70" Hideable="false" />
                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: age40_50%>" DataIndex="age40_50" Width="70" Hideable="false" />
                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: age50_60%>" DataIndex="age50_60" Width="70" Hideable="false" />
                                    <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: age60_99%>" DataIndex="age60_99" Width="70" Hideable="false" />


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

                        </ext:Panel>
                    </Items>
                </ext:Panel>


            </Items>
        </ext:Viewport>






    </form>
</body>
</html>
