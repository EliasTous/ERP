<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimePerformances.aspx.cs" Inherits="AionHR.Web.UI.Forms.TimePerformances" %>



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
     <script type="text/javascript" src="Scripts/ReportsCommon.js?id=10"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
 <script type="text/javascript" >
    
 </script>
  
  

 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />

        <ext:Hidden ID="vals" runat="server" />
        <ext:Hidden ID="texts" runat="server" />
        <ext:Hidden ID="labels" runat="server" />
        <ext:Hidden ID="loaderUrl" runat="server"  Text="ReportParameterBrowser.aspx?_reportName=TATV&values="/>


         <ext:Hidden ID="duration" runat="server" Text="<%$ Resources: FieldDuration %>"/>
         <ext:Hidden ID="pending" runat="server" Text="<%$ Resources: FieldPending %>"/>
         <ext:Hidden ID="approved" runat="server" Text="<%$ Resources: FieldApproved %>"/>
         <ext:Hidden ID="rejected" runat="server" Text="<%$ Resources: FieldRejected %>"/>
          <ext:Hidden ID="forHF" runat="server" Text="<%$ Resources: FieldFor %>"/>
        
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemotePaging="false"
            OnReadData="Store1_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App" IsPagingStore="true">
           
            <Model>
                <ext:Model ID="Model1" runat="server">
                    <Fields>
                        <ext:ModelField Name="duration" />
                        <ext:ModelField Name="pending" />
                         <ext:ModelField Name="approved" />
                        <ext:ModelField Name="rejected" />
                         <ext:ModelField Name="date" />
                        
                      
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="date" Direction="ASC" />
            </Sorters>
        </ext:Store>


    
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                     <ext:FormPanel runat="server" >
          
          
           
           
          
                         <Items>
                              <ext:Panel
            runat="server"
             Flex="1"
            Layout="FitLayout">

            <Items>
                <ext:CartesianChart Height="300"    InsetPadding="40"
                    ID="Chart1"
                    runat="server"
                    >
                    
                    <Store>
                        <ext:Store runat="server" ID="chartStore">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="dateString" />
                                        <ext:ModelField Name="duration" />
                                        <ext:ModelField Name="pending" />
                                        <ext:ModelField Name="approved" />
                                        <ext:ModelField Name="rejected" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>

                    <LegendConfig Dock="Right" runat="server" />

                

                    <Axes>
                        <ext:NumericAxis
                            Position="Left"
                            Fields="duration,pending,approved,rejected"
                            Grid="true"
                            Minimum="0" >
                          
                        </ext:NumericAxis>

                        <ext:CategoryAxis 
                            Position="Bottom"
                            Fields="dateString"
                            Grid="true">
                            <Label RotationDegrees="-45"  />
                        </ext:CategoryAxis>
                    </Axes>
                    <Series>
                        
                        <ext:LineSeries
                           
                            XField="dateString"
                            YField="duration"  >
                         
      <Renderer Handler="this.setTitle(App.duration.getValue());" />
                            <Marker>
                                <ext:Sprite  Duration="200"  Easing="BackIn" />
                            </Marker>

                            <HighlightConfig>
                                <ext:Sprite Scaling="2"  />
                            </HighlightConfig>

                            <Tooltip TrackMouse="true" runat="server">
                                <Renderer Handler="var title = context.series.getTitle(); toolTip.setHtml(title + ' for ' + record.get('dateString') + ': ' + record.get(context.series.getYField()) );" />
                            </Tooltip>
                        </ext:LineSeries>

                        <ext:LineSeries
                          
                            XField="dateString"
                            YField="pending">
                            <Renderer Handler="this.setTitle(App.pending.getValue());" />
                            <Marker>
                                <ext:Sprite SpriteType="Triangle" Duration="200" Easing="BackOut" />
                            </Marker>

                            <HighlightConfig>
                                <ext:Sprite Scaling="2"  />
                            </HighlightConfig>

                            <Tooltip TrackMouse="true" runat="server">
                                <Renderer Handler="var title = context.series.getTitle(); toolTip.setHtml(title + App.FieldFor.getValue()  + record.get('dateString') + ': ' + record.get(context.series.getYField()) );" />
                            </Tooltip>
                        </ext:LineSeries>

                        <ext:LineSeries
                           
                            XField="dateString"
                            YField="approved">
                               <Renderer Handler="this.setTitle(App.approved.getValue());" />
                            <Marker>
                                <ext:Sprite SpriteType="Arrow" Duration="200" Easing="BackOut" />
                            </Marker>

                            <HighlightConfig>
                                <ext:Sprite Scaling="2"  />
                            </HighlightConfig>

                            <Tooltip TrackMouse="true" runat="server">
                                <Renderer Handler="var title = context.series.getTitle(); toolTip.setHtml(title + App.FieldFor.getValue() + record.get('dateString') + ': ' + record.get(context.series.getYField()) );" />
                            </Tooltip>
                        </ext:LineSeries>

                        <ext:LineSeries
                           
                            XField="dateString"
                            YField="rejected">
                             <Renderer Handler="this.setTitle(App.rejected.getValue());" />
                            <Marker>
                                <ext:Sprite SpriteType="Cross" Duration="200" Easing="BackOut" />
                            </Marker>

                            <HighlightConfig>
                                <ext:Sprite Scaling="2"  />
                            </HighlightConfig>

                            <Tooltip TrackMouse="true" runat="server">
                                <Renderer Handler="var title = context.series.getTitle(); toolTip.setHtml(title + App.FieldFor.getValue()  + record.get('dateString') + ': ' + record.get(context.series.getYField()) );" />
                            </Tooltip>
                        </ext:LineSeries>
                    </Series>
                </ext:CartesianChart>
            </Items>
        </ext:Panel> 

                  <ext:GridPanel Flex="1"
                    ID="GridPanel1"
                    runat="server"
                    StoreID="Store1" 
                    PaddingSpec="0 0 1 0"
                    Header="false" 
                    Height="420"
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

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>

                              <ext:DateColumn    CellCls="cellLink" ID="ColDate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDayId%>" DataIndex="date" Flex="2" Hideable="false"/>
                              <ext:Column   ID="Colduration" MenuDisabled="true" runat="server" Text="<%$ Resources: Fieldduration %>" DataIndex="duration" Hideable="false" width="150" Align="Center"/> 
                              <ext:Column    CellCls="cellLink" ID="ColPending" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPending%>" DataIndex="pending" Flex="2" Hideable="false"/>
                              <ext:Column    CellCls="cellLink" ID="ColApproved" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldApproved%>" DataIndex="approved" Flex="2" Hideable="false"/>
                              <ext:Column    CellCls="cellLink" ID="ColRejected" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldRejected%>" DataIndex="rejected" Flex="2" Hideable="false"/>
                              
                              

                         
                           

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

                                <Renderer handler="return '&nbsp;';"  />

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
                      </ext:FormPanel>
               

            
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
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>

                                    
                            </Items>

                        </ext:FormPanel>
                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession();  if (!#{BasicInfoTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{BasicInfoTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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

    </form>
</body>
</html>

