<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActiveStatusFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.ActiveStatusFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="170"  ><Items>
<ext:ComboBox runat="server" ID="inactivePref" Editable="false" FieldLabel="">
    <Items>
        <ext:ListItem Text="<%$ Resources: All %>" Value="2" />
        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="0" />
        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="1" />
    </Items>

</ext:ComboBox>
</Items></ext:Panel>