<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPropertyExplorer.aspx.cs" Inherits="AionHR.Web.UI.Forms.UserPropertyExplorer" %>

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
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden runat="server" ID="currentPropertyId" />
         <ext:Hidden runat="server" ID="currentEmployeeId" />
       
        <ext:Viewport ID="Viewport1" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
            <Items>

                
                   

                <ext:FormPanel
                   
                    ID="propertiesForm"
                    runat="server"
                    Header="false"
                    Icon="ApplicationSideList"   
                     Height="430"
    
                 Scrollable="Both"
                    MinHeight="400"
                    DefaultAnchor="100%" AutoUpdateLayout="true" >

                    <Items>
                    </Items>
                 <Buttons>
                        <ext:Button ID="SavePropertiesButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                <Click Handler=" if (!#{PropertiesForm}.getForm().isValid()) {return false;} " />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="SaveProperties" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                    <EventMask ShowMask="true" Target="CustomTarget" />
                                    <ExtraParams>
                                        <ext:Parameter Name="values" Value="Ext.encode(#{propertiesForm}.getForm().getValues(false, false, false, true))" Mode="Raw" />
                                       
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button Visible="false" ID="Button8" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                            <Listeners>
                                <Click Handler="parent.hideWindow()" />
                            </Listeners>
                     
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
