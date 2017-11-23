<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Myinfo.aspx.cs" Inherits="AionHR.Web.UI.Forms.Myinfo" %>

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
    <script type="text/javascript">
        
    </script>


</head>
<body style="background: url(Images/bg.png) repeat;">
    
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="CurrentEmployee" runat="server" />
        <ext:Hidden ID="EmployeeTerminated" runat="server" />
        <ext:Hidden ID="CurrentEmployeeName" runat="server" />
       
        <ext:Window 
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="elias"
            Width="500"
            Height="500"
            AutoShow="false"
            Draggable="false"
            Maximizable="false"
            Resizable="true"
            Modal="true"
            Hidden="true"
            Layout="Fit" AutoScroll="true"
            >
            
            <Items>
                   <ext:TabPanel ID="panelRecordDetails" Layout="FitLayout" DefaultAnchor="100%" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false" Region="Center">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarFill runat="server">
                        </ext:ToolbarFill>

                    </Items>
                </ext:Toolbar>
            </TopBar>

            <Items>
                <ext:FormPanel  ID="MyinfoForm" DefaultButton="saveLegalReference"
                            runat="server"
                            Title="elias"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" Layout="AutoLayout"
                            BodyPadding="5"> 
                          <Items>
                       
                       
                                                     
                                <ext:TextField ID="middleName" runat="server" FieldLabel="<%$ Resources:FieldMiddleName%>"  />
                           
                                <ext:TextField ID="familyName" runat="server" FieldLabel="<%$ Resources:FieldFamilyName%>" Name="familyName" />
                              
                                <ext:TextField ID="homeEmail" runat="server" FieldLabel="<%$ Resources:FieldHomeEmail%>" Name="homeMail" Vtype="email" />
                               <ext:TextField ID="mobile" AllowBlank="true" MinLength="6" MaxLength="18" runat="server" FieldLabel="<%$ Resources:FieldMobile%>" Name="mobile" BlankText="<%$ Resources:Common, MandatoryField%>">
                                    <Validator Handler="return !isNaN(this.value);" />
                                </ext:TextField>
                                <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="religionCombo" runat="server" FieldLabel="<%$ Resources:FieldReligion%>" Name="religion" IDMode="Static" SubmitValue="true">
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion0%>" Value="0"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion1%>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion2%>" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion3%>" Value="3"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion4%>" Value="4"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion5%>" Value="5"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources:Common, Religion6%>" Value="6"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                                <ext:DateField
                                    runat="server" ID="birthDate"
                                    Name="birthDate"
                                    FieldLabel="<%$ Resources:FieldDateOfBirth%>"
                                    MsgTarget="Side"
                                    AllowBlank="true" />
                             <ext:TextField ID="birthPlace" runat="server" FieldLabel="<%$ Resources:FieldBirthPlace%>" Name="placeOfBirth" AllowBlank="true" />

                         
                           
                           
                            
                          
                        
                    </Items>
                    <BottomBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarFill runat="server" />
                                    <ext:Button runat="server" Text ="<%$ Resources:Common , Save %>" ID="saveButton">
                            <Listeners>
                        <Click Handler="CheckSession(); if (!#{MyinfoForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveMyInfo" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" />
                            <ExtraParams>
                                
                                <ext:Parameter Name="values" Value="#{MyinfoForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                        </ext:Button>
                        <ext:Button runat="server" Text ="<%$ Resources:Common , Cancel %>">
                            <Listeners>
                                        <Click Handler="this.up('window').hide();" />
                            </Listeners>
                        </ext:Button>
                                <ext:ToolbarFill runat="server" />
                            </Items>
                        </ext:Toolbar>
                    </BottomBar>
                    <Buttons>
                    
                    </Buttons>
                </ext:FormPanel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: ContactsTab %>" ID="Panel6" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Contacts.aspx?" Mode="Frame" ID="Loader6" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                      <Params>
                        <ext:Parameter Name="employeeId" Value="function() { return App.CurrentEmployee.getValue(); }" Mode="Value" />
                      </Params>
                        <LoadMask ShowMask="true" />
                    </Loader>

                </ext:Panel>
                <ext:Panel runat="server" Layout="FitLayout" Title="<%$ Resources: Dependants %>" ID="Panel8" DefaultAnchor="100%">
                    <Loader runat="server" Url="EmployeePages/Dependants.aspx" Mode="Frame" ID="Loader8" TriggerEvent="show"
                        ReloadOnEvent="true"
                        DisableCaching="true">
                        <Params>
                        <ext:Parameter Name="employeeId" Value="function() { return App.CurrentEmployee.getValue(); }" Mode="Value" />
                      </Params>
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            </Items>
           </ext:TabPanel>
            </Items>
    </ext:Window>


    </form>
</body>
</html>
