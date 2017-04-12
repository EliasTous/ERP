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
                                        <ext:Model ID="Model3" runat="server" IDProperty="departmentName">
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
                        <ext:Toolbar runat="server" >
                            <Items>
                                <ext:Panel runat="server" Layout="HBoxLayout" >
                                    <Items>
                                        <ext:Panel runat="server" Layout="HBoxLayout" ID="filterSet1" Hidden="true">
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


                                                </ext:ComboBox>
                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="ComboBox2" Name="positionId" EmptyText="Division">
                                                    <Store>
                                                        <ext:Store runat="server" ID="Store1">
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


                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet2" Hidden="true">
                                            <Items>
                                                <ext:DateField runat="server"></ext:DateField>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" Layout="HBoxLayout" ID="filterSet3" Hidden="true">
                                            <Items>
                                                <ext:DateField runat="server"></ext:DateField>
                                                <ext:DateField runat="server"></ext:DateField>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet4" Hidden="true">
                                            <Items>
                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="ComboBox3" Name="branchId" EmptyText="Employment History">
                                                    <Store>
                                                        <ext:Store runat="server" ID="Store2">
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


                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet5" Hidden="true">
                                            <Items>
                                                <ext:ComboBox runat="server" ID="employeeId" Width="130" LabelAlign="Top"
                                                    DisplayField="fullName"
                                                    ValueField="recordId" AllowBlank="true"
                                                    TypeAhead="false"
                                                    HideTrigger="true" SubmitValue="true"
                                                    MinChars="3" EmptyText="Employee"
                                                    TriggerAction="Query" ForceSelection="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="EmployeeStore" AutoLoad="false">
                                                            <Model>
                                                                <ext:Model runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="fullName" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>

                                                        </ext:Store>
                                                    </Store>


                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:ToolbarFill runat="server" />
                                <ext:ToolbarFill runat="server" />
                                <ext:Panel runat="server" Layout="HBoxLayout" >
                                    <Items>
                                        <ext:Panel runat="server" Hidden="true">
                                            <Items>
                                                <ext:DateField runat="server" ID="filterSet6" />
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel runat="server" ID="filterSet7" Hidden="true">
                                            <Items>
                                                <ext:ComboBox runat="server" ID="inactivePref" Editable="false" FieldLabel="">
                                                    <Items>
                                                        <ext:ListItem Text="<%$ Resources: All %>" Value="2" />
                                                        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="0" />
                                                        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="1" />
                                                    </Items>
                                                    <Listeners>
                                                        <Change Handler="App.Store1.reload()" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet8" Hidden="true">
                                            <Items>
                                                <ext:ComboBox EmptyText="Leave Type" runat="server" Width="130" LabelAlign="Top" ValueField="recordId" DisplayField="name" ID="divisionId" Name="divisionId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                                    <Store>
                                                        <ext:Store runat="server" ID="divisionStore">
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


                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet9" Hidden="true">
                                            <Items>

                                                <ext:CheckboxGroup runat="server">
                                                    <Items>
                                                        <ext:Checkbox runat="server" FieldLabel="Add"></ext:Checkbox>
                                                        <ext:Checkbox runat="server" FieldLabel="Edit"></ext:Checkbox>
                                                        <ext:Checkbox runat="server" FieldLabel="Delete"></ext:Checkbox>
                                                    </Items>
                                                </ext:CheckboxGroup>
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel runat="server" ID="filterSet10" Hidden="true">
                                            <Items>

                                                <ext:CheckboxGroup runat="server">
                                                    <Items>
                                                        <ext:Checkbox runat="server" FieldLabel="Employee"></ext:Checkbox>
                                                        <ext:Checkbox runat="server" FieldLabel="Time Management"></ext:Checkbox>
                                                        <ext:Checkbox runat="server" FieldLabel="Company Info"></ext:Checkbox>
                                                    </Items>
                                                </ext:CheckboxGroup>
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel runat="server" ID="filterSet11" Hidden="true">
                                            <Items>
                                                <ext:ComboBox EmptyText="User" runat="server" Width="130" LabelAlign="Top" ValueField="recordId" DisplayField="name" ID="ComboBox4" Name="divisionId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                                    <Store>
                                                        <ext:Store runat="server" ID="Store3">
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


                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel runat="server" ID="filterSet12" Hidden="true">
                                            <Items>
                                                <ext:ComboBox EmptyText="Class Ref" runat="server" Width="130" LabelAlign="Top" ValueField="recordId" DisplayField="name" ID="ComboBox5" Name="divisionId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                                    <Store>
                                                        <ext:Store runat="server" ID="Store4">
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


                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                         
                                    </Items>
                                    
                                </ext:Panel>
                                <ext:ToolbarFill runat="server" />
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
                                    <ext:Column Flex="2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment %>" DataIndex="departmentName" Hideable="false" />
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
