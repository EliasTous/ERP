﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT402.aspx.cs" Inherits="Web.UI.Forms.Reports.RT402" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

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
    <script type="text/javascript" src="../Scripts/RT101.js?id=18"></script>
    <script type="text/javascript">
        function alertNow(s, e) {

            Ext.MessageBox.alert(App.Error.getValue(), e.message);
            e.handled = true;
        }
        function cheackEmployeeValue()
        {
           
            if (App.employeeFilter.value === null) {
                Ext.MessageBox.alert(App.ErrorTitlEmptyValue.getValue(), App.ErrorEmptyValue.getValue());
                e.handled = false;
            }
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
        <ext:Hidden ID="ErrorEmptyValue" runat="server" Text="<%$ Resources:Common , ErrorEmptyValue %>" />
          <ext:Hidden ID="ErrorTitlEmptyValue" runat="server" Text="<%$ Resources:Common , Error %>" />
               <ext:Hidden ID="Error" runat="server" Text="<%$ Resources:Common , Error %>" />
         <ext:Hidden ID="dtIdValue" runat="server" Text="" />

        

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
                                  
                             
                                                                     
                                                                     
                               
                               <%-- <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                            
                                                <uc:employeeCombo runat="server" ID="employeeCombo1" />
                                            </Content>
                                        </ext:Container>--%>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="employeeFilter" Width="120" LabelAlign="Top"
                                                    DisplayField="fullName"
                                                    ValueField="recordId" AllowBlank="true"
                                                    TypeAhead="false"
                                                    HideTrigger="true" SubmitValue="true"
                                                    MinChars="3" EmptyText="<%$ Resources: FilterEmployee%>"
                                                    TriggerAction="Query" ForceSelection="true">
                                                    <Store>
                                                        <ext:Store runat="server" ID="Store2" AutoLoad="false">
                                                            <Model>
                                                                <ext:Model runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="recordId" />
                                                                        <ext:ModelField Name="fullName" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                            <Proxy>
                                                                <ext:PageProxy DirectFn="App.direct.FillEmployee"></ext:PageProxy>
                                                            </Proxy>
                                                        </ext:Store>
                                                    </Store>
         

                                                </ext:ComboBox>
                            
                                
                                
                                <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                         <ext:Button runat="server" Text="<%$Resources:Common, Go %>" >
                                            <Listeners>
                                                <Click Handler="cheackEmployeeValue();callbackPanel.PerformCallback('1');" />
                                             
                                            </Listeners>
                                             
                                        </ext:Button>
                                    </Content>
                                </ext:Container>
                                       
                        

                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <Content>

                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server"  ClientSideEvents-CallbackError="alertNow" ClientInstanceName="callbackPanel"
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
