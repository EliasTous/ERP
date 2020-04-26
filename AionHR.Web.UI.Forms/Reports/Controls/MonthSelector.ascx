<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthSelector.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.MonthSelector" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="120" ><Items>
    
     <ext:DateField  ID="datefield1" Width="120" runat="server"  Type="Month" Format="dd/MM/yyyy">
        
</ext:DateField>
    
    </Items>
    </ext:Panel>