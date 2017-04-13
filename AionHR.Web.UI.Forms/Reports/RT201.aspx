<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT201.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT201" %>

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
    <script type="text/javascript" src="../Scripts/RT201.js?id=18"></script>
    <script type="text/javascript">
        function printGrid(grid, window) {
            window.show();
            var bd = window.getBody();

            bd.document.body.innerHTML = grid.body.dom.innerHTML;
            bd.document.getElementById(grid.view.el.id).style.height = "auto";
            bd.document.getElementById(grid.view.scroller.id).style.height = "auto";

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

        <ext:Hidden ID="rtl" runat="server" />
        <ext:Hidden ID="format" runat="server" />


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

                        <ext:ModelField Name="effectiveDate" />
                        <ext:ModelField Name="salaryType" />
                        <ext:ModelField Name="paymentFrequency" />
                        <ext:ModelField Name="basicAmount" />
                        <ext:ModelField Name="currencyRef" />




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
                                <ext:Panel runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:Panel runat="server" Layout="HBoxLayout" ID="filterSet1" Hidden="true">
                                            <Items>
                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="branchId" Name="branchId" EmptyText="<%$ Resources:FieldBranch%>">
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

                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="departmentId" Name="departmentId" EmptyText="<%$ Resources:FieldDepartment%>">
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
                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="positionId" Name="positionId" EmptyText="<%$ Resources:FieldPosition%>">
                                                    <Store>
                                                        <ext:Store runat="server" ID="positionStore">
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
                                                <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="divisionId" Name="divisionId" EmptyText="Division">
                                                    <Store>
                                                        <ext:Store runat="server" ID="divisionStore">
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
                                            </Items>
                                        </ext:Panel>
                           

                                        <ext:Panel runat="server" ID="filterSet7" Hidden="true">
                                            <Items>
                                                <ext:ComboBox runat="server" ID="inactivePref" Editable="false" FieldLabel="">
                                                    <Items>
                                                        <ext:ListItem Text="<%$ Resources: All %>" Value="2" />
                                                        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="0" />
                                                        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="1" />
                                                    </Items>
                                              
                                                </ext:ComboBox>

                                            </Items>
                                        </ext:Panel>
                                </Items>
                                    </ext:Panel>
                                                                                 <ext:Button runat="server" Text="Go" >
                                            <Listeners>
                                                <Click Handler="App.firstStore.reload(); App.secondStore.reload();" />
                                            </Listeners>
                                        </ext:Button>
                                <ext:Button Icon="PageGear" runat="server">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="<%$ Resources:Common , Print %>" Icon="Printer">
                                                    <Listeners>
                                                        <Click Handler="printGrid(#{firstGrid}, #{PrintWindow});" />
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
                                    PaddingSpec="0 0 0 0" MinHeight="400" 
                                    Header="false" CollapseMode="Header" Collapsible="true" CollapseDirection="Top"
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
                                            <ext:DateColumn ID="cc" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEffectiveDate %>" DataIndex="effectiveDate" Hideable="false" Width="100" Align="Center">
                                            </ext:DateColumn>


                                            <ext:Column Visible="false" ID="Column13" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSalaryType %>" DataIndex="salaryType" Hideable="false" Flex="1" Align="Center">
                                                <Renderer Handler="return getPaymentTypeString(record.data['salaryType'])" />
                                            </ext:Column>

                                            <ext:Column Visible="false" ID="Column14" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPaymentFrequency %>" DataIndex="paymentFrequency" Flex="1" Hideable="false" Width="75" Align="Center">
                                                <Renderer Handler="return getPaymentTypeString(record.data['paymentFrequency'])" />
                                            </ext:Column>

                                            <ext:Column ID="Column20" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBasicAmount %>" DataIndex="basicAmount" Hideable="false" Flex="1" Width="75" Align="Center">
                                                <Renderer Handler="return record.data['basicAmount'] + '&nbsp;'+ record.data['currencyRef'];">
                                                </Renderer>
                                            </ext:Column>
                                        



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


                            </Items>

                        </ext:Panel>
                    </Items>
                </ext:Panel>


            </Items>
        </ext:Viewport>
        <ext:Window ID="PrintWindow" runat="server" Width="700" Height="400" Hidden="true">
        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:Button runat="server" Text="Print" Icon="Printer" OnClientClick="#{PrintWindow}.getBody().print();" />
                </Items>
            </ext:Toolbar>
        </TopBar>
        <Loader Url="Child.aspx"  runat="server">        
        </Loader>
    </ext:Window>





    </form>
</body>
</html>

