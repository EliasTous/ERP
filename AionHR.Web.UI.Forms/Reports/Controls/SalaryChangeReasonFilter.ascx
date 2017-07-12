<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalaryChangeReasonFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.SalaryChangeReasonFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="150">
    <Items>
       
        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local" Width="140" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="scrId" Name="scrId" EmptyText="<%$ Resources:FieldSCR%>">
            <Store>
                <ext:Store runat="server" ID="scrStore">
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
     
    </Items>
</ext:Panel>