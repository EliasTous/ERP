<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssetManagementPurchaseOrders.aspx.cs" Inherits="AionHR.Web.UI.Forms.AssetManagementPurchaseOrders" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/Common.css" />
    <link rel="stylesheet" href="CSS/LiveSearch.css" />
    <script type="text/javascript" src="Scripts/Branches.js?id=1" ></script>
    <script type="text/javascript" src="Scripts/common.js" ></script>
 
    
       
  

 
</head>
<body style="background: url(Images/bg.png) repeat;" >
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="1200000" />        
        
        <ext:Hidden ID="textMatch" runat="server" Text="<%$ Resources:Common , MatchFound %>" />
        <ext:Hidden ID="textLoadFailed" runat="server" Text="<%$ Resources:Common , LoadFailed %>" />
        <ext:Hidden ID="titleSavingError" runat="server" Text="<%$ Resources:Common , TitleSavingError %>" />
        <ext:Hidden ID="titleSavingErrorMessage" runat="server" Text="<%$ Resources:Common , TitleSavingErrorMessage %>" />
      
       
        
      
      
        <ext:Store
            ID="Store1"
            runat="server"
            RemoteSort="False"
            RemoteFilter="true"
            OnReadData="Store1_RefreshData"
            PageSize="30" IDMode="Explicit" Namespace="App" >
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
                        <ext:ModelField Name="supplierName" />
                        <ext:ModelField Name="branchName" />
                        <ext:ModelField Name="categoryName" />
                        <ext:ModelField Name="currencyName" />
                        <ext:ModelField Name="date" />
                         <ext:ModelField Name="categoryId" />
                        <ext:ModelField Name="qty" />
                          <ext:ModelField Name="supplierId" />
                         <ext:ModelField Name="comments"  />
                      <ext:ModelField Name="apStatus"  />
                         <ext:ModelField Name="status" />
                         <ext:ModelField Name="currencyId"  />
                      <ext:ModelField Name="costPrice"  />
      
       


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
                                <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Button ID="btnReload" runat="server"  Icon="Reload">       
                                     <Listeners>
                                        <Click Handler="CheckSession();#{Store1}.reload();" />
                                    </Listeners>                           
                                   
                                </ext:Button>

                                  <ext:ToolbarSeparator></ext:ToolbarSeparator>
                                <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:AssetCategoryControl runat="server" ID="categoryIdFilter" />
                                            </Content>
                                        </ext:Container>
                           
                                <ext:ToolbarSeparator runat="server" />
                                  <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:SupplierControl runat="server" ID="supplierIdFilter"      />
                                            </Content>
                                        </ext:Container>
                                <ext:ToolbarSeparator runat="server" />
                                 <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:jobInfo runat="server" ID="jobInfo1" EnableBranch="true" EnableDepartment="false" EnablePosition="false" EnableDivision="false"   />
                                            </Content>
                                        </ext:Container>
                                 <ext:ToolbarSeparator runat="server" />
                                  <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:ApprovalStatusControl runat="server" ID="apStatusFilter"   />
                                            </Content>
                                        </ext:Container>
                                <ext:ToolbarSeparator runat="server" />
                                  <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="statusFilter"   runat="server" EmptyText="<%$ Resources:FieldStatus%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                        <ext:ListItem Text="<%$ Resources: Status0%>" Value="0"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Status1%>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Status2%>" Value="2"></ext:ListItem>

                                    </Items>
                                   
                                </ext:ComboBox>

                                  <ext:ToolbarSeparator runat="server" />
                           
                             
                          
                              
                                <ext:Button runat="server" Text="<%$Resources:Common, Go %>">
                                            <Listeners>
                                                <Click Handler=" App.Store1.reload();" />
                                            </Listeners>
                                        </ext:Button>
                            
                              
                            
                            </Items>
                        </ext:Toolbar>

                    </TopBar>

                    <ColumnModel ID="ColumnModel1" runat="server" SortAscText="<%$ Resources:Common , SortAscText %>" SortDescText="<%$ Resources:Common ,SortDescText  %>" SortClearText="<%$ Resources:Common ,SortClearText  %>" ColumnsText="<%$ Resources:Common ,ColumnsText  %>" EnableColumnHide="false" Sortable="false" >
                        <Columns>
                                                                 
                    
                              <ext:Column  Visible="false" ID="ColrecordId" MenuDisabled="true" runat="server" DataIndex="recordId" />
                            <ext:Column ID="ColSupplierName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldSupplierName%>" DataIndex="supplierName"  Hideable="false" Flex="1"/>
                            <ext:Column   ID="ColCategoryName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCategoryName%>" DataIndex="categoryName" Flex="1" />
                               <ext:Column   ID="Column1" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldBranch %>" DataIndex="branchName" Flex="1" />
                             <ext:Column   ID="ColCurrencyName" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCurrencyName%>" DataIndex="currencyName" Flex="1" />
                            <ext:DateColumn  ID="Coldate" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldDate%>" DataIndex="date" Flex="1" Hideable="false" />
                        
                               
                      
                            <ext:Column    ID="Colqty" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldQty%>" DataIndex="qty" Flex="1" />
                             <ext:Column    ID="ColCostPrice" MenuDisabled="true" runat="server" Text="<%$ Resources: FieldCostPrice%>" DataIndex="costPrice" Flex="1" />
                        
                           
                        
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
                                <ext:Parameter Name="id" Value="record.getId()" Mode="Raw" />
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
            Height="450"
            AutoShow="false"
            Modal="true"
            Hidden="true"
            Layout="Fit">
            
            <Items>
                <ext:TabPanel ID="panelRecordDetails" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                    <Items>
                       <ext:FormPanel DefaultButton="SaveButton"
                            ID="BasicInfoTab"
                            runat="server"
                            Title="<%$ Resources: BasicInfoTabEditWindowTitle %>"
                            Icon="ApplicationSideList"
                            DefaultAnchor="100%" OnLoad="BasicInfoTab_Load"
                            BodyPadding="5">
                            <Items>
                                <ext:TextField ID="recordId" Hidden="true" runat="server"  Disabled="true" Name="recordId" />
                                      <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:SupplierControl runat="server" ID="supplierId"  FieldLabel="<%$ Resources:FieldSupplierName%>"    />
                                            </Content>
                                        </ext:Container>    
                                    <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:AssetCategoryControl runat="server" ID="categoryId" FieldLabel="<%$ Resources:FieldCategoryName%>"  />
                                            </Content>
                                        </ext:Container>                             
                                
                               <ext:DateField ID="date" runat="server" FieldLabel="<%$ Resources:FieldDate%>" Name="date"  />
                               <ext:NumberField ID="qty" runat="server" FieldLabel="<%$ Resources:FieldQty%>" Name="qty" MinValue="0" />
                                  <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:ApprovalStatusControl  runat="server" ID="apStatus" FieldLabel="<%$ Resources:Common, FieldApprovalStatus %>"  FieldType="Form" />
                                            </Content>
                                        </ext:Container>  
                                  <ext:Container runat="server"  Layout="FitLayout">
                                            <Content>
                                             
                                                <uc:CurrencyControl  runat="server" ID="CurrencyControl" FieldLabel="<%$ Resources: FieldCurrencyName%>"  />
                                            </Content>
                                        </ext:Container>  
                                         
                                   <ext:ComboBox   AnyMatch="true" CaseSensitive="false"  ID="status" Name="status"  runat="server" FieldLabel="<%$ Resources:FieldStatus%>" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1">
                                    <Items>

                                      
                                        <ext:ListItem Text="<%$ Resources: Status1%>" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="<%$ Resources: Status2%>" Value="2"></ext:ListItem>

                                    </Items>
                                    
                                </ext:ComboBox>
                                 <ext:ComboBox      AnyMatch="true" CaseSensitive="false"  runat="server" AllowBlank="false"   ValueField="recordId" QueryMode="Local" ForceSelection="true" TypeAhead="true" MinChars="1" DisplayField="name" ID="branchId" Name="branchId" FieldLabel="<%$ Resources:FieldBranch%>" SubmitValue="true" >
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
                                               
                                              
                                            </ext:ComboBox> 
                                      <ext:TextArea ID="comments" runat="server" FieldLabel="<%$ Resources:FieldComments%>" Name="comments" />
                                 <ext:NumberField ID="costPrice" runat="server" FieldLabel="<%$ Resources:FieldCostPrice%>" Name="costPrice" MinValue="0"  />
                            </Items>

                        </ext:FormPanel>
                     
                    </Items>
                    
                </ext:TabPanel>
            </Items>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="<%$ Resources:Common, Save %>" Icon="Disk">

                    <Listeners>
                        <Click Handler="CheckSession(); if (!#{BasicInfoTab}.getForm().isValid()) {return false;} " />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="SaveNewRecord" Failure="Ext.MessageBox.alert('#{titleSavingError}.value', '#{titleSavingErrorMessage}.value');">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EditRecordWindow}.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="#{recordId}.getValue()" Mode="Raw" />
                                <ext:Parameter Name="values" Value ="#{BasicInfoTab}.getForm().getValues(false, false, false, true)" Mode="Raw" Encode="true" />
                             
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

         

        


    </form>
</body>
</html>
