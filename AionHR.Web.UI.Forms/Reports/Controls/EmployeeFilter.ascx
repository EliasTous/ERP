<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.EmployeeFilter" %>

        <ext:ComboBox   AnyMatch="true"   CaseSensitive="false"  runat="server" ID="employeeId"  LabelAlign="Left"
            DisplayField="fullName"
            ValueField="recordId" AllowBlank="true"
            TypeAhead="false"
            HideTrigger="true"  SubmitValue="true"
            MinChars="3" EmptyText="<%$ Resources: FilterEmployee%>"
            TriggerAction="Query" ForceSelection="true">
            <Store>
                <ext:Store runat="server" ID="Store2" >
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

