﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemDefaults.aspx.cs" Inherits="Web.UI.Forms.SystemDefaults" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="CSS/Common.css?id=11" />
<script src="Scripts.js" type="text/javascript"></script>






   
    <link rel="stylesheet" href="CSS/LiveSearch.css?id=10" />
    <link rel="stylesheet" type="text/css" href="CSS/cropper.css?id=101" />
    <script type="text/javascript" src="Scripts/Nationalities.js?id=121"></script>
    <script type="text/javascript" src="Scripts/common.js?id=122"></script>
    <script type="text/javascript" src="Scripts/jquery-new.js?id=125"></script>
  
    <script type="text/javascript" src="Scripts/cropper.js?id=126"></script>
      <script type="text/javascript" src="Scripts/SystemDefaults.js?id=150"></script>



     

    <script type="text/javascript">
          
        var checkExtension = function (file) {
          
        try {

            if (file == null || file == '') {
                return true;
            }
            var dot = file.lastIndexOf('.');
            if (dot >= 0) {
                var ext = file.substr(dot + 1, file.length).toLowerCase();
                if (ext in { 'jpg': '', 'png': '', 'jpeg': '' }) { return true; }
            }

            return false;
        }
        catch (e) {
            return false;
        }
    }
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
        <ext:Hidden ID="CurrentEmployeePhotoName" runat="server" EnableViewState="true"  />
        <ext:Hidden runat="server" ID="imageData" />
        <ext:Hidden runat="server" ID="lblLoading" Text="<%$Resources:Common , Loading %>" />


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
                                <ext:ComboBox AllowBlank="true" AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldDateFormat %>" Name="dateFormat" runat="server" ID="dateFormatCombo">
                                    <Items>
                                        <ext:ListItem Text="Jan 31,2016" Value="MMM dd,yyyy" />
                                        <ext:ListItem Text="Jan 31,16" Value="MMM dd,yy" />
                                        <ext:ListItem Text="31/1/16" Value="dd/MM/yy" />
                                        <ext:ListItem Text="1/31/16" Value="MM/dd/yy" />
                                        <ext:ListItem Text="31/1/2016" Value="dd/MM/yyyy" />
                                    </Items>
                                </ext:ComboBox>
                            <%--    <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldTimeZone %>" Name="timeZone" runat="server" ID="timeZoneCombo">
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
                                </ext:ComboBox>--%>
                                  <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1"   FieldLabel="<%$ Resources: FieldLanguageId%>" Name="languageId" runat="server" ID="languageId">
                                                                 
                                  
                                  
                                    <Items>
                                        <ext:ListItem Text="<%$Resources:Common,EnglishLanguage %>" Value="1" />
                                        <ext:ListItem Text="<%$Resources:Common,ArabicLanguage %>" Value="2" />
                                         <ext:ListItem Text="<%$Resources:Common,FrenchLanguage %>" Value="3" />
                                    </Items>
                                </ext:ComboBox>
                                 <ext:ComboBox AllowBlank="true"   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"  Width="120" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" LabelWidth="150" DisplayField="name" ID="NQINid" Name="NQINid"  FieldLabel="<%$ Resources:FieldIndustry%>">
                                            <Store>
                                                <ext:Store runat="server" ID="NQINidStore">
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

                                   <ext:TextField runat="server" Name="backofficeEmail" ID="backofficeEmail" FieldLabel="<%$Resources:backofficeEmail %>" Vtype="email" LabelWidth="150">
                                           

                                        </ext:TextField>

                                 <ext:Image runat="server" ID="noImage" Hidden="true"  Width="100" Height="100" />
                        <ext:Image runat="server" ID="imgControl" MaxWidth="150" MaxHeight="150">
                            <Listeners>
                                <%--<Click Handler="triggierImageClick(App.employeeControl1_picturePath.fileInputEl.id); " />--%>
                                <Click Handler="InitCropper(App.CurrentEmployeePhotoName.value+'?x='+ new Date().getTime()); App.imageSelectionWindow.show();" />
                            </Listeners>

                        </ext:Image>


                        <ext:FileUploadField ID="picturePath" runat="server" ButtonOnly="true" Hidden="true">

                            <Listeners>
                                <Change Handler="showImagePreview(App.picturePath.fileInputEl.id);" />
                            </Listeners>
                            <DirectEvents>
                            </DirectEvents>
                        </ext:FileUploadField>
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
                               

                                <ext:ComboBox AllowBlank="true" AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldNameFormat %>" Name="nameFormat" runat="server" ID="nameFormatCombo">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:FirstNameLastName %>" Value="{firstName} {lastName}" />
                                         <ext:ListItem Text="<%$ Resources:FirstNameFamilyName %>" Value="{firstName} {familyName}" />
                                          <ext:ListItem Text="<%$ Resources:FirstNameMiddleNameFamilyNameLastName %>" Value="{firstName} {middleName} {familyName} {lastName}" />
                                         <ext:ListItem Text="<%$ Resources:FirstNameMiddleNameLastName %>" Value="{firstName} {middleName} {lastName}" />
                                        <ext:ListItem Text="<%$ Resources:LastNameFirstName %>" Value="{lastName} {firstName}" />
                                       
                                        
                                        <ext:ListItem Text="<%$ Resources:ReferenceFirstNameLastName %>" Value="{reference} {firstName} {lastName}" />
                                        <ext:ListItem Text="<%$ Resources:ReferenceFirstNameMiddleNameLastName %>" Value="{reference} {firstName} {middleName} {lastName}" />
                                        <ext:ListItem Text="<%$ Resources:ReferenceFirstNameMiddleNameFamilyNameLastName %>" Value="{reference} {firstName} {middleName} {familyName} {lastName}" />
                                        <ext:ListItem Text="<%$ Resources:ReferenceFirstNameMiddleNameFamilyName %>" Value="{reference} {firstName} {middleName} {familyName}" />
                                        <ext:ListItem Text="<%$ Resources:ReferenceLastNameFirstName %>" Value="{reference} {lastName} {firstName}" />
                                         
                                         


                                    </Items>
                                    <DirectEvents>
                                           <Select OnEvent="NameFormatChanged">
                                              
                                                <ExtraParams>

                                                <ext:Parameter Name="nameFormat" Value="this.value" Mode="Raw" Encode="true" />
                                               </ExtraParams>
                                                </Select>
                                    </DirectEvents>
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
                                <ext:NumberField  runat="server" LabelWidth="150" ID="retirementAge" Name="retirementAge" FieldLabel="<%$ Resources: retirementAge %>" MinValue="0"  MaxValue="100"/>
                                  <ext:NumberField  runat="server" AllowBlank="false" LabelWidth="150" ID="employeeRefSize" Name="employeeRefSize" FieldLabel="<%$ Resources: employeeRefSize %>" MinValue="0"  MaxValue="10">
                                      <Validator Handler="if(this.value==1 ||this.value==2 ) return false; else return true;"></Validator>
                                      </ext:NumberField>

                                  <ext:Container runat="server" Layout="FitLayout">
                                    <Content>

                                        <uc:ApprovalsControl runat="server" ID="lvApId"  LabelWidth="150" FieldLabel="<%$ Resources: lawViolationApproval %>" />

                                    </Content>
                                </ext:Container>
                                    <ext:Container runat="server" Layout="FitLayout">
                                    <Content>

                                        <uc:EntitlementDeductionControl runat="server" ID="lvEdId"  LabelWidth="150" FieldLabel="<%$ Resources: lawViolationEntitlementDeduction %>" Filter="2" />

                                    </Content>
                                </ext:Container>
                         

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
                              <%--  <ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldSchedule %>" Name="scId" DisplayField="name" ValueField="recordId" runat="server" ID="scId">
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

                                </ext:ComboBox>--%>
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
                                <ext:TextField runat="server" Name="lastGenFSDateTime" ReadOnly="true" ID="lastGenFSDateTime" LabelWidth="150" FieldLabel="<%$Resources:lastGenFSDateTime %>"></ext:TextField>
                                <ext:TextField runat="server" Name="lastReceivedPunch" ReadOnly="true" ID="lastReceivedPunch" LabelWidth="150" FieldLabel="<%$Resources:lastReceivedPunch %>"></ext:TextField>
                                <ext:TextField runat="server" Name="lastProcessedPunch" ReadOnly="true" ID="lastProcessedPunch" LabelWidth="150" FieldLabel="<%$Resources:lastProcessedPunch %>"></ext:TextField>
                                <ext:TextField runat="server" Name="lastGenTATV" ReadOnly="true" ID="lastGenTATV" LabelWidth="150" FieldLabel="<%$Resources:lastGenTATV %>"></ext:TextField>
                                 
                                <ext:ComboBox AllowBlank="true" AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: SourceAttendanceSchedule %>" Name="sourceTASC" runat="server" ID="sourceTASC">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:  FieldBranch %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources:  FieldDepartment %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources:  FieldCalendar %>" Value="3" />
                                       
                                    </Items>
                                </ext:ComboBox>
                                  <ext:ComboBox AllowBlank="true" AnyMatch="true" CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: SourceWorkingCalender %>" Name="sourceTACA" runat="server" ID="sourceTACA">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:  FieldBranch %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources:  FieldDepartment %>" Value="2" />
                                       
                                       
                                    </Items>
                                </ext:ComboBox>
                                 <ext:NumberField  runat="server" LabelWidth="150" ID="minPunchInterval" Name="minPunchInterval" FieldLabel="<%$ Resources: minPunchInterval %>" MinValue="5"  MaxValue="15"/>
                                       <ext:ComboBox AnyMatch="true"  CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldDailySchedule %>" Name="dailySchedule" runat="server" ID="dailySchedule">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: dailySchedule_15 %>" Value="<%$ Resources:ComboBoxValues, dailySchedule_15 %>" />
                                        <ext:ListItem Text="<%$ Resources: dailySchedule_30 %>" Value="<%$ Resources:ComboBoxValues, dailySchedule_30 %>" />
                                        <ext:ListItem Text="<%$ Resources: dailySchedule_60 %>" Value="<%$ Resources:ComboBoxValues, dailySchedule_60 %>" />
                                       
                                    </Items>
                                </ext:ComboBox>
                                  <ext:ComboBox AnyMatch="true"  CaseSensitive="false" QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: punchSource %>" Name="punchSource" runat="server" ID="punchSource">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: employeeReference %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: timeAttendance %>" Value="2" />
                                     
                                       
                                    </Items>
                                </ext:ComboBox>
                               <%--    <ext:Checkbox FieldLabel="<%$ Resources: FieldEnableCamera %>" LabelWidth="150" runat="server" InputValue="True" Name="enableCamera" ID="enableCameraCheck" />--%>
                                       <ext:NumberField  runat="server" LabelWidth="150" ID="weeklyTAHours" Name="weeklyTAHours" FieldLabel="<%$ Resources: weeklyTAHours %>" MinValue="0"  />
                                       <ext:NumberField  runat="server" LabelWidth="150" ID="prevDayTVTime" Name="prevDayTVTime" FieldLabel="<%$ Resources: prevDayVariation %>" MinValue="7" MaxValue="15"  />
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
                                 <ext:FieldSet Collapsible="true" runat="server" Title="<%$ Resources:Common,TimeCode%>" Visible="false">
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
                                <ext:FieldSet Collapsible="true" runat="server" Title="<%$ Resources:Common,TimeCode%>">
                                    <Items>
                                      <ext:NumberField Width="600" runat="server" LabelWidth="350" ID="pyalDays" Name="pyalDays" FieldLabel="<%$ Resources: pyalDays %>" MinValue="30"  MaxValue="360"/>
                                      <ext:NumberField Width="600" runat="server" LabelWidth="350" ID="pyaaDays" Name="pyaaDays" FieldLabel="<%$ Resources: pyaaDays %>" MinValue="30" MaxValue="360"/>
                                      <ext:NumberField Width="600" runat="server" LabelWidth="350" ID="pylvDays" Name="pylvDays" FieldLabel="<%$ Resources: pylvDays %>" MinValue="30" MaxValue="360" />
                     
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
                                        <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="400" LabelWidth="160" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: LoanCoverageType %>"  runat="server" DisplayField="value" ValueField="key"   Name="ldMethod" ID="ldMethod" >
                                             <Store>
                                                <ext:Store runat="server" ID="ldMethodStore">
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
                                                <Change Handler="handlePayment(this.value,this.next());"></Change>
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:NumberField Width="400" runat="server" LabelWidth="160" ID="ldValue" Name="ldValue" FieldLabel="<%$ Resources: PaymentValue %>" MinValue="0" />
                                        </Items>
                                  </ext:FieldSet>
                               
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="600" LabelWidth="350" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: PYFSLeaveBalEDId %>" Name="PYFSLeaveBalEDId" runat="server" DisplayField="name" ValueField="recordId" ID="PYFSLeaveBalEDId">
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
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="600" LabelWidth="350" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: payrollIndemnityEntitlement  %>" Name="PYINEDId" runat="server" DisplayField="name" ValueField="recordId" ID="PYINEDId">
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
                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="600" LabelWidth="350" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: PYISmale  %>" Name="PYISmale" runat="server" DisplayField="name" ValueField="recordId" ID="PYISmale">
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
                                      
                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" Width="600" LabelWidth="350" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: PYISfemale  %>" Name="PYISfemale" runat="server" DisplayField="name" ValueField="recordId" ID="PYISfemale">
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
                                         <ext:NumberField Width="400" runat="server" LabelWidth="160" ID="yearDays" Name="yearDays" FieldLabel="<%$ Resources: yearDays %>" MinValue="0" MaxValue="365" />
                                        </Items>
                                        </ext:FieldSet>
                         
                              
                               <ext:NumberField Width="400" runat="server" LabelWidth="150" ID="monthWorkDays" Name="monthWorkDays" FieldLabel="<%$ Resources: monthWorkDays  %>" MinValue="20" MaxValue="30" />

                                <ext:NumberField Width="400" runat="server" LabelWidth="150" ID="monthWorkHrs" Name="monthWorkHrs" FieldLabel="<%$ Resources: monthWorkHrs  %>" MinValue="200" MaxValue="300" />
                                <ext:NumberField Width="400" runat="server" LabelWidth="150" ID="dayWorkHrs" Name="dayWorkHrs" FieldLabel="<%$ Resources: dayWorkHrs  %>" MinValue="6" MaxValue="24" />

                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="true" runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="bsId" Name="bsId" FieldLabel="<%$ Resources: benefitSchedule%>" LabelWidth="150">
                                                    <Store>
                                                        <ext:Store runat="server" ID="bsIdStore">
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
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="true" runat="server" AllowBlank="true" ValueField="payCode" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="basicSalaryPayCode" Name="basicSalaryPayCode" FieldLabel="<%$ Resources: basicSalaryPayCode%>" LabelWidth="150">
                                                    <Store>
                                                        <ext:Store runat="server" ID="basicSalaryPayCodeStore">
                                                            <Model>
                                                                <ext:Model runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="payCode" />
                                                                        <ext:ModelField Name="name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                              
                                                  
                                                </ext:ComboBox>
                                 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Enabled="true" runat="server" AllowBlank="true" ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="FSWDEDId" Name="FSWDEDId" FieldLabel="<%$ Resources: FSWDEDId%>" LabelWidth="150">
                                                    <Store>
                                                        <ext:Store runat="server" ID="FSWDEDIdStore">
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
                                    <ext:NumberField Width="400" runat="server" LabelWidth="150" ID="MaxLoanDeduction" Name="MaxLoanDeduction" FieldLabel="<%$ Resources: MaxLoanDeduction  %>" MinValue="0" MaxValue="100" AllowDecimals="true" DecimalPrecision="2" AllowBlank="true"/>
                                    <ext:NumberField Width="400" runat="server" LabelWidth="150" ID="pytvEndDate" Name="pytvEndDate" FieldLabel="<%$ Resources: TimeVariationEndDate  %>" MinValue="15" MaxValue="28"  AllowBlank="true"/>
                               
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
                                <ext:Checkbox FieldLabel="<%$ Resources: apply_ALDA_CSPO %>" LabelWidth="150" runat="server" InputValue="True" Name="apply_ALDA_CSPO" ID="apply_ALDA_CSPO" />


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

                           <ext:FormPanel Hidden="true" DefaultButton="SaveBiometricSettingsBtn"
                            ID="BiometricSettings"
                            runat="server"
                            Title="<%$ Resources: BiometricSettings %>"
                            Icon="ApplicationSideList" AutoScroll="true"
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>
                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="pp_storeType" LabelWidth="150"  runat="server" FieldLabel="<%$ Resources: storeType %>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: sqlServer %>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: access%>" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: csvFile%>" Value="3"></ext:ListItem>

                                    </Items>
                                
                                </ext:ComboBox>  
                                  <ext:TextArea FieldLabel="<%$ Resources: storeConnection %>" LabelWidth="150" runat="server" Name="pp_storeConnection" ID="pp_storeConnection" MaxLength="100" />
                                <ext:Checkbox FieldLabel="<%$ Resources: pull %>" LabelWidth="150" runat="server" InputValue="True" Name="pp_pull" ID="pp_pull" />
                                <ext:Checkbox FieldLabel="<%$ Resources: push %>" LabelWidth="150" runat="server" InputValue="True" Name="pp_push" ID="pp_push" />
                                <ext:Checkbox FieldLabel="<%$ Resources: clearOnSuccess %>" LabelWidth="150" runat="server" InputValue="True" Name="pp_clearOnSuccess" ID="pp_clearOnSuccess" />
                                <ext:NumberField FieldLabel="<%$ Resources: sleepTime %>" AllowBlank="true" LabelWidth="150" runat="server"  Name="pp_sleepTime" ID="pp_sleepTime" MinValue="0" ></ext:NumberField>
                                    <ext:TextField runat="server" Name="pp_serialNo" AllowBlank="true" ID="pp_serialNo" FieldLabel="<%$ Resources:serialNo%>" LabelWidth="150" >
                                      <Validator Handler="return !isNaN(this.value);" />
                                     </ext:TextField>
                                 <ext:Checkbox FieldLabel="<%$ Resources: debugMode %>" LabelWidth="150" runat="server" InputValue="True" Name="pp_debugMode" ID="pp_debugMode" />
                                 <ext:Checkbox FieldLabel="<%$ Resources: shiftData %>" LabelWidth="150" runat="server" InputValue="True" Name="pp_shiftData" ID="pp_shiftData" />
                                <ext:TextField FieldLabel="<%$ Resources: pendingPunchesFolder %>" LabelWidth="150" runat="server" Name="pp_pendingDataFolder" ID="pp_pendingDataFolder" MaxLength="100" />
                              <ext:TextArea FieldLabel="<%$ Resources: punchInterface %>" LabelWidth="150" runat="server" Name="pp_punchInterface" ID="pp_punchInterface" MaxLength="255" />
                                    
                            </Items>
                            <Buttons>
                                <ext:Button Hidden="true"  ID="SaveBiometricSettingsBtn" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{BiometricSettings}.getForm().isValid()) {return false;}  " />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="SaveBiometricSettings" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            <EventMask ShowMask="true"  />
                                            <ExtraParams>

                                                <ext:Parameter Name="values" Value="#{BiometricSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
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
                                        <Click Handler="CheckSession(); if (!#{GeneralSettings}.getForm().isValid()||!#{EmployeeSettings}.getForm().isValid()||!#{AttendanceSettings}.getForm().isValid()||!#{PayrollSettings}.getForm().isValid()||!#{SecuritySettings}.getForm().isValid()||!#{BiometricSettings}.getForm().isValid()) {return false;}  " />
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
                                                <ext:Parameter Name="bio" Value="#{BiometricSettings}.getForm().getValues()" Mode="Raw" Encode="true" />
                                              
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                </ext:TabPanel>
                


            </Items>

        </ext:Viewport>



        <ext:Window
    ID="imageSelectionWindow"
    runat="server"
    Icon="PageEdit"
 
    Width="400"
    Height="400"
    AutoShow="false"
    Modal="true"
    Hidden="true"
    Resizable="false"
    Maximizable="false"
    Layout="Fit">

    <Items>

        <ext:FormPanel
            ID="imageUploadForm"
            runat="server" DefaultButton="SaveButton"
     
            Icon="ApplicationSideList"
            Header="false"
            DefaultAnchor="100%"
            BodyPadding="5">
            <Content>
                <%--    <div class="imageBox" style="width: 290px; height: 270px;display:none;">
                            <div class="spinner" style="display: none"></div>
                            <div class="thumbBox" style="width: 290px; height: 270px; border: 3px solid black;display:none;" onclick="App.employeeControl1_uploadPhotoButton.setDisabled(false);"></div>
                            <input type="button" id="btnZoomIn" value="+" style="float: right;display:none;">
                            <input type="button" id="btnZoomOut" value="-" style="float: right;display:none;">
                        </div>--%>
                <div>
                    <img width="200" height="300" src="" id="image"  crossorigin="Anonymous" />
                    <input type="button" id="button" value="press me" style="display: none;" />

                </div>
            </Content>
            <Items>
                <ext:Image runat="server" Width="150" Height="300" ID="employeePhoto" Hidden="true" Visible="false">
                </ext:Image>
                <%--<ext:Hidden runat="server" ID="imageData" Name="imageData" Visible="false" />--%>
            </Items>
            <BottomBar>
                <ext:Toolbar runat="server">
                    <Items>

                        <ext:ToolbarFill runat="server" />

                        <ext:Button runat="server" Icon="PictureAdd" Text="BrowsePicture">
                            <Listeners>
                                <Click Handler="triggierImageClick(App.FileUploadField1.fileInputEl.id); "></Click>
                            </Listeners>
                        </ext:Button>
                        <ext:Button runat="server" ID="uploadPhotoButton" Icon="DatabaseSave" Text=" UploadPicture">
                            <Listeners>

                                <Click Handler="CheckSession();   if (!#{imageUploadForm}.getForm().isValid() ) {  return false;}  GetCroppedImage(); "  ></Click>                        
   

                            </Listeners>
                            <%--      <DirectEvents>
                                        <Click OnEvent="UploadImage" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                            
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{imageSelectionWindow}.body}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="values" Value="#{imageUploadForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                                                
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>--%>
                        </ext:Button>
                        <ext:Button runat="server" Icon="Cancel" Text="RemovePicture ">
                            <Listeners>
                                <Click Handler=" InitCropper('Images/empPhoto.jpg'); App.uploadPhotoButton.setDisabled(false); " />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="RemovePicture"></Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:FileUploadField ID="FileUploadField1" runat="server" ButtonOnly="true" Hidden="true">
                            <Listeners>
                                <Change Handler="showImagePreview(App.FileUploadField1.fileInputEl.id); showImagePreview2(App.FileUploadField1.fileInputEl.id); " />
                            </Listeners>

                        </ext:FileUploadField>
                        <ext:ToolbarFill runat="server" />
                    </Items>

                </ext:Toolbar>

            </BottomBar>



            <Listeners>

                <AfterLayout Handler="CheckSession();" />
                   
                           
            </Listeners>
            <DirectEvents>
                <AfterLayout OnEvent="DisplayImage">
                </AfterLayout>
            </DirectEvents>
        </ext:FormPanel>


    </Items>

</ext:Window>


    </form>
</body>
</html>
