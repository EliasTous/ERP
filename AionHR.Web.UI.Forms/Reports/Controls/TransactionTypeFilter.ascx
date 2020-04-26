<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionTypeFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.TransactionTypeFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="120"  ><Items>
<ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="trxType" Editable="false" Width="120" FieldLabel="">
    <Items>
        <ext:ListItem Text="<%$ Resources: Common,All  %>" Value="0" />
        <ext:ListItem Text="<%$ Resources: Common,TrType1  %>" Value="1" />
        <ext:ListItem Text="<%$ Resources:  Common,TrType2 %>" Value="2" />
         <ext:ListItem Text="<%$ Resources:  Common,TrType3 %>" Value="3" />
        <ext:ListItem Text="<%$ Resources:  Common,TrType4 %>" Value="4" />
        <ext:ListItem Text="<%$ Resources:  Common,TrType5 %>" Value="5" />
         <ext:ListItem Text="<%$ Resources:  Common,TrType11 %>" Value="11" />
        <ext:ListItem Text="<%$ Resources:  Common,TrType12 %>" Value="12" />
        <ext:ListItem Text="<%$ Resources:  Common,TrType21 %>" Value="21" />
    </Items>

</ext:ComboBox>
</Items></ext:Panel>