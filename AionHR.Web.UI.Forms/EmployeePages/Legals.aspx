<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Legals.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.Legals" %>



<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Legals.js?id=1"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server"  />

          <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
        


        <Items>

                <ext:GridPanel AutoUpdateLayout="true"
                    ID="rightToWorkGrid" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="true"
                    Title="<%$ Resources: RWGridTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical" Flex="1"
                    Border="false" 
                    Icon="User" DefaultAnchor="100%"  
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="rightToWorkStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="rightToWork_RefreshData"
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
                                        <ext:ModelField Name="issueDate" />
                                        <ext:ModelField Name="expiryDate" />
                                        <ext:ModelField Name="dtId" />
                                        <ext:ModelField Name="remarks" />
                                        <ext:ModelField Name="documentRef" />
                                        <ext:ModelField Name="dtName" />
                                        <ext:ModelField Name="fileUrl" />
                                        <ext:ModelField Name="employeeName" IsComplex="true" />

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
                                        <Click OnEvent="ADDNewRW">
                                            <EventMask ShowMask="true" CustomTarget="={#{rightToWorkGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{rightToWorkStore}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
<%--                            <ext:Column ID="ColName" DataIndex="employeeName.fullName" Text="<%$ Resources: FieldRWEmployeeName%>" runat="server" Width="240">
                                <Renderer Handler=" return '<u>'+ record.data['employeeName'].fullName+'</u>'" />
                            </ext:Column>--%>
                            <ext:Column ID="dtName" DataIndex="dtName" Text="<%$ Resources: FieldRWDtName%>" runat="server" Flex="4" />
                            <ext:Column ID="documentRef1" DataIndex="documentRef" Text="<%$ Resources: FieldRWDocumentRef%>" runat="server" Flex="2" />
                            <ext:DateColumn Format="dd-MM-yyyy" ID="validFrom" DataIndex="issueDate" Text="<%$ Resources: FieldRWIssueDate%>" runat="server" width="100" />
                            <ext:DateColumn Format="dd-MM-yyyy" ID="validTo" DataIndex="expiryDate" Text="<%$ Resources: FieldRWExpiryDate%>" runat="server" width="100" />
                            <ext:Column ID="remarks" DataIndex="remarks" Text="<%$ Resources: FieldRWRemarks%>" runat="server" Flex="2" Visible="false" />
                            


                           <ext:Column runat="server"
                                ID="colEdit"  Visible="true"
                                Text=""
                                Width="120"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="var att ='&nbsp;'; if(record.data['fileUrl']!='') att = attachRender(); return att+'&nbsp;&nbsp;' +editRender()+'&nbsp;&nbsp;' +deleteRender();" />

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
                                ID="colAttach" Visible="false"
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
                        <CellClick OnEvent="PoPuPRW">
                            
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="path" Value="record.data['fileUrl']" Mode="Raw" />
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


                <ext:GridPanel Visible="True"
                    ID="BackgroundCheckGrid" AutoUpdateLayout="true" Collapsible="true"
                    runat="server"
                    PaddingSpec="0 0 1 0"   
                    Header="true"
                    Title="<%$ Resources: BCGridTitle %>"
                    Layout="FitLayout" Flex="1"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="BCStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            OnReadData="BCStore_RefreshData"
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            <Proxy>
                                <ext:PageProxy>
                                    <Listeners>
                                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                                    </Listeners>
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model2" runat="server" IDProperty="recordId">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="employeeId" />
                                        <ext:ModelField Name="date" />
                                        <ext:ModelField Name="expiryDate" />
                                        <ext:ModelField Name="ctId" />
                                        <ext:ModelField Name="remarks" />
                                        <ext:ModelField Name="fileUrl" />
                                        <ext:ModelField Name="ctName" />
                                        <ext:ModelField Name="employeeName" IsComplex="true" />
                                        
                                        

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
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewBC">
                                            <EventMask ShowMask="true" CustomTarget="={#{BackgroundCheckGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>

                                <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                <ext:TextField ID="TextField1" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{BCGrid}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                           <ext:Column ID="Column1" Visible="false" DataIndex="recordId" runat="server" />
