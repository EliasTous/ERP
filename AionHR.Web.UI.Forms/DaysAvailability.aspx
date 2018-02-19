<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaysAvailability.aspx.cs" Inherits="AionHR.Web.UI.Forms.DaysAvailability" %>


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
    <link rel="stylesheet" href="CSS/DailySchedule.css?id=7" />
    <script type="text/javascript" src="Scripts/DaysAvailability.js?id=310"></script>
    <script type="text/javascript" src="Scripts/Users.js?id=2"></script>
 
    <script type="text/javascript" src="Scripts/jquery-new.js?id=10"></script>

    <script type="text/javascript">

        function SetVisible() {
            App.setDefaultBtn.setHidden(false);
        }
        function SwapRTL() {
            if (document.getElementById("isRTL").value == '1') {

                $(".x-form-itemselector-add").css('background-image', 'url(/ux/resources/images/itemselector/left-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(/ux/resources/images/itemselector/right-gif/ext.axd)');

            }
            else {

                $(".x-form-itemselector-add").css('background-image', 'url(/ux/resources/images/itemselector/right-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(/ux/resources/images/itemselector/left-gif/ext.axd)');

            }
        }
        function AddSource(items) {
          
            var fromStore = App.userSelector.fromField.getStore();
            var toStore = App.userSelector.toField.getStore();

            while (fromStore.getCount() > 0)
                fromStore.removeAt(0);



            for (i = 0; i < items.length ; i++) {
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
         <ext:Hidden ID="currentEmployee" runat="server" />






        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout" ActiveIndex="0">
            <Items>
                <ext:Panel runat="server" ID="panelHeader" Region="North">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" Width="120" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="branchId" EmptyText="<%$ Resources: Branch %>">
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

                               
                                <ext:DateField runat="server" ID="dateFrom" Width="150" LabelWidth="30" FieldLabel="<%$ Resources: From %>" Editable="false" >
                                    <%--  <Listeners>
                                        <Change Handler="App.CompanyHeadCountStore.reload(); App.DimensionalHeadCountStore.reload();" />
                                    </Listeners>--%>
                                </ext:DateField>
                                <ext:Button MarginSpec="0 0 0 0" runat="server" Text="<%$ Resources: Load %>">
                                    <DirectEvents>
                                        <Click OnEvent="Load_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                               
                                <ext:ToolbarSeparator />
                            
                              
                             


                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    
                </ext:Panel>

                <ext:Panel ID="bodyPanel" Region="Center" runat="server">
                    <LayoutConfig>
                        <ext:HBoxLayoutConfig Pack="End" Align="Stretch"></ext:HBoxLayoutConfig>
                    </LayoutConfig>
                    <Items>
                        <ext:Panel runat="server" ID="pnlSchedule" Layout="FitLayout" Flex="6" MarginSpec="0 5 5 5" StyleSpec=" border: 1px solid #add2ed !important;" Html="">
                        </ext:Panel>
                           <ext:FormPanel Hidden="true" runat="server" ID="pnlTools" Layout="VBoxLayout" Flex="1" MarginSpec="0 5 5 5" StyleSpec=" border: 1px solid #add2ed !important;">
                            <Defaults>
                                <ext:Parameter Name="padding" Value="5 5 5 5" Mode="Value" />
                            </Defaults>
                            <LayoutConfig>
                                <ext:VBoxLayoutConfig Align="center" />
                            </LayoutConfig>
                            <Items>
                                <ext:Label runat="server" Text="<%$ Resources: From %>" />
                                <ext:TimeField
                                    ID="timeFrom" Text="From"
                                    runat="server"
                                    Width="100"
                                    Increment="30"
                                    SelectedTime="08:00"
                                    Format="HH:mm tt" />
                                <ext:Label runat="server" Text="<%$ Resources: To %>" />
                                <ext:TimeField
                                    ID="timeTo" Text="From"
                                    runat="server"
                                    Width="100"
                                    Increment="30"
                                    SelectedTime="13:00"
                                    Format="HH:mm tt" />
                                <ext:Button runat="server" Text="<%$ Resources: Save %>"  ID="btnSave" StyleSpec="margin:0 0 10px 0;">
                                    <DirectEvents>
                                        <Click OnEvent="Save_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" Text="<%$ Resources: DeleteDay %>"  ID="btnDeleteDay">
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
                                        <ext:ModelField Name="employeeName" IsComplex="true" />
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
        
    </form>
</body>
</html>


