<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayIdFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.PayIdFilter" %>
<ext:ComboBox AnyMatch="true" CaseSensitive="false" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$Resources:Common , PayRef %>" Name="payId" runat="server" DisplayField="payRefWithDateRange" ValueField="recordId" ID="payId" Width="100">
    <Store>
        <ext:Store runat="server" ID="payIdStore">
            <Model>
                <ext:Model runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="payRefWithDateRange" />

                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>
    </Store>
    <Triggers>
        <ext:FieldTrigger Icon="Clear" />
    </Triggers>
    <Listeners>
        <TriggerClick Handler="this.clearValue();" />
    </Listeners>
</ext:ComboBox>
