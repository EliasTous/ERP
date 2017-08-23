<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalaryTypeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.SalaryTypeFilter" %>

<ext:Panel runat="server" Layout="HBoxLayout" Width="190"  ><Items>
<ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="salaryTypeId" Editable="false" Width="190" FieldLabel="<%$ Resources:Common,SalaryType %>" LabelWidth="80">
    <Items>
        
        <ext:ListItem Text="<%$ Resources: Common,Day  %>" Value="1" />
        <ext:ListItem Text="<%$ Resources:  Common,Week %>" Value="2" />
         <ext:ListItem Text="<%$ Resources:  Common,BiWeek  %>" Value="3" />
        <ext:ListItem Text="<%$ Resources:  Common,FourWeek %>" Value="4" />
        <ext:ListItem Text="<%$ Resources:  Common,Month  %>" Value="5" />
        
    
    </Items>

</ext:ComboBox>
</Items></ext:Panel>