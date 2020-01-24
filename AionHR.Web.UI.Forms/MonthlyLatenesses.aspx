<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlyLatenesses.aspx.cs" Inherits="AionHR.Web.UI.Forms.MonthlyLatenesses" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/CertificateLevels.js" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
     <script type="text/javascript" src="Scripts/ReportsCommon.js?id=11"></script>
    
    


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />

           <ext:Hidden ID="vals" runat="server" />
          <ext:Hidden ID="currentEmployee" runat="server" />
        <ext:Hidden ID="texts" runat="server" />
        <ext:Hidden ID="labels" runat="server" />
        <ext:Hidden ID="previewed" runat="server" />
        <%--<ext:Hidden ID="loaderUrl" runat="server"  Text="ReportParameterBrowser.aspx?_reportName=TAUP&values="/>--%>
        
     
         <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App">
           
            <Model>
                <ext:Model ID="Model1" runat="server">
                    <Fields>

                        <ext:ModelField Name="employeeRef" />
                          <ext:ModelField Name="employeeId" />
                        
                        <ext:ModelField Name="employeeName" />
                          <ext:ModelField Name="departmentName" />
                          <ext:ModelField Name="branchName" />
                            <ext:ModelField Name="positionName" />
                          <ext:ModelField Name="clockDuration" />
                          <ext:ModelField Name="duration" />
                        <ext:ModelField Name="netLateness" />

                         <ext:ModelField Name="strClockDuration" />
                          <ext:ModelField Name="strDuration" />
                        <ext:ModelField Name="strNetLateness" />
                        
                     
                   </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="employeeName" Direction="ASC" />
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

                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <%--<ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button Visible="false" ID="btnDeleteSelected" runat="server" Text="<%$ Resources:Common , DeleteAll %>" Icon="Delete">
                                    <Listeners>
                                        <Click Handler="CheckSession();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnDeleteAll">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>--%>


                               

                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" Width="300" ForceSelection="true" 
                                    TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="payRefWithDateRange" ID="payId" 
                                    EmptyText="<%$ Resources: PayRef %>">                                
                                <Store>
                                    <ext:Store runat="server" ID="payIdStore">
                                        <Model>
                                            <ext:Model runat="server" IDProperty="recordId">
                                                <Fields>

                                                    <ext:ModelField Name="recordId" />
                                                    <ext:ModelField Name="payRefWithDateRange" />

                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                         <%--<Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillPayId"></ext:PageProxy>
                                         </Proxy>--%>
                                    </ext:Store>
                                </Store>
                                <DirectEvents>                                                       
                                        <Change OnEvent="FillDates">
                                                <ExtraParams>
                                                <ext:Parameter Name="payId" Value="#{payId}.getValue()" Mode="Raw" />  
                                            </ExtraParams>
                                        </Change>
                                    </DirectEvents>
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.clearValue();" />
                                </Listeners>
                            </ext:ComboBox>

                                <ext:ToolbarSeparator />

                                <ext:TextField ID="ColStartDate" Width="200" runat="server" FieldLabel="<%$ Resources:StartDate%>"  AllowBlank="true" ReadOnly="true" ></ext:TextField>
                                <ext:TextField ID="ColEndDate" Width="200" runat="server" FieldLabel="<%$ Resources:EndDate%>"  AllowBlank="true" ReadOnly="true" ></ext:TextField>
<%--                                <ext:DateColumn ID="colStartDate" Width="250" runat="server" FieldLabel="<%$ Resources:StartDate%>" 
                                    AllowBlank="true" ReadOnly="true" ></ext:DateColumn>--%>

