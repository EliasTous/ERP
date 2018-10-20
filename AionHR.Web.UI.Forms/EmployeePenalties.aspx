<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeePenalties.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePenalties" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Branches.js"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" >
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            
        }
    </script>

</head>
<body style="background: url(Images/bg.png) repeat;" >
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />


     
                           
                             
     <ext:Store runat="server" ID="PenaltyStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                          
                                        </ext:Store>
                        
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="40"  Namespace="App" IDMode="Explicit" IsPagingStore="true">

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
                        <ext:ModelField Name="employeeName" ServerMapping="employeeName.fullName" />
                        <ext:ModelField Name="penaltyName" />
                        <ext:ModelField Name="employeeId" />
                        <ext:ModelField Name="penaltyId" />
                        <ext:ModelField Name="Date" />
                        <ext:ModelField Name="apStatus" />
                        <ext:ModelField Name="notes" />



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
                                  <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <%--<uc:dateRange runat="server" ID="dateRange1" />--%>
                                        <uc:employeeCombo runat="server" ID="employeeFilter" />
                                    </Content>
                                      </ext:Container>
                                    <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                     <ext:ComboBox StoreID="PenaltyStore" AnyMatch="true" CaseSensitive="false" Enabled="false" ValueField="recordId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="PenaltyFilter" Name="PenaltyFilter" FieldLabel="<%$ Resources:FieldPenaltyName%>" LabelWidth="75" SubmitValue="true"  />

                                    <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button ID="BTGo" runat="server" Text="<%$Resources:Common ,Go %>">       
                                     <Listeners>
                                        <Click Handler="CheckSession();#{Store1}.reload();" />
                                    </Listeners>                           
                                   
                                </ext:Button>
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

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server"  DataIndex="recordId"   />
                              <ext:Column Visible="false" ID="ColemployeeId" MenuDisabled="true" runat="server"  DataIndex="employeeId" />
                             <ext:Column Visible="false" ID="ColpenaltyId" MenuDisabled="true" runat="server"  DataIndex="penaltyId" />
                            <ext:Column ID="ColEmployeeName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEmployeeName%>" DataIndex="employeeName"  Flex="2" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColPenaltyName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPenaltyName%>" DataIndex="penaltyName" Flex="2" Hideable="false"/>
                            
                            <ext:DateColumn  ID="ColDate" MenuDisabled="true" runat="server" DataIndex="Date" Text="<%$ Resources: FieldDate%>" Flex="1" Hideable="false" />
                          
                                  <ext:Column CellCls="cellLink" ID="ColNotes" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldNotes%>" DataIndex="notes" Flex="2" Hideable="false"/>



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
                                Text="<%$ Resources:Common, Edit %>"
                                Width="80"
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
                        <Render Handler="CheckSession(); this.on('cellclick', cellClick);" />


                        <AfterLayout Handler="item.getView().setScrollX(30);" />

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
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="BasicInfoTab"
                            runat="server" DefaultButton="SaveButton"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" Hidden="true" runat="server" FieldLabel="<%$ Resources:FieldrecordId%>" Disabled="true" DataIndex="recordId" />
                                <ext:DateField ID="date" runat="server" FieldLabel="<%$ Resources:FieldDate%>" DataIndex="date" Name="date"  />
                             


                                <ext:ComboBox StoreID="PenaltyStore" AnyMatch="true" CaseSensitive="false" Enabled="false" ValueField="recordId" AllowBlank="true" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" runat="server" ID="penaltyId" Name="penaltyId" FieldLabel="<%$ Resources:FieldPenaltyName%>" SimpleSubmit="true">
                                   
                                   
                                </ext:ComboBox>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="employeeId" Name="employeeId"
                                    DisplayField="fullName"
                                    ValueField="recordId"
                                    TypeAhead="false"
                                    FieldLabel="<%$ Resources: FieldEmployeeName%>"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" AllowBlank="false"
                                    TriggerAction="Query" ForceSelection="true">
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

                                 <ext:TextArea ID="notes"  runat="server" FieldLabel="<%$ Resources:FieldNotes%>"  DataIndex="notes"  />


                            </Items>

                        </ext:FormPanel>
                            <ext:GridPanel
                            ID="ApprovalsGridPanel"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            MaxHeight="350"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                             Title="<%$ Resources: Approvals %>"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True" >
                            
                            <Store>
                                <ext:Store runat="server" ID="ApprovalStore" OnReadData="ApprovalsStore_ReadData">
                                    <Model>
                                        <ext:Model runat="server">
                                            <Fields>
                                                <ext:ModelField Name="approverName" IsComplex="true" />
                                                <ext:ModelField Name="departmentName" />
                                                 <ext:ModelField Name="loanId" />
                                                <ext:ModelField Name="approverId" />
                                                <ext:ModelField Name="status" />
                                                 <ext:ModelField Name="statusString" />
                                                 <ext:ModelField Name="notes" />
                                                
                                                
                                                
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>


                            <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                <Columns>
                                    <ext:Column ID="loanId" Visible="false" DataIndex="loanId" runat="server" />
                                    <ext:Column ID="approverId" Visible="false" DataIndex="approverId" runat="server" />
                                 
                                        <ext:Column ID="Column8" DataIndex="approverName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="1">
                                           <Renderer Handler=" return record.data['approverName'].fullName; ">
                                           </Renderer>
                                         </ext:Column>
                                    <ext:Column ID="departmentName" DataIndex="departmentName" Text="<%$ Resources: FieldDepartment%>" runat="server" Flex="1"/>
                                    <ext:Column ID="lAstatus" Visible="true" DataIndex="statusString" runat="server" Width="100" text="<%$ Resources: FieldStatus%> " >
                                       
                                    </ext:Column>
                                      
                                    <ext:Column ID="LAnotes" DataIndex="notes" Text="<%$ Resources: ReturnNotes%>" runat="server" Flex="2">
                                       
                                    </ext:Column>
                                   



                                </Columns>
                            </ColumnModel>

                            <%--  alert(last.dayId);
                                                        if(App.leaveRequest1_shouldDisableLastDay.value=='1')
                                                             if(last.dayId==rec.data['dayId'])  
                                                                        this.setDisabled(false);
                                                            else this.setDisabled(true); 
                                                        else
                                                            this.setDisabled(true); --%>
                            
                           <Listeners>
                               <Activate Handler="#{ApprovalStore}.reload();" />
                           </Listeners>

                            <View>
                                <ext:GridView ID="GridView4" runat="server" />
                            </View>


                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectionModel3" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                            </SelectionModel>
                         
                     </ext:GridPanel>

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;} " />
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
        </ext:Window>



    </form>
</body>
</html>

