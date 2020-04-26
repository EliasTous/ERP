<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CertificateLevelFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.CertificateLevelFilter" %>
 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server"   ForceSelection="true" QueryMode="Local" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="clId"   >

                                            <Store>
                                                <ext:Store runat="server" ID="clStore">
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
            </Listeners>                                        </ext:ComboBox>