<%--                                <ext:DateField runat="server" ID="colStartDate" FieldLabel="<%$ Resources:StartDate%>"  AllowBlank="true"  ReadOnly="true" Width="200" Flex="1" />
                                <ext:DateField runat="server" ID="colEndDate" FieldLabel="<%$ Resources:EndDate%>" AllowBlank="true"  ReadOnly="true" Width="200" Flex="1" />--%>

                                <%-- <ext:DateColumn ID="colEndDate" Width="250" runat="server" FieldLabel="<%$ Resources:EndDate%>" 
                                    AllowBlank="true" ReadOnly="true" ></ext:DateColumn>--%>
                              
                              
                                <ext:Button runat="server" Text="<%$ Resources: Common,Preview%>" MarginSpec="0 10 0 650" Width="100" >
                                   <%-- <Listeners>
                                        <Click Handler="#{Store1}.reload();">
                                        </Click>
                                    </Listeners>--%>
                                    <DirectEvents>
                                        <Click OnEvent="Preview_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>

                                <ext:ToolbarSeparator />

                                <ext:Button runat="server" Text="<%$ Resources: Common,Generate%>" MarginSpec="0 0 0 0" Width="100">
                                   <%-- <Listeners>
                                        <Click Handler="#{Store1}.reload();">
                                        </Click>
                                    </Listeners>--%>
                                    <DirectEvents>
                                        <Click OnEvent="Generate_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>


                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="employeeId" runat="server" />
                             <ext:Column ID="ColEmployeeRef" DataIndex="employeeRef" Text="<%$ Resources: FieldEmployeeRef%>" runat="server" />
                            <ext:Column ID="ColName" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server">
                            </ext:Column>
                           <ext:Column ID="ColDepartmentName" DataIndex="departmentName" Text="<%$ Resources: FieldDepartmentName%>" runat="server" />
                            <ext:Column ID="ColBranchName" DataIndex="branchName" Text="<%$ Resources: FieldBranchName%>" runat="server" />
                            <ext:Column ID="ColPositionName" DataIndex="positionName" Text="<%$ Resources: FieldPositionName%>" runat="server" />
                            <ext:Column ID="ColClockDuration" DataIndex="strClockDuration" Text="<%$ Resources: FieldClockDuration%>" runat="server" />
                            <ext:Column ID="ColDuration" DataIndex="strDuration" Text="<%$ Resources: FieldDuration%>" runat="server" />
                            <ext:Column ID="ColNetLateness" DataIndex="strNetLateness" Text="<%$ Resources: FieldNetLateness%>" runat="server" />

                          
                             <ext:Column runat="server"
                                ID="colEdit"  Visible="true"
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
                    
                     <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                 <ext:Parameter Name="empId" Value="record.data['employeeId']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>

                   
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single"  StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                    
                </ext:GridPanel>
            </Items>
        </ext:Viewport>



       <ext:Window 
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:TimeVariations %>"
            Width="1050"
            Height="650"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                      
                        

                      <ext:GridPanel
                            ID="TimeVariationPanel"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            MaxHeight="350"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                             Title="<%$ Resources: TimeVariationTitle %>"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True" >
                                  <TopBar>
                                    <ext:Toolbar ID="Toolbar2" runat="server">
                                     <Items>                                 
                            </Items>
                        </ext:Toolbar>
                            
                    </TopBar>
                            
                            <Store>
                                <ext:Store runat="server" ID="TimeVariationStore">
                                    <Model>
                                        <ext:Model runat="server">
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
                                </ext:Store>
                            </Store>

                            
                            <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                   <ext:Column ID="ColDRecordId" MenuDisabled="true" runat="server"  DataIndex="recordId" Visible="false" />
        
                             <ext:Column ID="ColStringDayId" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDay%>" DataIndex="dayIdString" Width="200" Hideable="false">
                              
                            </ext:Column>
                            <ext:Column ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployeeName%>" DataIndex="employeeName" Width="125" Hideable="false">

                        
                            </ext:Column>
                            <ext:Column ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranchName%>" DataIndex="branchName" Width="75" Hideable="true">
                                
                            </ext:Column>

                              <ext:Column ID="ColtimeCodeString" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldltimeCode%>" DataIndex="timeCodeString" Width="100" Hideable="false">
                            
                            </ext:Column>
                              <ext:Column ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldClockDuration%>" DataIndex="clockDurationString" Width="100" Hideable="false" />
                            
                         <ext:Column ID="CoDuration" Visible="true" DataIndex="durationString" runat="server" Text="<%$ Resources: FieldDuration %>" Width="75" >
                                  
                                    
                             </ext:Column>
                                <ext:Column ID="Column5" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldApprovalStatus%>" DataIndex="apStatusString" Width="125" Hideable="false">
                               <%--<Renderer Handler="return LinkRender(value, metadata, record, rowIndex,  colIndex, store,record.data['apStatusString']);" />--%>
                            </ext:Column>
                               <ext:Column ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDamageLevel%>" DataIndex="damageLevelString" Width="125" Hideable="false">
                                   
                             
                            
                            </ext:Column>
                            <ext:Column ID="Column8" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldJustification%>" DataIndex="justification" Width="120" Hideable="false">
                                   
                             
                            
                            </ext:Column>

                           
                                   
                                   
                                    
                           
                        


                                </Columns>
                            </ColumnModel>

                          
                          <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                   

                            <View>
                                <ext:GridView ID="GridView2" runat="server" />
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                            </SelectionModel>
                         
                     </ext:GridPanel>

                    </Items>
                </ext:TabPanel>              

                


            </Items>
           
        </ext:Window>

    

    </form>
</body>
</html>

