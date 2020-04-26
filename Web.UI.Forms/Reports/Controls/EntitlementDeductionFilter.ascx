<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntitlementDeductionFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.EntitlementDeductionFilter" %>

 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server"   ForceSelection="true" QueryMode="Local" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="edId"  >

                                            <Store>
                                                <ext:Store runat="server" ID="edStore">
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
                                         
                                        </ext:ComboBox>
