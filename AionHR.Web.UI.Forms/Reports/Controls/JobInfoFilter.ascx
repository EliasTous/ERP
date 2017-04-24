<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobInfoFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.JobInfoFilter" %>

<ext:Panel runat="server" Layout="HBoxLayout" Width="710">
    <Items>
        <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="departmentId" Name="departmentId" EmptyText="<%$ Resources:FieldDepartment%>">
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
        <ext:Panel runat="server" Width="10" />
        <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="branchId" Name="branchId" EmptyText="<%$ Resources:FieldBranch%>">
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
         <ext:Panel runat="server" Width="10" />
         <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="divisionId" Name="divisionId" EmptyText="Division">
            <Store>
                <ext:Store runat="server" ID="divisionStore">
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
         <ext:Panel runat="server" Width="10" />
        <ext:ComboBox runat="server" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="positionId" Name="positionId" EmptyText="<%$ Resources:FieldPosition%>">
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
       
    </Items>
</ext:Panel>
