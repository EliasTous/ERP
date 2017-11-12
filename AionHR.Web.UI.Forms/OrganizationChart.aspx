<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrganizationChart.aspx.cs" Inherits="AionHR.Web.UI.Forms.OrganizationChart" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="CSS/OrganizationChart.css?id=4" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" type="text/javascript"></script>

    <script src="https://superal.github.io/canvas2image/canvas2image.js" type="text/javascript"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    
    <script type="text/javascript" src="Scripts/OrganizationChart.js?id=24" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
   

 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        
        <ext:Toolbar runat="server" ID="topBar" >
            <Items>
                <ext:Button runat="server" Text="<%$ Resources:Print %>" >
                    <Listeners>
                        <Click Handler="CheckSession(); PrintElem('chart_div');" />
                        
                    </Listeners>
                   
                </ext:Button>
                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                  <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="type" Editable="false" Name="type" FieldLabel="<%$ Resources: type %>" AllowBlank="false" ForceSelection="true"  >
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="0"   />
                                        <ext:ListItem Text="<%$ Resources: adminType %>" Value="2" />
                                        
                                    </Items>
                                 
                                </ext:ComboBox>

                   <ext:Button runat="server" Text="<%$ Resources: Common,Go%>" MarginSpec="0 0 0 0" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="FillHirarichy"></Click>
                                    </DirectEvents>
                                </ext:Button>
            </Items>
        </ext:Toolbar><div id="chart_div" ></div>


     
        
       

        

       



    </form>
</body>
</html>
