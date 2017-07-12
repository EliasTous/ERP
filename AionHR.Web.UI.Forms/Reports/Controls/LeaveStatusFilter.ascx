<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaveStatusFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.LeaveStatusFilter" %>
    <ext:Panel runat="server" Layout="HBoxLayout" Width="130">
    <Items>
  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" ID="statusPref" Editable="false" EmptyText="<%$ Resources: FieldStatus %>" Width="120"  >
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="0"  />
                                        <ext:ListItem Text="<%$ Resources: FieldPending %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: FieldRefused %>" Value="3" />
                                        
                                    </Items>
                                  
                                </ext:ComboBox>
        </Items>
        </ext:Panel>