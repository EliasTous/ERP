<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeApprovalsSelfServices.aspx.cs" Inherits="AionHR.Web.UI.Forms.TimeApprovalsSelfServices" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />

    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/LoanRequests.js"></script>


    <script type="text/javascript" src="Scripts/common.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />

    <link href="CSS/fileinput.min.css" rel="stylesheet" />
    <link href="CSS/theme.css" rel="stylesheet" />

    <!-- load the JS files in the right order -->
    <script src="Scripts/fileinput.js" type="text/javascript"></script>
    <script src="Scripts/theme.js" type="text/javascript">  </script>
    <script src="Scripts/moment.js" type="text/javascript">  </script>
    <script src="Scripts/moment-timezone.js" type="text/javascript">  </script>
        <script type="text/javascript" src="Scripts/ReportsCommon.js" ></script>
    <script type="text/javascript" src="Scripts/locales/ar.js?id=10"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentLanguage" runat="server"  />
        <ext:Hidden ID="vals" runat="server" />
        <ext:Hidden ID="texts" runat="server" />
        <ext:Hidden ID="labels" runat="server" />
        <ext:Hidden ID="format" runat="server" />
        <ext:Hidden ID="loaderUrl" runat="server"  Text="ReportParameterBrowser.aspx?_reportName=SSTA&values="/>

       
      


        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
            

                  


                     <ext:GridPanel 
                                                            ID="TimeGridPanel"
                                                            runat="server"
                                                           
                                                            Header="false"
                                                            Title="<%$ Resources:Time %>"
                                                            Layout="FitLayout"
                                                            Scroll="Vertical"
                                                            Border="false"
                                                            ColumnLines="True" IDMode="Explicit" RenderXType="True" >

                            <DockedItems>
                        <ext:Toolbar runat="server" Height="30" Dock="Top">

                            <Items>
                             
                             
                                
                                
                                          <ext:Button runat="server" Text="<%$ Resources:Common, Parameters%>"> 
                                       <Listeners>
                                           <Click Handler=" App.reportsParams.show();" />
                                       </Listeners>
                                        </ext:Button>
                                  <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                         <ext:Button runat="server" Text="<%$Resources:Common, Go %>" >
                                            <Listeners>
                                                <Click Handler="App.TimeStore.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                  <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                      
                                 
                               
                                 
                                        <ext:Button ID="btnApprovals" runat="server" Text="<%$ Resources:approveAll  %>" Icon="StopGreen">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="Timebatch">
                                            <EventMask ShowMask="true" CustomTarget="={#{TimeGridPanel}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="approve" Value="true" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                        
                                    </DirectEvents>
                                </ext:Button>
                                  <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                                <ext:Button ID="btnReject" runat="server"  Icon="StopRed" Text="<%$ Resources:rejectAll  %>"> 
                                     <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>      
                                      <DirectEvents>
                                        <Click OnEvent="Timebatch">
                                            <EventMask ShowMask="true" CustomTarget="={#{TimeGridPanel}.body}" />
                                             <ExtraParams>
                                                <ext:Parameter Name="approve" Value="false" Mode="Raw" />
                                            </ExtraParams>
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



                                                       
                    
                                
                                
                        
                              
                           
                              

                                
                     <Store>
                                                                <ext:Store 
                                                                    ID="TimeStore"
                                                                    runat="server" OnReadData="TimeStore_ReadData">
                                                                  
                                                                    <Model>
                                                                        <ext:Model ID="Model24" runat="server" >
                                                                            <Fields>
                                                                                                                                                            
                                                                                <ext:ModelField Name="employeeId" />
                                                                                <ext:ModelField Name="employeeName"  />
                                                                                <ext:ModelField Name="dayId" />
                                                                                <ext:ModelField Name="date"  />
                                                                                  <ext:ModelField Name="fullName"  />
                                                                           
                                                                                <ext:ModelField Name="timeCode" />
                                                                                  <ext:ModelField Name="shiftId" />
                                                                                <ext:ModelField Name="timeCodeString" />
                                                                                <ext:ModelField Name="approverId" />
                                                                                <ext:ModelField Name="status" />
                                                                                  <ext:ModelField Name="statusString" />
                                                                                 <ext:ModelField Name="arName" />
                                                                                <ext:ModelField Name="notes" />
                                                                                  <ext:ModelField Name="tvId" />
                                                                                 <ext:ModelField Name="seqNo" />
                                                                            

                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>

                                                                </ext:Store>
                                                            </Store>


                                                            <ColumnModel ID="ColumnModel24" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                                <Columns>
                                                                  


                                                                    <ext:Column ID="Column27" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                                                                   
                                                                    </ext:Column>
                                                                
                                                                    <ext:DateColumn ID="DateColumn5" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate %>" DataIndex="date" Hideable="false" Width="100" />
                          

                                                                     <ext:Column ID="Column26" DataIndex="timeCodeString" Text="<%$ Resources: FieldTimeCode %>"  runat="server" Flex="1" />
                                                                 <%--    <ext:Column ID="Column30" Visible="false" DataIndex="statusString" Text="<%$ Resources: FieldStatus %>" Flex="1" runat="server" >
                                                                        
                                                                    </ext:Column>--%>
                                                                      <ext:Column ID="Column2" DataIndex="statusString" Text="<%$ Resources:Common,status  %>"  runat="server" Flex="1" />
                                                                      <ext:Column ID="Column1" DataIndex="arName" Text="<%$ Resources:  Common,ApprovalReason %>"  runat="server" Flex="1" />
                                                                     <ext:Column ID="Column28" DataIndex="notes" Text="<%$ Resources: FieldNotes %>" runat="server" Flex="2" />

                                                                
                                                                    <ext:Column runat="server"
                                                                        ID="Column29" Visible="true"
                                                                        Text=""
                                                                        Width="100"
                                                                        Hideable="false"
                                                                        Align="Center"
                                                                        Fixed="true"
                                                                        Filterable="false"
                                                                        MenuDisabled="true"
                                                                        Resizable="false">

                                                                        <Renderer Handler="return  attachRender(); " />
                                                                    </ext:Column>



                                                                </Columns>
                                                            </ColumnModel>
                                                            <Listeners>
                                                                <Render Handler="this.on('cellclick', cellClick);" />
                                                                <Activate Handler="#{TimeStore}.reload();" />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                
                                                                <CellClick OnEvent="TimePoPUP">
                                                                    <EventMask ShowMask="true" />
                                                                      <ExtraParams>
                                                                         <ext:Parameter Name="employeeName" Value="record.data['fullName']" Mode="Raw" />
                                                                         <ext:Parameter Name="employeeId" Value="record.data['employeeId']" Mode="Raw" />
                                                                         <ext:Parameter Name="dayId" Value="record.data['dayId']" Mode="Raw" />
                                                                           <ext:Parameter Name="dayIdDate" Value="record.data['dayIdDate']" Mode="Raw" />
                                                                            <ext:Parameter Name="timeCode" Value="record.data['timeCode']" Mode="Raw" />
                                                                           <ext:Parameter Name="timeCodeString" Value="record.data['timeCodeString']" Mode="Raw" />
                                                                           <ext:Parameter Name="Notes" Value="record.data['notes']" Mode="Raw" />
                                                                           <ext:Parameter Name="status" Value="record.data['status']" Mode="Raw" />
                                                                             <ext:Parameter Name="shiftId" Value="record.data['shiftId']" Mode="Raw" />
                                                                             <ext:Parameter Name="seqNo" Value="record.data['seqNo']" Mode="Raw" />
                                                                             <ext:Parameter Name="tvId" Value="record.data['tvId']" Mode="Raw" />
                                                                    
                                                                      
                                                                        
                                                                         
                                                                    </ExtraParams>

                                                             

                                                                </CellClick>
                                                            </DirectEvents>


                                                            <View>
                                                                <ext:GridView ID="GridView24" runat="server" />
                                                            </View>


                                                            <SelectionModel>
                                                                <ext:RowSelectionModel ID="rowSelectionModel23" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                               
                                                            </SelectionModel>
                                                       
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
                  
                  
                </ext:GridPanel>
            </Items>
        </ext:Viewport>



         <ext:Window
            ID="TimeWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:Time%>"
            Width="450"
            Height="500"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Resizable="false"
            Maximizable="false"
            Draggable="false"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">

                   
          
           
                    <Items>
                        <ext:FormPanel
                            ID="TimeFormPanel" DefaultButton="SaveTimeButton"
                            runat="server"
                            Title="<%$ Resources:Time %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5"  OnLoad="TimeTab_Load">
                            <Items>
                                <ext:TextField ID="TimeemployeeIdTF" runat="server" Name="employeeId"  ReadOnly="true"  Hidden="true"/>
                                 <ext:TextField ID="seqNo" runat="server" Name="seqTF"  ReadOnly="true"  Hidden="true"/>
                                   <ext:TextField ID="tvId" runat="server" Name="tvId"  ReadOnly="true"  Hidden="true"/>
                                  <ext:TextField ID="shiftIdTF" runat="server" Name="shiftId" FieldLabel="<%$ Resources: FieldShift%>"   ReadOnly="true" />
                               
                                  <ext:TextField ID="TimeTimeCodeTF" runat="server" Name="timeCode"  ReadOnly="true" Hidden="true" />
                               <ext:TextField ID="TimeEmployeeName" runat="server" FieldLabel="<%$ Resources:FieldName%>" ReadOnly="true" />
                                <ext:DateField ID="TimedayIdDate" runat="server" Name="date"  FieldLabel="<%$ Resources:FieldDate%>"  ReadOnly="true" />
                                <ext:TextField ID="TimeTimeCodeString" runat="server" Name="timeCodeString" FieldLabel="<%$ Resources: FieldTimeCode%>"   ReadOnly="true" />


                                <ext:TextField ID="clockDuration" runat="server" Name="clockDuration" FieldLabel="<%$ Resources: FieldClockDuration%>"   ReadOnly="true" />
                                  <ext:TextField ID="duration" runat="server" Name="duration" FieldLabel="<%$ Resources: FieldDuration%>" ReadOnly="true" />
                                  <ext:TextField ID="damageLevel" runat="server" Name="damageLevel" FieldLabel="<%$ Resources: FielddamageLevel%>" ReadOnly="true" />
                                  <ext:TextField ID="shiftStart" runat="server" Name="shiftStart" FieldLabel="<%$ Resources: FieldshiftStart%>" ReadOnly="true" />
                                  <ext:TextField ID="shiftEnd" runat="server" Name="shiftEnd" FieldLabel="<%$ Resources: FieldshiftEnd%>" ReadOnly="true" />
                                
                               
                              


                            
                              



                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="TimeStatus" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"  Name="status" 
                                    FieldLabel="<%$ Resources: FieldStatus %>">
                                    <Items>
                                        
                                     
                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="<%$ Resources:ComboBoxValues,  SYTATATimeStatusNew %>" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="<%$ Resources:ComboBoxValues,  SYTATATimeStatusApproved %>" />
                                        <ext:ListItem Text="<%$ Resources: FieldRefused %>" Value="<%$ Resources:ComboBoxValues,  SYTATATimeStatusRefused %>" />
                                    </Items>

                                </ext:ComboBox>





                                <ext:TextArea runat="server" FieldLabel="<%$Resources: FieldNotes %>" ID="TimeNotes" Name="notes" MaxHeight="5" />


                            </Items>

                        </ext:FormPanel>
                         <ext:GridPanel MarginSpec="0 0 0 0"
                                                            ID="timeApprovalGrid"
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
                                                                    ID="timeApprovalStore"
                                                                    runat="server" 
                                                                    RemoteSort="false"
                                                                    RemoteFilter="false" >
                                                                  
                                                                    <Model>
                                                                        <ext:Model ID="Model33" runat="server" >
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
                                                                                     <ext:ModelField Name="seqNo" />
                                                                            

                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                   
                                                                </ext:Store>
                                                         

                                                              </Store>
                                                            <ColumnModel ID="ColumnModel34" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                                                <Columns>
                                                                   <ext:Column ID="Column38" DataIndex="dayId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="Column39" DataIndex="employeeId"  runat="server" Visible="false" />
                                                                   <ext:Column ID="Column40" DataIndex="timeCode"  runat="server" Visible="false" />

                                                                     <ext:Column ID="Column41" DataIndex="approverName" Text="<%$ Resources: FieldApproverName%>" runat="server" Flex="2">
                                                                  
                                                                    </ext:Column>
                                                                                                                              
                                                                                                                                
                          

                                                                     <ext:Column ID="Column43"  DataIndex="timeCodeString" Text="<%$ Resources: FieldTimeCode %>"  runat="server" Flex="1" />
                                                                     <ext:Column ID="Column44" DataIndex="statusString" Text="<%$ Resources: FieldStatus %>" Flex="1" runat="server" >
                                                                      
                                                                    </ext:Column>
                                                                     <ext:Column ID="Column45" DataIndex="notes" Text="<%$ Resources: FieldNotes %>" runat="server" Flex="2" />

                                                                
                                                                  



                                                                </Columns>
                                                            </ColumnModel>
                                                           

                                                            <View>
                                                                <ext:GridView ID="GridView34" runat="server" />
                                                            </View>


                                                            <SelectionModel>
                                                                <ext:RowSelectionModel ID="rowSelectionModel33" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                                               
                                                            </SelectionModel>
                                                        </ext:GridPanel>

                    </Items>
                </ext:TabPanel>
            </Items>

            <Buttons>
                <ext:Button ID="SaveTimeButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{TimeFormPanel}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveTimeRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{TimeWindow}.body}" />



                            <ExtraParams>
                                 <ext:Parameter Name="seqNo" Value="#{seqNo}.getValue()" Mode="Raw" />
                                  <ext:Parameter Name="tvId" Value="#{tvId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{TimeFormPanel}.getForm().getValues()" Mode="Raw" Encode="true" />
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
                    <Loader runat="server" Url="ReportParameterBrowser.aspx?_reportName=SSTA" Mode="Frame" ID="Loader8" TriggerEvent="show"
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
