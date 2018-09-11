<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Absent.aspx.cs" Inherits="AionHR.Web.UI.Forms.Absent" %>

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
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript">
  

        function startRefresh() {


            setInterval(RefreshAllGrids, 60000);

        }
        function RefreshAllGrids() {



            if (window.
                parent.App.tabPanel.getActiveTab().id == "ab") {



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
        
        <ext:Hidden ID="CurrentEmployee" runat="server"  />
        <ext:Hidden ID="format" runat="server"  />
        <ext:Hidden ID="CurrentDay" runat="server"  />
        <ext:Store
            ID="Store1"
            runat="server"
           RemoteSort="false"
            RemotePaging="false"
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

                        <ext:ModelField Name="dayId" />
                       
                        <ext:ModelField Name="name" IsComplex="true" />
                        <ext:ModelField Name="branchName" />
                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="dayStatusString" />
                        <ext:ModelField Name="ltName" />






                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="dayId" Direction="ASC" />
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
                   
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                    <TopBar>
             

                        <ext:Toolbar runat="server">
                            <Items>
                                   <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <%--<uc:dateRange runat="server" ID="dateRange1" />--%>
                                        <uc:dateRange runat="server" ID="dateRange1" />
                                    </Content>
                                </ext:Container>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <%--<uc:dateRange runat="server" ID="dateRange1" />--%>
                                        <uc:employeeCombo runat="server" ID="employeeCombo1" />
                                    </Content>
                                </ext:Container>
                                  <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" />

                                    </Content>

                                </ext:Container>


                                <ext:ToolbarSeparator runat="server" />
                                <ext:Button runat="server" Text="<%$Resources:Go %>">
                                    <Listeners>
                                        <Click Handler="CheckSession(); App.Store1.reload();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                        <Columns>

                            <ext:Column ID="ColDay" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDay%>" DataIndex="dayId" Flex="2" Hideable="false">
                                <Renderer Handler="var d = moment(record.data['dayId']);  return d.format(App.format.value);" />
                            </ext:Column>
                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="employeeName" Flex="3" Hideable="false">
                                <Renderer Handler="return record.data['name'].fullName;" />
                        
                            </ext:Column>
                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="2" Hideable="true">
                                
                            </ext:Column>
                            <ext:Column ID="ColDepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="2" Hideable="false">
                            
                            </ext:Column>

                           <ext:Column ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: Status%>" DataIndex="dayStatusString" Width="150" Hideable="false">
                            
                            </ext:Column>
                            <ext:Column Visible="false" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: LeaveType%>" DataIndex="ltName" Flex="2" Hideable="false">
                         </ext:Column>



                            <ext:Column runat="server"
                                ID="colEdit" Visible="false"
                                Text="<%$ Resources:Common, FieldDetails %>"
                                Width="80"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />
                                <SummaryRenderer Handler="return '&nbsp;';" />
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
                                <SummaryRenderer Handler="return '&nbsp;';" />
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
                                <SummaryRenderer Handler="return '&nbsp;';" />
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
               
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>


                </ext:GridPanel>
          
            </Items>
        </ext:Viewport>

 



    </form>
</body>
</html>

