<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FiscalYears.aspx.cs" Inherits="AionHR.Web.UI.Forms.FiscalYears" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript" src="Scripts/FiscalYears.js?id=8"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>

    <script type="text/javascript">
        function setReadOnly(attr, state) {


            Ext.getCmp(attr).setDisabled(!state);

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
        <ext:Hidden ID="SundayText" runat="server" Text="<%$ Resources:Common , SundayText %>" />
        <ext:Hidden ID="MondayText" runat="server" Text="<%$ Resources:Common , MondayText %>" />
        <ext:Hidden ID="TuesdayText" runat="server" Text="<%$ Resources:Common , TuesdayText %>" />
        <ext:Hidden ID="WednesdayText" runat="server" Text="<%$ Resources:Common , WednesdayText %>" />
        <ext:Hidden ID="ThursdayText" runat="server" Text="<%$ Resources:Common , ThursdayText %>" />
        <ext:Hidden ID="FridayText" runat="server" Text="<%$ Resources:Common , FridayText %>" />
        <ext:Hidden ID="SaturdayText" runat="server" Text="<%$ Resources:Common , SaturdayText %>" />
        <ext:Hidden ID="PeriodStatus0" runat="server" Text="<%$ Resources: StatusUnProcessed %>" />
        <ext:Hidden ID="PeriodStatus1" runat="server" Text="<%$ Resources: StatusPosted %>" />
       
        <ext:Hidden ID="CurrentYear" runat="server" />
        <ext:Hidden ID="CurrentDow" runat="server" />
        <ext:Hidden ID="IsWorkingDay" runat="server" />
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server" IDProperty="fiscalYear">
                    <Fields>

                        <ext:ModelField Name="fiscalYear" />
                        <ext:ModelField Name="startDate" />
                        <ext:ModelField Name="endDate" />
                        





                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
               
            </Sorters>
        </ext:Store>



        <ext:Viewport ID="Viewport1" runat="server" Layout="CardLayout" ActiveIndex="0">
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
                              

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />

                            <ext:Column CellCls="cellLink" Sortable="true" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldYear%>" DataIndex="fiscalYear" Flex="1" Hideable="false"/>
                               
                           <ext:DateColumn runat="server" ID="yearFrom" Text="<%$ Resources: FieldFrom%>" DataIndex="startDate" Width="150" />

                            <ext:DateColumn runat="server" ID="yearTo" Text="<%$ Resources: FieldTo%>" DataIndex="endDate" Width="150" />

                           

                            <ext:Column runat="server"
                                ID="colEdit" Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
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
                                ID="colDetails"
                                
                                Hideable="false"
                                Width="120"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer handler="return attachRender()+'&nbsp;&nbsp;'+deleteRender(); " />
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

                <ext:GridPanel runat="server"  Header="false" ID="YearPeriods">
                   
                      <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <Store>
                        <ext:Store ID="fiscalPeriodsStore" runat="server" OnReadData="fiscalPeriodsStore_ReadData">
                            <Model>
                                <ext:Model runat="server" IDProperty="periodId">
                                    <Fields>
                                        <ext:ModelField Name="fiscalYear" />
                                        <ext:ModelField Name="salaryType" />
                                        <ext:ModelField Name="periodId" />
                                        <ext:ModelField Name="status" />
                                        <ext:ModelField Name="startDate" />
                                        <ext:ModelField Name="endDate" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="Prev_Click">
                                            <ExtraParams>
                                                <ext:Parameter Name="index" Value="#{viewport1}.items.indexOf(#{viewport1}.layout.activeItem)" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                              <ext:ComboBox  ID="periodType" runat="server" EmptyText="<%$ Resources:FieldPeriodType%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                            <Items>
                                                  
                                                <ext:ListItem Text="<%$ Resources: SalaryWeekly%>" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources: SalaryBiWeekly%>" Value="3"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources: SalaryFourWeekly%>" Value="4"></ext:ListItem>
                                                <ext:ListItem Text="<%$ Resources: SalaryMonthly%>" Value="5"></ext:ListItem>
                                            </Items>
                                  <Listeners>
                                      <Select Handler="App.fiscalPeriodsStore.reload();">

                                      </Select>
                                  </Listeners>
                                        </ext:ComboBox>
                                
                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <ColumnModel>
                        <Columns>
                         
                                 <ext:DateColumn runat="server" ID="periodFrom" Text="<%$ Resources: FieldFrom%>" DataIndex="startDate" Width="150"  />

                            <ext:DateColumn runat="server" ID="periodTo" Text="<%$ Resources: FieldTo%>" DataIndex="endDate" Width="150" />

                         <ext:Column runat="server" DataIndex="status" Text="<%$ Resources: FieldStatus%>" Flex="1" >
                             <Renderer Handler="return getStatusText(record.data['status']);" />
                             </ext:Column>
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>



        <ext:Window
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
          
            Width="450"
            Height="480"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                   
                    <Items>
                        <ext:FormPanel DefaultButton="SaveButton"
                            ID="BasicInfoTab"
                            runat="server"
                           
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5">
                            <Items>
                                
                                <ext:ComboBox runat="server" FieldLabel="<%$ Resources:FieldYear %>" ID="fiscalYear" Name="fiscalYear" SubmitValue="true" QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" >
                                    <Items>
                                        <ext:ListItem Text="2015" Value="2015" />
                                        <ext:ListItem Text="2016" Value="2016" />
                                        <ext:ListItem Text="2017" Value="2017" />
                                        <ext:ListItem Text="2018" Value="2018" />
                                        <ext:ListItem Text="2019" Value="2019" />
                                        <ext:ListItem Text="2020" Value="2020" />
                                        <ext:ListItem Text="2021" Value="2021" />
                                        <ext:ListItem Text="2022" Value="2022" />
                                    </Items>
                                    <Listeners>
                                        <Select Handler="App.startDate.setValue(new Date(this.value,0,1,1,1,1,1)); App.endDate.setValue(new Date(this.value,11,31,1,1,1,1));" />
                                    </Listeners>
                                </ext:ComboBox>
                                
                                <ext:DateField runat="server" FieldLabel="<%$ Resources: FieldFrom%>"   DataIndex="startDate" ID="startDate" Name="startDate" ReadOnly="true" />
                                <ext:DateField runat="server" FieldLabel="<%$ Resources: FieldTo%>"    DataIndex="endDate" ID="endDate" Name="endDate" ReadOnly="true" />

                            </Items>

                        </ext:FormPanel>


                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                              
                                <ext:Parameter Name="schedule" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />

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

     
    </form>
</body>
</html>


