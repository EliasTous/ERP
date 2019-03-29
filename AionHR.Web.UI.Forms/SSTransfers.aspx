<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SSTransfers.aspx.cs" Inherits="AionHR.Web.UI.Forms.SSTransfers" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Nationalities.js?id=1"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript">

        function cheackEmployeeValue() {

            if (App.employeeId.value === null) {
                e.handled = false;
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

        <ext:Store
            ID="documentsTransfersStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            PageSize="50" IDMode="Explicit" Namespace="App" OnReadData="TransfersStore_RefreshData">
            <%--  <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>--%>
            <Model>
                <ext:Model ID="Model4" runat="server" IDProperty="seqNo">
                    <Fields>


                        <ext:ModelField Name="departmentName" />
                        <ext:ModelField Name="employeeName" IsComplex="true" />
                        <ext:ModelField Name="date" />
                        <ext:ModelField Name="doId" />
                        <ext:ModelField Name="seqNo" />
                        <ext:ModelField Name="statusName" />
                        <ext:ModelField Name="notes" />
                        <ext:ModelField Name="apStatus" />



                    </Fields>
                </ext:Model>
            </Model>
            <%-- <Sorters>
                                <ext:DataSorter Property="rowId" Direction="ASC" />
                            </Sorters>--%>
        </ext:Store>



        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:GridPanel
                    ID="DocumentTransfersGrid"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: DocumentsTransfers %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="ArrowSwitchBluegreen"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True" StoreID="documentsTransfersStore">
                 
                    <TopBar>
                        <ext:Toolbar ID="Toolbar7" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="statusFilter" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    EmptyText="<%$ Resources: FieldStatus %>">
                                    <Items>
                                           <ext:ListItem Text="<%$ Resources: All %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: New %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: Approved %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: Refused %>" Value="-1" />
                                      
                                    </Items>
                                 
                                </ext:ComboBox>
                                     <ext:Button runat="server" Text="<%$ Resources: Common,Go%>" MarginSpec="0 0 0 0" Width="100">
                                    <Listeners>
                                        <Click Handler="#{documentsTransfersStore}.reload();">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColumnDept" runat="server" MenuDisabled="true" Text="<%$ Resources: Department%>" DataIndex="departmentName" Flex="2" Hideable="false" />
                            <ext:Column ID="ColumnEmp" runat="server" MenuDisabled="true" Text="<%$ Resources: Employee%>" DataIndex="employeeName.fullName" Flex="2" Hideable="false">
                                <Renderer Handler="return record.data['employeeName'].fullName;" />
                            </ext:Column>
                            <ext:DateColumn CellCls="cellLink" ID="dateCol" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Width="150" Hideable="false">
                            </ext:DateColumn>
                            <ext:Column CellCls="cellLink" ID="ColumnStatus" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldStatus%>" DataIndex="statusName" Width="100" Hideable="false" />


                            <ext:Column runat="server"
                                ID="Column9" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler=" return ((record.data['apStatus']=='1')?editRender():'&nbsp;'); " />

                            </ext:Column>


                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar8" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar4" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>

                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />

                        <Activate Handler="#{documentsTransfersStore}.reload();" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPDT">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="seqNo" Value="record.data['seqNo']" Mode="Raw" />
                                <ext:Parameter Name="departmentName" Value="record.data['departmentName']" Mode="Raw" />
                                <ext:Parameter Name="employeeName" Value="record.data['employeeName'].fullName" Mode="Raw" />
                                <ext:Parameter Name="notes" Value="record.data['notes']" Mode="Raw" />
                                <ext:Parameter Name="doId" Value="record.data['doId']" Mode="Raw" />
                                <ext:Parameter Name="date" Value="record.data['date']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView4" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel3" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>



        <ext:Window
            ID="DocumentTransferWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:DocumentTransferWindow %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel
                    ID="DocumentTransferForm" DefaultButton="DT_Btn"
                    runat="server"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>

                        
                        
                        
                         <ext:TextField ID="doId"  runat="server" Name="doId" Hidden="true"  />
                        <ext:TextField ID="seqNo"  runat="server" Name="seqNo" Hidden="true" />
                        <ext:TextField ID="departmentName" FieldLabel="<%$ Resources:Department%>" runat="server" Name="departmentName" ReadOnly="true" />
                        <ext:TextField ID="employeeName" FieldLabel="<%$ Resources:Employee%>" runat="server" Name="employeeName" ReadOnly="true" />
                        <ext:TextField ID="date" runat="server" FieldLabel="<%$ Resources:FieldDate%>" Name="date" AllowBlank="false" ReadOnly="true" />
                        <ext:TextArea ID="notes" runat="server" Name="notes" FieldLabel="<%$ Resources:FieldNotes%>" ReadOnly="true" />
                          <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="status" Name="status"  ForceSelection="true" AllowBlank="false"
                                    FieldLabel="<%$ Resources: FieldStatus %>">
                                    <Items>
                                          
                                        <ext:ListItem Text="<%$ Resources: Approved %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: Refused %>" Value="-1" />
                                      
                                    </Items>
                                 
                                </ext:ComboBox>
                    </Items>

                </ext:FormPanel>

            </Items>


            <Buttons>
                <ext:Button ID="DT_Btn" runat="server" Text="<%$ Resources:Common, save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{DocumentTransferForm}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="saveDocumentTransfer" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{DocumentTransferWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="seqNo" Value="#{seqNo}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="doId" Value="#{doId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="status" Value="#{status}.getValue()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button6" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>



    </form>
</body>
</html>

