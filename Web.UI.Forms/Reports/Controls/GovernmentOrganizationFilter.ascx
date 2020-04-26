<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GovernmentOrganizationFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.GovernmentOrganizationFilter" %>

        <ext:ComboBox runat="server" QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="goId"  >
            <Store>
                <ext:Store runat="server" ID="goStore">
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
            <Triggers>
                <ext:FieldTrigger Icon="Clear" />
            </Triggers>
            <Listeners>
                <TriggerClick Handler="
                                       this.clearValue();" />
            </Listeners>

        </ext:ComboBox>