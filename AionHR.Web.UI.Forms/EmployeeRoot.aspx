<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeRoot.aspx.cs" Inherits="Web.UI.Forms.EmployeeRoot" %>

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
            


            
            <ext:Panel ID="leftPanel" runat="server" Region="West" Layout= "AutoLayout" Width="260" PaddingSpec="0 0 0 0" Padding="0"
                Header="true" Collapsible="true"   StyleSpec="border-bottom:2px solid #2A92D4;"
                Title="<%$ Resources:Common , NavigationPane %>" CollapseToolText="<%$ Resources:Common , CollapsePanel %>" ExpandToolText="<%$ Resources:Common , ExpandPanel %>" Icon="ApplicationTileVertical" BodyBorder="0">
               
                <Items>
                     <ext:Panel runat="server">
                                <Items>
                                    <ext:Image runat="server" ImageUrl="" Width="100" Height="100" />
                                    <ext:Label runat="server" Text="employee Name" />
                                </Items>
                                
                            </ext:Panel>
                    <ext:TreePanel runat="server" Title="<%$ Resources:Common , ActiveModule %>" RootVisible="false" ID="commonTree" Scroll="Vertical">
                        <SelectionModel>
                            <ext:TreeSelectionModel runat="server" ID="selModel">
                            </ext:TreeSelectionModel>
                        </SelectionModel>
                 


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
