<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeAttendanceViewSelfServices.aspx.cs" Inherits="AionHR.Web.UI.Forms.TimeAttendanceViewSelfServices" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/AttendanceDayView.js?id=20"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript">
        function setTotal(t, b) {
            // alert(t);
            // alert(document.getElementById("total"));

            document.getElementById("total").innerHTML = t;
            document.getElementById("totalBreaks").innerHTML = b;
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
        <ext:Hidden ID="TotalBreaksText" runat="server" Text="<%$ Resources: TotalBreaks %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="CurrentDay" runat="server" />
        <ext:Hidden ID="CurrentCA" runat="server" />
        <ext:Hidden ID="CurrentSC" runat="server" />

         <ext:Hidden ID="allHF" runat="server" Text="<%$ Resources: FieldAll %>" />
          <ext:Hidden ID="pendingHF" runat="server" Text="<%$ Resources: FieldPending %>" />
          <ext:Hidden ID="approvedHF" runat="server" Text="<%$ Resources: FieldApptoved %>" />
       


        <ext:Hidden ID="format" runat="server" />
        <ext:Store
            ID="Store1"
            runat="server"
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
                        <ext:ModelField Name="dayIdString" />
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
                        <ext:ModelField Name="caName" />
                        <ext:ModelField Name="scName" />
                        <ext:ModelField Name="apStatus" />
                           <ext:ModelField Name="apStatusString" />

                          <ext:ModelField Name="duration" />

                        <ext:ModelField Name="netOL" />
                        <ext:ModelField Name="netOLString" />
                      






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

                           
                            <Items>
                              <ext:Container Visible="false" runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" EnablePosition="false" />

                                    </Content>

                                </ext:Container>
                                <ext:ComboBox Visible="false" AnyMatch="true" CaseSensitive="false" runat="server" ID="employeeId" Width="130" LabelAlign="Top"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" EmptyText="<%$ Resources: FieldEmployee%>"
                                    TriggerAction="Query" ForceSelection="true">
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
                                  
                                    <Items>
                                        <ext:ListItem Text="-----All-----" Value="0" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:DateField runat="server" ID="startDayId" EmptyText="<%$ Resources: FieldStartDate%>" Width="130" LabelAlign="Top">
                                    <Listeners>
                                        <Change Handler="#{Store1}.reload()" />
                                        <FocusLeave Handler="#{Store1}.reload()" />
                                    </Listeners>
                                </ext:DateField>
                                <ext:DateField runat="server" ID="endDayId" EmptyText="<%$ Resources: FieldEndDate%>" Width="130" LabelAlign="Top">
                                    <Listeners>
                                           <Change Handler="#{Store1}.reload()" />
                                        <FocusLeave Handler="#{Store1}.reload()" />
                                    </Listeners>
                                </ext:DateField>
                              <%--   <ext:ComboBox AnyMatch="true" Width="80" CaseSensitive="false" runat="server" ID="apStatus" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"  Name="apStatus"
                                    EmptyText="<%$ Resources: FieldStatus %>">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: FieldAll %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldPending %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldApptoved %>" Value="2" />
                                    </Items>

                                </ext:ComboBox>--%>

                                <ext:Button runat="server" Text="<%$ Resources: Common,Go%>" Width="100">
                                    <Listeners>
                                        <Click Handler="#{Store1}.reload()" />
                                    </Listeners>
                                </ext:Button>


                                 

                              <%--  <ext:Button   ID="btnDeleteSelected" runat="server" Text="<%$ Resources: DeleteAll %>" Icon="Delete">
                                    <Listeners>
                                        <Click Handler="CheckSession();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnDeleteAll">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>--%>
                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />


                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>

                            <ext:Column ID="ColDay" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDay%>" DataIndex="dayIdString" Flex="2" Hideable="false">
                                
                                <SummaryRenderer Handler="return #{TotalText}.value;" />
                            </ext:Column>
                            <ext:Column ID="ColName" Visible="false" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="employeeName" Flex="3" Hideable="false">
                                <Renderer Handler="return record.data['employeeName'].fullName;" />
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>
                            <ext:Column ID="ColBranchName" Visible="false" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="2" Hideable="true">
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>
                            <ext:Column ID="ColDepartmentName" Visible="false" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="2" Hideable="false">
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>

                            <ext:Column ID="ColCheckIn" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCheckIn%>" DataIndex="checkIn" Flex="2" Hideable="false">
                                <Renderer Handler=" var olA = ''; if(record.data['OL_A']==null) return record.data['checkIn'];if(record.data['OL_A']=='00:00') olA=''; else olA= record.data['OL_A']; var cssClass='';if(record.data['OL_A'][0]=='-') cssClass='color:red;'; var result = ' <div style= ' + cssClass +' > ' + record.data['checkIn'] + '<br/>' + olA + '</div>'; return result;" />
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>

                            <ext:Column ID="ColCheckOut" Sortable="true" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCheckOut%>" DataIndex="checkOut" Flex="2" Hideable="false">
                                <Renderer Handler="var olD = '';if(record.data['OL_D']==null) return record.data['checkOut'] ; if(record.data['OL_D']=='00:00') olD=''; else olD= record.data['OL_D']; var cssClass='';if(record.data['OL_D'][0]=='-') cssClass='color:red;'; var result = ' <div style= ' + cssClass +' > ' + record.data['checkOut'] + '<br/>' + olD + '</div>'; return result;" />
                                <SummaryRenderer Handler="return '<hr/>';" />
                            </ext:Column>
                           <%--  <ext:Column ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDuration%>" DataIndex="duration" Flex="1" Hideable="false" />--%>
                           

                           <ext:Column SummaryType="None" ID="ColworkingTime" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldHoursWorked%>" DataIndex="workingTime" Flex="2" Hideable="false">
                                <Renderer Handler="var olN = '';if(record.data['OL_N']==null) return record.data['workingTime'];  if(record.data['OL_N']=='00:00') olN=''; else olN= record.data['OL_N']; var cssClass='';if(record.data['OL_N'][0]=='-') cssClass='color:red;'; var result = ' <div style= ' + cssClass +' > ' + record.data['workingTime'] + '<br/>' + olN + '</div>'; return result;" />
                                <SummaryRenderer Handler="return document.getElementById('total').innerHTML+ ' ' + #{HoursWorked}.value;" />
                            </ext:Column>


                           <%-- <ext:Column ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBreaks%>" DataIndex="breaks" Flex="2" Hideable="false">
                                <Renderer Handler="var olB = ''; if(record.data['OL_B']=='00:00') olB=''; else olB= record.data['OL_B'];var cssClass='';if(record.data['OL_B'][0]=='-') cssClass='color:red;'; var result = ' <div style= ' + cssClass +' > ' + record.data['breaks'] + '<br/>' + olB + '</div>'; return result;" />
                                <SummaryRenderer Handler="return document.getElementById('totalBreaks').innerHTML+ ' ' + #{TotalBreaksText}.value;" />
                            </ext:Column>--%>

                             <%-- <ext:Column ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldStatus%>" DataIndex="apStatusString" Flex="1" Hideable="false">
                               
                            <Renderer Handler="return LinkRender(value, metadata, record, rowIndex,  colIndex, store,record.data['apStatusString']);" />
                            </ext:Column>--%>
                               <ext:Column ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNetOL%>" DataIndex="netOLString" Flex="1" Hideable="false" />


                            <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text="<%$ Resources:Common, FieldDetails %>"
                                Width="80"
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
                                <ext:Parameter Name="ca" Value="record.data['caName']" Mode="Raw" />
                                <ext:Parameter Name="sc" Value="record.data['scName']" Mode="Raw" />
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
                <ext:Label runat="server" ID="totalBreaks" />
            </Items>
        </ext:Viewport>

        <ext:Window
            ID="AttendanceShiftWindow"
            runat="server"
            Icon="PageEdit"
            Width="450"
            Title="<%$ Resources:DayShifts %>"
            MinHeight="300"
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
                                <ext:Button Visible="false" ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
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
                            <ext:Column runat="server" DataIndex="recordId" Visible="false" />
                            <ext:Column runat="server" DataIndex="dayId" Visible="false" />
                            <ext:Column runat="server" DataIndex="employeeId" Visible="false" />
                            <ext:Column runat="server" DataIndex="checkIn" Text="<%$ Resources: FieldCheckIn %>" Flex="1" />
                            <ext:Column runat="server" DataIndex="checkOut" Text="<%$ Resources: FieldCheckOut %>" Flex="1" />
                            <ext:Column runat="server" DataIndex="duration" Text="<%$ Resources: FieldHoursWorked %>" Flex="1" />
                            <ext:Column runat="server"
                                ID="Column3" Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />
                            </ext:Column>
                        </Columns>

                    </ColumnModel>
                   <%-- <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>--%>
                    <%--<DirectEvents>
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
                    </DirectEvents>--%>
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
            Height="200"
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
                        <ext:TextField ID="recordId"  runat="server" Name="recordId" Hidden="true" />
                        <ext:TextField ID="shiftDayId" runat="server" Name="shiftDayId" Hidden="true" />
                        <ext:TextField ID="shiftEmpId" runat="server" Name="shiftEmpId" Hidden="true" />
                        <ext:Panel runat="server" Layout="HBoxLayout" >
                            <Items>
                                <ext:TextField Width="200" runat="server" ID="ca" FieldLabel="<%$Resources:CA %>"  Readonly="true"></ext:TextField>
                                <ext:TextField Width="200" runat="server" ID="sc" FieldLabel="<%$Resources:SC %>" Readonly="true"></ext:TextField>
                            </Items>
                        </ext:Panel>

                        <ext:TextField MarginSpec="20 0 0 0" ID="checkIn" runat="server" FieldLabel="<%$ Resources:FieldCheckIn%>" Name="checkIn" AllowBlank="false" EmptyText="00:00">
                          <%--  <Plugins>
                                <ext:InputMask Mask="99:99" />

                            </Plugins>--%>
                           <Validator Handler="return validateFrom(this.getValue());" />
                         <%--   <Listeners>
                                <FocusLeave Handler="alert(this.getValue().split(':').length - 1);"></FocusLeave>
      
                            </Listeners>--%>
                        </ext:TextField>

                        <ext:TextField ID="checkOut" runat="server" FieldLabel="<%$ Resources:FieldCheckOut%>" Name="checkOut" AllowBlank="true" EmptyText="00:00">
                        <%--    <Plugins>
                                <ext:InputMask Mask="99:99" AllowInvalid="true" />
                            </Plugins>--%>
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

 <ext:Window
            ID="TimeApprovalWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources: EditWindowsTimeApproval %>"
            Width="600"
            Height="200"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Draggable="True"
            Maximizable="false"
            Resizable="false" Header="true"
            Layout="Fit">

            <Items>
              
                   
                   
    <ext:GridPanel MarginSpec="0 0 0 0"
                                                            ID="TimeGridPanel"
                                                            runat="server"
                                                            PaddingSpec="0 0 1 0"
                                                            Header="false"
                                                            Title="<%$ Resources:EditWindowsTimeApproval %>"
                                                            Layout="FitLayout"
                                                            Scroll="Vertical"
                                                           
                                                            Border="false"
                                                              ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                                          <Store>
                                                                <ext:Store PageSize="30"
                                                                    ID="TimeStore"
                                                                    runat="server" 
                                                                    RemoteSort="false"
                                                                    RemoteFilter="false">
                                                                  
                                                                    <Model>
                                                                        <ext:Model ID="Model24" runat="server" >
                                                                            <Fields>
                                                                                                                                                            
                                                                                <ext:ModelField Name="employeeId" />
                                                                                <ext:ModelField Name="employeeName" IsComplex="true" />
                                                                                <ext:ModelField Name="dayId" />
                                                                                <ext:ModelField Name="dayIdDate"  />
                                                                               <ext:ModelField Name="approverName" IsComplex="true" />
                                                                                <ext:ModelField Name="timeCode" />
                                                                                <ext:ModelField Name="timeCodeString" />
                                                                                <ext:ModelField Name="approverId" />
                                                                                <ext:ModelField Name="status" />
                                                                                <ext:ModelField Name="notes" />
                                                                                  <ext:ModelField Name="statusString" />
                                                                            

                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>

                                                                </ext:Store>
                                                         

                                                              </Store>
                                                            <ColumnModel ID="ColumnModel24" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                                <Columns>
                                                                   <ext:Column ID="ColTimedayId" DataIndex="dayId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="ColTimeEmployeeId" DataIndex="employeeId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="ColtimeCode" DataIndex="timeCode"  runat="server" Visible="false" />

                                                                     <ext:Column ID="Column5" DataIndex="approverName" Text="<%$ Resources: FieldApproverName%>" runat="server" Flex="2">
                                                                    <Renderer Handler=" return record.data['approverName'].fullName;" />
                                                                    </ext:Column>
                                                                
                                                                    <ext:Column Visible="false" ID="Column27" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                                                                    <Renderer Handler=" return record.data['employeeName'].fullName;" />
                                                                    </ext:Column>
                                                                
                                                                    <ext:DateColumn Visible="false" ID="DateColumn5" MenuDisabled="true" runat="server" Text="<%$ Resources: Date %>" DataIndex="dayIdDate" Hideable="false" Width="100" />
                          

                                                                     <ext:Column ID="Column26"  DataIndex="timeCodeString" Text="<%$ Resources: FieldTimeCode %>"  runat="server" Flex="1" />
                                                                     <ext:Column ID="Column30" DataIndex="statusString" Text="<%$ Resources: FieldStatus %>" Flex="1" runat="server" >
                                                                      
                                                                    </ext:Column>
                                                                     <ext:Column ID="Column28" DataIndex="notes" Text="<%$ Resources: FieldNotes %>" runat="server" Flex="2" />

                                                                
                                                                  



                                                                </Columns>
                                                            </ColumnModel>
                                                           

                                                            <View>
                                                                <ext:GridView ID="GridView24" runat="server" />
                                                            </View>


                                                            <SelectionModel>
                                                                <ext:RowSelectionModel ID="rowSelectionModel23" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                               
                                                            </SelectionModel>
                                                        </ext:GridPanel>
                </Items>
       <Buttons>
                
                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
     </ext:Window>

    </form>
</body>
</html>
