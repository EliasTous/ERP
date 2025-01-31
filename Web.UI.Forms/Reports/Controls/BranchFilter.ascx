﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BranchFilter.ascx.cs" Inherits="Web.UI.Forms.Reports.Controls.BranchFilter" %>
 <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  Scrollable="Both"   runat="server" QueryMode="Local" Width="120" ForceSelection="true"  TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="branchId"  FieldLabel="<%$ Resources:Common,Branches%>" >
     <ListConfig MaxHeight="150"></ListConfig>
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

     <Triggers>
                <ext:FieldTrigger Icon="Clear" />
            </Triggers>
            <Listeners>
                <TriggerClick Handler="
                                       this.clearValue();" />
            </Listeners>
        </ext:ComboBox>