<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DepartmentFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.DepartmentFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="130">
    <Items>
       
        <ext:ComboBox runat="server" QueryMode="Local" Width="120" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="departmentId" Name="departmentId" EmptyText="<%$ Resources:FieldDepartment%>">
            <Store>
                <ext:Store runat="server" ID="departmentStore">
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