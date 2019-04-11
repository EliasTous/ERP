<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DivisionFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.DivisionFilter" %>
<ext:ComboBox   AnyMatch="true" CaseSensitive="false"   runat="server" QueryMode="Local" Width="120"  ForceSelection="true"  TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="divisionId"   FieldLabel="<%$ Resources:Common,Divisions%>" >
            <Store>
                <ext:Store runat="server" ID="divisionStore">
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