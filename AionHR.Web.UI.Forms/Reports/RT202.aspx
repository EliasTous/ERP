<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT202.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT202" %>

<%@ Register Assembly="DevExpress.XtraReports.v16.2.Web, Version=16.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

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
        var prev = '';
        function printGrid(grid, window) {
            window.show();


            //bd.document.getElementById(grid.view.el.id).style.height = "auto";
            //bd.document.getElementById(grid.view.scroller.id).style.height = "auto";

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
                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="positionName" />
                        <ext:ModelField Name="prevCurrencyRef" />
                        <ext:ModelField Name="prevBasicAmount" />
                        <ext:ModelField Name="prevSalaryType" />





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
                        <ext:Toolbar runat="server" Height="50" Layout="HBoxLayout" >
                            <Items>
                                <ext:Container runat="server" Width="700">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" />
                                        
                                    </Content>
                                   
                                </ext:Container>
                                    <ext:Container runat="server" Width="200">
                                    <Content>
                                        <uc:activeStatus runat="server" ID="activeStatus1" />
                                    </Content>
                                </ext:Container>
                                
                                <ext:Button runat="server" Text="Go" AutoPostBack="true" OnClick="Unnamed_Click">
                                </ext:Button>
                  
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Content>

                        <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server"></dx:ASPxWebDocumentViewer>

                    </Content>
                    <Items>
                        <ext:Panel runat="server" Height="200" Layout="AutoLayout" Width="1000" AutoScroll="true" ID="toPrint" Hidden="true">
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
                                                <Renderer Handler="if(prev==record.data['name'].fullName) return''; else prev=record.data['name'].fullName; return  record.data['name'].fullName; ">
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
                        <ext:Button runat="server" Text="Print" Icon="Printer" OnClientClick="#{PrintWindow}.getBody().focus(); #{PrintWindow}.getBody().print();" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <ext:GridPanel ExpandToolText="expand"
                    ID="GridPanel1" MarginSpec="0 17 0 0"
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


                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" />
                            <ext:Column ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldFullName%>" DataIndex="name.fullName" Flex="4" Hideable="false">
                                <Renderer Handler=" return  record.data['name'].fullName; ">
                                </Renderer>
                            </ext:Column>
                            <ext:DateColumn ID="DateColumn1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEffectiveDate %>" DataIndex="effectiveDate" Hideable="false" Width="100" Align="Center">
                            </ext:DateColumn>


                            <ext:Column Visible="false" ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSalaryType %>" DataIndex="salaryType" Hideable="false" Flex="1" Align="Center">
                                <Renderer Handler="return getPaymentTypeString(record.data['salaryType'])" />
                            </ext:Column>

                            <ext:Column Visible="false" ID="Column5" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPaymentFrequency %>" DataIndex="paymentFrequency" Flex="1" Hideable="false" Width="75" Align="Center">
                                <Renderer Handler="return getPaymentTypeString(record.data['paymentFrequency'])" />
                            </ext:Column>

                            <ext:Column ID="Column6" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBasicAmount %>" DataIndex="basicAmount" Hideable="false" Flex="1" Width="75" Align="Center">
                                <Renderer Handler="return record.data['basicAmount'] + '&nbsp;'+ record.data['currencyRef'];">
                                </Renderer>
                            </ext:Column>




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
        </ext:Window>






    </form>
</body>
</html>


