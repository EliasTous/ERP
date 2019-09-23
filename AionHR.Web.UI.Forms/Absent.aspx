<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Absent.aspx.cs" Inherits="AionHR.Web.UI.Forms.Absent" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Absent.js?id=10" ></script>
     <script type="text/javascript" src="Scripts/ReportsCommon.js?id=10"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
     <script type="text/javascript">
  
     
     
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
         <ext:Hidden ID="vals" runat="server" />
        <ext:Hidden ID="texts" runat="server" />
        <ext:Hidden ID="labels" runat="server" />
        <ext:Hidden ID="loaderUrl" runat="server"  Text="ReportParameterBrowser.aspx?_reportName=TATV&values="/>
          <ext:Hidden ID="isSuperUser" runat="server" />
        
        <ext:Store
            ID="Store1"
            runat="server"
           RemoteSort="false"
            RemotePaging="false"
            OnReadData="Store1_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App" IsPagingStore="true">
           
            <Model>
                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                       
                        <ext:ModelField Name="employeeName"  />
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
                        <ext:ModelField Name="apStatus" />
                           <ext:ModelField Name="damageLevel" />
                    
                           <ext:ModelField Name="damageLevelString" />
                   
                            <ext:ModelField Name="dayId" />
                        <ext:ModelField Name="dayIdString" />
                           <ext:ModelField Name="justification" />
                      
                                         
                        
                    
                        
                        
                        
                        
                        


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
                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="employeeName" Flex="3" Hideable="false">
                              <%--  <Renderer Handler="return record.data['employeeName'].fullName;" />--%>
                        
                            </ext:Column>
                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="2" Hideable="true">
                                
                            </ext:Column>
                          <%--  <ext:Column ID="ColPositionName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="2" Hideable="false">
                            
                            </ext:Column>--%>
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
                            <ext:Column ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldJustification%>" DataIndex="justification" Flex="5" Hideable="false">
                                   
                             
                            
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
                                ID="Column2"  Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                      <Renderer handler="if ((record.data['timeCode']===41 || record.data['timeCode']===21) && record.data['apStatus']!=-1) { if (record.data['apStatus']==2) return editRender()+'&nbsp;&nbsp;' +attachRender()+'&nbsp;&nbsp;'+rejectRender()+'&nbsp;&nbsp;'+historeRender(); else return editRender()+'&nbsp;&nbsp;' +attachRender()+'&nbsp;&nbsp;'+historeRender();   }else { if ( record.data['apStatus']==2 && App.isSuperUser.getValue()=='true'){return rejectRender()+'&nbsp;&nbsp;'+ editRender()+'&nbsp;&nbsp;'+historeRender();  }else   return editRender()+'&nbsp;&nbsp;'+historeRender(); } " />
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

                                <ext:Parameter Name="id" Value="record.data['recordId']" Mode="Raw" />
                                  <ext:Parameter Name="duration" Value="record.data['duration']" Mode="Raw" />
                                  <ext:Parameter Name="damage" Value="record.data['damageLevel']" Mode="Raw" />
                                   <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                   <ext:Parameter Name="employeeId" Value="record.data['employeeId']" Mode="Raw" />
                                  <ext:Parameter Name="timeCode" Value="record.data['timeCode']" Mode="Raw" />
                                 <ext:Parameter Name="apStatus" Value="record.data['apStatus']" Mode="Raw" />
                                <ext:Parameter Name="justification" Value="record.data['justification']" Mode="Raw" />
                                

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
                                   <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldDamageLevel%>"  runat="server" DisplayField="value" ValueField="key"   Name="damage" ID="damage" >
                                             <Store>
                                                <ext:Store runat="server" ID="damageStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                       </ext:ComboBox>
                                  <%-- <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" FieldLabel="<%$ Resources: FieldDamageLevel%>"  IDMode="Static" SubmitValue="true" ForceSelection="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: DamageWITH_DAMAGE%>" Value="<%$ Resources:ComboBoxValues, Damage_WITH_DAMAGE%>"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: DamageWITHOUT_DAMAGE%>" Value="<%$ Resources:ComboBoxValues, Damage_WITHOUT_DAMAGE%>" ></ext:ListItem>
                                      
                                    </Items>
                                    
                                </ext:ComboBox>--%>
                                <ext:TextArea ID="justification" runat="server" FieldLabel="<%$ Resources:FieldJustification%>" Name="justification"   AllowBlank="true" />
                             
                                 
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
                                                                                <ext:ModelField Name="employeeName"  />
                                                                                <ext:ModelField Name="dayId" />
                                                                                <ext:ModelField Name="dayIdDate"  />
                                                                               <ext:ModelField Name="approverName"  />
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
                                                                  
                                                                    </ext:Column>
                                                                
                                                                    <ext:Column Visible="false" ID="Column27" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                                                                  
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
                    <Loader runat="server" Url="ReportParameterBrowser.aspx?_reportName=TATV" Mode="Frame" ID="Loader8" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <Listeners>
                         </Listeners>
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            
                </Items>
        </ext:Window>


          <ext:Window 
            ID="overrideWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:overrideWindow %>"
            Width="450"
            Height="500"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="overrideForm" DefaultButton="SaveOverrideButton"
                            runat="server"
                            Title="<%$ Resources: overrideWindow %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5">
                            <Items>
                                    <ext:TextField ID="shiftId" runat="server"  Name="shiftId"   Hidden="true"/>
                                 <ext:TextField ID="ORId" runat="server"  Name="recordId"   Hidden="true"/>
                                  <ext:TextField ID="employeeId" runat="server"  Name="employeeId"   Hidden="true"/>
                              
                              
                               

                                
                                

                               <ext:TextField ID="employeeRef" runat="server" FieldLabel="<%$ Resources:FieldEmployeeRef%>" Name="FieldEmployeeRef"   ReadOnly="true"/>
                                 <ext:TextField ID="employeeName" runat="server" FieldLabel="<%$ Resources:FieldEmployeeName%>" Name="employeeName"   ReadOnly="true"/>
                                 <ext:FieldSet Collapsible="false" runat="server" Title="<%$ Resources: FieldShift%>" >
                                 <Items>
                                 <ext:DateField ID="dtFrom" runat="server" FieldLabel="<%$ Resources:FieldDateFrom%>" Name="dtFrom" Format="dd/MM/yyyy HH:mm" ReadOnly="true"  />
                                 <ext:DateField ID="dtTo" runat="server" FieldLabel="<%$ Resources:FieldDateTo%>" Name="dtTo" Format="dd/MM/yyyy HH:mm" ReadOnly="true"  />
                                     </Items>
                                   </ext:FieldSet>


                                    <ext:TextArea ID="punchesList" runat="server" FieldLabel="<%$ Resources:Fieldpunches%>" Name="punchesList"    Scrollable="Both" ReadOnly="true" />


                                   <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldTimeCode%>"  runat="server" DisplayField="value" ValueField="key"   Name="timeCode" ID="timeCode" ReadOnly="true" >
                                             <Store>
                                                <ext:Store runat="server" ID="timeCodeStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                       </ext:ComboBox>
                          
                             <ext:ComboBox AllowBlank="false"    AnyMatch="true" CaseSensitive="false"   runat="server"  ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" >
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
                                      <DirectEvents>
                                  <Change OnEvent="FillUdId">
                                  <EventMask ShowMask="true" /> 
                                      <ExtraParams>
                                        <ext:Parameter Name="branchId" Value="this.value" Mode="Raw" />
                                          </ExtraParams>
                                  </Change>

                                 </DirectEvents>
                                 
                                </ext:ComboBox>

                                  <ext:ComboBox  AllowBlank="false"   AnyMatch="true" CaseSensitive="false"   runat="server"  ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="udId" Name="udId" FieldLabel="<%$ Resources:FieldBiometric%>" >
                                    <Store>
                                        <ext:Store runat="server" ID="udIdStore">
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

                               

                              


                                   <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldInOut%>"  runat="server" DisplayField="value" ValueField="key"   Name="inOut" ID="inOut" >
                                             <Store>
                                                <ext:Store runat="server" ID="inOutStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>

                                      <DirectEvents>
                                      <Select OnEvent="SelectedCheckType" >

                                      <EventMask ShowMask="true" /> 
                                      <ExtraParams>
                                      <ext:Parameter Name="dtFrom" Value="App.dtFrom.getValue()" Mode="Raw" />
                                            <ext:Parameter Name="dtTo" Value="App.dtTo.getValue()" Mode="Raw" />
                                           <ext:Parameter Name="inOut" Value="App.inOut.getValue()" Mode="Raw" />
                                          </ExtraParams>
                                       </Select>

                                 </DirectEvents>
                                       </ext:ComboBox>


                                  <ext:DateField ID="clockStamp" runat="server" FieldLabel="<%$ Resources:FieldClockStamp%>" Format="dd/MM/yyyy HH:mm" Name="clockStamp"  />
                                
                             
                            </Items>

                        </ext:FormPanel>
                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveOverrideButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{overrideForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveOverrideNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{overrideWindow}.body}" />
                            <ExtraParams>
                              
                                   <ext:Parameter Name="shiftId" Value="App.shiftId.getValue()" Mode="Raw" />
                                  <ext:Parameter Name="clockStamp" Value="App.clockStamp.getValue()" Mode="Raw" />
                                  <ext:Parameter Name="employeeId" Value="App.employeeId.getValue()" Mode="Raw" />
                                    <ext:Parameter Name="recordId" Value="App.ORId.getValue()" Mode="Raw" />
                              
                                <ext:Parameter Name="values" Value ="#{overrideForm}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <uc:TimeVariationHistoryControl runat="server" ID="TimeVariationHistoryControl1" />

    </form>
</body>
</html>

