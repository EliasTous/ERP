<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeVariationTypeFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.TimeVariationTypeFilter" %>

<ext:Panel runat="server" Layout="HBoxLayout"   ><Items>
   
     <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" QueryMode="Local"  Width="120" ForceSelection="true" TypeAhead="true" MinChars="1" ValueField="key" DisplayField="value" ID="timeVariationType"   EmptyText="<%$ Resources:Common , FieldTimeVariationType%>" Name="timeVariationType" >
            <Store>
                <ext:Store runat="server" ID="timeVariationStore">
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
  
                                   
                                    
                               
</Items></ext:Panel>