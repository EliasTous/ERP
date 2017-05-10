<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.EmployeeFilter" %>
<ext:Panel runat="server" Layout="HBoxLayout" Width="130">
    <Items>
        <ext:ComboBox runat="server" ID="employeeFilter" Width="120" LabelAlign="Top"
            DisplayField="fullName"
            ValueField="recordId" AllowBlank="true"
            TypeAhead="false"
            HideTrigger="true" SubmitValue="true"
            MinChars="3" EmptyText="<%$ Resources: FilterEmployee%>"
            TriggerAction="Query" ForceSelection="false">
            <Store>
                <ext:Store runat="server" ID="Store2" AutoLoad="false">
                    <Model>
                        <ext:Model runat="server">
                            <Fields>
                                <ext:ModelField Name="recordId" />
                                <ext:ModelField Name="fullName" />
                            </Fields>
                        </ext:Model>
                    </Model>
                    <Proxy>
                        <ext:PageProxy DirectFn="App.direct.FillEmployee"></ext:PageProxy>
                    </Proxy>
                </ext:Store>
            </Store>
         

        </ext:ComboBox>
    </Items>
</ext:Panel>
