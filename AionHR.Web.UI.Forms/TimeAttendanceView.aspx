<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeAttendanceView.aspx.cs" Inherits="AionHR.Web.UI.Forms.TimeAttendanceView" %>

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
    <script type="text/javascript" src="Scripts/ReportsCommon.js"></script>
    <style type="text/css">
        .time-variation-link {
            cursor: pointer;
            font-weight: bold;
        }
        .time-variation-link:hover {
    text-decoration: underline;
}
        .x-grid-cell{
            vertical-align:middle;
        }
    </style>
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


            //setInterval(RefreshAllGrids, 60000);

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
        <ext:Hidden ID="overtimeText" runat="server" Text="<%$ Resources: otext %>" />
        <ext:Hidden ID="earlyCheckInText" runat="server" Text="<%$ Resources: EarlyCheckIn %>" />
        <ext:Hidden ID="minutesText" runat="server" Text="<%$ Resources: Minutes %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="CurrentDay" runat="server" />
        <ext:Hidden ID="CurrentCA" runat="server" />
        <ext:Hidden ID="CurrentSC" runat="server" />
         <ext:Hidden ID="vals" runat="server" />
        <ext:Hidden ID="texts" runat="server" />
        <ext:Hidden ID="labels" runat="server" />
       
        <ext:Hidden ID="loaderUrl" runat="server"  Text="ReportParameterBrowser.aspx?_reportName=TAAD&values="/>
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

                        
                       <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="employeeName"  />
                        <ext:ModelField Name="dayId" />
                        <ext:ModelField Name="branchName"  />
                        <ext:ModelField Name="positionName" />
                        <ext:ModelField Name="dayIdString"  />
                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="FSString" />
                        <ext:ModelField Name="ASString" />
                        <ext:ModelField Name="TVString" />
                        
                                                                            

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

                    <ColumnModel ID="ColumnModel1" runat="server"  SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>

                            
                            <ext:Column  ID="Column1" MenuDisabled="true" Align="Center" runat="server"  Text="<%$ Resources: FieldEmployeeName%>" DataIndex="employeeName" Flex="3" Hideable="false">
                                <Renderer Handler="return '<b>'+record.data['employeeName']+'</b><br />'+record.data['dayIdString']+'<br/>'+record.data['departmentName']+'<br/>'+record.data['positionName']+'<br/>'+record.data['branchName'];" />
                                
                            </ext:Column>
                            <ext:Column ID="Column2" MenuDisabled="true" Align="Center" runat="server" Text="<%$ Resources: SC%>" DataIndex="employeeName" Flex="3" Hideable="false">
                                <Renderer Handler="var fin_str = ''; var parts = record.data['FSString'].split('|'); console.log(parts); for(var i=0;i<parts.length;i++)fin_str +=parts[i]+'<br/>'; console.log(fin_str);return '<b>'+fin_str+'</b>'; " />
                                
                            </ext:Column>
                            <ext:Column ID="Column3" MenuDisabled="true" Align="Center" runat="server" Text="<%$ Resources: AttendanceShifts%>" DataIndex="employeeName" Flex="3" Hideable="false">
                                <Renderer Handler="var fin_str = '';  var parts = record.data['ASString'].split('|'); for(var i=0;i<parts.length;i++)fin_str +=parts[i]+'<br/>'; return '<b>'+fin_str+'</b>'; " />
                                
                            </ext:Column>
                             <ext:Column ID="Column5" MenuDisabled="true" Align="Center"  runat="server" Text="<%$ Resources: TimeVariations%>" DataIndex="employeeName" Flex="3" Hideable="false">
                                <Renderer Handler="if(record.data['TVString']=='') return '';var fin_str = ''; var parts = record.data['TVString'].split('|'); for(var i=0;i<parts.length;i++)fin_str +=parts[i]+'<br/>'; return fin_str; " />
                                
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
                   
                  <%--  <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>--%>
         <%--           <DirectEvents>
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
                    </DirectEvents>--%>
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
            ID="TimeApprovalWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources: EditWindowsTimeApproval %>"
            Width="800"
            Height="200"
            AutoShow="false"
            Modal="true"
     
            Hidden="true"
            Draggable="True"
            Maximizable="false"
            Resizable="false" Header="false"
            Layout="Fit">

            <Items>
              
                   
                   
   <ext:GridPanel MarginSpec="0 0 0 0"
                                                            ID="timeApprovalGrid"
                                                            runat="server"
                                                            Icon="User"
                                                           
                                                            Title="<%$ Resources:EditWindowsTimeApproval %>"
                                                            Layout="FitLayout"
                                                            Scroll="Vertical"
                                                           
                                                            Border="false"
                                                              ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                                          <Store>
                                                                <ext:Store PageSize="30"
                                                                    ID="Store3"
                                                                    runat="server" 
                                                                    RemoteSort="false"
                                                                    RemoteFilter="false">
                                                                  
                                                                    <Model>
                                                                        <ext:Model ID="Model3" runat="server" >
                                                                            <Fields>
                                                                                                                                                            
                                                                                <ext:ModelField Name="employeeId" />
                                                                                <ext:ModelField Name="employeeName"  />
                                                                                <ext:ModelField Name="dayId" />
                                                                                <ext:ModelField Name="dayIdDate"  />
                                                                               <ext:ModelField Name="approverName" />
                                                                                <ext:ModelField Name="timeCode" />
                                                                                <ext:ModelField Name="timeCodeString" />
                                                                                <ext:ModelField Name="approverId" />
                                                                                <ext:ModelField Name="status" />
                                                                                <ext:ModelField Name="notes" />
                                                                                  <ext:ModelField Name="statusString" />
                                                                                 <ext:ModelField Name="arName" />
                                                                            

                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>

                                                                </ext:Store>
                                                         

                                                              </Store>
                                                            <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                                <Columns>
                                                                   <ext:Column ID="Column6" DataIndex="dayId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="Column10" DataIndex="employeeId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="Column11" DataIndex="timeCode"  runat="server" Visible="false" />

                                                                     <ext:Column ID="Column12" DataIndex="approverName" Text="<%$ Resources: FieldApproverName%>" runat="server" Flex="1">
                                                                    
                                                                    </ext:Column>
                                                                
                                                                    <ext:Column Visible="false" ID="Column13" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="1">
                                                                    
                                                                    </ext:Column>
                                                                
                                                                   
                          

                                                                     <ext:Column ID="Column14"  DataIndex="timeCodeString" Text="<%$ Resources: FieldTimeCode %>"  runat="server" Flex="1"/>
                                                                     <ext:Column ID="Column15" DataIndex="statusString" Text="<%$ Resources: FieldStatus %>" Width="100" runat="server" >
                                                                      
                                                                    </ext:Column>
                                                                        <ext:Column ID="Column16" DataIndex="arName" Text="<%$ Resources: Common,ApprovalReason %>" Flex="1" runat="server" />
                                                                     <ext:Column ID="Column17" DataIndex="notes" Text="<%$ Resources: FieldNotes %>" runat="server" Flex="2" />

                                                                
                                                                  



                                                                </Columns>
                                                            </ColumnModel>
                                                           

                                                            <View>
                                                                <ext:GridView ID="GridView3" runat="server" />
                                                            </View>


                                                            <SelectionModel>
                                                                <ext:RowSelectionModel ID="rowSelectionModel2" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                               
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
                    <Loader runat="server" Url="ReportParameterBrowser.aspx?_reportName=TAAD" Mode="Frame" ID="Loader8" TriggerEvent="show"
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
