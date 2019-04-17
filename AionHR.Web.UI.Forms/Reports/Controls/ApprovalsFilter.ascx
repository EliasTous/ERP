<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalsFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.ApprovalsFilter" %>

 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server"   ForceSelection="true" QueryMode="Local" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="apId" FieldLabel="<%$ Resources:Common, Approvals%>"  >

                                            <Store>
                                                <ext:Store runat="server" ID="apStore">
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
