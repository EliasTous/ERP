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
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="CSS/DailySchedule.css?id=2" />
    <script type="text/javascript" src="Scripts/DailySchedule.js?id=9"></script>

    <script type="text/javascript">

        function SetVisible() {
            App.setDefaultBtn.setHidden(false);
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
                              
                                <ext:ToolbarSeparator />
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="employeeId" Width="160" LabelAlign="Top"
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
                                <ext:ToolbarSeparator />
                                <ext:DateField runat="server" ID="dateFrom" Width="150" LabelWidth="30" FieldLabel="<%$ Resources: From %>" Editable="false">
                                    <%--  <Listeners>
                                        <Change Handler="App.CompanyHeadCountStore.reload(); App.DimensionalHeadCountStore.reload();" />
                                    </Listeners>--%>
                                </ext:DateField>
                                <ext:DateField runat="server" ID="dateTo" Width="150" LabelWidth="30" FieldLabel="<%$ Resources: To %>" Editable="false">
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
                               

                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <DockedItems>


                        <ext:Toolbar ID="Toolbar2" runat="server" Dock="Bottom" ClassicButtonStyle="true">
                            <Items>
                                  <ext:Button runat="server" Text="<%$ Resources: BranchAvailability %>">
                                      <DirectEvents>
                                        <Click OnEvent="BranchAvailability_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" Text="<%$ Resources: Load %>">
                                    <DirectEvents>
                                        <Click OnEvent="Load_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" Text="Import" Visible="false">
                                </ext:Button>
                                <ext:Button runat="server" Text="<%$ Resources: Delete %>" ID="btnDelete">
                                       <DirectEvents>
                                        <Click OnEvent="Delete_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btnClear" runat="server" Text="<%$ Resources: Clear %>">
                                       <DirectEvents>
                                        <Click OnEvent="Clear_Click">
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
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
                                    ID="timeFrom" Text="From"
                                    runat="server"
                                    Width="100"
                                    Increment="30"
                                    SelectedTime="08:00"
                                    Format="hh:mm tt" />
                                <ext:Label runat="server" Text="<%$ Resources: To %>" />
                                <ext:TimeField
                                    ID="timeTo" Text="From"
                                    runat="server"
                                    Width="100"
                                    Increment="30"
                                    SelectedTime="13:00"
                                    Format="hh:mm tt" />
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



    </form>
</body>
</html>


