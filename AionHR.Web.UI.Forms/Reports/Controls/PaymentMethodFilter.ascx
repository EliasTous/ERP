<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentMethodFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.PaymentMethodFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="130">
    <Items>
        <ext:ComboBox Width="120" AllowBlank="false" ID="paymentMethod" runat="server" EmptyText="<%$ Resources:FieldPaymentMethod%>" Name="paymentMethod" IDMode="Static" SubmitValue="true">
            <Items>
                <ext:ListItem Text="<%$ Resources: All%>" Value="0"></ext:ListItem>
                <ext:ListItem Text="<%$ Resources: Cash%>" Value="1"></ext:ListItem>
                <ext:ListItem Text="<%$ Resources: Bank%>" Value="2"></ext:ListItem>
            </Items>
        </ext:ComboBox>
    </Items>
</ext:Panel>
