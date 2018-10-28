<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeVariationSelfServices.aspx.cs" Inherits="AionHR.Web.UI.Forms.TimeVariationSelfServices" %>

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
  
        var LinkRender = function (val, metaData, record, rowIndex, colIndex, store, apstatusString) {

            return '<a href="#" class="LinkRender"  style="cursor:pointer;"  >' + apstatusString + '</a>';
        };
        //function startRefresh() {


        //    setInterval(RefreshAllGrids, 60000);

        //}
        //function RefreshAllGrids() {



        //    if (window.
        //        parent.App.tabPanel.getActiveTab().id == "ab") {



        //        /* Not Chained
        //        App.activeStore.reload();
        //        App.absenseStore.reload();
        //        App.latenessStore.reload();
        //        App.missingPunchesStore.reload();
        //        App.checkMontierStore.reload();
        //        App.outStore.reload();*/


        //        /*Chained*/

        //        App.Store1.reload();
        //    }
        //    else {
        //        // alert('No Refresh');
        //    }

        //}
        function dump(obj) {
            var out = '';
            for (var i in obj) {
                out += i + ": " + obj[i] + "\n";


            }
            return out;
        }
        function DurationRenderer(value) {
          
            if (value == null) {
                return "";
            } else {
                return value;
            }
        }
        function damageRenderer(value) {
            alert(App.damageStore.getById(value).data.name);
            if (App.damageStore.getById(value) != null) {
                return App.damageStore.getById(value).data.name;
            } else {
                return value;
            }
        }

    </script>


