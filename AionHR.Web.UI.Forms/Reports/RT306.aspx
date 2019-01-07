<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RT306.aspx.cs" Inherits="AionHR.Web.UI.Forms.Reports.RT306" %>

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
                 <DockedItems>
                        <ext:Toolbar runat="server" Height="50" Dock="Top">

                   
                            <Items>
                             
                                       <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                               
                                               <uc:dateRange runat="server" ID="date2" />
                                            </Content>
                                        </ext:Container>
                                   <ext:ToolbarSeparator runat="server" />
                                <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                                <%--<uc:dateRange runat="server" ID="dateRange1" />--%>
                                                <uc:employeeCombo runat="server" ID="employeeCombo1"  />
                                            </Content>
                                        </ext:Container>
                                   <ext:ToolbarSeparator runat="server" />
                                      <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" ID="approverId" Name="approverId"
                                    DisplayField="fullName"
                                    ValueField="recordId"
                                    TypeAhead="false"
                                    EmptyText="<%$ Resources: FieldApproverName%>"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" 
                                    TriggerAction="Query" ForceSelection="true">
                                    <Store>

                                        <ext:Store runat="server" ID="ApproverStore" AutoLoad="false">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="fullName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillApprover"></ext:PageProxy>
                                            </Proxy>

                                        </ext:Store>

                                    </Store>
                                </ext:ComboBox>
                                   <ext:ToolbarSeparator runat="server" />
                              <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                <uc:TimeVariationTypeControl runat="server" ID="timeVariationType"  />
                                    </Content>

                                </ext:Container>
                              
                                
                               <ext:ToolbarSeparator runat="server" />
                                  <ext:ComboBox AnyMatch="true" Width="80" CaseSensitive="false" runat="server" ID="apStatus" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"  Name="apStatus"
                                    EmptyText="<%$ Resources: FieldApprovalStatus %>">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: FieldAll %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: FieldRejected %>" Value="-1" />
                                    </Items>

                                </ext:ComboBox>
                                    <ext:ToolbarSeparator runat="server" />
                              
                                
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
                       <ext:Toolbar runat="server" Dock="Top">
                            <Items>
                                 <ext:Container runat="server" Layout="FitLayout">
                                    <Content>
                                        <uc:jobInfo runat="server" ID="jobInfo1" EnableBranch="true" EnableDivision="true" EnablePosition="true" EnableDepartment="true" />
                                    </Content>
                                </ext:Container>
                                <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="esId" Name="esId" EmptyText="<%$ Resources:FieldEHStatus%>">
                                    <Store>
                                        <ext:Store runat="server" ID="statusStore">
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

                                    <Items>
                                        <ext:ListItem Text="<%$Resources:All %>" Value="0" />
                                    </Items>
                                </ext:ComboBox>
                                </Items>
                           </ext:Toolbar>

                 </DockedItems>
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
