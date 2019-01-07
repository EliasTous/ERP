<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelfServiceResetPasswords.aspx.cs" Inherits="AionHR.Web.UI.Forms.SelfServiceResetPasswords" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=2" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
      <script type="text/javascript" src="Scripts/Users.js?id=2"></script>
    <script type="text/javascript" src="Scripts/common.js?id=20"></script>
    <script type="text/javascript" src="Scripts/moment.js?id=10"></script>
     <script type="text/javascript" src="Scripts/jquery-new.js"></script>
   <script type="text/javascript">
 Ext.define("Ext.plugin.extjs.form.PasswordStrength", {
            extend: "Ext.AbstractPlugin",
            alias: "plugin.passwordstrength",
            colors: ["C11B17", "FDD017", "4AA02C", "6AFB92", "00FF00"],

            init: function (cmp) {
                var me = this;

                App.PasswordField.on("change", me.onFieldChange, me);


            },

            onFieldChange: function (field, newVal, oldVal) {
                if (newVal === "") {


                    App.pro.updateText('');
                    $("#pro-bar")[0].style.backgroundColor = "white";
                    App.pro.setStyle({ "background-color": "white" });
                    return;
                }
                var me = this,
                    score = me.scorePassword(field.value);



                me.processValue(field, score);


            },

            processValue: function (field, score) {

                var me = this,
                    colors = me.colors,
                    color;
                var i;

                if (score < 16) {
                    i = 1;
                    color = colors[0]; //very weak
                } else if (score > 15 && score < 25) {
                    i = 2;
                    color = colors[1]; //weak
                } else if (score > 24 && score < 35) {
                    i = 3;

                    color = colors[2]; //mediocre
                } else if (score > 34 && score < 45) {
                    i = 4;
                    color = colors[3]; //strong
                } else {
                    i = 5;

                    color = colors[4]; //very strong
                }



                App.pro.setValue(i / 5);





                App.pro.updateText(document.getElementById("level" + i).value);


                $("#pro-bar")[0].style.backgroundColor = "#" + colors[i];

            },

            scorePassword: function (passwd) {
                var score = 0;

                if (passwd.length < 5) {
                    score += 3;
                } else if (passwd.length > 4 && passwd.length < 8) {
                    score += 6;
                } else if (passwd.length > 7 && passwd.length < 16) {
                    score += 12;
                } else if (passwd.length > 15) {
                    score += 18;
                }

                if (passwd.match(/[a-z]/)) {
                    score += 1;
                }

                if (passwd.match(/[A-Z]/)) {
                    score += 5;
                }

                if (passwd.match(/\d+/)) {
                    score += 5;
                }

                if (passwd.match(/(.*[0-9].*[0-9].*[0-9])/)) {
                    score += 5;
                }

                if (passwd.match(/.[!,@,#,$,%,^,&,*,?,_,~]/)) {
                    score += 5;
                }

                if (passwd.match(/(.*[!,@,#,$,%,^,&,*,?,_,~].*[!,@,#,$,%,^,&,*,?,_,~])/)) {
                    score += 5;
                }

                if (passwd.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {
                    score += 2;
                }

                if (passwd.match(/([a-zA-Z])/) && passwd.match(/([0-9])/)) {
                    score += 2;
                }

                if (passwd.match(/([a-zA-Z0-9].*[!,@,#,$,%,^,&,*,?,_,~])|([!,@,#,$,%,^,&,*,?,_,~].*[a-zA-Z0-9])/)) {
                    score += 2;
                }

                return score;
            }
        });
   </script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden runat="server" ID="level1" Text="<%$ Resources:VeryWeak %>" />
        <ext:Hidden runat="server" ID="level2" Text="<%$ Resources:Weak %>" />
        <ext:Hidden runat="server" ID="level3" Text="<%$ Resources:Mediocre %>" />
        <ext:Hidden runat="server" ID="level4" Text="<%$ Resources:Strong %>" />
        <ext:Hidden runat="server" ID="level5" Text="<%$ Resources:VeryStrong %>" />
       
           <ext:Window 
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="elias"
            Width="400"
            Height="400"
            AutoShow="false"
            Draggable="true"
            Maximizable="true"
            Resizable="false"
            Modal="true"
            Hidden="true"
            Layout="Fit" AutoScroll="true"
            >
            <Listeners>
                <Close Handler="closeCurrentTab();"></Close>
            </Listeners>
            <Items>
                   <ext:TabPanel ID="panelRecordDetails" Layout="FitLayout" DefaultAnchor="100%" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" Region="Center">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarFill runat="server">
                        </ext:ToolbarFill>

                    </Items>
                </ext:Toolbar>
            </TopBar>

            <Items>
            <ext:FormPanel DefaultButton="SaveButton"
                    ID="BasicInfoTab" PaddingSpec="0 0 0 0"
                    runat="server"
                    Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5" Layout="VBoxLayout">


                    <Items>
                         <ext:TextField
                                    ID="oldPassword" TabIndex="8" Width="375"
                                    runat="server"
                                    Vtype="password"
                                    FieldLabel="<%$ Resources: FieldOldPassword%>"
                                    InputType="Password"
                                    AnchorHorizontal="100%" AllowBlank="false" />
                             
                            <ext:TextField
                                    ID="PasswordField" TabIndex="9" Width="375"
                                    runat="server"
                                    FieldLabel="<%$ Resources: FieldPassword%>"
                                    InputType="Password"
                                    Name="password"
                                    DataIndex="password"
                                    AllowBlank="false"
                                    AnchorHorizontal="100%">
                                    <Listeners>
                                        <ValidityChange Handler="this.next().next().validate();" />
                                        <Blur Handler="this.next().next().validate();" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:GenericPlugin TypeName="passwordstrength" />
                                    </Plugins>
                                </ext:TextField>
                        
                                <ext:ProgressBar runat="server" ID="pro" Width="270" MarginSpec="0 0 0 105">
                                    <Listeners>
                                        <Render Handler="this.updateText('');" />
                                    </Listeners>
                                </ext:ProgressBar>

                                <ext:TextField Width="375"
                                    ID="PasswordConfirmation" TabIndex="10"
                                    runat="server"
                                    Vtype="password"
                                    FieldLabel="<%$ Resources: FieldConfirmPassword%>"
                                    InputType="Password"
                                    AnchorHorizontal="100%">
                                    <Validator Handler="if(this.value!= this.prev().prev().value) return false; else return true;">
                                    </Validator>
                                    <CustomConfig>
                                        <ext:ConfigItem Name="initialPassField" Value="PasswordField" Mode="Value" />
                                    </CustomConfig>
                                </ext:TextField>
                        </Items>
                   <Buttons>
                                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk" TabIndex="10">

                                    <Listeners>
                                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) { return false;}" />
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
                                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel" TabIndex="11">
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
