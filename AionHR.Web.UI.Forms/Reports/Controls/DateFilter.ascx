﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.DateFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="120" ><Items>
<ext:DateField runat="server" ID="date" Format="dd-MM-yyyy" Name="date" Width="120"  EmptyText="<%$Resources: Date %>"/>

    </Items></ext:Panel>