﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActiveStatusFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.ActiveStatusFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="120"  ><Items>
<ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="inactivePref" Width="120" Editable="false" FieldLabel="">
    <Items>
        <ext:ListItem Text="<%$ Resources: All %>" Value="2" />
        <ext:ListItem Text="<%$ Resources: ActiveOnly %>" Value="0" />
        <ext:ListItem Text="<%$ Resources: InactiveOnly %>" Value="1" />
    </Items>

</ext:ComboBox>
</Items></ext:Panel>