<%--                            <ext:Column ID="Column2" DataIndex="employeeName.fullName" Text="<%$ Resources: FieldBCEmployeeName%>" runat="server" Width="240">
                                <Renderer Handler=" return '<u>'+ record.data['employeeName'].fullName+'</u>'" />
                            </ext:Column>--%>
                            <ext:Column ID="Column3" DataIndex="ctName" Text="<%$ Resources: FieldBCCTName%>" runat="server" Flex="1" />
                            <ext:DateColumn Format="dd-MM-yyyy" ID="DateColumn1" DataIndex="date" Text="<%$ Resources: FieldBCIssueDate%>" runat="server" width="100" />
                            <ext:DateColumn Format="dd-MM-yyyy" ID="DateColumn2" DataIndex="expiryDate" Text="<%$ Resources: FieldBCExpiryDate%>" runat="server" width="100" />
                            <%--<ext:Column  Visible="false" ID="Column4" DataIndex="remarks" Text="<%$ Resources: FieldBCRemarks%>" runat="server" Flex="1" />--%>
                          



                            <ext:Column runat="server"
                                ID="ColBCName" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="var att ='&nbsp;'; if(record.data['fileUrl']!='') att = attachRender(); return att+'&nbsp;&nbsp;'+editRender()+'&nbsp;&nbsp;'+deleteRender(); " />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="ColBCDelete" Flex="1" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="100"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                              <Renderer handler="return editRender()+'&nbsp;&nbsp;'+deleteRender();  "  />

                            </ext:Column>
                            <ext:Column runat="server" Visible="false"
                                ID="Column6"
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

                        <ext:Toolbar ID="Toolbar4" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar2" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                    
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPBC">
                            
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="path" Value="record.data['fileUrl']" Mode="Raw" />
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
            ID="EditRWwindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditRWWindowTitle %>"
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
                            ID="EditRWForm" DefaultButton="SaveRWButton"
                            runat="server"
                            Title="<%$ Resources: EditRWWindowTitle%>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField runat="server" Name="recordId"  ID="RWID" Hidden="true"  Disabled="true"/>
                                <ext:ComboBox ValueField="recordId" AllowBlank="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="dtId" Name="dtId" FieldLabel="<%$ Resources:FieldRWDocumentType%>" SimpleSubmit="true">
                                    <Store>
                                        <ext:Store runat="server" ID="RWDocumentTypeStore">
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

                                                                <Click OnEvent="addDocumentType">
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
                                <ext:TextField ID="documentRef" runat="server" FieldLabel="<%$ Resources:FieldRWDocumentRef%>" Name="documentRef"   AllowBlank="false"/>
                                <ext:DateField ID="rwIssueDate" runat="server" Name="issueDate" FieldLabel="<%$ Resources:FieldRWIssueDate%>" AllowBlank="false" />
                                <ext:DateField ID="rwExpiryDate" runat="server" Name="expiryDate" FieldLabel="<%$ Resources:FieldRWExpiryDate%>" AllowBlank="false" />
                                <ext:TextArea runat="server" Name="remarks" FieldLabel="<%$ Resources:FieldRWRemarks%>" />
                                <ext:FileUploadField runat="server" ID="rwFile" FieldLabel="<%$ Resources:FieldFile%>" />
                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveRWButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditRWForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveRW" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRWWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{RWID}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditRWForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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

        <ext:Window
            ID="EditBCWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditBCWindowTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="EditBCTab" DefaultButton="SaveBCButton"
                            runat="server"
                            Title="<%$ Resources: EditBCWindow %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="BCID" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Disabled="true" Name="recordId" />
                                
                           <ext:ComboBox    runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="ctId" Name="ctId" FieldLabel="<%$ Resources:FieldBCCheckType%>" SimpleSubmit="true">
                                                <Store>
                                                    <ext:Store runat="server" ID="checkTypeStore">
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
                                                        <ext:Button ID="Button2" runat="server" Icon="Add" Hidden="true">
                                                            <Listeners>
                                                                <Click Handler="CheckSession();  " />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                <Click OnEvent="addCheckType">
                                                                   
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                            </ext:ComboBox>
                                
                                <ext:DateField ID="DateField1" runat="server" Name="date" FieldLabel="<%$ Resources:FieldBCIssueDate%>" AllowBlank="false" />
                                <ext:DateField ID="DateField2" runat="server" Name="expiryDate" FieldLabel="<%$ Resources:FieldBCExpiryDate%>" AllowBlank="false" />
                                <ext:TextArea runat="server" Name="remarks" FieldLabel="<%$ Resources:FieldBCRemarks%>" />
                                 <ext:FileUploadField runat="server" ID="bcFile" FieldLabel="<%$ Resources:FieldFile%>" />

                            </Items>

                        </ext:FormPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveBCButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditBCTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveBC" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditBCWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{BCID}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{EditBCTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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

