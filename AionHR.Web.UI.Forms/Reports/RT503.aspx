﻿
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT503.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT503" %>


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
    <link rel="stylesheet" type="text/css" href="../CSS/RT200.css?id=2" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Dashboard.js"></script>
    <!--  <script type="text/javascript" src="Scripts/app.js"></script>-->
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script src="https://superal.github.io/canvas2image/canvas2image.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/moment.js"></script>
    <script type="text/javascript" src="../Scripts/RT200.js?id=18"></script>
    <script type="text/javascript">
        function alertNow(s, e) {
            Ext.MessageBox.alert('Error', e.message);
            e.handled = true;
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
         <ext:Hidden ID="hint" runat="server" Text="<%$ Resources:Common , hint %>" />
          <ext:Hidden ID="EmptyPayRef" runat="server" Text="<%$ Resources:Common , EmptyPayRef %>" />

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
                        <ext:Toolbar runat="server" Height="60">

                            <Items>
                           <%--  <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                               
                                               
                                                 
                                                <uc:payRefCombo runat="server" ID="payRefFilter" />
                                            </Content>
                                        </ext:Container>--%>
                                     <ext:TextField Visible="false" Width="120" runat="server" ID="payRef" EmptyText="<%$Resources:Common , PayRef %>" />
                              
                                     <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"  EmptyText="<%$Resources:Common , PayRef %>"  Name="payId" runat="server" DisplayField="payRefWithDateRange" ValueField="recordId" ID="payId" Width="250">
                                    <Store>
                                        <ext:Store runat="server" ID="payIdStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>

                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="payRefWithDateRange" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                   
                                 
                                </ext:ComboBox>
                                        <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                               
                                                <%--<uc:dateRange runat="server" ID="dateRange1" />--%>
                                                <uc:jobInfo runat="server" ID="jobInfo1" EnablePosition="true" EnableDivision="false" EnableDepartment="true" />
                                            </Content>
                                        </ext:Container>
                                   <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                                <%--<uc:dateRange runat="server" ID="dateRange1" />--%>
                                                <uc:paymentMethodCombo runat="server" ID="paymentMethodCombo" />
                                            </Content>
                                        </ext:Container>

                                   
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                         <ext:Button runat="server" Text="<%$Resources:Common, Go %>" >
                                            <Listeners>
                                                  <Click Handler=" if(App.payId.getValue()==null)  {Ext.MessageBox.alert(#{hint}.value,#{EmptyPayRef}.value );return ;}   callbackPanel.PerformCallback('1');" />
                                            </Listeners>
                                        </ext:Button>
                                    </Content>
                                </ext:Container>
                                       
                        

                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <Content>

                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="callbackPanel"  ClientSideEvents-CallbackError="alertNow"


                            Width="100%" OnCallback="ASPxCallbackPanel1_Callback" OnLoad="ASPxCallbackPanel1_Load" >
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server" ></dx:ASPxWebDocumentViewer>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </Content>
                    <Items>
                        <ext:Label runat="server" Text="fff" Hidden="true" />
                    </Items>
                </ext:Panel>


            </Items>
        </ext:Viewport>






    </form>
</body>
</html>