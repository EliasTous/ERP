<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeAutoApprovedVariations.aspx.cs" Inherits="Web.UI.Forms.ChangeAutoApprovedVariations" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Absent.js?id=20" ></script>
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
                         <ext:ModelField Name="activityId" />
                        


                    </Fields>
                </ext:Model>
            </Model>
            <%--<Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>--%>
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
                        <%--<ext:Toolbar runat="server" Height="30" Dock="Top">--%>
                            <%--<ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">

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

                                         <ext:ToolbarSeparator />

                                                 
                                     <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="cmbEmployeeImport" Width="260" LabelAlign="Left" FieldLabel="<%$ Resources: Branch %>"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" 
                                    TriggerAction="Query" ForceSelection="true">
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
                                            
                                        </ext:Store>
                                    </Store>
                                </ext:ComboBox>

                                       
                                    </Content>
                                </ext:Container>

                                
                            </Items>
                        </ext:Toolbar>--%>

                             <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>

                                   <ext:Button runat="server" Text="<%$ Resources:Common, Parameters%>"> 
                                       <Listeners>
                                           <Click Handler=" App.reportsParams.show();" />
                                       </Listeners>
                                        </ext:Button>
                                  <ext:Button  runat="server" Text="<%$ Resources: Common, Go %>">
                                        <Listeners>
                                             <Click Handler="App.Store1.reload();" />
                                        </Listeners>
                                </ext:Button>

                                                                  
                               
                    
                              
                             
                                <ext:ToolbarSeparator />

                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" Width="120" ForceSelection="true" TypeAhead="true" 
                                    MinChars="1" ValueField="recordId" DisplayField="name" ID="ApprovalStatusId"  EmptyText="<%$ Resources: FieldApprovalStatus %>">
                                    <Store>
                                        <ext:Store runat="server" ID="ApprovalStatusStore">
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

                                <ext:ToolbarSeparator />

                                
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" Width="120" ForceSelection="true" TypeAhead="true" 
                                    MinChars="1" ValueField="recordId" DisplayField="name" ID="DamageLevelId"  EmptyText="<%$ Resources: FieldDamageLevel %>">
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

                                 <ext:ToolbarSeparator />

                                
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" Width="120" ForceSelection="true" TypeAhead="true" 
                                    MinChars="1" ValueField="recordId" DisplayField="name" ID="arId"  EmptyText="<%$ Resources: FieldApprReas %>">
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

                                <ext:ToolbarSeparator />
                               
                                  <ext:TextField ID="justification" runat="server" FieldLabel="<%$ Resources:FieldJustification%>" Name="justification"   AllowBlank="true" />
                                  
                                <ext:ToolbarSeparator />

                                 <ext:Button MarginSpec="0 0 0 0" ID="btnApply" runat="server" Text="<%$ Resources: Apply %>">
                                    <DirectEvents>
                                        <Click OnEvent="Apply_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>

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
                      <%--<DirectEvents>
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
                    </DirectEvents>--%>
               
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>


                </ext:GridPanel>
          
            </Items>
        </ext:Viewport>

        
 

       

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


        





        <%--<uc:TimeVariationHistoryControl runat="server" ID="TimeVariationHistoryControl1" />--%>

    </form>
</body>
</html>


