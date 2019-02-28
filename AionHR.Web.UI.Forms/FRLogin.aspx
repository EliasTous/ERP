
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRLogin.aspx.cs" Inherits="AionHR.Web.UI.Forms.FRLogin" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--    <meta charset="utf-8"/>--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href='http://fonts.googleapis.com/css?family=Lato:400,700' rel='stylesheet' type='text/css'>

    <link rel="stylesheet" type="text/css" href="CSS/Header.css?id=2" />
    <link rel="stylesheet" type="text/css" href="CSS/Common.css?id=1" />
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
                <img src="Images/logo-light.png" style="margin-top:20px;margin-left:5px;margin-right:5px;"  width="73" height="20" />
            </div>
            <div class="title">
                <div style="width: 400px">
                    <span class="title-sub">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Common ,ApplicationTitle%>" /></span>
                </div>
             <%--   <div class="SubTitles">
                    <span class="subTitleSpan">
                        <asp:Literal ID="Literal3" runat="server"  /></span>
                </div>--%>
            </div>
        </div>
        <div class="right">
            <div class="button-group" style="margin-top: 15px;">
      
       
        <ext:ComboBox FieldStyle="background-color:#6fb0e9; color: white;text-align: center; font-weight: bold;
    font-size: 15px;
    text-decoration: none;
    text-align: center;
    cursor: pointer;"  
            ID="languageId"
            runat="server"
            DisplayField="text"
            ValueField="value" ForceSelection="true" Width="75"  AnyMatch="true"   CaseSensitive="false"  QueryMode="Local"  TypeAhead="true" MinChars="1" >
         
            <Store>
                <ext:Store runat="server">
                    <Model>
                        <ext:Model runat="server">
                            <Fields>
                                <ext:ModelField Name="value" />
                                <ext:ModelField Name="text" />
                          
                            </Fields>
                        </ext:Model>
                    </Model>
                    <Reader>
                        <ext:ArrayReader />
                    </Reader>
                </ext:Store>
            </Store>
          <Listeners>
                 <FocusEnter Handler="App.languageId.onTriggerClick();"   />            
            </Listeners>
            <DirectEvents>
                <Select OnEvent="Change_language" >
                    <ExtraParams>
                                <ext:Parameter Name="value" Value="this.value" Mode="Raw" />
                              
                            </ExtraParams>
                    </Select>
            </DirectEvents>
            <ListConfig>
                <Tpl runat="server" >
                    <Html>
                        <ul class="x-list-plain">
                            <tpl for=".">
                                <li role="option" class="x-boundlist-item" style="background-color:#6fb0e9;color:white; text-align: center;font-weight: bold;
                                                  font-size: 15px;
                                                     text-decoration: none;
                                                             text-align: center;
                                                          cursor: pointer;">
                                    {text}
                                </li>
                            </tpl>
                        </ul>
                    </Html>
                </Tpl>                              
            </ListConfig> 
        </ext:ComboBox>
    
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
                            vtype="email"
                             ValidateOnChange="false"
                             ValidateOnBlur="true"
                            BlankText="<%$ Resources:Common, MandatoryField %>"
                            AllowBlank="false"
                            FieldLabel="<%$ Resources:  UserID %>"
                            EmptyText="<%$ Resources:  EnterYourID %>" />
                        <ext:TextField ID="tbPassword"
                            runat="server" 
                            AllowBlank="false"
                            
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
