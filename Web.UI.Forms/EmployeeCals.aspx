<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeCals.aspx.cs" Inherits="Web.UI.Forms.EmployeeCals" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/JobInformation.js?id=11"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server"  />
        <ext:Hidden ID="EHCount" runat="server"  />
        <ext:Hidden ID="CurrentHireDate" runat="server"  />
        
        <ext:Hidden ID="ADDNewRecord" runat="server"/>
       
        <ext:Hidden ID="EmployeeTerminated" runat="server" />
          <ext:Viewport ID="Viewport11" runat="server" Layout="Fit" >
           
        


        <Items>
           
            <ext:GridPanel
                
               ID="employeeCalenderGrid"
                    runat="server"
                   
                    PaddingSpec="0 0 1 0"
                    Header="false" 
                    Title="<%$ Resources: ECGridTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"  
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                      
                        <ext:Store
                               ID="employeeCalenderyStore"
                                    runat="server"
                                    RemoteSort="False"
                                    RemoteFilter="true"
                                    OnReadData="employeeCalender_RefreshData"
                                    PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model1" runat="server" >
                                    <Fields>

                                        <ext:ModelField Name="caName" />
                                        <ext:ModelField Name="scName" />
                                         <ext:ModelField Name="scId" />
                                         <ext:ModelField Name="caId" />
                                         <ext:ModelField Name="dayIdDt"  />
                                         <ext:ModelField Name="employeeId"  />
                                         <ext:ModelField Name="employeeName"  />
                                        
                                      

                                    
                                    </Fields>
                                </ext:Model>
                            </Model>
                          <%--  <Sorters>
                                <ext:DataSorter Property="dayIdDt" Direction="ASC" />
                            </Sorters>--%>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewEH">
                                            <EventMask ShowMask="true" CustomTarget="={#{employeeCalenderGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{employeementHistoryStore}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            
                              
                              <ext:Column ID="ColName" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="2">
                               <Renderer Handler=" return record.data['employeeName'].fullName; "/>
                            </ext:Column>
                             <ext:DateColumn  CellCls="cellLink" ID="dayIdDtCO" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldECDate%>" DataIndex="dayIdDt" Width="100" Hideable="false" />
                            <ext:Column Visible="true"  MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCalendarName %>" DataIndex="caName" Hideable="false" Width="75" Align="Center"  />
                            <ext:Column   MenuDisabled="true" runat="server" Text="<%$ Resources: FieldScheduleName%>" DataIndex="scName" Flex="2" Hideable="false"/>
                                
                          



                            <ext:Column runat="server"
                                ID="colEdit" Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

                            </ext:Column>
                            
                            <ext:Column runat="server"
                                ID="colAttach" Visible="false"
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
                                ID="ColEHDelete" Visible="true"
                               
                                Width="100"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                  <Renderer handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

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
                
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPEH">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                               
                              
                                  <ext:Parameter Name="dayIdDtP" Value="record.data['dayIdDt']" Mode="Raw" />
                                <ext:Parameter Name="employeeId" Value="record.data['employeeId']" Mode="Raw" />
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
            ID="EditEHwindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditECWindowTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="EditEHForm" DefaultButton="SaveEHButton"
                            runat="server"
                            Title="<%$ Resources: EditECWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                    <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="employeeId"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" FieldLabel="<%$ Resources: FieldEmployeeName%>"
                                    TriggerAction="Query" ForceSelection="false"  ReadOnly="true">
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
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillEmployee"></ext:PageProxy>
                                            </Proxy>
                                        </ext:Store>
                                    </Store>
                       
                                  
                                </ext:ComboBox>
                                     <ext:DateField  ID="ecDate"  runat="server" Name="dayIdDt" FieldLabel="<%$ Resources:FieldECDate%>" AllowBlank="false" />
                                 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local"   ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldScheduleName %>" Name="scId" DisplayField="name" ValueField="recordId" runat="server" ID="scId">
                                            <Store>
                                                <ext:Store runat="server" ID="scheduleStore">
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
                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldCalendarName %>" Name="caId" DisplayField="name" ValueField="recordId" runat="server" ID="caId">
                                            <Store>
                                                <ext:Store runat="server" ID="caStore">
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
                                                <ext:Button ID="Button1" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addCalendar">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                            
                                
                                
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveEHButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditEHForm}.getForm().isValid()) {return false;};" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveEH" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditEHwindow}.body}" />
                            <ExtraParams>

                              
                               <ext:Parameter Name="values" Value="#{EditEHForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();"  />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

     

          

    </form>
</body>
</html>
