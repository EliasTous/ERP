<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemDefaults.aspx.cs" Inherits="AionHR.Web.UI.Forms.SystemDefaults" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Nationalities.js"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript">
        function ValidateIPaddress(ipaddress) {
            if (ipaddress == '')
                return true;
            if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(ipaddress)) {
                return (true);
            }

            return (false);
        }
        function handlePayment(val, field) {
            if (val < 4) {
                field.setFieldLabel(document.getElementById("paymentValueP").value);
                field.setMaxValue(100);
            }

            else {
                field.setFieldLabel(document.getElementById("paymentValue").value);
                field.setMaxValue(1000000);
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
         <ext:Hidden ID="paymentValue" runat="server" Text="<%$ Resources:PaymentValue %>" />
        <ext:Hidden ID="paymentValueP" runat="server" Text="<%$ Resources:PaymentValueP %>" />




        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:TabPanel runat="server">
                    <Items>
                       
                        <ext:FormPanel DefaultButton="SaveGeneralSettingsBtn"
                            ID="GeneralSettings"
                            runat="server"
                            Title="<%$ Resources: GeneralsDefaults %>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" LabelWidth="150" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldCountry %>" Name="countryId" runat="server" DisplayField="name" ValueField="recordId" ID="countryIdCombo">
                                    <Store>
                                        <ext:Store runat="server" ID="NationalityStore">
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
                                        <ext:Button ID="Button1" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addNationality">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldCurrency %>" Name="currencyId" DisplayField="name" ValueField="recordId" runat="server" ID="currencyIdCombo">
                                    <Store>
                                        <ext:Store runat="server" ID="CurrencyStore">
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

                                                <Click OnEvent="addCurrency">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldDateFormat %>" Name="dateFormat" runat="server" ID="dateFormatCombo">
                                    <Items>
                                        <ext:ListItem Text="Jan 31,2016" Value="MMM dd,yyyy" />
                                        <ext:ListItem Text="Jan 31,16" Value="MMM dd,yy" />
                                        <ext:ListItem Text="31/1/16" Value="dd/MM/yy" />
                                        <ext:ListItem Text="1/31/16" Value="MM/dd/yy" />
                                        <ext:ListItem Text="31/1/2016" Value="dd/MM/yyyy" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldTimeZone %>" Name="timeZone" runat="server" ID="timeZoneCombo">
                                    <Items>
                                        <ext:ListItem Text="-12 UTC" Value="-12" />
                                        <ext:ListItem Text="-11 UTC" Value="-11" />
                                        <ext:ListItem Text="-10 UTC" Value="-10" />
                                        <ext:ListItem Text="-9 UTC" Value="-9" />
                                        <ext:ListItem Text="-8 UTC" Value="-8" />
                                        <ext:ListItem Text="-7 UTC" Value="-7" />
                                        <ext:ListItem Text="-6 UTC" Value="-6" />
                                        <ext:ListItem Text="-5 UTC" Value="-5" />
                                        <ext:ListItem Text="-4 UTC" Value="-4" />
                                        <ext:ListItem Text="-3 UTC" Value="-3" />
                                        <ext:ListItem Text="-2 UTC" Value="-2" />
                                        <ext:ListItem Text="-1 UTC" Value="-1" />
                                        <ext:ListItem Text=" UTC" Value="0" />
                                        <ext:ListItem Text="+1 UTC" Value="1" />
                                        <ext:ListItem Text="+2 UTC" Value="2" />
                                        <ext:ListItem Text="+3 UTC" Value="3" />
                                        <ext:ListItem Text="+4 UTC" Value="4" />
                                        <ext:ListItem Text="+5 UTC" Value="5" />
                                        <ext:ListItem Text="+6 UTC" Value="6" />
                                        <ext:ListItem Text="+7 UTC" Value="7" />
                                        <ext:ListItem Text="+8 UTC" Value="8" />
                                        <ext:ListItem Text="+9 UTC" Value="9" />
                                        <ext:ListItem Text="+10 UTC" Value="10" />
                                        <ext:ListItem Text="+11 UTC" Value="11" />
                                        <ext:ListItem Text="+12 UTC" Value="12" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:Checkbox FieldLabel="<%$ Resources: FieldEnableHijri %>" LabelWidth="150" runat="server" InputValue="True" Name="enableHijri" ID="enableHijri" />
                            </Items>
                            <Buttons>
                                <ext:Button Hidden="true" ID="SaveGeneralSettingsBtn" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); alert(#{GeneralSettings}); if (!#{GeneralSettings}.getForm().isValid()) {return false;}  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveGeneralSettings" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true"  />
                                            <ExtraParams>

                                                <ext:Parameter Name="values" Value="#{GeneralSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:FormPanel>
                         <ext:FormPanel DefaultButton="SaveButton"
                            ID="EmployeeSettings"
                            runat="server"
                            Title="<%$ Resources: EmployeesDefaults %>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>


                             
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldVacationSchedule %>" Name="vsId" DisplayField="name" ValueField="recordId" runat="server" ID="vsId">
                                    <Store>
                                        <ext:Store runat="server" ID="vsStore">
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
                                        <ext:Button ID="Button4" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addVS">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                               

                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldNameFormat %>" Name="nameFormat" runat="server" ID="nameFormatCombo">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:FirstNameLastName %>" Value="{firstName} {lastName}" />
                                        <ext:ListItem Text="<%$ Resources:LastNameFirstName %>" Value="{lastName} {firstName}" />
                                        <ext:ListItem Text="<%$ Resources:FirstNameMiddleNameLastName %>" Value="{firstName} {middleName} {lastName}" />
                                        <ext:ListItem Text="<%$ Resources:ReferenceFirstNameLastName %>" Value="{reference} {firstName} {lastName}" />
                                        <ext:ListItem Text="<%$ Resources:ReferenceFirstNameMiddleNameLastName %>" Value="{reference} {firstName} {middleName} {lastName}" />
                                        <ext:ListItem Text="<%$ Resources:ReferenceFirstNameMiddleNameFamilyNameLastName %>" Value="{reference} {firstName} {middleName} {familyName} {lastName}" />
                                        <ext:ListItem Text="<%$ Resources:ReferenceLastNameFirstName %>" Value="{reference} {lastName} {firstName}" />


                                    </Items>
                                </ext:ComboBox>
                         



                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: PassportRTW %>" Name="passportCombo" DisplayField="name" ValueField="recordId" runat="server" ID="passportCombo">
                                    <Store>
                                        <ext:Store runat="server" ID="passportStore">
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
                                        <ext:Button ID="Button5" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addPassport">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: IDRTW %>" Name="idCombo" DisplayField="name" ValueField="recordId" runat="server" ID="idCombo">
                                    <Store>
                                        <ext:Store runat="server" ID="idStore">
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

                                                <Click OnEvent="addId">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                  
                         

                            </Items>
                            <Buttons>
                                <ext:Button Hidden="true" ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{EmployeeSettings}.getForm().isValid()) {return false;}  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveEmployeeSettings" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>

                                                <ext:Parameter Name="values" Value="#{EmployeeSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>

                            </Buttons>
                        </ext:FormPanel>
                        <ext:FormPanel DefaultButton="SaveAttendanceBtn"
                            ID="AttendanceSettings"
                            runat="server"
                            Title="<%$ Resources: AttendanceSettings %>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                   <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldWorkingCalendar %>" Name="caId" DisplayField="name" ValueField="recordId" runat="server" ID="caId">
                                    <Store>
                                        <ext:Store runat="server" ID="caStore">
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
                                        <ext:Button ID="Button3" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addCalendar">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldSchedule %>" Name="scId" DisplayField="name" ValueField="recordId" runat="server" ID="scId">
                                    <Store>
                                        <ext:Store runat="server" ID="scheduleStore">
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
                                       <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldFirstDayOfWeek %>" Name="fdow" runat="server" ID="fdowCombo">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:Common, MondayText %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources:Common, TuesdayText %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources:Common, WednesdayText %>" Value="3" />
                                        <ext:ListItem Text="<%$ Resources:Common, ThursdayText %>" Value="4" />
                                        <ext:ListItem Text="<%$ Resources:Common, FridayText %>" Value="5" />
                                        <ext:ListItem Text="<%$ Resources:Common, SaturdayText %>" Value="6" />
                                        <ext:ListItem Text="<%$ Resources:Common, SundayText %>" Value="7" />
                                    </Items>
                                </ext:ComboBox>
                                        <ext:Panel runat="server" Layout="HBoxLayout" Height="50">
                                    <Items>
                                        <ext:TextField runat="server" Name="localServerIP" ID="localServerIP" LabelWidth="150" FieldLabel="<%$Resources:LocalServerIP %>">
                                            <Validator Fn="ValidateIPaddress" />

                                        </ext:TextField>

                                        <ext:Label runat="server" MarginSpec="5 0 10 10" Text="  /AionWSLocal" />
                                    </Items>
                                </ext:Panel>
                                <ext:TextField runat="server" Name="lastGeneratedTADayId" ReadOnly="true" ID="lastGeneratedTADayId" LabelWidth="150" FieldLabel="<%$Resources:lastGeneratedTADayId %>">
                                </ext:TextField>
                                 <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldTSId %>" Name="tsId" DisplayField="name" ValueField="recordId" runat="server" ID="tsId">
                                    <Store>
                                        <ext:Store runat="server" ID="tsStore">
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

                                   <ext:Checkbox FieldLabel="<%$ Resources: FieldEnableCamera %>" LabelWidth="150" runat="server" InputValue="True" Name="enableCamera" ID="enableCameraCheck" />

                            </Items>
                            <Buttons>
                                <ext:Button Hidden="true"  ID="SaveAttendanceBtn" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{AttendanceSettings}.getForm().isValid()) {return false;}  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveAttendanceSettings" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true"   />
                                            <ExtraParams>

                                                <ext:Parameter Name="values" Value="#{AttendanceSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>

                            </Buttons>
                        </ext:FormPanel>
                        <ext:FormPanel DefaultButton="SavePayrollSettingsBtn"
                            ID="PayrollSettings"
                            runat="server"
                            Title="<%$ Resources: PayrollSettings %>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                 <ext:FieldSet Collapsible="true" runat="server" Title="<%$ Resources:Common,TimeCode%>">
                                 <Items>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" LabelWidth="160" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: absence %>" Name="py_aEDId" runat="server" DisplayField="name" ValueField="recordId" ID="py_aEDId">
                                    <Store>
                                        <ext:Store runat="server" ID="absenceStore">
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
                                        <ext:Button ID="Button7" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addAbsenceDed">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" LabelWidth="160" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: disappearance  %>" Name="py_dEDId" runat="server" DisplayField="name" ValueField="recordId" ID="py_dEDId">
                                    <Store>
                                        <ext:Store runat="server" ID="disappearanceStore">
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
                                        <ext:Button ID="Button12" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addDisappearanceDed">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" LabelWidth="160" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: missedPunch %>" Name="py_mEDId" runat="server" DisplayField="name" ValueField="recordId" ID="py_mEDId">
                                    <Store>
                                        <ext:Store runat="server" ID="missedPunchesStore">
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
                                        <ext:Button ID="Button13" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addmissedPunchesDed">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" LabelWidth="160" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: overtime %>" Name="py_oEDId" runat="server" DisplayField="name" ValueField="recordId" ID="py_oEDId">
                                    <Store>
                                        <ext:Store runat="server" ID="overtimeStore">
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
                                        <ext:Button ID="Button14" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addovertimee">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" LabelWidth="160" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: Lateness %>" Name="py_lEDId" runat="server" DisplayField="name" ValueField="recordId" ID="py_lEDId">
                                    <Store>
                                        <ext:Store runat="server" ID="latenessStore">
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
                                        <ext:Button ID="Button15" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addLatenessDed">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                     </Items>
                                     </ext:FieldSet>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="true" runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="ssId" Name="ssId" FieldLabel="<%$ Resources: SocialSecuritySchedule%>" LabelWidth="150" SimpleSubmit="true">
                                                    <Store>
                                                        <ext:Store runat="server" ID="ssIdstore">
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
                                               <%--   <RightButtons>
                                                        <ext:Button ID="Button3" runat="server" Icon="Add" Hidden="true" >
                                                            <Listeners>
                                                                <Click Handler="CheckSession();" />
                                                            </Listeners>
                                                            <DirectEvents>

                                                              <%--  <Click OnEvent="addBranch">
                                                                 
                                                                </Click>>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </RightButtons>--%>
                                                    <Listeners>
                                                        <FocusEnter Handler="if(!this.readOnly) this.rightButtons[0].setHidden(false);" />
                                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                
                              <%--  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" LabelWidth="160" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldSS %>" Name="ssDeductionId" runat="server" DisplayField="name" ValueField="recordId" ID="ssDeductionId">
                                    <Store>
                                        <ext:Store runat="server" ID="ssDeductionStore">
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
                                        <ext:Button ID="Button8" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addDedSS">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>--%>

                               <%-- <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" LabelWidth="160" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldPE %>" Name="peDeductionId" runat="server" DisplayField="name" ValueField="recordId" ID="peDeductionId">
                                    <Store>
                                        <ext:Store runat="server" ID="peDeductionStore">
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
                                            <DirectEvents>

                                                <Click OnEvent="addDedpe">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>--%>
                               
                                <ext:FieldSet Collapsible="true" runat="server" Title="<%$ Resources:Common, Loans %>">
                                    <Items>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="400" LabelWidth="160" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldLoan %>" Name="loanDeductionId" runat="server" DisplayField="name" ValueField="recordId" ID="loanDeductionId">
                                            <Store>
                                                <ext:Store runat="server" ID="loanDeductionStore">
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
                                                <ext:Button ID="Button10" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addDedloan">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Width="400" QueryMode="Local" LabelWidth="160" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: LoanCoverageType %>" Name="ldMethod" runat="server" ID="ldMethod">
                                            <Items>
                                                <ext:ListItem Text="<%$ Resources: PFromNetSalary %>" Value="1" />
                                                <ext:ListItem Text="<%$ Resources: PFromBasicSalary %>" Value="2" />
                                                <ext:ListItem Text="<%$ Resources: PFromLoan %>" Value="3" />
                                                <ext:ListItem Text="<%$ Resources: FixedAmount %>" Value="4" />
                                                <ext:ListItem Text="<%$ Resources: FixedPayment %>" Value="5" />

                                            </Items>
                                            <Listeners>
                                                <Change Handler="handlePayment(this.value,this.next());"></Change>
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:NumberField Width="400" runat="server" LabelWidth="160" ID="ldValue" Name="ldValue" FieldLabel="<%$ Resources: PaymentValue %>" MinValue="0" />
                                        </Items>
                                  </ext:FieldSet>
                               
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="400" LabelWidth="160" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: PYFSLeaveBalEDId %>" Name="PYFSLeaveBalEDId" runat="server" DisplayField="name" ValueField="recordId" ID="PYFSLeaveBalEDId">
                                            <Store>
                                                <ext:Store runat="server" ID="PYFSLeaveBalEDId_Store">
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
                                           <%-- <RightButtons>
                                                <ext:Button ID="Button8" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addDedloan">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>--%>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="400" LabelWidth="160" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: payrollIndemnityEntitlement  %>" Name="PYINEDId" runat="server" DisplayField="name" ValueField="recordId" ID="PYINEDId">
                                            <Store>
                                                <ext:Store runat="server" ID="PYINEDId_store">
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
                                           <%-- <RightButtons>
                                                <ext:Button ID="Button8" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addDedloan">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>--%>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                
                                 <ext:FieldSet Collapsible="true" runat="server" Title="<%$ Resources: IndemnitySchedules %>">
                                    <Items>
                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="400" LabelWidth="160" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: PYISmale  %>" Name="PYISmale" runat="server" DisplayField="name" ValueField="recordId" ID="PYISmale">
                                            <Store>
                                                <ext:Store runat="server" ID="PYISmale_store">
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
                                           <%-- <RightButtons>
                                                <ext:Button ID="Button8" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addDedloan">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>--%>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                      
                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="400" LabelWidth="160" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: PYISfemale  %>" Name="PYISfemale" runat="server" DisplayField="name" ValueField="recordId" ID="PYISfemale">
                                            <Store>
                                                <ext:Store runat="server" ID="PYISfemale_store">
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
                                           <%-- <RightButtons>
                                                <ext:Button ID="Button8" runat="server" Icon="Add" Hidden="true">
                                                    <Listeners>
                                                        <Click Handler="CheckSession();  " />
                                                    </Listeners>
                                                    <DirectEvents>

                                                        <Click OnEvent="addDedloan">
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </RightButtons>--%>
                                            <Listeners>
                                                <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        </Items>
                                        </ext:FieldSet>
                                <ext:FieldSet Collapsible="true" runat="server" Title="<%$ Resources: TerminationReason %>">
                                 <Items>

                                   <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: exemptMarriageTRId %>" Name="exemptMarriageTRId" DisplayField="name" ValueField="recordId" runat="server" ID="exemptMarriageTRId">
                                    <Store>
                                        <ext:Store runat="server" ID="exemptMarriageTR_Store">
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
                                  <%--  <RightButtons>
                                        <ext:Button ID="Button8" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addId">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>--%>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                   <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: exemptDeliveryTRId %>" Name="exemptDeliveryTRId" DisplayField="name" ValueField="recordId" runat="server" ID="exemptDeliveryTRId">
                                    <Store>
                                        <ext:Store runat="server" ID="exemptDeliveryTR_Store">
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
                                 <%--   <RightButtons>
                                        <ext:Button ID="Button9" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addId">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>--%>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                     </Items>
                                  </ext:FieldSet>

                                    
                                                                  
                            </Items>
                            <Buttons>
                                <ext:Button Hidden="true"  ID="SavePayrollSettingsBtn" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{PayrollSettings}.getForm().isValid()) {return false;}  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SavePayrollSettings" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true"  />
                                            <ExtraParams>

                                                <ext:Parameter Name="values" Value="#{PayrollSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:FormPanel>
                        <ext:FormPanel DefaultButton="SaveSecuritySettingsBtn"
                            ID="SecuritySettings"
                            runat="server"
                            Title="<%$ Resources: SecuritySettings %>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                    
                                <ext:Checkbox FieldLabel="<%$ Resources: apply_ALDA_CSBR %>" LabelWidth="150" runat="server" InputValue="True" Name="apply_ALDA_CSBR" ID="apply_ALDA_CSBR" />
                                <ext:Checkbox FieldLabel="<%$ Resources: apply_ALDA_CSDE %>" LabelWidth="150" runat="server" InputValue="True" Name="apply_ALDA_CSDE" ID="apply_ALDA_CSDE" />
                                <ext:Checkbox FieldLabel="<%$ Resources: apply_ALDA_CSDI %>" LabelWidth="150" runat="server" InputValue="True" Name="apply_ALDA_CSDI" ID="apply_ALDA_CSDI" />

                            </Items>
                            <Buttons>
                                <ext:Button Hidden="true"  ID="SaveSecuritySettingsBtn" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{SecuritySettings}.getForm().isValid()) {return false;}  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveSecuritySettings" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true"  />
                                            <ExtraParams>

                                                <ext:Parameter Name="values" Value="#{SecuritySettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:FormPanel>


                    </Items>
                       <Buttons>
                                <ext:Button ID="Button11" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{GeneralSettings}.getForm().isValid()||!#{EmployeeSettings}.getForm().isValid()||!#{AttendanceSettings}.getForm().isValid()||!#{PayrollSettings}.getForm().isValid()||!#{SecuritySettings}.getForm().isValid()) {return false;}  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveAll" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true"  />
                                            <ExtraParams>

                                                <ext:Parameter Name="emp" Value="#{EmployeeSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                                <ext:Parameter Name="gen" Value="#{GeneralSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                                <ext:Parameter Name="ta" Value="#{AttendanceSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                                <ext:Parameter Name="py" Value="#{PayrollSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                                <ext:Parameter Name="sec" Value="#{SecuritySettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                </ext:TabPanel>
                


            </Items>

        </ext:Viewport>






    </form>
</body>
</html>
