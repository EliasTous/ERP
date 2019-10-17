<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExpressionFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.ExpressionFilter" %>

 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server"   ForceSelection="true" QueryMode="Local" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name"  ID="expressionId" FieldLabel="<%$ Resources:Common, expressions%>"  >

                                            <Store>
                                                <ext:Store runat="server" ID="expressionStore">
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
