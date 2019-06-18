<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiscalYearFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.FiscalYearFilter" %>

  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources:Common, FiscalYears %>"   Name="year" runat="server" DisplayField="fiscalYear" ValueField="fiscalYear" ID="year">
                                    <Store>
                                        <ext:Store runat="server" ID="yearStore">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>

                                                        <ext:ModelField Name="fiscalYear" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                               
                                </ext:ComboBox>
