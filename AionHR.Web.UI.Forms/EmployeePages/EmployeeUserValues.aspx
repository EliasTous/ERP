<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeUserValues.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.EmployeeUserValues" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Contacts.js?id=0"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>
    <script type="text/javascript">
        function hideWindow() {
            App.EditWindow.close();
        }
 function refreshStore() {
            App.UserValueStore.reload();
        Ext.Notification.show({title:'success', html:'good'}); 
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
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="SuccRecordSaving" runat="server" Text="<%$ Resources:Common , RecordSavingSucc %>" />
        <ext:Hidden ID="NotificationTitle" runat="server" Text="<%$ Resources:Common , Notification %>" />
     
        <ext:Hidden ID="EmployeeTerminated" runat="server" />
        <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>



            <Items>
                <ext:GridPanel Visible="True"
                    ID="UserValueGrid" AutoUpdateLayout="true" Collapsible="false"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Layout="FitLayout" Flex="1"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="UserValueStore"
                            runat="server"
                            RemoteSort="False"
                            RemoteFilter="true"
                            OnReadData="UserValueStore_RefreshData"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model2" runat="server" IDProperty="seqNo">
                                    <Fields>

                                        <ext:ModelField Name="propertyName" />
                                        <ext:ModelField Name="mask" />
                                            <ext:ModelField Name="maskString" />
                                        <ext:ModelField Name="propertyId" />
                                        <ext:ModelField Name="value" />
                                      



                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button Visible="false" ID="Button2" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNew">
                                            <EventMask ShowMask="true" CustomTarget="={#{UserValueGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                               


                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column runat="server" DataIndex="propertyId" Visible="false" />
                                <ext:Column runat="server" DataIndex="mask" Visible="false" />

                            
                            <ext:Column runat="server" Text="<%$ Resources: FieldProperty %>" DataIndex="propertyName" Flex="1" />
                            <%--  <ext:Column runat="server" Text="<%$ Resources: FieldMask %>" DataIndex="maskString" Flex="1" />--%>
                              <ext:Column runat="server" Text="<%$ Resources: FieldValue %>" DataIndex="value" Flex="1" />
                          
                          

                         
                           
                            <ext:Column runat="server" Visible="false"
                                ID="Column7"
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
                                ID="ColCDelete" Flex="1" Visible="false"
                                Text=""
                                Width="100"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;'+deleteRender(); " />

                            </ext:Column>
                               <ext:Column runat="server"
                                ID="ColCName" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="return editRender(); " />

                            </ext:Column>


                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar4" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar2" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>

                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                        <CellClick Handler="App.Panel8.loader.url='UserPropertyExplorer.aspx?_employeeId='+App.CurrentEmployee.value+'&_propertyId='+ record.data['propertyId']; alert(App.Panel8.loader.url);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">

                            <ExtraParams>
                                <ext:Parameter Name="propertyId" Value="record.data['propertyId']" Mode="Raw" />

                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView2" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>









            </Items>
        </ext:Viewport>
        <ext:Window
            ID="EditWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowTitle %>"
            Width="340"
            Height="400"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Resizable="false"
            Maximizable="false"
            Layout="FitLayout">

            <Items>
                <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel Flex="1"
                            ID="EmployeeValueForm" DefaultButton="SaveEmployeeValue"
                            runat="server" 
                            Title="<%$ Resources: EditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                
                                      

                                 <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldProperty%>"  runat="server" DisplayField="name" ValueField="recordId"   Name="propertyId" ID="propertyId" AllowBlank="false" ReadOnly="true">
                                             <Store>
                                                <ext:Store runat="server" ID="propertyStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="name" />
                                                                <ext:ModelField Name="recordId" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                      
                                       </ext:ComboBox>
                                  <ext:Panel runat="server" Layout="FitLayout" Flex="1"  ID="Panel8" DefaultAnchor="100%" >
                                 <Loader runat="server"  Mode="Frame" ID="Loader8" TriggerEvent="show"
                                ReloadOnEvent="true"
                        DisableCaching="true">
                        <Listeners>
                         </Listeners>
                        <LoadMask ShowMask="true" />
                    </Loader>
                                      </ext:Panel>
                                    
                            </Items>



                        </ext:FormPanel>



                    </Items>
                </ext:TabPanel>
            </Items>
           
        </ext:Window>









    </form>
</body>
</html>


