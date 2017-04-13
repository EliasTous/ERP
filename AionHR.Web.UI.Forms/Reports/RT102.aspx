<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT102.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT102" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="../CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="../CSS/RT101.css?id=2" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Dashboard.js"></script>
    <!--  <script type="text/javascript" src="Scripts/app.js"></script>-->
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script src="https://superal.github.io/canvas2image/canvas2image.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/moment.js"></script>
    <script type="text/javascript" src="../Scripts/RT101.js?id=18"></script>
    <script type="text/javascript">
    
    </script>
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />

        <ext:Hidden ID="rtl" runat="server" />
        <ext:Hidden ID="format" runat="server" />

        <ext:Store PageSize="30"
            ID="secondStore" OnReadData="secondStore_ReadData"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true">
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

                        <ext:ModelField Name="name" IsComplex="true" />

                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="positionName" />
                        <ext:ModelField Name="branchName" />
                        <ext:ModelField Name="divisionName" />
                        <ext:ModelField Name="date" />
                        <ext:ModelField Name="esName" />


                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>
        <ext:Store PageSize="30"
            ID="firstStore" OnReadData="firstStore_ReadData"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model3" runat="server">
                    <Fields>

                        <ext:ModelField Name="name" IsComplex="true" />

                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="positionName" />
                        <ext:ModelField Name="branchName" />
                        
                        <ext:ModelField Name="date" />
                        


                    </Fields>
                </ext:Model>
            </Model>

        </ext:Store>
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">

            <Items>

                <ext:Panel
                    ID="Center"
                    runat="server"
                    Border="false"
                    Layout="FitLayout" AutoScroll="true"
                    Margins="0 0 0 0"
                    Region="Center">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                
                                        <ext:Panel runat="server" Layout="HBoxLayout" ID="filterSet3" Hidden="true">
                                            <Items>
                                                <ext:DateField ID="dateFrom" runat="server" EmptyText="<%$ Resources: DateFrom %>"></ext:DateField>
                                                <ext:DateField ID="dateTo" runat="server" EmptyText="<%$ Resources: DateTo %>"></ext:DateField>
                                            
                                                  <ext:Button runat="server" Text="Go" >
                                            <Listeners>
                                                <Click Handler="App.firstStore.reload(); App.secondStore.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                            </Items>
                                        </ext:Panel>
                                <ext:Button Icon="PageGear" runat="server">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , Print %>" Icon="Printer">
                                                    <Listeners>
                                                        <Click Handler="App.firstGrid.print();" />
                                                    </Listeners>
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , ExportAsPdf %>" Icon="DiskDownload" />
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , EmailReport %>" Icon="EmailAttach" />
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>
                                </Items>
                                        </ext:Toolbar>
                          
                    </TopBar>
                    <Items>
                        <ext:Panel runat="server" Height="200" Layout="AutoLayout" Width="1000" AutoScroll="true" ID="toPrint">
                            <Items>


                                <ext:GridPanel ExpandToolText="expand"
                                    ID="firstGrid" MarginSpec="0 17 0 0"
                                    runat="server" StoreID="firstStore"
                                    PaddingSpec="0 0 0 0" MinHeight="400" MaxHeight="400"
                                    Header="true" CollapseMode="Header" Collapsible="true" CollapseDirection="Top"
                                    Title="<%$ Resources: Additions %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>
                                            <ext:Column Visible="false" ID="Column2" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" />
                                            <ext:Column ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name.fullName" Flex="4" Hideable="false">
                                                <Renderer Handler=" return  record.data['name'].fullName; ">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column ID="ColDepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false">
                                            </ext:Column>
                                            <ext:Column ID="ColPositionName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                            <ext:Column ID="ColBranchName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                                           
                                            <ext:DateColumn ID="ColHireDate" Format="dd-MMM-yyyy" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Width="120" Hideable="false">
                                            </ext:DateColumn>


                                        </Columns>
                                    </ColumnModel>

                                    <View>
                                        <ext:GridView ID="GridView3" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel2" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>

                                <ext:GridPanel ExpandToolText="expand" MinHeight="400"
                                    ID="secondGrid" MarginSpec="0 17 0 0" MaxHeight="400"
                                    runat="server" StoreID="secondStore"
                                    PaddingSpec="0 0 0 0"
                                    Header="true" CollapseMode="Header" Collapsible="true" CollapseDirection="Top"
                                    Title="<%$ Resources: Terminations %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                                    <Store>
                                    </Store>


                                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>
                                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" />
                                            <ext:Column ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name.fullName" Flex="4" Hideable="false">
                                                <Renderer Handler=" return  record.data['name'].fullName ">
                                                </Renderer>
                                            </ext:Column>
                                            <ext:Column ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDepartment%>" DataIndex="departmentName" Flex="3" Hideable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column5" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPosition%>" DataIndex="positionName" Flex="3" Hideable="false" />
                                            <ext:Column ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch%>" DataIndex="branchName" Flex="3" Hideable="false" />
                                           
                                            <ext:DateColumn ID="DateColumn1" Format="dd-MMM-yyyy" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Width="120" Hideable="false">
                                            </ext:DateColumn>


                                        </Columns>
                                    </ColumnModel>

                                    <View>
                                        <ext:GridView ID="GridView1" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>
                                </ext:GridPanel>
                            </Items>

                        </ext:Panel>
                    </Items>
                </ext:Panel>


            </Items>
        </ext:Viewport>






    </form>
</body>
</html>
