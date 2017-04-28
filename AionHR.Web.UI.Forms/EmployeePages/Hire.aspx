<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hire.aspx.cs" Inherits="AionHR.Web.UI.Forms.EmployeePages.Hire" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Common.css?id=1" />
    <link rel="stylesheet" href="../CSS/LiveSearch.css" />
    <script type="text/javascript" src="../Scripts/Notes.js?id=18"></script>
    <script type="text/javascript" src="../Scripts/common.js?id=0"></script>
    <script type="text/javascript" src="../Scripts/moment.js?id=0"></script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server" />

        <ext:Hidden ID="CurrentEmployeeName" runat="server" />
        <ext:Viewport ID="Viewport11" runat="server" Layout="VBoxLayout" Padding="10">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>



            <Items>
                <ext:FormPanel runat="server" ID="hireInfoForm" Region="Center" Layout="HBoxLayout" >
                    <Items>
                        <ext:Panel runat="server"  flex="2"><Items></Items></ext:Panel>
                        <ext:Panel runat="server" Flex="4"><Items>
                        <ext:DateField runat="server" AllowBlank="false" ID="probationEndDate" Name="probationEndDate" LabelWidth="200" FieldLabel="<%$ Resources:FieldProbationEndDate %>"></ext:DateField>
                        <ext:DateField runat="server" AllowBlank="false" ID="nextReviewDate" Name="nextReviewDate"  LabelWidth="200" FieldLabel="<%$ Resources:FieldNextReviewDate %>"></ext:DateField>
                        <ext:ComboBox runat="server" AllowBlank="false" ValueField="recordId" QueryMode="Local"  LabelWidth="200" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="npId" Name="npId" FieldLabel="<%$ Resources:FieldNP%>" SimpleSubmit="true">
                            <Store>
                                <ext:Store runat="server" ID="npStore">
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
                            <RightButtons>
                                <ext:Button ID="Button2" runat="server" Icon="Add" Hidden="true">
                                    <Listeners>
                                        <Click Handler="CheckSession();  " />
                                    </Listeners>
                                    <DirectEvents>

                                        <Click OnEvent="addNP">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </RightButtons>
                            <Listeners>
                                <FocusEnter Handler="this.rightButtons[0].setHidden(false);" />
                                <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:DateField  LabelWidth="200" runat="server" AllowBlank="false" ID="termEndDate" Name="termEndDate" FieldLabel="<%$ Resources:FieldTermEndDate %>"></ext:DateField>
                        <ext:TextArea  LabelWidth="200"  runat="server" ID="recruitmentInfo" Name="recruitmentInfo" FieldLabel="<%$ Resources:FieldRecruitmentInfo %>" />
                        <ext:NumberField runat="server"  LabelWidth="200" ID="recruitmentCost" Name="recruitmentCost" FieldLabel="<%$ Resources:FieldRecruitmentCost %>" />
                            </Items></ext:Panel>
                         <ext:Panel runat="server"  flex="2"><Items></Items></ext:Panel>
                    </Items>
                    <BottomBar>
                        <ext:Toolbar runat="server"
                            >
                            <Items>
                                <ext:ToolbarFill runat="server" />
                                    <ext:Button runat="server" Text ="<%$ Resources:Common , Save %>">
                            <Listeners>
                        <Click Handler="CheckSession(); if (!#{hireInfoForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveHire" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" />
                            <ExtraParams>
                                
                                <ext:Parameter Name="values" Value="#{hireInfoForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                        </ext:Button>
                        <ext:Button runat="server" Text ="<%$ Resources:Common , Cancel %>">

                        </ext:Button>
                                <ext:ToolbarFill runat="server" />
                            </Items>
                        </ext:Toolbar>
                    </BottomBar>
                    <Buttons>
                    
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>


    </form>
</body>
</html>
