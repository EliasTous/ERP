﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resetPw.aspx.cs" Inherits="AionHR.Web.UI.Forms.resetPw" %>

<%@ Register TagPrefix="ext" Namespace="Ext.Net" Assembly="Ext.Net" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--    <meta charset="utf-8"/>--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />



    <link rel="stylesheet" type="text/css" href="CSS/Header.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
        <script type="text/javascript">
        Ext.define("Ext.plugin.extjs.form.PasswordStrength", {
            extend: "Ext.AbstractPlugin",
            alias: "plugin.passwordstrength",
            colors: ["C11B17", "FDD017", "4AA02C", "6AFB92", "00FF00"],

            init: function (cmp) {
                var me = this;

                App.tbPassword.on("change", me.onFieldChange, me);
                
               
            },

            onFieldChange: function (field, newVal, oldVal) {
                if (newVal === "") {
                    App.progress.inputEl.setStyle({
                        "background-color": null,
                        "background-image": null
                    });
                   
                    App.progress.rightButtons[0].setStyle({
                        "background-color": null,
                        "background-image": null
                    });
                    App.progress.rightButtons[0].setText('');
                    App.progress.score = 0;
                    App.progress.setValue('');
                    return;
                }
                var me = this,
                    score = me.scorePassword(App.tbPassword.value);

                App.progress.score = score;

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

                App.progress.inputEl.setStyle({
                    "background-color": "#" + color,
                    "background-image": "none"
                });
                App.progress.rightButtons[0].setStyle({
                    
                    "background-color": "#" + color,
                    "padding-right":"100px"
                });
                
                App.progress.rightButtons[0].setStyle({

                    "padding-right": ((5 - i) * 50) + "px"
                });
                if (i == 5)
                {
                    App.progress.rightButtons[0].setWidth(0);

                }
                
               
             
                App.progress.setValue(document.getElementById("level" + i).value);
               
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
   
    <style type="text/css">
        .error {
            color: red;
        }
    </style>
    <title>
        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Common , ApplicationTitle%>" /></title>
</head>
<body style="background: url(Images/bg.png)">

    <div class="header">
        <div class="left">
            <div class="logoImage">
                <img src="Images/logo2.png" width="90" height="82" />
            </div>
            <div class="title">
                <div style="width: 400px">
                    <span class="title-sub">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Common ,ApplicationTitle%>" /></span>
                </div>
                <div class="SubTitles">
                    <span class="subTitleSpan">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Common ,ApplicationModule%>" /></span>
                </div>
            </div>
        </div>
        <div class="right">
            <div class="button-group" style="margin-top: 15px;">
                <a class="button" href="ARresetPW.aspx">
                    <asp:Literal ID="Literal8" runat="server" Text="عربي" /></a>
            </div>
        </div>
    </div>

    <div class="borderFooter">
        <div class="c1 portion"></div>
        <div class="c1 portion"></div>
        <div class="c1 portion"></div>
        <div class="c1 portion"></div>
        <div class="c1 portion"></div>
    </div>
    <div class="footer">

        <span class="footer__note title-sub">
            <asp:Literal ID="Literal1" runat="server" Text="" /></span>


    </div>

    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
            <ext:Hidden runat="server" ID="level1" Text="<%$ Resources:VeryWeak %>" />
        <ext:Hidden runat="server" ID="level2" Text="<%$ Resources:Weak %>" />
        <ext:Hidden runat="server" ID="level3" Text="<%$ Resources:Mediocre %>" />
        <ext:Hidden runat="server" ID="level4"  Text="<%$ Resources:Strong %>"/>
        <ext:Hidden runat="server" ID="level5"  Text="<%$ Resources:VeryStrong %>"/>
        <ext:Viewport ID="Viewport1" runat="server">
            <Defaults>
                <ext:Parameter Name="margin" Value="100 0 5 0" Mode="Value" />
            </Defaults>
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Center" />
            </LayoutConfig>
            <Items>
                <ext:FormPanel 
                    ID="panelLogin"
                    runat="server"
                    Closable="false"
                    Resizable="false"
                    Icon="LockGo"
                    Title="<%$ Resources:ResetPassword%>"
                    Draggable="false"
                    Width="500"
                    Modal="false"
                    Frame="true"
                    BodyPadding="20"
                    Layout="FormLayout"
                    DefaultButton="btnLogin" Border="false" Shadow="true">

                    <Items>
                        <ext:TextField TabIndex="1"
                            ID="tbPassword"
                            runat="server" Anchor="-5" 
                            AutoFocus="true"
                            InputType="Password"
                            FieldLabel="<%$ Resources:NewPassword%>"
                            AllowBlank="false"  
                            BlankText=""
                             MaxWidth="200"
                            EmptyText=""  >
                            <Listeners>
                            <ValidityChange Handler="this.next().next().validate();" />
                            <Blur Handler="this.next().next().validate();" />
                        </Listeners>
                            
                            </ext:TextField>
                         
                       <ext:TextField runat="server" ReadOnly="true" Height="10" ID="progress" FieldLabel="Strength"  TabIndex="4">
                                         <Plugins>
                                <ext:GenericPlugin TypeName="passwordstrength" />
                            </Plugins>
                            <RightButtons>
                                <ext:Button runat="server" ID="Button1" />
                                <ext:HyperlinkButton runat="server" ID="HyperlinkButton1"   />
                            </RightButtons>
                                   
                                    <Listeners>
                                        <Focus Handler="this.blur()" />
                                    </Listeners>
                                </ext:TextField>
                        <ext:TextField ID="tbPasswordConfirm" TabIndex="2"
                            runat="server"
                            BlankText=""
                            InputType="Password"
                            AllowBlank="false"
                            FieldLabel="<%$ Resources:PasswordConfirm%>"
                            
                            EmptyText="" >
                            <Validator Handler="if(this.value!= this.prev().prev().value) return false; else return true;">
                                
                            </Validator>
                             <CustomConfig>
                        <ext:ConfigItem Name="initialPassField" Value="tbPassword" Mode="Value" />
                    </CustomConfig>
                            </ext:TextField>
                        
                        <ext:FieldContainer runat="server" ID="lblErroContainer" FieldLabel="">
                            <Items>
                                <ext:Label ID="lblError"
                                    runat="server"
                                    Text=""
                                    Visible="true"
                                    Cls="error" />
                            </Items>
                        </ext:FieldContainer>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnLogin" runat="server" Text="<%$ Resources:ResetPassword%>" TabIndex="3">
                            <Listeners>
                                <Click Handler="
                            if (!#{panelLogin}.validate()) {                                
                                return false;
                            }" />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="login_Click">
                                    <EventMask ShowMask="true" Msg="" MinDelay="500" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                   
                        
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>


</body>
</html>
