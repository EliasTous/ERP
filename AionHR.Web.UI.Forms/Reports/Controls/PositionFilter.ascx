<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PositionFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.PositionFilter" %>
 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local" Width="120"  ForceSelection="true" SimpleSubmit="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="positionId" FieldLabel="<%$ Resources:Common,Positions%>" >
            <Store>
                <ext:Store runat="server" ID="positionStore">
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
       