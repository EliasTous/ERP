<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveReplacementApprovals.aspx.cs" Inherits="Web.UI.Forms.LeaveReplacementApprovals" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/LeaveRequests.js?id=9"></script>
    <script type="text/javascript" src="Scripts/common.js?id=130"></script>
    <script type="text/javascript" src="Scripts/moment.js?id=20"></script>
    <script type="text/javascript">
function calcEndDate() {
      
        if (App.startDate.getValue() == null) {
            //alert(App.SpecifyStartDateFirst.value);
            App.calDays.setValue('');
            return;
        }
        var d;
        if (App.calDays.value == 1)
            d = moment(App.startDate.getValue());
        else
            d = moment(App.startDate.getValue()).add(parseInt(App.calDays.value) - 1, 'days');
        //App.endDate.setValue(new Date(d.toDate()));
    }
    function calcDays() {
        
        if (App.startDate.getValue() == '' && App.endDate.getValue() == '') {
            return;
        }
        if (App.startDate.getValue() == '') {
            //alert('specify start date');
            return;
        }
        App.calDays.setValue(parseInt(moment(App.endDate.getValue()).diff(moment(App.startDate.getValue()), 'days')+1));
    }
        //function CalcSum() {

        //    var sum = 0;
        //    App.LeaveDaysGrid.getStore().each(function (record) {
        //        sum += record.data['leaveHours'];
        //    });

        //    App.sumHours.setValue(sum.toFixed(2));
        //    App.sumHours2.setValue(sum.toFixed(2));


        //}

        //function FillReturnInfo(id, d1, d2) {

        //    App.leaveId.setValue(id);
        //    App.DateField1.setValue(new Date(d1));
        //    App.DateField2.setValue(new Date(d2));
        //    App.returnDate.setValue(new Date(d2));
        //    App.Button1.setDisabled(false);
        //}
        //function SetReturnDateState() {
        //    if (App.status.value == 2)
        //        App.returnDate.setDisabled(false);
        //    else
        //        App.returnDate.setDisabled(true);
        //}
     
      

        
    </script>
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
        <ext:Hidden ID="StatusUsed" runat="server" Text="<%$ Resources: FieldUsed %>" />
            <ext:Hidden ID="CurrentLeaveId" runat="server"  />
      
        
        
