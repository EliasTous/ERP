﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoanStatusFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.LoanStatusFilter" %>
    <ext:Panel runat="server" Layout="HBoxLayout" Width="130">
    <Items>
  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="statusPref" Editable="false" EmptyText="<%$ Resources: FilterStatus %>" Width="120"  >
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="0"  />
                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="1" />
                                      
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: FieldRejected %>" Value="-1" />
                                    </Items>
                                  
                                </ext:ComboBox>
        </Items>
        </ext:Panel>