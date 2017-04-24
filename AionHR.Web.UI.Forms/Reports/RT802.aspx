<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT802.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT802" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v16.2.Web, Version=16.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="../CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="../CSS/RT101.css?id=2" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Dashboard.js"></script>
    <!--  <script type="text/javascript" src="Scripts/app.js"></script>-->
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script src="https://superal.github.io/canvas2image/canvas2image.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/moment.js"></script>
    <script type="text/javascript" src="../Scripts/RT201.js?id=18"></script>
    <script type="text/javascript">
        var prev = '';
        function printGrid(grid, window) {
            window.show();


            //bd.document.getElementById(grid.view.el.id).style.height = "auto";
            //bd.document.getElementById(grid.view.scroller.id).style.height = "auto";

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

        <ext:Hidden ID="rtl" runat="server" />
        <ext:Hidden ID="format" runat="server" />



        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">

            <Items>

                <ext:Panel
                    ID="Center"
                    runat="server"
                    Border="false"
                    Layout="FitLayout" AutoScroll="true"
                    Margins="0 0 0 0"
                    Region="Center">

                    <TopBar>
                        <ext:Toolbar runat="server" Height="50" Layout="HBoxLayout">
                            <Items>

                                <ext:Container runat="server" Layout="FitLayout" >
                                    <Content>
                                        <uc:dateRange runat="server" ID="dateRange1" />
                                    </Content>
                                </ext:Container>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:usersCombo runat="server" ID="userCombo1" EnableViewState="true" />
                                    </Content>
                                </ext:Container>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:moduleCombo runat="server" ID="moduleCombo1" EnableViewState="true" />
                                    </Content>
                                </ext:Container>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:transactionCombo runat="server" ID="transactionCombo1" EnableViewState="true" />
                                    </Content>
                                </ext:Container>
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                          <ext:Button runat="server" Text="<%$Resources:Common, Go %>" >
                                     <Listeners>
                                                <Click Handler="callbackPanel.PerformCallback('1');" />
                                            </Listeners>
                                </ext:Button>
                                    </Content>
                                </ext:Container>
                              

                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Content>
                        
                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="callbackPanel" OnLoad="ASPxCallbackPanel1_Load"
                            Width="100%" OnCallback="ASPxCallbackPanel1_Callback">
                            <panelcollection>
                                <dx:PanelContent runat="server">
                                    <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server"></dx:ASPxWebDocumentViewer>
                                </dx:PanelContent>
                            </panelcollection>
                        </dx:ASPxCallbackPanel>

                    </Content>
                    <Items>
                        <ext:Panel runat="server" Height="200" Layout="AutoLayout" Width="1000" AutoScroll="true" ID="toPrint" Hidden="true">
                            <Items>




                                <ext:Label runat="server" Text="ggg" Hidden="true" />
                            </Items>

                        </ext:Panel>
                    </Items>
                </ext:Panel>


            </Items>
        </ext:Viewport>







    </form>
</body>
</html>



