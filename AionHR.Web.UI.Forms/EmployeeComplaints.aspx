﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeComplaints.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeeComplaints" %>



<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
     <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
   
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/EmployeeComplaints.js?id=13" ></script>


    <script type="text/javascript" src="Scripts/common.js" ></script>

<link href="CSS/theme.css" rel="stylesheet" />
 
<!-- load the JS files in the right order -->

<script src="Scripts/theme.js" type="text/javascript">  </script>
   
 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="currentCase" runat="server" />
        
        <ext:Hidden ID="StatusPending" runat="server" Text="<%$ Resources:FieldPending %>" />
        <ext:Hidden ID="StatusOpen" runat="server" Text="<%$ Resources: FieldOpen %>" />
        <ext:Hidden ID="StatusClosed" runat="server" Text="<%$ Resources: FieldClosed %>" />
        <ext:Hidden ID="CurrentLanguage" runat="server" />
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="True"
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
                        <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="dateReceived" />
                        <ext:ModelField Name="actionTaken" />
                        <ext:ModelField Name="status" />
                        <ext:ModelField Name="actionRequired" />
                        <ext:ModelField Name="complaintDetails" />
                        <ext:ModelField Name="employeeName" IsComplex="true" />
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
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
                            


                                 <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="branchId" Name="branchId" EmptyText="<%$ Resources:FilterBranch%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" >
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
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>
                               
                                </ext:ComboBox>
                                 
                                <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="departmentId" Name="departmentId" EmptyText="<%$ Resources:FilterDepartment%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" >
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
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>
                                

                                </ext:ComboBox>
                                <%-- <ext:ComboBox runat="server" ValueField="recordId" DisplayField="name" ID="divisionId" Name="divisionId" EmptyText="<%$ Resources:FilterDivision%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" >
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
                                    <Listeners>
                                        <Select Handler="#{Store1}.reload()" />
                                    </Listeners>
                                    
                                </ext:ComboBox>--%>
                                <ext:ComboBox runat="server" ID="statusPref" Editable="false" EmptyText="<%$ Resources: FilterStatus %>"  >
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="3"  />
                                        <ext:ListItem Text="<%$ Resources: FieldPending %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldOpen %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldClosed %>" Value="2" />
                                    </Items>
                                    <Listeners>
                                        <Change Handler="App.Store1.reload()" />
                                    </Listeners>
                                </ext:ComboBox>
                                  <ext:Button runat="server" Text="<%$ Resources: ButtonClear%>" MarginSpec="0 0 0 0"  Width="100">
                                    <Listeners>
                                        <Click Handler="#{departmentId}.clear();#{branchId}.clear(); #{Store1}.reload(); #{statusPref}.setValue(3);">

                                        </Click>
                                    </Listeners>
                                </ext:Button>

                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column ID="ColName" DataIndex="employeeName.fullName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="3">
                                 <Renderer Handler="return  record.data['employeeName'].fullName ">
                                </Renderer>
                            </ext:Column>
                            <ext:DateColumn Format="dd-MM-yyyy" ID="colDateReceived" DataIndex="dateReceived" Text="<%$ Resources: FieldDateReceived%>" runat="server" Flex="1" />
                            <ext:Column    CellCls="cellLink" ID="colActionTaken" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldActionTaken%>" DataIndex="actionTaken" Flex="2" Hideable="false"></ext:Column>
                            <ext:Column    CellCls="cellLink" ID="colActionRequired" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldActionRequired%>" DataIndex="actionRequired" Flex="2" Hideable="false"></ext:Column>
                            <ext:Column    CellCls="cellLink" ID="colComplaintDetails" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldComplaintDetails%>" DataIndex="complaintDetails" Flex="2" Hideable="false"></ext:Column>
                            <ext:Column ID="colStatus" DataIndex="status" Text="<%$ Resources: FieldStatus%>" runat="server" Flex="1">
                            <Renderer Handler="return GetStatusName(record.data['status']);" />
                            </ext:Column>
          
                        
                           

                            <ext:Column runat="server"
                                ID="colEdit"  Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colDelete" Flex="1" Visible="true"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer handler="return editRender()+ '&nbsp&nbsp'+ deleteRender();" />
                              
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
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single"  StopIDModeInheritance="true" />
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
            Width="600"
            Height="350"
            AutoShow="false"
            Modal="true"
            Hidden="true"
             Maximizable="false"
            Resizable="false"
             Draggable="false"
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
                                <ext:TextField ID="recordId" runat="server"  Name="recordId"  Hidden="true"/>
                                <ext:ComboBox runat="server" ID="employeeId"
                                    DisplayField="fullName"
                                    ValueField="recordId"
                                    TypeAhead="false" AllowBlank="false"
                                    FieldLabel="<%$ Resources: FieldEmployeeName%>"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3"
                                    TriggerAction="Query" ForceSelection="false">
                                    <Store>
                                        <ext:Store runat="server" ID="employeeStore" AutoLoad="false">
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
                                
                                <ext:DateField ID="dateReceived" runat="server" FieldLabel="<%$ Resources:FieldDateReceived%>" Name="dateReceived" AllowBlank="false" />                                
                                <ext:TextField ID="actionTaken" runat="server" FieldLabel="<%$ Resources:FieldActionTaken%>" Name="actionTaken" AllowBlank="false"/>
                                <ext:TextField ID="actionRequired" runat="server" FieldLabel="<%$ Resources:FieldActionRequired%>" Name="actionRequired" AllowBlank="false"/>
                                <ext:TextField ID="complaintDetails" runat="server" FieldLabel="<%$ Resources:FieldComplaintDetails%>" Name="complaintDetails" AllowBlank="false"/>
                                <ext:ComboBox runat="server" ID="status" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    FieldLabel="<%$ Resources: FieldStatus %>" AllowBlank="false">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: FieldOpen %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldPending %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldClosed %>" Value="2" />
                                    </Items>
                                    
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
                                <ext:Parameter Name="values" Value ="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
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

                       <%-- <ext:Panel runat="server" Title="<%$ Resources: CaseCommentsTabTitle %>" ID="caseCommentsTab" >
                            <Items>
                                 <ext:Panel runat="server"  Layout="HBoxLayout" Width="600" >
                                    <Items>
                                          <ext:TextArea runat="server" ID="newNoteText" Region="West" Width="550" Height="60" />
                                <ext:Button Region="East" ID="Button1" MarginSpec="20 0 0 0" Height="25" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                  <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecordComments">
                                            <ExtraParams >
                                                <ext:Parameter Name="noteText" Value="#{newNoteText}.getValue()"   Mode="Raw" />
                                            </ExtraParams>
                                            <EventMask ShowMask="true" CustomTarget="={#{caseCommentGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                    </Items>
                                </ext:Panel>
                <ext:GridPanel AutoUpdateLayout="true"
                    ID="caseCommentGrid" Collapsible="false"
                    runat="server"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Layout="FitLayout" 
                    Scroll="Vertical" Flex="1"
                    Border="false"  MinHeight="220" MaxHeight="220"
                    Icon="User" DefaultAnchor="100%"    
                    ColumnLines="false" IDMode="Explicit" RenderXType="True">
                    <Store>
                        <ext:Store
                            ID="caseCommentStore"
                            runat="server"
                            RemoteSort="True"
                            RemoteFilter="true"
                            
                            PageSize="50" IDMode="Explicit" Namespace="App">
                           
                           
                            <Model>
                                <ext:Model ID="Model2" runat="server" IDProperty="seqNo">
                                    <Fields>

                                        
                                        <ext:ModelField Name="comment" />
                                        <ext:ModelField Name="date"  />
                                        <ext:ModelField Name="userName"  />
                                        <ext:ModelField Name="seqNo"  />
                                        <ext:ModelField Name="caseId"  />

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="recordId" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false ">
                            <Items>
                                
                              
                              
                               

                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel2" runat="server"  SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldrecordId %>" DataIndex="seqNo" Hideable="false" Width="75" Align="Center" >
                                
                                </ext:Column>
                            <ext:Column CellCls="cellLink" ID="ColEHName" MenuDisabled="true" runat="server" DataIndex="comment" Flex="7" Hideable="false">
                                <Renderer Handler=" var s = moment(record.data['date']);  return '<b>'+ record.data['userName'] +'</b>  - '+ s.calendar()+'<br />'+ record.data['comment'];">
                                </Renderer>
                                <Editor>
                                    
                                    <ext:TextArea runat="server" ID="notesEditor" name="comment" >
                                        
                                    </ext:TextArea>
                                </Editor>
                            </ext:Column>
                            



                            <ext:Column runat="server"
                                ID="Column2" Visible="false"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="60"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Fn="editRender" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="ColEHDelete" Flex="1" Visible="true"
                                Text=""
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer handler="return editRender() + '  '+ deleteRender()" />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="Column3" Visible="false"
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
                    <Plugins >
                        <ext:RowEditing runat="server" SaveHandler="validateSave" SaveBtnText="<%$ Resources:Common , Save %>" CancelBtnText="<%$ Resources:Common , Cancel %>"  >
                            
                            </ext:RowEditing>
                        
                    </Plugins>
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
                         <RowBodyDblClick Handler="App.caseCommentGrid.editingPlugin.cancelEdit();" />
                        <RowDblClick Handler="App.caseCommentGrid.editingPlugin.cancelEdit();" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuPCase">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="index" Value="rowIndex" Mode="Raw" />
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
                    <%--</SelectionModel>
                </ext:GridPanel>--%>
                           <%-- </Items>
                            </ext:Panel>

                        <ext:GridPanel Title="<%$ Resources: CaseAttachmentsTitle %>" runat="server" ID="filesGrid"  DefaultAnchor="100%" Layout="FitLayout"
                              PaddingSpec="0 0 1 0"
                    Header="false"
                   
                    Scroll="Vertical" Flex="1"
                    Border="false"  MinHeight="270" MaxHeight="270"
                    Icon="User"    
                    ColumnLines="false" IDMode="Explicit" RenderXType="True"
                            >
                             <Store>
                        <ext:Store
                            ID="filesStore"
                            runat="server"
                            
                            
                            PageSize="50" IDMode="Explicit" Namespace="App">
                            
                            <Model>
                                <ext:Model ID="Model3" runat="server" IDProperty="seqNo">
                                    <Fields>

                                        <ext:ModelField Name="recordId" />
                                        <ext:ModelField Name="seqNo" />
                                        <ext:ModelField Name="fileName" />
                                        <ext:ModelField Name="url" />
                                        <ext:ModelField Name="date" />
                                       

                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="seqNo" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                                <TopBar>
                                   
                        <ext:Toolbar ID="Toolbar5" runat="server" ClassicButtonStyle="false">
                            
                            <Items>
                               
                                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                     
                                    <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="AddAttachments">
                                            <EventMask ShowMask="true" CustomTarget="={#{filesGrid}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                </Items>
                            </ext:Toolbar>
                                    </TopBar>
                            <ColumnModel ID="ColumnModel3" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="Column4" MenuDisabled="true" runat="server" DataIndex="seqNo" Hideable="false" Width="75" Align="Center" />
                            <ext:Column CellCls="cellLink" ID="Column5" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldAttachmentName%>" DataIndex="fileName" Flex="2" Hideable="false" />
                            <ext:DateColumn  ID="dateCol" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Flex="2" Hideable="false" >
                                <Renderer Handler="var s = moment(record.data['date']);   return s.calendar();" />
                                </ext:DateColumn>
                          

                            <ext:Column runat="server"
                                ID="Column8" Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return attachRender() + '&nbsp&nbsp' + deleteRender(); " />

                            </ext:Column>
                            <ext:Column runat="server"
                                ID="Column9" Flex="1" Visible="false"
                                Text="  "
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
                                ID="Column10" Visible="false"
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

                        <ext:Toolbar ID="Toolbar6" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar3" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>
                
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents >
                        <CellClick  OnEvent="PoPuPAttachement" IsUpload="true" FormID="form1" >
                            
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                <ext:Parameter Name="path" Value="record.data['url']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView3" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel2" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                  <%--  </SelectionModel>
                </ext:GridPanel>--%>
           
                       
                        
                    </Items>
                </ext:TabPanel>
            </Items>
           
        </ext:Window>

     

    </form>
</body>
</html>

