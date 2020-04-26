<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlankPage.aspx.cs" Inherits="Web.UI.Forms.BlankPage" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Branches.js?id=1" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
   
    
       
  

 
</head>
<body style="background: url(Images/bg.png) repeat;" onload="GetTimeZone();">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="timeZoneOffset" runat="server" EnableViewState="true" />
          <ext:Hidden ID="Yes" runat="server" Text="<%$ Resources:Common , Yes %>"  />
          <ext:Hidden ID="No" runat="server" Text="<%$ Resources:Common , No %>"  />
          <ext:Hidden ID="branchId" runat="server" Text=""  />
        
      
      
     


    
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
         
        </ext:Viewport>

       
        


    </form>
</body>
</html>
