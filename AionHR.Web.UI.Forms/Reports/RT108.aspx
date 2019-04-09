<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT108.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT108" %>

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
    <script type="text/javascript" src="../Scripts/RT101.js?id=18"></script>
    <script type="text/javascript">
        function alertNow(s, e) {

            Ext.MessageBox.alert(App.Error.getValue(), e.message);
            e.handled = true;
        }
        function ShowParamSelection(pms) {
            alert(pms);
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
               <ext:Hidden ID="Error" runat="server" Text="<%$ Resources:Common , Error %>" />
        <ext:Hidden ID="rtl" runat="server" />
        <ext:Hidden ID="format" runat="server" />


        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">

            <Items>

                <ext:Panel
                    ID="Center"
                    runat="server"
                    Border="false"
                    AutoScroll="true"
                    Margins="0 0 0 0"
                    Region="Center">
                    <TopBar>
                        <ext:Toolbar runat="server" Height="60">

                            <Items>
                                  <ext:Container runat="server"  Layout="FitLayout">
                                    <Content>
                                        <uc:employeeCombo runat="server" ID="employeeFilter" />
                                    </Content>
                                </ext:Container>
                                
                                <ext:Container runat="server"  Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" EnableDepartment="false" EnableDivision="false" EnablePosition="false" />
                                    </Content>
                                </ext:Container>
                                  <ext:Container runat="server"  Layout="FitLayout">
                                    <Content>
                                        <uc:activeStatus runat="server" ID="activeControl" />
                                    </Content>
                                </ext:Container>
                                 <ext:ComboBox Width="85" AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" EmptyText="<%$ Resources: FieldCountry %>" Name="countryId" runat="server" DisplayField="name" ValueField="recordId" ID="countryIdCombo">
                                    <Store>
                                        <ext:Store runat="server" ID="NationalityStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                   
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="GenderCombo"  Editable="false" EmptyText="<%$ Resources: FieldGender %>" ForceSelection="true" Width="75">
                                            <Items>
                                                 <ext:ListItem Text="<%$ Resources: All %>" Value="0" />
                                                <ext:ListItem Text="<%$ Resources: Male %>" Value="1" />
                                                <ext:ListItem Text="<%$ Resources: Female %>" Value="2" />
                                           
                                            </Items>

                                        </ext:ComboBox>
                                  <ext:ComboBox Width="100"   AnyMatch="true" CaseSensitive="false"   runat="server"  ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="sponsorId" Name="sponsorId" EmptyText="<%$ Resources:sponsor%>"  >
                                    <Store>
                                        <ext:Store runat="server" ID="sponsorStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                               
                                    <Listeners>
                                        <FocusEnter Handler="if(!this.readOnly) this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                   <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  runat="server" ID="locals"  Editable="false" EmptyText="<%$ Resources: locals %>" ForceSelection="true" Width="100">
                                            <Items>
                                                 <ext:ListItem Text="<%$ Resources: All %>" Value="0" />
                                                <ext:ListItem Text="<%$ Resources: local %>" Value="1" />
                                                <ext:ListItem Text="<%$ Resources: foreign %>" Value="2" />
                                           
                                            </Items>

                                        </ext:ComboBox>
                                             <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        
                                        <ext:Button runat="server" Text="<%$Resources:Common, Go %>"> 
                                            <Listeners>
                                                <Click Handler="callbackPanel.PerformCallback('1');" />
                                            </Listeners>
                                        </ext:Button>
                                         <ext:Button runat="server" Text="More"> 
                                       <Listeners>
                                           <Click Handler="App.Panel8.loader.url = '../ReportParameterBrowser.aspx?_reportName=RT108'; App.reportsParams.show();" />
                                       </Listeners>
                                        </ext:Button>
                                    </Content>
                                </ext:Container>
                                


                                
                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                    <Content>

                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="callbackPanel"
                            Width="100%" OnCallback="ASPxCallbackPanel1_Callback" OnLoad="ASPxCallbackPanel1_Load" ClientSideEvents-CallbackError="alertNow">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server"></dx:ASPxWebDocumentViewer>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </Content>
                    <Items>
                    </Items>
                </ext:Panel>


            </Items>
            
        </ext:Viewport>


        <ext:Window runat="server"  Icon="PageEdit"
            ID="reportsParams"
            Width="600"
            Height="500"
            Title="<%$Resources:Common,ReportParameters %>"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="FitLayout" Resizable="true">
            <Listeners>
                <Show Handler="App.Panel8.loader.load();"></Show>
            </Listeners>
            <Items>
                <ext:Panel runat="server" Layout="FitLayout"  ID="Panel8" DefaultAnchor="100%">
                    <Loader runat="server" Url="ReportParameterBrowser.aspx" Mode="Frame" ID="Loader8" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <Listeners>
                         </Listeners>
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            
                </Items>
        </ext:Window>



    </form>
</body>
</html>
