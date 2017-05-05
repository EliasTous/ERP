<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollDefaults.aspx.cs" Inherits="AionHR.Web.UI.Forms.PayrollDefaults" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Nationalities.js"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" >
        function handlePayment()
        {
            if (App.ldMethod.value < 4) {
                App.ldValue.setFieldLabel(document.getElementById("paymentValueP").value);
                App.ldValue.setMaxValue(100);
            }

            else {
                App.ldValue.setFieldLabel(document.getElementById("paymentValue").value);
                App.ldValue.setMaxValue(1000000);
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
        <ext:Hidden ID="paymentValue" runat="server" Text="<%$ Resources:PaymentValue %>" />
        <ext:Hidden ID="paymentValueP" runat="server" Text="<%$ Resources:PaymentValueP %>" />




        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:Panel
                    ID="EditRecordWindow"
                    runat="server"
                    Icon="PageEdit"
                    Title="<%$ Resources:EditWindowsTitle %>"
                    Width="450"
                    Height="330"
                    AutoShow="false"
                    Modal="true"
                    Hidden="False"
                    Layout="Fit">

                    <Items>
                        <ext:FormPanel DefaultButton="SaveButton"
                            ID="BasicInfoTab"
                            runat="server"
                            
                            DefaultAnchor="100%"
                            BodyPadding="5">
                            <Items>

                                <ext:ComboBox QueryMode="Local" ForceSelection="true" LabelWidth="130" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldUL %>" Name="ulDeductionId" runat="server" DisplayField="name" ValueField="recordId" ID="ulDeductionId">
                                    <Store>
                                        <ext:Store runat="server" ID="ulDeductionStore">
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
                                        <ext:Button ID="Button1" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addDedUL">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox QueryMode="Local" ForceSelection="true" LabelWidth="130" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldSS %>" Name="ssDeductionId" runat="server" DisplayField="name" ValueField="recordId" ID="ssDeductionId">
                                    <Store>
                                        <ext:Store runat="server" ID="ssDeductionStore">
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

                                                <Click OnEvent="addDedSS">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                
                                <ext:ComboBox QueryMode="Local" ForceSelection="true" LabelWidth="130" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldPE %>" Name="peDeductionId" runat="server" DisplayField="name" ValueField="recordId" ID="peDeductionId">
                                    <Store>
                                        <ext:Store runat="server" ID="peDeductionStore">
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
                                        <ext:Button ID="Button4" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addDedpe">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:ComboBox QueryMode="Local" ForceSelection="true" LabelWidth="130" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldOT %>" Name="otEntitlementId" runat="server" DisplayField="name" ValueField="recordId" ID="otEntitlementId">
                                    <Store>
                                        <ext:Store runat="server" ID="otEntitlementStore">
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
                                        <ext:Button ID="Button5" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addEnt">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:FieldSet runat="server"  Title="<%$ Resources:Common, Loans %>">
                                    <Items>
                                        <ext:ComboBox QueryMode="Local" Width="400" LabelWidth="130" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldLoan %>" Name="loanDeductionId" runat="server" DisplayField="name" ValueField="recordId" ID="loanDeductionId">
                                    <Store>
                                        <ext:Store runat="server" ID="loanDeductionStore">
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
                                        <ext:Button ID="Button3" runat="server" Icon="Add" Hidden="true">
                                            <Listeners>
                                                <Click Handler="CheckSession();  " />
                                            </Listeners>
                                            <DirectEvents>

                                                <Click OnEvent="addDedloan">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </RightButtons>
                                    <Listeners>
                                        <FocusEnter Handler=" if(!this.readOnly)this.rightButtons[0].setHidden(false);" />
                                        <FocusLeave Handler="this.rightButtons[0].setHidden(true);" />
                                    </Listeners>
                                </ext:ComboBox>
                                        <ext:ComboBox  Width="400" QueryMode="Local" LabelWidth="130" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: LoanCoverageType %>" Name="ldMethod" runat="server" ID="ldMethod">
                                            <Items>
                                                <ext:ListItem Text="<%$ Resources: PFromNetSalary %>" Value="1" />
                                                <ext:ListItem Text="<%$ Resources: PFromBasicSalary %>" Value="2" />
                                                <ext:ListItem Text="<%$ Resources: PFromLoan %>" Value="3" />
                                                <ext:ListItem Text="<%$ Resources: FixedAmount %>" Value="4" />
                                                <ext:ListItem Text="<%$ Resources: FixedPayment %>" Value="5" />
                                                
                                            </Items>
                                            <Listeners>
                                                <Change Handler="handlePayment();"></Change>
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:NumberField Width="400" runat="server" LabelWidth="130" ID="ldValue" Name="ldValue" FieldLabel="<%$ Resources: PaymentValue %>" MinValue="0" />
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                             <Buttons>
                        <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                            <Listeners>
                                <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;}  " />
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                                    <ExtraParams>

                                        <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>

                    </Buttons>
                        </ext:FormPanel>
                        <%--   <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                            <Items>
                              

                            </Items>
                        </ext:TabPanel>--%>
                    </Items>
                   
                </ext:Panel>
            </Items>

        </ext:Viewport>






    </form>
</body>
</html>

