<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserComboFilter.ascx.cs" Inherits="AionHR.Web.UI.Forms.Reports.Controls.UserComboFilter" %>
 
<ext:Panel runat="server" Layout="HBoxLayout" Width="200">
    <Items>

         <ext:ComboBox runat="server" ID="userId" EnableViewState="true"
                                    DisplayField="fullName"
                                    ValueField="recordId"
                                    TypeAhead="false"
                                    EmptyText="<%$ Resources: FieldUser%>"
                                    HideTrigger="true" SubmitValue="true"
                                    MinChars="3" 
                                    TriggerAction="Query" ForceSelection="false">
                                    <Store>
                                        <ext:Store runat="server" ID="usersStore" AutoLoad="false">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="recordId" />
                                                        <ext:ModelField Name="fullName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.FillUsers"></ext:PageProxy>
                                            </Proxy>

                                        </ext:Store>

                                    </Store>
                                </ext:ComboBox>
        </Items>
        </ext:Panel>
