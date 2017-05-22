﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AionHR.Web.UI.Forms.Login" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--    <meta charset="utf-8"/>--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href='http://fonts.googleapis.com/css?family=Lato:400,700' rel='stylesheet' type='text/css'>

    <link rel="stylesheet" type="text/css" href="CSS/Header.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
   <%-- <script src="Scripts/jquery-new.js"></script>
    <link href="CSS/PasswordStrength.css?id=3" rel="stylesheet" />
    <script src="Scripts/strength.js"></script>--%>
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
                <img src="Images/logo2.png"  width="80" height="57"  />
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
                <a class="button" href="ARLogin.aspx">
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
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Common , CopyRight%>" /></span>


    </div>
    <ext:Hidden runat="server" ID="lblLoading" Text="<%$Resources:Common , Loading %>" />
    <form id="Form1" runat="server">
       
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="12000" />

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
                    Icon="LockGo"
                    Title="<%$ Resources:Login %>"
                    Draggable="false"
                    Width="400"
                    Frame="true"
                    Layout="FormLayout"
                    BodyPadding="10" AutoUpdateLayout="false" DefaultAnchor="100%"
                    DefaultButton="btnLogin" Border="false" Shadow="true">
                    <FieldDefaults PreserveIndicatorIcon="false" />
                    <Items>
                      <ext:TextField
                            ID="tbAccountName"
                            runat="server"
                            AutoFocus="true"
                            
                            MsgTarget="Side"
                            FieldLabel="<%$ Resources:  Account %>"
                            
                            BlankText="<%$ Resources: Common, MandatoryField %>"
                            EmptyText="<%$ Resources:  EnterYourAccount %>">

                            
                            <Listeners>
                                <Change Handler=" App.direct.CheckFieldDirect(#{tbAccountName}.value,{
                success: function (result) {
                    if(result=='1'){
                    App.tbAccountName.setIndicatorIconCls('icon-tick'); App.tbAccountName.setIndicatorIconCls('icon-tick');}
                                    else
                                     {App.tbAccountName.setIndicatorIconCls('');App.tbAccountName.setIndicatorIconCls('');}
                }
            });   " />
                                <FocusLeave Handler="App.direct.CheckFieldDirect(#{tbAccountName}.value,{
                success: function (result) {
                    if(result=='1')
                    App.tbAccountName.setIndicatorIconCls('icon-tick');
                                    else{
                                    
                                    App.tbAccountName.setIndicatorIconCls('icon-error');App.tbAccountName.setIndicatorIconCls('icon-error');}
                }
            });    " />
                                <FocusEnter Handler="App.direct.CheckFieldDirect(#{tbAccountName}.value,{
                success: function (result) {
                    if(result=='1'){
                    App.tbAccountName.setIndicatorIconCls('icon-tick'); App.tbAccountName.setIndicatorIconCls('icon-tick');}
                                    else
                                     {App.tbAccountName.setIndicatorIconCls('');App.tbAccountName.setIndicatorIconCls('');}
                }
            });   " />
                                <%--<RemoteValidationValid Handler="this.setIndicatorIconCls('icon-tick');this.setIndicatorIconCls('icon-tick'); " />--%>
                               <%--<RemoteValidationInvalid Handler="this.setIndicatorIconCls('icon-error'); " />--%>
                            </Listeners>

                        </ext:TextField>

                        <ext:TextField ID="tbUsername"
                            runat="server"
                            MsgTarget="Side" 
                            BlankText="<%$ Resources:Common, MandatoryField %>"
                            AllowBlank="false"
                            FieldLabel="<%$ Resources:  UserID %>"
                            EmptyText="<%$ Resources:  EnterYourID %>" />
                        <ext:TextField ID="tbPassword"
                            runat="server" 
                            AllowBlank="false"
                            MsgTarget="Side"
                            BlankText="<%$ Resources:Common , MandatoryField %>"
                            FieldLabel="<%$ Resources: Password %>"
                            EmptyText="<%$ Resources: EnterYourPassword %>"
                            InputType="Password"  >
                        
                            </ext:TextField>
                        
                        <ext:Checkbox ID="rememberMeCheck" runat="server" FieldLabel="<%$ Resources: RememberMe %>" InputValue="True" />
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
                        <ext:Button ID="btnLogin" runat="server" Text="<%$ Resources:  Login %>">
                            <Listeners>
                                <Click Handler="
                                        if (!#{panelLogin}.validate()|| #{tbAccountName}.value=='') {                                
                                            return false;
                                        }
                                    
                                      Ext.net.Mask.show({msg:App.lblLoading.getValue(),el:#{panelLogin}.id});
                                    
                                    App.direct.Authenticate(#{tbAccountName}.value,#{tbUsername}.value,#{tbPassword}.value, {
                    success: function (result) { 
                       if(result=='1')
                                    {
                                    window.open('Default.aspx','_self');
                                    }
                                    else
                                    {
                                    Ext.net.Mask.hide();
                                    }
                    }
                  
                }); " />
                            </Listeners>
                          
                        </ext:Button>
                        
                        <ext:Button ID="btnForgot" runat="server" Text="<%$ Resources:Common , ResetPassword %>">
                            <DirectEvents>
                                <Click OnEvent="forgotpw_Event">
                                    <EventMask ShowMask="true" Msg="<%$ Resources:Common , Loading %>" MinDelay="500" />
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
