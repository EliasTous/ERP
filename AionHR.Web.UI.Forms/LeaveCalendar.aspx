<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveCalendar.aspx.cs" Inherits="AionHR.Web.UI.Forms.LeaveCalendar" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/LeaveCalendar.js?id=0"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript">
        function getMonthByNumber(numb) {
            return document.getElementById("m" + numb).value;
        }
        function setTotal(t) {
            // alert(t);
            // alert(document.getElementById("total"));

            document.getElementById("total").innerHTML = t;
            Ext.defer(function () {
                App.GridPanel1.view.refresh();
            }, 10);
        }
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";


            }
            return out;
        }
    </script>

    <style type="text/css">
        #DayPilotScheduler1 {
            width: 100% !important;
            height: 100% !important;
        }





            #DayPilotScheduler1 > table {
                width: 100% !important;
                height: 100% !important;
            }


                #DayPilotScheduler1 > table td:first-child div:first-child {
                    width: 100% !important;
                }
    </style>
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="StatusPending" runat="server" Text="<%$ Resources:FieldPending %>" />
        <ext:Hidden ID="StatusApproved" runat="server" Text="<%$ Resources: FieldApproved %>" />
        <ext:Hidden ID="StatusRefused" runat="server" Text="<%$ Resources: FieldRefused %>" />
        <ext:Hidden ID="CurrentMonth" runat="server" EnableViewState="true" />
        <ext:Hidden ID="CurrentYear" runat="server" EnableViewState="true" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,January%>" ID="m1" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,February%>" ID="m2" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,March%>" ID="m3" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,April%>" ID="m4" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,May%>" ID="m5" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,June%>" ID="m6" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,July%>" ID="m7" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,August%>" ID="m8" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,September%>" ID="m9" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,October%>" ID="m10" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,November%>" ID="m11" />
        <ext:Hidden runat="server" Text="<%$ Resources:Common,December%>" ID="m12" />
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App" IsPagingStore="true">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="startDate" />
                        <ext:ModelField Name="endDate" />
                        <ext:ModelField Name="ltId" />
                        <ext:ModelField Name="status" />
                        <ext:ModelField Name="isPaid" />
                        <ext:ModelField Name="destination" />
                        <ext:ModelField Name="justification" />
                        <ext:ModelField Name="ltName" />
                        <ext:ModelField Name="employeeName" IsComplex="true" />




                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>

        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>

                <ext:Panel runat="server" Region="Center">

                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="branchId" Name="branchId" EmptyText="<%$ Resources:FieldBranch%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
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
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>

                                </ext:ComboBox>

                                <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="departmentId" Name="departmentId" EmptyText="<%$ Resources:FieldDepartment%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
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
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>

                                </ext:ComboBox>
                                <%--   <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="monthCombo" Name="month" EmptyText="<%$ Resources:FieldMonth%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                       <Items>
                                           <ext:ListItem Text="<%$ Resources:Common,January%>" Value="1" />
                                           <ext:ListItem Text="<%$ Resources:Common,February%>" Value="2" />
                                           <ext:ListItem Text="<%$ Resources:Common,March%>" Value="3" />
                                           <ext:ListItem Text="<%$ Resources:Common,April%>" Value="4" />
                                           <ext:ListItem Text="<%$ Resources:Common,May%>" Value="5" />
                                           <ext:ListItem Text="<%$ Resources:Common,June%>" Value="6" />
                                           <ext:ListItem Text="<%$ Resources:Common,July%>" Value="7" />
                                           <ext:ListItem Text="<%$ Resources:Common,August%>" Value="8" />
                                           <ext:ListItem Text="<%$ Resources:Common,September%>" Value="9" />
                                           <ext:ListItem Text="<%$ Resources:Common,October%>" Value="10" />
                                           <ext:ListItem Text="<%$ Resources:Common,November%>" Value="11" />
                                           <ext:ListItem Text="<%$ Resources:Common,December%>" Value="12" />
                                       </Items>
                                       </ext:ComboBox>
                                  <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="yearCombo" Name="year" EmptyText="<%$ Resources:FieldYear%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                       <Items>
                                            <ext:ListItem Text="2015" Value="2015" />
                                            <ext:ListItem Text="2016" Value="2016" />
                                            <ext:ListItem Text="2017" Value="2017" />
                                            <ext:ListItem Text="2018" Value="2018" />
                                            <ext:ListItem Text="2019" Value="2019" />
                                            <ext:ListItem Text="2020" Value="2020" />
                                           <ext:ListItem Text="2021" Value="2021" />
                                           
                                       </Items>
                                       </ext:ComboBox>--%>
                                <ext:ComboBox runat="server" ID="employeeFilter" Width="130" LabelAlign="Top"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" EmptyText="<%$ Resources: FilterEmployee%>"
                                    TriggerAction="Query" ForceSelection="false">
                                    <Store>
                                        <ext:Store runat="server" ID="Store2" AutoLoad="false">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="fullName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillEmployeeFilter"></ext:PageProxy>
                                            </Proxy>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>

                                </ext:ComboBox>

                                <ext:ComboBox runat="server" ID="includeOpen" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    EmptyText="<%$ Resources: FilterStatus %>">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: FieldPending %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldRefused %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: FieldAll %>" Value="3" />
                                    </Items>
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:Button ID="applyButton" runat="server" Text="<%$ Resources: ApplyFilter%>">
                                    <Listeners>
                                        <Click Handler="CheckSession(); App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value);" />
                                    </Listeners>

                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel runat="server" Layout="VBoxLayout" AutoScroll="true">
                         <%--   <LayoutConfig>
                                <ext:VBoxLayoutConfig Align="Stretch" />
                            </LayoutConfig>--%>
                            <Defaults>
                                <ext:Parameter Name="margin" Value="0 5 0 5" Mode="Value" />
                            </Defaults>
                            <TopBar>
                                <ext:Toolbar runat="server" ID="calTools" ClassicButtonStyle="true">
                                    <Items>
                                        <ext:ToolbarFill />
                                        <ext:Label runat="server" ID="monthLbl" StyleSpec="text-align:center;" />
                                        <ext:Label runat="server" ID="Label1" StyleSpec="text-align:center;" Text="- " />
                                        <ext:Label runat="server" ID="yearLbl" StyleSpec="text-align:center;" />

                                        <ext:Button runat="server" Text="<">
                                            <Listeners>
                                                <Click Handler="CheckSession(); if(parseInt(#{CurrentMonth}.value)<1) {#{currentMonth}.value = 12; #{CurrentYear}.value = parseInt(#{CurrentYear}.value) - 1; } else{ #{CurrentMonth}.value = parseInt(#{CurrentMonth}.value) -1; } Ext.net.Mask.show(); App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}});    #{monthLbl}.setText(getMonthByNumber(#{CurrentMonth}.value)); #{yearLbl}.setText(#{CurrentYear}.value);" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" Text=">">
                                            <Listeners>
                                                <Click Handler="CheckSession(); if(parseInt(#{CurrentMonth}.value)>11) {#{currentMonth}.value = 1; #{CurrentYear}.value = parseInt(#{CurrentYear}.value) + 1; } else{ #{CurrentMonth}.value = parseInt(#{CurrentMonth}.value) + 1; } Ext.net.Mask.show(); App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}});    #{monthLbl}.setText(getMonthByNumber(#{CurrentMonth}.value));#{yearLbl}.setText(#{CurrentYear}.value);" />
                                            </Listeners>
                                        </ext:Button>

                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:Panel ID="schedulerHolder" runat="server" Layout="FitLayout" Flex="1" >
                                    <Items>
                                        <ext:Container ID="cont" runat="server">
                                            <Content>

                                                <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server" 
                                                    HeaderFontSize="8pt" HeaderHeight="20" CssOnly="false"
                                                    EventClickHandling="JavaScript" Scale="Day"
                                                    EventClickJavaScript="App.direct.HandleClick({0});"
                                                    EventFontSize="11px" 
                                                    CellDuration="1440"
                                                    OnEventClick="DayPilotScheduler1_EventClick"
                                                    OnBeforeEventRender="DayPilotScheduler1_BeforeEventRender"
                                                    EventHeight="25">
                                                </DayPilot:DayPilotScheduler>
                                            </Content>
                                        </ext:Container>
                                    </Items>
                                </ext:Panel>



                            </Items>
                        </ext:Panel>
                    </Items>
                    <Content>
                    </Content>

                </ext:Panel>
            </Items>



        </ext:Viewport>



        <ext:Window
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="450"
            Height="500"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="BasicInfoTab" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:DateField ID="startDate" runat="server" FieldLabel="<%$ Resources:FieldStartDate%>" Name="startDate" AllowBlank="false" />
                                <ext:DateField ID="endDate" runat="server" FieldLabel="<%$ Resources:FieldEndDate%>" Name="endDate" AllowBlank="false" />
                                <ext:TextArea ID="justification" runat="server" FieldLabel="<%$ Resources:FieldJustification%>" Name="justification" />
                                <ext:TextField ID="destination" runat="server" FieldLabel="<%$ Resources:FieldDestination%>" Name="destination" />


                                <ext:Checkbox runat="server" Name="isPaid" InputValue="true" ID="isPaid" DataIndex="isPaid" FieldLabel="<%$ Resources:FieldIsPaid%>" />

                                <ext:ComboBox runat="server" ID="employeeId"
                                    DisplayField="fullName"
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
                                </ext:ComboBox>


                                <ext:ComboBox runat="server" ID="ltId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId"
                                    FieldLabel="<%$ Resources: FieldLtName %>">
                                    <Store>
                                        <ext:Store runat="server" ID="ltStore">
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
                                    <RightButtons>
                                        <ext:Button ID="Button9" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addLeaveType">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>


                                <ext:ComboBox runat="server" ID="status" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    FieldLabel="<%$ Resources: FieldStatus %>">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: FieldPending %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldRefused %>" Value="2" />
                                    </Items>
                                </ext:ComboBox>

                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
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
    </form>
</body>
</html>
