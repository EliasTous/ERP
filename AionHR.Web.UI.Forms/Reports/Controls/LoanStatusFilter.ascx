<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoanStatusFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.LoanStatusFilter" %>
    <ext:Panel runat="server" Layout="HBoxLayout" Width="130">
    <Items>
  <ext:ComboBox runat="server" ID="statusPref" Editable="false" EmptyText="<%$ Resources: FilterStatus %>" Width="120"  >
                                    <Items>
                                        <ext:ListItem Text="<%$ Resources: All %>" Value="4"  />
                                        <ext:ListItem Text="<%$ Resources: FieldNew %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources: FieldInProcess %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: FieldApproved %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: FieldRejected %>" Value="3" />
                                    </Items>
                                  
                                </ext:ComboBox>
        </Items>
        </ext:Panel>