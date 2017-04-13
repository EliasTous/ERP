<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateRangeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.DateRangeFilter" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="350" ><Items>
<ext:DateField runat="server" ID="dateFrom"  EmptyText="From"/>
<ext:DateField runat="server" ID="dateTo"  EmptyText="To"/>
    </Items></ext:Panel>