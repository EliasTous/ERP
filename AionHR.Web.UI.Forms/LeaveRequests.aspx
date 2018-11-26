<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveRequests.aspx.cs" Inherits="AionHR.Web.UI.Forms.LeaveRequests" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/LeaveRequests.js?id=9"></script>
    <script type="text/javascript" src="Secripts/common.js?id=120"></script>
    <script type="text/javascript" src="Scripts/moment.js?id=20"></script>
    <script type="text/javascript">
        //function CalcSum() {

        //    var sum = 0;
        //    App.LeaveDaysGrid.getStore().each(function (record) {
        //        sum += record.data['leaveHours'];
        //    });

        //    App.sumHours.setValue(sum.toFixed(2));
        //    App.sumHours2.setValue(sum.toFixed(2));


        //}

        //function FillReturnInfo(id, d1, d2) {

        //    App.leaveId.setValue(id);
        //    App.DateField1.setValue(new Date(d1));
        //    App.DateField2.setValue(new Date(d2));
        //    App.returnDate.setValue(new Date(d2));
        //    App.Button1.setDisabled(false);
        //}
        //function SetReturnDateState() {
        //    if (App.status.value == 2)
        //        App.returnDate.setDisabled(false);
        //    else
        //        App.returnDate.setDisabled(true);
        //}
     
      

        
    </script>
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="StatusPending" runat="server" Text="<%$ Resources:FieldPending %>" />
        <ext:Hidden ID="StatusApproved" runat="server" Text="<%$ Resources: FieldApproved %>" />
        <ext:Hidden ID="StatusRefused" runat="server" Text="<%$ Resources: FieldRefused %>" />
        <ext:Hidden ID="StatusUsed" runat="server" Text="<%$ Resources: FieldUsed %>" />
      
        
        
<%--        <ext:Hidden ID="SundayText" runat="server" Text="<%$ Resources:Common , SundayText %>" />
        <ext:Hidden ID="MondayText" runat="server" Text="<%$ Resources:Common , MondayText %>" />
        <ext:Hidden ID="TuesdayText" runat="server" Text="<%$ Resources:Common , TuesdayText %>" />
        <ext:Hidden ID="WednesdayText" runat="server" Text="<%$ Resources:Common , WednesdayText %>" />
        <ext:Hidden ID="ThursdayText" runat="server" Text="<%$ Resources:Common , ThursdayText %>" />
        <ext:Hidden ID="FridayText" runat="server" Text="<%$ Resources:Common , FridayText %>" />
        <ext:Hidden ID="SaturdayText" runat="server" Text="<%$ Resources:Common , SaturdayText %>" />--%>
       <%-- <ext:Hidden ID="CurrentLeave" runat="server" />
        <ext:Hidden ID="DateFormat" runat="server" />
        <ext:Hidden ID="approved" runat="server" />--%>
       <%-- <ext:Hidden ID="LeaveChanged" runat="server" Text="1" EnableViewState="true" />
        <ext:Hidden ID="TotalText" runat="server" Text="<%$ Resources: TotalText %>" />
        <ext:Hidden ID="StoredLeaveChanged" runat="server" Text="0" EnableViewState="true" />--%>
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App">
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
                        <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="leaveRef" />
                        <ext:ModelField Name="startDate" />
                        <ext:ModelField Name="endDate" />
                        <ext:ModelField Name="returnDate" />
                        <ext:ModelField Name="ltId" />
                        <ext:ModelField Name="status" />
                        <ext:ModelField Name="isPaid" />
                        <ext:ModelField Name="destination" />
                        <ext:ModelField Name="justification" />
                        <ext:ModelField Name="ltName" />
                        <ext:ModelField Name="employeeName" IsComplex="true" />

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
                    ColumnLines="True" IDMode="Explicit" RenderXType="True" ForceFit="true">

                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Button3" runat="server" Icon="ControlEnd">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ReturnLeave">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" EnablePosition="false"  />

                                    </Content>

                                </ext:Container>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="employeeFilter" Width="130" LabelAlign="Top"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" EmptyText="<%$ Resources: FilterEmployee%>"
                                    TriggerAction="Query" ForceSelection="false">
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
                                 
                                    <Items>
                                        <ext:ListItem Text="-----All-----" Value="0" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="includeOpen" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    EmptyText="<%$ Resources: FilterStatus %>">
                                    <Items>
                                           <ext:ListItem Text="<%$ Resources: FieldAll %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: FieldRefused %>" Value="-1" />
                                      
                                    </Items>
                                 
                                </ext:ComboBox>
                                <ext:Button runat="server" Text="<%$ Resources: Common,Go%>" MarginSpec="0 0 0 0" Width="100">
                                    <Listeners>
                                        <Click Handler="#{Store1}.reload();">
                                        </Click>
                                    </Listeners>
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
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField Visible="false" ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{Store1}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column ID="Column4" DataIndex="leaveRef" Text="<%$ Resources: FieldLeaveRef%>" runat="server" Width="70" />
                            <ext:Column ID="ColName" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="6">
                                <Renderer Handler=" return  record.data['employeeName'].fullName" />
                            </ext:Column>
                            <ext:DateColumn ID="Column1" DataIndex="startDate" Text="<%$ Resources: FieldStartDate%>" runat="server" Flex="2" />
                            <ext:DateColumn ID="Column2" DataIndex="endDate" Text="<%$ Resources: FieldEndDate%>" runat="server" Flex="2" />
                            <ext:DateColumn ID="DateColumn3" DataIndex="returnDate" Text="<%$ Resources: FieldReturnDate%>" runat="server" Flex="2" >
                                
                                </ext:DateColumn>
                            <ext:Column runat="server" Width="70" Text="<%$ Resources: CalDays%>">
                                <Renderer Handler="return moment(record.data['endDate']).diff(moment(record.data['startDate']), 'days')+1" />
                            </ext:Column>
                            <ext:Column runat="server" Width="70" Text="<%$ Resources: LateDays%>">
                                <Renderer Handler="if(record.data['returnDate']!='') var d=moment(record.data['returnDate']).diff(moment(record.data['endDate']), 'days')+1; if(d>0) return d; else return '';" />
                            </ext:Column>
                            <ext:Column ID="Column3" DataIndex="status" Text="<%$ Resources: FieldStatus%>" runat="server" Flex="2">
                                <Renderer Handler="return(GetStatusName(record.data['status']));" />
                            </ext:Column>

                            <ext:Column ID="Column5" DataIndex="ltName" Text="<%$ Resources: FieldLtName%>" runat="server" Flex="2" />

                         <%--   <ext:CheckColumn runat="server" Flex="1" Text="<%$ Resources: FieldIsPaid %>" DataIndex="isPaid"></ext:CheckColumn>--%>


                            <ext:Column runat="server"
                                ID="colEdit" Visible="false"
                                Text=" "
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

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
                            </ext:Column>
                             <ext:Column runat="server"
                                ID="colDelete" Visible="true"
                                Text=" "
                                MinWidth="80"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

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
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>

        <uc:leaveControl runat="server" ID="leaveRequest1" />


    </form>
</body>
</html>
