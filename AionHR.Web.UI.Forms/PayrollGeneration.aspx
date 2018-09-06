<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollGeneration.aspx.cs" Inherits="AionHR.Web.UI.Forms.PayrollGeneration" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/PayrollGeneration.js?id=10"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" src="Scripts/moment.js"></script>
    <script type="text/javascript">
   function openInNewTab() {
            window.document.forms[0].target = '_blank';

        }

        function setStartEnd(s, e) {

            App.startDate.setValue(s.trim());
            App.endDate.setValue(e.trim());
            var d = moment(e, document.getElementById("DateFormat").value);
            App.payDate.setValue(d.toDate());
        }
        function CalcENSum() {
            if (App.entitlementDisabled.value == 'True')
                return '****';
            var enSum = 0;
            App.entitlementsGrid.getStore().each(function (record) {
                enSum += record.data['amount'];
            });
           
            return enSum;


        }
        function CalcDESum() {
            if (App.deductionDisabled.value == 'True')
                return '****';
            var deSum = 0;
            App.deductionGrid.getStore().each(function (record) {
                deSum += record.data['amount'];
            });

            return deSum;


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

        <ext:Hidden ID="DateFormat" runat="server" />
        <ext:Hidden ID="CurrentPayId" runat="server" />
        <ext:Hidden ID="CurrentPayId1" runat="server" />
        <ext:Hidden ID="CurrentSeqNo" runat="server" />
        <ext:Hidden ID="CurrentCurrencyRef" runat="server" />
        <ext:Hidden ID="PeriodStatus0" runat="server" Text="<%$ Resources: Status0 %>" />
        <ext:Hidden ID="PeriodStatus1" runat="server" Text="<%$ Resources: Status1 %>" />
        <ext:Hidden ID="PeriodStatus2" runat="server" Text="<%$ Resources: Status2 %>" />
        <ext:Hidden ID="total" runat="server" Text="<%$ Resources: TotalText %>" />
        <ext:Hidden ID="IsPayrollPosted" runat="server" />
        <ext:Hidden ID="entitlementDisabled" runat="server" />
        <ext:Hidden ID="deductionDisabled" runat="server" />
        <ext:Hidden ID="payRefHidden" runat="server" />
         <ext:Hidden ID="salaryTypeHidden" runat="server" />
        <ext:Hidden ID="fiscalYearHidden" runat="server" />
         <ext:Hidden ID="CurrentPayRef" runat="server" />

      
        <ext:Viewport ID="Viewport1" runat="server" Layout="CardLayout" ActiveIndex="0">
            
            <Items>
                <ext:Panel runat="server" ID="payrolls" Layout="FitLayout">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button runat="server" ID="AddButton" Text="<%$ Resources:Common,Add %>" Icon="Add">
                                    <Listeners>
                                        <Click Handler="CheckSession();App.direct.AddPayroll();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" EmptyText="<%$ Resources: FieldYear %>" Name="year" runat="server" DisplayField="fiscalYear" ValueField="fiscalYear" ID="year">
                                    <Store>
                                        <ext:Store runat="server" ID="yearStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>

                                                        <ext:ModelField Name="fiscalYear" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                      <%--  <Select Handler="App.salaryTypeFilter.setValue(5); App.payrollsStore.reload();">--%>
                                               <Select Handler="App.salaryTypeFilter.setValue(5); App.payrollsStore.reload();">
                                        </Select>
                                        
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="salaryTypeFilter"  Name="salaryTypeFilter" runat="server" EmptyText="<%$ Resources:FieldPeriodType%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: SalaryWeekly%>" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryBiWeekly%>" Value="3"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryFourWeekly%>" Value="4"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryMonthly%>" Value="5"></ext:ListItem>
                                    </Items>
                                    <Listeners>
                                        <Select Handler="App.payrollsStore.reload();">
                                        </Select>
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="status"  runat="server" EmptyText="<%$ Resources:FieldStatus%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: Status0%>" Value="0"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Status1%>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Status2%>" Value="2"></ext:ListItem>

                                    </Items>
                                    <Listeners>
                                        <Select Handler="App.payrollsStore.reload();">
                                        </Select>
                                    </Listeners>
                                </ext:ComboBox>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel  runat="server" ID="payrollsGrid" Scroll="Vertical" Layout="FitLayout"  SortableColumns="false"  EnableColumnResize="false" EnableColumnHide="false">
                            
                            <Store>
                                <ext:Store runat="server" ID="payrollsStore" OnReadData="payrollsStore_ReadData">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="recordId">
                                            <Fields>
                                                <ext:ModelField Name="recordId" />
                                                <ext:ModelField Name="fiscalYear" />
                                                <ext:ModelField Name="payRef" />
                                                <ext:ModelField Name="salaryType" />
                                                <ext:ModelField Name="periodId" />
                                                <ext:ModelField Name="status" />
                                                <ext:ModelField Name="startDate" />
                                                <ext:ModelField Name="endDate" />
                                                <ext:ModelField Name="notes" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column Visible="false" runat="server" ID="recordHeaderID" text="" DataIndex="recordId" Width="200" />
                                    <ext:Column runat="server" ID="c1" Text="<%$ Resources: FieldRef%>" DataIndex="payRef" Width="200"  />
                                    <ext:DateColumn runat="server" ID="periodFrom" Text="<%$ Resources: FieldFrom%>" DataIndex="startDate" Width="150" />

                                    <ext:DateColumn runat="server" ID="periodTo" Text="<%$ Resources: FieldTo%>" DataIndex="endDate" Width="150" />

                                    <ext:Column runat="server" DataIndex="status" Text="<%$ Resources: FieldStatus%>" Flex="1">
                                        <Renderer Handler="return getStatusText(record.data['status']);" />
                                    </ext:Column>
                                       <ext:Column runat="server" ID="Column2" Text="<%$ Resources: FieldNotes%>" DataIndex="notes"  Flex="2" />
                                     
                                    <ext:Column runat="server"
                                        ID="colEdit" Visible="true"
                                        Text=""
                                        Width="130"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                        <Renderer Handler="var d =attachRender(record.data['status']);if (record.data['status']!=2){ d+='&nbsp;&nbsp;'+editRender()+ '&nbsp;&nbsp;'+ deleteRender();} return d ;" />
                                   
                                      

                                    </ext:Column>
                                    
                              
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <Render Handler="this.on('cellclick', cellClick);" />
                        <%--        <AfterRender Handler="App.year.setValue(new Date().getFullYear()); App.salaryTypeFilter.setValue(5); App.status.setValue(2); App.payrollsStore.reload();" />--%>
                               <AfterRender Handler="App.year.setValue(new Date().getFullYear()); App.payrollsStore.reload();" />
                                <AfterLayout Handler="App.payrollsStore.reload();" />
                                
                            </Listeners>
                            <DirectEvents> 
                                <CellClick OnEvent="PoPuPHeader" IsUpload="true">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                        <ext:Parameter Name="salaryType" Value="record.data['salaryType']" Mode="Raw" />
                                         <ext:Parameter Name="fiscalYear" Value="record.data['fiscalYear']" Mode="Raw" />

                                        <ext:Parameter Name="payRef" Value="record.data['payRef']" Mode="Raw" />
                                        <ext:Parameter Name="status" Value="record.data['status']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView1" runat="server" />
                            </View>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel
                    ID="EditRecordWindow"
                    runat="server"
                    Icon="PageEdit"
                    Header="false"
                    Width="450"
                    Height="330"
                    AutoShow="false"
                    Modal="true"
                    Hidden="False"
                    Layout="Fit">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
                                    <Listeners>
                                        <Click Handler="CheckSession(); App.direct.PayrollPages();" />
                                    </Listeners>


                                </ext:Button>
                              
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>

                        <ext:FormPanel DefaultButton="SaveButton"
                            ID="BasicInfoTab"
                            runat="server"
                            Header="false"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Listeners>
                                <AfterLayout Handler="CheckSession(); App.fiscalYear.setValue(new Date().getFullYear()); App.salaryType.setValue(5);App.fiscalPeriodsStore.reload();" />
                            </Listeners>
                            <Items>
                                <ext:TextField runat="server" ID="payRefTF" Name="payRef" FieldLabel="<%$ Resources: FieldPayRef %>" AllowBlank="true" />
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldYear %>" Name="fiscalYear" runat="server" DisplayField="fiscalYear" ValueField="fiscalYear" ID="fiscalYear">
                                    <Store>
                                        <ext:Store runat="server" ID="fiscalyearStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>

                                                        <ext:ModelField Name="fiscalYear" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Listeners>
                                        <Select Handler="App.fiscalPeriodsStore.reload(); ">
                                        </Select>
                                    </Listeners>
                                    <DirectEvents>
                                        <Select OnEvent="UpdateStartEndDate">
                                            <ExtraParams>
                                                <ext:Parameter Name="period" Value="#{periodId}.getRawValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="salaryType" Name="salaryType" runat="server" FieldLabel="<%$ Resources:FieldPeriodType%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: SalaryWeekly%>" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryBiWeekly%>" Value="3"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryFourWeekly%>" Value="4"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: SalaryMonthly%>" Value="5"></ext:ListItem>
                                    </Items>
                                    <Listeners>
                                        <Select Handler="App.fiscalPeriodsStore.reload(); ">
                                        </Select>
                                    </Listeners>
                                       <DirectEvents>
                                        <Select OnEvent="UpdateStartEndDate">
                                            <ExtraParams>
                                                <ext:Parameter Name="period" Value="#{periodId}.getRawValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldPeriod %>" Name="periodId" DisplayField="name" ValueField="recordId" runat="server" ID="periodId">
                                    <Store>
                                        <ext:Store runat="server" ID="fiscalPeriodsStore" OnReadData="fiscalPeriodsStore_ReadData">
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
                                   <%-- <Listeners>
                                        <Select Handler="App.direct.UpdateStartEndDate();">
                                        </Select>
                                    </Listeners>--%>
                                       <DirectEvents>
                                        <Select OnEvent="UpdateStartEndDate">
                                            <ExtraParams>
                                                <ext:Parameter Name="period" Value="#{periodId}.getRawValue()" Mode="Raw" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:ComboBox>
                                <ext:TextField runat="server" ReadOnly="true" ID="startDate" Name="startDate" FieldLabel="<%$ Resources: FieldFrom %>" AllowBlank="false" />
                                <ext:TextField runat="server" ReadOnly="true" ID="endDate" Name="endDate" FieldLabel="<%$ Resources: FieldTo %>" AllowBlank="false"  />
                                <ext:DateField runat="server" ID="payDate" Name="payDate" FieldLabel="<%$ Resources: FieldPayDate %>" AllowBlank="false" />
                                <ext:TextArea runat="server" ID="notes" Name="notes" FieldLabel="<%$ Resources: FieldNotes %>" />
                            </Items>

                        </ext:FormPanel>


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

                                        <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>

                    </Buttons>
                </ext:Panel>
                <ext:Panel ID="detailsPanel" runat="server"  Layout="fit">
                    
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false" Dock="Top">

                            <Items>
                                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Back %>" Icon="PageWhiteGo">
                                    <Listeners>
                                        <Click Handler="CheckSession(); App.direct.PayrollPages();" />
                                    </Listeners>


                                </ext:Button>
                                  <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" EnablePosition="false" EnableDivision="false" />

                                    </Content>

                                </ext:Container>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:employeeCombo runat="server" ID="employeeCombo1"/>

                                    </Content>

                                </ext:Container>
                               
                                   <ext:Button runat="server" Text="<%$ Resources: Common,Go%>" MarginSpec="0 0 0 0" Width="100">
                                    <Listeners>
                                        <Click Handler="#{Store1}.reload(); ">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                 <ext:ToolbarSeparator runat="server" />
                                       <ext:Button runat="server" Icon="Printer">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server"  Text="<%$Resources:Common,Print%>" AutoPostBack="true" OnClick="printBtn_Click" OnClientClick="openInNewTab();"  >
                                            
                                                    <Listeners>
                                                        <Click Handler="openInNewTab();" />
                                                    </Listeners>
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server"  Text="Pdf" AutoPostBack="true" OnClick="ExportPdfBtn_Click"  >
                                            
                                                    
                                                </ext:MenuItem>
                                                <ext:MenuItem runat="server"  Text="Excel" AutoPostBack="true" OnClick="ExportXLSBtn_Click"  >
                                            
                                                    
                                                </ext:MenuItem>
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:Button>
                                <ext:ToolbarSeparator runat="server" />
                                  <ext:Button runat="server" Text="<%$ Resources: Common,Export%>" MarginSpec="0 0 0 0" >
                                  <DirectEvents>
                                      <Click OnEvent="ExportToExcel" />
                                  </DirectEvents>
                                </ext:Button>

                               
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                         
                
                  
                        <ext:GridPanel runat="server" ID="employeePayrolls" SortableColumns="false"
                                      EnableColumnResize="true" 
                                    EnableColumnHide="false"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                   
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                   
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">
                            <Store>
                                <ext:Store ID="Store1" runat="server" OnReadData="Store1_ReadData">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="seqNo">
                                            <Fields>
                                                <ext:ModelField Name="basicAmount" />
                                                <ext:ModelField Name="taxAmount" />
                                                  <ext:ModelField Name="cssAmount" />
                                                 <ext:ModelField Name="essAmount" />

                                                <ext:ModelField Name="netSalary" />
                                                <ext:ModelField Name="name" IsComplex="true" />
                                               <%-- <ext:ModelField Name="branchName" />--%>
                                                <ext:ModelField Name="departmentName" />
                                                <ext:ModelField Name="currencyName" />
                                                <ext:ModelField Name="currencyRef" />
                                                <ext:ModelField Name="calendarDays" />
                                                <ext:ModelField Name="calendarMinutes" />
                                                <ext:ModelField Name="workingDays" />
                                                <ext:ModelField Name="workingMinutes" />
                                                <ext:ModelField Name="eAmount" />
                                                <ext:ModelField Name="dAmount" />
                                                <ext:ModelField Name="seqNo" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column runat="server" DataIndex="name" Text="<%$ Resources: FieldEmployee%>" Flex="2">
                                        <Renderer Handler="return record.data['name'].fullName;" />
                                    </ext:Column>
                                      <ext:Column runat="server" ID="seqNoEM" DataIndex="seqNo" Hidden="true" />
                         <%--           <ext:Column runat="server" DataIndex="branchName" Text="<%$ Resources: FieldBranch%>" Flex="1" />--%>
                                    <ext:Column runat="server" DataIndex="departmentName" Text="<%$ Resources: FieldDepartment%>" Flex="1" />
                                    <ext:Column runat="server" DataIndex="calendarDays" Text="<%$ Resources: FieldCalDays%>" Width="100" />
                                    
                                    <ext:Column runat="server" DataIndex="workingDays" Text="<%$ Resources: FieldDays%>" Width="100" />
                                    <ext:Column runat="server" DataIndex="basicAmount" Text="<%$ Resources: FieldBasicAmount%>" >
                                        <Renderer Handler="return record.data['currencyRef']+'&nbsp; '+ record.data['basicAmount'] ;" />
                                        </ext:Column>
                                    
                                    <ext:Column runat="server" DataIndex="eAmount" Text="<%$ Resources: Entitlements%>" >
                                        <Renderer Handler="if(record.data['eAmount']==0) return '-';return  record.data['currencyRef'] +' &nbsp;'+ record.data['eAmount'];" />
                                        </ext:Column>
                                    
                                    <ext:Column runat="server" DataIndex="dAmount" Text="<%$ Resources: Deductions%>" >
                                        <Renderer Handler="if(record.data['dAmount']==0) return '-'; return '- '+record.data['currencyRef']+'&nbsp; '+ record.data['dAmount'] ;" />
                                     </ext:Column>
                                    <ext:Column runat="server" DataIndex="netSalary" Text="<%$ Resources: FieldNetSalary%>" >
                                        <Renderer Handler="if(record.data['netSalary']==0) return '-'; return record.data['currencyRef'] +'&nbsp; '+ record.data['netSalary'] ;" />
                                        </ext:Column>
                                    <ext:Column runat="server" DataIndex="cssAmount" Text="<%$ Resources: FieldCompanySocialSecurity%>" >
                                      <Renderer Handler="if(record.data['cssAmount']==0) return '-';return record.data['currencyRef'] +'&nbsp;'  + record.data['cssAmount']; " />
                                        </ext:Column>
                                     <ext:Column runat="server" DataIndex="essAmount" Text="<%$ Resources: FieldEmployeeSocialSecurity%>" >
                                      <Renderer Handler="if(record.data['essAmount']==0) return '-';return record.data['currencyRef'] +'&nbsp;'  + record.data['essAmount']; " />
                                        </ext:Column>
                                    <ext:Column runat="server"
                                        ID="Column1" Visible="true"
                                        Text=""
                                        Width="100"
                                        Hideable="false"
                                        Align="Center"
                                        Fixed="true"
                                        Filterable="false"
                                        MenuDisabled="true"
                                        Resizable="false">

                                      <%--  <Renderer Handler="  var d = (#{IsPayrollPosted}.value=='2')?'&nbsp;&nbsp;&nbsp;&nbsp;':editRender();return d+'&nbsp;&nbsp;' +attachRender1();" />--%>
                                      <Renderer Handler="
                                          var d;
                                        if (#{IsPayrollPosted}.value=='2') 
                                        d='&nbsp;&nbsp;' +attachRender1();
                                        else
                                        {
                                         
                                          d=editRender();
                                          d+='&nbsp;&nbsp;';
                                          d+=attachRender1();
                                           d+='&nbsp;&nbsp;';
                                          d+=deleteRender();
                                          d+='&nbsp;&nbsp;';
                                      
                                        }
                                        return d; "></Renderer>
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                 
                                       <BeforeRender  Handler="this.setHeight(App.detailsPanel.getHeight());" />
                                <Render Handler="this.on('cellclick', cellClick);" />
                                
                         
                            </Listeners>
                            <DirectEvents>
                                <CellClick OnEvent="PoPuPEM">
                                    <EventMask ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
                                        <ext:Parameter Name="eAmount" Value="record.data['eAmount']" Mode="Raw" />
                                        <ext:Parameter Name="dAmount" Value="record.data['dAmount']" Mode="Raw" />
                                        <ext:Parameter Name="cssAmount" Value="record.data['cssAmount']" Mode="Raw" />
                                         <ext:Parameter Name="essAmount" Value="record.data['essAmount']" Mode="Raw" />
                                        <ext:Parameter Name="basicAmount" Value="record.data['basicAmount']" Mode="Raw" />
                                        <ext:Parameter Name="taxAmount" Value="record.data['taxAmount']" Mode="Raw" />
                                        <ext:Parameter Name="netSalary" Value="record.data['netSalary']" Mode="Raw" />
                                        <ext:Parameter Name="currency" Value="record.data['currencyRef']" Mode="Raw" />
                                         <ext:Parameter Name="seqNo" Value="record.data['seqNo']" Mode="Raw" />
                                        <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                    </ExtraParams>

                                </CellClick>
                            </DirectEvents>
                            <View>
                                <ext:GridView ID="GridView2" runat="server" />
                            </View>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>


            </Items>

        </ext:Viewport>

        <ext:Window
            ID="EditEMWindow"
            runat="server"
            Icon="PageEdit"
            Draggable="false"
            Maximizable="false" Resizable="false"
            Width="300"
            Height="300"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:FormPanel
                    ID="EditEMForm" DefaultButton="SaveDocumentButton"
                    runat="server"
                    Header="false"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        <ext:TextField runat="server" Name="seqNo" ID="seqNo" Hidden="true" Disabled="true" />                                                                 
                        <ext:TextField runat="server" Name="basicAmount" ID="basicAmount" FieldLabel="<%$ Resources: FieldBasicAmount %>" ReadOnly="true"  />
                        <ext:TextField runat="server" Name="eAmount"   ID="eAmount"  FieldLabel="<%$ Resources: Entitlements%>" ReadOnly="true"  />
                        <ext:TextField runat="server" Name="dAmount"   ID="dAmount"  FieldLabel="<%$ Resources: Deductions%>" ReadOnly="true"  />
                       <%-- <ext:TextField runat="server" Name="taxAmount" ID="taxAmount" FieldLabel="<%$ Resources: FieldTaxAmount %>"  ReadOnly="true" />--%>
                        <ext:TextField runat="server" Name="cssAmount"  ID="cssAmount" FieldLabel="<%$ Resources: FieldCompanySocialSecurity%>" ReadOnly="true"/>
                        <ext:TextField runat="server" Name="essAmount"  ID="essAmount" FieldLabel="<%$ Resources: FieldEmployeeSocialSecurity%>" ReadOnly="true"/>
                        <ext:TextField runat="server" Name="netSalary" ID="netSalary" FieldLabel="<%$ Resources: FieldNetSalary %>" ReadOnly="true" />


                    </Items>

                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveDocumentButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditEMForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveEM" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditEMWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{seqNo}.getValue()" Mode="Raw" />

                                <ext:Parameter Name="values" Value="#{EditEMForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
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

        <ext:Window ID="EDWindow" runat="server"
            Icon="PageEdit"
            Draggable="false"
            Maximizable="false" Resizable="false"
            Width="700"
            Height="460"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            <Items>
                <ext:TabPanel runat="server" ID="edTabPanel" ActiveTabIndex="0">
                    <Items>
                        <ext:FormPanel
                            ID="entitlementsForm"
                            runat="server"
                            Title="<%$ Resources: Entitlements %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">

                            <Items>
                                <ext:GridPanel SortableColumns="false"  EnableColumnResize="false" EnableColumnHide="false"
                                    ID="entitlementsGrid"
                                    runat="server"
                                    Width="600" Header="false"
                                    Height="350" Layout="FitLayout"
                                    Frame="true" TitleCollapse="true" Scroll="Vertical">
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
                                            <Items>
                                                <ext:Button ID="AddENButton" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="ADDNewEN">
                                                            <EventMask ShowMask="true" CustomTarget="={#{entitlementsGrid}.body}" />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>


                                            </Items>
                                        </ext:Toolbar>

                                    </TopBar>
                                    <Store>
                                        <ext:Store ID="entitlementsStore" runat="server">
                                            <Model>
                                                <ext:Model runat="server" IDProperty="EDrecordId"   >
                                                    <Fields>
                                                        <ext:ModelField Name="seqNo" />
                                                        <ext:ModelField Name="edId" />
                                                        <ext:ModelField Name="edName" />
                                                        <ext:ModelField Name="payId" />
                                                        <ext:ModelField Name="amount" />
                                                        <ext:ModelField Name="edSeqNo" />
                                                        <ext:ModelField Name="EDrecordId" />
                                                        <ext:ModelField Name="masterId" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel>
                                        <Columns>


                                            <ext:Column ID="ColENName"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:FieldEntitlement%>"
                                                DataIndex="edName"
                                                Align="Center">
                                                <SummaryRenderer Handler="if(App.entitlementsGrid.getStore().getCount()>0) return #{total}.value;" />
                                            </ext:Column>


                                            <ext:NumberColumn
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:FieldAmount%>"
                                                DataIndex="amount"
                                                Align="Center">
                                                <SummaryRenderer Handler="if(App.entitlementsGrid.getStore().getCount()>0) return #{CurrentCurrencyRef}.value+ '&nbsp;'+CalcENSum() ;" />
                                                <Renderer Handler=" return #{CurrentCurrencyRef}.value+' &nbsp;' + record.data['amount']  ;" />
                                            </ext:NumberColumn>

                                            <ext:Column runat="server"
                                                ID="ColENDelete" Visible="true"
                                                Text=""
                                                Width="80"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                Hideable="false"
                                                MenuDisabled="true"
                                                Resizable="false">
                                                <SummaryRenderer Handler="if(App.entitlementsGrid.getStore().getCount()>0) return '<hr />'" />
                                                <Renderer Handler="if(#{IsPayrollPosted}.value=='1') return editRender()+'&nbsp;&nbsp;'+deleteRender(); " />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <Features>
                                        <ext:Summary runat="server" />
                                    </Features>
                                    <Listeners>
                                        <Render Handler="this.on('cellclick', cellClick);" />
                                    </Listeners>
                                    <DirectEvents>

                                        <CellClick OnEvent="PoPuPEN">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>

                                                <ext:Parameter Name="id" Value="record.data['edId']" Mode="Raw" />
                                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                                <ext:Parameter Name="values" Value="Ext.encode(#{entitlementsGrid}.getRowsValues({selectedOnly : true}))" Mode="Raw" />
                                            </ExtraParams>

                                        </CellClick>
                                    </DirectEvents>


                                </ext:GridPanel>
                            </Items>

                        </ext:FormPanel>
                        <ext:FormPanel
                            ID="DeductionForm"
                            runat="server"
                            Title="<%$ Resources: Deductions %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:GridPanel
                                    ID="deductionGrid" SortableColumns="false"  EnableColumnResize="false" EnableColumnHide="false"
                                    runat="server"
                                    Width="600" Header="false"
                                    Height="350" Layout="FitLayout"
                                    Frame="true" TitleCollapse="true" Scroll="Vertical">
                                    <Store>
                                        <ext:Store ID="deductionStore" runat="server">
                                            <Model>
                                                <ext:Model runat="server" IDProperty="EDrecordId" >
                                                    <Fields>
                                                        <ext:ModelField Name="seqNo" />
                                                        <ext:ModelField Name="edId" />
                                                        <ext:ModelField Name="edName" />
                                                        <ext:ModelField Name="amount" />
                                                         <ext:ModelField Name="payId" />
                                                         <ext:ModelField Name="edSeqNo" />
                                                        <ext:ModelField Name="EDrecordId" />
                                                        <ext:ModelField Name="masterId" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>

                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar4" runat="server" ClassicButtonStyle="false">
                                            <Items>
                                                <ext:Button ID="AddEDButton" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="ADDNewDE">
                                                            <EventMask ShowMask="true" CustomTarget="={#{deductionGrid}.body}" />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>


                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column ID="ColDEName"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:FieldDeduction%>"
                                                DataIndex="edName"
                                                Align="Center">
                                                <SummaryRenderer Handler="if(App.deductionGrid.getStore().getCount()>0) return #{total}.value;" />
                                            </ext:Column>

                                            <ext:NumberColumn
                                                runat="server"
                                                Text="<%$ Resources:FieldAmount%>"
                                                DataIndex="amount" Flex="2"
                                                Align="Center">
                                                <Editor>
                                                    <ext:NumberField
                                                        runat="server"
                                                        AllowBlank="false" />
                                                </Editor>
                                                <Renderer Handler="return #{CurrentCurrencyRef}.value+ '&nbsp;'+ record.data['amount'] ;">
                                                    
                                              </Renderer>
                                                <SummaryRenderer Handler="if(App.deductionGrid.getStore().getCount()>0) return #{CurrentCurrencyRef}.value+ '&nbsp;'+CalcDESum() ;" />
                                            </ext:NumberColumn>


                                            <ext:Column runat="server"
                                                ID="ColDEDelete" Visible="true"
                                                Text=""
                                                Width="80"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                Hideable="false"
                                                MenuDisabled="true"
                                                Resizable="false">
                                                <Renderer Handler="if(#{IsPayrollPosted}.value=='1') return editRender()+'&nbsp;&nbsp;'+deleteRender(); " />
                                                <SummaryRenderer Handler=" if(App.deductionGrid.getStore().getCount()>0) return '<hr />';" />
                                            </ext:Column>

                                        </Columns>
                                    </ColumnModel>
                                    <Features>
                                        <ext:Summary runat="server" />
                                    </Features>
                                    <Listeners>
                                        <Render Handler="this.on('cellclick', cellClick);" />
                                    </Listeners>
                                    <DirectEvents>

                                        <CellClick OnEvent="PoPuPDE">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>

                                                <ext:Parameter Name="id" Value="record.data['edId']" Mode="Raw" />
                                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                                <ext:Parameter Name="values" Value="Ext.encode(#{deductionGrid}.getRowsValues({selectedOnly : true}))" Mode="Raw" />
                                            </ExtraParams>

                                        </CellClick>
                                    </DirectEvents>
                                </ext:GridPanel>
                            </Items>

                        </ext:FormPanel>
                         <ext:FormPanel
                            ID="PayCodeFormPanel"
                            runat="server"
                            Title="<%$ Resources: socialsecurity %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:GridPanel
                                    ID="payCodeGrid" SortableColumns="false"  EnableColumnResize="false" EnableColumnHide="false"
                                    runat="server"
                                    Width="600" Header="false"
                                    Height="350" Layout="FitLayout"
                                    Frame="true" TitleCollapse="true" Scroll="Vertical">
                                    <Store>
                                        <ext:Store ID="PayCodeStore" runat="server">
                                            <Model>
                                                <ext:Model runat="server" >
                                                    <Fields>
                                                        <ext:ModelField Name="payCode" />
                                                        <ext:ModelField Name="pcName" />
                                                        <ext:ModelField Name="epct" />
                                                         <ext:ModelField Name="cpct" />
                                                        <ext:ModelField Name="amount" />
                                                        <ext:ModelField Name="essAmount" />
                                                        <ext:ModelField Name="cssAmount" />

                                                         

                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>

                                    
                                   
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column ID="Column3" Visible="true"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:PayCode%>"
                                                DataIndex="payCode"
                                                Align="Center">
                                                
                                            </ext:Column>
                                              <ext:Column ID="Column5"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:PayCodeName%>"
                                                DataIndex="pcName"
                                                Align="Center">
                                                  </ext:Column>
                                                   
                                                        <ext:Column ID="Column8"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:pct%>"
                                                DataIndex="epct"
                                                Align="Center">
                                                            </ext:Column>
                                            <ext:Column ID="Column4"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:FieldEmployeeSocialSecurity%>"
                                                DataIndex="essAmount"
                                                Align="Center">
                                                       </ext:Column>
                                                            
                                              <ext:Column ID="Column6"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:pct%>"
                                                DataIndex="cpct"
                                                Align="Center">
                                                
                                            </ext:Column>
                                             <ext:Column ID="Column9"
                                                runat="server" Flex="2"
                                                Text="<%$ Resources:FieldCompanySocialSecurity%>"
                                                DataIndex="cssAmount"
                                                Align="Center">
                                                
                                            </ext:Column>
                                            

                                          

                                        </Columns>
                                    </ColumnModel>
                                   
                                    
                                   
                                </ext:GridPanel>
                            </Items>

                        </ext:FormPanel>

                    </Items>
                    <Buttons>
                  <%--      <ext:Button ID="SaveEDButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                <Click Handler="CheckSession(); " />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="SaveAll" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EDWindow}.body}" />
                                    <ExtraParams>
                                        <ext:Parameter Name="entitlements" Value="Ext.encode(#{entitlementsGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw" />
                                        <ext:Parameter Name="deductions" Value="Ext.encode(#{deductionGrid}.getRowsValues({selectedOnly : false}))" Mode="Raw" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>--%>
                        <ext:Button ID="Button7" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide(); #{Store1}.reload();" />
                    </Listeners>
                </ext:Button>
                    </Buttons>
                </ext:TabPanel>
            </Items>
            <Listeners>
                <Close Handler="#{Store1}.reload();" />
            </Listeners>
        </ext:Window>

        <ext:Window
            ID="EditEDWindow"
            runat="server"
            Icon="PageEdit"
            Draggable="false"
            Maximizable="false" Resizable="false"
            Width="300"
            Height="200"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:FormPanel
                    ID="EditEDForm" DefaultButton="saveEDBT"
                    runat="server"
                    Header="false"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        <ext:TextField runat="server" Name="type" ID="type" Hidden="true" Disabled="true" />
                        <ext:TextField runat="server" Name="isInsert" ID="isInsert" Hidden="true" Disabled="true" />
                     
                           <ext:TextField runat="server" Name="seqNo" ID="EDseqNoTF" Hidden="true" Disabled="true"  />
                           <ext:TextField runat="server" Name="edSeqNo" ID="edSeqNo" Hidden="true" Disabled="true" />
                        
                        <ext:TextField runat="server" Name="masterId" ID="masterId" Hidden="true" Disabled="true" />
                     
                    
                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" AllowBlank="false" DisplayField="name" ID="edId" Name="edId" FieldLabel="<%$ Resources:FieldDeduction%>" SimpleSubmit="true">
                            <Store>
                                <ext:Store runat="server" ID="edStore">
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
                                <ext:Button ID="Button9" runat="server" Icon="Add" Hidden="true">
                                    <Listeners>
                                        <Click Handler="CheckSession();  " />
                                    </Listeners>

                                </ext:Button>
                            </RightButtons>
                            <Listeners>
                                <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:NumberField runat="server" Name="amount" ID="amount" FieldLabel="<%$ Resources: FieldBasicAmount %>" />


                    </Items>

                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="saveEDBT" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditEDForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveED" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditEDWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="type" Value="#{type}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="isInsert" Value="#{isInsert}.getValue()" Mode="Raw" />
                               
                                 <ext:Parameter Name="seqNo" Value="#{EDseqNoTF}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="edSeqNo" Value="#{edSeqNo}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="masterId" Value="#{masterId}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="edId" Value="#{edId}.getValue()" Mode="Raw" />

                                <ext:Parameter Name="values" Value="#{EditEDForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button5" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
         <ext:Window
            ID="EditHeaderWindow"
            runat="server"
            Icon="PageEdit"
            Draggable="false"
            Maximizable="false" Resizable="false"
            Width="300"
            Height="200"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:FormPanel
                    ID="EditHEForm" DefaultButton="Button4"
                    runat="server"
                    Header="false"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        
                        <ext:TextField runat="server" Name="recordId" ID="recordId" Hidden="true" Disabled="true" />
                      
                         <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="statusCombo" Name="statusCombo" runat="server" FieldLabel="<%$ Resources:FieldStatus%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: Status1%>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Status2%>" Value="2"></ext:ListItem>
                                        

                                    </Items>
                                   
                                </ext:ComboBox>
                        <ext:DateField runat="server" ID="fromDate" FieldLabel="<%$ Resources:FieldFrom%>" ReadOnly="true" />
                        <ext:DateField runat="server" ID="toDate" FieldLabel="<%$ Resources:FieldTo%>" ReadOnly="true" />
                    </Items>

                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button10" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditHEForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveHE" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditHeaderWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                

                                <ext:Parameter Name="values" Value="#{EditHEForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button12" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
         <ext:Window
            ID="EditGenerateWindow"
            runat="server"
            Icon="PageEdit"
            Draggable="false"
            Maximizable="false" Resizable="false"
            Width="450"
            Height="250"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>
                <ext:FormPanel
                    ID="EditGenerateForm" DefaultButton="GenerateButton"
                    runat="server"
                    Header="false"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                    <Items>
                        
                        <ext:TextField runat="server" Name="recordId" ID="recordIdTF" Hidden="true" Disabled="true" />
                        <ext:TextField FieldLabel ="<%$ Resources: FieldPayRef%>" runat="server" Name="generatePayRef" ID="generatePayRef" Hidden="false" Disabled="false"  ReadOnly="true"/>
                           <ext:ComboBox  AnyMatch="true" CaseSensitive="false" runat="server" ID="employeeId" Name="employeeId"
                                    DisplayField="fullName"
                                    ValueField="recordId" AllowBlank="true"
                                    TypeAhead="false"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" FieldLabel="<%$ Resources: FieldEmployee%>"
                                    TriggerAction="Query" ForceSelection="true">
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
                                    <Listeners>
                                        <Select Handler="#{Store2}.reload()" />
                                    </Listeners>
                                    <Items>
                                    </Items>
                                    
                                </ext:ComboBox>
                           <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"  Width="120" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="departmentId"  FieldLabel="<%$ Resources:FieldDepartment%>">
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
            


        </ext:ComboBox>
                           <ext:ComboBox   AnyMatch="true" CaseSensitive="false"    runat="server" QueryMode="Local" Width="120" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="branchId"  FieldLabel="<%$ Resources:FieldBranch%>">
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
                       
                    </Items>

                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="GenerateButton" runat="server" Text="<%$ Resources:Common, Generate %>" Icon="ApplicationGo">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{EditGenerateForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="GeneratePayroll1" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditGenerateForm}.body}" />
                            <ExtraParams>
                                 <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="generatePayRef" Value="#{generatePayRef}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="departmentId" Value="#{departmentId}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="branchId" Value="#{branchId}.getValue()" Mode="Raw" />
                                 <ext:Parameter Name="employeeId" Value="#{employeeId}.getValue()" Mode="Raw" />
                                                          
                                                           
                               
                                        
                                        
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button8" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>
