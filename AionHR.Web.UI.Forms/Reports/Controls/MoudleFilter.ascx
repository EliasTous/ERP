<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MoudleFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.MoudleFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="170"  ><Items>
<ext:ComboBox runat="server" ID="moduleId" Editable="false" FieldLabel="">
    <Items>
        
        <ext:ListItem Text="<%$ Resources: Common,Mod20  %>" Value="20" />
        <ext:ListItem Text="<%$ Resources:  Common,Mod21  %>" Value="21" />
         <ext:ListItem Text="<%$ Resources:  Common,Mod22  %>" Value="22" />
        <ext:ListItem Text="<%$ Resources:  Common,Mod23 %>" Value="23" />
        <ext:ListItem Text="<%$ Resources:  Common,Mod24  %>" Value="24" />
         <ext:ListItem Text="<%$ Resources: Common,Mod30  %>" Value="30" />
        <ext:ListItem Text="<%$ Resources:  Common,Mod31 %>" Value="31" />
        <ext:ListItem Text="<%$ Resources:  Common,Mod32  %>" Value="32" />
         <ext:ListItem Text="<%$ Resources: Common,Mod41  %>" Value="41" />
        <ext:ListItem Text="<%$ Resources:  Common,Mod42  %>" Value="42" />
         <ext:ListItem Text="<%$ Resources:  Common,Mod43  %>" Value="43" />
        <ext:ListItem Text="<%$ Resources:  Common,Mod44 %>" Value="44" />
        <ext:ListItem Text="<%$ Resources:  Common,Mod45  %>" Value="45" />
         <ext:ListItem Text="<%$ Resources: Common,Mod80  %>" Value="80" />
    
    </Items>

</ext:ComboBox>
</Items></ext:Panel>