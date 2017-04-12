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
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Panel runat="server" Layout="HBoxLayout">
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
                                                <ext:DateField ID="dateFrom" runat="server" EmptyText="<%$ Resources: DateFrom %>"></ext:DateField>
                                                <ext:DateField ID="dateTo" runat="server" EmptyText="<%$ Resources: DateTo %>"></ext:DateField>
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
                                <ext:Panel runat="server" Layout="HBoxLayout">
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
                                        <ext:Button runat="server" Text="Go" >
                                            <Listeners>
                                                <Click Handler="App.firstStore.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>

                                </ext:Panel>
                                <ext:ToolbarFill runat="server" />
                                <ext:ToolbarFill runat="server" />
                                <ext:Button Icon="PageGear" runat="server">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , Print %>" Icon="Printer">
                                                    <Listeners>
                                                        <Click Handler="PrintElem('Center');" />
                                                    </Listeners>
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , ExportAsPdf %>" Icon="DiskDownload" />
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , EmailReport %>" Icon="EmailAttach" />
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>
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
