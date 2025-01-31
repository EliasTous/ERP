﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approvals.aspx.cs" Inherits="Web.UI.Forms.Approvals" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/LeaveTypes.js?id=110"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" >
       
        function setCombos(approvalType)
        {
            if (approvalType == 2) {
                App.approvalFlow.setValue('1');
                App.approvalFlow.setReadOnly(true);
              
            }
            else {
                App.approvalFlow.setValue('');
                App.approvalFlow.setReadOnly(false);
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
        <ext:Hidden ID="leaveType1" runat="server" Text="<%$ Resources: Personal %>" />
        <ext:Hidden ID="leaveType2" runat="server" Text="<%$ Resources: Business %>" />
        <ext:Hidden ID="leaveType0" runat="server" Text="<%$ Resources: Other %>" />
            <ext:Hidden ID="apId" runat="server" Text="" />
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
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
                      
                        <ext:ModelField Name="name" />
                         <ext:ModelField Name="approvalTypeName" />
                         <ext:ModelField Name="approvalFlowName" />
                          <ext:ModelField Name="wfName" />

                        

                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>
                
       <ext:Store   ID="ApprovelDepartmentsStore" runat="server"   RemoteSort="False"
            RemoteFilter="true"
            OnReadData="ApprovelDepartmentsStore_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App" >
                                                
                                                                <Model>
                                                                    <ext:Model ID="Model2" runat="server" IDProperty="departmentId">
                                                                        <Fields>

                                                                            <ext:ModelField Name="departmentName" />
                                                                            <ext:ModelField Name="apId" />
                                                                            <ext:ModelField Name="departmentId" />
                                                           

                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                                <Sorters>
                                                                    <ext:DataSorter Property="departmentId" Direction="ASC" />
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
                    Title="<%$ Resources: WindowTitle %>"
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
                                <ext:ToolbarFill ID="ToolbarFillExport" runat="server" />
                                <ext:TextField ID="searchTrigger" runat="server" EnableKeyEvents="true" Width="180">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <KeyPress Fn="enterKeyPressSearchHandler" Buffer="100" />
                                        <TriggerClick Handler="#{Store1}.reload();" />
                                    </Listeners>
                                </ext:TextField>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false"/>
                           
                         
                            <ext:Column runat="server" Flex="1" Text="<%$ Resources: approvalType %>" DataIndex="approvalTypeName"></ext:Column>
                             <ext:Column runat="server" Flex="1" Text="<%$ Resources: approvalFlow %>" DataIndex="approvalFlowName"></ext:Column>
                              <ext:Column runat="server" Flex="1" Text="<%$ Resources: workflow %>" DataIndex="wfName"></ext:Column>
                           



                            <ext:Column runat="server"
                                ID="colDelete" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="100"
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
                                ID="colEdit" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

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
            </Items>
        </ext:Viewport>



        <ext:Window
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="450"
            Height="450"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="BasicInfoTab" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:TextField ID="name" runat="server" FieldLabel="<%$ Resources:FieldName%>" Name="name" AllowBlank="false" />
                                                            
                                 <ext:ComboBox  AnyMatch="true" AllowBlank="false" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: approvalType %>"  runat="server" DisplayField="value" ValueField="key"   Name="approvalType" ID="approvalType" >
                                             <Store>
                                                <ext:Store runat="server" ID="approvalTypeStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                     <Listeners>
                                         <Change Handler="setCombos(this.value);" />
                                     </Listeners>
                                          </ext:ComboBox>
                                  <ext:ComboBox  AnyMatch="true" AllowBlank="false" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: approvalFlow %>"  runat="server" DisplayField="value" ValueField="key"   Name="approvalFlow" ID="approvalFlow" >
                                             <Store>
                                                <ext:Store runat="server" ID="approvalFlowStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                     
                                          </ext:ComboBox>
                                  <ext:ComboBox  AnyMatch="true" AllowBlank="false" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: workflow  %>"  runat="server" DisplayField="name" ValueField="recordId"   Name="wfId" ID="wfId"  >
                                             <Store>
                                                <ext:Store runat="server" ID="workFlowStore">
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
                            <Buttons>
                          <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
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
                        </ext:FormPanel>
                      <%--  <ext:GridPanel 
                                    ID="ApprovelDepartmentsGrid"
                                    runat="server"
                                   StoreID="ApprovelDepartmentsStore"
                                    PaddingSpec="0 0 1 0"
                                    Header="false" 
                         
                                    Title=  "<%$ Resources: ApprovelDepartmentsWindowTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"  
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True" >
                                      
                              <TopBar>
                                  
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                            <Items>
                                  <ext:ComboBox
                                       AutoScroll="true"  AnyMatch="true" CaseSensitive="false" EnableRegEx="true"     runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" 
                                     TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldDepartment%>" DisplayField="name" ID="departmentId" Name="departmentId"  >
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
                                                  <RightButtons>
                                                        <ext:Button ID="Button2" runat="server" Icon="Add" Hidden="true">
                                                            <Listeners>
                                                                <Click Handler="CheckSession();  " />
                                                            </Listeners>
                                                            <DirectEvents>

                                                                <Click OnEvent="addDepartment">
                                                                   
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>
                                                <Listeners>
                                                    <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                                    <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                </Listeners>
                                            </ext:ComboBox>

                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                                            

                                        <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                                            <Columns>
                                                <ext:Column ID="ColapId" Visible="false" DataIndex="apId" runat="server" />
                                                <ext:Column ID="ColdepartmentId" Visible="false" DataIndex="departmentId" runat="server" />
                                                <ext:Column ID="ColdepartmentName" MenuDisabled="true" runat="server" Text="<%$ Resources: departmentName%>" DataIndex="departmentName" Flex="2"/>
                                               
                          
                             
                        
                           

                                                            <ext:Column runat="server"
                                                                ID="Column3" Visible="false"
                                                                Text="<%$ Resources: Common , Delete %>"
                                                                Width="100"
                                                                Align="Center"
                                                                Fixed="true"
                                                                Filterable="false"
                                                                Hideable="false"
                                                                MenuDisabled="true"
                                                                Resizable="false">
                                                                <Renderer Fn="deleteRender" />
                              
                                                        </ext:Column>
                                                        <ext:Column runat="server"
                                                            ID="Column4"
                                                            Visible="false"
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
                                                                ID="Column5"  Visible="true"
                                                                Text=""
                                                                Width="100"
                                                                Hideable="false"
                                                                Align="Center"
                                                                Fixed="true"
                                                                Filterable="false"
                                                                MenuDisabled="true"
                                                                Resizable="false">

                                                                <Renderer handler="return  deleteRender(); " />

                                                            </ext:Column>



                                                        </Columns>
                                                    </ColumnModel>
              
                                                    <Listeners>
                                                        <Render Handler="this.on('cellclick', cellClick);" />
                                                        <Activate Handler="#{ApprovelDepartmentsStore}.reload();" />
                                                    </Listeners>
                                                    <DirectEvents>
                        
                                                        <CellClick OnEvent="PoPuPAD">
                                                            <EventMask ShowMask="true" />
                                                            <ExtraParams>
                                                                <ext:Parameter Name="apId" Value="record.data['apId']" Mode="Raw" />
                                                                  <ext:Parameter Name="departmentId" Value="record.data['departmentId']" Mode="Raw" />
                                                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                                            </ExtraParams>

                                                        </CellClick>
                                                    </DirectEvents>
                                                    <View>
                                                        <ext:GridView ID="GridView2" runat="server" />
                                                    </View>

                  
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single"  StopIDModeInheritance="true" />
                       
                                                    </SelectionModel>
                            
                                                </ext:GridPanel>--%>
                    </Items>
                </ext:TabPanel>
            </Items>
          
        </ext:Window>



    </form>
</body>
</html>
