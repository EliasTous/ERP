<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaveRequestControl.ascx.cs" Inherits="AionHR.Web.UI.Forms.Controls.LeaveRequestControl" %>
<link rel="stylesheet" type="text/css" href="CSS/Common.css" />
<link rel="stylesheet" href="CSS/LiveSearch.css" />

<script type="text/javascript" src="Scripts/common.js?id=110"></script>
<script type="text/javascript" src="Scripts/moment.js?id=2"></script>
<script type="text/javascript" src="Scripts/LeaveRequests2.js?id=1"></script>
<script type="text/javascript" src="Scripts/Branches.js?id=13"></script>
<style type="text/css">
    .print-button{
        padding:3px;
    }
    .x-grid-row .x-grid-cell-inner {
    white-space: normal;
    border-bottom: none
}
.x-grid-row-over .x-grid-cell-inner {
    white-space: normal;
    border-bottom: none
}
</style>
<script type="text/javascript">


    var editRender = function () {
        return '<img class="imgEdit" style="cursor:pointer;" src="Images/Tools/edit.png" />';
    };

    var deleteRender = function () {
        return '<img class="imgDelete"  style="cursor:pointer;" src="Images/Tools/delete.png" />';
    };
    var attachRender = function () {
        return '<img class="imgAttach"  style="cursor:pointer;" src="Images/Tools/attach.png" />';
    };





    var commandName;
    var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {

        CheckSession();


        var t = e.getTarget(),
                columnId = this.columns[columnIndex].id; // Get column id

        if (t.className == "imgEdit") {
            //the ajax call is allowed
            commandName = t.className;
            return true;
        }

        if (t.className == "imgDelete") {
            //the ajax call is allowed
            commandName = t.className;
            return true;
        }
        if (t.className == "imgAttach") {
            //the ajax call is allowed
            commandName = t.className;
            return true;
        }
        if (columnId == "ColName")
            return true;


        //forbidden
        return false;
    };


    var getCellType = function (grid, rowIndex, cellIndex) {

        //var columnId = grid.columns[cellIndex].id; // Get column id
        //return columnId;

        if (cellIndex == 0)
            return "";
        if (commandName != "")
            return commandName;
        var columnId = grid.columns[cellIndex].id; // Get column id

        return columnId;
    };


    function dump(obj) {
        var out = '';
        for (var i in obj) {
            out += i + ": " + obj[i] + "\n";
        }
        return out;
    }
    function EnableLast() {
    }
    function openInNewTab() {
        
        window.document.forms[0].target = '_blank';
        setTimeout(function () { window.document.forms[0].target = ''; }, 0);
    }
    function CalcSum() {
     
        var sum = 0;
        var sumTotalWorkingHours = 0; 
        App.leaveRequest1_LeaveDaysGrid.getStore().each(function (record) {
            sumTotalWorkingHours += record.data['workingHours'];
            sum += record.data['leaveHours'];
        });
        //App.leaveRequest1_leavePeriod.setValue(sum.toFixed(2));
      //  App.leaveRequest1_leaveHours.setValue(sum);
      //  App.leaveRequest1_workingHours.setValue(sumTotalWorkingHours);
        //if (sum == 0 || sumTotalWorkingHours == 0)
        //    App.leaveRequest1_leaveDaysField.setValue(0);
        //else
        //App.leaveRequest1_leaveDaysField.setValue(App.leaveRequest1_LeaveDaysGrid.getStore().getCount() * (sum / sumTotalWorkingHours));
        
        

        
        App.leaveRequest1_sumWorkingHours.setValue(sumTotalWorkingHours);
        App.leaveRequest1_sumHours2.setValue(sum);
    }
    function getDay(dow) {
        switch (dow) {
            case 1: return document.getElementById('leaveRequest1_MondayText').value;
            case 2: return document.getElementById('leaveRequest1_TuesdayText').value;
            case 3: return document.getElementById('leaveRequest1_WednesdayText').value;
            case 4: return document.getElementById('leaveRequest1_ThursdayText').value;
            case 5: return document.getElementById('leaveRequest1_FridayText').value;
            case 6: return document.getElementById('leaveRequest1_SaturdayText').value;
            case 0: return document.getElementById('leaveRequest1_SundayText').value;
        }
    }
    function FillReturnInfo(id, d1, d2) {
        App.leaveRequest1_leaveId.setValue(id);
        App.leaveRequest1_DateField1.setValue(new Date(d1));
        App.leaveRequest1_DateField2.setValue(new Date(d2));
        App.leaveRequest1_returnDate.setValue(new Date(d2));
        App.leaveRequest1_Button1.setDisabled(false);
    }
    function SetReturnDateState() {
        if (App.leaveRequest1_status.value == 2) {
            App.leaveRequest1_returnDate.setDisabled(false);
            App.leaveRequest1_returnNotes.setDisabled(false);
        }
        else {
            App.leaveRequest1_returnDate.setDisabled(true);
            App.leaveRequest1_returnNotes.setDisabled(true);
        }
    }
    var s = function () { };
    function calcEndDate() {
        
        if (App.leaveRequest1_startDate.getValue() == null) {
            //alert(App.leaveRequest1_SpecifyStartDateFirst.value);
            App.leaveRequest1_calDays.setValue('');
            return;
        }
        var d;
        if (App.leaveRequest1_calDays.value == 1)
            d = moment(App.leaveRequest1_startDate.getValue());
        else
            d = moment(App.leaveRequest1_startDate.getValue()).add(parseInt(App.leaveRequest1_calDays.value) - 1, 'days');
        //App.leaveRequest1_endDate.setValue(new Date(d.toDate()));
    }
    function calcDays() {
        
        if (App.leaveRequest1_startDate.getValue() == '' && App.leaveRequest1_endDate.getValue() == '') {
            return;
        }
        if (App.leaveRequest1_startDate.getValue() == '') {
            //alert('specify start date');
            return;
        }
        App.leaveRequest1_calDays.setValue(parseInt(moment(App.leaveRequest1_endDate.getValue()).diff(moment(App.leaveRequest1_startDate.getValue()), 'days')+1));
    }
    function onHourFocusLeave(context)
    {
        if (context == null || context.column == null) return false; var rec = context.column.record;
        if (parseInt(rec.data['workingHours']) < parseInt(context.value))
        {
            

            
            context.setValue(rec.data['workingHours']);
        }
        //if (1 > context.value) {
        //    alert(rec.data['workingHours']);
        //    context.setValue(1);
        //}
        rec.set('leaveHours', context.value); rec.commit(); CalcSum();
    }
