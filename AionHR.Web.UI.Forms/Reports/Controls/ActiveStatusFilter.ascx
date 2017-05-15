<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActiveStatusFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.ActiveStatusFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="120"  ><Items>
<ext:ComboBox runat="server" ID="inactivePref" Width="120" Editable="false" FieldLabel="">
    <Items>
        <ext:ListItem Text="<%$ Resources: All %>" Value="0" />
        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="1" />
        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="2" />
    </Items>

</ext:ComboBox>
</Items></ext:Panel>