<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CurrencyFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.CurrencyFilter" %>

 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false"  ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="reference" ID="currencyId" Name="currencyId" FieldLabel="<%$ Resources:Common,FieldCurrency%>" >

                                            <Store>
                                                <ext:Store runat="server" ID="currencyStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="recordId" />
                                                                <ext:ModelField Name="reference" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                         
                                        </ext:ComboBox>
