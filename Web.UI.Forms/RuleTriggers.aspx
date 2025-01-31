﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RuleTriggers.aspx.cs" Inherits="Web.UI.Forms.RuleTriggers" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/CertificateLevels.js" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
   <script type="text/javascript">
         function AddSource(items) {
          
            var fromStore = App.ruleSelector.fromField.getStore();
            var toStore = App.ruleSelector.toField.getStore();

            while (fromStore.getCount() > 0)
                fromStore.removeAt(0);



            for (i = 0; i < items.length; i++) {
                if (fromStore.getById(items[i].recordId) == null && toStore.getById(items[i].recordId) == null) {

                    fromStore.add(items[i]);
                }

            }
        }

         function SwapRTL() {
            if (App.isRTL.getValue() == 'true') {

                $(".x-form-itemselector-add").css('background-image', 'url(ux/resources/images/itemselector/left-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(ux/resources/images/itemselector/right-gif/ext.axd)');

            }
            else {

                $(".x-form-itemselector-add").css('background-image', 'url(ux/resources/images/itemselector/right-gif/ext.axd)');
                $(".x-form-itemselector-remove").css('background-image', 'url(ux/resources/images/itemselector/left-gif/ext.axd)');

            }
        }
   </script>
 
</head>
<body style="background: url(Images/bg.png) repeat;" ">
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
            <ext:Hidden ID="CurrentModule" runat="server"  />
           <ext:Hidden ID="CurrentRuleId" runat="server"  />
        
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
                <ext:Model ID="Model1" runat="server" >
                    <Fields>

                        <ext:ModelField Name="recordId" />
                        
                        <ext:ModelField Name="className" />
                          <ext:ModelField Name="ruleName" />
                          <ext:ModelField Name="accessTypeName" />
                           <ext:ModelField Name="ruleId" />
                          <ext:ModelField Name="accessType" />
                          <ext:ModelField Name="classId" />
                         <ext:ModelField Name="seqNo" />
                        
                        
                        <%--<ext:ModelField Name="reference" />--%>
                      
                               </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="className" Direction="ASC" />
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

                    <TopBar>
                        
                        <ext:Toolbar ID="Toolbar1" runat="server" ClassicButtonStyle="false">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
                                     <Listeners>
                                        <Click Handler="CheckSession();" />
                                    </Listeners>                           
                                    <DirectEvents>
                                        <Click OnEvent="ADDNewRecord">
                                            <EventMask ShowMask="true" CustomTarget="={#{GridPanel1}.body}" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                               
                              
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                            <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />
                            <ext:Column    CellCls="cellLink" ID="ColClassName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldClass%>" DataIndex="className" Flex="1" Hideable="false" />
                            <ext:Column    CellCls="cellLink" ID="ColRuleName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldRule%>" DataIndex="ruleName" Flex="1" Hideable="false" />
                            <ext:Column    CellCls="cellLink" ID="ColAccessTypeName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldAccessType%>" DataIndex="accessTypeName" Flex="1" Hideable="false" />
                       
                            <%--<ext:Column ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldReference%>" DataIndex="reference" Width ="300" Hideable="false" />--%>
                             
                        
                           

                            <ext:Column runat="server"
                                ID="colDelete" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="100"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                Hideable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="deleteRender" />
                              
                            </ext:Column>
                            <ext:Column runat="server"
                                ID="colAttach"
                                Text="<%$ Resources:Common, Attach %>"
                                Hideable="false"
                                Width="60"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">
                                <Renderer Fn="attachRender" />
                            </ext:Column>
                            
                           <ext:Column runat="server"
                                ID="colEdit"  Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return editRender()+'&nbsp;&nbsp;' +deleteRender(); " />

                            </ext:Column>



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
                    <BottomBar>

                        <ext:PagingToolbar ID="PagingToolbar1"
                            runat="server"
                            FirstText="<%$ Resources:Common , FirstText %>"
                            NextText="<%$ Resources:Common , NextText %>"
                            PrevText="<%$ Resources:Common , PrevText %>"
                            LastText="<%$ Resources:Common , LastText %>"
                            RefreshText="<%$ Resources:Common ,RefreshText  %>"
                            BeforePageText="<%$ Resources:Common ,BeforePageText  %>"
                            AfterPageText="<%$ Resources:Common , AfterPageText %>"
                            DisplayInfo="true"
                            DisplayMsg="<%$ Resources:Common , DisplayMsg %>"
                            Border="true"
                            EmptyMsg="<%$ Resources:Common , EmptyMsg %>">
                            <Items>
                               
                            </Items>
                            <Listeners>
                                <BeforeRender Handler="this.items.removeAt(this.items.length - 2);" />
                            </Listeners>
                        </ext:PagingToolbar>

                    </BottomBar>
                    <Listeners>
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="accessType" Value="record.data['accessType']" Mode="Raw" />
                                   <ext:Parameter Name="ruleId" Value="record.data['ruleId']" Mode="Raw" />
                                   <ext:Parameter Name="classId" Value="record.data['classId']" Mode="Raw" />
                                 <ext:Parameter Name="className" Value="record.data['className']" Mode="Raw" />
                                 <ext:Parameter Name="seqNo" Value="record.data['seqNo']" Mode="Raw" />
                                
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>

                  
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single"  StopIDModeInheritance="true" />
                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>

        

        <ext:Window 
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindowsTitle %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel runat="server" ID="triggersForm" Layout="FitLayout" Title="<%$ Resources:Triggers%>">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Container runat="server" Layout="FitLayout"  >
                                    <Content>
                                        <uc:moduleCombo runat="server" ID="modulesCombo1" />
                                    </Content>
                                </ext:Container>
                                    


                                     
                                    </Items>

                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:GridPanel
                                    ID="classesGrid"
                                    runat="server"
                                    PaddingSpec="0 0 1 0"
                                    Header="false"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"
                                    Icon="User" 
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True">

                                    <Store>
                                        <ext:Store runat="server" ID="classesStore" OnReadData="classesStore_ReadData" AutoLoad="true">
                                            <Model>
                                                <ext:Model runat="server" IDProperty="id">
                                                    <Fields>
                                                        <ext:ModelField Name="id" />
                                                      
                                                       
                                                        <ext:ModelField Name="className" />
                                                            <ext:ModelField Name="classId" />
                                                         

                                                        
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>



                                    <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false">
                                        <Columns>

                                            <ext:Column Visible="false" ID="Column3" MenuDisabled="true" runat="server" DataIndex="classId" Hideable="false" Width="75" />
                                            <ext:Column ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources:Class%>" DataIndex="className" Hideable="false" Flex="1" />
                                   
                                               
                                          
                                            <ext:Column runat="server"
                                                ID="Column8" Visible="true"
                                                Text=""
                                                Width="90"
                                                Hideable="false"
                                                Align="Center"
                                                Fixed="true"
                                                Filterable="false"
                                                MenuDisabled="true"
                                                Resizable="false">

                                             <Renderer Handler="return  editRender();" />
                                            </ext:Column>

                                        </Columns>
                                    </ColumnModel>
                                    <DockedItems>

                                        <ext:Toolbar ID="Toolbar7" runat="server" Dock="Bottom">
                                            <Items>
                                                <ext:StatusBar ID="StatusBar4" runat="server" />
                                                <ext:ToolbarFill />

                                            </Items>
                                        </ext:Toolbar>

                                    </DockedItems>
                                    <Listeners>
                                        <Render Handler="this.on('cellclick', cellClick);" />
                                    </Listeners>
                                    <DirectEvents>
                                        <CellClick OnEvent="PoPuPClass">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>
                                                <ext:Parameter Name="classId" Value="record.data['classId']" Mode="Raw" />
                                                  <ext:Parameter Name="className" Value="record.data['className']" Mode="Raw" />
                                           
                                               
                                            </ExtraParams>

                                        </CellClick>
                                    </DirectEvents>

                                    <View>
                                        <ext:GridView ID="GridView4" runat="server" />
                                    </View>


                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelectionModel3" runat="server" Mode="Single" StopIDModeInheritance="true" />
                                        <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" StopIDModeInheritance="true" />--%>
                                    </SelectionModel>

                                </ext:GridPanel>
                            </Items>
                          
                        </ext:FormPanel>
                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    <ext:Window
            ID="TriggerWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources: Trigger %>"
            Width="550"
            Height="450"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">

            <Items>

                <ext:FormPanel
                    ID="TriggerForm" DefaultButton="SaveTriggerButton"
                    runat="server" Header="false"
                    Icon="ApplicationSideList"
                    DefaultAnchor="100%"
                    BodyPadding="5">
                 
                    <Items>

                        <ext:TextField ID="classSelectedId"  runat="server"  Name="classSelectedId"  Hidden="true"/>
                       
                        <ext:TextField ID="className"  runat="server"  Name="className" FieldLabel="<%$ Resources: FieldClass%>"  ReadOnly="true"  />

                        <ext:ComboBox  AnyMatch="true" CaseSensitive="false"  QueryMode="Local"  ForceSelection="true" TypeAhead="true" MinChars="1" FieldLabel="<%$ Resources: FieldAccessType%>"  runat="server" DisplayField="value" ValueField="key"   Name="accessType" ID="accessType" AllowBlank="false"  >
                                             <Store>
                                                <ext:Store runat="server" ID="accessTypeStore">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="value" />
                                                                <ext:ModelField Name="key" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                            <Listeners>
                                <Select Handler="App.ruleSelectorStore.reload(); App.ruleSelector.setDisabled(false);" />
                            </Listeners>
                                       </ext:ComboBox>
                        <ext:ItemSelector runat="server"  MaxHeight="300" MinHeight="300" AutoScroll="true" ID="ruleSelector" FromTitle="<%$Resources:All %>" DisplayField="name" ValueField="recordId"
                            ToTitle="<%$Resources:Selected %>" SubmitValue="true" SimpleSubmit="true"  >
                          <%--  <Listeners>
                                <AfterRender Handler="SwapRTL(); " />
                            </Listeners>--%>
                            <Store>
                                <ext:Store runat="server" ID="ruleSelectorStore" OnReadData="ruleSelectorStore_ReadData">
                                    <Model>
                                        <ext:Model runat="server" IDProperty="recordId">
                                            <Fields>
                                                <ext:ModelField Name="name" />
                                                <ext:ModelField Name="recordId" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                      
                        </ext:ItemSelector>

                    </Items>

                </ext:FormPanel>


            </Items>
            <Buttons>
             <ext:Button ID="Button5" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{TriggerForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="saveNewTrigger" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{TriggerWindow}.body}" />
                            <ExtraParams>
                              <ext:Parameter Name="classId" Value="#{classSelectedId}.getValue()" Mode="Raw" />
                                  <ext:Parameter Name="accessType" Value="#{accessType}.getValue()" Mode="Raw" />

                                <ext:Parameter Name="values" Value="#{TriggerForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button6" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>


    </form>
</body>
</html>