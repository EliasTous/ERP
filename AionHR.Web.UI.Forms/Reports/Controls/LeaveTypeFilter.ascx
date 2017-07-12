<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaveTypeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.LeaveTypeFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="150">
    <Items>
       
        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local" Width="140" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="ltId" Name="ltId" EmptyText="<%$ Resources:FieldLeaveType%>">
            <Store>
                <ext:Store runat="server" ID="ltStore">
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