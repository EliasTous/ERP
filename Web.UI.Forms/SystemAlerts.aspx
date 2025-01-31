﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemAlerts.aspx.cs" Inherits="Web.UI.Forms.SystemAlerts" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/SystemAlerts.js?id=1"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" >
    function dump(obj) {
         var out = '';
         for (var i in obj) {
             out += i + ": " + obj[i] + "\n";


         }
         return out;
     }
        </script>
</head>
<body style="background: url(Images/bg.png) repeat;">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
        <ext:Hidden ID="alert1Title" runat="server" Text="<%$ Resources: Alert1Title %>" />
        <ext:Hidden ID="alert2Title" runat="server" Text="<%$ Resources: Alert2Title %>" />
        <ext:Hidden ID="alert3Title" runat="server" Text="<%$ Resources: Alert3Title %>" />
        <ext:Hidden ID="alert4Title" runat="server" Text="<%$ Resources: Alert4Title %>" />
        <ext:Hidden ID="alert5Title" runat="server" Text="<%$ Resources: Alert5Title %>" />
        <ext:Hidden ID="alert6Title" runat="server" Text="<%$ Resources: Alert6Title %>" />
        <ext:Hidden ID="alert7Title" runat="server" Text="<%$ Resources: Alert7Title %>" />
        <ext:Hidden ID="alert8Title" runat="server" Text="<%$ Resources: Alert8Title %>" />
        <ext:Hidden ID="alert9Title" runat="server" Text="<%$ Resources: Alert9Title %>" />
        <ext:Hidden ID="alert10Title" runat="server" Text="<%$ Resources: Alert10Title %>" />
         <ext:Hidden ID="alert11Title" runat="server" Text="<%$ Resources: Alert11Title %>" />
         

      <%--  <ext:Hidden ID="alert101Description" runat="server" Text="<%$ Resources: Alert101Description %>" />
        <ext:Hidden ID="alert301Description" runat="server" Text="<%$ Resources: Alert301Description %>" />
        <ext:Hidden ID="alert302Description" runat="server" Text="<%$ Resources: Alert302Description %>" />
        <ext:Hidden ID="alert303Description" runat="server" Text="<%$ Resources: Alert303Description %>" />
        <ext:Hidden ID="alert304Description" runat="server" Text="<%$ Resources: Alert304Description %>" />
        <ext:Hidden ID="alert305Description" runat="server" Text="<%$ Resources: Alert305Description %>" />
        <ext:Hidden ID="alert306Description" runat="server" Text="<%$ Resources: Alert306Description %>" />
        <ext:Hidden ID="alert307Description" runat="server" Text="<%$ Resources: Alert307Description %>" />
          <ext:Hidden ID="alert308Description" runat="server" Text="<%$ Resources: Alert308Description %>" />

        <ext:Hidden ID="alert309Description" runat="server" Text="<%$ Resources: Alert309Description %>" />
         <ext:Hidden ID="alert310Description" runat="server" Text="<%$ Resources: Alert310Description %>" />
         <ext:Hidden ID="alert311Description" runat="server" Text="<%$ Resources: Alert311Description %>" />--%>

        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App">
            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server" IDProperty="alertId">
                    <Fields>

                        <ext:ModelField Name="alertId" />
                        <ext:ModelField Name="isActive" />
                        <ext:ModelField Name="sendTo" />
                        <ext:ModelField Name="days" />
                        <ext:ModelField Name="predefined" />
                         <ext:ModelField Name="name" />
                        
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>



        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
              
                <ext:GridPanel
                    ID="GridPanel1"
                    runat="server"
                    StoreID="Store1"
                    PaddingSpec="0 0 1 0"
                    Header="false"
                    Title="<%$ Resources: WindowTitle %>"
                    Layout="FitLayout"
                    Scroll="Vertical"
                    Border="false"
                    Icon="User"
                    ColumnLines="True" IDMode="Explicit" RenderXType="True">




                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="alertId" Hideable="false" Width="75" Align="Center" />
                            <ext:Column Visible="false" ID="Column2" MenuDisabled="true" runat="server" DataIndex="name" Hideable="false" Width="75" Align="Center" />
                    
                            <ext:WidgetColumn ID="Column1" MenuDisabled="true"  runat="server" Text="<%$ Resources: FieldActive %>" DataIndex="isActive" Hideable="false" Width="75" Align="Center">
                                <Widget>
                                    <ext:Checkbox runat="server" Name="isActive"  ID="chk">
                                        <Listeners>
                                            
                                         <Change Handler="var rec = this.getWidgetRecord(); rec.set('isActive',this.value);" >
                                                
                                            </Change>
                                        </Listeners>
                                    </ext:Checkbox>

                                </Widget>
                              
                            </ext:WidgetColumn>
                                <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName %>" DataIndex="name" Hideable="false" Flex="1">
                              
                            </ext:Column>
                         <%--   <ext:WidgetColumn ID="WidgetColumn1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSendTo %>" DataIndex="sendTo" Hideable="false" Width="150" Align="Center">
                                <Widget>
                                    <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  runat="server" Editable="false" ID="to" >
                                        <Items>
                                            <ext:ListItem Text="<%$ Resources: All %>" Value="2" />
                                            <ext:ListItem Text="<%$ Resources: Administrator %>" Value="0" />
                                            <ext:ListItem Text="<%$ Resources: User %>" Value="1" />
                                        </Items>
                                        <Listeners>
                                            <Select Handler="var rec = this.getWidgetRecord(); rec.set('sendTo',this.value); " />
                                        </Listeners>
                                    </ext:ComboBox>

                                </Widget>
                                
                         </ext:WidgetColumn>--%>
                             <ext:WidgetColumn ID="WidgetColumn2" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDays %>" DataIndex="days" Hideable="false" Width="125" Align="Center">
                                <Widget>
                                   
                                    <ext:NumberField runat="server" MinValue="0" >
                                        <Listeners>
                                            <Change Handler="var rec = this.getWidgetRecord(); rec.set('days',this.value);" />
                                        </Listeners>
                                        </ext:NumberField>

                                </Widget>

                            </ext:WidgetColumn>
                        
<%--                            <ext:Column MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDescription %>" DataIndex="alertId" Hideable="false" Flex="1">
                                <Renderer Handler="return GetAlertDescription(record.data['alertId']); " />
                            </ext:Column>--%>





                        </Columns>
                    </ColumnModel>
                    <DockedItems>

                        <ext:Toolbar ID="Toolbar2" runat="server" Dock="Bottom">
                            <Items>
                                <ext:StatusBar ID="StatusBar1" runat="server" />
                                <ext:ToolbarFill />

                            </Items>
                        </ext:Toolbar>

                    </DockedItems>


                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                    <Buttons>
                        <ext:Button runat="server" Text="<%$ Resources:Common, Save%>" >
                            <Listeners>
                                <Click Handler="CheckSession();"></Click>
                            </Listeners>
                            <DirectEvents>
                                <Click OnEvent="SaveAlerts" >
                                    <ExtraParams>
                                         <ext:Parameter Name="values" Value="Ext.encode(#{GridPanel1}.getRowsValues({dirtyRowsOnly : true}))" Mode="Raw"  />
                                    </ExtraParams>
                                    </Click>
                                
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button runat="server" Text="<%$ Resources:Common, Cancel%>" >

                        </ext:Button>

                    </Buttons>
                </ext:GridPanel>
             
            </Items>
            
        </ext:Viewport>







    </form>
</body>
</html>
