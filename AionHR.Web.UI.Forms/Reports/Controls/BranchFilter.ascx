<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BranchFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.BranchFilter" %>
 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"    runat="server" QueryMode="Local" Width="120" ForceSelection="true" SimpleSubmit="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="branchId"  FieldLabel="<%$ Resources:Common,Branches%>" >
            <Store>
                <ext:Store runat="server" ID="branchStore">
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