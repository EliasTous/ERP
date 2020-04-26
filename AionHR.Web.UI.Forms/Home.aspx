<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Web.UI.Forms.Home" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />

    <link rel="stylesheet" type="text/css" href="CSS/HeaderInside.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Home.css" />
    <style type="text/css">
        .bodyCls {
            background-image: url(Images/bg.png);
        }
    </style>
    <title>
        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Common , ApplicatonTitle%>" />
    </title>
</head>
<body>
    <ext:Hidden runat="server" ID="lblChangePassword" Text="<%$Resources:Common , ChangePassword %>" />
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" DirectMethodNamespace="AionHR" IDMode="Explicit" AjaxTimeout="1200000" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="border">
        <Items>

            <ext:Panel ID="topPanel" runat="server" Title="AionHR" Height="60" Header="false" Region="North" PaddingSpec="0 0 1 0" BodyStyle="background-color:#157fcc">
                <Items>
                    <ext:Container runat="server">
                        <Content>
                            <div class="logoImage">
                                <img src="Images/logo.png" width="80" height="57" />
                            </div>
                            <div class="title">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Common ,ApplicationModule%>" /></span>
                            </div>
                            <div class="buttons">

                                <a class="button" href="ARLogin.aspx">
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Common ,LanguageSwitch%>" /></a>
                                <a class="button" href="ARLogin.aspx">
                                    <img src="Images/logoff.png" />

                                </a>

                            </div>
                        </Content>
                    </ext:Container>
                </Items>


            </ext:Panel>

            <ext:Panel ID="centerPanel" runat="server" Layout="VBoxLayout" Region="Center" BodyBorder="0" StyleSpec="border-bottom:2px solid #2A92D4;" BodyCls="bodyCls">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>

                    <ext:Panel runat="server" Height="100"  BodyCls="bodyCls">
                        <Defaults>
                            <ext:Parameter Name="margin" Value="0 0 0 0" Mode="Value" />
                            <ext:Parameter Name="padding" Value="0 0 0 0" Mode="Value" />
                        </Defaults>
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
                        </LayoutConfig>

                        <Items>
                            <ext:Panel runat="server" Title="Company" TitleAlign="Center" Header="false" Border="true" PaddingSpec="5 10 10 5">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Container runat="server" Cls="img-chooser-view">
                                        <Content>
                                            <div class="thumb-wrap">
                                                <div class="thumb">

                                                    <a href="Default.aspx">
                                                        <img src="Images/company.png" height="50" width="50" /></a>
                                                    <span>Company Information</span>
                                                </div>

                                            </div>
                                        </Content>
                                    </ext:Container>
                                </Items>
                            </ext:Panel>
                            <ext:Panel runat="server" Title="Case Management" TitleAlign="Center" Header="false" Border="true" PaddingSpec="5 10 10 5">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Container runat="server" Cls="img-chooser-view">
                                        <Content>
                                            <div class="thumb-wrap">
                                                <div class="thumb">

                                                    <a href="Default.aspx">
                                                        <img src="Images/casemanagement.png" height="50" width="50" /></a>
                                                     <span>Case Management</span>
                                                </div>

                                            </div>
                                        </Content>
                                    </ext:Container>
                                </Items>
                            </ext:Panel>
                            <ext:Panel runat="server" Title="Employees" TitleAlign="Center" Header="false" Border="true" PaddingSpec="5 10 10 5">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Container runat="server" Cls="img-chooser-view">
                                        <Content>
                                            <div class="thumb-wrap">
                                                <div class="thumb">

                                                    <a href="Default.aspx">
                                                        <img src="Images/employees.png" height="50" width="50" /></a>
                                                      <span>Employees Files</span>
                                                </div>

                                            </div>
                                        </Content>
                                    </ext:Container>
                                </Items>
                            </ext:Panel>

                            <ext:Panel runat="server" Title="Work Schedule" TitleAlign="Center" Header="false" Border="true" PaddingSpec="5 10 10 5">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Container runat="server" Cls="img-chooser-view">
                                        <Content>
                                            <div class="thumb-wrap">
                                                <div class="thumb">

                                                    <a href="Default.aspx">
                                                        <img src="Images/schedule.png" height="50" width="50" /></a>
                                                      <span> Work Schedule</span>

                                                </div>

                                            </div>
                                        </Content>
                                    </ext:Container>
                                </Items>
                            </ext:Panel>
                            <ext:Panel runat="server" Title="Reports" TitleAlign="Center" Header="false" Border="true" PaddingSpec="5 10 10 5">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Container runat="server" Cls="img-chooser-view">
                                        <Content>
                                            <div class="thumb-wrap">
                                                <div class="thumb">

                                                    <a href="Default.aspx">
                                                        <img src="Images/reports.png" height="50" width="50" /></a>
                                                      <span>Reports & Statisctics</span>
                                                </div>

                                            </div>
                                        </Content>
                                    </ext:Container>
                                </Items>
                            </ext:Panel>

                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
        </Items>

    </ext:Viewport>
</body>
</html>
