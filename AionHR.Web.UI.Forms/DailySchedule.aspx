<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailySchedule.aspx.cs" Inherits="AionHR.Web.UI.Forms.DailySchedule" %>



<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />

    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js?id=1"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js?id=2"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css?id=3" />
    <link rel="stylesheet" href="CSS/DailySchedule.css?id=100" />
  <%--  <link rel="stylesheet" href="CSS/defaultTheme.css?id=11" />--%>
    <script type="text/javascript" src="Scripts/DailySchedule.js?id=8"></script>
    <script type="text/javascript" src="Scripts/Users.js?id=2"></script>
    <script type="text/javascript" src="Scripts/ReportsCommon.js"></script>
    <script type="text/javascript" src="Scripts/jquery-new.js?id=30"></script>
    <script type="text/javascript" src="Scripts/tableHeadFixer.js?id=2"></script>

    <script type="text/javascript">

        function SetVisible() {
            App.setDefaultBtn.setHidden(false);
        }
        function SwapRTL() {
            if (document.getElementById("isRTL").value == '1') {

                $(".x-form-itemselector-add").css('background-image', 'url(ux/resources/images/itemselector/left-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(ux/resources/images/itemselector/right-gif/ext.axd)');

            }
            else {

                $(".x-form-itemselector-add").css('background-image', 'url(ux/resources/images/itemselector/right-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(ux/resources/images/itemselector/left-gif/ext.axd)');

            }
        }
        function AddSource(items) {
          
            var fromStore = App.userSelector.fromField.getStore();
            var toStore = App.userSelector.toField.getStore();

            while (fromStore.getCount() > 0)
                fromStore.removeAt(0);



            for (i = 0; i < items.length; i++) {
                if (fromStore.getById(items[i].userId) == null && toStore.getById(items[i].userId) == null) {

                    fromStore.add(items[i]);
                }

            }
        }
        function show() {
          
            var fromStore = App.usersStore;
            var toStore = App.userSelector.toField.getStore();


            for (var i = 0; i < fromStore.getCount() ; i++) {
                var s = fromStore.getAt(i);
                toStore.add(s);

                var d = App.userSelector.fromField.getStore().getById(s.getId());

                if (d != null) {
                    App.userSelector.fromField.getStore().remove(d);

                }
            }



        }
        function setComboValues(employeeId, from, to) {

          
            if (employeeId != null)
            {
                App.employeeId.insertRecord(1, { fullName: 'Text1', recordId: employeeId });
                App.employeeId.setValue(employeeId);
            }
                else
                App.employeeId.setValue(null);
            if (from )
            {               
                App.dateFrom.setValue(new Date(from));
            }
            else
                App.dateFrom.setValue(null);
            if (to)
            {
                App.dateTo.setValue(new Date(to));
            }
            else
                App.dateTo.setValue(null);


            
         


        }

    
    </script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentCalendar" runat="server" />
        <ext:Hidden ID="dayId" runat="server" />
        <ext:Hidden ID="CurrentYear" runat="server" />
        <ext:Hidden ID="CurrentCalenderLabel" runat="server" />
         <ext:Hidden ID="isRTL" runat="server" />

          <ext:Hidden ID="vals" runat="server" />
        <ext:Hidden ID="texts" runat="server" />
        <ext:Hidden ID="labels" runat="server" />
        <ext:Hidden ID="format" runat="server" />
        <ext:Hidden ID="loaderUrl" runat="server"  Text="ReportParameterBrowser.aspx?_reportName=TAES&values="/>



        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout" ActiveIndex="0">
            <Items>
                <ext:Panel runat="server" ID="panelHeader" Region="North">
                    <DockedItems>


                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>

                                   <ext:Button runat="server" Text="<%$ Resources:Common, Parameters%>"> 
                                       <Listeners>
                                           <Click Handler=" App.reportsParams.show();" />
                                       </Listeners>
                                        </ext:Button>
                                         <ext:Button  runat="server" Text="<%$ Resources: Common, Go %>">
                                    <DirectEvents>
                                        <Click OnEvent="Load_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>

                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" Width="120" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="branchId" Hidden="true" EmptyText="<%$ Resources: Branch %>">
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
                                </ext:ComboBox>

                                <ext:ToolbarSeparator />
                                   
                                <ext:ComboBox AnyMatch="true" Hidden="true" CaseSensitive="false" runat="server" ID="employeeId" Width="160" LabelAlign="Top"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" EmptyText="<%$ Resources: SelectEmp %>"
                                    TriggerAction="Query" ForceSelection="true">
                                    <Store>
                                        <ext:Store runat="server" ID="EmployeeStore" AutoLoad="false">
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
                    
                              
                                <ext:DateField runat="server" Hidden="true" ID="dateFrom" Width="150" LabelWidth="30" FieldLabel="<%$ Resources: From %>"  AutoDataBind="true" >
                                    <%--  <Listeners>
                                        <Change Handler="App.CompanyHeadCountStore.reload(); App.DimensionalHeadCountStore.reload();" />
                                    </Listeners>--%>
                                  
                                </ext:DateField>
                                <ext:DateField runat="server" Hidden="true" ID="dateTo" Width="150" LabelWidth="30" FieldLabel="<%$ Resources: To %>"  AutoDataBind="true">
                                    <%--  <Listeners>
                                        <Change Handler="App.CompanyHeadCountStore.reload(); App.DimensionalHeadCountStore.reload();" />
                                    </Listeners>--%>
                                   

                                </ext:DateField>
                                <ext:ToolbarSeparator />
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="cmbEmployeeImport" Width="260" LabelAlign="Left" FieldLabel="<%$ Resources: ImportFrom %>"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" EmptyText="<%$ Resources: SelectEmp %>"
                                    TriggerAction="Query" ForceSelection="true">
                                    <Store>
                                        <ext:Store runat="server" ID="Store1" AutoLoad="false">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="fullName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillImportEmployee"></ext:PageProxy>
                                            </Proxy>
                                        </ext:Store>
                                    </Store>
                                </ext:ComboBox>
                                <ext:Button runat="server" Text="<%$ Resources: Import %>">
                                    <DirectEvents>
                                        <Click OnEvent="Import_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                   <ext:Button runat="server" Text="<%$ Resources: Export %>">
                                    <DirectEvents>
                                        <Click OnEvent="Export_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                        
                                    </DirectEvents>

                                       <Listeners>
                                           <Click Handler="App.userSelector.toField.getStore().removeAll();"></Click>
                                       </Listeners>
                                </ext:Button>
                                    <ext:Button MarginSpec="0 0 0 10" runat="server" Text="<%$ Resources: Delete %>" ID="btnDelete">
                                    <DirectEvents>
                                        <Click OnEvent="Delete_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button MarginSpec="0 0 0 10" ID="btnClear" runat="server" Text="<%$ Resources: Clear %>">
                                    <DirectEvents>
                                        <Click OnEvent="Clear_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                    <ext:ToolbarSeparator />
                                  <ext:Button MarginSpec="0 0 0 0" ID="Button2" runat="server" Text="<%$ Resources: setDefault %>">
                                    <DirectEvents>
                                        <Click OnEvent="SetDefault_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                               
                                 
                                  
                                

                            </Items>
                        </ext:Toolbar>
                          <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="true">
                               <Items>
                               <ext:TextField ID="workingHours" Width="150" runat="server" FieldLabel="<%$ Resources:workingHours%>" DataIndex="workingHours" AllowBlank="true" ReadOnly="true" ></ext:TextField>
                                  <ext:ComboBox AnyMatch="true" Width="150" LabelWidth="30" CaseSensitive="false" runat="server" ID="device" AllowBlank="true" Name="device"
                                    SubmitValue="true"
                                    TypeAhead="false"
                                    FieldLabel="<%$ Resources: Share%>" Icon="Share">
                                    <Items>
                                        <ext:ListItem Text="<%$Resources:Common,email %>" Value="1" />
                                        <ext:ListItem Text="<%$Resources:Common,whatsApp %>" Value="2" />
                                      

                                    </Items>
                                        <DirectEvents>
                                        <Select OnEvent="Share_Click">
                                            <EventMask ShowMask="true" />
                                        </Select>
                                        
                                    </DirectEvents>
                                </ext:ComboBox>
                                   </Items>
                               </ext:Toolbar>
                            <ext:Toolbar ID="labelbar" runat="server" Height="0" Dock="Top">

                            <Items>
                                 <ext:Label runat="server" ID="selectedFilters" />
                                </Items>
                            </ext:Toolbar>


                 
                 
                         

                        <ext:Toolbar ID="Toolbar2" Visible="false" runat="server" Dock="Bottom" ClassicButtonStyle="true">
                            <Items>
                                <ext:Button runat="server" Text="<%$ Resources: BranchAvailability %>">
                                    <DirectEvents>
                                        <Click OnEvent="BranchAvailability_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ComboBox AutoScroll="true" AnyMatch="true" CaseSensitive="false" EnableRegEx="true" runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="departmentId" Name="departmentId" EmptyText="<%$ Resources:FieldDepartment%>">

                                    <Store>
                                        <ext:Store runat="server" ID="departmentStore">
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
                              

                              
                                <ext:Button runat="server" Text="Import" Visible="false">
                                </ext:Button>
                            

                            </Items>
                        </ext:Toolbar>
                        

                        </DockedItems>
                 
                </ext:Panel>

                <ext:Panel ID="bodyPanel" Region="Center" runat="server">
                    <LayoutConfig>
                        <ext:HBoxLayoutConfig Pack="End" Align="Stretch"></ext:HBoxLayoutConfig>
                    </LayoutConfig>
                    <Items>
                        <ext:Panel runat="server" ID="pnlSchedule" Layout="FitLayout" Flex="6" MarginSpec="0 5 5 5" StyleSpec=" border: 1px solid #add2ed !important;" Html="">
                        </ext:Panel>
                        <ext:FormPanel runat="server" ID="pnlTools" Layout="VBoxLayout" Flex="1" MarginSpec="0 5 5 5" StyleSpec=" border: 1px solid #add2ed !important;">
                            <Defaults>
                                <ext:Parameter Name="padding" Value="5 5 5 5" Mode="Value" />
                            </Defaults>
                            <LayoutConfig>
                                <ext:VBoxLayoutConfig Align="center" />
                            </LayoutConfig>
                            <Items>
                                <ext:Label runat="server" Text="<%$ Resources: From %>" />
                                <ext:TimeField
                                    ForceSelection="true"
                                    ID="timeFrom" Text="From"
                                    runat="server"
                                    Width="75"
                                    Increment="15"
                                    SelectedTime="00:00"
                                    Format="H:i" />
                                <ext:Label runat="server" Text="<%$ Resources: To %>" />
                                <ext:TimeField
                                    ForceSelection="true"
                                    ID="timeTo" Text="From"
                                    runat="server"
                                    Width="75"
                                    Increment="15"
                                   SelectedTime="13:00"
                                    Format="H:i"  />
                                   
                                   
                                <ext:Button runat="server" Text="<%$ Resources: Save %>" Disabled="true" ID="btnSave" StyleSpec="margin:0 0 10px 0;">
                                    <DirectEvents>
                                        <Click OnEvent="Save_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" Text="<%$ Resources: DeleteDay %>" Disabled="true" ID="btnDeleteDay">
                                    <DirectEvents>
                                        <Click OnEvent="DeleteDay_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>

                            </Items>
                        </ext:FormPanel>
                        <ext:Panel runat="server" ID="pnlLegend" Hidden="true" Flex="1" MarginSpec="0 5 5 5" StyleSpec=" border: 1px solid #add2ed !important;" Layout="VBoxLayout"
                            BodyPadding="5">
                            <Defaults>
                                <ext:Parameter Name="margin" Value="0 0 5 0" Mode="Value" />
                            </Defaults>
                            <LayoutConfig>
                                <ext:VBoxLayoutConfig Align="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Button Width="16" Cls="IsReadyT" Disabled="true" Height="16" runat="server" ID="ButIsReadyT" MarginSpec="0 5 0 5" />
                                        <ext:Label ID="Label4" runat="server" Text="Ready"></ext:Label>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel runat="server">
                                    <Items>
                                        <ext:Button Width="16" Cls="IsReadyT" Disabled="true" Height="16" runat="server" ID="Button1" MarginSpec="0 5 0 5" />
                                        <ext:Label ID="Label1" runat="server" Text="Ready"></ext:Label>

                                    </Items>
                                </ext:Panel>

                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>



        <ext:Window
            ID="employeeScheduleWindow"
            runat="server"
            Icon="Group"
            Title="<%$ Resources: Employees %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:GridPanel MarginSpec="0 0 0 0"
                    ID="GridPanel5"
                    runat="server"
                    PaddingSpec="0 0 0 0"
                    Header="false"
                    Title=""
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True" StyleSpec=" border: 1px solid #add2ed !important;">

                    <Store>
                        <ext:Store
                            ID="storeEmployee"
                            runat="server"                           
                           AutoLoad="false">
                            <Model>
                                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                                    <Fields>
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="employeeName"  />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" Text="" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />


                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FullName %>" DataIndex="employeeName" Flex="3" Hideable="false">
                                <Renderer Handler=" return  record.data['employeeName'].fullName ">
                                </Renderer>
                            </ext:Column>




                        </Columns>
                    </ColumnModel>

                       <DirectEvents>
                        <CellDblClick OnEvent="LoadEmployeeSchedule">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                              
                                <ext:Parameter Name="employeeID" Value ="record.data['employeeId']" Mode="Raw"/>
                                 <ext:Parameter Name="employeeName" Value =" record.data['employeeName'].fullName" Mode="Raw"/>
                              
                            </ExtraParams>

                        </CellDblClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView12" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel11" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>


            </Items>

        
        </ext:Window>
           <ext:Window
            ID="groupUsersWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources: ExportSchedule %>"
            Width="550"
            Height="450"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel
                    ID="groupUsersForm" DefaultButton="SaveButton"
                    runat="server" Header="false"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                   <%-- <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" FieldWidth="160" ID="jobInfo1" EnableBranch="false" EnableDivision="false" />

                                    </Content>

                                </ext:Container>
                                <ext:Button runat="server" Text="<%$Resources:Filter %>">
                                    <Listeners>
                                        <Click Handler="App.direct.GetFilteredUsers();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>--%>
                    <Items>
                        <ext:ItemSelector runat="server"  MaxHeight="300" MinHeight="300" AutoScroll="true" ID="userSelector" FromTitle="<%$Resources:All %>" DisplayField="fullName" ValueField="recordId"
                            ToTitle="<%$Resources:Selected %>" SubmitValue="true" SimpleSubmit="true" >
                            <Listeners>
                                <AfterRender Handler="SwapRTL(); " />
                            </Listeners>
                            <Store>
                                <ext:Store runat="server" ID="userSelectorStore" OnReadData="userSelectorStore_ReadData">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="recordId">
                                            <Fields>
                                                <ext:ModelField Name="fullName" />
                                                <ext:ModelField Name="recordId" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                         <%--   <Listeners>

                                <Change Handler="App.direct.GetFilteredUsers();" />
                            </Listeners>--%>
                        </ext:ItemSelector>

                    </Items>

                </ext:FormPanel>


            </Items>
            <Buttons>
             <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{GroupUsersForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="ExportEmployees" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{GroupUsersWindow}.body}" />
                            <ExtraParams>
                              <%--  <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />--%>
                                <ext:Parameter Name="values" Value="#{GroupUsersForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button4" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
          <ext:Window runat="server"  Icon="PageEdit"
            ID="reportsParams"
            Width="600"
          
              MinHeight="500"
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
                    <Loader runat="server" Url="ReportParameterBrowser.aspx?_reportName=TAES" Mode="Frame" ID="Loader8" TriggerEvent="show"
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


