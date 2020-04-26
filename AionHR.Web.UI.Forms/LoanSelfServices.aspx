<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanSelfServices.aspx.cs" Inherits="Web.UI.Forms.LoanSelfServices" %>



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

    <script type="text/javascript" src="Scripts/locales/ar.js?id=10"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="currentCase" runat="server" />

        <ext:Hidden ID="StatusNew" runat="server" Text="<%$ Resources:FieldNew %>" />
        <ext:Hidden ID="StatusInProcess" runat="server" Text="<%$ Resources: FieldInProcess %>" />
        <ext:Hidden ID="StatusApproved" runat="server" Text="<%$ Resources: FieldApproved %>" />
        <ext:Hidden ID="StatusRejected" runat="server" Text="<%$ Resources: FieldRejected %>" />
        <ext:Hidden ID="EmptyText" runat="server" Text="<%$ Resources: EmptyComment %>" />
        <ext:Hidden ID="CurrentLanguage" runat="server" />
        <ext:Hidden ID="CurrentAmountCurrency" runat="server" />
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="PFromNetSalary" Text="<%$ Resources: PFromNetSalary %>" runat="server" />
        <ext:Hidden ID="PFromBasicSalary" Text="<%$ Resources: PFromBasicSalary %>" runat="server"/>
        <ext:Hidden ID="PFromLoan" Text="<%$ Resources: PFromLoan %>" runat="server"  />
        <ext:Hidden ID="FixedAmount" Text="<%$ Resources: FixedAmount %>"  runat="server"  />
        <ext:Hidden ID="FixedPayment" Text="<%$ Resources: FixedPayment %>"  runat="server" />

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
                        <ext:ModelField Name="loanRef" />
                        <ext:ModelField Name="ltId" />
                        <ext:ModelField Name="date" />
                        <ext:ModelField Name="effectiveDate" />
                        <ext:ModelField Name="branchId" />
                        <ext:ModelField Name="branchName" />
                        <ext:ModelField Name="purpose" />
                        <ext:ModelField Name="status" />
                        <ext:ModelField Name="currencyId" />
                        <ext:ModelField Name="amount" />
                        <ext:ModelField Name="payments" />
                        <ext:ModelField Name="ltName" />
                        <ext:ModelField Name="currencyRef" />
                        <ext:ModelField Name="deductedAmount" />
                         <ext:ModelField Name="ldMethod" />
                         <ext:ModelField Name="ldValue" />
                        <ext:ModelField Name="employeeName"  />
                        <ext:ModelField Name="apStatus"  />

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
                           



                              
                                
                         
                               


                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                          <%--  <ext:Column ID="ColName" DataIndex="employeeName" Text="<%$ Resources: FieldEmployeeName%>" runat="server" Flex="4">
                            
                                </Renderer>
                            </ext:Column>--%>
                          <%--  <ext:Column ID="Column5" DataIndex="loanRef" Text="<%$ Resources: FieldReference%>" runat="server" />--%>
                            <ext:Column ID="Column7" DataIndex="ltName" Text="<%$ Resources: FieldLtName%>" runat="server" Flex="1" />
                           <%-- <ext:Column ID="Column6" DataIndex="branchName" Text="<%$ Resources: FieldBranch%>" runat="server" Flex="1" />--%>

                          <%--  <ext:DateColumn ID="c" DataIndex="effectiveDate" Text="<%$ Resources: FieldDate%>" runat="server" Width="100" />--%>

                            <ext:Column ID="Column20" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldAmount %>" DataIndex="amount" Hideable="false" Width="140">
                                <Renderer Handler="return record.data['amount']; "></Renderer>
                            </ext:Column>
                            <%--<ext:Column ID="Column4" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDeductedAmount %>" DataIndex="deductedAmount" Hideable="false" Width="140">
                                <Renderer Handler="return record.data['deductedAmount'] ;"></Renderer>
                            </ext:Column>--%>
                             <ext:Column ID="Column1" DataIndex="currencyRef" Text="<%$ Resources: FieldSACurrencyName%>" runat="server" Flex="2" />
                         <ext:Column ID="Column12" DataIndex="purpose" Text="<%$ Resources: FieldPurpose%>" runat="server" Flex="2" />


                            <ext:Column ID="colStatus" DataIndex="status" Text="<%$ Resources: FieldStatus%>" runat="server" Width="100">
                                <Renderer Handler="return GetStatusName(record.data['apStatus']);" />
                            </ext:Column>

                            <ext:DateColumn ID="cc" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldEffectiveDate %>" DataIndex="effectiveDate" Hideable="false" Width="120" Align="Center">
                            </ext:DateColumn>
                            <%-- <ext:Column ID="ldMethodCo" DataIndex="ldMethod " Text="<%$ Resources: LoanCoverageType %>" runat="server" Flex="2"   >
                                 <Renderer Handler="return GetldMethodName(record.data['ldMethod']);" />
                                 </ext:Column>--%>

                            <%-- <ext:Column ID="ldValueCo" DataIndex="ldValue" Text="<%$ Resources: PaymentValue %>" runat="server" Flex="2" />--%>
                             
                           
                               <ext:Column ID="payments" DataIndex="ldValue" Text="<%$ Resources: FieldPayments %>" runat="server" Flex="2" />


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
                                <Renderer Handler="if (record.data['apStatus']=='1') return editRender()+ '&nbsp&nbsp'+ deleteRender();" />

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
            Height="350"
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
                            BodyPadding="5"  AutoScroll="true">
                            <Items>
                                <ext:TextField ID="recordId" runat="server" Name="recordId" Hidden="true" />
                                <ext:ComboBox Hidden="true"   AnyMatch="true" CaseSensitive="false"  runat="server" ID="employeeId"
                                    DisplayField="fullName"
                                    ValueField="recordId" Name="employeeId"
                                    TypeAhead="false" AllowBlank="true"
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
                                <ext:TextField Hidden="true" AllowBlank="true" runat="server" ID="loanRef" Name="loanRef" FieldLabel="<%$ Resources: FieldReference %>" />

                                <ext:ComboBox Hidden="true"   AnyMatch="true" CaseSensitive="false"  Enabled="false" runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" SimpleSubmit="true">
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

                                <ext:ComboBox  AnyMatch="true"  CaseSensitive="false"  runat="server" ID="ltId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    DisplayField="name"
                                    ValueField="recordId" AllowBlank="false"
                                    FieldLabel="<%$ Resources: FieldLtName %>">
                                    <Store>
                                        <ext:Store runat="server" ID="ltStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                           <ext:ModelField Name="ldMethod" />
                                                          <ext:ModelField Name="ldValue" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>


                                        </ext:Store>

                                    </Store>
                                  <Listeners>
                                      <Select Handler="record = this.findRecordByValue(this.value); 
                                          index = this.getStore().indexOf(record);
                                          App.ldMethod.setValue(this.getStore().getAt(index).data['ldMethod']);
                                          App.ldValue.setValue(this.getStore().getAt(index).data['ldValue']); " />
                                  </Listeners>
                                  
                                </ext:ComboBox>

                                     <ext:ComboBox  ReadOnly="true"   AnyMatch="true" CaseSensitive="false"  runat="server" ID="ldMethod" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    FieldLabel="<%$ Resources: LoanCoverageType %>" AllowBlank="true" SubmitValue="true">
                                    <Items>
                                       
                                              
                                                <ext:ListItem Text="<%$ Resources: PFromNetSalary %>" Value="1"  />
                                                <ext:ListItem Text="<%$ Resources: PFromBasicSalary %>" Value="2"  />
                                                <ext:ListItem Text="<%$ Resources: PFromLoan %>" Value="3" />
                                                <ext:ListItem Text="<%$ Resources: FixedAmount %>" Value="4"  />
                                                <ext:ListItem Text="<%$ Resources: FixedPayment %>" Value="5" />

                                            </Items>
                                          
                                        </ext:ComboBox>
                               

                                <%--<ext:TextField ID="employeeName" runat="server" FieldLabel="<%$ Resources:FieldEmployeeName%>" Name="employeeName"   AllowBlank="false"/>--%>
                                <ext:DateField Hidden="true"  ID="date" runat="server" FieldLabel="<%$ Resources:FieldDate%>" Name="date" AllowBlank="true" >
                                     <CustomConfig>
                                         <ext:ConfigItem Name="endDateField" Value="effectiveDate" Mode="Value" />
                                      </CustomConfig>
                                    </ext:DateField>

                                <ext:ComboBox     AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="reference" ID="currencyId" Name="currencyId" FieldLabel="<%$ Resources:FieldSACurrencyName%>" SimpleSubmit="true">

                                    <Store>
                                        <ext:Store runat="server" ID="currencyStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="reference" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                               
                                    <Listeners>
                                        <Change Handler="CurrentAmountCurrency.value = App.currencyId.getRawValue();" />
                                      
                                    </Listeners>
                                </ext:ComboBox>

                                <ext:TextField   ID="amount" AllowBlank="false" runat="server" FieldLabel="<%$ Resources:FieldAmount%>" Name="amount">
                                   <Validator Handler="return !isNaN(this.value);" /> 
                                    <%-- <Listeners>
                                                <Change Handler="document.getElementById('amount').value=this.getValue(); this.next().setValue(this.value);" />
                                            </Listeners>--%>
                                    
                                </ext:TextField>


                                <ext:TextArea   ID="purpose" runat="server" FieldLabel="<%$ Resources:FieldPurpose%>" Name="purpose" AllowBlank="false" />
                                 <ext:TextField Hidden="true"   ID="purposeField" InputType="Password" Visible="false" runat="server" FieldLabel="<%$ Resources:FieldPurpose%>" Name="purpose" AllowBlank="true" />
                                <ext:ComboBox Hidden="true"   AnyMatch="true" CaseSensitive="false"  runat="server" ID="apStatus" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"
                                    FieldLabel="<%$ Resources: FieldStatus %>" AllowBlank="true" SubmitValue="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldInProcess %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="3" />
                                        <ext:ListItem Text="<%$ Resources: FieldRejected %>" Value="-1" />
                                    </Items>
                                    <Listeners>
                                        <Change Handler="if(this.value==3) {this.next().setReadOnly(false); this.next().setValue(new Date());} else {this.next().setReadOnly(true); this.next().clear();}">
                                        </Change>
                                    </Listeners>
                                </ext:ComboBox>

                                <ext:DateField   AllowBlank="false"  runat="server" ID="effectiveDate" Name="effectiveDate" FieldLabel="<%$ Resources:FieldEffectiveDate%>" Vtype="daterange" >
                                    <CustomConfig>
                                  <ext:ConfigItem Name="startDateField" Value="date" Mode="Value" />
                                        </CustomConfig>
                                    </ext:DateField>
                                
                                   
                                        <ext:NumberField Hidden="false" Width="400"  runat="server"  ID="ldValue" Name="ldValue" FieldLabel="<%$ Resources: PaymentValue %>"  AllowBlank="true" >
                                        
                               
                                            </ext:NumberField>
                                

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
                                                <ext:Parameter Name="ldMethod" Value="#{ldMethod}.getValue()" Mode="Raw" />
                                                  <ext:Parameter Name="ldValue" Value="#{ldValue}.getValue()" Mode="Raw" />
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
