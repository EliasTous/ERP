<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Web.UI.Forms.ForgotPassword" %>

<%@ Register TagPrefix="ext" Namespace="Ext.Net" Assembly="Ext.Net" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--    <meta charset="utf-8"/>--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />



    <link rel="stylesheet" type="text/css" href="CSS/Header.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />

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
                <img src="Images/logo-light.png" style="margin-top:20px;margin-left:5px;margin-right:5px;"  width="73" height="20" />
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
                <a class="button" href="ARForgotPassword.aspx">
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
                    Title="<%$ Resources: RequestPasswordRecovery %>"
                    Draggable="false"
                    Width="400"
                    Frame="true"
                    Layout="FormLayout"
                    BodyPadding="10" AutoUpdateLayout="false" DefaultAnchor="100%"
                    DefaultButton="btnLogin" Border="false" Shadow="true">
                    <FieldDefaults PreserveIndicatorIcon="true" />

                    <Items>
                        <ext:TextField
                            ID="tbAccountName"
                            runat="server"
                            AutoFocus="true"
                            IsRemoteValidation="true"
                            ValidateBlank="false" ValidateOnBlur="true"
                            FieldLabel="<%$ Resources:  Account %>"
                            AllowBlank="false"
                            BlankText="<%$ Resources: Common, MandatoryField %>"
                            EmptyText="<%$ Resources:  EnterYourAccount %>">

                            <RemoteValidation Delay="2000" OnValidation="CheckField">
                                <EventMask ShowMask="true" CustomTarget="#{panelLogin}" />
                            </RemoteValidation>
                            <Listeners>

                                <RemoteValidationValid Handler="this.setIndicatorIconCls('icon-tick');this.setIndicatorIconCls('icon-tick'); " />
                                <RemoteValidationInvalid Handler="this.setIndicatorIconCls('icon-error'); " />
                            </Listeners>

                        </ext:TextField>

                        <ext:TextField ID="tbUsername"
                            runat="server"
                             Vtype="email" ValidateBlank="false" ValidateOnBlur="true"
                            BlankText="<%$ Resources:Common, MandatoryField %>"
                            AllowBlank="false"
                            FieldLabel="<%$ Resources:  UserID %>"
                            EmptyText="<%$ Resources:  EnterYourID %>" />

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
                        <ext:Button ID="btnLogin" runat="server" Text="Reset">
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