</script>
<ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
<ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
<ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
<ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
<ext:Hidden ID="StatusPending" runat="server" Text="<%$ Resources:FieldPending %>" />
<ext:Hidden ID="StatusApproved" runat="server" Text="<%$ Resources: FieldApproved %>" />
<ext:Hidden ID="StatusRefused" runat="server" Text="<%$ Resources: FieldRefused %>" />
<ext:Hidden ID="StatusUsed" runat="server" Text="<%$ Resources: FieldUsed %>" />
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
<ext:Hidden ID="shouldDisableLastDay" runat="server" />
<ext:Hidden ID="GridDisabled" runat="server" />
<ext:Hidden ID="LeaveChanged" runat="server" Text="1" EnableViewState="true" />
<ext:Hidden ID="TotalText" runat="server" Text="<%$ Resources: TotalText %>" />
<ext:Hidden ID="SpecifyStartDateFirst" runat="server" Text="<%$ Resources: SpecifyStartDateFirst %>" />
<ext:Hidden ID="StoredLeaveChanged" runat="server" Text="0" EnableViewState="true" />
<ext:Hidden ID="ViewOnly" runat="server" />
  <ext:Hidden ID="oldStart" runat="server"  />
 <ext:Hidden ID="oldEnd" runat="server"  />
 <ext:Hidden ID="endDateHidden" runat="server" text="" />
