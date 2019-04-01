<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalReasonFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.ApprovalReasonFilter" %>

 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server"   ForceSelection="true" QueryMode="Local" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="arId" FieldLabel="<%$ Resources:Common,ApprovalReason%>"  >

                                            <Store>
                                                <ext:Store runat="server" ID="arStore">
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
