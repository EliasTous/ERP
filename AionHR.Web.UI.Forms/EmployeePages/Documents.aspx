﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Documents.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.Documents" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Documents.js?id=23"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>
    <script type="text/javascript" src="../Scripts/moment.js?id=0"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server"  />
        <ext:Hidden ID="CurrentDateFormat" runat="server"  />
          <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
        


        <Items>

                <ext:GridPanel AutoUpdateLayout="true"
                    ID="employeeDocumentsGrid" Collapsible="false"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    
                    Layout="FitLayout"
                    Scroll="Vertical" Flex="1"
                    Border="false" 
                    Icon="User" DefaultAnchor="100%"  
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="employeeDocumentsStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="employeeDocumentsStore_RefreshData"
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
                                        <ext:ModelField Name="dtName" />
                                        <ext:ModelField Name="documentRef" />
                                        <ext:ModelField Name="expiryDate" ServerMapping="expiryDate.ToShortDateString()" />
                                        <ext:ModelField Name="remarks" />
                                        <ext:ModelField Name="fileUrl" />

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{employeeDocumentsGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180" Visible="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{employeeDocumentsStore}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column CellCls="cellLink" ID="ColEHName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentRef%>" DataIndex="documentRef" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDocumentType%>" DataIndex="dtName" Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="Column3" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldRemarks%>" DataIndex="remarks" Flex="2" Hideable="false" />
                            
                            <ext:Column  CellCls="cellLink" ID="Column2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldExpiryDate%>" DataIndex="expiryDate" Width="120" Hideable="false" >
                                <Renderer Handler="var now = moment(); var icon=''; var date = moment(record.data['expiryDate']);if(now>date)   icon= attachRender2();return record.data['expiryDate'] + '&nbsp;&nbsp;'+ icon;" />
                                </ext:Column>



                            <ext:Column runat="server"
                                ID="colEdit" Visible="true"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return attachRender()+'&nbsp;&nbsp;'+ editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="ColEHDelete" Flex="1" Visible="false"
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
                
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick  OnEvent="PoPuP">
                            <EventMask  ShowMask="false"  />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="path" Value="record.data['fileUrl']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex,record.data['fileUrl'])" Mode="Raw" />
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

        <ext:Window
            ID="EditDocumentWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditDocumentWindowTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="EditDocumentForm" DefaultButton="SaveDocumentButton"
                            runat="server"
                            Title="<%$ Resources: EditDocumentWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField runat="server" Name="recordId"  ID="EHID" Hidden="true"  Disabled="true"/>
                                <ext:TextField runat="server" Name="documentRef" AllowBlank="false"  ID="documentRef"  FieldLabel="<%$ Resources:FieldDocumentRef%>" />

                                <ext:ComboBox ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="dtId" Name="dtId" FieldLabel="<%$ Resources:FieldDocumentType%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="dtStore">
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
                                          <RightButtons>
                                                        <ext:Button ID="Button6" runat="server" Icon="Add" Hidden="true">
                                                            <Listeners>
                                                                <Click Handler="CheckSession();  " />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                <Click OnEvent="addType">
                                                                    <ExtraParams>
                                                                        
                                                                        
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                </ext:ComboBox>
                                <ext:DateField ID="expiryDate" runat="server" Name="expiryDate" FieldLabel="<%$ Resources:FieldExpiryDate%>"  />
                                <ext:FileUploadField runat="server" ID="documentFile" FieldLabel="<%$ Resources:FieldFile%>" />
                                <ext:TextArea runat="server" Name="remarks"  ID="remarks"  FieldLabel="<%$ Resources:FieldRemarks%>" />
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveDocumentButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditDocumentForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveDocument" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditDocumentWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{EHID}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditDocumentForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

       

          

    </form>
</body>
</html>

