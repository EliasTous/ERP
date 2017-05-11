<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayRefFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.PayRefFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="130">
    <Items>
        <ext:TextField Width="120" runat="server" ID="payRef" EmptyText="<%$Resources: PayRef %>" />
    </Items>
</ext:Panel>
