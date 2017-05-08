<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateRangeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.DateRangeFilter" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="250" ><Items>
<ext:DateField runat="server" ID="dateFrom" Width="120"  EmptyText="<%$Resources: From %>" Format="dd/MM/yyyy"/>
    <ext:Panel runat="server" Width="10" />
<ext:DateField runat="server" ID="dateTo" Width="120"  EmptyText="<%$Resources: To %>" Format="dd/MM/yyyy"/>
    </Items></ext:Panel>