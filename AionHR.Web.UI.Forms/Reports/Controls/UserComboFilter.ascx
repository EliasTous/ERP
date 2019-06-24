<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserComboFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.UserComboFilter" %>
 
   <ext:ComboBox runat="server" QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="fullName" ID="userId"  >
            <Store>
                <ext:Store runat="server" ID="userStore">
                    <Model>
                        <ext:Model runat="server">
                            <Fields>
                                <ext:ModelField Name="recordId" />
                                <ext:ModelField Name="fullName" />
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
