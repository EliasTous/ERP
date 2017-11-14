<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeavePayments.aspx.cs" Inherits="AionHR.Web.UI.Forms.LeavePayments" %>


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
    <script type="text/javascript" src="Scripts/LoanRequests.js"></script>


    <script type="text/javascript" src="Scripts/common.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />

    <link href="CSS/fileinput.min.css" rel="stylesheet" />
    <link href="CSS/theme.css" rel="stylesheet" />

    <!-- load the JS files in the right order -->
    <script src="Scripts/fileinput.js" type="text/javascript"></script>
    <script src="Scripts/theme.js" type="text/javascript">  </script>
    <script src="Scripts/moment.js" type="text/javascript">  </script>
    <script src="Scripts/moment-timezone.js" type="text/javascript">  </script>

    <script type="text/javascript" src="Scripts/locales/ar.js?id=7"></script>
    <script type="text/javascript">
       
    </script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="currentCase" runat="server" />

       
        <ext:Hidden ID="CurrentLanguage" runat="server" />
        <ext:Hidden ID="CurrentAmountCurrency" runat="server" />
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="PFromNetSalary" Text="<%$ Resources: PFromNetSalary %>" runat="server" />
        <ext:Hidden ID="PFromBasicSalary" Text="<%$ Resources: PFromBasicSalary %>" runat="server"/>
        <ext:Hidden ID="PFromLoan" Text="<%$ Resources: PFromLoan %>" runat="server"  />
        <ext:Hidden ID="FixedAmount" Text="<%$ Resources: FixedAmount %>"  runat="server"  />
        <ext:Hidden ID="FixedPayment" Text="<%$ Resources: FixedPayment %>"  runat="server" />

          <ext:Hidden ID="titleBalanceError" runat="server" Text="<%$ Resources: titleBalanceError %>" />
        <ext:Hidden ID="titleBalanceErrorMessage" runat="server" Text="<%$ Resources: titleBalanceErrorMessage %>" />
     
        

        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="40" IDMode="Explicit" Namespace="App">
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
                      
                        <ext:ModelField Name="date" />
                        <ext:ModelField Name="effectiveDate" />
                          <ext:ModelField Name="paymentRef" />
                          <ext:ModelField Name="amount" />
                         <ext:ModelField Name="employeeName" IsComplex="true" />

                          <ext:ModelField Name="salary" />
                          <ext:ModelField Name="days" />
                          <ext:ModelField Name="earnedLeaves" />
                          <ext:ModelField Name="usedLeaves" />
                          <ext:ModelField Name="paidLeaves" />
                          <ext:ModelField Name="leaveBalance" />
                         <ext:ModelField Name="postingStatus" />
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
                <ext:DataSorter Property="employeeId" Direction="ASC" />
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
                    ColumnLines="True" IDMode="Explicit" RenderXType="True" ForceFit="true">

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



                                <%--  <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" EnablePosition="false" />
                                     

                                    </Content>

                                </ext:Container>--%>
                                <%--<ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="statusPref" Editable="false" EmptyText="<%$ Resources: FilterStatus %>">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="0"  />
                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldInProcess %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="3" />
                                        <ext:ListItem Text="<%$ Resources: FieldRejected %>" Value="-1" />
                                    </Items>
                        
                                </ext:ComboBox>--%>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="employeeFilter" Width="130" LabelAlign="Top"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" EmptyText="<%$ Resources: FieldEmployeeName%>"
                                    TriggerAction="Query" ForceSelection="false">
                                    <Store>
                                        <ext:Store runat="server" ID="Store2" AutoLoad="false">
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
                       
                                    <Items>
                                        <ext:ListItem Text="-----All-----" Value="0" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:Button runat="server" Text="<%$ Resources: Common,Go%>" MarginSpec="0 0 0 0" Width="100">
                                    <Listeners>
                                        <Click Handler="#{Store1}.reload();">
                                        </Click>
                                    </Listeners>
                                </ext:Button>


                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                             <ext:Column ID="Column5" DataIndex="paymentRef" Text="<%$ Resources: FieldReference%>" runat="server" />
                            <ext:Column ID="ColName" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="4">
                                <Renderer Handler=" return record.data['employeeName'].fullName; ">
                                </Renderer>
                            </ext:Column>
                           
                          
                            <ext:DateColumn ID="Column6" DataIndex="date" Text="<%$ Resources: FieldDate%>" runat="server" Width="100" />
                              <ext:DateColumn ID="DateColumn1" DataIndex="effectiveDate" Text="<%$ Resources: FieldEffectiveDate%>" runat="server" Width="100" />
                          <ext:Column ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSalary %>" DataIndex="salary" Hideable="false" Width="140"/>
                            <ext:Column ID="Column7" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDays %>" DataIndex="days" Hideable="false" Width="140"/>


                            <ext:Column ID="Column20" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEarnedLeaves %>" DataIndex="earnedLeaves" Hideable="false" Width="140"/>
                            <ext:Column ID="Column8" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldUsedLeaves %>" DataIndex="usedLeaves" Hideable="false" Width="140"/>
                            <ext:Column ID="Column9" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPaidLeaves %>" DataIndex="paidLeaves" Hideable="false" Width="140"/>
                            <ext:Column ID="Column10" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBalanceLeaves %>" DataIndex="leaveBalance" Hideable="false" Width="140"/>
                        <%--    <ext:Column ID="Column11" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldPostingStatus %>" DataIndex="postingStatus" Hideable="false" Width="140"/>--%>
                          
                               
                           
                           

                           <%-- <ext:Column ID="Column12" DataIndex="purpose" Text="<%$ Resources: FieldPurpose%>" runat="server" Flex="2" />--%>


                          

                           <%-- <ext:DateColumn ID="cc" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEffectiveDate %>" DataIndex="effectiveDate" Hideable="false" Width="120" Align="Center">
                            </ext:DateColumn>--%>
                            <%-- <ext:Column ID="ldMethodCo" DataIndex="ldMethod " Text="<%$ Resources: LoanCoverageType %>" runat="server" Flex="2"   >
                                 <Renderer Handler="return GetldMethodName(record.data['ldMethod']);" />
                                 </ext:Column>--%>

                           
                           



                            <ext:Column runat="server"
                                ID="colEdit" Visible="false"
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
                                Visible="false"
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
                                ID="colDelete"  Visible="true"
                                Text=""
                                MinWidth="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Handler="return editRender();" />

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
            Width="600"
            Height="526"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Maximizable="false"
            Resizable="false"
            Draggable="True"
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
                            BodyPadding="5"  AutoScroll="true"  Layout="TableLayout">
                            <Items>
                                 <ext:Panel runat="server" MarginSpec="0 20 0 0" ID="left">
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                 <ext:TextField runat="server" ID="paymentRef" Name="paymentRef" FieldLabel="<%$ Resources: FieldReference %>" />
                                  <ext:DateField ID="date" runat="server" FieldLabel="<%$ Resources:FieldDate%>" Name="date" AllowBlank="false"/>
                                   
                                   
                                <ext:ComboBox     AnyMatch="true" CaseSensitive="false"  runat="server" ID="employeeId"
                                    DisplayField="fullName"
                                    ValueField="recordId" Name="employeeId"
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
                                    <DirectEvents>
                                        <Change OnEvent="FillEmployeeInfo">
                                              <ExtraParams>
                                                <ext:Parameter Name="effectiveDate" Value="#{effectiveDate}.getValue()" Mode="Raw" />
                                                                                                 
                                            </ExtraParams>
                                        </Change>
                                    </DirectEvents>
                                    <Listeners>
                                        <Change Handler="#{days}.setValue(0); #{amount}.setValue(0);  "></Change>
                                    </Listeners>
                                </ext:ComboBox>
                                
                                  <ext:DateField AllowBlank="false"  runat="server" ID="effectiveDate" Name="effectiveDate" FieldLabel="<%$ Resources:FieldEffectiveDate%>"  SubmitValue="true"  >
                                      <DirectEvents>
                                          <Change OnEvent="FillEmployeeInfo" >
                                              <ExtraParams>
                                                <ext:Parameter Name="effectiveDate" Value="#{effectiveDate}.getValue()" Mode="Raw" />
                                                   <ext:Parameter Name="employeeId" Value="#{employeeId}.getValue()" Mode="Raw" />
                                                
                                            </ExtraParams>
                                          </Change>
                                          
                                      </DirectEvents>
                                      <Listeners>
                                        <Change Handler="#{days}.setValue(0); #{amount}.setValue(0);  "></Change>
                                    </Listeners>
                                
                                    </ext:DateField>
                          
                                   <ext:NumberField runat="server" ReadOnly="true" ID="earnedLeaves" Name="earnedLeaves" FieldLabel="<%$ Resources: FieldEarnedLeaves %>" />
                                   <ext:NumberField runat="server" ReadOnly="true" ID="usedLeaves" Name="usedLeaves" FieldLabel="<%$ Resources: FieldUsedLeaves %>" />
                                   <ext:NumberField runat="server" ReadOnly="true" ID="paidLeaves" Name="paidLeaves" FieldLabel="<%$ Resources: FieldPaidLeaves %>" />
                                   <ext:NumberField runat="server" ReadOnly="true" ID="leaveBalance" Name="leaveBalance" FieldLabel="<%$ Resources: FieldBalanceLeaves %>" />
                                 <ext:NumberField runat="server" ReadOnly="true" ID="salary" Name="salary" FieldLabel="<%$ Resources: FieldSalary %>"  />
                                  <ext:NumberField runat="server" ID="days" Name="days" FieldLabel="<%$ Resources: FieldDays %>"  MsgTarget="None">
                                      <Listeners  >
                                         <Change Handler=" this.next().setValue((this.prev().value/30)*this.value);"   ></Change>
                                      </Listeners>
                                       <Validator Handler=" if(this.value>0 && this.value<#{leaveBalance}.getValue()) return true;">
                                           
                                       </Validator>
                              
                                      </ext:NumberField>
                                
                                 <ext:NumberField runat="server" ReadOnly="true" ID="amount" Name="amount" FieldLabel="<%$ Resources: FieldAmount %>" AllowDecimals="false" >
                                   
                                     </ext:NumberField>
                              
                              

                               

                             

                                

                                <%--<ext:TextField ID="employeeName" runat="server" FieldLabel="<%$ Resources:FieldEmployeeName%>" Name="employeeName"   AllowBlank="false"/>--%>
                              

                               

