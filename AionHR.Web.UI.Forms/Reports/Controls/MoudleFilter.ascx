<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MoudleFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.MoudleFilter" %>
<script type="text/javascript" runat="server">
 
   

</script>
<ext:Panel runat="server" Layout="HBoxLayout" Width="150"  ><Items>
  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"   ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="key" DisplayField="value" ID="modulesCombo"  EmptyText="<%$ Resources:Common ,Module%>"  MaxWidth=150 >
      <Store>
      <ext:Store runat="server" ID="modulesStore">
                    <Model>
                        <ext:Model runat="server" >
                            <Fields>
                                <ext:ModelField Name="key" />
                                <ext:ModelField Name="value" />
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
    </Store>
        
      </ext:ComboBox>
</Items></ext:Panel>