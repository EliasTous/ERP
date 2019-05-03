<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeAttendanceApprovals.aspx.cs" Inherits="AionHR.Web.UI.Forms.TimeAttendanceApprovals" %>

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
        <script type="text/javascript" src="Scripts/ReportsCommon.js?id=10"></script>
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
           <ext:Hidden ID="vals" runat="server" />
        <ext:Hidden ID="texts" runat="server" />
        <ext:Hidden ID="labels" runat="server" />
        <ext:Hidden ID="loaderUrl" runat="server"  Text="ReportParameterBrowser.aspx?_reportName=RT306&values="/>
        <ext:Hidden ID="CurrentEmployee" runat="server"  />
        <ext:Hidden ID="format" runat="server"  />
        <ext:Hidden ID="CurrentDay" runat="server"  />
        
            
        
     <%--   <ext:Store
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
        </ext:Store>--%>

          <ext:Store runat="server" ID="ApproverStore" AutoLoad="false">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="fullName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillApprover"></ext:PageProxy>
                                            </Proxy>

                                        </ext:Store>


        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
              
                                                        <ext:GridPanel MarginSpec="0 0 0 0"
                                                            ID="GridPanel1"
                                                            runat="server"
                                                            PaddingSpec="0 0 1 0"
                                                            Header="false"
                                                         
                                                            Layout="FitLayout"
                                                            Scroll="Vertical"
                                                            Border="false"
                                                            ColumnLines="True" IDMode="Explicit" RenderXType="True">
                  
                    
                  
            
                     
                                     <DockedItems>
                        <ext:Toolbar runat="server" Height="30" Dock="Top">

                            <Items>
                             
                             
                                
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                          <ext:Button runat="server" Text="<%$ Resources:Common, Parameters%>"> 
                                       <Listeners>
                                           <Click Handler=" App.reportsParams.show();" />
                                       </Listeners>
                                        </ext:Button>
                                         <ext:Button runat="server" Text="<%$Resources:Common, Go %>" >
                                            <Listeners>
                                                <Click Handler="App.Store1.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                       
                                    </Content>
                                </ext:Container>
                                       
                        

                            </Items>
                        </ext:Toolbar>
                           
                        <ext:Toolbar ID="labelbar" runat="server" Height="0" Dock="Top">

                            <Items>
                                 <ext:Label runat="server" ID="selectedFilters" />
                                </Items>
                            </ext:Toolbar>
                  </DockedItems>
                


                     <Store>
                                                                <ext:Store PageSize="30"
                                                                    ID="Store1"
                                                                    runat="server" OnReadData="Store1_RefreshData"
                                                                    RemoteSort="false"
                                                                    RemoteFilter="false">
                                                                    <%--<Proxy>
                                                                        <ext:PageProxy>
                                                                            <Listeners>
                                                                                <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                                                            </Listeners>
                                                                        </ext:PageProxy>
                                                                    </Proxy>--%>
                                                                    <Model>
                                                                        <ext:Model ID="Model2" runat="server" >
                                                                            <Fields>
                                                                                                                                                            
                                                                                <ext:ModelField Name="employeeId" />
                                                                                <ext:ModelField Name="employeeName" IsComplex="true" />
                                                                                <ext:ModelField Name="dayId" />
                                                                                <ext:ModelField Name="dayIdDate"  />
                                                                                  <ext:ModelField Name="dayIdString"  />
                                                                           
                                                                                  <ext:ModelField Name="approverName" IsComplex="true" />
                                                                           
                                                                                <ext:ModelField Name="timeCode" />
                                                                                  <ext:ModelField Name="shiftId" />
                                                                                <ext:ModelField Name="timeCodeString" />
                                                                                <ext:ModelField Name="approverId" />
                                                                                <ext:ModelField Name="status" />
                                                                                 <ext:ModelField Name="statusString" />
                                                                                <ext:ModelField Name="notes" />
                                                                                 <ext:ModelField Name="damageLevel" />
                                                                                 <ext:ModelField Name="duration" />
                                                                                <ext:ModelField Name="clockDuration" />
                                                                            
                                                                                
                                                                                

                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>

                                                                </ext:Store>
                                                            </Store>


                                                            <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                                <Columns>
                                                                   <ext:Column ID="Column1" DataIndex="dayId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="ColshiftId" DataIndex="shiftId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="Column2" DataIndex="employeeId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="Column3" DataIndex="timeCode"  runat="server" Visible="false" />

                                                                      <ext:Column ID="Column9" DataIndex="approverName" Text="<%$ Resources: FieldApproverName%>" runat="server" Flex="2">
                                                                    <Renderer Handler=" return record.data['approverName'].fullName;" />
                                                                    </ext:Column>

                                                                    <ext:Column ID="Column4" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                                                                    <Renderer Handler=" return record.data['employeeName'].fullName;" />
                                                                    </ext:Column>
                                                                
                                                                    <ext:Column ID="DateColumn2" MenuDisabled="true" runat="server" Text="<%$ Resources: Date %>" DataIndex="dayIdString" Hideable="false" Flex="2" />
                          

                                                                     <ext:Column ID="Column6" DataIndex="timeCodeString" Text="<%$ Resources: FieldTimeCode %>"  runat="server" Flex="1" />
                                                                    

                                                                    
                                                                     <ext:Column ID="Column11" DataIndex="duration" Text="<%$ Resources: FieldDuration %>" Flex="1" runat="server"  />
                                                                          <ext:Column ID="Column12" DataIndex="clockDuration" Text="<%$ Resources: FieldClockDuration %>" Flex="2" runat="server"  />

                                                                          <ext:Column ID="Column10" DataIndex="damageLevel" Text="<%$ Resources: FieldDamageLevel %>"  runat="server" Flex="1" />
                                                                  
                                                                     <ext:Column ID="Column7" DataIndex="statusString" Text="<%$ Resources: FieldStatus %>" Flex="1" runat="server" />


                                                                     <ext:Column ID="Column8" DataIndex="notes" Text="<%$ Resources: FieldNotes %>" runat="server" Flex="2" />

                                                                
                                                                    <ext:Column runat="server"
                                                                        ID="Column29" Visible="false"
                                                                        Text=""
                                                                        Width="100"
                                                                        Hideable="false"
                                                                        Align="Center"
                                                                        Fixed="true"
                                                                        Filterable="false"
                                                                        MenuDisabled="true"
                                                                        Resizable="false">

                                                                        <Renderer Handler="return  appendRender(); " />
                                                                    </ext:Column>



                                                                </Columns>
                                                            </ColumnModel>
                                                         <%--   <Listeners>
                                                                <Render Handler="this.on('cellclick', cellClick);" />
                                                                <Activate Handler="#{TimeStore}.reload();" />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                
                                                                <CellClick OnEvent="TimePoPUP">
                                                                    <EventMask ShowMask="true" />
                                                                      <ExtraParams>
                                                                         <ext:Parameter Name="employeeName" Value="record.data['employeeName'].fullName" Mode="Raw" />
                                                                         <ext:Parameter Name="employeeId" Value="record.data['employeeId']" Mode="Raw" />
                                                                         <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                                                           <ext:Parameter Name="dayIdDate" Value="record.data['dayIdDate']" Mode="Raw" />
                                                                            <ext:Parameter Name="timeCode" Value="record.data['timeCode']" Mode="Raw" />
                                                                          <ext:Parameter Name="timeCodeString" Value="record.data['timeCodeString']" Mode="Raw" />
                                                                         <ext:Parameter Name="notes" Value="record.data['notes']" Mode="Raw" />
                                                                           <ext:Parameter Name="status" Value="record.data['status']" Mode="Raw" />
                                                                             <ext:Parameter Name="shiftId" Value="record.data['shiftId']" Mode="Raw" />
                                                                    
                                                                      
                                                                        
                                                                         
                                                                    </ExtraParams>

                                                             

                                                                </CellClick>
                                                            </DirectEvents>--%>


                                                        


                                                        

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
           <ext:Window runat="server"  Icon="PageEdit"
            ID="reportsParams"
            Width="600"
            Height="500"
            Title="<%$Resources:Common,Parameters %>"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="FitLayout" Resizable="true">
            <Listeners>
                <Show Handler="App.Panel8.loader.load();"></Show>
            </Listeners>
            <Items>
                <ext:Panel runat="server" Layout="FitLayout"  ID="Panel8" DefaultAnchor="100%">
                    <Loader runat="server" Url="ReportParameterBrowser.aspx?_reportName=RT306" Mode="Frame" ID="Loader8" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <Listeners>
                         </Listeners>
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            
                </Items>
        </ext:Window>

    </form>
</body>
</html>

