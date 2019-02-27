<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssetManagementOnboardings.aspx.cs" Inherits="AionHR.Web.UI.Forms.AssetManagementOnboardings" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Branches.js"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript" >
      
    </script>

</head>
<body style="background: url(Images/bg.png) repeat;" >
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />

        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
         <ext:Hidden ID="CurrentPositionId" runat="server"  />


     
                           
                             
    
                        
        <ext:Store
            ID="Store1"
            runat="server"
          RemoteSort="false"
            RemoteFilter="false"
            OnReadData="Store1_RefreshData"
            PageSize="40"  Namespace="App" IDMode="Explicit" >

            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>

                <ext:Model ID="Model1" runat="server" IDProperty="recordId">
                    <Fields>

                        <ext:ModelField Name="recordId" />
                        <ext:ModelField Name="name" />
                        <ext:ModelField Name="positionRef" />
                     
                      <%--  <ext:ModelField Name="tsId" />
                        <ext:ModelField Name="tsName" />--%>



                    </Fields>
                </ext:Model>
            </Model>
         <%--   <Sorters>
                <ext:DataSorter Property="recordId" Direction="ASC" />
            </Sorters>--%>
        </ext:Store>

         <ext:Store
            ID="OnboardingStore"
            runat="server"
            RemoteSort="True"
            RemoteFilter="true"
            OnReadData="OnboardingStore_RefreshData"
            PageSize="40"  Namespace="App" IDMode="Explicit" IsPagingStore="true">

            <Proxy>
                <ext:PageProxy>
                    <Listeners>
                        <Exception Handler="Ext.MessageBox.alert('#{textLoadFailed}.value', response.statusText);" />
                    </Listeners>
                </ext:PageProxy>
            </Proxy>
            <Model>

                <ext:Model ID="Model2" runat="server" IDProperty="categoryId">
                    <Fields>

                        <ext:ModelField Name="categoryName" />
                        <ext:ModelField Name="positionName" />
                        <ext:ModelField Name="deliveryDuration" />
                         <ext:ModelField Name="categoryId" />
                        <ext:ModelField Name="positionId" />
                      
          


                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="categoryId" Direction="ASC" />
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

                 <%--   <TopBar>
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
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                              <%--  <ext:Button ID="btnReload" runat="server"  Icon="Reload">       
                                     <Listeners>
                                        <Click Handler="CheckSession();#{Store1}.reload();" />
                                    </Listeners>                           
                                   
                                </ext:Button>
                              
                            

                            </Items>
                        </ext:Toolbar>

                    </TopBar>--%>

                        <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>

                            <ext:Column Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server"  DataIndex="recordId" />
                            <ext:Column ID="ColReference" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldReference%>" DataIndex="positionRef"  Width="150" Hideable="false" />
                            <ext:Column CellCls="cellLink" ID="ColName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldName%>" DataIndex="name" Flex="2" Hideable="false">
                            </ext:Column>
                           






                            <ext:Column runat="server"
                                ID="colDelete" Flex="1" Visible="false"
                                Text="<%$ Resources: Common , Delete %>"
                                Width="60"
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

                            <ext:Column runat="server" Flex="1"
                                ID="colEdit" Visible="true"
                                Text="<%$ Resources:Common, Edit %>"
                                Width="80"
                                Hideable="false"
                                Align="Center"
                                Fixed="true"
                                Filterable="false"
                                MenuDisabled="true"
                                Resizable="false">

                                <Renderer Handler="return editRender(); " />
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
                        <Render Handler="CheckSession(); this.on('cellclick', cellClick);" />


                        <AfterLayout Handler="item.getView().setScrollX(30);" />

                    </Listeners>
                    <DirectEvents>
                        <CellClick OnEvent="PoPuP">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                             <ext:Parameter Name="id" Value="record.data['recordId']" Mode="Raw" />
                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />

                                           
           
                            </ExtraParams>

                        </CellClick>
                    </DirectEvents>
                    <View>
                        <ext:GridView ID="GridView1" runat="server" />
                    </View>


                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" StopIDModeInheritance="true" />
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
                       
                          <ext:GridPanel 
                                    ID="OnboardingGrid"
                                    runat="server"
                                    StoreID="OnboardingStore"
                                    PaddingSpec="0 0 1 0"
                                    Header="false" 
                         
                                    Title=  "<%$ Resources: OnboardingWindowTitle %>"
                                    Layout="FitLayout"
                                    Scroll="Vertical"
                                    Border="false"  
                                    Icon="User"
                                    ColumnLines="True" IDMode="Explicit" RenderXType="True" >
                                      
                              <TopBar>
                                  
                        <ext:Toolbar ID="Toolbar3" runat="server" ClassicButtonStyle="false">
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
                                 <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:AssetCategoryControl runat="server" ID="categoryIdFilter"   />
                                            </Content>
                                        </ext:Container>
                            </Items>
                        </ext:Toolbar>

                    </TopBar>
                                            

                                        <ColumnModel ID="ColumnModel2" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                                            <Columns>
                                             
                                             
                                                <ext:Column ID="ColcategoryName" MenuDisabled="true" runat="server" Text="<%$ Resources:FieldCategoryName %>" DataIndex="categoryName" Flex="2"/>
                                                <ext:Column ID="ColDeliveryDuration" MenuDisabled="true" runat="server" Text="<%$ Resources:FieldDeliveryDuration %>" DataIndex="deliveryDuration"  Flex="2"/>
                          
                             
                        
                           

                                                            <ext:Column runat="server"
                                                                ID="Column3" Visible="false"
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
                                                            ID="Column4"
                                                            Visible="false"
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
                            
                                                           <ext:Column runat="server" Flex="1"
                                                                ID="Column5"  Visible="true"
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
              
                                                    <Listeners>
                                                        <Render Handler="this.on('cellclick', cellClick);" />
                                                        <Activate Handler="#{OnboardingStore}.reload();" />
                                                    </Listeners>
                                                    <DirectEvents>
                        
                                                        <CellClick OnEvent="PoPuPOB">
                                                            <EventMask ShowMask="true" />
                                                            <ExtraParams>
                                                               
                                                                     <ext:Parameter Name="positionId" Value="record.data['positionId']" Mode="Raw" />
                                 <ext:Parameter Name="categoryId" Value="record.data['categoryId']" Mode="Raw" />
                                 <ext:Parameter Name="deliveryDuration" Value="record.data['deliveryDuration']" Mode="Raw" />
                                                                <ext:Parameter Name="type" Value="getCellType( this, rowIndex, cellIndex)" Mode="Raw" />
                                                            </ExtraParams>

                                                        </CellClick>
                                                    </DirectEvents>
                                                    <View>
                                                        <ext:GridView ID="GridView2" runat="server" />
                                                    </View>

                  
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel ID="rowSelectionModel1" runat="server" Mode="Single"  StopIDModeInheritance="true" />
                       
                                                    </SelectionModel>
                            
                                                </ext:GridPanel>
                          

                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
             <%--   <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value="#{BasicInfoTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>--%>
                <ext:Button ID="CancelButton" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

          <ext:Window 
            ID="EditOnBoardingRecordWindow"
            runat="server"
            Icon="PageEdit"
            Title="<%$ Resources:EditOnBoardingRecordWindowsTitle %>"
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
                            ID="OnBoardingTab" DefaultButton="SaveButton"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditOnBoardingWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                              
                               <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:AssetCategoryControl runat="server" ID="AssetCategoryControl" FieldLabel="<%$ Resources: Common ,FieldCategory%>"   />
                                            </Content>
                                        </ext:Container>
                                  <ext:NumberField ID="deliveryDuration" runat="server" FieldLabel="<%$ Resources:FieldDeliveryDuration%>" Name="deliveryDuration"   AllowBlank="false" MinValue="0"/>
                                <%--<ext:TextField ID="intName" runat="server" FieldLabel="<%$ Resources:IntName%>" Name="intName"   AllowBlank="false"/>--%>
                            </Items>

                        </ext:FormPanel>
                        
                    </Items>
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{OnBoardingTab}.getForm().isValid()) {return false;}  " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditOnBoardingRecordWindow}.body}" />
                            <ExtraParams>
                             
                                <ext:Parameter Name="values" Value ="#{OnBoardingTab}.getForm().getValues()" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button2" runat="server" Text="<%$ Resources:Common , Cancel %>" Icon="Cancel">
                    <Listeners>
                        <Click Handler="this.up('window').hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>



    </form>
</body>
</html>

