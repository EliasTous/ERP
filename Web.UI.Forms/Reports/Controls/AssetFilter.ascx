﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.AssetFilter" %>

    
        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="assetId" EmptyText="<%$ Resources: Common ,Assets%>" >
            <Store>
                <ext:Store runat="server" ID="assetIdStore">
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
     
   