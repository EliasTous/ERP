﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeAttendanceView.aspx.cs" Inherits="AionHR.Web.UI.Forms.TimeAttendanceView" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/AttendanceDayView.js?id=5"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript">
        function setTotal(t) {
            // alert(t);
            // alert(document.getElementById("total"));

            document.getElementById("total").innerHTML = t;
            Ext.defer(function () {
                App.GridPanel1.view.refresh();
            }, 10);
        }

        function startRefresh() {


            setInterval(RefreshAllGrids, 60000);

        }
        function RefreshAllGrids() {



            if (window.
                parent.App.tabPanel.getActiveTab().id == "ad") {



                /* Not Chained
                App.activeStore.reload();
                App.absenseStore.reload();
                App.latenessStore.reload();
                App.missingPunchesStore.reload();
                App.checkMontierStore.reload();
                App.outStore.reload();*/


                /*Chained*/

                App.Store1.reload();
            }
            else {
                // alert('No Refresh');
            }

        }
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";


            }
            return out;
        }
    </script>


</head>
<body style="background: url(Images/bg.png) repeat;" onload="startRefresh();">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="TotalText" runat="server" Text="<%$ Resources: TotalText %>" />
        <ext:Hidden ID="HoursWorked" runat="server" Text="<%$ Resources: FieldHoursWorked %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server"  />
        <ext:Hidden ID="CurrentDay" runat="server"  />
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

                        <ext:ModelField Name="dayId" />
                        <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="employeeName" IsComplex="true" />
                        <ext:ModelField Name="branchName" />
                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="checkIn" />
                        <ext:ModelField Name="checkOut" />
                        <ext:ModelField Name="workingTime" />
                        <ext:ModelField Name="breaks" />
                        <ext:ModelField Name="OL_A" />
                        <ext:ModelField Name="OL_B" />
                        <ext:ModelField Name="OL_D" />
                        <ext:ModelField Name="OL_N" />
                        <ext:ModelField Name="OL_A_SIGN" />
                        <ext:ModelField Name="OL_B_SIGN" />
                        <ext:ModelField Name="OL_D_SIGN" />
                        <ext:ModelField Name="OL_N_SIGN" />
                        <ext:ModelField Name="netOL" />






                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>



        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:GridPanel
                    ID="GridPanel1"
                    runat="server"
                    StoreID="Store1"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false" Dock="Top">

                            <Defaults>
                                <ext:Parameter Name="width" Value="220" Mode="Raw" />
                                <ext:Parameter Name="labelWidth" Value="70" Mode="Raw" />
                            </Defaults>
                            <Items>
                                <ext:ComboBox runat="server" Width="130" LabelAlign="Top" EmptyText="<%$ Resources:FieldBranch%>" ValueField="recordId" DisplayField="name" ID="branchId" Name="branchId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
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

                                <ext:ComboBox runat="server" Width="155" EmptyText="<%$ Resources:FieldDepartment%>" LabelAlign="Top" ValueField="recordId" DisplayField="name" ID="departmentId" Name="departmentId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
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
                                <ext:ComboBox EmptyText="<%$ Resources: FieldDivision%>" runat="server" Width="130" LabelAlign="Top" ValueField="recordId" DisplayField="name" ID="divisionId" Name="divisionId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
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
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>

                                </ext:ComboBox>
                                <ext:ComboBox runat="server" ID="employeeId" Width="130" LabelAlign="Top"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" EmptyText="<%$ Resources: FieldEmployee%>"
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
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillEmployee"></ext:PageProxy>
                                            </Proxy>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>
                                    <Items>
                                        <ext:ListItem Text="-----All-----" Value="0" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:DateField runat="server" ID="dayId" EmptyText="<%$ Resources: FieldDate%>" Width="130" LabelAlign="Top">
                                    <Listeners>
                                        <Change Handler="#{Store1}.reload()" />
                                        <FocusLeave Handler="#{Store1}.reload()" />
                                    </Listeners>
                                </ext:DateField>
                                <ext:Button runat="server" Text="<%$ Resources: ButtonClear%>" MarginSpec="0 0 0 0" Width="100">
                                    <Listeners>
                                        <Click Handler="#{departmentId}.clear();#{dayId}.setValue(new Date()); #{branchId}.clear(); #{divisionId}.clear(); #{Store1}.reload(); #{employeeId}.clear();">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Text="<%$ Resources: ButtonRefresh%>" Width="100">
                                    <Listeners>
                                        <Click Handler="#{Store1}.reload()" />
                                    </Listeners>
                                </ext:Button>



                                <ext:Button Visible="false" ID="btnDeleteSelected" runat="server" Text="<%$ Resources:Common , DeleteAll %>" Icon="Delete">
                                    <Listeners>
                                        <Click Handler="CheckSession();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnDeleteAll">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />


                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>

                            <ext:Column ID="ColDay" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDay%>" DataIndex="dayId" Flex="2" Hideable="false">

                                <SummaryRenderer Handler="return #{TotalText}.value;" />
                            </ext:Column>
                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="employeeName.fullName" Flex="3" Hideable="false">
                                <Renderer Handler="return record.data['employeeName'].fullName;" />
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>
                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="2" Hideable="true">
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>
                            <ext:Column ID="ColDepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="2" Hideable="false">
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>

                            <ext:Column ID="ColCheckIn" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCheckIn%>" DataIndex="checkIn" Flex="2" Hideable="false">
                                <Renderer Handler=" var olA = ''; if(record.data['OL_A']=='00:00') olA=''; else olA= record.data['OL_A']; var cssClass='';if(record.data['OL_A_SIGN']<0) cssClass='color:red;'; var result = ' <div style= ' + cssClass +' > ' + record.data['checkIn'] + '<br/>' + olA + '</div>'; return result;" />
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>

                            <ext:Column ID="ColCheckOut" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCheckOut%>" DataIndex="checkOut" Flex="2" Hideable="false">
                                <Renderer Handler="var olD = ''; if(record.data['OL_D']=='00:00') olD=''; else olD= record.data['OL_D']; var cssClass='';if(record.data['OL_D_SIGN']<0) cssClass='color:red;'; var result = ' <div style= ' + cssClass +' > ' + record.data['checkOut'] + '<br/>' + olD + '</div>'; return result;" />
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>


                            <ext:Column SummaryType="None" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldHoursWorked%>" DataIndex="hoursWorked" Flex="2" Hideable="false">
                                <Renderer Handler="var olN = ''; if(record.data['OL_N']=='00:00') olN=''; else olN= record.data['OL_N']; var cssClass='';if(record.data['OL_N_SIGN']<0) cssClass='color:red;'; var result = ' <div style= ' + cssClass +' > ' + record.data['workingTime'] + '<br/>' + olN + '</div>'; return result;" />
                                <SummaryRenderer Handler="return document.getElementById('total').innerHTML+ ' ' + #{HoursWorked}.value;" />
                            </ext:Column>


                            <ext:Column ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBreaks%>" DataIndex="breaks" Flex="2" Hideable="false">
                                <Renderer Handler="var olB = ''; if(record.data['OL_B']=='00:00') olB=''; else olB= record.data['OL_B'];var cssClass='';if(record.data['OL_B_SIGN']<0) cssClass='color:red;'; var result = ' <div style= ' + cssClass +' > ' + record.data['breaks'] + '<br/>' + olB + '</div>'; return result;" />
                            </ext:Column>



                            <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text="<%$ Resources:Common, FieldDetails %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />
                                <SummaryRenderer Handler="return '&nbsp;';" />
                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colDelete" Flex="1" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="deleteRender" />
                                <SummaryRenderer Handler="return '&nbsp;';" />
                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colAttach"
                                Text="<%$ Resources:Common, Attach %>"
                                Hideable="false"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="attachRender" />
                                <SummaryRenderer Handler="return '&nbsp;';" />
                            </ext:Column>




                        </Columns>

                    </ColumnModel>

                    <BottomBar>

                        <ext:PagingToolbar ID="PagingToolbar1"
                            runat="server"
                            FirstText="<%$ Resources:Common , FirstText %>"
                            NextText="<%$ Resources:Common , NextText %>"
                            PrevText="<%$ Resources:Common , PrevText %>"
                            LastText="<%$ Resources:Common , LastText %>"
                            RefreshText="<%$ Resources:Common ,RefreshText  %>"
                            BeforePageText="<%$ Resources:Common ,BeforePageText  %>"
                            AfterPageText="<%$ Resources:Common , AfterPageText %>"
                            DisplayInfo="true"
                            DisplayMsg="<%$ Resources:Common , DisplayMsg %>"
                            Border="true"
                            EmptyMsg="<%$ Resources:Common , EmptyMsg %>">
                            <Items>
                            </Items>
                            <Listeners>
                                <BeforeRender Handler="this.items.removeAt(this.items.length - 2);" />
                            </Listeners>
                        </ext:PagingToolbar>

                    </BottomBar>

                    <View>

                        <ext:GridView ID="GridView1" runat="server">
                        </ext:GridView>


                    </View>
                    <Features>

                        <ext:Summary ID="Summary1" runat="server" DefaultValueMode="Ignore" />
                    </Features>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                <ext:Parameter Name="employeeId" Value="record.data['employeeId']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>


                </ext:GridPanel>
                <ext:Label runat="server" ID="total" />
            </Items>
        </ext:Viewport>

        <ext:Window
            ID="AttendanceShiftWindow"
            runat="server"
            Icon="PageEdit"
            Width="450"
            Title="<%$ Resources:DayShifts %>"
            MinHeight="100"
            MaxHeight="600"
            AutoShow="false"
            Modal="true"
            Maximizable="false" Resizable="false" Draggable="false" 
            Hidden="true"
            Layout="FitLayout">

            <Items>
                <ext:GridPanel runat="server"
                    ID="attendanceShiftGrid"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Flex="1"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                     <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="AddShift">
                                            <EventMask ShowMask="true" CustomTarget="={#{attendanceShiftGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Store>
                        <ext:Store runat="server" ID="attendanceShiftStore">

                            <Model>
                                <ext:Model runat="server" IDProperty="recordId">
                                    <Fields>
                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="dayId" />
                                        <ext:ModelField Name="checkIn" />
                                        <ext:ModelField Name="checkOut" />
                                        <ext:ModelField Name="duration" />


                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                             <ext:Column runat="server" DataIndex="recordId"  Visible="false" />
                            <ext:Column runat="server" DataIndex="dayId"  Visible="false" />
                            <ext:Column runat="server" DataIndex="employeeId"  Visible="false" />
                            <ext:Column runat="server" DataIndex="checkIn" Text="<%$ Resources: FieldCheckIn %>" Flex="1" />
                            <ext:Column runat="server" DataIndex="checkOut" Text="<%$ Resources: FieldCheckOut %>" Flex="1" />
                            <ext:Column runat="server" DataIndex="duration" Text="<%$ Resources: FieldHoursWorked %>" Flex="1" />
                            <ext:Column runat="server"
                                ID="Column3" Visible="true"
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
                        </Columns>

                    </ColumnModel>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPShift">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                <ext:Parameter Name="employeeId" Value="record.data['employeeId']" Mode="Raw" />
                                <ext:Parameter Name="shiftId" Value="record.data['recordId']" Mode="Raw" />
                                <ext:Parameter Name="checkedIn" Value="record.data['checkIn']" Mode="Raw" />
                                <ext:Parameter Name="checkedOut" Value="record.data['checkOut']" Mode="Raw" />
                                
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                </ext:GridPanel>

            </Items>

        </ext:Window>

        <ext:Window ID="logBodyScreen" runat="server"
            Icon="PageEdit"
            Width="450"
            Height="500"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            <Items>
                <ext:FormPanel runat="server" ID="logBodyForm" Layout="FitLayout">
                    <Items>
                        <ext:TextArea runat="server" ID="bodyText" Name="data" />
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window
            ID="EditShiftWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="450"
            Height="150"
            AutoShow="false"
            Modal="true"
            Hidden="true"
             Draggable="false"
             Maximizable="false"
            Resizable="false" Header="false"
            Layout="Fit">

            <Items>
               
                        <ext:FormPanel
                            ID="EditShiftForm" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:TextField ID="shiftDayId" runat="server" Name="shiftDayId" Hidden="true" />
                                <ext:TextField ID="shiftEmpId" runat="server" Name="shiftEmpId" Hidden="true" />
                                <ext:TextField ID="checkIn" runat="server" FieldLabel="<%$ Resources:FieldCheckIn%>" Name="checkIn" AllowBlank="false">
                                    <Plugins>
                                        <ext:InputMask Mask="99:99" />

                                    </Plugins>
                                    <Validator Handler="return validateFrom(this.getValue());" />
                                </ext:TextField>

                                <ext:TextField ID="checkOut" runat="server" FieldLabel="<%$ Resources:FieldCheckOut%>" Name="checkOut" AllowBlank="true">
                                    <Plugins>
                                        <ext:InputMask Mask="99:99" AllowInvalid="true"  />
                                    </Plugins>
                                    <Validator Handler="return validateTo(this.getValue(),this.prev().getValue());" />
                                </ext:TextField>
                            </Items>

                        </ext:FormPanel>

                  
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditShiftForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveShift" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditShiftWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="recordId" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="dayId" Value="#{shiftDayId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="EmployeeId" Value="#{shiftEmpId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditShiftForm}.getForm().getValues()" Mode="Raw" Encode="true" />
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
