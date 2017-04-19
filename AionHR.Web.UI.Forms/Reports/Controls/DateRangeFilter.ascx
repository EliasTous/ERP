<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateRangeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.DateRangeFilter" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="400" ><Items>
<ext:DateField runat="server" ID="dateFrom"  EmptyText="<%$Resources: From %>"/>
<ext:DateField runat="server" ID="dateTo"  EmptyText="<%$Resources: To %>"/>
    </Items></ext:Panel>