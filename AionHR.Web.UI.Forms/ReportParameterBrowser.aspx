<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportParameterBrowser.aspx.cs" Inherits="AionHR.Web.UI.Forms.ReportParameterBrowser" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Nationalities.js?id=1"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form2" runat="server">
        <ext:ResourceManager ID="ResourceManager2" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="Hidden1" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="Hidden2" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="Hidden3" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="Hidden4" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden runat="server" ID="labels" />
        <ext:Hidden runat="server" ID="texts" />
        <ext:Viewport ID="Viewport2" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel
                    ID="FormPanel1"
                    runat="server" 
                    Header="false" DefaultButton="Button1"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%" BodyPadding="5">

                    <Items>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button1" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                <Click Handler="CheckSession(); if (!#{FormPanel1}.getForm().isValid()) {return false;} " />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="SaveProperties" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                    <EventMask ShowMask="true" Target="CustomTarget" />
                                    <ExtraParams>
                                        <ext:Parameter Name="values" Value="#{FormPanel1}.getForm().getValues()" Mode="Raw" Encode="true" />
                                    </ExtraParams>
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
