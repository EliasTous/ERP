<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetCategoryFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.AssetCategoryFilter" %>
<ext:Panel runat="server" Layout="FitLayout"  ID="categoryIdPanel">
    <Items>
    
        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="categoryId" EmptyText="<%$ Resources: Common ,FieldCategory%>" >
            <Store>
                <ext:Store runat="server" ID="categoryIdStore">
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