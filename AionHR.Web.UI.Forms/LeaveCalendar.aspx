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
    <script type="text/javascript" src="Scripts/LeaveCalendar.js?id=2"></script>
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
        function CalcSum() {

            var sum = 0;
            App.LeaveDaysGrid.getStore().each(function (record) {
                sum += record.data['leaveHours'];
            });

            App.sumHours.setValue(sum);
            App.sumHours2.setValue(sum);


        }

        function FillReturnInfo(id, d1, d2) {

            App.leaveId.setValue(id);
            App.DateField1.setValue(new Date(d1));
            App.DateField2.setValue(new Date(d2));
            App.returnDate.setValue(new Date(d2));
            App.Button1.setDisabled(false);
        }
        function SetReturnDateState() {
            if (App.status.value == 2)
                App.returnDate.setDisabled(false);
            else
                App.returnDate.setDisabled(true);
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
              <ext:Hidden ID="SundayText" runat="server" Text="<%$ Resources:Common , SundayText %>" />
        <ext:Hidden ID="MondayText" runat="server" Text="<%$ Resources:Common , MondayText %>" />
        <ext:Hidden ID="TuesdayText" runat="server" Text="<%$ Resources:Common , TuesdayText %>" />
        <ext:Hidden ID="WednesdayText" runat="server" Text="<%$ Resources:Common , WednesdayText %>" />
        <ext:Hidden ID="ThursdayText" runat="server" Text="<%$ Resources:Common , ThursdayText %>" />
        <ext:Hidden ID="FridayText" runat="server" Text="<%$ Resources:Common , FridayText %>" />
        <ext:Hidden ID="SaturdayText" runat="server" Text="<%$ Resources:Common , SaturdayText %>" />
         <ext:Hidden ID="CurrentLeave" runat="server" />
        <ext:Hidden ID="DateFormat" runat="server" />
        <ext:Hidden ID="approved" runat="server" />
        <ext:Hidden ID="LeaveChanged" runat="server" Text="1" EnableViewState="true" />
        <ext:Hidden ID="StoredLeaveChanged" runat="server" Text="0" EnableViewState="true" />
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
            <Listeners>
                <AfterLayout Handler="App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,#{Viewport1}.getWidth(),{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}});    " />
            </Listeners>
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
                                  <ext:Button ID="Button3" runat="server" Icon="ControlEnd">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ReturnLeave">
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
                                        <Click Handler="CheckSession(); App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,#{Viewport1}.getWidth(),{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}}); " />
                                    </Listeners>

                                </ext:Button>
                                
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel runat="server" Layout="VBoxLayout" AutoScroll="true">
                            <%--<LayoutConfig>
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
                                                <Click Handler="CheckSession(); if(parseInt(#{CurrentMonth}.value)<1) {#{currentMonth}.value = 12; #{CurrentYear}.value = parseInt(#{CurrentYear}.value) - 1; } else{ #{CurrentMonth}.value = parseInt(#{CurrentMonth}.value) -1; } Ext.net.Mask.show(); App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,#{Viewport1}.getWidth(),{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}});    #{monthLbl}.setText(getMonthByNumber(#{CurrentMonth}.value)); #{yearLbl}.setText(#{CurrentYear}.value);" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" Text=">">
                                            <Listeners>
                                                <Click Handler="CheckSession(); if(parseInt(#{CurrentMonth}.value)>11) {#{currentMonth}.value = 1; #{CurrentYear}.value = parseInt(#{CurrentYear}.value) + 1; } else{ #{CurrentMonth}.value = parseInt(#{CurrentMonth}.value) + 1; } Ext.net.Mask.show(); App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,#{Viewport1}.getWidth(),{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}});    #{monthLbl}.setText(getMonthByNumber(#{CurrentMonth}.value));#{yearLbl}.setText(#{CurrentYear}.value);" />
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
                                                    EventFontSize="11px"   CellWidth="20"
                                                    CellDuration="1440" 
                                                    OnEventClick="DayPilotScheduler1_EventClick"
                                                    OnBeforeEventRender="DayPilotScheduler1_BeforeEventRender"
                                                    EventHeight="25">
                                                </DayPilot:DayPilotScheduler>
                                            </Content>
                                            <Listeners>
                                                <AfterRender Handler="App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,#{Viewport1}.getWidth(),{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}});    " />
                                                <%--<AfterLayout  Handler="App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,#{Viewport1}.getWidth(),{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}});    " />--%>
                                            </Listeners>
                           <%--           <Listeners>
                                          <AfterLayout Handler="App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,#{Viewport1}.getWidth(),{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}});    " />
                                      </Listeners>--%>
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
             Resizable="false"
             Maximizable="false"
             Draggable="false"
            Layout="Fit">

            <Items>
              <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <DirectEvents>
                        <TabChange OnEvent="Unnamed_Event">
                            <ExtraParams>
                                <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                            </ExtraParams>
                        </TabChange>
                    </DirectEvents>

                    <%--<Listeners>
                        <TabChange Handler="CheckSession(); App.direct.Unnamed_Event();" />

                    </Listeners>--%>
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
                                <ext:DateField ID="startDate" runat="server" FieldLabel="<%$ Resources:FieldStartDate%>" Name="startDate" AllowBlank="false">
                                    <DirectEvents>
                                        <Change OnEvent="MarkLeaveChanged">
                                            <ExtraParams>
                                                <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Change>
                                    </DirectEvents>
                                    <%--          <Listeners>
                                        <Change Handler="alert(this.value);App.direct.MarkLeaveChanged(); CalcSum();" />
                                    </Listeners>--%>
                                </ext:DateField>
                                <ext:DateField ID="endDate" runat="server" FieldLabel="<%$ Resources:FieldEndDate%>" Name="endDate" AllowBlank="false">
                                    <DirectEvents>
                                        <Change OnEvent="MarkLeaveChanged">
                                            <ExtraParams>
                                                <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Change>
                                    </DirectEvents>
                                    <%--<Listeners>
                                        <Change Handler="App.direct.MarkLeaveChanged(); CalcSum(); " />
                                    </Listeners>--%>
                                </ext:DateField>
                                 <ext:ComboBox runat="server" ID="employeeId" AllowBlank="false"
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
                                     <DirectEvents>
                                        <Select OnEvent="MarkLeaveChanged">
                                            <ExtraParams>
                                                <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:ComboBox>
                                <ext:TextField runat="server" ID="sumHours" ReadOnly="true" FieldLabel="<%$ Resources:TotalText%>" />
                                <ext:TextArea ID="justification" runat="server" FieldLabel="<%$ Resources:FieldJustification%>" Name="justification" />
                                <ext:TextField ID="destination" runat="server" FieldLabel="<%$ Resources:FieldDestination%>" Name="destination" AllowBlank="false" />


                                <ext:Checkbox runat="server" Name="isPaid" InputValue="true" ID="isPaid" DataIndex="isPaid" FieldLabel="<%$ Resources:FieldIsPaid%>" />

                               


                                <ext:ComboBox runat="server" ID="ltId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId" AllowBlank="false"
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


                                <ext:ComboBox runat="server" ID="status" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false"
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
                                 <ext:FieldSet runat="server" Title="<%$ Resources:ReturnInfo%>">
                            <Items>
                                <ext:DateField runat="server" Disabled="true" Name="returnDate" ID="returnDate" FieldLabel="<%$ Resources: FieldReturnDate %>" />
                                </Items>
                                </ext:FieldSet>
                            </Items>

                        </ext:FormPanel>
                        <ext:FormPanel ID="LeaveDays" runat="server" Title="<%$ Resources: LeaveDaysWindowTitle %>">
                            <Items>
                                <ext:GridPanel
                                    ID="LeaveDaysGrid"
                                    runat="server"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    MaxHeight="350"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                        <ext:Store runat="server" ID="leaveDaysStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="leaveId" />
                                                        <ext:ModelField Name="dayId" />
                                                        <ext:ModelField Name="dow" />
                                                        <ext:ModelField Name="workingHours" />
                                                        <ext:ModelField Name="leaveHours" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>
                                            <ext:Column ID="Column4" Visible="false" DataIndex="recordId" runat="server">
                                            </ext:Column>
                                            <ext:Column ID="Column7" Visible="false" DataIndex="leaveId" runat="server">
                                            </ext:Column>
                                            <ext:Column ID="Column6" DataIndex="dayId" Text="<%$ Resources: FieldDayId%>" runat="server" Width="85">
                                                <Renderer Handler="var friendlydate = moment(record.data['dayId'], 'YYYYMMDD');  return friendlydate.format(document.getElementById('DateFormat').value);">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column ID="DateColumn1" DataIndex="dow" Text="<%$ Resources: FieldDOW%>" runat="server" Width="100">
                                                <Renderer Handler="return getDay(record.data['dow']);">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column ID="DateColumn2" DataIndex="workingHours" Text="<%$ Resources: FieldWorkingHours%>" runat="server" Flex="2">
                                            </ext:Column>
                                            <ext:WidgetColumn ID="WidgetColumn2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldLeaveHours %>" DataIndex="leaveHours" Hideable="false" Width="125" Align="Center">
                                                <Widget>

                                                    <ext:NumberField runat="server" MinValue="1" DataIndex="leaveHours">
                                                        <Listeners>
                                                            <Change Handler="var rec = this.getWidgetRecord(); if(rec.data['workingHours']<this.value){this.setValue(rec.data['workingHours']); }if(1>this.value){this.setValue(1);}  rec.set('leaveHours',this.value); rec.commit(); CalcSum(); " />
                                                            <AfterRender Handler=" this.maxValue=this.getWidgetRecord().data['workingHours'];" />
                                                        </Listeners>
                                                    </ext:NumberField>

                                                </Widget>

                                            </ext:WidgetColumn>





                                        </Columns>
                                    </ColumnModel>

                                    <DockedItems>

                                        <ext:Toolbar ID="Toolbar4" runat="server" Dock="Bottom">
                                            <Items>
                                                <ext:StatusBar ID="StatusBar2" runat="server" />
                                                <ext:ToolbarFill />

                                            </Items>
                                        </ext:Toolbar>

                                    </DockedItems>


                                    <View>
                                        <ext:GridView ID="GridView2" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                    <BottomBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:ToolbarFill runat="server" />
                                                <ext:TextField runat="server" Width="400" LabelWidth="290" PaddingSpec="0 20 0 0" ID="sumHours2" ReadOnly="true" FieldLabel="<%$ Resources:TotalText%>" />
                                            </Items>
                                        </ext:Toolbar>
                                    </BottomBar>
                                </ext:GridPanel>


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
                                <ext:Parameter Name="days" Value="Ext.encode(#{LeaveDaysGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw" />
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
                <Listeners>
                <BeforeHide Handler="App.direct.UpdateCal(#{CurrentMonth}.value,#{CurrentYear}.value,#{Viewport1}.getWidth(),{success: function (result) {  Ext.net.Mask.hide();},error: function (result) {  Ext.net.Mask.hide();}});    " />
            </Listeners>
        </ext:Window>
         <ext:Window
            ID="leaveReturnWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:LeaveEndWindowTitle %>"
            Width="400"
            Height="250"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Maximizable="false"
            Resizable="false"
            Draggable="false"
            Layout="Fit">

            <Items>
                <ext:FormPanel runat="server" ID="leaveReturnForm">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <ext:ComboBox runat="server" ID="returnedEmployee" Width="350"   LabelWidth="150" LabelAlign="Left"
                                            DisplayField="fullName"
                                            ValueField="recordId" AllowBlank="true"
                                            TypeAhead="false"
                                            HideTrigger="true" SubmitValue="true"
                                            MinChars="3" FieldLabel="<%$ Resources: FilterEmployee%>"
                                            TriggerAction="Query" ForceSelection="false">
                                            <Store>
                                                <ext:Store runat="server" ID="Store3" AutoLoad="false">
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

                                            <Listeners>
                                                <Select Handler="App.Button1.setDisabled(true); App.DateField1.clear(); App.DateField2.clear();  App.direct.FillLeave();" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Content>
                                </ext:Container>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FieldSet runat="server" Title="<%$ Resources:LeaveInfo%>">
                            <Items>
                                <ext:DateField LabelWidth="150" Width="350" ID="DateField1" runat="server" FieldLabel="<%$ Resources:FieldStartDate%>" />
                                <ext:DateField LabelWidth="150" Width="350" ID="DateField2" runat="server" FieldLabel="<%$ Resources:FieldEndDate%>" />
                            </Items>
                        </ext:FieldSet>
                        <ext:FieldSet runat="server" Title="<%$ Resources:ReturnInfo%>">
                            <Items>
                                <ext:TextField LabelWidth="150" Width="350" runat="server" Hidden="true" ID="leaveId" />
                                <ext:DateField LabelWidth="150" Width="350" ID="DateField3" AllowBlank="false" runat="server" Name="DateField3" FieldLabel="<%$ Resources:FieldReturnDate%>" />

                            </Items>

                        </ext:FieldSet>

                    </Items>
                    <Buttons>
                        <ext:Button ID="Button1" runat="server" Disabled="true" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                <Click Handler="CheckSession();  if (!#{leaveReturnForm}.getForm().isValid()) {return false;}  " />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="SaveLeaveReturn" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{leaveReturnWindow}.body}" />
                                    <ExtraParams>
                                        <ext:Parameter Name="id" Value="#{leaveId}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="values" Value="#{leaveReturnForm}.getForm().getValues()" Mode="Raw" Encode="true" />

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
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </form>
</body>
</html>