<%--                                <ext:TextField  ID="amount" AllowBlank="false" runat="server" FieldLabel="<%$ Resources:FieldAmount%>" Name="amount">
                                <Listeners>
                                                <Change Handler="document.getElementById('amount').value=this.getValue(); this.next().setValue(this.value);" />
                                            </Listeners>
                                    <Validator Handler="  return !isNaN(this.value)&& this.value>0;" />
                                </ext:TextField>
--%>

                               </Items>
                                </ext:Panel>
                                                              
                                     <ext:Panel runat="server" MarginSpec="0 0 0 0" ID="rightPanel">
                                  <Items>
                                        <ext:DateField ReadOnly="true"  ID="hireDateDf" runat="server" FieldLabel="<%$ Resources:hireDate%>" Name="hireDate" AllowBlank="true">
                                    
                                </ext:DateField>
                                        
                                <ext:TextField  ReadOnly="true"  ID="serviceDuration" runat="server" FieldLabel="<%$ Resources:serviceDuration%>" Name="serviceDuration" AllowBlank="true">
                                    
                                </ext:TextField>
                                        
                                 <ext:TextField ReadOnly="true"  ID="departmentNameTx" runat="server" FieldLabel="<%$ Resources:departmentName%>" Name="departmentName" AllowBlank="true">
                                    
                                </ext:TextField>
                                 <ext:TextField ReadOnly="true"  ID="positionNameTx" runat="server" FieldLabel="<%$ Resources:positionName%>" Name="positionName" AllowBlank="true">
                                    
                                </ext:TextField>
                                  <ext:TextField ReadOnly="true"  ID="branchNameTx" runat="server" FieldLabel="<%$ Resources:branchName%>" Name="branchName" AllowBlank="true">
                                    
                                </ext:TextField>
                               
                                
                                   <ext:TextField  ReadOnly="true"  ID="nationalityTx" runat="server" FieldLabel="<%$ Resources:nationality%>" Name="nationality" AllowBlank="true">
                                    
                                </ext:TextField>
                                 
                                    
                              
                                  <ext:DateField  ReadOnly="true"   ID="lastLeaveStartDate" runat="server" FieldLabel="<%$ Resources:lastLeaveStartDateTitle%>" Name="lastLeaveStartDateTitle" AllowBlank="true">
                                    
                                </ext:DateField>
                                <ext:DateField  ReadOnly="true"   ID="lastLeaveEndDate" runat="server" FieldLabel="<%$ Resources:lastLeaveEndDateTitle%>" Name="lastLeaveEndDate" AllowBlank="true">
                                    
                                </ext:DateField>
                                 
                                 
                              
                                </Items>
                                     </ext:Panel>    
                                

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
                           
                      
                        
                      





                    </Items>
                </ext:TabPanel>
            </Items>

        </ext:Window>

    

    </form>
</body>
</html>
