<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateRangeFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.DateRangeFilter" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="320" ><Items>
<ext:DateField runat="server" ID="dateFrom" Width="150" LabelWidth="30" FieldLabel="<%$Resources: From %>" Format="dd/MM/yyyy"/>
    <ext:Panel runat="server" Width="10" />
<ext:DateField runat="server" ID="dateTo" Width="150" LabelWidth="30"  FieldLabel="<%$Resources: To %>" Format="dd/MM/yyyy"/>
    </Items></ext:Panel>