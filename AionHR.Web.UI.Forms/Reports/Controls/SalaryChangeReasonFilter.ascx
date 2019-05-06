<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalaryChangeReasonFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.SalaryChangeReasonFilter" %>

  
       
        <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local" Width="140" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="recordId" DisplayField="name" ID="scrId">
            <Store>
                <ext:Store runat="server" ID="scrStore">
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
     

