<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalaryTypeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.SalaryTypeFilter" %>


    <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"   ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="key" DisplayField="value" ID="salaryTypeId"  EmptyText="<%$ Resources:Common ,SalaryType%>" SubmitValue="true"   >
      <Store>
      <ext:Store runat="server" ID="salaryTypeStore">
                    <Model>
                        <ext:Model runat="server">
                            <Fields>
                                <ext:ModelField Name="key" />
                                <ext:ModelField Name="value" />
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
    </Store>
        
      </ext:ComboBox>

   
        
      
        
    
  