<ext:Hidden ID="startDateHidden" runat="server" text=""  />
  <ext:Store runat="server" ID="ApprovalsStore" OnReadData="ApprovalsStore_ReadData">
                                    <Model>
                                        <ext:Model runat="server">
                                            <Fields>
                                                <ext:ModelField Name="employeeName"  />
                                                   <ext:ModelField Name="approverName"  />
                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="stringStatus" />
                                                <ext:ModelField Name="notes" />
                                                 <ext:ModelField Name="leaveId" />
                                                   <ext:ModelField Name="arName" />
                                                
                                                
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
<ext:Window
    ID="EditRecordWindow"
    runat="server"
    Icon="PageEdit"
    Title="<%$ Resources:EditWindowsTitle %>"
    Width="700"
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
                        <TabChange Handler="CheckSession(); App.leaveRequest1_direct.Unnamed_Event();" />
                    </Listeners>--%>
            <Items>
                <ext:FormPanel
                    ID="BasicInfoTab" DefaultButton="SaveButton"
                    runat="server"
                    Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                    Icon="ApplicationSideList"
                    AutoScroll="true"
                    DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                    BodyPadding="5">
                    <Items>
                        <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                        <ext:TextField ID="leaveRef" runat="server" Name="leaveRef"  FieldLabel="<%$ Resources:FieldLeaveRef%>" >
                               <Validator Handler="return !isNaN(this.value);" />
                            </ext:TextField>
                           <ext:ComboBox    AnyMatch="true" CaseSensitive="false"  runat="server" ID="employeeId" AllowBlank="false"
                            DisplayField="fullName" Name="employeeId"
                            ValueField="recordId"
                            TypeAhead="false"
                            FieldLabel="<%$ Resources: FieldEmployeeName%>"
                            HideTrigger="true" SubmitValue="true"
                            MinChars="3"
                            TriggerAction="Query" ForceSelection="true">
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

                          <ext:ComboBox    AnyMatch="true" CaseSensitive="false"  runat="server" ID="replacementIdCB" AllowBlank="true"
                            DisplayField="fullName" Name="replacementId"
                            ValueField="recordId"
                            TypeAhead="false"
                            FieldLabel="<%$ Resources: FieldReplacementEmployeeName%>"
                            HideTrigger="true" SubmitValue="true"
                            MinChars="3"
                            TriggerAction="Query" ForceSelection="true">
                            <Store>
                                <ext:Store runat="server" ID="replacementStore">
                                    <Model>
                                        <ext:Model runat="server">
                                            <Fields>
                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="fullName" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.FillReplacementEmployee"></ext:PageProxy>
                                    </Proxy>

                                </ext:Store>

                            </Store>
                          

                        </ext:ComboBox>
                         <ext:Panel runat="server" Layout="HBoxLayout">
                            <Items>
                           <ext:DateField ID="startDate"    runat="server" FieldLabel="<%$ Resources:FieldStartDate%>" Name="startDate" AllowBlank="false">
                            <DirectEvents>
                                <change OnEvent="MarkLeaveChanged">
                                    <ExtraParams>
                                        <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                    </ExtraParams>
                                </change>
                                
                            </DirectEvents>
                         <Listeners>
                                        <Change Handler="if(moment(this.value).isSame(moment( #{oldStart}.value) )) {return false;} #{oldStart}.value = this.value; calcDays();" />

                                    </Listeners>
                            <%--          <Listeners>
                                        <Change Handler="alert(this.value);App.leaveRequest1_direct.MarkLeaveChanged(); CalcSum();" />
                                    </Listeners>--%>
                        </ext:DateField>
                          
                                <ext:DateField ID="endDate" LabelWidth="65"    runat="server" FieldLabel="<%$ Resources:FieldEndDate%>"  Name="endDate" AllowBlank="false">
                                 
                                    <DirectEvents>
                                        <change OnEvent="MarkLeaveChanged">
                                            <ExtraParams>
                                                <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </change>
                                    </DirectEvents>
                                   <%-- <Listeners>
                                        <Change Handler="App.leaveRequest1_direct.MarkLeaveChanged(); CalcSum(); " />
                                    </Listeners>--%>
                                    <Listeners>
                                        <Change Handler="if(moment(this.value).isSame(moment( #{oldEnd}.value) )) {return false;} #{oldEnd}.value = this.value; calcDays();calcEndDate();" />

                                    </Listeners>
                                </ext:DateField>
                                <ext:NumberField runat="server" ID="calDays" Width="150" Name="calDays" MinValue="1" FieldLabel="<%$Resources:CalDays %>" LabelWidth="30">
                                  <Listeners>
                                        <Change Handler="if(this.value==null) { return false;}calcEndDate();" />

                                    </Listeners>
                                </ext:NumberField>
                            </Items>
                        </ext:Panel>
                           <ext:Panel runat="server" Layout="HBoxLayout">
                            <Items>
                                   <ext:NumberField ReadOnly="true" Visible="false"  runat="server" Width="200" ID="leaveDaysField"  Name="leaveDaysField"  FieldLabel="<%$Resources:leaveDays %>" >
                                       </ext:NumberField>
                                   <ext:NumberField ReadOnly="true" Visible="false" runat="server" Width="200" ID="leaveHours" Name="leaveHours"  FieldLabel="<%$Resources:leaveHours %>">
                                       </ext:NumberField>
                                   <ext:NumberField ReadOnly="true"  Visible="false"  runat="server" Width="200" ID="workingHours"  Name="workingHours"  FieldLabel="<%$Resources:workingHours %>" >
                                       </ext:NumberField>
                            </Items>
                        </ext:Panel>
                     
                     <%--   <ext:TextField runat="server" ID="leavePeriod" Name="leavePeriod" ReadOnly="true" FieldLabel="<%$ Resources:TotalText%>" />--%>
                        <ext:TextArea ID="justification" runat="server" FieldLabel="<%$ Resources:FieldJustification%>" Name="justification" />
                        <ext:TextField ID="destination" runat="server" FieldLabel="<%$ Resources:FieldDestination%>" Name="destination" AllowBlank="false" />


                     <%--   <ext:Checkbox runat="server" Name="isPaid" InputValue="true" ID="isPaid" DataIndex="isPaid" FieldLabel="<%$ Resources:FieldIsPaid%>" />--%>




                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="ltId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                            DisplayField="name" Name="ltId"
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
                            <DirectEvents>
                                <Change OnEvent="EnableStatus"></Change>
                            </DirectEvents>
                        </ext:ComboBox>


                      <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:ApprovalStatusControl  runat="server" ID="LeaveApprovalStatusControl" FieldLabel="<%$ Resources:Common, status %>" ReadOnly="true" />
                                            </Content>
                                        </ext:Container>  

                        <ext:FieldSet ID="summary" runat="server" Disabled="true" Title="<%$ Resources:RecentLeaves%>" >
                            <Items>
                                <ext:TextField runat="server" ID="leaveBalance" FieldLabel="<%$Resources: LeaveBalance %>" Name="leaveBalance" />
                                <ext:TextField runat="server" ID="yearsInService" FieldLabel="<%$Resources: YearsInService %>" Name="yearsInService" />
                            </Items>
                        </ext:FieldSet>
                        <ext:FieldSet runat="server" Title="<%$ Resources:ReturnInfo%>" ID="returnFieldSet">
                            <Items>
                                <ext:DateField Disabled="true" runat="server" Name="returnDate" ID="returnDate" FieldLabel="<%$ Resources: FieldReturnDate %>">
                                    <DirectEvents>
                                        <Change OnEvent="CalcReturnDate">
                                            <ExtraParams>
                                                <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="returnDate" Value="#{returnDate}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Change>
                                    </DirectEvents>
                                    <Listeners>
                                        <FocusLeave Handler="if(App.leaveRequest1_returnDate.getValue()>=App.leaveRequest1_endDate.getValue()) App.leaveRequest1_shouldDisableLastDay.value='1'; else App.leaveRequest1_shouldDisableLastDay.value='0';" />
                                    </Listeners>
                                </ext:DateField>
                                <ext:TextArea runat="server" FieldLabel="<%$Resources: ReturnNotes %>" ID="returnNotes" Name="returnNotes" />
                                <ext:TextField  Visible="false" InputType="Password" ReadOnly="true" runat="server" FieldLabel="<%$Resources: ReturnNotes %>" ID="notesField" Name="returnNotes" />
                            </Items>
                        </ext:FieldSet>
                        
                    </Items>

                </ext:FormPanel>
                <ext:FormPanel ID="LeaveDays" runat="server" OnLoad="LeaveDays_Load" Title="<%$ Resources: LeaveDaysWindowTitle %>">
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
                                                  <ext:ModelField Name="replacementName" ServerMapping="replacementName.fullName" />
                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="leaveId" />
                                                <ext:ModelField Name="dayId" />
                                                <ext:ModelField Name="dow" />
                                                <ext:ModelField Name="workingHours" />
                                                <ext:ModelField Name="leaveHours" />
                                                 <ext:ModelField Name="replacementId" />
                                               
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
                                    <ext:Column ID="Column6" DataIndex="dayId" Text="<%$ Resources: FieldDayId%>" runat="server" Width="100">

                                        <Renderer Handler="var friendlydate = moment(record.data['dayId'], 'YYYYMMDD');  return friendlydate.format(document.getElementById('leaveRequest1_DateFormat').value);">
                                        </Renderer>
                                    </ext:Column>
                                    <ext:Column ID="DateColumn1" DataIndex="dow" Text="<%$ Resources: FieldDOW%>" runat="server" Width="100">
                                        <Renderer Handler="return getDay(record.data['dow']);">
                                        </Renderer>
                                    </ext:Column>
                                    <ext:Column ID="DateColumn2" DataIndex="workingHours" Text="<%$ Resources: FieldWorkingHours%>" runat="server" Flex="2">
                                    </ext:Column>
                                    <%--          <ext:WidgetColumn  ID="WidgetColumn2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldLeaveHours %>" DataIndex="leaveHours" Hideable="false" Width="125" Align="Center">
                                        
                                        <Widget>
                                            <ext:NumberField ID="leaveHours" runat="server" MinValue="1" DataIndex="leaveHours">
                                                <Listeners>
                                                    <Change Handler="var rec = this.getWidgetRecord();  if(rec.data['workingHours']<this.value){this.setValue(rec.data['workingHours']); }if(1>this.value){this.setValue(1);}  rec.set('leaveHours',this.value); rec.commit(); CalcSum(); " />
                                                    <AfterRender Handler=" if(App.leaveRequest1_shouldDisableLastDay.value=='1') this.setDisabled(true);else this.setDisabled(false); this.maxValue=this.getWidgetRecord().data['workingHours'];" />
                                                    <AfterLayoutAnimation Handler=" this.maxValue=this.getWidgetRecord().data['workingHours'];" />
                                                    
                                                </Listeners>
                                            </ext:NumberField>
                                        </Widget>
                                        <Listeners>
                                             
                                        </Listeners>
                                        
                                    </ext:WidgetColumn>--%>
                                    <ext:ComponentColumn  Text="<%$ Resources: FieldLeaveHours %>" runat="server" DataIndex="leaveHours" ItemID="comp">
                                        <Component>
                                            <ext:NumberField ID="NumberField1" runat="server" MinValue="0" DataIndex="leaveHours">
                                                <Listeners>
                                                    <%--<Change Handler="var rec = this.column.record; rec.set('leaveHours',this.value); rec.commit(); CalcSum(); " />--%>
                                                    <%--    <AfterRender Handler="  if(App.leaveRequest1_shouldDisableLastDay.value=='1') this.setDisabled(true);else this.setDisabled(false); this.maxValue=this.getWidgetRecord().data['workingHours'];" />
                                                    <AfterLayoutAnimation Handler=" this.maxValue=this.getWidgetRecord().data['workingHours'];" />
                                                    --%>
                                                    <%--<DirtyChange Handler="var rec = this.column.record; rec.set('leaveHours',this.value); rec.commit(); CalcSum(); " />--%>
                                                    <FocusLeave Handler="onHourFocusLeave(this);" />
                                                </Listeners>
                                            </ext:NumberField>
                                        </Component>
                                        <Listeners>
                                            <Bind Handler=" if(App.leaveRequest1_shouldDisableLastDay.value=='1'){var s =App.leaveRequest1_LeaveDaysGrid.getRowsValues();  if(record.data['dayId'] == s[s.length-1].dayId) cmp.setDisabled(false); else cmp.setDisabled(true);}else {if(App.leaveRequest1_GridDisabled.value=='True') cmp.setDisabled(true); else cmp.setDisabled(false);} cmp.maxValue=record.data['workingHours']; cmp.setValue(record.data['leaveHours']);" />


                                        </Listeners>
                                    </ext:ComponentColumn>
                                      <ext:Column ID="Column1" DataIndex="replacementName" Text="<%$ Resources: FieldReplacementEmployeeName%>" runat="server" Flex="2">
                                                
                                    </ext:Column>
                                  
                                    <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="return editRender(); " />

                            </ext:Column>




                                </Columns>
                            </ColumnModel>
                         <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPLD">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                                            
                                                 
                                             
                                <ext:Parameter Name="id" Value="record.data['recordId']" Mode="Raw" />
                                 <ext:Parameter Name="leaveId" Value="record.data['leaveId']" Mode="Raw" />
                                 <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                 <ext:Parameter Name="dow" Value="record.data['dow']" Mode="Raw" />
                                 <ext:Parameter Name="workingHours" Value="record.data['workingHours']" Mode="Raw" />
                                 <ext:Parameter Name="leaveHours" Value="record.data['leaveHours']" Mode="Raw" />
                                 <ext:Parameter Name="replacementId" Value="record.data['replacementId']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                   
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
                                <ext:Toolbar runat="server" >
                                    <Items>
                                        <ext:ToolbarFill runat="server" />
                                              <ext:TextField runat="server" Width="200"  LabelWidth="150"  ID="sumWorkingHours" ReadOnly="true" FieldLabel="<%$ Resources:TotalWorkingHoursText%>" />
                                        <ext:TextField runat="server"  Width="200"   LabelWidth="100"  ID="sumHours2" ReadOnly="true" FieldLabel="<%$ Resources:TotalText%>" />
                                    
                                    </Items>
                                </ext:Toolbar>
                            </BottomBar>
                        </ext:GridPanel>


                    </Items>

                </ext:FormPanel>
                
                <ext:FormPanel ID="ApprovalsForm" runat="server" OnLoad="LeaveDays_Load" Title="<%$ Resources: Approvals %>">
                    <Listeners>
                        <Activate Handler="#{ApprovalsStore}.reload();" />
                    </Listeners>
                    <Items>
                        <ext:GridPanel
                            ID="ApprovalsGridPanel"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            MaxHeight="350"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            Icon="User"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True" RenderTo="playcontainer" StoreID="ApprovalsStore">
                            
                            <Store>
                              
                            </Store>


                            <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="leaveIdCO" Visible="false" DataIndex="leaveId" runat="server">
                                    </ext:Column>
                                      <ext:Column ID="Column2" DataIndex="approverName" Text="<%$ Resources: FieldApproverName%>" runat="server" Flex="1">
                                         
                                          
                                         </ext:Column>
                                        <ext:Column ID="ColName" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="1">
                                       
                                          
                                         </ext:Column>
                                    <ext:Column ID="departmentName" DataIndex="departmentName" Text="<%$ Resources: Department%>" runat="server" Flex="1"/>
                                    <ext:Column ID="stringStatus" Visible="true" DataIndex="stringStatus" runat="server" Width="100" text="<%$ Resources: FieldStatus%> " >
                                    </ext:Column>
                                     <ext:Column ID="Column3" Visible="true" DataIndex="arName" runat="server" Width="100" text="<%$ Resources: Common,ApprovalReason%> " >
                                    </ext:Column>
                                      
                                    <ext:Column ID="notes" DataIndex="notes" Text="<%$ Resources: ReturnNotes%>" runat="server" Flex="2">
                                       
                                    </ext:Column>
                                   



                                </Columns>
                            </ColumnModel>

                            <%--  alert(last.dayId);
                                                        if(App.leaveRequest1_shouldDisableLastDay.value=='1')
                                                             if(last.dayId==rec.data['dayId'])  
                                                                        this.setDisabled(false);
                                                            else this.setDisabled(true); 
                                                        else
                                                            this.setDisabled(true); --%>
                            <DockedItems>

                                <ext:Toolbar ID="Toolbar1" runat="server" Dock="Bottom">
                                    <Items>
                                        <ext:StatusBar ID="StatusBar1" runat="server" />
                                        <ext:ToolbarFill />

                                    </Items>
                                </ext:Toolbar>

                            </DockedItems>


                            <View>
                                <ext:GridView ID="GridView1" runat="server" />
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel2" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                         
                        </ext:GridPanel>


                    </Items>

                </ext:FormPanel>

            </Items>
        </ext:TabPanel>
    </Items>
    <BottomBar>
        <ext:Toolbar runat="server" ClassicButtonStyle="true">
            <Items>
                
                <ext:Container runat="server" Width="60" Height="25">
                    <Content>
                        <%--<asp:ImageButton runat="server"  ID="imgButton"  CausesValidation="false"  ImageUrl="~/Images/Tools/expand-all.gif" OnClientClick="openInNewTab();"   />--%>
                <asp:Button  runat="server" Width="60"   Text="<%$Resources:Print %>" CssClass="x-btn-inner x-btn-inner-default-small x-btn x-unselectable x-box-item x-toolbar-item x-btn-default-small print-button" OnClick="Button3_Click"  OnClientClick="openInNewTab();" />
            </Content>
                </ext:Container>
                <ext:ToolbarSeparator runat="server" />
                <ext:ToolbarFill></ext:ToolbarFill>
                 <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

            <Listeners>
                <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()||!#{LeaveDays}.getForm().isValid()) {return false;}  " />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                    <ExtraParams>
                        <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                         <ext:Parameter Name="status" Value="#{status}.getValue()" Mode="Raw" />
                        
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
        <ext:Button ID="Button3" runat="server" Text="Print" Hidden="true" >
            <Listeners>
                <Click Handler="openInNewTab" />
            </Listeners>
                    <DirectEvents>
                        <Click OnEvent="Button3_DirectClick"   IsUpload="true" FormID="form1" ></Click>
                    </DirectEvents>
                </ext:Button>
            </Items>
        </ext:Toolbar>
    </BottomBar>
    <Buttons>
        
       
        
    </Buttons>
    
    <DirectEvents>
        <Close OnEvent="closing" />
    </DirectEvents>
