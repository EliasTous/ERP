<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DepartmentFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.DepartmentFilter" %>

        <ext:ComboBox runat="server" QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="departmentId"  FieldLabel="<%$ Resources:Common,Departments%>">
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
            <Triggers>
                <ext:FieldTrigger Icon="Clear" />
            </Triggers>
            <Listeners>
                <TriggerClick Handler="
                                       this.clearValue();" />
            </Listeners>

        </ext:ComboBox>
     
