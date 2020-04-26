<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalStatus.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.ApprovalStatus" %>

       <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1"   runat="server" DisplayField="value" ValueField="key"    ID="apStatus" >
                                             <Store>
                                                <ext:Store runat="server" ID="apStatusStore">
                                                    <Model>
                                                        <ext:Model runat="server" >
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                       </ext:ComboBox>
       

     
   