</ext:Window>

<ext:Window
    ID="leaveReturnWindow"
    runat="server"
    Icon="PageEdit"
    Title="<%$ Resources:LeaveEndWindowTitle %>"
    Width="400"
    Height="300"
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
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="returnedEmployee" Width="350" LabelWidth="150" LabelAlign="Left"
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
                                        <Select Handler="App.leaveRequest1_Button1.setDisabled(true); App.leaveRequest1_DateField1.clear(); App.leaveRequest1_DateField2.clear();  App.direct.leaveRequest1.FillLeave();" />
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
                        <ext:TextField Visible="False" ReadOnly="true" InputType="Password" LabelWidth="150" Width="350" ID="textField1" AllowBlank="true" runat="server" Name="returnNotes" FieldLabel="<%$ Resources:ReturnNotes%>" />
                        <ext:TextArea LabelWidth="150" Width="350" ID="textField2" AllowBlank="true" runat="server" Name="returnNotes" FieldLabel="<%$ Resources:ReturnNotes%>" />
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
 
<ext:Window
    ID="replacementWindow"
    runat="server"
    Icon="PageEdit"
    Title="<%$ Resources:LeaveDaysWindowTitle%>"
    Width="400"
    Height="300"
    AutoShow="false"
    Modal="true"
    Hidden="true"
    Maximizable="false"
    Resizable="false"
    Draggable="false"
    Layout="Fit">

    <Items>
        <ext:FormPanel runat="server" 
            ID="replacementForm" DefaultButton="SaveReplacementButton"
                 
                    Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                    Icon="ApplicationSideList"
                    AutoScroll="true"
                    DefaultAnchor="100%" OnLoad="replacementForm_Load"
                    BodyPadding="5">

            <Items>
              
                                             

                                
                  <ext:TextField  runat="server" Hidden="true" ID="recordIdTF"  Name="recordId" />
                  <ext:TextField runat="server" Hidden="true" ID="leaveIdTF"  Name="leaveId" />
                  <ext:TextField  runat="server" Hidden="true" ID="dayIdTF"  Name="dayId" />
                  <ext:TextField  runat="server" Hidden="true" ID="dowTF"   Name="dow"/>
                  <ext:TextField  runat="server" Hidden="true" ID="workingHoursTF"  Name="workingHours" />
                  <ext:TextField runat="server" Hidden="true" ID="leaveHoursTF"   Name="leaveHours"/>
                           <ext:ComboBox    AnyMatch="true" CaseSensitive="false"  runat="server" ID="replacementId" AllowBlank="true"
                            DisplayField="fullName" Name="replacementId"
                            ValueField="recordId"
                            TypeAhead="false"
                            FieldLabel="<%$ Resources: FieldEmployeeName%>"
                            HideTrigger="true" SubmitValue="true"
                            MinChars="3"
                            TriggerAction="Query" ForceSelection="true" StoreID="replacementStore">
                            
                          

                        </ext:ComboBox>
                     
              
                     
                    

               

            </Items>
            <Buttons>
                <ext:Button ID="SaveReplacementButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession();  if (!#{replacementForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveReplacement" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{replacementForm}.body}" />
                            <ExtraParams>
                                 <ext:Parameter Name="id" Value="#{recordIdTF}.getValue()" Mode="Raw" />
                                                
                                             

                                 <ext:Parameter Name="leaveId" Value="#{leaveIdTF}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="dayId" Value="#{dayIdTF}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="dow" Value="#{dowTF}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="workingHours" Value="#{workingHoursTF}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="leaveHours" Value="#{leaveHoursTF}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="replacementId" Value="#{replacementId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{replacementForm}.getForm().getValues()" Mode="Raw" Encode="true" />

                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button5" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
                
            </Buttons>
        </ext:FormPanel>
    </Items>
</ext:Window>

<ext:Container runat="server" Width="60" Height="25">
                    <Content>
                       <%--<asp:ImageButton runat="server"  ID="imgButton"  CausesValidation="false"  ImageUrl="~/Images/Tools/expand-all.gif" OnClientClick="openInNewTab();"   />--%>
                 <div id="myGrid">
                     <div id="playcontainer" style="margin:50px;border-bottom: none"></div>
                  </div>
            </Content>
</ext:Container>