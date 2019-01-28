<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmploymentStatusFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.EmploymentStatusFilter" %>
    <ext:Panel runat="server" Layout="HBoxLayout" Width="150">
    <Items>
  <ext:ComboBox AnyMatch="true" CaseSensitive="false" runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="esId" Name="esId" EmptyText="<%$ Resources: Common ,FieldEHStatus%>" width="150">
                                    <Store>
                                        <ext:Store runat="server" ID="statusStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="name" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>

                                    <Items>
                                        <ext:ListItem Text="<%$Resources:Common ,All %>" Value="0" />
                                    </Items>
                                </ext:ComboBox>
        </Items>
        </ext:Panel>