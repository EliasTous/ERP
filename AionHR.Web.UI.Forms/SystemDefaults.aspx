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
    <script type="text/javascript" >
        function ValidateIPaddress(ipaddress) {
            if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(ipaddress)) {
                return (true);
            }
            
            return (false);
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





        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:Panel
                    ID="EditRecordWindow"
                    runat="server"
                    Icon="PageEdit"
                    Title="<%$ Resources:EditWindowsTitle %>"
                    Width="450"
                    Height="330"
                    AutoShow="false"
                    Modal="true"
                    Hidden="False"
                    Layout="Fit">

                    <Items>
                        <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                            <Items>
                                <ext:FormPanel DefaultButton="SaveButton"
                                    ID="BasicInfoTab"
                                    runat="server"
                                    Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                                    Icon="ApplicationSideList"
                                    DefaultAnchor="100%"
                                    BodyPadding="5">
                                    <Items>

                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  LabelWidth="150" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldCountry %>" Name="countryId" runat="server" DisplayField="name" ValueField="recordId" ID="countryIdCombo">
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
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldCurrency %>" Name="currencyId" DisplayField="name" ValueField="recordId" runat="server" ID="currencyIdCombo">
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
                                         <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldWorkingCalendar %>" Name="caId" DisplayField="name" ValueField="recordId" runat="server" ID="caId">
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
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldVacationSchedule %>" Name="vsId" DisplayField="name" ValueField="recordId" runat="server" ID="vsId">
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
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldDateFormat %>" Name="dateFormat" runat="server" ID="dateFormatCombo">
                                            <Items>
                                                <ext:ListItem Text="Jan 31,2016" Value="MMM dd,yyyy" />
                                                <ext:ListItem Text="Jan 31,16" Value="MMM dd,yy" />
                                                <ext:ListItem Text="31/1/16" Value="dd/MM/yy" />
                                                <ext:ListItem Text="1/31/16" Value="MM/dd/yy" />
                                                <ext:ListItem Text="31/1/2016" Value="dd/MM/yyyy" />
                                            </Items>
                                        </ext:ComboBox>

                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldNameFormat %>" Name="nameFormat" runat="server" ID="nameFormatCombo">
                                            <Items>
                                                <ext:ListItem Text="<%$ Resources:FirstNameLastName %>" Value="{firstName} {lastName}" />
                                                <ext:ListItem Text="<%$ Resources:LastNameFirstName %>" Value="{lastName} {firstName}" />
                                                <ext:ListItem Text="<%$ Resources:FirstNameMiddleNameLastName %>" Value="{firstName} {middleName} {lastName}" />
                                                <ext:ListItem Text="<%$ Resources:ReferenceFirstNameLastName %>" Value="{reference} {firstName} {lastName}" />
                                                <ext:ListItem Text="<%$ Resources:ReferenceFirstNameMiddleNameLastName %>" Value="{reference} {firstName} {middleName} {lastName}" />
                                                <ext:ListItem Text="<%$ Resources:ReferenceLastNameFirstName %>" Value="{reference} {lastName} {firstName}" />
                                                

                                            </Items>
                                        </ext:ComboBox>
                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" LabelWidth="150" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldFirstDayOfWeek %>" Name="fdow" runat="server" ID="fdowCombo">
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


                                        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" LabelWidth="150"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldTimeZone %>" Name="timeZone" runat="server" ID="timeZoneCombo">
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
                                        <ext:TextField runat="server" Name="localServerIP" ID="localServerIP"  LabelWidth="150" FieldLabel="<%$Resources:LocalServerIP %>">
                                            <Validator Fn="ValidateIPaddress" />
                                        </ext:TextField>
                                        <ext:Checkbox FieldLabel="<%$ Resources: FieldEnableCamera %>" LabelWidth="150" runat="server" InputValue="True" Name="enableCamera" ID="enableCameraCheck" />
                                        <ext:Checkbox FieldLabel="<%$ Resources: FieldEnableHijri %>" LabelWidth="150" runat="server" InputValue="True" Name="enableHijri" ID="enableHijri" />
                                    </Items>

                                </ext:FormPanel>

                            </Items>
                        </ext:TabPanel>
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
            </Items>

        </ext:Viewport>






    </form>
</body>
</html>