</head>
<body style="background: url(Images/bg.png) repeat;" >
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        
        <ext:Hidden ID="CurrentEmployee" runat="server"  />
        <ext:Hidden ID="format" runat="server"  />
        <ext:Hidden ID="CurrentDay" runat="server"  />
        
            
        
        <ext:Store
            ID="Store1"
            runat="server"
           RemoteSort="false"
            RemotePaging="false"
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
                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                       
                        <ext:ModelField Name="employeeName" ServerMapping="employeeName.fullName" />
                        <ext:ModelField Name="branchName" />
                           <ext:ModelField Name="positionName" />
                        <ext:ModelField Name="dayStatusString" />
                           <ext:ModelField Name="timeCodeString" />
                           <ext:ModelField Name="employeeId" />
                           <ext:ModelField Name="shiftId" />
                           <ext:ModelField Name="timeCode" />
                           <ext:ModelField Name="clockDuration" />
                           <ext:ModelField Name="clockDurationString" />
                           <ext:ModelField Name="duration" />
                            <ext:ModelField Name="durationString" />
                           <ext:ModelField Name="apStatusString" />
                           <ext:ModelField Name="damageLevel" />
                         <ext:ModelField Name="recordId" />
                           <ext:ModelField Name="damageLevelString" />
                   
                            <ext:ModelField Name="dayId" />
                        <ext:ModelField Name="dayIdString" />
                      
                                         
                        
                        
                        
                        
                        
                        
                        


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
                   
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                  
                    
                  
            
                        <DockedItems>
                        <ext:Toolbar runat="server" Dock="Top">
                            <Items>
                                   <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <%--<uc:dateRange runat="server" ID="dateRange1" />--%>
                                        <uc:dateRange runat="server" ID="dateRange1" />
                                    </Content>
                                </ext:Container>
                                <ext:Container Visible="false" runat="server" Layout="FitLayout">
                                    <Content>
                                        <%--<uc:dateRange runat="server" ID="dateRange1" />--%>
                                        <uc:employeeCombo runat="server" ID="employeeCombo1"  />
                                    </Content>
                                </ext:Container>
                                  <ext:Container Visible="false" runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" />

                                    </Content>

                                </ext:Container>
                              
                                <ext:ToolbarSeparator runat="server" />
                                 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="timeVariationType" runat="server" EmptyText="<%$ Resources:FieldTimeVariationType%>" Name="timeVariationType" IDMode="Static" SubmitValue="true" MaxWidth="120">
                                    <Items>
                                       <ext:ListItem Text="<%$ Resources:Common ,  All %>" Value="0"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  UnpaidLeaves %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_UNPAID_LEAVE %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  PaidLeaves %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_PAID_LEAVE %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  SHIFT_LEAVE_WITHOUT_EXCUSE %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_SHIFT_LEAVE_WITHOUT_EXCUSE %>" ></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  DAY_LEAVE_WITHOUT_EXCUSE  %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_DAY_LEAVE_WITHOUT_EXCUSE %>" ></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  LATE_CHECKIN %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_LATE_CHECKIN %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  DURING_SHIFT_LEAVE %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_DURING_SHIFT_LEAVE %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  EARLY_LEAVE   %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_EARLY_LEAVE   %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  EARLY_CHECKIN %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_EARLY_CHECKIN %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  OVERTIME %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_OVERTIME %>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common ,  MISSED_PUNCH %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_MISSED_PUNCH %>"></ext:ListItem>
                                         <ext:ListItem Text="<%$ Resources:Common ,  Day_Bonus %>" Value="<%$ Resources:ComboBoxValues ,  TimeVariationType_Day_Bonus %>"></ext:ListItem>
                                      
                                      
                                    </Items>
                                  
                                    
                                </ext:ComboBox>
                                     <ext:ToolbarSeparator runat="server" />
                               
                                   <ext:ComboBox AnyMatch="true" Width="80" CaseSensitive="false" runat="server" ID="apStatus" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"  Name="apStatus"
                                    EmptyText="<%$ Resources: FieldApprovalStatus %>">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: FieldAll %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="-1" />
                                        <ext:ListItem Text="<%$ Resources: FieldRejected %>" Value="2" />
                                    </Items>

                                </ext:ComboBox>
                                   <ext:ToolbarSeparator runat="server" />
                                <ext:Button runat="server" Text="<%$Resources:Go %>">
                                    <Listeners>
                                        <Click Handler="CheckSession(); App.Store1.reload();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                        <ext:Toolbar Visible="false" runat="server">
                            <Items> 
                                  
                              
                                   <ext:ComboBox Visible="false" AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="esId" Name="esId" EmptyText="<%$ Resources: Common ,FieldEHStatus%>" MaxWidth="150">
                                    <Store>
                                        <ext:Store runat="server" ID="statusStore">
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

                                    <Items>
                                        <ext:ListItem Text="<%$Resources:Common ,All %>" Value="0" />
                                    </Items>
                                </ext:ComboBox>
                                 
                                  <ext:NumberField  ID="fromDuration" runat="server" FieldLabel="<%$ Resources:fromDuration%>" Name="fromDuration" MinValue="0"   >
                                      <Listeners>
                                          <FocusLeave Handler="this.value<0 ? this.setValue(0) : this.value"></FocusLeave>
                                      </Listeners>
                                      </ext:NumberField>
                                   <ext:ToolbarSeparator runat="server" />
                                  
                                  <ext:NumberField ID="toDuration" runat="server" FieldLabel="<%$ Resources:toDuration%>" Name="toDuration"  MinValue="0" >
                                      <Listeners>
                                          <FocusLeave Handler="this.value<0 ? this.setValue(0) : this.value"></FocusLeave>
                                      </Listeners>
                                      </ext:NumberField>
                                      
                                <ext:ToolbarSeparator runat="server" />


                            </Items>
                        </ext:Toolbar>
                            </DockedItems>
                


                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>
                              <ext:Column ID="ColRecordId" MenuDisabled="true" runat="server"  DataIndex="recordId" Visible="false" />
                          <%--  <ext:Column ID="ColDay" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDay%>" DataIndex="dayId" Flex="2" Hideable="false">
                                <Renderer Handler=" var d = moment(record.data['dayId']);   return d.format(App.format.value);" />
                            </ext:Column>--%>
                              <%-- <ext:DateColumn ID="ColDayId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDay%>" DataIndex="dayIdDate" Flex="2" Hideable="false">--%>
                              
                          <%--  </ext:DateColumn>--%>
                             <ext:Column ID="ColStringDayId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDay%>" DataIndex="dayIdString" Flex="2" Hideable="false">
                              
                            </ext:Column>
                            <ext:Column ID="ColName" Visible="false" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="employeeName" Flex="3" Hideable="false">
                              <%--  <Renderer Handler="return record.data['employeeName'].fullName;" />--%>
                        
                            </ext:Column>
                            <ext:Column ID="ColBranchName" Visible="false" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="2" Hideable="true">
                                
                            </ext:Column>
                            <ext:Column ID="ColPositionName" Visible="false" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="2" Hideable="false">
                            
                            </ext:Column>
                              <ext:Column ID="ColtimeCodeString" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldltimeCode%>" DataIndex="timeCodeString" Flex="2" Hideable="false">
                            
                            </ext:Column>
                              <ext:Column ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldClockDuration%>" DataIndex="clockDurationString" Flex="2" Hideable="false" />
                            
                         <ext:Column ID="CoDuration" Visible="true" DataIndex="durationString" runat="server" Text="<%$ Resources: FieldDuration %>" Flex="1" >
                                  
                                    
                             </ext:Column>
                                <ext:Column ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldApprovalStatus%>" DataIndex="apStatusString" Flex="2" Hideable="false">
                               <Renderer Handler="return LinkRender(value, metadata, record, rowIndex,  colIndex, store,record.data['apStatusString']);" />
                            </ext:Column>
                               <ext:Column ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDamageLevel%>" DataIndex="damageLevelString" Flex="2" Hideable="false">
                                   
                             
                            
                            </ext:Column>


                          



                            <ext:Column runat="server"
                                ID="colEdit" Visible="false"
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
                              <ext:Column runat="server"
                                ID="Column2"  Visible="false"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return editRender();" />

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
                   
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                      <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>

                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                  <ext:Parameter Name="duration" Value="record.data['duration']" Mode="Raw" />
                                  <ext:Parameter Name="damage" Value="record.data['damageLevel']" Mode="Raw" />
                                   <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                   <ext:Parameter Name="employeeId" Value="record.data['employeeId']" Mode="Raw" />
                                  <ext:Parameter Name="timeCode" Value="record.data['timeCode']" Mode="Raw" />

                                  <ext:Parameter Name="shiftId" Value="record.data['shiftId']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
               
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
            Width="450"
            Height="330"
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
                              <ext:TextField ID="recordId" runat="server"  Name="recordId"  Hidden="true"/>
                              <ext:NumberField ID="duration" runat="server" FieldLabel="<%$ Resources:FieldDuration%>" Name="duration"   AllowBlank="true"/>
                                   <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="damage" runat="server" FieldLabel="<%$ Resources: FieldDamageLevel%>" Name="damage" IDMode="Static" SubmitValue="true" ForceSelection="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: DamageWITH_DAMAGE%>" Value="<%$ Resources:ComboBoxValues, Damage_WITH_DAMAGE%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: DamageWITHOUT_DAMAGE%>" Value="<%$ Resources:ComboBoxValues, Damage_WITHOUT_DAMAGE%>" ></ext:ListItem>
                                      
                                    </Items>
                                    
                                </ext:ComboBox>
                             
                                 
                                <%--<ext:TextField ID="intName" runat="server" FieldLabel="<%$ Resources:IntName%>" Name="intName"   AllowBlank="false"/>--%>
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
                                <ext:Parameter Name="values" Value ="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
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

