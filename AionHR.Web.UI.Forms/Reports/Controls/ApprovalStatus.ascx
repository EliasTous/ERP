<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalStatus.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.ApprovalStatus" %>

     
         <ext:ComboBox AnyMatch="true"  CaseSensitive="false" runat="server" ID="apStatus" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1"  EmptyText="<%$ Resources:Common, FieldApprovalStatus %>">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources:Common, FieldAll %>" Value="0" />
                                        <ext:ListItem Text="<%$ Resources:Common, FieldNew %>" Value="1" />
                                        <ext:ListItem Text="<%$ Resources: Common,FieldApproved %>" Value="2" />
                                        <ext:ListItem Text="<%$ Resources: Common,FieldRejected %>" Value="-1" />
                                    </Items>

                                </ext:ComboBox>

     
   