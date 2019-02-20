<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.SupplierFilter" %>
       
        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="supplierId" EmptyText="<%$ Resources:FieldSupplier%>"  >
            <Store>
                <ext:Store runat="server" ID="supplierIdStore">
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
     
   