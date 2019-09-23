<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeVariationHistoryControl.ascx.cs" Inherits="AionHR.Web.UI.Forms.TimeVariationHistoryControl" %>
<link rel="stylesheet" type="text/css" href="CSS/Common.css" />
<link rel="stylesheet" href="CSS/LiveSearch.css" />



<script type="text/javascript">





    
    


      
  
</script>
         

<ext:Hidden runat="server" ID="currentClassId" />
<ext:Hidden runat="server" ID="currentMasterRef" />


     <ext:Store
            ID="TimeVariationHistoryStore"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="TimeVariationHistory_RefreshData"
            PageSize="50" IDMode="Explicit" Namespace="App">
        
            <Model>
                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />                       
                        <ext:ModelField Name="eventDt" />
                          <ext:ModelField Name="ttName" />
                          <ext:ModelField Name="userName" />
                          <ext:ModelField Name="data" />
                      
                        

                      
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>
        </ext:Store>

  <ext:Window 
            ID="TimeVariationHistoryWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:TimeVariationHistoryWindow %>"
            Width="650"
           
      MinHeight="350"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit" >
            
            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        

                        <ext:GridPanel
                            ID="TimeVariationHistoryGrid"
                            runat="server"
                            PaddingSpec="0 0 1 0"
                            Header="false"
                            MaxHeight="350"
                            Layout="FitLayout"
                            Scroll="Vertical"
                            Border="false"
                            StoreID="TimeVariationHistoryStore"
                              Title="<%$ Resources:TimeVariationHistoryWindow %>"
                            ColumnLines="True" IDMode="Explicit" RenderXType="True" >
                           
                            <%--      <TopBar>
                                    <ext:Toolbar ID="Toolbar3" runat="server">
                                     <Items>
                                         
                                    <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Add %>" Icon="Add">       
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
                            
                    </TopBar>--%>
                        

                            
                            <ColumnModel ID="ColumnModel4" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="true">
                                <Columns>
                                    <ext:Column ID="ColRecordId" Visible="false" DataIndex="recordId" runat="server" />

                                   
                                    <ext:DateColumn ID="ColEventDt" DataIndex="eventDt" Text="<%$ Resources: eventDt%>" runat="server" Flex="1"/>
                                    <ext:Column ID="ColTtName" DataIndex="ttName" Text="<%$ Resources: ttName%>" runat="server" Flex="1"/>
                                   <ext:Column ID="ColUserName" DataIndex="userName" Text="<%$ Resources: userName%>" runat="server" Flex="1"/>
                                  

                           
                                   
                                   
                                    
                            <ext:Column runat="server"
                                ID="Column1"  Visible="true"
                                Text=""
                                Width="100"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer handler="return editRender() ; " />

                            </ext:Column>


                                </Columns>
                            </ColumnModel>

                          
                          <Listeners>
                             
                        <Render Handler="this.on('cellclick', cellClick);" />
                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                               
                                <ext:Parameter Name="data" Value="record.data['data']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
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
                </ext:TabPanel>
            </Items>
         
        </ext:Window>

   <ext:Window 
            ID="EditRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditWindow %>"
            Width="450"
            Height="330"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                        <ext:FormPanel
                            ID="TimeVariationHistoryForm" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: EditWindow %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" 
                            BodyPadding="5">
                            <Items>
                              <ext:TextArea ID="data"  runat="server"   Name="data" ReadOnly="true" MinHeight="250"  />
                               
                               
                               
                                
                                  
                            </Items>
                          
                        </ext:FormPanel>
                          

                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
           <%--     <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession();  if (!#{TimeVariationHistoryForm}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                
                                <ext:Parameter Name="value" Value="#{TextField8}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{TimeVariationHistoryForm}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>--%>
                <ext:Button ID="Button3" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>