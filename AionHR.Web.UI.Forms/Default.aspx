<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AionHR.Web.UI.Forms.Default" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Tools.css" />
    
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/app.js"></script>
    <script type="text/javascript" src="Scripts/Common.js"></script>
    <script type="text/javascript" src="Scripts/default.js"></script>

    <title>
        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Common , ApplicationTitle%>" />
    </title>
</head>
<body>
    <ext:Hidden runat="server" ID="lblChangePassword" Text="<%$Resources:Common , ChangePassword %>" />
    <ext:Hidden runat="server" ID="lblError" Text="<%$Resources:Common , Error %>" />
    <ext:Hidden runat="server" ID="lblOk" Text="<%$Resources:Common , Ok %>" />
    <ext:Hidden runat="server" ID="lblErrorOperation" Text="<%$Resources:Common , ErrorOperation %>" />
    <ext:Hidden runat="server" ID="lblLoading" Text="<%$Resources:Common , Loading %>" />
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" IDMode="Explicit" AjaxTimeout="1200000" />
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
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Common ,ApplicationModule%>" /><asp:Literal ID="CompanyNameLiteral" runat="server" Text="" /></span>
                            </div>
                            <div class="buttons">

                                <a id="btnChangeLanguage" class="button" href="#">
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Common ,LanguageSwitch%>" /></a>
                                <a id="btnLogout" class="button" href="#">
                                    <img src="Images/logoff.png" />
                                </a>

                            </div>
                        </Content>
                    </ext:Container>
                </Items>


            </ext:Panel>
            <ext:Panel ID="leftPanel" runat="server" Region="West"  Layout="AccordionLayout" AutoUpdateLayout="true" Width="260" PaddingSpec="0 0 0 0" Padding="0"
                Header="true" Collapsible="true"  Split="true" CollapseMode="Mini" StyleSpec="border-bottom:2px solid #2A92D4;"
                Title="<%$ Resources:Common , NavigationPane %>" CollapseToolText="<%$ Resources:Common , CollapsePanel %>" ExpandToolText="<%$ Resources:Common , ExpandPanel %>" Icon="ApplicationTileVertical" BodyBorder="0">
                <TopBar>
                    <ext:Toolbar ID="Toolbar1" runat="server" Border="true">
                        <Items>
                            <ext:Label runat="server" Text="<%$ Resources:Common , Modules %>" />
                            <ext:Button ID="btnEmployeeFiles" runat="server" Icon="Group" ToolTip="<%$ Resources:Common , EmployeeFiles %>">
                                <Listeners>
                                    <Click Handler="#{commonTree}.setTitle(this.tooltip);openModule(1);" />
                                </Listeners>
                            </ext:Button>

                        

                            <ext:ToolbarSeparator runat="server"></ext:ToolbarSeparator>
                            <ext:Button ID="btnCompany" runat="server" Icon="Building" ToolTip="<%$ Resources:Common , Company %>">                              
                                <Listeners>
                                    <Click Handler="#{commonTree}.setTitle(this.tooltip);openModule(3);" />
                                </Listeners>
                            </ext:Button>
                              <ext:ToolbarSeparator runat="server"></ext:ToolbarSeparator>
                            <ext:Button ID="btnScheduler" runat="server" Icon="CalendarSelectDay" ToolTip="<%$ Resources:Common , Scheduler %>">                              
                                <Listeners>
                                    <Click Handler="#{commonTree}.setTitle(this.tooltip);openModule(4);" />
                                </Listeners>
                            </ext:Button>
                                <ext:ToolbarSeparator runat="server"></ext:ToolbarSeparator>
                        <%--    <ext:Button ID="btnReport" runat="server" Icon="ChartBar" ToolTip="<%$ Resources:Common , Reports %>">                              
                                <Listeners>
                                    <Click Handler="#{commonTree}.setTitle(this.tooltip);openModule(5);" />
                                </Listeners>
                            </ext:Button>--%>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:TreePanel runat="server" Title="Tree Panel" AutoUpdateLayout="true" RootVisible="false" ID="commonTree" Scroll="Vertical">
                        <SelectionModel>
                            <ext:TreeSelectionModel runat="server" ID="selModel">
                            </ext:TreeSelectionModel>
                        </SelectionModel>
                        <TopBar>
                            <ext:Toolbar ID="Toolbar2" runat="server" Hidden="true">
                                <Items>
                                    <ext:TextField ID="filerTreeTrigger" runat="server" EnableKeyEvents="true" Width="150" EmptyText="<%$ Resources:Common , Filter %>">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyUp Fn="filterCommonTree" Buffer="100" />
                                            <TriggerClick Handler="clearFilter();" />
                                        </Listeners>
                                    </ext:TextField>
                                    <ext:Button ID="btnExpandAll" runat="server" ToolTip="<%$ Resources:Common , ExpandAll %>" IconCls="icon-expand-all">
                                        <Listeners>
                                            <Click Handler="#{commonTree}.expandAll();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:ToolbarSeparator>
                                    </ext:ToolbarSeparator>
                                    <ext:Button ID="btnCollapseAll" runat="server" ToolTip="<%$ Resources:Common , CollapseAll %>" IconCls="icon-collapse-all">
                                        <Listeners>
                                            <Click Handler="#{commonTree}.collapseAll();" />
                                        </Listeners>
                                    </ext:Button>

                                </Items>
                            </ext:Toolbar>
                        </TopBar>


                        <Fields>
                            <ext:ModelField Name="idTab" />
                            <ext:ModelField Name="url" />
                            <ext:ModelField Name="title" />
                            <ext:ModelField Name="css" />
                            <ext:ModelField Name="click" />
                        </Fields>

                        <Listeners>
                            <ItemClick Handler="CheckSession(); onTreeItemClick(record, e);" />
                        </Listeners>

                    </ext:TreePanel>
                <%--    <ext:Panel
                        ID="pnlAlignMiddle"
                        runat="server"
                        Title="Buttons"
                        Layout="VBoxLayout"
                        BodyPadding="5">
                        <Defaults>
                            <ext:Parameter Name="margin" Value="0 0 5 0" Mode="Value" />
                        </Defaults>
                        <LayoutConfig>
                            <ext:VBoxLayoutConfig Align="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Button
                                runat="server"
                                Text="Paste"
                                Icon="User"
                                Scale="Large"
                                IconAlign="Top"
                                Cls="x-btn-as-arrow"
                                RowSpan="3" />
                            <ext:Button
                                runat="server"
                                Text="Paste"
                                Icon="User"
                                Scale="Large"
                                IconAlign="Top"
                                Cls="x-btn-as-arrow"
                                RowSpan="3" />
                            <ext:Button
                                runat="server"
                                Text="Paste"
                                Icon="User"
                                Scale="Large"
                                IconAlign="Top"
                                Cls="x-btn-as-arrow"
                                RowSpan="3" />

                        </Items>
                    </ext:Panel>

                    <ext:Panel runat="server" Title="Settings" />
                    <ext:Panel runat="server" Title="Even More Stuff" />
                    <ext:Panel runat="server" Title="My Stuff" />--%>


                </Items>
            </ext:Panel>
            <ext:TabPanel ID="tabPanel" runat="server" Region="Center" EnableTabScroll="true" MinTabWidth="100" BodyBorder="0" StyleSpec="border-bottom:2px solid #2A92D4;">
                <Items>
                    <ext:Panel ID="tabHome" Closable="false" runat="server" Title="<%$ Resources:Common , Home %>" Icon="House" >
                        <Loader ID="Loader1"
                            runat="server"
                            Url="Employees.aspx"
                            Mode="Frame" 
                            ShowMask="true">
                            <LoadMask ShowMask="true" />
                        </Loader>
                    </ext:Panel>



                </Items>
            </ext:TabPanel>
        </Items>
    </ext:Viewport>
</body>
</html>
