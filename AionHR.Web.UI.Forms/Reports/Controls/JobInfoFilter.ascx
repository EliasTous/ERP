<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobInfoFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.JobInfoFilter" %>
<script type="text/javascript" >
    function setStatus(de,br,po,di)
    {
        App.jobInfo1_departmentId.setHidden(!de);
        App.jobInfo1_branchId.setHidden(!br);
        App.jobInfo1_positionId.setHidden(!po);
        App.jobInfo1_divisionId.setHidden(!di);


    }

    function setWidth(width, id) {
        App.jobInfo1_departmentId.setWidth(width);
        App.jobInfo1_branchId.setWidth(width);
        App.jobInfo1_positionId.setWidth(width);
        App.jobInfo1_divisionId.setWidth(width);
    }
    function setDepartmentAllowBlank(allowBlank) {
        App.jobInfo1_departmentId.allowBlank = allowBlank;
      
    }
    function setBranchAllowBlank(allowBlank)
    {
        App.jobInfo1_branchId.allowBlank=allowBlank;
      
    }
    function setPositionAllowBlank(allowBlank)
    {
        App.jobInfo1_positionId.allowBlank=allowBlank;
       
    }
    function setDivisionAllowBlank(allowBlank)
    {
        App.jobInfo1_divisionId.allowBlank=allowBlank;
    }
</script>
<ext:Panel runat="server" Layout="HBoxLayout" >
    <Items>
        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"  Width="120" ForceSelection="true"  TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="departmentId"  EmptyText="<%$ Resources:FieldDepartment%>"  >
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
        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"    runat="server" QueryMode="Local" Width="120" ForceSelection="true"  TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="branchId"  EmptyText="<%$ Resources:FieldBranch%>" >
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
         <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local" Width="120"  ForceSelection="true"  TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="divisionId"   EmptyText="<%$ Resources:FieldDivision%>" >
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
        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local" Width="120"  ForceSelection="true"  TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="positionId" EmptyText="<%$ Resources:FieldPosition%>" >
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