<%--        <ext:Hidden ID="SundayText" runat="server" Text="<%$ Resources:Common , SundayText %>" />
        <ext:Hidden ID="MondayText" runat="server" Text="<%$ Resources:Common , MondayText %>" />
        <ext:Hidden ID="TuesdayText" runat="server" Text="<%$ Resources:Common , TuesdayText %>" />
        <ext:Hidden ID="WednesdayText" runat="server" Text="<%$ Resources:Common , WednesdayText %>" />
        <ext:Hidden ID="ThursdayText" runat="server" Text="<%$ Resources:Common , ThursdayText %>" />
        <ext:Hidden ID="FridayText" runat="server" Text="<%$ Resources:Common , FridayText %>" />
        <ext:Hidden ID="SaturdayText" runat="server" Text="<%$ Resources:Common , SaturdayText %>" />--%>
       <%-- <ext:Hidden ID="CurrentLeave" runat="server" />
        <ext:Hidden ID="DateFormat" runat="server" />
        <ext:Hidden ID="approved" runat="server" />--%>
       <%-- <ext:Hidden ID="LeaveChanged" runat="server" Text="1" EnableViewState="true" />
        <ext:Hidden ID="TotalText" runat="server" Text="<%$ Resources: TotalText %>" />
        <ext:Hidden ID="StoredLeaveChanged" runat="server" Text="0" EnableViewState="true" />--%>
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="leaveRef" />
                        <ext:ModelField Name="startDate" />
                        <ext:ModelField Name="endDate" />
                        <ext:ModelField Name="returnDate" />
                        <ext:ModelField Name="ltId" />
                        <ext:ModelField Name="status" />
                          <ext:ModelField Name="statusString" />
                        <ext:ModelField Name="isPaid" />
                        <ext:ModelField Name="destination" />
                        <ext:ModelField Name="justification" />
                        <ext:ModelField Name="ltName" />
                        <ext:ModelField Name="employeeName"    />
                         <ext:ModelField Name="replApStatus"    />

                        

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
                    ColumnLines="True" IDMode="Explicit" RenderXType="True" ForceFit="true">

                  

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column ID="Column4" DataIndex="leaveRef" Text="<%$ Resources: FieldLeaveRef%>" runat="server" Width="70" />
                            <ext:Column ID="ColName" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="6">
                             <%--   <Renderer Handler=" return  record.data['employeeName'].fullName" />--%>
                            </ext:Column>
                            <ext:DateColumn ID="Column1" DataIndex="startDate" Text="<%$ Resources: FieldStartDate%>" runat="server" Flex="2" />
                            <ext:DateColumn ID="Column2" DataIndex="endDate" Text="<%$ Resources: FieldEndDate%>" runat="server" Flex="2" />
                        <%--    <ext:DateColumn ID="DateColumn3" DataIndex="returnDate" Text="<%$ Resources: FieldReturnDate%>" runat="server" Flex="2" >
                                
                                </ext:DateColumn>--%>
                            <ext:Column runat="server" Width="70" Text="<%$ Resources: CalDays%>">
                                <Renderer Handler="return moment(record.data['endDate']).diff(moment(record.data['startDate']), 'days')+1" />
                            </ext:Column>
                           <%-- <ext:Column runat="server" Width="70" Text="<%$ Resources: LateDays%>">
                                <Renderer Handler="if(record.data['returnDate']!='') var d=moment(record.data['returnDate']).diff(moment(record.data['endDate']), 'days')+1; if(d>0) return d; else return '';" />
                            </ext:Column>--%>
                            <ext:Column ID="Column3" DataIndex="statusString" Text="<%$ Resources: FieldStatus%>" runat="server" Flex="2">
                            
                            </ext:Column>

                            <ext:Column ID="Column5" DataIndex="ltName" Text="<%$ Resources: FieldLtName%>" runat="server" Flex="2" />

                         <%--   <ext:CheckColumn runat="server" Flex="1" Text="<%$ Resources: FieldIsPaid %>" DataIndex="isPaid"></ext:CheckColumn>--%>


                            <ext:Column runat="server"
                                ID="colEdit" Visible="false"
                                Text=" "
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

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
                            </ext:Column>
                             <ext:Column runat="server"
                                ID="colDelete" Visible="true"
                                Text=" "
                                MinWidth="80"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return editRender(); " />

                            </ext:Column>



                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar2" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar1" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
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
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                   <ext:Parameter Name="replApStatus" Value="record.data['replApStatus']" Mode="Raw" />

                                
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>

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
           
            <%--<Listeners>
                        <TabChange Handler="CheckSession(); App.direct.Unnamed_Event();" />
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
                                   

                                </ext:Store>

                            </Store>
                         <%--   <DirectEvents>
                                <Select OnEvent="MarkLeaveChanged">
                                    <ExtraParams>
                                        <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                    </ExtraParams>
                                </Select>
                            </DirectEvents>--%>

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
                         <%--   <DirectEvents>
                                <change OnEvent="MarkLeaveChanged">
                                    <ExtraParams>
                                        <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                    </ExtraParams>
                                </change>
                                
                            </DirectEvents>--%>
                      <%--   <Listeners>
                                        <Change Handler="if(moment(this.value).isSame(moment( #{oldStart}.value) )) {return false;} #{oldStart}.value = this.value; calcDays();" />

                                    </Listeners>--%>
                            <%--          <Listeners>
                                        <Change Handler="alert(this.value);App.direct.MarkLeaveChanged(); CalcSum();" />
                                    </Listeners>--%>
                        </ext:DateField>
                          
                                <ext:DateField ID="endDate" LabelWidth="65"    runat="server" FieldLabel="<%$ Resources:FieldEndDate%>"  Name="endDate" AllowBlank="false">
                                 
                               <%--     <DirectEvents>
                                        <change OnEvent="MarkLeaveChanged">
                                            <ExtraParams>
                                                <ext:Parameter Name="startDate" Value="#{startDate}.getValue()" Mode="Raw" />
                                                <ext:Parameter Name="endDate" Value="#{endDate}.getValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </change>
                                    </DirectEvents>--%>
                                   <%-- <Listeners>
                                        <Change Handler="App.direct.MarkLeaveChanged(); CalcSum(); " />
                                    </Listeners>--%>
                             <%--       <Listeners>
                                        <Change Handler="if(moment(this.value).isSame(moment( #{oldEnd}.value) )) {return false;} #{oldEnd}.value = this.value; calcDays();calcEndDate();" />

                                    </Listeners>--%>
                                </ext:DateField>
                                <ext:NumberField runat="server" ID="calDays" Width="150" Name="calDays" MinValue="1" FieldLabel="<%$Resources:CalDays %>" LabelWidth="30">
                                 
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
                         
                          
                          
                        </ext:ComboBox>


                      <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:ApprovalStatusControl  runat="server" ID="LeaveApprovalStatusControl" FieldLabel="<%$ Resources:Common, status %>" ReadOnly="true" />
                                            </Content>
                                        </ext:Container>  


                          <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:ApprovalStatusControl  runat="server" ID="apStatus" FieldLabel="<%$ Resources: apStatus %>"  />
                                            </Content>
                                        </ext:Container>  

                        <ext:FieldSet ID="summary" runat="server" Disabled="true" Title="<%$ Resources:RecentLeaves%>" >
                            <Items>
                                <ext:TextField runat="server" ID="leaveBalance" FieldLabel="<%$Resources: LeaveBalance %>" Name="leaveBalance" />
                                <ext:TextField runat="server" ID="yearsInService" FieldLabel="<%$Resources: YearsInService %>" Name="yearsInService" />
                            </Items>
                        </ext:FieldSet>
                   <%--     <ext:FieldSet runat="server" Title="<%$ Resources:ReturnInfo%>" ID="returnFieldSet">
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
                                  <%--  <Listeners>
                                        <FocusLeave Handler="if(App.returnDate.getValue()>=App.endDate.getValue()) App.shouldDisableLastDay.value='1'; else App.shouldDisableLastDay.value='0';" />
                                    </Listeners>
                                </ext:DateField>
                                <ext:TextArea runat="server" FieldLabel="<%$Resources: ReturnNotes %>" ID="returnNotes" Name="returnNotes" />
                                <ext:TextField  Visible="false" InputType="Password" ReadOnly="true" runat="server" FieldLabel="<%$Resources: ReturnNotes %>" ID="notesField" Name="returnNotes" />
                            </Items>
                        </ext:FieldSet>--%>
                        
                    </Items>

                </ext:FormPanel>
               

            </Items>
        </ext:TabPanel>
    </Items>
    <BottomBar>
        <ext:Toolbar runat="server" ClassicButtonStyle="true">
            <Items>
                
             
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
     
            </Items>
        </ext:Toolbar>
    </BottomBar>
    <Buttons>
        
       
        
    </Buttons>
          </ext:Window>
    
    </form>
</body>
</html>
